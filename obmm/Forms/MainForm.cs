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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using BaseTools.Configuration;
using BaseTools.XS;
using BaseTools.Configuration.Parsers;
using SV = BaseTools.Searching.StringValidator;
using System.Threading;
using System.Diagnostics;
using OblivionModManager.Forms;

namespace OblivionModManager {
	public partial class MainForm : Form {
		private List<System.Windows.Forms.FormClosingEventHandler> fceh = new List<FormClosingEventHandler>();
        private string strFilter="";
        private string strTmpDir = "";
        //private bool bLoadingForm = true;
        private bool bClosingForm = false;
        private bool bImporting=false;
        check modUpdateCheckList = new check();
        private omod currentlySelectedOmod = null;

        private bool updatingESPList = false;

		public MainForm() {
			InitializeComponent();



            downloadStatusLabel.Text = (Program.downloadList.Count + Program.pausedList.Count) + " download" + (Program.downloadList.Count + Program.pausedList.Count > 1 ? "s" : "");
            if (Program.downloadList.Count + Program.pausedList.Count > 0)
            {
                downloadStatusLabel.Visible = true;
            }
            backgroundModImportWorker.WorkerReportsProgress = true;
            backgroundModImportWorker.RunWorkerAsync(null);
            backgroundModDownloadWorker.WorkerReportsProgress = true;
            backgroundModDownloadWorker.RunWorkerAsync(0);
            backgroundModDownloadWorker2.WorkerReportsProgress = true;
            backgroundModDownloadWorker2.RunWorkerAsync(1);
            backgroundNexusInfoDownloadWorker.RunWorkerAsync(null);
            backgroundImageLoader.RunWorkerAsync(null);

            statusUpdateTimer.Start();
            importFileTimer.Start();
            Program.ProgramForm = this;
			LoadOBMMEX();
			lvEspList.ListViewItemSorter=new EspListSorter();
			lvModList.ListViewItemSorter=new omodListSorter();

            lvModList.Columns[0].Width = Properties.Settings.Default.ModNameColumnWidth;
            lvModList.Columns[1].Width = Properties.Settings.Default.ModAuthorColumnWidth;
            lvModList.Columns[2].Width = Properties.Settings.Default.ModVersionColumnWidth;
            lvModList.Columns[3].Width = Properties.Settings.Default.ModNbDataFilesColumnWidth;
            lvModList.Columns[4].Width = Properties.Settings.Default.ModNbPluginsColumnWidth;
            lvModList.Columns[5].Width = Properties.Settings.Default.ModNbPluginsColumnWidth; // does script exist?
            lvModList.Columns[6].Width = Properties.Settings.Default.DatePackedColumnWidth;
            lvModList.Columns[7].Width = Properties.Settings.Default.ConflictColumnWidth;
            lvModList.Columns[8].Width = Properties.Settings.Default.FileSizeColumnWidth;

            if (Properties.Settings.Default.DefaultView == "List")
            {
                radList.Checked = true;
                lvModList.View = View.List;
            }
            else if (Properties.Settings.Default.DefaultView == "Details")
            {
                radDetail.Checked = true;
                lvModList.View = View.Details;
            }

            Program.lvEspList = lvEspList;

			GlobalSettings.LoadSettings();

            if (Program.bSkyrimSEMode)
            {
                bLaunch.Text = "Launch SkyrimSE";
                this.Text = "Tes Mod Manager " + Program.version + " for Skyrim Special Edition (" + Program.gamePath+")";
                if (File.Exists("CreationKit.exe"))
                {
                    btnLaunchCreationKit.Visible = true;
                    this.btnLaunchCreationKit.Text = "Launch Creation Kit";
                }
            }
            else if (Program.bSkyrimMode)
            {
                bLaunch.Text = "Launch Skyrim";
                this.Text = "Tes Mod Manager " + Program.version + " for Skyrim (" + Program.gamePath + ")";
                if (File.Exists("CreationKit.exe"))
                {
                    btnLaunchCreationKit.Visible = true;
                    this.btnLaunchCreationKit.Text = "Launch Creation Kit";
                }
            }
            else if (Program.bMorrowind)
            {
                this.Text = "Tes Mod Manager " + Program.version + " for Morrowind (" + Program.gamePath + ")";
                if (File.Exists("TES Construction Set.exe"))
                {
                    btnLaunchCreationKit.Visible = true;
                    this.btnLaunchCreationKit.Text = "Launch Construction Set";
                }
            }
            else
            {
                this.Text = "Tes Mod Manager " + Program.version + " for Oblivion (" + Program.gamePath + ")";
                if (File.Exists("TesConstructionSet.exe"))
                {
                    btnLaunchCreationKit.Visible = true;
                    this.btnLaunchCreationKit.Text = "Launch Construction Set";
                }
            }
//            int[] nexusversion = GetTESVersion("5010");   <-- this is not reliable on the new web site compared to GetTESVersionMP. Both take too long
//            if (Program.version!=""+nexusversion[0]+"."+nexusversion[1]+"."+nexusversion[2])
//                this.Text += " [ a new version ("+nexusversion+")is available on nexus]";
            UpdateEspList();
			UpdateOmodList();
			UpdateGroups();
			if(Settings.mfSize!=System.Drawing.Size.Empty) Size=Settings.mfSize;
			if(Settings.mfMaximized) WindowState=System.Windows.Forms.FormWindowState.Maximized;
			if(Settings.mfSplitterPos!=0) splitContainer1.SplitterDistance=Settings.mfSplitterPos;
			if(Settings.EspColWidth1!=-1) lvEspList.Columns[0].Width=Settings.EspColWidth1;
			if(Settings.EspColWidth2!=-1) lvEspList.Columns[1].Width=Settings.EspColWidth2;
			cmbEspSortOrder.SelectedIndex=Settings.EspSortOrder;
			cmbOmodSortOrder.SelectedIndex=Settings.omodSortOrder;

            if (Program.BOSSpath!=null && Program.BOSSpath.Length > 0)
                btnBOSSSortPlugins.Visible = true;
            else
                btnBOSSSortPlugins.Visible = false;
            if (Program.LOOTpath != null && Program.LOOTpath.Length > 0)
                btnLootSortPlugins.Visible = true;
            else
                btnLootSortPlugins.Visible = false;
			
			DirectoryInfo ocdlist = new DirectoryInfo(Path.Combine(Program.BaseDir, "ocdlist"));
			
			FileInfo[] rootfiles;

            if (!ocdlist.Exists)
                ocdlist.Create();

			// You should not have ocd definitions in the root folder
            if ((rootfiles = ocdlist.GetFiles("*.xbt")).Length > 0)
			{
				if (MessageBox.Show("You have " + rootfiles.Length.ToString() +
				                    " OCD list definitions in your obmm\\ocdlist root folder. As of version 10.7.17,"+
				                    " this is strongly discouraged.\r\nDelete these? (Recommended)", "Root OCD",
				                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
				{
					for(int i=0;i<rootfiles.Length;i++)
					{
						rootfiles[i].Delete();
					}
				}
			}
			
			
			DirectoryInfo scriptDir = new DirectoryInfo(Path.Combine(Program.BaseDir, "scripting"));
			
			if (scriptDir.Exists)
			{
				System.Security.Policy.Evidence evidence;
				evidence = new System.Security.Policy.Evidence();
				evidence.AddHostEvidence(new System.Security.Policy.Zone(System.Security.SecurityZone.MyComputer));
				
				FileInfo[] sCSharp, sVBNet;
				sCSharp = scriptDir.GetFiles("*.cs");
				sVBNet = scriptDir.GetFiles("*.vb");
				string curd = Path.Combine(Program.BaseDir, "scripting");
				
				bool se = Scripting.DotNetScriptHandler.ShowError;
				Scripting.DotNetScriptHandler.ShowError = scripterrors;
				
				foreach(FileInfo f in sCSharp)
				{
					Scripting.ScriptRunner.ExecuteScript(File.ReadAllText(f.FullName),curd,curd, ScriptType.cSharp, evidence);
				}
				foreach(FileInfo f in sVBNet)
				{
					Scripting.ScriptRunner.ExecuteScript(File.ReadAllText(f.FullName), curd, curd, ScriptType.vb, evidence);
				}
				
				Scripting.DotNetScriptHandler.ShowError = se;
			}
			else
			{
				//MessageBox.Show("Directory \"" + scriptDir.FullName +"\" is missing", "Directory Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				scriptDir.Create();
			}
			
			new DescriptionPanel.DescPanel().Execute(this);
			
			UpdateESPM();

            if (Program.bSkyrimMode)
            {
                if (Program.loadOrderList == null || Program.loadOrderList.Count == 0)
                {
                    Program.loadOrderList = new List<string>();
                    foreach (EspInfo espi in Program.Data.Esps)
                    {
                        Program.loadOrderList.Add(espi.LowerFileName);
                    }
                }
            }

            btnToolTip.SetToolTip(this.btnImport, "Import a file (zip, fomod, etc...) to create a new OMOD mod");
            btnToolTip.SetToolTip(this.bLoad, "Load an existing mod (omod, omod2, fomod) without creating a new OMOD mod");
            btnToolTip.SetToolTip(this.bCreate, "Create a new OMOD mod");
            btnToolTip.SetToolTip(this.bActivate, "(De)Activate the selected mod");
            btnToolTip.SetToolTip(this.bEdit, "Edit the selected mod to create an OMOD mod");

            if (Program.bSkyrimMode)
                runASkyProcPatcherToolStripMenuItem.Visible = true;
            else
                runASkyProcPatcherToolStripMenuItem.Visible = false;

            // check SKSE version
            refreshScriptExtenderVersion();
		}
		bool scripterrors = false;
		#region "Refreshing"
		private void RefreshLists() {
			if(Settings.AutoUpdateConflicts) Conflicts.UpdateConflicts();
			UpdateEspList();
			UpdateOmodList();
		}

        private bool IsSrcriptExtenderUpToDate(string latestSEversion, string currentSEversion)
        {
            bool bRet = false;

            if (latestSEversion.Length > 0)
            {
                // extract various components
                string[] webversion=latestSEversion.Split('.');
                string[] localversion = currentSEversion.Split('.');
                int webmajor = (webversion.Length>0?Convert.ToInt32(webversion[0]):0);
                int webminor=(webversion.Length>1?Convert.ToInt32(webversion[1]):0);
                int webpatch = (webversion.Length > 2 ? Convert.ToInt32(webversion[2]): 0);
                int localmajor = (localversion.Length > 0 ? Convert.ToInt32(localversion[0]) : 0);
                int localminor = (localversion.Length > 1 ? Convert.ToInt32(localversion[1]) : 0);
                int localpatch = (localversion.Length > 2 ? Convert.ToInt32(localversion[2]) : 0);

                if (webmajor == 0)
                {
                    // shift
                    webmajor = webminor;
                    webminor = webpatch;
                    webpatch = 0;
                }
                if (localmajor == 0)
                {
                    localmajor = localminor;
                    localminor = localpatch;
                    localpatch = 0;
                }
                if (webmajor > localmajor)
                    bRet = true;
                else if (webmajor == localmajor && webminor > localminor)
                    bRet = true;
                else if (webmajor == localmajor && webminor == localminor && webpatch > localpatch)
                    bRet = true;

            }
            return bRet;
        }

        private void refreshScriptExtenderVersion()
        {
            string latestSEversion = getLatestScriptExtenderVersion();
            FileVersionInfo scriptextenderfvi = null;
            FileVersionInfo gamever = null;
            if (Program.bSkyrimSEMode)
            {
                //if (File.Exists(Path.Combine(Program.gamePath, "skse_loader.exe")) || File.Exists(Path.Combine(Program.gamePath, "skse_steam_loader.dll")))
                //{
                //    scriptextenderfvi = File.Exists(Path.Combine(Program.gamePath, "skse_loader.exe")) ? FileVersionInfo.GetVersionInfo(Path.Combine(Program.gamePath, "skse_loader.exe")) : FileVersionInfo.GetVersionInfo(Path.Combine(Program.gamePath, "skse_steam_loader.dll"));
                //}
                gamever = FileVersionInfo.GetVersionInfo(Path.Combine(Program.gamePath, "SkyrimSE.exe"));
            }
            else if (Program.bSkyrimMode)
            {
                if (File.Exists(Path.Combine(Program.gamePath, "skse_loader.exe")) || File.Exists(Path.Combine(Program.gamePath, "skse_steam_loader.dll")))
                {
                    scriptextenderfvi = File.Exists(Path.Combine(Program.gamePath, "skse_loader.exe")) ? FileVersionInfo.GetVersionInfo(Path.Combine(Program.gamePath, "skse_loader.exe")) : FileVersionInfo.GetVersionInfo(Path.Combine(Program.gamePath, "skse_steam_loader.dll"));
                }
                gamever = FileVersionInfo.GetVersionInfo(Path.Combine(Program.gamePath, "tesv.exe"));
            }
            else if (Program.bMorrowind)
            {
                gamever = FileVersionInfo.GetVersionInfo(Path.Combine(Program.gamePath, "morrowind.exe"));

                scriptextenderfvi = File.Exists(Path.Combine(Program.gamePath, "mwse.dll")) ? FileVersionInfo.GetVersionInfo(Path.Combine(Program.gamePath, "mwse.dll")) : null;
            }
            else
            {
                scriptextenderfvi = File.Exists(Path.Combine(Program.gamePath, "obse_loader.exe")) ? FileVersionInfo.GetVersionInfo(Path.Combine(Program.gamePath, "obse_loader.exe")) : null;
                gamever = FileVersionInfo.GetVersionInfo(Path.Combine(Program.gamePath, "oblivion.exe"));
            }
            toolStripLblScriptExtenderVersion.Text = "" + Program.gameName + " " + gamever.FileVersion;
            if (scriptextenderfvi != null)
            {
                string currentSEversion = "";
                try
                {
                    string[] cleanver = scriptextenderfvi.FileVersion.Split(new char[] { ',' });
                    if (cleanver.Length > 1)
                        currentSEversion = "" + Convert.ToInt32(cleanver[1]) + "." + Convert.ToInt32(cleanver[2]) + "." + Convert.ToInt32(cleanver[3]);
                    else
                        currentSEversion = cleanver[0];
                }
                catch
                {
                    currentSEversion = scriptextenderfvi.FileVersion;
                }
                toolStripLblScriptExtenderVersion.Text += " - " + (Program.bSkyrimMode ? "SKSE" : (Program.bMorrowind?"MWSE":"OBSE")) + " " + currentSEversion;

                if (IsSrcriptExtenderUpToDate(latestSEversion,currentSEversion)) //latestSEversion.Length > 0 && latestSEversion != currentSEversion)
                {
                    toolStripLblScriptExtenderVersion.ForeColor = Color.Red;
                    if (!Program.bMorrowind)
                        toolStripLblScriptExtenderVersion.Text += " (Click HERE to UPDATE to latest version:" + latestSEversion + ")";
                }
                else
                    toolStripLblScriptExtenderVersion.ForeColor = Color.Green;

            }
            else
            {
                toolStripLblScriptExtenderVersion.ForeColor = Color.Black;
                toolStripLblScriptExtenderVersion.Text += " - No script extender detected"+(!(Program.bMorrowind || Program.bSkyrimSEMode)?" (Click HERE to install)":"");
            }
        }

        private string getLatestScriptExtenderVersion()
        {
            string ver = "";
            if (Program.bMorrowind)
                return ver;

            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                byte[] bytepage = wc.DownloadData((Program.bSkyrimMode ? "http://skse.silverlock.org/" : "http://obse.silverlock.org/"));

                string page = System.Text.Encoding.ASCII.GetString(bytepage).ToString();
                if (Program.bSkyrimMode)
                {
                    if (page.IndexOf("Current build (") != -1)
                    {
                        page = page.Substring(page.IndexOf("Current build (") + "Current build (".Length);
                        page = page.Substring(0, page.IndexOf(","));
                        //int extensionindex = 0;
                        //if (page.IndexOf(".7z") != -1)
                        //    extensionindex = page.IndexOf(".7z");
                        //else if (page.IndexOf(".zip") != -1)
                        //    extensionindex = page.IndexOf(".zip");
                        //else
                        //    extensionindex = page.IndexOf("<"); ; //??

                        //page = page.Substring(0, extensionindex);
                        //page = page.Replace(".silverlock.org/download/", "");
                        //page = page.Replace((Program.bSkyrimMode ? "skse_" : "obse_"), "");
                        char[] separator = { '.' };
                        string[] version = page.Split(separator);


                        foreach (string versionnumber in version)
                        {
                            ver += "" + Convert.ToInt32(versionnumber) + ".";
                        }
                        if (ver.EndsWith("."))
                            ver = ver.Substring(0, ver.Length - 1);
                    }
                }
                else
                {
                    if (page.IndexOf(".silverlock.org/download/") != -1)
                    {
                        page = page.Substring(page.IndexOf(".silverlock.org/download/"));
                        int extensionindex = 0;
                        if (page.IndexOf(".7z") != -1)
                            extensionindex = page.IndexOf(".7z");
                        else if (page.IndexOf(".zip") != -1)
                            extensionindex = page.IndexOf(".zip");
                        else
                            extensionindex = page.IndexOf("<"); ; //??

                        page = page.Substring(0, extensionindex);
                        page = page.Replace(".silverlock.org/download/", "");
                        page = page.Replace((Program.bSkyrimMode ? "skse_" : "obse_"), "");
                        char[] separator = { '_' };
                        string[] version = page.Split(separator);


                        foreach (string versionnumber in version)
                        {
                            ver += "" + Convert.ToInt32(versionnumber) + ".";
                        }
                        if (ver.EndsWith("."))
                            ver = ver.Substring(0, ver.Length - 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.logger.WriteToLog("Could not check for OBSE/SKSE update: "+ex.Message, Logger.LogLevel.Low);
            }
            return ver;
        }

        private string GetMissingDependencies(EspInfo ei)
        {
            string missingDependencies = "";
            if (ei.header.DependsOn != null && ei.header.DependsOn.Length > 0)
            {
                // are dependencies OK?
                string[] deps = ei.header.DependsOn.Split(',');
                bool bFound = false;
                foreach (string dep in deps)
                {
                    if (dep.Length == 0 || dep.ToLower()=="skyrim.esm")
                        continue;
                    foreach (EspInfo ei2 in Program.Data.Esps)
                    {
                        if (ei2.FileName.ToLower() == dep.ToLower() && ei2.Active)
                        {
                            bFound = true;
                            break;
                        }
                    }
                    if (!bFound)
                    {
                        missingDependencies += dep + " ";
                    }
                    else
                        bFound = false;
                }
            }

            return missingDependencies;
        }

		private void UpdateEspList() {
            int idx = lvEspList.Items.IndexOf(lvEspList.TopItem);
            lvEspList.SuspendLayout();
			lvEspList.Items.Clear();
			int ActiveCount=0;
            updatingESPList = true;
			foreach(EspInfo ei in Program.Data.Esps) {
                string toolText = "";

                try
                {
                    try
                    {
                        if (File.Exists(Path.Combine(Program.DataFolderPath,ei.FileName)))
                            ei.header = ConflictDetector.TesFile.GetHeader(Path.Combine(Program.DataFolderPath, ei.FileName));
                        else if (File.Exists(Path.Combine(Program.DataFolderPath,ei.FileName + ".ghost")))
                            ei.header = ConflictDetector.TesFile.GetHeader(Path.Combine(Program.DataFolderPath,ei.FileName + ".ghost"));
                    }
                    catch
                    {
                        continue;
                    }
                    toolText += ei.FileName + "\nAuthor: " + ei.header.Author;

                    if (ei.BelongsTo == "Unknown" || ei.BelongsTo.Contains("Steam"))
                    {
                        string filename = ei.FileName.ToLower();
                        //foreach (string mod in Program.SteamModList)
                        //{
                        //    if (filename == mod)
                        //    {
                        //        // Check DLCList.txt
                        //        ei.BelongsTo = "Steam Workshop";
                        //        break;
                        //    }
                        //}
                        if (Program.SteamModList.ContainsKey(filename))
                        {
                            ei.BelongsTo = Program.SteamModList[filename];

                            string bsa = filename.Replace(".esp", ".bsa").Replace(".esm", ".bsa");
                            if (File.Exists(Path.Combine(Program.DataFolderPath,bsa)))
                            {
                                toolText += "\n\nData file: " + bsa;
                            }
                        }
                        else
                        {
                            // was it part of an omod but we lost the fact that it is installed???
                            foreach (omod mod in Program.Data.omods)
                            {
                                for (int i = 0; i < mod.AllPlugins.Length; i++)
                                {
                                    if (filename == Path.GetFileName(mod.AllPlugins[i]).ToLower())
                                    {
                                        // found the mod!! (check if active assuming no name collision)
                                        mod.AquisitionActivate(false);
                                        mod.markAsActive(ei); // acquisition activate does not find files only on disk
                                        ei.BelongsTo = mod.ModName;
                                    }
                                }
                            }

                        }
                    }
                    string missingDependencies = GetMissingDependencies(ei);
                    bool bDependenciesAreOk = missingDependencies.Length == 0;
                    ListViewItem lvi = new ListViewItem(new string[] { ei.FileName, ei.BelongsTo });
                    if (!bDependenciesAreOk)
                    {
                        lvi.ForeColor = Color.Red;
                        toolText += "\n\nRequired active plugin(s) '" + missingDependencies + "' missing or not active";
                        ei.Active = false;
                        lvi.Checked = false;
                    }
                    else
                    {
                        toolText += "\n\nRequired active plugin(s): " + ei.header.DependsOn;
                    }

                    if (ei.Active)
                    {
                        lvi.Checked = true;
                        toolText += "\n\nFormID: " + (ActiveCount++).ToString("X").PadLeft(2, '0');
                    }
                    toolText += "\n\nBelongs to:" + ei.BelongsTo;
                    if (ei.header.Description != null)
                    {
                        toolText += "\n\n" + ei.header.Description;
                    }
                    lvi.Tag = ei;
                    if (ei.Parent != null)
                    {
                        switch (ei.Parent.Conflict)
                        {
                            case ConflictLevel.Active:
                                lvi.ImageIndex = 0;
                                lvi.ToolTipText = "This mod is active\r\n" + toolText;
                                break;
                            case ConflictLevel.NoConflict:
                                lvi.ImageIndex = 1;
                                lvi.ToolTipText = "This mod has no conflicts\r\n" + toolText;
                                break;
                            case ConflictLevel.MinorConflict:
                                lvi.ImageIndex = 2;
                                lvi.ToolTipText = "This mod has minor conflicts\r\n" + toolText;
                                break;
                            case ConflictLevel.MajorConflict:
                                lvi.ImageIndex = 3;
                                lvi.ToolTipText = "This mod has major conflicts\r\n" + toolText;
                                break;
                            case ConflictLevel.Unusable:
                                lvi.ImageIndex = 4;
                                lvi.ToolTipText = "Unusable mod (plugin name conflict)\r\n" + toolText;
                                break;
                            default:
                                MessageBox.Show("omod had unrecognized conflict level", "Error");
                                lvi.ImageIndex = 1;
                                lvi.ToolTipText = "Unrecognized conflict level\r\n" + toolText;
                                break;
                        }
                    }
                    else
                        lvi.ToolTipText = toolText;
                    lvEspList.Items.Add(lvi);
                }
                catch (Exception ex)
                {
                    Program.logger.WriteToLog("Could not add ESP " + ei.FileName + " to ESP list:" + ex.Message, Logger.LogLevel.Low);
                }
			}
            updatingESPList = false;
			lvEspList.ResumeLayout();
            try { if (idx>-1) lvEspList.TopItem = lvEspList.Items[idx]; }
            catch { };
        }

		private void UpdateOmodList() {
            System.Threading.Monitor.Enter(lvModList);
            string selectedmod = "";
            ListViewItem selectedItem = null;
            int pos = 0;
            ListViewItem topitem = null;
            try
            {
                lvModList.OwnerDraw = true;
                lvModList.SuspendLayout();
                lvModList.ListViewItemSorter = null; // we will sort at the end to speed up list update
                
                
                
                if (lvModList.SelectedIndices.Count > 0)
                {
                    selectedmod = ((omod)lvModList.SelectedItems[0].Tag).ModName;
                    //selectedItem = lvModList.SelectedItems[0];
                    pos = lvModList.SelectedIndices[0];
                }
                else if (lvModList.View == View.Details || lvModList.View == View.List)
                    topitem = lvModList.TopItem;
                else
                    pos = 0;
                lvModList.Items.Clear();
                lvModList.BeginUpdate();
                string modFilter = (cmbGroupFilter.SelectedItem != null ? cmbGroupFilter.SelectedItem.ToString() : "");
                Program.logger.WriteToLog("Refreshing omod list with " + Program.Data.omods.Count + " mods and filter=" + modFilter, Logger.LogLevel.High);
                for (int j = 0; j < Program.Data.omods.Count; j++)
                {
                    omod o = Program.Data.omods[j];
                    Program.logger.WriteToLog("Checking " + o.FileName, Logger.LogLevel.High);
                    if (!File.Exists(Path.Combine(Settings.omodDir,o.LowerFileName)))
                    {
                        Program.Data.omods.Remove(o);
                        //omod.Remove(o.LowerFileName);
                        j--;
                        continue;
                    }
                    if (o.Hidden && !Settings.bShowHiddenMods) continue;
                    if (o.ModName == null) o.ModName = o.FileName;
                    if (!o.ModName.ToLower().Contains(strFilter)) continue;
                    if (cmbGroupFilter.SelectedIndex != 0)
                    {
                        if (cmbGroupFilter.SelectedIndex == 1) // ALL
                        {
                            if (o.group != 0) continue;
                        }
                        else if (modFilter == "[Active mods only]")
                        {
                            if (o.Conflict != ConflictLevel.Active) continue;
                        }
                        else
                        {
                            if ((o.group & (ulong)((ulong)0x01 << (cmbGroupFilter.SelectedIndex - 2))) == 0) continue;
                        }
                    }
                    Program.logger.WriteToLog("Adding " + o.FileName, Logger.LogLevel.High);
                    ListViewItem lvi = new ListViewItem(GlobalSettings.ShowOMODNames ? o.ModName : Path.GetFileNameWithoutExtension(o.FileName));
                    for (int i = 0; i < Settings.omodGroups.Count; i++)
                    {
                        if ((o.group & (ulong)((ulong)0x01 << i)) != 0)
                        {
                            if (Settings.omodGroups[i].Font != null)
                            {
                                lvi.Font = Settings.omodGroups[i].Font;
                            }
                        }
                    }
                    lvi.ToolTipText = o.ModName + "\nAuthor: " + o.Author + "\n" + "Version: " + o.Version + (o.bSystemMod?"\nSystem Mod":"")+"\n\n" + o.Description;
                    lvi.Tag = o;
                    lvi.SubItems.Add(o.Author);
                    lvi.SubItems.Add(o.Version);
                    lvi.SubItems.Add("" + o.AllDataFiles.Length);
                    lvi.SubItems.Add("" + o.AllPlugins.Length);
                    lvi.SubItems.Add("" + (o.bHasScript ? "Yes" : "No"));
                    lvi.SubItems.Add(new FileInfo(Path.Combine(Settings.omodDir,o.LowerFileName)).CreationTime.ToString());
                    lvi.SubItems.Add(o.Conflict.ToString());
                    float filesize = new FileInfo(Path.Combine(Settings.omodDir,o.LowerFileName)).Length;
                    string sfilesize = "";
                    if (filesize / 1024 / 1024 / 1024 > 1)
                        sfilesize = String.Format("{0:F2} GB", filesize / 1024 / 1024 / 1024);
                    else if (filesize / 1024 / 1024 > 1)
                        sfilesize = String.Format("{0:F2} MB", filesize / 1024 / 1024);
                    else
                        sfilesize = String.Format("{0:F2} KB", filesize / 1024);
                    lvi.SubItems.Add(sfilesize);

                    if (o.Website != null && o.Website.Contains("nexusmods/"))
                        o.Website = o.Website.Replace("nexusmods/", "nexusmods.com/");

                    string modid = Program.GetModIDFromWebsite(o.Website); if (modid.Length == 0) modid = Program.GetModID(o.LowerFileName);
                    lvi.SubItems.Add(modid);

                    if (o.bUpdateExists)
                        lvi.ForeColor = Color.Red;

                    if (o.Hidden && lvi.Font.Style != FontStyle.Italic)
                    {
                        lvi.Font = new Font(lvi.Font.Name, lvi.Font.Size, FontStyle.Italic);
                    }
                    else if (!o.Hidden && lvi.Font.Style == FontStyle.Italic)
                    {
                        lvi.Font = new Font(lvi.Font.Name, lvi.Font.Size, FontStyle.Regular);
                    }

                    switch (o.Conflict)
                    {
                        case ConflictLevel.Active:
                            lvi.ImageIndex = 0;
                            lvi.ToolTipText = "This mod is active\r\n" + lvi.ToolTipText;
                            break;
                        case ConflictLevel.NoConflict:
                            lvi.ImageIndex = 1;
                            lvi.ToolTipText = "This mod has no conflicts\r\n" + lvi.ToolTipText;
                            break;
                        case ConflictLevel.MinorConflict:
                            lvi.ImageIndex = 2;
                            lvi.ToolTipText = "This mod has minor conflicts\r\n" + lvi.ToolTipText;
                            break;
                        case ConflictLevel.MajorConflict:
                            lvi.ImageIndex = 3;
                            lvi.ToolTipText = "This mod has major conflicts\r\n" + lvi.ToolTipText;
                            break;
                        case ConflictLevel.Unusable:
                            lvi.ImageIndex = 4;
                            lvi.ToolTipText = "Unusable mod (plugin name conflict)\r\n" + lvi.ToolTipText;
                            break;
                        default:
                            MessageBox.Show("omod had unrecognized conflict level", "Error");
                            lvi.ImageIndex = 1;
                            lvi.ToolTipText = "Unrecognized conflict level\r\n" + lvi.ToolTipText;
                            break;
                    }
                    lvModList.Items.Add(lvi);
                    try
                    {
                        if (o.ModName == selectedmod)
                        {
                            lvi.Selected = true;
                            selectedItem = lvi;
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.logger.WriteToLog("Could not set selected item: " + ex.Message, Logger.LogLevel.Medium);
                    }
                }
                //if (lvModList.Items.Count > pos)
                //{
                //    lvModList.Items[pos].Selected = true;
                //    lvModList.EnsureVisible(pos);
                //    lvModList.s
                //}
                //if ((lvModList.View == View.Details || lvModList.View == View.List) && topitem!=null)
                //    lvModList.TopItem = topitem;
                //else if (selectedItem != null)
                //{
                //    selectedItem.EnsureVisible();
                //}
                //else if (pos != 0 && pos < lvModList.Items.Count)
                //    lvModList.EnsureVisible(pos);
            }
            catch (Exception ex)
            {
                Program.logger.WriteToLog("Exception while refreshing omod list:" + ex.Message, Logger.LogLevel.Low);
            }
            finally
            {
                lvModList.EndUpdate();
                lvModList.ResumeLayout();
                lvModList.OwnerDraw = false;
                lvModList.ListViewItemSorter = new omodListSorter();
            }
            try
            {
                if ((lvModList.View == View.Details || lvModList.View == View.List) && topitem != null)
                {
                    lvModList.TopItem = topitem;
                    topitem.EnsureVisible();
                    foreach (ListViewItem itm in lvModList.Items)
                    {
                        if (itm.Text == topitem.Text)
                        {
                            pos = itm.Index;
                            break;
                        }
                    }
                    lvModList.EnsureVisible(pos);
                }
                else if (selectedItem != null)
                {
                    selectedItem.EnsureVisible();
                }
                else if (pos != 0 && pos < lvModList.Items.Count)
                    lvModList.EnsureVisible(pos);
            }
            catch (Exception ex)
            {
                Program.logger.WriteToLog("Could not reset top item: "+ex.Message, Logger.LogLevel.High);
            }
            System.Threading.Monitor.Exit(lvModList);
            Program.logger.WriteToLog("Done refreshing omod list", Logger.LogLevel.High);

		}
		#endregion
		
		#region "Scripting"
		public void AddExit(System.Windows.Forms.FormClosingEventHandler h)
		{
			fceh.Add(h);
		}
		public ListView ESPList
		{
			get
			{
				return lvEspList;
			}
		}
		public ListView ModList
		{
			get
			{
				return lvModList;
			}
		}
		public SplitContainer MainContainer
		{
			get
			{
				return splitContainer1;
			}
		}
		#endregion
		void LoadOBMMEX()
		{
			if (!File.Exists(Path.Combine(Program.BaseDir, "obmmex.xbt")))
			{
				XSettings.SettingsFolder = new DirectoryInfo(Path.Combine(Program.BaseDir, "xsettings")).FullName;
				return;
			}
			
			ConfigList config = new GeneralConfig().LoadConfiguration(Path.Combine(Program.BaseDir, "obmmex.xbt"));
			
			ConfigPair cp;
			
			if ((cp = config.GetPair(new SV("XSettings.SettingsFolder", false))) != null)
			{
				XSettings.SettingsFolder = new DirectoryInfo(cp.DataAsString).FullName;
			}
			if ((cp = config.GetPair(new SV("CreateModForm.ImageTimeout", false))) != null)
			{
				CreateModForm.ImageTimeout = cp.DataAsInteger;
			}
			if ((cp = config.GetPair(new SV("DebugScripts", false))) != null)
			{
				scripterrors = cp.DataAsBoolean;
			}
		}
		private void lvEspList_ItemCheck(object sender, ItemCheckEventArgs e) {
			EspInfo ei=(EspInfo)lvEspList.Items[e.Index].Tag;
			if(ei.Active==(e.NewValue==CheckState.Checked)) return;
			if(e.NewValue==CheckState.Checked)
            {
                string missingDependencies = GetMissingDependencies(ei);
                if (missingDependencies.Length > 0)
                {
                    MessageBox.Show("This plugin is missing the following plugins(s): "+missingDependencies, "Error");
                    e.NewValue = e.CurrentValue;
                    return;
                }
                else
                {
                    ei.Active = true;
                    OblivionESP.SetActivePlugins();
                    UpdateEspList();
                    return;
                }
			}
			if(ei.Parent!=null) {
				if(ei.Deactivatable==DeactiveStatus.Disallow) {
					MessageBox.Show("This plugin belongs to "+ei.BelongsTo+".\n"+
					                "You must disable the entire mod instead of just this single file.", "Error");
					e.NewValue=e.CurrentValue;
                    return;
                }
                else if (ei.Deactivatable == DeactiveStatus.WarnAgainst && Settings.DefaultEspWarn == DeactiveStatus.WarnAgainst)
                {
					if(MessageBox.Show("This plugin belongs to "+ei.BelongsTo+".\n"+
					                   "It is recommended that you disable the entire mod instead of just this single file.\n"+
					                   "Are you sure you wish to continue?", "Warning", MessageBoxButtons.YesNo)!=DialogResult.Yes) {
						e.NewValue=e.CurrentValue;
                        return;
					}
				}
			}
			((EspInfo)(lvEspList.Items[e.Index].Tag)).Active=false;
            OblivionESP.SetActivePlugins();
            UpdateEspList();
		}
		void UpdateESPM()
		{
			int cnt = 0;
			int omodnum = 0;
			for(int i=0;i<lvEspList.Items.Count;i++)
			{
                if (Program.bSkyrimMode && lvEspList.Items[i].SubItems[0].Text == "Skyrim.esm")
                    lvEspList.Items[i].Checked = true;
                else if (Program.bMorrowind && lvEspList.Items[i].SubItems[0].Text == "Morrowind.esm")
                    lvEspList.Items[i].Checked = true;
                else if (lvEspList.Items[i].SubItems[0].Text == "Oblivion.esm")
                    lvEspList.Items[i].Checked = true;

				if (lvEspList.Items[i].Checked)
				{
					cnt++;
					if (lvEspList.Items[i].SubItems[1].Text != "Unknown")
					{
						omodnum++;
					}
				}
			}
			
			if (cnt == 0)
				lblStatus.Text = "No Active ESPMS";
			else if (cnt < 256)
			{
				lblStatus.Text = "Active ESPMs: " + cnt.ToString() + "\r\nOMod ESPMs: " + omodnum.ToString();
			}
			else
				lblStatus.Text = "Too many active ESPMs!";

            lblStatus.Text += "\r\n" + lvModList.Items.Count + " mods listed";

//            if (!bLoadingForm)
//                OblivionESP.SetActivePlugins();
		}

		private void lvModList_ItemActivate(object sender, EventArgs e) {
            if (sender != null && Settings.bDeActivateOnDoubleClick == false) return; // ignore double-click
			if(lvModList.SelectedItems.Count!=1) return;
			ActivateOmod((omod)lvModList.SelectedItems[0].Tag, false);

            // save mod list
            Program.SaveData();

			RefreshLists();
		}

		private void bCreate_Click(object sender, EventArgs e) {
			if(CreateModForm.ShowForm()) {
				UpdateOmodList();
			}
		}

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.logger.WriteToLog("deleteToolStripMenuItem_Click()", Logger.LogLevel.High);
            if(lvEspList.SelectedItems.Count!=1) return;
            EspInfo ei = (EspInfo)lvEspList.SelectedItems[0].Tag;
            if (ei.BelongsTo != EspInfo.UnknownOwner)
            {
                if (ei.BelongsTo == EspInfo.BaseOwner)
                {
                    MessageBox.Show("That plugin is an essental part of the game and cannot be removed", "Error");
                    return;
                }
                else
                {
                    if (DialogResult.No == MessageBox.Show("That plugin is part of " + ei.BelongsTo + " and will cause the mod to stop functionning properly. Are you sure?", "Warning about "+ei.BelongsTo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                        return;
                }
            }
            if (Settings.WarnOnModDelete && MessageBox.Show("This will delete " + ei.FileName + " permanently.\n" +
                                                         "Are you sure you wish to do this?", "Warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            File.Delete(Path.Combine(Program.DataFolderPath,ei.FileName));
            Program.Data.Esps.Remove(ei);
            lvEspList.Items.RemoveAt(lvEspList.SelectedIndices[0]);
        }

		private void cleanToolStripMenuItem_Click(object sender, EventArgs e) {
            int DeletedCount = 0;
            int SkippedCount = 0;
            int NotFoundCount = 0;
            foreach (ListViewItem lvi in lvModList.SelectedItems)
            {
                ((omod)lvi.Tag).Clean(ref DeletedCount, ref SkippedCount, ref NotFoundCount);
            }
            RefreshLists();
            MessageBox.Show("Files deleted: " + DeletedCount.ToString() + "\n" +
                            "Files skipped: " + SkippedCount.ToString() + "\n" +
                            "Files not found: " + NotFoundCount.ToString(), "Message");
            //if (lvModList.SelectedItems.Count != 1) return;
            //omod o=(omod)lvModList.SelectedItems[0].Tag;
            //if(o.Conflict==ConflictLevel.Active) {
            //    MessageBox.Show(o.ModName+" must be deactivated before it can be cleaned.", "Error");
            //    return;
            //}
            //if(Settings.WarnOnModDelete&&MessageBox.Show("If you clean this mod some files may be permanently deleted.\n"+
            //                                             "Are you sure you wish to continue?", "Warning", MessageBoxButtons.YesNo)!=DialogResult.Yes) {
            //    return;
            //}
            //int DeletedCount=0;
            //int SkippedCount=0;
            //int NotFoundCount=0;
            //o.Clean(ref DeletedCount, ref SkippedCount, ref NotFoundCount);
            //Conflicts.UpdateConflicts();
            //UpdateEspList();
            //UpdateOmodList();
            //MessageBox.Show("Files deleted: "+DeletedCount.ToString()+"\n"+
            //                "Files skipped: "+SkippedCount.ToString()+"\n"+
            //                "Files not found: "+NotFoundCount.ToString(), "Message");
		}

		private void deleteToolStripMenuItem1_Click(object sender, EventArgs e) {
            Program.logger.WriteToLog("deleteToolStripMenuItem1_Click", Logger.LogLevel.High);
            //if(lvModList.SelectedItems.Count!=1) return;
            foreach (ListViewItem lvi in lvModList.SelectedItems)
            {
                omod o = (omod)lvi.Tag; // lvModList.SelectedItems[0].Tag;
                if (MessageBox.Show("This will delete " + o.FileName + " permanently.\n" +
                                   "Are you sure you wish to do this?", "Warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
                if (o.Conflict == ConflictLevel.Active)
                {
                    if (MessageBox.Show(o.ModName + " is still active.\n" +
                                       "A forced disable will be carried out before deletion, but may have side effects.\n" +
                                       "It is recommended that you disable the omod normally before attempting to delete it.\n" +
                                       "Are you sure you wish to continue?", "Warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;
                    o.DeletionDeactivate();
                }
                o.Close();
                Program.Data.omods.Remove(o);
                try
                {
                    File.Delete(Path.Combine(Settings.omodDir, o.FileName));
                }
                catch { };
                lvModList.Items.RemoveAt(lvi.Index); // lvModList.SelectedIndices[0]);
            }
		}

		private void viewReadmeToolStripMenuItem_Click(object sender, EventArgs e) {
			if(lvModList.SelectedItems.Count!=1) return;
			omod o=(omod)lvModList.SelectedItems[0].Tag;
			try {
				string s=o.GetReadme();
				TextEditor te=new TextEditor("View readme", s, true, true);
				if(te.ShowDialog()==DialogResult.Yes) {
					o.ReplaceReadme(te.Result);

				}
			} catch(Exception ex) {
				MessageBox.Show("Error opening readme: "+ex.Message, "Error");
                Program.logger.WriteToLog("Could not load image: " + ex.Message, Logger.LogLevel.Error);
            }
		}

		private void viewScriptToolStripMenuItem_Click(object sender, EventArgs e) {
			if(lvModList.SelectedItems.Count!=1) return;
			omod o=(omod)lvModList.SelectedItems[0].Tag;
			try {
				string s=o.GetScript();
				Forms.ScriptEditor se=new OblivionModManager.Forms.ScriptEditor(s,o.AllPlugins,o.AllDataFiles);
				se.FilesList = o.GetScriptFileList();
				if(se.ShowDialog()==DialogResult.Yes) o.ReplaceScript(se.Result);
			} catch(Exception ex) {
				MessageBox.Show("Error opening script.\n"+ex.Message, "Error");
			}
		}

        // switches the esp at index with the esp at index+1
        private bool SwitchEsps(int index)
        {
            return SwitchEsps(index, false);
        }
		private bool SwitchEsps(int index, bool bSwitchOnly) {
			if(Settings.NeverTouchLoadOrder) {
				MessageBox.Show("You cannot modify load order with the 'never modify load order' option checked", "Error");
				return false;
			}
			if(EspListSorter.order!=EspSortOrder.LoadOrder) {
				MessageBox.Show("Esps must be sorted by load order before you can switch them", "Error");
				return false;
			}
			EspInfo bottom=(EspInfo)lvEspList.Items[index].Tag;
			EspInfo top=(EspInfo)lvEspList.Items[index+1].Tag;
			if(bottom.MustLoadBefore.IndexOf(top.LowerFileName)!=-1) {
				MessageBox.Show(bottom.FileName+" must load before "+top.FileName, "Error");
				return false;
			}
			if(top.MustLoadAfter.IndexOf(bottom.LowerFileName)!=-1) {
				MessageBox.Show(top.FileName+" must load after "+bottom.FileName, "Error");
				return false;
			}
            if (top.LowerFileName.EndsWith(".esp") && bottom.LowerFileName.EndsWith(".esm") && Settings.bPreventMovingESPBeforeESM)
            {
                if (DialogResult.No == MessageBox.Show("An esp should not be loaded before an esm. Allow?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    return false;
                else
                    Settings.bPreventMovingESPBeforeESM = false;
			}

            for (int curmode = 0; curmode < Program.loadOrderList.Count; curmode++)
            {
                if (Program.loadOrderList[curmode] == bottom.LowerFileName)
                {
                    Program.loadOrderList[curmode] = top.LowerFileName;
                }
                else if (Program.loadOrderList[curmode] == top.LowerFileName)
                {
                    Program.loadOrderList[curmode] = bottom.LowerFileName;
                }
            }

            if (!bSwitchOnly)
            {
                try
                {
                    if (Settings.bUseTimeStamps)
                    {
                        Program.logger.WriteToLog("Setting timestamps ", Logger.LogLevel.High);
                        DateTime oldtime = File.GetLastWriteTime(Path.Combine(Program.DataFolderPath, bottom.FileName));
                        File.SetLastWriteTime(Path.Combine(Program.DataFolderPath, bottom.FileName), File.GetLastWriteTime(Path.Combine(Program.DataFolderPath, top.FileName)));
                        string bsa = bottom.LowerFileName; bsa = bsa.Replace(".esm", "").Replace(".esp", "").Replace(".ghost", ""); bsa += ".bsa";
                        if (File.Exists(Path.Combine(Program.DataFolderPath, bsa)))
                            File.SetLastWriteTime(Path.Combine(Program.DataFolderPath, bsa), File.GetLastWriteTime(Path.Combine(Program.DataFolderPath, top.FileName)));
                        File.SetLastWriteTime(Path.Combine(Program.DataFolderPath, top.FileName), oldtime);
                        bsa = top.LowerFileName; bsa = bsa.Replace(".esm", "").Replace(".esp", "").Replace(".ghost", ""); bsa += ".bsa";
                        if (File.Exists(Path.Combine(Program.DataFolderPath, bsa)))
                            File.SetLastWriteTime(Path.Combine(Program.DataFolderPath, bsa), oldtime);
                        Program.logger.WriteToLog("Done setting timestamps ", Logger.LogLevel.High);
                    }
                }
                catch (Exception ex)
                {
                    //                MessageBox.Show("Unable to set timestamps on files: " + ex.Message, "Cannot set timestamps", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    Program.logger.WriteToLog("Unable to set timestamps on files: " + ex.Message, Logger.LogLevel.Error);
                }
                Program.Data.SortEsps();
                // save loadorder
                OblivionESP.SetActivePlugins();
            }

            ListViewItem lvi = lvEspList.Items[index];
            lvEspList.Items.RemoveAt(index);
            lvEspList.Items.Insert(index + 1, lvi);

			return true;
		}

		private void bMoveUp_Click(object sender, EventArgs e) {
			/*if(lvEspList.SelectedItems.Count!=1) return;
			if(lvEspList.SelectedIndices[0]==0) return;
			SwitchEsps(lvEspList.SelectedIndices[0]-1);*/
            Program.logger.WriteToLog("bMoveUp_Click for " + lvEspList.SelectedIndices.Count + " plugins", Logger.LogLevel.High);
			
			if (lvEspList.SelectedIndices.Count > 0)
			{
				int[] listcopy = new int[lvEspList.SelectedIndices.Count];
				
				for(int i=0;i<listcopy.Length;i++)
				{
					listcopy[i] = lvEspList.SelectedIndices[i];
				}
				
				Array.Sort(listcopy);
				
				if (listcopy[listcopy.Length-1] > 0)
				{
					for(int i=0;i<listcopy.Length;i++)
					{
						if (!SwitchEsps(listcopy[i]-1))
							break;
					}
				}
			}
            //UpdateEspList();
            Program.logger.WriteToLog("bMoveUp_Click done", Logger.LogLevel.High);
        }

		private void moveToTopToolStripMenuItem_Click(object sender, EventArgs e) {
            int[] listcopy = new int[lvEspList.SelectedIndices.Count];
            Program.logger.WriteToLog("moveToTopToolStripMenuItem_Click for " + listcopy.Length + " plugins", Logger.LogLevel.High);
            for (int i = 0; i < listcopy.Length; i++)
            {
                listcopy[i] = lvEspList.SelectedIndices[i];
            }
            Array.Sort(listcopy);
            for (int i = 0; i < listcopy.Length; i++)
            {
                int position = listcopy[i];
                while (position != i)
                {
                    if (!SwitchEsps(--position)) break;
                }
            }
            Program.logger.WriteToLog("moveToTopToolStripMenuItem_Click done", Logger.LogLevel.High);
        }

		private void bMoveDown_Click(object sender, EventArgs e) {
//			if(lvEspList.SelectedItems.Count!=1) return;
			/*if(lvEspList.SelectedItems.Count!=1) return;
			if(lvEspList.SelectedIndices[0]==lvEspList.Items.Count-1) return;
			SwitchEsps(lvEspList.SelectedIndices[0]);*/

            Program.logger.WriteToLog("bMoveDown_Click for " + lvEspList.SelectedIndices.Count+"plugins", Logger.LogLevel.High);
            if (lvEspList.SelectedIndices.Count > 0)
			{
				int[] listcopy = new int[lvEspList.SelectedIndices.Count];
				
				for(int i=0;i<listcopy.Length;i++)
				{
					listcopy[i] = lvEspList.SelectedIndices[i];
				}
				
				Array.Sort(listcopy);
				
				if (listcopy[listcopy.Length-1] < lvEspList.Items.Count-1)
				{
					for(int i=listcopy.Length-1;i>=0;i--)
					{
						if (!SwitchEsps(listcopy[i]))
							break;
					}
				}
			}
            //UpdateEspList();
            Program.logger.WriteToLog("bMoveDown_Click done", Logger.LogLevel.High);
        }

		private void moveToBottomToolStripMenuItem_Click(object sender, EventArgs e) {
//			if(lvEspList.SelectedItems.Count!=1) return;
            Program.logger.WriteToLog("moveToBottomToolStripMenuItem_Click for " + lvEspList.SelectedIndices.Count + "plugins", Logger.LogLevel.High);
            int[] listcopy = new int[lvEspList.SelectedIndices.Count];
            for (int i = 0; i < listcopy.Length; i++)
            {
                listcopy[i] = lvEspList.SelectedIndices[i];
            }
            Array.Sort(listcopy);
            for (int i = listcopy.Length - 1, j=0; i > -1; i--,j++)
            {
                int position = listcopy[i];
                while (position < lvEspList.Items.Count - 1 - j)
                {
                    if (!SwitchEsps(position++)) break;
                }
            }
            //UpdateEspList();
            Program.logger.WriteToLog("moveToBottomToolStripMenuItem_Click done", Logger.LogLevel.High);
        }

		private void viewDataFilesToolStripMenuItem_Click(object sender, EventArgs e) {
			if(lvEspList.SelectedItems.Count==0) return;
			System.Collections.Generic.List<string> toadd=new System.Collections.Generic.List<string>();
			foreach(ListViewItem lvi in lvEspList.SelectedItems) {
				try {
					toadd.AddRange(ConflictDetector.TesFile.GetDataFileList(Path.Combine(Program.DataFolderPath,lvi.Text)));
				} catch(Exception ex) {
					MessageBox.Show("An error occurred trying to parse plugin "+lvi.SubItems[1].Text+"\n"+
					                "Error: "+ex.Message, "Error");
				}
			}
			//Remove duplicates
			int index;
			for(int i=0;i<toadd.Count;i++) {
				while((index=toadd.LastIndexOf(toadd[i]))>i) toadd.RemoveAt(index);
			}
			string text=string.Join(Environment.NewLine, toadd.ToArray());
			(new TextEditor("Required data files", text, false, false)).ShowDialog();
		}

		private void unlinkToolStripMenuItem_Click(object sender, EventArgs e) {
			if(lvEspList.SelectedItems.Count==0) return;
			bool MadeChanges=false;
			foreach(ListViewItem lvi in lvEspList.SelectedItems) {
				EspInfo ei=(EspInfo)lvi.Tag;
				if(ei.Parent!=null) {
					ei.Unlink();
					MadeChanges=true;
				}
			}
			if(MadeChanges) UpdateEspList();
		}

		private void lvEspList_KeyDown(object sender, KeyEventArgs e) {
			if(e.Alt) {
				if(e.KeyCode==Keys.Up) {
					bMoveUp_Click(null, null);
					e.Handled=true;
				} else if(e.KeyCode==Keys.Down) {
					bMoveDown_Click(null, null);
					e.Handled=true;
				}
			}
		}

		private void lvModList_SelectedIndexChanged(object sender, EventArgs e) {
			if(lvModList.SelectedItems.Count!=1) return;
			omod o=(omod)lvModList.SelectedItems[0].Tag;
            currentlySelectedOmod = o;
			//group filter
			foreach(ToolStripMenuItem tsmi in GroupToolStripMenuItem.DropDownItems) {
				if(tsmi==GroupNoneToolStripMenuItem) continue;
				if((o.group&(ulong)((ulong)0x01<<(int)tsmi.Tag))>0) {
					tsmi.Checked=true;
				} else {
					tsmi.Checked=false;
				}
			}
			//menus
            addPictureToolStripMenuItem.Visible = false;
            addInfoFromTesNexusToolStripMenuItem.Visible = false;
            if (o.DoesReadmeExist()) viewReadmeToolStripMenuItem.Text = "View readme";
            else
            {
                viewReadmeToolStripMenuItem.Text = "Create readme";
                addInfoFromTesNexusToolStripMenuItem.Visible = true;
            }
			if(o.DoesScriptExist()) viewScriptToolStripMenuItem.Text="View script";
			else
            {
                viewScriptToolStripMenuItem.Text="Create script";
                addInfoFromTesNexusToolStripMenuItem.Visible = true;
            }
            if (o.Website == "") visitWebsiteToolStripMenuItem.Enabled = false;
			else visitWebsiteToolStripMenuItem.Enabled=true;
			if(o.Email=="") emailAuthorToolStripMenuItem.Enabled=false;
			else emailAuthorToolStripMenuItem.Enabled=true;

            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose(); // clear image loaded if any
                pictureBox1.Image = null;
            }

            if (o.IsImageCached && o.image!=null)
            {
                try
                {
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }
                    pictureBox1.Image = o.image;
                }
                catch
                { };
            }
            else
            {
                System.Threading.Monitor.Enter(Program.OmodImagePreloadList);
                if (!Program.OmodImagePreloadList.Contains(o))
                    Program.OmodImagePreloadList.Add(o);
                System.Threading.Monitor.Exit(Program.OmodImagePreloadList);
            }
            //if (!backgroundImageLoader.IsBusy)
            //{
            //    //backgroundImageLoader.CancelAsync();
            //    backgroundImageLoader.RunWorkerAsync(o);
            //}
            //else
            //if (o.image == null)
            //{
            //    addPictureToolStripMenuItem.Visible = true;
            addInfoFromTesNexusToolStripMenuItem.Visible = true;
            //}
            ////image
            //try
            //{
            //    pictureBox1.Image = o.image;
            //}
            //catch (Exception ex)
            //{
            //    Program.logger.WriteToLog("Could not load image: " + ex.Message, Logger.LogLevel.Low);
            //}
			//activation button
			switch(o.Conflict) {
				case ConflictLevel.Active:
					bActivate.Text="Deactivate";
					bActivate.Enabled=true;
					break;
					/*case ConflictLevel.Unusable:
                bActivate.Text="Activate";
                bActivate.Enabled=false;
                break;*/
				default:
					bActivate.Text="Activate";
					bActivate.Enabled=true;
					break;
			}
		}

		private void bActivate_Click(object sender, EventArgs e) {
			lvModList_ItemActivate(null, null);
		}

		private void bConflict_Click(object sender, EventArgs e) {
			//(new TextEditor("Conflict report", Conflicts.UpdateConflictsWithReport(), false, false)).ShowDialog();
			ConflictDetector.ReportGenerator.GenerateReport();
		}

		private void infoToolStripMenuItem_Click(object sender, EventArgs e) {
			if(lvModList.SelectedItems.Count!=1) return;
			omod o=(omod)lvModList.SelectedItems[0].Tag;
			string report=o.GetInfo();
			
			(new TextEditor(o.FileName+" info", report, false, false)).ShowDialog();
		}

		private void bLoad_Click(object sender, EventArgs e) {
			OpenDialog.Title="Select mod files to load";
			OpenDialog.Filter="all files|*.*";
			OpenDialog.Multiselect=true;
			if(OpenDialog.ShowDialog()==DialogResult.OK)
            {
                foreach (string s in OpenDialog.FileNames)
                {
                    Program.LoadNewOmod(s);
                    Program.SaveData();
                }
				UpdateOmodList();
			}
		}

		private void visitWebsiteToolStripMenuItem_Click(object sender, EventArgs e) {
            //if(lvModList.SelectedItems.Count!=1) return;
            foreach (ListViewItem lvi in lvModList.SelectedItems)
            {
                omod o = (omod)lvi.Tag; //(omod)lvModList.SelectedItems[0].Tag;
                if (o.Website == "")
                {
                    MessageBox.Show(o.FileName + " does not have a website", "Error");
                    return;
                }
                string s = o.Website;
                if (!s.ToLower().StartsWith("http://")) s = "http://" + s;
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(s);
                if (p != null) p.Close();
            }
		}

		private void emailAuthorToolStripMenuItem_Click(object sender, EventArgs e) {
			if(lvModList.SelectedItems.Count!=1) return;
			omod o=(omod)lvModList.SelectedItems[0].Tag;
			if(o.Email=="") {
				MessageBox.Show(o.FileName+" does not have an email address", "Error");
				return;
			}
			string s="mailto:"+o.Email+"?subject="+o.FileName;
			System.Diagnostics.Process p=System.Diagnostics.Process.Start(s);
			if(p!=null) p.Close();
		}

		private void bHelp_Click(object sender, EventArgs e) {
			try {
				System.Diagnostics.Process.Start(Program.HelpPath);
			} catch(Exception ex) {
				MessageBox.Show("An error occured while trying to open the help file.\n"+ex.Message, "Error");
			}
		}

		private void cmbEspSortOrder_SelectedIndexChanged(object sender, EventArgs e) {
			EspListSorter.order=(EspSortOrder)cmbEspSortOrder.SelectedIndex;
			if(EspListSorter.order!=EspSortOrder.LoadOrder) {
                lvEspList.AllowDrop = false;
				bMoveDown.Enabled=false;
				bMoveUp.Enabled=false;
				moveDownToolStripMenuItem.Enabled=false;
				moveUpToolStripMenuItem.Enabled=false;
			} else {
                lvEspList.AllowDrop = true;
                bMoveDown.Enabled = true;
				bMoveUp.Enabled=true;
				moveDownToolStripMenuItem.Enabled=true;
				moveUpToolStripMenuItem.Enabled=true;
			}
			lvEspList.Sort();
		}

		private void bAbout_Click(object sender, EventArgs e) {
			(new About()).ShowDialog();
			if(Program.Launch!=LaunchType.None) Close();
		}

		private void bLaunch_Click(object sender, EventArgs e) {
            // make sure that everything is sync'ed
            OblivionESP.SetActivePlugins(); 
            if (Settings.ShowLaunchWarning)
            {
				MessageBox.Show(
					"This is a public service announcement, due to the huge number of people who completely missunderstand what this button is for\n"+
					"Basically, YOU DO NOT HAVE TO USE THIS BUTTON FOR TMM TO WORK!!!\n"+
					"Launching the game via the normal launcher will not magically make obmm lose it's load order or anything.\n"+
					"If you have obse or skse installed, this button will launch obse instead of oblivion or skse instead of Skyrim, so the two are not incompatible.\n"+
					"If you want to override this behaviour, or launch a custom program, you can just edit the command line box in the settings\n"+
					"To prove that you've read this, hold down shift while clicking OK so that this message will not appear again.", "Message");
				if(Program.KeyPressed(16))
                    Settings.ShowLaunchWarning=false;
				else return;
			}
			Program.Launch=LaunchType.Game;
			Close();
		}

		private void bEdit_Click(object sender, EventArgs e) {
			if(lvModList.SelectedItems.Count!=1) return;
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
			if(CreateModForm.ShowForm((omod)lvModList.SelectedItems[0].Tag)) {
				UpdateOmodList();
			}
		}

		private void pictureBox1_Click(object sender, EventArgs e) {
			if(pictureBox1.Image!=null) (new ImageForm(pictureBox1.Image)).ShowDialog();
		}

		private void TidyDataFolder() {
            Program.logger.WriteToLog("TidyDataFolder()", Logger.LogLevel.High);
            System.Collections.Generic.List<DirectoryInfo> dis = new System.Collections.Generic.List<DirectoryInfo>();
			bool match=true;
			try
			{
				while(match)
				{
					dis.Clear();
					dis.Add(new DirectoryInfo(Program.DataFolderPath));
					
					match=false;
					for(int i=0;i<dis.Count;i++)
					{
						DirectoryInfo[] temp=dis[i].GetDirectories();
						if(temp.Length!=0)
						{
							dis.AddRange(temp);
						}
						else
						{
							if(dis[i].GetFileSystemInfos().Length==0)
							{
                                Program.logger.WriteToLog("..Cleaning up "+dis[i].Name, Logger.LogLevel.High);
                                dis[i].Delete();
								dis.RemoveAt(i--);
								match=true;
							}
						}
					}
				}
			}
			catch(Exception ex)
			{
                Program.logger.WriteToLog("Could not tidy data folder: " + ex.Message, Logger.LogLevel.Low);				
			}
		}
		
		
		
		
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            bClosingForm = true;
			TidyDataFolder();
			Settings.mfSize=Size;
			if(WindowState==FormWindowState.Maximized) Settings.mfMaximized=true;
			else Settings.mfMaximized=false;
			Settings.mfSplitterPos=splitContainer1.SplitterDistance;
			Settings.EspColWidth1=lvEspList.Columns[0].Width;
			Settings.EspColWidth2=lvEspList.Columns[1].Width;
			Settings.EspSortOrder=cmbEspSortOrder.SelectedIndex;
			Settings.omodSortOrder=cmbOmodSortOrder.SelectedIndex;
			
			GlobalSettings.SaveSettings();

            Properties.Settings.Default.MainFormX = this.Location.X;
            Properties.Settings.Default.MainFormY= this.Location.Y;

            Properties.Settings.Default.MainFormW=this.Width;
            Properties.Settings.Default.MainFormH=this.Height;
            Properties.Settings.Default.Save();
			
			foreach(FormClosingEventHandler h in fceh)
			{
				h.Invoke(sender, e);
			}

            backgroundNexusModUpdateChecker.CancelAsync();

            OblivionESP.SetActivePlugins();
		}

		private void bSettings_Click(object sender, EventArgs e) {
			(new OptionsForm()).ShowDialog();
			UpdateGroups();
		}

		private void IgnoreKeyPress(object sender, KeyPressEventArgs e) {
			e.Handled=true;
		}

		private void cmbOmodSortOrder_SelectedIndexChanged(object sender, EventArgs e) {
			omodListSorter.order=(omodSortOrder)cmbOmodSortOrder.SelectedIndex;
			lvModList.Sort();
		}

		private void cmbGroupFilter_SelectedIndexChanged(object sender, EventArgs e) {
			if(cmbGroupFilter.SelectedIndex==cmbGroupFilter.Items.Count-1) {
				cmbGroupFilter.SelectedIndex=0;
				bSettings_Click(null, null);
			} else {
				UpdateOmodList();
			}
		}

		private void UpdateGroups() {
			GroupToolStripMenuItem.DropDownItems.Clear();
			GroupToolStripMenuItem.DropDownItems.Add(GroupNoneToolStripMenuItem);
			for(int i=0;i<Settings.omodGroups.Count;i++) {
				ToolStripMenuItem tsmi=new ToolStripMenuItem(Settings.omodGroups[i].Name);
				tsmi.Tag=i;
				tsmi.Click+=GroupNoneToolStripMenuItem_Click;
				GroupToolStripMenuItem.DropDownItems.Add(tsmi);
			}
			cmbGroupFilter.Items.Clear();
			cmbGroupFilter.Items.Add("All");
			cmbGroupFilter.Items.Add("Unassigned");
			cmbGroupFilter.Items.AddRange(Settings.omodGroups.ToArray());
            cmbGroupFilter.Items.Add("[Active mods only]");
            cmbGroupFilter.Items.Add("[Edit groups]");
			cmbGroupFilter.SelectedIndex=0;
		}

		private void GroupNoneToolStripMenuItem_Click(object _sender, EventArgs e) {
            ToolStripMenuItem sender = (ToolStripMenuItem)_sender;
            sender.Checked = !sender.Checked;
            foreach (ListViewItem lvi in lvModList.SelectedItems)
            {
                omod o = (omod)lvi.Tag;
                if (sender == GroupNoneToolStripMenuItem)
                {
                    o.group = 0;
//                    lvModList_SelectedIndexChanged(null, null);
//                    return;
                }
                else if (sender.Checked)
                {
                    o.group |= (ulong)((ulong)1 << (int)sender.Tag);
                }
                else
                {
                    o.group &= 0xFFFFFFFFFFFFFFFF - (ulong)((ulong)1 << (int)sender.Tag);
                }
            }
            UpdateOmodList();
            //if (lvModList.SelectedItems.Count != 1) return;
            //ToolStripMenuItem sender=(ToolStripMenuItem)_sender;
            //omod o=(omod)lvModList.SelectedItems[0].Tag;
            //if(sender==GroupNoneToolStripMenuItem) {
            //    o.group=0;
            //    lvModList_SelectedIndexChanged(null, null);
            //    return;
            //}
            //sender.Checked=!sender.Checked;
            //if(sender.Checked) {
            //    o.group|=(ulong)((ulong)1<<(int)sender.Tag);
            //} else {
            //    o.group&=0xFFFFFFFFFFFFFFFF - (ulong)((ulong)1<<(int)sender.Tag);
            //}
		}

		private void aquisitionActivateToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (ListViewItem lvi in lvModList.SelectedItems)
            {
                omod o = (omod)lvi.Tag;
                if (o.Conflict != ConflictLevel.Active) o.AquisitionActivate(false);
            }
            UpdateOmodList();
//            if (lvModList.SelectedItems.Count != 1) return;
//			omod o=(omod)lvModList.SelectedItems[0].Tag;
//			if(o.Conflict==ConflictLevel.Active) return;
//			o.AquisitionActivate(true);
//			UpdateOmodList();
		}

		private void simulateToolStripMenuItem_Click(object sender, EventArgs e) {
			if(lvModList.SelectedItems.Count!=1) return;
			omod o=(omod)lvModList.SelectedItems[0].Tag;
			if(o.Conflict==ConflictLevel.Active) return;

			string[] plugins=o.AllPlugins;
			DataFileInfo[] dfis=o.AllDataFiles;
			string[] dataFiles=new string[dfis.Length];
			for(int i=0;i<dfis.Length;i++) dataFiles[i]=dfis[i].FileName;
			ScriptReturnData srd;
			try {
				srd=Scripting.ScriptRunner.SimulateScript(o.GetScript(), dataFiles, plugins);
			} catch(Exception ex) {
				MessageBox.Show("The script did not complete successfully\n"+ex.Message);
				return;
			}
			Forms.SimResults simForm=new Forms.SimResults(srd, plugins, dataFiles);
			simForm.ShowDialog();
		}

		private void forceDisableToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (ListViewItem lvi in lvModList.SelectedItems)
            {
                omod o=(omod)lvi.Tag;
                if(o.Conflict==ConflictLevel.Active) o.Deactivate(true);
            }
            RefreshLists();
            //if (lvModList.SelectedItems.Count != 1) return;
            //omod o=(omod)lvModList.SelectedItems[0].Tag;
            //if(o.Conflict==ConflictLevel.Active) o.Deactivate(true);
            //RefreshLists();
		}

		private void viewDataConflictsToolStripMenuItem_Click(object sender, EventArgs e) {
			if(lvModList.SelectedItems.Count!=1) return;
			omod o=(omod)lvModList.SelectedItems[0].Tag;
			(new TextEditor("data file and script conflicts", Conflicts.ConflictReport(o), false, false)).ShowDialog();
		}

		private void convertToArchiveToolStripMenuItem_Click(object sender, EventArgs e) {
			SaveOmodDialog.Title="Save omod as";
			SaveOmodDialog.Filter="7-zip (*.7z)|*.7z|zip (*.zip)|*.zip";
			SaveOmodDialog.AddExtension=false;
			SaveOmodDialog.OverwritePrompt=false;
			try {
				if(lvModList.SelectedItems.Count!=1) return;
				omod o=(omod)lvModList.SelectedItems[0].Tag;
				SaveOmodDialog.FileName=Path.GetFileNameWithoutExtension(o.FileName);
				if(SaveOmodDialog.ShowDialog()!=DialogResult.OK) return;
				string ZipName=SaveOmodDialog.FileName;
				if(SaveOmodDialog.FilterIndex==1) {
					if(Path.GetExtension(ZipName).ToLower()!=".7z") ZipName+=".7z";
				} else {
					if(Path.GetExtension(ZipName).ToLower()!=".zip") ZipName+=".zip";
				}
				if(File.Exists(ZipName)&&MessageBox.Show("File '"+ZipName+"' already exists.\n"+"Do you wish to overwrite?", "Warning", MessageBoxButtons.YesNo)!=DialogResult.Yes) return;
				bool CreateConversionData=MessageBox.Show("Create omod conversion information?","Question", MessageBoxButtons.YesNo)==DialogResult.Yes;
				string path=o.GetDataFiles();
				string temppath=null;
				if(path==null) {
					path=o.GetPlugins();
					if(path==null) path=Program.CreateTempDirectory();
				} else {
					temppath=o.GetPlugins();
					if(temppath!=null) {
						foreach(string s in Directory.GetFiles(temppath, "*", SearchOption.AllDirectories)) {
							string esppath=s.Substring(temppath.Length);
							Directory.CreateDirectory(Path.Combine(path, Path.GetDirectoryName(esppath)));
							File.Move(s, Path.Combine(path, esppath));
						}
					}
				}
				if(o.DoesReadmeExist()) {
					File.WriteAllText(Path.Combine(Path.Combine(path,o.ModName),"_readme.rtf"), o.GetReadme());
				}
				if(CreateConversionData) {
					Directory.CreateDirectory(Path.Combine(path, Program.omodConversionData));
					if(o.DoesScriptExist()) {
						File.WriteAllText(Path.Combine(Path.Combine(path, Program.omodConversionData),"script.txt"), o.GetScript());
					}
					File.WriteAllText(Path.Combine(Path.Combine(path, Program.omodConversionData),"info.txt"), o.GetInfo());
					temppath=o.GetImage();
					if(temppath!=null) {
                        //System.Drawing.Imaging.ImageFormat imf=o.image.RawFormat;
						File.Move(temppath, Path.Combine(Path.Combine(path, Program.omodConversionData),"screenshot"));
					}
					temppath=o.GetConfig();
					File.Move(temppath, Path.Combine(Path.Combine(path, Program.omodConversionData),"config"));
				}
				if(SaveOmodDialog.FilterIndex==1) {
					System.Diagnostics.ProcessStartInfo psi=new System.Diagnostics.ProcessStartInfo(Path.Combine(Program.BaseDir, "7zr.exe"));
					psi.Arguments="a -t7z -mx="+(Settings.CompressionBoost?"9":"7")+" \""+ZipName+"\" \""+path+"*\"";
					psi.CreateNoWindow=true;
					psi.UseShellExecute=false;
					System.Diagnostics.Process p=System.Diagnostics.Process.Start(psi);
					p.WaitForExit();
				} else {
					ICSharpCode.SharpZipLib.Zip.FastZip fs=new ICSharpCode.SharpZipLib.Zip.FastZip();
					fs.CreateZip(ZipName, path, true, null);
				}
			} catch(Exception ex) {
				MessageBox.Show("Conversion failed\nError: "+ex.Message, "Error");
				return;
			}
			MessageBox.Show("Conversion successful", "Message");
		}

		private void extractToFolderToolStripMenuItem_Click(object sender, EventArgs e) {
			if(lvModList.SelectedItems.Count!=1) return;
			omod o=(omod)lvModList.SelectedItems[0].Tag;
			folderBrowserDialog1.Description="Select the path to extract "+o.FileName+" to";
			if(folderBrowserDialog1.ShowDialog()!=DialogResult.OK) return;
			if(Directory.Exists(folderBrowserDialog1.SelectedPath+"\\"+o.ModName)) {
				MessageBox.Show("Unable to extract to that directory.\n"+"The path '"+folderBrowserDialog1.SelectedPath+"\\"+o.ModName+"' already exists", "Error");
				return;
			}
			try {
				bool CreateConversionData=MessageBox.Show("Create omod conversion information?","Question", MessageBoxButtons.YesNo)==DialogResult.Yes;
				string path=o.GetDataFiles();
				string temppath=null;
				if(path==null) {
					path=o.GetPlugins();
					if(path==null) path=Program.CreateTempDirectory();
				} else {
					temppath=o.GetPlugins();
					if(temppath!=null) {
						foreach(string s in Directory.GetFiles(temppath, "*", SearchOption.AllDirectories)) {
							string esppath=s.Substring(temppath.Length);
							Directory.CreateDirectory(Path.Combine(path,Path.GetDirectoryName(esppath)));
							File.Move(s, Path.Combine(path,esppath));
						}
					}
				}
				if(o.DoesReadmeExist()) {
					File.WriteAllText(Path.Combine(Path.Combine(path, o.ModName),"_readme.rtf"), o.GetReadme());
				}
				if(CreateConversionData) {
					Directory.CreateDirectory(Path.Combine(path, Program.omodConversionData));
					if(o.DoesScriptExist()) {
						File.WriteAllText(Path.Combine(Path.Combine(path, Program.omodConversionData),"script.txt"), o.GetScript());
					}
					File.WriteAllText(path+Program.omodConversionData+"info.txt", o.GetInfo());
					temppath=o.GetImage();
					if(temppath!=null) {
                        //System.Drawing.Imaging.ImageFormat imf=o.image.RawFormat;
						File.Move(temppath, Path.Combine(Path.Combine(path, Program.omodConversionData),"screenshot"));
					}
					temppath=o.GetConfig();
					if (temppath.Length>0) File.Move(temppath, Path.Combine(Path.Combine(path, Program.omodConversionData),"config"));
				}
				Program.RecersiveDirectoryMove(path, Path.Combine(folderBrowserDialog1.SelectedPath,o.ModName), false);
				MessageBox.Show("Done");
			} catch(Exception ex) {
				MessageBox.Show("Extraction failed\nError: "+ex.Message, "Error");
			}
		}

		private void exportOmodConversionDataToolStripMenuItem_Click(object sender, EventArgs e) {
			if(lvModList.SelectedItems.Count!=1) return;
			omod o=(omod)lvModList.SelectedItems[0].Tag;
			folderBrowserDialog1.Description="Select the path to extract "+o.FileName+" to";
			if(folderBrowserDialog1.ShowDialog()!=DialogResult.OK) return;
			if(Directory.Exists(Path.Combine(folderBrowserDialog1.SelectedPath,folderBrowserDialog1.SelectedPath))) {
				MessageBox.Show("Unable to extract to that directory.\n"+
				                "The path '"+ Path.Combine(folderBrowserDialog1.SelectedPath,folderBrowserDialog1.SelectedPath)+"' already exists", "Error");
				return;
			}
			try {
				string path=Path.Combine(folderBrowserDialog1.SelectedPath, Program.omodConversionData)+"\\";
				string temppath;
				Directory.CreateDirectory(path);
				if(o.DoesScriptExist()) {
					File.WriteAllText(Path.Combine(path,"script.txt"), o.GetScript());
				}
				File.WriteAllText(Path.Combine(path,"info.txt"), o.GetInfo());
				temppath=o.GetImage();
				if(temppath!=null) {
                    //System.Drawing.Imaging.ImageFormat imf=o.image.RawFormat;
					File.Move(temppath, Path.Combine(path,"screenshot"));
				}
				temppath=o.GetConfig();
				File.Move(temppath, Path.Combine(path,"config"));
			} catch(Exception ex) {
				MessageBox.Show("Extraction failed\nError: "+ex.Message, "Error");
			}
		}

		private void bSaves_Click(object sender, EventArgs e) {
			(new SaveForm()).ShowDialog();
            UpdateEspList();

// trying to sort
//            lvEspList.Sort(); 
//            OblivionESP.SetActivePlugins();
            
		}

		private void PipeFileWatcher_Created(object sender, FileSystemEventArgs e) {
            try
            {
                System.Threading.Thread.Sleep(300); //Give the other process a chance to close the file
                //			if(Application.OpenForms.Count>1) {
                //				MessageBox.Show("You can only import new files when obmm is displaying the data file list", "Error");
                //			} else {
                string[] lines = File.ReadAllLines(Program.PipeFilename);
                Program.RunCommandLine(lines);
                //foreach(string s in mods) Program.LoadNewOmod(s);
                UpdateOmodList();
                //			}
                File.Delete(Path.Combine(Program.PipeFilename));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to receive file link. Please try again. (Reason: "+ex.Message+")","Error retrieving download link",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
		}

		private void bUtilities_Click(object sender, EventArgs e) {
			cmsUtilitiesMenu.Show(bUtilities, 0, 22);
		}

		private void bSABrowserToolStripMenuItem_Click(object sender, EventArgs e) {
			(new BSABrowser()).ShowDialog();
		}

		private void loadOrderToolStripMenuItem_Click(object sender, EventArgs e) {
			System.Text.StringBuilder sb=new System.Text.StringBuilder();
			foreach(EspInfo ei in Program.Data.Esps) {
				if(ei.Active) sb.Append(ei.FileName+Environment.NewLine);
			}
			new TextEditor("Load order", sb.ToString(), false, false).ShowDialog();
		}

		private void omodReportToolStripMenuItem_Click(object sender, EventArgs e) {
			SaveOmodDialog.Title="Save report as";
			SaveOmodDialog.Filter="text file|*.txt";
			SaveOmodDialog.AddExtension=true;
			SaveOmodDialog.OverwritePrompt=true;
			if(SaveOmodDialog.ShowDialog()==DialogResult.OK) {
				StreamWriter sw=new StreamWriter(SaveOmodDialog.FileName);
				char t='\t';
				foreach(omod o in Program.Data.omods) {
					sw.WriteLine(o.FileName+t+o.ModName+t+o.Version+t+
					             o.Author+t+o.Email+t+o.Website+t+o.CreationTime.ToString());
				}
				sw.Close();
			}
		}

		private void ActivateOmod(omod mod, bool force) {
			if(mod.Conflict!=ConflictLevel.Active) Conflicts.UpdateConflict(mod);
			bool warn=!force;
			switch(mod.Conflict) {
				case ConflictLevel.Active:
					if(!force) {
						if(!Settings.WarnOnModDelete||MessageBox.Show("Deactivate "+mod.FileName+"?", "Question", MessageBoxButtons.YesNo)==DialogResult.Yes) {
							mod.Deactivate(false);
						}
					}
					return;
				case ConflictLevel.MajorConflict:
					if(!force) {
						switch(MessageBox.Show(mod.FileName+" conflicts with an already active mod.\n"+
						                       "Do you wish to be warned about any file conflicts?", "Warning", MessageBoxButtons.YesNoCancel)) {
								case DialogResult.Yes: warn=true; break;
								case DialogResult.No: warn=false; break;
								case DialogResult.Cancel: return;
						}
					}
					break;
				case ConflictLevel.Unusable:
					if(!force) {
						switch(MessageBox.Show(mod.FileName+" will overwrite one or more existing esps.\n"+
						                       "Activating this mod could cause serious problems with your game.\n"+
						                       "Please use the 'view data conflicts' option to ensure this mod is not doing anything you don't expect it to before you continue\n"+
						                       "Do you wish to be warned about any file conflicts?", "Warning", MessageBoxButtons.YesNoCancel)) {
								case DialogResult.Yes: warn=true; break;
								case DialogResult.No: warn=false; break;
								case DialogResult.Cancel: return;
						}
					}
					break;
			}
			try {
				mod.Activate(warn);
			} catch(Exception ex) {
				MessageBox.Show("An error occurred while activating the mod.\n"+
				                "It may not have been activated completely.\n"+
				                "Error: "+ex.Message, "Error");
			}
		}

		private void activateAllToolStripMenuItem_Click(object sender, EventArgs e) {
			foreach(ListViewItem lvi in lvModList.Items) {
				ActivateOmod((omod)lvi.Tag, true);
			}
			RefreshLists();
		}

		private void deactivateAllToolStripMenuItem_Click(object sender, EventArgs e) {
			foreach(ListViewItem lvi in lvModList.Items) {
				((omod)lvi.Tag).Deactivate(true);
			}
			RefreshLists();
		}

		private void activateAllToolStripMenuItem1_Click(object sender, EventArgs e) {
			foreach(ListViewItem lvi in lvEspList.Items) {
				((EspInfo)lvi.Tag).Active=true;
				lvi.Checked=true;
			}
			UpdateEspList();
		}

		private void deactivateAllToolStripMenuItem1_Click(object sender, EventArgs e) {
			foreach(ListViewItem lvi in lvEspList.Items) {
				if(((EspInfo)lvi.Tag).BelongsTo==EspInfo.BaseOwner) continue;
				if(((EspInfo)lvi.Tag).Deactivatable==DeactiveStatus.Disallow) continue;
				((EspInfo)lvi.Tag).Active=false;
				lvi.Checked=false;
			}
		}

		private void cleanAllToolStripMenuItem_Click(object sender, EventArgs e) {
			if(MessageBox.Show("This option will disable all mods and permanently delete all data files and esps which are linked to omods.\n"+
			                   "Are you sure you wish to continue?", "Warning", MessageBoxButtons.YesNo)!=DialogResult.Yes) {
				return;
			}
			//Deactivate everything
			foreach(omod o in Program.Data.omods) o.Deactivate(true);
			//Clear up any left over obmm stuff
			Program.Data.BSAs.Clear();
			Program.Data.DataFiles.Clear();
			Program.Data.INIEdits.Clear();
			for(int i=0;i<Program.Data.Esps.Count;i++) {
				if(Program.Data.Esps[i].Parent!=null) Program.Data.Esps.RemoveAt(i--);
			}
			//Clear out the data folder
			foreach(omod o in Program.Data.omods) o.Clean();
			TidyDataFolder();
			RefreshLists();
		}

		private void cleanFilteredToolStripMenuItem_Click(object sender, EventArgs e) {
			int DeletedCount=0;
			int SkippedCount=0;
			int NotFoundCount=0;
			foreach(ListViewItem lvi in lvModList.Items) {
				((omod)lvi.Tag).Clean(ref DeletedCount, ref SkippedCount, ref NotFoundCount);
			}
			RefreshLists();
			MessageBox.Show("Files deleted: "+DeletedCount.ToString()+"\n"+
			                "Files skipped: "+SkippedCount.ToString()+"\n"+
			                "Files not found: "+NotFoundCount.ToString(), "Message");
		}

		private void tidyDataFolderToolStripMenuItem_Click(object sender, EventArgs e) { TidyDataFolder(); }

		private void bBatch_Click(object sender, EventArgs e) {
			BatchContextMenu.Show(bBatch, 0, 0);
		}

		private void bImport_Click(object sender, EventArgs e) {
			ImportContextMenu.Show(bImport, 0, 0);
            UpdateOmodList();
		}

		private void exportOmodListToolStripMenuItem_Click(object sender, EventArgs e) {
			SaveOmodDialog.Title="Save active omod list as";
			SaveOmodDialog.Filter="Active omod list (*.oaml)|*.oaml";
			SaveOmodDialog.AddExtension=false;
			SaveOmodDialog.OverwritePrompt=false;
			if(SaveOmodDialog.ShowDialog()!=DialogResult.OK) return;
			if(Path.GetExtension(SaveOmodDialog.FileName).ToLower()!=".oaml") SaveOmodDialog.FileName+=".oaml";
			if(File.Exists(SaveOmodDialog.FileName)&&MessageBox.Show("File '"+SaveOmodDialog.FileName+"' already exists.\n"+
			                                                         "Do you wish to overwrite?", "Warning", MessageBoxButtons.YesNo)!=DialogResult.Yes) return;
			BinaryWriter bw=new BinaryWriter(File.Create(SaveOmodDialog.FileName));
			foreach(omod o in Program.Data.omods) {
				if(o.Conflict==ConflictLevel.Active) bw.Write(o.CRC);
			}
			bw.Close();
		}

		private void exportLoadOrderToolStripMenuItem_Click(object sender, EventArgs e) {
			SaveOmodDialog.Title="Save load order data as";
			SaveOmodDialog.Filter="Load order data (*.olod)|*.olod";
			SaveOmodDialog.AddExtension=false;
			SaveOmodDialog.OverwritePrompt=false;
			if(SaveOmodDialog.ShowDialog()!=DialogResult.OK) return;
			if(Path.GetExtension(SaveOmodDialog.FileName).ToLower()!=".olod") SaveOmodDialog.FileName+=".olod";
			if(File.Exists(SaveOmodDialog.FileName)&&MessageBox.Show("File '"+SaveOmodDialog.FileName+"' already exists.\n"+
			                                                         "Do you wish to overwrite?", "Warning", MessageBoxButtons.YesNo)!=DialogResult.Yes) return;
			BinaryWriter bw=new BinaryWriter(File.Create(SaveOmodDialog.FileName));
			foreach(EspInfo ei in Program.Data.Esps) {
				bw.Write(ei.LowerFileName);
				bw.Write(ei.DateModified.ToBinary());
			}
			bw.Close();
		}

		private void importOmodListToolStripMenuItem_Click(object sender, EventArgs e) {
			OpenDialog.Title="Select active omod list to import";
			OpenDialog.Filter="Active omod list (*.oaml)|*.oaml";
			OpenDialog.Multiselect=false;
			if(OpenDialog.ShowDialog()!=DialogResult.OK) return;
			BinaryReader br=new BinaryReader(File.OpenRead(OpenDialog.FileName));
			uint[] crcs=new uint[br.BaseStream.Length/4];
			int i=0;
			while(br.BaseStream.Position<br.BaseStream.Length) {
				crcs[i++]=br.ReadUInt32();
			}
			br.Close();
			foreach(omod o in Program.Data.omods) {
				if(o.Conflict==ConflictLevel.Active) {
					if(Array.IndexOf<uint>(crcs, o.CRC)==-1) o.Deactivate(true);
				}
			}
			foreach(omod o in Program.Data.omods) {
				if(o.Conflict!=ConflictLevel.Active) {
					try {
						if(Array.IndexOf<uint>(crcs, o.CRC)!=-1) o.Activate(false);
					} catch {
						MessageBox.Show("An error occured activating "+o.FileName, "Error");
					}
				}
			}
			RefreshLists();
		}

		private void importLoadOrderToolStripMenuItem_Click(object sender, EventArgs e) {
			OpenDialog.Title="Select load order data file to import";
			OpenDialog.Filter="Load order data (*.olod)|*.olod";
			OpenDialog.Multiselect=false;
			if(OpenDialog.ShowDialog()!=DialogResult.OK) return;


            Program.loadOrderList.Clear();
			BinaryReader br=new BinaryReader(File.OpenRead(OpenDialog.FileName));
			while(br.BaseStream.Position<br.BaseStream.Length) {
				string file=br.ReadString();
				DateTime pos=DateTime.FromBinary(br.ReadInt64());
				EspInfo ei=Program.Data.GetEsp(file);
				if(ei!=null) {
					ei.DateModified=pos;
					FileInfo fi=new FileInfo(Path.Combine(Program.DataFolderPath,file));
					fi.LastWriteTime=pos;
                    if (Program.bSkyrimMode)
                        Program.loadOrderList.Add(file.ToLower());
				} else {
					MessageBox.Show("esp '"+file+"' was not found", "Error");
				}
			}
			br.Close();
			Program.Data.SortEsps();
			UpdateEspList();
		}

		private void bSACreatorToolStripMenuItem_Click(object sender, EventArgs e) {
			//MessageBox.Show("The BSA creator is incomplete, and does not generate working BSAs", "Warning");
			(new Forms.BSACreator()).ShowDialog();
		}

		private void archiveInvalidationToolStripMenuItem_Click(object sender, EventArgs e) {
			(new Forms.ArchiveInvalidation()).ShowDialog();
		}

		private void deleteBackupsToolStripMenuItem_Click(object sender, EventArgs e) {
            Program.logger.WriteToLog("deleteBackupsToolStripMenuItem_Click()", Logger.LogLevel.High);
			bool error=false;
			foreach(string s in Directory.GetFiles(Program.BackupDir, "*.*")) {
				try { File.Delete(s); } catch { error=true; }
			}
			foreach(string s in Directory.GetFiles(Program.CorruptDir, "*.*")) {
				try { File.Delete(s); } catch { error=true; }
			}
			if(error) {
				MessageBox.Show("An error occured, and one or more files could not be deleted.\n"+
				                "Make sure that none of the files in the backup or corrupt folders are write protected.",
				                "Error");
			}
		}

		private void editToolStripMenuItem_Click(object sender, EventArgs e) {
			bEdit_Click(null, null);
		}

		private void updateConflictsToolStripMenuItem_Click(object sender, EventArgs e) {
			Conflicts.UpdateConflicts();
			UpdateEspList();
			UpdateOmodList();
		}

		private void exportOmodGroupInformationToolStripMenuItem_Click(object sender, EventArgs e) {
			SaveOmodDialog.Title="Save group information as";
			SaveOmodDialog.Filter="omod group information (*.ogi)|*.ogi";
			SaveOmodDialog.AddExtension=true;
			SaveOmodDialog.OverwritePrompt=true;
			if(SaveOmodDialog.ShowDialog()!=DialogResult.OK) return;

			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter;
			formatter=new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

			BinaryWriter bw=new BinaryWriter(File.Create(SaveOmodDialog.FileName));
			bw.Write(Settings.omodGroups.Count);
			foreach(omodGroup og in Settings.omodGroups) { og.Write(bw, formatter); }
			bw.Write(Program.Data.omods.Count);
			foreach(omod o in Program.Data.omods) { bw.Write(o.LowerFileName); bw.Write(o.group); }
			bw.Close();
		}

		private void importOmodGroupInfoToolStripMenuItem_Click(object sender, EventArgs e) {
			OpenDialog.Title="Select omod group information to import";
			OpenDialog.Filter="Active omod list (*.ogi)|*.ogi";
			OpenDialog.Multiselect=false;
			if(OpenDialog.ShowDialog()!=DialogResult.OK) return;
			Settings.omodGroups.Clear();
			foreach(omod o in Program.Data.omods) {
				o.group=0;
			}

			BinaryReader br=new BinaryReader(File.OpenRead(OpenDialog.FileName));
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter;
			formatter=new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			int count;

			count=br.ReadInt32();
			for(int i=0;i<count;i++) Settings.omodGroups.Add(omodGroup.Read(br, formatter));
			count=br.ReadInt32();
			for(int i=0;i<count;i++) {
				omod o=Program.Data.GetMod(br.ReadString());
				ulong group=br.ReadUInt64();
				if(o!=null) o.group=group;
			}
			br.Close();

			UpdateOmodList();
			UpdateGroups();
		}

		private void bSAFixerToolStripMenuItem_Click(object sender, EventArgs e) {
			OpenDialog.Title="Select BSA(s) to check";
			OpenDialog.Filter="BethesdaSoftworks archive (*.bsa)|*.bsa";
			OpenDialog.Multiselect=true;
			if(OpenDialog.ShowDialog()!=DialogResult.OK) return;
			Classes.BSAUncorrupter.BSAFixResult result=Classes.BSAUncorrupter.ScanBSA(OpenDialog.FileNames);
			string message;
			if(OpenDialog.FileNames.Length>1) message=OpenDialog.FileNames.Length.ToString()+" files checked";
			else message="'"+OpenDialog.FileName+"' checked";
			message+="\nFolder found: "+result.foldersFound+
				"\nCorrupted folders found and fixed: "+result.foldersFixed+
				"\nFiles found: "+result.filesFound+
				"\nCorrupted files found and fixed: "+result.filesFixed+
				"\nFiles possibly still corrupted: "+result.Failed;
			MessageBox.Show(message, "Results");
		}

		private void dataFileBrowserToolStripMenuItem_Click(object sender, EventArgs e) {
			bool b=MessageBox.Show("Do you want to include files packed inside BSAs?\n"+
			                       "Warning: Choosing 'Yes' will drastically slow down this utility", "Question", MessageBoxButtons.YesNo)==DialogResult.Yes;
			(new Forms.DataFileBrowser(b)).ShowDialog();
		}

		private void conflictReportToolStripMenuItem1_Click(object sender, EventArgs e) {
			(new ConflictReport.NewReportGenerator()).ShowDialog();
		}

		private void rescanEspHeadersToolStripMenuItem_Click(object sender, EventArgs e) {
			foreach(EspInfo ei in Program.Data.Esps) { if(ei.Parent==null) { ei.GetHeader(); } }
			UpdateEspList();
		}

		private void hideInactiveFToolStripMenuItem_Click(object sender, EventArgs e) {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            foreach (ListViewItem lvi in lvModList.Items)
            {
				omod o=(omod)lvi.Tag;
				if(o.Conflict!=ConflictLevel.Active) o.Hide();
			}
			UpdateOmodList();
		}

		private void aquisitionActivateFilteredToolStripMenuItem_Click(object sender, EventArgs e) {
			foreach(ListViewItem lvi in lvModList.Items) {
				omod o=(omod)lvi.Tag;
				if(o.Conflict!=ConflictLevel.Active) o.AquisitionActivate(false);
			}
			UpdateOmodList();
		}

		private void hiddenOmodSwitcherToolStripMenuItem_Click(object sender, EventArgs e) {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            new Forms.omodEnabler().ShowDialog();
			UpdateOmodList();
		}

		private void hideToolStripMenuItem_Click(object sender, EventArgs e) {
//			if(lvModList.SelectedItems.Count!=1) return;
            foreach (ListViewItem lvi in lvModList.SelectedItems)
            {
                omod o = (omod)lvi.Tag;
                if (!o.Hidden)
                    o.Hide();
                else
                    o.Show();
            }
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            UpdateOmodList();
        }

		private void nifViewerToolStripMenuItem_Click(object sender, EventArgs e) {
			if(!File.Exists(Path.Combine(Program.BaseDir, "NifViewer.exe"))) {
				MessageBox.Show("The nifviewer utility could not be found","Error");
				return;
			}
			/*try {
                System.Reflection.Assembly asm=System.Reflection.Assembly.ReflectionOnlyLoad(@"Microsoft.DirectX.Direct3DX, Version=1.0.2911.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
            } catch {
                MessageBox.Show("You do not have the required version of MDX installed.\n"+
                    "Please install the october 2005 DirectX update or newer, and ensure the MDX component is selected to be installed.", "Error");
            }*/
			System.Diagnostics.Process.Start(Path.Combine(Program.BaseDir, "NifViewer.exe"));
		}

		private void lvEspList_GiveFeedback(object sender, GiveFeedbackEventArgs e) {
			Point p=lvEspList.PointToClient(Form.MousePosition);
			ListViewItem lvi=lvEspList.GetItemAt(p.X, p.Y);
			if(lvi==null) lvEspList.SelectedIndices.Clear();
			else lvi.Selected=true;
		}

		private void lvEspList_DragDrop(object sender, DragEventArgs e) {
            Program.logger.WriteToLog("lvEspList_DragDrop - HoverSelection=" + lvEspList.HoverSelection, Logger.LogLevel.High);
            lvEspList.MultiSelect = false;
            if (lvEspList.SelectedIndices.Count != 1) return;
			int toswap=(int)e.Data.GetData(typeof(int)) - 1;
			if(toswap==-1) return;
			int swapwith=lvEspList.SelectedIndices[0];
			if(toswap==swapwith) return;
			if(swapwith>toswap) {
				for(int i=0;i<swapwith-toswap;i++) {
					if(!SwitchEsps(toswap+i, true)) break;
				}
			} else {
				for(int i=0;i<toswap-swapwith;i++) {
					if(!SwitchEsps(toswap-(i+1),true)) break;
				}
			}
            Program.Data.SortEsps();
            // save loadorder
            OblivionESP.SetActivePlugins();
		}
		
		private bool DragDropInProgress=false;
		private void lvEspList_ItemDrag(object sender, ItemDragEventArgs e) {
            lvEspList.MultiSelect = false;
            Program.logger.WriteToLog("lvEspList_ItemDrag - HoverSelection=" + lvEspList.HoverSelection, Logger.LogLevel.High);
            if (lvEspList.SelectedIndices.Count != 1 || e.Button != MouseButtons.Left) return;
			if(EspListSorter.order!=EspSortOrder.LoadOrder) return;
			DragDropInProgress=true;
			lvEspList.DoDragDrop(lvEspList.SelectedIndices[0]+1, DragDropEffects.Move);
            lvEspList.MultiSelect = true;
        }

		private void lvEspList_DragEnter(object sender, DragEventArgs e) {
            lvEspList.MultiSelect = false;
            if (!DragDropInProgress) return;
			e.Effect=DragDropEffects.Move;
			DragDropInProgress=false;
            Program.logger.WriteToLog("lvEspList_DragEnter - HoverSelection="+ lvEspList.HoverSelection, Logger.LogLevel.High);
		}
		
		void OCDCreatorToolStripMenuItemClick(object sender, EventArgs e)
		{
			new OCDForm().Show();
		}
		
		int VersionCompare(int maj1, int min1, int bui1, int maj2, int min2, int bui2)
		{
			return (maj1 > maj2) ? 1 : (maj1 == maj2 ? ((min1 > min2) ? 1 : ((min2 == min1) ? ((bui1 > bui2) ? 1 : ((bui1 == bui2) ? 0 : -1)) : -1)) : -1);
		}
        //static TextReader DownloadFile(string url, bool bSilent)
        //{
        //    if (!bSilent)
        //    {
        //        Stream s = DownloadForm.DownloadFile(url, bSilent);
        //        if (s != null)
        //            return new StreamReader(s);
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        System.Net.WebClient wc = new System.Net.WebClient();
        //        MemoryStream ms = new MemoryStream(wc.DownloadData(url));
        //        TextReader tr = new StreamReader(ms);
        //        return tr;
        //    }
        //    //fullpath = Path.Combine(dir, filename);
        //    //System.Net.WebClient webClient = new System.Net.WebClient();
        //    //Uri uri = new Uri(url);
        //    //webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
        //    //webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadFileCompletedCallback);
        //    //webClient.DownloadFileAsync(uri, fullpath);
        //    ////            webClient.DownloadFile(response, fullpath);
        //    //bool bDone = false;
        //    //int lastprogress = 0;
        //    //progress = 0;
        //    //System.Threading.Thread.Sleep(10);
        //    //while (!bDone) //  && !pf.bCancelled) //  && webClient.IsBusy
        //    //{
        //    //    System.Threading.Thread.Sleep(10);
        //    //    Application.DoEvents();

        //    //    if (progress == -1)
        //    //        bDone = true;
        //    //    else if (progress > lastprogress)
        //    //    {
        //    //        lastprogress = progress;
        //    //        downloadBackGroundWorker.ReportProgress(progress);
        //    //        //    pf.UpdateProgress(20 + progress / 4);
        //    //    }
        //    //}
        //    ////pf.UpdateProgress(45);
        //    //downloadBackGroundWorker.ReportProgress(progress);

        //    //webClient.Dispose();


        //}
		void CheckForUpdatesToolStripMenuItemClick(object sender, EventArgs e)
		{
            try
            {
                //if(lvModList.SelectedItems.Count!=1) return;
                foreach (ListViewItem item in lvModList.SelectedItems)
                {
                    //omod o = (omod)lvModList.SelectedItems[0].Tag;
                    omod o = (omod)item.Tag;
                    if (CheckForUpdate(o.Website, o.ModName, o.Version, true, false))
                    {
                        o.bUpdateExists = true;
                        //lvModList.SelectedItems[0].ForeColor = Color.Red;
                        item.ForeColor = Color.Red;
                    }
                    else
                    {
                        o.bUpdateExists = false;
                        //lvModList.SelectedItems[0].ForeColor = Color.Black;
                        item.ForeColor = Color.Black;
                    }
                }
            }
            catch { };
		}
//        string GetVersionAL(string Website, bool verbose, bool bSilent)
//        {
//            string version = "";

//            if(Website=="")
//            {
//                return "";
//            }
//            string s=Website.Replace("http://tesnexus.com", "http://www.tesnexus.com");


//            s = s.Replace("http://www.tesnexus.com/downloads/file.php?id=", "http://www.oblivion.nexusmods.com/mods/");
//            s = s.Replace("http://www.skyrimnexus.com/downloads/file.php?id=", "http://www.skyrim.nexusmods.com/mods/");

//            if (s.StartsWith("http://www.oblivion.nexusmods.com/mods/", StringComparison.CurrentCultureIgnoreCase) ||
//                s.StartsWith("http://www.skyrim.nexusmods.com/mods/", StringComparison.CurrentCultureIgnoreCase))
//            {
//                version = Program.GetTESVersion(s, bSilent);

//                if (version == null || version=="")
//                    return "";
	
////                if (version!=o.Version)
//////				if (VersionCompare(o.CreationTime.Year, o.CreationTime.Month, o.CreationTime.Day, tver[0], tver[1], tver[2]) == -1)
////                if (verbose)
////                {
////                    DialogResult dlgres= MessageBox.Show("An update is available for " + o.ModName + ". Do you want to download it now?", "Update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
////                    if ( dlgres == DialogResult.Yes)
////                    {
////                        System.Diagnostics.Process.Start(o.Website);
////                    }
////                    else if (dlgres == DialogResult.Cancel)
////                    {
////                        throw new Exception("User cancelled check"); ;
////                    }

////                }
//            }
//            else
//            {
////				if (verbose)
////					MessageBox.Show("This mod does not use TES Nexus as its website", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            return version;
//        }
		bool CheckForUpdate(string Website, string modName, string modVersion, bool verbose, bool bSilent)
		{
            //if (o == null || o.Website==null)
            //    return false;

            bool bUpdateExists = false;

            string s = Website.ToLower();
            if (s.Length == 0)
			{
                // check filename for TesNexus id
                s = Program.GetModID(modName);
                if (s.Length == 0)
                {
                    //				if (verbose)
                    //					MessageBox.Show(o.FileName+" does not have a website", "Error");
                    return false;
                }
                s = "http://www.nexusmods.com/" + Program.gameName + "/mods/" + s;
			}
            s=s.Replace("http://tesnexus.com", "http://www.tesnexus.com");

            if (Program.bSkyrimMode)
                s=s.Replace("tesnexus", "skyrimnexus");

            s = s.Replace("http://skyrimnexus", "http://www.skyrimnexus");

            s = s.Replace("http://www.tesnexus.com/downloads/file.php?id=", "http://www.nexusmods.com/oblivion/mods/");
            s = s.Replace("http://www.skyrimnexus.com/downloads/file.php?id=", "http://www.nexusmods.com/skyrim/mods/");
            s = s.Replace("http://www." + Program.gameName + ".nexusmods.com/mods/", "http://www.nexusmods.com/" + Program.gameName + "/mods/");

            if (s.StartsWith("http://www.nexusmods.com/oblivion/mods/", StringComparison.CurrentCultureIgnoreCase) ||
                s.StartsWith("http://www.nexusmods.com/skyrim/mods/", StringComparison.CurrentCultureIgnoreCase))
			{
                string modid=s.Substring(s.LastIndexOf('/')+1);
                string name = "", version = "", description = "", author = "", website = "", imagefile = null;
                try
                {
                    Program.GetNexusModInfo(modid, ref name, ref version, ref description, ref author, ref website, ref imagefile, true);
                }
                catch (Exception ex)
                {
                    Program.logger.WriteToLog("Could not check for update for mod ID=" + modid, Logger.LogLevel.Low);
                    throw ex;
                }

                if (version!="File not found" && version!="File hidden" && version.Length>0 && version.ToLower().CompareTo(modVersion.ToLower()) != 0)
                {
                    bUpdateExists = true;
                    if (verbose)
                    {
                        DialogResult dlgres = MessageBox.Show("An update to '" + version + "' is available for " + modName + ". Do you want to download it now?", "Update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (dlgres == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Website);
                        }
                        else if (dlgres == DialogResult.Cancel)
                        {
                            throw new Exception("User cancelled check"); ;
                        }
                    }
                }
                else
                {
                    bUpdateExists = false;
                }

                //string tver = Program.GetTESVersionMP(s, bSilent);
				
                //if (tver == null || tver =="")
                //{
                //    //tver= GetVersionAL(Website, verbose, bSilent);
                //    tver = Program.GetTESVersion(modid, bSilent);
                //}

                //if (tver == "File not found")
                //{
                //    if (verbose)
                //    {
                //        DialogResult dlgres = MessageBox.Show(modName + " was not found on "+Website, "Mod not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    }
                //}
                //else if (tver == "File hidden")
                //{
                //    // no need to say anything really
                //}
                //else if (tver.Trim().ToLower() != modVersion.Trim().ToLower() && tver.Length > 0) // did it change?
                //{
                //    if (verbose)
                //    {
                //        DialogResult dlgres = MessageBox.Show("An update to '" + tver + "' is available for " + modName + ". Do you want to download it now?", "Update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                //        if (dlgres == DialogResult.Yes)
                //        {
                //            System.Diagnostics.Process.Start(Website);
                //        }
                //        else if (dlgres == DialogResult.Cancel)
                //        {
                //            throw new Exception("User cancelled check"); ;
                //        }
                //    }
                //    else
                //        return true;
                //}
			}
			else
			{
//				if (verbose)
//					MessageBox.Show("This mod does not use Nexus as its website", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bUpdateExists = false;
			}
            return bUpdateExists;
		}
        //string GetTESVersionMP(string fileid, bool bSilent)
        //{
        //    try
        //    {
        //        if (fileid != null)
        //        {
        //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
					
        //            for(int i=0;i<fileid.Length;i++)
        //            {
        //                char c = fileid[i];
						
        //                if ("0123456789".IndexOf(c) != -1)
        //                    sb.Append(c);
        //            }
        //            fileid = sb.ToString();
        //        }
				
        //        if (fileid != null && fileid.Length > 0)
        //        {
        //            TextReader tr;
        //            tr = DownloadFile("http://"+(Program.bSkyrimMode ? "skyrim":"oblivion") +".nexusmods.com/mods/"+ fileid, bSilent); // DownloadFile("http://www.tesnexus.com/downloads/file.php?id=" + fileid);
        //            string modVersion = null;
        //            string modName = "";
        //            string modAuthor = "";
        //            string modImage = null;

        //            Program.getNexusInfo(fileid, tr, ref modName, ref modVersion, ref modAuthor, ref modImage, bSilent);
        //            tr.Close();

        //            return modVersion;
        //        }
				
        //        return null;
        //    }
        //    catch(Exception ex)
        //    {
        //        Program.logger.WriteToLog("Could not get TES Version: " + ex.Message, Logger.LogLevel.Low);
        //        return null;
        //    }
        //}
//        public static string GetTESVersion(string fileid, bool bSilent)
//        {
//            string version = "";
////			CreateModForm.getNexusInfo(fileid,tr,ref modNamw, ref modVersion, ref modAuthor, ref modImage)
//            try
//            {
//                int[] tvers = new int[] {-1,-1,-1};
//                if (fileid != null)
//                {
//                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
					
//                    for(int i=0;i<fileid.Length;i++)
//                    {
//                        char c = fileid[i];
						
//                        if ("0123456789".IndexOf(c) != -1)
//                            sb.Append(c);
//                    }
//                    fileid = sb.ToString();
//                }
				
//                if (fileid != null && fileid.Length > 0)
//                {
//                    TextReader tr;
//                    tr = DownloadFile((!Program.bSkyrimMode ? "http://www.oblivion.nexusmods.com/ajax/modactionlog/?id=" : "http://www.skyrim.nexusmods.com/ajax/modactionlog/?id=") + fileid, bSilent);
//                    bool bVersion = false;
//                    string lastline = "";
//                    string line = "";
//                    while ((line = tr.ReadLine()) != null && !bVersion)
//                    {
//                        if (line.EndsWith("</li>"))
//                        {
//                            if (line.Contains("file version changed to"))
//                            {
//                                version = line.Substring(line.IndexOf('f'));
//                                version = version.Substring(version.IndexOf('\'') + 1);
//                                if (version.IndexOf('\'') != -1)
//                                    version = version.Substring(0, version.IndexOf('\''));
//                                version.Replace("'", "");
//                                //string[] stvers = version.Split('.');
//                                //tvers[0] = Convert.ToInt32(stvers[0]);
//                                //tvers[1] = Convert.ToInt32(stvers[1]);
//                                //tvers[2] = Convert.ToInt32(stvers[2]);
//                                bVersion = true;
//                            }
//                            if (!bVersion && line.Replace("\t", "").Trim().StartsWith("New file:"))
//                            {
//                                lastline = lastline.Replace("<span>", "").Replace("</span>", "").Trim().Replace("\t", "");
//                                DateTime convertedDate = new DateTime(); ;
//                                try { convertedDate = DateTime.Parse(lastline); }
//                                catch { };

//                                tvers[0] = convertedDate.Year;
//                                tvers[1] = convertedDate.Month;
//                                tvers[2] = convertedDate.Day;
//                                version = "" + convertedDate.Year + "." + convertedDate.Month + "." + convertedDate.Day;
//                            }
//                        }
//                        lastline = line;
//                    }

//                    return version;
//                }
				
//                return null;
//            }
//            catch(Exception)
//            {
//                return null;
//            }
//        }
		
		void CheckForModUpdatesToolStripMenuItemClick(object sender, EventArgs e)
		{
            //for(int i=0;i<lvModList.Items.Count;i++)
            //{
            //    if (lvModList.Items[i].Tag != null)
            //    {
            //        Color pbc = lvModList.Items[i].BackColor;
            //        Color pfc = lvModList.Items[i].ForeColor;
            //        lvModList.Items[i].BackColor = Color.Blue;
            //        lvModList.Items[i].ForeColor = Color.White;
            //        Application.DoEvents();
            //        omod o = lvModList.Items[i].Tag as omod;
            //        if (CheckForUpdate(o, false, true))
            //        {
            //            o.bUpdateExists = true;
            //            lvModList.Items[i].BackColor = Color.Red;
            //        }
            //        else
            //        {
            //            o.bUpdateExists = false;
            //            lvModList.Items[i].BackColor = pbc;
            //        }
            //        lvModList.Items[i].ForeColor = pfc;
            //    }
            //}
            CheckForModUpdatesToolStripMenuItem1Click(sender, e);
		}
		
		void OCDSearchToolStripMenuItemClick(object sender, EventArgs e)
		{
			new SearchForm().Show();
		}
		
		
		void LvEspListItemChecked(object sender, ItemCheckedEventArgs e)
		{
            if (!updatingESPList)
			    UpdateESPM();
		}
		
		void CheckForOBMMExtendedUpdatesToolStripMenuItemClick(object sender, EventArgs e)
		{
			CheckForOBMMUpdates();
		}
		
		void CheckForOBMMUpdates()
		{
			try
			{
                string modid="", modName = "", modVersion = "", modDescription = "", modAuthor = "", modWebsite = "", imagefile=null;
                modid = Program.bSkyrimMode ? "5010" : "41346";
                Program.GetNexusModInfo("5010", ref modName, ref modVersion, ref modDescription, ref modAuthor, ref modWebsite, ref imagefile, true);

                if (modVersion.ToLower().CompareTo(Program.version.ToLower())!=0)
                //string tver = Program.bSkyrimMode ? Program.GetTESVersionMP("5010", false) : Program.GetTESVersionMP("41346", false);
				
                //int tvmaj = -1, tvmin = -1, tvbui = -1;
                //string[] tvers = tver.Split('.');
				
                //if (tvers.Length >= 1)
                //    tvmaj = int.Parse(tvers[0]);
                //if (tvers.Length >= 2)
                //{
                //    tvmin = int.Parse(tvers[1]);
                //}
                //if (tver.Length >= 3)
                //    tvbui = int.Parse(tvers[2]);

                //int curtvmaj = -1, curtvmin = -1, curtvbui = -1;
                //string[] curtvers = Program.version.Split('.');

                //if (curtvers.Length >= 1)
                //    curtvmaj = int.Parse(curtvers[0]);
                //if (curtvers.Length >= 2)
                //{
                //    curtvmin = int.Parse(curtvers[1]);
                //}
                //if (curtvers.Length >= 3)
                //    curtvbui = int.Parse(curtvers[2]);
                //if (VersionCompare(curtvmaj, curtvmin, curtvbui, tvmaj, tvmin, tvbui) == -1)
				{
					if (MessageBox.Show("An update is available for TesModManager"+
					                    ". Do you want to download it now?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
                        System.Diagnostics.Process.Start("http://www.nexusmods.com/skyrim/mods/5010/?tab=2&navtag=http%3A%2F%2Fwww.nexusmods.com%2Fskyrim%2Fajax%2Fmodfiles%2F%3Fid%3D5010&pUp=1");
					}
				}
/*                
				DateTime localver = new FileInfo("TesModManager.exe").LastWriteTimeUtc;
				ConfigList manifest;
				
				TextReader tr = DownloadFile("http://dl.dropbox.com/u/15040829/OBMMEx.xbt");
				
				manifest = new GeneralConfig().ReadConfiguration(tr.ReadToEnd());
				
				tr.Close();
				
				DateTime newver;
				
				{
					ConfigList versioninfo = manifest.GetSection("Version");
					
					int year = versioninfo.GetPair("Year").DataAsInteger;
					int month = versioninfo.GetPair("Month").DataAsInteger;
					int day = versioninfo.GetPair("Day").DataAsInteger;
					
					int hour = versioninfo.GetPair("Hour").DataAsInteger;
					int minute = versioninfo.GetPair("Minute").DataAsInteger;
					
					int offset = versioninfo.GetPair("Offset").DataAsInteger;
					
					newver = new DateTime(year, month, day, hour, minute, 0, DateTimeKind.Utc).AddMinutes(offset);
					
				}
				
				
				if (localver >= newver)
					return;
				
				if (MessageBox.Show("An update is available for Oblivion Mod Manager Extended"+
				                    ". Do you want to download it now?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					string basename = manifest["Base"];
					
					object[] fileo = manifest.GetPair("Files").DataAsArray;
					
					string[] files = new string[fileo.Length];
					
					for(int i=0;i<fileo.Length;i++)
					{
						if (fileo[i] is string)
						{
							files[i] = basename + (string)fileo[i];
						}
						else
						{
							throw new Exception("Malformed manifest file");
						}
					}
					
					if (!Directory.Exists("update"))
					{
						Directory.CreateDirectory("update");
					}
					
					List<MemoryStream> mstr = DownloadForm.DownloadFiles(files);
					
					for(int i=0;i<mstr.Count;i++)
					{
						File.WriteAllBytes("update\\" + fileo[i].ToString(), mstr[i].ToArray());
						
						mstr[i].Close();
					}
					
					System.Diagnostics.Process.Start("OBMMUpdater.exe", "OblivionModManager \"Oblivion Mod Manager Extended\" update");
					
					this.Close();
				}
 */
			}
			catch(Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
			}
  
		}
		
		void BtnMoarSettingsClick(object sender, EventArgs e)
		{
			cmdExtended.Show(btnMoarSettings, 0, 0);
		}
		
		void MoreSettingsToolStripMenuItemClick(object sender, EventArgs e)
		{
			new ESForm().ShowDialog();
		}

        private void backgroundNexusModUpdateChecker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            updatecheckstatuslabel.Text = e.UserState.ToString();
        }

        private void backgroundNexusModUpdateChecker_DoWork(object sender, DoWorkEventArgs e)
        {
            //CheckForModUpdates(e.Argument);
            check checklist = e.Argument as check;
            int total=checklist.weblist.Keys.Count;
            int current=1;
            try
            {

                // go check all version numbers
                foreach (string mod in checklist.weblist.Keys)
                {
                    backgroundNexusModUpdateChecker.ReportProgress(current*100/total,"Checking update for "+mod+" ("+current+" of "+total+")");
                    current++;
                    try
                    {

                        if (CheckForUpdate(checklist.weblist[mod], mod, checklist.verlist[mod], false, true))
                        {
                            checklist.updatelist.Remove(mod);
                            checklist.updatelist.Add(mod, true);
                        }
                        else
                            checklist.updatelist.Remove(mod);
                    }
                    catch (Exception ex)
                    {
                        checklist.updatelist.Remove(mod);
                        if (DialogResult.No == MessageBox.Show("Could not check for update for mod '" + mod + "'. Error: " + ex.Message + ". Continue?", "Error checking for updates", MessageBoxButtons.YesNo, MessageBoxIcon.Error))
                        {
                            break;
                        }
                    }
                    if (backgroundNexusModUpdateChecker.CancellationPending)
                        break;
                }

            }
            catch (Exception ex)
            {
                Program.logger.WriteToLog("Checking for mods update cancelled: " + ex.Message, Logger.LogLevel.Error);
            }
        }
        private void backgroundNexusModUpdateChecker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //check checklist = e.Result as check;

            lvModList.SuspendLayout();
            lvModList.BeginUpdate();

            updatecheckstatuslabel.Visible = true;
            // put the results back in the mod list
            for (int i = 0; i < lvModList.Items.Count; i++)
            {
                if (lvModList.Items[i].Tag != null)
                {
                    omod o = lvModList.Items[i].Tag as omod;
                    if (o != null)
                    {
                        Application.DoEvents();
                        if (modUpdateCheckList.updatelist.ContainsKey(o.ModName))
                        {
                            o.bUpdateExists = true;
                            lvModList.Items[i].ForeColor = Color.Red;
                        }
                        else
                        {
                            o.bUpdateExists = false;
                            lvModList.Items[i].ForeColor = Color.Black;
                        }
                    }
                }
            }
            lvModList.ResumeLayout();
            lvModList.EndUpdate();
            updatecheckstatuslabel.Text = "No update check in progress";
            updatecheckstatuslabel.Visible = false;

        }

        class check
        {
            public Dictionary<string, string> weblist = new Dictionary<string, string>();
            public Dictionary<string, string> verlist = new Dictionary<string, string>();
            public Dictionary<string, bool> updatelist = new Dictionary<string, bool>();
        }
        object
        CheckForModUpdates(object obj)
        {
            check checklist = obj as check;
            try
            {

                // go check all version numbers
                foreach (string mod in checklist.weblist.Keys)
                {
                    try
                    {
                        if (CheckForUpdate(checklist.weblist[mod], mod, checklist.verlist[mod], false, true))
                        {
                            checklist.updatelist.Remove(mod);
                            checklist.updatelist.Add(mod, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (DialogResult.No == MessageBox.Show("Could not check for update for mod '" + mod + "'. Error: " + ex.Message + ". Continue?", "Error checking for updates", MessageBoxButtons.YesNo, MessageBoxIcon.Error))
                        {
                            break;
                        }
                    }

                }


                //// put the results back in the mod list
                //for (int i = 0; i < lvModList.Items.Count; i++)
                //{
                //    if (lvModList.Items[i].Tag != null)
                //    {
                //        omod o = lvModList.Items[i].Tag as omod;
                //        if (o != null && checklist.updatelist.ContainsKey(o.ModName))
                //        {
                //            Color pbc = lvModList.Items[i].BackColor;
                //            Color pfc = lvModList.Items[i].ForeColor;
                //            lvModList.Items[i].BackColor = Color.Blue;
                //            lvModList.Items[i].ForeColor = Color.White;
                //            Application.DoEvents();
                //            if (checklist.updatelist[o.ModName])
                //            {
                //                o.bUpdateExists = true;
                //                lvModList.Items[i].BackColor = Color.Red;
                //            }
                //            else
                //            {
                //                o.bUpdateExists = false;
                //                lvModList.Items[i].BackColor = pbc;
                //            }
                //            lvModList.Items[i].ForeColor = pfc;
                //        }
                //    }
                //}
             //for (int i = 0; i < lvModList.Items.Count; i++)
             //   {
             //       if (lvModList.Items[i].Tag != null)
             //       {
             //           Color pbc = lvModList.Items[i].BackColor;
             //           Color pfc = lvModList.Items[i].ForeColor;
             //           lvModList.Items[i].BackColor = Color.Blue;
             //           lvModList.Items[i].ForeColor = Color.White;
             //           Application.DoEvents();
             //           omod o = lvModList.Items[i].Tag as omod;
             //           if (CheckForUpdate(o.Website, o.ModName, o.Version, false, true))
             //           {
             //               o.bUpdateExists = true;
             //               lvModList.Items[i].BackColor = Color.Red;
             //           }
             //           else
             //           {
             //               o.bUpdateExists = false;
             //               lvModList.Items[i].BackColor = pbc;
             //           }
             //           lvModList.Items[i].ForeColor = pfc;
             //       }
             //   }
            }
            catch (Exception ex)
            {
                Program.logger.WriteToLog("Checking for mods update cancelled: " + ex.Message, Logger.LogLevel.Error);
            }
            return checklist;
        }
		void CheckForModUpdatesToolStripMenuItem1Click(object sender, EventArgs e)
		{
            if (backgroundNexusModUpdateChecker.IsBusy)
                return;

            // retrieve the list of mods, websites and versions
            //for (int i = 0; i < lvModList.Items.Count; i++)
            //{
            //    if (lvModList.Items[i].Tag != null)
            //    {
            //        omod o = lvModList.Items[i].Tag as omod;
                    foreach (omod o in Program.Data.omods)
                    {
                        if (!modUpdateCheckList.weblist.ContainsKey(o.ModName))
                        {
                            modUpdateCheckList.weblist.Add(o.ModName, o.Website);
                            modUpdateCheckList.verlist.Add(o.ModName, o.Version);
                            modUpdateCheckList.updatelist.Add(o.ModName, false);
                        }
                    }
            //    }
            //}

//            ThreadPool.QueueUserWorkItem(new WaitCallback(CheckForModUpdates), checklist);
            updatecheckstatuslabel.Visible = true;

            backgroundNexusModUpdateChecker.WorkerReportsProgress = true;
            backgroundNexusModUpdateChecker.RunWorkerAsync(modUpdateCheckList);



            //ThreadStart work = CheckForModUpdates;
            //Thread thread = new Thread(work);
            //thread.Start();

            //CheckForModUpdates(checklist);
		}
		
		void OCDSearchToolStripMenuItem1Click(object sender, EventArgs e)
		{
			new SearchForm().Show();
		}
		
		void OCDEditorToolStripMenuItemClick(object sender, EventArgs e)
		{
			new OCDForm().Show();
		}

        private void cbFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFilter.Checked)
                // ignore capitalization
                strFilter = txtFilter.Text.ToLower();
            else
                strFilter = "";

            UpdateOmodList();
        }

        private void cmbChoser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void extractFileCallback(SevenZip.ExtractFileCallbackArgs args)
        {
            switch (args.Reason)
            {
                case SevenZip.ExtractFileCallbackReason.Start:
                    // starting new file
                    args.ExtractToFile = strTmpDir+"\\"+args.ArchiveFileInfo.FileName;
                    break;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

            OpenFileDialog openfileDlg = new OpenFileDialog();

            openfileDlg.Filter = "all files|*.*|7z files|*.7z|zip files|*.zip|rar files|*.rar|omod files|*.omod|omod2 files|*.omod2";
            openfileDlg.Multiselect = true;
            openfileDlg.ShowDialog();

            string[] filenames = openfileDlg.FileNames;// .FileName;
            //ProgressForm 
            Program.pf = null;
            foreach (string filename in filenames)
            {
                importStatusLabel.Visible = true;
                importStatusLabel.Text = "Importing " + filename;
                Program.pf = new ProgressForm("Importing " + filename, false);
                Program.pf.SetProgressRange(100);
                Program.pf.ShowInTaskbar = true;
                Program.pf.Show();

                try
                {
                    Program.importFile(filename, Program.pf);

                    if (Program.pf!=null && Program.pf.bCancelled)
                        break;
                }
                finally
                {
                    if (Program.pf != null)
                    {
                        Program.pf.Hide();
                        Program.pf.Dispose();
                        Program.pf = null;
                    }
                }

                // save omods to disk
                Program.SaveData();
            }

            importStatusLabel.Text = "No import";
            importStatusLabel.Visible = false;
            Application.UseWaitCursor = false;
            UpdateOmodList();


/*
            string extension = filename.Substring(filename.LastIndexOf('.')+1);
            switch (extension)
            {
                case "zip":
                case "7z":
                case "rar":
                    // first extract all files
                    strTmpDir=Program.CreateTempDirectory();
                    SevenZip.SevenZipExtractor sevenZipExtract = new SevenZip.SevenZipExtractor(filename);
//                    sevenZipExtract.PreserveDirectoryStructure = true;
                    int[] indexlist = new int[sevenZipExtract.FilesCount];
                    for (int i = 0; i < sevenZipExtract.FilesCount; i++)
                        indexlist[i] = i;
                   
                    sevenZipExtract.ExtractFiles(strTmpDir, indexlist); //  (new SevenZip.ExtractFileCallback(extractFileCallback));

//                    CreateModForm createModForm = new CreateModForm(strTmpDir); // this expects the file to have textures and meshes at the root
                    CreateModForm createModForm = new CreateModForm();
                    string[] dir = Directory.GetDirectories(strTmpDir,"textures",SearchOption.AllDirectories);
                    if (dir.Length > 0)
                    {
                        // import from the directory above
                        dir[0] = dir[0].Substring(0, dir[0].LastIndexOf('\\') + 1);
                        createModForm.AddFilesFromFolder(dir[0]);
                    }
                    //ToDo:  add ESMS and BSAs
                    string[] bsalist = Directory.GetFiles(strTmpDir, "*.bsa", SearchOption.AllDirectories);
                    string[] esmlist = Directory.GetFiles(strTmpDir, "*.esm", SearchOption.AllDirectories);
                    string[] esplist = Directory.GetFiles(strTmpDir, "*.esp", SearchOption.AllDirectories);
                    List<string> pluginslist = new List<string>();
                    pluginslist.AddRange(esmlist);
                    pluginslist.AddRange(esplist);
                    createModForm.ops.esps = pluginslist.ToArray();
                    for (int curplugin = 0; curplugin < pluginslist.Count; curplugin++)
                    {
                        pluginslist[curplugin] = Path.GetFileName(pluginslist[curplugin]);
                    }
                    createModForm.ops.espPaths=pluginslist.ToArray();
                    for (int curplugin = 0; curplugin < pluginslist.Count; curplugin++)
                    {
                        pluginslist[curplugin] = Path.GetFileName(filename);
                    }
                    createModForm.ops.espSources = pluginslist.ToArray();

                    List<string> datalist = new List<string>();
                    datalist.AddRange(bsalist);
                    createModForm.ops.DataFiles = datalist.ToArray();
                    for (int curdata = 0; curdata < datalist.Count; curdata++)
                    {
                        datalist[curdata] = Path.GetFileName(datalist[curdata]);
                    }
                    createModForm.ops.DataFilePaths = datalist.ToArray();
                    for (int curdata = 0; curdata < datalist.Count; curdata++)
                    {
                        datalist[curdata] = Path.GetFileName(filename);
                    }
                    createModForm.ops.DataSources = datalist.ToArray();

                    int zni;
                    if ((zni = filename.IndexOf('-')) != -1)
                    {
                        string[] chunks = filename.Substring(zni + 1).Split('-');

                        string nexusFileid = "";
                        // check the chunks to see if they are numeric only
                        foreach (string chunk in chunks)
                        {
                            bool isnumeric = true;
                            for (int i = 0; i < chunk.Length; i++)
                            {
                                if ("0123456789".IndexOf(chunk[i]) == -1)
                                {
                                    isnumeric = false;
                                    break;
                                }
                            }
                            if (isnumeric)
                            {
                                nexusFileid = chunk;
                                break;
                            }
                        }
                        if (nexusFileid.Length>0)
                        {
                            if ((GlobalSettings.AlwaysImportTES || MessageBox.Show("Import info from " + (Program.bSkyrimMode ? "Skyrim" : "TES") + "Nexus?", "Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                                == DialogResult.Yes))
                            {
                                if (Program.KeyPressed(16))
                                    GlobalSettings.AlwaysImportTES = true;
                                createModForm.ApplyTESNexus(GlobalSettings.LastTNID = nexusFileid);
                            }
                        }
                    }
                    // now check what we have here
                    if (createModForm.ShowForm(false))
                    {
                        UpdateOmodList();
                    }
                    break;
                case "omod":
                case "omod2":
                    if (null!=Program.LoadNewOmod(filename))
				        UpdateOmodList();
                    break;
                default:
                    break;
            }
 */
        }

        //typedef struct _boss_db_int * boss_db;
        //uint32_t SortMods (boss_db db, const bool trialOnly, uint8_t *** sortedPlugins, size_t * sortedListLength, uint8_t *** unrecognisedPlugins, size_t * unrecListLength);
//        [System.Runtime.InteropServices.DllImport("boss32.dll", CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
//        UInt32 SortMods(UIntPtr db, bool trialOnly, Byte*** sortedPlugins, ref UIntPtr sortedListLength, Byte*** unrecognisedPlugins, ref UIntPtr unrecListLength);

        //uint32_t CreateBossDb (boss_db * db, const uint32_t clientGame, const uint8_t * dataPath);
        //uint32_t UpdateMasterlist (boss_db db, const uint8_t * masterlistPath);
        //uint32_t Load (boss_db db, const uint8_t * masterlistPath, const uint8_t * userlistPath);
        //void DestroyBossDb (boss_db db);
        private void btnBOSSSortPlugins_Click(object sender, EventArgs e)
        {
            
            /*
            int length = 0;
            UIntPtr db;
            const UInt32 BOSS_API_GAME_OBLIVION;
            const UInt32 BOSS_API_GAME_SKYRIM;

            ret = CreateBossDb(ref db, game, NULL);
            ret = UpdateMasterlist(db, mPath);
            ret = Load(db, mPath, NULL);
            SortMods(db, false, null, ref length, null, ref length);
            DestroyBossDb(db);*/

            System.Diagnostics.Process tmm = new System.Diagnostics.Process();
            tmm.StartInfo.FileName = Program.BOSSpath + "\\BOSS.exe";
            tmm.StartInfo.WorkingDirectory = Program.BOSSpath;
            tmm.StartInfo.Arguments = "-g "+ Program.gameName;
            tmm.Start();
            tmm.WaitForExit();
            Program.loadLoadOrderTxtFile();
            string[] esps = OblivionESP.GetActivePlugins();
            foreach (ListViewItem lvi in lvEspList.Items)
            {
                EspInfo ea = (EspInfo)lvi.Tag;
                ea.DateModified = File.GetLastWriteTime(Path.Combine(Program.DataFolderPath,ea.FileName));
            }
            lvEspList.Sort();
        }

        private void omodContextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (lvModList.SelectedIndices.Count > 1)
            {
                editToolStripMenuItem.Enabled = false;
                infoToolStripMenuItem.Enabled = false;
                viewReadmeToolStripMenuItem.Enabled = false;
                viewScriptToolStripMenuItem.Enabled = false;
                visitWebsiteToolStripMenuItem.Enabled = true;
                emailAuthorToolStripMenuItem.Enabled = false;
                viewDataConflictsToolStripMenuItem.Enabled = false;
                checkForModUpdatesToolStripMenuItem1.Enabled = false;
                convertToArchiveToolStripMenuItem.Enabled = false;
                extractToFolderToolStripMenuItem.Enabled = false;
                exportOmodConversionDataToolStripMenuItem.Enabled = false;
                checkForUpdatesToolStripMenuItem.Enabled = true;
                simulateToolStripMenuItem.Enabled = false;
            }
            else if (lvModList.SelectedIndices.Count == 1)
            {
                editToolStripMenuItem.Enabled = true;
                infoToolStripMenuItem.Enabled = true;
                viewReadmeToolStripMenuItem.Enabled = true;
                viewScriptToolStripMenuItem.Enabled = true;
                visitWebsiteToolStripMenuItem.Enabled = true;
                emailAuthorToolStripMenuItem.Enabled = true;
                viewDataConflictsToolStripMenuItem.Enabled = true;
                checkForModUpdatesToolStripMenuItem1.Enabled = true;
                convertToArchiveToolStripMenuItem.Enabled = true;
                extractToFolderToolStripMenuItem.Enabled = true;
                exportOmodConversionDataToolStripMenuItem.Enabled = true;
                checkForUpdatesToolStripMenuItem.Enabled = true;
                simulateToolStripMenuItem.Enabled = true;

                if (((omod)lvModList.SelectedItems[0].Tag).Hidden)
                {
                    hideToolStripMenuItem.Text = "Unhide";
                }
                else
                {
                    hideToolStripMenuItem.Text = "Hide";
                }
            }

        }

        private void lvEspList_MouseDown(object sender, MouseEventArgs e)
        {
            if (lvEspList.SelectedIndices.Count > 1)
            {
                viewDataFilesToolStripMenuItem.Enabled = false;
                unlinkToolStripMenuItem.Enabled = false;
            }
            else
            {
                viewDataFilesToolStripMenuItem.Enabled = true;
                unlinkToolStripMenuItem.Enabled = true;
            }
        }

        private void lvEspList_MouseHover(object sender, EventArgs e)
        {
            // definitely done loading
            //bLoadingForm = false;
        }

        private void btnImport_MouseHover(object sender, EventArgs e)
        {
//            btnToolTip.SetToolTip(this.btnImport, "Import a file (zip, fomod, etc...) to create a new OMOD mod");
        }

        private void bLoad_MouseHover(object sender, EventArgs e)
        {
//            btnToolTip.SetToolTip(this.bLoad, "Load an existing mod (omod, omod2, fomod) without creating a new OMOD mod");
        }

        private void bCreate_MouseHover(object sender, EventArgs e)
        {
//            btnToolTip.SetToolTip(this.bCreate, "Create a new OMOD mod");
        }

        private void bActivate_MouseHover(object sender, EventArgs e)
        {
//            btnToolTip.SetToolTip(this.bActivate, "(De)Activate the selected mod");
        }

        private void bEdit_MouseHover(object sender, EventArgs e)
        {
//            btnToolTip.SetToolTip(this.bEdit, "Edit the selected mod to create an OMOD mod");
        }

        private void doImport()
        {
            System.Threading.Monitor.Enter(Program.importList);
            if (Program.importList.Count > 0)
            {
                string file = Program.importList[0];
                System.Threading.Monitor.Exit(Program.importList);

                ProgressForm pf = new ProgressForm("Importing " + file + " from Nexus.com", false);
                pf.SetProgressRange(100);
                pf.EnableCancel("");
                pf.ShowInTaskbar = true;
                pf.Show();
                importStatusLabel.Visible = true;
                importStatusLabel.Text = "Importing " + file + " (" + Program.importList.Count + " imports)";
                //backgroundModImportWorker.ReportProgress(0, "Importing " + file + " (" + Program.importList.Count + " imports)");
                try
                {
                    Program.importFile(file, pf);
                }
                catch (Exception ex)
                {
                    Program.logger.WriteToLog("Failed to import file " + file + ": " + ex.Message, Logger.LogLevel.Low);
                }
                importStatusLabel.Text = "No import";
                importStatusLabel.Visible = false;
                //backgroundModImportWorker.ReportProgress(0, "No import");
                File.Delete(file);
                System.Threading.Monitor.Enter(Program.importList);
                Program.importList.Remove(file);
                System.Threading.Monitor.Exit(Program.importList);
                pf.Hide();
                pf.Close();
                pf.Dispose();
                UpdateOmodList();
                Application.UseWaitCursor = false;
                pf = null;
            }
            else
                System.Threading.Monitor.Exit(Program.importList);
        }
        private void backgroundModImportWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            while (!bClosingForm)
            {
                System.Threading.Thread.Sleep(10);
                // running import on a different thread can create conflicts. Avoid for now
//                doImport();
            }

        }

        private void statusUpdateTimer_Tick(object sender, EventArgs e)
        {
            //if (Program.downloadList.Count > 0)
            //    downloadStatusLabel.Text = "Downloading " + Program.downloadList.Count + " Nexus mods";
            //else
            //    downloadStatusLabel.Text = "No download";
        }

        private void backgroundModDownloadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!bClosingForm)
            {
                System.Threading.Thread.Sleep(10);
                System.Threading.Monitor.Enter(Program.downloadList);
                if (Program.downloadList.Count > 0)
                {
                    FileDownload download = new FileDownload("");
                    foreach (FileDownload dl in Program.downloadList.Values)
                    {
                        download = dl;
                        break;
                    }
                    Program.downloadList.Remove(download.nxm);
                    System.Threading.Monitor.Exit(Program.downloadList);

                    System.Threading.Monitor.Enter(Program.downloadingList);
                    if (!Program.downloadingList.ContainsKey(download.nxm))
                        Program.downloadingList.Add(download.nxm, download);
                    System.Threading.Monitor.Exit(Program.downloadingList);

                    backgroundModDownloadWorker.ReportProgress(0, "Downloading " + download.nxm);
                    //if (!Program.handleNXMLink(nxmlink, backgroundModDownloadWorker, (int)e.Argument))
                    if (!Program.handleNXMLink(download, (BackgroundWorker)sender, (int)e.Argument))
                    {
                        // put the URL back
                        if (!download.bCancelled) //(Program.bCancelDownload && download.nxm == Program.strDownloadToCancel))
                        {
                            System.Threading.Monitor.Enter(Program.downloadList);
                            Program.downloadList.Add(download.nxm, download);
                            System.Threading.Monitor.Exit(Program.downloadList);
                        }
                        //else
                        //{
                        //    Program.bCancelDownload=false;
                        //    Program.strDownloadToCancel = "";
                        //}
                    }

                    System.Threading.Monitor.Enter(Program.downloadingList);
                    Program.downloadingList.Remove(download.nxm);
                    System.Threading.Monitor.Exit(Program.downloadingList);

                    try
                    {
                        backgroundModDownloadWorker.ReportProgress(100, "No download");
                    }
                    catch { };
                }
                else
                {
                    System.Threading.Monitor.Exit(Program.downloadList);
                    //downloadProgressBar.Visible = false;
                    //downloadStatusLabel.Visible = false;
                    //downloadProgressBar2.Visible = false;
                    //downloadStatusLabel2.Visible = false;
                }
            }

        }

        private void backgroundModImportWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundModDownloadWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                int totaldownloads = Program.downloadList.Count + Program.pausedList.Count + Program.downloadingList.Count;
                if (totaldownloads > 0)
                {
                    FileDownload dl = e.UserState as FileDownload;
                    downloadProgressBar.Visible = true;
                    downloadStatusLabel.Visible = true;
                    if (dl != null)
                        downloadStatusLabel.Text = dl.filename + " (" + totaldownloads + " download" + (totaldownloads > 1 ? "s" : "") + ")";
                    else
                        downloadStatusLabel.Text = " (" + totaldownloads + " download" + (totaldownloads > 1 ? "s" : "") + ")"; //downloadStatusLabel.Text.Substring(0, downloadStatusLabel.Text.IndexOf('(')) + "(" + totaldownloads + " download" + (totaldownloads > 1 ? "s" : "") + ")";
                    if (e.ProgressPercentage != 0 && e.ProgressPercentage != -1)
                    {
                        if (downloadProgressBar.ProgressBar != null)
                        {
                            downloadProgressBar.Value = e.ProgressPercentage;
                            downloadProgressBar.ToolTipText = e.ProgressPercentage + " downloaded";
                        }
                    }
                }
                else
                {
                    downloadProgressBar.Visible = false;
                    downloadStatusLabel.Visible = false;
                    downloadProgressBar2.Visible = false;
                    downloadStatusLabel2.Visible = false;
                }
            }
            catch { };
        }
        //private void backgroundModDownloadWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    int totaldownloads = Program.downloadList.Count + Program.pausedList.Count + Program.downloadingList.Count;
        //    if (totaldownloads > 0)
        //    {
        //        downloadProgressBar2.Visible = true;
        //        downloadStatusLabel2.Visible = true;
        //        if (e.UserState != null)
        //            downloadStatusLabel2.Text = e.UserState.ToString() + " (" + totaldownloads + " download" + (totaldownloads > 1 ? "s" : "") + ")";
        //        else
        //            downloadStatusLabel2.Text = " (" + totaldownloads + " download" + (totaldownloads > 1 ? "s" : "") + ")"; //downloadStatusLabel2.Text.Substring(0, downloadStatusLabel2.Text.IndexOf('(') + 1) + "(" + totaldownloads + " download" + (totaldownloads > 1 ? "s" : "") + ")";
        //        if (e.ProgressPercentage != 0 && e.ProgressPercentage != -1)
        //        {
        //            if (downloadProgressBar2.ProgressBar != null)
        //            {
        //                downloadProgressBar2.Value = e.ProgressPercentage;
        //                downloadProgressBar2.ToolTipText = e.ProgressPercentage + " downloaded";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        downloadProgressBar2.Visible = false;
        //        downloadStatusLabel2.Visible = false;
        //        downloadProgressBar.Visible = false;
        //        downloadStatusLabel.Visible = false;
        //    }
        //}

        private void backgroundNexusInfoDownloadWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            while (!bClosingForm)
            {
                System.Threading.Thread.Sleep(10);
                System.Threading.Monitor.Enter(Program.nexusInfoDownloadList);
                if (Program.nexusInfoDownloadList.Count > 0)
                {
                    string filename = Program.nexusInfoDownloadList[0];
                    System.Threading.Monitor.Exit(Program.nexusInfoDownloadList);

                    // download the info and put it in the cache folder as an omod2
                    string nexusid = Program.GetModID(filename);
                    if (nexusid.Length == 0)
                    {
                        omod o = Program.Data.GetMod(filename);
                        nexusid = Program.GetModID(o.Website);
                    }
                    omodCreationOptions ops = new omodCreationOptions();
                    //string Name = "", Version = "", Description = "", Author = "", Website = "", Image = "";
                    ops.Image = "";
                    Program.GetNexusModInfo(nexusid, ref ops.Name, ref ops.Version, ref ops.Description, ref ops.Author, ref ops.website, ref ops.Image, true);
                    if (!Directory.Exists(Path.Combine(Settings.omodDir, "cache")))
                        Directory.CreateDirectory(Path.Combine(Settings.omodDir,"cache"));
                    string newdoc = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" +
                                    "<fomod>" +
                                        "<Name>" + ops.Name + "</Name>" +
                                        "<Author>" + ops.Author + "</Author>" +
                                        "<Version MachineVersion=\"1.0\">" + ops.Version + "</Version>" +
                                        "<Website>" + ops.website + "</Website>" +
                                        "<Description>" + ops.Description + "</Description>" +
                        //"<Groups>"+
                        //    "<element>bugfixes, scripts, meshes, textures</element>"+
                        //"</Groups>"+
                                    "</fomod>";
                    //omod.CreateZipOmod2(ops, "cache//" + Path.GetFileName(filename), 0);
                    string infodir = Path.Combine(Settings.omodDir, "info");
                    try
                    {
                        string cacheText = Path.Combine(infodir, filename + ".xml").Replace(".ghost", "");
                        // just in case
                        if (!Directory.Exists(infodir))
                            Directory.CreateDirectory(infodir);

                        File.WriteAllText(cacheText, newdoc);
                    }
                    catch { };
                    if (ops.Image!=null && ops.Image.Length > 0)
                    {
                        try
                        {
                            string cacheImage = Path.Combine(infodir, filename + ".jpg").Replace(".ghost", "");

                            // copy file to info directory
                            if (File.Exists(cacheImage))
                                File.Delete(cacheImage);
                            File.Copy(ops.Image, cacheImage);
                        }
                        catch { };
                    }
                    System.Threading.Monitor.Enter(Program.nexusInfoDownloadList);
                    Program.nexusInfoDownloadList.Remove(filename);
                    System.Threading.Monitor.Exit(Program.nexusInfoDownloadList);
                    //UpdateOmodList();
                }
                else
                    System.Threading.Monitor.Exit(Program.nexusInfoDownloadList);


            }
        }

        private void importFileTimer_Tick(object sender, EventArgs e)
        {
            if (!bImporting)
            {
                bImporting = true;
                doImport();
                bImporting = false;
            }
        }

        private void toolStripReactivateMod_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvModList.SelectedItems)
            {
                omod o = lvi.Tag as omod;
                o.Deactivate(true);
                ActivateOmod(o, true);
            }
        }

        private void radList_CheckedChanged(object sender, EventArgs e)
        {
            lvModList.View = View.List;
            Properties.Settings.Default.DefaultView = "List";
        }

        private void radDetail_CheckedChanged(object sender, EventArgs e)
        {
            lvModList.View = View.Details;
            Properties.Settings.Default.DefaultView = "Details";
        }

        private void lvModList_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            Properties.Settings.Default.ModNameColumnWidth=lvModList.Columns[0].Width;
            Properties.Settings.Default.ModAuthorColumnWidth = lvModList.Columns[1].Width;
            Properties.Settings.Default.ModVersionColumnWidth=lvModList.Columns[2].Width;
            Properties.Settings.Default.ModNbDataFilesColumnWidth=lvModList.Columns[3].Width;
            Properties.Settings.Default.ModNbPluginsColumnWidth=lvModList.Columns[4].Width;
            Properties.Settings.Default.DatePackedColumnWidth = lvModList.Columns[5].Width;
            Properties.Settings.Default.ConflictColumnWidth = lvModList.Columns[6].Width;
            Properties.Settings.Default.FileSizeColumnWidth = lvModList.Columns[7].Width;
        }

