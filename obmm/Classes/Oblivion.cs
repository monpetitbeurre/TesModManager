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
using System.Collections.Generic;
using Formatter=System.Runtime.Serialization.Formatters.Binary.BinaryFormatter;
using MessageBox=System.Windows.Forms.MessageBox;

namespace OblivionModManager {
    public static class OblivionESP {
        private static readonly string espfile=Path.Combine(Program.ESPDir,"plugins.txt");
        public static readonly string loadorder = Path.Combine(Program.ESPDir,"loadorder.txt");
        public static readonly string dlclist = Path.Combine(Program.ESPDir,"DLCList.txt");
        public static readonly string steammodlist = Path.Combine(Program.ESPDir,"SteamModList.txt");
        private static readonly string bashespfile = Path.Combine(Program.BaseDir, @"loadorder.txt");

        public class EspPluginSorter : IComparer<EspInfo>
        {
            public int Compare(EspInfo ea, EspInfo eb)
            {
                int icomp = 0;

                string a = ea.FileName.ToLower();
                string b = eb.FileName.ToLower();

                if (b == a)
                    icomp = 0;
                else if (Path.GetExtension(a) != Path.GetExtension(b) && Settings.bPreventMovingESPBeforeESM)
                    icomp = (Path.GetExtension(a) == ".esm") ? (-1) : 1;
                else if (Program.loadOrderList.Count > 0)
                {
                    foreach (string strline in Program.loadOrderList)
                    {
                        if (strline == a)
                        {
                            icomp = -1;
                            break;
                        }
                        else if (strline == b)
                        {
                            icomp = 1;
                            break;
                        }
                    }
                }
                else
                {
                    FileInfo fia = new FileInfo(Path.Combine(Program.currentGame.DataFolderPath,a));
                    FileInfo fib = new FileInfo(Path.Combine(Program.currentGame.DataFolderPath,b));
                    icomp = DateTime.Compare(fia.LastWriteTime, fib.LastWriteTime);
                }
                return icomp;
            }
        }

        public class PluginSorter : IComparer<string>
        {
            public int Compare(string a, string b) {
                int icomp = 0;

                a = a.ToLower();
                b = b.ToLower();

                if (b == a)
                    icomp = 0;
                else if (Path.GetExtension(a) != Path.GetExtension(b) && Settings.bPreventMovingESPBeforeESM)
                    icomp = (Path.GetExtension(a) == ".esm") ? (-1) : 1;
                else if (Program.loadOrderList.Count > 0)
                {
                    foreach (string strline in Program.loadOrderList)
                    {
                        string pin = strline.ToLower();
                        if (pin == a)
                        {
                            icomp = -1;
                            break;
                        }
                        else if (pin == b)
                        {
                            icomp = 1;
                            break;
                        }
                    }
                }
                else
                {
                    FileInfo fia = new FileInfo(Path.Combine(Program.currentGame.DataFolderPath,a));
                    FileInfo fib = new FileInfo(Path.Combine(Program.currentGame.DataFolderPath,b));
                    icomp= DateTime.Compare(fia.LastWriteTime, fib.LastWriteTime);
                }
                return icomp;
            }
        }

        public static bool CreateList() {
            if(!Directory.Exists(Program.ESPDir)) return false;
            if(File.Exists(espfile)) return true;
            try {
                FileStream fs=File.Create(espfile);
                fs.Close();
            } catch { }
            return File.Exists(espfile);
        }

        public static bool[] ArePluginsActive(string[] fileNames) {
            string[] FileNames=new string[fileNames.Length];
            bool[] result=new bool[fileNames.Length];
            List<string> ActiveEsps = new List<string>();
            if (Program.currentGame.Name=="Morrowind")
            {
                string valuename = "GameFile";
                int curini = 0;
                string esp="";
                while (esp != null)
                {
                    esp = OblivionModManager.INI.GetINIValue("[Game Files]", valuename + curini++);
                    if (esp != null && esp.Length > 0)
                        ActiveEsps.Add(esp);
                }
            }
            else
            {
                ActiveEsps = new List<string>(File.ReadAllLines(espfile, System.Text.Encoding.Default));
            }
            for (int i = 0; i < result.Length; i++)
            {
                FileNames[i] = fileNames[i].ToLower();
                result[i] = false;
            }
            int found;
            foreach (string s in ActiveEsps)
            {
                if (s == "" || s.StartsWith("#")) continue;
                string espfile = s.ToLower();
                if (Program.currentGame.NickName == "skyrimse" && s.StartsWith("*"))
                    espfile = espfile.Substring(1); 
                if ((found = Array.IndexOf(FileNames, espfile)) != -1) result[found] = true;
            }
            
            return result;
        }

