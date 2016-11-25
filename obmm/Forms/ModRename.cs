using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OblivionModManager.Forms
{
    public partial class ModRename : Form
    {
        public string newModName="";
        public string modName = "";
        public ModRename(string modName, string modFileName)
        {
            InitializeComponent();
            this.modName = modName;
            txtModName.Text = modName;
            txtModFilename.Text = modFileName;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            newModName = txtModName.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtModName.Text = this.modName;
        }
    }
}
