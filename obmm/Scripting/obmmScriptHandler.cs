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
using MessageBox=System.Windows.Forms.MessageBox;
using mbButtons=System.Windows.Forms.MessageBoxButtons;
using DialogResult=System.Windows.Forms.DialogResult;
using File=System.IO.File;
using Directory=System.IO.Directory;
using Path=System.IO.Path;
using BaseTools.Configuration;
using BaseTools.Configuration.Parsers;
using System.IO;

using SV = BaseTools.Searching.StringValidator;

namespace OblivionModManager.Scripting {
	public static class obmmScriptHandler {
		private class FlowControlStruct {
			public readonly int line;
			public readonly byte type;
			public readonly string[] values;
			public readonly string var;
			public bool active;
			public bool hitCase=false;
			public int forCount=0;

			//Inactive
			public FlowControlStruct(byte type) {
				line=-1;
				this.type=type;
				values=null;
				var=null;
				active=false;
			}

			//If
			public FlowControlStruct(int line, bool active) {
				this.line=line;
				type=0;
				values=null;
				var=null;
				this.active=active;
			}

			//Select
			public FlowControlStruct(int line, string[] values) {
				this.line=line;
				type=1;
				this.values=values;
				var=null;
				active=false;
			}

			//For
			public FlowControlStruct(string[] values, string var, int line) {
				this.line=line;
				type=2;
				this.values=values;
				this.var=var;
				active=false;
			}
		}
		private static ScriptReturnData srd;
		private static Dictionary<string, string> variables;


