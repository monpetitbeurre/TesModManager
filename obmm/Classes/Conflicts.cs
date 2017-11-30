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
using File = System.IO.File;

namespace OblivionModManager {
    public static class Conflicts {
        public static void UpdateConflicts() {
            int count = 0;
            ProgressForm pf = new ProgressForm("Updating conflicts...", false);
            int tries = 0;
            while (tries++ < 3)
            {
                try
                {
                    pf.SetProgressRange(Program.Data.omods.Count);
                    pf.ShowInTaskbar = true;
                    pf.EnableCancel("cancelled");
                    pf.Show();
                    foreach (omod o in Program.Data.omods)
                    {
                        //if (!o.Hidden)
                        UpdateConflict(o);
                        pf.UpdateProgress(count++);
                        System.Windows.Forms.Application.DoEvents();
                        if (pf.bCancelled) break;
                    }
                }
                catch
                {
                    // do it again coz collection changed?
                    continue;
                }
                finally
                {
                    pf.Hide();
                    pf.Close();
                    pf.Dispose();
                    System.Windows.Forms.Application.UseWaitCursor = false;
                }
                break;
            }
        }
        public static void UpdateConflict(omod o) {
            if(o.Conflict==ConflictLevel.Active) return;
            o.Conflict=ConflictLevel.NoConflict;
            if(!Settings.TrackConflicts) return;
            //Check that no esps already exist
            foreach(string s in o.AllPlugins) {
                if (File.Exists(System.IO.Path.Combine(Program.currentGame.DataFolderPath, s)))
                {
                    o.Conflict=ConflictLevel.Unusable;
                    return;
                }
            }
            //Check for any script defined conflicting mods
            foreach(ConflictData cd in o.ConflictsWith) {
                if(Program.Data.DoesModExist(cd, true)) {
                    if((int)cd.level>(int)o.Conflict) o.Conflict=cd.level;
                }
            }
            foreach(omod o2 in Program.Data.omods) {
                if(o2.Conflict!=ConflictLevel.Active||o2==o) continue;
                foreach(ConflictData cd in o2.ConflictsWith) {
                    if(cd==o) {
                        if((int)cd.level>(int)o.Conflict) o.Conflict=cd.level;
                    }
                }
            }
            if(o.Conflict==ConflictLevel.MajorConflict) return;
            //Check that no data files already exist
            foreach(DataFileInfo df in o.AllDataFiles) {
                if (df.LowerFileName.StartsWith("fomod\\")) continue; // ignore some fomod specific files
                DataFileInfo dfi = Program.Data.GetDataFile(df);
                if(dfi==null) {
                    if(!File.Exists(System.IO.Path.Combine(Program.currentGame.DataFolderPath,df.FileName))) continue;
                    o.Conflict = ConflictLevel.MajorConflict;
                    return;
                } else if(df.CRC==dfi.CRC) {
                    o.Conflict = ConflictLevel.MinorConflict;
                } else {
                    o.Conflict = ConflictLevel.MajorConflict;
                    return;
                }
            }
        }
        public static string ConflictReport(omod o) {
            string epilog=Environment.NewLine+Environment.NewLine+"This conflict report displays only data file and script defined conflicts"+
                Environment.NewLine+"For EDID conflicts, please use the full conflict report tool.";
            if(o.Conflict==ConflictLevel.Active) return "["+o.FileName+"] (Active)"+Environment.NewLine+
                "Data file and script conflicts are not tracked for active mods."+epilog;
            string ModReport="";
            ConflictLevel Conflict=ConflictLevel.NoConflict;
            //Check that no esps already exist
            foreach(string s in o.AllPlugins) {
                EspInfo ei=Program.Data.GetEsp(s);
                if(ei!=null||File.Exists(System.IO.Path.Combine(Program.currentGame.DataFolderPath,s))) {
                    Conflict=ConflictLevel.Unusable;
                    ModReport+="Plugin file "+s+" already exists."+Environment.NewLine;
                    if(ei!=null) {
                        ModReport+="- belongs to: "+ei.BelongsTo;
                    } else {
                        ModReport+="- esp missing from obmm database";
                    }
                    ModReport+=Environment.NewLine;
                }
            }
            //Check for any script defined conflicting mods
            foreach(ConflictData cd in o.ConflictsWith) {
                if(Program.Data.DoesModExist(cd, true)) {
                    if((int)cd.level>(int)Conflict) Conflict=cd.level;
                    ModReport+="Conflicts with active mod "+cd.File+Environment.NewLine;
                    if(cd.Comment!=null) ModReport+="- "+cd.Comment+Environment.NewLine;
                }
            }
            foreach(omod o2 in Program.Data.omods) {
                if(o2.Conflict!=ConflictLevel.Active||o2==o) continue;
                foreach(ConflictData cd in o2.ConflictsWith) {
                    if(cd==o) {
                        if((int)cd.level>(int)Conflict) Conflict=cd.level;
                        ModReport+="Active mod "+o2.FileName+" reports that it conflicts with this one"+Environment.NewLine;
                        if(cd.Comment!=null) ModReport+="- "+cd.Comment+Environment.NewLine;
                    }
                }
            }
            //Check that no data files already exist
            foreach(DataFileInfo df in o.AllDataFiles) {
                DataFileInfo dfi=Program.Data.GetDataFile(df);
                string basepath = o.bSystemMod ? Path.Combine(Program.currentGame.DataFolderPath, "..") : Program.currentGame.DataFolderPath;
                if (dfi==null)
                {
                    if (!File.Exists(System.IO.Path.Combine(basepath, df.FileName))) continue;
                    if((int)ConflictLevel.MajorConflict>(int)Conflict) Conflict=ConflictLevel.MajorConflict;
                    ModReport+="Data file "+df.FileName+" already exists"+Environment.NewLine;
                    ModReport+="- No data found on file."+Environment.NewLine;
                }
                else if(df.CRC==dfi.CRC)
                {
                    if (df.CRC == 0)
                    {
                        if ((int)ConflictLevel.MajorConflict > (int)Conflict) Conflict = ConflictLevel.MajorConflict;
                        ModReport += "Data file " + df.FileName + " already exists" + Environment.NewLine;
                        ModReport += "- CRC have not been calculated so exact is not possible." + Environment.NewLine;
                        ModReport += "- Data file owned by " + dfi.Owners + Environment.NewLine;
                    }
                    else
                    {
                        if ((int)ConflictLevel.MinorConflict > (int)Conflict) Conflict = ConflictLevel.MinorConflict;
                        ModReport += "Data file " + df.FileName + " already exists" + Environment.NewLine;
                        ModReport += "- CRC's match, so probably nothing to worry about." + Environment.NewLine;
                        ModReport += "- Data file owned by " + dfi.Owners + Environment.NewLine;
                    }
                }
                else
                {
                    if((int)ConflictLevel.MajorConflict>(int)Conflict) Conflict=ConflictLevel.MajorConflict;
                    ModReport+="Data file "+df.FileName+" already exists"+Environment.NewLine;
                    ModReport+="- CRC mismatch. The new file is different from the old."+Environment.NewLine;
                    ModReport+="- Data file owned by "+dfi.Owners+Environment.NewLine;
                }
            }
            string prolog="["+o.FileName+"] (";
            switch(Conflict) {
            case ConflictLevel.Unusable: prolog+="unusable"; break;
            case ConflictLevel.MajorConflict: prolog+="major conflict"; break;
            case ConflictLevel.MinorConflict: prolog+="minor conflict"; break;
            case ConflictLevel.NoConflict: prolog+="no conflicts"; break;
            }
            prolog+=")"+Environment.NewLine;
            return prolog+ModReport+epilog;
        }
    }
}
