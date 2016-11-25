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
using ICSharpCode.TextEditor;

namespace OblivionModManager.Forms {
	public partial class ScriptEditor : Form {
		private string result;
		private ScriptType scriptType;
		private bool Edited=false;
		private FindForm ff=null;
		private readonly string[] pluginFiles;
		private readonly string[] dataFiles;
		private readonly bool CanSimulate;

		public string Result { get { return ""+((char)(byte)scriptType)+result; } }

		public ScriptEditor(string script) {
			InitializeComponent();
			//Forms designer keeps reseting these properties, so leave them here
			this.teEdit.ShowEOLMarkers = false;
			this.teEdit.ShowSpaces = false;
			this.teEdit.ShowTabs = false;
			this.teEdit.ShowVRuler = false;

			if(script!=null&&script.Length>0&&(byte)script[0]<(byte)ScriptType.Count) {
				scriptType=(ScriptType)script[0];
				script=script.Substring(1);
			} else scriptType=ScriptType.obmmScript;
			SetScriptType();
			teEdit.Text=script;
			teEdit.Document.DocumentChanged+=new ICSharpCode.TextEditor.Document.DocumentEventHandler(Document_DocumentChanged);
			if(Settings.ScriptEdMaximized) {
				this.WindowState=FormWindowState.Maximized;
			} else {
				this.Size=Settings.ScriptEdSize;
			}
		}

		public ScriptEditor(string script, string[] pluginFiles, DataFileInfo[] dataFiles) : this(script) {
			this.pluginFiles=pluginFiles;
			string[] df=new string[dataFiles.Length];
			for(int i=0;i<df.Length;i++) df[i]=dataFiles[i].FileName;
			this.dataFiles=df;
			CanSimulate=true;
			SetScriptType();
		}

		private void SetScriptType() {
			switch(scriptType) {
				case ScriptType.obmmScript:
					teEdit.SetHighlighting("obmmScript");
					obmmScriptToolStripMenuItem.Checked=true;
					ironpythonToolStripMenuItem.Checked=false;
					cToolStripMenuItem.Checked=false;
					vBToolStripMenuItem.Checked=false;
                    xmlToolStripMenuItem.Checked = false;
					bCheckForErrors.Text = "Check flow control";
					bSimulate.Enabled=false;
					break;
				case ScriptType.Python:
					teEdit.SetHighlighting("python");
					obmmScriptToolStripMenuItem.Checked=false;
					ironpythonToolStripMenuItem.Checked=true;
					cToolStripMenuItem.Checked=false;
					vBToolStripMenuItem.Checked=false;
                    xmlToolStripMenuItem.Checked = false;
					bCheckForErrors.Text = "Check Syntax";
					bSimulate.Enabled=CanSimulate;
					break;
				case ScriptType.cSharp:
					teEdit.SetHighlighting("cSharp");
					obmmScriptToolStripMenuItem.Checked=false;
					ironpythonToolStripMenuItem.Checked=false;
					cToolStripMenuItem.Checked=true;
					vBToolStripMenuItem.Checked=false;
                    xmlToolStripMenuItem.Checked = false;
					bCheckForErrors.Text = "Check Syntax";
					bSimulate.Enabled=CanSimulate;
					break;
				case ScriptType.vb:
					teEdit.SetHighlighting("vb");
					obmmScriptToolStripMenuItem.Checked=false;
					ironpythonToolStripMenuItem.Checked=false;
					cToolStripMenuItem.Checked=false;
					vBToolStripMenuItem.Checked=true;
                    xmlToolStripMenuItem.Checked = false;
					bCheckForErrors.Text = "Check Syntax";
					bSimulate.Enabled=CanSimulate;
					break;
                case ScriptType.xml:
                    teEdit.SetHighlighting("xml");
                    obmmScriptToolStripMenuItem.Checked = false;
                    ironpythonToolStripMenuItem.Checked = false;
                    cToolStripMenuItem.Checked = false;
                    vBToolStripMenuItem.Checked = false;
                    xmlToolStripMenuItem.Checked = true;
                    bCheckForErrors.Text = "Check Syntax";
                    bSimulate.Enabled = CanSimulate;
                    break;
            }
		}

		private void Document_DocumentChanged(object sender, ICSharpCode.TextEditor.Document.DocumentEventArgs e) {
			Edited=true;
			teEdit.Document.DocumentChanged-=Document_DocumentChanged;
		}

		private void bOpen_Click(object sender, EventArgs e) {
			if(OpenDialog.ShowDialog()==DialogResult.OK) {
				teEdit.Text=System.IO.File.ReadAllText(OpenDialog.FileName);
			}
		}

