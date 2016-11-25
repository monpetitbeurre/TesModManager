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
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using BaseTools.Configuration;
using BaseTools.Configuration.Parsers;
using System.Net;
using System.Text;
using SV = BaseTools.Searching.StringValidator;
using BaseTools.Dialog;
using System.Threading;


namespace OblivionModManager {
	public partial class CreateModForm : Form {
		public omodCreationOptions ops=new omodCreationOptions();
		private bool EditingPlugins=true;
		private bool alreadyTES = false;
		private string InitialName;

		void UseAsImageToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (lvFiles.SelectedItems.Count != 1)
				return;
			
			
			string filename = lvFiles.SelectedItems[0].SubItems[1].Text;
			string extn = Path.GetExtension(filename);
			string[] valids = new string[] {".jpg", ".jpeg", ".bmp", ".gif", ".png"};
			
			bool valid = false;
			for(int i=0;i<valids.Length;i++)
			{
				if (string.Compare(valids[i], extn, true) == 0)
				{
					valid = true;
					break;
				}
			}
			
			if (!valid)
			{
				string vf = "";
				
				foreach(string s in valids)
				{
					if (vf.Length > 0)
						vf += ", ";
					vf += s;
				}
				
				MessageBox.Show("Invalid format. Valid formats are: " + vf, "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			
			try
			{
				ScreenshotPic.Image = new Bitmap(filename);
				ops.Image = filename;
			}
			catch(Exception ex)
			{
				MessageBox.Show("An error occurred: " + ex.Message);
			}
		}
		void EventSetup()
		{
			/*this.bEdReadme.Click += new System.EventHandler(this.bEdReadme_Click);
			this.bEdScript.Click += new System.EventHandler(this.bEdScript_Click);
			this.cmbCompType.SelectedIndexChanged += new System.EventHandler(this.cmbCompLevel1_SelectedIndexChanged);
			this.cmbCompType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbNoKeyPress);
			this.cmbDataCompLevel.SelectedIndexChanged += new System.EventHandler(this.cmbCompLevel1_SelectedIndexChanged);
			this.cmbDataCompLevel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbNoKeyPress);
			this.cmbModCompLevel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbNoKeyPress);
			this.lvFiles.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvFiles_AfterLabelEdit);
			this.lvFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvFiles_KeyDown);
			this.FilesContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.FilesContextMenu_Opening);
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			this.validateToolStripMenuItem.Click += new System.EventHandler(this.validateToolStripMenuItem_Click);
			this.importModDetailsToolStripMenuItem.Click += new System.EventHandler(this.importModDetailsToolStripMenuItem_Click);
			this.scanForDataFilesToolStripMenuItem.Click += new System.EventHandler(this.scanForDataFilesToolStripMenuItem_Click);
			this.viewRequiredDataFilesToolStripMenuItem.Click += new System.EventHandler(this.viewRequiredDataFilesToolStripMenuItem_Click);
			this.useAsImageToolStripMenuItem.Click += new System.EventHandler(this.UseAsImageToolStripMenuItemClick);
			this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
			this.bAddFromFolder.Click += new System.EventHandler(this.bAddFromFolder_Click);
			this.tbVersion.Leave += new System.EventHandler(this.tbVersion_Leave);
			this.tbVersion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DigitKeysOnly);
			this.rbPlugins.CheckedChanged += new System.EventHandler(this.rbCheckedChanged);
			this.rbData.CheckedChanged += new System.EventHandler(this.rbCheckedChanged);
			this.bEdDescription.Click += new System.EventHandler(this.bEdDescription_Click);
			this.bCreate.Click += new System.EventHandler(this.bCreate_Click);
			this.bUp.Click += new System.EventHandler(this.bUp_Click);
			this.bDown.Click += new System.  .
             * (this.bDown_Click);
			this.bAddZip.Click += new System.EventHandler(this.bAddZip_Click);
			this.bScreenshot.MouseLeave += new System.EventHandler(this.bScreenshot_MouseLeave);
			this.bScreenshot.Click += new System.EventHandler(this.bScreenshot_Click);
			this.bScreenshot.MouseEnter += new System.EventHandler(this.bScreenshot_MouseEnter);
			this.bGroups.Click += new System.EventHandler(this.bGroups_Click);
			this.bRemoveScreenshot.Click += new System.EventHandler(this.bRemoveScreenshot_Click);*/
			cbIncludeVersion.Checked = GlobalSettings.IncludeVersionNumber;
		}
		public void CreateModFormShared() {
			InitializeComponent();
			EventSetup();
            Program.logger.WriteToLog("Current Directory: " + Program.CurrentDir, Logger.LogLevel.High);
			DialogResult=DialogResult.Cancel;
			//Set dud context menus
			cmbCompType.ContextMenu=DudMenu;
			cmbDataCompLevel.ContextMenu=DudMenu;
			cmbModCompLevel.ContextMenu=DudMenu;
			//Set the combo box indexes
            //cmbCompType.SelectedIndex=1;
            //cmbDataCompLevel.SelectedIndex=1;
            //cmbModCompLevel.SelectedIndex=0;
			//Init arrays
			ops.espPaths=new string[0];
			ops.esps=new string[0];
			ops.espSources=new string[0];
			ops.DataFilePaths=new string[0];
			ops.DataFiles=new string[0];
			ops.DataSources=new string[0];
			//cms groups icons
			for(int i=0;i<Settings.omodGroups.Count;i++) {
				ToolStripMenuItem tsmi=new ToolStripMenuItem(Settings.omodGroups[i].Name);
				tsmi.CheckOnClick=true;
				cmsGroups.Items.Add(tsmi);
			}
			try {
				cmbDataCompLevel.SelectedIndex=(byte)Settings.dataCompressionLevel;
				cmbModCompLevel.SelectedIndex=(byte)Settings.omodCompressionLevel;
                cmbCompType.SelectedIndex = (byte)Settings.dataCompressionType;
			} catch {
				MessageBox.Show("Invalid compression level.\n"+
				                "This could be the result of a corrupted save file.\n"+
				                "If you notice any other strange behaviour, use the reset button on the options page to reset your settings to their defaults.", "Error");
				cmbDataCompLevel.SelectedIndex=1;
				cmbModCompLevel.SelectedIndex=0;
                cmbCompType.SelectedIndex = 0;
			}

            CreateModToolTip.SetToolTip(rdSystemMod, "A system mod is for ENB or HDT files not installed under the Data folder");
            CreateModToolTip.SetToolTip(rdDataMod, "Standard mods are installed under the Data folder");
            CreateModToolTip.SetToolTip(cbOmod2, "Version 2 mods can be extracted or manipulated using normal tools");
        }
		public CreateModForm() {
			CreateModFormShared();
			//Set some default options
			ops.readme="";
			ops.script="";
			ops.Description="";
            //cmbCompType.SelectedItem=(Settings.dataCompressionType== CompressionType.SevenZip?"7-zip":"zip");
            //cmbDataCompLevel.SelectedIndex = 0; // (byte)Settings.dataCompressionLevel;
            cbOmod2.Checked = Settings.bOmod2IsDefault;
            //if (cbOmod2.Checked && Settings.bSaveOmod2AsZip)
            //    cmbCompType.Text = "zip";
            cbOmod2_CheckedChanged(null, null);
            //Make sure the file will generate successfully
            if (File.Exists(Path.Combine(Settings.omodDir,tbName.Text + " 1.0.omod")))
            {
				int i=1;
                while (File.Exists(Path.Combine(Settings.omodDir, tbName.Text + i.ToString() + " 1.0.omod"))) i++;
				tbName.Text+=i.ToString();
			}
			InitialName=tbName.Text;
		}
        public CreateModForm(string path)
        {
            CreateModFormShared();
            //cmbCompType.SelectedIndex = (byte)Settings.dataCompressionType;
            //cmbDataCompLevel.SelectedIndex = (byte)Settings.dataCompressionLevel;
            cbOmod2.Checked = Settings.bOmod2IsDefault;
            //if (cbOmod2.Checked && Settings.bSaveOmod2AsZip)
            //    cmbCompType.Text = "zip";
            cbOmod2_CheckedChanged(null, null);
            AddFilesFromFolder(path);
        }
		public CreateModForm(omod o) {
			CreateModFormShared();
			ops.readme=o.GetReadme();
			ops.script=o.GetScript();
			ops.Description=o.Description;
            cmbCompType.Text = ((CompressionType)Settings.dataCompressionType == CompressionType.SevenZip ? "7-zip" : "zip");
            cmbDataCompLevel.Text = Settings.dataCompressionLevel.ToString();
            cmbCompType.Text = (o.CompType == CompressionType.SevenZip ? "7-zip" : "zip");
            cbOmod2.Checked = Settings.bOmod2IsDefault?true:o.bOmod2;
            rdSystemMod.Checked = o.bSystemMod;
            //if (cbOmod2.Checked && Settings.bSaveOmod2AsZip)
            //    cmbCompType.Text = "zip";
            cbOmod2_CheckedChanged(null, null);
            if (ops.readme == null) ops.readme = "";
			if(ops.script==null) ops.script="";
			if(ops.Description==null) ops.Description="";
			ScreenshotPic.Image=o.image;
			ops.Image=o.GetImage();
			tbName.Text=o.ModName;
			tbVersion.Text=o.Version;
			tbAuthor.Text=o.Author;
			tbEmail.Text=o.Email;
			tbWebsite.Text=o.Website;
			string s=o.GetPlugins();
			if(s!=null) AddFilesFromFolder(s, o.ModName, true);
			s=o.GetDataFiles();
			if(s!=null) AddFilesFromFolder(s, o.ModName, true);
			for(int i=0;i<cmsGroups.Items.Count;i++) {
				if((o.group&(ulong)(1<<i)) > 0) ((ToolStripMenuItem)cmsGroups.Items[i]).Checked=true;
			}
		}

        public bool ShowForm(bool bClearForm)
        {
            if (bClearForm)
            {
                CreateModForm cfm = new CreateModForm();
                if (cfm.ShowDialog() == DialogResult.OK) return true;
            }
            else
            {
                lvFiles.Items.Clear();
                if (EditingPlugins)
                {
                    for (int i = 0; i < ops.esps.Length; i++)
                    {
                        lvFiles.Items.Add(new ListViewItem(new string[] { ops.espPaths[i], ops.esps[i], ops.espSources[i] }));
                    }
                }
                else
                {
                    for (int i = 0; i < ops.DataFiles.Length; i++)
                    {
                        lvFiles.Items.Add(new ListViewItem(new string[] { ops.DataFilePaths[i], ops.DataFiles[i], ops.DataSources[i] }));
                    }
                }

                Application.UseWaitCursor = false;
                Application.DoEvents();
                // for some reason, the first one always cancels out...
                if (this.ShowDialog() == DialogResult.Cancel)
                {
                    if (this.ShowDialog() == DialogResult.OK) return true;
                }
                else
                    return true;
            }
            return false;
        }
        public static bool ShowForm()
        {
			CreateModForm cfm=new CreateModForm();
			if(cfm.ShowDialog()==DialogResult.OK) return true;
			return false;
		}
		public static bool ShowForm(omod o) {
			CreateModForm cfm=new CreateModForm(o);
			if(cfm.ShowDialog()==DialogResult.OK) return true;
			return false;
		}

		private void DigitKeysOnly(object sender, KeyPressEventArgs e) {
            e.Handled = false; //if (e.KeyChar != '.' && e.KeyChar != '\b' && !char.IsDigit(e.KeyChar)) e.Handled = true;
		}

		private void cmbNoKeyPress(object sender, KeyPressEventArgs e) {
			e.Handled=true;
		}

		private void bEdReadme_Click(object sender, EventArgs e) {
			TextEditor te=new TextEditor("Edit readme", ops.readme, true, true);
			if(te.ShowDialog()==DialogResult.Yes) ops.readme=te.Result;
		}
		private void AppendFileList(string[] files, StringBuilder sb)
		{
			foreach(string f in files)
			{
				sb.AppendLine(f.Replace(@"\", @"\\"));
			}
		}
		private void bEdScript_Click(object sender, EventArgs e) {
			Forms.ScriptEditor se=new Forms.ScriptEditor(ops.script);
			StringBuilder sb = new StringBuilder();
			SaveListboxContents();
			sb.AppendLine("Plugin Files:");
			AppendFileList(ops.espPaths, sb);
			sb.AppendLine();
			sb.AppendLine("Data Files:");
			AppendFileList(ops.DataFilePaths, sb);
			
			se.FilesList = sb.ToString();
			
			if(se.ShowDialog()==DialogResult.Yes) ops.script=se.Result;
		}

		private void bEdDescription_Click(object sender, EventArgs e) {
			TextEditor te=new TextEditor("Edit description", ops.Description, false, true);
			if(te.ShowDialog()==DialogResult.Yes) ops.Description=te.Result;
		}

		private void SaveListboxContents() {
			//Some sanity checking and security
			for(int i=0;i<lvFiles.Items.Count;i++) {
				if(lvFiles.Items[i].Text=="")
                    lvFiles.Items.RemoveAt(i--);
				else if(!Program.IsSafeFileName(lvFiles.Items[i].Text))
                    lvFiles.Items.RemoveAt(i--);
				lvFiles.Items[i].Text=lvFiles.Items[i].Text.Replace('/', '\\');
			}
			//Update the file lists
			string[] Files=new string[lvFiles.Items.Count];
			string[] Paths=new string[lvFiles.Items.Count];
			for(int i=0;i<lvFiles.Items.Count;i++) {
				Files[i]=lvFiles.Items[i].SubItems[1].Text;
				Paths[i]=lvFiles.Items[i].Text;
			}
			if(EditingPlugins) {
				ops.esps=Files;
				ops.espPaths=Paths;
			} else {
				ops.DataFiles=Files;
				ops.DataFilePaths=Paths;
			}
		}

        private void CreateOmod(omodCreationOptions ops, string omodfname, ulong groups, bool bActivateNewMod, bool bHidden)
        {
            try
            {
                Enabled = false;


                omod.CreateOmod(ops, omodfname, groups);

                if (bActivateNewMod)
                {
                    omod newmod = Program.Data.GetMod(omodfname);

                    if (true == newmod.Activate(true))
                        MessageBox.Show(omodfname + " created and activated successfully", "Message");
                    else
                        MessageBox.Show(omodfname + " created successfully but activation failed", "Message");
                }
                else
                    MessageBox.Show(omodfname + " created successfully", "Message");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (obmmCancelException)
            {
                Enabled = true;
                Focus();
                try { File.Delete(Path.Combine(Settings.omodDir,omodfname)); }
                catch { }
            }
            catch (Exception ex)
            {
                Enabled = true;
                Focus();
                MessageBox.Show("An error occurred while creating the omod file.\n" + ex.Message, "Error");
                try { File.Delete(Path.Combine(Settings.omodDir,omodfname)); }
                catch { }
            }
            GC.Collect();
        }

		private void bCreate_Click(object sender, EventArgs e) {
			Settings.dataCompressionType=(cmbCompType.SelectedItem.ToString()=="7-zip"?CompressionType.SevenZip:CompressionType.Zip);
            Settings.dataCompressionLevel = (CompressionLevel)Enum.Parse(typeof(CompressionLevel), cmbDataCompLevel.Text.Replace(" ",""),true);
            Settings.omodCompressionLevel = (CompressionLevel)Enum.Parse(typeof(CompressionLevel), cmbModCompLevel.Text.Replace(" ", ""), true);
			tbVersion_Leave(null, null);
			ops.Name=tbName.Text;
            ops.Version = tbVersion.Text;
            ops.bOmod2 = cbOmod2.Checked;
            ops.bSystemMod = rdSystemMod.Checked;
			string omodfname= ops.Name;
            if (cbIncludeVersion.Checked)
            {
                omodfname=ops.Name+" "+ops.Version;
                //omodfname=ops.Name+" "+ops.MajorVersion;
                //if(ops.MinorVersion!=-1) {
                //    omodfname+="."+ops.MinorVersion;
                //    if(ops.BuildVersion!=-1) omodfname+="."+ops.BuildVersion;
                //}
			}
            omodfname+=".omod"+ (ops.bOmod2 ? "2" + (Settings.bSaveOmod2AsZip ? (cmbCompType.SelectedItem.ToString() == "7-zip" ? ".7z" : ".zip") : "") : "");

			//Sanity check and some warnings
			if(tbName.Text=="") {
				MessageBox.Show("You must enter a mod name", "Error");
				return;
			}
			if(tbName.Text.IndexOfAny(Path.GetInvalidFileNameChars())!=-1) {
				MessageBox.Show("The mod name contains invalid characters\n"+
				                "The mod name may not contain '/', '\\', ':', '*', '?', '\"', '<', '>' or '|'", "Error");
			}
			if(tbEmail.Text.IndexOf('?')!=-1) {
				MessageBox.Show("Illegal character in email address", "Error");
				return;
			}
			if(tbAuthor.Text=="" && Settings.bWarnAboutMissingInfo) {
				if(MessageBox.Show("You haven't entered an author name.\n"+
				                   "Are you sure you wish to continue?", "Warning", MessageBoxButtons.YesNo)!=DialogResult.Yes) {
					return;
				}
			}
            if (ops.Description == "" && Settings.bWarnAboutMissingInfo)
            {
				if(MessageBox.Show("You haven't created a description.\n"+
				                   "Are you sure you wish to continue?", "Warning", MessageBoxButtons.YesNo)!=DialogResult.Yes) {
					return;
				}
			}

            // check if exists (update? replacement? patch? option?
            omod oldmod = Program.Data.GetModByName(tbName.Text);
            bool bActivateNewMod = false;
            if (oldmod != null)
            {
                if (MessageBox.Show("A mod by this name already exists with version "+oldmod.Version+".\n\n" +
                                    "* If this is an upgrade then the existing mod will be removed and this one will be installed instead. \n\n" +
                                    "* If this is a patch or an option, you should keep the existing mod in place and install this one next to it. \n\n" +
                                    "* If the existing mod is active and you chose to update, the existing one will be deactivated and the new one activated.\n\n" +
                                   "Do you wish to remove the existing mod and put this one instead?", "Creating mod version "+ops.Version, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
                else
                {
                    if (oldmod.Conflict == ConflictLevel.Active)
                        bActivateNewMod = true;
                    if (File.Exists(Path.Combine(Program.BackupDir,oldmod.LowerFileName))) File.Delete(Path.Combine(Program.BackupDir,oldmod.LowerFileName));
                    oldmod.Close();
                    try
                    {
                        File.Move(Path.Combine(Settings.omodDir,oldmod.LowerFileName), Path.Combine(Program.BackupDir,oldmod.LowerFileName));
                    }
                    catch { };
                    oldmod.DeletionDeactivate();
                    omod.Remove(oldmod.LowerFileName);
                    Program.Data.omods.Remove(oldmod);
                }
            }
            else if (File.Exists(Path.Combine(Settings.omodDir,omodfname)))
            {
                if (MessageBox.Show("These settings will create a file with the same name as an already existing one.\n" +
                                   "Do you wish to overwrite it?", "Error", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
                else
                {
                    if (File.Exists(Program.BackupDir + omodfname)) File.Delete(Program.BackupDir + omodfname);
                    omod.Remove(omodfname);
                    File.Move(Path.Combine(Settings.omodDir,omodfname), Program.BackupDir + omodfname);
                }
            }
            validateToolStripMenuItem_Click(null, null);
			//if(rbData.Checked) rbPlugins.Checked=true; else rbData.Checked=true;
			validateToolStripMenuItem_Click(null, null);
			SaveListboxContents();
			//Set the rest of the options
            ops.CompressionType = (cmbCompType.Text == "7-zip" ? CompressionType.SevenZip : CompressionType.Zip);
            ops.DataFileCompresionLevel = (CompressionLevel)Enum.Parse(typeof(CompressionLevel), cmbDataCompLevel.Text.Replace(" ", ""), true);
            ops.omodCompressionLevel = (CompressionLevel)Enum.Parse(typeof(CompressionLevel), cmbModCompLevel.Text.Replace(" ", ""), true);
			ops.Author=tbAuthor.Text.Trim();
			ops.website=tbWebsite.Text.Trim();
			ops.email=tbEmail.Text.Trim();
			
			ulong groups=0;
			for(int i=0;i<cmsGroups.Items.Count;i++) {
				if(((ToolStripMenuItem)cmsGroups.Items[i]).Checked) groups|=(ulong)((ulong)0x01<<i);
			}

            CreateOmod(ops, omodfname, groups, bActivateNewMod, oldmod!=null?oldmod.Hidden:false);

            //try {
            //    Enabled=false;
            //    omod.CreateOmod(ops, omodfname, groups);
            //    if (bActivateNewMod)
            //    {
            //        omod newmod = Program.Data.GetMod(omodfname);
            //        if (oldmod.Hidden)
            //            newmod.Hide();

            //        if (true==newmod.Activate(true))
            //            MessageBox.Show(omodfname + " created and activated successfully", "Message");
            //        else
            //            MessageBox.Show(omodfname + " created successfully but activation failed", "Message");
            //    }
            //    else
            //        MessageBox.Show(omodfname + " created successfully", "Message");
            //    DialogResult=DialogResult.OK;
            //    Close();
            //}catch(obmmCancelException) {
            //    Enabled=true;
            //    Focus();
            //    try { File.Delete(Settings.omodDir+omodfname); } catch { }
            //} catch(Exception ex) {
            //    Enabled=true;
            //    Focus();
            //    MessageBox.Show("An error occurred while creating the omod file.\n"+ex.Message, "Error");
            //    try { File.Delete(Settings.omodDir+omodfname); } catch { }
            //}
            //GC.Collect();
		}

		public void UpdateListView() {
			lvFiles.Items.Clear();
			if(EditingPlugins) {
				for(int i=0;i<ops.esps.Length;i++) {
					lvFiles.Items.Add(new ListViewItem(new string[] { ops.espPaths[i], ops.esps[i], ops.espSources[i] }));
				}
			} else {
				for(int i=0;i<ops.DataFiles.Length;i++) {
					lvFiles.Items.Add(new ListViewItem(new string[] { ops.DataFilePaths[i], ops.DataFiles[i], ops.DataSources[i]}));
				}
			}
        }

		private void rbCheckedChanged(object sender, EventArgs e) {
			if(!((RadioButton)sender).Checked) return;
			//Causes rediculus slowdown
			//RemoveListBoxDuplicates();
			SaveListboxContents();
			if(rbPlugins.Checked) {
				EditingPlugins=true;
				bUp.Enabled=true;
				bDown.Enabled=true;
			} else {
				EditingPlugins=false;
				bUp.Enabled=false;
				bDown.Enabled=false;
			}
			UpdateListView();
		}

		private void bAdd_Click(object sender, EventArgs e) {
			OpenDialog.Multiselect=true;
			if(rbPlugins.Checked) {
				OpenDialog.Title="Select plugins to add";
				OpenDialog.Filter="plugin files (*.esp,*.esm)|*.esp;*.esm";
			} else {
				OpenDialog.Title="Select other files to add";
				OpenDialog.Filter="All files|*.*";
			}
			if(OpenDialog.ShowDialog()==DialogResult.OK) {
				foreach(string s in OpenDialog.FileNames) {
					string path=s.ToLower();
					if(rbData.Checked) {
                        if (path.StartsWith(Path.Combine(Program.CurrentDir, Program.DataFolderName) + "\\"))
                        {
                            path = s.Substring((Path.Combine(Program.CurrentDir, Program.DataFolderName) + "\\").Length);
						} else if(path.StartsWith(Program.DataFolderName+"\\")) {
							path=s.Substring(5);
						} else {
							path=Path.GetFileName(s);
						}
					} else {
						path=Path.GetFileName(s);
					}
					lvFiles.Items.Add(new ListViewItem(new string[] { path, s }));
				}
			}
		}
		public void AddFilesFromFolder(string folder)
		{
			AddFilesFromFolder(folder, null, false);
		}
		public void AddFilesFromFolder(string folder, string zipname, bool bAutomatic)
        {
            bool bModeESPToDataFolder = false;
			bool usefoldername = false;
			if (zipname == null)
			{
				usefoldername = true;
				zipname = new DirectoryInfo(folder).Name;
			}
			
			string addedname = zipname;
			if (addedname.IndexOf('.') != -1 && !usefoldername)
			{
				addedname = Path.GetFileNameWithoutExtension(addedname);
			}
			
			if(!Path.IsPathRooted(folder)) folder=Path.GetFullPath(folder);
			string readme=null;
			{
				string[] Files;
				SaveListboxContents();
				//Check for esps in subdirectorys
                if (!bAutomatic)
                {
                    foreach (string dir in Directory.GetDirectories(folder))
                    {
                        string[] esps=Directory.GetFiles(dir, "*.esp", SearchOption.AllDirectories);
                        string[] esms=Directory.GetFiles(dir, "*.esm", SearchOption.AllDirectories);
                        if (esps.Length > 0 || esms.Length > 0)
                        {
                            if (MessageBox.Show("Warning - this folder contains esp files in sub directories." +
                                                (esps.Length > 0 ? "\n* " + esps[0].Replace(dir+"\\", "") : "") + (esms.Length > 0 ? "\n* " + esms[0].Replace(dir+"\\", "") : "") +
                                               "\nThis mod may have been packed in a non standard way or contains multiple versions." +
                                               "\nThis requires a script to tell TesModManager what to do with any optional esps." +
                                               "\nYou can copy the mod to a new folder and set it up as you'd like, and then use " +
                                               "\n'Add folder' or TMM can move the esp files to the right place for you." +
                                               "\n\nDo you want to move the esp files to the Data folder?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                bModeESPToDataFolder = true;
                            break;
                        }
                    }
                }
				//readme
				Files=Directory.GetFiles(folder, "*readme*.txt");
				if(Files.Length==0) Files=Directory.GetFiles(folder, "*readme*.rtf");
				if(Files.Length>0) {
                    if (ops.readme == "" || bAutomatic || MessageBox.Show("Overwrite current readme with contents of '" + Files[0] + "'?",
					                                   "Question", MessageBoxButtons.YesNo)==DialogResult.Yes) {
						readme=Files[0];
						ops.readme=Program.ReadAllText(readme);
					}
				} else {
					Files=Directory.GetFiles(folder, "*.txt");
					if(Files.Length==0) Files=Directory.GetFiles(folder, "*.rtf");
					if(Files.Length==1) {
                        if (ops.readme == "" || bAutomatic || MessageBox.Show("Overwrite current readme with contents of '" + Files[0] + "'?",
						                                   "Question", MessageBoxButtons.YesNo)==DialogResult.Yes) {
							readme=Files[0];
							ops.readme=Program.ReadAllText(readme);
						}
					}
				}
				
				string omodCD = folder + Program.omodConversionData;
				
                //if (!Directory.Exists(omodCD) && zipname != null)
                //{
                //    zipname = zipname.Replace(".zip", "").Replace(".7z", "").Replace(".rar", "");
                //    //omodCD = @"obmm\ocd\" + zipname;
                //}
				
				if (zipname != null && OCDCheck(Path.GetFileNameWithoutExtension(zipname)))
				{
					
				}
				else if (Directory.Exists(omodCD))
				{
					//script
					Files=Directory.GetFiles(folder+Program.omodConversionData, "script.txt");
					if(Files.Length>0) {
                        if (ops.script == "" || bAutomatic||MessageBox.Show("Overwrite current script with contents of 'script.txt'?",
						                                   "Question", MessageBoxButtons.YesNo)==DialogResult.Yes) {
							ops.script=Program.ReadAllText(Files[0]);
						}
					}
					//screenshot
					Files=Directory.GetFiles(folder+Program.omodConversionData, "screenshot");
					if(Files.Length>0) {
                        if (ops.Image == null || bAutomatic||MessageBox.Show("Overwrite current screenshot with contents of " + Files[0] + "?",
						                                    "Question", MessageBoxButtons.YesNo)==DialogResult.Yes) {
							try {
								ScreenshotPic.Image=Image.FromFile(Files[0]);
								ops.Image=Files[0];
							} catch {
								MessageBox.Show("The image file appears to be corrupt", "Error");
							}
						}
					}
					//data
					Files=Directory.GetFiles(folder+Program.omodConversionData, "config");
					if(Files.Length>0) {
						if(bAutomatic || RequestImport()) {
							BinaryReader br=new BinaryReader(File.OpenRead(folder+Program.omodConversionData+"config"));
							byte version=br.ReadByte();
							if(version>Program.CurrentOmodVersion) {
								MessageBox.Show("This version of tmm is too old to read the omod conversion data\n"+
								                "Conversion data is from a version "+version+" omod, but this version of obmm can only read up to version "+Program.CurrentOmodVersion,
								                "Error");
							} else {
								tbName.Text=br.ReadString();
								int MajorVersion=br.ReadInt32();
								int MinorVersion=br.ReadInt32();
								tbVersion.Text=MajorVersion.ToString();
								if(MinorVersion!=-1)tbVersion.Text+="."+MinorVersion.ToString();
								tbAuthor.Text=br.ReadString();
								tbEmail.Text=br.ReadString();
								tbWebsite.Text=br.ReadString();
								ops.Description=br.ReadString();
                                ops.Version = tbVersion.Text;
								if(version>=2) br.ReadInt64();//Creation date
								else br.ReadString();
								br.ReadByte();  //Don't really want to read in the compression type
								if(version>=1) {
									int i=br.ReadInt32();
									if(i!=-1) tbVersion.Text+="."+i;
								}
								br.Close();
							}
						}
					}
				}
				else if (File.Exists(folder + "ocd.xbt"))
				{
					if (RequestImport())
						LoadXBT(new GeneralConfig().LoadConfiguration(folder + "ocd.xbt"));
				}
                else if (Directory.Exists(Path.Combine(folder,"fomod")))
                {
                    //script
                    Files = Directory.GetFiles(Path.Combine(folder, "fomod"), "ModuleConfig.xml");
                    if (Files.Length > 0)
                    {
                        if (ops.script == "" || bAutomatic || MessageBox.Show("Overwrite current script with contents of 'ModuleConfig.xml'?",
                                                           "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            ops.script = ""+(char)(ScriptType.xml)+Program.ReadAllText(Files[0]);
                        }
                    }
                    Files = Directory.GetFiles(Path.Combine(folder, "fomod"), "script.cs");
                    if (Files.Length > 0)
                    {
                        if (ops.script == "" || bAutomatic|| MessageBox.Show("Overwrite current script with contents of 'script.cs'?",
                                                           "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            ops.script = "" + (char)(ScriptType.cSharp) + Program.ReadAllText(Files[0]);
                        }
                    }
                    //data
                    string infofile = Path.Combine(Path.Combine(folder, "fomod"), "info.xml");
                    if (File.Exists(infofile))
                    {
                        if (bAutomatic || RequestImport("fomod"))
                        {
                            try
                            {
                                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                                doc.Load(infofile);
                                string modName = "", modVersion = "", modAuthor = "", modDescription = "", modWebsite = "";
                                omod.DecodeFomodXML(doc, ref modName, ref modVersion, ref modAuthor, ref modDescription, ref modWebsite);
                                tbName.Text = modName; tbVersion.Text = modVersion; tbAuthor.Text = modAuthor; ops.Description = modDescription; tbWebsite.Text = modWebsite;
                                //System.Xml.XmlNode node = doc.SelectSingleNode("//fomod/Name/text()");
                                //try { tbName.Text = doc.SelectSingleNode("//fomod/Name/text()").Value; }catch { };
                                //try { tbVersion.Text = doc.SelectSingleNode("//fomod/Version/text()").Value; }catch { };
                                //try { tbAuthor.Text = doc.SelectSingleNode("//fomod/Author/text()").Value; }catch { };
                                ////                            tbEmail.Text = doc.SelectSingleNode("//fomod/Email/text()").Value;
                                //try { tbWebsite.Text = doc.SelectSingleNode("//fomod/Website/text()").Value; }catch { };
                                //try { ops.Description = (doc.SelectSingleNode("//fomod/Description/text()")!=null?doc.SelectSingleNode("//fomod/Description/text()").Value:""); }catch { };
                                string modid = Program.GetModID(zipname);
                                if (modid.Length > 0 && (modWebsite == null || modWebsite.Length == 0))
                                {
                                    ops.website = tbWebsite.Text = "http://www.nexusmods.com/" + Program.gameName + "/mods/" + modid;
                                }
                            }
                            catch { };
                        }
                    }
                }
				else if (zipname != null)
				{
					/*if (ocdList == null)
					{
						if (File.Exists(@"obmm\ocd.xbt"))
						{
							ocdList = new GeneralConfig().LoadConfiguration(@"obmm\ocd.xbt");
						}
						else
							ocdList = new ConfigList();
					}
					
					ConfigList config;
					
					
					if ((config = ocdList.GetSection(zipname)) != null)
					{
						LoadXBT(config);
					}*/
					//if (!OCDCheck(zipname))
					{
						try
						{
                            string tesid = Program.GetModID(zipname);
								
							if (tesid.Length>0 && (ops.Name==null ||ops.Name.Length==0))
							{
								if (!alreadyTES && (GlobalSettings.AlwaysImportTES || MessageBox.Show("Import info from Nexus?", "Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
									                == DialogResult.Yes))
								{
									if (Program.KeyPressed(16))
										GlobalSettings.AlwaysImportTES = true;
                                    ApplyTESNexus(zipname, false);
                                    GlobalSettings.LastTNID = tesid;
								}
							}
						}
						catch(Exception)
						{
						}
					}
				}
			}
            {
				List<string> MatchedFiles=new List<string>();
				List<string> MatchedPaths=new List<string>();
				string[] NewFiles;
				string[] NewPaths;
				string[] NewSources;
				
				//esps/esms
				MatchedPaths.AddRange(Directory.GetFiles(folder, "*.esp", SearchOption.AllDirectories));
				MatchedPaths.AddRange(Directory.GetFiles(folder, "*.esm", SearchOption.AllDirectories));
				MatchedFiles.AddRange(MatchedPaths);
				
				
				
				for(int i=0;i<MatchedPaths.Count;i++)
					MatchedPaths[i] = MatchedPaths[i].Substring(folder.Length);
				
				if (ops.espPaths.Length > 0)
				{
					List<string> espPaths, esps, espSources;
					espPaths = new List<string>(ops.espPaths);
					esps = new List<string>(ops.esps);
					espSources = new List<string>(ops.espSources);
					for(int i=0;i<espPaths.Count;i++)
					{
						for(int j=0;j<MatchedPaths.Count;j++)
						{
							if (string.Compare(espPaths[i], MatchedPaths[j], true) == 0)
							{
								espPaths.RemoveAt(i);
								esps.RemoveAt(i);
								espSources.RemoveAt(i);
								i--;
							}
						}
					}
					ops.espPaths = espPaths.ToArray();
					ops.esps = esps.ToArray();
					ops.espSources = espSources.ToArray();
				}
				NewFiles=new string[ops.esps.Length+MatchedFiles.Count];
				NewPaths=new string[ops.esps.Length+MatchedFiles.Count];
				NewSources=new string[ops.esps.Length+MatchedFiles.Count];
				Array.Copy(ops.esps, NewFiles, ops.esps.Length);
				Array.Copy(MatchedFiles.ToArray(), 0, NewFiles, ops.esps.Length, MatchedFiles.Count);
				Array.Copy(ops.espPaths, NewPaths, ops.esps.Length);
				Array.Copy(ops.espSources, NewSources, ops.espSources.Length);
				for(int i=ops.espSources.Length;i<NewSources.Length;i++)
					NewSources[i] = addedname;
				
				Array.Copy(MatchedPaths.ToArray(), 0, NewPaths, ops.esps.Length, MatchedFiles.Count);
				ops.esps=NewFiles;
                if (bModeESPToDataFolder)
                {
                    for (int i = 0; i < NewPaths.Length;i++ )
                    {
                        NewPaths[i] = Path.GetFileName(NewPaths[i]);
                    }
                }
                ops.espPaths=NewPaths;
				ops.espSources = NewSources;
				//data files
				MatchedPaths.Clear();
				MatchedPaths.AddRange(Directory.GetFiles(folder, "*", SearchOption.AllDirectories));
				foreach(string s in MatchedFiles)
					MatchedPaths.Remove(s);
				
				if (ops.DataFilePaths.Length > 0)
				{
					List<string> rels = new List<string>();
					
					foreach(string s in MatchedPaths)
					{
						rels.Add(s.Substring(folder.Length));
					}
					
					List<string> DataFilePaths, DataFiles, DataSources;
					DataFilePaths = new List<string>(ops.DataFilePaths);
					DataFiles = new List<string>(ops.DataFiles);
					DataSources = new List<string>(ops.DataSources);
					for(int i=0;i<DataFilePaths.Count;i++)
					{
						for(int j=0;j<MatchedPaths.Count && i>0;j++)
						{
							if (string.Compare(DataFilePaths[i], rels[j], true) == 0)
							{
								DataFilePaths.RemoveAt(i);
								DataFiles.RemoveAt(i);
								DataSources.RemoveAt(i);
								i--;
							}
						}
					}
					ops.DataFilePaths = DataFilePaths.ToArray();
					ops.DataFiles = DataFiles.ToArray();
					ops.DataSources = DataSources.ToArray();
				}
				
				for(int i=0;i<MatchedPaths.Count;i++)
				{
					if(MatchedPaths[i].Contains(Program.omodConversionData)|| // MatchedPaths[i]==readme||   // readme needs to be kept as the script might refer to it
					   string.Compare(System.IO.Path.GetFileName(MatchedPaths[i]), "archiveinvalidation.txt", true) == 0 ||
					   string.Compare(System.IO.Path.GetFileName(MatchedPaths[i]), "ocd.xbt", true) == 0) {
						MatchedPaths.RemoveAt(i--);
						continue;
					}
					FileInfo fi=new FileInfo(MatchedPaths[i]);
					if((fi.Attributes&(FileAttributes.System|FileAttributes.Hidden))!=0) {
						MatchedPaths.RemoveAt(i--);
						continue;
					}
				}
				MatchedFiles.Clear();
				MatchedFiles.AddRange(MatchedPaths);
                for (int i = 0; i < MatchedPaths.Count; i++)
                {
                    MatchedPaths[i] = MatchedPaths[i].Substring(folder.Length);
                    if (MatchedPaths[i].ToLower().StartsWith(Program.DataFolderName+"\\"))
                        MatchedPaths[i] = MatchedPaths[i].Substring((Program.DataFolderName+"\\").Length);
                }
				NewFiles=new string[ops.DataFiles.Length+MatchedFiles.Count];
				NewPaths=new string[ops.DataFiles.Length+MatchedFiles.Count];
				NewSources=new string[ops.DataSources.Length+MatchedFiles.Count];
				Array.Copy(ops.DataFiles, NewFiles, ops.DataFiles.Length);
				Array.Copy(MatchedFiles.ToArray(), 0, NewFiles, ops.DataFiles.Length, MatchedFiles.Count);
				Array.Copy(ops.DataFilePaths, NewPaths, ops.DataFiles.Length);
				Array.Copy(MatchedPaths.ToArray(), 0, NewPaths, ops.DataFiles.Length, MatchedFiles.Count);
				Array.Copy(ops.DataSources, NewSources, ops.DataSources.Length);
				for(int i=ops.DataSources.Length;i<NewSources.Length;i++)
					NewSources[i] = addedname;
				ops.DataFiles=NewFiles;
				ops.DataFilePaths=NewPaths;
				ops.DataSources = NewSources;
			}
			//Clean up
			UpdateListView();
		}
		public void NotifyImage()
		{
			byte[] buffer=File.ReadAllBytes(@"obmm\temp.png");
			MemoryStream ms=new MemoryStream(buffer);
			SetImage(Image.FromStream(ms), @"obmm\temp.png");
		}
		void LoadXBT(ConfigList config)
		{
			string omodName = config["Name"];
			string omodVersion = config["Version"];
			string omodAuthor = config["Author"];
			string omodEmail = config["Email"];
			string omodWebsite = config["Website"];
			string omodDescription = config["Description"];
			string omodImage = null;
			string omodScript = config["Script"];
			
			if (omodName != null)
				tbName.Text = omodName;
			
			if (omodVersion != null)
				tbVersion.Text = omodVersion;
			
			if (omodAuthor != null)
				tbAuthor.Text = omodAuthor;
			
			if (omodEmail != null)
				tbEmail.Text = omodEmail;
			
			if (omodWebsite != null)
			{
				if (omodWebsite.StartsWith("PES://", StringComparison.CurrentCultureIgnoreCase))
					omodWebsite = "http://planetelderscrolls.gamespy.com/View.php?view=OblivionMods.Detail&id=" + omodWebsite.Substring(6);
				else if (omodWebsite.StartsWith("TESNEXUS://", StringComparison.CurrentCultureIgnoreCase))
					omodWebsite = "http://www.nexusmods.com/oblivion/mods/" + omodWebsite.Substring(11);
                else if (omodWebsite.StartsWith("SKYRIMNEXUS://", StringComparison.CurrentCultureIgnoreCase))
                    omodWebsite = "http://www.nexusmods.com/skyrim/mods/" + omodWebsite.Substring(11);
                tbWebsite.Text = omodWebsite;
			}
			
			if (omodDescription != null)
				ops.Description = omodDescription;
			
			ConfigPair imgp;
			
			imgp = config.GetPair(new SV("Image"));
			
			if (imgp != null)
			{
				if (imgp.DataIsArray)
				{
					object[] dar;
					dar = imgp.DataAsArray;
					Random r = new Random();
					omodImage = dar[r.Next() % dar.Length].ToString();
				}
				else if (imgp.DataIsString)
				{
					omodImage = imgp.DataAsString;
				}
			}
			
			if (omodImage != null)
			{
				try
				{
					new ImageDownload(omodImage, this).Show();
				}
				catch(Exception)
				{
					
				}
			}
			
			if (omodScript != null)
			{
				ops.script = omodScript;
			}
		}
		public static int ImageTimeout = 30000;
		void SetImage(Image img, string fn)
		{
			try
			{
				ScreenshotPic.Image = img;
				ops.Image = fn;
				
			} catch {
				MessageBox.Show("The image file appears to be corrupt", "Error");
			}
		}
		bool OCDCheck(string name)
		{
			/*if (ocdList == null)
			{
				if (File.Exists(@"obmm\ocd.xbt"))
				{
					ocdList = new GeneralConfig().LoadConfiguration(@"obmm\ocd.xbt");
				}
				else
					ocdList = new ConfigList();
			}*/
			if (Directory.Exists(@"obmm\ocdlist"))
			{
				List<FileInfo> files = new List<FileInfo>();
				DirectoryInfo ocdlist = new DirectoryInfo(@"obmm\ocdlist");
				
				
				files.AddRange(ocdlist.GetFiles("*.xbt", SearchOption.AllDirectories));
				files.AddRange(ocdlist.GetFiles("*.json", SearchOption.AllDirectories));
				files.AddRange(ocdlist.GetFiles("*.ini", SearchOption.AllDirectories));
				files.AddRange(ocdlist.GetFiles("*.cidb", SearchOption.AllDirectories));
				
				if (OCDCheck(files.ToArray(), name))
					return true;
			}
			
			return false;
		}
		
		class ModNameValidator : BaseTools.Searching.IValidator<string>
		{
			string modname;
			
			public ModNameValidator(string modname)
			{
				this.modname = modname.Replace(' ', '_');
			}
			public bool ItemValid(string s)
			{
				s = s.Replace(' ', '_');
				
				
				
				if (s.Length > 0 && s[0] == '*')
				{
					int mni;
					if (s.Length > 2 && s[1] == '-' && (mni = modname.IndexOf('-')) != -1)
						return string.Compare(s.Substring(1), modname.Substring(mni+1).Split('-')[0], true) == 0;
					else
						return modname.EndsWith(s.Substring(1), StringComparison.CurrentCultureIgnoreCase);
				}
				else
					return string.Compare(modname, s, true) == 0;
			}
		}
		
		bool OCDCheck(FileInfo[] files, string name)
		{
			foreach(FileInfo fi in files)
			{
				if (!fi.Exists)
					continue;
				
				IConfig loader;
				
				if (fi.Name.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase))
					loader = new JSONConfig();
				else if (fi.Name.EndsWith(".ini", StringComparison.CurrentCultureIgnoreCase))
					loader = new IniConfig();
				else if (fi.Name.EndsWith(".cidb", StringComparison.CurrentCultureIgnoreCase))
					loader = new BuilderConfig();
				else
					loader = new GeneralConfig();
				
				ConfigList ocdList = loader.LoadConfiguration(fi.FullName);
				
				ConfigList config;
				
				if ((config = ocdList.GetSection(new ModNameValidator(name))) != null)
				{
					if (GlobalSettings.AlwaysImportOCDList || MessageBox.Show("Import OCD list data?", "Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						if (Program.KeyPressed(16))
							GlobalSettings.AlwaysImportOCDList = true;
						
						LoadXBT(config);
						return true;
					}
					else
						return false;
				}
			}
			return false;
		}

		private void bAddFromFolder_Click(object sender, EventArgs e) {
			while(Settings.omodCreatorFolderBrowserDir.IndexOf('\\')!=-1) {
				if(Directory.Exists(Settings.omodCreatorFolderBrowserDir)) {
					FolderDialog.SelectedPath=Settings.omodCreatorFolderBrowserDir;
					break;
				}
				Settings.omodCreatorFolderBrowserDir=Settings.omodCreatorFolderBrowserDir.Substring(0, Settings.omodCreatorFolderBrowserDir.LastIndexOf('\\'));
			}
			if(FolderDialog.ShowDialog()!=DialogResult.OK) return;
			Settings.omodCreatorFolderBrowserDir=FolderDialog.SelectedPath;
			AddFilesFromFolder(FolderDialog.SelectedPath+"\\");
		}

		private void bAddZip_Click(object sender, EventArgs e) {
			OpenDialog.Multiselect=false;
			OpenDialog.Title="Select mod to convert to omod format";
			OpenDialog.Filter="Compressed Archives (*.zip,*.rar,*.7z)|*.zip;*.rar;*.7z|All Files|*.*";
			if(OpenDialog.ShowDialog()!=DialogResult.OK) return;
			string Dir=Program.CreateTempDirectory();

            SevenZip.SevenZipExtractor sevenZipExtract = new SevenZip.SevenZipExtractor(OpenDialog.FileName);
            sevenZipExtract.ExtractFiles(Dir, new List<string>(sevenZipExtract.ArchiveFileNames).ToArray());

            // make sure that the current dir did not change
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));

			if(Directory.GetFileSystemEntries(Dir).Length==1) {
				string[] folders=Directory.GetDirectories(Dir);
				if(folders.Length==1) {
					string str;
					int index=folders[0].LastIndexOf('\\');
					if(index!=-1) str=folders[0].Substring(index+1); else str=folders[0];
					switch(str.ToLower()+"\\") {
						case "lsdata\\":
						case "meshes\\":
						case "music\\":
						case "textures\\":
						case "shaders\\":
						case "video\\":
						case "sound\\":
						case Program.omodConversionData:
							break;
						default:
							Dir=folders[0]+"\\";
							break;
					}

				}
			}
            if (Directory.Exists(Path.Combine(Dir, Program.DataFolderName)))
            {
				foreach(string s in Directory.GetFiles(Dir)) {
                    try { File.Move(s, Path.Combine(Dir, Path.Combine(Program.DataFolderName, Path.GetFileName(s)))); }
                    catch { }
				}
				Dir=Path.Combine(Dir,Program.DataFolderName);
			}
			AddFilesFromFolder(Dir, new FileInfo(OpenDialog.FileName).Name,false);
		}
        bool RequestImport()
        {
            return RequestImport("OMod");
        }

        bool RequestImport(string type)
		{
			bool reqi = GlobalSettings.AlwaysImportOCD || MessageBox.Show(type+" conversion data is available.\n"+
			                                                              "Would you like to import it?", "Question", MessageBoxButtons.YesNo)==DialogResult.Yes;
			
			if (Program.KeyPressed(16))
				return GlobalSettings.AlwaysImportOCD = reqi;
			else
				return reqi;
		}
		private void openToolStripMenuItem_Click(object sender, EventArgs e) {
			try {
				System.Diagnostics.Process.Start(lvFiles.SelectedItems[0].SubItems[1].Text);
			} catch(Exception ex) {
				MessageBox.Show("An error occured attempting to open the file.\n"+
				                ex.Message, "Error");
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
			foreach(ListViewItem lvi in lvFiles.SelectedItems) {
				lvFiles.Items.Remove(lvi);
			}
		}

		private void scanForDataFilesToolStripMenuItem_Click(object sender, EventArgs e) {
			if(!EditingPlugins) return;
			System.Collections.Generic.List<string> toadd=new System.Collections.Generic.List<string>();
			foreach(ListViewItem lvi in lvFiles.SelectedItems) {
				try {
					toadd.AddRange(ConflictDetector.TesFile.GetDataFileList(lvi.SubItems[1].Text));
				} catch(Exception ex) {
					MessageBox.Show("An error occurred trying to parse plugin "+lvi.SubItems[1].Text+"\n"+
					                "Error: "+ex.Message, "Error");
				}
			}
			//Remove missing files
			for(int i=0;i<toadd.Count;i++) {
				if(!File.Exists(Path.GetFullPath(Path.Combine(Program.DataFolderName,toadd[i])))) {
					toadd.RemoveAt(i--);
				}
			}
			//Remove duplicates
			int index;
			for(int i=0;i<toadd.Count;i++) {
				while((index=toadd.LastIndexOf(toadd[i]))>i) toadd.RemoveAt(index);
			}
			int lower=ops.DataFilePaths.Length;
			Array.Resize<string>(ref ops.DataFilePaths, lower+toadd.Count);
			Array.Resize<string>(ref ops.DataFiles, lower+toadd.Count);
			for(int i=0;i<toadd.Count;i++) {
				ops.DataFilePaths[lower+i]=toadd[i];
				ops.DataFiles[lower+i]=Path.GetFullPath(Path.Combine(Program.DataFolderName,toadd[i]));
			}
		}

        private string getNumericOnly(string input)
        {
            string output = "";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                if ("0123456789".IndexOf(c) != -1)
                    sb.Append(c);
            }
            output = sb.ToString();

            return output;
        }

		private void tbVersion_Leave(object sender, EventArgs e) {
			try {
                ops.Version = tbVersion.Text;
                string[] versions = tbVersion.Text.Split('.');
				if(versions.Length==0) throw new obmmException(""); // ||versions.Length>3) throw new obmmException("");
                if (versions[0] == "") ops.MajorVersion = 0; else try { ops.MajorVersion = Convert.ToInt32(getNumericOnly(versions[0])); }
                    catch { };
				if(versions.Length>1) {
                    if (versions[1] == "") ops.MinorVersion = 0; else try { ops.MinorVersion = Convert.ToInt32(getNumericOnly(versions[1])); }
                        catch { };
				} else ops.MinorVersion=-1;
				if(versions.Length>2) {
                    if (versions[2] == "") ops.BuildVersion = 0; else try { ops.BuildVersion = Convert.ToInt32(getNumericOnly(versions[2])); }
                        catch { };
				} else ops.BuildVersion=-1;
			} catch {
				//tbVersion.Text="1";
			}
		}

		private void bUp_Click(object sender, EventArgs e) {
			if(lvFiles.SelectedItems.Count==0) return;
			if(lvFiles.SelectedItems.Count>1) {
				MessageBox.Show("Can only move 1 mod at a time", "Error");
				return;
			}
			if(lvFiles.SelectedIndices[0]==0) return;
			ListViewItem lvi=lvFiles.SelectedItems[0];
			int index=lvFiles.SelectedIndices[0];
			lvFiles.Items.RemoveAt(index);
			lvFiles.Items.Insert(index-1, lvi);
		}

		private void bDown_Click(object sender, EventArgs e) {
			if(lvFiles.SelectedItems.Count==0) return;
			if(lvFiles.SelectedItems.Count>1) {
				MessageBox.Show("Can only move 1 mod at a time", "Error");
				return;
			}
			if(lvFiles.SelectedIndices[0]==lvFiles.Items.Count-1) return;
			ListViewItem lvi=lvFiles.SelectedItems[0];
			int index=lvFiles.SelectedIndices[0];
			lvFiles.Items.RemoveAt(index);
			lvFiles.Items.Insert(index+1, lvi);
		}

		private void validateToolStripMenuItem_Click(object sender, EventArgs e) {
			ProgressForm pb=null;
            if (lvFiles.Items.Count > 1000)
            {
                pb = new ProgressForm("Validating", false);
                pb.SetProgressRange(lvFiles.Items.Count);
                pb.Show();
                pb.Activate();
                Enabled = false;
            }
            int InvalidCount = 0;
            int DuplicateCount = 0;
            try
            {
                System.Collections.Generic.List<string> files = new List<string>(lvFiles.Items.Count);
                for (int i = 0; i < lvFiles.Items.Count; i++)
                {
                    if (!File.Exists(lvFiles.Items[i].SubItems[1].Text))
                    {
                        InvalidCount++;
                        lvFiles.Items.RemoveAt(i--);
                    }
                    else
                    {
                        files.Add(lvFiles.Items[i].Text.ToLower());
                    }
                }
                for (int i = 0; i < files.Count; i++)
                {
                    if (pb != null && i % 100 == 0)
                    {
                        pb.UpdateProgress(i);
                        pb.Refresh();
                    }
                    for (int j = i + 1; j < files.Count; j++)
                    {
                        if (files[i] == files[j])
                        {
                            DuplicateCount++;
                            switch (MessageBox.Show("Two files with duplicate paths were found\n" +
                                                   "old: " + lvFiles.Items[i].SubItems[1].Text + "\n" +
                                                   "new: " + lvFiles.Items[j].SubItems[1].Text + "\n\n" +
                                                   "Do you wish to remove the older file?\n" +
                                                   "Click no to remove the newer one\n" +
                                                   "Click cancel to ignore this pair of files (omods created with duplicate files are will be non-functional)", "Duplicate file found",
                                                   MessageBoxButtons.YesNoCancel))
                            {
                                case DialogResult.Yes:
                                    lvFiles.Items.RemoveAt(i);
                                    files.RemoveAt(i);
                                    j = i + 1;
                                    break;
                                case DialogResult.No:
                                    files.RemoveAt(j);
                                    lvFiles.Items.RemoveAt(j--);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            }
            finally
            {
                if (pb != null)
                {
                    pb.Unblock();
                    pb.Close();
                    Enabled = true;
                    Focus();
                }
            }
			if(sender!=null) {
				MessageBox.Show("Invalid file references removed: "+InvalidCount.ToString()+"\n"+
				                "Duplicate entries removed: "+DuplicateCount.ToString(), "Message");
			}
		}

		private void FilesContextMenu_Opening(object sender, CancelEventArgs e) {
			if(lvFiles.SelectedItems.Count==0) {
				deleteToolStripMenuItem.Enabled=false;
			} else {
				deleteToolStripMenuItem.Enabled=true;
			}
			if(lvFiles.SelectedItems.Count==1) {
				openToolStripMenuItem.Enabled=true;
			} else {
				openToolStripMenuItem.Enabled=false;
			}
			if(EditingPlugins) {
				if(lvFiles.SelectedItems.Count==0) {
					importModDetailsToolStripMenuItem.Enabled=false;
					scanForDataFilesToolStripMenuItem.Enabled=false;
					viewRequiredDataFilesToolStripMenuItem.Enabled=false;
				} else {
					scanForDataFilesToolStripMenuItem.Enabled=true;
					viewRequiredDataFilesToolStripMenuItem.Enabled=true;
					if(lvFiles.SelectedItems.Count==1) {
						importModDetailsToolStripMenuItem.Enabled=true;
					} else {
						importModDetailsToolStripMenuItem.Enabled=false;
					}
				}
			} else {
				importModDetailsToolStripMenuItem.Enabled=false;
				scanForDataFilesToolStripMenuItem.Enabled=false;
				viewRequiredDataFilesToolStripMenuItem.Enabled=false;
			}
		}

		private void bScreenshot_Click(object sender, EventArgs e) {
			OpenDialog.Title="Choose a screenshot to add";
			OpenDialog.Filter="Image files (bmp, jpg, gif, tif, png)|*.bmp;*.jpg;*.jpeg;*.gif;*.tif;*.tiff;*.png|All files|*.*";
			OpenDialog.Multiselect=false;
			if(OpenDialog.ShowDialog()==DialogResult.OK) {
				try {
					byte[] buffer=File.ReadAllBytes(OpenDialog.FileName);
					MemoryStream ms=new MemoryStream(buffer);
					Image i=Image.FromStream(ms);
					if(Program.IsImageAnimated(i)) {
						MessageBox.Show("Animated or multi-resolution images are not supported", "Error");
					} else {
						ScreenshotPic.Image=i;
						ops.Image=OpenDialog.FileName;
					}
				} catch {
					MessageBox.Show("The image file appears to be corrupt", "Error");
				}
			}
		}

		private void bRemoveScreenshot_Click(object sender, EventArgs e) {
			ScreenshotPic.Image=null;
			ops.Image=null;
		}

		private void cmbCompLevel1_SelectedIndexChanged(object sender, EventArgs e) {
            //if (cmbCompType.SelectedIndex == 0 && cmbDataCompLevel.SelectedIndex == 0 && Settings.WarnOnModDelete)
            //{
            //    MessageBox.Show("Using very high compression in 7-zip mode requires 1-2 GB of ram depending on the size of the mod you are trying to compress.\n" +
            //                    "If omod creation fails with an out of memory error, please switch to a lower level of compression.", "Warning");
            //}
		}

		private void bScreenshot_MouseEnter(object sender, EventArgs e) {
			if(ScreenshotPic.Image!=null) {
				ScreenshotPic.Visible=true;
				lvFiles.Visible=false;
			}
		}

		private void bScreenshot_MouseLeave(object sender, EventArgs e) {
			ScreenshotPic.Visible=false;
			lvFiles.Visible=true;
		}

		private void importModDetailsToolStripMenuItem_Click(object sender, EventArgs e) {
			if(!EditingPlugins) return;
			if(lvFiles.SelectedItems.Count!=1) {
				MessageBox.Show("You must select a single esp to import information from", "Error");
				return;
			}
			ConflictDetector.HeaderInfo hi=ConflictDetector.TesFile.GetHeader(lvFiles.SelectedItems[0].SubItems[1].Text);
			if(hi.Author==null||hi.Author=="DEFAULT") {
				if(hi.Description==null) {
					MessageBox.Show("This esp does not contain author or description information", "Warning");
				} else {
					MessageBox.Show("This esp does not contain author information", "Warning");
				}
			} else {
				if(hi.Description==null) {
					MessageBox.Show("This esp does not contain description information", "Warning");
				}
			}
			if(hi.Author!="DEFAULT"&&hi.Author!=null) tbAuthor.Text=hi.Author;
			if(hi.Description!=null) ops.Description=hi.Description;
			if(tbName.Text==InitialName) {
				tbName.Text=Path.GetFileNameWithoutExtension(lvFiles.SelectedItems[0].SubItems[1].Text);
			}
		}

		private void bGroups_Click(object sender, EventArgs e) { cmsGroups.Show(bGroups, 0, 0); }

		private void viewRequiredDataFilesToolStripMenuItem_Click(object sender, EventArgs e) {
			if(!EditingPlugins) return;
			System.Collections.Generic.List<string> toadd=new System.Collections.Generic.List<string>();
			foreach(ListViewItem lvi in lvFiles.SelectedItems) {
				try {
					toadd.AddRange(ConflictDetector.TesFile.GetDataFileList(lvi.SubItems[1].Text));
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
			string text="Warning: If this mod uses nif's which are packed into a BSA archive, obmm won't be able to extract the list of required texture from them."+
				Environment.NewLine+Environment.NewLine+string.Join(Environment.NewLine, toadd.ToArray());
			(new TextEditor("Required data files", text, false, false)).ShowDialog();
		}

		private void lvFiles_AfterLabelEdit(object sender, LabelEditEventArgs e) {
			if(e==null||e.Label==null) return;
			if(e.Label.Contains("/")) {
				e.CancelEdit=true;
				lvFiles.Items[e.Item].Text=e.Label.Replace('/', '\\');
			}
		}

		private void lvFiles_KeyDown(object sender, KeyEventArgs e) {
			if(e.KeyCode==Keys.Delete||e.KeyCode==Keys.Back) {
				e.Handled=true;
				deleteToolStripMenuItem_Click(null, null);
			}
			if(e.Control&&e.KeyCode==Keys.A) {
				e.Handled=true;
				SuspendLayout();
				lvFiles.SelectedIndices.Clear();
				for(int i=0;i<lvFiles.Items.Count;i++) {
					lvFiles.SelectedIndices.Add(i);
				}
				ResumeLayout();
			}
		}

        public void ApplyTESNexus(string modfile, bool bSilent)
        {
            if (alreadyTES)
                return;

            string imagefile="";
            Program.GetNexusModInfo(modfile, ref ops.Name, ref ops.Version, ref ops.Description, ref ops.Author, ref ops.website, ref imagefile, bSilent);

            if (imagefile != null && imagefile.Length > 0)
            {
                byte[] buffer = File.ReadAllBytes(imagefile);
                MemoryStream ms = new MemoryStream(buffer);
                SetImage(Image.FromStream(ms), imagefile);
            }
            alreadyTES = true;

            if (ops.Name != null)
                tbName.Text = ops.Name.Trim();

            if (ops.Version != null)
                tbVersion.Text = ops.Version;

            if (ops.Author != null)
                tbAuthor.Text = ops.Author;

            tbWebsite.Text = ops.website;

        }
		
		void CbIncludeVersionCheckedChanged(object sender, EventArgs e)
		{
			GlobalSettings.IncludeVersionNumber = cbIncludeVersion.Checked;
		}
		
		void BtnOCDListClick(object sender, EventArgs e)
		{
			string s = InputBox.Show("Filename", "Filename:", GlobalSettings.LastTNID);
			
			if (s != null)
			{
				if (OCDCheck(s))
					GlobalSettings.LastTNID = s;
			}
		}
		
		void BtnTESNexusClick(object sender, EventArgs e)
		{
            string id=GlobalSettings.LastTNID;
            if (tbWebsite.Text.Length > 0 && (tbWebsite.Text.Contains("?id=") || tbWebsite.Text.Contains("nexusmods.com/")))
            {
                id = tbWebsite.Text;
                if (tbWebsite.Text.Contains("?id="))
                {
                    id = id.Substring(id.LastIndexOf("?id="));
                    id = id.Replace("?id=", "");
                }
                else
                {
                    id = id.Substring(id.LastIndexOf("/mods/"));
                    id = id.Replace("/mods/", "");
                }
            }
			id = InputBox.Show("Mod ID", "Mod ID:", id);
			
			if (id != null)
			{
                alreadyTES = false; // force refresh
				GlobalSettings.LastTNID = id;
				ApplyTESNexus(id, false);
			}
		}
		
		void BEdDescriptionMouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (MessageBox.Show("Copy readme to description?", "Copy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					ops.Description = ops.readme;
			}
		}
		void BEdReadmeMouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (MessageBox.Show("Copy description to readme?", "Copy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					ops.readme = ops.Description;
			}
		}

        private void cbOmod2_CheckedChanged(object sender, EventArgs e)
        {
            ops.bOmod2 = cbOmod2.Checked;
            if (ops.bOmod2)
            {
                // only zip is supported for now
                //cmbCompType.SelectedItem = "zip";

                //if (Settings.bSaveOmod2AsZip)
                //    cmbCompType.Text = "zip";
                cmbDataCompLevel.Enabled = false;
            }
            else
            {
                cmbDataCompLevel.Enabled = true;
                rdDataMod.Checked = true;
            }
        }

        private void cmbModCompLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ops.bOmod2)
                cmbDataCompLevel.Text = cmbModCompLevel.Text;
        }

        private void rdSystemMod_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSystemMod.Checked)
                ops.bOmod2 = cbOmod2.Checked = true;
        }
		
	}
}