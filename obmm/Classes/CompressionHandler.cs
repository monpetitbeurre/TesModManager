/* This file is part of Oblivion Mod Manager.
 * 
 * Oblivion Mod Manager is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Oblivion Mod Manager is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using SevenZip.Compression.LZMA;

namespace OblivionModManager {
    public class SparseFileWriterStream : Stream {
        private long position=0;
        private long length;
        
        private BinaryReader FileList;

        private string BaseDirectory;
        private string CurrentFile;
        private uint FileCRC;
        private long FileLength;
        private long Written;
        private FileStream CurrentOutputStream=null;

        public string GetBaseDirectory() {
            return BaseDirectory;
        }

        public SparseFileWriterStream(Stream fileList) {
            FileList=new BinaryReader(fileList);
            BaseDirectory=Program.CreateTempDirectory();
            CreateDirectoryStructure();
            NextFile();
        }

        private void CreateDirectoryStructure() {
            long TotalLength=0;
            while(FileList.PeekChar()!=-1) {
                string Path=FileList.ReadString();
                FileList.ReadInt32();
                TotalLength+=FileList.ReadInt64();
                int upto=0;
                while(true) {
                    int i=Path.IndexOf('\\', upto);
                    if(i==-1) break;
                    string directory=Path.Substring(0, i);
                    if(!Directory.Exists(BaseDirectory+directory)) Directory.CreateDirectory(BaseDirectory+directory);
                    upto=i+1;
                }
            }
            length=TotalLength;
            FileList.BaseStream.Position=0;
        }

        private void NextFile() {
            CurrentFile=FileList.ReadString();
            FileCRC=FileList.ReadUInt32();
            FileLength=FileList.ReadInt64();
            if(CurrentOutputStream!=null) CurrentOutputStream.Close();
            //Messy hack, but obmm wont let you have a .. in a relative path anyway
            //Triggering this would require someone to maliciously hex-edit the omod. Not really something that needs worrying about.
            if(!Program.IsSafeFileName(CurrentFile)) {
                CurrentOutputStream=File.Create(Path.Combine(Program.TempDir,"IllegalFile"));
            } else {
                CurrentOutputStream=File.Create(Path.Combine(BaseDirectory,CurrentFile));
            }
            Written=0;
        }

        public override long Position {
            get { return position; }
            set { throw new NotImplementedException("The SparseFileStream does not support seeking"); }
        }
        public override long Length { get { return length; } }
        public override bool CanRead { get { return false; } }
        public override bool CanSeek { get { return false; } }
        public override bool CanWrite { get { return true; } }

        public override int Read(byte[] buffer, int offset, int count) {
            throw new NotImplementedException("The SparseFileStream does not support reading");
        }

        public override void Write(byte[] buffer, int offset, int count) {
            while(Written+count>FileLength) {
                CurrentOutputStream.Write(buffer, offset, (int)(FileLength-Written));
                offset+=(int)(FileLength-Written);
                count-=(int)(FileLength-Written);
                NextFile();
            }
            if(count>0) {
                CurrentOutputStream.Write(buffer, offset, count);
                Written+=count;
            }
        }

        public override void SetLength(long length) {
            throw new NotImplementedException("The SparseFileStream does not support length");
        }

        public override long Seek(long offset, SeekOrigin origin) {
            throw new NotImplementedException("The SparseFileStream does not support seeking");
        }

        public override void Flush() { if(CurrentOutputStream!=null) CurrentOutputStream.Flush(); }

        public override void Close() {
            Flush();
            //Added this to properly create any empty files at the end of the archive
            while(FileList.BaseStream.Position<FileList.BaseStream.Length) {
                CurrentFile=FileList.ReadString();
                FileCRC=FileList.ReadUInt32();
                FileLength=FileList.ReadInt64();
                if(FileLength>0) throw new obmmException("Compressed data file stream didn't contain enough information to fill all files");
                if(CurrentOutputStream!=null) CurrentOutputStream.Close();
                if(!Program.IsSafeFileName(CurrentFile)) {
                    CurrentOutputStream=File.Create(Path.Combine(Program.TempDir, "IllegalFile"));
                } else {
                    CurrentOutputStream=File.Create(BaseDirectory+CurrentFile);
                }
            }
            if(CurrentOutputStream!=null) {
                CurrentOutputStream.Close();
                CurrentOutputStream=null;
            }
        }
    }

    public class SparseFileReaderStream : Stream {
        private long position=0;
        private long length;

        private string[] FilePaths;
        private int FileCount=0;
        private FileStream CurrentInputStream=null;
        private long CurrentFileEnd=0;
        private bool Finished=false;

        public string CurrentFile { get { return FilePaths[FileCount-1]; } }

        public SparseFileReaderStream(string[] filePaths) {
            length=0;
            foreach(string s in filePaths) {
                length+=(new FileInfo(s)).Length;
            }
            FilePaths=filePaths;
            NextFile();
        }

        private bool NextFile() {
            if(CurrentInputStream!=null) CurrentInputStream.Close();
            if(FileCount>=FilePaths.Length) {
                CurrentInputStream=null;
                Finished=true;
                return false;
            }
            CurrentInputStream=File.OpenRead(FilePaths[FileCount++]);
            CurrentFileEnd+=CurrentInputStream.Length;
            return true;
        }

        public override long Position {
            get { return position; }
            set { throw new NotImplementedException("The SparseFileReaderStream does not support seeking"); }
        }
        public override long Length { get { return length; } }
        public override bool CanRead { get { return true; } }
        public override bool CanSeek { get { return false; } }
        public override bool CanWrite { get { return false; } }

        public override int Read(byte[] buffer, int offset, int count) {
            if(Finished) return 0;
            int read=0;
            while(count>CurrentFileEnd-position) {
                CurrentInputStream.Read(buffer, offset, (int)(CurrentFileEnd-position));
                offset+=(int)(CurrentFileEnd-position);
                count-=(int)(CurrentFileEnd-position);
                read+=(int)(CurrentFileEnd-position);
                position+=CurrentFileEnd-position;
                if(!NextFile()) return read;
            }
            if(count>0) {
                CurrentInputStream.Read(buffer, offset, count);
                position+=count;
                read+=count;
            }
            return read;
        }

        public override void Write(byte[] buffer, int offset, int count) {
            throw new NotImplementedException("The SparseFileReaderStream does not support writing");
        }

        public override void SetLength(long length) {
            throw new NotImplementedException("The SparseFileReaderStream does not support setting length");
        }

        public override long Seek(long offset, SeekOrigin origin) {
            throw new NotImplementedException("The SparseFileReaderStream does not support seeking");
        }

        public override void Flush() { if(CurrentInputStream!=null) CurrentInputStream.Flush(); }

        public override void Close() {
            Flush();
            if(CurrentInputStream!=null) {
                CurrentInputStream.Close();
                CurrentInputStream=null;
            }
        }

    }

    public abstract class CompressionHandler {
        private static SevenZipHandler SevenZip=new SevenZipHandler();
        private static ZipHandler Zip=new ZipHandler();
        private static ICSharpCode.SharpZipLib.Checksums.Crc32 CRC32=new ICSharpCode.SharpZipLib.Checksums.Crc32();

        public static void CompressFiles(string[] files, string[] FolderStructure, out FileStream CompressedStream, 
            out Stream FileList, CompressionType CompType, CompressionLevel CompLevel/*, bool AllowCancel*/) {
            
            FileList=GenerateFileList(files, FolderStructure);
            switch(CompType) {
            case CompressionType.SevenZip:
                CompressedStream=SevenZip.CompressAll(files, CompLevel);
                break;
            case CompressionType.Zip:
                CompressedStream=Zip.CompressAll(files, CompLevel);
                break;
            default:
                CompressedStream = SevenZip.CompressAll(files, CompLevel);
                //throw new obmmException("Unrecognised compression type");
                break;
            }
        }

        public static string DecompressFiles(Stream FileList, Stream CompressedStream, CompressionType type) {
            //Needs to take compression type into account
            switch(type) {
            case CompressionType.SevenZip: return SevenZip.DecompressAll(FileList, CompressedStream);
            case CompressionType.Zip: return Zip.DecompressAll(FileList, CompressedStream);
            default: 
                return SevenZip.DecompressAll(FileList, CompressedStream);
                //throw new obmmException("Unrecognised compression type");
            }
        }

        private static Stream GenerateFileList(string[] files,string[] FolderStructure) {
            MemoryStream FileList=new MemoryStream();
            BinaryWriter bw=new BinaryWriter(FileList);
            for(int i=0;i<files.Length;i++) {
                bw.Write(FolderStructure[i]);
                bw.Write(CRC(files[i]));
                bw.Write((new FileInfo(files[i])).Length);
            }
            bw.Flush();
            FileList.Position=0;
            return FileList;
        }

        public static uint CRC(string s) {
            FileStream fs=File.OpenRead(s);
            uint i=CRC(fs);
            fs.Close();
            return i;
        }
        public static uint CRC(Stream InputStream) {
            byte[] buffer=new byte[4096];
            CRC32.Reset();
            while(InputStream.Position+4096<InputStream.Length) {
                InputStream.Read(buffer, 0, 4096);
                CRC32.Update(buffer);
            }
            if(InputStream.Position<InputStream.Length) {
                int i=(int)(InputStream.Length-InputStream.Position);
                InputStream.Read(buffer, 0, i);
                CRC32.Update(buffer, 0, i);
            }
            return (uint)CRC32.Value;
        }
        public static uint CRC(byte[] b) {
            CRC32.Reset();
            CRC32.Update(b);
            return (uint)CRC32.Value;
        }

        protected abstract string DecompressAll(Stream FileList, Stream CompressedStream);
        protected abstract FileStream CompressAll(string[] FilePaths, CompressionLevel level);
    }

    public class SevenZipHandler : CompressionHandler {
        protected override string DecompressAll(Stream FileList, Stream CompressedStream) {
            SparseFileWriterStream sfs=new SparseFileWriterStream(FileList);
            byte[] buffer=new byte[5];
            Decoder decoder=new Decoder();
            CompressedStream.Read(buffer, 0, 5);
            decoder.SetDecoderProperties(buffer);
            CompressionProgressBar pb=new CompressionProgressBar();
            pb.Init(CompressedStream.Length-CompressedStream.Position,false);
            try {
                decoder.Code(CompressedStream, sfs, CompressedStream.Length-CompressedStream.Position, sfs.Length, pb);
            } finally {
                pb.End();
                sfs.Close();
            }
            return sfs.GetBaseDirectory();
        }

        protected override FileStream CompressAll(string[] FilePaths, CompressionLevel level/*, bool AllowCancel*/) {
            SparseFileReaderStream sfs=new SparseFileReaderStream(FilePaths);
            Encoder coder=new Encoder();
            int DiSize;
            switch(level) {
            case CompressionLevel.VeryHigh:
                DiSize=1 << 26;
                break;
            case CompressionLevel.High:
                DiSize=1 << 25;
                break;
            case CompressionLevel.Medium:
                DiSize=1 << 23;
                break;
            case CompressionLevel.Low:
                DiSize=1 << 21;
                break;
            case CompressionLevel.VeryLow:
                DiSize=1 << 19;
                break;
            default:
                throw new obmmException("Unrecognised compression level");
            }
            coder.SetCoderProperties(new SevenZip.CoderPropID[] { SevenZip.CoderPropID.DictionarySize },
                new object[] { DiSize } );
            if(Settings.CompressionBoost) {
                coder.SetCoderProperties(new SevenZip.CoderPropID[] { SevenZip.CoderPropID.NumFastBytes },
                new object[] { 273 });
            }
            FileStream fs=Program.CreateTempFile();
            coder.WriteCoderProperties(fs);
            CompressionProgressBar pb=new CompressionProgressBar();
            pb.Init(sfs.Length,true);
            //if(AllowCancel)
            try {
                coder.Code(sfs, fs, sfs.Length, -1, pb);
            } catch {
                fs.Close();
                throw;
            } finally {
                pb.End();
                sfs.Close();
            }
            fs.Flush();
            fs.Position=0;
            return fs;
        }
    }

    public class ZipHandler : CompressionHandler {
        public static int GetCompressionLevel(CompressionLevel level) {
            switch(level) {
            case CompressionLevel.VeryHigh:
                return 9;
            case CompressionLevel.High:
                return 7;
            case CompressionLevel.Medium:
                return 5;
            case CompressionLevel.Low:
                return 3;
            case CompressionLevel.VeryLow:
                return 1;
            case CompressionLevel.None:
                return 0;
            default:
                throw new obmmException("Unrecognised compression level");
            }
        }

        protected override string DecompressAll(Stream FileList, Stream CompressedStream) {
            SparseFileWriterStream sfs=new SparseFileWriterStream(FileList);
            ICSharpCode.SharpZipLib.Zip.ZipFile zip=new ICSharpCode.SharpZipLib.Zip.ZipFile(CompressedStream);
            Stream file=zip.GetInputStream(0);
            byte[] buffer=new byte[4096];
            int i;
            while((i=file.Read(buffer, 0, 4096))>0) {
                sfs.Write(buffer, 0, i);
            }
            sfs.Close();
            return sfs.GetBaseDirectory();
        }

        protected override FileStream CompressAll(string[] FilePaths, CompressionLevel level) {
            FileStream fs=Program.CreateTempFile();
            string TempFileName=fs.Name;
            SparseFileReaderStream sfs=new SparseFileReaderStream(FilePaths);
            ICSharpCode.SharpZipLib.Zip.ZipOutputStream ZipStream=new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(fs);
            ZipStream.SetLevel(GetCompressionLevel(level));
            ICSharpCode.SharpZipLib.Zip.ZipEntry ze=new ICSharpCode.SharpZipLib.Zip.ZipEntry("a");
            ZipStream.PutNextEntry(ze);
            //write sfs to the zip file
            byte[] buffer=new byte[4096];
            int upto=0;
            while(sfs.Length-upto>4096) {
                sfs.Read(buffer, 0, 4096);
                ZipStream.Write(buffer, 0, 4096);
                upto+=4096;
            }
            if(sfs.Length-upto>0) {
                sfs.Read(buffer, 0, (int)(sfs.Length-upto));
                ZipStream.Write(buffer, 0, (int)(sfs.Length-upto));
            }
            //finish
            ZipStream.Finish();
            ZipStream.Close();
            return File.OpenRead(TempFileName);
        }
    }

    public class CompressionProgressBar : SevenZip.ICodeProgress {
        private ProgressForm pf;
        private long TotalSize;

        public void Init(long totalSize, bool compressing) {
            if(compressing) {
                pf=new ProgressForm("Compressing", true);
            } else {
                pf=new ProgressForm("Decompressing", false);
            }
            pf.ShowInTaskbar = true;
            pf.Show();
            TotalSize=totalSize;
        }

        public void SetProgress(Int64 inSize, Int64 outSize) {
            pf.UpdateProgress((float)inSize/(float)TotalSize);
            pf.UpdateRatio((float)outSize/(float)inSize);
            System.Windows.Forms.Application.DoEvents();
        }

        public void End() {
            pf.Unblock();
            pf.Close();
        }
    }
}
