namespace ConflictDetector {
    partial class SettingsForm {
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

        #region Windows Forms Designer generated code
        private System.Windows.Forms.Button bOpen;
        private System.Windows.Forms.Button bRun;
        private System.Windows.Forms.CheckBox cbMinor;
        private System.Windows.Forms.CheckBox cbIncludeEsps;
        private System.Windows.Forms.CheckBox cbMajor;
        private System.Windows.Forms.CheckBox cbIncludeOmods;
        private System.Windows.Forms.CheckBox cbVeryMinor;
        private System.Windows.Forms.ListBox lbFiles;
        private System.Windows.Forms.CheckBox cbIgnoreInactiveEsps;
        private System.Windows.Forms.CheckBox cbIgnoreInactiveOmods;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lbFiles = new System.Windows.Forms.ListBox();
            this.cbVeryMinor = new System.Windows.Forms.CheckBox();
            this.cbIncludeOmods = new System.Windows.Forms.CheckBox();
            this.cbMajor = new System.Windows.Forms.CheckBox();
            this.cbIncludeEsps = new System.Windows.Forms.CheckBox();
            this.cbMinor = new System.Windows.Forms.CheckBox();
            this.bRun = new System.Windows.Forms.Button();
            this.bOpen = new System.Windows.Forms.Button();
            this.cbIgnoreInactiveEsps = new System.Windows.Forms.CheckBox();
            this.cbIgnoreInactiveOmods = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Oblivion plugins(*.esp, *.esm, *.esl)|*.esp;*.esm;*,esl";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // lbFiles
            // 
            this.lbFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFiles.Location = new System.Drawing.Point(16, 201);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.Size = new System.Drawing.Size(232, 95);
            this.lbFiles.TabIndex = 8;
            // 
            // cbVeryMinor
            // 
            this.cbVeryMinor.AutoSize = true;
            this.cbVeryMinor.Location = new System.Drawing.Point(16, 8);
            this.cbVeryMinor.Name = "cbVeryMinor";
            this.cbVeryMinor.Size = new System.Drawing.Size(153, 17);
            this.cbVeryMinor.TabIndex = 0;
            this.cbVeryMinor.Text = "Display very minor conflicts";
            // 
            // cbIncludeOmods
            // 
            this.cbIncludeOmods.AutoSize = true;
            this.cbIncludeOmods.Checked = true;
            this.cbIncludeOmods.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncludeOmods.Location = new System.Drawing.Point(16, 123);
            this.cbIncludeOmods.Name = "cbIncludeOmods";
            this.cbIncludeOmods.Size = new System.Drawing.Size(95, 17);
            this.cbIncludeOmods.TabIndex = 5;
            this.cbIncludeOmods.Text = "Include omods";
            this.cbIncludeOmods.CheckedChanged += new System.EventHandler(this.cbIncludeOmods_CheckedChanged);
            // 
            // cbMajor
            // 
            this.cbMajor.AutoSize = true;
            this.cbMajor.Checked = true;
            this.cbMajor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMajor.Location = new System.Drawing.Point(16, 54);
            this.cbMajor.Name = "cbMajor";
            this.cbMajor.Size = new System.Drawing.Size(130, 17);
            this.cbMajor.TabIndex = 2;
            this.cbMajor.Text = "Display major conflicts";
            // 
            // cbIncludeEsps
            // 
            this.cbIncludeEsps.AutoSize = true;
            this.cbIncludeEsps.Checked = true;
            this.cbIncludeEsps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncludeEsps.Location = new System.Drawing.Point(16, 77);
            this.cbIncludeEsps.Name = "cbIncludeEsps";
            this.cbIncludeEsps.Size = new System.Drawing.Size(143, 17);
            this.cbIncludeEsps.TabIndex = 3;
            this.cbIncludeEsps.Text = "Include unparented esps";
            this.cbIncludeEsps.CheckedChanged += new System.EventHandler(this.cbIncludeEsps_CheckedChanged);
            // 
            // cbMinor
            // 
            this.cbMinor.AutoSize = true;
            this.cbMinor.Checked = true;
            this.cbMinor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMinor.Location = new System.Drawing.Point(16, 31);
            this.cbMinor.Name = "cbMinor";
            this.cbMinor.Size = new System.Drawing.Size(130, 17);
            this.cbMinor.TabIndex = 1;
            this.cbMinor.Text = "Display minor conflicts";
            // 
            // bRun
            // 
            this.bRun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bRun.Location = new System.Drawing.Point(16, 305);
            this.bRun.Name = "bRun";
            this.bRun.Size = new System.Drawing.Size(232, 23);
            this.bRun.TabIndex = 9;
            this.bRun.Text = "Run";
            this.bRun.Click += new System.EventHandler(this.BRunClick);
            // 
            // bOpen
            // 
            this.bOpen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bOpen.Location = new System.Drawing.Point(16, 169);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(232, 23);
            this.bOpen.TabIndex = 7;
            this.bOpen.Text = "Add extra files";
            this.bOpen.Click += new System.EventHandler(this.BOpenClick);
            // 
            // cbIgnoreInactiveEsps
            // 
            this.cbIgnoreInactiveEsps.AutoSize = true;
            this.cbIgnoreInactiveEsps.Checked = true;
            this.cbIgnoreInactiveEsps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnoreInactiveEsps.Location = new System.Drawing.Point(16, 100);
            this.cbIgnoreInactiveEsps.Name = "cbIgnoreInactiveEsps";
            this.cbIgnoreInactiveEsps.Size = new System.Drawing.Size(178, 17);
            this.cbIgnoreInactiveEsps.TabIndex = 4;
            this.cbIgnoreInactiveEsps.Text = "Ignore inactive unparented esps";
            // 
            // cbIgnoreInactiveOmods
            // 
            this.cbIgnoreInactiveOmods.AutoSize = true;
            this.cbIgnoreInactiveOmods.Checked = true;
            this.cbIgnoreInactiveOmods.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnoreInactiveOmods.Location = new System.Drawing.Point(16, 146);
            this.cbIgnoreInactiveOmods.Name = "cbIgnoreInactiveOmods";
            this.cbIgnoreInactiveOmods.Size = new System.Drawing.Size(130, 17);
            this.cbIgnoreInactiveOmods.TabIndex = 6;
            this.cbIgnoreInactiveOmods.Text = "Ignore inactive omods";
            // 
            // SettingsForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(280, 348);
            this.Controls.Add(this.cbIgnoreInactiveOmods);
            this.Controls.Add(this.cbIgnoreInactiveEsps);
            this.Controls.Add(this.cbIncludeOmods);
            this.Controls.Add(this.cbMajor);
            this.Controls.Add(this.bRun);
            this.Controls.Add(this.lbFiles);
            this.Controls.Add(this.cbIncludeEsps);
            this.Controls.Add(this.bOpen);
            this.Controls.Add(this.cbMinor);
            this.Controls.Add(this.cbVeryMinor);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(288, 375);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}