        private void conflictedFilesPickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<DataFileInfo> conflicts = new List<DataFileInfo>();
            foreach (DataFileInfo dfi in Program.Data.DataFiles.Values)
            {
                if (dfi.OwnerList != null && dfi.OwnerList.Count>1)
                {
                    conflicts.Add(dfi);
                }
            }
            if (conflicts.Count > 0)
            {
                ConflictedFilePickerForm frm = new ConflictedFilePickerForm(conflicts);
                frm.ShowDialog();
                frm.Dispose();
            }
            else
            {
                MessageBox.Show("No conflicts to pick from","No conflicts",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void toolStripLblScriptExtenderVersion_Click(object sender, EventArgs e)
        {
            if (toolStripLblScriptExtenderVersion.Text.ToLower().Contains("update") || toolStripLblScriptExtenderVersion.Text.ToLower().Contains("install"))
            {
                if (DialogResult.No == MessageBox.Show("Do you want to install the latest Script Extender?", "Script Extender", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    return;
                toolStripProcessingStatusLabel.Visible = true;
                toolStripProcessingStatusLabel.Text = "Retrieving latest " + (Program.bSkyrimMode ? "SKSE" : "OBSE");
                Application.DoEvents();
                try
                {

                    // there is an update to install?
                    System.Net.WebClient wc = new System.Net.WebClient();
                    byte[] bytepage = wc.DownloadData((Program.bSkyrimMode ? "http://skse.silverlock.org/" : "http://obse.silverlock.org/"));

                    string page = System.Text.Encoding.ASCII.GetString(bytepage).ToString();
                    File.Delete(Program.TempDir + "\\scriptextender.7z");
                    if (Program.bSkyrimMode)
                    {
                        string filelink = "";
                        if (page.IndexOf("download/skse_") != -1)
                        {
                            filelink = page.Substring(page.LastIndexOf("download/skse_"));
                        }
                        if (filelink.Length == 0  && page.IndexOf("beta/skse_") != -1)
                        {
                            filelink = page.Substring(page.LastIndexOf("beta/skse_"));
                        }
                        if (filelink.Length>0)
                        {
                            string extension = "";
                            if (filelink.IndexOf(".7z") != -1)
                                extension = ".7z";
                            else
                                extension = ".zip";
                            filelink = filelink.Substring(0, filelink.IndexOf(extension));
                            filelink = "http://skse.silverlock.org/" + filelink + extension;
                            toolStripProcessingStatusLabel.Text = "Downloading latest " + (Program.bSkyrimMode ? "SKSE" : "OBSE");
                            Application.DoEvents();
                            bytepage = wc.DownloadData(filelink);
                            File.WriteAllBytes(Program.TempDir + "\\scriptextender.7z", bytepage);
                        }
                    }
                    else
                    {
                        if (page.IndexOf(".silverlock.org/download/") != -1)
                        {
                            string filelink = page.Substring(page.IndexOf(".silverlock.org/download/"));
                            string extension = "";
                            if (filelink.IndexOf(".7z") != -1)
                                extension = ".7z";
                            else
                                extension = ".zip";
                            filelink = filelink.Substring(0, filelink.IndexOf(extension));
                            filelink = "http://" + (Program.bSkyrimMode ? "skse" : "obse") + filelink + extension;
                            toolStripProcessingStatusLabel.Text = "Downloading latest " + (Program.bSkyrimMode ? "SKSE" : "OBSE");
                            Application.DoEvents();
                            bytepage = wc.DownloadData(filelink);
                            File.WriteAllBytes(Program.TempDir + "\\scriptextender.7z", bytepage);
                        }
                    }
                    if (File.Exists(Program.TempDir + "\\scriptextender.7z"))
                    {
                        string extenderTmpDir = Path.Combine(Program.TempDir, "scriptextender");
                        SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(Program.TempDir + "\\scriptextender.7z");
                        List<string> files = new List<string>(zextract.ArchiveFileNames);
                        for (int i = 0; i < files.Count; i++)
                        {
                            if (files[i].ToLower().Contains("\\src\\") || files[i].ToLower().EndsWith(".txt") || files[i].ToLower().EndsWith("\\src"))
                            {
                                files.RemoveAt(i);
                                i--;
                            }

                        }
                        toolStripProcessingStatusLabel.Text = "Extracting latest " + (Program.bSkyrimMode ? "SKSE" : "OBSE");
                        Application.DoEvents();
                        zextract.ExtractFiles(extenderTmpDir, files.ToArray());
                        zextract.Dispose();
                        // make sure that the current dir did not change
                        Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));

                        toolStripProcessingStatusLabel.Text = "Installing latest " + (Program.bSkyrimMode ? "SKSE" : "OBSE");
                        Application.DoEvents();

                        //List<string> newfiles = new List<string>(files.Count);

                        // check if the files are in a subfolder
                        string pathtoremove = "";
                        foreach (string file in files)
                        {
                            if (file.ToLower().EndsWith(Program.bSkyrimMode?"skse_loader.exe":"obse_loader.exe"))
                            {
                                pathtoremove=file.ToLower().Replace((Program.bSkyrimMode?"skse_loader.exe":"obse_loader.exe"),"");
                                break;
                            }
                        }
                        if (pathtoremove.IndexOf("\\Data") != -1)
                            pathtoremove = pathtoremove.Substring(0, pathtoremove.IndexOf("\\Data"));
                        else if (pathtoremove.IndexOf("\\")!=-1)
                            pathtoremove = pathtoremove.Substring(0, pathtoremove.IndexOf("\\"));
                        //else if (pathtoremove.IndexOf(Path.GetFileNameWithoutExtension(filelink))!=-1)
                        //    pathtoremove = pathtoremove.Substring(0, pathtoremove.IndexOf(Path.GetFileNameWithoutExtension(filelink)));
                        files = new List<string>(Directory.EnumerateFiles(Path.Combine(extenderTmpDir,pathtoremove), "*.*", SearchOption.AllDirectories));
                        List<string> dirs = new List<string>(Directory.EnumerateDirectories(Path.Combine(extenderTmpDir, pathtoremove), "*.*", SearchOption.AllDirectories));
                        foreach (string dir in dirs)
                        {
                            string newdir = dir.Replace(Path.Combine(extenderTmpDir, pathtoremove) + "\\", "");
                            if (!Directory.Exists(newdir))
                                Directory.CreateDirectory(newdir);
                        }
                        string srcdir = Path.Combine(extenderTmpDir, "src\\").ToLower();
                        string oldpath=Path.Combine(extenderTmpDir, pathtoremove);
                        for (int j = 0; j < files.Count; j++)
                        {
                            if (files[j].ToLower().StartsWith(srcdir)) // ignore source code
                                continue;
                            string targetfile = files[j].Replace(oldpath, Program.gamePath);
                            if (File.Exists(targetfile))
                            {
                                File.SetAttributes(targetfile, FileAttributes.Normal);
                                File.Delete(targetfile);
                            }
                            if (Path.GetDirectoryName(targetfile).Length!=0)
                                Directory.CreateDirectory(Path.GetDirectoryName(targetfile));
                            File.Copy(files[j], targetfile);
                            File.Delete(files[j]);
                        }
                        try { Directory.Delete(extenderTmpDir, true); }catch{};
                        File.Delete(Program.TempDir + "\\scriptextender.7z");
                        refreshScriptExtenderVersion();
                    }
                }
                catch (Exception ex)
                {
                    Program.logger.WriteToLog("Could not update ScriptExtender: " + ex.Message, Logger.LogLevel.Low);
                    MessageBox.Show("Could not update ScriptExtender: " + ex.Message, "Error updating Script Extender", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                toolStripProcessingStatusLabel.Text = "";
                toolStripProcessingStatusLabel.Visible = false;
                Application.DoEvents();
            }
        }

        private void runASkyProcPatcherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SkyProcPatchersForm frm = new SkyProcPatchersForm();
            frm.ShowDialog();

            // check any change in ESPs/ESMs
            Program.loadLoadOrderTxtFile();
            foreach (string plugin in Program.loadOrderList)
            {
                bool bFound = false;
                foreach (EspInfo ei in Program.Data.Esps)
                {
                    if (plugin == ei.LowerFileName)
                        bFound = true;
                }
                if (!bFound)
                    Program.Data.Esps.Add(new EspInfo(plugin));
            }
            string[] esps = OblivionESP.GetActivePlugins();
            foreach (ListViewItem lvi in lvEspList.Items)
            {
                EspInfo ea = (EspInfo)lvi.Tag;
                ea.DateModified = File.GetLastWriteTime(Path.Combine(Program.DataFolderPath,ea.FileName));
            }
            lvEspList.Sort();
        }

        private void downloadStatusLabel_Click(object sender, EventArgs e)
        {
            // show list of downloads with checkbox to pause/cancel
            DownloadsForm df = new DownloadsForm();

            df.ShowDialog();
            df.Dispose();
        }

        private void addInfoFromTesNexusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!backgroundTesNexusInfoAdder.IsBusy)
                backgroundTesNexusInfoAdder.RunWorkerAsync(lvModList.SelectedItems[0].Tag);
            //foreach (ListViewItem lvi in lvModList.SelectedItems)
            //{
            //    omod o = (omod)lvModList.SelectedItems[0].Tag;
            //    toolStripProcessingStatusLabel.Text = "Retrieving Tes Nexus information for "+o.ModName;
            //    o.AddNexusInfo();
            //    try
            //    {
            //        pictureBox1.Image = o.image;
            //    }
            //    catch (Exception ex)
            //    {
            //        Program.logger.WriteToLog("Could not load image: " + ex.Message, Logger.LogLevel.Low);
            //    }
            //    toolStripProcessingStatusLabel.Text = "";
            //}
        }

        private void addPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvModList.SelectedItems.Count != 1) return;
            // browse for file
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.Filter = "Images (*.jpg)|*.jpg";
            if (DialogResult.OK == ofdlg.ShowDialog())
            {
                // add it
                omod o = (omod)lvModList.SelectedItems[0].Tag;
                o.AddPicture(ofdlg.FileName);
            }
        }

