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
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using BSAList=System.Collections.Generic.List<OblivionModManager.Classes.BSAArchive>;
using StringList=System.Collections.Generic.List<string>;
using HashTable=System.Collections.Generic.Dictionary<ulong, OblivionModManager.Classes.BSAArchive.BSAFileInfo>;
using System.IO;
using System.IO.Compression;

namespace OblivionModManager.Classes {
    class BSAArchive {

        [Flags]
        private enum FileFlags : int { Meshes=1, Textures=2 }

        public struct BSAFileInfo {
            public readonly BSAArchive bsa;
            public readonly int offset;
            public readonly int size;
            public readonly bool compressed;

            public BSAFileInfo(BSAArchive _bsa, int _offset, int _size) {
                bsa=_bsa;
                offset=_offset;
                size=_size;

                if((size&(1<<30))!=0) {
                    size^=1<<30;
                    compressed=!bsa.defaultCompressed;
                } else compressed=bsa.defaultCompressed;

            }

            /*public MemoryStream GetData() {
                bsa.br.BaseStream.Seek(offset, SeekOrigin.Begin);
                if(compressed) {
                    byte[] b=new byte[size-4];
                    byte[] output=new byte[bsa.br.ReadUInt32()];
                    bsa.br.Read(b, 0, size-4);

                    ICSharpCode.SharpZipLib.Zip.Compression.Inflater inf=new ICSharpCode.SharpZipLib.Zip.Compression.Inflater();
                    inf.SetInput(b, 0, b.Length);
                    inf.Inflate(output);

                    return new MemoryStream(output);
                } else {
                    return new MemoryStream(bsa.br.ReadBytes(size));
                }
            }*/

            public byte[] GetRawData() {
                bsa.br.BaseStream.Seek(offset, SeekOrigin.Begin);
                if(compressed) {
                    byte[] b=new byte[size-4];
                    byte[] output=new byte[bsa.br.ReadUInt32()];
                    bsa.br.Read(b, 0, size-4);

                    ICSharpCode.SharpZipLib.Zip.Compression.Inflater inf=new ICSharpCode.SharpZipLib.Zip.Compression.Inflater();
                    inf.SetInput(b, 0, b.Length);
                    inf.Inflate(output);

                    return output;
                } else {
                    return bsa.br.ReadBytes(size);
                }
            }
        }

        private struct BSAFileInfo4 {
            public string path;
            public readonly ulong hash;
            public readonly int size;
            public readonly uint offset;

            public BSAFileInfo4(BinaryReader br, bool defaultCompressed) {
                path=null;

                hash=br.ReadUInt64();
                size=br.ReadInt32();
                offset=br.ReadUInt32();

                if(defaultCompressed) size^=(1 << 30);
            }
        }

        private struct BSAFolderInfo4 {
            public string path;
            public readonly ulong hash;
            public readonly int count;
            public int offset;

            public BSAFolderInfo4(BinaryReader br) {
                path=null;
                offset=0;

                hash=br.ReadUInt64();
                count=br.ReadInt32();
                //offset=br.ReadInt32();
                br.BaseStream.Position+=4; //Don't need the offset here
            }
        }

        private struct BSAHeader4 {
            public readonly uint bsaVersion;
            public readonly int directorySize;
            public readonly int archiveFlags;
            public readonly int folderCount;
            public readonly int fileCount;
            public readonly int totalFolderNameLength;
            public readonly int totalFileNameLength;
            public readonly FileFlags fileFlags;

            public BSAHeader4(BinaryReader br) {
                br.BaseStream.Position+=4;
                bsaVersion=br.ReadUInt32();
                directorySize=br.ReadInt32();
                archiveFlags=br.ReadInt32();
                folderCount=br.ReadInt32();
                fileCount=br.ReadInt32();
                totalFolderNameLength=br.ReadInt32();
                totalFileNameLength=br.ReadInt32();
                fileFlags=(FileFlags)br.ReadInt32();
            }

            public bool ContainsMeshes { get { return (fileFlags&FileFlags.Meshes)!=0; } }
            public bool ContainsTextures { get { return (fileFlags&FileFlags.Textures)!=0; } }
        }

        private BinaryReader br;
        private string name;
        private bool defaultCompressed;
        private static bool Loaded=false;

