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
using System.Security.Cryptography;
using System.Collections.Generic;
using Serializable=System.SerializableAttribute;
using Path=System.IO.Path;
using File=System.IO.File;
using Directory=System.IO.Directory;
using System.IO;

namespace OblivionModManager {
    public class omodGroup {
        private string name;
        private System.Drawing.Font font;
        private System.Drawing.Color color;

        public string Name { get { return name; } }
        public System.Drawing.Font Font { get { return font; } }
        public System.Drawing.Color Color { get { return color; } }

        public omodGroup(string Name) {
            name=Name;
            font=null;
            color=System.Drawing.Color.Black;
        }

        public void SetFont(System.Drawing.Font Font, System.Drawing.Color Color) {
            font=Font;
            color=Color;
        }

        public void Rename(string Name) {
            name=Name;
        }

        public override string ToString() {
            return Name;
        }

        public void Write(System.IO.BinaryWriter bw, System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter) {
            bw.Write(name);
            if(Font!=null) {
                bw.Write(true);
                System.IO.MemoryStream ms=new System.IO.MemoryStream(512);
                formatter.Serialize(ms, font);
                bw.Write((int)ms.Length);
                bw.Write(ms.ToArray());
                bw.Write(color.ToArgb());
            } else {
                bw.Write(false);
            }
        }

        public static omodGroup Read(System.IO.BinaryReader br, System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter) {
            omodGroup og=new omodGroup(br.ReadString());
            if(br.ReadBoolean()) {
                System.IO.MemoryStream ms=new System.IO.MemoryStream(br.ReadBytes(br.ReadInt32()));
                og.SetFont((System.Drawing.Font)formatter.Deserialize(ms),System.Drawing.Color.FromArgb(br.ReadInt32()));
            }
            return og;
        }
    }

    [System.Flags]
    public enum ArchiveInvalidationFlags : uint {
        Textures  = 0x1,
        Meshes    = 0x2,
        Sounds    = 0x4,
        Music     = 0x8,
        TreesLOD  = 0x10,
        Fonts     = 0x20,
        Menus     = 0x40,
        Misc      = 0x80,
        Base      = 0x100,
        IgnoreNormal    = 0x1000,
        BSAOnly         = 0x2000,
        MatchExtensions = 0x4000,
        Universal       = 0x8000,
        EditBSAs        = 0x10000,
        EditAllEntries  = 0x20000,
        HashGenAI       = 0x40000,
        HashWarn        = 0x80000,
        BSARedirection  = 0x100000,
        PackFaceTextures= 0x200000,

        Default = ArchiveInvalidationFlags.BSAOnly|ArchiveInvalidationFlags.MatchExtensions|
                  ArchiveInvalidationFlags.Textures|ArchiveInvalidationFlags.BSARedirection|
                  ArchiveInvalidationFlags.HashGenAI
    }

    public struct ScriptLabel {
        public string Label;
        public int LineNo;

        public ScriptLabel(string label, int lineNo) {
            Label=label;
            LineNo=lineNo;
        }
    }

    public struct PluginLoadInfo {
        public string Plugin;
        public string Target;
        public bool LoadAfter;

        public PluginLoadInfo(string plugin, string target, bool loadAfter) {
            Plugin=plugin;
            Target=target;
            LoadAfter=loadAfter;
        }
    }

    public struct ScriptEspEdit {
        public readonly bool IsGMST;
        public readonly string Plugin;
        public readonly string EDID;
        public readonly string Value;

        public ScriptEspEdit(bool gmst, string plugin, string edid, string value) {
            IsGMST=gmst;
            Plugin=plugin;
            EDID=edid;
            Value=value;
        }
    }

