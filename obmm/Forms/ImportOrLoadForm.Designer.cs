namespace OblivionModManager.Forms
{
    partial class ImportOrLoadForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportOrLoadForm));
            this.btnImportAndCustomize = new System.Windows.Forms.Button();
            this.btnLoadAsIs = new System.Windows.Forms.Button();
            this.lbWhatToDoWithFile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnImportAndCustomize
            // 
            this.btnImportAndCustomize.Location = new System.Drawing.Point(23, 68);
            this.btnImportAndCustomize.Name = "btnImportAndCustomize";
            this.btnImportAndCustomize.Size = new System.Drawing.Size(137, 36);
            this.btnImportAndCustomize.TabIndex = 0;
            this.btnImportAndCustomize.Text = "Import and customize";
            this.btnImportAndCustomize.UseVisualStyleBackColor = true;
            this.btnImportAndCustomize.Click += new System.EventHandler(this.btnImportAndCustomize_Click);
            // 
            // btnLoadAsIs
            // 
            this.btnLoadAsIs.Location = new System.Drawing.Point(166, 68);
            this.btnLoadAsIs.Name = "btnLoadAsIs";
            this.btnLoadAsIs.Size = new System.Drawing.Size(137, 36);
            this.btnLoadAsIs.TabIndex = 1;
            this.btnLoadAsIs.Text = "Load as is";
            this.btnLoadAsIs.UseVisualStyleBackColor = true;
            this.btnLoadAsIs.Click += new System.EventHandler(this.btnLoadAsIs_Click);
            // 
            // lbWhatToDoWithFile
            // 
            this.lbWhatToDoWithFile.AutoSize = true;
            this.lbWhatToDoWithFile.Location = new System.Drawing.Point(33, 30);
            this.lbWhatToDoWithFile.Name = "lbWhatToDoWithFile";
            this.lbWhatToDoWithFile.Size = new System.Drawing.Size(270, 13);
            this.lbWhatToDoWithFile.TabIndex = 2;
            this.lbWhatToDoWithFile.Text = "Mod has been downloaded, how do you want to add it?";
            // 
            // ImportOrLoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 123);
            this.Controls.Add(this.lbWhatToDoWithFile);
            this.Controls.Add(this.btnLoadAsIs);
            this.Controls.Add(this.btnImportAndCustomize);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImportOrLoadForm";
            this.Text = "Confirm import or load mod file";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImportAndCustomize;
        private System.Windows.Forms.Button btnLoadAsIs;
        private System.Windows.Forms.Label lbWhatToDoWithFile;
    }
}