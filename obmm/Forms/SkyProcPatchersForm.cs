using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OblivionModManager.Forms
{
    public partial class SkyProcPatchersForm : Form
    {
        string sumpath = "";
        public SkyProcPatchersForm()
        {
            InitializeComponent();

            string[] patchers = Directory.GetFiles(Path.Combine(Program.DataFolderName, "Skyproc patchers"), "*.jar", SearchOption.AllDirectories);
            lbSkyProcPatchers.Items.AddRange(patchers);

            foreach (string patcher in patchers)
            {
                if (patcher.ToLower().EndsWith("\\sum.jar"))
                {
                    btnRunSUM.Text = "Run SkyProc Unified Manager (recommended)";
                    sumpath = patcher;
                    break;
                }
                else
                    btnRunSUM.Text = "Get SkyProc Unified Manager (recommended)";

            }
        }

        private void startPatcher(string patchername)
        {
            Process patcher = new Process();
            patcher.StartInfo = new ProcessStartInfo(Path.GetFileName(patchername)); //java[0]); //Path.GetFileName(patchername)); //"cmd.exe");
            patcher.StartInfo.Arguments = ""; //-Xms400 -jar " + Path.GetFileName(patchername); // "start /k " + Path.GetFileName(patchername);
            patcher.StartInfo.WorkingDirectory = Path.GetDirectoryName(patchername);
            patcher.StartInfo.UseShellExecute = true;
            patcher.Start();
            patcher.WaitForExit();
        }

        private void lbSkyProcPatchers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //string[] java = new string[1];
            //if (Directory.Exists("c:\\Program files (x86)\\Java"))
            //    java = Directory.GetFiles("c:\\Program files (x86)\\Java", "java.exe", SearchOption.AllDirectories);
            //else if (Directory.Exists("c:\\Program files\\Java"))
            //    java = Directory.GetFiles("c:\\Program files\\Java", "java.exe", SearchOption.AllDirectories);
            string patchername = lbSkyProcPatchers.SelectedItem as string;

            startPatcher(patchername);
        }

        private void btnRunSUM_Click(object sender, EventArgs e)
        {
            if (btnRunSUM.Text.Contains("Run"))
            {
                startPatcher(sumpath);
            }
            else
            {
                Process.Start("http://www.nexusmods.com/skyrim/mods/29865");
            }
        }
    }
}