    public class ScriptReturnData {
        public readonly List<string> IgnorePlugins=new List<string>();
        public readonly List<string> InstallPlugins=new List<string>();
        public bool InstallAllPlugins=true;
        public readonly List<string> IgnoreData=new List<string>();
        public readonly List<string> InstallData=new List<string>();
        public bool InstallAllData=true;
        public readonly List<PluginLoadInfo> LoadOrderList=new List<PluginLoadInfo>();
        public readonly List<ConflictData> ConflictsWith=new List<ConflictData>();
        public readonly List<ConflictData> DependsOn=new List<ConflictData>();
        public readonly List<string> RegisterBSAList=new List<string>();
        public bool CancelInstall=false;
        public readonly List<string> UncheckedPlugins=new List<string>();
        public readonly List<ScriptEspWarnAgainst> EspDeactivation=new List<ScriptEspWarnAgainst>();
        public readonly List<ScriptCopyDataFile> CopyDataFiles=new List<ScriptCopyDataFile>();
        public readonly List<ScriptCopyDataFile> CopyPlugins=new List<ScriptCopyDataFile>();
        public readonly List<INIEditInfo> INIEdits=new List<INIEditInfo>();
        public readonly List<SDPEditInfo> SDPEdits=new List<SDPEditInfo>();
        public readonly List<ScriptEspEdit> EspEdits=new List<ScriptEspEdit>();
        public readonly List<string> EarlyPlugins=new List<string>();
    }

    public class ScriptExecutationData {
        public PluginLoadInfo[] PluginOrder;
        public string[] UncheckedPlugins;
        public ScriptEspWarnAgainst[] EspDeactivationWarning;
        public ScriptEspEdit[] EspEdits;
        public string[] EarlyPlugins;
    }

    [@Serializable]
    public struct ConflictData {
        public ConflictLevel level;
        public string File;
        public int MinMajorVersion;
        public int MinMinorVersion;
        public int MaxMajorVersion;
        public int MaxMinorVersion;
        public string Comment;
        public bool Partial;