        private BSAArchive(string path, bool populateAll) {
            name=Path.GetFileNameWithoutExtension(path).ToLower();
            BSAHeader4 header;
            br=new BinaryReader(File.OpenRead(path), System.Text.Encoding.Default);
            header=new BSAHeader4(br);
            if(header.bsaVersion!=0x67||(!populateAll&&!header.ContainsMeshes&&!header.ContainsTextures)) {
                br.Close();
                return;
            }
            defaultCompressed=(header.archiveFlags & 0x100)>0;

            //Read folder info
            BSAFolderInfo4[] folderInfo = new BSAFolderInfo4[header.folderCount];
            BSAFileInfo4[] fileInfo = new BSAFileInfo4[header.fileCount];
            for(int i=0;i<header.folderCount;i++) folderInfo[i]=new BSAFolderInfo4(br);
            int count=0;
            for(uint i=0;i<header.folderCount;i++) {
                folderInfo[i].path=new string(br.ReadChars(br.ReadByte()-1));
                br.BaseStream.Position++;
                folderInfo[i].offset=count;
                for(int j=0;j<folderInfo[i].count;j++) fileInfo[count+j]=new BSAFileInfo4(br, defaultCompressed);
                count += folderInfo[i].count;
            }
            for(uint i=0;i<header.fileCount;i++) {
                fileInfo[i].path="";
                char c;
                while((c=br.ReadChar())!='\0') fileInfo[i].path+=c;
            }

            for(int i=0;i<header.folderCount;i++) {
                for(int j=0;j<folderInfo[i].count;j++) {
                    BSAFileInfo4 fi4=fileInfo[folderInfo[i].offset+j];
                    string ext=Path.GetExtension(fi4.path);
                    BSAFileInfo fi=new BSAFileInfo(this, (int)fi4.offset, fi4.size);
                    string fpath=Path.Combine(folderInfo[i].path, Path.GetFileNameWithoutExtension(fi4.path));
                    ulong hash=GenHash(fpath, ext);
                    if(ext==".nif") {
                        Meshes[hash]=fi;
                    } else if(ext==".dds") {
                        Textures[hash]=fi;
                    }
                    All[hash]=fi;
                }
            }
            LoadedArchives.Add(this);
        }

        private static readonly BSAList LoadedArchives=new BSAList();
        private static readonly HashTable Meshes=new HashTable();
        private static readonly HashTable Textures=new HashTable();
        private static readonly HashTable All=new HashTable();

        public static bool CheckForTexture(string path) {
            if(!Loaded) Load(false);

            if(File.Exists(Path.Combine(Program.DataFolderName,path))) return true;
            path=path.ToLower().Replace('/', '\\');
            string ext=Path.GetExtension(path);
            ulong hash=GenHash(Path.ChangeExtension(path,null),ext);
            if(Textures.ContainsKey(hash)) return true;
            return false;
        }

        public static byte[] GetMesh(string path) {
            if(!Loaded) Load(false);

            path=path.ToLower().Replace('/', '\\');
            string ext=Path.GetExtension(path);
            if(ext==".nif") {
                if(File.Exists(Path.Combine(Program.DataFolderName,path))) return File.ReadAllBytes(Path.Combine(Program.DataFolderName,path));
                ulong hash=GenHash(Path.ChangeExtension(path, null), ext);
                if(!Meshes.ContainsKey(hash)) return null;
                return Meshes[hash].GetRawData();
            }
            return null;
        }

        private static ulong GenHash(string file) {
            file=file.ToLower().Replace('/', '\\');
            return GenHash(Path.ChangeExtension(file, null), Path.GetExtension(file));
        }
        private static ulong GenHash(string file, string ext) {
            file=file.ToLower();
            ext=ext.ToLower();
            ulong hash=0;
            if(file.Length>0) {
                hash=(ulong)(
                   (((byte)file[file.Length-1])*0x1)+
                    ((file.Length>2?(byte)file[file.Length-2]:(byte)0)*0x100)+
                     (file.Length*0x10000)+
                    (((byte)file[0])*0x1000000)
                );
            }
            if(file.Length>3) {
                hash+=(ulong)(GenHash2(file.Substring(1, file.Length-3))*0x100000000);
            }
            if(ext.Length>0) {
                hash+=(ulong)(GenHash2(ext)*0x100000000);
                byte i=0;
                switch(ext) {
                case ".nif": i=1; break;
                //case ".kf": i=2; break;
                case ".dds": i=3; break;
                //case ".wav": i=4; break;
                }
                if(i!=0) {
                    byte a=(byte)(((i&0xfc)<<5)+(byte)((hash&0xff000000)>>24));
                    byte b=(byte)(((i&0xfe)<<6)+(byte)(hash&0xff));
                    byte c=(byte)((i<<7)+(byte)((hash&0xff00)>>8));
                    hash-=hash&0xFF00FFFF;
                    hash+=(uint)((a<<24)+b+(c<<8));
                }
            }
            return hash;
        }

        private static uint GenHash2(string s) {
            uint hash=0;
            for(int i=0;i<s.Length;i++) {
                hash*=0x1003f;
                hash+=(byte)s[i];
            }
            return hash;
        }

        private void Dispose() {
            if(br!=null) {
                br.Close();
                br=null;
            }
        }

        private static void Load(bool populateAll) {
            foreach(string s in Directory.GetFiles(Program.DataFolderName, "*.bsa")) new BSAArchive(s, populateAll);
            Loaded=true;
        }

        public static void Clear() {
            foreach(BSAArchive BSA in LoadedArchives) BSA.Dispose();
            Meshes.Clear();
            Textures.Clear();
            All.Clear();
            Loaded=false;
        }

        public static byte[] GetFileFromBSA(string bsa, string path) {
            if(!Loaded) Load(true);

            ulong hash=GenHash(path);
            if(!All.ContainsKey(hash)) return null;
            bsa=bsa.ToLower();
            BSAFileInfo fi=All[hash];
            if(fi.bsa.name!=bsa) return null;
            return fi.GetRawData();
        }

        public static byte[] GetFileFromBSA(string path) {
            if(!Loaded) Load(true);

            ulong hash=GenHash(path);
            if(!All.ContainsKey(hash)) return null;
            return All[hash].GetRawData();
        }
    }
}
