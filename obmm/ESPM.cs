/*
XBaseTools
Copyright (C) 2010 Matthew Perry

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
using System.IO;
using System.Collections.Generic;

namespace OblivionModManager
{
	/// <summary>
	/// Description of ESPM.
	/// </summary>
	public class ESPM
	{
		public static void RestoreESPM()
		{
			{
				DirectoryInfo dataDir = new DirectoryInfo(Path.Combine(Program.currentGame.DataFolderPath,"OBMM\\Hidden"));
				
				if (dataDir.Exists)
				{
					
					List<FileInfo> allESPM = new List<FileInfo>();
					allESPM.AddRange(dataDir.GetFiles("*.esp"));
					allESPM.AddRange(dataDir.GetFiles("*.esm"));
					allESPM.AddRange(dataDir.GetFiles("*.bsa"));
					
					foreach(FileInfo espm in allESPM)
					{
                        try
                        {
                            string newfile = Path.Combine(Program.currentGame.DataFolderPath, espm.Name);
                            if (!File.Exists(newfile))
                            {
                                espm.MoveTo(newfile);
                            }
                        }
                        catch (Exception ex)
                        {
                            Program.logger.WriteToLog("Could not restore "+ espm.Name+": "+ex.Message, Logger.LogLevel.Warning);
                        }
					}
				}
			}
			
			{
				DirectoryInfo dataDir = new DirectoryInfo(Program.currentGame.DataFolderPath);
				
				if (dataDir.Exists)
				{
					FileInfo[] files = dataDir.GetFiles("*.ghost");
					
					foreach(FileInfo fi in files)
					{
						string newfile;
						newfile = Path.Combine(Program.currentGame.DataFolderPath,fi.Name.Replace(".ghost",""));
						
						if (!File.Exists(newfile))
							fi.MoveTo(newfile);
					}
				}
			}
			
		}
		public static void HideESPM()
		{
			List<string> activeESPM = new List<string>();
			
			{
				StreamReader sr = new StreamReader(Path.Combine(Program.ESPDir,"plugins.txt"));
				
				string line;
				while((line = sr.ReadLine()) != null)
				{
					if (line.Length > 0 && line[0] != '#')
						activeESPM.Add(line);
				}
				
				sr.Close();
			}
			
			DirectoryInfo dataDir = new DirectoryInfo(Program.currentGame.DataFolderPath+"");
			
			//FileInfo[] allESPM = dataDir.GetFiles("*.esp");
			List<FileInfo> allESPM = new List<FileInfo>();
			allESPM.AddRange(dataDir.GetFiles("*.esp"));
			allESPM.AddRange(dataDir.GetFiles("*.esm"));
			
			foreach(FileInfo espm in allESPM)
			{
				if (!activeESPM.Contains(espm.Name) && espm.Name.ToLower()!="skyrim.esm" && espm.Name.ToLower()!="update.esm" && Settings.bGhostInactiveMods)
				{
					string espmname = Path.Combine(Program.currentGame.DataFolderPath,espm.Name + ".ghost");
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
						string destfn = Path.Combine(Program.currentGame.DataFolderPath,bsa.Name + ".ghost");
						
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