        private void openAllModUpdatesSitesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvModList.Items)
            {
                omod o = (omod)lvi.Tag;
                if (o.Website.Length > 0 && o.bUpdateExists)
                {
                    string s = o.Website;
                    if (!s.ToLower().StartsWith("http://")) s = "http://" + s;
                    System.Diagnostics.Process p = System.Diagnostics.Process.Start(s);
                    if (p != null) p.Close();
                }
            }
        }

        private void backgroundImageLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!bClosingForm)
            {
                System.Threading.Thread.Sleep(10);
                System.Threading.Monitor.Enter(Program.OmodImagePreloadList);
                if (Program.OmodImagePreloadList.Count > 0)
                {
                    omod o = Program.OmodImagePreloadList[0];
                    System.Threading.Monitor.Exit(Program.OmodImagePreloadList);

                    //omod o = e.Argument as omod;
                    //try
                    //{
                    //    //toolStripLblGuiMessage.Text = "Retrieving image for " + o.FileName;
                    //}
                    //catch { };
                    try
                    {
                        if (o.image == null)
                        {
                            //addPictureToolStripMenuItem.Visible = true;
                            //addInfoFromTesNexusToolStripMenuItem.Visible = true;
                            System.Threading.Monitor.Enter(Program.nexusInfoDownloadList);
                            Program.nexusInfoDownloadList.Add(o.FileName);
                            System.Threading.Monitor.Exit(Program.nexusInfoDownloadList);
                        }
                        if (currentlySelectedOmod == o)
                        {
                            if (pictureBox1.Image != null)
                            {
                                pictureBox1.Image.Dispose();
                                pictureBox1.Image = null;
                            }
                            pictureBox1.Image = o.image;
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.logger.WriteToLog("Could not load image: " + ex.Message, Logger.LogLevel.Low);
                    }

                    System.Threading.Monitor.Enter(Program.OmodImagePreloadList);
                    Program.OmodImagePreloadList.Remove(o);
                    System.Threading.Monitor.Exit(Program.OmodImagePreloadList);
                }
                else
                    System.Threading.Monitor.Exit(Program.OmodImagePreloadList);

            }

            //toolStripLblGuiMessage.Text = "";
        }

        private void backgroundTesNexusInfoAdder_DoWork(object sender, DoWorkEventArgs e)
        {
            //foreach (ListViewItem lvi in lvModList.SelectedItems)
            //{
            //    omod o = (omod)lvModList.SelectedItems[0].Tag;
            omod o = e.Argument as omod;
            toolStripProcessingStatusLabel.Visible = true;
            toolStripProcessingStatusLabel.Text = "Retrieving Tes Nexus information for " + o.ModName;
                o.AddNexusInfo();
                //try
                //{
                //    pictureBox1.Image = o.image;
                //}
                //catch (Exception ex)
                //{
                //    Program.logger.WriteToLog("Could not load image: " + ex.Message, Logger.LogLevel.Low);
                //}
                toolStripProcessingStatusLabel.Text = "";
                toolStripProcessingStatusLabel.Visible = false;
            //}
        }

        private void updatecheckstatuslabel_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Do you want to cancel mod update check?", "Update checking", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                backgroundNexusModUpdateChecker.CancelAsync();
            }
        }

        private void toolStripLblCK_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.WorkingDirectory = Program.gamePath;
            if (File.Exists(Path.Combine(Program.gamePath, "obse_loader.exe")))
            {
                process.StartInfo.FileName = Path.Combine(Program.gamePath, "obse_loader.exe");
                process.StartInfo.Arguments = "-editor";
            }
            else if (File.Exists("skse_loader.exe"))
            {
                process.StartInfo.FileName = Path.Combine(Program.gamePath, "skse_loader.exe");
                process.StartInfo.Arguments = "-altexe creationkit.exe";
            }
            else if (Program.bSkyrimMode)
            {
                process.StartInfo.FileName = Path.Combine(Program.gamePath, "CreationKit.exe");
            }
            else if (Program.bMorrowind)
            {
                process.StartInfo.FileName = Path.Combine(Program.gamePath, "TES Construction Set.exe");
            }
            else
            {
                process.StartInfo.FileName = Path.Combine(Program.gamePath, "TESConstructionSet.exe");
            }
            process.Start();
        }

        private void btnLaunchCreationKit_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = Program.gamePath;
            process.StartInfo.WorkingDirectory = Program.gamePath;
            if (File.Exists(Path.Combine(Program.gamePath, "obse_loader.exe")))
            {
                process.StartInfo.FileName = Path.Combine(Program.gamePath, "obse_loader.exe");
                process.StartInfo.Arguments = "-editor";
            }
            else if (File.Exists(Path.Combine(Program.gamePath, "skse_loader.exe")))
            {
                process.StartInfo.FileName = Path.Combine(Program.gamePath, "skse_loader.exe");
                process.StartInfo.Arguments = "-altexe creationkit.exe";
            }
            else if (Program.bSkyrimMode)
            {
                process.StartInfo.FileName = Path.Combine(Program.gamePath, "CreationKit.exe");
            }
            else if (Program.bMorrowind)
            {
                process.StartInfo.FileName = Path.Combine(Program.gamePath, "TES Construction Set.exe");
            }
            else
            {
                process.StartInfo.FileName = Path.Combine(Program.gamePath, "TESConstructionSet.exe");
            }
            process.Start();
        }

        private void lvModList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //"Date installed",
            //"Group",
            
            if (e.Column==ModNameColumn.Index)
            {
                cmbOmodSortOrder.SelectedItem = "File name";
            }
            else if (e.Column==ModAuthorColumn.Index)
            {
                cmbOmodSortOrder.SelectedItem = "Author";
            }
            else if (e.Column==ModVersionColumn.Index)
            {
                //cmbOmodSortOrder.SelectedItem = ;
            }
            else if (e.Column==ModNbFilesColumn.Index)
            {
                //cmbOmodSortOrder.SelectedItem = ;
            }
            else if (e.Column==ModNbPluginsColumn.Index)
            {
                //cmbOmodSortOrder.SelectedItem = ;
            }
            else if (e.Column==ScriptExistColumn.Index)
            {
                //cmbOmodSortOrder.SelectedItem = ;
            }
            else if (e.Column==DatePackedColumn.Index)
            {
                cmbOmodSortOrder.SelectedItem = "Date packed";
            }
            else if (e.Column==ConflictLevelColumn.Index)
            {
                cmbOmodSortOrder.SelectedItem = "Conflict level";
            }
            else if (e.Column==FileSizeColumn.Index)
            {
                cmbOmodSortOrder.SelectedItem = "File size";
            }
            else if (e.Column==ModIDColumn.Index)
            {
                cmbOmodSortOrder.SelectedItem = "Mod ID";
            }
        }

        private void btnLootSortPlugins_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process tmm = new System.Diagnostics.Process();
            tmm.StartInfo.FileName = Program.LOOTpath + "\\LOOT.exe";
            tmm.StartInfo.WorkingDirectory = Program.LOOTpath;
            //tmm.StartInfo.Arguments = "-g " + Program.gameName;
            tmm.Start();
            tmm.WaitForExit();
            Program.loadLoadOrderTxtFile();
            string[] esps = OblivionESP.GetActivePlugins();
            foreach (ListViewItem lvi in lvEspList.Items)
            {
                EspInfo ea = (EspInfo)lvi.Tag;
                ea.DateModified = File.GetLastWriteTime(Path.Combine(Program.DataFolderPath, ea.FileName));
            }
            lvEspList.Sort();

        }

        private void openLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", Program.logger.logFile);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string pressedkeys = Control.ModifierKeys.ToString();
            if (pressedkeys=="None")
            {
                Program.logger.WriteToLog("Default Window position - x:" + Properties.Settings.Default.MainFormX + " y:" + Properties.Settings.Default.MainFormY, Logger.LogLevel.Low);
                Program.logger.WriteToLog("Default Window Width:" + Properties.Settings.Default.MainFormW + " Height:" + Properties.Settings.Default.MainFormH, Logger.LogLevel.Low);

                if (Properties.Settings.Default.MainFormX >= Screen.PrimaryScreen.WorkingArea.X && Properties.Settings.Default.MainFormY >= Screen.PrimaryScreen.WorkingArea.Y
                    && Properties.Settings.Default.MainFormX < Screen.PrimaryScreen.Bounds.Width - 100
                    && Properties.Settings.Default.MainFormY < Screen.PrimaryScreen.Bounds.Height - 100)
                {
                    this.Location = new Point(Properties.Settings.Default.MainFormX, Properties.Settings.Default.MainFormY);
                }
                else
                {
                    Properties.Settings.Default.MainFormX = Screen.PrimaryScreen.WorkingArea.X;
                    Properties.Settings.Default.MainFormY = Screen.PrimaryScreen.WorkingArea.Y;
                    this.Location = new Point(Properties.Settings.Default.MainFormX, Properties.Settings.Default.MainFormY);
                }
                Program.logger.WriteToLog("Window position - x:" + this.Location.X + " y:" + this.Location.Y, Logger.LogLevel.Low);

                if (this.Location.X < Screen.PrimaryScreen.WorkingArea.X || this.Location.Y < Screen.PrimaryScreen.WorkingArea.Y
                    || this.Location.X > Screen.PrimaryScreen.WorkingArea.X + Screen.PrimaryScreen.Bounds.Width
                    || this.Location.Y > Screen.PrimaryScreen.WorkingArea.Y + Screen.PrimaryScreen.Bounds.Height)
                {
                    this.Location = new Point(0, 0);
                }
                Program.logger.WriteToLog("Final Window position - x:" + this.Location.X + " y:" + this.Location.Y, Logger.LogLevel.Low);

                if (Properties.Settings.Default.MainFormW > 100 && Properties.Settings.Default.MainFormH > 100)
                {
                    this.Width = Properties.Settings.Default.MainFormW;
                    this.Height = Properties.Settings.Default.MainFormH;
                }
                else
                {
                    Properties.Settings.Default.MainFormW = this.Width = Screen.PrimaryScreen.Bounds.Width - Properties.Settings.Default.MainFormX - 100;
                    Properties.Settings.Default.MainFormH = this.Height = Screen.PrimaryScreen.Bounds.Height - Properties.Settings.Default.MainFormY - 100;
                }
                Program.logger.WriteToLog("Window Width:" + this.Width + " Height:" + this.Height, Logger.LogLevel.Low);

                if (this.Width < 100 || this.Height < 100 || this.Width > Screen.PrimaryScreen.Bounds.Width || this.Height > Screen.PrimaryScreen.Bounds.Height)
                {
                    this.Width = Screen.PrimaryScreen.Bounds.Width - 100;
                    this.Height = Screen.PrimaryScreen.Bounds.Height - 100;
                }
                Program.logger.WriteToLog("Final Window Width:" + this.Width + " Height:" + this.Height, Logger.LogLevel.Low);
            }
        }


	}
}
