/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 23/07/2010
 * Time: 4:27 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;
using Microsoft.Win32;
using System.Resources;
using System.Reflection;
using System.Security.Principal;

//using IWshRuntimeLibrary;

namespace OBMMExInstaller
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		ResourceManager resman = new ResourceManager("OBMMExInstaller.Data", Assembly.GetExecutingAssembly());
		private IWshRuntimeLibrary.WshShellClass WshShell;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
		}
		
		void MainFormShown(object sender, EventArgs e)
		{
			/*if (File.Exists("generate.ida"))
			{
				BinaryReader br = new BinaryReader(new FileStream("generate.ida", FileMode.Open));
				
				int dircount = br.ReadInt32();
				
				StringBuilder sb = new StringBuilder();
				
				sb.AppendLine("Directories:");
				
				for(int i=0;i<dircount;i++)
				{
					sb.AppendLine(br.ReadString());
				}
				
				int fcount = br.ReadInt32();
				
				sb.AppendLine("Files:");
				
				for(int i=0;i<fcount;i++)
				{
					
					sb.Append(br.ReadString()).Append(": ");
					
					int fsz = br.ReadInt32();
					sb.AppendLine(fsz.ToString());
					
					br.ReadBytes(fsz);
				}
				
				br.Close();
				
				txtDebug.Text = sb.ToString();
			}*/
		}
		
		public static string FindSoftware(string key, string subkey)
		{
			RegistryKey lm, lu;
			lm = Registry.LocalMachine.OpenSubKey("SOFTWARE\\" + key, true);
			
			lu = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\" + key, true);
			
			
			object o;
			
			if ((lm != null) && (o = lm.GetValue(subkey)) != null)
				return o.ToString();
			else if ((lu != null) && (o = lu.GetValue(subkey)) != null)
				return o.ToString();
			else
				return null;
			
		}

        /// <summary>
        /// Determines whether or not the user belongs to the Windows Administrator group.
        /// </summary>
        public bool IsUserWindowsAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            try
            {
                //get the currently logged in user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
                if (!isAdmin)
                    isAdmin = principal.IsInRole("BUILTIN\\Administrators");
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }
            return isAdmin;
        }
		
		void MainFormLoad(object sender, EventArgs e)
		{
			/*if (!File.Exists(Program.DataFolderName+".dat"))
			{
				MessageBox.Show("data.dat not found. You must extract all files from the zip", "Missing File", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
			}*/
			
			BinaryReader br = new BinaryReader(new MemoryStream((byte[])resman.GetObject("data")));
			
			this.Text = br.ReadString();
			br.Close();
			
			string path;

            if (!IsUserWindowsAdministrator())
            {
                if (DialogResult.Cancel == MessageBox.Show("Setup needs administrative rights to automatically detect Oblivion or Skyrim's path. You can point setup to the right location or Cancel and run as administrator", "No administrative rights", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    this.Close();
            }
            else
            {
                if ((path = FindSoftware(@"Bethesda Softworks\Oblivion", "Installed Path")) != null)
                {
                    if (path.EndsWith(@"\"))
                        path = path.Substring(0, path.Length - 1);

                    txtOblivionPath.Text = path;
                }
                if ((path = FindSoftware(@"Valve\Steam", "InstallPath")) != null)
                {
                    DirectoryInfo di = new DirectoryInfo(Path.Combine(path, @"steamapps\common\skyrim"));

                    if (di.Exists)
                    {
                        txtSkyrimPath.Text = di.FullName;
                    }
                    if (Directory.Exists(Path.Combine(path, @"steamapps\common\morrowind")))
                    {
                        txtMorrowindPath.Text = Path.Combine(path, @"steamapps\common\morrowind");
                    }
                }
            }
			File.WriteAllBytes("Interop.IWshRuntimeLibrary.dll", (byte[])resman.GetObject("Interop.IWshRuntimeLibrary"));
			
		}
		
		void CreateShortcut(string location, string executable, string wd, string ico)
		{
			WshShell = new IWshRuntimeLibrary.WshShellClass();

			IWshRuntimeLibrary.IWshShortcut MyShortcut;

			MyShortcut = (IWshRuntimeLibrary.IWshShortcut)WshShell.CreateShortcut(location);

			MyShortcut.TargetPath = executable;
			MyShortcut.WorkingDirectory = wd;

			MyShortcut.Description = this.Text;
			MyShortcut.IconLocation = ico;

			MyShortcut.Save();
		}
		
		void BtnInstallClick(object sender, EventArgs e)
		{
			try
			{
                if (txtOblivionPath.Text.Trim().Length == 0 && txtSkyrimPath.Text.Trim().Length == 0 && txtMorrowindPath.Text.Trim().Length == 0)
					return;

                if (cbInstallForOblivion.Checked && txtOblivionPath.Text.Trim().Length != 0)
                {
                    DirectoryInfo installDir = new DirectoryInfo(txtOblivionPath.Text);

                    if (!installDir.Exists)
                        installDir.Create();

                    BinaryReader br = new BinaryReader(new MemoryStream((byte[])resman.GetObject("data")));

                    br.ReadString();

                    DateTime stamp = DateTime.FromBinary(br.ReadInt64());

                    int dircount = br.ReadInt32();

                    for (int i = 0; i < dircount; i++)
                    {
                        DirectoryInfo di = new DirectoryInfo(Path.Combine(txtOblivionPath.Text, br.ReadString()));

                        if (!di.Exists)
                            di.Create();
                    }

                    int fcount = br.ReadInt32();
                    for (int i = 0; i < fcount; i++)
                    {
                        string filename = br.ReadString();

                        int fsz = br.ReadInt32();
                        string cb = Path.Combine(txtOblivionPath.Text, filename);

                        if (File.Exists(cb))
                            File.Delete(cb);

                        File.WriteAllBytes(cb, br.ReadBytes(fsz));
                        File.SetLastWriteTimeUtc(cb, stamp);
                    }

                    br.Close();

                    if (chkDesktop.Checked)
                        CreateShortcut(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Tes Mod Manager for Oblivion.lnk"),
                                   Path.Combine(txtOblivionPath.Text, "TesModManager.exe"), txtOblivionPath.Text, Path.Combine(txtOblivionPath.Text, "TesModManager.exe"));

                    if (chkStartMenu.Checked)
                        CreateShortcut(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Tes Mod Manager for Oblivion.lnk"),
                                   Path.Combine(txtOblivionPath.Text, "TesModManager.exe"), txtOblivionPath.Text, Path.Combine(txtOblivionPath.Text, "TesModManager.exe"));
                }
                if (cbInstallForSkyrim.Checked && txtSkyrimPath.Text.Trim().Length != 0)
                {
                    DirectoryInfo installDir = new DirectoryInfo(txtSkyrimPath.Text);

                    if (!installDir.Exists)
                        installDir.Create();

                    BinaryReader br = new BinaryReader(new MemoryStream((byte[])resman.GetObject("data")));

                    br.ReadString();

                    DateTime stamp = DateTime.FromBinary(br.ReadInt64());

                    int dircount = br.ReadInt32();

                    for (int i = 0; i < dircount; i++)
                    {
                        DirectoryInfo di = new DirectoryInfo(Path.Combine(txtSkyrimPath.Text, br.ReadString()));

                        if (!di.Exists)
                            di.Create();
                    }

                    int fcount = br.ReadInt32();
                    for (int i = 0; i < fcount; i++)
                    {
                        string filename = br.ReadString();

                        int fsz = br.ReadInt32();
                        string cb = Path.Combine(txtSkyrimPath.Text, filename);

                        if (File.Exists(cb))
                            File.Delete(cb);

                        File.WriteAllBytes(cb, br.ReadBytes(fsz));
                        File.SetLastWriteTimeUtc(cb, stamp);
                    }

                    br.Close();

                    if (chkDesktop.Checked)
                        CreateShortcut(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Tes Mod Manager for Skyrim.lnk"),
                                   Path.Combine(txtSkyrimPath.Text, "TesModManager.exe"), txtSkyrimPath.Text, Path.Combine(txtSkyrimPath.Text, "TesModManager.exe"));

                    if (chkStartMenu.Checked)
                        CreateShortcut(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Tes Mod Manager for Skyrim.lnk"),
                                   Path.Combine(txtSkyrimPath.Text, "TesModManager.exe"), txtSkyrimPath.Text, Path.Combine(txtSkyrimPath.Text, "TesModManager.exe"));

                }
                if (cbInstallForMorrowind.Checked && txtMorrowindPath.Text.Trim().Length != 0)
                {
                    DirectoryInfo installDir = new DirectoryInfo(txtMorrowindPath.Text);

                    if (!installDir.Exists)
                        installDir.Create();

                    BinaryReader br = new BinaryReader(new MemoryStream((byte[])resman.GetObject("data")));

                    br.ReadString();

                    DateTime stamp = DateTime.FromBinary(br.ReadInt64());

                    int dircount = br.ReadInt32();

                    for (int i = 0; i < dircount; i++)
                    {
                        DirectoryInfo di = new DirectoryInfo(Path.Combine(txtMorrowindPath.Text, br.ReadString()));

                        if (!di.Exists)
                            di.Create();
                    }

                    int fcount = br.ReadInt32();
                    for (int i = 0; i < fcount; i++)
                    {
                        string filename = br.ReadString();

                        int fsz = br.ReadInt32();
                        string cb = Path.Combine(txtMorrowindPath.Text, filename);

                        if (File.Exists(cb))
                            File.Delete(cb);

                        File.WriteAllBytes(cb, br.ReadBytes(fsz));
                        File.SetLastWriteTimeUtc(cb, stamp);
                    }

                    br.Close();

                    if (chkDesktop.Checked)
                        CreateShortcut(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Tes Mod Manager for Morrowind.lnk"),
                                   Path.Combine(txtMorrowindPath.Text, "TesModManager.exe"), txtMorrowindPath.Text, Path.Combine(txtMorrowindPath.Text, "TesModManager.exe"));

                    if (chkStartMenu.Checked)
                        CreateShortcut(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Tes Mod Manager for Morrowind.lnk"),
                                   Path.Combine(txtMorrowindPath.Text, "TesModManager.exe"), txtMorrowindPath.Text, Path.Combine(txtMorrowindPath.Text, "TesModManager.exe"));

                }

                if (cbRegisterAsNexusDownloadMgr.Checked)
                {
                    object value = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\Software\\Classes\\nxm\\shell\\open\\command", "", null);

                    if (value != null)
                    {
                        string cmd = (value as string);
                        cmd = cmd.ToLower();
                        if (!cmd.Contains("tesmodmanager.exe"))
                        {
                            // nxm protocol is registered to another application
                            DialogResult dlgres = MessageBox.Show("Another application is registered as Nexus Download Manager. Do you want to replace it? (This prompt can be disabled in the Settings dialog)", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dlgres == DialogResult.Yes)
                            {
                                value = null;
                            }
                        }
                    }
                    if (value == null)
                    {
                        string apppath="";
                        if (txtSkyrimPath.Text.Length > 0)
                            apppath = Path.Combine(txtSkyrimPath.Text, "TesModManager.exe");
                        else if (txtOblivionPath.Text.Length > 0)
                            apppath = Path.Combine(txtOblivionPath.Text, "TesModManager.exe");
                        else if (txtMorrowindPath.Text.Length>0)
                            apppath = Path.Combine(txtMorrowindPath.Text, "TesModManager.exe");

                        try
                        {
                            Microsoft.Win32.Registry.SetValue("HKEY_LOCAL_MACHINE\\Software\\Classes\\nxm", "", "URL: Nexus mod protocol");
                            Microsoft.Win32.Registry.SetValue("HKEY_LOCAL_MACHINE\\Software\\Classes\\nxm", "URL Protocol", Microsoft.Win32.RegistryValueKind.String);
                            Microsoft.Win32.Registry.SetValue("HKEY_LOCAL_MACHINE\\Software\\Classes\\nxm\\DefaultIcon", "", apppath + ",0");
                            Microsoft.Win32.Registry.SetValue("HKEY_LOCAL_MACHINE\\Software\\Classes\\nxm\\shell\\open\\command", "", "\"" + apppath + "\" \"%1\"");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Could not modify registry because you do not have administrative rights", "Registry access error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

				
				MessageBox.Show("Successfully installed " + this.Text, "Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				this.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
			}
		}
		
		void BtnOblivionBrowseClick(object sender, EventArgs e)
		{
			fbdBrowse.SelectedPath = txtOblivionPath.Text;
			
			if (fbdBrowse.ShowDialog() == DialogResult.OK)
			{
				txtOblivionPath.Text = fbdBrowse.SelectedPath;
			}
		}

        private void btnSkyrimBrowse_Click(object sender, EventArgs e)
        {
            fbdBrowse.SelectedPath = txtSkyrimPath.Text;

            if (fbdBrowse.ShowDialog() == DialogResult.OK)
            {
                txtSkyrimPath.Text = fbdBrowse.SelectedPath;
            }

        }

        private void cbInstallForOblivion_CheckedChanged(object sender, EventArgs e)
        {
            if (cbInstallForOblivion.Checked)
            {
                txtOblivionPath.Enabled = true;
                btnOblivionBrowse.Enabled = true;
            }
            else
            {
                txtOblivionPath.Enabled = false;
                btnOblivionBrowse.Enabled = false;
            }
        }

        private void cbInstallForSkyrim_CheckedChanged(object sender, EventArgs e)
        {
            if (cbInstallForSkyrim.Checked)
            {
                txtSkyrimPath.Enabled = true;
                btnSkyrimBrowse.Enabled = true;
            }
            else
            {
                txtSkyrimPath.Enabled = false;
                btnSkyrimBrowse.Enabled = false;
            }
        }

        private void btnMorrowindBrowse_Click(object sender, EventArgs e)
        {
            fbdBrowse.SelectedPath = txtMorrowindPath.Text;

            if (fbdBrowse.ShowDialog() == DialogResult.OK)
            {
                txtMorrowindPath.Text = fbdBrowse.SelectedPath;
            }
        }

        private void cbInstallForMorrowind_CheckedChanged(object sender, EventArgs e)
        {
            if (cbInstallForMorrowind.Checked)
            {
                txtMorrowindPath.Enabled = true;
                btnMorrowindBrowse.Enabled = true;
            }
            else
            {
                txtMorrowindPath.Enabled = false;
                btnMorrowindBrowse.Enabled = false;
            }
        }
	}
}
