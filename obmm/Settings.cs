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
using System.Collections.Generic;
using Formatter=System.Runtime.Serialization.Formatters.Binary.BinaryFormatter;
using MessageBox=System.Windows.Forms.MessageBox;
/*using BaseTools.Configuration;
using BaseTools.Configuration.Parsers;
using SV = BaseTools.Searching.StringValidator;*/

namespace OblivionModManager {
	public static class Settings {
		public const int CurrentVersion = 18;

		public static int MaxMemoryStreamSize; //64 mb
		public static bool ShowScriptWarnings;
		public static System.Drawing.Size mfSize;
		public static bool mfMaximized;
		public static int mfSplitterPos;
        public static int altPanelSplitterDistance;
        public static List<omodGroup> omodGroups;
		public static bool AllowInsecureScripts;
		public static bool WarnOnINIEdit;
		public static bool WarnOnModDelete;
		public static bool ShowNewModInfo;
		public static bool LockFOV;
		public static DeactiveStatus DefaultEspWarn;
		public static string OblivionCommandLine;
		public static int EspColWidth1;
		public static int EspColWidth2;
		public static System.Drawing.Size TextEdSize;
		public static bool TextEdMaximized;
		public static string omodDir;
		public static string tempDir;
        public static string conflictsBackupDir;
        public static string omodCreatorFolderBrowserDir;
		public static string BSACreatorFolderBrowserDir;
		public static bool TrackConflicts;
		public static bool AutoUpdateConflicts;
		public static bool UpdateEsps;
		public static byte UseProcessKiller;
		public static bool ShowLaunchWarning;
		public static bool SafeMode;
		public static bool NewEspsLoadLast;
		public static bool AdvSelectMany;
		public static System.Drawing.Size ScriptEdSize;
		public static bool ScriptEdMaximized;
		public static bool NeverTouchLoadOrder;
        public static bool bAskToBeNexusDownloadManager;
        public static bool bDeActivateOnDoubleClick;
        public static bool bLoadOrderAsUTF8;
        public static bool bUseTimeStamps;
        public static bool bEnableDebugLogging;
        public static bool bGhostInactiveMods;
        public static bool bShowHiddenMods;
        public static bool bSaveOmod2AsZip;
        public static bool bDeactivateMissingOMODs;
        public static bool bWarnAboutMissingInfo;
        public static bool bShowSimpleOverwriteForm;
        public static bool bPreventMovingESPBeforeESM;
        public static bool bOmod2IsDefault;
        public static bool CDShowMajor;
		public static bool CDShowMinor;
		public static bool CDShowVeryMinor;
		public static bool CDIncludeEsp;
		public static bool CDIgnoreInactiveEsp;
		public static bool CDIncludeOmod;
		public static bool CDIgnoreInactiveOmod;

		public static string[] PKServicesStop;
		public static string[] PKServicesKeep;
		public static string[] PKProcessesClose;
		public static string[] PKProcessesKill;
		public static string[] PKProcessesKeep;
		public static uint PKFlags;
		public static int PKTimeOut;

		public static int EspSortOrder;
		public static int omodSortOrder;

		public static bool UpdateInvalidation;
		public static ArchiveInvalidationFlags InvalidationFlags;

		public static bool CompressionBoost;
		public static CompressionType dataCompressionType;
		public static CompressionLevel omodCompressionLevel;
		public static CompressionLevel dataCompressionLevel;

