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

namespace OblivionModManager.Forms {
    public partial class SimResults : Form {
        public SimResults(ScriptReturnData srd, string[] plugins, string[] data) {
            InitializeComponent();
            //Install Flags
            chkCanceled.Checked = srd.CancelInstall;
            chkDataFiles.Checked = srd.InstallAllData;
            chkPlugins.Checked = srd.InstallAllPlugins;

            //Plugins
            if(srd.InstallAllPlugins) {
                lstPIgnored.Items.AddRange(srd.IgnorePlugins.ToArray());
                for(int i=0;i<srd.IgnorePlugins.Count;i++) srd.IgnorePlugins[i]=srd.IgnorePlugins[i].ToLower();
                foreach(string plugin in plugins) {
                    if(!srd.IgnorePlugins.Contains(plugin.ToLower())) {
                        lstPInstalled.Items.Add(plugin);
                    }
                }
            } else {
                lstPInstalled.Items.AddRange(srd.InstallPlugins.ToArray());
                for(int i=0;i<srd.InstallPlugins.Count;i++) srd.InstallPlugins[i]=srd.InstallPlugins[i].ToLower();
                foreach(string plugin in plugins) {
                    if(!srd.InstallPlugins.Contains(plugin.ToLower())) {
                        lstPIgnored.Items.Add(plugin);
                    }
                }
            }

            //Data
            if(srd.InstallAllData) {
                lstDFIgnored.Items.AddRange(srd.IgnoreData.ToArray());
                for(int i=0;i<srd.IgnoreData.Count;i++) srd.IgnoreData[i]=srd.IgnoreData[i].ToLower();
                foreach(string file in data) {
                    if(!srd.IgnoreData.Contains(file.ToLower())) {
                        lstDFInstalled.Items.Add(file);
                    }
                }
            } else {
                lstDFInstalled.Items.AddRange(srd.InstallData.ToArray());
                for(int i=0;i<srd.InstallData.Count;i++) srd.InstallData[i]=srd.InstallData[i].ToLower();
                foreach(string file in data) {
                    if(!srd.InstallData.Contains(file.ToLower())) {
                        lstDFIgnored.Items.Add(file);
                    }
                }
            }

            //Edits and Misc Settings

            //Load Order
            ListViewItem lvi;
            foreach(PluginLoadInfo pli in srd.LoadOrderList) {
                lvi = new ListViewItem(new string[] { pli.Plugin, pli.Target, (pli.LoadAfter ? "After" : "Before") });
                lvLoadOrder.Items.Add(lvi);
            }

            //Copy Data Files
            foreach(ScriptCopyDataFile scdf in srd.CopyDataFiles) {
                lvi = new ListViewItem(new string[] { scdf.CopyFrom, scdf.CopyTo });
                lvCopiedDF.Items.Add(lvi);
            }

            //Copy Plugins
            foreach(ScriptCopyDataFile scdf in srd.CopyPlugins) {
                lvi = new ListViewItem(new string[] { scdf.CopyFrom, scdf.CopyTo });
                lvCopiedPF.Items.Add(lvi);
            }

            //Conflicts
            string minver;
            string maxver;
            string level;
            foreach(ConflictData cd in srd.ConflictsWith) {

                minver = cd.MinMajorVersion + "." + cd.MinMinorVersion;
                maxver = cd.MaxMajorVersion + "." + cd.MaxMinorVersion;
                level = cd.level.ToString();
                switch(cd.level) {
                case ConflictLevel.NoConflict:
                    level = "None";
                    break;
                case ConflictLevel.MinorConflict:
                    level = "Minor";
                    break;
                case ConflictLevel.MajorConflict:
                    level = "Major";
                    break;
                case ConflictLevel.Unusable:
                    level = "Unusable";
                    break;
                }

                lvi = new ListViewItem(new string[] { cd.File, minver, maxver, level, cd.Comment, (cd.Partial ? "Yes" : "No") });
                lvConflicts.Items.Add(lvi);
            }

            //Depends

            foreach(ConflictData cd in srd.DependsOn) {
                minver = cd.MinMajorVersion + "." + cd.MinMinorVersion;
                maxver = cd.MaxMajorVersion + "." + cd.MaxMinorVersion;

                lvi = new ListViewItem(new string[] { cd.File, minver, maxver, cd.Comment, (cd.Partial? "Yes" : "No") });
                lvDepends.Items.Add(lvi);
            }

            //INI Edits
            foreach(INIEditInfo iei in srd.INIEdits) {
                lvi = new ListViewItem(new string[] { iei.Section, iei.Name, iei.NewValue });
                lvINIEdit.Items.Add(lvi);
            }

            //SDP Edits
            foreach(SDPEditInfo sei in srd.SDPEdits) {
                lvi = new ListViewItem(new string[] { sei.Package.ToString(), sei.Shader, sei.BinaryObject });
                lvSDPEdit.Items.Add(lvi);
            }

            //ESP Edits
            foreach(ScriptEspEdit see in srd.EspEdits) {
                lvi = new ListViewItem(new string[] { see.Plugin, (see.IsGMST ? "GMST" : "Global"), see.EDID, see.Value });
                lvESPEdit.Items.Add(lvi);
            }

            //BSA Registration

            foreach(string bsa in srd.RegisterBSAList) {
                lvi = new ListViewItem(bsa);
                lvBSAReg.Items.Add(lvi);
            }

            //Unchecked ESPs
            foreach(string esp in srd.UncheckedPlugins) {
                lvi = new ListViewItem(esp);
                lvESPUnchecked.Items.Add(lvi);
            }


            //Deactivation Warnings
            foreach(ScriptEspWarnAgainst sewa in srd.EspDeactivation) {
                level = "No Warning";
                switch(sewa.Status) {
                case DeactiveStatus.Disallow:
                    level = "Disallow";
                    break;
                case DeactiveStatus.WarnAgainst:
                    level = "Warning";
                    break;
                }
                lvi = new ListViewItem(new string[] { sewa.Plugin, level });
                lvESPWarn.Items.Add(lvi);
            }

        }
    }
}