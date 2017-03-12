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
using System.IO;
using System.Collections.Generic;
using MessageBox=System.Windows.Forms.MessageBox;

namespace OblivionModManager {
    [Serializable]
    public class sData {
        public readonly List<omod> omods=new List<omod>();

        //public readonly List<DataFileInfo> DataFiles =new List<DataFileInfo>();
        public readonly Dictionary<string, DataFileInfo> DataFiles = new Dictionary<string, DataFileInfo>();

        public readonly List<EspInfo> Esps=new List<EspInfo>();

        public readonly List<BSA> BSAs=new List<BSA>();

        public readonly List<INIEditInfo> INIEdits=new List<INIEditInfo>();

        public void WriteTo(BinaryWriter _Writer)
        {
            // write the list of omods
            _Writer.Write((Int32)omods.Count);
            foreach (omod cOmod in omods)
                cOmod.WriteDataTo(_Writer);

            // write the list of DataFileInfo
            _Writer.Write((Int32)DataFiles.Count);
            foreach (DataFileInfo cDataFile in DataFiles.Values)
                cDataFile.WriteDataTo(_Writer);

            // write the list of EspInfo
            _Writer.Write((Int32)Esps.Count);
            foreach (EspInfo cEsp in Esps)
                cEsp.WriteDataTo(_Writer);

            // write the list of BSA
            _Writer.Write((Int32)BSAs.Count);
            foreach (BSA cBSA in BSAs)
                cBSA.WriteDataTo(_Writer);

            // write the list of INIEditInfo
            _Writer.Write((Int32)INIEdits.Count);
            foreach (INIEditInfo cINIEdit in INIEdits)
                cINIEdit.WriteDataTo(_Writer);
        }

        public sData()
        {
        }
        public sData(BinaryReader _Reader): this(_Reader,4)
        {
            
        }