		public static void SaveSettings() {
			Formatter formatter = new Formatter();
			BinaryWriter bw=new BinaryWriter(File.Open(Program.SettingsFile, FileMode.Create));
			try {
				bw.Write(CurrentVersion);
				bw.Write(MaxMemoryStreamSize);
				bw.Write(ShowScriptWarnings);
				bw.Write(mfSize.Width);
				bw.Write(mfSize.Height);
				bw.Write(mfMaximized);
				bw.Write(mfSplitterPos);
				bw.Write((byte)omodGroups.Count);
				foreach(omodGroup og in omodGroups) og.Write(bw, formatter);
				bw.Write(UpdateInvalidation);
				bw.Write(AllowInsecureScripts);
				bw.Write(WarnOnINIEdit);
				bw.Write(WarnOnModDelete);
				bw.Write(TextEdSize.Width);
				bw.Write(TextEdSize.Height);
				bw.Write(TextEdMaximized);
				bw.Write(ShowNewModInfo);
				bw.Write(LockFOV);
				bw.Write(omodDir);
				bw.Write(tempDir);
				bw.Write((byte)DefaultEspWarn);
				bw.Write(OblivionCommandLine);
				bw.Write(EspColWidth1);
				bw.Write(EspColWidth2);
				bw.Write(TrackConflicts);
				bw.Write(AutoUpdateConflicts);
				bw.Write(UpdateEsps);
				bw.Write(UseProcessKiller);
				bw.Write(ShowLaunchWarning);
				bw.Write(SafeMode);
				bw.Write(NeverTouchLoadOrder);

				bw.Write(CDShowMajor);
				bw.Write(CDShowMinor);
				bw.Write(CDShowVeryMinor);
				bw.Write(CDIncludeEsp);
				bw.Write(CDIgnoreInactiveEsp);
				bw.Write(CDIncludeOmod);
				bw.Write(CDIgnoreInactiveOmod);

				bw.Write(PKServicesStop.Length);
				for(int i=0;i<PKServicesStop.Length;i++) bw.Write(PKServicesStop[i]);
				bw.Write(PKServicesKeep.Length);
				for(int i=0;i<PKServicesKeep.Length;i++) bw.Write(PKServicesKeep[i]);
				bw.Write(PKProcessesClose.Length);
				for(int i=0;i<PKProcessesClose.Length;i++) bw.Write(PKProcessesClose[i]);
				bw.Write(PKProcessesKill.Length);
				for(int i=0;i<PKProcessesKill.Length;i++) bw.Write(PKProcessesKill[i]);
				bw.Write(PKProcessesKeep.Length);
				for(int i=0;i<PKProcessesKeep.Length;i++) bw.Write(PKProcessesKeep[i]);
				bw.Write(PKFlags);
				bw.Write(PKTimeOut);

				bw.Write(EspSortOrder);
				bw.Write(omodSortOrder);
				bw.Write(CompressionBoost);
				bw.Write((byte)dataCompressionType);
				bw.Write((byte)omodCompressionLevel);
				bw.Write((byte)dataCompressionLevel);
				bw.Write(omodCreatorFolderBrowserDir);
				bw.Write(BSACreatorFolderBrowserDir);
				bw.Write((uint)InvalidationFlags);

				bw.Write(NewEspsLoadLast);

				bw.Write(AdvSelectMany);
				bw.Write(ScriptEdSize.Width);
				bw.Write(ScriptEdSize.Height);
				bw.Write(ScriptEdMaximized);
                bw.Write(bAskToBeNexusDownloadManager);
                bw.Write(bDeActivateOnDoubleClick);
                bw.Write(bLoadOrderAsUTF8);
                bw.Write(bUseTimeStamps);
                bw.Write(bEnableDebugLogging);
                bw.Write(bGhostInactiveMods);
                bw.Write(bShowHiddenMods);
                bw.Write(bSaveOmod2AsZip);
                bw.Write(bDeactivateMissingOMODs);
                bw.Write(bWarnAboutMissingInfo);
                bw.Write(bShowSimpleOverwriteForm);
                bw.Write(bPreventMovingESPBeforeESM);
                bw.Write(bOmod2IsDefault);
                bw.Write(conflictsBackupDir);
                bw.Write(altPanelSplitterDistance);
            }
            finally
            {
				bw.Close();
			}
		}
		/*public class SimpleList
		{
			ConfigList cfg;
			public SimpleList(ConfigList cfg)
			{
				this.cfg = cfg;
			}
			
			public ConfigPair ReadPair(string key)
			{
				return cfg.GetPair(new SV(key, false));
			}
			public int ReadInt32(string key)
			{
				return ReadPair(key).DataAsInteger;
			}
			public bool ReadBoolean(string key)
			{
				return ReadPair(key).DataAsBoolean;
			}
			public string ReadString(string key)
			{
				return ReadPair(key).DataAsString;
			}
			public double ReadDouble(string key)
			{
				return ReadPair(key).DataAsDecimal;
			}
			public byte ReadByte(string key)
			{
				return (byte)ReadPair(key).DataAsInteger;
			}
		}*/
		public static void LoadSettings() {
			DefaultSettings();
			if(!File.Exists(Program.SettingsFile)) return;
			BinaryReader br;
			Formatter formatter=new Formatter();
			int count;
			
			#region "Binary Reader"
			try {
				br=new BinaryReader(File.OpenRead(Program.SettingsFile));
			} catch { return; }
			try {
				int version=br.ReadInt32();
				if(version>CurrentVersion) {
					MessageBox.Show("Your setting file appears to be from a newer version of obmm, and cannot be read", "Error");
					return;
				}
				MaxMemoryStreamSize=br.ReadInt32();
				if(version==0&&MaxMemoryStreamSize==0) {
					MaxMemoryStreamSize=67108864;
					MessageBox.Show("Your settings file appears to be corrupted.", "Error");
					return;
				}
				ShowScriptWarnings=br.ReadBoolean();
				mfSize.Width=br.ReadInt32();
				mfSize.Height=br.ReadInt32();
				mfMaximized=br.ReadBoolean();
				mfSplitterPos=br.ReadInt32();
				byte b=br.ReadByte();
				if(version>=6) {
					for(int i=0;i<b;i++) omodGroups.Add(omodGroup.Read(br, formatter));
				} else {
					for(int i=0;i<b;i++) omodGroups.Add(new omodGroup(br.ReadString()));
				}
				UpdateInvalidation=br.ReadBoolean();
				if(version>=17) {
					AllowInsecureScripts=br.ReadBoolean();
				}
				WarnOnINIEdit=br.ReadBoolean();
				WarnOnModDelete=br.ReadBoolean();
				TextEdSize.Width=br.ReadInt32();
				TextEdSize.Height=br.ReadInt32();
				TextEdMaximized=br.ReadBoolean();
				ShowNewModInfo=br.ReadBoolean();
				LockFOV=br.ReadBoolean();
				omodDir=Path.GetFullPath(br.ReadString());
				if(version>=5) {
					tempDir=br.ReadString();
                    if (Settings.tempDir.Length == 0 || Settings.tempDir.ToLower().StartsWith(Program.currentGame.DataFolderPath.ToLower()))
                    {
                        Settings.tempDir = Path.Combine(Path.GetTempPath(), Program.currentGame.Name + @"MM\");
                    }
                }
				DefaultEspWarn=(DeactiveStatus)br.ReadByte();
				OblivionCommandLine=br.ReadString();
				EspColWidth1=br.ReadInt32();
				EspColWidth2=br.ReadInt32();
				if(version>=16) {
					TrackConflicts=br.ReadBoolean();
				}
				if(version>=2) {
					AutoUpdateConflicts=br.ReadBoolean();
				}
				if(version>=4) {
					UpdateEsps=br.ReadBoolean();
				}
				if(version>=8&&version<=11) {
					br.ReadBoolean();   //Used to be BackupOrigFiles
				}
				if(version>=9) {
					UseProcessKiller=br.ReadByte();
				}
				if(version>=11) {
					ShowLaunchWarning=br.ReadBoolean();
				}
				if(version>=12) {
					SafeMode=br.ReadBoolean();
				}
				if(version>=18) {
					NeverTouchLoadOrder=br.ReadBoolean();
				}

				CDShowMajor=br.ReadBoolean();
				CDShowMinor=br.ReadBoolean();
				CDShowVeryMinor=br.ReadBoolean();
				CDIncludeEsp=br.ReadBoolean();
				CDIgnoreInactiveEsp=br.ReadBoolean();
				CDIncludeOmod=br.ReadBoolean();
				CDIgnoreInactiveOmod=br.ReadBoolean();

				if(version>=10) {
					count=br.ReadInt32();
					PKServicesStop=new string[count];
					for(int i=0;i<count;i++) PKServicesStop[i]=br.ReadString();
					count=br.ReadInt32();
					PKServicesKeep=new string[count];
					for(int i=0;i<count;i++) PKServicesKeep[i]=br.ReadString();
					count=br.ReadInt32();
					PKProcessesClose=new string[count];
					for(int i=0;i<count;i++) PKProcessesClose[i]=br.ReadString();
					count=br.ReadInt32();
					PKProcessesKill=new string[count];
					for(int i=0;i<count;i++) PKProcessesKill[i]=br.ReadString();
					count=br.ReadInt32();
					PKProcessesKeep=new string[count];
					for(int i=0;i<count;i++) PKProcessesKeep[i]=br.ReadString();
					PKFlags=br.ReadUInt32();
					PKTimeOut=br.ReadInt32();
				}

				EspSortOrder=br.ReadInt32();
				omodSortOrder=br.ReadInt32();
				CompressionBoost=br.ReadBoolean();
				if(version>=3) {
					dataCompressionType=(CompressionType)br.ReadByte();
					omodCompressionLevel=(CompressionLevel)br.ReadByte();
					dataCompressionLevel=(CompressionLevel)br.ReadByte();
				}
				omodCreatorFolderBrowserDir=br.ReadString();
				if(version>=7) {
					BSACreatorFolderBrowserDir=br.ReadString();
				}
				InvalidationFlags=(ArchiveInvalidationFlags)br.ReadUInt32();
				if(version>=13) {
					NewEspsLoadLast=br.ReadBoolean();
				}
				if(version>=14) {
					AdvSelectMany=br.ReadBoolean();
					ScriptEdSize.Width=br.ReadInt32();
					ScriptEdSize.Height=br.ReadInt32();
					ScriptEdMaximized=br.ReadBoolean();
                    try { bAskToBeNexusDownloadManager = br.ReadBoolean(); }  catch { bAskToBeNexusDownloadManager = false; };
                    try {bDeActivateOnDoubleClick = br.ReadBoolean(); }  catch { bDeActivateOnDoubleClick = true; };
                    try { bLoadOrderAsUTF8 = br.ReadBoolean(); }  catch { bLoadOrderAsUTF8 = false; };
                    try { bUseTimeStamps = br.ReadBoolean(); }  catch { bUseTimeStamps = true; };
                    try { bEnableDebugLogging = br.ReadBoolean(); } catch { bEnableDebugLogging = false; };

                    if (Settings.bEnableDebugLogging)
                        Program.logger.setLogLevel("high");
                    else
                        Program.logger.setLogLevel("low");

                    try{ bGhostInactiveMods = br.ReadBoolean(); } catch { bGhostInactiveMods = true; };
                    try { bShowHiddenMods = br.ReadBoolean(); } catch { bShowHiddenMods = false; };
                    try { bSaveOmod2AsZip = br.ReadBoolean(); } catch { bSaveOmod2AsZip = true; };
                    try { bDeactivateMissingOMODs = br.ReadBoolean(); } catch { bDeactivateMissingOMODs = false; };
                    try { bWarnAboutMissingInfo = br.ReadBoolean(); }catch { bWarnAboutMissingInfo = true; };
                    try { bShowSimpleOverwriteForm = br.ReadBoolean(); } catch { bShowSimpleOverwriteForm = false; };
                    try { bPreventMovingESPBeforeESM = br.ReadBoolean(); } catch { bPreventMovingESPBeforeESM = true; };
                    try { bOmod2IsDefault = br.ReadBoolean(); }catch { bOmod2IsDefault = true; };
                    try { conflictsBackupDir = br.ReadString(); }
                    catch { conflictsBackupDir = Program.currentGame.DataFolderPath; };
                    try { altPanelSplitterDistance = br.ReadInt32(); } catch { altPanelSplitterDistance = -9001; }
                }

				if(version<15&&(ArchiveInvalidationFlags.EditBSAs&InvalidationFlags)>0) {
					MessageBox.Show("The recommended archiveinvalidation method is BSA redirection, but this copy of obmm is set up to use BSA alteration.\n"+
					                "Unless there is a specific reason you require BSA alteration, it is recommended that you switch back to the default settings.\n"+
					                "To do this, click 'utilities|Archive invalidation'\n"+
					                "Click 'Reset to defaults'\n"+
					                "Click 'Update now'");
				}
			} catch {
				DefaultSettings();
			} finally {
				if(br!=null) br.Close();
			}
			#endregion
		}
		public static void DefaultSettings() {
			MaxMemoryStreamSize=67108864; //64 mb
			ShowScriptWarnings=true;
			mfSize=System.Drawing.Size.Empty;
			mfMaximized=false;
			mfSplitterPos=0;
            altPanelSplitterDistance = -9001;
			omodGroups=new List<omodGroup>();
			UpdateInvalidation=false;
			AllowInsecureScripts=true;
			WarnOnINIEdit=true;
			WarnOnModDelete=true;
			ShowNewModInfo=false;
			LockFOV=false;
			DefaultEspWarn=DeactiveStatus.WarnAgainst;
			OblivionCommandLine="";
			EspColWidth1=-1;
			EspColWidth2=-1;
			InvalidationFlags=ArchiveInvalidationFlags.Default;
			TextEdSize=System.Drawing.Size.Empty;
			TextEdMaximized=false;
			TrackConflicts=true;
			AutoUpdateConflicts=false;
			UpdateEsps=false;
			UseProcessKiller=0;
			ShowLaunchWarning=true;
			SafeMode=true;
			AdvSelectMany=false;
			ScriptEdSize=new System.Drawing.Size(0, 0);
			ScriptEdMaximized=true;
			NeverTouchLoadOrder=false;

			CDShowMajor=true;
			CDShowMinor=false;
			CDShowVeryMinor=false;
			CDIncludeEsp=true;
			CDIgnoreInactiveEsp=false;
			CDIncludeOmod=true;
			CDIgnoreInactiveOmod=false;
			EspSortOrder=0;
			omodSortOrder=0;
			CompressionBoost=false;
			dataCompressionType=CompressionType.SevenZip;
			omodCompressionLevel=CompressionLevel.VeryHigh;
			dataCompressionLevel=CompressionLevel.High;

			PKServicesStop=new string[0];
			PKServicesKeep=new string[] { "Windows Audio", "Windows Management Instrumentation" };
			PKProcessesClose=new string[0];
			PKProcessesKill=new string[0];
			PKProcessesKeep=new string[] { "explorer.exe", "services.exe", "svchost.exe", "lsass.exe", "csrss.exe", "smss.exe",
				"winlogon.exe", "system", "system idle process" };
			PKFlags=0;
			PKTimeOut=1000;

			omodCreatorFolderBrowserDir="";
			BSACreatorFolderBrowserDir="";
			omodDir=Path.GetFullPath(Path.Combine(Program.BaseDir, "mods"));

            tempDir = Path.Combine(Path.GetTempPath(), Program.currentGame.Name + @"MM\");

			NewEspsLoadLast=true;
            bAskToBeNexusDownloadManager = true;
            bDeActivateOnDoubleClick = true;
            bLoadOrderAsUTF8 = false;
            bUseTimeStamps = true;
            bEnableDebugLogging = false;
            bGhostInactiveMods = true;
            bSaveOmod2AsZip = true;
            bDeactivateMissingOMODs = false;
            bShowSimpleOverwriteForm = false;
            GlobalSettings.AlwaysImportTES = false;
            GlobalSettings.AlwaysImportOCD = true;
            GlobalSettings.AlwaysImportOCDList = false;
            GlobalSettings.ShowOMODNames = true;
            GlobalSettings.IncludeVersionNumber = true;
        }
	}
}