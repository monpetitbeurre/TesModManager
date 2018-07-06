/* This file is part of Oblivion Mod Manager.
 * 
 * Oblivion Mod Manager is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Oblivion Mod Manager is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Windows.Forms;

namespace OblivionModManager.Forms {
    public partial class LimitedUserForm : Form {
        public LimitedUserForm() {
            InitializeComponent();
            UpdateEspList();
        }

        private void UpdateEspList() {
            System.Threading.Monitor.Enter(lvEspList);

            lvEspList.SuspendLayout();
            lvEspList.Items.Clear();
            int ActiveCount=0;
            foreach(EspInfo ei in Program.Data.Esps) {
                string toolText=ei.FileName+"\nAuthor: "+ei.header.Author;
                ListViewItem lvi=new ListViewItem(new string[] { ei.FileName, ei.BelongsTo });
                if(ei.Active) {
                    lvi.Checked=true;
                    if (ei.FileName.ToLower().EndsWith(".esl"))
                    {
                        toolText += "\n\nFormID: " + (0xFE).ToString("x").PadLeft(2, '0');
                    }
                    else
                    {
                        toolText += "\n\nFormID: " + (ActiveCount++).ToString("x").PadLeft(2, '0');
                    }
                }
                if(ei.header.Description!=null) {
                    toolText+="\n\n"+ei.header.Description;
                }
                lvi.Tag=ei;
                lvi.ToolTipText=toolText;
                lvEspList.Items.Add(lvi);
            }
            lvEspList.ResumeLayout();
            System.Threading.Monitor.Exit(lvEspList);
        }

        private void lvEspList_ItemCheck(object sender, ItemCheckEventArgs e) {
            if(((EspInfo)(lvEspList.Items[e.Index].Tag)).Active==(e.NewValue==CheckState.Checked)) return;
            if(e.NewValue==CheckState.Checked) ((EspInfo)(lvEspList.Items[e.Index].Tag)).Active=true;
            else ((EspInfo)(lvEspList.Items[e.Index].Tag)).Active=false;
        }

        private void bSaves_Click(object sender, EventArgs e) {
            (new SaveForm()).ShowDialog();
            UpdateEspList();
        }

        private void bBSAs_Click(object sender, EventArgs e) {
            (new BSABrowser()).ShowDialog();
        }

        private void bConflicts_Click(object sender, EventArgs e) {
            ConflictDetector.ReportGenerator.GenerateReport();
        }

        private void bLaunch_Click(object sender, EventArgs e) {
            Program.Launch=LaunchType.Game;
            Close();
        }
    }
}