namespace OblivionModManager {
    partial class CreateModForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateModForm));
            this.label4 = new System.Windows.Forms.Label();
            this.grpCompression = new System.Windows.Forms.GroupBox();
            this.cbOmod2 = new System.Windows.Forms.CheckBox();
            this.cmbModCompLevel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDataCompLevel = new System.Windows.Forms.ComboBox();
            this.cmbCompType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bRemoveScreenshot = new System.Windows.Forms.Button();
            this.bGroups = new System.Windows.Forms.Button();
            this.ScreenshotPic = new System.Windows.Forms.PictureBox();
            this.tbVersion = new System.Windows.Forms.TextBox();
            this.bScreenshot = new System.Windows.Forms.Button();
            this.OpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.bAddZip = new System.Windows.Forms.Button();
            this.FolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.cmsGroups = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bUp = new System.Windows.Forms.Button();
            this.bDown = new System.Windows.Forms.Button();
            this.rbData = new System.Windows.Forms.RadioButton();
            this.tbName = new System.Windows.Forms.TextBox();
            this.bEdDescription = new System.Windows.Forms.Button();
            this.rbPlugins = new System.Windows.Forms.RadioButton();
            this.bCreate = new System.Windows.Forms.Button();
            this.bEdReadme = new System.Windows.Forms.Button();
            this.bAddFromFolder = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbWebsite = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbAuthor = new System.Windows.Forms.TextBox();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FilesContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useAsImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importModDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanForDataFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewRequiredDataFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbIncludeVersion = new System.Windows.Forms.CheckBox();
            this.bEdScript = new System.Windows.Forms.Button();
            this.bAdd = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.grpInformation = new System.Windows.Forms.GroupBox();
            this.btnOCDList = new System.Windows.Forms.Button();
            this.btnTESNexus = new System.Windows.Forms.Button();
            this.backgroundModCreator = new System.ComponentModel.BackgroundWorker();
            this.gbDataMod = new System.Windows.Forms.GroupBox();
            this.rdDataMod = new System.Windows.Forms.RadioButton();
            this.rdSystemMod = new System.Windows.Forms.RadioButton();
            this.CreateModToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grpCompression.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScreenshotPic)).BeginInit();
            this.FilesContextMenu.SuspendLayout();
            this.grpInformation.SuspendLayout();
            this.gbDataMod.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Name";
            // 
            // grpCompression
            // 
            this.grpCompression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCompression.Controls.Add(this.gbDataMod);
            this.grpCompression.Controls.Add(this.cbOmod2);
            this.grpCompression.Controls.Add(this.cmbModCompLevel);
            this.grpCompression.Controls.Add(this.label1);
            this.grpCompression.Controls.Add(this.cmbDataCompLevel);
            this.grpCompression.Controls.Add(this.cmbCompType);
            this.grpCompression.Controls.Add(this.label2);
            this.grpCompression.Controls.Add(this.label3);
            this.grpCompression.Location = new System.Drawing.Point(12, 122);
            this.grpCompression.Name = "grpCompression";
            this.grpCompression.Size = new System.Drawing.Size(356, 174);
            this.grpCompression.TabIndex = 50;
            this.grpCompression.TabStop = false;
            this.grpCompression.Text = "Compression";
            // 
            // cbOmod2
            // 
            this.cbOmod2.AutoSize = true;
            this.cbOmod2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbOmod2.Checked = true;
            this.cbOmod2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOmod2.Location = new System.Drawing.Point(96, 113);
            this.cbOmod2.Name = "cbOmod2";
            this.cbOmod2.Size = new System.Drawing.Size(85, 17);
            this.cbOmod2.TabIndex = 17;
            this.cbOmod2.Text = "OMod v2     ";
            this.cbOmod2.UseVisualStyleBackColor = true;
            this.cbOmod2.CheckedChanged += new System.EventHandler(this.cbOmod2_CheckedChanged);
            // 
            // cmbModCompLevel
            // 
            this.cmbModCompLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModCompLevel.Items.AddRange(new object[] {
            "Very high",
            "High",
            "Medium",
            "Low",
            "Very low",
            "None"});
            this.cmbModCompLevel.Location = new System.Drawing.Point(166, 80);
            this.cmbModCompLevel.Name = "cmbModCompLevel";
            this.cmbModCompLevel.Size = new System.Drawing.Size(121, 21);
            this.cmbModCompLevel.TabIndex = 15;
            this.cmbModCompLevel.SelectedIndexChanged += new System.EventHandler(this.cmbModCompLevel_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Data Files Compression";
            // 
            // cmbDataCompLevel
            // 
            this.cmbDataCompLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataCompLevel.Enabled = false;
            this.cmbDataCompLevel.Items.AddRange(new object[] {
            "Very high",
            "High",
            "Medium",
            "Low",
            "Very low"});
            this.cmbDataCompLevel.Location = new System.Drawing.Point(166, 53);
            this.cmbDataCompLevel.Name = "cmbDataCompLevel";
            this.cmbDataCompLevel.Size = new System.Drawing.Size(121, 21);
            this.cmbDataCompLevel.TabIndex = 13;
            // 
            // cmbCompType
            // 
            this.cmbCompType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompType.Items.AddRange(new object[] {
            "7-zip",
            "zip"});
            this.cmbCompType.Location = new System.Drawing.Point(166, 29);
            this.cmbCompType.Name = "cmbCompType";
            this.cmbCompType.Size = new System.Drawing.Size(121, 21);
            this.cmbCompType.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Data Files Compression Level";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "OMod Compression Level";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Version";
            // 
            // bRemoveScreenshot
            // 
            this.bRemoveScreenshot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bRemoveScreenshot.Location = new System.Drawing.Point(12, 605);
            this.bRemoveScreenshot.Name = "bRemoveScreenshot";
            this.bRemoveScreenshot.Size = new System.Drawing.Size(90, 23);
            this.bRemoveScreenshot.TabIndex = 49;
            this.bRemoveScreenshot.Text = "Remove Image";
            this.bRemoveScreenshot.Click += new System.EventHandler(this.bRemoveScreenshot_Click);
            // 
            // bGroups
            // 
            this.bGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bGroups.Location = new System.Drawing.Point(326, 605);
            this.bGroups.Name = "bGroups";
            this.bGroups.Size = new System.Drawing.Size(80, 23);
            this.bGroups.TabIndex = 47;
            this.bGroups.Text = "Groups";
            this.bGroups.Click += new System.EventHandler(this.bGroups_Click);
            // 
            // ScreenshotPic
            // 
            this.ScreenshotPic.Location = new System.Drawing.Point(12, 345);
            this.ScreenshotPic.Name = "ScreenshotPic";
            this.ScreenshotPic.Size = new System.Drawing.Size(300, 225);
            this.ScreenshotPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ScreenshotPic.TabIndex = 46;
            this.ScreenshotPic.TabStop = false;
            this.ScreenshotPic.Visible = false;
            // 
            // tbVersion
            // 
            this.tbVersion.Location = new System.Drawing.Point(76, 43);
            this.tbVersion.MaxLength = 10;
            this.tbVersion.Name = "tbVersion";
            this.tbVersion.Size = new System.Drawing.Size(121, 20);
            this.tbVersion.TabIndex = 3;
            this.tbVersion.Text = "1.0";
            this.tbVersion.Leave += new System.EventHandler(this.tbVersion_Leave);
            // 
            // bScreenshot
            // 
            this.bScreenshot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bScreenshot.Location = new System.Drawing.Point(12, 576);
            this.bScreenshot.Name = "bScreenshot";
            this.bScreenshot.Size = new System.Drawing.Size(90, 23);
            this.bScreenshot.TabIndex = 45;
            this.bScreenshot.Text = "Add Image";
            this.bScreenshot.Click += new System.EventHandler(this.bScreenshot_Click);
            this.bScreenshot.MouseEnter += new System.EventHandler(this.bScreenshot_MouseEnter);
            this.bScreenshot.MouseLeave += new System.EventHandler(this.bScreenshot_MouseLeave);
            // 
            // OpenDialog
            // 
            this.OpenDialog.Multiselect = true;
            this.OpenDialog.RestoreDirectory = true;
            // 
            // bAddZip
            // 
            this.bAddZip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bAddZip.Location = new System.Drawing.Point(374, 202);
            this.bAddZip.Name = "bAddZip";
            this.bAddZip.Size = new System.Drawing.Size(100, 23);
            this.bAddZip.TabIndex = 39;
            this.bAddZip.Text = "Add Archive";
            this.bAddZip.Click += new System.EventHandler(this.bAddZip_Click);
            // 
            // FolderDialog
            // 
            this.FolderDialog.Description = "Select mod directory";
            this.FolderDialog.ShowNewFolderButton = false;
            // 
            // cmsGroups
            // 
            this.cmsGroups.Name = "cmsGroups";
            this.cmsGroups.Size = new System.Drawing.Size(61, 4);
            // 
            // bUp
            // 
            this.bUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bUp.Location = new System.Drawing.Point(326, 576);
            this.bUp.Name = "bUp";
            this.bUp.Size = new System.Drawing.Size(80, 23);
            this.bUp.TabIndex = 43;
            this.bUp.Text = "Move Up";
            this.bUp.Click += new System.EventHandler(this.bUp_Click);
            // 
            // bDown
            // 
            this.bDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bDown.Location = new System.Drawing.Point(412, 576);
            this.bDown.Name = "bDown";
            this.bDown.Size = new System.Drawing.Size(80, 23);
            this.bDown.TabIndex = 44;
            this.bDown.Text = "Move Down";
            this.bDown.Click += new System.EventHandler(this.bDown_Click);
            // 
            // rbData
            // 
            this.rbData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbData.Location = new System.Drawing.Point(108, 608);
            this.rbData.Name = "rbData";
            this.rbData.Size = new System.Drawing.Size(69, 17);
            this.rbData.TabIndex = 42;
            this.rbData.Text = "Data Files";
            this.rbData.CheckedChanged += new System.EventHandler(this.rbCheckedChanged);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(76, 19);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(121, 20);
            this.tbName.TabIndex = 0;
            // 
            // bEdDescription
            // 
            this.bEdDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bEdDescription.Location = new System.Drawing.Point(374, 175);
            this.bEdDescription.Name = "bEdDescription";
            this.bEdDescription.Size = new System.Drawing.Size(100, 23);
            this.bEdDescription.TabIndex = 36;
            this.bEdDescription.Text = "Edit Description";
            this.bEdDescription.Click += new System.EventHandler(this.bEdDescription_Click);
            this.bEdDescription.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BEdDescriptionMouseUp);
            // 
            // rbPlugins
            // 
            this.rbPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbPlugins.Checked = true;
            this.rbPlugins.Location = new System.Drawing.Point(108, 579);
            this.rbPlugins.Name = "rbPlugins";
            this.rbPlugins.Size = new System.Drawing.Size(59, 17);
            this.rbPlugins.TabIndex = 41;
            this.rbPlugins.TabStop = true;
            this.rbPlugins.Text = "Plugins";
            this.rbPlugins.CheckedChanged += new System.EventHandler(this.rbCheckedChanged);
            // 
            // bCreate
            // 
            this.bCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCreate.Location = new System.Drawing.Point(412, 605);
            this.bCreate.Name = "bCreate";
            this.bCreate.Size = new System.Drawing.Size(80, 23);
            this.bCreate.TabIndex = 48;
            this.bCreate.Text = "Create OMod";
            this.bCreate.Click += new System.EventHandler(this.bCreate_Click);
            // 
            // bEdReadme
            // 
            this.bEdReadme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bEdReadme.Location = new System.Drawing.Point(374, 122);
            this.bEdReadme.Name = "bEdReadme";
            this.bEdReadme.Size = new System.Drawing.Size(100, 23);
            this.bEdReadme.TabIndex = 34;
            this.bEdReadme.Text = "Edit Readme";
            this.bEdReadme.Click += new System.EventHandler(this.bEdReadme_Click);
            this.bEdReadme.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BEdReadmeMouseUp);
            // 
            // bAddFromFolder
            // 
            this.bAddFromFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bAddFromFolder.Location = new System.Drawing.Point(374, 231);
            this.bAddFromFolder.Name = "bAddFromFolder";
            this.bAddFromFolder.Size = new System.Drawing.Size(100, 23);
            this.bAddFromFolder.TabIndex = 38;
            this.bAddFromFolder.Text = "Add Folder";
            this.bAddFromFolder.Click += new System.EventHandler(this.bAddFromFolder_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Website";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File path";
            this.columnHeader2.Width = 170;
            // 
            // tbWebsite
            // 
            this.tbWebsite.Location = new System.Drawing.Point(76, 68);
            this.tbWebsite.Name = "tbWebsite";
            this.tbWebsite.Size = new System.Drawing.Size(121, 20);
            this.tbWebsite.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(212, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Author";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Relative path";
            this.columnHeader1.Width = 163;
            // 
            // tbAuthor
            // 
            this.tbAuthor.Location = new System.Drawing.Point(256, 19);
            this.tbAuthor.Name = "tbAuthor";
            this.tbAuthor.Size = new System.Drawing.Size(116, 20);
            this.tbAuthor.TabIndex = 5;
            // 
            // lvFiles
            // 
            this.lvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvFiles.ContextMenuStrip = this.FilesContextMenu;
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.HideSelection = false;
            this.lvFiles.LabelEdit = true;
            this.lvFiles.Location = new System.Drawing.Point(12, 302);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(480, 268);
            this.lvFiles.TabIndex = 40;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvFiles_AfterLabelEdit);
            this.lvFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvFiles_KeyDown);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Source";
            // 
            // FilesContextMenu
            // 
            this.FilesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.useAsImageToolStripMenuItem,
            this.validateToolStripMenuItem,
            this.importModDetailsToolStripMenuItem,
            this.scanForDataFilesToolStripMenuItem,
            this.viewRequiredDataFilesToolStripMenuItem});
            this.FilesContextMenu.Name = "FilesContextMenu";
            this.FilesContextMenu.Size = new System.Drawing.Size(197, 158);
            this.FilesContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.FilesContextMenu_Opening);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.openToolStripMenuItem.Text = "Open File";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.deleteToolStripMenuItem.Text = "Remove File";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // useAsImageToolStripMenuItem
            // 
            this.useAsImageToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("useAsImageToolStripMenuItem.Image")));
            this.useAsImageToolStripMenuItem.Name = "useAsImageToolStripMenuItem";
            this.useAsImageToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.useAsImageToolStripMenuItem.Text = "Use as Image";
            this.useAsImageToolStripMenuItem.Click += new System.EventHandler(this.UseAsImageToolStripMenuItemClick);
            // 
            // validateToolStripMenuItem
            // 
            this.validateToolStripMenuItem.Name = "validateToolStripMenuItem";
            this.validateToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.validateToolStripMenuItem.Text = "Validate";
            this.validateToolStripMenuItem.Click += new System.EventHandler(this.validateToolStripMenuItem_Click);
            // 
            // importModDetailsToolStripMenuItem
            // 
            this.importModDetailsToolStripMenuItem.Name = "importModDetailsToolStripMenuItem";
            this.importModDetailsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.importModDetailsToolStripMenuItem.Text = "Import mod details";
            this.importModDetailsToolStripMenuItem.Click += new System.EventHandler(this.importModDetailsToolStripMenuItem_Click);
            // 
            // scanForDataFilesToolStripMenuItem
            // 
            this.scanForDataFilesToolStripMenuItem.Name = "scanForDataFilesToolStripMenuItem";
            this.scanForDataFilesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.scanForDataFilesToolStripMenuItem.Text = "Scan for Data Files";
            this.scanForDataFilesToolStripMenuItem.Click += new System.EventHandler(this.scanForDataFilesToolStripMenuItem_Click);
            // 
            // viewRequiredDataFilesToolStripMenuItem
            // 
            this.viewRequiredDataFilesToolStripMenuItem.Name = "viewRequiredDataFilesToolStripMenuItem";
            this.viewRequiredDataFilesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.viewRequiredDataFilesToolStripMenuItem.Text = "View required data files";
            this.viewRequiredDataFilesToolStripMenuItem.Click += new System.EventHandler(this.viewRequiredDataFilesToolStripMenuItem_Click);
            // 
            // cbIncludeVersion
            // 
            this.cbIncludeVersion.AutoSize = true;
            this.cbIncludeVersion.Location = new System.Drawing.Point(212, 71);
            this.cbIncludeVersion.Name = "cbIncludeVersion";
            this.cbIncludeVersion.Size = new System.Drawing.Size(154, 17);
            this.cbIncludeVersion.TabIndex = 2;
            this.cbIncludeVersion.Text = "Include version in file name";
            this.cbIncludeVersion.UseVisualStyleBackColor = true;
            this.cbIncludeVersion.CheckedChanged += new System.EventHandler(this.CbIncludeVersionCheckedChanged);
            // 
            // bEdScript
            // 
            this.bEdScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bEdScript.Location = new System.Drawing.Point(374, 149);
            this.bEdScript.Name = "bEdScript";
            this.bEdScript.Size = new System.Drawing.Size(100, 23);
            this.bEdScript.TabIndex = 35;
            this.bEdScript.Text = "Edit Script";
            this.bEdScript.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bEdScript.Click += new System.EventHandler(this.bEdScript_Click);
            // 
            // bAdd
            // 
            this.bAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bAdd.Location = new System.Drawing.Point(374, 260);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(100, 23);
            this.bAdd.TabIndex = 37;
            this.bAdd.Text = "Add Files";
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(212, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "E-mail";
            // 
            // tbEmail
            // 
            this.tbEmail.Location = new System.Drawing.Point(256, 43);
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(116, 20);
            this.tbEmail.TabIndex = 9;
            // 
            // grpInformation
            // 
            this.grpInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpInformation.Controls.Add(this.tbName);
            this.grpInformation.Controls.Add(this.label4);
            this.grpInformation.Controls.Add(this.label5);
            this.grpInformation.Controls.Add(this.tbVersion);
            this.grpInformation.Controls.Add(this.label8);
            this.grpInformation.Controls.Add(this.cbIncludeVersion);
            this.grpInformation.Controls.Add(this.tbWebsite);
            this.grpInformation.Controls.Add(this.label6);
            this.grpInformation.Controls.Add(this.tbAuthor);
            this.grpInformation.Controls.Add(this.label7);
            this.grpInformation.Controls.Add(this.tbEmail);
            this.grpInformation.Location = new System.Drawing.Point(12, 12);
            this.grpInformation.Name = "grpInformation";
            this.grpInformation.Size = new System.Drawing.Size(472, 104);
            this.grpInformation.TabIndex = 51;
            this.grpInformation.TabStop = false;
            this.grpInformation.Text = "Information";
            // 
            // btnOCDList
            // 
            this.btnOCDList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOCDList.Location = new System.Drawing.Point(245, 576);
            this.btnOCDList.Name = "btnOCDList";
            this.btnOCDList.Size = new System.Drawing.Size(75, 23);
            this.btnOCDList.TabIndex = 52;
            this.btnOCDList.Text = "OCD List";
            this.btnOCDList.UseVisualStyleBackColor = true;
            this.btnOCDList.Click += new System.EventHandler(this.BtnOCDListClick);
            // 
            // btnTESNexus
            // 
            this.btnTESNexus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTESNexus.Location = new System.Drawing.Point(245, 605);
            this.btnTESNexus.Name = "btnTESNexus";
            this.btnTESNexus.Size = new System.Drawing.Size(75, 23);
            this.btnTESNexus.TabIndex = 53;
            this.btnTESNexus.Text = "Nexus";
            this.btnTESNexus.UseVisualStyleBackColor = true;
            this.btnTESNexus.Click += new System.EventHandler(this.BtnTESNexusClick);
            // 
            // gbDataMod
            // 
            this.gbDataMod.Controls.Add(this.rdSystemMod);
            this.gbDataMod.Controls.Add(this.rdDataMod);
            this.gbDataMod.Location = new System.Drawing.Point(6, 134);
            this.gbDataMod.Name = "gbDataMod";
            this.gbDataMod.Size = new System.Drawing.Size(344, 34);
            this.gbDataMod.TabIndex = 18;
            this.gbDataMod.TabStop = false;
            // 
            // rdDataMod
            // 
            this.rdDataMod.AutoSize = true;
            this.rdDataMod.Checked = true;
            this.rdDataMod.Location = new System.Drawing.Point(14, 15);
            this.rdDataMod.Name = "rdDataMod";
            this.rdDataMod.Size = new System.Drawing.Size(92, 17);
            this.rdDataMod.TabIndex = 0;
            this.rdDataMod.TabStop = true;
            this.rdDataMod.Text = "Standard Mod";
            this.rdDataMod.UseVisualStyleBackColor = true;
            // 
            // rdSystemMod
            // 
            this.rdSystemMod.AutoSize = true;
            this.rdSystemMod.Location = new System.Drawing.Point(158, 15);
            this.rdSystemMod.Name = "rdSystemMod";
            this.rdSystemMod.Size = new System.Drawing.Size(158, 17);
            this.rdSystemMod.TabIndex = 1;
            this.rdSystemMod.Text = "System Mod (ENB, HDT, ...)";
            this.rdSystemMod.UseVisualStyleBackColor = true;
            this.rdSystemMod.CheckedChanged += new System.EventHandler(this.rdSystemMod_CheckedChanged);
            // 
            // CreateModForm
            // 
            this.ClientSize = new System.Drawing.Size(504, 640);
            this.Controls.Add(this.btnTESNexus);
            this.Controls.Add(this.btnOCDList);
            this.Controls.Add(this.grpCompression);
            this.Controls.Add(this.bRemoveScreenshot);
            this.Controls.Add(this.bGroups);
            this.Controls.Add(this.ScreenshotPic);
            this.Controls.Add(this.bScreenshot);
            this.Controls.Add(this.bAddZip);
            this.Controls.Add(this.bUp);
            this.Controls.Add(this.bDown);
            this.Controls.Add(this.rbData);
            this.Controls.Add(this.bEdDescription);
            this.Controls.Add(this.rbPlugins);
            this.Controls.Add(this.bCreate);
            this.Controls.Add(this.bEdReadme);
            this.Controls.Add(this.bAddFromFolder);
            this.Controls.Add(this.lvFiles);
            this.Controls.Add(this.bEdScript);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.grpInformation);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(370, 556);
            this.Name = "CreateModForm";
            this.Text = "Mod Creator";
            this.grpCompression.ResumeLayout(false);
            this.grpCompression.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScreenshotPic)).EndInit();
            this.FilesContextMenu.ResumeLayout(false);
            this.grpInformation.ResumeLayout(false);
            this.grpInformation.PerformLayout();
            this.gbDataMod.ResumeLayout(false);
            this.gbDataMod.PerformLayout();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnTESNexus;
        private System.Windows.Forms.Button btnOCDList;
        private System.Windows.Forms.GroupBox grpInformation;
        private System.Windows.Forms.ToolStripMenuItem useAsImageToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpCompression;

        #endregion

        private System.Windows.Forms.Button bEdReadme;
        private System.Windows.Forms.Button bEdScript;
        private System.Windows.Forms.ComboBox cmbCompType;
        private System.Windows.Forms.ComboBox cmbDataCompLevel;
        private System.Windows.Forms.ComboBox cmbModCompLevel;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.Button bAddFromFolder;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbVersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbPlugins;
        private System.Windows.Forms.RadioButton rbData;
        private System.Windows.Forms.Button bEdDescription;
        private System.Windows.Forms.Button bCreate;
        private System.Windows.Forms.OpenFileDialog OpenDialog;
        private System.Windows.Forms.FolderBrowserDialog FolderDialog;
        private System.Windows.Forms.TextBox tbAuthor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbEmail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button bUp;
        private System.Windows.Forms.Button bDown;
        private System.Windows.Forms.TextBox tbWebsite;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button bAddZip;
        private System.Windows.Forms.ContextMenuStrip FilesContextMenu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateToolStripMenuItem;
        private System.Windows.Forms.Button bScreenshot;
        private System.Windows.Forms.ToolStripMenuItem scanForDataFilesToolStripMenuItem;
        private System.Windows.Forms.PictureBox ScreenshotPic;
        private System.Windows.Forms.CheckBox cbIncludeVersion;
        private System.Windows.Forms.ToolStripMenuItem importModDetailsToolStripMenuItem;
        private System.Windows.Forms.Button bGroups;
        private System.Windows.Forms.ContextMenuStrip cmsGroups;
        private System.Windows.Forms.ContextMenu DudMenu=new System.Windows.Forms.ContextMenu();
        private System.Windows.Forms.ToolStripMenuItem viewRequiredDataFilesToolStripMenuItem;
        private System.Windows.Forms.Button bRemoveScreenshot;
        private System.Windows.Forms.CheckBox cbOmod2;
        private System.ComponentModel.BackgroundWorker backgroundModCreator;
        private System.Windows.Forms.GroupBox gbDataMod;
        private System.Windows.Forms.RadioButton rdSystemMod;
        private System.Windows.Forms.RadioButton rdDataMod;
        private System.Windows.Forms.ToolTip CreateModToolTip;
    }
}