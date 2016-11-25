namespace OblivionModManager.Forms {
    partial class BSACreator {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSACreator));
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.DudMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmbCompression = new System.Windows.Forms.ComboBox();
            this.bCreate = new System.Windows.Forms.Button();
            this.bAddFile = new System.Windows.Forms.Button();
            this.bAddEsp = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.bAddFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.cmbCompLevel = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "BSA archives (*.bsa)|*.bsa";
            this.saveFileDialog1.InitialDirectory = "data";
            this.saveFileDialog1.RestoreDirectory = true;
            this.saveFileDialog1.Title = "Save archive as";
            // 
            // lvFiles
            // 
            this.lvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvFiles.LabelEdit = true;
            this.lvFiles.Location = new System.Drawing.Point(12, 12);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(579, 317);
            this.lvFiles.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvFiles.TabIndex = 0;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvFiles_AfterLabelEdit);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Relative path";
            this.columnHeader1.Width = 187;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File path";
            this.columnHeader2.Width = 264;
            // 
            // DudMenu
            // 
            this.DudMenu.Name = "DudMenu";
            this.DudMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // cmbCompression
            // 
            this.cmbCompression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbCompression.FormattingEnabled = true;
            this.cmbCompression.Items.AddRange(new object[] {
            "No compression",
            "Compress 20%",
            "Compress 40%",
            "Compress 60%",
            "Compress 80%",
            "Compress all",
            "Manual"});
            this.cmbCompression.Location = new System.Drawing.Point(12, 337);
            this.cmbCompression.Name = "cmbCompression";
            this.cmbCompression.Size = new System.Drawing.Size(121, 21);
            this.cmbCompression.TabIndex = 2;
            this.cmbCompression.Text = "No compression";
            this.cmbCompression.SelectedIndexChanged += new System.EventHandler(this.cmbCompression_SelectedIndexChanged);
            this.cmbCompression.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox1_KeyPress);
            // 
            // bCreate
            // 
            this.bCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCreate.Location = new System.Drawing.Point(516, 335);
            this.bCreate.Name = "bCreate";
            this.bCreate.Size = new System.Drawing.Size(75, 23);
            this.bCreate.TabIndex = 3;
            this.bCreate.Text = "Create";
            this.bCreate.UseVisualStyleBackColor = true;
            this.bCreate.Click += new System.EventHandler(this.bCreate_Click);
            // 
            // bAddFile
            // 
            this.bAddFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAddFile.Location = new System.Drawing.Point(266, 335);
            this.bAddFile.Name = "bAddFile";
            this.bAddFile.Size = new System.Drawing.Size(75, 23);
            this.bAddFile.TabIndex = 4;
            this.bAddFile.Text = "Add File(s)";
            this.bAddFile.UseVisualStyleBackColor = true;
            this.bAddFile.Click += new System.EventHandler(this.bAddFile_Click);
            // 
            // bAddEsp
            // 
            this.bAddEsp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAddEsp.Location = new System.Drawing.Point(347, 335);
            this.bAddEsp.Name = "bAddEsp";
            this.bAddEsp.Size = new System.Drawing.Size(75, 23);
            this.bAddEsp.TabIndex = 5;
            this.bAddEsp.Text = "Add esp(s)";
            this.bAddEsp.UseVisualStyleBackColor = true;
            this.bAddEsp.Click += new System.EventHandler(this.bAddEsp_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // bAddFolder
            // 
            this.bAddFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAddFolder.Location = new System.Drawing.Point(428, 335);
            this.bAddFolder.Name = "bAddFolder";
            this.bAddFolder.Size = new System.Drawing.Size(75, 23);
            this.bAddFolder.TabIndex = 6;
            this.bAddFolder.Text = "Add folder(s)";
            this.bAddFolder.UseVisualStyleBackColor = true;
            this.bAddFolder.Click += new System.EventHandler(this.bAddFolder_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Choose folder to include";
            // 
            // cmbCompLevel
            // 
            this.cmbCompLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbCompLevel.ContextMenuStrip = this.DudMenu;
            this.cmbCompLevel.Enabled = false;
            this.cmbCompLevel.FormattingEnabled = true;
            this.cmbCompLevel.Items.AddRange(new object[] {
            "Very High",
            "High",
            "Medium",
            "Low",
            "Very Low"});
            this.cmbCompLevel.Location = new System.Drawing.Point(139, 337);
            this.cmbCompLevel.Name = "cmbCompLevel";
            this.cmbCompLevel.Size = new System.Drawing.Size(121, 21);
            this.cmbCompLevel.TabIndex = 8;
            // 
            // BSACreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 371);
            this.Controls.Add(this.cmbCompLevel);
            this.Controls.Add(this.lvFiles);
            this.Controls.Add(this.bAddFolder);
            this.Controls.Add(this.bAddEsp);
            this.Controls.Add(this.bAddFile);
            this.Controls.Add(this.cmbCompression);
            this.Controls.Add(this.bCreate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(396, 338);
            this.Name = "BSACreator";
            this.Text = "BSA Creator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ContextMenuStrip DudMenu;
        private System.Windows.Forms.ComboBox cmbCompression;
        private System.Windows.Forms.Button bCreate;
        private System.Windows.Forms.Button bAddFile;
        private System.Windows.Forms.Button bAddEsp;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button bAddFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox cmbCompLevel;
    }
}