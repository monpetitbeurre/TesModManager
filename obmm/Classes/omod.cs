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
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;

namespace OblivionModManager {

	[Serializable]
	public class omod {

        public void WriteDataTo(BinaryWriter _Writer)
        {
            WriteDataTo(_Writer, 7);
        }
        public void WriteDataTo(BinaryWriter _Writer, int version)
        {
            try
            {
                _Writer.Write((Author != null ? Author : ""));
                _Writer.Write((Int32)AllDataFiles.Length);
                foreach (DataFileInfo di in AllDataFiles)
                    di.WriteDataTo(_Writer);
                _Writer.Write((Int32)AllPlugins.Length);
                foreach (string plugin in AllPlugins)
                {
                    _Writer.Write(plugin!=null?plugin:"");
                }
                _Writer.Write((BSAs != null) ? (Int32)BSAs.Length : (Int32)0);
                if (BSAs != null) foreach (string bsa in BSAs)
                    {
                    _Writer.Write(bsa!=null?bsa:"");
                    }
                _Writer.Write(BuildVersion);
                _Writer.Write((byte)CompType);
                _Writer.Write((int)Conflict);
                _Writer.Write((Int32)ConflictsWith.Count);
                foreach (ConflictData conflict in ConflictsWith)
                    conflict.WriteDataTo(_Writer);
                _Writer.Write((UInt32)CRC);
                _Writer.Write((Int64)CreationTime.Ticks);
                _Writer.Write((DataFiles != null) ? (Int32)DataFiles.Length : (Int32)0);
                if (DataFiles != null) foreach (DataFileInfo di in DataFiles)
                        di.WriteDataTo(_Writer);
                _Writer.Write((Int32)DependsOn.Count);
                foreach (ConflictData depend in DependsOn)
                    depend.WriteDataTo(_Writer);
                _Writer.Write((Description != null ? Description : ""));
                _Writer.Write((Email != null ? Email : ""));
                _Writer.Write((FileName != null ? FileName : ""));
                _Writer.Write((FilePath != null) ? FilePath : "");
                _Writer.Write(FileVersion);
                if (version <= 3)
                {
                    _Writer.Write((UInt32)group);
                }
                else if (version > 3)
                {
                    _Writer.Write((ulong)group);
                }
                _Writer.Write(hidden);
                _Writer.Write((INIEdits != null) ? (Int32)INIEdits.Count : (Int32)0);
                if (INIEdits != null) foreach (INIEditInfo edit in INIEdits)
                        edit.WriteDataTo(_Writer);
                _Writer.Write(LowerFileName!=null?LowerFileName:"");
                if (version <= 3)
                {
                    _Writer.Write((Int32)MajorVersion);
                    _Writer.Write((Int32)MinorVersion);
                }
                else if (version > 3)
                {
                    _Writer.Write((Version != null ? Version : ""));
                }
                _Writer.Write((ModName != null ? ModName : FileName));
                _Writer.Write((Plugins != null) ? (Int32)Plugins.Length : (Int32)0);
                if (Plugins != null) foreach (string plugin in Plugins)
                    {
                    _Writer.Write(plugin!=null?plugin:"");
                    }
                _Writer.Write((SDPEdits != null) ? (Int32)SDPEdits.Count : (Int32)0);
                if (SDPEdits != null) foreach (SDPEditInfo si in SDPEdits)
                        si.WriteDataTo(_Writer);
                _Writer.Write((Website != null ? Website : ""));

                //            _Writer.Write(FullFilePath);
                //            _Writer.Write(Hidden);
                //            _Writer.Write(image);
                //            _Writer.Write(ModFile);
                //            _Writer.Write(Version);

                if (version > 4)
                {
                    _Writer.Write(bUpdateExists);
                }
                if (version > 5)
                {
                    _Writer.Write(bHasScript);
                }
                if (version > 6)
                {
                    _Writer.Write(bSystemMod);
                }
            }
            catch (Exception ex)
            {
                Program.logger.WriteToLog("Could not save mod "+ModName+": "+ex.Message+" "+ex.StackTrace,Logger.LogLevel.Warning);
                throw;
            }
        }

        public omod() // empty omod
        {
        }

        public omod(BinaryReader _Reader, int version) // SetDataFrom
        {
            try
            {
                Author = _Reader.ReadString();

                int len = _Reader.ReadInt32(); // .Write(AllDataFiles.Length);
                AllDataFiles = new DataFileInfo[len];
                for (int i = 0; i < len; i++)
                    AllDataFiles.SetValue(new DataFileInfo(_Reader), i);

                len = _Reader.ReadInt32();
                AllPlugins = new string[len];
                for (int i = 0; i < len; i++)
                    AllPlugins.SetValue(_Reader.ReadString(), i);

                len = _Reader.ReadInt32();
                BSAs = new string[len];
                for (int i = 0; i < len; i++)
                    BSAs.SetValue(_Reader.ReadString(), i);

                BuildVersion = _Reader.ReadInt32();
                CompType = (CompressionType)_Reader.ReadByte();
                Conflict = (ConflictLevel)_Reader.ReadInt32();

                len = _Reader.ReadInt32();
                for (int i = 0; i < len; i++)
                    ConflictsWith.Add(new ConflictData(_Reader));

                CRC = _Reader.ReadUInt32();
                CreationTime = new DateTime(_Reader.ReadInt64());

                len = _Reader.ReadInt32();
                DataFiles = new DataFileInfo[len];
                for (int i = 0; i < len; i++)
                    DataFiles.SetValue(new DataFileInfo(_Reader), i);

                len = _Reader.ReadInt32();
                for (int i = 0; i < len; i++)
                    DependsOn.Add(new ConflictData(_Reader));

                Description = _Reader.ReadString();
                Email = _Reader.ReadString();
                FileName = _Reader.ReadString();
                FilePath = _Reader.ReadString(); if (FilePath.Length == 0) FilePath = null;
                FileVersion = _Reader.ReadByte();
                if (version <= 3)
                {
                    group = _Reader.ReadUInt32();
                }
                else if (version > 3)
                {
                    group = _Reader.ReadUInt64();
                }

                hidden = _Reader.ReadBoolean();

                len = _Reader.ReadInt32();
                for (int i = 0; i < len; i++)
                {
                    if (INIEdits == null) INIEdits = new List<INIEditInfo>();
                    INIEdits.Add(new INIEditInfo(_Reader));
                }

                LowerFileName = _Reader.ReadString();
                if (version == 2)
                {
                    MajorVersion = _Reader.ReadInt32();
                    MinorVersion = _Reader.ReadInt32();
                    Version = "" + MajorVersion + ((MinorVersion != -1) ? ("." + MinorVersion + ((BuildVersion != -1) ? ("." + BuildVersion) : "")) : "");
                }
                else if (version >= 3)
                {
                    Version = _Reader.ReadString();
                }
                ModName = _Reader.ReadString();

                len = _Reader.ReadInt32();
                Plugins = new string[len];
                for (int i = 0; i < len; i++)
                    Plugins.SetValue(_Reader.ReadString(), i);

                len = _Reader.ReadInt32();
                if (SDPEdits == null) SDPEdits = new List<SDPEditInfo>();
                for (int i = 0; i < len; i++)
                    SDPEdits.Add(new SDPEditInfo(_Reader));

                Website = _Reader.ReadString();

                // FullFilePath=_Reader.ReadString();
                // Hidden=_Reader.ReadBoolean();
                //            _Writer.Write(image);
                //            _Writer.Write(ModFile);
                // Version = _Reader.ReadString();

                if (version > 4)
                {
                    bUpdateExists=_Reader.ReadBoolean();
                }
                if (version > 5)
                {
                    bHasScript = _Reader.ReadBoolean();
                }
                else
                {
                    if (File.Exists(Path.Combine(Settings.omodDir,this.FileName)))
                        bHasScript = this.DoesScriptExist();
                }
                if (version > 6)
                {
                    bSystemMod = _Reader.ReadBoolean();
                }
                pd = null;

                // make sure that the mod file is hidden
                if (hidden) this.Hide();

                // acquire proper time if needed
                if (CreationTime.Year < 2010)
                    CreationTime = File.GetCreationTime(Path.Combine(Settings.omodDir, FileName));

            }
            catch
            {
                // omod data is corrupt ignore.
                throw new Exception("Could not recreate mod from data: data file is corrupt");
            }

        }


		protected class PrivateData {
			public ZipFile modFile=null;
            //public System.Drawing.Image image;
		}

		private static void WriteStreamToZip(BinaryWriter bw, Stream input) {
			input.Position=0;
			byte[] buffer=new byte[4096];
			int upto=0;
			while(input.Length-upto>4096) {
				input.Read(buffer, 0, 4096);
				bw.Write(buffer, 0, 4096);
				upto+=4096;
			}
			if(input.Length-upto>0) {
				input.Read(buffer, 0, (int)(input.Length-upto));
				bw.Write(buffer, 0, (int)(input.Length-upto));
			}
		}

        public delegate void CompressingEventCallback(object sender, SevenZip.ProgressEventArgs e);

        public void CompressingCallback(object sender, SevenZip.ProgressEventArgs e)
        {
            //e.PercentDone;
        }
        public static void theCompressor_FileCompressionFinished(object sender, EventArgs e)
        {
            Console.WriteLine("Compressed ");// + args.FileInfo.FileName);
            //if (pf != null) pf.UpdateProgress();// .UpdateProgress(55);// + args.PercentDone / 5);
            //Application.DoEvents();
        }
        internal static void Create7zOmod2(omodCreationOptions ops, string omodFileName, ulong groups)
        {

            //ProgressForm
            Program.pf = new ProgressForm("Generating "+omodFileName, false);
            Program.pf.ShowInTaskbar = true;
            Program.pf.Show();

            try
            {
                //SevenZip.SevenZipCompressor.SetLibraryPath( "SevenZipSharp.dll");

                SevenZip.SevenZipCompressor theCompressor = new SevenZip.SevenZipCompressor();
                theCompressor.ArchiveFormat = SevenZip.OutArchiveFormat.SevenZip;
                theCompressor.CompressionMode = SevenZip.CompressionMode.Create;
                theCompressor.CompressionMethod = SevenZip.CompressionMethod.Default;
                theCompressor.DirectoryStructure = true;
                switch (ops.omodCompressionLevel)
                {
                    case CompressionLevel.VeryHigh:
                        theCompressor.CompressionLevel = SevenZip.CompressionLevel.Ultra;
                        break;
                    case CompressionLevel.High:
                        theCompressor.CompressionLevel = SevenZip.CompressionLevel.High;
                        break;
                    case CompressionLevel.Medium:
                        theCompressor.CompressionLevel = SevenZip.CompressionLevel.Normal;
                        break;
                    case CompressionLevel.Low:
                        theCompressor.CompressionLevel = SevenZip.CompressionLevel.Low;
                        break;
                    case CompressionLevel.VeryLow:
                        theCompressor.CompressionLevel = SevenZip.CompressionLevel.Fast;
                        break;
                }
                //theCompressor.Compressing += new EventHandler<SevenZip.ProgressEventArgs>(this.CompressingCallback);

                string tempdir = Program.CreateTempDirectory();

                for (int i = 0; i < ops.esps.Length; i++)
                {
                    string target = Path.Combine(tempdir, ops.espPaths[i].ToLower()); //Path.GetFileName(ops.espPaths[i])); //ops.espPaths[i].ToLower().Replace(ops.espSources[i] + "\\", ""));
                    if (!File.Exists(target))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(target));
                        if (ops.esps[i].Contains(tempdir))
                            File.Move(ops.esps[i], target);
                        else
                            File.Copy(ops.esps[i], target);
                    }
                }
                for (int i = 0; i < ops.DataFiles.Length; i++)
                {
                    string targetpath=(ops.DataFilePaths[i].StartsWith(ops.DataSources[i] + "\\")?ops.DataFilePaths[i].Substring((ops.DataSources[i] + "\\").Length):ops.DataFilePaths[i]);
                    Directory.CreateDirectory(Path.GetDirectoryName(Path.Combine(tempdir, targetpath)));
                    if (ops.DataFiles[i].Contains(tempdir))
                        File.Move(ops.DataFiles[i], Path.Combine(tempdir, targetpath));
                    else
                        File.Copy(ops.DataFiles[i], Path.Combine(tempdir, targetpath));
                }


                //string espdir = "";
                //string directory = "";
                //if (ops.esps.Length > 0)
                //{
                //    string path=ops.esps[0].Replace(Program.TempDir,"");
                //    path = path.Substring(0,path.IndexOf("\\"));
                //    directory = espdir = Program.TempDir + path;
                //}

                //if (ops.DataFiles.Length > 0)
                //{
                //    string path = ops.DataFiles[0].Replace(Program.TempDir, "");
                //    path = path.Substring(0, path.IndexOf("\\"));
                //    directory = Program.TempDir + path;
                //    //Directory.Move(directory2, directory);
                //    foreach (string esp in ops.esps)
                //    {
                //        File.Move(esp, directory + "\\"+esp.Replace(espdir, ""));
                //    }
                //}
                TextWriter txtwr = null;
                // copy readme.txt, config.ini and image.jpg
                if (ops.readme.Length > 0)
                {
                    txtwr = new StreamWriter(Path.Combine(tempdir, "readme.txt"));
                    txtwr.Write(ops.readme);
                    txtwr.Close();
                }
                if (ops.script.Length > 0)
                {
                    txtwr = new StreamWriter(Path.Combine(tempdir, "script.txt"));
                    txtwr.Write(ops.script.ToCharArray());
                    txtwr.Close();
                }

                txtwr = new StreamWriter(Path.Combine(tempdir, "config.ini"));
                txtwr.Write(("OmodVersion=" + "10" +/* Program.CurrentOmodVersion +*/ "\r\n" +
                                "Name=" + ops.Name + "\r\n" +
                                "MajorVersion=" + ops.MajorVersion + "\r\n" +
                                "MinorVersion=" + ops.MinorVersion + "\r\n" +
                                "Author=" + ops.Author + "\r\n" +
                                "Email=" + ops.email + "\r\n" +
                                "Website=" + ops.website + "\r\n" +
                                "DateTime=" + DateTime.Now.ToShortDateString() + "\r\n" +
                                "Compression=" + (byte)ops.CompressionType + "\r\n" +
                                "BuildVersion=" + ops.BuildVersion + "\r\n" +
                                "Version=" + ops.Version + "\r\n" +
                                "SystemMod=" + ops.bSystemMod + "\r\n" +
                                "Description=" + ops.Description + "\r\n").ToCharArray());
                txtwr.Close();

                if (ops.Image != null && ops.Image.Length > 0)
                    File.Copy(ops.Image, Path.Combine(tempdir, "image.jpg"), true);

