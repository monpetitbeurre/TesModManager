using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace OblivionModManager.Forms
{
    public partial class frmNexusLogin : Form
    {
        public bool bCancelled;
        public string username;
        public string password;
        public bool bRemember = false;

        public frmNexusLogin(string nexususername, string nexuspassword)
        {
            InitializeComponent();
            txtNexusUsername.Text = nexususername;
            txtNexusPassword.Text = nexuspassword;
            this.BringToFront();
            cbRememberCredentials.Checked=Properties.Settings.Default.AutoLoginToNexus;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bCancelled = false;
            DialogResult = DialogResult.OK;
            username = txtNexusUsername.Text;
            password = txtNexusPassword.Text;
            bRemember = cbRememberCredentials.Checked;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            bCancelled = true;
            this.Close();
        }

        private void cbRememberCredentials_CheckedChanged(object sender, EventArgs e)
        {
            bRemember = cbRememberCredentials.Checked;
        }
    }
}
