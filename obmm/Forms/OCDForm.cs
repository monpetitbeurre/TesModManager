/*
Copyright (C) 13/06/2010  Matthew "Scent Tree" Perry
scent.tree@gmail.com

This library/program is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

This libary/program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using BaseTools.Configuration;
using BaseTools.Configuration.Parsers;
using BaseTools.Dialog;
using System.Collections.Generic;
using SV = BaseTools.Searching.StringValidator;
using System.IO;
using System.Net;
using System.Xml;

namespace OblivionModManager
{
	/// <summary>
	/// Description of OCDForm.
	/// </summary>
	public partial class OCDForm : Form
	{
		string selectedOCD = null;
		ConfigList ocdInfo = new ConfigList();
		string openFile = null;
		bool dirty = false;
		string ocdScript = "";
		ConfigList ocdRes = null;
		public OCDForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			CheckOCD();
		}
		
		void CheckOCD()
		{
			grpOCD.Enabled = (selectedOCD != null);
		}
		
		void DefsRename(object sender, EventArgs e)
		{
			if (lstDefinitions.SelectedIndex != -1)
			{
				string oname = lstDefinitions.Items[lstDefinitions.SelectedIndex].ToString();
				string name;
				
				if ((name = InputBox.Show("Rename", "New Name:", oname)) != null)
				{
					ConfigPair cp;
					
					if ((cp = ocdInfo.GetPair(oname)) != null)
						cp.Key = name;
					
					//MessageBox.Show(cp.Key + " " + lstDefinitions.SelectedIndex.ToString());
					
					lstDefinitions.Items[lstDefinitions.SelectedIndex] = name;
					selectedOCD = name;
					dirty = true;
				}
			}
		}
		
		void BtnMenuClick(object sender, EventArgs e)
		{
			cmsMenu.Show(btnMenu, 0, 0);
		}
		
		void DefRem(object sender, EventArgs e)
		{
			int si;
			if ((si = lstDefinitions.SelectedIndex) != -1)
			{
				
				string name = lstDefinitions.Items[lstDefinitions.SelectedIndex].ToString();
				lstDefinitions.Items.RemoveAt(lstDefinitions.SelectedIndex);
				
				ocdInfo.RemoveKey(name);
				selectedOCD = null;
				dirty = true;
				
				if (si < lstDefinitions.Items.Count)
					lstDefinitions.SelectedIndex = si;
			}
			CheckOCD();
		}
		void EditScript()
		{
			OblivionModManager.Forms.ScriptEditor se = new OblivionModManager.Forms.ScriptEditor(ocdScript);
			if (se.ShowDialog() == DialogResult.Yes)
				ocdScript = se.Result;
		}
		void LoadXBT(ConfigList config)
		{
			ClearFields();
			
			string omodName = config["Name"];
			string omodVersion = config["Version"];
			string omodAuthor = config["Author"];
			string omodEmail = config["Email"];
			string omodWebsite = config["Website"];
			string omodDescription = config["Description"];
			string omodScript = config["Script"];
			ConfigList omodRes = config.GetSection("Resources", false);
			
			if (omodName != null)
				txtName.Text = omodName;
			
			if (omodVersion != null)
				txtVersion.Text = omodVersion;
			
			if (omodAuthor != null)
				txtAuthor.Text = omodAuthor;
			
			if (omodEmail != null)
				txtEmail.Text = omodEmail;
			
			if (omodWebsite != null)
				txtWebsite.Text = omodWebsite.Replace("http://www.tesnexus.com/downloads/file.php?id=", "TESNexus://").Replace(
					"http://planetelderscrolls.gamespy.com/View.php?view=OblivionMods.Detail&id=", "PES://");
			
			if (omodDescription != null)
				txtDescription.Text = omodDescription.Replace("\r\n", "\n").Replace("\n", "\r\n");
			
			if (omodScript != null)
				ocdScript = omodScript;
			
			if (omodRes != null)
				ocdRes = omodRes;
			
			ConfigPair cp = config.GetPair(new SV("Image"));
			
			if (cp != null)
			{
				if (cp.DataIsString)
				{
					txtImage.Text = cp.DataAsString;
				}
				else if (cp.DataIsArray)
				{
					object[] objs = cp.DataAsArray;
					foreach(object o in objs)
					{
						if (txtImage.Text.Length > 0)
							txtImage.Text += "\r\n";
						
						txtImage.Text += o.ToString();
					}
				}
			}
		}
		void ClearFields()
		{
			txtName.Text = "";
			txtVersion.Text = "";
			txtAuthor.Text = "";
			txtEmail.Text = "";
			txtWebsite.Text = "";
			txtDescription.Text = "";
			txtImage.Text = "";
			ocdScript = "";
			ocdRes = null;
		}
		void ApplyOCD()
		{
			if (selectedOCD != null && lstDefinitions.SelectedIndex != -1)
			{
				dirty = true;
				
				ConfigPair ocdPair = ocdInfo.GetPair(new SV(selectedOCD));
				
				ConfigList ocd = new ConfigList();
				
				
				ocd.AddPair(StringPair("Name", txtName.Text));
				
				if (txtVersion.Text.Length > 0)
					ocd.AddPair(StringPair("Version", txtVersion.Text));
				
				ocd.AddPair(StringPair("Author", txtAuthor.Text));
				
				if (txtWebsite.Text.Length > 0)
					ocd.AddPair(StringPair("Website", txtWebsite.Text));
				
				if (txtImage.Text.Length > 0)
				{
					string[] lines = txtImage.Text.Replace("\r\n", "\n").Split('\n');
					List<string> images = new List<string>();
					
					for(int i=0;i<lines.Length;i++)
					{
						string line = lines[i].Trim();
						
						if (line.Length > 0)
							images.Add(line);
					}
					
					if (images.Count == 1)
					{
						ocd.AddPair(StringPair("Image", images[0]));
					}
					else if (images.Count >= 2)
					{
						object[] objs = new object[images.Count];
						
						for(int i=0;i<images.Count;i++)
						{
							objs[i] = images[i];
						}
						
						ocd.AddPair(new ConfigPair("Image", objs, false, true));
					}
				}
				
				ocd.AddPair(StringPair("Description", txtDescription.Text));
				
				if (ocdScript.Length > 0)
					ocd.AddPair(StringPair("Script", ocdScript));
				
				if (ocdRes != null && ocdRes.GetList().Count > 0)
					ocd.AddPair(new ConfigPair("Resources", ocdRes, false, true));
				
				ocdPair.Data = ocd;
			}
		}
		void BtnApplyClick(object sender, EventArgs e)
		{
			if (txtName.Text.Trim().Length == 0)
			{
				MessageBox.Show("A name is required", "Name Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (txtAuthor.Text.Trim().Length == 0)
			{
				MessageBox.Show("An author is required", "Author Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (txtDescription.Text.Trim().Length == 0)
			{
				MessageBox.Show("A description is required", "Description Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			
			ApplyOCD();
		}
		ConfigPair StringPair(string key, string data)
		{
			return new ConfigPair(key, data, false, true);
		}
		
		void LstDefinitionsSelectedIndexChanged(object sender, EventArgs e)
		{
			//this.Text = (k++).ToString() + ": " + lstDefinitions.SelectedIndex.ToString() + " " + lstDefinitions.Items[lstDefinitions.SelectedIndex].ToString();
			ApplyOCD();
			if (lstDefinitions.SelectedIndex != -1)
			{
				selectedOCD = lstDefinitions.Items[lstDefinitions.SelectedIndex].ToString();
				LoadXBT(ocdInfo.GetSection(selectedOCD));
			}
			else
			{
				selectedOCD = null;
				ClearFields();
			}
			CheckOCD();
		}
		
		void BtnDefAddClick(object sender, EventArgs e)
		{
			string name;
			
			name = InputBox.Show("New OCD", "OCD Name:");
			
			if (name != null)
			{
				if (ocdInfo.HasSection(new SV(name)))
				{
					MessageBox.Show("That name is already on the list", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					dirty = true;
					lstDefinitions.Items.Add(name);
					ConfigList cl = new ConfigList();
					ocdInfo.AddSection(name, cl);
					
					int ii;
					if ((ii = name.IndexOf('-')) != -1)
					{
						cl.AddPair(new ConfigPair("Website", "TESNexus://" + name.Substring(ii+1), false, true));
					}
				}
			}
			CheckOCD();
		}
		
		void NewToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (ClearDirty())
			{
				selectedOCD = null;
				openFile = null;
				lstDefinitions.Items.Clear();
				ocdInfo = new ConfigList();
				ClearFields();
				CheckOCD();
				UpdateTitle();
			}
		}
		
		void LoadToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (ClearDirty())
			{
				OpenFileDialog ofd = new OpenFileDialog();
				ofd.Filter = "OCD Files|*.xbt";
				ofd.RestoreDirectory = true;
				ofd.InitialDirectory = new System.IO.DirectoryInfo(Path.Combine(Program.BaseDir, "ocdlist")).FullName;
				
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					selectedOCD = null;
					ClearFields();
					CheckOCD();
					openFile = ofd.FileName;
					ocdInfo = new GeneralConfig().LoadConfiguration(ofd.FileName);
					UpdateTitle();
					RefreshList();
					//MessageBox.Show("Working Directory: " + System.IO.Directory.GetCurrentDirectory());
				}
			}
		}
		void RefreshList()
		{
			lstDefinitions.Items.Clear();
			foreach(ConfigPair cp in ocdInfo)
			{
				if (cp.DataIsList)
					lstDefinitions.Items.Add(cp.Key);
			}
		}
		
		void SaveToolStripMenuItemClick(object sender, EventArgs e)
		{
			ApplyOCD();
			Save();
		}
		
		void UpdateTitle()
		{
			Text = "OCD Creator - " + OpenFileName();
		}
		string OpenFileName()
		{
			return openFile == null ? "Untitled" : (new System.IO.FileInfo(openFile).Name);
		}
		bool ClearDirty()
		{
			if (dirty)
			{
				DialogResult dr;
				dr = MessageBox.Show("Save changes to " + OpenFileName() + "?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				
				if (dr == DialogResult.No)
				{
					dirty = false;
					return true;
				}
				else if (dr == DialogResult.Yes)
				{
					return Save();
				}
				else if (dr == DialogResult.Cancel)
				{
					return false;
				}
				return false;
			}
			else
			{
				return true;
			}
		}
		bool Save()
		{
			if (openFile == null)
			{
				return SaveAs();
			}
			else
			{
				dirty = false;
				XBT.SaveConfiguration(openFile, ocdInfo);
				return true;
			}
		}
		bool SaveAs()
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.RestoreDirectory = true;
			sfd.Filter = "OCD Files|*.xbt;*.json;*.cidb";
			sfd.InitialDirectory = new System.IO.DirectoryInfo(Path.Combine(Program.BaseDir, "ocdlist")).FullName;
			
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				openFile = sfd.FileName;
				UpdateTitle();
				dirty = false;
				XBT.SaveConfiguration(openFile, ocdInfo);
				return true;
			}
			else
			{
				return false;
			}
		}
		void SaveAsAction(object sender, EventArgs e)
		{
			ApplyOCD();
			SaveAs();
		}
		
		void OCDFormFormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = !ClearDirty();
		}
		
		void BtnScriptClick(object sender, EventArgs e)
		{
			EditScript();
		}
		
		TextBox AssosciatedTextbox(Label lbl)
		{
			if (lbl == lblName)
				return txtName;
			else if (lbl == lblVersion)
				return txtVersion;
			else if (lbl == lblAuthor)
				return txtAuthor;
			else if (lbl == lblDescription)
				return txtDescription;
			else if (lbl == lblEmail)
				return txtEmail;
			else if (lbl == lblImage)
				return txtImage;
			else if (lbl == lblWebsite)
				return txtWebsite;
			else
				return null;
		}
		
		
		void LstDefinitionsKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				DefRem(sender, default(EventArgs));
			}
			else if (e.KeyCode == Keys.Insert)
			{
				BtnDefAddClick(sender, default(EventArgs));
			}
			else if (e.KeyCode == Keys.C && e.Control)
			{
				CloneToolStripMenuItemClick(sender, default(EventArgs));
			}
		}
		
		void SelectKBS(object sender, EventArgs e)
		{
			AssosciatedTextbox((Label)sender).Focus();
		}
		
		void AddItemToolStripMenuItemClick(object sender, EventArgs e)
		{
			BtnDefAddClick(sender, e);
		}
		
		void ImportDataToolStripMenuItemClick(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.RestoreDirectory = true;
			ofd.Filter = "OCD Files|*.xbt;*.json;*.cidb";
			ofd.Multiselect = true;
			
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				foreach(string FileName in ofd.FileNames)
				{
					ConfigList impfile = XBT.LoadConfiguration(FileName);
					foreach(ConfigPair cp in impfile)
					{
						if (cp.DataIsList)
						{
							if (!ocdInfo.HasSection(new SV(cp.Key)))
								ocdInfo.AddPair(cp);
						}
					}
				}
				RefreshList();
			}
		}
		
		void SortItemsToolStripMenuItemClick(object sender, EventArgs e)
		{
			ConfigList newList = new ConfigList();
			
			List<string> alphalist = new List<string>();
			
			
			foreach(ConfigPair cp in ocdInfo)
			{
				if (cp.DataIsList)
					alphalist.Add(cp.Key);
			}
			
			alphalist.Sort();
			
			foreach(string key in alphalist)
			{
				newList.AddPair(ocdInfo.GetPair(new SV(key)));
			}
			
			ocdInfo = newList;
			
			RefreshList();
		}
		
		void ExportOCDToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (selectedOCD != null)
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Filter = "OCD Files|*.xbt;*.json;*.cidb";
				sfd.FileName = "ocd.xbt";
				
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					XBT.SaveConfiguration(sfd.FileName, ocdInfo.GetSection(selectedOCD));
				}
			}
		}
		
		void TxtWebsiteTextChanged(object sender, EventArgs e)
		{
			if (string.Compare(txtWebsite.Text, "tes", true) == 0)
			{
				txtWebsite.Text = "TESNexus://";
				txtWebsite.Select(txtWebsite.Text.Length, 0);
			}
			else if (string.Compare(txtWebsite.Text, "pes", true) == 0)
			{
				txtWebsite.Text = "PES://";
				txtWebsite.Select(txtWebsite.Text.Length, 0);
			}
		}
		
		void LblWebsiteDoubleClick(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(txtWebsite.Text.Replace("TESNexus://", "http://www.tesnexus.com/downloads/file.php?id=").Replace("PES://", "http://planetelderscrolls.gamespy.com/View.php?view=OblivionMods.Detail&id="));
			}
			catch(Exception)
			{
				
			}
			
		}
		
		void CloneToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (selectedOCD != null && selectedOCD.Length > 0)
			{
				string name;
				
				name = InputBox.Show("Clone OCD", "OCD Name:");
				
				if (name != null)
				{
					if (ocdInfo.HasSection(new SV(name)))
					{
						MessageBox.Show("That name is already on the list", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						dirty = true;
						lstDefinitions.Items.Add(name);
						ConfigList cl = (ConfigList)ocdInfo.GetSection(selectedOCD).Clone();
						ocdInfo.AddSection(name, cl);
						
						int ii;
						if ((ii = name.IndexOf('-')) != -1)
						{
							cl.AddPair(new ConfigPair("Website", "TESNexus://" + name.Substring(ii+1), false, true));
						}
					}
				}
				CheckOCD();
			}
		}
		TextReader DownloadFile(string url)
		{
			WebClient wc = new WebClient();
			MemoryStream ms = new MemoryStream(wc.DownloadData(url));
			TextReader tr = new StreamReader(ms);
			return tr;
		}
		void TESNexusToolStripMenuItemClick(object sender, EventArgs e)
		{
			string modid;
            modid = InputBox.Show("Mod ID", "Mod ID:");
			
			
			try
			{
                if (modid != null)
				{
					System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    for (int i = 0; i < modid.Length; i++)
					{
                        char c = modid[i];
						
						if ("0123456789".IndexOf(c) != -1)
							sb.Append(c);
					}
                    modid = sb.ToString();
				}

                if (modid != null && modid.Length > 0)
				{
					
					string modName = null;
					string modAuthor = null;
					string modDescription = null;
					string modVersion = null;
                    string modWebsite = null;
                    string modImage = "";

                    Program.GetNexusModInfo(modid, ref modName, ref modVersion, ref modDescription, ref modAuthor, ref modWebsite, ref modImage, false);

                    
                    
                    //TextReader tr;
                    //tr = DownloadFile("http://www.tesnexus.com/downloads/file.php?id=" + fileid);
                    //string line;
                    //List<object> modImages = new List<object>();
					
                    //while((line = tr.ReadLine()) != null)
                    //{
                    //    if (line.StartsWith("<h2>") && line.EndsWith("</h2>") && modName == null)
                    //    {
                    //        modName = line.Substring(4, line.Length - 9);
                    //    }
                    //    else if (line.EndsWith("<div class=\"info_bg\">") && modAuthor == null)
                    //    {
                    //        tr.ReadLine();
                    //        tr.ReadLine();
                    //        line = tr.ReadLine();
                    //        int pos;
                    //        pos = line.IndexOf('>');
							
                    //        modAuthor = line.Substring(pos+1, line.Length - pos - 1 - 6);
                    //    }
                    //    else if (line.EndsWith("Version</div>") && modVersion == null)
                    //    {
                    //        line = tr.ReadLine();
                    //        int pos;
                    //        pos = line.IndexOf('>');
							
                    //        modVersion = line.Substring(pos+1, line.Length - pos - 1 - 6);
                    //    }
                    //    /*else if (line.EndsWith("file_title=\"open image\" class=\"open\"></a>"))
                    //    {
                    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
							
                    //        bool ignore = true;
                    //        for(int i=0;i<line.Length;i++)
                    //        {
                    //            char c = line[i];
                    //            if (ignore)
                    //            {
                    //                if (c == '"')
                    //                {
                    //                    ignore = false;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (c == '"')
                    //                    break;
                    //                else
                    //                    sb.Append(c);
                    //            }
                    //        }
							
                    //        modImages.Add("http://www.tesnexus.com" + sb.ToString());
                    //    }*/
                    //}
                    //tr.Close();
					
                    //{
                    //    tr = DownloadFile("http://www.tesnexus.com/downloads/file/images.php?id=" + fileid);
                    //    while((line = tr.ReadLine()) != null)
                    //    {
                    //        if (line.EndsWith("</a></h2>"))
                    //        {
                    //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
								
                    //            bool ignore = true;
                    //            for(int i=0;i<line.Length;i++)
                    //            {
                    //                char c = line[i];
                    //                if (ignore)
                    //                {
                    //                    if (c == '"')
                    //                    {
                    //                        ignore = false;
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    if (c == '"')
                    //                        break;
                    //                    else
                    //                        sb.Append(c);
                    //                }
                    //            }
								
                    //            modImages.Add("http://www.tesnexus.com" + sb.ToString());
                    //        }
                    //    }
                    //    tr.Close();
                    //}
					
                    //{
                    //    bool sr = false;
                    //    tr = DownloadFile("http://www.tesnexus.com/downloads/file/description.php?id=" + fileid);
                    //    while((line = tr.ReadLine()) != null && modDescription == null)
                    //    {
                    //        line = line.Trim();
                    //        if (sr)
                    //        {
                    //            if (line.Length > 0)
                    //            {
                    //                line = line.Replace("<br />", "\r\n").Replace("&nbsp;", " ").Replace("&quot;", "\"")
                    //                    .Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    //                System.Text.StringBuilder sb = new System.Text.StringBuilder();
									
                    //                bool ignore = false;
                    //                for(int i=0;i<line.Length;i++)
                    //                {
                    //                    char c = line[i];
                    //                    if (c == '<')
                    //                        ignore = true;
                    //                    else if (c == '>')
                    //                        ignore = false;
                    //                    else if (!ignore)
                    //                        sb.Append(c);
                    //                }
									
                    //                modDescription = sb.ToString();
                    //            }
                    //        }
                    //        else if (line.EndsWith("</h3>"))
                    //        {
                    //            sr = true;
                    //        }
                    //    }
                    //    tr.Close();
                    //}
					
                    ///*{
                    //    tr = DownloadFile("http://www.tesnexus.com/downloads/file/files.php?id=" + fileid);
                    //    while((line = tr.ReadLine()) != null && modFileName == null)
                    //    {
                    //        if (line.EndsWith("</a></h2>"))
                    //        {
                    //            modFileName = line.Substring(0, line.Length - 9).Trim() + "-" + fileid;
                    //        }
                    //    }
                    //}*/
					
                    ////if (modFileName != null)
					{
						ConfigList lst = new ConfigList();
                        ocdInfo.AddSection("*-" + modid, lst);
                        lstDefinitions.Items.Add("*-" + modid);
						
						if (modName != null)
							lst.AddPair(StringPair("Name", modName));
						
						if (modVersion != null)
							lst.AddPair(StringPair("Version", modVersion));
						
						if (modAuthor != null)
							lst.AddPair(StringPair("Author", modAuthor));

                        lst.AddPair(StringPair("Website", "TESNexus://" + modid));
						
                        //if (modImages.Count > 0)
                        //{
                        //    if (modImages.Count == 1)
                        //        lst.AddPair(StringPair("Image", (string)modImages[0]));
                        //    else
                        //        lst.AddPair(new ConfigPair("Image", modImages.ToArray(), false, true));
                        //}
                        if (modImage!=null && modImage.Length > 0)
                            lst.AddPair(StringPair("Image", (string)modImage));

						
						if (modDescription != null)
						{
							lst.AddPair(StringPair("Description", modDescription));
						}
						dirty = true;
						
					}
				}
			}
			catch(Exception)
			{
				
			}
		}
		
		void BtnCloneClick(object sender, EventArgs e)
		{
			CloneToolStripMenuItemClick(sender, e);
		}
		
		void BtnTESClick(object sender, EventArgs e)
		{
			TESNexusToolStripMenuItemClick(sender, e);
		}
		
		void BtnResourcesClick(object sender, EventArgs e)
		{
			if (ocdRes == null)
				ocdRes = new ConfigList();
			GeneralConfig gc = new GeneralConfig();
			TextEditor te = new TextEditor("Edit readme", gc.WriteConfiguration(ocdRes), true, true);
			if (te.ShowDialog() == DialogResult.Yes)
				ocdRes = gc.ReadConfiguration(te.Result);
		}
	}
}
