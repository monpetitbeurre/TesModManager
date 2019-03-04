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
using System.Security.Permissions;
using MessageBox=System.Windows.Forms.MessageBox;
using mbButtons=System.Windows.Forms.MessageBoxButtons;
using DialogResult=System.Windows.Forms.DialogResult;
using File=System.IO.File;
using Directory=System.IO.Directory;
using Path=System.IO.Path;
using Regex=System.Text.RegularExpressions.Regex;
using RegexOptions=System.Text.RegularExpressions.RegexOptions;

namespace OblivionModManager.Scripting {

	public class ScriptFunctions : IScriptFunctions {
        private static ScriptReturnData srd = new ScriptReturnData();
        private static string[] dataFileList = new string[0];
        private static string[] pluginList = new string[0];
        private static System.Security.PermissionSet permissions = new System.Security.PermissionSet(PermissionState.None);
        private static string DataFiles = "";
        private static string Plugins="";
        private static string[] dataFolderList = new string[0];
        private static string[] pluginFolderList = new string[0];
        private static bool testMode=false;

        public ScriptReturnData getsrd() { return _getsrd(); }
        public static ScriptReturnData _getsrd()
        {
            return srd;
        }
        public void setsrd(ScriptReturnData _srd) { _setsrd(_srd); }
        public static void _setsrd(ScriptReturnData _srd)
        {
            srd=_srd;
        }
        public string getdataFilePath() { return _getdataFilePath(); }
        public static string _getdataFilePath()
        {
            return DataFiles;
        }
        public void setdataFilePath(string value) { _setdataFilePath(value); }
        public static void _setdataFilePath(string list)
        {
            DataFiles = list;
        }
        public string getpluginPath() { return _getpluginPath(); }
        public static string _getpluginPath()
        {
            return Plugins;
        }
        public void setpluginPath(string value) { _setpluginPath(value); }
        public static void _setpluginPath(string list)
        {
            Plugins = list;
        }
        public string[] getdataFileList() { return _getdataFileList(); }
        public static string[] _getdataFileList()
        {
            return dataFileList;
        }
        public void setdataFileList(string[] value) { _setdataFileList(value); }
        public static void _setdataFileList(string[] list)
        {
            dataFileList = (string[])list.Clone();
        }
        public string[] getpluginList() { return _getpluginList(); }
        public static string[] _getpluginList()
        {
            return pluginList;
        }
        public void setpluginList(string[] value) { _setpluginList(value); }
        public static void _setpluginList(string[] list)
        {
            pluginList = (string[])list.Clone();
        }

        public ScriptFunctions(ScriptReturnData Srd, string dataFilesPath, string pluginsPath)
        {
            _ScriptFunctions(Srd, dataFilesPath, pluginsPath);
        }
		public static void _ScriptFunctions(ScriptReturnData Srd, string dataFilesPath, string pluginsPath)
        {
            srd = Srd;
			DataFiles=dataFilesPath;
			Plugins=pluginsPath;
			permissions=new System.Security.PermissionSet(PermissionState.None);
			List<string> paths=new List<string>(4);
			paths.Add(Program.BaseDir);
			paths.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My games\\Oblivion"));
			if(dataFilesPath!=null) paths.Add(dataFilesPath);
			if(pluginsPath!=null) paths.Add(pluginsPath);
			permissions.AddPermission(new FileIOPermission(FileIOPermissionAccess.AllAccess, paths.ToArray()));
			permissions.AddPermission(new UIPermission(UIPermissionWindow.AllWindows));
            List<string> datafiles = new List<string>();
            List<string> finalDatafiles = new List<string>();
            if (dataFilesPath != null)
            {
                datafiles = new List<string>(Directory.GetFiles(dataFilesPath, "*.*", System.IO.SearchOption.AllDirectories));
            }

            // is it FOMOD based?
            foreach (string datafile in datafiles)
            {
                string lowerdatafile=datafile.ToLower();
                if (lowerdatafile.Contains("/fomod") || lowerdatafile.Contains("\\fomod"))
                {
                    dataFilesPath = datafile.Substring(0, lowerdatafile.IndexOf("fomod"));
                    pluginsPath = dataFilesPath;
                    DataFiles = dataFilesPath;
                    Plugins = pluginsPath;
                    break;
                }
            }
            for (int i = 0; i < datafiles.Count; i++)
            {
                if (datafiles[i].ToLower().EndsWith(".esp") || datafiles[i].ToLower().EndsWith(".esm") || datafiles[i].ToLower().EndsWith(".esl"))
                    continue;
                finalDatafiles.Add(datafiles[i].Replace(dataFilesPath, ""));
            }
            dataFileList = finalDatafiles.ToArray();

            if (!string.IsNullOrWhiteSpace(pluginsPath)) pluginList = Directory.GetFiles(pluginsPath, "*.es*", System.IO.SearchOption.AllDirectories);
            for (int i = 0; i < pluginList.Length && !string.IsNullOrWhiteSpace(pluginsPath); i++) pluginList[i] = pluginList[i].Replace(pluginsPath, "");
            testMode = false;
		}

