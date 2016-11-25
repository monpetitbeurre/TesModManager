namespace OblivionModManager.Forms
{
    partial class SkyProcPatchersForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkyProcPatchersForm));
            this.lbSkyProcPatchers = new System.Windows.Forms.ListBox();
            this.btnRunSUM = new System.Windows.Forms.Button();
            this.lblSkyProc = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbSkyProcPatchers
            // 
            this.lbSkyProcPatchers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSkyProcPatchers.FormattingEnabled = true;
            this.lbSkyProcPatchers.Location = new System.Drawing.Point(0, 50);
            this.lbSkyProcPatchers.Name = "lbSkyProcPatchers";
            this.lbSkyProcPatchers.Size = new System.Drawing.Size(831, 368);
            this.lbSkyProcPatchers.TabIndex = 0;
            this.lbSkyProcPatchers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbSkyProcPatchers_MouseDoubleClick);
            // 
            // btnRunSUM
            // 
            this.btnRunSUM.Location = new System.Drawing.Point(11, 9);
            this.btnRunSUM.Name = "btnRunSUM";
            this.btnRunSUM.Size = new System.Drawing.Size(221, 31);
            this.btnRunSUM.TabIndex = 1;
            this.btnRunSUM.Text = "Run SkyProc Unified Manager (SUM)";
            this.btnRunSUM.UseVisualStyleBackColor = true;
            this.btnRunSUM.Click += new System.EventHandler(this.btnRunSUM_Click);
            // 
            // lblSkyProc
            // 
            this.lblSkyProc.AutoSize = true;
            this.lblSkyProc.Location = new System.Drawing.Point(238, 18);
            this.lblSkyProc.Name = "lblSkyProc";
            this.lblSkyProc.Size = new System.Drawing.Size(357, 13);
            this.lblSkyProc.TabIndex = 2;
            this.lblSkyProc.Text = "SkyProc Unified Manager allows you to manage all your SkyProc Patchers";
            // 
            // SkyProcPatchersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 418);
            this.Controls.Add(this.lblSkyProc);
            this.Controls.Add(this.btnRunSUM);
            this.Controls.Add(this.lbSkyProcPatchers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SkyProcPatchersForm";
            this.Text = "SkyProc Patchers (run SUM or double click to start the selected patcher)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbSkyProcPatchers;
        private System.Windows.Forms.Button btnRunSUM;
        private System.Windows.Forms.Label lblSkyProc;
    }
}