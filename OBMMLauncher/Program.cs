/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 11/07/2010
 * Time: 1:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace ESPMHider
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		public static readonly string OblivionESPDir=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"oblivion\\");
        public static readonly string DataFolderName = "Data";
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			//DirectoryInfo hm = new DirectoryInfo(Program.DataFolderName+"\\OBMM\\Hidden");
			
			
			//if (!hm.Exists)
			//	hm.Create();
			
			if (args.Length == 1 && args[0] == "-launch")
			{
				LaunchOblivion();
				Application.Exit();
			}
			else
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
		}
		public static void RestoreESPM()
		{
			{
				DirectoryInfo dataDir = new DirectoryInfo(Program.DataFolderName+"\\OBMM\\Hidden");
				
				if (dataDir.Exists)
				{
					
					List<FileInfo> allESPM = new List<FileInfo>();
					allESPM.AddRange(dataDir.GetFiles("*.esp"));
					allESPM.AddRange(dataDir.GetFiles("*.esm"));
					allESPM.AddRange(dataDir.GetFiles("*.bsa"));
					
					foreach(FileInfo espm in allESPM)
					{
						espm.MoveTo(Program.DataFolderName+"\\" + espm.Name);
					}
				}
			}
			
			{
				DirectoryInfo dataDir = new DirectoryInfo(Program.DataFolderName+"");
				
				if (dataDir.Exists)
				{
					FileInfo[] files = dataDir.GetFiles("*.ghost");
					
					foreach(FileInfo fi in files)
					{
                        try { fi.MoveTo(Program.DataFolderName+"\\" + fi.Name.Substring(0, fi.Name.Length - 6)); }
                        catch { };// file may exist already
					}
				}
			}
			
		}
		public static void LaunchOblivion()
		{
			HideESPM();
			if (File.Exists("obse_loader.exe"))
			{
				Process.Start("obse_loader.exe");
			}
			else
			{
				Process.Start("Oblivion.exe");
			}
		}
		public static void HideESPM()
		{
			List<string> activeESPM = new List<string>();
			
			{
				StreamReader sr = new StreamReader(OblivionESPDir + "plugins.txt");
				
				string line;
				while((line = sr.ReadLine()) != null)
				{
					if (line.Length > 0 && line[0] != '#')
						activeESPM.Add(line);
				}
				
				sr.Close();
			}
			
			DirectoryInfo dataDir = new DirectoryInfo(Program.DataFolderName+"");
			
			//FileInfo[] allESPM = dataDir.GetFiles("*.esp");
			List<FileInfo> allESPM = new List<FileInfo>();
			allESPM.AddRange(dataDir.GetFiles("*.esp"));
			allESPM.AddRange(dataDir.GetFiles("*.esm"));
			
			foreach(FileInfo espm in allESPM)
			{
                if (!activeESPM.Contains(espm.Name))
				{
					string espmname = Program.DataFolderName+"\\" + espm.Name + ".ghost";
					if (File.Exists(espmname))
					{
						int num = 0;
						while(File.Exists(espmname + "-" + num.ToString()))
							num++;
						
						espmname += "-" + num.ToString();
					}
                    string oldname = espm.Name;
                    espm.MoveTo(espmname);

                    FileInfo[] bsas = dataDir.GetFiles(Path.GetFileNameWithoutExtension(oldname) + "*.bsa");
					
					foreach(FileInfo bsa in bsas)
					{
						string destfn = Program.DataFolderName+"\\" + bsa.Name + ".ghost";
						
						if (File.Exists(destfn))
						{
							int num = 0;
							while(File.Exists(destfn + "-" + num.ToString()))
								num++;
							
							destfn += "-" + num.ToString();
						}
						
						bsa.MoveTo(destfn);
					}
				}
			}
		}
		
	}
}
