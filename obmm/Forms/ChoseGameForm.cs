using OblivionModManager.Classes;
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
    public partial class ChoseGameForm : Form
    {
        public Game ChosenGame = null;

        public ChoseGameForm(Game[] games, bool startMode = true)
        {
            InitializeComponent();
            this.dgGames.AutoGenerateColumns = false;
            this.dgGames.DataSource = games;

            if (!startMode)
            {
                this.Text = "Set game path";
                this.lblPick.Text = string.Empty;
                this.btnStart.Text = "Ok";
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (this.dgGames.SelectedRows.Count > 0)
            {
                this.ChosenGame = this.dgGames.SelectedRows[0].DataBoundItem as Game;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DgGames_SelectionChanged(object sender, EventArgs e)
        {
            this.btnAddGamePath.Enabled = this.dgGames.SelectedRows.Count > 0;
            this.btnStart.Enabled = this.dgGames.SelectedRows.Count > 0;
        }

        private void btnAddGamePath_Click(object sender, EventArgs e)
        {
            SetGamePathForm frm = new Forms.SetGamePathForm(this.dgGames.SelectedRows[0].DataBoundItem as Game);

            frm.ShowDialog();
        }
    }
}