		private void bSave_Click(object sender, EventArgs e) {
			if(saveFileDialog1.ShowDialog()==DialogResult.OK) {
				try {
					System.IO.File.WriteAllText(saveFileDialog1.FileName, teEdit.Text);
				} catch(Exception ex) {
					MessageBox.Show("An error occurred while trying to save the file\n"+ex.Message, "Error");
				}
			}
		}

		private void ScriptEditor_FormClosing(object sender, FormClosingEventArgs e) {
			if (autosave && Edited)
			{
				DialogResult=DialogResult.Yes;
				result=teEdit.Text;
			}
			else if(Edited) {
				switch(MessageBox.Show("Save changes?", "question", MessageBoxButtons.YesNoCancel)) {
					case DialogResult.Yes:
						DialogResult=DialogResult.Yes;
						result=teEdit.Text;
						break;
					case DialogResult.No:
						DialogResult=DialogResult.No;
						break;
					case DialogResult.Cancel:
						e.Cancel=true;
						return;
				}
			}
			Settings.ScriptEdSize=Size;
			if(WindowState==FormWindowState.Maximized) Settings.ScriptEdMaximized=true;
			else Settings.ScriptEdMaximized=false;
			if(ff!=null) ff.Close();
		}

		private void bFind_Click(object sender, EventArgs e) {
			if(ff!=null) return;
			ff=new FindForm();
			ff.bFind.Click+=new EventHandler(ffbFind_Click);
			ff.FormClosed+=new FormClosedEventHandler(ff_FormClosed);
			ff.Show();
			bFindNext.Enabled=true;
		}

		public void ff_FormClosed(object sender, FormClosedEventArgs e) {
			ff=null;
			bFindNext.Enabled=false;
		}

