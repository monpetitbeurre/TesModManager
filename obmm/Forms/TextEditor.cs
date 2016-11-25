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

namespace OblivionModManager {
	public class TextEditor : Form {
		#region FormDesignerGunk
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextEditor));
			this.rtbEdit = new System.Windows.Forms.RichTextBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenDialog = new System.Windows.Forms.OpenFileDialog();
			this.ToolBar = new System.Windows.Forms.ToolStrip();
			this.bOpen = new System.Windows.Forms.ToolStripButton();
			this.bSave = new System.Windows.Forms.ToolStripButton();
			this.bRTF = new System.Windows.Forms.ToolStripButton();
			this.bBold = new System.Windows.Forms.ToolStripButton();
			this.bItalic = new System.Windows.Forms.ToolStripButton();
			this.bUnderline = new System.Windows.Forms.ToolStripButton();
			this.cmbFontSize = new System.Windows.Forms.ToolStripComboBox();
			this.bFind = new System.Windows.Forms.ToolStripButton();
			this.bFindNext = new System.Windows.Forms.ToolStripButton();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.tsbSaveExit = new System.Windows.Forms.ToolStripButton();
			this.contextMenuStrip1.SuspendLayout();
			this.ToolBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// rtbEdit
			// 
			this.rtbEdit.AcceptsTab = true;
			this.rtbEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			                                                             | System.Windows.Forms.AnchorStyles.Left)
			                                                            | System.Windows.Forms.AnchorStyles.Right)));
			this.rtbEdit.BackColor = System.Drawing.Color.White;
			this.rtbEdit.ContextMenuStrip = this.contextMenuStrip1;
			this.rtbEdit.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtbEdit.HideSelection = false;
			this.rtbEdit.Location = new System.Drawing.Point(0, 28);
			this.rtbEdit.Name = "rtbEdit";
			this.rtbEdit.Size = new System.Drawing.Size(455, 256);
			this.rtbEdit.TabIndex = 0;
			this.rtbEdit.Text = "";
			this.rtbEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbEdit_KeyDown);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			                                      	this.cutToolStripMenuItem,
			                                      	this.copyToolStripMenuItem,
			                                      	this.pasteToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(103, 70);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			this.cutToolStripMenuItem.Text = "Cut";
			this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
			// 
			// OpenDialog
			// 
			this.OpenDialog.Filter = "text files (*.txt, *.rtf)|*.txt;*.rtf|All files|*.*";
			this.OpenDialog.RestoreDirectory = true;
			this.OpenDialog.Title = "Choose file to import";
			// 
			// ToolBar
			// 
			this.ToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			                            	this.tsbSaveExit,
			                            	this.bOpen,
			                            	this.bSave,
			                            	this.bRTF,
			                            	this.bBold,
			                            	this.bItalic,
			                            	this.bUnderline,
			                            	this.cmbFontSize,
			                            	this.bFind,
			                            	this.bFindNext});
			this.ToolBar.Location = new System.Drawing.Point(0, 0);
			this.ToolBar.Name = "ToolBar";
			this.ToolBar.Size = new System.Drawing.Size(455, 25);
			this.ToolBar.TabIndex = 1;
			// 
			// bOpen
			// 
			this.bOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bOpen.Image = ((System.Drawing.Image)(resources.GetObject("bOpen.Image")));
			this.bOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bOpen.Name = "bOpen";
			this.bOpen.Size = new System.Drawing.Size(23, 22);
			this.bOpen.Text = "Open file";
			this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
			// 
			// bSave
			// 
			this.bSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bSave.Image = ((System.Drawing.Image)(resources.GetObject("bSave.Image")));
			this.bSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bSave.Name = "bSave";
			this.bSave.Size = new System.Drawing.Size(23, 22);
			this.bSave.Text = "Save file";
			this.bSave.Click += new System.EventHandler(this.bSave_Click);
			// 
			// bRTF
			// 
			this.bRTF.CheckOnClick = true;
			this.bRTF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bRTF.Image = ((System.Drawing.Image)(resources.GetObject("bRTF.Image")));
			this.bRTF.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bRTF.Name = "bRTF";
			this.bRTF.Size = new System.Drawing.Size(23, 22);
			this.bRTF.Text = "Toggle RTF";
			this.bRTF.Click += new System.EventHandler(this.bRTF_CheckedChanged);
			// 
			// bBold
			// 
			this.bBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bBold.Enabled = false;
			this.bBold.Image = ((System.Drawing.Image)(resources.GetObject("bBold.Image")));
			this.bBold.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bBold.Name = "bBold";
			this.bBold.Size = new System.Drawing.Size(23, 22);
			this.bBold.Text = "Bold";
			this.bBold.Click += new System.EventHandler(this.bBold_Click);
			// 
			// bItalic
			// 
			this.bItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bItalic.Enabled = false;
			this.bItalic.Image = ((System.Drawing.Image)(resources.GetObject("bItalic.Image")));
			this.bItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bItalic.Name = "bItalic";
			this.bItalic.Size = new System.Drawing.Size(23, 22);
			this.bItalic.Text = "Italic";
			this.bItalic.Click += new System.EventHandler(this.bItalic_Click);
			// 
			// bUnderline
			// 
			this.bUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bUnderline.Enabled = false;
			this.bUnderline.Image = ((System.Drawing.Image)(resources.GetObject("bUnderline.Image")));
			this.bUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bUnderline.Name = "bUnderline";
			this.bUnderline.Size = new System.Drawing.Size(23, 22);
			this.bUnderline.Text = "Underline";
			this.bUnderline.Click += new System.EventHandler(this.bUnderline_Click);
			// 
			// cmbFontSize
			// 
			this.cmbFontSize.Enabled = false;
			this.cmbFontSize.Items.AddRange(new object[] {
			                                	"8",
			                                	"10",
			                                	"12",
			                                	"14",
			                                	"18",
			                                	"22",
			                                	"28",
			                                	"32",
			                                	"48",
			                                	"78"});
			this.cmbFontSize.Name = "cmbFontSize";
			this.cmbFontSize.Size = new System.Drawing.Size(121, 25);
			this.cmbFontSize.Text = "10";
			this.cmbFontSize.SelectedIndexChanged += new System.EventHandler(this.cmbFontSize_SelectedIndexChanged);
			this.cmbFontSize.Leave += new System.EventHandler(this.cmbFontSize_Leave);
			this.cmbFontSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbFontSize_KeyPress);
			// 
			// bFind
			// 
			this.bFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.bFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bFind.Name = "bFind";
			this.bFind.Size = new System.Drawing.Size(46, 22);
			this.bFind.Text = "Search";
			this.bFind.Click += new System.EventHandler(this.bFind_Click);
			// 
			// bFindNext
			// 
			this.bFindNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.bFindNext.Enabled = false;
			this.bFindNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bFindNext.Name = "bFindNext";
			this.bFindNext.Size = new System.Drawing.Size(59, 22);
			this.bFindNext.Text = "Find next";
			this.bFindNext.Click += new System.EventHandler(this.bFindNext_Click);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "Rich text file (*.rtf)|*.rtf|Plain text (*.txt)|*.txt";
			this.saveFileDialog1.RestoreDirectory = true;
			this.saveFileDialog1.Title = "Save file as";
			// 
			// tsbSaveExit
			// 
			this.tsbSaveExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSaveExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveExit.Image")));
			this.tsbSaveExit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSaveExit.Name = "tsbSaveExit";
			this.tsbSaveExit.Size = new System.Drawing.Size(23, 22);
			this.tsbSaveExit.Text = "Save and Exit";
			this.tsbSaveExit.Click += new System.EventHandler(this.TsbSaveExitClick);
			// 
			// TextEditor
			// 
			this.ClientSize = new System.Drawing.Size(455, 284);
			this.Controls.Add(this.ToolBar);
			this.Controls.Add(this.rtbEdit);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TextEditor";
			this.Text = "Text editor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextEditor_FormClosing);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ToolBar.ResumeLayout(false);
			this.ToolBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripButton tsbSaveExit;

		#endregion

		private System.Windows.Forms.RichTextBox rtbEdit;
		private ToolStrip ToolBar;
		private ToolStripButton bOpen;
		private ToolStripButton bRTF;
		private ToolStripButton bBold;
		private ToolStripButton bItalic;
		private ToolStripButton bUnderline;
		private ToolStripComboBox cmbFontSize;
		private ContextMenuStrip contextMenuStrip1;
		private ToolStripMenuItem cutToolStripMenuItem;
		private ToolStripMenuItem copyToolStripMenuItem;
		private ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog OpenDialog;
		private ToolStripButton bSave;
		private SaveFileDialog saveFileDialog1;
		private ToolStripButton bFind;
		private ToolStripButton bFindNext;
		#endregion

		public string Result;
		private bool AllowRTF;
		private bool Edited=false;
		private FindForm ff=null;

		public TextEditor(string Title,string InitialContents,bool allowRTF,bool AllowEdit) {
			Result=InitialContents;
			AllowRTF=allowRTF;
			InitializeComponent();
			if(Settings.TextEdSize!=System.Drawing.Size.Empty) {
				this.Size=Settings.TextEdSize;
				if(Settings.TextEdMaximized) this.WindowState=FormWindowState.Maximized;
			} else {
				this.WindowState=FormWindowState.Maximized;
			}
			
			Text=Title;

			bBold.Enabled=false;
			bItalic.Enabled=false;
			bUnderline.Enabled=false;
			cmbFontSize.Enabled=false;
			if(!AllowRTF) {
				bRTF.Enabled=false;
				rtbEdit.Text=InitialContents;
			} else {
				bRTF.Checked=false;
				if(InitialContents!="") {
					try {
						rtbEdit.Rtf=InitialContents;
						bRTF.Checked=true;
					} catch {
						//Probably means that a plain text file has been imported
						rtbEdit.Text=InitialContents;
					}
				}
			}

			if(!AllowEdit) {
				rtbEdit.ReadOnly=true;
				bRTF.Enabled=false;
				bOpen.Enabled=false;
			} else rtbEdit.TextChanged+=new EventHandler(rtbEdit_TextChanged);
			rtbEdit.Select(0, 0);
		}
		public TextEditor(string p) {
			InitializeComponent();
			if(Settings.TextEdSize!=System.Drawing.Size.Empty) {
				this.Size=Settings.TextEdSize;
				if(Settings.TextEdMaximized) this.WindowState=FormWindowState.Maximized;
			} else {
				this.WindowState=FormWindowState.Maximized;
			}
			Text="Conflict report";

			bBold.Enabled=false;
			bItalic.Enabled=false;
			bUnderline.Enabled=false;
			cmbFontSize.Enabled=false;
			bRTF.Enabled=false;
			bOpen.Enabled=false;

			rtbEdit.ReadOnly=false;
			rtbEdit.SuspendLayout();
			string[] ss=p.Split('\n');
			string current="";
			foreach(string s in ss) {
				switch(s) {
					case "black":
						rtbEdit.SelectedText=current;
						current="";
						rtbEdit.SelectionColor=Color.Black;
						break;
					case "blue":
						rtbEdit.SelectedText=current;
						current="";
						rtbEdit.SelectionColor=Color.Blue;
						break;
					case "green":
						rtbEdit.SelectedText=current;
						current="";
						rtbEdit.SelectionColor=Color.Green;
						break;
					case "orange":
						rtbEdit.SelectedText=current;
						current="";
						rtbEdit.SelectionColor=Color.Orange;
						break;
					case "red":
						rtbEdit.SelectedText=current;
						current="";
						rtbEdit.SelectionColor=Color.Red;
						break;
					default:
						current+=s+Environment.NewLine;
						break;
				}
			}
			if(current!="") rtbEdit.SelectedText=current;
			rtbEdit.ResumeLayout();
			rtbEdit.ReadOnly=true;
			rtbEdit.Select(0, 0);
		}

		private void cmbFontSize_KeyPress(object sender, KeyPressEventArgs e) {
			if(e.KeyChar!='\b'&&!char.IsDigit(e.KeyChar)) e.Handled=true;
		}

		private void bBold_Click(object sender, EventArgs e) {
			rtbEdit.SelectionFont=new Font(rtbEdit.SelectionFont, rtbEdit.SelectionFont.Style^FontStyle.Bold);
		}

		private void bItalic_Click(object sender, EventArgs e) {
			rtbEdit.SelectionFont=new Font(rtbEdit.SelectionFont, rtbEdit.SelectionFont.Style^FontStyle.Italic);
		}

		private void bUnderline_Click(object sender, EventArgs e) {
			rtbEdit.SelectionFont=new Font(rtbEdit.SelectionFont, rtbEdit.SelectionFont.Style^FontStyle.Underline);
		}

		private void cmbFontSize_Leave(object sender, EventArgs e) {
			try {
				float f=Convert.ToInt32(cmbFontSize.Text);
				rtbEdit.SelectionFont=new Font(rtbEdit.SelectionFont.FontFamily, f, rtbEdit.SelectionFont.Style);
			} catch {
				cmbFontSize.Text=rtbEdit.SelectionFont.SizeInPoints.ToString();
			}
		}

		private void cmbFontSize_SelectedIndexChanged(object sender, EventArgs e) {
			rtbEdit.Focus();
		}

		private void bOpen_Click(object sender, EventArgs e) {
			if(OpenDialog.ShowDialog()==DialogResult.OK) {
				if(AllowRTF) {
					try {
						rtbEdit.Rtf=Program.ReadAllText(OpenDialog.FileName);
						bRTF.Checked=true;
					} catch {
						rtbEdit.Text=Program.ReadAllText(OpenDialog.FileName);
					}
				} else {
					rtbEdit.Text=Program.ReadAllText(OpenDialog.FileName);
				}
			}
		}

		private void rtbEdit_TextChanged(object sender, EventArgs e) {
			Edited=true;
			rtbEdit.TextChanged-=new EventHandler(rtbEdit_TextChanged);
		}

		private void bRTF_CheckedChanged(object sender, EventArgs e) {
			if(bRTF.Checked) {
				bBold.Enabled=true;
				bItalic.Enabled=true;
				bUnderline.Enabled=true;
				cmbFontSize.Enabled=true;
			} else {
				if(MessageBox.Show("Warning: This will clear any formatting from the current document",
				                   "warning", MessageBoxButtons.OKCancel)==DialogResult.OK) {
					string s=rtbEdit.Text;
					rtbEdit.Clear();
					rtbEdit.Text=s;
					bBold.Enabled=false;
					bItalic.Enabled=false;
					bUnderline.Enabled=false;
					cmbFontSize.Enabled=false;
				}
			}
		}

		private void cutToolStripMenuItem_Click(object sender, EventArgs e) {
			rtbEdit.Cut();
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
			rtbEdit.Copy();
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {
			rtbEdit.Paste();
		}

		private void bSave_Click(object sender, EventArgs e) {
			if(saveFileDialog1.ShowDialog()!=DialogResult.OK) return;
			try {
				if(saveFileDialog1.FilterIndex==1) {
					System.IO.File.WriteAllText(saveFileDialog1.FileName, rtbEdit.Rtf);
				} else {
					System.IO.File.WriteAllText(saveFileDialog1.FileName, rtbEdit.Text, System.Text.Encoding.Default);
				}
			} catch(Exception ex) {
				MessageBox.Show("An error occured while trying to save the file\n"+ex.Message, "Error");
			}
		}

		private void bFind_Click(object sender, EventArgs e) {
			//rtbEdit.HideSelection=false;
			if(ff!=null) return;
			ff=new FindForm();
			ff.bFind.Click+=new EventHandler(ffbFind_Click);
			ff.FormClosed+=new FormClosedEventHandler(ff_FormClosed);
			ff.Show();
			bFindNext.Enabled=true;
		}

		public void ff_FormClosed(object sender, FormClosedEventArgs e) {
			//rtbEdit.HideSelection=true;
			ff=null;
			bFindNext.Enabled=false;
		}

		public void ffbFind_Click(object sender, EventArgs e) {
			//Focus();
			int start=rtbEdit.SelectionStart+rtbEdit.SelectionLength;
			if(start>rtbEdit.Text.Length) start=0;
			if(rtbEdit.Find(ff.tbFind.Text, start, RichTextBoxFinds.None)==-1) {
				if(start==0||rtbEdit.Find(ff.tbFind.Text, 0, RichTextBoxFinds.None)==-1) {
					MessageBox.Show("Search string not found", "Message");
				}
			}
		}

		private void bFindNext_Click(object sender, EventArgs e) {
			if(ff==null) return;
			ffbFind_Click(null, null);
		}
		bool autoyes = false;
		private void TextEditor_FormClosing(object sender, FormClosingEventArgs e) {
			if(Edited) {
				if (autoyes)
				{
					DialogResult=DialogResult.Yes;
					if(AllowRTF&&bRTF.Checked) {
						if(rtbEdit.Text=="") Result="";
						else Result=rtbEdit.Rtf;
					} else {
						Result=rtbEdit.Text;
					}
				}
				else
				{
					switch(MessageBox.Show("Save changes?", "question", MessageBoxButtons.YesNoCancel)) {
						case DialogResult.Yes:
							DialogResult=DialogResult.Yes;
							if(AllowRTF&&bRTF.Checked) {
								if(rtbEdit.Text=="") Result="";
								else Result=rtbEdit.Rtf;
							} else {
								Result=rtbEdit.Text;
							}
							break;
						case DialogResult.No:
							DialogResult=DialogResult.No;
							break;
						case DialogResult.Cancel:
							e.Cancel=true;
							return;
					}
				}
			}
			Settings.TextEdSize=Size;
			if(WindowState==FormWindowState.Maximized) Settings.TextEdMaximized=true;
			else Settings.TextEdMaximized=false;
			if(ff!=null) ff.Close();
		}

		private void rtbEdit_KeyDown(object sender, KeyEventArgs e) {
			if(e.Control) {
				e.Handled=true;
				switch(e.KeyCode) {
					case Keys.S:
						bSave_Click(null, null);
						break;
					case Keys.O:
						bOpen_Click(null, null);
						break;
					default:
						e.Handled=false;
						break;
				}
			}
		}
		
		void TsbSaveExitClick(object sender, EventArgs e)
		{
			autoyes = true;
			this.Close();
		}
	}
}