        public static void SetActivePlugins() {
            Program.logger.WriteToLog("SetActivePlugins", Logger.LogLevel.High);
            File.Delete(espfile + ".bak");
            File.Move(espfile, espfile + ".bak");
            StreamWriter sw = new StreamWriter(espfile, false, System.Text.Encoding.Default);
            sw.WriteLine("# This file is used to tell "+Program.currentGame.Name + " which data files to load.");
            sw.WriteLine("# Use the game launcher or obmm to choose which files you want.");
            sw.WriteLine("# Please do not modify this file by hand.");
            sw.WriteLine();
            sw.WriteLine("# last modified by TesModManager: "+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString());
            sw.WriteLine();
            List<EspInfo> espBackup = new List<EspInfo>();
            foreach(EspInfo transfer in Program.Data.Esps){
                espBackup.Add(transfer);                
            }

            Program.Data.Esps.Clear();


            foreach (System.Windows.Forms.ListViewItem Esp in Program.lvEspList.Items)
            {
                foreach (EspInfo EspI in espBackup)
                {
                    EspInfo esp = (EspInfo)Esp.Tag;
                    if(esp.FileName.Equals(EspI.FileName))
                        Program.Data.Esps.Add(EspI);
                }
            }




            foreach(EspInfo ei in Program.Data.Esps) {
                if(ei.Active || ei.LowerFileName=="skyrim.esm")
                {
                    if (Program.currentGame.NickName == "skyrimse")
                        sw.Write("*");
                    sw.WriteLine(ei.FileName);
                }
            }
            sw.Close();

            try {
                if (!Settings.NeverTouchLoadOrder)
                {
                    Program.logger.WriteToLog("Saving load order...", Logger.LogLevel.Low);
                    sw = new StreamWriter(loadorder, false, (Settings.bLoadOrderAsUTF8 ? System.Text.Encoding.UTF8 : System.Text.Encoding.Default));
                    foreach (EspInfo ei in Program.Data.Esps)
                    {
                        sw.WriteLine(ei.FileName);
                    }
                    sw.Close();
                    if (Program.currentGame.Name=="Morrowind")
                    {
                        // the load order is under [gamefiles] in morrowind.ini each entry is gamefileX=<espname> where X starts at 0
                        // GetINIValue. WriteINIValue
                        // find the current max
                        string valuename = "GameFile";
                        int curini=0;
                        string esp = "";
                        while (esp != null)
                        {
                            esp = OblivionModManager.INI.GetINIValue("[Game Files]", valuename + curini);
                            if (esp != null && esp.Length > 0)
                                curini++;
                        }
                        int plugincount = curini;
                        if (plugincount > Program.Data.Esps.Count)
                        {
                            for (int i=Program.Data.Esps.Count;i<plugincount;i++)
                            {
                                OblivionModManager.INI.WriteINIValue("GameFiles", valuename + curini++, "");
                            }
                        }
                        curini = 0;
                        foreach (EspInfo ei in Program.Data.Esps)
                        {
                            if (ei.Active)
                                OblivionModManager.INI.WriteINIValue("[Game Files]", valuename + curini++, ei.FileName);
                        }
                    }

                }
            }
            catch { }
            finally { if (sw != null) sw.Close(); }

            BinaryWriter bw=null;
            try {
                bw=new BinaryWriter(File.Create(bashespfile));
                foreach(EspInfo ei in Program.Data.Esps) {
                    if(ei.Active) {
                        bw.Write(ei.FileName);
                        bw.Write(ei.DateModified.ToBinary());
                    }
                }
            } catch { } finally { if(bw!=null) bw.Close(); }
            Program.logger.WriteToLog("SetActivePlugins Done", Logger.LogLevel.High);
        }

        public static string[] GetActivePlugins() {
            if(Program.Data!=null) SetActivePlugins();
            List<string> ActiveEsps=new List<string>(File.ReadAllLines(espfile, System.Text.Encoding.Default));
            for(int i=0;i<ActiveEsps.Count;i++) {
                try {
                    if (ActiveEsps[i].Length==0||
                        ActiveEsps[i][0]=='#'||
                        (Program.currentGame.NickName == "skyrimse" && ActiveEsps[i][0] != '*') ||
                        !File.Exists(Path.Combine(Program.currentGame.DataFolderPath,ActiveEsps[i])))
                            ActiveEsps.RemoveAt(i--);
                } catch { ActiveEsps.RemoveAt(i--); }
            }
            ActiveEsps.Sort(new PluginSorter());
            return ActiveEsps.ToArray();
        }
    }

    public static class INI {
        private static string inibase = Path.Combine(Program.INIDir, Program.currentGame.IniBaseName);
        private static string ini = inibase+".ini";
        
        public static bool CreateINI() {
            if (Program.currentGame.Name=="Morrowind") return true;
            if(!Directory.Exists(Program.INIDir)) return false;
            if(File.Exists(ini)) return true;
            try { File.Copy(inibase + "_default.ini", ini); }
            catch { }
            return File.Exists(ini);
        }

        public static string GetINIValue(string section, string name) {
            string[] ss=GetINISection(section);
            if(ss==null) throw new obmmException("ini section "+section+" does not exist");
            name=name.ToLower();
            foreach(string s in ss) {
                if(s.Trim().ToLower().StartsWith(name+"=")) {
                    string res=s.Substring(s.IndexOf('=')+1).Trim();
                    int i=res.IndexOf(';');
                    if(i!=-1) res=res.Substring(0, i-1);
                    return res;
                }
            }
            return null;
        }

        public static void WriteINIValue(string section,string name,string value) {
            string[] ssa = GetINISection(section);
            if(ssa==null) throw new obmmException("ini section "+section+" does not exist");
            List<string> ss = new List<string>(ssa);
            bool matched =false;
            string lname=name.ToLower();
            for(int i=0;i<ss.Count;i++) {
                string s=ss[i];
                if(s.Trim().ToLower().StartsWith(lname+"=")) {
                    if(value==null) {
                        ss.RemoveAt(i--);
                    } else {
                        ss[i]=name+"="+value;
                     
                    }
                    matched=true;
                    break;
                }
            }
            if(!matched) ss.Add(name+"="+value);
            ReplaceINISection(section,ss.ToArray());
        }

        private static string[] GetINISection(string section) {
            List<string> contents=new List<string>();
            bool InSection=false;
            section=section.ToLower();
            StreamReader sr=new StreamReader(File.OpenRead(ini), System.Text.Encoding.Default);
            try {
                while(sr.Peek()!=-1) {
                    string s=sr.ReadLine();
                    if(InSection) {
                        if(s.Trim().StartsWith("[")&&s.Trim().EndsWith("]")) break;
                        contents.Add(s);
                    } else {
                        if(s.Trim().ToLower()==section) InSection=true;
                    }
                }
            } finally {
                if(sr!=null) sr.Close();
            }
            if(!InSection) return null;
            return contents.ToArray();
        }

