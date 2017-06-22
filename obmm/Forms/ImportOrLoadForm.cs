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
    public partial class ImportOrLoadForm : Form
    {
        public ImportOrLoadForm(string fileName, string modName = "")
        {
            InitializeComponent();
        }

        public bool ImportAsIs = true;

        private void btnImportAndCustomize_Click(object sender, EventArgs e)
        {
            this.ImportAsIs = false;
            this.Close();
        }

        private void btnLoadAsIs_Click(object sender, EventArgs e)
        {
            this.ImportAsIs = true;
            this.Close();
        }
    }
}
