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
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Security;
using System.Security.Permissions;
using System.Collections.Generic;

namespace OblivionModManager.Scripting {
	public class ScriptingException : ApplicationException {
        public ScriptingException(string msg) : base(msg) { }
		public ScriptingException(string msg, Exception inner) : base(msg, inner) { }
	}

	public class ExecutionCancelledException : ApplicationException { }

	public interface IScript {
		void Execute(IScriptFunctions sf);
	}

	public interface IScriptFunctions {
        string getdataFilePath();
        void setdataFilePath(string value);
        string getpluginPath();
        void setpluginPath(string value);
        ScriptReturnData getsrd();
        void setsrd(ScriptReturnData srd);
        string[] getdataFileList();
        void setdataFileList(string[] datafiles);
        string[] getpluginList();
        void setpluginList(string[] plugins);
        bool GetDisplayWarnings();
		MainForm ProgramForm
		{
			get;
		}
		bool DialogYesNo(string msg);
		bool DialogYesNo(string msg, string title);
		bool DataFileExists(string path);
		Version GetOBMMVersion();
		Version GetOBSEVersion();
		Version GetOBGEVersion();
		Version GetOblivionVersion();
		Version GetOBSEPluginVersion(string plugin);

		string[] GetPlugins(string path, string pattern, bool recurse);
		string[] GetDataFiles(string path, string pattern, bool recurse);
		string[] GetPluginFolders(string path, string pattern, bool recurse);
		string[] GetDataFolders(string path, string pattern, bool recurse);

		string[] GetActiveEspNames();
		string[] GetExistingEspNames();
		string[] GetActiveOmodNames();

		string[] Select(string[] items, string[] previews, string[] descs, string title, bool many);
        string[] Select(string[] items, string[] previews, string[] descs, string title, bool many, bool atleastone);

		void Message(string msg);
		void Message(string msg, string title);
		void DisplayImage(string path);
		void DisplayImage(string path, string title);
		void DisplayText(string path);
		void DisplayText(string path, string title);

		void LoadEarly(string plugin);
		void LoadBefore(string plugin1, string plugin2);
		void LoadAfter(string plugin1, string plugin2);
		void SetNewLoadOrder(string[] plugins);

		void UncheckEsp(string plugin);
		void SetDeactivationWarning(string plugin, DeactiveStatus warning);

		void ConflictsWith(string filename);
		void ConslictsWith(string filename, string comment);
		void ConflictsWith(string filename, string comment, ConflictLevel level);
		void ConflictsWith(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion);
		void ConflictsWith(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, string comment);
		void ConflictsWith(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, string comment, ConflictLevel level);
		void ConflictsWith(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, string comment, ConflictLevel level, bool regex);
		void DependsOn(string filename);
		void DependsOn(string filename, string comment);
		void DependsOn(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion);
		void DependsOn(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, string comment);
		void DependsOn(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, string comment, bool regex);

		void DontInstallAnyPlugins();
		void DontInstallAnyDataFiles();
		void InstallAllPlugins();
		void InstallAllDataFiles();

		void DontInstallPlugin(string name);
		void DontInstallDataFile(string name);
		void DontInstallDataFolder(string folder, bool recurse);
		void InstallPlugin(string name);
		void InstallDataFile(string name);
		void InstallDataFolder(string folder, bool recurse);

		void CopyPlugin(string from, string to);
		void CopyDataFile(string from, string to);
		void CopyDataFolder(string from, string to, bool recurse);

		void PatchPlugin(string from, string to, bool create);
		void PatchDataFile(string from, string to, bool create);

		void RegisterBSA(string path);
		void UnregisterBSA(string path);

		void EditINI(string section, string key, string value);
		void EditShader(byte package, string name, string path);

		void FatalError();

		void SetGMST(string file, string edid, string value);
		void SetGlobal(string file, string edid, string value);

		void SetPluginByte(string file, long offset, byte value);
		void SetPluginShort(string file, long offset, short value);
		void SetPluginInt(string file, long offset, int value);
		void SetPluginLong(string file, long offset, long value);
		void SetPluginFloat(string file, long offset, float value);

		string InputString();
		string InputString(string title);
		string InputString(string title, string initial);

		string ReadINI(string section, string value);
		string ReadRendererInfo(string value);

		void EditXMLLine(string file, int line, string value);
		void EditXMLReplace(string file, string find, string replace);

		System.Windows.Forms.Form CreateCustomDialog();
		
		byte[] ReadDataFile(string file);
		byte[] ReadExistingDataFile(string file);
		byte[] GetDataFileFromBSA(string file);
		byte[] GetDataFileFromBSA(string bsa, string file);

        byte[] GetFileFromFomod(string filename);
        void InstallFileFromFomod(string name);

        void GenerateDataFile(string file, byte[] data);
        void GenerateNewDataFile(string file, byte[] data);
		void CancelDataFileCopy(string file);
		void CancelDataFolderCopy(string folder);
		void GenerateBSA(string file, string path, string prefix, int cRatio, int cLevel);

		bool IsSimulation();
	}
}

namespace fomm.Scripting
{
    // NMM uses all kinds of classes for the same thing...
    public class CSharpBaseScript : BaseScript
    {
    }
    public class SkyrimBaseScript : BaseScript
    {
    }
    public class SkyrimCSharpBaseScript : BaseScript
    {
    }
    public class FalloutNewVegasBaseScript : BaseScript
    {
    }
    public class Fallout3BaseScript : BaseScript
    {
    }
    public class Fallout3CSharpBaseScript : BaseScript
    {
    }
    public class FalloutBaseScript : BaseScript
    {
    }
    public class FalloutCSharpBaseScript : BaseScript
    {
    }
    public class FalloutNVCSharpBaseScript : BaseScript
    {
    }
    public class FalloutNVBaseScript : BaseScript
    {
    }

    public class BaseScript
    {
//        FalloutNewVegasBaseScript(OblivionModManager.ScriptReturnData srd, string dataFilesPath, string pluginsPath)
//            : base(srd, dataFilesPath, pluginsPath)
//        {
//        }

        static public bool OnActivate()
        {
            // the default for a FOMOD is not to install anything until specified
            DontInstallAnyDataFiles();
            DontInstallAnyPlugins();
            Message("Hi");
            return true;
        }
        static public void Setup(OblivionModManager.ScriptReturnData srd, string[] datafiles, string[] plugins, string datafilespath, string pluginspath)
        {
            OblivionModManager.Scripting.ScriptFunctions._ScriptFunctions(srd, datafiles, plugins);
            OblivionModManager.Scripting.ScriptFunctions._ScriptFunctions(srd, datafilespath, pluginspath);
        }


