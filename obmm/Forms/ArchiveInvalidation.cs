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
    public partial class ArchiveInvalidation : Form {
        public ArchiveInvalidation() {
            InitializeComponent();
            cbAutoUpdate.Checked=Settings.UpdateInvalidation;
            foreach(Control c in this.Controls) {
                if(c is CheckBox&&c!=cbAutoUpdate) {
                    c.Tag=Convert.ToUInt32((string)c.Tag);
                }
            }
            rbUniversal.Tag=(uint)ArchiveInvalidationFlags.Universal;
            rbEditBSA.Tag=(uint)ArchiveInvalidationFlags.EditBSAs;
            rbRedirection.Tag=(uint)ArchiveInvalidationFlags.BSARedirection;

            SetControls();
        }

        private void SetControls() {
            foreach(Control c in this.Controls) {
                if(c is CheckBox&&c!=cbAutoUpdate) {
                    ((CheckBox)c).Checked=(Settings.InvalidationFlags&(ArchiveInvalidationFlags)c.Tag)>0;
                }
            }
            if((Settings.InvalidationFlags&ArchiveInvalidationFlags.EditBSAs)>0) {
                rbEditBSA.Checked=true;
            } else if((Settings.InvalidationFlags&ArchiveInvalidationFlags.Universal)>0) {
                rbUniversal.Checked=true;
            } else if((Settings.InvalidationFlags&ArchiveInvalidationFlags.BSARedirection)>0) {
                rbRedirection.Checked=true;
            } else {
                rbStandard.Checked=true;
            }
        }

        private void bUpdateNow_Click(object sender, EventArgs e) {
            OblivionBSA.UpdateInvalidationFile();
            if(rbEditBSA.Checked) {
                MessageBox.Show("Files modified: "+OblivionBSA.FilesModified+"\n"+
                    "Entries modified: "+OblivionBSA.EntriesModified+"\n"+
                    "Hash collisions: "+OblivionBSA.HashCollisions, "Done");
                Program.logger.WriteToLog("Files modified: " + OblivionBSA.FilesModified + "\n" +
                    "Entries modified: " + OblivionBSA.EntriesModified + "\n" +
                    "Hash collisions: " + OblivionBSA.HashCollisions, Logger.LogLevel.High);
            }
            else if (rbStandard.Checked)
            {
                MessageBox.Show("Entries created: "+OblivionBSA.EntriesModified, "Done");
                Program.logger.WriteToLog("Entries created: " + OblivionBSA.EntriesModified, Logger.LogLevel.High);
            }
            else
            {
                MessageBox.Show("Done");
            }
        }

        private void ModeChanged(object sender, EventArgs e) {
            SuspendLayout();
            bRemoveBSAEdits.Enabled=rbEditBSA.Checked;
            if(sender==rbEditBSA&&!rbEditBSA.Checked) OblivionBSA.RestoreBSA();
            if(sender!=rbStandard) {
                ArchiveInvalidationFlags flag=(ArchiveInvalidationFlags)((Control)sender).Tag;
                if(((RadioButton)sender).Checked) {
                    Settings.InvalidationFlags|=flag;
                } else {
                    Settings.InvalidationFlags&=uint.MaxValue-flag;
                }
            }
            if(((RadioButton)sender).Checked) {
                bool b;
                if(sender==rbUniversal||sender==rbRedirection) b=false; else b=true;
                foreach(Control c in this.Controls) {
                    if(c is CheckBox&&c!=cbAutoUpdate) {
                        c.Enabled=b;
                    }
                }
                if(sender==rbEditBSA) {
                    cbBSAOnly.Enabled=false;
                    cbMatchExtensions.Enabled=false;
                    cbPackFaceTextures.Enabled=false;
                }
                if(sender==rbStandard) {
                    cbEditAllBSA.Enabled=false;
                    cbHashAI.Enabled=false;
                    cbHashWarn.Enabled=false;
                    cbPackFaceTextures.Enabled=false;
                }
                if(sender==rbRedirection) {
                    cbPackFaceTextures.Enabled=true;
                }
            }
            ResumeLayout();
        }

        private void FlagBoxChanged(object sender, EventArgs e) {
            ArchiveInvalidationFlags flag=(ArchiveInvalidationFlags)((Control)sender).Tag;
            if(((CheckBox)sender).Checked) {
                Settings.InvalidationFlags|=flag;
            } else {
                Settings.InvalidationFlags&=uint.MaxValue-flag;
            }
        }

        private void cbAutoUpdate_CheckedChanged(object sender, EventArgs e) {
            if(cbAutoUpdate.Checked&&rbRedirection.Checked&&!cbPackFaceTextures.Checked) {
                if(MessageBox.Show("BSA redirection only needs to be done once, so using autoupdate on exit is not needed.\n"+
                    "Continue anyway?", "Warning", MessageBoxButtons.YesNo)!=DialogResult.Yes) {
                    cbAutoUpdate.Checked=false;
                    return;
                }
            }
            Settings.UpdateInvalidation=cbAutoUpdate.Checked;
        }

        private void bRemoveBSAEdits_Click(object sender, EventArgs e) {
            cbAutoUpdate.Checked=false;
            int i=OblivionBSA.RestoreBSA();
            if(i==1) MessageBox.Show("1 entry restored");
            else MessageBox.Show(i.ToString()+" entries restored");
        }

        private void bResetTimestamps_Click(object sender, EventArgs e) {
            OblivionBSA.ResetTimeStamps();
        }

        private void bReset_Click(object sender, EventArgs e) {
            Settings.InvalidationFlags=ArchiveInvalidationFlags.Default;
            cbAutoUpdate.Checked=false;
            SetControls();
        }
    }
}