		public ScriptFunctions(ScriptReturnData Srd, string[] dataFiles, string[] plugins)
        {
            _ScriptFunctions(Srd, dataFiles, plugins);
        }
        public static void _ScriptFunctions(ScriptReturnData Srd, string[] dataFiles, string[] plugins)
        {
            srd = Srd;
			dataFileList=(string[])dataFiles.Clone();
			pluginList=(string[])plugins.Clone();

			List<string> df = new List<string>();
			string dir;

			df.Add("");
			for(int i = 0;i < dataFileList.Length;++i) {
				dataFileList[i] = dataFileList[i].ToLower();
				dir = dataFileList[i];
				while(dir.Contains(@"\")) {
					dir = Path.GetDirectoryName(dir);
					if(dir != null && dir != "") {
						if(!df.Contains(dir)) df.Add(dir);
					} else break;
				}
			}
			dataFolderList=df.ToArray();

			df.Clear();
			df.Add("");
			for(int i = 0;i < pluginList.Length;++i) {
				pluginList[i] = pluginList[i].ToLower();
				dir = pluginList[i];
				while(dir.Contains(@"\")) {
					dir = Path.GetDirectoryName(dir);
					if(dir != null && dir != "") {
						if(!df.Contains(dir)) df.Add(dir);
					} else break;
				}
			}
			pluginFolderList=df.ToArray();

			string[] paths=new string[2];
			paths[0]=Program.BaseDir;
			paths[1]=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My games\\Oblivion");
			permissions=new System.Security.PermissionSet(PermissionState.None);
			permissions.AddPermission(new FileIOPermission(FileIOPermissionAccess.PathDiscovery|FileIOPermissionAccess.Read, paths));
			permissions.AddPermission(new UIPermission(UIPermissionWindow.AllWindows));
			testMode=true;
		}
		public MainForm ProgramForm
		{
			get
			{
				return Program.ProgramForm;
			}
		}
		private static bool ExistsIn(string path, string[] files) {
			if(files == null) return false;
			return Array.Exists<string>(files, new Predicate<string>(path.ToLower().Equals));
		}

        private static void CheckPathSafty(string path)
        {
			if(!Program.IsSafeFileName(path)) throw new ScriptingException("Illegal file name: '"+path+"'");
		}
        private static void CheckPluginSafty(string path)
        {
			permissions.Assert();
			if(!Program.IsSafeFileName(path)) throw new ScriptingException("Illegal file name: '"+path+"'");
            if (!(testMode ? ExistsIn(path, pluginList) : File.Exists(Path.Combine(Plugins,path))) &&
                !(testMode ? ExistsIn(path, dataFileList) : File.Exists(Path.Combine(DataFiles,path))))
            {
                List<string> strlist = new List<string>();
                foreach (ScriptCopyDataFile cp in srd.CopyPlugins)
                {
                    strlist.Add(cp.CopyTo);
                }
                foreach (ScriptCopyDataFile cp in srd.CopyDataFiles)
                {
                    strlist.Add(cp.CopyTo);
                }
                if (!ExistsIn(path, strlist.ToArray()))
                {
                    // throw new ScriptingException("File '" + path + "' not found");
                    Program.logger.WriteToLog("File '" + path + "' not found. Skipping... ", Logger.LogLevel.Warning);
                }
            }
		}
        private static void CheckDataSafety(string path)
        {
			permissions.Assert();
			if(!Program.IsSafeFileName(path)) throw new ScriptingException("Illegal file name: '"+path+"'");
			if(!(testMode?ExistsIn(path, dataFileList):File.Exists(Path.Combine(DataFiles,path)))) throw new ScriptingException("File '"+path+"' not found");
		}
        private static void CheckFolderSafety(string path)
        {
			if(!Program.IsSafeFolderName(path)) throw new ScriptingException("Illegal folder name: '"+path+"'");
		}
        private static void CheckPluginFolderSafety(string path)
        {
			permissions.Assert();
			if(path.EndsWith("\\")||path.EndsWith("/")) path=path.Remove(path.Length-1);
			if(!Program.IsSafeFolderName(path)) throw new ScriptingException("Illegal folder name: '"+path+"'");
			if(!(testMode?ExistsIn(path, pluginFolderList):Directory.Exists(Path.Combine(Plugins,path)))) throw new ScriptingException("Folder '"+path+"' not found");
		}
        private static void CheckDataFolderSafety(string path)
        {
			permissions.Assert();
			if(path.EndsWith("\\")||path.EndsWith("/")) path=path.Remove(path.Length-1);
			if(!Program.IsSafeFolderName(path)) throw new ScriptingException("Illegal folder name: '"+path+"'");
			if(!(testMode?ExistsIn(path, dataFolderList):Directory.Exists(Path.Combine(DataFiles,path)))) throw new ScriptingException("Folder '"+path+"' not found");
		}
        private static void CancelCheck() { if (Program.KeyPressed((int)System.Windows.Forms.Keys.Escape)) throw new ExecutionCancelledException(); }

        private static string[] SimulateFSOutput(string[] fsList, string path, string pattern, bool recurse)
        {
			pattern = "^" + (pattern == "" ? ".*" : pattern.Replace("[", @"\[").Replace(@"\", "\\").Replace("^", @"\^").Replace("$", @"\$").
			                 Replace("|", @"\|").Replace("+", @"\+").Replace("(", @"\(").Replace(")", @"\)").
			                 Replace(".", @"\.").Replace("*", ".*").Replace("?", ".{0,1}")) + "$";
			return Array.FindAll(fsList, delegate(string value)
			                     {
			                     	if((path.Length > 0 && value.StartsWith(path.ToLower() + @"\")) || path.Length == 0) {
			                     		if(value == "" || (!recurse && Regex.Matches(value.Substring(path.Length), @"\\", RegexOptions.None).Count > 1)) return false;
			                     		if(Regex.IsMatch(value.Substring(value.LastIndexOf('\\') + 1), pattern)) return true;
			                     	}
			                     	return false;
			                     });
		}
        private static string[] GetFilePaths(string path, string pattern, bool recurse)
        {
			permissions.Assert();
			return Directory.GetFiles(path, (pattern != ""&&pattern!=null) ? pattern : "*", recurse ? System.IO.SearchOption.AllDirectories : System.IO.SearchOption.TopDirectoryOnly);
		}
        private static string[] GetDirectoryPaths(string path, string pattern, bool recurse)
        {
			permissions.Assert();
			return Directory.GetDirectories(path, (pattern != ""&&pattern!=null) ? pattern : "*", recurse ? System.IO.SearchOption.AllDirectories : System.IO.SearchOption.TopDirectoryOnly);
		}
        private static string[] StripPathList(string[] paths, int baseLength)
        {
			for(int i=0;i<paths.Length;i++) if(Path.IsPathRooted(paths[i])) paths[i]=paths[i].Substring(baseLength);
			return paths;
		}

        public bool GetDisplayWarnings() { return _GetDisplayWarnings(); }
        public static bool _GetDisplayWarnings() { return Settings.ShowScriptWarnings; }

        public static string[] _GetModFileList() { return dataFileList; }
        public bool DialogYesNo(string msg) { return _DialogYesNo(msg, "Question"); }
        public bool DialogYesNo(string msg, string title) { return _DialogYesNo(msg, title); }
        public static bool _DialogYesNo(string msg, string title)
        {
			return MessageBox.Show(msg, title, System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.Yes;
		}
        public bool DataFileExists(string path) { return _DataFileExists(path); }
        public static bool _DataFileExists(string path)
        {
			CheckPathSafty(path);
			permissions.Assert();
			return File.Exists(Path.Combine(Program.currentGame.DataFolderPath,path));
		}
        public bool PerformBasicInstall() { return _PerformBasicInstall(); }
        public static bool _PerformBasicInstall()
        {
            // A basic install installs all of the file in the mod to the Data directory
            // or activates all esp and esm files.
            _InstallAllDataFiles();
            _InstallAllPlugins();
            return true;
        }
        public Version GetOBMMVersion() { return _GetOBMMVersion(); }
        public static Version _GetOBMMVersion()
        {
			return new Version(Program.version+".0"); // (Program.MajorVersion, Program.MinorVersion, Program.BuildNumber, 0);
		}
        public bool ScriptExtenderPresent() { return _ScriptExtenderPresent(); }
        public static bool _ScriptExtenderPresent()
        {
            return (_GetSKSEVersion() != null || _GetOBSEVersion() != null); ;
        }
        public Version GetSKSEVersion() { return _GetSKSEVersion(); }
        public static Version _GetSKSEVersion()
        {
            permissions.Assert();
            if (!File.Exists(Path.Combine(Program.currentGame.GamePath, "skse_loader.exe")) && !File.Exists(Path.Combine(Program.currentGame.GamePath, "skse_steam_loader.dll"))) return null;
            else return
                    new Version(System.Diagnostics.FileVersionInfo.GetVersionInfo(
                    File.Exists(Path.Combine(Program.currentGame.GamePath, "skse_loader.exe")) ? Path.Combine(Program.currentGame.GamePath, "skse_loader.exe") : Path.Combine(Program.currentGame.GamePath, "skse_steam_loader.dll")).FileVersion.Replace(", ", "."));
        }
        public Version GetMWSEVersion() { return _GetMWSEVersion(); }
        public static Version _GetMWSEVersion()
        {
            permissions.Assert();
            if (!File.Exists(Path.Combine(Program.currentGame.GamePath, "mwse.dll"))) return null;
            else return
                    new Version(System.Diagnostics.FileVersionInfo.GetVersionInfo(Path.Combine(Program.currentGame.GamePath, "mwse.dll")).FileVersion.Replace(", ", "."));
        }
        public Version GetOBSEVersion() { return _GetOBSEVersion(); }
        public static Version _GetOBSEVersion()
        {
			permissions.Assert();
			if(!File.Exists(Path.Combine(Program.currentGame.GamePath, "obse_loader.exe")) && !File.Exists(Path.Combine(Program.currentGame.GamePath, "obse_steam_loader.dll"))) return null;
			else return 
					new Version(System.Diagnostics.FileVersionInfo.GetVersionInfo(
					File.Exists(Path.Combine(Program.currentGame.GamePath, "obse_loader.exe")) ? Path.Combine(Program.currentGame.GamePath, "obse_loader.exe") : Path.Combine(Program.currentGame.GamePath, "obse_steam_loader.dll")).FileVersion.Replace(", ", "."));
		}
        public Version GetScriptExtenderVersion() { return _GetScriptExtenderVersion(); }
        public static Version _GetScriptExtenderVersion()
        {
			permissions.Assert();
            if (!File.Exists(Path.Combine(Program.currentGame.GamePath, Program.currentGame.ScriptExtenderExe)) &&
                !File.Exists(Path.Combine(Program.currentGame.GamePath, Program.currentGame.ScriptExtenderDLL)))
                return null;
            else
            {
                string extender = File.Exists(Path.Combine(Program.currentGame.GamePath, Program.currentGame.ScriptExtenderExe)) ? Program.currentGame.ScriptExtenderExe : Program.currentGame.ScriptExtenderDLL;
                Version version = null;
                try
                {
                    version = new Version(System.Diagnostics.FileVersionInfo.GetVersionInfo(Path.Combine(Program.currentGame.GamePath, extender)).FileVersion.Replace(", ", "."));
                }
                catch { }
                return version;
            }
        }
        public Version GetOBGEVersion() { return _GetOBGEVersion(); }
        public static Version _GetOBGEVersion()
        {
			permissions.Assert();
            if (!File.Exists(Path.Combine(Program.currentGame.DataFolderPath, "\\obse\\plugins\\obge.dll"))) return null;
            else return new Version(System.Diagnostics.FileVersionInfo.GetVersionInfo(Path.Combine(Program.currentGame.DataFolderPath, "\\obse\\plugins\\obge.dll")).FileVersion.Replace(", ", "."));
		}
        public Version GetOblivionVersion() { return _GetOblivionVersion(); }
        public static Version _GetOblivionVersion()
        {
			permissions.Assert();
			return new Version(System.Diagnostics.FileVersionInfo.GetVersionInfo(Path.Combine(Program.currentGame.GamePath, "oblivion.exe")).FileVersion.Replace(", ", "."));
		}
        public Version GetOBSEPluginVersion(string plugin) { return _GetOBSEPluginVersion(plugin); }
        public static Version _GetOBSEPluginVersion(string plugin)
        {
            plugin = Path.ChangeExtension(Path.Combine(Path.Combine(Program.currentGame.DataFolderPath, "\\obse\\plugins"), plugin), ".dll");
			CheckPathSafty(plugin);
			permissions.Assert();
			if(!File.Exists(plugin)) return null;
			else return new Version(System.Diagnostics.FileVersionInfo.GetVersionInfo(plugin).FileVersion.Replace(", ", "."));
		}
        public Version GetScriptExtenderPluginVersion(string plugin) { return _GetScriptExtenderPluginVersion(plugin); }
        public static Version _GetScriptExtenderPluginVersion(string plugin)
        {
			plugin=Path.ChangeExtension(Path.Combine(Path.Combine(Program.currentGame.DataFolderPath, Path.Combine(Program.currentGame.ScriptExtenderName, "plugins")), plugin), ".dll");
			CheckPathSafty(plugin);
			permissions.Assert();
			if(!File.Exists(plugin)) return null;
			else return new Version(System.Diagnostics.FileVersionInfo.GetVersionInfo(plugin).FileVersion.Replace(", ", "."));
		}
        public Version GetSKSEPluginVersion(string plugin) { return _GetSKSEPluginVersion(plugin); }
        public static Version _GetSKSEPluginVersion(string plugin)
        {
            plugin = Path.ChangeExtension(Path.Combine(Path.Combine(Program.currentGame.DataFolderPath, "\\skse\\plugins"), plugin), ".dll");
            CheckPathSafty(plugin);
            permissions.Assert();
            if (!File.Exists(plugin)) return null;
            else return new Version(System.Diagnostics.FileVersionInfo.GetVersionInfo(plugin).FileVersion.Replace(", ", "."));
        }

        public string[] GetPlugins(string path, string pattern, bool recurse) { return _GetPlugins(path, pattern, recurse); }
        public static string[] _GetPlugins(string path, string pattern, bool recurse)
        {
			CheckPluginFolderSafety(path);
			return testMode ? SimulateFSOutput(pluginList, path, pattern, recurse)
				: StripPathList(GetFilePaths(Plugins + path, pattern, recurse), Plugins.Length);
		}
        public string[] GetDataFiles(string path, string pattern, bool recurse) { return _GetDataFiles(path, pattern, recurse); }
        public static string[] _GetDataFiles(string path, string pattern, bool recurse)
        {
			CheckDataFolderSafety(path);
			return testMode ? SimulateFSOutput(dataFileList, path, pattern, recurse)
				: StripPathList(GetFilePaths(DataFiles + path, pattern, recurse), DataFiles.Length);
		}
        public string[] GetPluginFolders(string path, string pattern, bool recurse) { return _GetPluginFolders(path, pattern, recurse); }
        public static string[] _GetPluginFolders(string path, string pattern, bool recurse)
        {
			CheckPluginFolderSafety(path);
			return testMode ? SimulateFSOutput(pluginFolderList, path, pattern, recurse)
				: StripPathList(GetDirectoryPaths(Plugins + path, pattern, recurse), Plugins.Length);
		}
        public string[] GetDataFolders(string path, string pattern, bool recurse) { return _GetDataFolders(path, pattern, recurse); }
        public static string[] _GetDataFolders(string path, string pattern, bool recurse)
        {
			CheckDataFolderSafety(path);
			return testMode ? SimulateFSOutput(dataFolderList, path, pattern, recurse)
				: StripPathList(GetDirectoryPaths(DataFiles + path, pattern, recurse), DataFiles.Length);
		}

        public string[] GetActiveEspNames() { return _GetActiveEspNames(); }
        public static string[] _GetActiveEspNames()
        {
			permissions.Assert();
			Program.Data.SortEsps();
			List<string> names=new List<string>();
			for(int i=0;i<Program.Data.Esps.Count;i++) if(Program.Data.Esps[i].Active) names.Add(Program.Data.Esps[i].FileName);
			return names.ToArray();
		}
        public byte[] GetExistingDataFile(string filename) { return _ReadExistingDataFile(filename); }
        public string[] GetExistingEspNames() { return _GetExistingEspNames(); }
        public static string[] _GetExistingEspNames()
        {
			permissions.Assert();
			Program.Data.SortEsps();
			string[] names=new string[Program.Data.Esps.Count];
			for(int i=0;i<names.Length;i++) names[i]=Program.Data.Esps[i].FileName;
			return names;
		}
        public string[] GetActiveOmodNames() { return _GetActiveOmodNames(); }
        public static string[] _GetActiveOmodNames()
        {
			string[] names=new string[Program.Data.omods.Count];
			for(int i=0;i<names.Length;i++) names[i]=Program.Data.omods[i].ModName;
			return names;
		}

        public string[] Select(string[] items, string[] previews, string[] descs, string title, bool many) { return _Select(items, previews, descs, title, many, false); }
        public string[] Select(string[] items, string[] previews, string[] descs, string title, bool many, bool atleastone) { return _Select(items, previews, descs, title, many, atleastone); }
        public static string[] _Select(string[] items, string[] previews, string[] descs, string title, bool many, bool atleastone)
        {
			permissions.Assert();
            string[] result = new string[0];
			if(previews!=null) {
				for(int i=0;i<previews.Length;i++) {
                    if (previews[i] != null && previews[i] != "")
                    {
                        try
                        {
                            CheckDataSafety(previews[i]);
                        }
                        catch
                        { }

                        previews[i] = Path.Combine(DataFiles, previews[i]);
                    }
                }
			}
            if (items.Length > 0)
            {
                Forms.SelectForm sf = new OblivionModManager.Forms.SelectForm(items, title, many, previews, descs, atleastone);
                sf.ShowDialog();
                CancelCheck();
                result = new string[sf.SelectedIndex.Length];
                for (int i = 0; i < sf.SelectedIndex.Length; i++)
                {
                    result[i] = items[sf.SelectedIndex[i]];
                }
            }
			return result;
		}

        public void Message(string msg) { _Message(msg); }
        public static void _Message(string msg)
        {
			MessageBox.Show(msg);
		}
        public void Message(string msg, string title) { _Message(msg,title); }
        public static void _Message(string msg, string title)
        {
			MessageBox.Show(msg, title);
		}
        public void DisplayImage(string path) { _DisplayImage(path, null); }
        public void DisplayImage(string path, string title) { _DisplayImage(path, title); }
        public static void _DisplayImage(string path, string title)
        {
			CheckDataSafety(path);
			permissions.Assert();
			System.Drawing.Image image=System.Drawing.Image.FromFile(DataFiles+path);
			new ImageForm(image, (title!=null)?title:path).ShowDialog();
			image.Dispose();
		}
        public void DisplayText(string path) { _DisplayText(path, null); }
        public void DisplayText(string path, string title) { _DisplayText(path, title); }
        public static void _DisplayText(string path, string title)
        {
			CheckDataSafety(path);
			permissions.Assert();
			string s=File.ReadAllText(DataFiles+path, System.Text.Encoding.Default);
			new TextEditor((title!=null)?title:path, s, true, false).ShowDialog();
		}

        public void LoadEarly(string plugin) {_LoadEarly(plugin); }
        public static void _LoadEarly(string plugin)
        {
			CheckPathSafty(plugin);
			plugin=plugin.ToLower();
			if(srd.EarlyPlugins.Contains(plugin)) return;
			srd.EarlyPlugins.Add(plugin);
		}
        public void LoadBefore(string plugin1, string plugin2) { _ModLoadOrder(plugin1, plugin2, false); }
        public static void _LoadBefore(string plugin1, string plugin2) { _ModLoadOrder(plugin1, plugin2, false); }
        public void LoadAfter(string plugin1, string plugin2) { _ModLoadOrder(plugin1, plugin2, true); }
        public static void _LoadAfter(string plugin1, string plugin2) { _ModLoadOrder(plugin1, plugin2, true); }
        public static void _ModLoadOrder(string plugin1, string plugin2, bool after)
        {
			CheckPathSafty(plugin1);
			string path1=plugin1.ToLower();
			bool found=false;
			for(int i=0;i<srd.CopyPlugins.Count;i++) {
				if(srd.CopyPlugins[i].CopyTo==path1) {
					found=true;
					break;
				}
			}
			if(!found) CheckPluginSafty(plugin1);
			CheckPathSafty(plugin2);
			plugin1=plugin1.ToLower();
			plugin2=plugin2.ToLower();
			for(int i=0;i<srd.LoadOrderList.Count;i++) {
				if(plugin1==srd.LoadOrderList[i].Plugin&&plugin2==srd.LoadOrderList[i].Target) srd.LoadOrderList.RemoveAt(i--);
			}
			srd.LoadOrderList.Add(new PluginLoadInfo(plugin1, plugin2, after));
		}

        public void SetNewLoadOrder(string[] plugins) { _SetNewLoadOrder(plugins); }
        public static void _SetNewLoadOrder(string[] plugins)
        {
			if(plugins.Length!=Program.Data.Esps.Count) throw new ScriptingException("SetNewLoadOrder was called with an invalid plugin list");
			permissions.Assert();
			for(int i=0;i<plugins.Length;i++) {
				CheckPathSafty(plugins[i]);
				plugins[i]=Path.Combine(Program.currentGame.DataFolderPath, plugins[i]);
				if(!File.Exists(plugins[i])) throw new ScriptingException("Plugin '"+plugins[i]+"' does not exist");
			}
			for(int i=1;i<=plugins.Length;i++) {
				new System.IO.FileInfo(plugins[i-1]).LastWriteTime=new DateTime(2005, 1+(i-i%28)/28, i%28);
			}
			Program.Data.SortEsps();
		}

        public void UncheckEsp(string plugin) { _UncheckEsp(plugin); }
        public static void _UncheckEsp(string plugin)
        {
			CheckPluginSafty(plugin);
			plugin=plugin.ToLower();
			if(!srd.UncheckedPlugins.Contains(plugin)) srd.UncheckedPlugins.Add(plugin);
		}
        public static void _SetPluginActivation(string plugin, bool bActivatePlugin)
        {
            if (bActivatePlugin)
            {
                _InstallPlugin(plugin);
            }
            else
            {
                _UncheckEsp( plugin);
            }
        }

        public void SetDeactivationWarning(string plugin, DeactiveStatus warning) {_SetDeactivationWarning(plugin, warning);}
        public static void _SetDeactivationWarning(string plugin, DeactiveStatus warning)
        {
			CheckPluginSafty(plugin);
			plugin=plugin.ToLower();
			for(int i=0;i<srd.EspDeactivation.Count;i++) {
				if(srd.EspDeactivation[i].Plugin==plugin) srd.EspDeactivation.RemoveAt(i--);
			}
			srd.EspDeactivation.Add(new ScriptEspWarnAgainst(plugin, warning));
		}

        public void ConflictsWith(string filename) { _ConflictsWith(filename, 0, 0, 0, 0, null, ConflictLevel.MajorConflict, false); }
        public void ConslictsWith(string filename, string comment) { _ConflictsWith(filename, 0, 0, 0, 0, comment, ConflictLevel.MajorConflict, false); }
        public void ConflictsWith(string filename, string comment, ConflictLevel level) { _ConflictsWith(filename, 0, 0, 0, 0, comment, level, false); }
        public void ConflictsWith(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion)
        {
			_ConflictsWith(name, minMajorVersion, minMinorVersion, maxMajorVersion, maxMinorVersion, null, ConflictLevel.MajorConflict, false);
		}
        public void ConflictsWith(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, string comment)
        {
			_ConflictsWith(name, minMajorVersion, minMinorVersion, maxMajorVersion, maxMinorVersion, comment, ConflictLevel.MajorConflict, false);
		}
        public void ConflictsWith(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, string comment, ConflictLevel level)
        {
			_ConflictsWith(name, minMajorVersion, minMinorVersion, maxMajorVersion, maxMinorVersion, comment, level, false);
		}
        public void ConflictsWith(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, string comment, ConflictLevel level, bool regex)
        {
            _ConflictsWith(name, minMajorVersion, minMinorVersion, maxMajorVersion, maxMinorVersion, comment, level, regex);
        }
        public static void _ConflictsWith(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, string comment, ConflictLevel level, bool regex)
        {
			ConflictData cd=new ConflictData();
			cd.File=name;
			cd.Comment=comment;
			cd.level=level;
			cd.MinMajorVersion=minMajorVersion;
			cd.MinMinorVersion=minMinorVersion;
			cd.MaxMajorVersion=maxMajorVersion;
			cd.MaxMinorVersion=maxMinorVersion;
			cd.Partial=regex;
			srd.ConflictsWith.Add(cd);
		}
        public void DependsOn(string filename) { _DependsOn(filename, 0, 0, 0, 0, null, false); }
        public void DependsOn(string filename, string comment) { _DependsOn(filename, 0, 0, 0, 0, comment, false); }
        public void DependsOn(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion)
        {
			_DependsOn(name, minMajorVersion, minMinorVersion, maxMajorVersion, maxMinorVersion, null, false);
		}
        public void DependsOn(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, string comment)
        {
			_DependsOn(name, minMajorVersion, minMinorVersion, maxMajorVersion, maxMinorVersion, comment, false);
		}
        public void DependsOn(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, string comment, bool regex)
        {
            _DependsOn(name, minMajorVersion, minMinorVersion, maxMajorVersion, maxMinorVersion, comment, regex);
        }
        public static void _DependsOn(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, string comment, bool regex)
        {
			ConflictData cd=new ConflictData();
			cd.File=name;
			cd.Comment=comment;
			cd.MinMajorVersion=minMajorVersion;
			cd.MinMinorVersion=minMinorVersion;
			cd.MaxMajorVersion=maxMajorVersion;
			cd.MaxMinorVersion=maxMinorVersion;
			cd.Partial=regex;
			srd.DependsOn.Add(cd);
		}

        public void DontInstallAnyPlugins() { _DontInstallAnyPlugins(); }
        public static void _DontInstallAnyPlugins() { srd.InstallAllPlugins = false; }
        public void DontInstallAnyDataFiles() { _DontInstallAnyDataFiles(); }
        public static void _DontInstallAnyDataFiles() { srd.InstallAllData = false; }
        public void InstallAllPlugins() { _InstallAllPlugins(); }
        public static void _InstallAllPlugins() { srd.InstallAllPlugins = true; }
        public void InstallAllDataFiles() { _InstallAllDataFiles(); }
        public static void _InstallAllDataFiles() { srd.InstallAllData = true; }

        public void DontInstallPlugin(string name) { _DontInstallPlugin( name); }
        public static void _DontInstallPlugin(string name)
        {
			CheckPluginSafty(name);
			Program.strArrayRemove(srd.InstallPlugins, name);
			if(!Program.strArrayContains(srd.IgnorePlugins, name)) srd.IgnorePlugins.Add(name);
		}
        public void DontInstallDataFile(string name) { _DontInstallDataFile(name); }
        public static void _DontInstallDataFile(string name)
        {
			CheckDataSafety(name);
			Program.strArrayRemove(srd.InstallData, name);
			if(!Program.strArrayContains(srd.IgnoreData, name)) srd.IgnoreData.Add(name);
		}
        public void DontInstallDataFolder(string folder, bool recurse) { _DontInstallDataFolder(folder, recurse); }
        public static void _DontInstallDataFolder(string folder, bool recurse)
        {
			CheckDataFolderSafety(folder);
			if(testMode) {
				folder=folder.ToLower();
				if(folder.EndsWith("\\")||folder.EndsWith("/")) folder=folder.Remove(folder.Length-1);
				foreach(string s in dataFileList) {
					if(s.StartsWith(folder)&&(recurse||s.IndexOf('\\', folder.Length+1)==-1)) {
						Program.strArrayRemove(srd.InstallData, s);
						if(!Program.strArrayContains(srd.IgnoreData, s)) srd.IgnoreData.Add(s);
					}
				}
			} else {
				permissions.Assert();
				foreach(string path in Directory.GetFiles(DataFiles+folder, "*", recurse?System.IO.SearchOption.AllDirectories:System.IO.SearchOption.TopDirectoryOnly)) {
					string file=Path.GetFullPath(path).Substring(DataFiles.Length);
					Program.strArrayRemove(srd.InstallData, file);
					if(!Program.strArrayContains(srd.IgnoreData, file)) srd.IgnoreData.Add(file);
				}
			}
		}
        public void InstallPlugin(string name) { _InstallPlugin(name); }
        public static void _InstallPlugin(string name)
        {
			CheckPluginSafty(name);
			Program.strArrayRemove(srd.IgnorePlugins, name);
			if(!Program.strArrayContains(srd.InstallPlugins, name)) srd.InstallPlugins.Add(name);
		}
        public void InstallFolderFromFomod(string fromname, string toname, bool recurse) { _InstallFolderFromFomod(fromname, toname, recurse); }
        public static void _InstallFolderFromFomod(string fromname, string toname, bool recurse)
        {
            _CopyDataFolder(fromname, toname, recurse);
        }
        public void InstallFolderFromFomod(string name, bool recurse) { _InstallFolderFromFomod(name, recurse); }
        public static void _InstallFolderFromFomod(string name, bool recurse)
        {
            _InstallDataFolder(name, recurse);
        }
        public void InstallFileFromFomod(string fromname, string toname) { _InstallFileFromFomod(fromname, toname); }
        public static void _InstallFileFromFomod(string fromname, string toname)
        {
            _CopyDataFile(fromname,toname);
        }
        public void InstallFileFromFomod(string name) { _InstallFileFromFomod(name); }
        public static void _InstallFileFromFomod(string name)
        {
            _InstallDataFile(name);
        }
        public void InstallDataFile(string name) { _InstallDataFile(name); }
        public static void _InstallDataFile(string name)
        {
			CheckDataSafety(name);
			Program.strArrayRemove(srd.IgnoreData, name);
			if(!Program.strArrayContains(srd.InstallData, name)) srd.InstallData.Add(name);
		}
        public void InstallDataFolder(string folder, bool recurse) { _InstallDataFolder(folder, recurse); }
        public static void _InstallDataFolder(string folder, bool recurse)
        {
			CheckDataFolderSafety(folder);
			if(testMode) {
				folder=folder.ToLower();
				if(folder.EndsWith("\\")||folder.EndsWith("/")) folder=folder.Remove(folder.Length-1);
				foreach(string s in dataFileList) {
					if(s.StartsWith(folder)&&(recurse||s.IndexOf('\\', folder.Length+1)==-1)) {
						Program.strArrayRemove(srd.IgnoreData, s);
						if(!Program.strArrayContains(srd.InstallData, s)) srd.InstallData.Add(s);
					}
				}
			} else {
				permissions.Assert();
				foreach(string path in Directory.GetFiles(DataFiles+folder, "*", recurse?System.IO.SearchOption.AllDirectories:System.IO.SearchOption.TopDirectoryOnly)) {
					string file=Path.GetFullPath(path).Substring(DataFiles.Length);
					Program.strArrayRemove(srd.IgnoreData, file);
					if(!Program.strArrayContains(srd.InstallData, file)) srd.InstallData.Add(file);
				}
			}
		}

        public void CopyPlugin(string from, string to) { _CopyPlugin(from, to); }
        public static void _CopyPlugin(string from, string to)
        {
			CheckPluginSafty(from);
            if (to == null || to.Length == 0)
                to = Path.GetFileName(from);
			CheckPathSafty(to);
			string lto=to.ToLower();
			if(!lto.EndsWith(".esp")&&!lto.EndsWith(".esm") && !lto.EndsWith(".esl")) throw new ScriptingException("Copied plugins must have a .esp, .esl or .esm file extension");
			if(to.Contains("\\")||to.Contains("/")) throw new ScriptingException("Cannot copy a plugin to a subdirectory of the data folder");
			for(int i=0;i<srd.CopyPlugins.Count;i++) {
				if(srd.CopyPlugins[i].CopyTo==lto) srd.CopyPlugins.RemoveAt(i--);
			}
			srd.CopyPlugins.Add(new ScriptCopyDataFile(from, to));
		}
        public void CopyDataFile(string from, string to) { _CopyDataFile( from,  to); }
        public static void _CopyDataFile(string from, string to)
        {
            try
            {
                CheckDataSafety(from);
                if (to == null || to.Length == 0)
                    to = Path.GetFileName(from);
                CheckPathSafty(to);
                string lto = to.ToLower();
                //if (lto.EndsWith(".esm") || lto.EndsWith(".esp")) throw new ScriptingException("Copied data files cannot have a .esp or .esm file extension");
                for (int i = 0; i < srd.CopyDataFiles.Count; i++)
                {
                    if (srd.CopyDataFiles[i].CopyTo == lto) srd.CopyDataFiles.RemoveAt(i--);
                }
                srd.CopyDataFiles.Add(new ScriptCopyDataFile(from, to));
            }
            catch (Exception ex)
            {
                MessageBox.Show("File '" + from + "' cannot be installed: " + ex.Message, "File cannot be copied", mbButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        public void CopyDataFolder(string from, string to, bool recurse) { _CopyDataFolder(from, to, recurse); }
        public static void _CopyDataFolder(string from, string to, bool recurse)
        {
            // check if it's actually a folder
            if (File.Exists(Path.Combine(DataFiles,from)))
            {
                if (string.IsNullOrWhiteSpace(to))
                {
                    to = from;
                }

                _CopyDataFile(from, to);
                return;
            }
            try
            {
			    CheckDataFolderSafety(from);

                // valid FROM folder. But what does it contain?
                string[] espfiles = Directory.GetFiles(Path.Combine(DataFiles, from), "*.es*");
                string[] bsafiles = Directory.GetFiles(Path.Combine(DataFiles, from), "*.bsa");

                if (espfiles.Length > 0 || bsafiles.Length > 0)
                {
                    // mislabelled to
                    to = ".";
                }
                else
                {
                    to = from;
                }

			    CheckFolderSafety(to);

			    if(testMode) {
				    from=from.ToLower();
				    to=to.ToLower();
				    if(from.EndsWith("\\")||from.EndsWith("/")) from=from.Remove(from.Length-1);
				    if(to.EndsWith("\\")||to.EndsWith("/")) to=to.Remove(to.Length-1);
				    foreach(string s in dataFileList) {
					    if(s.StartsWith(from)&&(recurse||s.IndexOf('\\', from.Length+1)==-1)) {
						    string lto=Path.Combine(to, s.Substring(s.IndexOf('\\', from.Length-1)+1));
						    for(int i=0;i<srd.CopyDataFiles.Count;i++) {
							    if(srd.CopyDataFiles[i].CopyTo==lto) srd.CopyDataFiles.RemoveAt(i--);
						    }
						    srd.CopyDataFiles.Add(new ScriptCopyDataFile(s, lto));
					    }
				    }
			    } else {
				    permissions.Assert();
				    from=Path.GetFullPath(Path.Combine(DataFiles, from));
				    foreach(string path in Directory.GetFiles(from, "*", recurse?System.IO.SearchOption.AllDirectories:System.IO.SearchOption.TopDirectoryOnly)) {
					    string filefrom=Path.GetFullPath(path).Substring(DataFiles.Length);
					    string fileto=Path.GetFullPath(path).Substring(from.Length);
					    if(fileto.StartsWith(""+Path.DirectorySeparatorChar)||fileto.StartsWith(""+Path.AltDirectorySeparatorChar)) fileto=fileto.Substring(1);
					    fileto=Path.Combine(to, fileto);
					    string lto=fileto.ToLower();
					    for(int i=0;i<srd.CopyDataFiles.Count;i++) {
						    if(srd.CopyDataFiles[i].CopyTo==lto) srd.CopyDataFiles.RemoveAt(i--);
					    }
                        if (!lto.EndsWith(".esp") && !lto.EndsWith(".esm") && !lto.EndsWith(".esl"))
                            srd.CopyDataFiles.Add(new ScriptCopyDataFile(filefrom, fileto));
                        else
                            srd.CopyPlugins.Add(new ScriptCopyDataFile(filefrom, fileto));
                    }
			    }
            }
            catch (Exception ex)
            {
                if (!Directory.Exists(DataFiles + from))
                {
                    MessageBox.Show("Skippping non existing requested folder '" + from + "'", "Ignoring folder", mbButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Folder '" + from + "' cannot be installed: " + ex.Message, "Folder cannot be copied", mbButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }

		}

        public void PatchPlugin(string from, string to, bool create) { _PatchPlugin(from, to, create); }
        public static void _PatchPlugin(string from, string to, bool create)
        {
			CheckPluginSafty(from);
			CheckPathSafty(to);
			string lto=to.ToLower();
			if(!lto.EndsWith(".esp")&&!lto.EndsWith(".esm") && !lto.EndsWith(".esl")) throw new ScriptingException("Copied plugins must have a .esp, .esl or .esm file extension");
			if(to.Contains("\\")||to.Contains("/")) throw new ScriptingException("Cannot copy a plugin to a subdirectory of the data folder");
			to=Path.Combine(Program.currentGame.DataFolderPath,to);

			permissions.Assert();

			if(testMode) {
				if(File.Exists(to)&&!Program.Data.DoesEspExist(to))
					throw new ScriptingException("Cannot patch file '"+to+"' because it already exists but is not parented to an omod");
				return;
			}

			DateTime timestamp=File.GetLastWriteTime(Plugins+from);
			if(File.Exists(to)) {
				if(!Program.Data.DoesEspExist(to)) {
					throw new ScriptingException("Cannot patch file '"+to+"' because it already exists but is not parented to an omod");
				}
				timestamp=File.GetLastWriteTime(to);
				File.Delete(to);
			} else if(!create) return;
			File.Move(Plugins+from, to);
			File.SetLastWriteTime(to, timestamp);
		}
        public void PatchDataFile(string from, string to, bool create) { _PatchDataFile(from, to, create); }
        public static void _PatchDataFile(string from, string to, bool create)
        {
			CheckDataSafety(from);
			CheckPathSafty(to);
			string lto=to.ToLower();
			if(lto.EndsWith(".esp")||lto.EndsWith(".esm") || lto.EndsWith(".esl")) throw new ScriptingException("Copied data files must not have a .esp, .esl or .esm file extension");
			to=Path.Combine(Program.currentGame.DataFolderPath,to);

			permissions.Assert();

			if(testMode) {
				if(File.Exists(to)&&!Program.Data.DoesDataFileExist(to))
					throw new ScriptingException("Cannot patch file '"+to+"' because it already exists but is not parented to an omod");
				return;
			}

			DateTime timestamp=File.GetLastWriteTime(DataFiles+from);
			if(File.Exists(to)) {
				if(!Program.Data.DoesDataFileExist(to)) {
					throw new ScriptingException("Cannot patch file '"+to+"' because it already exists but is not parented to an omod");
				}
				timestamp=File.GetLastWriteTime(to);
				File.Delete(to);
			} else if(!create) return;
			File.Move(DataFiles+from, to);
			File.SetLastWriteTime(to, timestamp);
		}

        public void RegisterBSA(string path) { _RegisterBSA(path); }
        public static void _RegisterBSA(string path)
        {
			CheckDataSafety(path);
			if(path.Contains(";")||path.Contains(",")||path.Contains("=")) throw new ScriptingException("BSA path cannot contain the characters ',', '=' or ';'");
			if(!srd.RegisterBSAList.Contains(path)) srd.RegisterBSAList.Add(path);
		}
        public void UnregisterBSA(string path) { _UnregisterBSA(path); }
        public static void _UnregisterBSA(string path)
        {
			CheckDataSafety(path);
			if(path.Contains(";")||path.Contains(",")||path.Contains("=")) throw new ScriptingException("BSA path cannot contain the characters ',', '=' or ';'");
			if(srd.RegisterBSAList.Contains(path)) srd.RegisterBSAList.Remove(path);
		}

        public void EditINI(string section, string key, string value) { _EditINI(section, key, value); }
        public static void _EditINI(string section, string key, string value)
        {
			srd.INIEdits.Add(new INIEditInfo(section, key, value));
		}
        public void EditShader(byte package, string name, string path) { _EditShader(package, name, path); }
        public static void _EditShader(byte package, string name, string path)
        {
			CheckDataSafety(path);
			srd.SDPEdits.Add(new SDPEditInfo(package, name, DataFiles+path));
		}

        public void FatalError() { _FatalError(); }
        public static void _FatalError() { srd.CancelInstall = true; }

        public void SetGMST(string file, string edid, string value) { _SetGMST(file, edid, value); }
        public static void _SetGMST(string file, string edid, string value)
        {
			CheckPluginSafty(file);
			srd.EspEdits.Add(new ScriptEspEdit(false, file.ToLower(), edid, value));
		}
        public void SetGlobal(string file, string edid, string value) { _SetGlobal(file, edid, value); }
        public static void _SetGlobal(string file, string edid, string value)
        {
			CheckPluginSafty(file);
			srd.EspEdits.Add(new ScriptEspEdit(false, file.ToLower(), edid, value));
		}

        public void SetPluginByte(string file, long offset, byte value) { _SetPluginByte(file, offset, value); }
        public static void _SetPluginByte(string file, long offset, byte value)
        {
			CheckPluginSafty(file);
			if(testMode) return;
			permissions.Assert();
			System.IO.FileStream fs=File.OpenWrite(Plugins+file);
			fs.Position=offset;
			fs.WriteByte(value);
			fs.Close();
		}
        public void SetPluginShort(string file, long offset, short value) { _SetPluginShort(file, offset, value); }
        public static void _SetPluginShort(string file, long offset, short value)
        {
			CheckPluginSafty(file);
			if(testMode) return;
			permissions.Assert();
			byte[] data=BitConverter.GetBytes(value);
			System.IO.FileStream fs=File.OpenWrite(Plugins+file);
			fs.Position=offset;
			fs.Write(data, 0, 2);
			fs.Close();
		}
        public void SetPluginInt(string file, long offset, int value) { _SetPluginInt(file, offset, value); }
        public static void _SetPluginInt(string file, long offset, int value)
        {
			CheckPluginSafty(file);
			if(testMode) return;
			permissions.Assert();
			byte[] data=BitConverter.GetBytes(value);
			System.IO.FileStream fs=File.OpenWrite(Plugins+file);
			fs.Position=offset;
			fs.Write(data, 0, 4);
			fs.Close();
		}
        public void SetPluginLong(string file, long offset, long value) { _SetPluginLong(file, offset, value); }
        public static void _SetPluginLong(string file, long offset, long value)
        {
			CheckPluginSafty(file);
			if(testMode) return;
			permissions.Assert();
			byte[] data=BitConverter.GetBytes(value);
			System.IO.FileStream fs=File.OpenWrite(Plugins+file);
			fs.Position=offset;
			fs.Write(data, 0, 8);
			fs.Close();
		}
        public void SetPluginFloat(string file, long offset, float value) { _SetPluginFloat(file, offset, value); }
        public static void _SetPluginFloat(string file, long offset, float value)
        {
			CheckPluginSafty(file);
			if(testMode) return;
			permissions.Assert();
			byte[] data=BitConverter.GetBytes(value);
			System.IO.FileStream fs=File.OpenWrite(Plugins+file);
			fs.Position=offset;
			fs.Write(data, 0, 4);
			fs.Close();
		}

        public string InputString() { return _InputString("", ""); }
        public string InputString(string title) { return _InputString(title, ""); }
        public string InputString(string title, string initial) { return _InputString(title, initial); }
        public static string _InputString(string title, string initial)
        {
			permissions.Assert();
			TextEditor te=new TextEditor(title, initial, false, true);
			te.ShowDialog();
			if(te.DialogResult!=DialogResult.Yes) return "";
			else return te.Result;
		}

        public string ReadINI(string section, string value) { return _ReadINI(section, value); }
        public static string _ReadINI(string section, string value)
        {
			permissions.Assert();
			return INI.GetINIValue(section, value);
		}
        public string ReadRendererInfo(string value) { return _ReadRendererInfo(value); }
        public static string _ReadRendererInfo(string value)
        {
			permissions.Assert();
			return OblivionRenderInfo.GetInfo(value);
		}

        public void EditXMLLine(string file, int line, string value){ _EditXMLLine(file, line, value); }
        public static void _EditXMLLine(string file, int line, string value)
        {
			CheckDataSafety(file);
			string ext=Path.GetExtension(file).ToLower();
			if(ext!=".txt"&&ext!=".xml"&&ext!=".bat"&&ext!=".ini") throw new ScriptingException("Can only edit files with a .xml, .ini, .bat or .txt extension");
			permissions.Assert();
			string[] lines=File.ReadAllLines(DataFiles+file);
			if(line<0||line>=lines.Length) throw new ScriptingException("Invalid line number");
			lines[line]=value;
			File.WriteAllLines(DataFiles+file, lines);
		}
        public void EditXMLReplace(string file, string find, string replace) { _EditXMLReplace(file, find, replace); }
        public static void _EditXMLReplace(string file, string find, string replace)
        {
			CheckDataSafety(file);
			string ext=Path.GetExtension(file).ToLower();
			if(ext!=".txt"&&ext!=".xml"&&ext!=".bat"&&ext!=".ini") throw new ScriptingException("Can only edit files with a .xml, .ini, .bat or .txt extension");
			permissions.Assert();
			string text=File.ReadAllText(DataFiles+file);
			text=text.Replace(find, replace);
			File.WriteAllText(DataFiles+file, text);
		}

        public System.Windows.Forms.Form CreateCustomDialog() { return _CreateCustomDialog(); }
        public static System.Windows.Forms.Form _CreateCustomDialog()
        {
			permissions.Assert();
			return new System.Windows.Forms.Form();
		}

        public byte[] ReadDataFile(string file) { return _ReadDataFile(file); }
        public static byte[] _ReadDataFile(string file)
        {
			if(testMode) throw new ScriptingException("ReadDataFile cannot be used in a script simulation");
			CheckDataSafety(file);
			permissions.Assert();
			return File.ReadAllBytes(Path.Combine(DataFiles, file));
		}
        public byte[] GetFileFromFomod(string file)
        {
            return _ReadDataFile(file);
        }

        public byte[] ReadExistingDataFile(string file) { return _ReadExistingDataFile(file); }
        public static byte[] _ReadExistingDataFile(string file)
        {
			CheckPathSafty(file);
			permissions.Assert();
            if (File.Exists(Path.Combine(Program.currentGame.DataFolderPath,file)))
			    return File.ReadAllBytes(Path.Combine(Program.currentGame.DataFolderPath,file));
            else
                return null;
		}
        public byte[] GetDataFileFromBSA(string file) { return _GetDataFileFromBSA(file); }
        public static byte[] _GetDataFileFromBSA(string file)
        {
			CheckPathSafty(file);
			permissions.Assert();
			return Classes.BSAArchive.GetFileFromBSA(file);
		}
        public byte[] GetDataFileFromBSA(string bsa, string file) { return _GetDataFileFromBSA(bsa, file); }
        public static byte[] _GetDataFileFromBSA(string bsa, string file)
        {
			CheckPathSafty(file);
			permissions.Assert();
			return Classes.BSAArchive.GetFileFromBSA(bsa, file);
		}

        public void GenerateDataFile(string file, byte[] data) { _GenerateNewDataFile(file, data); }
        public void GenerateNewDataFile(string file, byte[] data) { _GenerateNewDataFile(file, data); }
        public static void _GenerateNewDataFile(string file, byte[] data)
        {
			if(testMode) throw new ScriptingException("GenerateNewDataFile cannot be used in a script simulation");
			CheckPathSafty(file);
			permissions.Assert();
			string file2=Path.Combine(DataFiles, file);
			if(!File.Exists(file2)) {
				string lto=file.ToLower();
				if(lto.EndsWith(".esm")||lto.EndsWith(".esp") || lto.EndsWith(".esl")) throw new ScriptingException("Copied data files cannot have a .esp, .esl or .esm file extension");
				for(int i=0;i<srd.CopyDataFiles.Count;i++) {
					if(srd.CopyDataFiles[i].CopyTo==lto) srd.CopyDataFiles.RemoveAt(i--);
				}
				srd.CopyDataFiles.Add(new ScriptCopyDataFile(file, file));
			}
			if(!Directory.Exists(Path.GetDirectoryName(file2))) Directory.CreateDirectory(Path.GetDirectoryName(file2));
			File.WriteAllBytes(file2, data);
		}
        public void CancelDataFileCopy(string file) { _CancelDataFileCopy(file); }
        public static void _CancelDataFileCopy(string file)
        {
			if(testMode) throw new ScriptingException("CancelDataFileCopy cannot be used in a script simulation");
			CheckPathSafty(file);
			permissions.Assert();
			string file2=Path.Combine(DataFiles, file);
			string lto=file.ToLower();
			for(int i=0;i<srd.CopyDataFiles.Count;i++) {
				if(srd.CopyDataFiles[i].CopyTo==lto) srd.CopyDataFiles.RemoveAt(i--);
			}
			File.Delete(file2);
		}
        public void CancelDataFolderCopy(string folder) { _CancelDataFolderCopy(folder); }
        public static void _CancelDataFolderCopy(string folder)
        {
			if(testMode) throw new ScriptingException("CancelDataFolderCopy cannot be used in a script simulation");
			CheckPathSafty(folder);
			permissions.Assert();
			string lto=folder.ToLower();
			for(int i=0;i<srd.CopyDataFiles.Count;i++) {
				if(srd.CopyDataFiles[i].CopyTo.StartsWith(lto)) {
					File.Delete(Path.Combine(DataFiles, srd.CopyDataFiles[i].CopyTo));
					srd.CopyDataFiles.RemoveAt(i--);
				}
			}
		}
        public void GenerateBSA(string file, string path, string prefix, int cRatio, int cLevel) { _GenerateBSA(file, path, prefix, cRatio, cLevel); }
        public static void _GenerateBSA(string file, string path, string prefix, int cRatio, int cLevel)
        {
			if(testMode) throw new ScriptingException("GenerateBSA cannot be used in a script simulation");
			CheckPathSafty(file);
			CheckDataFolderSafety(path);
			permissions.Assert();
			path=Path.Combine(DataFiles, path);
			string file2=Path.Combine(DataFiles, file);
			if(!File.Exists(file2)) {
				string lto=file.ToLower();
				if(lto.EndsWith(".esm")||lto.EndsWith(".esp") || lto.EndsWith(".esl")) throw new ScriptingException("Copied data files cannot have a .esp, .esl or .esm file extension");
				for(int i=0;i<srd.CopyDataFiles.Count;i++) {
					if(srd.CopyDataFiles[i].CopyTo==lto) srd.CopyDataFiles.RemoveAt(i--);
				}
				srd.CopyDataFiles.Add(new ScriptCopyDataFile(file, file));
			}
			Forms.BSACreator.CreateBSA(file2, path, prefix, cRatio, cLevel, false);
		}

        public bool IsSimulation() { return _IsSimulation(); }
        public static bool _IsSimulation() { return testMode; }
    }

}