        private static void ReplaceINISection(string section, string[] ReplaceWith) {
            List<string> contents=new List<string>();
            StreamReader sr=new StreamReader(File.OpenRead(ini), System.Text.Encoding.Default);
            try {
                section=section.ToLower();
                bool InSection=false;
                while(sr.Peek()!=-1) {
                    string s=sr.ReadLine();
                    if(!InSection) {
                        contents.Add(s);
                        if(s.Trim().ToLower()==section) {
                            InSection=true;
                            contents.AddRange(ReplaceWith);
                        }
                    } else {
                        if(s.Trim().StartsWith("[")&&s.Trim().EndsWith("]")) {
                            contents.Add(s);
                            InSection=false;
                        }
                    }
                }
            } finally {
                if(sr!=null) sr.Close();
            }
            StreamWriter sw=new StreamWriter(File.Create(ini), System.Text.Encoding.Default);
            try {
                foreach(string s in contents) {
                    sw.WriteLine(s);
                }
            } finally {
                if(sw!=null) sw.Close();
            }
        }

        public static bool IsBSARegistered(string FileName) {
            FileName=FileName.ToLower();
            string[] BSAs=GetINISection("[Archives]");
            for(int i=0;i<BSAs.Length;i++) {
                string s=BSAs[i].Trim().ToLower();
                int index=s.IndexOf(';');
                if(index!=-1) s=s.Remove(index, s.Length-index);
                index=s.IndexOf('=');
                if(index!=-1) s=s.Remove(0, index+1);
                if(s.Trim()==FileName) return true;
            }
            return false;
        }
    }

    public static class OblivionBSA {
        #region Structs
        private class BSAEditData {
            public readonly string FolderName;
            public readonly List<BSAEditFileInfo> Files=new List<BSAEditFileInfo>();

            public BSAEditData(string folder) {
                FolderName=folder;
            }
        }

        private class BSAEditFileInfo {
            public readonly ulong Hash;
            public readonly long HashOffset;
            public string FileName;
            public bool Exists=false;

            public BSAEditFileInfo(ulong hash, long offset) {
                Hash=hash;
                HashOffset=offset;
            }
        }

        [Serializable]
        private class BSAEdit {
            public readonly string FileName;
            public readonly long FileSize;
            public readonly List<ulong> OldData=new List<ulong>();
            public readonly List<long> Offsets=new List<long>();

            public BSAEdit(string file, long fileSize) { FileName=file; FileSize=fileSize; }
        }
        #endregion

        private static readonly string InvalidationFile;
        private static readonly string RedirectionPath= Path.Combine(Program.BaseDir,"BSARedirection.bsa");
        public static readonly List<string> Archives=new List<string>();
        private static bool UpdatedList=false;

        private static int filesModified;
        private static int entriesModified;
        private static int hashCollisions;
        private static bool NoUpdates=false;
        public static int FilesModified { get { return filesModified; } }
        public static int EntriesModified { get { return entriesModified; } }
        public static int HashCollisions { get { return hashCollisions; } }

        static OblivionBSA() {
            try { 
                InvalidationFile=INI.GetINIValue("[Archive]", "SInvalidationFile");
                if(InvalidationFile==null) InvalidationFile="ArchiveInvalidation.txt";
            } catch { 
                InvalidationFile="ArchiveInvalidation.txt";
            }
        }

