using OblivionModManager.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OblivionModManager.Forms
{
    public partial class SetGamePathForm : Form
    {
        Game game = null;

        public SetGamePathForm(Game game)
        {
            InitializeComponent();

            this.game = game;
            this.lblPathTo.Text = "Path to " + game.Name;
            this.txtGamePath.Text = game.GamePath;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(this.txtGamePath.Text, this.game.ExeName)) || MessageBox.Show(this.game.ExeName + " was not found in this folder. Save?", "No game found", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.game.GamePath = this.txtGamePath.Text;
                try
                {
                    Microsoft.Win32.RegistryKey lm = null;
                    lm = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\TesModManager", true);
                    if (lm == null)
                    {
                        lm = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\TesModManager");
                    }
                    lm.SetValue(this.game.Name, this.game.GamePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not save folder to registry: " + ex.Message + ". You will need to pick it next time too", "Failed to write to registry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnBrowseForGamePath_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Path.Combine(this.txtGamePath.Text, this.game.ExeName)))
            {
                MessageBox.Show(this.game.ExeName + " was not found in this folder!", "No game found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
