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
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Windows.Forms;

namespace OblivionModManager.Forms {
    public partial class omodEnabler : Form {
        public omodEnabler() {
            InitializeComponent();
            //System.Collections.Generic.List<int> list=new System.Collections.Generic.List<int>();
            int i=0;
            foreach(omod o in Program.Data.omods) {
                lbSelect.Items.Add(o.FileName);
                //lbSelect.SelectedIndices
                if(o.Hidden) lbSelect.SelectedIndices.Add(i);
                //if(o.Hidden) list.Add(i);
                i++;
            }
            //lbSelect.
        }

        private void omodEnabler_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bool[] disabled = new bool[lbSelect.Items.Count];
            Array.Clear(disabled, 0, disabled.Length);
            foreach (int i in lbSelect.SelectedIndices) disabled[i] = true;
            for (int i = 0; i < lbSelect.Items.Count; i++)
            {
                if (disabled[i] != Program.Data.omods[i].Hidden)
                {
                    if (disabled[i]) Program.Data.omods[i].Hide();
                    else Program.Data.omods[i].Show();
                }
            }
            this.Close();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lbSelect.Items.Count; i++)
            {
                lbSelect.SetSelected(i, true);
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            lbSelect.ClearSelected();
        }
    }
}
