/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 11/07/2010
 * Time: 1:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Media;


namespace ESPMHider
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//




            btnOblivion.Enabled = File.Exists("Oblivion.exe") | File.Exists("TesV.exe");
            btnOblivionLauncher.Enabled = File.Exists("OblivionLauncher.exe") | File.Exists("SkyrimLauncher.exe");
			btnOBMM.Enabled = File.Exists("TesModManager.exe");
			btnTESCS.Enabled = File.Exists("TESConstructionSet.exe");
			btnWryeBash.Enabled = File.Exists(@"Mopy\Wrye Bash Launcher.pyw");
			btnTES4Edit.Enabled = File.Exists("TES4Edit.exe");
			
			/*FileInfo[] musicFiles = new DirectoryInfo(@"Data\Music\Explore").GetFiles("*.mp3");
			
			if (musicFiles.Length > 0)
			{
				Random r = new Random();
				new SoundPlayer(musicFiles[r.Next() % musicFiles.Length].FullName).PlayLooping();
			}*/
			if (File.Exists("LauncherMusic.wav"))
				new SoundPlayer("LauncherMusic.wav").Play();
		}
		
		void BtnHideInactiveClick(object sender, EventArgs e)
		{
			Program.HideESPM();
		}
		
		
		void BtnRestoreClick(object sender, EventArgs e)
		{
			Program.RestoreESPM();
		}
		
		
		
		void BtnOBMMClick(object sender, EventArgs e)
		{
			Program.RestoreESPM();
			Process.Start("TesModManager.exe");
			this.Close();
		}
		
		void BtnOblivionLauncherClick(object sender, EventArgs e)
		{
            if (File.Exists("OblivionLauncher.exe"))
			    Process.Start("OblivionLauncher.exe");
            else if (File.Exists("SkyrimLauncher.exe"))
                Process.Start("SkyrimLauncher.exe");
			this.Close();
		}
		
		void BtnOblivionClick(object sender, EventArgs e)
		{
			Program.LaunchOblivion();
			this.Close();
		}
		
		void BtnTESCSClick(object sender, EventArgs e)
		{
			if (File.Exists("obse_loader.exe"))
			{
				Process.Start("obse_loader.exe", "-editor");
				this.Close();
			}
			else
			{
				Process.Start("TESConstructionSet.exe");
				this.Close();
			}
		}
		
		void BtnWryeBashClick(object sender, EventArgs e)
		{
			Program.RestoreESPM();
			if (!File.Exists(@"Mopy\Wrye Bash Launcher.cmd"))
			{
				StreamWriter sw = new StreamWriter(@"Mopy\Wrye Bash Launcher.cmd");
				
				sw.WriteLine("@echo off");
				sw.WriteLine("set LAUNCHER=Wrye Bash Launcher.pyw");
				sw.WriteLine("if not exist \"%LAUNCHER%\" cd Mopy");
				sw.WriteLine("start \"\" \"%LAUNCHER%\"");
				
				sw.Close();
			}
			
			Process.Start(@"Mopy\Wrye Bash Launcher.cmd");
			this.Close();
		}
		
		void BtnTES4EditClick(object sender, EventArgs e)
		{
				Process.Start("TES4Edit.exe");
				this.Close();
		}
	}
}
