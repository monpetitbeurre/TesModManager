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
using System.Collections.Generic;
using System.IO;
using MessageBox=System.Windows.Forms.MessageBox;

namespace OblivionModManager.Classes {
    static class BSAUncorrupter {
        public struct BSAFixResult {
            public int filesFound;
            public int filesFixed;
            public int foldersFound;
            public int foldersFixed;
            public int Failed;
        }

        private class BSAFolderEntry {
            public readonly ulong Hash;
            public readonly int Files;
            public readonly long HashOffset;

            public BSAFolderEntry(ulong hash, int files, long offset) {
                Hash=hash;
                Files=files;
                HashOffset=offset;
            }
        }

        private class BSAFileEntry {
            public readonly long HashOffset;
            public readonly ulong Hash;

            public BSAFileEntry(ulong hash, long offset) {
                Hash=hash;
                HashOffset=offset;
            }
        }

        private static string ReadString(int len, BinaryReader br) {
            string s="";
            for(int i=0;i<len;i++) s+=(char)br.ReadByte();
            return s;
        }

        public static BSAFixResult ScanBSA(string[] files) {
            BSAFixResult result=new BSAFixResult();
            foreach(string s in files) ScanBSA(s, ref result);
            return result;
        }
        public static void ScanBSA(string file, ref BSAFixResult result) {
            BinaryReader br=null;
            BinaryWriter bw=null;
            try {
                FileStream fs=MonoFS.File.Open(file, FileMode.Open, FileAccess.ReadWrite);
                br=new BinaryReader(fs, System.Text.Encoding.Default);
                bw=new BinaryWriter(fs, System.Text.Encoding.Default);
                if(Program.ReadCString(br)!="BSA") {
                    throw new obmmException("'"+file+"' is not a valid BSA archive");
                }
                if(br.ReadUInt32()!=103) {
                    if(MessageBox.Show("This BSA archive has an unknown version number.\n"+
                    "Attempt to open anyway?", "Warning", System.Windows.Forms.MessageBoxButtons.YesNo)!=System.Windows.Forms.DialogResult.Yes) {
                        br.Close();
                        return;
                    }
                }
                br.BaseStream.Position+=8;
                int FolderCount=br.ReadInt32();
                int FileCount=br.ReadInt32();
                br.BaseStream.Position+=12;
                BSAFileEntry[] Files=new BSAFileEntry[FileCount];
                BSAFolderEntry[] Folders=new BSAFolderEntry[FolderCount];
                ulong[] FileHashes=new ulong[FileCount];
                result.filesFound+=FileCount;
                result.foldersFound+=FolderCount;
                for(int i=0;i<FolderCount;i++) {
                    long offset=br.BaseStream.Position;
                    ulong hash=br.ReadUInt64();
                    int numfiles=br.ReadInt32();
                    Folders[i]=new BSAFolderEntry(hash, numfiles, offset);
                    br.BaseStream.Position+=4;
                }
                int filecount=0;
                for(int i=0;i<FolderCount;i++) {
                    string folder=ReadString(br.ReadByte()-1, br);
                    ulong newhash=OblivionBSA.GenHash(folder, "");
                    if(newhash!=Folders[i].Hash) {
                        long offset=br.BaseStream.Position;
                        br.BaseStream.Position=Folders[i].HashOffset;
                        bw.Write(newhash);
                        br.BaseStream.Position=offset;
                        result.foldersFixed++;
                    }
                    br.ReadByte();
                    for(int j=0;j<Folders[i].Files;j++) {
                        long offset=br.BaseStream.Position;
                        ulong hash=br.ReadUInt64();
                        Files[filecount++]=new BSAFileEntry(hash, offset);
                        br.BaseStream.Position+=8;
                    }
                }
                for(int i=0;i<FileCount;i++) {
                    string s="";
                    while(true) {
                        char c=br.ReadChar();
                        if(c=='\0') break;
                        s+=c;
                    }
                    ulong newhash=OblivionBSA.GenHash(s);
                    if(newhash!=Files[i].Hash) {
                        long offset=br.BaseStream.Position;
                        br.BaseStream.Position=Files[i].HashOffset;
                        bw.Write(newhash);
                        br.BaseStream.Position=offset;
                        result.filesFixed++;
                    }
                }
            } catch(Exception ex) {
                Program.logger.WriteToLog("Could not scan BSA: "+ex.Message,Logger.LogLevel.Error);
                result.Failed++;
            } finally {
                if(br!=null) br.Close();
                if(bw!=null) bw.Close();
            }
        }
    }
}
