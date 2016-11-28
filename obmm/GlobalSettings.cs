/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 27/09/2010
 * Time: 3:23 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using BaseTools.Configuration;
using BaseTools.Configuration.Parsers;
using SV = BaseTools.Searching.StringValidator;
namespace OblivionModManager
{
	/// <summary>
	/// Description of GlobalSettings.
	/// </summary>
	public class GlobalSettings
	{
		public static bool AlwaysImportOCD = false, AlwaysImportTES = false, AlwaysImportOCDList = false, IncludeVersionNumber = false;
		public static bool ShowOMODNames = false;
		public static string LastTNID = "";
        private static string settingsFilename = Path.Combine(Program.BaseDir, "settings.xbt");

        public static void LoadSettings()
		{
            try
            {
                if (File.Exists(settingsFilename))
                {
                    ConfigList oeSettings = new GeneralConfig().LoadConfiguration(settingsFilename);
                    ConfigList aiSettings = oeSettings.GetSection(new SV("Always Import", false));

                    ConfigPair cp;

                    if ((cp = aiSettings.GetPair(new SV("OMOD Conversion Data", false))) != null)
                        GlobalSettings.AlwaysImportOCD = cp.DataAsBoolean;

                    if ((cp = aiSettings.GetPair(new SV("TESNexus", false))) != null)
                        GlobalSettings.AlwaysImportTES = cp.DataAsBoolean;

                    if ((cp = aiSettings.GetPair(new SV("OCD List", false))) != null)
                        GlobalSettings.AlwaysImportOCDList = cp.DataAsBoolean;

                    if ((cp = oeSettings.GetPair(new SV("Include Version Number", false))) != null)
                        GlobalSettings.IncludeVersionNumber = cp.DataAsBoolean;

                    if ((cp = oeSettings.GetPair(new SV("Show OMOD Names", false))) != null)
                        GlobalSettings.ShowOMODNames = cp.DataAsBoolean;

                    if ((cp = oeSettings.GetPair(new SV("Last TESNexus", false))) != null)
                        GlobalSettings.LastTNID = cp.DataAsString;
                }
            }
            catch (Exception ex)
            {
                Program.logger.WriteToLog("Could not load global settings: "+ex.Message,Logger.LogLevel.Warning);
            }
		}
		public static void SaveSettings()
		{
			ConfigList cl = new ConfigList();
			ConfigList ai = new ConfigList();
			cl.AddSection("Always Import", ai);
			
			ai.AddPair("OMOD Conversion Data", GlobalSettings.AlwaysImportOCD);
			ai.AddPair("TESNexus", GlobalSettings.AlwaysImportTES);
			ai.AddPair("OCD List", GlobalSettings.AlwaysImportOCDList);
			
			cl.AddPair("Include Version Number", GlobalSettings.IncludeVersionNumber);
			cl.AddPair("Show OMOD Names", GlobalSettings.ShowOMODNames);
			cl.AddPair("Last TESNexus", GlobalSettings.LastTNID);
			
			new GeneralConfig().SaveConfiguration(settingsFilename, cl);
		}
	}
}