		private static string DataFiles;
		private static string Plugins;
		private static string cLine="0";
		private static string[] SplitLine(string s) {
			List<string> temp=new List<string>();
			bool WasLastSpace=false;
			bool InQuotes=false;
			bool WasLastEscape=false;
			bool DoubleBreak=false;
			bool InVar=false;
			string CurrentWord="";
			string CurrentVar="";

			if(s=="") return new string[0];
			s+=" ";
			for(int i=0;i<s.Length;i++) {
				switch(s[i]) {
					case '%':
						WasLastSpace=false;
						if(InVar) {
							if(variables.ContainsKey(CurrentWord)) CurrentWord=CurrentVar+variables[CurrentWord];
							else CurrentWord=CurrentVar+"%"+CurrentWord+"%";
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
								if(!variables.ContainsKey(CurrentWord)) temp.Add("");
								else temp.Add(variables[CurrentWord]);
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
							if(InVar) Warn("String marker found in the middle of a variable name");
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
			if(InVar) Warn("Unterminated variable");
			if(InQuotes) Warn("Unterminated quote");
			return temp.ToArray();
		}

		private static void Warn(string Message) {
			if(Settings.ShowScriptWarnings)
				MessageBox.Show(Message+" on line "+cLine, "Error in script");
		}

		private static bool FunctionIf(string[] line) {
			if(line.Length==1) {
				Warn("Missing arguments to function 'If'");
				return false;
			}
			switch(line[1]) {
				case "DialogYesNo":
					switch(line.Length) {
						case 2:
							Warn("Missing arguments to function 'If DialogYesNo'");
							return false;
						case 3:
							return MessageBox.Show(line[2], "", mbButtons.YesNo)==DialogResult.Yes;
						case 4:
							return MessageBox.Show(line[2], line[3], mbButtons.YesNo)==DialogResult.Yes;
						default:
							Warn("Unexpected arguments after function 'If DialogYesNo'");
							goto case 4;
					}
				case "DataFileExists":
					if(line.Length==2) {
						Warn("Missing arguments to function 'If DataFileExists'");
						return false;
					}
					return File.Exists(Path.Combine(Program.currentGame.DataFolderPath,line[2]));
				case "VersionGreaterThan":
					if(line.Length==2) {
						Warn("Missing arguments to function 'If VersionGreaterThan'");
						return false;
					}
					try {
						Version v=new Version(line[2]+".0");
                        Version v2 = new Version(Program.version + ".0");
                            //Program.MajorVersion.ToString()+"."+Program.MinorVersion.ToString()+"."+Program.BuildNumber.ToString()+".0");
						return (v2>v);
					} catch {
						Warn("Invalid argument to function 'If VersionGreaterThan'");
						return false;
					}
				case "VersionLessThan":
					if(line.Length==2) {
						Warn("Missing arguments to function 'If VersionLessThan'");
						return false;
					}
					try {
						Version v=new Version(line[2]+".0");
                        Version v2 = new Version(Program.version + ".0");
                        //Program.MajorVersion.ToString()+"."+Program.MinorVersion.ToString()+"."+Program.BuildNumber.ToString()+".0");
						return (v2<v);
					} catch {
						Warn("Invalid argument to function 'If VersionGreaterThan'");
						return false;
					}
				case "ScriptExtenderPresent":
					if(line.Length>2) Warn("Unexpected arguments to 'If ScriptExtenderPresent'");
					return File.Exists(Path.Combine(Program.currentGame.GamePath, Program.currentGame.ScriptExtenderExe)) || File.Exists(Path.Combine(Program.currentGame.GamePath, Program.currentGame.ScriptExtenderDLL));
				case "ScriptExtenderNewerThan":
					if(line.Length==2) {
						Warn("Missing arguments to function 'If ScriptExtenderNewerThan'");
						return false;
					}
					if(line.Length>3) Warn("Unexpected arguments to 'If ScriptExtenderNewerThan'");
					if(!(File.Exists(Path.Combine(Program.currentGame.GamePath, Program.currentGame.ScriptExtenderExe)) || File.Exists(Path.Combine(Program.currentGame.GamePath, Program.currentGame.ScriptExtenderDLL)))) return false;
					try {
						System.Diagnostics.FileVersionInfo fvi;
                        fvi = File.Exists(Path.Combine(Program.currentGame.GamePath, Program.currentGame.ScriptExtenderExe)) ? System.Diagnostics.FileVersionInfo.GetVersionInfo(Path.Combine(Program.currentGame.GamePath, Program.currentGame.ScriptExtenderExe)) :
                            System.Diagnostics.FileVersionInfo.GetVersionInfo(Path.Combine(Program.currentGame.GamePath, Program.currentGame.ScriptExtenderDLL));
						if(fvi.FileVersion==null) return false;
						Version v=new Version(line[2]);
						Version v2=new Version(fvi.FileVersion.Replace(", ", "."));
						return (v2>=v);
					} catch {
						Warn("Invalid argument to function 'If ScriptExtenderNewerThan'");
						return false;
					}
				case "GraphicsExtenderPresent":
					if(line.Length>2) Warn("Unexpected arguments to 'If GraphicsExtenderPresent'");
                    return File.Exists(Path.Combine(Program.currentGame.DataFolderPath, Program.currentGame.GraphicsExtenderPath));
                case "GraphicsExtenderNewerThan":
					if(line.Length==2) {
						Warn("Missing arguments to function 'If GraphicsExtenderNewerThan'");
						return false;
					}
					if(line.Length>3) Warn("Unexpected arguments to 'If GraphicsExtenderNewerThan'");
                    if (!File.Exists(Path.Combine(Program.currentGame.DataFolderPath, Program.currentGame.GraphicsExtenderPath)))
                        return false;
					try {
                        System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(Path.Combine(Program.currentGame.DataFolderPath, Program.currentGame.GraphicsExtenderPath));
						if(fvi.FileVersion==null) return false;
						Version v=new Version(line[2]); ;
						Version v2=new Version(fvi.FileVersion.Replace(", ", "."));
						return (v2>=v);
					} catch {
						Warn("Invalid argument to function 'If GraphicsExtenderNewerThan'");
						return false;
					}
				case "OblivionNewerThan":
					if(line.Length==2) {
						Warn("Missing arguments to function 'If OblivionNewerThan'");
						return false;
					}
					if(line.Length>3) Warn("Unexpected arguments to 'If OblivionNewerThan'");
					try {
                        System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(Path.Combine(Program.currentGame.GamePath, Program.currentGame.ExeName));// "oblivion.exe");
						if(fvi.FileVersion==null) return false;
						Version v=new Version(line[2]); ;
						Version v2=new Version(fvi.FileVersion.Replace(", ", "."));
						bool b=v2>=v;
						return (v2>=v);
					} catch {
						Warn("Invalid argument to function 'If OblivionNewerThan'");
						return false;
					}
				case "Equal":
					if(line.Length<4) {
						Warn("Missing arguments to function 'If Equal'");
						return false;
					}
					if(line.Length>4) Warn("Unexpected arguments to 'If Equal'");
					return line[2]==line[3];
				case "GreaterEqual":
					case "GreaterThan": {
						if(line.Length<4) {
							Warn("Missing arguments to function 'If Greater'");
							return false;
						}
						if(line.Length>4) Warn("Unexpected arguments to 'If Greater'");
						int arg1, arg2;
						if(!int.TryParse(line[2], out arg1)||!int.TryParse(line[3], out arg2)) {
							Warn("Invalid argument upplied to function 'If Greater'");
							return false;
						}
						if(line[1]=="GreaterEqual") return arg1 >= arg2;
						else return arg1>arg2;
					}
				case "fGreaterEqual":
					case "fGreaterThan": {
						if(line.Length<4) {
							Warn("Missing arguments to function 'If fGreater'");
							return false;
						}
						if(line.Length>4) Warn("Unexpected arguments to 'If fGreater'");
						double arg1, arg2;
						if(!double.TryParse(line[2], out arg1)||!double.TryParse(line[3], out arg2)) {
							Warn("Invalid argument upplied to function 'If fGreater'");
							return false;
						}
						if(line[1]=="fGreaterEqual") return arg1 >= arg2;
						else return arg1>arg2;
					}
				default:
					Warn("Unknown argument '"+line[1]+"' supplied to 'If'");
					return false;
			}

		}

		private static string[] FunctionSelect(string[] line, bool many, bool Previews, bool Descriptions) {
			if(line.Length<3) {
				Warn("Missing arguments to function 'Select'");
				return new string[0];
			}
			//variables
			string[] items;
			string[] previews;
			string[] descs;
			int argsperoption=1+(Previews?1:0)+(Descriptions?1:0);
			//Remove first 2 arguments
			string title=line[1];
			items=new string[line.Length-2];
			Array.Copy(line, 2, items, 0, line.Length-2);
			line=items;
			//Check for incorrect number of arguments
			if(line.Length%argsperoption!=0) {
				Warn("Unexpected extra arguments to 'Select'");
				Array.Resize<string>(ref line, line.Length-line.Length%argsperoption);
			}
			//Create arrays to pass to the select form
			items=new string[line.Length/argsperoption];
			previews=Previews?new string[line.Length/argsperoption]:null;
			descs=Descriptions?new string[line.Length/argsperoption]:null;
			for(int i=0;i<line.Length/argsperoption;i++) {
				items[i]=line[i*argsperoption];
				if(Previews) {
					previews[i]=line[i*argsperoption + 1];
					if(Descriptions) descs[i]=line[i*argsperoption + 2];
				} else {
					if(Descriptions) descs[i]=line[i*argsperoption + 1];
				}
			}
			//Check for previews
			if (previews != null)
			{
				int imno = 0;
				for(int i=0;i<previews.Length;i++)
				{
					//MessageBox.Show(previews[i]);
					if (previews[i] == "None")
					{
						previews[i] = null;
					}
					else if (previews[i].StartsWith("https://", StringComparison.CurrentCultureIgnoreCase) || previews[i].StartsWith("http://", StringComparison.CurrentCultureIgnoreCase))
					{
						List<MemoryStream> allstreams = DownloadForm.DownloadFiles(new string[] { previews[i] }, false);
						
						if (allstreams[0] == null)
							previews[i] = null;
						else
						{
							string imn = Path.Combine(DataFiles,"onlineimg" + imno.ToString() + ".png");
							if (File.Exists(imn))
								File.Delete(imn);
							
							FileStream fs = new FileStream(imn, FileMode.Create);
							allstreams[0].WriteTo(fs);
							fs.Close();
							
							previews[i] = imn;
							imno++;
						}
					}
					else if (!Program.IsSafeFileName(previews[i]))
					{
						Warn("Preview file path '"+previews[i]+"' was invalid");
						previews[i] = null;
					}
					else if (!File.Exists(Path.Combine(DataFiles,previews[i])))
					{
						Warn("Preview file path '" + previews[i] + "' does not exist");
						previews[i] = null;
					}
					else
					{
						previews[i] = Path.Combine(DataFiles,previews[i]);
					}
				}
			}
			//Display select form
			Forms.SelectForm sf=new OblivionModManager.Forms.SelectForm(items, title, many, previews, descs);
			try {
				sf.ShowDialog();
			} catch(ExecutionCancelledException) {
				srd.CancelInstall=true;
				return new string[0];
			}
			string[] result=new string[sf.SelectedIndex.Length];
			for(int i=0;i<sf.SelectedIndex.Length;i++) {
				result[i]="Case "+items[sf.SelectedIndex[i]];
			}
			return result;
		}

		private static string[] FunctionSelectVar(string[] line, bool IsVariable) {
			string Func;
			if(IsVariable) Func=" to function 'SelectVar'"; else Func="to function 'SelectString'";
			if(line.Length<2) {
				Warn("Missing arguments"+Func);
				return new string[0];
			}
			if(line.Length>2) Warn("Unexpected arguments"+Func);
			if(IsVariable) {
				if(!variables.ContainsKey(line[1])) {
					Warn("Invalid argument"+Func+"\nVariable '"+line[1]+"' does not exist");
					return new string[0];
				} else return new string[] { "Case "+variables[line[1]] };
			} else {
				return new string[] { "Case "+line[1] };
			}
		}

		private static FlowControlStruct FunctionFor(string[] line, int LineNo) {
			FlowControlStruct NullLoop=new FlowControlStruct(2);
			if(line.Length<3) {
				Warn("Missing arguments to function 'For'");
				return NullLoop;
			}
			if(line[1]=="Each") line[1]=line[2];
			switch(line[1]) {
					case "Count": {
						if(line.Length<5) {
							Warn("Missing arguments to function 'For Count'");
							return NullLoop;
						}
						if(line.Length>6) Warn("Unexpected extra arguments to 'For Count'");
						int start, end, step=1;
						if(!int.TryParse(line[3], out start)||!int.TryParse(line[4], out end)||(line.Length>=6&&!int.TryParse(line[5], out step))) {
							Warn("Invalid argument to 'For Count'");
							return NullLoop;
						}
						List<string> steps=new List<string>();
						for(int i=start;i<=end;i+=step) {
							steps.Add(i.ToString());
						}
						return new FlowControlStruct(steps.ToArray(), line[2], LineNo);
					}
					case "DataFolder": {
						if(line.Length<5) {
							Warn("Missing arguments to function 'For Each DataFolder'");
							return NullLoop;
						}
						if(line.Length>7) Warn("Unexpected extra arguments to 'For Each DataFolder'");
						if(!Program.IsSafeFolderName(line[4])) {
							Warn("Invalid argument to 'For Each DataFolder'\nDirectory '"+line[4]+"' is not valid");
							return NullLoop;
						}
						if(!Directory.Exists(Path.Combine(DataFiles,line[4]))) {
							Warn("Invalid argument to 'For Each DataFolder'\nDirectory '"+line[4]+"' is not a part of this plugin");
							return NullLoop;
						}
						System.IO.SearchOption option=System.IO.SearchOption.TopDirectoryOnly;
						if(line.Length>5) {
							switch(line[5]) {
								case "True":
									option=System.IO.SearchOption.AllDirectories;
									break;
								case "False":
									break;
								default:
									Warn("Invalid argument '"+line[5]+"' to 'For Each DataFolder'.\nExpected 'True' or 'False'");
									break;
							}
						}
						try {
							string[] paths=Directory.GetDirectories(Path.Combine(DataFiles,line[4]), line.Length>6?line[6]:"*", option);
							for(int i=0;i<paths.Length;i++) if(Path.IsPathRooted(paths[i])) paths[i]=paths[i].Substring(DataFiles.Length);
							return new FlowControlStruct(paths, line[3], LineNo);
						} catch {
							Warn("Invalid argument to 'For Each DataFolder'");
							return NullLoop;
						}
					}
					case "PluginFolder": {
						if(line.Length<5) {
							Warn("Missing arguments to function 'For Each PluginFolder'");
							return NullLoop;
						}
						if(line.Length>7) Warn("Unexpected extra arguments to 'For Each PluginFolder'");
						if(!Program.IsSafeFolderName(line[4])) {
							Warn("Invalid argument to 'For Each PluginFolder'\nDirectory '"+line[4]+"' is not valid");
							return NullLoop;
						}
						if(!Directory.Exists(Plugins+line[4])) {
							Warn("Invalid argument to 'For Each PluginFolder'\nDirectory '"+line[4]+"' is not a part of this plugin");
							return NullLoop;
						}
						System.IO.SearchOption option=System.IO.SearchOption.TopDirectoryOnly;
						if(line.Length>5) {
							switch(line[5]) {
								case "True":
									option=System.IO.SearchOption.AllDirectories;
									break;
								case "False":
									break;
								default:
									Warn("Invalid argument '"+line[5]+"' to 'For Each PluginFolder'.\nExpected 'True' or 'False'");
									break;
							}
						}
						try {
							string[] paths=Directory.GetDirectories(Path.Combine(Plugins,line[4]), line.Length>6?line[6]:"*", option);
							for(int i=0;i<paths.Length;i++) if(Path.IsPathRooted(paths[i])) paths[i]=paths[i].Substring(Plugins.Length);
							return new FlowControlStruct(paths, line[3], LineNo);
						} catch {
							Warn("Invalid argument to 'For Each PluginFolder'");
							return NullLoop;
						}
					}
					case "DataFile": {
						if(line.Length<5) {
							Warn("Missing arguments to function 'For Each DataFile'");
							return NullLoop;
						}
						if(line.Length>7) Warn("Unexpected extra arguments to 'For Each DataFile'");
						if(!Program.IsSafeFolderName(line[4])) {
							Warn("Invalid argument to 'For Each DataFile'\nDirectory '"+line[4]+"' is not valid");
							return NullLoop;
						}
						if(!Directory.Exists(Path.Combine(DataFiles,line[4]))) {
							Warn("Invalid argument to 'For Each DataFile'\nDirectory '"+line[4]+"' is not a part of this plugin");
							return NullLoop;
						}
						System.IO.SearchOption option=System.IO.SearchOption.TopDirectoryOnly;
						if(line.Length>5) {
							switch(line[5]) {
								case "True":
									option=System.IO.SearchOption.AllDirectories;
									break;
								case "False":
									break;
								default:
									Warn("Invalid argument '"+line[5]+"' to 'For Each DataFile'.\nExpected 'True' or 'False'");
									break;
							}
						}
						try {
							string[] paths=Directory.GetFiles(Path.Combine(DataFiles,line[4]), line.Length>6?line[6]:"*", option);
							for(int i=0;i<paths.Length;i++) if(Path.IsPathRooted(paths[i])) paths[i]=paths[i].Substring(DataFiles.Length);
							return new FlowControlStruct(paths, line[3], LineNo);
						} catch {
							Warn("Invalid argument to 'For Each DataFile'");
							return NullLoop;
						}
					}
					case "Plugin": {
						if(line.Length<5) {
							Warn("Missing arguments to function 'For Each Plugin'");
							return NullLoop;
						}
						if(line.Length>7) Warn("Unexpected extra arguments to 'For Each Plugin'");
						if(!Program.IsSafeFolderName(line[4])) {
							Warn("Invalid argument to 'For Each Plugin'\nDirectory '"+line[4]+"' is not valid");
							return NullLoop;
						}
						if(!Directory.Exists(Plugins+line[4])) {
							Warn("Invalid argument to 'For Each Plugin'\nDirectory '"+line[4]+"' is not a part of this plugin");
							return NullLoop;
						}
						System.IO.SearchOption option=System.IO.SearchOption.TopDirectoryOnly;
						if(line.Length>5) {
							switch(line[5]) {
								case "True":
									option=System.IO.SearchOption.AllDirectories;
									break;
								case "False":
									break;
								default:
									Warn("Invalid argument '"+line[5]+"' to 'For Each Plugin'.\nExpected 'True' or 'False'");
									break;
							}
						}
						try {
							string[] paths=Directory.GetFiles(Path.Combine(Plugins,line[4]), line.Length>6?line[6]:"*", option);
							for(int i=0;i<paths.Length;i++) if(Path.IsPathRooted(paths[i])) paths[i]=paths[i].Substring(Plugins.Length);
							return new FlowControlStruct(paths, line[3], LineNo);
						} catch {
							Warn("Invalid argument to 'For Each Plugin'");
							return NullLoop;
						}
					}
			}
			return NullLoop;
		}
		public static void FunctionSelectNumber(string[] line) {
			double val;
			
			if (line.Length == 2 && line[1] == "UseVars")
			{
				line = new string[] {
					"SelectNumber",
					variables["SNCaption"],
					variables["SNMessage"],
					variables["SNDefault"],
					variables["SNMin"],
					variables["SNMax"],
					variables["SNStep"],
					variables["SNDecimalPlaces"]
				};
			}
			
			if (line.Length == 8)
			{
				
				val = NumberForm.SelectNumber(line[1], line[2], double.Parse(line[3]),
				                              double.Parse(line[4]), double.Parse(line[5]),
				                              double.Parse(line[6]), int.Parse(line[7]));
				
				variables["SelectNumber"] = val.ToString();
			}
			else
			{
				Warn("Incorrect number of arguments for SelectNumber(Caption, Message, Default, Min, Max, Step, DecimalPlaces)");
			}
		}
		private static void FunctionMessage(string[] line) {
			switch(line.Length) {
				case 1:
					Warn("Missing arguments to function 'Message'");
					break;
				case 2:
					MessageBox.Show(line[1]);
					break;
				case 3:
					MessageBox.Show(line[1], line[2]);
					break;
				default:
					MessageBox.Show(line[1], line[2]);
					Warn("Unexpected arguments after 'Message'");
					break;
			}
		}

		private static void FunctionLoadEarly(string[] line) {
			if(line.Length<2) {
				Warn("Missing arguments to LoadEarly");
				return;
			} else if(line.Length>2) {
				Warn("Unexpected arguments to LoadEarly");
			}
			line[1]=line[1].ToLower();
			if(!srd.EarlyPlugins.Contains(line[1])) srd.EarlyPlugins.Add(line[1]);
		}

		private static void FunctionLoadOrder(string[] line, bool LoadAfter) {
			string WarnMess;
			if(LoadAfter) WarnMess = "function 'LoadAfter'"; else WarnMess="function 'LoadBefore'";
			if(line.Length<3) {
				Warn("Missing arguments to "+WarnMess);
				return;
			} else if(line.Length>3) {
				Warn("Unexpected arguments to "+WarnMess);
			}
			srd.LoadOrderList.Add(new PluginLoadInfo(line[1], line[2], LoadAfter));
		}

		private static void FunctionConflicts(string[] line, bool Conflicts, bool Regex) {
			string WarnMess;
			if(Conflicts) WarnMess="function 'ConflictsWith"; else WarnMess="function 'DependsOn";
			if(Regex) WarnMess+="Regex'"; else WarnMess+="'";
			ConflictData cd=new ConflictData();
			cd.level=ConflictLevel.MajorConflict;
			switch(line.Length) {
				case 1:
					Warn("Missing arguments to "+WarnMess);
					return;
				case 2:
					cd.File=line[1];
					break;
				case 3:
					cd.Comment=line[2];
					goto case 2;
				case 4:
					switch(line[3]) {
						case "Unusable":
							cd.level=ConflictLevel.Unusable;
							break;
						case "Major":
							cd.level=ConflictLevel.MajorConflict;
							break;
						case "Minor":
							cd.level=ConflictLevel.MinorConflict;
							break;
						default:
							Warn("Unknown conflict level after "+WarnMess);
							break;
					}
					goto case 3;
				case 5:
					Warn("Unexpected arguments to "+WarnMess);
					goto case 4;
				case 6:
					cd.File=line[1];
					try {
						cd.MinMajorVersion=Convert.ToInt32(line[2]);
						cd.MinMinorVersion=Convert.ToInt32(line[3]);
						cd.MaxMajorVersion=Convert.ToInt32(line[4]);
						cd.MaxMinorVersion=Convert.ToInt32(line[5]);
					} catch {
						Warn("Arguments to "+WarnMess+" in incorrect format");
						return;
					}
					break;
				case 7:
					cd.Comment=line[6];
					goto case 6;
				case 8:
					switch(line[7]) {
						case "Unusable":
							cd.level=ConflictLevel.Unusable;
							break;
						case "Major":
							cd.level=ConflictLevel.MajorConflict;
							break;
						case "Minor":
							cd.level=ConflictLevel.MinorConflict;
							break;
						default:
							Warn("Unknown conflict level after "+WarnMess);
							break;
					}
					goto case 7;
				default:
					Warn("Unexpected arguments to "+WarnMess);
					goto case 8;
			}
			cd.Partial=Regex;
			if(Conflicts) srd.ConflictsWith.Add(cd); else srd.DependsOn.Add(cd);
		}

		private static void FunctionModifyInstall(string[] line, bool plugins, bool Install) {
			string WarnMess;
			if(plugins) {
				if(Install) WarnMess="function 'InstallPlugin'"; else WarnMess="function 'DontInstallPlugin'";
			} else {
				if(Install) WarnMess="function 'InstallDataFile'"; else WarnMess="function 'DontInstallDataFile'";
			}
			if(line.Length==1) {
				Warn("Missing arguments to "+WarnMess);
				return;
			}
			if(line.Length>2) Warn("Unexpected arguments after "+WarnMess);
			if(plugins) {
				if(!File.Exists(Path.Combine(Plugins,line[1]))) {
					Warn("Invalid argument to "+WarnMess+"\nFile '"+line[1]+"' is not part of this plugin");
					return;
				}
				if(line[1].IndexOfAny(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar })!=-1) {
					Warn("Invalid argument to "+WarnMess+"\nThis function cannot be used on plugins stored in subdirectories");
					return;
				}
				if(Install) {
					Program.strArrayRemove(srd.IgnorePlugins, line[1]);
					if(!Program.strArrayContains(srd.InstallPlugins, line[1])) srd.InstallPlugins.Add(line[1]);
				} else {
					Program.strArrayRemove(srd.InstallPlugins, line[1]);
					if(!Program.strArrayContains(srd.IgnorePlugins, line[1])) srd.IgnorePlugins.Add(line[1]);
				}
			} else {
				if(!File.Exists(DataFiles+line[1])) {
					Warn("Invalid argument to "+WarnMess+"\nFile '"+line[1]+"' is not part of this plugin");
					return;
				}
				if(Install) {
					Program.strArrayRemove(srd.IgnoreData, line[1]);
					if(!Program.strArrayContains(srd.InstallData, line[1])) srd.InstallData.Add(line[1]);
				} else {
					Program.strArrayRemove(srd.InstallData, line[1]);
					if(!Program.strArrayContains(srd.IgnoreData, line[1])) srd.IgnoreData.Add(line[1]);
				}
			}
		}

		private static void FunctionModifyInstallFolder(string[] line, bool Install) {
			string WarnMess;
			if(Install) WarnMess="function 'InstallDataFolder'"; else WarnMess="function 'DontInstallDataFolder'";
			if(line.Length==1) {
				Warn("Missing arguments to "+WarnMess);
				return;
			}
			if(line.Length>3) Warn("Unexpected arguments to "+WarnMess);
			line[1]=Program.MakeValidFolderPath(line[1]);

			if(!Directory.Exists(DataFiles+line[1])) {
				Warn("Invalid argument to "+WarnMess+"\nFolder '"+line[1]+"' is not part of this plugin");
				return;
			}

			if(line.Length>=3) {
				switch(line[2]) {
					case "True":
						foreach(string folder in Directory.GetDirectories(Path.Combine(DataFiles,line[1]))) {
							FunctionModifyInstallFolder(new string[] { "", folder.Substring(DataFiles.Length), "True" }, Install);
						}
						break;
					case "False":
						break;
					default:
						Warn("Invalid argument to "+WarnMess+"\nExpected True or False");
						return;
				}
			}

			foreach(string path in Directory.GetFiles(DataFiles+line[1])) {
				string file=line[1]+System.IO.Path.GetFileName(path);
				if(Install) {
					Program.strArrayRemove(srd.IgnoreData, file);
					if(!Program.strArrayContains(srd.InstallData, file)) srd.InstallData.Add(file);
				} else {
					Program.strArrayRemove(srd.InstallData, file);
					if(!Program.strArrayContains(srd.IgnoreData, file)) srd.IgnoreData.Add(file);
				}
			}
		}

		private static void FunctionRegisterBSA(string[] line, bool Register) {
			string WarnMess;
			if(Register) WarnMess="function 'RegisterBSA'"; else WarnMess="function 'UnregisterBSA'";
			if(line.Length==1) {
				Warn("Missing arguments to "+WarnMess);
				return;
			}
			if(line[1].Contains(",")||line[1].Contains(";")||line[1].Contains("=")) {
				Warn("BSA file names are not allowed to include the characters ',' '=' or ';'\n"+
				     "Invalid argument to "+WarnMess);
				return;
			}
			if(line.Length>2) Warn("Unexpected arguments after "+WarnMess);
			line[1]=line[1].ToLower();
			if(Register) {
				if(!srd.RegisterBSAList.Contains(line[1])) srd.RegisterBSAList.Add(line[1]);
			} else {
				srd.RegisterBSAList.Remove(line[1]);
			}
		}

		private static void FunctionUncheckESP(string[] line) {
			if(line.Length==1) {
				Warn("Missing arguments to UncheckESP");
				return;
			}
			if(line.Length>2) Warn("Unexpected arguments to UncheckESP");
			if(!File.Exists(Path.Combine(Plugins,line[1]))) {
				Warn("Invalid argument to UncheckESP\nFile '"+line[1]+"' is not part of this plugin");
				return;
			}
			line[1]=line[1].ToLower();
			if(!srd.UncheckedPlugins.Contains(line[1])) srd.UncheckedPlugins.Add(line[1]);
		}

		private static void FunctionSetDeactivationWarning(string[] line) {
			if(line.Length<3) {
				Warn("Missing arguments to SetDeactivationWarning");
				return;
			}
			if(line.Length>3) Warn("Unexpected arguments to SetDeactivationWarning");
			if(!File.Exists(Path.Combine(Plugins,line[1]))) {
				Warn("Invalid argument to SetDeactivationWarning\nFile '"+line[1]+"' is not part of this plugin");
				return;
			}
			line[1]=line[1].ToLower();
			for(int i=0;i<srd.EspDeactivation.Count;i++) {
				if(srd.EspDeactivation[i].Plugin==line[1]) srd.EspDeactivation.RemoveAt(i--);
			}
			switch(line[2]) {
				case "Allow":
					srd.EspDeactivation.Add(new ScriptEspWarnAgainst(line[1], DeactiveStatus.Allow));
					break;
				case "WarnAgainst":
					srd.EspDeactivation.Add(new ScriptEspWarnAgainst(line[1], DeactiveStatus.WarnAgainst));
					break;
				case "Disallow":
					srd.EspDeactivation.Add(new ScriptEspWarnAgainst(line[1], DeactiveStatus.Disallow));
					break;
				default:
					Warn("invalid argument to SetDeactivationWarning");
					return;
			}
		}

		private static void FunctionCopyDataFile(string[] line, bool Plugin) {
			string WarnMess;
			if(Plugin) WarnMess="function 'CopyPlugin'"; else WarnMess="function 'CopyDataFile'";
			if(line.Length<3) {
				Warn("Missing arguments to "+WarnMess);
				return;
			}
			if(line.Length>3) Warn("Unexpected arguments to "+WarnMess);
			string upperfrom=line[1];
			string upperto=line[2];
			line[1]=line[1].ToLower();
			line[2]=line[2].ToLower();
			if(!Program.IsSafeFileName(line[1])||!Program.IsSafeFileName(line[2])) {
				Warn("Invalid argument to "+WarnMess);
				return;
			}
			if(line[1]==line[2]) {
				Warn("Invalid argument to "+WarnMess+"\nYou cannot copy a file over itself");
				return;
			}
			if(Plugin) {
				if(!File.Exists(Path.Combine(Plugins,line[1]))) {
					Warn("Invalid argument to CopyPlugin\nFile '"+upperfrom+"' is not part of this plugin");
					return;
				}
				if(line[2].IndexOfAny(new char[] { System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar })!=-1) {
					Warn("Plugins cannot be copied to subdirectories of the data folder");
					return;
				}
				if(!(line[2].EndsWith(".esp")||line[2].EndsWith(".esm"))) {
					Warn("Copied plugins must have a .esp or .esm extension");
					return;
				}
			} else {
				if(!File.Exists(Path.Combine(DataFiles,line[1]))) {
					Warn("Invalid argument to CopyDataFile\nFile '"+upperfrom+"' is not part of this plugin");
					return;
				}
				if(line[2].EndsWith(".esp")||line[2].EndsWith(".esm")) {
					Warn("Copied data files cannot have a .esp or .esm extension");
					return;
				}
			}

			if(Plugin) {
				for(int i=0;i<srd.CopyPlugins.Count;i++) {
					if(srd.CopyPlugins[i].CopyTo==line[2]) srd.CopyPlugins.RemoveAt(i--);
				}
				srd.CopyPlugins.Add(new ScriptCopyDataFile(upperfrom, upperto));
			} else {
				for(int i=0;i<srd.CopyDataFiles.Count;i++) {
					if(srd.CopyDataFiles[i].CopyTo==line[2]) srd.CopyDataFiles.RemoveAt(i--);
				}
				srd.CopyDataFiles.Add(new ScriptCopyDataFile(upperfrom, upperto));
			}
		}

		private static void FunctionCopyDataFolder(string[] line) {
			if(line.Length<3) {
				Warn("Missing arguments to CopyDataFolder");
				return;
			}
			if(line.Length>4) Warn("Unexpected arguments to CopyDataFolder");
			line[1]=Program.MakeValidFolderPath(line[1].ToLower());
			line[2]=Program.MakeValidFolderPath(line[2].ToLower());
			if(!Program.IsSafeFolderName(line[1])||!Program.IsSafeFolderName(line[2])) {
				Warn("Invalid argument to CopyDataFolder");
				return;
			}
			if(!Directory.Exists(Path.Combine(DataFiles,line[1]))) {
				Warn("Invalid argument to CopyDataFolder\nFolder '"+line[1]+"' is not part of this plugin");
				return;
			}
			if(line[1]==line[2]) {
				Warn("Invalid argument to CopyDataFolder\nYou cannot copy a folder over itself");
				return;
			}

			if(line.Length>=4) {
				switch(line[3]) {
					case "True":
						foreach(string folder in Directory.GetDirectories(Path.Combine(DataFiles,line[1]))) {                            
							FunctionCopyDataFolder(new string[] { "", folder.Substring(DataFiles.Length), Path.Combine(line[2],folder.Substring(Path.Combine(DataFiles,line[1]).Length)), "True" });
						}
						break;
					case "False":
						break;
					default:
						Warn("Invalid argument to CopyDataFolder\nExpected True or False");
						return;
				}
			}

			foreach(string s in Directory.GetFiles(Path.Combine(DataFiles,line[1]))) {
				string from=line[1]+Path.GetFileName(s);
				string to=line[2]+Path.GetFileName(s);
				string lto=to.ToLower();
				for(int i=0;i<srd.CopyDataFiles.Count;i++) {
					if(srd.CopyDataFiles[i].CopyTo==lto) srd.CopyDataFiles.RemoveAt(i--);
				}
				srd.CopyDataFiles.Add(new ScriptCopyDataFile(from, to));
			}
		}

		private static void FunctionPatch(string[] line, bool Plugin) {
			string WarnMess;
			if(Plugin) WarnMess="function 'PatchPlugin'"; else WarnMess="function 'PatchDataFile'";
			if(line.Length<3) {
				Warn("Missing arguments to "+WarnMess);
				return;
			}
			if(line.Length>4) Warn("Unexpected arguments to "+WarnMess);
			string lowerto=line[2].ToLower();
			if(!Program.IsSafeFileName(line[1])||!Program.IsSafeFileName(line[2])) {
				Warn("Invalid argument to "+WarnMess);
				return;
			}
			string copypath;
			if(Plugin) {
				copypath=Path.Combine(Plugins,line[1]);
				if(!File.Exists(copypath)) {
					Warn("Invalid argument to PatchPlugin\nFile '"+line[1]+"' is not part of this plugin");
					return;
				}
				if(line[2].IndexOfAny(new char[] { System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar })!=-1) {
					Warn("Plugins cannot be copied to subdirectories of the data folder");
					return;
				}
				if(!(lowerto.EndsWith(".esp")||lowerto.EndsWith(".esm"))) {
					Warn("Plugins must have a .esp or .esm extension");
					return;
				}

			} else {
				copypath=Path.Combine(DataFiles,line[1]);
				if(!File.Exists(copypath)) {
					Warn("Invalid argument to PatchDataFile\nFile '"+line[1]+"' is not part of this plugin");
					return;
				}
				if(lowerto.EndsWith(".esp")||lowerto.EndsWith(".esm")) {
					Warn("Data files cannot have a .esp or .esm extension");
					return;
				}
			}
			DateTime timestamp=File.GetLastWriteTime(copypath);
			if(File.Exists(Path.Combine(Program.currentGame.DataFolderPath,line[2]))) {
				if(Plugin) {
					if(!Program.Data.DoesEspExist(line[2])) {
						Warn("Cannot patch file '"+line[2]+"' because it already exists but is not parented to an omod");
						return;
					}
				} else {
					if(!Program.Data.DoesDataFileExist(line[2])) {
						Warn("Cannot patch file '"+line[2]+"' because it already exists but is not parented to an omod");
						return;
					}
				}
                timestamp = File.GetLastWriteTime(Path.Combine(Program.currentGame.DataFolderPath, line[2]));
				File.Delete(Path.Combine(Program.currentGame.DataFolderPath,line[2]));
			} else if(line.Length<4||line[3]!="True") return;
            File.Move(copypath, Path.Combine(Program.currentGame.DataFolderPath, line[2]));
            File.SetLastWriteTime(Path.Combine(Program.currentGame.DataFolderPath, line[2]), timestamp);
		}

		private static void FunctionEditINI(string[] line) {
			if(line.Length<4) {
				Warn("Missing arguments to EditINI");
				return;
			}
			if(line.Length>4) Warn("Unexpected arguments to EditINI");
			srd.INIEdits.Add(new INIEditInfo(line[1], line[2], line[3]));
		}

		private static void FunctionEditShader(string[] line) {
			if(line.Length<4) {
				Warn("Missing arguments to 'EditShader'");
				return;
			}
			if(line.Length>4) Warn("Unexpected arguments to 'EditShader'");
			if(!Program.IsSafeFileName(line[3])) {
				Warn("Invalid argument to 'EditShader'\n'"+line[3]+"' is not a valid file name");
				return;
			}
			if(!File.Exists(Path.Combine(DataFiles,line[3]))) {
				Warn("Invalid argument to 'EditShader'\nFile '"+line[3]+"' does not exist");
				return;
			}
			byte package;
			if(!byte.TryParse(line[1], out package)) {
				Warn("Invalid argument to function 'EditShader'\n'"+line[1]+"' is not a valid shader package ID");
				return;
			}
			srd.SDPEdits.Add(new SDPEditInfo(package, line[2], Path.Combine(DataFiles,line[3])));
		}

		private static void FunctionSetEspVar(string[] line, bool GMST) {
			string WarnMess;
			if(GMST) WarnMess="function 'SetGMST'"; else WarnMess="function 'SetGlobal'";
			if(line.Length<4) {
				Warn("Missing arguments to "+WarnMess);
				return;
			}
			if(line.Length>4) Warn("Unexpected extra arguments to "+WarnMess);
			if(!Program.IsSafeFileName(line[1])) {
				Warn("Illegal plugin name supplied to "+WarnMess);
				return;
			}
			if(!File.Exists(Plugins+line[1])) {
				Warn("Invalid argument to "+WarnMess+"\nFile '"+line[1]+"' is not part of this plugin");
				return;
			}
			srd.EspEdits.Add(new ScriptEspEdit(GMST, line[1].ToLower(), line[2].ToLower(), line[3]));
		}

		private static void FunctionSetEspData(string[] line, Type type) {
			string WarnMess=null;
			if(type==typeof(byte)) WarnMess="function 'SetPluginByte'";
			else if(type==typeof(short)) WarnMess="function 'SetPluginShort'";
			else if(type==typeof(int)) WarnMess="function 'SetPluginInt'";
			else if(type==typeof(long)) WarnMess="function 'SetPluginLong'";
			else if(type==typeof(float)) WarnMess="function 'SetPluginFloat'";
			if(line.Length<4) {
				Warn("Missing arguments to "+WarnMess);
				return;
			}
			if(line.Length>4) Warn("Unexpected extra arguments to "+WarnMess);
			if(!Program.IsSafeFileName(line[1])) {
				Warn("Illegal plugin name supplied to "+WarnMess);
				return;
			}
			if(!File.Exists(Plugins+line[1])) {
				Warn("Invalid argument to "+WarnMess+"\nFile '"+line[1]+"' is not part of this plugin");
				return;
			}
			long offset;
			byte[] data=null;
			if(!long.TryParse(line[2], out offset)||offset<0) {
				Warn("Invalid argument to "+WarnMess+"\nOffset '"+line[2]+"' is not valid");
				return;
			}
			if(type==typeof(byte)) {
				byte value;
				if(!byte.TryParse(line[3], out value)) {
					Warn("Invalid argument to "+WarnMess+"\nValue '"+line[3]+"' is not valid");
					return;
				}
				data=BitConverter.GetBytes(value);
			}
			if(type==typeof(short)) {
				short value;
				if(!short.TryParse(line[3], out value)) {
					Warn("Invalid argument to "+WarnMess+"\nValue '"+line[3]+"' is not valid");
					return;
				}
				data=BitConverter.GetBytes(value);
			}
			if(type==typeof(int)) {
				int value;
				if(!int.TryParse(line[3], out value)) {
					Warn("Invalid argument to "+WarnMess+"\nValue '"+line[3]+"' is not valid");
					return;
				}
				data=BitConverter.GetBytes(value);
			}
			if(type==typeof(long)) {
				long value;
				if(!long.TryParse(line[3], out value)) {
					Warn("Invalid argument to "+WarnMess+"\nValue '"+line[3]+"' is not valid");
					return;
				}
				data=BitConverter.GetBytes(value);
			}
			if(type==typeof(float)) {
				float value;
				if(!float.TryParse(line[3], out value)) {
					Warn("Invalid argument to "+WarnMess+"\nValue '"+line[3]+"' is not valid");
					return;
				}
				data=BitConverter.GetBytes(value);
			}
			System.IO.FileStream fs=File.OpenWrite(Path.Combine(Plugins,line[1]));
			if(offset+data.Length>=fs.Length) {
				Warn("Invalid argument to "+WarnMess+"\nOffset '"+line[2]+"' is out of range");
				fs.Close();
				return;
			}
			fs.Position=offset;
			fs.Write(data, 0, data.Length);
			fs.Close();
		}

		private static void FunctionDisplayFile(string[] line, bool Image) {
			string WarnMess;
			if(Image) WarnMess="function 'DisplayImage'"; else WarnMess="function 'DisplayText'";
			if(line.Length<2) {
				Warn("Missing arguments to "+WarnMess);
				return;
			}
			if(line.Length>3) Warn("Unexpected extra arguments to "+WarnMess);
			if(!Program.IsSafeFileName(line[1])) {
				Warn("Illegal path supplied to "+WarnMess);
				return;
			}
			if(!File.Exists(Path.Combine(DataFiles,line[1]))) {
				Warn("Non-existant file '"+line[1]+"' supplied to "+WarnMess);
				return;
			}
			if(Image) {
				System.Drawing.Image image=System.Drawing.Image.FromFile(Path.Combine(DataFiles,line[1]));
				new ImageForm(image, (line.Length>2)?line[2]:line[1]).ShowDialog();
				image.Dispose();
			} else {
				string s=File.ReadAllText(Path.Combine(DataFiles,line[1]), System.Text.Encoding.Default);
				new TextEditor((line.Length>2)?line[2]:line[1], s, true, false).ShowDialog();
			}
		}

		private static void FunctionSetVar(string[] line) {
			if(line.Length<3) {
				Warn("Missing arguments to function SetVar");
				return;
			}
			if(line.Length>3) Warn("Unexpected extra arguments to function SetVar");
			variables[line[1]]=line[2];
		}
		
		#region "INI"
		/*
		static ConfigList currentINI = null;
		static ConfigPair currentSection = null;
		static ConfigPair currentKey = null;
		
		public static bool SectionSafe
		{
			get
			{
				return (currentINI != null) && (currentSection != null);
			}
		}
		public static bool IniSafe
		{
			get
			{
				return (currentINI != null);
			}
		}
		public static bool KeySafe
		{
			get
			{
				return (currentINI != null) && (currentSection != null) && (currentKey != null);
			}
		}
		public static void ResetINI()
		{
			currentINI = null;
			currentSection = null;
			currentKey = null;
		}
		public static void FunctionLoadINI(string[] line)
		{
			if (line.Length < 2)
			{
				Warn("USAGE: LoadINI FileName");
			}
			else
			{
				ResetINI();
				currentINI = new IniConfig().LoadConfiguration(RealName(line[1]));
			}
		}
		public static void FunctionSaveINI(string[] line)
		{
			if (line.Length < 2)
			{
				Warn("USAGE: SaveINI FileName");
			}
			else
			{
				new IniConfig().SaveConfiguration(RealName(line[1]), currentINI);
			}
		}
		public static void FunctionNewINI(string[] line)
		{
			ResetINI();
			currentINI = new ConfigList();
		}
		public static void FunctionResetINI(string[] line)
		{
			ResetINI();
		}
		public static void FunctionSelectKey(string[] line)
		{
			if (line.Length < 2)
			{
				Warn("USAGE: SelectKey name");
			}
			else
			{
				if (SectionSafe)
				{
					currentKey = currentSection.DataAsList.GetPair(new SV(line[1], false));
				}
				else
				{
					Warn("A section must be selected before a key can be selected");
				}
			}
		}
		public static void FunctionSelectSection(string[] line)
		{
			if (line.Length < 2)
			{
				Warn("USAGE: SelectSection name");
			}
			else
			{
				if (IniSafe)
				{
					currentSection = currentINI;
				}
				else
				{
					Warn("No ini is active");
				}
			}
		}
		public static void FunctionDeleteSection(string[] line)
		{
			if (line.Length < 2)
			{
				Warn("USAGE: DeleteSection name");
			}
			else
			{
				if (IniSafe)
				{
					currentINI.RemoveKey(new SV(line[1], false));
				}
				else
				{
					Warn("No ini is active");
				}
			}
		}
		public static void FunctionDeleteKey(string[] line)
		{
			if (line.Length < 2)
			{
				Warn("USAGE: DeleteKey name");
			}
			else
			{
				if (SectionSafe)
				{
					currentSection.DataAsList.RemoveKey(new SV(line[1], false));
				}
				else
				{
					Warn("A section must be selected before a key can be deleted");
				}
			}
		}
		public static void FunctionRenameSection(string[] line)
		{
			if (line.Length < 2)
			{
				Warn("USAGE: RenameSection name");
			}
			else
			{
				if (IniSafe)
				{
					currentSection.Key = line[1];
				}
			}
		}
		
		public static string RealName(string ininame)
		{
			if (string.Compare(ininame, "oblivion", true) == 0)
			{
				return OblivionINIDir + @"\Oblivion.ini";
			}
			else if (string.Compare(ininame, "rendererinfo") == 0)
			{
				return OblivionINIDir + @"\RendererInfo.txt";
			}
			else
			{
				return BaseTools.Helpers.IOHelper.RelativeTo("Data", ininame);
			}
		}
		*/
		#endregion
		
		public static void FunctionLetVar(string[] line) {
			if(line.Length<4) {
				Warn("Missing arguments to function Let");
				return;
			}
			if (line[2] == "=" || line[2] == ":=" || line[2] == "=>")
			{
				variables[line[1]]=line[3];
			}
			else if (line[2] == "=:" || line[2] == "<=")
			{
				variables[line[3]]=line[1];
			}
			else
			{
				Warn("Unknown symbol '" + line[2] + "', expected =, =:, => or :=, <=");
				return;
			}
			
			if(line.Length > 4)
				Warn("Unexpected extra arguments to function Let");
		}

		private static void FunctionGetDirectoryName(string[] line) {
			if(line.Length<3) {
				Warn("Missing arguments to GetDirectoryName");
				return;
			}
			if(line.Length>3) Warn("Unexpected arguments to GetDirectoryName");
			try {
				variables[line[1]]=Path.GetDirectoryName(line[2]);
			} catch {
				Warn("Invalid argument to GetDirectoryName");
			}
		}

		private static void FunctionGetFileName(string[] line) {
			if(line.Length<3) {
				Warn("Missing arguments to GetFileName");
				return;
			}
			if(line.Length>3) Warn("Unexpected arguments to GetFileName");
			try {
				variables[line[1]]=Path.GetFileName(line[2]);
			} catch {
				Warn("Invalid argument to GetFileName");
			}
		}

		private static void FunctionGetFileNameWithoutExtension(string[] line) {
			if(line.Length<3) {
				Warn("Missing arguments to GetFileNameWithoutExtension");
				return;
			}
			if(line.Length>3) Warn("Unexpected arguments to GetFileNameWithoutExtension");
			try {
				variables[line[1]]=Path.GetFileNameWithoutExtension(line[2]);
			} catch {
				Warn("Invalid argument to GetFileNameWithoutExtension");
			}
		}

		private static void FunctionCombinePaths(string[] line) {
			if(line.Length<4) {
				Warn("Missing arguments to CombinePaths");
				return;
			}
			if(line.Length>4) Warn("Unexpected arguments to CombinePaths");
			try {
				variables[line[1]]=Path.Combine(line[2], line[3]);
			} catch {
				Warn("Invalid argument to CombinePaths");
			}
		}

		private static void FunctionSubstring(string[] line) {
			if(line.Length<4) {
				Warn("Missing arguments to Substring");
				return;
			}
			if(line.Length>5) Warn("Unexpected extra arguments to Substring");
			if(line.Length==4) {
				int start;
				if(!int.TryParse(line[3], out start)) {
					Warn("Invalid argument to Substring");
					return;
				}
				variables[line[1]]=line[2].Substring(start);
			} else {
				int start, end;
				if(!int.TryParse(line[3], out start)||!int.TryParse(line[4], out end)) {
					Warn("Invalid argument to Substring");
					return;
				}
				variables[line[1]]=line[2].Substring(start, end);
			}
		}

		private static void FunctionRemoveString(string[] line) {
			if(line.Length<4) {
				Warn("Missing arguments to RemoveString");
				return;
			}
			if(line.Length>5) Warn("Unexpected extra arguments to RemoveString");
			if(line.Length==4) {
				int start;
				if(!int.TryParse(line[3], out start)) {
					Warn("Invalid argument to RemoveString");
					return;
				}
				variables[line[1]]=line[2].Remove(start);
			} else {
				int start, end;
				if(!int.TryParse(line[3], out start)||!int.TryParse(line[4], out end)) {
					Warn("Invalid argument to RemoveString");
					return;
				}
				variables[line[1]]=line[2].Remove(start, end);
			}
		}

		private static void FunctionStringLength(string[] line) {
			if(line.Length<3) {
				Warn("Missing arguments to StringLength");
				return;
			}
			if(line.Length>3) Warn("Unexpected extra arguments to StringLength");
			variables[line[1]]=line[2].Length.ToString();
		}

		private static void FunctionInputString(string[] line) {
			if(line.Length<2) {
				Warn("Missing arguments to InputString");
				return;
			}
			if(line.Length>4) Warn("Unexpected arguments to InputString");
			string title="";
			string initial="";
			if(line.Length>2) title=line[2];
			if(line.Length>3) initial=line[3];
			TextEditor te=new TextEditor(title, initial, false, true);
			te.ShowDialog();
			if(te.DialogResult!=DialogResult.Yes) variables[line[1]]="";
			else variables[line[1]]=te.Result;
		}

		private static void FunctionReadINI(string[] line) {
			if(line.Length<4) {
				Warn("Missing arguments to function ReadINI");
				return;
			}
			if(line.Length>4) Warn("Unexpected extra arguments to function ReadINI");
			try {
				variables[line[1]]=INI.GetINIValue(line[2], line[3]);
			} catch(Exception e) { variables[line[1]]=e.Message; }
		}

		private static void FunctionReadRenderer(string[] line) {
			if(line.Length<3) {
				Warn("Missing arguments to function 'ReadRendererInfo'");
				return;
			}
			if(line.Length>3) Warn("Unexpected extra arguments to function 'ReadRendererInfo'");
			try {
				variables[line[1]]=OblivionRenderInfo.GetInfo(line[2]);
			} catch(Exception e) { variables[line[1]]=e.Message; }
		}

		private static void FunctionEditXMLLine(string[] line) {
			if(line.Length<4) {
				Warn("Missing arguments to function 'EditXMLLine'");
				return;
			}
			if(line.Length>4) Warn("Unexpected extra arguments to function 'EditXMLLine'");
			line[1]=line[1].ToLower();
			if(!Program.IsSafeFileName(line[1])||!File.Exists(Path.Combine(DataFiles,line[1]))) {
				Warn("Invalid filename supplied to function 'EditXMLLine'");
				return;
			}
			string ext=Path.GetExtension(line[1]);
			if(ext!=".xml"&&ext!=".txt"&&ext!=".ini"&&ext!=".bat") {
				Warn("Invalid filename supplied to function 'EditXMLLine'");
				return;
			}
			int index;
			if(!int.TryParse(line[2], out index)||index<1) {
				Warn("Invalid line number supplied to function 'EditXMLLine'");
				return;
			}
			index-=1;
			string[] lines=File.ReadAllLines(Path.Combine(DataFiles,line[1]));
			if(lines.Length<=index) {
				Warn("Invalid line number supplied to function 'EditXMLLine'");
				return;
			}
			lines[index]=line[3];
			File.WriteAllLines(Path.Combine(DataFiles,line[1]), lines);
		}

		private static void FunctionEditXMLReplace(string[] line) {
			if(line.Length<4) {
				Warn("Missing arguments to function 'EditXMLReplace'");
				return;
			}
			if(line.Length>4) Warn("Unexpected extra arguments to function 'EditXMLReplace'");
			line[1]=line[1].ToLower();
			if(!Program.IsSafeFileName(line[1])||!File.Exists(Path.Combine(DataFiles,line[1]))) {
				Warn("Invalid filename supplied to function 'EditXMLReplace'");
				return;
			}
			string ext=Path.GetExtension(line[1]);
			if(ext!=".xml"&&ext!=".txt"&&ext!=".ini"&&ext!=".bat") {
				Warn("Invalid filename supplied to function 'EditXMLLine'");
				return;
			}
			/*bool Recurse=true;
            if(line.Length==5) {
                switch(line[4]) {
                case "True": break;
                case "False": Recurse=false; break;
                default: Warn("Invalid argument supplied to function 'EditXMLReplace'"); break;
                }
            }*/
			string text=File.ReadAllText(Path.Combine(DataFiles,line[1]));
			text=text.Replace(line[2], line[3]);
			File.WriteAllText(Path.Combine(DataFiles,line[1]), text);
		}

		private static void FunctionExecLines(string[] line, Queue<string> queue) {
			if(line.Length<2) {
				Warn("Missing arguments to function 'ExecLines'");
				return;
			}
			if(line.Length>2) Warn("Unexpected extra arguments to function 'ExecLines'");
			string[] lines=line[1].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			foreach(string s in lines) queue.Enqueue(s);
		}

		private static int iSet(List<string> func) {
			if(func.Count==0) throw new obmmException("Empty iSet");
			if(func.Count==1) return int.Parse(func[0]);
			//check for brackets
			int index;

			index=func.IndexOf("(");
			while(index!=-1) {
				int count=1;
				List<string> newfunc=new List<string>();
				for(int i=index+1;i<func.Count;i++) {
					if(func[i]=="(") count++;
					else if(func[i]==")") count--;
					if(count==0) {
						func.RemoveRange(index, (i-index)+1);
						func.Insert(index, iSet(newfunc).ToString());
						break;
					} else newfunc.Add(func[i]);
				}
				if(count!=0) throw new obmmException("Mismatched brackets");
				index=func.IndexOf("(");
			}

			//not
			index=func.IndexOf("not");
			while(index!=-1) {
				int i=int.Parse(func[index+1]);
				i=~i;
				func[index+1]=i.ToString();
				func.RemoveAt(index);
				index=func.IndexOf("not");
			}

			//and
			index=func.IndexOf("not");
			while(index!=-1) {
				int i=int.Parse(func[index-1]) & int.Parse(func[index+1]);
				func[index+1]=i.ToString();
				func.RemoveRange(index-1, 2);
				index=func.IndexOf("not");
			}

			//or
			index=func.IndexOf("or");
			while(index!=-1) {
				int i=int.Parse(func[index-1]) | int.Parse(func[index+1]);
				func[index+1]=i.ToString();
				func.RemoveRange(index-1, 2);
				index=func.IndexOf("or");
			}

			//xor
			index=func.IndexOf("xor");
			while(index!=-1) {
				int i=int.Parse(func[index-1]) ^ int.Parse(func[index+1]);
				func[index+1]=i.ToString();
				func.RemoveRange(index-1, 2);
				index=func.IndexOf("xor");
			}

			//mod
			index=func.IndexOf("mod");
			while(index!=-1) {
				int i=int.Parse(func[index-1]) % int.Parse(func[index+1]);
				func[index+1]=i.ToString();
				func.RemoveRange(index-1, 2);
				index=func.IndexOf("mod");
			}

			//mod
			index=func.IndexOf("%");
			while(index!=-1) {
				int i=int.Parse(func[index-1]) % int.Parse(func[index+1]);
				func[index+1]=i.ToString();
				func.RemoveRange(index-1, 2);
				index=func.IndexOf("%");
			}

			//power
			index=func.IndexOf("^");
			while(index!=-1) {
				int i=(int)Math.Pow(int.Parse(func[index-1]), int.Parse(func[index+1]));
				func[index+1]=i.ToString();
				func.RemoveRange(index-1, 2);
				index=func.IndexOf("^");
			}

			//division
			index=func.IndexOf("/");
			while(index!=-1) {
				int i=int.Parse(func[index-1]) / int.Parse(func[index+1]);
				func[index+1]=i.ToString();
				func.RemoveRange(index-1, 2);
				index=func.IndexOf("/");
			}

			//multiplication
			index=func.IndexOf("*");
			while(index!=-1) {
				int i=int.Parse(func[index-1]) * int.Parse(func[index+1]);
				func[index+1]=i.ToString();
				func.RemoveRange(index-1, 2);
				index=func.IndexOf("*");
			}

			//add
			index=func.IndexOf("+");
			while(index!=-1) {
				int i=int.Parse(func[index-1]) + int.Parse(func[index+1]);
				func[index+1]=i.ToString();
				func.RemoveRange(index-1, 2);
				index=func.IndexOf("+");
			}

			//sub
			index=func.IndexOf("-");
			while(index!=-1) {
				int i=int.Parse(func[index-1]) - int.Parse(func[index+1]);
				func[index+1]=i.ToString();
				func.RemoveRange(index-1, 2);
				index=func.IndexOf("-");
			}

			if(func.Count!=1) throw new obmmException("leftovers in function");
			return int.Parse(func[0]);
		}
		private static double fSet(List<string> func) {
			if(func.Count==0) throw new obmmException("Empty iSet");
			if(func.Count==1) return int.Parse(func[0]);
			//check for brackets
			int index;

			index=func.IndexOf("(");
			while(index!=-1) {
				int count=1;
				List<string> newfunc=new List<string>();
				for(int i=index;i<func.Count;i++) {
					if(func[i]=="(") count++;
					else if(func[i]==")") count--;
					if(count==0) {
						func.RemoveRange(index, i-index);
						func.Insert(index, fSet(newfunc).ToString());
						break;
					} else newfunc.Add(func[i]);
				}
				if(count!=0) throw new obmmException("Mismatched brackets");
				index=func.IndexOf("(");
			}

			//sin
			index=func.IndexOf("sin");
			while(index!=-1) {
				func[index+1]=Math.Sin(double.Parse(func[index+1])).ToString();
				func.RemoveAt(index);
				index=func.IndexOf("sin");
			}

			//cos
			index=func.IndexOf("cos");
			while(index!=-1) {
				func[index+1]=Math.Cos(double.Parse(func[index+1])).ToString();
				func.RemoveAt(index);
				index=func.IndexOf("cos");
			}

			//tan
			index=func.IndexOf("tan");
			while(index!=-1) {
				func[index+1]=Math.Tan(double.Parse(func[index+1])).ToString();
				func.RemoveAt(index);
				index=func.IndexOf("tan");
			}

			//sinh
			index=func.IndexOf("sinh");
			while(index!=-1) {
				func[index+1]=Math.Sinh(double.Parse(func[index+1])).ToString();
				func.RemoveAt(index);
				index=func.IndexOf("sinh");
			}

			//cosh
			index=func.IndexOf("cosh");
			while(index!=-1) {
				func[index+1]=Math.Cosh(double.Parse(func[index+1])).ToString();
				func.RemoveAt(index);
				index=func.IndexOf("cosh");
			}

			//tanh
			index=func.IndexOf("tanh");
			while(index!=-1) {
				func[index+1]=Math.Tanh(double.Parse(func[index+1])).ToString();
				func.RemoveAt(index);
				index=func.IndexOf("tanh");
			}

			//exp
			index=func.IndexOf("exp");
			while(index!=-1) {
				func[index+1]=Math.Exp(double.Parse(func[index+1])).ToString();
				func.RemoveAt(index);
				index=func.IndexOf("exp");
			}

			//log
			index=func.IndexOf("log");
			while(index!=-1) {
				func[index+1]=Math.Log10(double.Parse(func[index+1])).ToString();
				func.RemoveAt(index);
				index=func.IndexOf("log");
			}

			//ln
			index=func.IndexOf("ln");
			while(index!=-1) {
				func[index+1]=Math.Log(double.Parse(func[index+1])).ToString();
				func.RemoveAt(index);
				index=func.IndexOf("ln");
			}

			//mod
			index=func.IndexOf("mod");
			while(index!=-1) {
				double i=double.Parse(func[index-1]) % double.Parse(func[index+1]);
				func[index+1]=i.ToString();
				func.RemoveRange(index-1, 2);
				index=func.IndexOf("mod");
			}

			//mod2
			index=func.IndexOf("%");
			while(index!=-1) {
				double i=double.Parse(func[index-1]) % double.Parse(func[index+1]);
				func[index+1]=i.ToString();
				func.RemoveRange(index-1, 2);
				index=func.IndexOf("%");
			}

			//power
			index=func.IndexOf("^");
			while(index!=-1) {
				double i=Math.Pow(double.Parse(func[index-1]), double.Parse(func[index+1]));
				func[index+1]=i.ToString();
				func.RemoveRange(index-1, 2);
				index=func.IndexOf("^");
			}

			//division
			index=func.IndexOf("/");
			while(index!=-1) {
				double i=double.Parse(func[index-1]) / double.Parse(func[index+1]);
				func[index+1]=i.ToString();
				func.RemoveRange(index-1, 2);
				index=func.IndexOf("/");
			}

			//multiplication
			index=func.IndexOf("*");
			while(index!=-1) {
				double i=double.Parse(func[index-1]) * double.Parse(func[index+1]);
				func[index+1]=i.ToString();
				func.RemoveRange(index-1, 2);
				index=func.IndexOf("*");
			}

			//add
			index=func.IndexOf("+");
			while(index!=-1) {
				double i=double.Parse(func[index-1]) + double.Parse(func[index+1]);
				func[index+1]=i.ToString();
				func.RemoveRange(index-1, 2);
				index=func.IndexOf("+");
			}

			//sub
			index=func.IndexOf("-");
			while(index!=-1) {
				double i=double.Parse(func[index-1]) - double.Parse(func[index+1]);
				func[index+1]=i.ToString();
				func.RemoveRange(index-1, 2);
				index=func.IndexOf("-");
			}

			if(func.Count!=1) throw new obmmException("leftovers in function");
			return double.Parse(func[0]);
		}
		private static void FunctionSet(string[] line, bool integer) {
			if(line.Length<3) {
				Warn("Missing arguments to function "+(integer?"iSet":"fSet"));
				return;
			}
			List<string> func=new List<string>();
			for(int i=2;i<line.Length;i++) func.Add(line[i]);
			string result;
			try {
				if(integer) {
					int i=iSet(func);
					result=i.ToString();
				} else {
					float f=(float)fSet(func);
					result=f.ToString();
				}
				variables[line[1]]=result;
			} catch {
				Warn("Invalid arguments to function "+(integer?"iSet":"fSet"));
			}
		}

		public static ScriptReturnData Execute(string InputScript, string DataPath, string PluginsPath) {
			srd=new ScriptReturnData();
			if(InputScript==null) return srd;

			DataFiles=DataPath;
			Plugins=PluginsPath;
			variables=new Dictionary<string, string>();
			Stack<FlowControlStruct> FlowControl=new Stack<FlowControlStruct>();
			Queue<string> ExtraLines= new Queue<string>();
			variables["NewLine"]=Environment.NewLine;
			variables["Tab"]="\t";
			string[] script=InputScript.Replace("\r", "").Split('\n');
			string[] line;
			string s;
			bool AllowRunOnLines=false;
			string SkipTo=null;
			bool Break=false;
			for(int i=0;i<script.Length||ExtraLines.Count>0;i++) {
				if(ExtraLines.Count>0) {
					i--;
					s=ExtraLines.Dequeue().Replace('\t', ' ').Trim();
				} else {
					s=script[i].Replace('\t', ' ').Trim();
				}
				cLine=i.ToString();
				if(AllowRunOnLines) {
					while(s.EndsWith("\\")) {
						s=s.Remove(s.Length-1);
						if(ExtraLines.Count>0) {
							s+=ExtraLines.Dequeue().Replace('\t', ' ').Trim();
						} else {
							if(++i==script.Length) Warn("Run-on line passed end of script");
							else s+=script[i].Replace('\t', ' ').Trim();
						}
					}
				}

				if(SkipTo!=null) {
					if(s==SkipTo) SkipTo=null;
					else continue;
				}

				line=SplitLine(s);
				if(line.Length==0) continue;

				if(FlowControl.Count!=0&&!FlowControl.Peek().active) {
					switch(line[0]) {
						case "":
							Warn("Empty function");
							break;
						case "If":
						case "IfNot":
							FlowControl.Push(new FlowControlStruct(0));
							break;
						case "Else":
							if(FlowControl.Count!=0&&FlowControl.Peek().type==0) {
								FlowControl.Peek().active=FlowControl.Peek().line!=-1;
							} else Warn("Unexpected Else");
							break;
						case "EndIf":
							if(FlowControl.Count!=0&&FlowControl.Peek().type==0) FlowControl.Pop();
							else Warn("Unexpected EndIf");
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
							FlowControl.Push(new FlowControlStruct(1));
							break;
						case "Case":
							if(FlowControl.Count!=0&&FlowControl.Peek().type==1) {
								if(FlowControl.Peek().line!=-1&&Array.IndexOf<string>(FlowControl.Peek().values, s)!=-1) {
									FlowControl.Peek().active=true;
									FlowControl.Peek().hitCase=true;
								}
							} else Warn("Unexpected Break");
							break;
						case "Default":
							if(FlowControl.Count!=0&&FlowControl.Peek().type==1) {
								if(FlowControl.Peek().line!=-1&&!FlowControl.Peek().hitCase) FlowControl.Peek().active=true;
							} else Warn("Unexpected Default");
							break;
						case "EndSelect":
							if(FlowControl.Count!=0&&FlowControl.Peek().type==1) FlowControl.Pop();
							else Warn("Unexpected EndSelect");
							break;
						case "For":
							FlowControl.Push(new FlowControlStruct(2));
							break;
						case "EndFor":
							if(FlowControl.Count!=0&&FlowControl.Peek().type==2) FlowControl.Pop();
							else Warn("Unexpected EndFor");
							break;
						case "Break":
						case "Continue":
						case "Exit":
							break;
					}
				} else {
					switch(line[0]) {
						case "":
							Warn("Empty function");
							break;
							//Control structures
						case "Goto":
							if(line.Length<2) {
								Warn("Not enough arguments to function 'Goto'");
							} else {
								if(line.Length>2) Warn("Unexpected extra arguments to function 'Goto'");
								SkipTo="Label "+line[1];
								FlowControl.Clear();
							}
							break;
						case "Label":
							break;
						case "If":
							FlowControl.Push(new FlowControlStruct(i, FunctionIf(line)));
							break;
						case "IfNot":
							FlowControl.Push(new FlowControlStruct(i, !FunctionIf(line)));
							break;
						case "Else":
							if(FlowControl.Count!=0&&FlowControl.Peek().type==0) FlowControl.Peek().active=false;
							else Warn("Unexpected Else");
							break;
						case "EndIf":
							if(FlowControl.Count!=0&&FlowControl.Peek().type==0) FlowControl.Pop();
							else Warn("Unexpected EndIf");
							break;
						case "Select":
							FlowControl.Push(new FlowControlStruct(i, FunctionSelect(line, false, false, false)));
							break;
						case "SelectMany":
							FlowControl.Push(new FlowControlStruct(i, FunctionSelect(line, true, false, false)));
							break;
						case "SelectWithPreview":
							FlowControl.Push(new FlowControlStruct(i, FunctionSelect(line, false, true, false)));
							break;
						case "SelectManyWithPreview":
							FlowControl.Push(new FlowControlStruct(i, FunctionSelect(line, true, true, false)));
							break;
						case "SelectWithDescriptions":
							FlowControl.Push(new FlowControlStruct(i, FunctionSelect(line, false, false, true)));
							break;
						case "SelectManyWithDescriptions":
							FlowControl.Push(new FlowControlStruct(i, FunctionSelect(line, true, false, true)));
							break;
						case "SelectWithDescriptionsAndPreviews":
							FlowControl.Push(new FlowControlStruct(i, FunctionSelect(line, false, true, true)));
							break;
						case "SelectManyWithDescriptionsAndPreviews":
							FlowControl.Push(new FlowControlStruct(i, FunctionSelect(line, true, true, true)));
							break;
						case "SelectVar":
							FlowControl.Push(new FlowControlStruct(i, FunctionSelectVar(line, true)));
							break;
						case "SelectString":
							FlowControl.Push(new FlowControlStruct(i, FunctionSelectVar(line, false)));
							break;
							case "Break": {
								bool found=false;
								FlowControlStruct[] fcs=FlowControl.ToArray();
								for(int k=0;k<fcs.Length;k++) {
									if(fcs[k].type==1) {
										for(int j=0;j<=k;j++) fcs[j].active=false;
										found=true;
										break;
									}
								}
								if(!found) Warn("Unexpected Break");
								break;
							}
						case "Case":
							if(FlowControl.Count==0||FlowControl.Peek().type!=1) Warn("Unexpected Case");
							break;
						case "Default":
							if(FlowControl.Count==0||FlowControl.Peek().type!=1) Warn("Unexpected Default");
							break;
						case "EndSelect":
							if(FlowControl.Count!=0&&FlowControl.Peek().type==1) FlowControl.Pop();
							else Warn("Unexpected EndSelect");
							break;
							case "For": {
								FlowControlStruct fc=FunctionFor(line, i);
								FlowControl.Push(fc);
								if(fc.line!=-1&&fc.values.Length>0) {
									variables[fc.var]=fc.values[0];
									fc.active=true;
								}
								break;
							}
							case "Continue": {
								bool found=false;
								FlowControlStruct[] fcs=FlowControl.ToArray();
								for(int k=0;k<fcs.Length;k++) {
									if(fcs[k].type==2) {
										fcs[k].forCount++;
										if(fcs[k].forCount==fcs[k].values.Length) {
											for(int j=0;j<=k;j++) fcs[j].active=false;
										} else {
											i=fcs[k].line;
											variables[fcs[k].var]=fcs[k].values[fcs[k].forCount];
											for(int j=0;j<k;j++) FlowControl.Pop();
										}
										found=true;
										break;
									}
								}
								if(!found) Warn("Unexpected Continue");
								break;
							}
							case "Exit": {
								bool found=false;
								FlowControlStruct[] fcs=FlowControl.ToArray();
								for(int k=0;k<fcs.Length;k++) {
									if(fcs[k].type==2) {
										for(int j=0;j<=k;j++) FlowControl.Peek().active=false;
										found=true;
										break;
									}
								}
								if(!found) Warn("Unexpected Exit");
								break;
							}
						case "EndFor":
							if(FlowControl.Count!=0&&FlowControl.Peek().type==2) {
								FlowControlStruct fc=FlowControl.Peek();
								fc.forCount++;
								if(fc.forCount==fc.values.Length) FlowControl.Pop();
								else {
									i=fc.line;
									variables[fc.var]=fc.values[fc.forCount];
								}
							} else Warn("Unexpected EndFor");
							break;
							//Functions
						case "Message":
							FunctionMessage(line);
							break;
						case "SelectNumber":
							FunctionSelectNumber(line);
							break;
						case "LoadEarly":
							FunctionLoadEarly(line);
							break;
						case "LoadBefore":
							FunctionLoadOrder(line, false);
							break;
						case "LoadAfter":
							FunctionLoadOrder(line, true);
							break;
						case "ConflictsWith":
							FunctionConflicts(line, true, false);
							break;
						case "DependsOn":
							FunctionConflicts(line, false, false);
							break;
						case "ConflictsWithRegex":
							FunctionConflicts(line, true, true);
							break;
						case "DependsOnRegex":
							FunctionConflicts(line, false, true);
							break;
						case "DontInstallAnyPlugins":
							srd.InstallAllPlugins=false;
							break;
						case "DontInstallAnyDataFiles":
							srd.InstallAllData=false;
							break;
						case "InstallAllPlugins":
							srd.InstallAllPlugins=true;
							break;
						case "InstallAllDataFiles":
							srd.InstallAllData=true;
							break;
						case "InstallPlugin":
							FunctionModifyInstall(line, true, true);
							break;
						case "DontInstallPlugin":
							FunctionModifyInstall(line, true, false);
							break;
						case "InstallDataFile":
							FunctionModifyInstall(line, false, true);
							break;
						case "DontInstallDataFile":
							FunctionModifyInstall(line, false, false);
							break;
						case "DontInstallDataFolder":
							FunctionModifyInstallFolder(line, false);
							break;
						case "InstallDataFolder":
							FunctionModifyInstallFolder(line, true);
							break;
						case "RegisterBSA":
							FunctionRegisterBSA(line, true);
							break;
						case "UnregisterBSA":
							FunctionRegisterBSA(line, false);
							break;
						case "FatalError":
							srd.CancelInstall=true;
							break;
						case "Return":
							Break=true;
							break;
						case "UncheckESP":
							FunctionUncheckESP(line);
							break;
						case "SetDeactivationWarning":
							FunctionSetDeactivationWarning(line);
							break;
						case "CopyDataFile":
							FunctionCopyDataFile(line, false);
							break;
						case "CopyPlugin":
							FunctionCopyDataFile(line, true);
							break;
						case "CopyDataFolder":
							FunctionCopyDataFolder(line);
							break;
						case "PatchPlugin":
							FunctionPatch(line, true);
							break;
						case "PatchDataFile":
							FunctionPatch(line, false);
							break;
						case "EditINI":
							FunctionEditINI(line);
							break;
						case "EditSDP":
						case "EditShader":
							FunctionEditShader(line);
							break;
						case "SetGMST":
							FunctionSetEspVar(line, true);
							break;
						case "SetGlobal":
							FunctionSetEspVar(line, false);
							break;
						case "SetPluginByte":
							FunctionSetEspData(line, typeof(byte));
							break;
						case "SetPluginShort":
							FunctionSetEspData(line, typeof(short));
							break;
						case "SetPluginInt":
							FunctionSetEspData(line, typeof(int));
							break;
						case "SetPluginLong":
							FunctionSetEspData(line, typeof(long));
							break;
						case "SetPluginFloat":
							FunctionSetEspData(line, typeof(float));
							break;
						case "DisplayImage":
							FunctionDisplayFile(line, true);
							break;
						case "DisplayText":
							FunctionDisplayFile(line, false);
							break;
						case "Let":
							FunctionLetVar(line);
							break;
						case "SetVar":
							FunctionSetVar(line);
							break;
						case "GetFolderName":
						case "GetDirectoryName":
							FunctionGetDirectoryName(line);
							break;
						case "GetFileName":
							FunctionGetFileName(line);
							break;
						case "GetFileNameWithoutExtension":
							FunctionGetFileNameWithoutExtension(line);
							break;
						case "CombinePaths":
							FunctionCombinePaths(line);
							break;
						case "Substring":
							FunctionSubstring(line);
							break;
						case "RemoveString":
							FunctionRemoveString(line);
							break;
						case "StringLength":
							FunctionStringLength(line);
							break;
						case "InputString":
							FunctionInputString(line);
							break;
						case "ReadINI":
							FunctionReadINI(line);
							break;
						case "ReadRendererInfo":
							FunctionReadRenderer(line);
							break;
						case "ExecLines":
							FunctionExecLines(line, ExtraLines);
							break;
						case "iSet":
							FunctionSet(line, true);
							break;
						case "fSet":
							FunctionSet(line, false);
							break;
						case "EditXMLLine":
							FunctionEditXMLLine(line);
							break;
						case "EditXMLReplace":
							FunctionEditXMLReplace(line);
							break;
						case "AllowRunOnLines":
							AllowRunOnLines=true;
							break;
						default:
							Warn("Unknown function '"+line[0]+"'");
							break;
					}
				}
				if(Program.KeyPressed((int)System.Windows.Forms.Keys.Escape)) srd.CancelInstall=true;
				if(Break||srd.CancelInstall) break;
			}
			if(SkipTo!=null) Warn("Expected "+SkipTo);
			//set srd to null so the garbage collector clears it up
			ScriptReturnData TempResult=srd;
			srd=null;
			variables=null;

			return TempResult;
		}
	}
}