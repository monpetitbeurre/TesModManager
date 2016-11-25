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
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace OblivionModManager {
    public class ProgressForm : Form {
        #region FormDesignerGunk
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.pbRatio = new System.Windows.Forms.ProgressBar();
            this.lProgress = new System.Windows.Forms.Label();
            this.lRatio = new System.Windows.Forms.Label();
            this.bCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.ForeColor = System.Drawing.Color.Lime;
            this.pbProgress.Location = new System.Drawing.Point(12, 12);
            this.pbProgress.Maximum = 10000;
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(255, 15);
            this.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbProgress.TabIndex = 0;
            // 
            // pbRatio
            // 
            this.pbRatio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbRatio.ForeColor = System.Drawing.Color.Lime;
            this.pbRatio.Location = new System.Drawing.Point(12, 33);
            this.pbRatio.Maximum = 10000;
            this.pbRatio.Name = "pbRatio";
            this.pbRatio.Size = new System.Drawing.Size(255, 15);
            this.pbRatio.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbRatio.TabIndex = 1;
            // 
            // lProgress
            // 
            this.lProgress.AutoSize = true;
            this.lProgress.Location = new System.Drawing.Point(273, 10);
            this.lProgress.Name = "lProgress";
            this.lProgress.Size = new System.Drawing.Size(0, 13);
            this.lProgress.TabIndex = 2;
            // 
            // lRatio
            // 
            this.lRatio.AutoSize = true;
            this.lRatio.Location = new System.Drawing.Point(273, 31);
            this.lRatio.Name = "lRatio";
            this.lRatio.Size = new System.Drawing.Size(0, 13);
            this.lRatio.TabIndex = 3;
            // 
            // bCancel
            // 
            this.bCancel.Enabled = false;
            this.bCancel.Location = new System.Drawing.Point(107, 54);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 4;
            this.bCancel.Text = "Cancel";
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // ProgressForm
            // 
            this.ClientSize = new System.Drawing.Size(309, 88);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.lRatio);
            this.Controls.Add(this.lProgress);
            this.Controls.Add(this.pbRatio);
            this.Controls.Add(this.pbProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressForm";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.ProgressBar pbRatio;
        private System.Windows.Forms.Label lProgress;
        private System.Windows.Forms.Label lRatio;
        #endregion
        private Button bCancel;
        private string Error;
        private bool BlockClose=true;

        public bool bCancelled = false;

        public ProgressForm(string title,bool ShowRatio) {
            InitializeComponent();
        	Application.UseWaitCursor=true;
            Text=title;
            if(!ShowRatio) {
                pbRatio.Visible=false;
                lRatio.Visible=false;
                pbProgress.Height+=21;
                lProgress.Top+=10;
            }
            this.Closing+=new CancelEventHandler(ProgressForm_FormClosing);
        }

        public void SetProgressRange(int high) {
            pbProgress.Maximum=high;
        }

        public void EnableCancel(string error) {
            Error=error;
            bCancel.Enabled=true;
        }

        public void Unblock() { BlockClose=false; }

        private volatile float vProgress;
        private volatile float vRatio;
        public void ThreadUpdate(float progress, float ratio) {
            vProgress=progress;
            vRatio=ratio;
        }

        delegate void updateprogressDelegate(int value, string text);

        private void updateprogress(int value, string text)
        {
            int newvalue = 0;
            if (value!=-1)
                newvalue = value;
            else
                newvalue=pbProgress.Value+1;

            if (!pbProgress.InvokeRequired)
            {
                if (newvalue>pbProgress.Minimum && newvalue<=pbProgress.Maximum) pbProgress.Value = newvalue;
                lProgress.Text = text; // ((int)(100 * (float)pbProgress.Value / (float)pbProgress.Maximum)).ToString() + "%";
                if (!Focused) Focus();
            }
            else
            {
                try
                {
                    newvalue = (newvalue <= pbProgress.Maximum && newvalue >= pbProgress.Minimum) ? newvalue : pbProgress.Value;
                    this.Invoke(new MethodInvoker(delegate { pbProgress.Value = newvalue; }));
                    this.Invoke(new MethodInvoker(delegate { lProgress.Text = text; }));
                }
                catch { };
            }
        }
        public void UpdateProgress() {
            //if (!pbProgress.InvokeRequired)
            //{
                //pbProgress.Value++;
                //lProgress.Text = ((int)(100 * (float)pbProgress.Value / (float)pbProgress.Maximum)).ToString() + "%";
                //if (!Focused) Focus();
                updateprogress(-1, ((int)(100 * (float)pbProgress.Value / (float)pbProgress.Maximum)).ToString() + "%");
            //}
            //else
            //{
            //    updateprogressDelegate dlg = new updateprogressDelegate(updateprogress);
            //    pbProgress.Invoke(dlg,-1, new object[] { ((int)(100 * (float)pbProgress.Value / (float)pbProgress.Maximum)).ToString() + "%"});
            //}
        }
        public void UpdateProgress(float fraction) {
            if(fraction<0||fraction>1) return;
            //if (!pbProgress.InvokeRequired)
            //{
                //pbProgress.Value = ((int)(fraction * 10000));
                //lProgress.Text = ((int)(fraction * 100)).ToString() + "%";
                //if (!Focused) Focus();
                updateprogress(((int)(fraction * 10000)), ((int)(fraction * 100)).ToString() + "%");
            //}
            //else
            //{
            //    updateprogressDelegate dlg = new updateprogressDelegate(updateprogress);
            //    dlg.Invoke(((int)(fraction * 10000)), ((int)(fraction * 100)).ToString() + "%");
            //}
        }
        public void UpdateProgress(int value) {
            //if (!pbProgress.InvokeRequired)
            //{
                //pbProgress.Value = value;
                //lProgress.Text = ((int)(100 * (float)value / (float)pbProgress.Maximum)).ToString() + "%";
                //if (!Focused) Focus();
                updateprogress(value, ((int)(100 * (float)value / (float)pbProgress.Maximum)).ToString() + "%");
            //}
            //else
            //{
            //    updateprogressDelegate dlg = new updateprogressDelegate(updateprogress);
            //    dlg.Invoke(value, ((int)(100 * (float)value / (float)pbProgress.Maximum)).ToString() + "%");
            //}
        }

        public void UpdateRatio(float fraction) {
            if(fraction<0) return;
            if(fraction>1) {
                pbRatio.ForeColor=Color.Red;
                pbRatio.Value=10000;
            } else {
                pbRatio.ForeColor=Color.Lime;
                pbRatio.Value=((int)(fraction*10000));
            }
            lRatio.Text=(((int)(fraction*100)).ToString()+"%");
        }

        private void ProgressForm_FormClosing(object sender, CancelEventArgs e) {
        	if(BlockClose) {
            	e.Cancel=true;
        	} else {
        		Application.UseWaitCursor=false;
        	}
        }

        private void bCancel_Click(object sender, EventArgs e) {
            bCancelled = true;
            this.Close();
//            throw new obmmCancelException("");
        }

    }

    public class obmmCancelException : obmmException { public obmmCancelException(string msg) : base(msg) { } }
}
