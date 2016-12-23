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
using System.IO;
using System.Collections.Generic;
using OblivionModManager;

namespace ConflictDetector {
    public class plugin {
        private string name;
        private string omod;
        private bool active;
        private List<EDID[]> conflicts;

        public string Name {
            get { return name; }
        }
        public string Parent {
            get { return omod;}
        }
        public bool Active {
            get { return active;}
        }

        public void AddConflict(List<EDID> edids) {
            for(int i=0;i<edids.Count;i++) {
                if(edids[i].plugin==this) edids.RemoveAt(i);
            }
            conflicts.Add(edids.ToArray());
        }

        public plugin(string _name,string _omod,bool _active) {
            name=_name;
            omod=_omod;
            active=_active;
            conflicts=new List<EDID[]>();
        }

        public string GetReport() {
            int maxconflict=0;
            string report;
            string Name="["+name+"] ";
            if(omod==null) {
                Name+="(unparented) ";
            } else {
                Name+="(Part of "+omod+")";
            }
            if(active) {
                Name+=" - Active";
            } else {
                Name+=" - Inactive";
            }
            Name+="\n";
            if(conflicts.Count==0) {
                report="black\n"+
                    Name+
                    "blue\n"+
                    "No conflicts\n";
            } else {
                report="black\n"+Name;
                string report2="";
                foreach(EDID[] e in conflicts) {
                    switch(e[0].rectype) {
                    case "WRLD":
                        if(maxconflict<1) maxconflict=1;
                        if(!ReportGenerator.CheckForVeryMinor) continue;
                        report2+="green\n";
                        break;
                    case "DIAL":
                    case "CELL":
                        if(maxconflict<2) maxconflict=2;
                        if(!ReportGenerator.CheckForMinor) continue;
                        report2+="orange\n";
                        break;
                    default:
                        if(maxconflict<3) maxconflict=3;
                        if(!ReportGenerator.CheckForMajor) continue;
                        report2+="red\n";
                        break;
                    }
                    report2+="EDID '"+e[0].uppername+"' of record type "+e[0].rectype+" conflicts with "+e.Length.ToString()+" other plugin";
                    if(e.Length==1) report2+=".\n"; else report2+="s.\n";
                    foreach(EDID e2 in e) {
                        report2+="   "+e2.plugin.Name.PadRight(50, ' ')+"(Record type: "+e2.rectype+")";
                        if(e2.plugin.Parent==null) {
                            report2+=" (Unparented)";
                        } else {
                            report2+=" (Parent: "+e2.plugin.Parent+")";
                        }
                        if(e2.plugin.Active) {
                            report2+=" (Active)\n";
                        } else {
                            report2+=" (Inactive)\n";
                        }
                    }
                }
                if(report2=="") {
                    switch(maxconflict) {
                    case 1: report+="green\nVery minor conflicts\n"; break;
                    case 2: report+="orange\nMinor conflicts\n"; break;
                    case 3: report+="red\nMajor conflicts\n"; break;
                    }
                } else report+=report2;
            }
            return report;
        }
    }

    public class sorter : IComparer<EDID> {
        public int Compare(EDID a, EDID b) {
            return string.Compare(a.name, b.name);
        }
    }

    public class ReportGenerator {
        private static void AddConflicts(List<EDID> conflicts) {
            foreach(EDID e in conflicts) {
                List<EDID> list= new List<EDID>();
                list.AddRange(conflicts.ToArray());
                e.plugin.AddConflict(list);
            }
        }

        public static bool CheckForVeryMinor;
        public static bool CheckForMinor;
        public static bool CheckForMajor;

        public static bool IncludeEsps;
        public static bool IgnoreInactiveEsps;
        public static bool IncludeOmods;
        public static bool IgnoreInactiveOmods;

        public static string[] Additional;
        public static List<string> Errors;
        public static bool Run=false;

        public static void GenerateReport() {
            //get some settings
            Run=false;
            (new SettingsForm()).ShowDialog();
            if(!Run) return;
            //Get the list of edids
            List<EDID> list=new List<EDID>();
            List<plugin> plugins=new List<plugin>();
            Errors=new List<string>();

            if(IncludeEsps|IncludeOmods) {
                foreach(OblivionModManager.EspInfo ei in OblivionModManager.Program.Data.Esps) {
                    if(Path.GetExtension(ei.FileName)!=".esp"||ei.BelongsTo==OblivionModManager.EspInfo.BaseOwner) continue;
                    if(ei.Parent==null) {
                        if(!IncludeEsps) continue;
                        if(!ei.Active&&IgnoreInactiveEsps) continue;
                        try {
                            plugin p=new plugin(ei.FileName, null, ei.Active);
                            list.AddRange(TesFile.GetIDList(Path.Combine(Program.currentGame.DataFolderPath,ei.FileName), p));
                            plugins.Add(p);
                        } catch {
                            Errors.Add("An error occured trying to read plugin '"+ei.FileName+"'");
                        }
                    } else {
                        if(!IncludeOmods||!IgnoreInactiveOmods) continue;
                        if(!ei.Active) continue;
                        try {
                            plugin p=new plugin(ei.FileName, ei.BelongsTo, ei.Active);
                            list.AddRange(TesFile.GetIDList(Path.Combine(Program.currentGame.DataFolderPath,ei.FileName), p));
                            plugins.Add(p);
                        } catch {
                            Errors.Add("An error occured trying to read plugin '"+ei.FileName+"'");
                        }
                    }
                }
                if(IncludeOmods&&!IgnoreInactiveOmods) {
                    foreach(OblivionModManager.omod o in OblivionModManager.Program.Data.omods) {
                        string s=o.GetPlugins();
                        if(s==null) continue;
                        foreach(string file in Directory.GetFiles(s, "*.esp")) {
                            plugin p=new plugin(Path.GetFileName(file), o.FileName, o.Conflict==OblivionModManager.ConflictLevel.Active);
                            list.AddRange(TesFile.GetIDList(file, p));
                            plugins.Add(p);
                        }
                    }
                }
            }
            foreach(string s in Additional) {
                //if(Path.GetExtension(s)==".omod") {

                //} else {
                    plugin p=new plugin(s, null, false);
                    list.AddRange(TesFile.GetIDList(s, p));
                    plugins.Add(p);
                //}
            }
            list.Sort(new sorter());
            //generate the plugins conflicts array
            List<EDID> conflicts=new List<EDID>();
            string lastid="";
            foreach(EDID e in list) {
                if(e.name==lastid) {
                    conflicts.Add(e);
                } else {
                    if(conflicts.Count>1) {
                        AddConflicts(conflicts);
                    }
                    conflicts.Clear();
                    conflicts.Add(e);
                    lastid=e.name;
                }
            }
            //generate the report
            string report="";
            if(Errors.Count>0) {
                report=Errors.Count.ToString()+" errors occured while generating the conflict report\n";
                foreach(string s in Errors) report+=s+"\n";
            }
            foreach(plugin p in plugins) {
                report+=p.GetReport()+"\n";
            }
            //display it
            (new OblivionModManager.TextEditor(report)).ShowDialog();
            //Clean up
            Additional=null;
            Errors=null;
        }
    }
}