        static public void MessageBox(string msg) { OblivionModManager.Scripting.ScriptFunctions._Message(msg); }
        static public void SetPluginActivation(string plugin, bool bActivate) { OblivionModManager.Scripting.ScriptFunctions._SetPluginActivation(plugin, bActivate); }
        static public string[] GetFomodFileList() { return OblivionModManager.Scripting.ScriptFunctions._GetModFileList(); }
        static public string[] GetModFileList() { return OblivionModManager.Scripting.ScriptFunctions._GetModFileList(); }
        static public bool GetDisplayWarnings() { return OblivionModManager.Scripting.ScriptFunctions._GetDisplayWarnings(); }
        static public bool DialogYesNo(string msg) { return OblivionModManager.Scripting.ScriptFunctions._DialogYesNo(msg, "Question"); }
        static public bool DialogYesNo(string msg, string title) { return OblivionModManager.Scripting.ScriptFunctions._DialogYesNo(msg, title); }
        static public bool DataFileExists(string path) { return OblivionModManager.Scripting.ScriptFunctions._DataFileExists(path); }
        static public bool ScriptExtenderPresent() { return OblivionModManager.Scripting.ScriptFunctions._ScriptExtenderPresent(); }
        static public Version GetOBMMVersion() { return OblivionModManager.Scripting.ScriptFunctions._GetOBMMVersion(); }
        static public Version GetOBSEVersion() { return OblivionModManager.Scripting.ScriptFunctions._GetOBSEVersion(); }
        static public Version GetSkseVersion() { return OblivionModManager.Scripting.ScriptFunctions._GetSKSEVersion(); }
        static public Version GetSKSEVersion() { return OblivionModManager.Scripting.ScriptFunctions._GetSKSEVersion(); }
        static public Version GetScriptExtenderVersion() { return OblivionModManager.Scripting.ScriptFunctions._GetScriptExtenderVersion(); }
        static public Version GetOBGEVersion() { return OblivionModManager.Scripting.ScriptFunctions._GetOBGEVersion(); }
        static public Version GetOblivionVersion() { return OblivionModManager.Scripting.ScriptFunctions._GetOblivionVersion(); }
        static public Version GetOBSEPluginVersion(string plugin) { return OblivionModManager.Scripting.ScriptFunctions._GetOBSEPluginVersion(plugin); }
        static public Version GetScriptExtenderPluginVersion(string plugin) { return OblivionModManager.Scripting.ScriptFunctions._GetScriptExtenderPluginVersion(plugin); }
        static public Version GetSKSEPluginVersion(string plugin) { return OblivionModManager.Scripting.ScriptFunctions._GetSKSEPluginVersion(plugin); }
        static public OblivionModManager.ScriptReturnData GetScriptReturnData() { return OblivionModManager.Scripting.ScriptFunctions._getsrd(); }
        static public string[] GetPluginList() { return OblivionModManager.Scripting.ScriptFunctions._getpluginList(); }
        static public string[] GetDataFileList() { return OblivionModManager.Scripting.ScriptFunctions._getdataFileList(); }
        static public string[] GetPlugins(string path, string pattern, bool recurse) { return OblivionModManager.Scripting.ScriptFunctions._GetPlugins(path, pattern, recurse); }
        static public string[] GetDataFiles(string path, string pattern, bool recurse) { return OblivionModManager.Scripting.ScriptFunctions._GetDataFiles(path, pattern, recurse); }
        static public string[] GetPluginFolders(string path, string pattern, bool recurse) { return OblivionModManager.Scripting.ScriptFunctions._GetPluginFolders(path, pattern, recurse); }
        static public string[] GetDataFolders(string path, string pattern, bool recurse) { return OblivionModManager.Scripting.ScriptFunctions._GetDataFolders(path, pattern, recurse); }
        static public string[] GetActiveEspNames() { return OblivionModManager.Scripting.ScriptFunctions._GetActiveEspNames(); }
        static public string[] GetActivePlugins() { return OblivionModManager.Scripting.ScriptFunctions._GetActiveEspNames(); }
        static public string[] GetExistingEspNames() { return OblivionModManager.Scripting.ScriptFunctions._GetExistingEspNames(); }
        static public string[] GetActiveOmodNames() { return OblivionModManager.Scripting.ScriptFunctions._GetActiveOmodNames(); }
        static public int[] Select(string[] items, string[] previews, string[] descs, string title, bool many)
        {
            string[] options = OblivionModManager.Scripting.ScriptFunctions._Select(items, previews, descs, title, many, false);
            int[] indexes = new int[options.Length];
            int j=0;
            foreach (string opt in options)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (opt == items[i]) indexes[j++] = i;
                }
            }
            return indexes;
        }
        static public string[] Select(string[] items, string[] previews, string[] descs, string title, bool many, bool atleastone) { return OblivionModManager.Scripting.ScriptFunctions._Select(items, previews, descs, title, many, atleastone); }
        static public void Message(string msg) { OblivionModManager.Scripting.ScriptFunctions._Message(msg); }
        static public void Message(string msg, string title) { OblivionModManager.Scripting.ScriptFunctions._Message(msg, title); }
        static public void DisplayImage(string path) { OblivionModManager.Scripting.ScriptFunctions._DisplayImage(path, null); }
        static public void DisplayImage(string path, string title) { OblivionModManager.Scripting.ScriptFunctions._DisplayImage(path, title); }
        static public void DisplayText(string path) { OblivionModManager.Scripting.ScriptFunctions._DisplayText(path, null); }
        static public void DisplayText(string path, string title) { OblivionModManager.Scripting.ScriptFunctions._DisplayText(path, title); }
        static public void LoadEarly(string plugin) { OblivionModManager.Scripting.ScriptFunctions._LoadEarly(plugin); }
        static public void LoadBefore(string plugin1, string plugin2) { OblivionModManager.Scripting.ScriptFunctions._ModLoadOrder(plugin1, plugin2, false); }
        static public void LoadAfter(string plugin1, string plugin2) { OblivionModManager.Scripting.ScriptFunctions._ModLoadOrder(plugin1, plugin2, true); }
        static public void SetNewLoadOrder(string[] plugins) { OblivionModManager.Scripting.ScriptFunctions._SetNewLoadOrder(plugins); }
        static public void UncheckEsp(string plugin) { OblivionModManager.Scripting.ScriptFunctions._UncheckEsp(plugin); }
        static public void SetDeactivationWarning(string plugin, OblivionModManager.DeactiveStatus warning) { OblivionModManager.Scripting.ScriptFunctions._SetDeactivationWarning(plugin, warning); }
        static public void ConflictsWith(string filename) { OblivionModManager.Scripting.ScriptFunctions._ConflictsWith(filename, 0, 0, 0, 0, null, OblivionModManager.ConflictLevel.MajorConflict, false); }
        static public void ConslictsWith(string filename, string comment) { OblivionModManager.Scripting.ScriptFunctions._ConflictsWith(filename, 0, 0, 0, 0, comment, OblivionModManager.ConflictLevel.MajorConflict, false); }
        static public void ConflictsWith(string filename, string comment, OblivionModManager.ConflictLevel level) { OblivionModManager.Scripting.ScriptFunctions._ConflictsWith(filename, 0, 0, 0, 0, comment, level, false); }
        static public void ConflictsWith(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion)
        {
            OblivionModManager.Scripting.ScriptFunctions._ConflictsWith(name, minMajorVersion, minMinorVersion, maxMajorVersion, maxMinorVersion, null, OblivionModManager.ConflictLevel.MajorConflict, false);
		}
        static public void ConflictsWith(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, string comment)
        {
            OblivionModManager.Scripting.ScriptFunctions._ConflictsWith(name, minMajorVersion, minMinorVersion, maxMajorVersion, maxMinorVersion, comment, OblivionModManager.ConflictLevel.MajorConflict, false);
		}
        static public void ConflictsWith(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, string comment, OblivionModManager.ConflictLevel level)
        {
			OblivionModManager.Scripting.ScriptFunctions._ConflictsWith(name, minMajorVersion, minMinorVersion, maxMajorVersion, maxMinorVersion, comment, level, false);
		}
        static public void ConflictsWith(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, string comment, OblivionModManager.ConflictLevel level, bool regex)
        {
            OblivionModManager.Scripting.ScriptFunctions._ConflictsWith(name, minMajorVersion, minMinorVersion, maxMajorVersion, maxMinorVersion, comment, level, regex);
        }
        static public void DependsOn(string filename) { OblivionModManager.Scripting.ScriptFunctions._DependsOn(filename, 0, 0, 0, 0, null, false); }
        static public void DependsOn(string filename, string comment) { OblivionModManager.Scripting.ScriptFunctions._DependsOn(filename, 0, 0, 0, 0, comment, false); }
        static public void DependsOn(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion)
        {
			OblivionModManager.Scripting.ScriptFunctions._DependsOn(name, minMajorVersion, minMinorVersion, maxMajorVersion, maxMinorVersion, null, false);
		}
        static public void DependsOn(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, string comment)
        {
			OblivionModManager.Scripting.ScriptFunctions._DependsOn(name, minMajorVersion, minMinorVersion, maxMajorVersion, maxMinorVersion, comment, false);
		}
        static public void DependsOn(string name, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, string comment, bool regex)
        {
            OblivionModManager.Scripting.ScriptFunctions._DependsOn(name, minMajorVersion, minMinorVersion, maxMajorVersion, maxMinorVersion, comment, regex);
        }
        static public void DontInstallAnyPlugins() { OblivionModManager.Scripting.ScriptFunctions._DontInstallAnyPlugins(); }
        static public void DontInstallAnyDataFiles() { OblivionModManager.Scripting.ScriptFunctions._DontInstallAnyDataFiles(); }
        static public void InstallAllPlugins() { OblivionModManager.Scripting.ScriptFunctions._InstallAllPlugins(); }
        static public void InstallAllDataFiles() { OblivionModManager.Scripting.ScriptFunctions._InstallAllDataFiles(); }
        static public void DontInstallPlugin(string name) { OblivionModManager.Scripting.ScriptFunctions._DontInstallPlugin( name); }
        static public void DontInstallDataFile(string name) { OblivionModManager.Scripting.ScriptFunctions._DontInstallDataFile(name); }
        static public void DontInstallDataFolder(string folder, bool recurse) { OblivionModManager.Scripting.ScriptFunctions._DontInstallDataFolder(folder, recurse); }
        static public void InstallPlugin(string name) { OblivionModManager.Scripting.ScriptFunctions._InstallPlugin(name); }
        static public void InstallFileFromFomod(string name) { OblivionModManager.Scripting.ScriptFunctions._InstallFileFromFomod(name); }
        static public void InstallFileFromFomod(string fromname, string toname) { OblivionModManager.Scripting.ScriptFunctions._InstallFileFromFomod(fromname, toname); }
        static public void InstallDataFile(string name) { OblivionModManager.Scripting.ScriptFunctions._InstallDataFile(name); }
        static public void InstallDataFolder(string folder, bool recurse) { OblivionModManager.Scripting.ScriptFunctions._InstallDataFolder(folder, recurse); }
        static public void CopyPlugin(string from, string to) { OblivionModManager.Scripting.ScriptFunctions._CopyPlugin(from, to); }
        static public void CopyDataFile(string from, string to)
        { 
            string str=from.ToLower();
            if (str.Contains(".esp") || str.Contains(".esm"))
                OblivionModManager.Scripting.ScriptFunctions._CopyPlugin(from, to);
            else
                OblivionModManager.Scripting.ScriptFunctions._CopyDataFile( from,  to);
        }
        static public void CopyDataFolder(string from, string to, bool recurse) { OblivionModManager.Scripting.ScriptFunctions._CopyDataFolder(from, to, recurse); }
        static public void PatchPlugin(string from, string to, bool create) { OblivionModManager.Scripting.ScriptFunctions._PatchPlugin(from, to, create); }
        static public void PatchDataFile(string from, string to, bool create) { OblivionModManager.Scripting.ScriptFunctions._PatchDataFile(from, to, create); }
        static public void RegisterBSA(string path) { OblivionModManager.Scripting.ScriptFunctions._RegisterBSA(path); }
        static public void UnregisterBSA(string path) { OblivionModManager.Scripting.ScriptFunctions._UnregisterBSA(path); }
        static public void EditINI(string section, string key, string value) { OblivionModManager.Scripting.ScriptFunctions._EditINI(section, key, value); }
        static public void EditShader(byte package, string name, string path) { OblivionModManager.Scripting.ScriptFunctions._EditShader(package, name, path); }
        static public void FatalError() { OblivionModManager.Scripting.ScriptFunctions._FatalError(); }
        static public void SetGMST(string file, string edid, string value) { OblivionModManager.Scripting.ScriptFunctions._SetGMST(file, edid, value); }
        static public void SetGlobal(string file, string edid, string value) { OblivionModManager.Scripting.ScriptFunctions._SetGlobal(file, edid, value); }
        static public void SetPluginByte(string file, long offset, byte value) { OblivionModManager.Scripting.ScriptFunctions._SetPluginByte(file, offset, value); }
        static public void SetPluginShort(string file, long offset, short value) { OblivionModManager.Scripting.ScriptFunctions._SetPluginShort(file, offset, value); }
        static public void SetPluginInt(string file, long offset, int value) { OblivionModManager.Scripting.ScriptFunctions._SetPluginInt(file, offset, value); }
        static public void SetPluginLong(string file, long offset, long value) { OblivionModManager.Scripting.ScriptFunctions._SetPluginLong(file, offset, value); }
        static public void SetPluginFloat(string file, long offset, float value) { OblivionModManager.Scripting.ScriptFunctions._SetPluginFloat(file, offset, value); }
        static public string InputString() { return OblivionModManager.Scripting.ScriptFunctions._InputString("", ""); }
        static public string InputString(string title) { return OblivionModManager.Scripting.ScriptFunctions._InputString(title, ""); }
        static public string InputString(string title, string initial) { return OblivionModManager.Scripting.ScriptFunctions._InputString(title, initial); }
        static public string ReadINI(string section, string value) { return OblivionModManager.Scripting.ScriptFunctions._ReadINI(section, value); }
        static public string ReadRendererInfo(string value) { return OblivionModManager.Scripting.ScriptFunctions._ReadRendererInfo(value); }
        static public void EditXMLLine(string file, int line, string value){ OblivionModManager.Scripting.ScriptFunctions._EditXMLLine(file, line, value); }
        static public void EditXMLReplace(string file, string find, string replace) { OblivionModManager.Scripting.ScriptFunctions._EditXMLReplace(file, find, replace); }
        static public System.Windows.Forms.Form CreateCustomDialog() { return OblivionModManager.Scripting.ScriptFunctions._CreateCustomDialog(); }
        static public byte[] ReadDataFile(string file) { return OblivionModManager.Scripting.ScriptFunctions._ReadDataFile(file); }
        static public byte[] GetExistingDataFile(string file) { return OblivionModManager.Scripting.ScriptFunctions._ReadExistingDataFile(file); }
        static public byte[] GetFileFromFomod(string file) { return OblivionModManager.Scripting.ScriptFunctions._ReadDataFile(file); }
        static public byte[] ReadExistingDataFile(string file) { return OblivionModManager.Scripting.ScriptFunctions._ReadExistingDataFile(file); }
        static public byte[] GetDataFileFromBSA(string file) { return OblivionModManager.Scripting.ScriptFunctions._GetDataFileFromBSA(file); }
        static public byte[] GetDataFileFromBSA(string bsa, string file) { return OblivionModManager.Scripting.ScriptFunctions._GetDataFileFromBSA(bsa, file); }
        static public void GenerateDataFile(string file, byte[] data) { OblivionModManager.Scripting.ScriptFunctions._GenerateNewDataFile(file, data); }
        static public void GenerateNewDataFile(string file, byte[] data) { OblivionModManager.Scripting.ScriptFunctions._GenerateNewDataFile(file, data); }
        static public void CancelDataFileCopy(string file) { OblivionModManager.Scripting.ScriptFunctions._CancelDataFileCopy(file); }
        static public void CancelDataFolderCopy(string folder) { OblivionModManager.Scripting.ScriptFunctions._CancelDataFolderCopy(folder); }
        static public void GenerateBSA(string file, string path, string prefix, int cRatio, int cLevel) { OblivionModManager.Scripting.ScriptFunctions._GenerateBSA(file, path, prefix, cRatio, cLevel); }
        static public bool IsSimulation() { return OblivionModManager.Scripting.ScriptFunctions._IsSimulation(); }
        static public bool PerformBasicInstall() { return OblivionModManager.Scripting.ScriptFunctions._PerformBasicInstall(); }
        public static System.Windows.Forms.Form CreateCustomForm()
        {
            return new System.Windows.Forms.Form();
        }
    }