        public static void ReadArchives() {
            string s=null;
            try {
                if (Program.currentGame.NickName.Contains("skyrim"))
                    s = INI.GetINIValue("[Archive]", "sResourceArchiveList");
                else if (Program.currentGame.Name == "Morrowind")
                {
                    s = "";
                    string valuename = "Archive ";
                    int curarchive = 0;
                    string archivename = "";
                    do
                    {
                        archivename = INI.GetINIValue("[Archives]", valuename + curarchive++);
                        s += archivename + ",";
                    }
                    while (archivename != null && archivename.Length > 0);
                }
                else
                    s = INI.GetINIValue("[Archive]", "sArchiveList");
            }
            catch (obmmException)
            {
//                MessageBox.Show("Could not load BSA list", "Warning");
//                NoUpdates=true;
//                return;
            }
            if(s==null) {
                if (Program.currentGame.NickName == "skyrimse")
                {
                    Archives.AddRange(new string[] { "Skyrim - Voices_en0.bsa", "Skyrim - Sounds.bsa", "Skyrim - Shaders.bsa", "Skyrim - Misc.bsa",
                        "Skyrim - Textures0.bsa", "Skyrim - Textures1.bsa", "Skyrim - Textures2.bsa", "Skyrim - Textures3.bsa", "Skyrim - Textures4.bsa",
                        "Skyrim - Textures5.bsa", "Skyrim - Textures6.bsa", "Skyrim - Textures7.bsa", "Skyrim - Textures8.bsa",
                        "Skyrim - Meshes0.bsa", "Skyrim - Meshes1.bsa", "Skyrim - Interface.bsa", "Skyrim - Animations.bsa" });
                }
                else if (Program.currentGame.NickName == "skyrim")
                {
                    Archives.AddRange(new string[] { "Skyrim - VoicesExtra.bsa", "Skyrim - Voices.bsa",
                        "Skyrim - Textures.bsa", "Skyrim - Sounds.bsa", "Skyrim - Shaders.bsa", "Skyrim - Misc.bsa",
                        "Skyrim - Meshes.bsa", "Skyrim - Interface.bsa", "Skyrim - Animations.bsa" });
                }
                else if (Program.currentGame.NickName == "morrowind")
                {
                    Archives.AddRange(new string[] { "Morrowind.bsa" });
                }
                else
                {
                    Archives.AddRange(new string[] { "Oblivion - Meshes.bsa", "Oblivion - Textures - Compressed.bsa",
                    "Oblivion - Sounds.bsa", "Oblivion - Voices1.bsa", "Oblivion - Voices2.bsa",
                    "Oblivion - Misc.bsa" });
                }
            }
            else
            {
                Archives.AddRange(s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                for(int i=0;i<Archives.Count;i++) Archives[i]=Archives[i].Trim();
            }
        }

        public static void CommitArchives() {
            if (!NoUpdates && UpdatedList)
            {
                if (Program.currentGame.NickName.Contains("skyrim"))
                    INI.WriteINIValue("[Archive]", "sResourceArchiveList", string.Join(", ", Archives.ToArray()));
                else if (Program.currentGame.Name=="Morrowind")
                {
                    // under [Archives]. Each archive is named "Archive x" where x starts at 0
                    // count them
                    string valuename="Archive ";
                    int curarchive=0;
                    string archivename="";
                    do
                    {
                        archivename = INI.GetINIValue("[Archives]", valuename + curarchive++);
                    }
                    while (archivename != null && archivename.Length > 0);
                    int count = curarchive;
                    curarchive = 0;
                    foreach (string newarchivename in Archives)
                    {
                        INI.WriteINIValue("[Archives]", "Archive "+curarchive++, newarchivename);
                    }
                    if (count > Archives.Count)
                    {
                        for (int i=curarchive;i<count;i++)
                        {
                            INI.WriteINIValue("[Archives]", "Archive " + i, "");
                        }
                    }
                }
                else
                    INI.WriteINIValue("[Archive]", "sArchiveList", string.Join(", ", Archives.ToArray()));

            }
        }

        public static bool RegisterBSA(string name) {
            if (NoUpdates) {
                //                MessageBox.Show("Failed to register BSA "+name+" because the [Archive] section could not be found in ini file", "Error");
                Program.logger.WriteToLog("Failed to register BSA " + name + " because the [Archive] section could not be found in ini file", Logger.LogLevel.Error);
                return false;
            }
            string lname = name.ToLower();
            foreach (string s in Archives) {
                if (s.ToLower() == lname) return true;
            }
            UpdatedList = true;
            if (Path.GetFileName(name) == "BSARedirection.bsa") Archives.Insert(0, name);
            else Archives.Add(name);
            return false;
        }

        public static void UnregisterBSA(string name) {
            if(NoUpdates) {
//                MessageBox.Show("Failed to unregister BSA "+name+" because the [Archive] section could not be found in ini file", "Error");
                Program.logger.WriteToLog("Failed to unregister BSA " + name + " because the [Archive] section could not be found in ini file", Logger.LogLevel.Error);
                return;
            }
            name=name.ToLower();
            for(int i=0;i<Archives.Count;i++) {
                if(Archives[i].ToLower()==name) {
                    Archives.RemoveAt(i);
                    UpdatedList=true;
                    return;
                }
            }
        }

        public static string[] GetBSAEntries(string path) {
            List<string> files=new List<string>();
            BinaryReader br=null;
            br = new BinaryReader(File.OpenRead(path), System.Text.Encoding.GetEncoding("ISO-8859-1"));
            if(Program.ReadCString(br)!="BSA") {
                br.Close();
                return new string[0];
            }
            br.ReadUInt32();
            int offset=br.ReadInt32();
            br.ReadUInt32();
            int FolderCount=br.ReadInt32();
            int TotalFileCount=br.ReadInt32();
            int TotalFolderLength=br.ReadInt32();
            int TotalFileNameLength=br.ReadInt32();
            br.ReadUInt32();
            long FileNameBlockStart=offset+TotalFolderLength+16*TotalFileCount+17*FolderCount;
            long folderpos=br.BaseStream.Position;
            for(int i=0;i<FolderCount;i++) {
                br.BaseStream.Position=folderpos+8;
                int filecount=br.ReadInt32();
                br.BaseStream.Position=br.ReadInt32()-TotalFileNameLength+1;
                string fname=Program.ReadCString(br).Replace('/', '\\');
                br.BaseStream.Position=FileNameBlockStart;
                for (int j = 0; j < filecount; j++)
                {
                    string filename = Program.ReadCString(br).Replace('/', '\\');
                    Program.logger.WriteToLog("BSAEntry: path=" + fname + " file=" + filename, Logger.LogLevel.High);
                    files.Add(Path.Combine(fname, filename));
                }
                FileNameBlockStart=br.BaseStream.Position;
                folderpos+=16;
            }
            br.Close();
            return files.ToArray();
        }

        public static string[] GetBSAEntries() {
            List<string> files=new List<string>();
            foreach(string path in Directory.GetFiles(Program.currentGame.DataFolderPath, "*.bsa")) {
                files.AddRange(GetBSAEntries(path));
            }
            files.Sort();
            return files.ToArray();
        }

        public static int RestoreBSA() {
            if(!File.Exists(Program.BSAEditFile)) return 0;
            List<BSAEdit> Edits;
            Formatter f=new Formatter();
            Stream s=File.OpenRead(Program.BSAEditFile);
            Edits=(List<BSAEdit>)f.Deserialize(s);
            s.Close();
            int count=0;
            try {
                foreach(BSAEdit be in Edits) {
                    if(!File.Exists(be.FileName)) {
//                        MessageBox.Show("Edited BSA '"+be.FileName+"' is missing, and will be ignored","Warning");
                        Program.logger.WriteToLog("Edited BSA '" + be.FileName + "' is missing, and will be ignored", Logger.LogLevel.Warning);
                        continue;
                    }
                    FileInfo fi=new FileInfo(be.FileName);
                    if(be.FileSize!=0&&fi.Length!=be.FileSize) {
 //                       MessageBox.Show("Edited BSA '"+be.FileName+"' has changed since the last time the BSA patcher was run, and will be ignored","Warning");
                        Program.logger.WriteToLog("Edited BSA '" + be.FileName + "' has changed since the last time the BSA patcher was run, and will be ignored", Logger.LogLevel.Warning);
                        continue;
                    }
                    count+=be.Offsets.Count;
                    DateTime origdate=fi.LastWriteTime;
                    BinaryWriter bw=new BinaryWriter(File.Open(be.FileName, FileMode.Open));
                    try {
                        for(int i=0;i<be.Offsets.Count;i++) {
                            bw.BaseStream.Position=be.Offsets[i];
                            bw.Write(be.OldData[i]);
                        }
                    } finally {
                        if(bw!=null) bw.Close();
                    }
                    fi.LastWriteTime=origdate;
                    
                }
                if (File.Exists(Program.BSAEditFile)) File.Delete(Program.BSAEditFile);
                if (File.Exists(Program.BSAEditFile+".ghost")) File.Delete(Program.BSAEditFile+".ghost");
            }
            catch (Exception ex)
            {
//                MessageBox.Show("An error occured while trying to undo BSA archive edits\n"+
//                    "If you have something else open which may be using the BSA's, (the game, the CS, another mod tool, etc.), please close it while using obmm\n"+
//                    "Error: "+ex.Message, "Error");
                Program.logger.WriteToLog("An error occured while trying to undo BSA archive edits\n" +
                    "If you have something else open which may be using the BSA's, (the game, the CS, another mod tool, etc.), please close it while using obmm\n" +
                    "Error: " + ex.Message, Logger.LogLevel.Error);
                return -1;
            }
            return count;
        }

        private static bool ShouldFolderBeProcessed(string folder) {
            int index=folder.IndexOf('\\');
            if(index==-1) return (Settings.InvalidationFlags&ArchiveInvalidationFlags.Base)>0;
            folder=folder.Substring(0, index);
            switch(folder) {
            case "textures": return (Settings.InvalidationFlags&ArchiveInvalidationFlags.Textures)>0;
            case "meshes": return (Settings.InvalidationFlags&ArchiveInvalidationFlags.Meshes)>0;
            case "sound": return (Settings.InvalidationFlags&ArchiveInvalidationFlags.Sounds)>0;
            case "music": return (Settings.InvalidationFlags&ArchiveInvalidationFlags.Music)>0;
            case "fonts": return (Settings.InvalidationFlags&ArchiveInvalidationFlags.Fonts)>0;
            case "menus": return (Settings.InvalidationFlags&ArchiveInvalidationFlags.Menus)>0;
            case "trees":
            case "distantlod": return (Settings.InvalidationFlags&ArchiveInvalidationFlags.TreesLOD)>0;
            default: return (Settings.InvalidationFlags&ArchiveInvalidationFlags.Misc)>0;
            }
        }

        private static uint GetFlags() {
            uint result=0;
            if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Meshes)>0) {
                result|=0x1;
            }
            if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Textures)>0) {
                result|=0x2;
            }
            if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Menus)>0) {
                result|=0xC;
            }
            if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Sounds)>0) {
                result|=0xC;
            }
            if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Music)>0) {
                result|=0x10;
            }
            if((Settings.InvalidationFlags&ArchiveInvalidationFlags.TreesLOD)>0) {
                result|=0x40;
            }
            if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Fonts)>0) {
                result|=0x80;
            }
            if((Settings.InvalidationFlags&(ArchiveInvalidationFlags.Misc|ArchiveInvalidationFlags.Base))>0) {
                result|=0x120;
            }
            return result;
        }

        private static string[] ProcessFolder(BSAEditData Data, BSAEdit Edits, BinaryWriter bw) {
            List<string> Problems=new List<string>();
            for(int i=1;i<Data.Files.Count-1;i++) {
                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.IgnoreNormal)>0) {
                    if(Data.Files[i].FileName.EndsWith("_n.dds")) continue;
                    if(Data.Files[i].FileName.EndsWith("_g.dds")) continue;
                }
                if(Data.Files[i].Exists) {
                    int count=1;
                    bool EditDown=false;
                    bool EditAtAll=true;

                    ulong TempHash=Data.Files[i].Hash+1;
                    while(Data.Files[i+count].Hash==TempHash) {
                        if(!Data.Files[i+count++].Exists) {
                            EditDown=true;
                            break;
                        } else TempHash++;
                    }
                    if(!EditDown) {
                        if(Data.Files[i+count].Hash>TempHash+300) TempHash+=0xCD;
                        else if(Data.Files[i+count].Hash>TempHash+20) TempHash+=0xD;
                    } else {
                        TempHash=Data.Files[i].Hash-1;
                        count=1;
                        while(Data.Files[i-count].Hash==TempHash) {
                            if(!Data.Files[i-count++].Exists) {
                                hashCollisions++;
                                Problems.Add(Data.Files[i].FileName);
                                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.HashWarn)>0) {
                                    Program.logger.WriteToLog("Hash map collision between:\n" +
                                    Data.Files[i - 1].FileName + "\n" +
                                    Data.Files[i].FileName + "\n" +
                                    Data.Files[i + 1].FileName + "\n\n" +
                                    Data.Files[i].FileName + " may not show up correctly in game", Logger.LogLevel.Warning);
//                                    System.Windows.Forms.MessageBox.Show("Hash map collision between:\n" +
//                                    Data.Files[i-1].FileName+"\n"+
//                                    Data.Files[i].FileName+"\n"+
//                                    Data.Files[i+1].FileName+"\n\n"+
//                                    Data.Files[i].FileName+" may not show up correctly in game", "Warning");
                                }
                                EditAtAll=false;
                                break;
                            } else TempHash--;
                        }
                        if(EditAtAll) {
                            if(Data.Files[i-count].Hash<TempHash-300) TempHash-=0xCD;
                            else if(Data.Files[i-count].Hash<TempHash-20) TempHash-=0xD;
                        }
                    }
                    if(EditAtAll) {
                        entriesModified++;
                        bw.BaseStream.Position=Data.Files[i].HashOffset;
                        bw.Write(TempHash);
                        Edits.Offsets.Add(Data.Files[i].HashOffset);
                        Edits.OldData.Add(Data.Files[i].Hash);
                    }
                }
            }
            return Problems.ToArray();
        }

        public static void SnipBSA() {
            filesModified=0;
            entriesModified=0;
            hashCollisions=0;
            List<BSAEdit> Edits=new List<BSAEdit>();
            List<string> ProblemFiles=new List<string>();
            string[] Paths=Directory.GetFiles(Program.currentGame.DataFolderPath, "*.bsa");
            ProgressForm pf=new ProgressForm("", false);
            pf.SetProgressRange(Paths.Length+1);
            pf.ShowInTaskbar = true;
            pf.Show();
            foreach(string path in Paths) {
                pf.Text="Patching BSA '"+Path.GetFileName(path)+"'";
                pf.UpdateProgress();
                pf.Invalidate();
                pf.Update();
                FileInfo fi=new FileInfo(path);
                BSAEdit be=new BSAEdit(path,fi.Length);
                DateTime origdate=fi.LastWriteTime;
                BinaryReader br;
                try {
                    br = new BinaryReader(File.Open(path, FileMode.Open), System.Text.Encoding.GetEncoding("ISO-8859-1"));
                } catch(Exception ex) {
                    Program.logger.WriteToLog("BSA '" + path + "' could not be opened, and has not been edited.\n" +
                        "Make sure that nothing else is using this file, and then try running the BSA patcher again.\n" +
                        "Error: " + ex.Message, Logger.LogLevel.Error);
//                    MessageBox.Show("BSA '" + path + "' could not be opened, and has not been edited.\n" +
//                        "Make sure that nothing else is using this file, and then try running the BSA patcher again.\n"+
//                        "Error: "+ex.Message);
                    continue;
                }
                BinaryWriter bw=new BinaryWriter(br.BaseStream);
                try {
                    if(Program.ReadCString(br)!="BSA") continue;
                    br.ReadUInt32();
                    int offset=br.ReadInt32();
                    br.ReadUInt32();
                    int FolderCount=br.ReadInt32();
                    int TotalFileCount=br.ReadInt32();
                    int TotalFolderLength=br.ReadInt32();
                    int TotalFileNameLength=br.ReadInt32();
                    uint flags=br.ReadUInt32();
                    if((Settings.InvalidationFlags&ArchiveInvalidationFlags.EditAllEntries)==0) {
                        if((flags&GetFlags())==0) continue;
                    }
                    long FileNameBlockStart=offset+TotalFolderLength+16*TotalFileCount+17*FolderCount;
                    long folderpos=br.BaseStream.Position;
                    for(int i=0;i<FolderCount;i++) {
                        br.BaseStream.Position=folderpos+8;
                        int filecount=br.ReadInt32();
                        br.BaseStream.Position=br.ReadInt32()-TotalFileNameLength+1;
                        string fname=Program.ReadCString(br).Replace('/', '\\');
                        if(Directory.Exists(Path.Combine(Program.currentGame.DataFolderPath,fname))) {
                            BSAEditData EditData=new BSAEditData(fname);
                            EditData.Files.Add(new BSAEditFileInfo(0, 0));
                            for(int j=0;j<filecount;j++) {
                                long pos=br.BaseStream.Position;
                                EditData.Files.Add(new BSAEditFileInfo(br.ReadUInt64(), pos));
                                br.BaseStream.Position+=8;
                            }
                            EditData.Files.Add(new BSAEditFileInfo(ulong.MaxValue, 0));
                            br.BaseStream.Position=FileNameBlockStart;
                            for(int j=0;j<filecount;j++) {
                                string FileName=Program.ReadCString(br);
                                EditData.Files[j+1].FileName=FileName;
                                EditData.Files[j+1].Exists=File.Exists(Path.Combine(Program.currentGame.DataFolderPath,Path.Combine(fname,FileName)));
                            }
                            FileNameBlockStart=br.BaseStream.Position;
                            if((Settings.InvalidationFlags&ArchiveInvalidationFlags.EditAllEntries)>0||ShouldFolderBeProcessed(fname)) {
                                string[] Problems=ProcessFolder(EditData, be, bw);
                                if(ShouldFolderBeProcessed(fname)) {
                                    foreach(string s in Problems) ProblemFiles.Add(fname+'\\'+s);
                                }
                            }
                        } else {
                            br.BaseStream.Position=FileNameBlockStart;
                            for(int j=0;j<filecount;j++) Program.ReadCString(br);
                            FileNameBlockStart=br.BaseStream.Position;
                        }
                        folderpos+=16;
                    }
                } catch { 
                    continue; 
                } finally {
                    br.Close();
                }
                if(be.Offsets.Count!=0) Edits.Add(be);
                fi.LastWriteTime=origdate;
            }
            pf.Text="Patching BSA's (Saving changes)";
            if(Edits.Count>0) {
                Formatter f=new Formatter();
                Stream s=File.Create(Program.BSAEditFile);
                f.Serialize(s, Edits);
                s.Close();
                filesModified=Edits.Count;
            } else {
                filesModified=0;
                entriesModified=0;
            }
            if(ProblemFiles.Count>0&&(Settings.InvalidationFlags&ArchiveInvalidationFlags.HashGenAI)>0) {
                StreamWriter sw=new StreamWriter(File.Create(InvalidationFile), System.Text.Encoding.Default);
                foreach(string s in ProblemFiles) sw.WriteLine(s.Replace('\\', '/'));
                sw.Close();
            } else {
                try { File.Delete(InvalidationFile); } catch { }
            }
            pf.Unblock();
            pf.Close();
        }

        public static void UpdateInvalidationFile() {
            if((Settings.InvalidationFlags&ArchiveInvalidationFlags.BSARedirection)==0) UnregisterBSA(RedirectionPath);
            if((Settings.InvalidationFlags&ArchiveInvalidationFlags.EditBSAs)>0) {
                if(RestoreBSA()==-1) return;
                SnipBSA();
                return;
            }
            if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Universal)>0) {
                StreamWriter sw=new StreamWriter(File.Open(InvalidationFile, FileMode.Create), System.Text.Encoding.Default);
                sw.WriteLine("meshes/ \\s");
                sw.WriteLine("textures/ \\s");
                sw.WriteLine("sound/ \\s");
                sw.Close();
                return;
            }
            if((Settings.InvalidationFlags&ArchiveInvalidationFlags.BSARedirection)>0) {
                if(File.Exists(InvalidationFile)) File.Delete(InvalidationFile);
                string path=((Settings.InvalidationFlags&ArchiveInvalidationFlags.PackFaceTextures)>0)?Path.GetFullPath(Path.Combine(Program.currentGame.DataFolderPath,"textures\\faces")):null;
                if(path!=null&&!Directory.Exists(path)) path=null;
                Forms.BSACreator.CreateBSA(RedirectionPath, path, null, 0, 0, true);
                RegisterBSA(Path.Combine(RedirectionPath));
                return;
            }
            List<string> files=new List<string>();
            string textures = Path.Combine(Program.currentGame.DataFolderPath, "textures");
            string meshes = Path.Combine(Program.currentGame.DataFolderPath, "meshes");
            string sound = Path.Combine(Program.currentGame.DataFolderPath, "sound");
            string music = Path.Combine(Program.currentGame.DataFolderPath, "music");
            string fonts = Path.Combine(Program.currentGame.DataFolderPath, "fonts");
            string menus = Path.Combine(Program.currentGame.DataFolderPath, "menus");
            string video = Path.Combine(Program.currentGame.DataFolderPath, "video");
            string shaders = Path.Combine(Program.currentGame.DataFolderPath, "shaders");
            string lsdata = Path.Combine(Program.currentGame.DataFolderPath, "lsdata");
            string facegen = Path.Combine(Program.currentGame.DataFolderPath, "facegen");
            string trees = Path.Combine(Program.currentGame.DataFolderPath, "trees");
            string distantlod = Path.Combine(Program.currentGame.DataFolderPath, "distantlod");

            if ((Settings.InvalidationFlags & ArchiveInvalidationFlags.MatchExtensions) > 0)
            {

                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Textures)>0) {
                    if(Directory.Exists(textures)) {
                        List<string> temp=new List<string>();
                        temp.AddRange(Directory.GetFiles(textures, "*.dds", SearchOption.AllDirectories));
                        temp.AddRange(Directory.GetFiles(textures, "*.tga", SearchOption.AllDirectories));
                        temp.AddRange(Directory.GetFiles(textures, "*.hdr", SearchOption.AllDirectories));
                        temp.AddRange(Directory.GetFiles(textures, "*.bmp", SearchOption.AllDirectories));
                        temp.AddRange(Directory.GetFiles(textures, "*.jpg", SearchOption.AllDirectories));
                        temp.AddRange(Directory.GetFiles(textures, "*.jpeg", SearchOption.AllDirectories));
                        if((Settings.InvalidationFlags&ArchiveInvalidationFlags.IgnoreNormal)>0) {
                            for(int i=0;i<temp.Count;i++) {
                                if(Path.GetFileNameWithoutExtension(temp[i]).EndsWith("_n")||
                                Path.GetFileNameWithoutExtension(temp[i]).EndsWith("_g"))
                                    temp.RemoveAt(i--);
                            }
                        }
                        files.AddRange(temp.ToArray());
                    }
                }
                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Meshes)>0) {
                    if(Directory.Exists(meshes))
                    files.AddRange(Directory.GetFiles(meshes, "*.nif", SearchOption.AllDirectories));
                }
                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Sounds)>0) {
                    if(Directory.Exists(sound)) {
                        files.AddRange(Directory.GetFiles(sound, "*.mp3", SearchOption.AllDirectories));
                        files.AddRange(Directory.GetFiles(sound, "*.wav", SearchOption.AllDirectories));
                    }
                }
                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Music)>0) {
                    if(Directory.Exists(music)) {
                        files.AddRange(Directory.GetFiles(music, "*.mp3", SearchOption.AllDirectories));
                        files.AddRange(Directory.GetFiles(music, "*.wav", SearchOption.AllDirectories));
                    }
                }
                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Fonts)>0) {
                    if(Directory.Exists(fonts)) {
                        files.AddRange(Directory.GetFiles(fonts, "*.fnt", SearchOption.AllDirectories));
                        files.AddRange(Directory.GetFiles(fonts, "*.tex", SearchOption.AllDirectories));
                    }
                }
                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Menus)>0) {
                    if(Directory.Exists(menus)) {
                        files.AddRange(Directory.GetFiles(menus, "*.xml", SearchOption.AllDirectories));
                        files.AddRange(Directory.GetFiles(menus, "*.txt", SearchOption.AllDirectories));
                    }
                }
                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Misc)>0) {
                    if(Directory.Exists(video))
                        files.AddRange(Directory.GetFiles(video, "*.bik", SearchOption.AllDirectories));
                    if(Directory.Exists(shaders)) {
                        files.AddRange(Directory.GetFiles(shaders, "*.fx", SearchOption.AllDirectories));
                        files.AddRange(Directory.GetFiles(shaders, "*.sdp", SearchOption.AllDirectories));
                    }
                    if(Directory.Exists(lsdata))
                        files.AddRange(Directory.GetFiles(lsdata, "*.dat", SearchOption.AllDirectories));
                    if(Directory.Exists(facegen))
                    files.AddRange(Directory.GetFiles(facegen, "*.ctl", SearchOption.AllDirectories));
                }
                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.TreesLOD)>0) {
                    if(Directory.Exists(trees))
                        files.AddRange(Directory.GetFiles(trees, "*.spt", SearchOption.AllDirectories));
                    if(Directory.Exists(distantlod)) {
                        files.AddRange(Directory.GetFiles(distantlod, "*.lod", SearchOption.AllDirectories));
                        files.AddRange(Directory.GetFiles(distantlod, "*.cmp", SearchOption.AllDirectories));
                    }
                }
            } else {
                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Textures)>0) {
                    if(Directory.Exists(textures)) {
                        List<string> temp=new List<string>();
                        temp.AddRange(Directory.GetFiles(textures, "*", SearchOption.AllDirectories));
                        if((Settings.InvalidationFlags&ArchiveInvalidationFlags.IgnoreNormal)>0) {
                            for(int i=0;i<temp.Count;i++) {
                                if(Path.GetFileNameWithoutExtension(temp[i]).EndsWith("_n")||
                                Path.GetFileNameWithoutExtension(temp[i]).EndsWith("_g"))
                                    temp.RemoveAt(i--);
                            }
                        }
                        files.AddRange(temp.ToArray());
                    }
                }
                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Meshes)>0) {
                    if(Directory.Exists(meshes))
                        files.AddRange(Directory.GetFiles(meshes, "*", SearchOption.AllDirectories));
                }
                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Sounds)>0) {
                    if(Directory.Exists(sound))
                        files.AddRange(Directory.GetFiles(sound, "*", SearchOption.AllDirectories));
                }
                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Music)>0) {
                    if(Directory.Exists(music))
                        files.AddRange(Directory.GetFiles(music, "*", SearchOption.AllDirectories));
                }
                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Fonts)>0) {
                    if(Directory.Exists(fonts))
                        files.AddRange(Directory.GetFiles(fonts, "*", SearchOption.AllDirectories));
                }
                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Menus)>0) {
                    if(Directory.Exists(menus))
                        files.AddRange(Directory.GetFiles(menus, "*", SearchOption.AllDirectories));
                }
                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Misc)>0) {
                    if(Directory.Exists(video))
                        files.AddRange(Directory.GetFiles(video, "*", SearchOption.AllDirectories));
                    if(Directory.Exists(shaders))
                        files.AddRange(Directory.GetFiles(shaders, "*", SearchOption.AllDirectories));
                    if(Directory.Exists(lsdata))
                        files.AddRange(Directory.GetFiles(lsdata, "*", SearchOption.AllDirectories));
                    if(Directory.Exists(facegen))
                        files.AddRange(Directory.GetFiles(facegen, "*", SearchOption.AllDirectories));
                }
                if((Settings.InvalidationFlags&ArchiveInvalidationFlags.TreesLOD)>0) {
                    if(Directory.Exists(trees))
                        files.AddRange(Directory.GetFiles(trees, "*", SearchOption.AllDirectories));
                    if(Directory.Exists(distantlod))
                        files.AddRange(Directory.GetFiles(distantlod, "*", SearchOption.AllDirectories));
                }
            }
            if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Base)>0) {
                List<string> temp=new List<string>();
                temp.AddRange(Directory.GetFiles(Program.currentGame.DataFolderPath, "*"));
                for(int i=0;i<temp.Count;i++) {
                    switch(Path.GetExtension(temp[i]).ToLower()) {
                    case ".esp":
                    case ".esm":
                    case ".bsa":
                    case ".txt":
                        temp.RemoveAt(i--);
                        break;
                    }
                }
                files.AddRange(temp.ToArray());
            }
            for(int i=0;i<files.Count;i++) {
                files[i]=files[i].ToLower();
                if(files[i].StartsWith(Program.currentGame.DataFolderPath)) files[i]=files[i].Substring((Program.currentGame.DataFolderPath +"\\").Length);
            }
            if((Settings.InvalidationFlags&ArchiveInvalidationFlags.BSAOnly)>0) {
                string[] entries=GetBSAEntries();
                files.Sort();
                int upto=0;
                for(int i=0;i<files.Count;) {
                    if(upto==entries.Length) {
                        files.RemoveRange(i, files.Count-i);
                        break;
                    }
                    int result=string.Compare(entries[upto], files[i]);
                    if(result>0) files.RemoveAt(i);
                    else if(result<0) upto++;
                    else i++;
                }
            }
            entriesModified=files.Count;
            if(files.Count>0) {
                StreamWriter sw=new StreamWriter(File.Open(InvalidationFile, FileMode.Create), System.Text.Encoding.Default);
                foreach(string s in files) sw.WriteLine(s.Replace('\\','/'));
                sw.Close();
            } else {
                File.Delete(InvalidationFile);
            }
        }

        public static ulong GenHash(string s) {
            string extension="";
            int i=s.LastIndexOf('.');
            if(i!=-1) {
                extension=s.Substring(i);
                s=s.Remove(i);
            }
            return GenHash(s, extension);
        }

        public static ulong GenHash(string file, string ext) {
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
            if(file.Length>3) hash+=(ulong)(GenHash2(file.Substring(1, file.Length-3))*0x100000000);
            if(ext.Length>0) {
                hash+=(ulong)(GenHash2(ext)*0x100000000);
                byte i=0;
                switch(ext) {
                case ".nif": i=1; break;
                case ".kf": i=2; break;
                case ".dds": i=3; break;
                case ".wav": i=4; break;
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

        public static uint GenHash2(string s) {
            uint hash=0;
            for(int i=0;i<s.Length;i++) {
                hash*=0x1003f;
                hash+=(byte)s[i];
            }
            return hash;
        }

        public static void ResetTimeStamps()
        {
            DateTime date=new DateTime(2006,1,1);
            if (Program.currentGame.NickName.Contains("skyrim"))
            {
                // first cover standard BSA
                foreach (string s in System.IO.Directory.GetFiles(Program.currentGame.DataFolderPath, "Skyrim - *.bsa"))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(s);
                    fi.LastWriteTime = date;
                }
                // now cover BSAs according to esp/esm order
                foreach (EspInfo espi in Program.Data.Esps)
                {
                    string s = espi.FileName; s = s.Replace(".esm", "").Replace(".esp", "").Replace(".esl", ""); s = s + ".bsa"; s = Path.Combine(Program.currentGame.DataFolderPath,s);
                    if (System.IO.File.Exists(s))
                    {
                        date=date.AddMinutes(1);
                        System.IO.FileInfo fi = new System.IO.FileInfo(s);
                        fi.LastWriteTime = date;
                    }
                }
            }
            else
            {
                foreach (string s in System.IO.Directory.GetFiles(Program.currentGame.DataFolderPath, "*.bsa"))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(s);
                    fi.LastWriteTime = date;
                    date = date.AddMinutes(1);
                }
            }

        }
    }

    public static class OblivionRenderInfo {
        private static readonly string info=Path.Combine(Program.INIDir,"RendererInfo.txt");

        public static string GetInfo(string s) {
            s=s.ToLower();
            try {
                string[] lines=File.ReadAllLines(info);
                for(int i=0;i<lines.Length;i++) {
                    lines[i]=lines[i].Trim().ToLower();
                    if(lines[i].StartsWith(s)) {
                        lines=lines[i].Split(':');
                        if(lines.Length!=2) return "-1";
                        return lines[1].Trim();
                    }
                }
                return "Value '"+s+"' not found";
            } catch (Exception e) { return e.Message; }
        }

    }

}