        public sData(BinaryReader _Reader, int version)
        {
            try
            {
                // read the list of omods
                int len = _Reader.ReadInt32();
                for (int i = 0; i < len; i++)
                    omods.Add(new omod(_Reader, version));

                // read the list of DataFileInfo
                len = _Reader.ReadInt32();
                for (int i = 0; i < len; i++)
                {
                    DataFileInfo dfi = new DataFileInfo(_Reader);
                    DataFiles.Add(dfi.LowerFileName,dfi);
                }

                // read the list of EspInfo
                len = _Reader.ReadInt32();
                for (int i = 0; i < len; i++)
                    Esps.Add(new EspInfo(_Reader));

                // link ESPs back to their parent omod
                foreach (EspInfo esp in Esps)
                    foreach (omod om in omods)
                    {
                        if (om.FileName.Equals(esp.ParentOmodName))
                            esp.Parent = om;
                    }


                // read the list of BSA
                len = _Reader.ReadInt32();
                for (int i = 0; i < len; i++)
                    BSAs.Add(new BSA(_Reader));

                // read the list of INIEditInfo
                len = _Reader.ReadInt32();
                for (int i = 0; i < len; i++)
                    INIEdits.Add(new INIEditInfo(_Reader));
            }
            catch
            {
                // corrupt data file? rebuild
                omods.Clear();
                DataFiles.Clear();
                Esps.Clear();
                BSAs.Clear();
                INIEdits.Clear();
                MessageBox.Show("Data file is corrupt! It will need to be rebuilt by rescanning all omods.", "Corrupt file", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public bool DoesModNameExist(string s)
        {
            s = s.ToLower();
            foreach (omod o in omods)
            {
                if (o.ModName == s) return true;
            }
            return false;
        }

        public bool DoesModExist(string s) {
            s=s.ToLower();
            string mods="";
            foreach(omod o in omods) {
                if(o.LowerFileName==s)
                    return true;
                mods += o.LowerFileName + "\n";
            }
            return false;
        }
        public bool DoesModExist(ConflictData cd, bool Active) {
            foreach(omod o in omods) {
                if(Active&&o.Conflict!=ConflictLevel.Active) continue;
                if(cd==o) return true;
            }
            return false;
        }

        public omod GetModByName(string s)
        {
            s = s.ToLower();
            foreach (omod o in omods)
            {
                if (o.ModName.ToLower() == s) return o;
            }
            return null;
        }

        public omod GetMod(string s) {
            s=s.ToLower();
            foreach(omod o in omods) {
                if(o.LowerFileName==s) return o;
            }
            return null;
        }

        public bool DoesEspExist(string s) {
            s=s.ToLower();
            foreach(EspInfo ei in Esps) {
                if(ei.LowerFileName==s) return true;
            }
            return false;
        }

        public EspInfo GetEsp(string s) {
            s=s.ToLower();
            foreach(EspInfo ei in Esps) {
                if(ei.LowerFileName==s) return ei;
            }
            return null;
        }

        public bool DoesDataFileExist(string s) {
            s=s.ToLower();
            return DataFiles.ContainsKey(s);
            //foreach(DataFileInfo sfi in DataFiles) {
            //    if(sfi.LowerFileName==s) return true;
            //}
            //return false;
        }
        public bool DoesDataFileExist(DataFileInfo dfi) {
            return DataFiles.ContainsKey(dfi.LowerFileName);
            //foreach (DataFileInfo sfi in DataFiles)
            //{
            //    if(sfi==dfi) return true;
            //}
            //return false;
        }

        public DataFileInfo GetDataFile(string s) {
            s=s.ToLower();
            if (DataFiles.ContainsKey(s))
                return DataFiles[s];
            else
                return null;
            //foreach(DataFileInfo sfi in DataFiles) {
            //    if(sfi.LowerFileName==s) return sfi;
            //}
            //return null;
        }
        public DataFileInfo GetDataFile(DataFileInfo dfi) {
            if (DataFiles.ContainsKey(dfi.LowerFileName))
                return DataFiles[dfi.LowerFileName];
            else
                return null;
            //foreach (DataFileInfo sfi in DataFiles)
            //{
            //    if(sfi==dfi) return sfi;
            //}
            //return null;
        }

        public bool DoesBSAExist(string s) {
            s=s.ToLower();
            foreach(BSA b in BSAs) {
                if(b.LowerFileName==s) return true;
            }
            return false;
        }

        public BSA GetBSA(string s) {
            s=s.ToLower();
            foreach(BSA b in BSAs) {
                if(b.LowerFileName==s) return b;
            }
            return null;
        }

        private class PluginSorter : IComparer<EspInfo> {
            public int Compare(EspInfo a, EspInfo b) {
                int icomp = 0;

                if (b.LowerFileName == a.LowerFileName)
                    icomp = 0;
                else if (Path.GetExtension(a.LowerFileName) != Path.GetExtension(b.LowerFileName) && Settings.bPreventMovingESPBeforeESM)
                    icomp = (Path.GetExtension(a.LowerFileName) == ".esm") ? (-1) : 1;
                else if (Program.loadOrderList.Count>0)
                {
                    foreach (string strline in Program.loadOrderList)
                    {
                        string pin = strline.ToLower();
                        if (pin == a.LowerFileName)
                        {
                            icomp = -1;
                            break;
                        }
                        else if (pin == b.LowerFileName)
                        {
                            icomp = 1;
                            break;
                        }
                    }
                }
                else
                {
                    icomp = DateTime.Compare(a.DateModified, b.DateModified);
                }
                return icomp;
            }
        }
        public void SortEsps() {
            Program.logger.WriteToLog("Sorting ESPs ", Logger.LogLevel.High);
            foreach (EspInfo ei in Esps)
            {
                ei.DateModified=File.GetLastWriteTime(Path.Combine(Program.currentGame.DataFolderPath,ei.FileName));
            }
            Esps.Sort(new PluginSorter());

            if(Settings.NeverTouchLoadOrder) return;
            if (!Settings.bUseTimeStamps) return;

            try
            {
                DateTime date = new DateTime(2008, 1, 1);
                for (int j = 0; j < Esps.Count; j++)
                {
                    date = date.AddMinutes(1);
                    if (File.Exists(Path.Combine(Program.currentGame.DataFolderPath,Esps[j].FileName)))
                        File.SetLastWriteTime(Path.Combine(Program.currentGame.DataFolderPath,Esps[j].FileName), date);
                    string bsa = Esps[j].LowerFileName; bsa = bsa.Replace(".esm", "").Replace(".esp", "").Replace(".ghost", ""); bsa += ".bsa";
                    if (File.Exists(Path.Combine(Program.currentGame.DataFolderPath,bsa)))
                        File.SetLastWriteTime(Path.Combine(Program.currentGame.DataFolderPath,bsa), date);
                }
            }
            catch (Exception ex)
            {
//                MessageBox.Show("Unable to set timestamps on files: " + ex.Message, "Cannot set timestamps", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                Program.logger.WriteToLog("Unable to set timestamps on files: " + ex.Message, Logger.LogLevel.Error);
            }
            Program.logger.WriteToLog("Sorting ESPs ", Logger.LogLevel.High);
        }

        public void InsertESP(EspInfo ei, PluginLoadInfo[] plis, bool early) {
            if(Settings.NeverTouchLoadOrder || !Settings.bUseTimeStamps) {
                SortEsps();
                return;
            }

            DateTime MinTime;
            DateTime MaxTime;
            DateTime InsertAt=DateTime.Now;
            if(early&&Esps.Count>0) {
                MaxTime=File.GetLastWriteTime(Path.Combine(Program.currentGame.DataFolderPath,Esps[0].FileName));
                if(InsertAt+TimeSpan.FromMinutes(1)>=MaxTime) InsertAt=MaxTime-TimeSpan.FromMinutes(1);
            } else if(Settings.NewEspsLoadLast&&Esps.Count>0) {
                MaxTime=File.GetLastWriteTime(Path.Combine(Program.currentGame.DataFolderPath,(Esps[Esps.Count-1]).FileName));
                if(InsertAt-TimeSpan.FromMinutes(1)<=MaxTime) InsertAt=MaxTime+TimeSpan.FromMinutes(1);
            }
            MinTime=DateTime.MinValue;
            MaxTime=DateTime.MaxValue;
            foreach(EspInfo esp in Esps) {
                foreach(string target in esp.MustLoadAfter) {
                    if(target!=ei.LowerFileName) continue;
                    DateTime temp=File.GetLastWriteTime(Path.Combine(Program.currentGame.DataFolderPath,esp.FileName));
                    if(temp<MaxTime) MaxTime=temp;
                }
                foreach(string target in esp.MustLoadBefore) {
                    if(target!=ei.LowerFileName) continue;
                    DateTime temp=File.GetLastWriteTime(Path.Combine(Program.currentGame.DataFolderPath,esp.FileName));
                    if(temp>MinTime) MinTime=temp;
                }
            }
            foreach(PluginLoadInfo pli in plis) {
                if(pli.Plugin.ToLower()!=ei.LowerFileName) continue;
                EspInfo target=GetEsp(pli.Target);
                if(pli.LoadAfter) {
                    ei.MustLoadAfter.Add(pli.Target.ToLower());
                    if(target!=null) {
                        DateTime temp=File.GetLastWriteTime(Path.Combine(Program.currentGame.DataFolderPath,target.FileName));
                        if(temp>MinTime) MinTime=temp;
                    }
                } else {
                    ei.MustLoadBefore.Add(pli.Target.ToLower());
                    if(target!=null) {
                        DateTime temp=File.GetLastWriteTime(Path.Combine(Program.currentGame.DataFolderPath,target.FileName));
                        if(temp<MaxTime) MaxTime=temp;
                    }
                }
            }
            if(MinTime>DateTime.MinValue&&MaxTime<DateTime.MaxValue&&MinTime>=MaxTime+TimeSpan.FromSeconds(2)) {
//                MessageBox.Show("Unable to correctly insert plugin "+ei.FileName+" because of conflicting load order information.\n"+
//                    "This is most likely to occur if two existing mods are already loading in the wrong order.\n"+
//                    "Please check the readmes of other mods to ensure you have installed them correctly.\n"+
//                    ei.FileName+" has been added to the end of the load order.", "Error");
                Program.logger.WriteToLog("Unable to correctly insert plugin " + ei.FileName + " because of conflicting load order information.\n" +
                    "This is most likely to occur if two existing mods are already loading in the wrong order.\n" +
                    "Please check the readmes of other mods to ensure you have installed them correctly.\n" +
                    ei.FileName + " has been added to the end of the load order.", Logger.LogLevel.Error);
            }
            else
            {
                if(InsertAt>MaxTime) InsertAt=MaxTime-TimeSpan.FromMinutes(2);
                while(InsertAt<MinTime+TimeSpan.FromSeconds(1)) InsertAt+=TimeSpan.FromSeconds(1);
                try
                {
                    File.SetLastWriteTime(Path.Combine(Program.currentGame.DataFolderPath,ei.FileName), InsertAt);
                    string bsa = ei.LowerFileName; bsa = bsa.Replace(".esm", "").Replace(".esp", "").Replace(".ghost", ""); bsa += ".bsa";
                    if (File.Exists(Path.Combine(Program.currentGame.DataFolderPath,bsa)))
                        File.SetLastWriteTime(Path.Combine(Program.currentGame.DataFolderPath,bsa), InsertAt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to set timestamps on files: " + ex.Message, "Cannot set timestamps", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }

            }
            ei.Active=true;
            Esps.Add(ei);
            SortEsps();
        }
    }
}
