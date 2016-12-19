namespace OblivionModManager {
    partial class SaveForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lName = new System.Windows.Forms.Label();
            this.lLocation = new System.Windows.Forms.Label();
            this.lDate = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvPlugins = new System.Windows.Forms.ListView();
            this.SavePluginColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SaveImageList = new System.Windows.Forms.ImageList(this.components);
            this.lvSaves = new System.Windows.Forms.ListView();
            this.SaveFileNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SavePlayerNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SaveLocationColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SaveDateSavedColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SaveFileSizeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.DudMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bSync = new System.Windows.Forms.Button();
            this.lGametime = new System.Windows.Forms.Label();
            this.btnActivateMissingMods = new System.Windows.Forms.Button();
            this.gView = new System.Windows.Forms.GroupBox();
            this.radDetails = new System.Windows.Forms.RadioButton();
            this.radList = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gView.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(73, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 192);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(12, 207);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(71, 13);
            this.lName.TabIndex = 1;
            this.lName.Text = "Player name: ";
            // 
            // lLocation
            // 
            this.lLocation.AutoSize = true;
            this.lLocation.Location = new System.Drawing.Point(12, 220);
            this.lLocation.Name = "lLocation";
            this.lLocation.Size = new System.Drawing.Size(82, 13);
            this.lLocation.TabIndex = 2;
            this.lLocation.Text = "Player location: ";
            // 
            // lDate
            // 
            this.lDate.AutoSize = true;
            this.lDate.Location = new System.Drawing.Point(12, 233);
            this.lDate.Name = "lDate";
            this.lDate.Size = new System.Drawing.Size(68, 13);
            this.lDate.TabIndex = 4;
            this.lDate.Text = "Date saved: ";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(15, 249);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvPlugins);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvSaves);
            this.splitContainer1.Size = new System.Drawing.Size(578, 381);
            this.splitContainer1.SplitterDistance = 189;
            this.splitContainer1.TabIndex = 6;
            // 
            // lvPlugins
            // 
            this.lvPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SavePluginColumn});
            this.lvPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPlugins.Location = new System.Drawing.Point(0, 0);
            this.lvPlugins.Name = "lvPlugins";
            this.lvPlugins.Size = new System.Drawing.Size(578, 189);
            this.lvPlugins.SmallImageList = this.SaveImageList;
            this.lvPlugins.TabIndex = 0;
            this.lvPlugins.UseCompatibleStateImageBehavior = false;
            this.lvPlugins.View = System.Windows.Forms.View.Details;
            // 
            // SavePluginColumn
            // 
            this.SavePluginColumn.Text = "Plugin";
            this.SavePluginColumn.Width = 561;
            // 
            // SaveImageList
            // 
            this.SaveImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SaveImageList.ImageStream")));
            this.SaveImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.SaveImageList.Images.SetKeyName(0, "black.bmp");
            this.SaveImageList.Images.SetKeyName(1, "red.bmp");
            this.SaveImageList.Images.SetKeyName(2, "orange.bmp");
            this.SaveImageList.Images.SetKeyName(3, "green.bmp");
            // 
            // lvSaves
            // 
            this.lvSaves.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SaveFileNameColumn,
            this.SavePlayerNameColumn,
            this.SaveLocationColumn,
            this.SaveDateSavedColumn,
            this.SaveFileSizeColumn});
            this.lvSaves.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSaves.FullRowSelect = true;
            this.lvSaves.Location = new System.Drawing.Point(0, 0);
            this.lvSaves.MultiSelect = false;
            this.lvSaves.Name = "lvSaves";
            this.lvSaves.ShowItemToolTips = true;
            this.lvSaves.Size = new System.Drawing.Size(578, 188);
            this.lvSaves.SmallImageList = this.SaveImageList;
            this.lvSaves.TabIndex = 0;
            this.lvSaves.UseCompatibleStateImageBehavior = false;
            this.lvSaves.View = System.Windows.Forms.View.Details;
            this.lvSaves.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvSaves_ColumnClick);
            this.lvSaves.SelectedIndexChanged += new System.EventHandler(this.lvSaves_SelectedIndexChanged);
            // 
            // SaveFileNameColumn
            // 
            this.SaveFileNameColumn.Text = "FileName";
            this.SaveFileNameColumn.Width = 174;
            // 
            // SavePlayerNameColumn
            // 
            this.SavePlayerNameColumn.Text = "Player name";
            this.SavePlayerNameColumn.Width = 106;
            // 
            // SaveLocationColumn
            // 
            this.SaveLocationColumn.Text = "Location";
            this.SaveLocationColumn.Width = 96;
            // 
            // SaveDateSavedColumn
            // 
            this.SaveDateSavedColumn.Text = "Date saved";
            this.SaveDateSavedColumn.Width = 103;
            // 
            // SaveFileSizeColumn
            // 
            this.SaveFileSizeColumn.Text = "File size";
            this.SaveFileSizeColumn.Width = 76;
            // 
            // cmbSort
            // 
            this.cmbSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbSort.ContextMenuStrip = this.DudMenu;
            this.cmbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Items.AddRange(new object[] {
            "File name",
            "Player name",
            "Location",
            "Date saved",
            "File size"});
            this.cmbSort.Location = new System.Drawing.Point(15, 636);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(121, 21);
            this.cmbSort.TabIndex = 1;
            this.cmbSort.SelectedIndexChanged += new System.EventHandler(this.cmbSort_SelectedIndexChanged);
            this.cmbSort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbSort_KeyPress);
            // 
            // DudMenu
            // 
            this.DudMenu.Name = "DudMenu";
            this.DudMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // bSync
            // 
            this.bSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSync.Location = new System.Drawing.Point(458, 636);
            this.bSync.Name = "bSync";
            this.bSync.Size = new System.Drawing.Size(135, 23);
            this.bSync.TabIndex = 3;
            this.bSync.Text = "Sync active plugins";
            this.bSync.UseVisualStyleBackColor = true;
            this.bSync.Click += new System.EventHandler(this.bSync_Click);
            // 
            // lGametime
            // 
            this.lGametime.AutoSize = true;
            this.lGametime.Location = new System.Drawing.Point(226, 220);
            this.lGametime.Name = "lGametime";
            this.lGametime.Size = new System.Drawing.Size(10, 13);
            this.lGametime.TabIndex = 10;
            this.lGametime.Text = " ";
            // 
            // btnActivateMissingMods
            // 
            this.btnActivateMissingMods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActivateMissingMods.Location = new System.Drawing.Point(317, 636);
            this.btnActivateMissingMods.Name = "btnActivateMissingMods";
            this.btnActivateMissingMods.Size = new System.Drawing.Size(135, 23);
            this.btnActivateMissingMods.TabIndex = 2;
            this.btnActivateMissingMods.Text = "Activate missing mods";
            this.btnActivateMissingMods.UseVisualStyleBackColor = true;
            this.btnActivateMissingMods.Click += new System.EventHandler(this.btnActivateMissingMods_Click);
            // 
            // gView
            // 
            this.gView.Controls.Add(this.radDetails);
            this.gView.Controls.Add(this.radList);
            this.gView.Location = new System.Drawing.Point(485, 179);
            this.gView.Name = "gView";
            this.gView.Size = new System.Drawing.Size(107, 66);
            this.gView.TabIndex = 11;
            this.gView.TabStop = false;
            // 
            // radDetails
            // 
            this.radDetails.AutoSize = true;
            this.radDetails.Checked = true;
            this.radDetails.Location = new System.Drawing.Point(20, 36);
            this.radDetails.Name = "radDetails";
            this.radDetails.Size = new System.Drawing.Size(57, 17);
            this.radDetails.TabIndex = 1;
            this.radDetails.TabStop = true;
            this.radDetails.Text = "Details";
            this.radDetails.UseVisualStyleBackColor = true;
            this.radDetails.CheckedChanged += new System.EventHandler(this.radDetails_CheckedChanged);
            // 
            // radList
            // 
            this.radList.AutoSize = true;
            this.radList.Location = new System.Drawing.Point(20, 14);
            this.radList.Name = "radList";
            this.radList.Size = new System.Drawing.Size(41, 17);
            this.radList.TabIndex = 0;
            this.radList.TabStop = true;
            this.radList.Text = "List";
            this.radList.UseVisualStyleBackColor = true;
            this.radList.CheckedChanged += new System.EventHandler(this.radList_CheckedChanged);
            // 
            // SaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 665);
            this.Controls.Add(this.gView);
            this.Controls.Add(this.btnActivateMissingMods);
            this.Controls.Add(this.lGametime);
            this.Controls.Add(this.cmbSort);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.bSync);
            this.Controls.Add(this.lDate);
            this.Controls.Add(this.lLocation);
            this.Controls.Add(this.lName);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(421, 555);
            this.Name = "SaveForm";
            this.Text = "Save games";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gView.ResumeLayout(false);
            this.gView.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.Label lLocation;
        private System.Windows.Forms.Label lDate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvPlugins;
        private System.Windows.Forms.ListView lvSaves;
        private System.Windows.Forms.ImageList SaveImageList;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.ContextMenuStrip DudMenu;
        private System.Windows.Forms.Button bSync;
        private System.Windows.Forms.Label lGametime;
        private System.Windows.Forms.Button btnActivateMissingMods;
        private System.Windows.Forms.ColumnHeader SavePluginColumn;
        private System.Windows.Forms.ColumnHeader SaveFileNameColumn;
        private System.Windows.Forms.ColumnHeader SavePlayerNameColumn;
        private System.Windows.Forms.ColumnHeader SaveLocationColumn;
        private System.Windows.Forms.ColumnHeader SaveDateSavedColumn;
        private System.Windows.Forms.ColumnHeader SaveFileSizeColumn;
        private System.Windows.Forms.GroupBox gView;
        private System.Windows.Forms.RadioButton radDetails;
        private System.Windows.Forms.RadioButton radList;
    }
}