		public void ffbFind_Click(object sender, EventArgs e) {
			int index, start;
			if(teEdit.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection.Count>0) {
				start=teEdit.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].Offset+teEdit.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].Length; ;
			} else {
				start=teEdit.ActiveTextAreaControl.TextArea.Caret.Offset; ;
			}
			if(start>teEdit.Text.Length) start=0;
			if((index=teEdit.Text.IndexOf(ff.tbFind.Text, start, StringComparison.CurrentCultureIgnoreCase))==-1) {
				if(start==0||(index=teEdit.Text.IndexOf(ff.tbFind.Text, 0, StringComparison.CurrentCultureIgnoreCase))==-1) {
					MessageBox.Show("Search string not found", "Message");
					return;
				}
			}
			System.Drawing.Point sp=teEdit.Document.OffsetToPosition(index);
			System.Drawing.Point ep=teEdit.Document.OffsetToPosition(index+ff.tbFind.Text.Length);
			teEdit.ActiveTextAreaControl.TextArea.Caret.Position=sp;
			teEdit.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(new ICSharpCode.TextEditor.Document.DefaultSelection(teEdit.Document, sp, ep));
			//teEdit.ActiveTextAreaControl.ScrollToCaret();
		}

		private void bFindNext_Click(object sender, EventArgs e) {
			if(ff==null) return;
			ffbFind_Click(null, null);
		}

		private void Help_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start("hh.exe", "obmm\\obmm.chm::/scripting.htm");
		}

		private void cutToolStripMenuItem_Click(object sender, EventArgs e) {
			teEdit.ActiveTextAreaControl.TextArea.ExecuteDialogKey(Keys.X|Keys.Control);
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
			teEdit.ActiveTextAreaControl.TextArea.ExecuteDialogKey(Keys.C|Keys.Control);
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {
			teEdit.ActiveTextAreaControl.TextArea.ExecuteDialogKey(Keys.V|Keys.Control);
		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) {
			teEdit.ActiveTextAreaControl.TextArea.ExecuteDialogKey(Keys.A|Keys.Control);
		}

		private void undoToolStripMenuItem_Click(object sender, EventArgs e) {

			teEdit.ActiveTextAreaControl.TextArea.ExecuteDialogKey(Keys.Z|Keys.Control);
		}

		private void redoToolStripMenuItem_Click(object sender, EventArgs e) {
			teEdit.ActiveTextAreaControl.TextArea.ExecuteDialogKey(Keys.Y|Keys.Control);
		}

		private void scriptContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
			undoToolStripMenuItem.Enabled=teEdit.Document.UndoStack.CanUndo;
			redoToolStripMenuItem.Enabled=teEdit.Document.UndoStack.CanRedo;
		}

		private struct ControlStruct {
			public byte type;
			public int line;

			public ControlStruct(byte type, int line) {
				this.type=type;
				this.line=line;
			}
		}
		private static string CheckLine(string s, out string func) {
			System.Collections.Generic.List<string> temp=new System.Collections.Generic.List<string>();
			bool WasLastSpace=false;
			bool InQuotes=false;
			bool WasLastEscape=false;
			bool DoubleBreak=false;
			bool InVar=false;
			string CurrentWord="";
			string CurrentVar="";
			func=null;

			s+=" ";
			for(int i=0;i<s.Length;i++) {
				switch(s[i]) {
					case '%':
						WasLastSpace=false;
						if(InVar) {
							CurrentWord=CurrentVar+"%"+CurrentWord+"%";
							CurrentVar="";
							InVar=false;
						} else {
							if(InQuotes&&WasLastEscape) {
								CurrentWord+="%";
							} else {
								InVar=true;
								CurrentVar=CurrentWord;
								CurrentWord="";
							}
						}
						WasLastEscape=false;
						break;
					case ',':
					case ' ':
						WasLastEscape=false;
						if(InVar) {
							CurrentWord=CurrentVar+"%"+CurrentWord;
							CurrentVar="";
							InVar=false;
						}
						if(InQuotes) {
							CurrentWord+=s[i];
						} else if(!WasLastSpace) {
							if(InVar) {
								temp.Add("");
								InVar=false;
							} else temp.Add(CurrentWord);
							CurrentWord="";
							WasLastSpace=true;
						}
						break;
					case ';':
						WasLastEscape=false;
						if(!InQuotes) {
							DoubleBreak=true;
						} else CurrentWord+=s[i];
						break;
					case '"':
						if(InQuotes&&WasLastEscape) {
							CurrentWord+=s[i];
						} else {
							if(InVar) return "String marker found in the middle of a variable name";
							if(InQuotes) {
								InQuotes=false;
							} else {
								InQuotes=true;
							}
						}
						WasLastSpace=false;
						WasLastEscape=false;
						break;
					case '\\':
						if(InQuotes&&WasLastEscape) {
							CurrentWord+=s[i];
							WasLastEscape=false;
						} else if(InQuotes) {
							WasLastEscape=true;
						} else {
							CurrentWord+=s[i];
						}
						WasLastSpace=false;
						break;
					default:
						WasLastEscape=false;
						WasLastSpace=false;
						CurrentWord+=s[i];
						break;
				}
				if(DoubleBreak) break;
			}
			if(InVar) return "Unterminated variable";
			if(InQuotes) return "Unterminated quote";
			if(temp.Count>0) func=temp[0];
			return null;
		}
		private void bCheckForErrors_Click(object sender, EventArgs e) {
			if(scriptType != ScriptType.obmmScript) {
				string err = "";
				string output = "";

				switch(scriptType) {
					case ScriptType.cSharp:
						err = OblivionModManager.Scripting.DotNetScriptHandler.CheckSyntaxCS(teEdit.Text, out output);
						break;
					case ScriptType.Python:
						err = OblivionModManager.Scripting.pythonScriptHandler.CheckSyntax(teEdit.Text);
						break;
					case ScriptType.vb:
						err = OblivionModManager.Scripting.DotNetScriptHandler.CheckSyntaxVB(teEdit.Text, out output);
						break;
                    case ScriptType.xml:
//                        err = OblivionModManager.Scripting.DotNetScriptHandler.CheckSyntaxXML(teEdit.Text, out output);
                        break;
                }
				tbErrors.Text=err;
				tbOutput.Text=output;
				return;
			}
			string[] lines=teEdit.Text.Replace("\r", "").Split(new char[] { '\n' });
			bool AllowRunOnLines=false;
			bool foundReturn=false;
			bool foundDefault=false;
			int forCount=0, selectCount=0;
			System.Collections.Generic.Stack<ControlStruct> controls=new System.Collections.Generic.Stack<ControlStruct>();
			System.Collections.Generic.List<string> errors=new System.Collections.Generic.List<string>();
			for(int i=1;i<=lines.Length;i++) {
				string line=lines[i-1].Replace('\t', ' ').Trim();
				if(AllowRunOnLines) {
					while(line.EndsWith("\\")) {
						line=line.Remove(line.Length-1);
						if(i++==lines.Length) {
							errors.Add("Run-on line passed end of script");
							continue;
						} else line+=lines[i-1].Replace('\t', ' ').Trim();
					}
				}
				string error=CheckLine(line, out line);
				if(error!=null) {
					errors.Add(error+" on line "+i);
					continue;
				}
				if(line==null||line=="") continue;
				if(foundReturn&&line!="EndFor"&&line!="EndIf"&&line!="EndSelect"&&line!="Case"&&line!="Default") {
					errors.Add("Unreachable code detected on line "+i);
					foundReturn=false;
				}
				switch(line) {
					case "AllowRunOnLines":
						if(controls.Count>0) {
							errors.Add("AllowRunOnLines should not be used inside a control structure (line "+i+")");
							continue;
						}
						AllowRunOnLines=true;
						break;
					case "If":
					case "IfNot":
						controls.Push(new ControlStruct(0, i));
						break;
					case "Else":
						if(controls.Count==0||controls.Peek().type!=0) {
							errors.Add("Unexpected Else on line "+i);
							continue;
						}
						break;
					case "EndIf":
						if(controls.Count==0||controls.Pop().type!=0) {
							errors.Add("Unexpected EndIf on line "+i);
							continue;
						}
						foundReturn=false;
						break;
					case "Select":
					case "SelectMany":
					case "SelectWithPreview":
					case "SelectManyWithPreview":
					case "SelectWithDescriptions":
					case "SelectManyWithDescriptions":
					case "SelectWithDescriptionsAndPreviews":
					case "SelectManyWithDescriptionsAndPreviews":
					case "SelectVar":
					case "SelectString":
						controls.Push(new ControlStruct(1, i));
						selectCount++;
						break;
					case "Case":
						if(controls.Count==0||controls.Peek().type!=1) {
							errors.Add("Unexpected Case on line "+i);
							continue;
						}
						foundReturn=false;
						foundDefault=false;
						break;
					case "Default":
						if(controls.Count==0||controls.Peek().type!=1) {
							errors.Add("Unexpected Default on line "+i);
							continue;
						}
						foundReturn=false;
						if(foundDefault) errors.Add("Select has multiple sequential Default statements (line "+i+")");
						else foundDefault=true;
						break;
					case "Break":
						if(selectCount==0) {
							errors.Add("Unexpected Break on line "+i);
							continue;
						}
						foundReturn=true;
						break;
					case "EndSelect":
						if(controls.Count==0||controls.Pop().type!=1) {
							errors.Add("Unexpected EndSelect on line "+i);
							continue;
						}
						selectCount--;
						foundReturn=false;
						foundDefault=false;
						break;
					case "For":
						controls.Push(new ControlStruct(2, i));
						forCount++;
						break;
					case "Continue":
					case "Exit":
						if(forCount==0) {
							errors.Add("Unexpected "+line+" on line "+i);
							continue;
						}
						foundReturn=true;
						break;
					case "EndFor":
						if(controls.Count==0||controls.Pop().type!=2) {
							errors.Add("Unexpected EndFor on line "+i);
							continue;
						}
						forCount--;
						foundReturn=false;
						break;
					case "Return":
					case "FatalError":
						foundReturn=true;
						break;
				}
			}
			if(controls.Count>0) errors.Add("Unclosed control structure starting at line "+controls.Pop().line);

			if(errors.Count>0) {
				string err="";
				for(int i=0;i<errors.Count;i++) err+=errors[i]+Environment.NewLine;
				tbErrors.Text=err;
			} else {
				tbErrors.Text="No malformed lines or control structures found";
			}
		}

		private void ScriptTypeChanged(object sender, EventArgs e) {
			if(sender==obmmScriptToolStripMenuItem) {
				scriptType=ScriptType.obmmScript;
			} else if(sender==ironpythonToolStripMenuItem) {
				scriptType=ScriptType.Python;
			} else if(sender==cToolStripMenuItem) {
				scriptType=ScriptType.cSharp;
			} else if(sender==vBToolStripMenuItem) {
				scriptType=ScriptType.vb;
			} else if(sender==xmlToolStripMenuItem) {
				scriptType=ScriptType.xml;
			}
			SetScriptType();
		}

		private void ScriptEditor_KeyDown(object sender, KeyEventArgs e) {
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
		public string FilesList
		{
			get
			{
				return txtFiles.Text;
			}
			set
			{
				txtFiles.Text = value;
			}
		}
		private void bSimulate_Click(object sender, EventArgs e) {
			ScriptReturnData srd;
			this.teEdit.Enabled=false;
			try {
				srd=Scripting.ScriptRunner.SimulateScript(""+(char)(byte)scriptType+teEdit.Text, dataFiles, pluginFiles);
			} catch(Exception ex) {
				MessageBox.Show("The script did not complete successfully\n"+ex.Message);
				return;
			} finally {
				this.teEdit.Enabled=true;
			}
			SimResults simForm=new SimResults(srd, pluginFiles, dataFiles);
			simForm.ShowDialog();
		}
		public string ScriptText
		{
			get
			{
				return teEdit.Text;
			}
			set
			{
				teEdit.Text = value;
			}
		}
		void TsbPrefabsClick(object sender, EventArgs e)
		{
			new PrefabForm(this).ShowDialog();
		}
		private bool autosave = false;
		void TsbSaveExitClick(object sender, EventArgs e)
		{
			autosave = true;
			this.Close();
		}
	}
}