                theCompressor.FileCompressionFinished += theCompressor_FileCompressionFinished;
                theCompressor.Compressing += theCompressor_Compressing;
                Program.pf.SetProgressRange(Directory.GetFiles(tempdir, "*.*", SearchOption.AllDirectories).Length);
                bool bDone = false;
                while (!bDone)
                {
                    try
                    {
                        theCompressor.CompressDirectory(tempdir, Path.Combine(Settings.omodDir, omodFileName), true);
                        bDone = true;
                    }
                    catch (Exception ex)
                    {
                        Program.logger.WriteToLog("Compression of " + omodFileName + " failed:" + ex.Message, Logger.LogLevel.Low);
                        switch (theCompressor.CompressionLevel)
                        {
                            case SevenZip.CompressionLevel.Ultra:
                                theCompressor.CompressionLevel = SevenZip.CompressionLevel.High;
                                break;
                            case SevenZip.CompressionLevel.High:
                                theCompressor.CompressionLevel = SevenZip.CompressionLevel.Normal;
                                break;
                            case SevenZip.CompressionLevel.Normal:
                                theCompressor.CompressionLevel = SevenZip.CompressionLevel.Low;
                                break;
                            case SevenZip.CompressionLevel.Low:
                                theCompressor.CompressionLevel = SevenZip.CompressionLevel.Fast;
                                break;
                            default:
                                // cannot compress it!!!
                                throw ex;
                        }
                    }

                }
            }
            finally
            {
                Program.pf.Unblock();
                Program.pf.Close();
                Program.pf = null;
            }
        }

        static void theCompressor_Compressing(object sender, SevenZip.ProgressEventArgs e)
        {
            Console.WriteLine(e.PercentDone+"%");
            //Program.pf.UpdateProgress();
            //Application.DoEvents();
        }

        internal static void CreateZipOmod2(omodCreationOptions ops, string omodFileName, ulong groups)
        {
            ZipOutputStream ZipStream;
            BinaryWriter omodStream;
            ZipEntry ze;
//            FileStream DataCompressed;
//            Stream DataInfo;
            omodFileName = Path.Combine(Settings.omodDir, omodFileName);

            ProgressForm pf = new ProgressForm("Generating "+omodFileName, false);
            pf.ShowInTaskbar = true;
            pf.Show();
            
            ZipStream = new ZipOutputStream(File.Open(omodFileName, FileMode.CreateNew));
            omodStream = new BinaryWriter(ZipStream);
            try
            {
                ZipStream.SetLevel(ZipHandler.GetCompressionLevel(ops.omodCompressionLevel));
                //The readme
                foreach (string filename in ops.DataFiles)
                {
                    if (filename.ToLower() == "readme.txt")
                    {
                        // readme already there blank this one
                        ops.readme = "";
                        break;
                    }
                }
                if (ops.readme != "" && ops.readme != null)
                {
                    ze = new ZipEntry("readme.txt");
                    ZipStream.PutNextEntry(ze);
                    omodStream.Write(ops.readme.ToCharArray());
                    omodStream.Flush();
                }
                //The script
                string scriptname = "script.txt";
                foreach (string filename in ops.DataFiles)
                {
                    if (filename.ToLower() == "script.txt")
                    {
                        // script already there, change name
                        scriptname = "script";
                        break;
                    }
                }
                if (ops.script != "" && ops.script != null)
                {
                    ze = new ZipEntry(scriptname);
                    ZipStream.PutNextEntry(ze);
                    omodStream.Write(ops.script.ToCharArray()); //.Replace("\0", "").ToCharArray());
                    omodStream.Flush();
                }
                //The image
                foreach (string filename in ops.DataFiles)
                {
                    if (filename.ToLower() == "image.jpg")
                    {
                        // image already there blank this one
                        ops.Image = "";
                        break;
                    }
                }
                if (ops.Image != "" && ops.Image != null)
                {
                    ze = new ZipEntry("image.jpg"); // + ops.Image.Substring(ops.Image.LastIndexOf('.')+1,ops.Image.Length-ops.Image.LastIndexOf('.')-1));
                    ZipStream.PutNextEntry(ze);
                    FileStream fs = File.OpenRead(ops.Image);
                    WriteStreamToZip(omodStream, fs);
                    omodStream.Flush();
                    fs.Close();
                }
                //The config file
                string configFileName = "config.ini";
                foreach (string filename in ops.DataFiles)
                {
                    if (Path.GetFileName(filename).ToLower() == configFileName)
                    {
                        // That's a problem
                        configFileName = "config";
                        break;
                    }
                }
                ze = new ZipEntry(configFileName);
                ZipStream.PutNextEntry(ze);
                omodStream.Write(("OmodVersion=" + "10" +/* Program.CurrentOmodVersion +*/ "\r\n" +
                                "Name=" + ops.Name + "\r\n" +
                                "MajorVersion=" + ops.MajorVersion + "\r\n" +
                                "MinorVersion=" + ops.MinorVersion + "\r\n" +
                                "Author=" + ops.Author + "\r\n" +
                                "Email=" + ops.email + "\r\n" +
                                "Website=" + ops.website + "\r\n" +
                                "DateTime=" + DateTime.Now.ToShortDateString() + "\r\n" +
                                "Compression=" + (byte)ops.CompressionType + "\r\n" +
                                "BuildVersion=" + ops.BuildVersion + "\r\n" +
                                "Version=" + ops.Version + "\r\n" +
                                "Description=" + ops.Description + "\r\n" +
                                "SystemMod="+ops.bSystemMod).ToCharArray());

                omodStream.Flush();
                //plugins
                int espCount = 0;
                if (ops.esps!=null && ops.esps.Length > 0)
                {
                    GC.Collect();
//                   foreach (string filenm in ops.esps)
                   for (int i = 0; i < ops.esps.Length; i++)
                   {
                       FileInfo finfo = new FileInfo(ops.esps[i]); // filenm);
                       FileStream fs = File.OpenRead(finfo.FullName);
                       byte[] buffer = new byte[fs.Length];
                       fs.Read(buffer, 0, buffer.Length);
                       string filenm2 = ops.espPaths[i]; // filenm.Replace(Program.TempDir, "");
//                       filenm2 = filenm2.Substring(filenm2.IndexOf("\\") + 1); // remove temp path and "0\\"
                       ZipEntry entry = new ZipEntry(filenm2);
                       ZipStream.PutNextEntry(entry);
                       omodStream.Write(buffer, 0, buffer.Length);
                       fs.Close();

                       espCount++;
                       pf.UpdateProgress((float)espCount / (float)(ops.esps.Length + ops.DataFiles.Length));
                       System.Windows.Forms.Application.DoEvents();

                    }
                }
                //data files
                int dataCount = 0;
                if (ops.DataFiles!=null && ops.DataFiles.Length > 0)
                {
                    GC.Collect();
//                    foreach (string filenm in ops.DataFiles)
                    for (int i=0;i<ops.DataFiles.Length;i++)
                    {
                        string filenm = ops.DataFiles[i];
                        string filenm2 = ops.DataFilePaths[i]; //filenm.Replace(Program.TempDir, "");
//                        filenm2 = filenm2.Substring(filenm2.IndexOf("\\") + 1); // remove temp path and "0\\"
                        ZipEntry entry = new ZipEntry(filenm2);
                        ZipStream.PutNextEntry(entry);

                        FileInfo finfo = new FileInfo(filenm);
                        FileStream fs = File.OpenRead(finfo.FullName);
                        byte[] buffer = new byte[100*1024];
                        int bytesread=0;
                        int totalbytesread=0;
                        while ((bytesread = fs.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            totalbytesread+=bytesread;
                            omodStream.Write(buffer, 0, bytesread);
                            omodStream.Flush();
                            pf.UpdateProgress((float)(((float)totalbytesread)/fs.Length +dataCount + espCount) / (float)(ops.esps.Length + ops.DataFiles.Length));
                            System.Windows.Forms.Application.DoEvents();
                        }
                        fs.Close();

                        dataCount++;
                        pf.UpdateProgress((float)(dataCount + espCount) / (float)(ops.esps.Length + ops.DataFiles.Length));
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
                ZipStream.Finish();
            }
            finally
            {
                omodStream.Close();
                pf.Unblock();
                pf.Close();
            }

        }

		public static void CreateOmod(omodCreationOptions ops, string omodFileName, ulong groups)
        {
            Program.logger.WriteToLog("Creating omod: " + omodFileName, Logger.LogLevel.High);

            if (ops.bOmod2)
            {
                if (ops.CompressionType == CompressionType.Zip)
                    CreateZipOmod2(ops, omodFileName, groups);
                else
                    Create7zOmod2(ops, omodFileName, groups);
            }
            else
                CreateOmod1(ops, omodFileName, groups);

            //Cleanup
            omod o = Program.LoadNewOmod(Path.Combine(Settings.omodDir,omodFileName));
            if (o != null)
            {
                o.group = groups;
                Conflicts.UpdateConflict(o);
            }
            //Program.ClearTempFiles();
            GC.Collect();
        }
		public static void CreateOmod1(omodCreationOptions ops, string omodFileName, ulong groups) {
			ZipOutputStream ZipStream;
			BinaryWriter omodStream;
			ZipEntry ze;
			FileStream DataCompressed;
			Stream DataInfo;
			omodFileName=Path.Combine(Settings.omodDir,omodFileName);

			ZipStream=new ZipOutputStream(File.Open(omodFileName, FileMode.CreateNew));
			omodStream=new BinaryWriter(ZipStream);

            // free as much memory as possible
            GC.Collect(); GC.WaitForPendingFinalizers(); GC.Collect();

			try {
				ZipStream.SetLevel(ZipHandler.GetCompressionLevel(ops.omodCompressionLevel));
				//The readme
				if(ops.readme!=null &&ops.readme.Length>0) {
					ze=new ZipEntry("readme");
					ZipStream.PutNextEntry(ze);
					omodStream.Write(ops.readme);
					omodStream.Flush();
				}
				//The script
				if(ops.script!=null&&ops.script.Length>0) {

					ze=new ZipEntry("script");
					ZipStream.PutNextEntry(ze);
					omodStream.Write(ops.script);
					omodStream.Flush();
				}
				//The image
				if(ops.Image!=""&&ops.Image!=null) {
					ze=new ZipEntry("image");
					ZipStream.PutNextEntry(ze);
					FileStream fs=File.OpenRead(ops.Image);
					WriteStreamToZip(omodStream, fs);
					omodStream.Flush();
					fs.Close();
				}
				//The config file
				ze=new ZipEntry("config");
				ZipStream.PutNextEntry(ze);
				omodStream.Write(Program.CurrentOmodVersion);
				omodStream.Write(ops.Name);
				omodStream.Write(ops.MajorVersion);
				omodStream.Write(ops.MinorVersion);
				omodStream.Write(ops.Author);
				omodStream.Write(ops.email);
				omodStream.Write(ops.website);
				omodStream.Write(ops.Description);
				omodStream.Write(DateTime.Now.ToBinary());
				omodStream.Write((byte)ops.CompressionType);
				omodStream.Write(ops.BuildVersion);
				omodStream.Flush();
				//plugins
				if(ops.esps.Length>0) {
					GC.Collect();
					ze=new ZipEntry("plugins.crc");
					ZipStream.PutNextEntry(ze);
					CompressionHandler.CompressFiles(ops.esps, ops.espPaths, out DataCompressed, out DataInfo,
					                                 ops.CompressionType, ops.DataFileCompresionLevel);
					WriteStreamToZip(omodStream, DataInfo);
					omodStream.Flush();
					ZipStream.SetLevel(0);
					ze=new ZipEntry("plugins");
					ZipStream.PutNextEntry(ze);
					WriteStreamToZip(omodStream, DataCompressed);
					omodStream.Flush();
					ZipStream.SetLevel(ZipHandler.GetCompressionLevel(ops.omodCompressionLevel));
					DataCompressed.Close();
					DataInfo.Close();
				}
				//data files
				if(ops.DataFiles.Length>0) {
					GC.Collect();
					ze=new ZipEntry("data.crc");
					ZipStream.PutNextEntry(ze);
					CompressionHandler.CompressFiles(ops.DataFiles, ops.DataFilePaths, out DataCompressed, out DataInfo,
					                                 ops.CompressionType, ops.DataFileCompresionLevel);
					WriteStreamToZip(omodStream, DataInfo);
					omodStream.Flush();
					ZipStream.SetLevel(0);
					ze=new ZipEntry("data");
					ZipStream.PutNextEntry(ze);
					WriteStreamToZip(omodStream, DataCompressed);
					omodStream.Flush();
					ZipStream.SetLevel(ZipHandler.GetCompressionLevel(ops.omodCompressionLevel));
					DataCompressed.Close();
					DataInfo.Close();
				}
				ZipStream.Finish();
			} finally {
				omodStream.Close();
			}
		}

		public static void Remove(string filename) {
			filename=filename.ToLower();
			for(int i=0;i<Program.Data.omods.Count;i++) {
				omod o=Program.Data.omods[i];
				if(o.LowerFileName==filename) {
					if(o.Conflict==ConflictLevel.Active) {
						o.DeletionDeactivate();
					}
                    o.Close();
                    File.Delete(o.FullFilePath);
                    if (Program.Data.omods[i].LowerFileName == filename)
					    Program.Data.omods.RemoveAt(i);
                    break;
				}
			}
		}

		[NonSerialized]
		private PrivateData pd=new PrivateData();
		public void RecreatePrivateData() { if(pd==null) pd=new PrivateData(); }

		public readonly string FilePath;
		public string FileName;
		public string LowerFileName;
		public string ModName;
		public readonly int MajorVersion;
		public readonly int MinorVersion;
		public readonly int BuildVersion;
		public string Description;
		public readonly string Email;
		public string Website;
		public string Author;
		public readonly DateTime CreationTime;
		public readonly string[] AllPlugins;
		public readonly DataFileInfo[] AllDataFiles;
		public readonly uint CRC;
		public readonly CompressionType CompType;
		private readonly byte FileVersion;
		private bool hidden=false;
		public bool Hidden { get { return hidden; } }
        public bool bOmod2=false;
        public bool bFomod = false;
        public bool bHasReadme = false;
        public bool bHasScript = false;
        public bool bUpdateExists = false;
        public bool bBAINpackage = false;
		public ulong group;
		public string Version = "";
        public bool bSystemMod = false;
//        {
//			get { return ""+MajorVersion+((MinorVersion!=-1)?("."+MinorVersion+((BuildVersion!=-1)?("."+BuildVersion):"")):""); }
//		}
		public string FullFilePath {
            get { if (FilePath == null) return Path.Combine(Settings.omodDir, FileName); else return Path.Combine(FilePath,FileName); }
		}

		public string[] Plugins;
		public DataFileInfo[] DataFiles;
		public string[] BSAs;
		public List<INIEditInfo> INIEdits;
		public List<SDPEditInfo> SDPEdits;

		public ConflictLevel Conflict=ConflictLevel.NoConflict;

		public readonly List<ConflictData> ConflictsWith=new List<ConflictData>();
		public readonly List<ConflictData> DependsOn=new List<ConflictData>();

        public string fomodroot = ""; // root for the fomod files if fomod is a deep archive
		private ZipFile ModFile {
			get {
				if(pd.modFile!=null) return pd.modFile;
                try
                {
                    string filelow = FullFilePath.ToLower();
                    if (filelow.EndsWith(".zip") || filelow.EndsWith(".omod") || filelow.EndsWith(".omod2") || filelow.EndsWith(".zip.ghost") || filelow.EndsWith(".omod.ghost") || filelow.EndsWith(".omod2.ghost"))
                    {
                        // check for PK 
                        byte[] buf = null;
                        FileStream fs = new FileStream(FullFilePath, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        long numBytes = 2;
                        buf = br.ReadBytes((int)numBytes);
                        br.Close();
                        fs.Close();
                        if (buf[0]=='P' && buf[1]=='K')
                            pd.modFile = new ZipFile(FullFilePath);
                    }
                }
                catch
                {
                    // not a zip then. 7z then
                    
                }
				return pd.modFile;
			}
		}
        public bool IsImageCached
        {
            get
            {
                string cacheImage = Path.Combine(Path.Combine(Settings.omodDir, "info"), this.LowerFileName + ".jpg").Replace(".ghost", "");
                return (File.Exists(cacheImage));
            }
        }
		public System.Drawing.Image image {
			get {

                System.Drawing.Image foundimage=null;

                //if(pd.image!=null) return pd.image;
                Stream s;

                string cacheImage = Path.Combine(Path.Combine(Settings.omodDir, "info"), this.LowerFileName + ".jpg").Replace(".ghost", "");
                if (File.Exists(cacheImage))
                {
                    try
                    {
                        //pd.image = Image.FromFile(cacheImage);
                        foundimage = Image.FromFile(cacheImage);
                    }
                    catch
                    {
                        // bad file? Delete!
                        File.Delete(cacheImage);
                    };
                }
                else if (FileName.ToLower().EndsWith(".omod") || (CompType == CompressionType.Zip && ModFile != null))
                {
                    s = ExtractWholeFile("image");
                    if (s == null)
                        s = ExtractWholeFile("image.jpg");
                    if (s != null)
                    {
                        if (s.Length > 0)
                        {
                            byte[] imagebytes = new byte[s.Length];
                            s.Read(imagebytes, 0, (int)s.Length);
                            File.WriteAllBytes(cacheImage, imagebytes);
                            s.Seek(0,SeekOrigin.Begin);
                            //pd.image = System.Drawing.Image.FromStream(s);
                            foundimage = System.Drawing.Image.FromStream(s);
                        }
                        s.Close();
                        if (Program.IsImageAnimated(foundimage)) //pd.image))
                        {
                            //					MessageBox.Show("Animated or multi-resolution images are not supported","Error");
                            foundimage.Dispose();
                            foundimage = null;
                            //pd.image = null;
                        }
                    }
                }
                else
                {
                    //pd.image = null;
                    if (File.Exists(Path.Combine(Settings.omodDir, FileName)))
                    {
                        SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Path.Combine(Settings.omodDir, FileName));
                        foreach (string file in zextract.ArchiveFileNames)
                        {
                            if (file.ToLower()=="image.jpg")
                            {
                                Stream st = new MemoryStream();
                                zextract.ExtractFile(file, st);

                                // make sure that the current dir did not change
                                Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));

                                st.Seek(0, SeekOrigin.Begin);
                                byte[] imagebytes = new byte[st.Length];
                                st.Read(imagebytes, 0, (int)st.Length);
                                File.WriteAllBytes(cacheImage, imagebytes);

                                st.Seek(0, SeekOrigin.Begin);
                                //string filename = "";
                                //BinaryWriter sw = new BinaryWriter(Program.CreateTempFile(out filename));
                                //byte[] img = new byte[st.Length];
                                //st.Read(img, 0, (int)st.Length);
                                //st.Close();
                                //sw.Write(img, 0, (int)img.Length);
                                //sw.Close();
                                //zextract.Dispose();
                                try
                                {
                                    //pd.image = new Bitmap(st); //st);
                                    foundimage = new Bitmap(st); //st);
                                    st.Close();
                                }
                                catch (Exception ex)
                                {
                                    Program.logger.WriteToLog(this.FileName+" contains a bad image.jpg file: "+ex.Message,Logger.LogLevel.Low);
                                }

                                break;
                            }
                        }
                        zextract.Dispose();
                    }
                }
                    

                /*
                if (bFomod)
                    s = null;
                else if (bOmod2)
                    s = ExtractWholeFile("image.jpg");
                else
                    s = ExtractWholeFile("image");
                if (s == null) return null;
				if (!(s.Length==0)) pd.image=System.Drawing.Image.FromStream(s);
				s.Close();
				if(Program.IsImageAnimated(pd.image)) {
//					MessageBox.Show("Animated or multi-resolution images are not supported","Error");
					pd.image=null;
				}
                 * */

                try
                {
                    if (!File.Exists(cacheImage) && foundimage != null)
                        // copy file to info directory
                        foundimage.Save(cacheImage);
                    //if (!File.Exists(cacheImage) && pd.image != null)
                    //    // copy file to info directory
                    //    pd.image.Save(cacheImage);
                }
                catch { };

				return foundimage;
                //return pd.image;
            }
		}


        public static void DecodeFomodXML(System.Xml.XmlDocument doc, ref string Name, ref string Version, ref string Author, ref string Description, ref string Website)
        {
            System.Xml.XmlNode rootnode = doc.SelectSingleNode("//fomod");
            foreach (System.Xml.XmlNode xmlnode in rootnode.ChildNodes)
            {
                switch (xmlnode.Name)
                {
                    case "Name":
                        if (xmlnode.ChildNodes.Count > 0 && xmlnode.ChildNodes[0].Name=="#text")
                            Name = doc.SelectSingleNode("//fomod/Name/text()").Value;
                        break;
                    case "Version":
                        if (xmlnode.ChildNodes.Count > 0 && xmlnode.ChildNodes[0].Name == "#text")
                            Version = doc.SelectSingleNode("//fomod/Version/text()").Value;
                        //string numVersion = Version;
                        //for (int i = 0; i < numVersion.Length; i++)
                        //{
                        //    if (numVersion[i] < '0' || numVersion[i] > '9')
                        //    {
                        //        numVersion = numVersion.Substring(0, i);
                        //        break;
                        //    }
                        //}
                        //string[] tvers = numVersion.Split('.');
                        //MajorVersion = Convert.ToInt32(tvers[0]);
                        //if (tvers.Length > 1) MinorVersion = Convert.ToInt32(tvers[1]);
                        //if (tvers.Length > 2) BuildVersion = Convert.ToInt32(tvers[2]);
                        break;
                    case "Author":
                        if (xmlnode.ChildNodes.Count > 0 && xmlnode.ChildNodes[0].Name == "#text")
                            Author = doc.SelectSingleNode("//fomod/Author/text()").Value;
                        break;
                    case "Website":
                        if (xmlnode.ChildNodes.Count > 0 && xmlnode.ChildNodes[0].Name == "#text")
                            Website = doc.SelectSingleNode("//fomod/Website/text()").Value;
                        break;
                    case "Description":
                        if (xmlnode.ChildNodes.Count > 0 && xmlnode.ChildNodes[0].Name == "#text")
                            Description = doc.SelectSingleNode("//fomod/Description/text()").Value;
                        break;
                    default:
                        break;
                }
            }
        }


		public omod(string path,bool bInOmodDir)
        {
            Program.logger.WriteToLog(" omod("+path+") "+(bInOmodDir?"(in mods dir)":"(outside standards mods dir)"), Logger.LogLevel.High);
			BinaryReader br=null;
            string strTmpDir = "";
			try {
				if(bInOmodDir)
                    FilePath=null;
                else
                    FilePath=Path.GetDirectoryName(path)+"\\";
				FileName=Path.GetFileName(path);
				LowerFileName=FileName.ToLower();
                Stream Config=null;


                SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Path.Combine(Settings.omodDir, FileName));

                //bool bBainPackage = false;
                foreach (string file in zextract.ArchiveFileNames)
                {

                    if (file.ToLower().Contains("fomod\\info.xml") || file.ToLower().Contains("fomod\\moduleconfig.xml") || file.ToLower().Contains("fomod\\script.cs")) // || file.ToLower().Contains("fomod\\info.xml")) // info.xml is ONLY mod information, not install information
                    {
                        bFomod = true;
                        break;
                    }
                    else if (Path.GetFileName(Path.GetDirectoryName(file)).StartsWith("00"))
                    {
                        bBAINpackage = true;
                        break;
                    }
                    else if (file.ToLower()=="config.ini" || file.ToLower() == "config.txt")
                    {
                        bOmod2 = true;
                        break;
                    }
                }
                zextract.Dispose();

                if (ModFile != null)
                {
                    Config = ExtractWholeFile("config");
                    if (Config == null)
                    {
                        Config = ExtractWholeFile("config.ini");
                        if (Config != null)
                            bOmod2 = true;
                    }
                    else if (Config == null)
                    {
                        Config = ExtractWholeFile("config.txt");
                        if (Config != null)
                            bOmod2 = true;
                    }
                    else if (Path.GetExtension(path).ToLower() == ".omod2")
                    {
                        bOmod2 = true;
                        if (ModFile.FindEntry("script.txt", true)!=-1)
                            bHasScript = true;
                    }
                    else
                    {
                        if (ModFile.FindEntry("script", true)!=-1)
                            bHasScript = true;
                    }
                }
                else
                {
                    this.CompType = CompressionType.SevenZip;
                }
                /*
                if (LowerFileName.EndsWith(".omod") || LowerFileName.EndsWith(".omod.ghost"))
				    Config=ExtractWholeFile("config");
                else if (LowerFileName.EndsWith(".omod2") || LowerFileName.EndsWith(".omod2.ghost"))
                {
                    Config = ExtractWholeFile("config.ini");
                    bOmod2 = true;
                }*/
                if (Config==null)
                {
                    try
                    {
                        SevenZip.SevenZipExtractor sevenZipExtract = new SevenZip.SevenZipExtractor(Path.Combine(Settings.omodDir, FileName));
                        strTmpDir = Program.CreateTempDirectory();
                        foreach (string file in sevenZipExtract.ArchiveFileNames)
                        {
                            if (file.ToLower()=="config.ini" || file.ToLower() == "config.txt")
                            {
                                Config = new MemoryStream();
                                sevenZipExtract.ExtractFile(file, Config);
                                Config.Seek(0,SeekOrigin.Begin);
                                bOmod2 = true;
                                break;
                            }
                            if (file.ToLower().Contains("fomod\\info.xml"))
                            {
                                StreamWriter sw = new StreamWriter(Path.Combine(strTmpDir, "info.xml"));
                                sevenZipExtract.ExtractFile(file, sw.BaseStream);
                                sw.Close();
                                // make sure that the current dir did not change
                                Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));
                                bFomod = true;

                                break;
                            }// check if script is present
                            if (file.ToLower() == "script.txt" || file.ToLower().Contains("fomod\\script.cs") || file.ToLower().Contains("fomod\\moduleconfig.xml"))
                            {
                                bHasScript = true;
                            }
                        }
                        if (File.Exists(Path.Combine(strTmpDir, "info.xml")))
                        {
                            Config = new StreamReader(Path.Combine(strTmpDir, "info.xml")).BaseStream;
                            bFomod = true;

                        }
                        else if (Config==null)
                        {
                            // no info file???? darn - get website and version out of filename
                            string s = Program.GetModID(path);
                            if (s.Length != 0)
                            {
                                Website = "http://www.nexusmods.com/" + Program.currentGame.NexusName + "/mods/" + s;
                            }
                            try
                            {
                                string version = path.Substring(path.IndexOf(s) + s.Length + 1);
                                version = version.Substring(0, version.LastIndexOf("."));
                                Version = version.Replace('-', '.');
                            }
                            catch
                            { }

                        }
                        List<DataFileInfo> datafileslist = new List<DataFileInfo>();
                        List<string> pluginlist = new List<string>();
                        int fileindex = 0;
                        foreach (string file in sevenZipExtract.ArchiveFileNames)
                        {
                            if (file.ToLower().EndsWith(".esp") || file.ToLower().EndsWith(".esm"))
                                pluginlist.Add(file);
                            else if (!sevenZipExtract.ArchiveFileData[fileindex].IsDirectory)
                                datafileslist.Add(new DataFileInfo(file, sevenZipExtract.ArchiveFileData[fileindex].Crc));
                            fileindex++;
                        }
                        AllDataFiles = datafileslist.ToArray();
                        AllPlugins = pluginlist.ToArray();
                        sevenZipExtract.Dispose();
                    }
                    catch
                    {
                        throw new obmmException("Could not find configuration data");
                    }
//                    if (Config == null)
//                        throw new obmmException("Could not find configuration data");
//                    else
                        //bFomod = true;
                }
                // ghosted mods are hidden
                if (LowerFileName.EndsWith(".ghost"))
                    this.hidden = true;
                if (bOmod2)
                {
                    StreamReader sr = new StreamReader(Config);
                    string line="";
                    bool bInDescriptionText = false;
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (!bInDescriptionText)
                        {
                            if (line.StartsWith("OmodVersion="))
                            {
                            }
                            else if (line.StartsWith("Name="))
                            {
                                ModName = line.Substring(line.IndexOf('=') + 1);
                            }
                            else if (line.StartsWith("MajorVersion="))
                            {
                                MajorVersion = int.Parse(line.Substring(line.IndexOf('=') + 1));
                            }
                            else if (line.StartsWith("MinorVersion="))
                            {
                                MinorVersion = int.Parse(line.Substring(line.IndexOf('=') + 1));
                            }
                            else if (line.StartsWith("Author="))
                            {
                                Author = line.Substring(line.IndexOf('=') + 1);
                            }
                            else if (line.StartsWith("BuildVersion="))
                            {
                                BuildVersion = int.Parse(line.Substring(line.IndexOf('=') + 1));
                            }
                            else if (line.StartsWith("Version="))
                            {
                                Version = line.Substring(line.IndexOf('=') + 1);
                            }
                            else if (line.StartsWith("Email="))
                            {
                                Email = line.Substring(line.IndexOf('=') + 1);
                            }
                            else if (line.StartsWith("Website="))
                            {
                                Website = line.Substring(line.IndexOf('=') + 1);
                            }
                            else if (line.StartsWith("DateTime="))
                            {
                                string sCreationTime = line.Substring(line.IndexOf('=') + 1);
                                if (!DateTime.TryParseExact(sCreationTime, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out CreationTime))
                                {
                                    CreationTime = new FileInfo(path).CreationTime; // new DateTime(2006, 1, 1);
                                }
                            }
                            else if (line.Contains("Description"))
                            {
                                Description = line.Substring(line.IndexOf('=') + 1);
                                bInDescriptionText = true;
                            }
                            else if (line.Contains("SystemMod"))
                            {
                                bSystemMod = bool.Parse(line.Substring(line.IndexOf('=') + 1));
                            }
                        }
                        else
                            Description += "\r\n" + line;
                    }
                    if (Version == "") Version = "" + MajorVersion + ((MinorVersion != -1) ? ("." + MinorVersion + ((BuildVersion != -1) ? ("." + BuildVersion) : "")) : "");
                    AllPlugins = GetPluginList();
                    AllDataFiles = GetDataList();
                    foreach (string s2 in AllPlugins)
                    {
                        if (!Program.IsSafeFileName(s2))
                            throw new obmmException("File " + FileName + " appears to have been modified in a way which could cause a security risk." +
                                                    Environment.NewLine + "tmm will not load it.");
                    }
                    foreach (DataFileInfo dfi in AllDataFiles)
                    {
                        if (!Program.IsSafeFileName(dfi.FileName))
                            throw new obmmException("File " + FileName + " appears to have been modified in a way which could cause a security risk." +
                                                    Environment.NewLine + "tmm will not load it.");
                    }
                }
                else if (bFomod)
                {
                    try
                    {
                        Description = "";
                        Email = "";
                        if (Config != null)
                        {
                            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                            try
                            {
                                StreamReader sr = new StreamReader(Config);
                                doc.Load(sr);
                            }
                            catch (Exception ex)
                            {
                                // could not decode XML
                                Program.logger.WriteToLog("Could not decode XML: "+ex.Message,Logger.LogLevel.High);
                            }
                            DecodeFomodXML(doc, ref  ModName, ref  Version, ref  Author, ref  Description, ref  Website);

                            if (Version.Length > 0)
                            {
                                string numVersion = Version;
                                for (int i = 0; i < numVersion.Length; i++)
                                {
                                    if (numVersion[i] < '0' || numVersion[i] > '9')
                                    {
                                        numVersion = numVersion.Substring(0, i);
                                        break;
                                    }
                                }
                                if (numVersion.Length > 0)
                                {
                                    string[] tvers = numVersion.Split('.');
                                    MajorVersion = Convert.ToInt32(tvers[0]);
                                    if (tvers.Length > 1) MinorVersion = Convert.ToInt32(tvers[1]);
                                    if (tvers.Length > 2) BuildVersion = Convert.ToInt32(tvers[2]);
                                }
                            }
                            string modid = Program.GetModID(LowerFileName);
                            if (modid.Length > 0 && (Website == null || Website.Length == 0))
                            {
                                Website = "http://www.nexusmods.com/" + Program.currentGame.NexusName + "/mods/" + modid;
                            }

                            Config.Close();
                        }
                    }
                    catch { };

                }
                else if (Config!=null)
                {
                    br = new BinaryReader(Config, System.Text.Encoding.GetEncoding("ISO-8859-1"));
                    byte version = br.ReadByte();
                    FileVersion = version;
                    if (version > Program.CurrentOmodVersion)
                    {
                        throw new obmmException(FileName + " was created with a newer version of obmm and could not be loaded");
                    }
                    ModName = br.ReadString();
                    MajorVersion = br.ReadInt32();
                    MinorVersion = br.ReadInt32();
                    Author = br.ReadString();
                    Email = br.ReadString();
                    Website = br.ReadString();
                    Description = br.ReadString();
                    if (version >= 2)
                    {
                        CreationTime = DateTime.FromBinary(br.ReadInt64());
                    }
                    else
                    {
                        string sCreationTime = br.ReadString();
                        if (!DateTime.TryParseExact(sCreationTime, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out CreationTime))
                        {
                            CreationTime = new DateTime(2006, 1, 1);
                        }
                    }
                    if (Description == "") Description = "No description";
                    CompType = (CompressionType)br.ReadByte();
                    if (version >= 1)
                    {
                        BuildVersion = br.ReadInt32();
                    }
                    else BuildVersion = -1;
                    Version = "" + MajorVersion + ((MinorVersion != -1) ? ("." + MinorVersion + ((BuildVersion != -1) ? ("." + BuildVersion) : "")) : "");

                    AllPlugins = GetPluginList();
                    AllDataFiles = GetDataList();
                    foreach (string s2 in AllPlugins)
                    {
                        if (!Program.IsSafeFileName(s2))
                            throw new obmmException("File " + FileName + " appears to have been modified in a way which could cause a security risk." +
                                                    Environment.NewLine + "tmm will not load it.");
                    }
                    foreach (DataFileInfo dfi in AllDataFiles)
                    {
                        if (!Program.IsSafeFileName(dfi.FileName))
                            throw new obmmException("File " + FileName + " appears to have been modified in a way which could cause a security risk." +
                                                    Environment.NewLine + "tmm will not load it.");
                    }
                    CRC = CompressionHandler.CRC(FullFilePath);
                }
			} finally {
				if(br!=null) br.Close();
				Close();
			}
            // acquire proper time if needed
            if (CreationTime.Year < 2010)
                CreationTime = File.GetCreationTime(Path.Combine(Settings.omodDir, FileName));

		}

		public void Close() {
            if (pd != null)
            {
                if (pd.modFile != null)
                {
                    pd.modFile.Close();
                    pd.modFile = null;
                }
                //if (pd.image != null)
                //{
                //    pd.image = null;
                //}
            }
		}

		private void CreateTargetDirectoryStructure() {
			for(int x=0;x<DataFiles.Length;x++) {
				string s=Path.Combine((bSystemMod?Program.currentGame.GamePath:Program.currentGame.DataFolderPath),Path.GetDirectoryName(DataFiles[x].FileName));
				if(s.Length!=0 && !Directory.Exists(s)) Directory.CreateDirectory(s);
			}
		}
        private void CreateDirectoryStructure(DataFileInfo[] FileList, ref string path)
        {
            foreach (DataFileInfo dfi in FileList)
            {
                string sPath = dfi.FileName;
                int upto = 0;
                while (true)
                {
                    int i = sPath.IndexOf('\\', upto);
                    if (i == -1) i = sPath.IndexOf('/', upto); ;
                    if (i == -1) break;
                    string directory = sPath.Substring(0, i);
                    if (!Directory.Exists(Path.Combine(path, directory))) Directory.CreateDirectory(Path.Combine(path, directory));
                    upto = i + 1;
                }
            }
        }

		private ScriptExecutationData ExecuteScript(string plugins, string data) {
			//Execute script
            string script=GetScript();
            if ((script == null || script.Length == 0) && bBAINpackage) script = "BAIN";
            ScriptReturnData srd = null;
            try
            {
                srd = Scripting.ScriptRunner.ExecuteScript(script, data, plugins);
            }
            catch (Exception ex)
            {
                Program.logger.WriteToLog("Script execution failed. ("+ex.Message+") Mod will not be installed", Logger.LogLevel.Low);
                return null;
            }
			bool HasClickedYesToAll;
			bool HasClickedNoToAll;
			//return on fatal error
            if (srd.CopyDataFiles.Count == 0 && srd.CopyPlugins.Count == 0 && srd.EspDeactivation.Count == 0 && srd.EspEdits.Count == 0 && srd.INIEdits.Count == 0 && srd.InstallAllData == false && srd.InstallAllPlugins == false && srd.InstallData.Count == 0 && srd.InstallPlugins.Count == 0 && srd.SDPEdits.Count == 0)
            {
                // nothing was selected. Mod is not installable
                Program.logger.WriteToLog("No action was selected for installation. Mod will not be installed", Logger.LogLevel.Low);
                srd.CancelInstall = true;
            }
			if(srd.CancelInstall) return null;
			//Check that any required mods are already active
			foreach(ConflictData cd in srd.DependsOn) {
				if(!Program.Data.DoesModExist(cd, true)) {
					string s="This mod depends on "+cd.File+", which is either not active or the wrong version";
					if(cd.Comment!=null) s+="\n"+cd.Comment;
                    Program.logger.WriteToLog(s, Logger.LogLevel.Error);
//                    MessageBox.Show(s, "Error");
					return null;
				}
			}
			HasClickedYesToAll=false;
			foreach(ConflictData cd in srd.ConflictsWith) {
				if(Program.Data.DoesModExist(cd, true)) {
					string s;
					if(cd.Comment!=null) s="\n"+cd.Comment; else s="";
					switch(cd.level) {
						case ConflictLevel.Unusable:
//							MessageBox.Show("This mod has a fatal script defined conflict with "+cd.File+" and cant be activated."+s, "Error");
                            Program.logger.WriteToLog("This mod has a fatal script defined conflict with " + cd.File + " and cannot be activated." + s, Logger.LogLevel.Error);
							return null;
						case ConflictLevel.MajorConflict:
							s="This mod has a script defined major conflict with "+cd.File+s+"\nAre you sure you wish to activate it?";
							break;
						case ConflictLevel.MinorConflict:
							s="This mod has a script defined minor conflict with "+cd.File+s+"\nAre you sure you wish to activate it?";
							break;
					}
					if(!HasClickedYesToAll&&MessageBox.Show(s, "Warning", MessageBoxButtons.YesNo)!=DialogResult.Yes) return null;
					else {
						if((System.Windows.Forms.Control.ModifierKeys&Keys.Control)>0) HasClickedYesToAll=true;
					}
				}
			}
			//Update conflicts and dependencies
			ConflictsWith.Clear();
			ConflictsWith.AddRange(srd.ConflictsWith);
			DependsOn.Clear();
			DependsOn.AddRange(srd.DependsOn);
			//Create some variables
			List<string> strtemp1=new List<string>();
			//Fill the Plugins array with the plugins that need to be installed
			if(srd.InstallAllPlugins) {
				foreach(string s in AllPlugins) if(!s.Contains("\\")) strtemp1.Add(s);
			}
			foreach(string s in srd.InstallPlugins) { if(!Program.strArrayContains(strtemp1, s)) strtemp1.Add(s); }
			foreach(string s in srd.IgnorePlugins) { Program.strArrayRemove(strtemp1, s); }
			foreach(ScriptCopyDataFile scd in srd.CopyPlugins) {
				if(!File.Exists(Path.Combine(plugins,scd.CopyFrom))) {
//					MessageBox.Show("The script attempted to copy the plugin '"+scd.hCopyFrom+"', but it did not exist","Warning");
                    Program.logger.WriteToLog("The script attempted to copy the plugin '" + scd.hCopyFrom + "', but it did not exist", Logger.LogLevel.Warning);
                }
                else
                {
					if(scd.CopyFrom!=scd.CopyTo) {
						if(File.Exists(Path.Combine(plugins,scd.CopyTo))) File.Delete(Path.Combine(plugins,scd.CopyTo));
						File.Copy(Path.Combine(plugins,scd.CopyFrom), Path.Combine(plugins,scd.hCopyTo));
					}
					if(!Program.strArrayContains(strtemp1, scd.hCopyTo)) strtemp1.Add(scd.hCopyTo);
				}
			}
			for(int i=0;i<strtemp1.Count;i++) if(!File.Exists(Path.Combine(plugins,strtemp1[i]))) strtemp1.RemoveAt(i--);
			Plugins=strtemp1.ToArray();
			strtemp1.Clear();
			//Fill the data file array with data files to be installed
			if(srd.InstallAllData) {
				for(int i=0;i<AllDataFiles.Length;i++) strtemp1.Add(AllDataFiles[i].FileName);
			}
			foreach(string s in srd.InstallData) { if(!Program.strArrayContains(strtemp1, s)) strtemp1.Add(s); }
			foreach(string s in srd.IgnoreData) { Program.strArrayRemove(strtemp1, s); }
			foreach(ScriptCopyDataFile scd in srd.CopyDataFiles) {
				if(!File.Exists(Path.Combine(data,scd.CopyFrom))) {
//					MessageBox.Show("The script attempted to copy the data file '"+scd.hCopyFrom+"', but it did not exist", "Warning");
                    Program.logger.WriteToLog("The script attempted to copy the data file '" + scd.hCopyFrom + "', but it did not exist", Logger.LogLevel.Warning);
                }
                else
                {
					if(scd.CopyFrom!=scd.CopyTo) {
						if(!Directory.Exists(Path.GetDirectoryName(Path.Combine(data,scd.CopyTo)))) Directory.CreateDirectory(Path.GetDirectoryName(Path.Combine(data,scd.hCopyTo)));
						if(File.Exists(Path.Combine(data,scd.CopyTo))) File.Delete(Path.Combine(data,scd.CopyTo));
						File.Copy(Path.Combine(data,scd.CopyFrom), Path.Combine(data,scd.hCopyTo));
					}
					if(!Program.strArrayContains(strtemp1, scd.hCopyTo)) strtemp1.Add(scd.hCopyTo);
				}
			}
			for(int i=0;i<strtemp1.Count;i++) if(!File.Exists(Path.Combine(data,strtemp1[i]))) strtemp1.RemoveAt(i--);
			List<DataFileInfo> dtemp1 = new List<DataFileInfo>();
			foreach(string s in strtemp1) {
				DataFileInfo dfi;//=Program.Data.GetDataFile(s);
				dfi=Program.strArrayGet(AllDataFiles, s);
				if(dfi!=null) dtemp1.Add(new DataFileInfo(dfi));
				else dtemp1.Add(new DataFileInfo(s, CompressionHandler.CRC(Path.Combine(data,s))));
			}
            List<DataFileInfo> dtemp2 = new List<DataFileInfo>();
            foreach (DataFileInfo dfi in dtemp1)
            {
                if (Path.GetExtension(dfi.FileName).ToLower().StartsWith(".esp"))
                {
                    bool bFound=false;
                    // plugin as data file? Check that it is not already installed as a plugin
                    foreach (string plin in Plugins)
                    {
                        if (plin.ToLower()==dfi.FileName.ToLower())
                        {
                            bFound=true;
                            break;
                        }
                    }
                    if (bFound)
                        continue;
                }
                 dtemp2.Add(dfi);
            }
			DataFiles=dtemp2.ToArray();
			strtemp1.Clear();
			dtemp1.Clear();
			//Register BSAs
			foreach(string s in srd.RegisterBSAList) {
				strtemp1.Add(s);
				BSA b=Program.Data.GetBSA(s);
				if(b==null) {
					OblivionBSA.RegisterBSA(s);
					Program.Data.BSAs.Add(new BSA(s, FileName));
				} else {
					b.UsedBy.Add(FileName);
				}
			}
			BSAs=strtemp1.ToArray();
			//Edit oblivion.ini files
			INIEdits=new List<INIEditInfo>();
			HasClickedNoToAll=false;
			HasClickedYesToAll=false;
			foreach(INIEditInfo iei in srd.INIEdits) {
                if (HasClickedNoToAll || (!HasClickedYesToAll && Settings.WarnOnINIEdit && MessageBox.Show(FileName + " wants to edit " + Program.currentGame.Name + "ini.\n" +
				                                                                                    "Section: "+iei.Section+"\n"+
				                                                                                    "Key: "+iei.Name+"\n"+
				                                                                                    "New value: "+iei.NewValue+"\n"+
				                                                                                    "Do you want to allow it?", "Warning", MessageBoxButtons.YesNo)!=DialogResult.Yes)) {
					if((System.Windows.Forms.Control.ModifierKeys&Keys.Control)>0) HasClickedNoToAll=true;
					continue;
				} else {
					if((System.Windows.Forms.Control.ModifierKeys&Keys.Control)>0) HasClickedYesToAll=true;
				}
				if(Program.Data.INIEdits.Contains(iei)) {
					INIEditInfo edit=Program.Data.INIEdits[Program.Data.INIEdits.IndexOf(iei)];
//					if(HasClickedNoToAll||(!HasClickedYesToAll&&MessageBox.Show("The omod '"+edit.Plugin.FileName+"' has already edited '"+iei.Section+" "+iei.Name+"'\n"+
					if(HasClickedNoToAll||(!HasClickedYesToAll&&MessageBox.Show("The omod '"+edit.omodName+"' has already edited '"+iei.Section+" "+iei.Name+"'\n"+
					                                                            "Do you want to overwrite this change?\n"+
					                                                            "Original value: "+edit.OldValue+"\n"+
					                                                            "Current value: "+edit.NewValue+"\n"+
					                                                            "New value: "+iei.NewValue, "Warning", MessageBoxButtons.YesNo)!=DialogResult.Yes)) {
						if((System.Windows.Forms.Control.ModifierKeys&Keys.Control)>0) HasClickedNoToAll=true;
						continue;
					} else {
						if((System.Windows.Forms.Control.ModifierKeys&Keys.Control)>0) HasClickedYesToAll=true;
					}
					iei.OldValue=edit.OldValue;
					if(edit.Plugin.INIEdits!=null) edit.Plugin.INIEdits.Remove(edit);
					Program.Data.INIEdits.Remove(edit);
				} else {
					iei.OldValue=INI.GetINIValue(iei.Section, iei.Name);
				}
				iei.Plugin=this;
				INI.WriteINIValue(iei.Section, iei.Name, iei.NewValue);
				Program.Data.INIEdits.Add(iei);
				INIEdits.Add(iei);
			}
			
			if(INIEdits.Count==0) INIEdits=null;
			//Edit shader files
			SDPEdits=new List<SDPEditInfo>();
			foreach(SDPEditInfo sei in srd.SDPEdits) {
				if(Classes.OblivionSDP.EditShader(sei.Package, sei.Shader, sei.BinaryObject, FileName)) SDPEdits.Add(sei);
				sei.BinaryObject=null;
			}
			if(SDPEdits.Count==0) SDPEdits=null;
			//return
			ScriptExecutationData sed=new ScriptExecutationData();
			sed.PluginOrder=srd.LoadOrderList.ToArray();
			sed.UncheckedPlugins=srd.UncheckedPlugins.ToArray();
			sed.EspDeactivationWarning=srd.EspDeactivation.ToArray();
			sed.EspEdits=srd.EspEdits.ToArray();
			sed.EarlyPlugins=srd.EarlyPlugins.ToArray();
			return sed;
		}

        private void LogModAction(string action)
        {
            string logfilename=Path.Combine(Program.INIDir,"modaction.log");

            if (File.Exists(logfilename) && new FileInfo(logfilename).Length > 1024 * 1024)
            {
                File.Copy(logfilename, logfilename + ".bak", true);
                File.Delete(logfilename);
            }
            StreamWriter sw = new StreamWriter(logfilename,true);
            sw.WriteLine(""+DateTime.Now+":"+action);
            sw.Close();
        }

		public bool Activate(bool warn) {
            Program.logger.WriteToLog("Activating " + this.FileName, Logger.LogLevel.High);
            bool bRet = true;
            hidden = false;
			bool HasClickedYesToAll;
			bool HasClickedNoToAll;
			//Extract plugins and data files
			string PluginsPath=GetPlugins();
            string DataPath = GetDataFiles(PluginsPath); // put them in same place since some scripts expect that

            // this could be a FOMOD. Let's double check
            //if (!bFomod)
            if (!string.IsNullOrWhiteSpace(PluginsPath))
            {
                // need to change base path?
                string[] dirs=Directory.GetFiles(PluginsPath, "moduleconfig.xml", SearchOption.AllDirectories);
                if (dirs.Length > 0)
                {
                    dirs[0] = dirs[0].ToLower();
                    dirs[0] = dirs[0].Replace("fomod\\moduleconfig.xml", "");
                    PluginsPath = DataPath = dirs[0];
                    bFomod = true;
                }
                else
                {
                    dirs = Directory.GetFiles(PluginsPath, "script.cs", SearchOption.AllDirectories);
                    if (dirs.Length > 0)
                    {
                        dirs[0] = dirs[0].ToLower();
                        dirs[0] = dirs[0].Replace("fomod\\script.cs", "");
                        PluginsPath = DataPath = dirs[0];
                        bFomod = true;
                    }
                }
            }

            string BAINpath = "";
            if (!bFomod) // fomod has precedence over BAIN
            {
                // is this a BAIN package?
                if (DataPath == PluginsPath || DataPath.Length == 0 || PluginsPath.Length == 0)
                {
                    foreach (string dir in Directory.GetDirectories(DataPath, "00 *.*", SearchOption.AllDirectories))
                    {
                        //string shortfile = Path.GetDirectoryName(file.Replace(DataPath, ""));
                        //while (!Path.GetFileName(shortfile).StartsWith("00") && shortfile.Length > 0)
                        //{
                        //    shortfile = Path.GetDirectoryName(shortfile);
                        //}
                        //if (Path.GetFileName(shortfile).StartsWith("00"))
                        //{
                        // It IS a BAIN package
                        bBAINpackage = true;
                        // re-base it
                        BAINpath = Path.GetDirectoryName(dir).Replace(DataPath, "");
                        PluginsPath = DataPath = Path.GetDirectoryName(dir);
                        break;
                        //    }
                    }
                    //if (!bBAINpackage)
                    //{
                    //    foreach (string file in Directory.GetFiles(DataPath, "*.*", SearchOption.AllDirectories))
                    //    {
                    //        string file2 = file.Replace(DataPath, "");
                    //        if (file2.ToLower().StartsWith(Program.DataFolderName.ToLower() + "\\"))
                    //        {
                    //            // rebase
                    //            PluginsPath = DataPath = Path.Combine(DataPath, Program.DataFolderName);
                    //            break;
                    //        }
                    //    }
                    //}
                }
            }
			//Run the attached script
			ScriptExecutationData sed=ExecuteScript(PluginsPath,DataPath);
            //if (bBAINpackage)
            //{
            //    // rebase all path
            //    for (int i=0;i<Plugins.Length;i++)
            //    {
            //        Plugins[i] = Path.Combine(BAINpath, Plugins[i]);
            //    }
            //    for (int i = 0; i < DataFiles.Length; i++)
            //    {
            //        DataFiles[i] = new DataFileInfo(Path.Combine(BAINpath, DataFiles[i].FileName),DataFiles[i].CRC);
            //    }
            //}
            if (sed == null)
            {
                Program.logger.WriteToLog(this.ModName + " was not activated", Logger.LogLevel.Low);
                return false;
            }
			//Final check for serious conflicts
			HasClickedYesToAll=false;
			HasClickedNoToAll=false;
			for(int i=0;i<Plugins.Length;i++) {
				if(Program.Data.DoesEspExist(Plugins[i])||File.Exists(Path.Combine(Program.currentGame.DataFolderPath,Plugins[i]))) {
					if(Array.IndexOf<string>(Program.BannedFiles, Plugins[i].ToLower())!=-1) {
						if(warn) MessageBox.Show(Plugins[i]+" is a protected game base file and cannot be overwritten by mods", "Error");
						Program.ArrayRemoveAt<string>(ref Plugins, i--);
						continue;
					} else if(HasClickedNoToAll||(!HasClickedYesToAll&&warn&&MessageBox.Show(Plugins[i]+" already exists!\n"+
					                                                                         "Overwrite?", "Warning", MessageBoxButtons.YesNo)!=DialogResult.Yes)) {
						if((System.Windows.Forms.Control.ModifierKeys&Keys.Control)>0) HasClickedNoToAll=true;
						Program.ArrayRemoveAt<string>(ref Plugins, i--);
						continue;
					} else {
						if((System.Windows.Forms.Control.ModifierKeys&Keys.Control)>0) HasClickedYesToAll=true;
						EspInfo ei=Program.Data.GetEsp(Plugins[i]);
						if(ei!=null) {
							if(ei.Parent!=null) ei.Parent.UnlinkPlugin(ei);
							Program.Data.Esps.Remove(ei);
							File.Delete(Path.Combine(Program.currentGame.DataFolderPath,ei.FileName));
						} else
                            File.Delete(Path.Combine(Program.currentGame.DataFolderPath,Plugins[i]));
					}
				}
			}
			//copy plugins
			for(int i=0;i<Plugins.Length;i++) {
				File.Move(Path.Combine(PluginsPath,Plugins[i]), Path.Combine(Program.currentGame.DataFolderPath,Plugins[i]));
				EspInfo ei=new EspInfo(Plugins[i], this);
				Program.Data.InsertESP(ei, sed.PluginOrder, Array.IndexOf<string>(sed.EarlyPlugins, ei.LowerFileName)!=-1);
				foreach(string s in sed.UncheckedPlugins) {
					if(s==Plugins[i].ToLower()) ei.Active=false;
				}
				foreach(ScriptEspWarnAgainst s in sed.EspDeactivationWarning) {
					if(s.Plugin==Plugins[i].ToLower()) ei.Deactivatable=s.Status;
				}
				foreach(ScriptEspEdit see in sed.EspEdits) {
					if(see.Plugin!=Plugins[i].ToLower()) continue;
					try {
						if(see.IsGMST) ConflictDetector.TesFile.SetGMST(Path.Combine(Program.currentGame.DataFolderPath,see.Plugin), see.EDID, see.Value);
						else ConflictDetector.TesFile.SetGLOB(Path.Combine(Program.currentGame.DataFolderPath,see.Plugin), see.EDID, see.Value);
					} catch(Exception ex) {
                        Program.logger.WriteToLog("An error occurred editing plugin " + see.Plugin + "\n" + ex.Message, Logger.LogLevel.Error);
//                        MessageBox.Show("An error occured editing plugin " + see.Plugin + "\n" +
//						                ex.Message, "Error");
					}
				}
			}
			//copy data files
			HasClickedYesToAll=false;
			HasClickedNoToAll=false;
			CreateTargetDirectoryStructure();
			
			List<DataFileInfo[]> overwrites = new List<DataFileInfo[]>();
            List<DataFileInfo> conflicts = new List<DataFileInfo>();

            string targetfilename = "";
            string targetroot = (bSystemMod ? "" : Program.currentGame.DataFolderPath+"\\");

			for(int i=0;i<DataFiles.Length;i++)
			{
                if (DataFiles[i].LowerFileName == "image.jpg" || DataFiles[i].LowerFileName == "config.ini" || DataFiles[i].LowerFileName == "readme.txt"
                    || DataFiles[i].LowerFileName == "script.txt" || DataFiles[i].LowerFileName.StartsWith("fomod\\"))
                    continue;
                DataFileInfo dfi = null;
                if (Program.Data.DoesDataFileExist(DataFiles[i]) || File.Exists(Path.Combine(targetroot,DataFiles[i].FileName)))
				{
					dfi = Program.Data.GetDataFile(DataFiles[i]);
					if (dfi == null || dfi.CRC != DataFiles[i].CRC)
					{
                        if (dfi == null)
                        {
                            uint CRC2 = Crc32.ComputeCRC(Path.Combine(targetroot,DataFiles[i].FileName));
                            if (CRC2 != CRC)
                            {
                                Program.logger.WriteToLog("File '" + DataFiles[i].FileName + "' does not belong to any known mod", Logger.LogLevel.High);
                                dfi = new DataFileInfo(DataFiles[i].FileName, CRC2);
                                dfi.OwnerList.Add("unknown");
                                Program.Data.DataFiles.Add(dfi.LowerFileName,dfi); // add it to the list of data files!
                            }
                            else
                                continue;
                        }

						overwrites.Add(new DataFileInfo[] {DataFiles[i], dfi});
                        conflicts.Add(dfi);
					}
				}
			}
			
            if (Settings.bShowSimpleOverwriteForm && overwrites.Count > 0)
                new OverwriteForm(overwrites).ShowDialog();
				
			
			for(int i=0;i<DataFiles.Length;i++)
			{
                if (DataFiles[i].LowerFileName == "image.jpg" || DataFiles[i].LowerFileName == "config.ini" || DataFiles[i].LowerFileName == "readme.txt"
                    || DataFiles[i].LowerFileName.StartsWith("fomod\\"))
                    continue;

				DataFileInfo dfi=null;
                if (Program.Data.DoesDataFileExist(DataFiles[i]) || File.Exists(Path.Combine(targetroot,DataFiles[i].FileName)))
				{
                    Program.logger.WriteToLog("The file '" + DataFiles[i].FileName + "' already exists", Logger.LogLevel.High);

					if(Array.IndexOf<string>(Program.BannedFiles, DataFiles[i].LowerFileName)!=-1)
					{
						if(warn) MessageBox.Show(DataFiles[i].FileName+" is a protected game base file and cannot be overwritten by mods", "Error");
						Program.ArrayRemoveAt<DataFileInfo>(ref DataFiles, i--);
						continue;
					}
					dfi=Program.Data.GetDataFile(DataFiles[i]);
					uint CRC=DataFiles[i].CRC;
                    if (dfi!=null)
                        Program.logger.WriteToLog("New file CRC: " + CRC +" existing CRC: "+dfi.CRC+"  ("+(dfi.CRC==CRC?"same":"different")+") owners: "+dfi.Owners, Logger.LogLevel.High);

                    bool bOverwriteFile = true;
                    if (DataFiles[i].Tag == null) // overwrite form fills out the tag element with a bool for overwrite
                        bOverwriteFile = false;
                    else
                        bOverwriteFile = (bool)DataFiles[i].Tag;

                    Program.logger.WriteToLog("Overwrite is set to "+bOverwriteFile, Logger.LogLevel.High);

                    

					if (dfi != null && dfi.CRC != CRC)
					{
                        // The new file is different (CRC differs)

                        // Is there a file to backup just in case?
                        if (dfi != null && dfi.OwnerList.Count == 1)
                        {

                            // find the owner?
                            bool bFoundOwner = false;
//                            foreach (omod o2 in Program.Data.omods)
                            foreach (string mod in dfi.OwnerList)
                            {
                                if (bFoundOwner)
                                    break;
                                omod o2 = Program.Data.omods.Find(delegate(omod o) { return o.LowerFileName == mod; });

                                if (o2 != null)
                                {
                                    foreach (DataFileInfo dfi2 in o2.DataFiles)
                                    {
                                        if (dfi2.LowerFileName == dfi.LowerFileName)
                                        {
                                            if (dfi2.CRC == dfi.CRC)
                                            {
                                                if (!Settings.bShowSimpleOverwriteForm)
                                                {
                                                    Program.logger.WriteToLog("Making a backup of existing file that belongs to mod " + o2.LowerFileName, Logger.LogLevel.High);
                                                    if (!File.Exists(Path.Combine(Program.ConflictsDir, DataFiles[i].FileName + "." + o2.LowerFileName)))
                                                    {
                                                        targetfilename = Path.Combine(Program.ConflictsDir, DataFiles[i].FileName + "." + o2.LowerFileName);
                                                        Directory.CreateDirectory(Path.GetDirectoryName(targetfilename));
                                                        File.Copy(Path.Combine(targetroot, DataFiles[i].FileName), targetfilename);
                                                    }
                                                }
                                                bFoundOwner = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            if (!bFoundOwner)
                            {
                                if (!Settings.bShowSimpleOverwriteForm)
                                {
                                    Program.logger.WriteToLog("Making a backup of existing file that belongs to unknown mod ", Logger.LogLevel.High);
                                    if (File.Exists(Path.Combine(targetroot, DataFiles[i].FileName)) && !File.Exists(Path.Combine(Program.ConflictsDir, DataFiles[i].FileName + ".unknown")))
                                    {
                                        try
                                        {
                                            targetfilename = Path.Combine(Program.ConflictsDir, DataFiles[i].FileName + ".unknown");
                                            Directory.CreateDirectory(Path.GetDirectoryName(targetfilename));
                                            File.Copy(Path.Combine(targetroot, DataFiles[i].FileName), targetfilename);
                                        }
                                        catch { };
                                    }
                                }
                            }
                        }
					}
                    else if (dfi==null)
                    {
                        // no known existing file - Mod was installed either manually or using a different tool _AND_ not found on first pass
                        Program.logger.WriteToLog("File '" + DataFiles[i].FileName + "' does not belong to any known mod", Logger.LogLevel.High);

                        uint CRC2 = Crc32.ComputeCRC(Path.Combine(targetroot,DataFiles[i].FileName));
                        if (CRC2 != CRC)
                        {
                            DataFileInfo dfi2 = new DataFileInfo(DataFiles[i].FileName, CRC2);

                            if (!Settings.bShowSimpleOverwriteForm)
                            {
                                targetfilename = Path.Combine(Program.ConflictsDir, DataFiles[i].FileName + ".unknown");
                                File.Delete(targetfilename);
                                Directory.CreateDirectory(Path.GetDirectoryName(targetfilename));
                                Program.logger.WriteToLog("Backing up File '" + DataFiles[i].FileName + "'", Logger.LogLevel.High);
                                File.Copy(Path.Combine(targetroot, DataFiles[i].FileName), targetfilename);
                            }
                        }
                    }
                    else if (dfi.CRC == CRC)
                    {
                        // same file already there!
                        Program.logger.WriteToLog("File '" + DataFiles[i].FileName + "' is already present (same CRC)", Logger.LogLevel.High);
                        // copy a backup version in case the other mod file gets removed
                        if (dfi.OwnerList.Count == 1 && !Settings.bShowSimpleOverwriteForm)
                        {
                            targetfilename = Path.Combine(Program.ConflictsDir, DataFiles[i].FileName + "." + dfi.Owners);
                            if (!File.Exists(targetfilename) && File.Exists(Path.Combine(DataPath, DataFiles[i].FileName)))
                            {                                
                                Directory.CreateDirectory(Path.GetDirectoryName(targetfilename));
                                File.Copy(Path.Combine(DataPath, DataFiles[i].FileName), targetfilename);
                            }
                            else
                            {
                                if (!File.Exists(Path.Combine(DataPath, DataFiles[i].FileName)))
                                    Program.logger.WriteToLog("Could not install " + DataFiles[i].FileName + " as it is missing from mod " + ModName, Logger.LogLevel.Warning);
                            }
                        }
                    }

                    // There is already a file. Let's add to the list and reuse it's information
                    if (dfi != null)
                    {
                        dfi.AddOwner(this);
                        DataFiles[i] = dfi;
                    }

                    if (bOverwriteFile)
                    {
                        if (dfi != null)
                            dfi.CRC = CRC; // we use the new file, hence the new CRC
                    }
                    else
                    {
                        // not overwriting but unknown file? Well, let's just not install it then
                        //if(dfi == null)
                        //{
                        //    Program.ArrayRemoveAt<DataFileInfo>(ref DataFiles, i--);
                        //}

                        if (!Settings.bShowSimpleOverwriteForm)
                        {
                            // copy a backup version in case the other mod file gets removed
                            if (!File.Exists(Path.Combine(Program.ConflictsDir, DataFiles[i].FileName + "." + this.LowerFileName)) && File.Exists(Path.Combine(DataPath, DataFiles[i].FileName)))
                            {
                                targetfilename = Path.Combine(Program.ConflictsDir, DataFiles[i].FileName + "." + this.LowerFileName);
                                Directory.CreateDirectory(Path.GetDirectoryName(targetfilename));
                                File.Copy(Path.Combine(DataPath, DataFiles[i].FileName), targetfilename);
                                Program.logger.WriteToLog("Making a backup of new file " + DataFiles[i].FileName, Logger.LogLevel.High);
                            }
                            else
                            {
                                if (!File.Exists(Path.Combine(DataPath,DataFiles[i].FileName)))
                                    Program.logger.WriteToLog("Could not make backup copy of " + DataFiles[i].FileName + " as it is missing from mod " + ModName, Logger.LogLevel.Medium);
                            }
                        }
                        continue;
                    }

				}
				else
				{
				    Program.Data.DataFiles.Add(DataFiles[i].LowerFileName,DataFiles[i]);
				}
                DataFiles[i].AddOwner(this);
                Program.logger.WriteToLog("Writing new file " + DataFiles[i].FileName, Logger.LogLevel.High);
                File.Delete(Path.Combine(targetroot,DataFiles[i].FileName));
                File.Move(Path.Combine(DataPath, DataFiles[i].FileName), Path.Combine(targetroot,DataFiles[i].FileName));

                // potential conflict? Make a backup
                targetfilename = Path.Combine(Program.ConflictsDir, DataFiles[i].FileName + "." + this.LowerFileName);
                if (dfi != null && dfi.OwnerList.Count > 1 && !File.Exists(targetfilename))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(targetfilename));
                    File.Copy(Path.Combine(targetroot, DataFiles[i].FileName), targetfilename);
                }
            }

            if (conflicts.Count > 0 && !Settings.bShowSimpleOverwriteForm)
            {
                // resolve conflicts by picking winners
                ConflictedFilePickerForm conflictfrm = new ConflictedFilePickerForm(conflicts);
                conflictfrm.ShowDialog();
            }

			//mark the mod as active, update other mods conflicts and return
			Conflict=ConflictLevel.Active;
            LogModAction("Activated " + this.FileName);
            Program.logger.WriteToLog("Activated " + this.FileName, Logger.LogLevel.Low);
            return bRet;
        }

		public void AquisitionActivate(bool force) {
			List<DataFileInfo> dataFiles=new List<DataFileInfo>();
			for(int i=0;i<AllDataFiles.Length;i++) {
				if(File.Exists(Path.Combine(Program.currentGame.DataFolderPath,AllDataFiles[i].FileName))) {
					DataFileInfo dfi=Program.Data.GetDataFile(AllDataFiles[i]);
					if(dfi==null) {
						dfi=new DataFileInfo(AllDataFiles[i]);
						Program.Data.DataFiles.Add(dfi.LowerFileName,dfi);
					}
					dfi.AddOwner(this);
					dataFiles.Add(dfi);
				}
			}
			DataFiles=dataFiles.ToArray();

			List<string> plugins=new List<string>();
			for(int i=0;i<AllPlugins.Length;i++) {
				EspInfo ei=Program.Data.GetEsp(AllPlugins[i]);
				if(ei!=null&&ei.Parent==null) {
					ei.Parent=this;
					ei.BelongsTo=FileName;
					plugins.Add(ei.LowerFileName);
				}
			}
			Plugins=plugins.ToArray();

			if(force||dataFiles.Count>0||plugins.Count>0) {
				hidden=false;
				Conflict=ConflictLevel.Active;
			}

			BSAs=new string[0];
			INIEdits=new List<INIEditInfo>();
			SDPEdits=new List<SDPEditInfo>();
		}
        
		public bool Deactivate(bool Force) {
			if(Conflict!=ConflictLevel.Active) return true;
            Program.logger.WriteToLog("Deactivating " + this.FileName, Logger.LogLevel.High);
            //Check for any dependent mods that are still active
			foreach(omod o in Program.Data.omods) {
				if(o.Conflict!=ConflictLevel.Active||o==this) continue;
				foreach(ConflictData cd in o.DependsOn) {
					if(cd==this) {
						if(Force) {
							o.Deactivate(true);
							break;
						} else {
							if(MessageBox.Show("Active mod "+o.FileName+" depends on "+FileName+" and must also be deactivated.\n"+
							                   "Deactivate "+o.FileName+"?", "Question", MessageBoxButtons.YesNo)==DialogResult.Yes) {
								if(!o.Deactivate(false)) return false; else break;
							} else return false;
						}
					}
				}
			}
			try {
				//Deactivate the mod
				Conflict=ConflictLevel.NoConflict;
				ConflictsWith.Clear();
				DependsOn.Clear();
				//Undo any ini or shader edits
				if(INIEdits!=null) {
					foreach(INIEditInfo iei in INIEdits) {
						INI.WriteINIValue(iei.Section, iei.Name, iei.OldValue);
						Program.Data.INIEdits.Remove(iei);
					}
					INIEdits=null;
				}
				if(SDPEdits!=null) {
					foreach(SDPEditInfo sei in SDPEdits) {
						Classes.OblivionSDP.RestoreShader(sei.Package, sei.Shader);
					}
					SDPEdits=null;
				}
				//Clear out the plugins
				foreach(string s in Plugins) {
					EspInfo ei=Program.Data.GetEsp(s);
					if(ei!=null) Program.Data.Esps.Remove(ei);
                    if (File.Exists(Path.Combine(Program.currentGame.DataFolderPath,s))) File.Delete(Path.Combine(Program.currentGame.DataFolderPath,s));
                    if (File.Exists(Path.Combine(Program.currentGame.DataFolderPath,s + ".ghost"))) File.Delete(Path.Combine(Program.currentGame.DataFolderPath,s + ".ghost"));
                }
				//Clear out the data files
                foreach (DataFileInfo dfi in DataFiles)
                {
                    dfi.RemoveOwner(this);
                }
                
                DataFiles = new DataFileInfo[0];
				//Unregister BSAs
				for(int i=0;i<Program.Data.BSAs.Count;i++) {
					BSA b=(BSA)Program.Data.BSAs[i];
					if(b.UsedBy.Contains(FileName)) b.UsedBy.Remove(FileName);
					if(b.UsedBy.Count==0) {
						OblivionBSA.UnregisterBSA(b.FileName);
						Program.Data.BSAs.RemoveAt(i--);
					}
				}
			} catch(Exception ex) {
				if(Force) return true;
//				MessageBox.Show("An error occured trying to deactivate "+FileName+".\n"+ex.Message, "Error");
                Program.logger.WriteToLog("An error occurred trying to deactivate " + FileName + ".\n" + ex.Message, Logger.LogLevel.Error);
                return false;
			}
            LogModAction("Deactivated "+this.FileName);
            Program.logger.WriteToLog("Deactivated " + this.FileName, Logger.LogLevel.Low);
			return true;
		}

		public void DeletionDeactivate() {
            Program.logger.WriteToLog("DeletionDeactivate()", Logger.LogLevel.High);
            //Undo any ini or shader edits
			if(INIEdits!=null) {
				foreach(INIEditInfo iei in INIEdits) {
					INI.WriteINIValue(iei.Section, iei.Name, iei.OldValue);
					Program.Data.INIEdits.Remove(iei);
				}
				INIEdits=null;
			}
			if(SDPEdits!=null) {
				foreach(SDPEditInfo sei in SDPEdits) {
					Classes.OblivionSDP.RestoreShader(sei.Package, sei.Shader);
				}
				SDPEdits=null;
			}
			//Clear out the plugins
            if (Plugins != null)
            {
                foreach (string s in Plugins)
                {
                    EspInfo ei = Program.Data.GetEsp(s);
                    if (ei != null) Program.Data.Esps.Remove(ei);
                    if (File.Exists(Path.Combine(Program.currentGame.DataFolderPath, s))) File.Delete(Path.Combine(Program.currentGame.DataFolderPath, s));
                    if (File.Exists(Path.Combine(Program.currentGame.DataFolderPath, s + ".ghost"))) File.Delete(Path.Combine(Program.currentGame.DataFolderPath, s + ".ghost"));
                }
            }
			//Clear out the data files
            if (DataFiles != null)
            {
                foreach (DataFileInfo dfi in DataFiles)
                {
                    dfi.RemoveOwner(this);
                }
            }

			//Clear some public data
			ConflictsWith.Clear();
			DependsOn.Clear();
			Plugins=null;
			DataFiles=null;

			//make sure this mod doesn't exist in the main data list
			if(Program.Data.omods.Contains(this)) Program.Data.omods.Remove(this);
            LogModAction("Deletion/Deactivated " + this.FileName);
            Program.logger.WriteToLog("Deletion/Deactivated " + this.FileName, Logger.LogLevel.Low);
        }

		public void Clean() {
			int a=0, b=0, c=0;
			Clean(ref a, ref b, ref c);
		}
		public void Clean(ref int DeletedCount, ref int SkippedCount, ref int NotFoundCount) {
            Program.logger.WriteToLog("Clean()", Logger.LogLevel.High);
            //scan for ghost data file links
			//Not required anymore?
			//for(int i=0;i<Program.Data.DataFiles.Count;i++) {
			//    DataFileInfo dfi=Program.Data.DataFiles[i];
			//    if(dfi.UsedBy.Contains(LowerFileName)) dfi.UsedBy.Remove(LowerFileName);
			//    if(dfi.UsedBy.Count==0) Program.Data.DataFiles.RemoveAt(i--);
			//}
			//delete plugins
			foreach(string s in AllPlugins) {
                if (!File.Exists(Path.Combine(Program.currentGame.DataFolderPath,s)) && !File.Exists(Path.Combine(Program.currentGame.DataFolderPath,s+".ghost")))
                {
					NotFoundCount++;
					continue;
				}
				if(Array.IndexOf<string>(Program.BannedFiles, s.ToLower())!=-1) {
					SkippedCount++;
					continue;
				}
				EspInfo ei=Program.Data.GetEsp(s);
				if(ei==null||ei.BelongsTo==EspInfo.UnknownOwner) {
                    if (File.Exists(Path.Combine(Program.currentGame.DataFolderPath, s))) File.Delete(Path.Combine(Program.currentGame.DataFolderPath, s));
                    if (File.Exists(Path.Combine(Program.currentGame.DataFolderPath, s + ".ghost"))) File.Delete(Path.Combine(Program.currentGame.DataFolderPath, s + ".ghost"));
                    if (ei != null) Program.Data.Esps.Remove(ei);
					DeletedCount++;
				} else SkippedCount++;
			}
			//delete data files
			for(int i=0;i<AllDataFiles.Length;i++) {
				string s=AllDataFiles[i].FileName;
				if(!File.Exists(Path.Combine(Program.currentGame.DataFolderPath,s))) {
					NotFoundCount++;
					continue;
				}
				if(Array.IndexOf<string>(Program.BannedFiles, s.ToLower())!=-1) {
					SkippedCount++;
					continue;
				}
				if(!Program.Data.DoesDataFileExist(s)) {
					File.Delete(Path.Combine(Program.currentGame.DataFolderPath,s));
					DeletedCount++;
				} else SkippedCount++;
			}
		}

        private string[] GetPluginList()
        {
            return GetPluginList(null);
        }
        private string[] GetPluginList(string strTempDir)
        {
            List<string> ar = new List<string>();

/*
            if (LowerFileName.EndsWith(".omod2") || LowerFileName.EndsWith(".omod2.ghost"))
                bOmod2 = true;
            else if (!LowerFileName.EndsWith(".omod") || LowerFileName.EndsWith(".omod.ghost"))
                bFomod = true;

            if (bOmod2)
            {
                foreach (ZipEntry e in ModFile)
                {
                    if (e.Name.ToLower().EndsWith(".esp") || e.Name.ToLower().EndsWith(".esm"))
                        ar.Add(e.Name);
                }
            }
            else if (bFomod)
            {
                SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Settings.omodDir + FileName);
                foreach (string file in zextract.ArchiveFileNames)
                {
                    if (file.ToLower().EndsWith(".esp") || file.ToLower().EndsWith(".esm"))
                    {
                        ar.Add(file);
                    }
                }
//                string[] esps = Directory.GetFiles(strTempDir, "*.es?", SearchOption.AllDirectories);
//                foreach (string esp in esps)
//                {
//                    ar.Add(esp.Replace(strTempDir,""));
//                }
            }
            else
            {
                Stream TempStream = ExtractWholeFile("plugins.crc");
                if (TempStream == null) return new string[0];
                BinaryReader br = new BinaryReader(TempStream);
                while (br.PeekChar() != -1)
                {
                    ar.Add(br.ReadString());
                    br.ReadInt32();
                    br.ReadInt64();
                }
                br.Close();
            }
*/

            if (FileName.ToLower().EndsWith(".omod") || (CompType == CompressionType.Zip && ModFile != null))
            {
                Stream TempStream = ExtractWholeFile("plugins.crc");
                if (TempStream != null)
                {
                    BinaryReader br = new BinaryReader(TempStream);
                    while (br.PeekChar() != -1)
                    {
                        ar.Add(br.ReadString());
                        br.ReadInt32();
                        br.ReadInt64();
                    }
                    br.Close();
                    TempStream.Close();
                }
                else // omod2
                {
                    foreach (ZipEntry e in ModFile)
                    {
                        if (e.Name.ToLower().EndsWith(".esp") || e.Name.ToLower().EndsWith(".esm"))
                            ar.Add(e.Name);
                    }
                }
            }
            else // FOMOD or 7z omod2
            {
                SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Path.Combine(Settings.omodDir,FileName));
                foreach (string file in zextract.ArchiveFileNames)
                {
                    if (file.ToLower().EndsWith(".esp") || file.ToLower().EndsWith(".esm"))
                    {
                        ar.Add(file);
                    }
                }
            }
            
            return ar.ToArray();
		}

        private DataFileInfo[] GetDataList()
        {
            return GetDataList(null);
        }
        private DataFileInfo[] GetDataList(string strTempDir)
        {
            List<DataFileInfo> ar = new List<DataFileInfo>();

            /*
            if (LowerFileName.EndsWith(".omod2") || LowerFileName.EndsWith(".omod2.ghost"))
                bOmod2 = true;
            else if (!LowerFileName.EndsWith(".omod") || LowerFileName.EndsWith(".omod.ghost"))
                bFomod = true;

            if (bOmod2)
            {
                foreach (ZipEntry e in ModFile)
                {
                    if (!e.Name.ToLower().EndsWith(".esp") && !e.Name.ToLower().EndsWith(".esm") && !e.Name.EndsWith("\\") && !e.Name.EndsWith("/"))
                        ar.Add(new DataFileInfo(e.Name, (uint)e.Crc));
                }
            }
            else if (bFomod)
            {
                SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Settings.omodDir + FileName);
                foreach (string file in zextract.ArchiveFileNames)
                {
                    if (!file.ToLower().EndsWith(".esp") && !file.ToLower().EndsWith(".esm") && !file.EndsWith("\\") && !file.EndsWith("/"))
                    {
                        ar.Add(new DataFileInfo(file,(uint)0));
                    }
                }
                zextract.Dispose();


//                string[] files = Directory.GetFiles(strTempDir, "*.*", SearchOption.AllDirectories);
//                foreach (string file in files)
//                {
//                    if (Path.GetExtension(file).ToLower()!="esp"  && Path.GetExtension(file).ToLower()!="esm")
//                        ar.Add(new DataFileInfo(file.Replace(strTempDir, ""), (uint)0));
//                }
            }
            else
            {
                Stream TempStream = ExtractWholeFile("data.crc");
                if (TempStream == null) return new DataFileInfo[0];
                BinaryReader br = new BinaryReader(TempStream);
                while (br.PeekChar() != -1)
                {
                    string s = br.ReadString();
                    ar.Add(new DataFileInfo(s, br.ReadUInt32()));
                    br.ReadInt64();
                }
                br.Close();
            }
            */

            if (FileName.ToLower().EndsWith(".omod") || (CompType == CompressionType.Zip && ModFile != null))
            {
                Stream TempStream = ExtractWholeFile("data.crc");
                if (TempStream != null)
                {
                    BinaryReader br = new BinaryReader(TempStream);
                    while (br.PeekChar() != -1)
                    {
                        string s = br.ReadString();
                        ar.Add(new DataFileInfo(s, br.ReadUInt32()));
                        br.ReadInt64();
                    }
                    br.Close();
                    TempStream.Close();
                }
                else // omod2
                {
                    foreach (ZipEntry e in ModFile)
                    {
                        if (!e.Name.ToLower().EndsWith(".esp") && !e.Name.ToLower().EndsWith(".esm") &&
                            e.Name.ToLower() != "script.txt" && e.Name.ToLower() != "config.ini" && e.Name.ToLower() != "image.jpg" && e.Name.ToLower() != "readme.txt" && !e.Name.EndsWith("\\") && !e.Name.EndsWith("/"))
                            ar.Add(new DataFileInfo(e.Name, (uint)e.Crc));
                    }
                }
            }
            else
            {
                SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Path.Combine(Settings.omodDir, FileName));
                int fileindex = 0;
                foreach (string file in zextract.ArchiveFileNames)
                {
                    if (!file.ToLower().EndsWith(".esp") && !file.ToLower().EndsWith(".esm") &&
                        file.ToLower() != "script.txt" && file.ToLower() != "config.ini" && file.ToLower() != "image.jpg" && file.ToLower() != "readme.txt" && !file.EndsWith("\\") && !file.EndsWith("/") &&
                        !zextract.ArchiveFileData[fileindex].IsDirectory)
                    {
                        ar.Add(new DataFileInfo(file, zextract.ArchiveFileData[fileindex].Crc));
                    }
                    fileindex++;
                }
                zextract.Dispose();
            }

			return ar.ToArray();
		}

		public string GetPlugins() {

            Program.logger.WriteToLog("omod.GetPlugins",Logger.LogLevel.High);

            string path = "";

            if (FileName.ToLower().EndsWith(".omod") || (CompType == CompressionType.Zip && ModFile != null))
            {
                Stream s = ExtractWholeFile("plugins.crc");
                Stream s2 = ExtractWholeFile("data.crc");
                if (s != null || s2 != null)
                {
                    if (s != null)
                    {
                        s.Close();
                        path = ParseCompressedStream("plugins.crc", "plugins");
                    }
                    s2?.Close();
                }
                else // omod2
                {
                    Program.logger.WriteToLog(" bOmod2=true", Logger.LogLevel.High);
                    try
                    {
                        path = Program.CreateTempDirectory();
                        string filepath = path;
                        foreach (ZipEntry e in ModFile)
                        {
                            if (e.Name.ToLower().EndsWith(".esp") || e.Name.ToLower().EndsWith(".esm"))
                            {
                                Program.logger.WriteToLog(" Extracting " + e.Name, Logger.LogLevel.High);
                                ExtractWholeFile(e.Name, ref filepath).Close();
                                Program.logger.WriteToLog(" Moving " + filepath + " to " + Path.Combine(path, e.Name), Logger.LogLevel.High);
                                Directory.CreateDirectory(Path.Combine(path, Path.GetDirectoryName(e.Name.Replace("/", "\\"))));
                                File.Move(filepath, Path.Combine(path, e.Name.Replace("/","\\")));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Could not get plugins: " + ex.Message, "Error extracting plugins", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Program.logger.WriteToLog("Could not get plugins: " + ex.Message, Logger.LogLevel.Error);
                    }
                }
            }
            else // fomod
            {
                Program.logger.WriteToLog(" 7z mod", Logger.LogLevel.High);
                //ProgressForm 
                Program.pf = new ProgressForm("Extracting plugins", false);
                try
                {
                    string currentDirectory = Directory.GetCurrentDirectory();

                    path = Program.CreateTempDirectory();
                    SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Path.Combine(Settings.omodDir, FileName));
                    Program.pf.SetProgressRange(zextract.ArchiveFileNames.Count);
                    Program.pf.UseWaitCursor = false;
                    Application.UseWaitCursor = false;
                    Program.pf.Show();
                    int count = 0;
                    List<string> files = new List<string>(zextract.ArchiveFileNames);
                    foreach (string file in zextract.ArchiveFileNames)
                    {
                        if (!file.ToLower().EndsWith(".esp") && !file.ToLower().EndsWith(".esm"))
                        {
                            files.Remove(file);
                        }
                        Program.pf.UpdateProgress(count);
                        //                    if (file.ToLower().EndsWith(".esp") || file.ToLower().EndsWith(".esm"))
                        //                    {
                        //                        Directory.CreateDirectory(Path.GetDirectoryName(Path.Combine(path, file)));
                        //                        StreamWriter sw = new StreamWriter(Path.Combine(path, file));
                        //                        zextract.ExtractFile(file,sw.BaseStream);
                        //                        sw.Close();
                        //                    }
                        count++;
                    }

                    FileSystemWatcher fswatch = new FileSystemWatcher(path, "*.*");
                    fswatch.IncludeSubdirectories = true;
                    fswatch.Created += Program.fswatch_Created;
                    fswatch.EnableRaisingEvents = true;
                    Program.bEnableFileSystemWatcher = true;

                    zextract.ExtractFiles(path, files.ToArray());
                    zextract.Dispose();

                    Program.bEnableFileSystemWatcher = false;
                    fswatch.EnableRaisingEvents = false;

                    // make sure that the current dir did not change
                    Directory.SetCurrentDirectory(currentDirectory);
                }
                finally
                {
                    Program.pf.Hide();
                    Program.pf.Close();
                    Program.pf.Dispose();
                    Program.pf = null;
                }
            }

            Program.logger.WriteToLog("Finished omod.GetPlugins", Logger.LogLevel.High);
            return path;
		}

        public string GetDataFiles()
        {
            string path = "";
            path = Program.CreateTempDirectory();
            return GetDataFiles(path);
        }
		public string GetDataFiles(string path) {

            /*
            if (LowerFileName.EndsWith(".omod2") || LowerFileName.EndsWith(".omod2.ghost"))
                bOmod2 = true;
            else if (!LowerFileName.EndsWith(".omod") || LowerFileName.EndsWith(".omod.ghost"))
                bFomod = true;

            if (bOmod2)
            {
                string filepath = path;
                CreateDirectoryStructure(AllDataFiles, ref path);
                foreach (ZipEntry e in ModFile)
                {
                    if (!e.Name.ToLower().EndsWith(".esp") && !e.Name.ToLower().EndsWith(".esm") && !e.Name.EndsWith("\\") && !e.Name.EndsWith("/"))
                    {
                        ExtractWholeFile(e.Name, ref filepath).Close();
                        if (File.Exists(Path.Combine(path, e.Name))) File.Delete(Path.Combine(path, e.Name));
                        File.Move(filepath, Path.Combine(path,e.Name));
                    }
                }
            }
            else if (bFomod)
            {
                ProgressForm pf = new ProgressForm("Extracting data files", false);
                if (path==null) path = Program.CreateTempDirectory();
                SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Settings.omodDir + FileName);
                pf.SetProgressRange(zextract.ArchiveFileNames.Count);
                pf.UseWaitCursor = false;
                Application.UseWaitCursor = false;
                pf.Show();
                //int count = 0;
                List<string> files = new List<string>(zextract.ArchiveFileNames);
                foreach (string file in zextract.ArchiveFileNames)
                {
                    if (file.ToLower().EndsWith(".esp") || file.ToLower().EndsWith(".esm"))
                    {
                        files.Remove(file);
                    }
                    //                    pf.UpdateProgress(count);
                    //                    if (!file.ToLower().Contains(".esp") && !file.ToLower().Contains(".esm"))
                    //                    {
                    //                        if (Path.GetExtension(Path.Combine(path, file)).Length==0)
                    //                            Directory.CreateDirectory(Path.Combine(path, file));
                    //                        else
                    //                            Directory.CreateDirectory(Path.GetDirectoryName(Path.Combine(path, file)));
                    //                        StreamWriter sw = null;
                    //                        try
                    //                        {
                    //                            sw = new StreamWriter(Path.Combine(path, file));
                    //                            try { zextract.ExtractFile(file, sw.BaseStream); }
                    //                            catch (ArgumentOutOfRangeException ex) {}; // entry was a folder. nothing to extract
                    //                            sw.Close();
                    //                        }
                    //                        catch (UnauthorizedAccessException ex) {}; // cannot create a SW to a folder
                    //                        
                    //                    }
                    //                    count++;
                }
                zextract.ExtractFiles(path, files.ToArray());
                zextract.Dispose();
                pf.Hide();
                pf.Close();
                pf.Dispose();
            }
            else
                path= ParseCompressedStream("data.crc", "data");
            */

            if (FileName.ToLower().EndsWith(".omod") || (CompType == CompressionType.Zip && ModFile != null))
            {
                Stream s = ExtractWholeFile("data.crc");
                Stream s2 = ExtractWholeFile("plugins.crc");
                if (s != null || s2 != null)
                {
                    if (s != null)
                    {
                        s.Close();
                        path = ParseCompressedStream("data.crc", "data");
                    }
                    s2?.Close();
                }
                else
                {
                    string filepath = path;
                    CreateDirectoryStructure(AllDataFiles, ref path);
                    foreach (ZipEntry e in ModFile)
                    {
                        if (!e.Name.ToLower().EndsWith(".esp") && !e.Name.ToLower().EndsWith(".esm") &&
                             e.Name.ToLower() != "script.txt" && e.Name.ToLower() != "config.ini" && 
                             e.Name.ToLower() != "image.jpg" && e.Name.ToLower() != "readme.txt" &&
                             e.Name.ToLower() != "image" && e.Name.ToLower() != "readme" &&
                             e.Name.ToLower() != "config" && e.Name.ToLower() != "plugins" &&
                             e.Name.ToLower() != "plugins.crc" &&
                             !e.Name.EndsWith("\\") && !e.Name.EndsWith("/"))
                        {
                            ExtractWholeFile(e.Name, ref filepath).Close();
                            if (File.Exists(Path.Combine(path, e.Name))) File.Delete(Path.Combine(path, e.Name));
                            try
                            {
                                File.Move(filepath, Path.Combine(path, e.Name));
                            }
                            catch (Exception ex)
                            {
                                Program.logger.WriteToLog("Could not extract file '" + filepath + "' to '" + Path.Combine(path, e.Name) + "': " + ex.Message, Logger.LogLevel.Low);
                            }
                        }
                    }
                }

            }
            else
            {
                Program.pf = new ProgressForm("Extracting data files", false);
                try
                {
                    string currentDirectory = Directory.GetCurrentDirectory();

                    if (path == null) path = Program.CreateTempDirectory();
                    SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Path.Combine(Settings.omodDir, FileName));
                    Program.pf.SetProgressRange(zextract.ArchiveFileNames.Count);
                    Program.pf.UseWaitCursor = false;
                    Application.UseWaitCursor = false;
                    Program.pf.Show();
                    List<string> files = new List<string>(zextract.ArchiveFileNames);
                    foreach (string file in zextract.ArchiveFileNames)
                    {
                        if (file.ToLower().EndsWith(".esp") || file.ToLower().EndsWith(".esm") ||
                            file.ToLower() == "script.txt" || file.ToLower() == "config.ini" || file.ToLower() == "image.jpg" || file.ToLower() == "readme.txt")
                        {
                            files.Remove(file);
                        }

                    }
                    FileSystemWatcher fswatch = new FileSystemWatcher(path, "*.*");
                    fswatch.IncludeSubdirectories = true;
                    fswatch.Created += Program.fswatch_Created;
                    fswatch.EnableRaisingEvents = true;
                    Program.bEnableFileSystemWatcher = true;

                    Program.pf.SetProgressRange(files.Count);
                    zextract.FileExtractionFinished += new EventHandler<SevenZip.FileInfoEventArgs>(Program.sevenZipExtract_FileExtractionFinished);
                    zextract.ExtractFiles(path, files.ToArray());
                    zextract.Dispose();

                    Program.bEnableFileSystemWatcher = false;
                    fswatch.EnableRaisingEvents = false;

                    // make sure that the current dir did not change
                    Directory.SetCurrentDirectory(currentDirectory);
                }
                finally
                {
                    if (Program.pf != null)
                    {
                        Program.pf.Hide();
                        Program.pf.Close();
                        Program.pf.Dispose();
                        Program.pf = null;
                    }
                }
            }

            return path;
        }


		public string GetReadme() {
            Stream s=null;
            string readme = "";

            /*
            if (LowerFileName.EndsWith(".omod2") || LowerFileName.EndsWith(".omod2.ghost"))
                bOmod2 = true;
            else if (!LowerFileName.EndsWith(".omod") || LowerFileName.EndsWith(".omod.ghost"))
                bFomod = true;

            if (bFomod)
                s = null;
            else if (bOmod2)
            {
                s = ExtractWholeFile("readme.txt");
                if (s == null) return null;
                BinaryReader br = new BinaryReader(s);
                readme = new string(br.ReadChars((int)s.Length));
                br.Close();
            }
            else
            {
                s = ExtractWholeFile("readme");
                if (s == null) return null;
                BinaryReader br = new BinaryReader(s);
                readme = br.ReadString();
                br.Close();
            }
            */
            if (this.LowerFileName.EndsWith(".omod") || this.LowerFileName.EndsWith(".omod.ghost"))
                s = ExtractWholeFile("readme.txt");
            else
            {
                SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Path.Combine(Settings.omodDir, FileName));
                string strTmpDir = Program.CreateTempDirectory();
                string readmefile = "";
                foreach (string file in zextract.ArchiveFileNames)
                {
                    if (file.ToLower().Contains("readme.txt"))
                    {
                        readmefile = file;
                        break;
                    }
                }
                if (readmefile != "")
                {
                    StreamWriter sw = new StreamWriter(Path.Combine(strTmpDir, "readme.txt")); ;
                    zextract.ExtractFile(readmefile, sw.BaseStream);
                    sw.Close();
                    s = new StreamReader(Path.Combine(strTmpDir, "readme.txt")).BaseStream;
                    // make sure that the current dir did not change
                    Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));

                }
                zextract.Dispose();
            }

            if (s != null)
            {
                BinaryReader br = new BinaryReader(s);
                readme = new string(br.ReadChars((int)s.Length));
                br.Close();
                s.Close();
            }
            else
            {
                s = ExtractWholeFile("readme");
                if (s != null)
                {
                    BinaryReader br = new BinaryReader(s);
                    readme = br.ReadString();
                    br.Close();
                    s.Close();
                }
            }
			return readme;
		}

		public string GetScript() {
            Stream s=null;
            string script = "";

            /*
            if (LowerFileName.EndsWith(".omod2") || LowerFileName.EndsWith(".omod2.ghost"))
                bOmod2 = true;
            else if (!LowerFileName.EndsWith(".omod") || LowerFileName.EndsWith(".omod.ghost"))
                bFomod = true;

            if (bFomod)
            {

                
                // TODO: make this generic? turn alldatafiles/allplugins into list (to enable searches?)
                // use SevenZip for EVERYTHING?

                SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Settings.omodDir+FileName);
                string strTmpDir = Program.CreateTempDirectory();
                string scriptfile = "";
                foreach (string file in zextract.ArchiveFileNames)
                {
                    if (file.ToLower().Contains("fomod\\script.cs") || file.ToLower().Contains("fomod\\moduleconfig.xml"))
                    {
                        scriptfile = file;
                    }
                }
                if (scriptfile!="")
                {
                    StreamWriter sw = new StreamWriter(Path.Combine(strTmpDir, "script.txt")); ;
                    zextract.ExtractFile(scriptfile, sw.BaseStream);
                    sw.Close();
                    Stream ms = new StreamReader(Path.Combine(strTmpDir, "script.txt")).BaseStream;
                    BinaryReader br2 = new BinaryReader(ms);
                    script = (scriptfile.EndsWith(".cs") ? (char)(ScriptType.cSharp) : (char)(ScriptType.xml)) + new string(br2.ReadChars((int)br2.BaseStream.Length));
                    br2.Close();
                    ms.Close();
                }
                zextract.Dispose();
            }
            else if (bOmod2)
            {
                s = ExtractWholeFile("script.txt");
                if (s == null) return null;
                BinaryReader br = new BinaryReader(s);
                script = new string(br.ReadChars((int)br.BaseStream.Length)); // br.ReadString();
                if (script[0] > 10)
                {
                    if (script.Contains("<con"))
                        script = (char)(ScriptType.xml) + script;
                    else if (script.Contains("using  System;"))
                        script = (char)(ScriptType.cSharp) + script;
                    else
                        script = (char)(ScriptType.obmmScript) + script;
                }
                br.Close();
            }
            else
            {
                s = ExtractWholeFile("script");
                if (s == null) return null;
                BinaryReader br = new BinaryReader(s);
                script = br.ReadString(); //new string(br.ReadChars((int)br.BaseStream.Length)); // br.ReadString();
                //            while (br.BaseStream.Position != br.BaseStream.Length)
                //            {
                //                try { script += br.ch; }
                //                catch { };
                //            }
                br.Close();
            }
            */

            if (FileName.ToLower().EndsWith(".omod") || (CompType == CompressionType.Zip && ModFile != null))
            {
                s = ExtractWholeFile("script");
                if (s != null)
                {
                    BinaryReader br = new BinaryReader(s);
                    script = br.ReadString();
                    br.Close();
                    s.Close();
                }
                else // omod2
                {
                    s = ExtractWholeFile("script.txt");
                    if (s != null)
                    {
                        BinaryReader br = new BinaryReader(s);
                        script = new string(br.ReadChars((int)br.BaseStream.Length)); // br.ReadString();
                        if (script[0] > 10)
                        {
                            if (script.Contains("<con"))
                                script = (char)(ScriptType.xml) + script;
                            else if (script.Contains("using  System;"))
                                script = (char)(ScriptType.cSharp) + script;
                            else
                                script = (char)(ScriptType.obmmScript) + script;
                        }
                        br.Close();
                        s.Close();
                    }
                }
            }
            if (s==null)
            {
                // TODO: make this generic? turn alldatafiles/allplugins into list (to enable searches?)
                // use SevenZip for EVERYTHING?

                SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Path.Combine(Settings.omodDir, FileName));
                string strTmpDir = Program.CreateTempDirectory();
                string scriptfile = "";
                foreach (string file in zextract.ArchiveFileNames)
                {
                    if (file.ToLower().Contains("fomod\\script.cs"))
                    {
                        scriptfile = file;
                        fomodroot = file.ToLower().Replace("fomod\\script.cs", "");
                        break;
                    }
                    else if (file.ToLower().Contains("fomod\\moduleconfig.xml"))
                    {
                        scriptfile = file;
                        fomodroot = file.ToLower().Replace("fomod\\moduleconfig.xml","");
                        break;
                    }
                    if (file.ToLower()=="script.txt")
                    {
                        scriptfile = file;
                        break;
                    }
                }
                if (scriptfile != "")
                {
                    StreamWriter sw = new StreamWriter(Path.Combine(strTmpDir, "script.txt")); ;
                    zextract.ExtractFile(scriptfile, sw.BaseStream);
                    sw.Close();
                    // make sure that the current dir did not change
                    Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));

                    Stream ms = new StreamReader(Path.Combine(strTmpDir, "script.txt")).BaseStream;
                    BinaryReader br2 = new BinaryReader(ms);
                    string scripttext = new string(br2.ReadChars((int)br2.BaseStream.Length));
                    if (scriptfile.EndsWith(".cs"))
                        script=""+(char)(ScriptType.cSharp);
                    else if (scriptfile.EndsWith(".xml"))
                        script=""+(char)(ScriptType.xml);
                    else if (scriptfile.EndsWith(".txt"))
                    {
                        if (scripttext[0] < ' ')
                        {
                        }
                        else if (script.Contains("<con"))
                            script = ""+(char)(ScriptType.xml);
                        // could be omod (\0) or XML or CS
                        else if (scripttext.Contains("DontInstall"))
                            script = "" + (char)(ScriptType.obmmScript);
                        else if (scripttext.Contains("using  System;"))
                        {
                            script = "" + (char)(ScriptType.cSharp);
                        }
                        else
                            script = "" + (char)(ScriptType.obmmScript);
                    }
                    script += scripttext;
                    br2.Close();
                    ms.Close();
                }
                zextract.Dispose();
            }
			return script;
		}

		public string GetImage() {
            string BitmapPath = null;
            Stream s=null;

            if (FileName.ToLower().EndsWith(".omod") || (CompType == CompressionType.Zip && ModFile != null))
            {
                s = ExtractWholeFile("image", ref BitmapPath);
                if (s==null)
                    s = ExtractWholeFile("image.jpg", ref BitmapPath);
            }
            else
            {
                SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Path.Combine(Settings.omodDir, FileName));
                foreach (string file in zextract.ArchiveFileNames)
                {
                    if (file.ToLower() == "image.jpg")
                    {
                        s = new MemoryStream();
                        zextract.ExtractFile(file, s);

                        // make sure that the current dir did not change
                        Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));

                        string filename = "";
                        BinaryWriter sw = new BinaryWriter(Program.CreateTempFile(out filename));
                        byte[] img = new byte[s.Length];
                        s.Seek(0, SeekOrigin.Begin);
                        s.Read(img, 0, (int)s.Length);
                        //s.Close();
                        sw.Write(img, 0, (int)img.Length);
                        sw.Close();
                        zextract.Dispose();
                        BitmapPath = filename;
                        break;
                    }
                }

                if (File.Exists(Path.Combine("cache", this.FileName)))
                {
                    omod o = new omod(Path.Combine("cache", this.FileName), false);
                    s = o.ExtractWholeFile("config.txt", ref BitmapPath);
                }
            }
            if (s == null)
                BitmapPath = null;
            else
                s.Close();

            return BitmapPath;
		}
		
		/*public string[] GetPluginList()
		{
			List<string> pluginList = new List<string>();
			if (AllPlugins != null && AllPlugins.Length > 0)
			{
				foreach(string s in AllPlugins)
					pluginList.Add(s);
			}
			return pluginList.ToArray();
		}
		public string[] GetDataList()
		{
			List<string> dataList = new List<string>();
			if (AllDataFiles != null && AllDataFiles.Length > 0)
			{
				foreach(DataFileInfo df in AllDataFiles)
					dataList.Add(df.FileName);
			}
			return dataList.ToArray();
		}*/
		public void AppendFileList(string[] files, System.Text.StringBuilder sb)
		{
			foreach(string f in files)
			{
				sb.AppendLine(f.Replace(@"\", @"\\"));
			}
		}
		public void AppendFileList(DataFileInfo[] files, System.Text.StringBuilder sb)
		{
			foreach(DataFileInfo f in files)
			{
				sb.AppendLine(f.FileName.Replace(@"\", @"\\"));
			}
		}
		public string GetScriptFileList()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			
			sb.AppendLine("Plugin Files:");
			AppendFileList(GetPluginList(), sb);
			sb.AppendLine();
			sb.AppendLine("Data Files:");
			AppendFileList(GetDataList(), sb);
			
			return sb.ToString();
		}

		public string GetInfo() {
			string n=Environment.NewLine;
			string report=FileName+n+n+"[basic info]"+n+"Name: "+ModName+n;
			report+=n+"Author: "+Author+n+"version: "+Version+
				n+"Contact: "+Email+n+"Website: "+Website+n+n+Description+n;
			report+=n+"Date this omod was compiled: "+CreationTime.ToString();
			report+=n+"Contains readme: ";
			if(DoesReadmeExist()) report+="yes"; else report+="no";
			report+=n+"Contains script: ";
			if(DoesScriptExist()) report+="yes"; else report+="no";
			report+=n+n+"[omod file information]"+n;
			FileInfo fi;
			fi=new FileInfo(FullFilePath);
			if(fi.Length<8192) { //8kb
				report+="File size: "+fi.Length.ToString()+" bytes"+n;
			} else if(fi.Length<8388608) { //8mb
				report+="File size: "+(fi.Length/1024).ToString()+" kilobytes"+n;
			} else {
				report+="File size: "+(fi.Length/1048576).ToString()+" megabytes"+n;
			}
			report+="Internal omod file version: "+FileVersion+n;
			report+="CRC: "+CRC.ToString("x").ToUpper().PadLeft(8, '0')+n;
			report+="Created or installed: "+fi.CreationTime.ToShortDateString()+" "+fi.CreationTime.ToShortTimeString()+n;
			report+="Last modified: "+fi.LastWriteTime.ToShortDateString()+" "+fi.LastWriteTime.ToShortTimeString()+n;
			if(AllPlugins!=null&&AllPlugins.Length>0) {
				report+=n+"[Complete plugin list]"+n;
				foreach(string s in AllPlugins) report+=s+n;
			}
			if(AllDataFiles!=null&&AllDataFiles.Length>0) {
				report+=n+"[Complete data file list]"+n;
				foreach(DataFileInfo df in AllDataFiles)
					report+=df.FileName.PadRight(80)+" ("+df.CRC.ToString("x").ToUpper().PadLeft(8, '0')+")"+n;
			}
			if(Conflict==ConflictLevel.Active) {
				if(Plugins!=null&&Plugins.Length>0) {
					report+=n+"[Currently installed plugin list]"+n;
					foreach(string s in Plugins) report+=s+n;
				}
				if(DataFiles!=null&&DataFiles.Length>0) {
					report+=n+"[Currently installed data files]"+n;
					foreach(DataFileInfo dfi in DataFiles) report+=dfi.FileName+n;
				}
				if(BSAs!=null&&BSAs.Length>0) {
					report+=n+"[Registered BSAs]"+n;
					foreach(string s in BSAs) report+=s+n;
				}
				if(INIEdits!=null&&INIEdits.Count>0) {
					report+=n+"["+Program.currentGame.Name + " edits]"+n;
					foreach(INIEditInfo iei in INIEdits) report+=iei.Section+" "+iei.Name+": "+iei.NewValue+" (Changed from "+iei.OldValue+")"+n;
				}
				if(SDPEdits!=null&&SDPEdits.Count>0) {
					report+=n+"[Shader package edits]"+n;
					foreach(SDPEditInfo sei in SDPEdits) report+="Shader '"+sei.Shader+"' in package "+sei.Package+n;
				}
			}
			return report;
		}

		public bool DoesReadmeExist() {
            bool bRet = false;

            if (bHasReadme) return true;

            /*
            if (LowerFileName.EndsWith(".omod2") || LowerFileName.EndsWith(".omod2.ghost"))
                bOmod2 = true;
            else if (!LowerFileName.EndsWith(".omod") || LowerFileName.EndsWith(".omod.ghost"))
                bFomod = true;

            if (bFomod)
            {

                // no readme there
//                SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(FileName);
//                ze = zextract.ArchiveFileNames.Contains("");
            }
            else if (bOmod2)
            {
                if (ModFile.GetEntry("readme.txt") != null)
                    bRet = true;
            }
            else
            {
                if (ModFile.GetEntry("readme") != null)
                    bRet = true;
            }
            */

            if (FileName.ToLower().EndsWith(".omod") || (CompType == CompressionType.Zip && ModFile != null))
            {
                if (ModFile.GetEntry("readme") != null)
                    bRet = true;
                else if (ModFile.GetEntry("readme.txt") != null)
                    bRet = true;
            }
            else
            {
                SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Path.Combine(Settings.omodDir, FileName));
                foreach (string file in zextract.ArchiveFileNames)
                {
                    if (file.ToLower().Contains("readme.txt"))
                    {
                        bRet = true;
                        break;
                    }
                }
                zextract.Dispose();
            }

            bHasReadme = bRet;

            return bRet;
		}

		public bool DoesScriptExist() {
            bool bRet=false;

            if (bHasScript) return true;

            /*
            if (LowerFileName.EndsWith(".omod2") || LowerFileName.EndsWith(".omod2.ghost"))
                bOmod2 = true;
            else if (!LowerFileName.EndsWith(".omod") || LowerFileName.EndsWith(".omod.ghost"))
                bFomod = true;

            if (bFomod)
            {
                SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Settings.omodDir+FileName);
                foreach (string file in zextract.ArchiveFileNames)
                {
                    if (file.ToLower().Contains("fomod\\script.cs") || file.ToLower().Contains("fomod\\moduleconfig.xml"))
                    {
                        bHasScript = bRet = true;
                        break;
                    }
                }
                zextract.Dispose();

            }
            else if (bOmod2)
            {
                if (ModFile.GetEntry("script.txt") != null)
                    bRet = true;
            }
            else
            {
                if (ModFile.GetEntry("script") != null)
                    bRet = true;
            }
            */

            if (FileName.ToLower().EndsWith(".omod") || (CompType == CompressionType.Zip && ModFile != null))
            {
                if (ModFile.GetEntry("script") != null)
                    bRet = true;
                else if (ModFile.GetEntry("script.txt") != null)
                    bRet = true;
            }
            else
            {
                SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Path.Combine(Settings.omodDir, FileName));
                foreach (string file in zextract.ArchiveFileNames)
                {
                    if (file.ToLower().Contains("script.txt") || file.ToLower().Contains("fomod\\script.cs") || file.ToLower().Contains("fomod\\moduleconfig.xml"))
                    {
                        bRet = true;
                        break;
                    }
                }
                zextract.Dispose();
            }

            bHasScript = bRet;
            return bRet;
		}

		private string ParseCompressedStream(string fileList, string compressedStream) {
			string path;
			Stream FileList=ExtractWholeFile(fileList);
			if(FileList==null) return null;
			Stream CompressedStream=ExtractWholeFile(compressedStream);
			path=CompressionHandler.DecompressFiles(FileList, CompressedStream, CompType);
			FileList.Close();
			CompressedStream.Close();
			return path;
		}

		public string GetConfig() {
			string s="";
            Stream st = null;

            if (FileName.ToLower().EndsWith(".omod") || (CompType == CompressionType.Zip && ModFile != null))
            {
                st = ExtractWholeFile("config", ref s);
                if (st == null)
                {
                    st = ExtractWholeFile("config.txt", ref s);
                }
            }
            else
            {
                SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Path.Combine(Settings.omodDir, FileName));
                foreach (string file in zextract.ArchiveFileNames)
                {
                    if (file.ToLower().Contains("config.ini") || file.ToLower().Contains("config.txt"))
                    {
                        st = new MemoryStream();
                        zextract.ExtractFile(file, st);

                        // make sure that the current dir did not change
                        Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));

                        string filename = "";
                        BinaryWriter sw = new BinaryWriter(Program.CreateTempFile(out filename));
                        byte[] config = new byte[st.Length];
                        st.Read(config,0,(int)st.Length);
                        st.Close();
                        sw.Write(config, 0, (int)config.Length);
                        sw.Close();
                        zextract.Dispose();
                        s = filename;
                        break;
                    }
                }
                zextract.Dispose();
                if (st == null)
                {
                    if (File.Exists(Path.Combine("cache", this.FileName)))
                    {
                        omod o = new omod(Path.Combine("cache", this.FileName), false);
                        st = o.ExtractWholeFile("config.txt", ref s);
                    }
                }
            }
            if (st != null) st.Close();

			return s;
		}

		private Stream ExtractWholeFile(string s) {
            Stream st = null;
			string s2=null;
            try
            {
                st = ExtractWholeFile(s, ref s2);
            }
            catch
            {
                // there might not be such a file...This is acceptable
            }
            return st;
		}
		private Stream ExtractWholeFile(string s, ref string path) {
            Stream st = null;

            if (ModFile != null)
            {
                try
                {
                    ZipEntry ze = ModFile.GetEntry(s);
                    if (ze == null) return null;
                    st = ExtractWholeFile(ze, ref path);
                }
                catch
                {
                    // there might not be such a file...This is acceptable
                }
            }

            return st;
		}
		private Stream ExtractWholeFile(ZipEntry ze, ref string path) {
			Stream file=ModFile.GetInputStream(ze);
			Stream TempStream;
            //if(path!=null||ze.Size>Settings.MaxMemoryStreamSize) {
				TempStream=Program.CreateTempFile(out path);
            //} else {
            //    TempStream=new MemoryStream((int)ze.Size);
            //}
			byte[] buffer=new byte[4096];
			int i;
			while((i=file.Read(buffer, 0, 4096))>0) {
				TempStream.Write(buffer, 0, i);
			}
			TempStream.Position=0;
			return TempStream;
		}

        private void ReplaceFileInOmod(string file, string contents)
        {
            ReplaceFileInOmod(file, contents, null);
        }
        private void ReplaceFileInOmod(string file, string contents, string newfile)
        {
            List<string> filelist = new List<string>();
            filelist.Add(file);
            ReplaceFilesInOmod(filelist.ToArray(), contents, newfile);
        }
        private void ReplaceFilesInOmod(string[] filelist, string contents, string newfile)
        {
            Close();
            //if (ModFile == null)
            {
                string currentDirectory = Directory.GetCurrentDirectory();

                SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Path.Combine(Settings.omodDir, FileName));
                //                comp7zrWrapper zextract = new comp7zrWrapper(Path.Combine(Settings.omodDir, FileName));
                string dir = Program.CreateTempDirectory();
                zextract.ExtractFiles(dir, new List<string>(zextract.ArchiveFileNames).ToArray()); //  (new SevenZip.ExtractFileCallback(extractFileCallback));
                zextract.Dispose();
                // make sure that the current dir did not change
                Directory.SetCurrentDirectory(currentDirectory);

                if (contents == null || contents == "")
                {
                    if (filelist != null && filelist.Length > 0)
                    {
                        foreach (string file in filelist)
                            File.Delete(Path.Combine(dir, file));
                    }

                    if (newfile != null && newfile != "")
                    {
                        foreach (string file in filelist)
                            File.Copy(newfile, Path.Combine(dir, file));
                    }
                }
                else
                {
                    if (filelist.Length > 0)
                    {
                        BinaryWriter bw = new BinaryWriter(File.Open(Path.Combine(dir, filelist[0]), FileMode.Create));
                        bw.Write(contents.ToCharArray());
                        bw.Close();
                    }
                }
                string dir2 = Program.CreateTempDirectory();
                if (ModFile == null)
                {
                    SevenZip.SevenZipCompressor zcomp = new SevenZip.SevenZipCompressor();

                    bool bDone = false;

                    while (!bDone)
                    try
                    {
                        zcomp.CompressDirectory(dir, Path.Combine(dir2, FileName), "", "*", true);
                        bDone = true;
                    }
                    catch (Exception ex)
                    {
                        Program.logger.WriteToLog("Compression of " + FileName + " failed:" + ex.Message, Logger.LogLevel.Low);
                        switch (zcomp.CompressionLevel)
                        {
                            case SevenZip.CompressionLevel.Ultra:
                                zcomp.CompressionLevel = SevenZip.CompressionLevel.High;
                                break;
                            case SevenZip.CompressionLevel.High:
                                zcomp.CompressionLevel = SevenZip.CompressionLevel.Normal;
                                break;
                            case SevenZip.CompressionLevel.Normal:
                                zcomp.CompressionLevel = SevenZip.CompressionLevel.Low;
                                break;
                            case SevenZip.CompressionLevel.Low:
                                zcomp.CompressionLevel = SevenZip.CompressionLevel.Fast;
                                break;
                            default:
                                // cannot compress it!!!
                                throw ex;
                        }
                    }

                    //                comp7zrWrapper zcomp = new comp7zrWrapper(dir2 + FileName);
                    //                zcomp.CompressDirectory(dir);
                }
                else
                {
                    ModFile.Close();
                    FastZip fz = new FastZip();
                    fz.CreateZip(Path.Combine(dir2, FileName), dir, false, null);
                }
                File.Delete(Path.Combine(Settings.omodDir, FileName));
                File.Move(dir2 + FileName, Path.Combine(Settings.omodDir, FileName));
            }
            //else
            //{
            //    string dir2 = Program.CreateTempDirectory();
            //    // want fz to leave scope
            //    {
            //        FastZip fz = new FastZip();
            //        string dir = Program.CreateTempDirectory();
            //        fz.ExtractZip(Settings.omodDir + FileName, dir, null);
            //        if (contents == null || contents == "")
            //        {
            //            if (filelist != null && filelist.Length > 0)
            //            {
            //                foreach (string file in filelist)
            //                    File.Delete(dir + file);
            //            }

            //            if (newfile != null && newfile != "")
            //            {
            //                foreach (string file in filelist)
            //                    File.Copy(newfile, dir + file);
            //            }
            //        }
            //        else
            //        {
            //            if (filelist.Length > 0)
            //            {
            //                BinaryWriter bw = new BinaryWriter(File.Open(dir + filelist[0], FileMode.Create));
            //                bw.Write(contents);
            //                bw.Close();
            //            }
            //        }
            //        fz.CreateZip(dir2 + FileName, dir, false, null);
            //    }
            //    File.Delete(Settings.omodDir + FileName);
            //    File.Move(dir2 + FileName, Settings.omodDir + FileName);
            //}
        }
        public void AddPicture(string filename)
        {
            ReplaceFileInOmod("image.jpg", null, filename);
        }
        public void AddNexusInfo()
        {
            string tesid = Program.GetModID(this.FileName);
            if (tesid.Length == 0)
            {
                tesid = Path.GetFileName(Website);
            }
            string modName = "";
            string modVersion = "";
            string modDescription = "";
            string modAuthor = "";
            string modWebsite = "";
            string imagefile = "";
            Program.GetNexusModInfo(tesid, ref modName, ref modVersion, ref modDescription, ref modAuthor, ref modWebsite, ref imagefile, true);
            if (this.Description==null || this.Description.Length == 0)
                this.Description = modDescription;
            if (this.Author==null || this.Author.Length == 0)
                this.Author = modAuthor;
            if (this.Website==null || this.Website.Length == 0)
                this.Website = modWebsite;
            List<string> filelist = new List<string>();
            string newdoc=  "<?xml version=\"1.0\" encoding=\"UTF-16\"?>"+
                            "<fomod>"+
                                "<Name>"+this.ModName+"</Name>"+
                                "<Author>"+this.Author+"</Author>"+
                                "<Version MachineVersion=\"1.0\">"+this.Version+"</Version>"+
                                "<Website>"+this.Website+"</Website>"+
                                "<Description>"+this.Description+"</Description>"+
                                //"<Groups>"+
                                //    "<element>bugfixes, scripts, meshes, textures</element>"+
                                //"</Groups>"+
                            "</fomod>";
            try
            {
                string cacheText = Path.Combine(Path.Combine(Settings.omodDir, "info"), this.LowerFileName + ".xml").Replace(".ghost", "");

                File.WriteAllText(cacheText, newdoc);
            }
            catch { };
            if (imagefile.Length > 0)
            {
                try
                {
                    string cacheImage = Path.Combine(Path.Combine(Settings.omodDir, "info"), this.LowerFileName + ".jpg").Replace(".ghost", "");

                    // copy file to info directory
                    if (File.Exists(cacheImage))
                        File.Delete(cacheImage);
                    File.Copy(imagefile, cacheImage);
                }
                catch { };
                try
                {
                    //pd.image = System.Drawing.Image.FromFile(imagefile);
                    ReplaceFileInOmod("image.jpg", null, imagefile);
                }
                catch
                {
                }
            }
            if (this.Description.Length > 0)
            {
                bool bReplace = true;
                if (this.bHasReadme && DialogResult.No == MessageBox.Show("File already has readme. Replace with '" + this.Description + "' ?", "Replace readme?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    bReplace = false;

                if (bReplace)
                {
                    ReplaceReadme(this.Description);
                    this.bHasReadme = true;
                }
            }
        }

		public void ReplaceReadme(string readme) {
            if (bFomod)
                ReplaceFileInOmod("readme.txt", readme);
            else if (bOmod2)
                ReplaceFileInOmod("readme.txt", readme);
            else
                ReplaceFileInOmod("readme", readme);
		}

		public void ReplaceScript(string script) {
            if (bFomod)
            {
                if ((OblivionModManager.ScriptType)script[0]==OblivionModManager.ScriptType.xml)
                    ReplaceFileInOmod("fomod\\ModuleConfig.xml", script.Length > 1 ? script : null);
                else
                    ReplaceFileInOmod("fomod\\script.cs", script.Length > 1 ? script : null);
            }
            else if (bOmod2)
                ReplaceFileInOmod("script.txt", script.Length > 1 ? script : null);
            else
                ReplaceFileInOmod("script", script.Length > 1 ? script : null);
        }

		public void Hide() {
            //if(pd!=null && pd.image!=null) {
            //    pd.image.Dispose();
            //    pd.image=null;
            //}
			hidden=true;
            if (!this.FileName.EndsWith(".ghost"))
            {
                try
                {
                    this.Close();
                    File.Delete(Path.Combine(Settings.omodDir, this.FileName + ".ghost"));
                    File.Move(Path.Combine(Settings.omodDir, this.FileName), Path.Combine(Settings.omodDir, this.FileName + ".ghost"));
                    this.FileName += ".ghost";
                    this.LowerFileName += ".ghost";
                }
                catch (Exception ex)
                {
                    Program.logger.WriteToLog("Could not Hide '" + LowerFileName + ": " + ex.Message, Logger.LogLevel.High);
                }
            }
		}
		public void Show() {
			hidden=false;
            if (this.FileName.EndsWith(".ghost"))
            {
                this.FileName.Replace(".ghost", "");
                this.LowerFileName.Replace(".ghost","");
            }
        }

        public void markAsActive(EspInfo ei)
        {
            if (Plugins != null)
            {
                List<string> esps = new List<string>(Plugins);
                if (!esps.Contains(ei.LowerFileName))
                {
                    esps.Add(ei.LowerFileName);
                    Plugins = esps.ToArray();
                }
                Conflict = ConflictLevel.Active;
                ei.BelongsTo = ModName;

                List<DataFileInfo> files = new List<DataFileInfo>(DataFiles);
                // check files
                for (int i = 0; i < AllDataFiles.Length; i++)
                {
                    if (File.Exists(Path.Combine(Program.currentGame.DataFolderPath,AllDataFiles[i].FileName)) && !files.Contains(AllDataFiles[i]))
                    {
                        files.Add(AllDataFiles[i]);
                    }
                }
            }
        }

		public void UnlinkPlugin(EspInfo ei) {
			if(Plugins!=null) {
				List<string> esps=new List<string>(Plugins);
				Program.strArrayRemove(esps, ei.LowerFileName);
				Plugins=esps.ToArray();
			}
		}

		public void UnlinkDataFile(DataFileInfo dfi) {
			if(DataFiles!=null) {
				List<DataFileInfo> files=new List<DataFileInfo>(DataFiles);
				Program.strArrayRemove(files, dfi.LowerFileName);
				DataFiles=files.ToArray();
			}
		}

	}
}