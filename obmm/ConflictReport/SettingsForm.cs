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
using System.Collections.Generic;
using System.Windows.Forms;

namespace ConflictDetector {
    public partial class SettingsForm : Form {

        public SettingsForm() {
            InitializeComponent();
            cbMajor.Checked=OblivionModManager.Settings.CDShowMajor;
            cbMinor.Checked=OblivionModManager.Settings.CDShowMinor;
            cbVeryMinor.Checked=OblivionModManager.Settings.CDShowVeryMinor;
            cbIncludeEsps.Checked=OblivionModManager.Settings.CDIncludeEsp;
            cbIgnoreInactiveEsps.Checked=OblivionModManager.Settings.CDIgnoreInactiveEsp;
            cbIncludeOmods.Checked=OblivionModManager.Settings.CDIncludeOmod;
            cbIgnoreInactiveOmods.Checked=OblivionModManager.Settings.CDIgnoreInactiveOmod;
        }

        void BOpenClick(object sender, System.EventArgs e) {
            if(openFileDialog1.ShowDialog()==DialogResult.OK) {
                lbFiles.Items.AddRange(openFileDialog1.FileNames);
            }
        }

        void BRunClick(object sender, System.EventArgs e) {
            //Save settings
            OblivionModManager.Settings.CDShowMajor=cbMajor.Checked;
            OblivionModManager.Settings.CDShowMinor=cbMinor.Checked;
            OblivionModManager.Settings.CDShowVeryMinor=cbVeryMinor.Checked;
            OblivionModManager.Settings.CDIncludeEsp=cbIncludeEsps.Checked;
            OblivionModManager.Settings.CDIgnoreInactiveEsp=cbIgnoreInactiveEsps.Checked;
            OblivionModManager.Settings.CDIncludeOmod=cbIncludeOmods.Checked;
            OblivionModManager.Settings.CDIgnoreInactiveOmod=cbIgnoreInactiveOmods.Checked;
            //Run report
            ReportGenerator.CheckForVeryMinor=cbVeryMinor.Checked;
            ReportGenerator.CheckForMinor=cbMinor.Checked;
            ReportGenerator.CheckForMajor=cbMajor.Checked;
            ReportGenerator.IncludeEsps=cbIncludeEsps.Checked;
            ReportGenerator.IgnoreInactiveEsps=cbIgnoreInactiveEsps.Checked;
            ReportGenerator.IncludeOmods=cbIncludeOmods.Checked;
            ReportGenerator.IgnoreInactiveOmods=cbIgnoreInactiveOmods.Checked;
            string[] add=new string[lbFiles.Items.Count];
            for(int i=0;i<add.Length;i++) add[i]=(string)lbFiles.Items[i];
            ReportGenerator.Additional=add;
            ReportGenerator.Run=true;
            Close();
        }

        private void cbIncludeEsps_CheckedChanged(object sender, EventArgs e) {
            cbIgnoreInactiveEsps.Enabled=cbIncludeEsps.Checked;
        }

        private void cbIncludeOmods_CheckedChanged(object sender, EventArgs e) {
            cbIgnoreInactiveOmods.Enabled=cbIncludeOmods.Checked;
        }
    }
}