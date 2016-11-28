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
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Program=OblivionModManager.Program;

namespace ConflictDetector {
    [Serializable]
    public class HeaderInfo {
        public string Author;
        public string Description;
        public string DependsOn;
    }

    public struct EDID {
        public readonly string name;
        public readonly plugin plugin;
        public readonly string rectype;
        public readonly string uppername;

        public EDID(string Name, string Rectype, plugin Plugin) {
            name=Name.ToLower();
            uppername=Name;
            plugin=Plugin;
            rectype=Rectype;
        }
    }

    public static class TesFile {
        [System.Runtime.InteropServices.DllImport("NifScanner.dll", CharSet=CharSet.Ansi)]
        public static extern string GenList(byte[] data, int datasize);

        /*private static string ReadRecName(BinaryReader br) {
            byte[] b = new byte[4];
            br.Read(b, 0, 4);
            return ""+((char)b[0])+((char)b[1])+((char)b[2])+((char)b[3]);
        }
        private static string ReadCString(BinaryReader br) {
            string s="";
            byte b;
            while(true) {
                b=br.ReadByte();
                if(b==0) break;
                s+=(char)b;
            }
            return s;
        }*/

        public static void SetGMST(string file, string edid, string value)
        {
            if (Program.bSkyrimMode)
                SkyrimSetGMST(file,edid,value);
            else if (Program.bMorrowind)
            {
            }
            else
                OblivionSetGMST(file,edid,value);
        }
        public static void SkyrimSetGMST(string file, string edid, string value) {
            switch (edid.ToLower()[0])
            {
                case 'f':
                    float fparse;
                    if (!float.TryParse(value, out fparse))
                        throw new OblivionModManager.obmmException("Invalid value for float gmst");
                    break;
                case 'i':
                    int iparse;
                    if (!int.TryParse(value, out iparse))
                        throw new OblivionModManager.obmmException("Invalid value of integer gmst");
                    break;
                case 's':
                    break;
                default:
                    throw new OblivionModManager.obmmException("Unknown gmst type");
            }
            File.Delete(OblivionModManager.Program.TempDir + "tempesp");
            File.Move(file, OblivionModManager.Program.TempDir + "tempesp");
            BinaryReader br = new BinaryReader(File.OpenRead(OblivionModManager.Program.TempDir + "tempesp"), System.Text.Encoding.GetEncoding("ISO-8859-1"));
            BinaryWriter bw = null;
            try
            {
                string s;
                int size;
                long loc1 = 0;
                long loc2 = 0;
                int gsize = 0;
                s = OblivionModManager.Program.ReadBString(br, 4);
                if (s != "TES4")
                {
                    br.Close();
                    File.Move(OblivionModManager.Program.TempDir + "tempesp", file);
                    throw new OblivionModManager.obmmException("file was not a valid tes4 plugin");
                }
                bw = new BinaryWriter(File.Create(file));
                size = br.ReadInt32();
                br.BaseStream.Position = 0;
                bw.Write(br.ReadBytes(size + 24));
                while (br.PeekChar() != -1)
                {
                    s = OblivionModManager.Program.ReadBString(br, 4);
                    if (s == "GRUP")
                    {
                        loc1 = 0;
                        size = br.ReadInt32();
                        gsize = size;
                        s = OblivionModManager.Program.ReadBString(br, 4);
                        br.BaseStream.Position -= 12;
                        if (s == "GMST")
                        {
                            loc1 = br.BaseStream.Position + 4;
                            bw.Write(br.ReadBytes(20));
                        }
                        else
                        {
                            bw.Write(br.ReadBytes(size));
                        }
                        continue;
                    }
                    if (s == "GMST")
                    {
                        loc2 = br.BaseStream.Position;
                        size = br.ReadInt32();
                        br.BaseStream.Position -= 8;
                        bw.Write(br.ReadBytes(20));
                        s = OblivionModManager.Program.ReadBString(br, 4);
                        if (s != "EDID")
                        {
                            br.BaseStream.Position -= 4;
                            bw.Write(br.ReadBytes(size));
                            continue;
                        }
                        br.BaseStream.Position += 2;
                        s = OblivionModManager.Program.ReadCString(br).ToLower();
                        if (s != edid)
                        {
                            br.BaseStream.Position = loc2 + 16;
                            bw.Write(br.ReadBytes(size));
                            continue;
                        }
                        long loc3;
                        switch (s[0])
                        {
                            case 'f':
                                loc3 = br.BaseStream.Position + 6;
                                br.BaseStream.Position = loc2 + 16;
                                bw.Write(br.ReadBytes(size));
                                bw.Seek((int)loc3, SeekOrigin.Begin);
                                bw.Write(float.Parse(value));
                                bw.Seek(0, SeekOrigin.End);
                                break;
                            case 'i':
                                loc3 = br.BaseStream.Position + 6;
                                br.BaseStream.Position = loc2 + 16;
                                bw.Write(br.ReadBytes(size));
                                bw.Seek((int)loc3, SeekOrigin.Begin);
                                bw.Write(int.Parse(value));
                                bw.Seek(0, SeekOrigin.End);
                                break;
                            case 's':
                                loc3 = br.BaseStream.Position + 6;
                                long savedpos = loc2 + 16 + size;
                                br.BaseStream.Position = loc2 + 16;
                                bw.Write(br.ReadBytes((int)(loc3 - (loc2 + 18))));
                                bw.Write((short)(value.Length + 1));
                                foreach (char c in value) bw.Write((byte)c);
                                bw.Write((byte)0);
                                bw.Seek((int)loc2, SeekOrigin.Begin);
                                int size2 = value.Length + edid.Length + 14;
                                bw.Write(size2);
                                if (loc1 != 0)
                                {
                                    bw.Seek((int)loc1, SeekOrigin.Begin);
                                    bw.Write(gsize - (size - size2));
                                }
                                bw.Seek(0, SeekOrigin.End);
                                br.BaseStream.Position = savedpos;
                                break;
                        }
                        bw.Write(br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position)));
                    }
                    else
                    {
                        size = br.ReadInt32();
                        br.BaseStream.Position -= 8;
                        bw.Write(br.ReadBytes(size + 20));
                    }
                }
            }
            finally
            {
                if (br != null) br.Close();
                if (bw != null) bw.Close();
            }
        }
        public static void OblivionSetGMST(string file, string edid, string value)
        {
            switch(edid.ToLower()[0]) {
            case 'f':
                float fparse;
                if(!float.TryParse(value, out fparse))
                    throw new OblivionModManager.obmmException("Invalid value for float gmst");
                break;
            case 'i':
                int iparse;
                if(!int.TryParse(value, out iparse))
                    throw new OblivionModManager.obmmException("Invalid value of integer gmst");
                break;
            case 's':
                break;
            default:
                throw new OblivionModManager.obmmException("Unknown gmst type");
            }
            File.Delete(OblivionModManager.Program.TempDir+"tempesp");
            File.Move(file, OblivionModManager.Program.TempDir+"tempesp");
            BinaryReader br = new BinaryReader(File.OpenRead(OblivionModManager.Program.TempDir + "tempesp"), System.Text.Encoding.GetEncoding("ISO-8859-1"));
            BinaryWriter bw=null;
            try {
                string s;
                int size;
                long loc1=0;
                long loc2=0;
                int gsize=0;
                s=OblivionModManager.Program.ReadBString(br,4);
                if(s!="TES4") {
                    br.Close();
                    File.Move(OblivionModManager.Program.TempDir+"tempesp",file);
                    throw new OblivionModManager.obmmException("file was not a valid tes4 plugin");
                }
                bw=new BinaryWriter(File.Create(file));
                size=br.ReadInt32();
                br.BaseStream.Position=0;
                bw.Write(br.ReadBytes(size+20));
                while(br.PeekChar()!=-1) {
                    s=OblivionModManager.Program.ReadBString(br, 4);
                    if(s=="GRUP") {
                        loc1=0;
                        size=br.ReadInt32();
                        gsize=size;
                        s=OblivionModManager.Program.ReadBString(br, 4);
                        br.BaseStream.Position-=12;
                        if(s=="GMST") {
                            loc1=br.BaseStream.Position+4;
                            bw.Write(br.ReadBytes(20));
                        } else {
                            bw.Write(br.ReadBytes(size));
                        }
                        continue;
                    }
                    if(s=="GMST") {
                        loc2=br.BaseStream.Position;
                        size=br.ReadInt32();
                        br.BaseStream.Position-=8;
                        bw.Write(br.ReadBytes(20));
                        s=OblivionModManager.Program.ReadBString(br, 4);
                        if(s!="EDID") {
                            br.BaseStream.Position-=4;
                            bw.Write(br.ReadBytes(size));
                            continue;
                        }
                        br.BaseStream.Position+=2;
                        s=OblivionModManager.Program.ReadCString(br).ToLower();
                        if(s!=edid) {
                            br.BaseStream.Position=loc2+16;
                            bw.Write(br.ReadBytes(size));
                            continue;
                        }
                        long loc3;
                        switch(s[0]) {
                        case 'f':
                            loc3=br.BaseStream.Position+6;
                            br.BaseStream.Position=loc2+16;
                            bw.Write(br.ReadBytes(size));
                            bw.Seek((int)loc3, SeekOrigin.Begin);
                            bw.Write(float.Parse(value));
                            bw.Seek(0, SeekOrigin.End);
                            break;
                        case 'i':
                            loc3=br.BaseStream.Position+6;
                            br.BaseStream.Position=loc2+16;
                            bw.Write(br.ReadBytes(size));
                            bw.Seek((int)loc3, SeekOrigin.Begin);
                            bw.Write(int.Parse(value));
                            bw.Seek(0, SeekOrigin.End);
                            break;
                        case 's':
                            loc3=br.BaseStream.Position+6;
                            long savedpos=loc2+16+size;
                            br.BaseStream.Position=loc2+16;
                            bw.Write(br.ReadBytes((int)(loc3-(loc2+18))));
                            bw.Write((short)(value.Length+1));
                            foreach(char c in value) bw.Write((byte)c);
                            bw.Write((byte)0);
                            bw.Seek((int)loc2, SeekOrigin.Begin);
                            int size2=value.Length+edid.Length+14;
                            bw.Write(size2);
                            if(loc1!=0) {
                                bw.Seek((int)loc1, SeekOrigin.Begin);
                                bw.Write(gsize-(size-size2));
                            }
                            bw.Seek(0, SeekOrigin.End);
                            br.BaseStream.Position=savedpos;
                            break;
                        }
                        bw.Write(br.ReadBytes((int)(br.BaseStream.Length-br.BaseStream.Position)));
                    } else {
                        size=br.ReadInt32();
                        br.BaseStream.Position-=8;
                        bw.Write(br.ReadBytes(size+20));
                    }
                }
            } finally {
                if(br!=null) br.Close();
                if(bw!=null) bw.Close();
            }
        }
        public static void SetGLOB(string file, string edid, string value)
        {
            if (Program.bSkyrimMode)
                SkyrimSetGLOB(file,edid,value);
            else if (Program.bMorrowind)
            {
            }
            else
                OblivionSetGLOB(file,edid,value);
        }
        public static void SkyrimSetGLOB(string file, string edid, string value) {
            long offset = -1;
            char type = '\0';
            float f = 0;
            int l = 0;
            short s = 0;
            string str;
            int size;

            BinaryReader br = new BinaryReader(File.OpenRead(file), System.Text.Encoding.GetEncoding("ISO-8859-1"));
            try
            {
                str = OblivionModManager.Program.ReadBString(br, 4);
                if (str != "TES4")
                    throw new OblivionModManager.obmmException("file was not a valid tes4 plugin");
                size = br.ReadInt32();
                while (br.PeekChar() != -1 && br.PeekChar() != 'H') br.BaseStream.Position++; // look for HEDR

                // found HEDR
                str = Program.ReadBString(br, 4);
                // skip some constant crap 0C 00 00 00 80 3f for Oblivion  0C 00 D7 A3 70 3F for Skyrim
                br.BaseStream.Position += 6;

                Int32 numrecords = br.ReadInt32();
                ulong nextObjID = br.ReadUInt32();

                while (br.PeekChar() != -1)
                {
                    str = OblivionModManager.Program.ReadBString(br, 4);
                    if (str == "GRUP")
                    {
                        size = br.ReadInt32();
                        str = OblivionModManager.Program.ReadBString(br, 4);
                        if (str != "GLOB")
                        {
                            br.BaseStream.Position += size - 12;
                        }
                        else
                        {
                            br.BaseStream.Position += 8;
                        }
                        continue;
                    }
                    if (str == "GLOB")
                    {
                        size = br.ReadInt32();
                        br.BaseStream.Position += 12;
                        long nextrecord = br.BaseStream.Position + size;
                        if (OblivionModManager.Program.ReadBString(br, 4) != "EDID")
                        {
                            br.BaseStream.Position = nextrecord;
                            continue;
                        }
                        br.ReadInt16();
                        str = OblivionModManager.Program.ReadCString(br).ToLower();
                        if (str != edid)
                        {
                            br.BaseStream.Position = nextrecord;
                            continue;
                        }
                        if (OblivionModManager.Program.ReadBString(br, 4) != "FNAM")
                        {
                            br.BaseStream.Position = nextrecord;
                            continue;
                        }
                        br.ReadInt16();
                        type = (char)br.ReadByte();
                        switch (type)
                        {
                            case 'f':
                                if (!float.TryParse(value, out f))
                                    throw new OblivionModManager.obmmException("New global value was not in the required format");
                                break;
                            case 'l':
                                if (!int.TryParse(value, out l))
                                    throw new OblivionModManager.obmmException("New global value was not in the required format");
                                break;
                            case 's':
                                if (!short.TryParse(value, out s))
                                    throw new OblivionModManager.obmmException("New global value was not in the required format");
                                break;
                        }
                        if (OblivionModManager.Program.ReadBString(br, 4) != "FLTV")
                        {
                            br.BaseStream.Position = nextrecord;
                            continue;
                        }
                        br.ReadInt16();
                        offset = br.BaseStream.Position;
                        break;
                    }
                }
            }
            finally
            {
                br.Close();
            }

            if (offset != -1)
            {
                BinaryWriter bw = new BinaryWriter(File.Open(file, FileMode.Open, FileAccess.Write));
                bw.Seek((int)offset, SeekOrigin.Begin);
                switch (type)
                {
                    case 'f':
                        bw.Write(f);
                        break;
                    case 'l':
                        bw.Write((float)l);
                        break;
                    case 's':
                        bw.Write((float)s);
                        break;
                }
                bw.Close();
            }
            else
            {
                throw new OblivionModManager.obmmException("The specified global EDID was not found");
            }
        }
        public static void OblivionSetGLOB(string file, string edid, string value)
        {
            long offset=-1;
            char type='\0';
            float f=0;
            int l=0;
            short s=0;
            string str;
            int size;

            BinaryReader br = new BinaryReader(File.OpenRead(file), System.Text.Encoding.GetEncoding("ISO-8859-1"));
            try {
                str=OblivionModManager.Program.ReadBString(br, 4);
                if(str!="TES4")
                    throw new OblivionModManager.obmmException("file was not a valid tes4 plugin");
                size=br.ReadInt32();
                br.BaseStream.Position+=size+12;

                while(br.PeekChar()!=-1) {
                    str=OblivionModManager.Program.ReadBString(br, 4);
                    if(str=="GRUP") {
                        size=br.ReadInt32();
                        str=OblivionModManager.Program.ReadBString(br, 4);
                        if(str!="GLOB") {
                            br.BaseStream.Position+=size-12;
                        } else {
                            br.BaseStream.Position+=8;
                        }
                        continue;
                    }
                    if(str=="GLOB") {
                        size=br.ReadInt32();
                        br.BaseStream.Position+=12;
                        long nextrecord=br.BaseStream.Position+size;
                        if(OblivionModManager.Program.ReadBString(br,4)!="EDID") {
                            br.BaseStream.Position=nextrecord;
                            continue;
                        }
                        br.ReadInt16();
                        str=OblivionModManager.Program.ReadCString(br).ToLower();
                        if(str!=edid) {
                            br.BaseStream.Position=nextrecord;
                            continue;
                        }
                        if(OblivionModManager.Program.ReadBString(br,4)!="FNAM") {
                            br.BaseStream.Position=nextrecord;
                            continue;
                        }
                        br.ReadInt16();
                        type=(char)br.ReadByte();
                        switch(type) {
                        case 'f':
                            if(!float.TryParse(value, out f))
                                throw new OblivionModManager.obmmException("New global value was not in the required format");
                            break;
                        case 'l':
                            if(!int.TryParse(value, out l))
                                throw new OblivionModManager.obmmException("New global value was not in the required format");
                            break;
                        case 's':
                            if(!short.TryParse(value, out s))
                                throw new OblivionModManager.obmmException("New global value was not in the required format");
                            break;
                        }
                        if(OblivionModManager.Program.ReadBString(br,4)!="FLTV") {
                            br.BaseStream.Position=nextrecord;
                            continue;
                        }
                        br.ReadInt16();
                        offset=br.BaseStream.Position;
                        break;
                    }
                }
            } finally {
                br.Close();
            }

            if(offset!=-1) {
                BinaryWriter bw=new BinaryWriter(File.Open(file, FileMode.Open, FileAccess.Write));
                bw.Seek((int)offset, SeekOrigin.Begin);
                switch(type) {
                case 'f':
                    bw.Write(f);
                    break;
                case 'l':
                    bw.Write((float)l);
                    break;
                case 's':
                    bw.Write((float)s);
                    break;
                }
                bw.Close();
            } else {
                throw new OblivionModManager.obmmException("The specified global EDID was not found");
            }
        }
        public static HeaderInfo GetHeader(string FilePath) {
            if (Program.bSkyrimMode)
                return SkyrimGetHeader(FilePath);
            else if (Program.bMorrowind)
            {
                return MorrowindGetHeader(FilePath);
            }
            else
                return OblivionGetHeader(FilePath);
        }
        public static HeaderInfo SkyrimGetHeader(string FilePath) {
            BinaryReader br = new BinaryReader(File.OpenRead(FilePath), System.Text.Encoding.GetEncoding("ISO-8859-1"));
            HeaderInfo hi = new HeaderInfo();
            try
            {
                string s;
                uint size;
                s = OblivionModManager.Program.ReadBString(br, 4);
                if (s != "TES4") return hi;
                size = br.ReadUInt32();
                while (br.PeekChar() != -1 && br.PeekChar() != 'H') br.BaseStream.Position++; // look for HEDR

                // found HEDR
                s = Program.ReadBString(br, 4);
                // skip some constant crap 0C 00 00 00 80 3f for Oblivion  0C 00 D7 A3 70 3F for Skyrim
                br.BaseStream.Position += 6;

                Int32 numrecords = br.ReadInt32();
                ulong nextObjID = br.ReadUInt32();
                while (br.PeekChar()!=-1 && s!="INTV")
                {
                    s = Program.ReadBString(br, 4);
                    ushort subsize = br.ReadUInt16();
                    if (s == "CNAM")
                        hi.Author = Program.ReadCString(br);
                    else if (s == "SNAM")
                        hi.Description = Program.ReadCString(br);
                    else if (s == "MAST")
                        hi.DependsOn = (hi.DependsOn!=null && hi.DependsOn.Length > 0 ? hi.DependsOn + "," : "") + Program.ReadCString(br);
                    else br.BaseStream.Position += subsize;
                }
            }
            finally
            {
                br.Close();
            }
            return hi;
        }
        public static HeaderInfo MorrowindGetHeader(string FilePath)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(FilePath), System.Text.Encoding.GetEncoding("ISO-8859-1"));
            HeaderInfo hi = new HeaderInfo();
            try
            {
                string s;
                uint size;
                s = OblivionModManager.Program.ReadBString(br, 4);
                if (s != "TES3") return hi;
                size = br.ReadUInt32();
                br.BaseStream.Position += 8;
                s = OblivionModManager.Program.ReadBString(br, 4); // should be HEDR
                size = br.ReadUInt32();
                uint fversion = br.ReadUInt32(); // supposed to be a 4bytes float
                uint fournull = br.ReadUInt32(); 
                hi.Author = Program.ReadBString(br,32);
                hi.Author=hi.Author.Substring(0,hi.Author.IndexOf('\0'));
                hi.Description = Program.ReadBString(br, 260);
                hi.Description = hi.Description.Substring(0, hi.Description.IndexOf('\0'));
                s = OblivionModManager.Program.ReadBString(br, 4); // should be MAST
                while (s == "MAST")
                {
                    size = br.ReadUInt32(); // master file name length
                    hi.DependsOn = (hi.DependsOn != null && hi.DependsOn.Length > 0 ? hi.DependsOn + "," : "") + Program.ReadBString(br, (int)size-1);
                    Program.ReadBString(br, 1); // read the terminating NULL character
                    s = OblivionModManager.Program.ReadBString(br, 4); // should be DATA
                    size = br.ReadUInt32(); // Data length
                    double data = br.ReadDouble(); // data itself
                    s = OblivionModManager.Program.ReadBString(br, 4); // should be DATA
                }
            }
            finally
            {
                br.Close();
            }
            return hi;
        }
        public static HeaderInfo OblivionGetHeader(string FilePath)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(FilePath), System.Text.Encoding.GetEncoding("ISO-8859-1"));
            HeaderInfo hi=new HeaderInfo();
            try {
                string s;
                uint size;
                s=OblivionModManager.Program.ReadBString(br, 4);
                if(s!="TES4") return hi;
                size=br.ReadUInt32();
                br.BaseStream.Position+=12;
                uint read = 0;
                while(read<size) {
                    s=OblivionModManager.Program.ReadBString(br, 4);
                    ushort subsize=br.ReadUInt16();
                    if(s=="CNAM") hi.Author=Program.ReadCString(br);
                    else if(s=="SNAM") hi.Description=Program.ReadCString(br);
                    else if (s == "MAST")
                        hi.DependsOn = (hi.DependsOn != null && hi.DependsOn.Length > 0 ? hi.DependsOn + "," : "") + Program.ReadCString(br);
                    else br.BaseStream.Position += subsize;
                    read+=(uint)(subsize+6);
                }
            } finally {
                br.Close();
            }
            return hi;
        }
        public static EDID[] GetIDList(string FilePath, plugin p)
        {
            return (Program.bSkyrimMode ? SkyrimGetIDList(FilePath, p) : Program.bMorrowind? null: OblivionGetIDList(FilePath, p));
        }
        public static EDID[] OblivionGetIDList(string FilePath, plugin p) {
            List<EDID> ids=new List<EDID>();
            BinaryReader br = new BinaryReader(File.OpenRead(FilePath), System.Text.Encoding.GetEncoding("ISO-8859-1"));
            try {
                string s;
                uint size;
                s=Program.ReadBString(br, 4);
                if(s!="TES4") return new EDID[0];
                size=br.ReadUInt32();
                br.BaseStream.Position+=size+12;
                while(br.PeekChar()!=-1) {
                    s=Program.ReadBString(br, 4);
                    size = br.ReadUInt32();
                    uint comp=br.ReadUInt32();
                    br.BaseStream.Position+=8;
                    if(s=="GRUP") continue;
                    if((comp&0x00040000)>0) {
                        ICSharpCode.SharpZipLib.Zip.Compression.Inflater inf;
                        inf=new ICSharpCode.SharpZipLib.Zip.Compression.Inflater();
                        byte[] In=new byte[size-4];
                        byte[] Out=new byte[br.ReadUInt32()];
                        br.Read(In, 0, (int)(size-4));
                        inf.SetInput(In);
                        inf.Inflate(Out);
                        if((byte)Out[0]=='E'&&(byte)Out[1]=='D'&&(byte)Out[2]=='I'&&(byte)Out[3]=='D') {
                            string s2="";
                            int i=6;
                            while(Out[i]!=0) {
                                s2+=(char)Out[i++];
                            }
                            ids.Add(new EDID(s2, s, p));
                        }
                    } else {
                        string s2=Program.ReadBString(br, 4);
                        if(s2=="EDID") {
                            s2="";
                            byte b;
                            ushort size2=br.ReadUInt16();
                            while((b=br.ReadByte())!=0) {
                                s2+=(char)b;
                            }
                            br.BaseStream.Position+=size-(6+size2);
                            ids.Add(new EDID(s2, s, p));
                        } else {
                            br.BaseStream.Position+=size-4;
                        }
                    }

                }
            } finally {
                br.Close();
            }
            return ids.ToArray();
        }

        public static EDID[] SkyrimGetIDList(string FilePath, plugin p)
        {
            List<EDID> ids = new List<EDID>();
            BinaryReader br = new BinaryReader(File.OpenRead(FilePath), System.Text.Encoding.GetEncoding("ISO-8859-1"));
            bool bInGroup = false;
            try
            {
                string s;
                uint size;
                s = Program.ReadBString(br, 4);
                if (s != "TES4") return new EDID[0];
                size = br.ReadUInt32();
                while (br.PeekChar() != -1 && br.PeekChar() != 'H') br.BaseStream.Position++; // look for HEDR

                // found HEDR
                s = Program.ReadBString(br, 4);
                // skip some constant crap 0C 00 00 00 80 3f for Oblivion  0C 00 D7 A3 70 3F for Skyrim
                br.BaseStream.Position += 6;

                //                double version = br.ReadDouble(); // nope
                Int32 numrecords = br.ReadInt32();
                ulong nextObjID = br.ReadUInt32();


                while (br.PeekChar() != -1)
                {
                    s = Program.ReadBString(br, 4);
                    if (s == "GRUP")
                    {
                        // skip the group header
                        size = br.ReadUInt32(); // size
                        Program.ReadBString(br, 4); // group label
                        br.ReadInt32(); // group type
                        br.ReadUInt16(); // stamp
                        br.ReadUInt16(); // unknown
                        br.ReadUInt16(); // version
                        br.ReadUInt16(); // unknown
                        bInGroup = true;
                        continue;
                    }
                    else if (!bInGroup)
                    {
                        size = br.ReadUInt16();
                    }
                    else
                    {
                        size = br.ReadUInt32();
                        br.ReadUInt32(); // flags
                        br.ReadUInt32(); // ID
                        br.ReadUInt32(); // revision
                        br.ReadUInt16(); // version
                        br.ReadUInt16(); // unknown
                    }
                    if (s == "EDID")
                    {
                        string s2 = "";
                        s2 = Program.ReadBString(br, (int)size);
                        ids.Add(new EDID(s2, s, p));
                    }
                    else
                    {
                        br.BaseStream.Position += size;
                    }

                }
            }
            finally
            {
                br.Close();
            }
            return ids.ToArray();
        }

        private static string[] OblivionGetDataFileList(byte[] record, string rtype)
        {
            string pICON;
            if(rtype=="LTEX") pICON="textures\\landscape\\"; else pICON="textures\\";
            
            List<string> ids=new List<string>();
            MemoryStream ms=new MemoryStream(record);
            BinaryReader br = new BinaryReader(ms, System.Text.Encoding.GetEncoding("ISO-8859-1"));
            int size=record.Length;
            int read=0;
            while(read<size) {
                string s=Program.ReadBString(br, 4);
                ushort size2=br.ReadUInt16();
                read+=size2+6;
                if(size2==0) continue;
                long newpos=br.BaseStream.Position+size2;
                char[] ch=new char[size2];
                br.Read(ch, 0, size2);
                string s2=new string(ch);
                if(s2[s2.Length-1]=='\0') s2=s2.Remove(s2.Length-1);
                /*string s2="";
                byte b;
                while((b=br.ReadByte())!=0) s2+=(char)b;*/
                switch(s) {
                case "MODL":    //Nif file
                case "MOD2":
                case "MOD3":
                case "MOD4":
                    //if(!File.Exists(Program.DataFolderName+"\\meshes\\"+s2)) break;
                    ids.Add("meshes\\"+s2.ToLower());
                        try {
                            string texlist="meshes\\"+s2;
                            byte[] data=OblivionModManager.Classes.BSAArchive.GetMesh(texlist);
                            if(data!=null) texlist=GenList(data, data.Length);
                            else texlist=null;
                            if(texlist!=null&&texlist!="") {
                                string[] textures=texlist.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                                for(int i=0;i<textures.Length;i++) {
                                    string tex=textures[i].ToLower();
                                    ids.Add(tex);
                                    string tex2=Path.GetDirectoryName(tex)+"\\"+Path.GetFileNameWithoutExtension(tex)+"_n"+Path.GetExtension(tex);
                                    if(OblivionModManager.Classes.BSAArchive.CheckForTexture(tex2)) ids.Add(tex2);
                                    tex2=Path.GetDirectoryName(tex)+"\\"+Path.GetFileNameWithoutExtension(tex)+"_g"+Path.GetExtension(tex);
                                    if(OblivionModManager.Classes.BSAArchive.CheckForTexture(tex2)) ids.Add(tex2);
                                }
                            }
                        } catch { }       
                    break;
                case "ICON":    //Texture
                case "ICO2":
                    ids.Add(pICON+s2.ToLower());
                    break;
                case "DESC":    //Book contents
                    if(s2.Length==0||s2[0]!='<') break;
                    try {
                        s2=s2.ToLower();
                        int upto=0;
                        while(true) {
                            int index=s2.IndexOf("<img", upto);
                            if(index==-1) break;
                            index=s2.IndexOf("src=", index);
                            if(index==-1) break;

                            index+=4;
                            char end;
                            if(s2[index]=='"') end='"'; else end=' ';
                            int index2=s2.IndexOf(end);
                            if(index2==-1) break;
                            ids.Add("textures\\menus\\"+s2.Substring(index, index2-index).ToLower());
                            ids.Add("textures\\menus80\\"+s2.Substring(index, index2-index).ToLower());
                            ids.Add("textures\\menus50\\"+s2.Substring(index, index2-index).ToLower());
                        }
                    } catch { }
                    break;
                case "SCTX":    //Script text
                    string[] lines=s2.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    for(int i=0;i<lines.Length;i++) {
                        string line=lines[i];
                        int comment=line.IndexOf(';');
                        if(comment!=-1) line=line.Remove(comment);
                        line=line.Replace("\r","").Trim().ToLower();
                        if(line.StartsWith("streammusic")) {
                            string[] file=line.Split('"');
                            if(file.Length>1&&file[1].ToLower().StartsWith(Program.DataFolderPath+"\\")) ids.Add(file[1].ToLower().Substring(5));
                            //TODO: get glowmaps/normal maps
                        } else if(line.StartsWith("playbink")) {
                            string[] file=line.Split('"');
                            if(file.Length>1) ids.Add("video\\"+file[1].ToLower());
                        }
                    }
                    break;
                case "FNAM":
                    switch(rtype) {
                    case "SOUN":
                        ids.Add("sounds\\"+s2.ToLower());
                        break;
                    case "CLMT":
                        ids.Add("textures\\"+s2.ToLower());
                        break;
                    }
                    break;
                case "CNAME":
                case "DNAME":
                    if(rtype=="WTHR") ids.Add("textures\\"+s2.ToLower());
                    break;
                case "INAME":
                    if(rtype=="FACT") ids.Add("textures\\"+s2.ToLower());
                    break;
                case "GNAM":
                    if(rtype=="CLMT") ids.Add("textures\\"+s2.ToLower());
                    break;
                }
                br.BaseStream.Position=newpos;
            }
            ms.Close();
            return ids.ToArray();
        }
        private static string[] SkyrimGetDataFileList(byte[] record, string rtype)
        {
            string pICON;
            if (rtype == "LTEX") pICON = "textures\\landscape\\"; else pICON = "textures\\";

            List<string> ids = new List<string>();
            MemoryStream ms = new MemoryStream(record);
            BinaryReader br = new BinaryReader(ms, System.Text.Encoding.GetEncoding("ISO-8859-1"));
            int size = record.Length;
            int size2 = 0;
            long offset = br.BaseStream.Position;
            string s = "";
            string s2 = "";
            while (br.BaseStream.Position - offset < size)
            {
                s = Program.ReadBString(br, 4);
                Console.WriteLine("Found "+s);
                //ushort size2 = br.ReadUInt16();
                //read += size2 + 6;
                //if (size2 == 0) continue;
                //long newpos = br.BaseStream.Position + size2;
                //char[] ch = new char[size2];
                //br.Read(ch, 0, size2);
                //string s2 = new string(ch);
                //if (s2[s2.Length - 1] == '\0') s2 = s2.Remove(s2.Length - 1);
                /*string s2="";
                byte b;
                while((b=br.ReadByte())!=0) s2+=(char)b;*/
                size2 = br.ReadUInt16();
                switch (s)
                {
                    case "MODL":    //Model???
                        uint modl = br.ReadUInt32(); // UINT32 usually...
                        Console.WriteLine("=>" + modl);
                        break;
                    case "MOD2":
                    case "MOD3":
                    case "MOD4":
                        s2 = Program.ReadBString(br, size2).Replace("\0", "");
                        Console.WriteLine("=>"+s2);
                        //if(!File.Exists(Program.DataFolderName+"\\meshes\\"+s2)) break;
                        ids.Add("meshes\\" + s2.ToLower());
                        try
                        {
                            string texlist = "meshes\\" + s2.Replace("\0", ""); ;
                            byte[] data = OblivionModManager.Classes.BSAArchive.GetMesh(texlist);
                            if (data != null) texlist = GenList(data, data.Length);
                            else texlist = null;
                            if (texlist != null && texlist != "")
                            {
                                string[] textures = texlist.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int i = 0; i < textures.Length; i++)
                                {
                                    string tex = textures[i].ToLower();
                                    ids.Add(tex);
                                    string tex2 = Path.GetDirectoryName(tex) + "\\" + Path.GetFileNameWithoutExtension(tex) + "_n" + Path.GetExtension(tex);
                                    if (OblivionModManager.Classes.BSAArchive.CheckForTexture(tex2)) ids.Add(tex2);
                                    tex2 = Path.GetDirectoryName(tex) + "\\" + Path.GetFileNameWithoutExtension(tex) + "_g" + Path.GetExtension(tex);
                                    if (OblivionModManager.Classes.BSAArchive.CheckForTexture(tex2)) ids.Add(tex2);
                                }
                            }
                        }
                        catch { }
                        break;
                    case "ICON":    //Texture
                    case "ICO2":
                        s2 = Program.ReadBString(br, size2).Replace("\0", "");
                        Console.WriteLine("=>"+s2);
                        ids.Add(pICON + s2.ToLower());
                        break;
                    case "DESC":    //Book contents
                        s2 = Program.ReadBString(br, size2).Replace("\0", "");
                        Console.WriteLine("=>"+s2);
                        if (s2.Length == 0 || s2[0] != '<') break;
                        try
                        {
                            s2 = s2.ToLower();
                            int upto = 0;
                            while (true)
                            {
                                int index = s2.IndexOf("<img", upto);
                                if (index == -1) break;
                                index = s2.IndexOf("src=", index);
                                if (index == -1) break;

                                index += 4;
                                char end;
                                if (s2[index] == '"') end = '"'; else end = ' ';
                                int index2 = s2.IndexOf(end);
                                if (index2 == -1) break;
                                ids.Add("textures\\menus\\" + s2.Substring(index, index2 - index).ToLower());
                                ids.Add("textures\\menus80\\" + s2.Substring(index, index2 - index).ToLower());
                                ids.Add("textures\\menus50\\" + s2.Substring(index, index2 - index).ToLower());
                            }
                        }
                        catch { }
                        break;
                    case "SCTX":    //Script text
                        s2 = Program.ReadBString(br, size2).Replace("\0", "");
                        Console.WriteLine("=>"+s2);
                        string[] lines = s2.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < lines.Length; i++)
                        {
                            string line = lines[i];
                            int comment = line.IndexOf(';');
                            if (comment != -1) line = line.Remove(comment);
                            line = line.Replace("\r", "").Trim().ToLower();
                            if (line.StartsWith("streammusic"))
                            {
                                string[] file = line.Split('"');
                                if (file.Length > 1 && file[1].ToLower().StartsWith(Program.DataFolderPath+"\\")) ids.Add(file[1].ToLower().Substring(5));
                                //TODO: get glowmaps/normal maps
                            }
                            else if (line.StartsWith("playbink"))
                            {
                                string[] file = line.Split('"');
                                if (file.Length > 1) ids.Add("video\\" + file[1].ToLower());
                            }
                        }
                        break;
                    case "FNAM":
                        s2 = Program.ReadBString(br, size2).Replace("\0", "");
                        Console.WriteLine("=>"+s2);
                        switch (rtype)
                        {
                            case "SOUN":
                                ids.Add("sounds\\" + s2.ToLower());
                                break;
                            case "CLMT":
                                ids.Add("textures\\" + s2.ToLower());
                                break;
                        }
                        break;
                    case "CNAME":
                    case "DNAME":
                        s2 = Program.ReadBString(br, size2).Replace("\0", "");
                        Console.WriteLine("=>"+s2);
                        if (rtype == "WTHR") ids.Add("textures\\" + s2.ToLower());
                        break;
                    case "INAME":
                        s2 = Program.ReadBString(br, size2).Replace("\0", "");
                        Console.WriteLine("=>"+s2);
                        if (rtype == "FACT") ids.Add("textures\\" + s2.ToLower());
                        break;
                    case "GNAM":
                        s2 = Program.ReadBString(br, size2).Replace("\0", "");
                        Console.WriteLine("=>"+s2);
                        if (rtype == "CLMT") ids.Add("textures\\" + s2.ToLower());
                        break;
                    default:
                        Program.ReadBString(br, size2);
                        break;
                }
            }
            ms.Close();
            return ids.ToArray();
        }
        public static string[] GetDataFileList(string FilePath)
        {
            return (Program.bSkyrimMode ? SkyrimGetDataFileList(FilePath) : Program.bMorrowind?null:OblivionGetDataFileList(FilePath));
        }

        public static string[] OblivionGetDataFileList(string FilePath)
        {
            List<string> ids = new List<string>();
            BinaryReader br = new BinaryReader(File.OpenRead(FilePath), System.Text.Encoding.GetEncoding("ISO-8859-1"));
            try {
                string s;
                uint size;
                s=Program.ReadBString(br, 4);
                if(s!="TES4") return new string[0];
                size=br.ReadUInt32();
                br.BaseStream.Position+=size+12;
                while(br.PeekChar()!=-1) {
                    s=Program.ReadBString(br, 4);
                    size=br.ReadUInt32();

                    uint comp=br.ReadUInt32();
                    br.BaseStream.Position+=8;
                    if(s=="GRUP") continue;
                    if((comp&0x00040000)>0) {
                        ICSharpCode.SharpZipLib.Zip.Compression.Inflater inf;
                        inf=new ICSharpCode.SharpZipLib.Zip.Compression.Inflater();
                        byte[] In=new byte[size-4];
                        byte[] Out=new byte[br.ReadUInt32()];
                        br.Read(In, 0, (int)(size-4));
                        inf.SetInput(In);
                        inf.Inflate(Out);
                        try {
                            ids.AddRange(OblivionGetDataFileList(Out,s));
                        } catch { }
                    } else {
                        try {
                            ids.AddRange(OblivionGetDataFileList(br.ReadBytes((int)size),s));
                        } catch { }
                    }
                }
            } finally {
                br.Close();
            }
            OblivionModManager.Classes.BSAArchive.Clear();
            return ids.ToArray();
        }

        public static string[] SkyrimGetDataFileList(string FilePath)
        {
            List<string> ids = new List<string>();
            BinaryReader br = new BinaryReader(File.OpenRead(FilePath), System.Text.Encoding.GetEncoding("ISO-8859-1"));
            bool bInGroup = false;
            try
            {
                string s;
                uint size;
                s = Program.ReadBString(br, 4);
                if (s != "TES4") return new string[0];
                size = br.ReadUInt32();
                while (br.PeekChar() != -1 && br.PeekChar() != 'H') br.BaseStream.Position++; // look for HEDR
                // found HEDR
                s = Program.ReadBString(br, 4);
                // skip some constant crap 0C 00 00 00 80 3f for Oblivion  0C 00 D7 A3 70 3F for Skyrim
                br.BaseStream.Position += 6;
                Int32 numrecords = br.ReadInt32();
                ulong nextObjID = br.ReadUInt32();
                uint flags = 0;

                while (br.PeekChar() != -1)
                {
                    s = Program.ReadBString(br, 4);
                    Console.WriteLine("Found "+s);
                    if (s == "GRUP")
                    {
                        // skip the group header
                        size = br.ReadUInt32(); // size
                        Program.ReadBString(br, 4); // group label
                        br.ReadInt32(); // group type
                        br.ReadUInt16(); // stamp
                        br.ReadUInt16(); // unknown
                        br.ReadUInt16(); // version
                        br.ReadUInt16(); // unknown
                        bInGroup = true;
                        continue;
                    }
                    else if (!bInGroup)
                    {
                        size = br.ReadUInt16();
                        // skip it
                        Program.ReadBString(br, (int)size);
                    }
                    else
                    {
                        size = br.ReadUInt32();
                        flags=br.ReadUInt32(); // flags
                        br.ReadUInt32(); // ID
                        br.ReadUInt32(); // revision
                        br.ReadUInt16(); // version
                        br.ReadUInt16(); // unknown
                        if ((flags & 0x00040000) > 0)
                        {
                            ICSharpCode.SharpZipLib.Zip.Compression.Inflater inf;
                            inf = new ICSharpCode.SharpZipLib.Zip.Compression.Inflater();
                            byte[] In = new byte[size - 4];
                            byte[] Out = new byte[br.ReadUInt32()];
                            br.Read(In, 0, (int)(size - 4));
                            inf.SetInput(In);
                            inf.Inflate(Out);
                            try
                            {
                                ids.AddRange(SkyrimGetDataFileList(Out, s));
                            }
                            catch { }
                        }
                        else
                        {
                            try
                            {
                                ids.AddRange(SkyrimGetDataFileList(br.ReadBytes((int)size), s));
                            }
                            catch { }
                        }
                    }
                }
            }
            finally
            {
                br.Close();
            }
            OblivionModManager.Classes.BSAArchive.Clear();
            return ids.ToArray();
        }
    }
}