/*
    /// <summary>
    /// Implements the functions availabe to C# scripts.
    /// </summary>
    public class CSharpScriptFunctionProxy : ScriptFunctionProxy
    {
        #region Constructors

        /// <summary>
        /// A simple constructor that initializes the object with the given values.
        /// </summary>
        /// <param name="p_modMod">The mod for which the script is running.</param>
        /// <param name="p_gmdGameMode">The game mode currently being managed.</param>
        /// <param name="p_eifEnvironmentInfo">The application's envrionment info.</param>
        /// <param name="p_igpInstallers">The utility class to use to install the mod items.</param>
        /// <param name="p_uipUIProxy">The UI manager to use to interact with UI elements.</param>
        public CSharpScriptFunctionProxy(IMod p_modMod, IGameMode p_gmdGameMode, IEnvironmentInfo p_eifEnvironmentInfo, InstallerGroup p_igpInstallers, UIUtil p_uipUIProxy)
            : base(p_modMod, p_gmdGameMode, p_eifEnvironmentInfo, p_igpInstallers, p_uipUIProxy)
        {
        }

        #endregion

        #region File Management

        /// <summary>
        /// Installs the specified file from the mod to the specified location on the file system.
        /// </summary>
        /// <remarks>
        /// This is the legacy form of <see cref="ScriptFunctionProxy.InstallFileFromMod(string, string)"/>. It now just calls
        /// <see cref="ScriptFunctionProxy.InstallFileFromMod(string, string)"/>.
        /// </remarks>
        /// <param name="p_strFrom">The path of the file in the mod to install.</param>
        /// <param name="p_strTo">The path on the file system where the file is to be created.</param>
        /// <returns><c>true</c> if the file was written; <c>false</c> otherwise.</returns>
        /// <seealso cref="ScriptFunctionProxy.InstallFileFromMod(string, string)"/>
        public bool CopyDataFile(string p_strFrom, string p_strTo)
        {
            return InstallFileFromMod(p_strFrom, p_strTo);
        }

        #endregion

        #region UI

        #region Select

        /// <summary>
        /// Displays a selection form to the user.
        /// </summary>
        /// <param name="p_sopOptions">The options from which to select.</param>
        /// <param name="p_strTitle">The title of the selection form.</param>
        /// <param name="p_booSelectMany">Whether more than one item can be selected.</param>
        /// <returns>The indices of the selected items.</returns>
        public int[] Select(SelectOption[] p_sopOptions, string p_strTitle, bool p_booSelectMany)
        {
            bool booHasPreviews = false;
            bool booHasDescriptions = false;
            foreach (SelectOption so in p_sopOptions)
            {
                if (so.Preview != null)
                    booHasPreviews = true;
                if (so.Desc != null)
                    booHasDescriptions = true;
            }
            string[] strItems = new string[p_sopOptions.Length];
            Image[] imgPreviews = booHasPreviews ? new Image[p_sopOptions.Length] : null;
            string[] strDescriptions = booHasDescriptions ? new string[p_sopOptions.Length] : null;
            for (int i = 0; i < p_sopOptions.Length; i++)
            {
                strItems[i] = p_sopOptions[i].Item;
                if (booHasPreviews)
                    imgPreviews[i] = new ExtendedImage(Mod.GetFile(p_sopOptions[i].Preview));
                if (booHasDescriptions)
                    strDescriptions[i] = p_sopOptions[i].Desc;
            }
            return Select(strItems, imgPreviews, strDescriptions, p_strTitle, p_booSelectMany);
        }

        /// <summary>
        /// Displays a selection form to the user.
        /// </summary>
        /// <remarks>
        /// The items, previews, and descriptions are repectively ordered. In other words,
        /// the i-th item in <paramref name="p_strItems"/> uses the i-th preview in
        /// <paramref name="p_strPreviewPaths"/> and the i-th description in <paramref name="p_strDescriptions"/>.
        /// 
        /// Similarly, the idices return as results correspond to the indices of the items in
        /// <paramref name="p_strItems"/>.
        /// </remarks>
        /// <param name="p_strItems">The items from which to select.</param>
        /// <param name="p_strPreviewPaths">The preview image file names for the items.</param>
        /// <param name="p_strDescriptions">The descriptions of the items.</param>
        /// <param name="p_strTitle">The title of the selection form.</param>
        /// <param name="p_booSelectMany">Whether more than one item can be selected.</param>
        /// <returns>The indices of the selected items.</returns>
        public int[] Select(string[] p_strItems, string[] p_strPreviewPaths, string[] p_strDescriptions, string p_strTitle, bool p_booSelectMany)
        {
            Image[] imgPreviews = null;
            if (p_strPreviewPaths != null)
            {
                imgPreviews = new Image[p_strPreviewPaths.Length];
                for (Int32 i = 0; i < p_strPreviewPaths.Length; i++)
                    if (!String.IsNullOrEmpty(p_strPreviewPaths[i]))
                        imgPreviews[i] = new ExtendedImage(Mod.GetFile(p_strPreviewPaths[i]));
            }
            return Select(p_strItems, imgPreviews, p_strDescriptions, p_strTitle, p_booSelectMany);
        }

        /// <summary>
        /// Displays a selection form to the user.
        /// </summary>
        /// <remarks>
        /// The items, previews, and descriptions are repectively ordered. In other words,
        /// the i-th item in <paramref name="p_strItems"/> uses the i-th preview in
        /// <paramref name="p_imgPreviews"/> and the i-th description in <paramref name="p_strDescriptions"/>.
        /// 
        /// Similarly, the idices return as results correspond to the indices of the items in
        /// <paramref name="p_strItems"/>.
        /// </remarks>
        /// <param name="p_strItems">The items from which to select.</param>
        /// <param name="p_imgPreviews">The preview images for the items.</param>
        /// <param name="p_strDescriptions">The descriptions of the items.</param>
        /// <param name="p_strTitle">The title of the selection form.</param>
        /// <param name="p_booSelectMany">Whether more than one item can be selected.</param>
        /// <returns>The indices of the selected items.</returns>
        public int[] Select(string[] p_strItems, Image[] p_imgPreviews, string[] p_strDescriptions, string p_strTitle, bool p_booSelectMany)
        {
            List<SelectOption> lstOptions = new List<SelectOption>();
            for (Int32 i = 0; i < p_strItems.Length; i++)
            {
                string strDescription = p_strDescriptions.IsNullOrEmpty() ? null : p_strDescriptions[i];
                Image imgPreview = p_imgPreviews.IsNullOrEmpty() ? null : p_imgPreviews[i];
                lstOptions.Add(new SelectOption(p_strItems[i], false, strDescription, imgPreview));
            }
            string[] strSelections = UIManager.Select(lstOptions, p_strTitle, p_booSelectMany);
            List<Int32> lstSelectionIndices = new List<Int32>();
            foreach (string strSelection in strSelections)
                lstSelectionIndices.Add(Array.IndexOf(p_strItems, strSelection));
            return lstSelectionIndices.ToArray();
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// Implements the functions availabe to scripts.
    /// </summary>
    /// <remarks>
    /// The proxy allows sandboxed scripts to call functions that can perform
    /// actions outside of the sandbox.
    /// </remarks>
    public class ScriptFunctionProxy : MarshalByRefObject
    {
        #region Events

        /// <summary>
        /// Raised when a task in the set has started.
        /// </summary>
        /// <remarks>
        /// The argument passed with the event args is the task that
        /// has been started.
        /// </remarks>
        public event EventHandler<EventArgs<IBackgroundTask>> TaskStarted = delegate { };

        #endregion

        #region Properties

        /// <summary>
        /// Gets the mod for which the script is running.
        /// </summary>
        /// <value>The mod for which the script is running.</value>
        protected IMod Mod { get; private set; }

        /// <summary>
        /// Gets the game mode currently being managed.
        /// </summary>
        /// <value>The game mode currently being managed.</value>
        protected IGameMode GameMode { get; private set; }

        /// <summary>
        /// Gets the application's envrionment info.
        /// </summary>
        /// <value>The application's envrionment info.</value>
        protected IEnvironmentInfo EnvironmentInfo { get; private set; }

        /// <summary>
        /// Gets the installer group to use to install mod items.
        /// </summary>
        /// <value>The installer group to use to install mod items.</value>
        protected InstallerGroup Installers { get; private set; }

        /// <summary>
        /// Gets the manager to use to display UI elements.
        /// </summary>
        /// <value>The manager to use to display UI elements.</value>
        protected UIUtil UIManager { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// A simple constructor that initializes the object with the given values.
        /// </summary>
        /// <param name="p_modMod">The mod for which the script is running.</param>
        /// <param name="p_gmdGameMode">The game mode currently being managed.</param>
        /// <param name="p_eifEnvironmentInfo">The application's envrionment info.</param>
        /// <param name="p_igpInstallers">The utility class to use to install the mod items.</param>
        /// <param name="p_uipUIProxy">The UI manager to use to interact with UI elements.</param>
        public ScriptFunctionProxy(IMod p_modMod, IGameMode p_gmdGameMode, IEnvironmentInfo p_eifEnvironmentInfo, InstallerGroup p_igpInstallers, UIUtil p_uipUIProxy)
        {
            Mod = p_modMod;
            GameMode = p_gmdGameMode;
            EnvironmentInfo = p_eifEnvironmentInfo;
            Installers = p_igpInstallers;
            UIManager = p_uipUIProxy;
        }

        #endregion

        #region Event Raising

        /// <summary>
        /// Raises the <see cref="TaskStarted"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs{IBackgroundTask}"/> describing the task that was started.</param>
        protected virtual void OnTaskStarted(EventArgs<IBackgroundTask> e)
        {
            TaskStarted(this, e);
        }

        /// <summary>
        /// Raises the <see cref="TaskStarted"/> event.
        /// </summary>
        /// <param name="p_bgtTask">The task that was started.</param>
        protected void OnTaskStarted(IBackgroundTask p_bgtTask)
        {
            OnTaskStarted(new EventArgs<IBackgroundTask>(p_bgtTask));
        }

        #endregion

        #region Installation

        /// <summary>
        /// Performs a basic install of the mod.
        /// </summary>
        /// <remarks>
        /// A basic install installs all of the file in the mod to the Data directory
        /// or activates all esp and esm files.
        /// </remarks>
        /// <returns><c>true</c> if the installation succeed;
        /// <c>false</c> otherwise.</returns>
        public bool PerformBasicInstall()
        {
            BasicInstallTask bitTask = new BasicInstallTask(Mod, GameMode, Installers.FileInstaller, Installers.PluginManager);
            OnTaskStarted(bitTask);
            return bitTask.Execute();
        }

        #endregion

        #region File Management

        /// <summary>
        /// Installs the files in the specified folder from the mod to the file system.
        /// </summary>
        /// <param name="p_strFrom">The path of the folder in the mod containing the files to install.</param>
        /// <param name="p_booRecurse">Whether to install all files in all subfolders.</param>
        /// <returns><c>true</c> if the file was written; <c>false</c> otherwise.</returns>
        public bool InstallFolderFromMod(string p_strFrom, bool p_booRecurse)
        {
            return InstallFolderFromMod(p_strFrom, p_strFrom, p_booRecurse);
        }

        /// <summary>
        /// Installs the files in the specified folder from the mod to the specified location on the file system.
        /// </summary>
        /// <param name="p_strFrom">The path of the folder in the mod containing the files to install.</param>
        /// <param name="p_strTo">The path on the file system where the files are to be created.</param>
        /// <param name="p_booRecurse">Whether to install all files in all subfolders.</param>
        /// <returns><c>true</c> if the file was written; <c>false</c> otherwise.</returns>
        public bool InstallFolderFromMod(string p_strFrom, string p_strTo, bool p_booRecurse)
        {
            string strFrom = p_strFrom.Trim().Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar).ToLowerInvariant();
            if (!strFrom.EndsWith(Path.DirectorySeparatorChar.ToString()))
                strFrom += Path.DirectorySeparatorChar;
            string strTo = p_strTo.Trim().Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            if ((strTo.Length > 0) && (!strTo.EndsWith(Path.DirectorySeparatorChar.ToString())))
                strTo += Path.DirectorySeparatorChar;
            foreach (string strMODFile in GetModFileList(strFrom, p_booRecurse))
            {
                string strNewFileName = strMODFile.Substring(strFrom.Length);
                if (!InstallFileFromMod(strMODFile, Path.Combine(strTo, strNewFileName)))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Installs the specified file from the mod to the specified location on the file system.
        /// </summary>
        /// <param name="p_strFrom">The path of the file in the mod to install.</param>
        /// <param name="p_strTo">The path on the file system where the file is to be created.</param>
        /// <returns><c>true</c> if the file was written; <c>false</c> otherwise.</returns>
        public virtual bool InstallFileFromMod(string p_strFrom, string p_strTo)
        {
            string strFixedTo = GameMode.GetModFormatAdjustedPath(Mod.Format, p_strTo);
            bool booSuccess = false;
            try
            {
                new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
                booSuccess = Installers.FileInstaller.InstallFileFromMod(p_strFrom, strFixedTo);
            }
            finally
            {
                PermissionSet.RevertAssert();
            }
            return booSuccess;
        }

        /// <summary>
        /// Installs the speified file from the mod to the file system.
        /// </summary>
        /// <param name="p_strFile">The path of the file to install.</param>
        /// <returns><c>true</c> if the file was written; <c>false</c> otherwise.</returns>
        public bool InstallFileFromMod(string p_strFile)
        {
            return InstallFileFromMod(p_strFile, p_strFile);
        }

        /// <summary>
        /// Retrieves the list of files in the mod.
        /// </summary>
        /// <returns>The list of files in the mod.</returns>
        public string[] GetModFileList()
        {
            string[] strFiles = null;
            try
            {
                new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
                strFiles = Mod.GetFileList().ToArray();
            }
            finally
            {
                PermissionSet.RevertAssert();
            }
            for (Int32 i = strFiles.Length - 1; i >= 0; i--)
                strFiles[i] = strFiles[i].Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            return strFiles;
        }

        /// <summary>
        /// Retrieves the list of files in the specified folder in the mod.
        /// </summary>
        /// <param name="p_strFolder">The folder whose file list is to be retrieved.</param>
        /// <param name="p_booRecurse">Whether to return files that are in subdirectories of the given directory.</param>
        /// <returns>The list of files in the specified folder in the mod.</returns>
        public string[] GetModFileList(string p_strFolder, bool p_booRecurse)
        {
            string[] strFiles = null;
            try
            {
                new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
                strFiles = Mod.GetFileList(p_strFolder, p_booRecurse).ToArray();
            }
            finally
            {
                PermissionSet.RevertAssert();
            }
            for (Int32 i = strFiles.Length - 1; i >= 0; i--)
                strFiles[i] = strFiles[i].Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            return strFiles;
        }

        /// <summary>
        /// Retrieves the specified file from the mod.
        /// </summary>
        /// <param name="p_strFile">The file to retrieve.</param>
        /// <returns>The requested file data.</returns>
        public byte[] GetFileFromMod(string p_strFile)
        {
            byte[] bteFile = null;
            try
            {
                new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
                bteFile = Mod.GetFile(p_strFile);
            }
            finally
            {
                PermissionSet.RevertAssert();
            }
            return bteFile;
        }

        /// <summary>
        /// Gets a filtered list of all files in a user's Data directory.
        /// </summary>
        /// <param name="p_strPath">The subdirectory of the Data directory from which to get the listing.</param>
        /// <param name="p_strPattern">The pattern against which to filter the file paths.</param>
        /// <param name="p_booAllFolders">Whether or not to search through subdirectories.</param>
        /// <returns>A filtered list of all files in a user's Data directory.</returns>
        public string[] GetExistingDataFileList(string p_strPath, string p_strPattern, bool p_booAllFolders)
        {
            string strFixedPath = GameMode.GetModFormatAdjustedPath(Mod.Format, p_strPath);
            return Installers.DataFileUtility.GetExistingDataFileList(strFixedPath, p_strPattern, p_booAllFolders);
        }

        /// <summary>
        /// Determines if the specified file exists in the user's Data directory.
        /// </summary>
        /// <param name="p_strPath">The path of the file whose existence is to be verified.</param>
        /// <returns><c>true</c> if the specified file exists; <c>false</c>
        /// otherwise.</returns>
        public bool DataFileExists(string p_strPath)
        {
            string strFixedPath = GameMode.GetModFormatAdjustedPath(Mod.Format, p_strPath);
            return Installers.DataFileUtility.DataFileExists(strFixedPath);
        }

        /// <summary>
        /// Gets the speified file from the user's Data directory.
        /// </summary>
        /// <param name="p_strPath">The path of the file to retrieve.</param>
        /// <returns>The specified file, or <c>null</c> if the file does not exist.</returns>
        public byte[] GetExistingDataFile(string p_strPath)
        {
            string strFixedPath = GameMode.GetModFormatAdjustedPath(Mod.Format, p_strPath);
            return Installers.DataFileUtility.GetExistingDataFile(strFixedPath);
        }

        /// <summary>
        /// Writes the file represented by the given byte array to the given path.
        /// </summary>
        /// <remarks>
        /// This method writes the given data as a file at the given path. If the file
        /// already exists the user is prompted to overwrite the file.
        /// </remarks>
        /// <param name="p_strPath">The path where the file is to be created.</param>
        /// <param name="p_bteData">The data that is to make up the file.</param>
        /// <returns><c>true</c> if the file was written; <c>false</c> otherwise.</returns>
        public bool GenerateDataFile(string p_strPath, byte[] p_bteData)
        {
            string strFixedPath = GameMode.GetModFormatAdjustedPath(Mod.Format, p_strPath);
            return Installers.FileInstaller.GenerateDataFile(strFixedPath, p_bteData);
        }

        #endregion

        #region UI

        #region MessageBox

        /// <summary>
        /// Shows a message box with the given message.
        /// </summary>
        /// <param name="p_strMessage">The message to display in the message box.</param>
        public void MessageBox(string p_strMessage)
        {
            UIManager.ShowMessageBox(p_strMessage);
        }

        /// <summary>
        /// Shows a message box with the given message and title.
        /// </summary>
        /// <param name="p_strMessage">The message to display in the message box.</param>
        /// <param name="p_strTitle">The message box's title, display in the title bar.</param>
        public void MessageBox(string p_strMessage, string p_strTitle)
        {
            UIManager.ShowMessageBox(p_strMessage, p_strTitle);
        }

        /// <summary>
        /// Shows a message box with the given message, title, and buttons.
        /// </summary>
        /// <param name="p_strMessage">The message to display in the message box.</param>
        /// <param name="p_strTitle">The message box's title, display in the title bar.</param>
        /// <param name="p_mbbButtons">The buttons to show in the message box.</param>
        public DialogResult MessageBox(string p_strMessage, string p_strTitle, MessageBoxButtons p_mbbButtons)
        {
            return UIManager.ShowMessageBox(p_strMessage, p_strTitle, p_mbbButtons);
        }

        /// <summary>
        /// Shows a message box with the given message, title, buttons, and icon.
        /// </summary>
        /// <param name="p_strMessage">The message to display in the message box.</param>
        /// <param name="p_strTitle">The message box's title, display in the title bar.</param>
        /// <param name="p_mbbButtons">The buttons to show in the message box.</param>
        /// <param name="p_mdiIcon">The icon to display in the message box.</param>
        public DialogResult MessageBox(string p_strMessage, string p_strTitle, MessageBoxButtons p_mbbButtons, MessageBoxIcon p_mdiIcon)
        {
            return UIManager.ShowMessageBox(p_strMessage, p_strTitle, p_mbbButtons, p_mdiIcon);
        }

        #endregion

        #region ExtendedMessageBox

        /// <summary>
        /// Shows an extended message box with the given message, title, details, buttons, and icon.
        /// </summary>
        /// <param name="p_strMessage">The message to display in the message box.</param>
        /// <param name="p_strTitle">The message box's title, displayed in the title bar.</param>
        /// <param name="p_strDetails">The message box's details, displayed in the details area.</param>
        /// <param name="p_mbbButtons">The buttons to show in the message box.</param>
        /// <param name="p_mdiIcon">The icon to display in the message box.</param>
        public DialogResult ExtendedMessageBox(string p_strMessage, string p_strTitle, string p_strDetails, MessageBoxButtons p_mbbButtons, MessageBoxIcon p_mdiIcon)
        {
            return UIManager.ShowExtendedMessageBox(p_strMessage, p_strTitle, p_strDetails, p_mbbButtons, p_mdiIcon);
        }

        #endregion

        #region Select

        /// <summary>
        /// Displays a selection form to the user.
        /// </summary>
        /// <param name="p_lstOptions">The options from which to select.</param>
        /// <param name="p_strTitle">The title of the selection form.</param>
        /// <param name="p_booSelectMany">Whether more than one items can be selected.</param>
        /// <returns>The selected option names.</returns>
        public string[] Select(IList<SelectOption> p_lstOptions, string p_strTitle, bool p_booSelectMany)
        {
            return UIManager.Select(p_lstOptions, p_strTitle, p_booSelectMany);
        }

        #endregion

        #endregion

        #region Version Checking

        /// <summary>
        /// Gets the version of the mod manager.
        /// </summary>
        /// <returns>The version of the mod manager.</returns>
        public virtual Version GetModManagerVersion()
        {
            return EnvironmentInfo.ApplicationVersion;
        }

        /// <summary>
        /// Gets the version of the game that is installed.
        /// </summary>
        /// <returns>The version of the game, or <c>null</c> if Fallout
        /// is not installed.</returns>
        public Version GetGameVersion()
        {
            return GameMode.GameVersion;
        }

        #endregion

        #region Plugin Management

        /// <summary>
        /// The returns a list of the paths of the given plugins, relative to the game mode's installation path.
        /// </summary>
        /// <param name="p_lstPlugins">The plugins whose paths are to be made relative.</param>
        /// <returns>A list of the paths of the given plugins, relative to the game mode's installation path.</returns>
        protected string[] RelativizePluginPaths(IList<Plugin> p_lstPlugins)
        {
            string[] strPlugins = new string[p_lstPlugins.Count];
            string strInstallationPath = Path.Combine(GameMode.GameModeEnvironmentInfo.InstallationPath, GameMode.GetModFormatAdjustedPath(Mod.Format, null));
            Int32 intTrimLength = strInstallationPath.Trim(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar).Length + 1;
            for (Int32 i = 0; i < p_lstPlugins.Count; i++)
                strPlugins[i] = p_lstPlugins[i].Filename.Remove(0, intTrimLength);
            return strPlugins;
        }

        /// <summary>
        /// Gets a list of all installed plugins.
        /// </summary>
        /// <returns>A list of all installed plugins.</returns>
        public string[] GetAllPlugins()
        {
            return RelativizePluginPaths(Installers.PluginManager.ManagedPlugins);
        }

        #region Plugin Activation Management

        /// <summary>
        /// Retrieves a list of currently active plugins.
        /// </summary>
        /// <returns>A list of currently active plugins.</returns>
        public string[] GetActivePlugins()
        {
            return RelativizePluginPaths(Installers.PluginManager.ActivePlugins);
        }

        /// <summary>
        /// Sets the activated status of a plugin (i.e., and esp or esm file).
        /// </summary>
        /// <param name="p_strPluginPath">The path to the plugin to activate or deactivate.</param>
        /// <param name="p_booActivate">Whether to activate the plugin.</param>
        public void SetPluginActivation(string p_strPluginPath, bool p_booActivate)
        {
            string strFixedPath = GameMode.GetModFormatAdjustedPath(Mod.Format, p_strPluginPath);
            Installers.PluginManager.SetPluginActivation(strFixedPath, p_booActivate);
        }

        #endregion

        #region Load Order Management

        /// <summary>
        /// Sets the load order of the specifid plugin.
        /// </summary>
        /// <param name="p_strPlugin">The path to the plugin file whose load order is to be set.</param>
        /// <param name="p_intNewIndex">The new load order index of the plugin.</param>
        protected void DoSetPluginOrderIndex(string p_strPlugin, int p_intNewIndex)
        {

            string strFixedPath = Path.Combine(GameMode.GameModeEnvironmentInfo.InstallationPath, GameMode.GetModFormatAdjustedPath(Mod.Format, p_strPlugin));
            Plugin plgPlugin = Installers.PluginManager.ManagedPlugins.Find(x => x.Filename.Equals(strFixedPath, StringComparison.OrdinalIgnoreCase));
            Installers.PluginManager.SetPluginOrderIndex(plgPlugin, p_intNewIndex);
        }

        /// <summary>
        /// Sets the load order of the specifid plugin.
        /// </summary>
        /// <param name="p_strPlugin">The path to the plugin file whose load order is to be set.</param>
        /// <param name="p_intNewIndex">The new load order index of the plugin.</param>
        public void SetPluginOrderIndex(string p_strPlugin, int p_intNewIndex)
        {
            DoSetPluginOrderIndex(p_strPlugin, p_intNewIndex);
        }

        /// <summary>
        /// Sets the load order of the plugins.
        /// </summary>
        /// <remarks>
        /// Each plugin will be moved from its current index to its indices' position
        /// in <paramref name="p_intPlugins"/>.
        /// </remarks>
        /// <param name="p_intPlugins">The new load order of the plugins. Each entry in this array
        /// contains the current index of a plugin. This array must contain all current indices.</param>
        protected void DoSetLoadOrder(int[] p_intPlugins)
        {
            List<Plugin> lstPlugins = new List<Plugin>(Installers.PluginManager.ManagedPlugins);
            if (p_intPlugins.Length != lstPlugins.Count)
                throw new ArgumentException("Length of new load order array was different to the total number of plugins");

            for (int i = 0; i < p_intPlugins.Length; i++)
                if (p_intPlugins[i] < 0 || p_intPlugins[i] >= p_intPlugins.Length)
                    throw new IndexOutOfRangeException("A plugin index was out of range");

            for (int i = 0; i < lstPlugins.Count; i++)
                Installers.PluginManager.SetPluginOrderIndex(lstPlugins[i], i);
        }

        /// <summary>
        /// Sets the load order of the plugins.
        /// </summary>
        /// <remarks>
        /// Each plugin will be moved from its current index to its indices' position
        /// in <paramref name="p_intPlugins"/>.
        /// </remarks>
        /// <param name="p_intPlugins">The new load order of the plugins. Each entry in this array
        /// contains the current index of a plugin. This array must contain all current indices.</param>
        public void SetLoadOrder(int[] p_intPlugins)
        {
            DoSetLoadOrder(p_intPlugins);
        }

        /// <summary>
        /// Moves the specified plugins to the given position in the load order.
        /// </summary>
        /// <remarks>
        /// Note that the order of the given list of plugins is not maintained. They are re-ordered
        /// to be in the same order as they are in the before-operation load order. This, I think,
        /// is somewhat counter-intuitive and may change, though likely not so as to not break
        /// backwards compatibility.
        /// </remarks>
        /// <param name="p_intPlugins">The list of plugins to move to the given position in the
        /// load order. Each entry in this array contains the current index of a plugin.</param>
        /// <param name="p_intPosition">The position in the load order to which to move the specified
        /// plugins.</param>
        protected void DoSetLoadOrder(int[] p_intPlugins, int p_intPosition)
        {
            List<Plugin> lstPlugins = new System.Collections.Generic.List<Plugin>(Installers.PluginManager.ManagedPlugins);
            Array.Sort<int>(p_intPlugins);

            Int32 intLoadOrder = 0;
            for (int i = 0; i < p_intPosition; i++)
            {
                if (Array.BinarySearch<int>(p_intPlugins, i) >= 0)
                    continue;
                Installers.PluginManager.SetPluginOrderIndex(lstPlugins[i], intLoadOrder++);
            }
            for (int i = 0; i < p_intPlugins.Length; i++)
                Installers.PluginManager.SetPluginOrderIndex(lstPlugins[p_intPlugins[i]], intLoadOrder++);
            for (int i = p_intPosition; i < lstPlugins.Count; i++)
            {
                if (Array.BinarySearch<int>(p_intPlugins, i) >= 0)
                    continue;
                Installers.PluginManager.SetPluginOrderIndex(lstPlugins[i], intLoadOrder++);
            }
        }

        /// <summary>
        /// Moves the specified plugins to the given position in the load order.
        /// </summary>
        /// <remarks>
        /// Note that the order of the given list of plugins is not maintained. They are re-ordered
        /// to be in the same order as they are in the before-operation load order. This, I think,
        /// is somewhat counter-intuitive and may change, though likely not so as to not break
        /// backwards compatibility.
        /// </remarks>
        /// <param name="p_intPlugins">The list of plugins to move to the given position in the
        /// load order. Each entry in this array contains the current index of a plugin.</param>
        /// <param name="p_intPosition">The position in the load order to which to move the specified
        /// plugins.</param>
        public void SetLoadOrder(int[] p_intPlugins, int p_intPosition)
        {
            DoSetLoadOrder(p_intPlugins, p_intPosition);
        }

        /// <summary>
        /// Orders the plugins such that the specified plugins are in the specified
        /// order.
        /// </summary>
        /// <remarks>
        /// The given plugins may not end up consecutively ordered.
        /// </remarks>
        /// <param name="p_strRelativelyOrderedPlugins">The plugins to order relative to one another.</param>
        public void SetRelativeLoadOrder(string[] p_strRelativelyOrderedPlugins)
        {
            if (p_strRelativelyOrderedPlugins.Length == 0)
                return;
            System.Collections.Generic.List<string> lstRelativelyOrderedPlugins = new System.Collections.Generic.List<string>();
            foreach (string strPlugin in p_strRelativelyOrderedPlugins)
                lstRelativelyOrderedPlugins.Add(GameMode.GetModFormatAdjustedPath(Mod.Format, strPlugin));

            Plugin plgCurrent = null;
            Int32 intInitialIndex = 0;
            while (((plgCurrent = Installers.PluginManager.GetRegisteredPlugin(lstRelativelyOrderedPlugins[intInitialIndex])) == null) && (intInitialIndex < lstRelativelyOrderedPlugins.Count))
                intInitialIndex++;
            if (plgCurrent == null)
                return;
            for (Int32 i = intInitialIndex + 1; i < lstRelativelyOrderedPlugins.Count; i++)
            {
                Plugin plgNext = Installers.PluginManager.GetRegisteredPlugin(lstRelativelyOrderedPlugins[i]);
                if (plgNext == null)
                    continue;
                Int32 intNextPosition = Installers.PluginManager.GetPluginOrderIndex(plgNext);
                //we have to set this value every time, instead of caching the value (by
                // declaring Int32 intCurrentPosition outside of the for loop) because
                // calling Installers.PluginManager.SetPluginOrderIndex() does not guarantee
                // that the load order will change. for example trying to order an ESM
                // after an ESP file will result in no change, and will mean the intCurrentPosition
                // we are dead reckoning will be wrong
                Int32 intCurrentPosition = Installers.PluginManager.GetPluginOrderIndex(plgCurrent);
                if (intNextPosition > intCurrentPosition)
                {
                    plgCurrent = plgNext;
                    continue;
                }
                Installers.PluginManager.SetPluginOrderIndex(plgNext, intCurrentPosition + 1);
                //if the reorder worked, we have a new current, otherwise the old one is still the
                // correct current.
                if (intNextPosition != Installers.PluginManager.GetPluginOrderIndex(plgNext))
                    plgCurrent = plgNext;
            }
        }

        #endregion

        #endregion

        #region Ini File Value Management

        #region Ini File Value Retrieval

        /// <summary>
        /// Retrieves the specified settings value as a string.
        /// </summary>
        /// <param name="p_strSettingsFileName">The name of the settings file from which to retrieve the value.</param>
        /// <param name="p_strSection">The section containing the value to retrieve.</param>
        /// <param name="p_strKey">The key of the value to retrieve.</param>
        /// <returns>The specified value as a string.</returns>
        public string GetIniString(string p_strSettingsFileName, string p_strSection, string p_strKey)
        {
            return Installers.IniInstaller.GetIniString(p_strSettingsFileName, p_strSection, p_strKey);
        }

        /// <summary>
        /// Retrieves the specified settings value as an integer.
        /// </summary>
        /// <param name="p_strSettingsFileName">The name of the settings file from which to retrieve the value.</param>
        /// <param name="p_strSection">The section containing the value to retrieve.</param>
        /// <param name="p_strKey">The key of the value to retrieve.</param>
        /// <returns>The specified value as an integer.</returns>
        public Int32 GetIniInt(string p_strSettingsFileName, string p_strSection, string p_strKey)
        {
            return Installers.IniInstaller.GetIniInt(p_strSettingsFileName, p_strSection, p_strKey);
        }

        #endregion

        #region Ini Editing

        /// <summary>
        /// Sets the specified value in the specified Ini file to the given value.
        /// </summary>
        /// <param name="p_strSettingsFileName">The name of the settings file to edit.</param>
        /// <param name="p_strSection">The section in the Ini file to edit.</param>
        /// <param name="p_strKey">The key in the Ini file to edit.</param>
        /// <param name="p_strValue">The value to which to set the key.</param>
        /// <returns><c>true</c> if the value was set; <c>false</c>
        /// if the user chose not to overwrite the existing value.</returns>
        public bool EditIni(string p_strSettingsFileName, string p_strSection, string p_strKey, string p_strValue)
        {
            return Installers.IniInstaller.EditIni(p_strSettingsFileName, p_strSection, p_strKey, p_strValue);
        }

        #endregion

        #endregion

        #region Obsolete/Ignored

        /// <summary>
        /// Registers a warning to be displayed when the user deactivates the specified plugin in the mod manager.
        /// </summary>
        /// <remarks>
        /// This method is ignored. Registering warnings is not supported by the currect implementation of the mod
        /// manager.
        /// </remarks>
        /// <param name="p_strPlugin">The plugin for which to register a warning.</param>
        /// <param name="p_strWarningType">The type of warning to register.</param>
        public void SetDeactivationWarning(string p_strPlugin, string p_strWarningType)
        {
            DeactivationWarningType dwtWarningType = (DeactivationWarningType)Enum.Parse(typeof(DeactivationWarningType), p_strWarningType);
            SetDeactivationWarning(p_strPlugin, dwtWarningType);
        }

        /// <summary>
        /// Registers a warning to be displayed when the user deactivates the specified plugin in the mod manager.
        /// </summary>
        /// <remarks>
        /// This method is ignored. Registering warnings is not supported by the currect implementation of the mod
        /// manager.
        /// </remarks>
        /// <param name="p_strPlugin">The plugin for which to register a warning.</param>
        /// <param name="p_dwtWarningType">The type of warning to register.</param>
        private void SetDeactivationWarning(string p_strPlugin, DeactivationWarningType p_dwtWarningType)
        {
            //TODO implement registering plugin deactivation warnings
            // in addition to generic warning types, we should allow custom messages
        }

        #endregion
    }

    /// <summary>
    /// List to possible warning types when registering a warning for a plugin deactivation.
    /// </summary>
    public enum DeactivationWarningType
    {
        /// <summary>
        /// Allow the deactivation.
        /// </summary>
        Allow,

        /// <summary>
        /// Warn that the deactivation may cause problems.
        /// </summary>
        WarnAgainst,

        /// <summary>
        /// Prevent the mod from being deactivated.
        /// </summary>
        Disallow
    }
    /// <summary>
    /// Encapsulates the information about a plugin.
    /// </summary>
    public class Plugin
    {
        #region Properties

        /// <summary>
        /// Gets the description of the plugin.
        /// </summary>
        /// <value>The description of the plugin.</value>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the image of the plugin.
        /// </summary>
        /// <value>The picture of the plugin.</value>
        public Image Picture { get; private set; }

        /// <summary>
        /// Gets the filename of the plugin.
        /// </summary>
        /// <value>The filename of the plugin.</value>
        public string Filename { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// A simple constructor that initializes the object with the given values.
        /// </summary>
        /// <param name="p_strPath">The filename of the plugin.</param>
        /// <param name="p_strDescription">The description of the plugin.</param>
        /// <param name="p_imgPicture">The picture of the plugin.</param>
        public Plugin(string p_strPath, string p_strDescription, Image p_imgPicture)
        {
            Filename = p_strPath;
            Description = p_strDescription;
            Picture = p_imgPicture;
        }

        #endregion

        /// <summary>
        /// Uses the filename to represent the plugin.
        /// </summary>
        /// <returns>The filename.</returns>
        public override string ToString()
        {
            return String.Format("{0} ({1})", Path.GetFileName(Filename), Filename);
        }
 
    }
 */
}