        public ConflictData(BinaryReader _Reader)
        {
            level = ConflictLevel.NoConflict; //init
            level = (ConflictLevel)_Reader.ReadInt32();
            File = _Reader.ReadString();
            MinMajorVersion = _Reader.ReadInt32();
            MinMinorVersion = _Reader.ReadInt32();
            MaxMajorVersion = _Reader.ReadInt32();
            MaxMinorVersion = _Reader.ReadInt32();
            Comment = _Reader.ReadString();
            Partial = _Reader.ReadBoolean();
        }
        public void WriteDataTo(BinaryWriter _Writer)
        {
            _Writer.Write((int)level);
            _Writer.Write(File);
            _Writer.Write(MinMajorVersion);
            _Writer.Write(MinMinorVersion);
            _Writer.Write(MaxMajorVersion);
            _Writer.Write(MaxMinorVersion);
            _Writer.Write(Comment);
            _Writer.Write(Partial);
        }

  
        public static bool operator==(ConflictData cd, omod o) {
            if(!cd.Partial&&cd.File!=o.ModName) return false;
            if(cd.Partial) {
                System.Text.RegularExpressions.Regex reg=new System.Text.RegularExpressions.Regex(cd.File, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if(!reg.IsMatch(o.ModName)) return false;
            }
            if(cd.MaxMajorVersion!=0||cd.MaxMinorVersion!=0) {
                if(cd.MaxMajorVersion>o.MajorVersion) return false;
                if(cd.MaxMajorVersion==o.MajorVersion&&cd.MaxMinorVersion>o.MinorVersion) return false;
            }
            if(cd.MinMajorVersion!=0||cd.MinMinorVersion!=0) {
                if(cd.MinMajorVersion<o.MajorVersion) return false;
                if(cd.MinMajorVersion==o.MajorVersion&&cd.MinMinorVersion<o.MinorVersion) return false;
            }
            return true;
        }
        public static bool operator!=(ConflictData cd, omod o) {
            return !(cd==o);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
        public override bool Equals(object obj) {
            if(obj is ConflictData) {
                ConflictData cd=(ConflictData)obj;
                if(File==cd.File&&MinMajorVersion==cd.MinMajorVersion&&MinMinorVersion==cd.MinMinorVersion&&
                    MaxMajorVersion==cd.MaxMajorVersion&&MaxMinorVersion==cd.MaxMinorVersion&&Comment==cd.Comment) {
                    return true;
                }
            } else if(obj is omod) {
                return (this==(omod)obj);
            }
            return false;
        }
    }

    public struct ScriptEspWarnAgainst {
        public string Plugin;
        public DeactiveStatus Status;

        public ScriptEspWarnAgainst(string plugin, DeactiveStatus status) {
            Plugin=plugin;
            Status=status;
        }
    }

    public struct ScriptCopyDataFile {
        public readonly string CopyFrom;
        public readonly string CopyTo;
        public readonly string hCopyFrom;
        public readonly string hCopyTo;
        public ScriptCopyDataFile(string from, string to) {
            CopyFrom=from.ToLower();
            CopyTo=to.ToLower();
            hCopyFrom=from;
            hCopyTo=to;
        }
    }

    public class omodCreationOptions {
        public string Name;
        public string Author;
        public string email;
        public string website;
        public string Description;
        public string Version;
        public string Image;
        public int MajorVersion;
        public int MinorVersion;
        public int BuildVersion;
        public CompressionType CompressionType;
        public CompressionLevel DataFileCompresionLevel;
        public CompressionLevel omodCompressionLevel;
        public string[] esps;
        public string[] espPaths;
        public string[] espSources;
        public string[] DataFiles;
        public string[] DataFilePaths;
        public string[] DataSources;
        public string readme;
        public string script;
        public bool bOmod2;
        public bool bSystemMod;
    }

    [@Serializable]
    public class BSA {
        public readonly List<string> UsedBy=new List<string>();
        public readonly string FileName;
        public readonly string LowerFileName;

        public BSA(BinaryReader _Reader)
        {
            int len = _Reader.ReadInt32();
            for (int i = 0; i < len; i++)
                UsedBy.Add(_Reader.ReadString());
            FileName = _Reader.ReadString();
            LowerFileName = _Reader.ReadString();
        }
        public void WriteDataTo(BinaryWriter _Writer)
        {
            _Writer.Write(UsedBy.Count);
            foreach (string file in UsedBy)
                _Writer.Write(file);
            _Writer.Write(FileName);
            _Writer.Write(LowerFileName);
        }
        public BSA(string fileName, string modname)
        {
            FileName=fileName;
            LowerFileName=FileName.ToLower();
            UsedBy.Add(modname);
        }
    }

    [@Serializable]
    public class EspInfo {
        public const string UnknownOwner="Unknown";
        public const string BaseOwner="Base";
        public static string[] BaseFiles = { Program.currentGame.Name+".esm" };

        public readonly string FileName;
        public readonly string LowerFileName;
        public string BelongsTo;
        public readonly List<string> MustLoadAfter=new List<string>();
        public readonly List<string> MustLoadBefore=new List<string>();
        public bool Active;
        public System.DateTime DateModified;
        public DeactiveStatus Deactivatable;
        public omod Parent;
        public string ParentOmodName;

        public ConflictDetector.HeaderInfo header;

        public EspInfo(BinaryReader _Reader)
        {
            //            UnknownOwner = _Reader.ReadString();
            //            BaseOwner = _Reader.ReadString();
            int len = _Reader.ReadInt32();
            for (int i = 0; i < len; i++)
                BaseFiles.SetValue(_Reader.ReadString(), i);
            FileName = _Reader.ReadString();
            LowerFileName = _Reader.ReadString();
            BelongsTo = _Reader.ReadString();
            len = _Reader.ReadInt32();
            for (int i = 0; i < len; i++)
                MustLoadAfter.Add(_Reader.ReadString());
            len = _Reader.ReadInt32();
            for (int i = 0; i < len; i++)
                MustLoadBefore.Add(_Reader.ReadString());
            Active = _Reader.ReadBoolean();
            DateModified = new System.DateTime(_Reader.ReadInt64());
            Deactivatable = (DeactiveStatus)_Reader.ReadInt32();
            ParentOmodName = _Reader.ReadString();
            Parent = null;
            header = new ConflictDetector.HeaderInfo();
            header.Author = _Reader.ReadString();
            header.Description = _Reader.ReadString();
        }
        public void WriteDataTo(BinaryWriter _Writer)
        {
            _Writer.Write((System.Int32)BaseFiles.Length);
            foreach (string file in BaseFiles)
                _Writer.Write(file);
            if (FileName == null || LowerFileName == null || BelongsTo == null)
                Program.logger.WriteToLog("Corrupt ESP entry! Filename= "+FileName+" lowerfilename="+LowerFileName+" BelongsTO="+BelongsTo,Logger.LogLevel.Low);
            _Writer.Write((FileName != null) ? FileName : "");
            _Writer.Write((LowerFileName != null) ? LowerFileName : "");
            _Writer.Write((BelongsTo != null) ? BelongsTo : "");
            _Writer.Write((System.Int32)MustLoadAfter.Count);
            foreach (string file in MustLoadAfter)
                _Writer.Write(file);
            _Writer.Write((System.Int32)MustLoadBefore.Count);
            foreach (string file in MustLoadBefore)
                _Writer.Write(file);
            _Writer.Write(Active);
            _Writer.Write((System.Int64)DateModified.Ticks);
            _Writer.Write((int)Deactivatable);
            _Writer.Write((Parent != null) ? Parent.FileName : "");
            _Writer.Write((header.Author != null) ? header.Author : "");
            _Writer.Write((header.Description != null) ? header.Description : "");
        }

        public void GetHeader()
        {
            if(!System.IO.File.Exists(Path.Combine(Program.currentGame.DataFolderPath,FileName))) return;
            header=ConflictDetector.TesFile.GetHeader(Path.Combine(Program.currentGame.DataFolderPath,FileName));
        }

        public void Unlink() {
            if(Parent==null) return;
            Parent.UnlinkPlugin(this);
            Parent=null;
            Deactivatable=DeactiveStatus.Allow;
            BelongsTo=UnknownOwner;
        }

        public EspInfo(string fileName,omod parent) {
            FileName=fileName;
            LowerFileName=FileName.ToLower();
            BelongsTo=parent.FileName.Substring(0,parent.FileName.Length-5);
            Parent=parent;
            GetHeader();
            Deactivatable=Settings.DefaultEspWarn;
        }
        public EspInfo(string fileName) {
            FileName=fileName;
            LowerFileName=FileName.ToLower();
            if(System.Array.IndexOf<string>(BaseFiles, LowerFileName)!=-1) {
                BelongsTo=BaseOwner;
            } else {
                BelongsTo=UnknownOwner;
            }
            Parent=null;
            Deactivatable=DeactiveStatus.Allow;
            GetHeader();
        }

#if DEBUG
        public override string ToString() {
            return FileName;
        }
#endif
    }

    [@Serializable]
    public class DataFileInfo {
        public readonly string FileName;
        public readonly string LowerFileName;
        public uint CRC;
        private readonly List<string> UsedBy=new List<string>();
        public object Tag = null;

        public DataFileInfo(BinaryReader _Reader)
        {
            FileName = _Reader.ReadString();
            LowerFileName = _Reader.ReadString();
            CRC = _Reader.ReadUInt32();
            int len = _Reader.ReadInt32();
            for (int i = 0; i < len; i++)
            {
                string modname = _Reader.ReadString();
                // only add to existing mods
                if (File.Exists(Path.Combine(Settings.omodDir,modname)) || File.Exists(Path.Combine(Settings.omodDir, modname + ".ghost")))
                    UsedBy.Add(modname);
                else
                {
                    // file does not exists no need to keep dependency
                }

            }
        }
        public void WriteDataTo(BinaryWriter _Writer)
        {
            _Writer.Write(FileName);
            _Writer.Write(LowerFileName);
            _Writer.Write(CRC);
            _Writer.Write((System.Int32)UsedBy.Count);
            foreach (string usedby in UsedBy)
                _Writer.Write(usedby);
        }
        

        public static bool operator==(DataFileInfo a, DataFileInfo b) {
            if(null==(object)a||null==(object)b) {
                if(null!=(object)a||null!=(object)b) return false; else return true;
            }
            return (a.LowerFileName==b.LowerFileName);
        }
        public static bool operator!=(DataFileInfo a, DataFileInfo b) {
            return !(a==b);
        }
        public override bool Equals(object obj) {
            if(!(obj is DataFileInfo)) return false;
            return this==(DataFileInfo)obj;
        }
        public override int GetHashCode() { return LowerFileName.GetHashCode(); }

        //I dont really want this to be here, but .NET does some stupid implicit convertion by calling ToString() it isn't
        /*public static implicit operator string(DataFileInfo dfi) {
            return dfi.FileName;
        }*/

        public override string ToString() {
            return FileName;
        }

        public DataFileInfo(string s,uint crc) {
            FileName=s;
            LowerFileName=FileName.ToLower();
            CRC=crc;

            if (CRC == 0)
            {
                if (File.Exists(s))
                {
                    CRC = Crc32.ComputeCRC(s);
                }
                else
                {
                    Program.logger.WriteToLog("Cannot compute CRC for '" + s + "': file cannot be found", Logger.LogLevel.High);
                }
            }
        }
        public DataFileInfo(DataFileInfo orig): this(orig.FileName, orig.CRC)
        {
        }

        public void AddOwner(omod o) { if(!UsedBy.Contains(o.LowerFileName)) UsedBy.Add(o.LowerFileName); }
        public void RemoveOwner(omod o) {
            UsedBy.Remove(o.LowerFileName);
            Program.logger.WriteToLog("Deleting " + FileName, Logger.LogLevel.High);
            string basepath = o.bSystemMod? Path.Combine(Program.currentGame.DataFolderPath, "..") : Program.currentGame.DataFolderPath;
            if (UsedBy.Count == 0)
            {
                Program.Data.DataFiles.Remove(this.LowerFileName);
                if (File.Exists(Path.Combine(basepath, FileName))) File.Delete(Path.Combine(basepath, FileName));
                if (File.Exists(Path.Combine(basepath, FileName + "." + o.LowerFileName))) File.Delete(Path.Combine(basepath, FileName + "." + o.LowerFileName));
            }
            else
            {
                // restore the last installed if possible
                if (File.Exists(Path.Combine(basepath, this.FileName+"."+UsedBy[UsedBy.Count-1])))
                    File.Copy(Path.Combine(basepath, this.FileName+"."+UsedBy[UsedBy.Count-1]),Path.Combine(basepath, this.FileName), true);
                if (File.Exists(Path.Combine(basepath, FileName + "." + o.LowerFileName)))
                    File.Delete(Path.Combine(basepath, FileName + "." + o.LowerFileName));

                // restoring the file that came from the first mod
                //foreach (omod o2 in Program.Data.omods)
                //{
                //    if (o2.LowerFileName == UsedBy[0])
                //    {
                //        // check the CRC
                //        foreach (DataFileInfo dfi in o2.DataFiles)
                //        {
                //            if (dfi.LowerFileName == this.LowerFileName)
                //            {
                //                if (dfi.CRC != this.CRC)
                //                {
                //                    //string path = o2.GetDataFiles();
                //                    //File.Copy(Path.Combine(path, this.FileName), Path.Combine(Program.DataFolderName+"", this.FileName), true);
                //                }
                //                break;
                //            }
                //        }
                //    }
                //}
            }
        }
        public string Owners { get { return string.Join(", ", UsedBy.ToArray()); } }
        public List<string> OwnerList { get { return UsedBy; } }
    }

    [@Serializable]
    public class INIEditInfo {
        public readonly string Section="";
        public readonly string Name="";
        public string OldValue="";
        public readonly string NewValue="";
        public omod Plugin;
        public string omodName="";

        public INIEditInfo(string section, string name, string newvalue) {
            Section=section.ToLower();
            Name=name.ToLower();
            NewValue=newvalue;
            omodName = "";
        }

        public INIEditInfo(BinaryReader _Reader)
        {
            Section = _Reader.ReadString();
            Name = _Reader.ReadString();
            OldValue = _Reader.ReadString();
            NewValue = _Reader.ReadString();
            omodName = _Reader.ReadString();
            Plugin = null; //new omod(_Reader);
        }
        public void WriteDataTo(BinaryWriter _Writer)
        {
            _Writer.Write(Section);
            _Writer.Write(Name);
            _Writer.Write(OldValue==null?"":OldValue);
            _Writer.Write(NewValue);
            if (omodName.Length == 0 && Plugin!=null) omodName = Plugin.ModName;
            _Writer.Write(omodName); // Plugin.ModName);
            // Plugin.WriteDataTo(_Writer);
        }


        public static bool operator==(INIEditInfo a, INIEditInfo b) { return (a.Section==b.Section) && (a.Name==b.Name); }
        public static bool operator!=(INIEditInfo a, INIEditInfo b) { return (a.Section!=b.Section) || (a.Name!=b.Name); }
        public override bool Equals(object obj) { return this==(obj as INIEditInfo); }
        public override int GetHashCode() { return Section.GetHashCode() + Name.GetHashCode(); }
    }

    [@Serializable]
    public class SDPEditInfo {
        public readonly byte Package;
        public readonly string Shader="";
        public string BinaryObject="";

        public SDPEditInfo(byte package, string shader, string binaryObject) {
            Package=package;
            Shader=shader.ToLower();
            BinaryObject=binaryObject.ToLower();
        }
        public SDPEditInfo(BinaryReader _Reader)
        {
            Package = _Reader.ReadByte();
            Shader = _Reader.ReadString();
            BinaryObject = _Reader.ReadString();
        }
        public void WriteDataTo(BinaryWriter _Writer)
        {
            _Writer.Write((byte)Package);
            _Writer.Write(Shader);
            _Writer.Write(BinaryObject != null ? BinaryObject : string.Empty);
        }

    }

    public class obmmException : System.ApplicationException {
        public obmmException(string message) : base(message) { }
    }

    public enum CompressionType : byte { SevenZip, Zip }
    public enum CompressionLevel : byte { VeryHigh, High, Medium, Low, VeryLow, None }
    public enum ConflictLevel { Active, NoConflict, MinorConflict, MajorConflict, Unusable }
    public enum DeactiveStatus { Allow, WarnAgainst, Disallow }
    public enum EspSortOrder { LoadOrder, Alpha, Active, Owner, FileSize, DateCreated }
    public enum omodSortOrder { Name, Author, ConflictLevel, FileSize, DateCreated, DateInstalled, Group, ModID }
    public enum ScriptType { obmmScript, Python, cSharp, vb, xml, BAIN, Count }

    public class EspListSorter : System.Collections.IComparer {
        public static EspSortOrder order=EspSortOrder.LoadOrder;
        public int Compare(object a, object b) {
            System.Windows.Forms.ListViewItem la=(System.Windows.Forms.ListViewItem)a;
            System.Windows.Forms.ListViewItem lb=(System.Windows.Forms.ListViewItem)b;
            EspInfo ea = (EspInfo)la.Tag;
            EspInfo eb = (EspInfo)lb.Tag;
            if (ea == null) return -1;
            if (eb == null) return 1;
            switch(order) {
            case EspSortOrder.LoadOrder:
                    if (Path.GetExtension(ea.LowerFileName) != Path.GetExtension(eb.LowerFileName) && Settings.bPreventMovingESPBeforeESM)
                {
                    if (Path.GetExtension(ea.LowerFileName) == ".esm")
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else if (Program.loadOrderList.Count > 0)
                {
                    foreach (string strline in Program.loadOrderList)
                    {
                        string pin = strline.ToLower();
                        if (pin == ea.LowerFileName)
                        {
                            return -1;
                        }
                        else if (pin == eb.LowerFileName)
                        {
                            return 1;
                        }
                    }
                }
                if(ea.DateModified==eb.DateModified) return 0;
                if(ea.DateModified>eb.DateModified) return 1; else return -1;
            case EspSortOrder.Alpha:
                return string.Compare(ea.FileName, eb.FileName);  
            case EspSortOrder.Active:
                if(la.Checked==lb.Checked) return 0;
                if(la.Checked) return -1; else return 1;
            case EspSortOrder.Owner:
                return string.Compare(ea.BelongsTo, eb.BelongsTo);
            case EspSortOrder.FileSize:
                long sizea=(new System.IO.FileInfo(Path.Combine(Program.currentGame.DataFolderPath,ea.FileName))).Length;
                long sizeb=(new System.IO.FileInfo(Path.Combine(Program.currentGame.DataFolderPath,eb.FileName))).Length;
                if(sizea==sizeb) return 0;
                if(sizea>sizeb) return -1; else return 1;
            case EspSortOrder.DateCreated:
                System.DateTime da=(new System.IO.FileInfo(Path.Combine(Program.currentGame.DataFolderPath,ea.FileName))).CreationTime;
                System.DateTime db=(new System.IO.FileInfo(Path.Combine(Program.currentGame.DataFolderPath,eb.FileName))).CreationTime;
                return System.DateTime.Compare(da, db);
            default: return 0;
            }
        }
    }
    public class omodListSorter : System.Collections.IComparer {
        public static omodSortOrder order=omodSortOrder.Name;
        public int Compare(object a, object b) {
            System.Windows.Forms.ListViewItem la=(System.Windows.Forms.ListViewItem)a;
            System.Windows.Forms.ListViewItem lb=(System.Windows.Forms.ListViewItem)b;
            omod oa = (omod)la.Tag;
            omod ob = (omod)lb.Tag;
            System.DateTime da;
            System.DateTime db;
            switch(order) {
            case omodSortOrder.Name:
                return string.Compare(oa.FileName, ob.FileName);
            case omodSortOrder.Author:
                return string.Compare(oa.Author, ob.Author);
            case omodSortOrder.ConflictLevel:
                if((int)oa.Conflict==(int)ob.Conflict) return 0;
                if((int)oa.Conflict>(int)ob.Conflict) return 1; else return 0;
            case omodSortOrder.FileSize:
                long sizea=(new System.IO.FileInfo(Path.Combine(Settings.omodDir,oa.FileName))).Length;
                long sizeb=(new System.IO.FileInfo(Path.Combine(Settings.omodDir,ob.FileName))).Length;
                if(sizea==sizeb) return 0;
                if(sizea>sizeb) return -1; else return 1;
            case omodSortOrder.DateCreated:
                //This is deliberately backward
                return System.DateTime.Compare(ob.CreationTime, oa.CreationTime);
            case omodSortOrder.DateInstalled:
                //As is this
                da=(new System.IO.FileInfo(Path.Combine(Settings.omodDir,oa.FileName))).CreationTime;
                db=(new System.IO.FileInfo(Path.Combine(Settings.omodDir,ob.FileName))).CreationTime;
                return System.DateTime.Compare(db, da);
            case omodSortOrder.Group:
                if(oa.group==ob.group) return 0;
                if(oa.group>ob.group) return -1; else return 1;
            case omodSortOrder.ModID:
                string modidA=Program.GetModIDFromWebsite(oa.Website); if (modidA.Length==0)  modidA=Program.GetModID(oa.LowerFileName);
                string modidB=Program.GetModIDFromWebsite(ob.Website); if (modidB.Length==0)  modidB=Program.GetModID(ob.LowerFileName);
                int iModIDA = int.Parse((modidA.Length > 0 ? modidA : "0"));
                int iModIDB = int.Parse((modidB.Length>0?modidB:"0"));
                if (iModIDA == iModIDB) return 0;
                if (iModIDA > iModIDB) return -1; else return 1;
            default: return 0;
            }
        }
    }

    public class Crc32 : HashAlgorithm {
        public const UInt32 DefaultPolynomial = 0xedb88320;
        public const UInt32 DefaultSeed = 0xffffffff;

        private UInt32 hash;
        private UInt32 seed;
        private UInt32[] table;
        private static UInt32[] defaultTable;

        public Crc32() {
            table = InitializeTable(DefaultPolynomial);
            seed = DefaultSeed;
            Initialize();
        }

        public Crc32(UInt32 polynomial, UInt32 seed) {
            table = InitializeTable(polynomial);
            this.seed = seed;
            Initialize();
        }

        public static string ComputeToCRCString(string filename)
        {
            Crc32 crc32 = new Crc32();
            String hash = String.Empty;

            using (FileStream fs = File.Open(filename, FileMode.Open))
                foreach (byte b in crc32.ComputeHash(fs)) hash += b.ToString("x2").ToLower();

            return hash;
        }

        public static uint ComputeCRC(string filename)
        {
            Crc32 crc32 = new Crc32();
            uint hash = 0;

            using (FileStream fs = File.Open(filename, FileMode.Open))
            {
                byte[] b = crc32.ComputeHash(fs);
                hash |= (uint)(b[0]) << 24;
                hash |= (uint)(b[1]) << 16;
                hash |= (uint)(b[2]) << 8;
                hash |= (uint)(b[3]);

            }
            return hash;
        }

        public override void Initialize()
        {
            hash = seed;
        }
        
        protected override void HashCore(byte[] buffer, int start, int length) {
            hash = CalculateHash(table, hash, buffer, start, length);
        }

        protected override byte[] HashFinal() {
            byte[] hashBuffer = UInt32ToBigEndianBytes(~hash);
            this.HashValue = hashBuffer;
            return hashBuffer;
        }

        public override int HashSize {
            get { return 32; }
        }

        public static UInt32 Compute(byte[] buffer) {
            return ~CalculateHash(InitializeTable(DefaultPolynomial), DefaultSeed, buffer, 0, buffer.Length);
        }

        public static UInt32 Compute(UInt32 seed, byte[] buffer) {
            return ~CalculateHash(InitializeTable(DefaultPolynomial), seed, buffer, 0, buffer.Length);
        }

        public static UInt32 Compute(UInt32 polynomial, UInt32 seed, byte[] buffer) {
            return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
        }

        private static UInt32[] InitializeTable(UInt32 polynomial) {
            if (polynomial == DefaultPolynomial && defaultTable != null)
                return defaultTable;

            UInt32[] createTable = new UInt32[256];
            for (int i = 0; i < 256; i++) {
                UInt32 entry = (UInt32)i;
                for (int j = 0; j < 8; j++)
                    if ((entry & 1) == 1)
                        entry = (entry >> 1) ^ polynomial;
                    else
                        entry = entry >> 1;
                createTable[i] = entry;
            }

            if (polynomial == DefaultPolynomial)
                defaultTable = createTable;

            return createTable;
        }

        private static UInt32 CalculateHash(UInt32[] table, UInt32 seed, byte[] buffer, int start, int size) {
            UInt32 crc = seed;
            for (int i = start; i < size; i++)
                unchecked {
                    crc = (crc >> 8) ^ table[buffer[i] ^ crc & 0xff];
                }
            return crc;
        }

        private byte[] UInt32ToBigEndianBytes(UInt32 x) {
            return new byte[] {
			    (byte)((x >> 24) & 0xff),
			    (byte)((x >> 16) & 0xff),
			    (byte)((x >> 8) & 0xff),
			    (byte)(x & 0xff)
		    };
        }
    }

}