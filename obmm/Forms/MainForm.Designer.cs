namespace OblivionModManager {
    partial class MainForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lvEspList = new System.Windows.Forms.ListView();
            this.ch1Esp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch1Belongs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EspContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activateAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deactivateAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDataFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unlinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lvModList = new System.Windows.Forms.ListView();
            this.ModNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ModAuthorColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ModVersionColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ModNbFilesColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ModNbPluginsColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ScriptExistColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DatePackedColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ConflictLevelColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileSizeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ModIDColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.omodContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewReadmeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visitWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addInfoFromTesNexusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emailAuthorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDataConflictsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupNoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aquisitionActivateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripReactivateMod = new System.Windows.Forms.ToolStripMenuItem();
            this.simulateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forceDisableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractToFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportOmodConversionDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConflictImageList = new System.Windows.Forms.ImageList(this.components);
            this.bCreate = new System.Windows.Forms.Button();
            this.OpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.bMoveUp = new System.Windows.Forms.Button();
            this.bMoveDown = new System.Windows.Forms.Button();
            this.bActivate = new System.Windows.Forms.Button();
            this.bLoad = new System.Windows.Forms.Button();
            this.bHelp = new System.Windows.Forms.Button();
            this.cmbEspSortOrder = new System.Windows.Forms.ComboBox();
            this.bAbout = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.radDetail = new System.Windows.Forms.RadioButton();
            this.radList = new System.Windows.Forms.RadioButton();
            this.bLaunch = new System.Windows.Forms.Button();
            this.bEdit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bSettings = new System.Windows.Forms.Button();
            this.cmbOmodSortOrder = new System.Windows.Forms.ComboBox();
            this.cmbGroupFilter = new System.Windows.Forms.ComboBox();
            this.SaveOmodDialog = new System.Windows.Forms.SaveFileDialog();
            this.PipeFileWatcher = new System.IO.FileSystemWatcher();
            this.bUtilities = new System.Windows.Forms.Button();
            this.cmsUtilitiesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.conflictReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conflictReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bSABrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bSACreatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bSAFixerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.archiveInvalidationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataFileBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hiddenOmodSwitcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nifViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForModUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oCDSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oCDEditorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.conflictedFilesPickerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.runASkyProcPatcherToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openAllModUpdatesSitesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BatchContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.activateAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deactivateAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanFilteredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tidyDataFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteBackupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateConflictsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rescanEspHeadersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideInactiveFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aquisitionActivateFilteredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bBatch = new System.Windows.Forms.Button();
            this.bImport = new System.Windows.Forms.Button();
            this.ImportContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loadOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.omodReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportOmodListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportLoadOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportOmodGroupInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importOmodListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importLoadOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importOmodGroupInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnMoarSettings = new System.Windows.Forms.Button();
            this.cmdExtended = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkForModUpdatesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.oCDSearchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.oCDEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moreSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conflictedFilesPickerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runASkyProcPatcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openAllModUpdatesSitesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cbFilter = new System.Windows.Forms.CheckBox();
            this.gbFIlter = new System.Windows.Forms.GroupBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnBOSSSortPlugins = new System.Windows.Forms.Button();
            this.btnToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.backgroundModImportWorker = new System.ComponentModel.BackgroundWorker();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.downloadStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.downloadProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.downloadStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.downloadProgressBar2 = new System.Windows.Forms.ToolStripProgressBar();
            this.importStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLblScriptExtenderVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.updatecheckstatuslabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProcessingStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLblGUImessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.backgroundModDownloadWorker = new System.ComponentModel.BackgroundWorker();
            this.backgroundNexusInfoDownloadWorker = new System.ComponentModel.BackgroundWorker();
            this.importFileTimer = new System.Windows.Forms.Timer(this.components);
            this.backgroundNexusModUpdateChecker = new System.ComponentModel.BackgroundWorker();
            this.backgroundImageLoader = new System.ComponentModel.BackgroundWorker();
            this.backgroundTesNexusInfoAdder = new System.ComponentModel.BackgroundWorker();
            this.btnLaunchCreationKit = new System.Windows.Forms.Button();
            this.backgroundModCreator = new System.ComponentModel.BackgroundWorker();
            this.backgroundModDownloadWorker2 = new System.ComponentModel.BackgroundWorker();
            this.btnLootSortPlugins = new System.Windows.Forms.Button();
            this.EspContextMenu.SuspendLayout();
            this.omodContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PipeFileWatcher)).BeginInit();
            this.cmsUtilitiesMenu.SuspendLayout();
            this.BatchContextMenu.SuspendLayout();
            this.ImportContextMenu.SuspendLayout();
            this.cmdExtended.SuspendLayout();
            this.gbFIlter.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvEspList
            // 
            this.lvEspList.AllowDrop = true;
            this.lvEspList.CheckBoxes = true;
            this.lvEspList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch1Esp,
            this.ch1Belongs});
            this.lvEspList.ContextMenuStrip = this.EspContextMenu;
            this.lvEspList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEspList.HideSelection = false;
            this.lvEspList.Location = new System.Drawing.Point(0, 0);
            this.lvEspList.Name = "lvEspList";
            this.lvEspList.ShowItemToolTips = true;
            this.lvEspList.Size = new System.Drawing.Size(329, 320);
            this.lvEspList.TabIndex = 0;
            this.lvEspList.UseCompatibleStateImageBehavior = false;
            this.lvEspList.View = System.Windows.Forms.View.Details;
            this.lvEspList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvEspList_ItemCheck);
            this.lvEspList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.LvEspListItemChecked);
            this.lvEspList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvEspList_ItemDrag);
            this.lvEspList.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvEspList_DragDrop);
            this.lvEspList.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvEspList_DragEnter);
            this.lvEspList.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.lvEspList_GiveFeedback);
            this.lvEspList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvEspList_KeyDown);
            this.lvEspList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvEspList_MouseDown);
            this.lvEspList.MouseHover += new System.EventHandler(this.lvEspList_MouseHover);
            // 
            // ch1Esp
            // 
            this.ch1Esp.Text = "Esp";
            this.ch1Esp.Width = 138;
            // 
            // ch1Belongs
            // 
            this.ch1Belongs.Text = "Part of";
            this.ch1Belongs.Width = 146;
            // 
            // EspContextMenu
            // 
            this.EspContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem,
            this.moveToTopToolStripMenuItem,
            this.moveToBottomToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.activateAllToolStripMenuItem1,
            this.deactivateAllToolStripMenuItem1,
            this.viewDataFilesToolStripMenuItem,
            this.unlinkToolStripMenuItem});
            this.EspContextMenu.Name = "EspContextMenu";
            this.EspContextMenu.Size = new System.Drawing.Size(162, 202);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.moveUpToolStripMenuItem.Text = "Move up";
            this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.bMoveUp_Click);
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.moveDownToolStripMenuItem.Text = "Move down";
            this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.bMoveDown_Click);
            // 
            // moveToTopToolStripMenuItem
            // 
            this.moveToTopToolStripMenuItem.Name = "moveToTopToolStripMenuItem";
            this.moveToTopToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.moveToTopToolStripMenuItem.Text = "Move to top";
            this.moveToTopToolStripMenuItem.Click += new System.EventHandler(this.moveToTopToolStripMenuItem_Click);
            // 
            // moveToBottomToolStripMenuItem
            // 
            this.moveToBottomToolStripMenuItem.Name = "moveToBottomToolStripMenuItem";
            this.moveToBottomToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.moveToBottomToolStripMenuItem.Text = "Move to bottom";
            this.moveToBottomToolStripMenuItem.Click += new System.EventHandler(this.moveToBottomToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // activateAllToolStripMenuItem1
            // 
            this.activateAllToolStripMenuItem1.Name = "activateAllToolStripMenuItem1";
            this.activateAllToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.activateAllToolStripMenuItem1.Text = "Activate all";
            this.activateAllToolStripMenuItem1.Click += new System.EventHandler(this.activateAllToolStripMenuItem1_Click);
            // 
            // deactivateAllToolStripMenuItem1
            // 
            this.deactivateAllToolStripMenuItem1.Name = "deactivateAllToolStripMenuItem1";
            this.deactivateAllToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.deactivateAllToolStripMenuItem1.Text = "Deactivate all";
            this.deactivateAllToolStripMenuItem1.Click += new System.EventHandler(this.deactivateAllToolStripMenuItem1_Click);
            // 
            // viewDataFilesToolStripMenuItem
            // 
            this.viewDataFilesToolStripMenuItem.Name = "viewDataFilesToolStripMenuItem";
            this.viewDataFilesToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.viewDataFilesToolStripMenuItem.Text = "View data files";
            this.viewDataFilesToolStripMenuItem.Click += new System.EventHandler(this.viewDataFilesToolStripMenuItem_Click);
            // 
            // unlinkToolStripMenuItem
            // 
            this.unlinkToolStripMenuItem.Name = "unlinkToolStripMenuItem";
            this.unlinkToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.unlinkToolStripMenuItem.Text = "Unlink";
            this.unlinkToolStripMenuItem.Click += new System.EventHandler(this.unlinkToolStripMenuItem_Click);
            // 
            // lvModList
            // 
            this.lvModList.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.lvModList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvModList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ModNameColumn,
            this.ModAuthorColumn,
            this.ModVersionColumn,
            this.ModNbFilesColumn,
            this.ModNbPluginsColumn,
            this.ScriptExistColumn,
            this.DatePackedColumn,
            this.ConflictLevelColumn,
            this.FileSizeColumn,
            this.ModIDColumn});
            this.lvModList.ContextMenuStrip = this.omodContextMenu;
            this.lvModList.FullRowSelect = true;
            this.lvModList.HideSelection = false;
            this.lvModList.Location = new System.Drawing.Point(0, 22);
            this.lvModList.Name = "lvModList";
            this.lvModList.ShowItemToolTips = true;
            this.lvModList.Size = new System.Drawing.Size(371, 462);
            this.lvModList.SmallImageList = this.ConflictImageList;
            this.lvModList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvModList.TabIndex = 0;
            this.lvModList.UseCompatibleStateImageBehavior = false;
            this.lvModList.View = System.Windows.Forms.View.List;
            this.lvModList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvModList_ColumnClick);
            this.lvModList.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.lvModList_ColumnWidthChanged);
            this.lvModList.ItemActivate += new System.EventHandler(this.lvModList_ItemActivate);
            this.lvModList.SelectedIndexChanged += new System.EventHandler(this.lvModList_SelectedIndexChanged);
            // 
            // ModNameColumn
            // 
            this.ModNameColumn.Text = "Mod Name";
            this.ModNameColumn.Width = 400;
            // 
            // ModAuthorColumn
            // 
            this.ModAuthorColumn.Text = "Author";
            this.ModAuthorColumn.Width = 120;
            // 
            // ModVersionColumn
            // 
            this.ModVersionColumn.Text = "Version";
            // 
            // ModNbFilesColumn
            // 
            this.ModNbFilesColumn.Text = " Data files";
            // 
            // ModNbPluginsColumn
            // 
            this.ModNbPluginsColumn.Text = "Plugins";
            // 
            // ScriptExistColumn
            // 
            this.ScriptExistColumn.Text = "Scripted";
            // 
            // DatePackedColumn
            // 
            this.DatePackedColumn.Text = "Date Packed";
            this.DatePackedColumn.Width = 140;
            // 
            // ConflictLevelColumn
            // 
            this.ConflictLevelColumn.Text = "Conflict";
            this.ConflictLevelColumn.Width = 100;
            // 
            // FileSizeColumn
            // 
            this.FileSizeColumn.Text = "File size";
            this.FileSizeColumn.Width = 100;
            // 
            // ModIDColumn
            // 
            this.ModIDColumn.Text = "Mod ID";
            // 
            // omodContextMenu
            // 
            this.omodContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.viewReadmeToolStripMenuItem,
            this.viewScriptToolStripMenuItem,
            this.visitWebsiteToolStripMenuItem,
            this.addInfoFromTesNexusToolStripMenuItem,
            this.addPictureToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.emailAuthorToolStripMenuItem,
            this.viewDataConflictsToolStripMenuItem,
            this.GroupToolStripMenuItem,
            this.aquisitionActivateToolStripMenuItem,
            this.toolStripReactivateMod,
            this.simulateToolStripMenuItem,
            this.forceDisableToolStripMenuItem,
            this.deleteToolStripMenuItem1,
            this.cleanToolStripMenuItem,
            this.convertToArchiveToolStripMenuItem,
            this.extractToFolderToolStripMenuItem,
            this.exportOmodConversionDataToolStripMenuItem,
            this.hideToolStripMenuItem});
            this.omodContextMenu.Name = "omodContextMenu";
            this.omodContextMenu.Size = new System.Drawing.Size(230, 466);
            this.omodContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.omodContextMenu_Opening);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // viewReadmeToolStripMenuItem
            // 
            this.viewReadmeToolStripMenuItem.Name = "viewReadmeToolStripMenuItem";
            this.viewReadmeToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.viewReadmeToolStripMenuItem.Text = "View readme";
            this.viewReadmeToolStripMenuItem.Click += new System.EventHandler(this.viewReadmeToolStripMenuItem_Click);
            // 
            // viewScriptToolStripMenuItem
            // 
            this.viewScriptToolStripMenuItem.Name = "viewScriptToolStripMenuItem";
            this.viewScriptToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.viewScriptToolStripMenuItem.Text = "View script";
            this.viewScriptToolStripMenuItem.Click += new System.EventHandler(this.viewScriptToolStripMenuItem_Click);
            // 
            // visitWebsiteToolStripMenuItem
            // 
            this.visitWebsiteToolStripMenuItem.Name = "visitWebsiteToolStripMenuItem";
            this.visitWebsiteToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.visitWebsiteToolStripMenuItem.Text = "Visit website";
            this.visitWebsiteToolStripMenuItem.Click += new System.EventHandler(this.visitWebsiteToolStripMenuItem_Click);
            // 
            // addInfoFromTesNexusToolStripMenuItem
            // 
            this.addInfoFromTesNexusToolStripMenuItem.Name = "addInfoFromTesNexusToolStripMenuItem";
            this.addInfoFromTesNexusToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.addInfoFromTesNexusToolStripMenuItem.Text = "Add info from TesNexus";
            this.addInfoFromTesNexusToolStripMenuItem.Visible = false;
            this.addInfoFromTesNexusToolStripMenuItem.Click += new System.EventHandler(this.addInfoFromTesNexusToolStripMenuItem_Click);
            // 
            // addPictureToolStripMenuItem
            // 
            this.addPictureToolStripMenuItem.Name = "addPictureToolStripMenuItem";
            this.addPictureToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.addPictureToolStripMenuItem.Text = "Add picture";
            this.addPictureToolStripMenuItem.Visible = false;
            this.addPictureToolStripMenuItem.Click += new System.EventHandler(this.addPictureToolStripMenuItem_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("checkForUpdatesToolStripMenuItem.Image")));
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "&Check for Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.CheckForUpdatesToolStripMenuItemClick);
            // 
            // emailAuthorToolStripMenuItem
            // 
            this.emailAuthorToolStripMenuItem.Name = "emailAuthorToolStripMenuItem";
            this.emailAuthorToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.emailAuthorToolStripMenuItem.Text = "Email author";
            this.emailAuthorToolStripMenuItem.Click += new System.EventHandler(this.emailAuthorToolStripMenuItem_Click);
            // 
            // viewDataConflictsToolStripMenuItem
            // 
            this.viewDataConflictsToolStripMenuItem.Name = "viewDataConflictsToolStripMenuItem";
            this.viewDataConflictsToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.viewDataConflictsToolStripMenuItem.Text = "View data conflicts";
            this.viewDataConflictsToolStripMenuItem.Click += new System.EventHandler(this.viewDataConflictsToolStripMenuItem_Click);
            // 
            // GroupToolStripMenuItem
            // 
            this.GroupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GroupNoneToolStripMenuItem});
            this.GroupToolStripMenuItem.Name = "GroupToolStripMenuItem";
            this.GroupToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.GroupToolStripMenuItem.Text = "Group";
            // 
            // GroupNoneToolStripMenuItem
            // 
            this.GroupNoneToolStripMenuItem.Name = "GroupNoneToolStripMenuItem";
            this.GroupNoneToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.GroupNoneToolStripMenuItem.Text = "None";
            this.GroupNoneToolStripMenuItem.Click += new System.EventHandler(this.GroupNoneToolStripMenuItem_Click);
            // 
            // aquisitionActivateToolStripMenuItem
            // 
            this.aquisitionActivateToolStripMenuItem.Name = "aquisitionActivateToolStripMenuItem";
            this.aquisitionActivateToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.aquisitionActivateToolStripMenuItem.Text = "Acquisition activate";
            this.aquisitionActivateToolStripMenuItem.Click += new System.EventHandler(this.aquisitionActivateToolStripMenuItem_Click);
            // 
            // toolStripReactivateMod
            // 
            this.toolStripReactivateMod.Name = "toolStripReactivateMod";
            this.toolStripReactivateMod.Size = new System.Drawing.Size(229, 22);
            this.toolStripReactivateMod.Text = "Reactivate";
            this.toolStripReactivateMod.Click += new System.EventHandler(this.toolStripReactivateMod_Click);
            // 
            // simulateToolStripMenuItem
            // 
            this.simulateToolStripMenuItem.Name = "simulateToolStripMenuItem";
            this.simulateToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.simulateToolStripMenuItem.Text = "Simulate";
            this.simulateToolStripMenuItem.Click += new System.EventHandler(this.simulateToolStripMenuItem_Click);
            // 
            // forceDisableToolStripMenuItem
            // 
            this.forceDisableToolStripMenuItem.Name = "forceDisableToolStripMenuItem";
            this.forceDisableToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.forceDisableToolStripMenuItem.Text = "Force disable";
            this.forceDisableToolStripMenuItem.Click += new System.EventHandler(this.forceDisableToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(229, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // cleanToolStripMenuItem
            // 
            this.cleanToolStripMenuItem.Name = "cleanToolStripMenuItem";
            this.cleanToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.cleanToolStripMenuItem.Text = "Clean";
            this.cleanToolStripMenuItem.Click += new System.EventHandler(this.cleanToolStripMenuItem_Click);
            // 
            // convertToArchiveToolStripMenuItem
            // 
            this.convertToArchiveToolStripMenuItem.Name = "convertToArchiveToolStripMenuItem";
            this.convertToArchiveToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.convertToArchiveToolStripMenuItem.Text = "Convert to archive";
            this.convertToArchiveToolStripMenuItem.Click += new System.EventHandler(this.convertToArchiveToolStripMenuItem_Click);
            // 
            // extractToFolderToolStripMenuItem
            // 
            this.extractToFolderToolStripMenuItem.Name = "extractToFolderToolStripMenuItem";
            this.extractToFolderToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.extractToFolderToolStripMenuItem.Text = "Extract to folder";
            this.extractToFolderToolStripMenuItem.Click += new System.EventHandler(this.extractToFolderToolStripMenuItem_Click);
            // 
            // exportOmodConversionDataToolStripMenuItem
            // 
            this.exportOmodConversionDataToolStripMenuItem.Name = "exportOmodConversionDataToolStripMenuItem";
            this.exportOmodConversionDataToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.exportOmodConversionDataToolStripMenuItem.Text = "Export omod conversion data";
            this.exportOmodConversionDataToolStripMenuItem.Click += new System.EventHandler(this.exportOmodConversionDataToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // ConflictImageList
            // 
            this.ConflictImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ConflictImageList.ImageStream")));
            this.ConflictImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ConflictImageList.Images.SetKeyName(0, "blue.bmp");
            this.ConflictImageList.Images.SetKeyName(1, "green.bmp");
            this.ConflictImageList.Images.SetKeyName(2, "orange.bmp");
            this.ConflictImageList.Images.SetKeyName(3, "red.bmp");
            this.ConflictImageList.Images.SetKeyName(4, "black.bmp");
            // 
            // bCreate
            // 
            this.bCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCreate.Location = new System.Drawing.Point(472, 532);
            this.bCreate.Name = "bCreate";
            this.bCreate.Size = new System.Drawing.Size(75, 23);
            this.bCreate.TabIndex = 6;
            this.bCreate.Text = "Create";
            this.bCreate.Click += new System.EventHandler(this.bCreate_Click);
            this.bCreate.MouseHover += new System.EventHandler(this.bCreate_MouseHover);
            // 
            // OpenDialog
            // 
            this.OpenDialog.Filter = "mod file (*.omod)|*.omod|mod file (*.omod2)|*.omod2";
            this.OpenDialog.Multiselect = true;
            this.OpenDialog.RestoreDirectory = true;
            this.OpenDialog.Title = "Select mod files to load";
            // 
            // bMoveUp
            // 
            this.bMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bMoveUp.Location = new System.Drawing.Point(8, 532);
            this.bMoveUp.Name = "bMoveUp";
            this.bMoveUp.Size = new System.Drawing.Size(80, 23);
            this.bMoveUp.TabIndex = 1;
            this.bMoveUp.Text = "Move up";
            this.bMoveUp.Click += new System.EventHandler(this.bMoveUp_Click);
            // 
            // bMoveDown
            // 
            this.bMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bMoveDown.Location = new System.Drawing.Point(94, 532);
            this.bMoveDown.Name = "bMoveDown";
            this.bMoveDown.Size = new System.Drawing.Size(80, 23);
            this.bMoveDown.TabIndex = 2;
            this.bMoveDown.Text = "Move down";
            this.bMoveDown.Click += new System.EventHandler(this.bMoveDown_Click);
            // 
            // bActivate
            // 
            this.bActivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bActivate.Location = new System.Drawing.Point(391, 532);
            this.bActivate.Name = "bActivate";
            this.bActivate.Size = new System.Drawing.Size(75, 23);
            this.bActivate.TabIndex = 5;
            this.bActivate.Text = "Activate";
            this.bActivate.Click += new System.EventHandler(this.bActivate_Click);
            this.bActivate.MouseHover += new System.EventHandler(this.bActivate_MouseHover);
            // 
            // bLoad
            // 
            this.bLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bLoad.Location = new System.Drawing.Point(634, 532);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(75, 23);
            this.bLoad.TabIndex = 8;
            this.bLoad.Text = "Load";
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            this.bLoad.MouseHover += new System.EventHandler(this.bLoad_MouseHover);
            // 
            // bHelp
            // 
            this.bHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bHelp.Location = new System.Drawing.Point(722, 135);
            this.bHelp.Name = "bHelp";
            this.bHelp.Size = new System.Drawing.Size(120, 23);
            this.bHelp.TabIndex = 10;
            this.bHelp.Text = "Help";
            this.bHelp.Click += new System.EventHandler(this.bHelp_Click);
            // 
            // cmbEspSortOrder
            // 
            this.cmbEspSortOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbEspSortOrder.FormattingEnabled = true;
            this.cmbEspSortOrder.Items.AddRange(new object[] {
            "Load order",
            "Alphabetic",
            "Active",
            "Owner",
            "File size"});
            this.cmbEspSortOrder.Location = new System.Drawing.Point(180, 534);
            this.cmbEspSortOrder.Name = "cmbEspSortOrder";
            this.cmbEspSortOrder.Size = new System.Drawing.Size(95, 21);
            this.cmbEspSortOrder.TabIndex = 3;
            this.cmbEspSortOrder.Text = "Load order";
            this.cmbEspSortOrder.SelectedIndexChanged += new System.EventHandler(this.cmbEspSortOrder_SelectedIndexChanged);
            this.cmbEspSortOrder.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IgnoreKeyPress);
            // 
            // bAbout
            // 
            this.bAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bAbout.Location = new System.Drawing.Point(722, 164);
            this.bAbout.Name = "bAbout";
            this.bAbout.Size = new System.Drawing.Size(120, 23);
            this.bAbout.TabIndex = 11;
            this.bAbout.Text = "About";
            this.bAbout.Click += new System.EventHandler(this.bAbout_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.radDetail);
            this.splitContainer1.Panel2.Controls.Add(this.radList);
            this.splitContainer1.Panel2.Controls.Add(this.lvModList);
            this.splitContainer1.Size = new System.Drawing.Size(704, 487);
            this.splitContainer1.SplitterDistance = 329;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lvEspList);
            this.splitContainer2.Size = new System.Drawing.Size(329, 487);
            this.splitContainer2.SplitterDistance = 320;
            this.splitContainer2.TabIndex = 1;
            // 
            // radDetail
            // 
            this.radDetail.AutoSize = true;
            this.radDetail.Location = new System.Drawing.Point(107, 4);
            this.radDetail.Name = "radDetail";
            this.radDetail.Size = new System.Drawing.Size(77, 17);
            this.radDetail.TabIndex = 2;
            this.radDetail.TabStop = true;
            this.radDetail.Text = "Detail view";
            this.radDetail.UseVisualStyleBackColor = true;
            this.radDetail.CheckedChanged += new System.EventHandler(this.radDetail_CheckedChanged);
            // 
            // radList
            // 
            this.radList.AutoSize = true;
            this.radList.Checked = true;
            this.radList.Location = new System.Drawing.Point(35, 4);
            this.radList.Name = "radList";
            this.radList.Size = new System.Drawing.Size(66, 17);
            this.radList.TabIndex = 1;
            this.radList.TabStop = true;
            this.radList.Text = "List view";
            this.radList.UseVisualStyleBackColor = true;
            this.radList.CheckedChanged += new System.EventHandler(this.radList_CheckedChanged);
            // 
            // bLaunch
            // 
            this.bLaunch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bLaunch.Location = new System.Drawing.Point(722, 106);
            this.bLaunch.Name = "bLaunch";
            this.bLaunch.Size = new System.Drawing.Size(120, 23);
            this.bLaunch.TabIndex = 9;
            this.bLaunch.Text = "Launch the game";
            this.bLaunch.Click += new System.EventHandler(this.bLaunch_Click);
            // 
            // bEdit
            // 
            this.bEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bEdit.Location = new System.Drawing.Point(553, 532);
            this.bEdit.Name = "bEdit";
            this.bEdit.Size = new System.Drawing.Size(75, 23);
            this.bEdit.TabIndex = 7;
            this.bEdit.Text = "Edit";
            this.bEdit.Click += new System.EventHandler(this.bEdit_Click);
            this.bEdit.MouseHover += new System.EventHandler(this.bEdit_MouseHover);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(722, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 90);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // bSettings
            // 
            this.bSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSettings.Location = new System.Drawing.Point(722, 193);
            this.bSettings.Name = "bSettings";
            this.bSettings.Size = new System.Drawing.Size(120, 23);
            this.bSettings.TabIndex = 12;
            this.bSettings.Text = "Settings";
            this.bSettings.Click += new System.EventHandler(this.bSettings_Click);
            // 
            // cmbOmodSortOrder
            // 
            this.cmbOmodSortOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbOmodSortOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOmodSortOrder.FormattingEnabled = true;
            this.cmbOmodSortOrder.Items.AddRange(new object[] {
            "File name",
            "Author",
            "Conflict level",
            "File size",
            "Date packed",
            "Date installed",
            "Group",
            "Mod ID"});
            this.cmbOmodSortOrder.Location = new System.Drawing.Point(724, 533);
            this.cmbOmodSortOrder.Name = "cmbOmodSortOrder";
            this.cmbOmodSortOrder.Size = new System.Drawing.Size(120, 21);
            this.cmbOmodSortOrder.TabIndex = 17;
            this.cmbOmodSortOrder.SelectedIndexChanged += new System.EventHandler(this.cmbOmodSortOrder_SelectedIndexChanged);
            this.cmbOmodSortOrder.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IgnoreKeyPress);
            // 
            // cmbGroupFilter
            // 
            this.cmbGroupFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGroupFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroupFilter.FormattingEnabled = true;
            this.cmbGroupFilter.Items.AddRange(new object[] {
            "All",
            "Unassigned",
            "[Active mods only]",
            "[Edit groups]"});
            this.cmbGroupFilter.Location = new System.Drawing.Point(724, 506);
            this.cmbGroupFilter.Name = "cmbGroupFilter";
            this.cmbGroupFilter.Size = new System.Drawing.Size(120, 21);
            this.cmbGroupFilter.TabIndex = 16;
            this.cmbGroupFilter.SelectedIndexChanged += new System.EventHandler(this.cmbGroupFilter_SelectedIndexChanged);
            this.cmbGroupFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IgnoreKeyPress);
            // 
            // SaveOmodDialog
            // 
            this.SaveOmodDialog.AddExtension = false;
            this.SaveOmodDialog.Filter = "7-zip (*.7z)|*.7z|zip (*.zip)|*.zip";
            this.SaveOmodDialog.OverwritePrompt = false;
            this.SaveOmodDialog.RestoreDirectory = true;
            this.SaveOmodDialog.Title = "Save omod as";
            // 
            // PipeFileWatcher
            // 
            this.PipeFileWatcher.EnableRaisingEvents = true;
            this.PipeFileWatcher.Filter = System.IO.Path.GetFileName(Program.PipeFilename);
            this.PipeFileWatcher.Path = System.IO.Path.GetDirectoryName(Program.PipeFilename);
            this.PipeFileWatcher.SynchronizingObject = this;
            this.PipeFileWatcher.Created += new System.IO.FileSystemEventHandler(this.PipeFileWatcher_Created);
            // 
            // bUtilities
            // 
            this.bUtilities.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bUtilities.Location = new System.Drawing.Point(722, 222);
            this.bUtilities.Name = "bUtilities";
            this.bUtilities.Size = new System.Drawing.Size(120, 23);
            this.bUtilities.TabIndex = 13;
            this.bUtilities.Text = "Utilities";
            this.bUtilities.UseVisualStyleBackColor = true;
            this.bUtilities.Click += new System.EventHandler(this.bUtilities_Click);
            // 
            // cmsUtilitiesMenu
            // 
            this.cmsUtilitiesMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conflictReportToolStripMenuItem,
            this.conflictReportToolStripMenuItem1,
            this.saveManagerToolStripMenuItem,
            this.bSABrowserToolStripMenuItem,
            this.bSACreatorToolStripMenuItem,
            this.bSAFixerToolStripMenuItem,
            this.archiveInvalidationToolStripMenuItem,
            this.dataFileBrowserToolStripMenuItem,
            this.hiddenOmodSwitcherToolStripMenuItem,
            this.nifViewerToolStripMenuItem,
            this.checkForModUpdatesToolStripMenuItem,
            this.oCDSearchToolStripMenuItem,
            this.oCDEditorToolStripMenuItem1,
            this.conflictedFilesPickerToolStripMenuItem1,
            this.runASkyProcPatcherToolStripMenuItem1,
            this.openAllModUpdatesSitesToolStripMenuItem1,
            this.openLogFileToolStripMenuItem});
            this.cmsUtilitiesMenu.Name = "cmsUtilitiesMenu";
            this.cmsUtilitiesMenu.Size = new System.Drawing.Size(218, 378);
            // 
            // conflictReportToolStripMenuItem
            // 
            this.conflictReportToolStripMenuItem.Name = "conflictReportToolStripMenuItem";
            this.conflictReportToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.conflictReportToolStripMenuItem.Text = "Old conflict detector";
            this.conflictReportToolStripMenuItem.Visible = false;
            this.conflictReportToolStripMenuItem.Click += new System.EventHandler(this.bConflict_Click);
            // 
            // conflictReportToolStripMenuItem1
            // 
            this.conflictReportToolStripMenuItem1.Name = "conflictReportToolStripMenuItem1";
            this.conflictReportToolStripMenuItem1.Size = new System.Drawing.Size(217, 22);
            this.conflictReportToolStripMenuItem1.Text = "Conflict detector";
            this.conflictReportToolStripMenuItem1.Click += new System.EventHandler(this.conflictReportToolStripMenuItem1_Click);
            // 
            // saveManagerToolStripMenuItem
            // 
            this.saveManagerToolStripMenuItem.Name = "saveManagerToolStripMenuItem";
            this.saveManagerToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.saveManagerToolStripMenuItem.Text = "Save manager";
            this.saveManagerToolStripMenuItem.Click += new System.EventHandler(this.bSaves_Click);
            // 
            // bSABrowserToolStripMenuItem
            // 
            this.bSABrowserToolStripMenuItem.Name = "bSABrowserToolStripMenuItem";
            this.bSABrowserToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.bSABrowserToolStripMenuItem.Text = "BSA browser";
            this.bSABrowserToolStripMenuItem.Click += new System.EventHandler(this.bSABrowserToolStripMenuItem_Click);
            // 
            // bSACreatorToolStripMenuItem
            // 
            this.bSACreatorToolStripMenuItem.Name = "bSACreatorToolStripMenuItem";
            this.bSACreatorToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.bSACreatorToolStripMenuItem.Text = "BSA creator";
            this.bSACreatorToolStripMenuItem.Click += new System.EventHandler(this.bSACreatorToolStripMenuItem_Click);
            // 
            // bSAFixerToolStripMenuItem
            // 
            this.bSAFixerToolStripMenuItem.Name = "bSAFixerToolStripMenuItem";
            this.bSAFixerToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.bSAFixerToolStripMenuItem.Text = "BSA un-corrupter";
            this.bSAFixerToolStripMenuItem.Click += new System.EventHandler(this.bSAFixerToolStripMenuItem_Click);
            // 
            // archiveInvalidationToolStripMenuItem
            // 
            this.archiveInvalidationToolStripMenuItem.Name = "archiveInvalidationToolStripMenuItem";
            this.archiveInvalidationToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.archiveInvalidationToolStripMenuItem.Text = "Archive invalidation";
            this.archiveInvalidationToolStripMenuItem.Click += new System.EventHandler(this.archiveInvalidationToolStripMenuItem_Click);
            // 
            // dataFileBrowserToolStripMenuItem
            // 
            this.dataFileBrowserToolStripMenuItem.Name = "dataFileBrowserToolStripMenuItem";
            this.dataFileBrowserToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.dataFileBrowserToolStripMenuItem.Text = "Data file browser";
            this.dataFileBrowserToolStripMenuItem.Click += new System.EventHandler(this.dataFileBrowserToolStripMenuItem_Click);
            // 
            // hiddenOmodSwitcherToolStripMenuItem
            // 
            this.hiddenOmodSwitcherToolStripMenuItem.Name = "hiddenOmodSwitcherToolStripMenuItem";
            this.hiddenOmodSwitcherToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.hiddenOmodSwitcherToolStripMenuItem.Text = "Hidden omod switcher";
            this.hiddenOmodSwitcherToolStripMenuItem.Click += new System.EventHandler(this.hiddenOmodSwitcherToolStripMenuItem_Click);
            // 
            // nifViewerToolStripMenuItem
            // 
            this.nifViewerToolStripMenuItem.Name = "nifViewerToolStripMenuItem";
            this.nifViewerToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.nifViewerToolStripMenuItem.Text = "Nif viewer";
            this.nifViewerToolStripMenuItem.Visible = false;
            this.nifViewerToolStripMenuItem.Click += new System.EventHandler(this.nifViewerToolStripMenuItem_Click);
            // 
            // checkForModUpdatesToolStripMenuItem
            // 
            this.checkForModUpdatesToolStripMenuItem.Name = "checkForModUpdatesToolStripMenuItem";
            this.checkForModUpdatesToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.checkForModUpdatesToolStripMenuItem.Text = "Check for Mod Updates";
            this.checkForModUpdatesToolStripMenuItem.Click += new System.EventHandler(this.CheckForModUpdatesToolStripMenuItemClick);
            // 
            // oCDSearchToolStripMenuItem
            // 
            this.oCDSearchToolStripMenuItem.Name = "oCDSearchToolStripMenuItem";
            this.oCDSearchToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.oCDSearchToolStripMenuItem.Text = "OCD Search";
            this.oCDSearchToolStripMenuItem.Click += new System.EventHandler(this.OCDSearchToolStripMenuItem1Click);
            // 
            // oCDEditorToolStripMenuItem1
            // 
            this.oCDEditorToolStripMenuItem1.Name = "oCDEditorToolStripMenuItem1";
            this.oCDEditorToolStripMenuItem1.Size = new System.Drawing.Size(217, 22);
            this.oCDEditorToolStripMenuItem1.Text = "OCD Editor";
            this.oCDEditorToolStripMenuItem1.Click += new System.EventHandler(this.OCDEditorToolStripMenuItemClick);
            // 
            // conflictedFilesPickerToolStripMenuItem1
            // 
            this.conflictedFilesPickerToolStripMenuItem1.Name = "conflictedFilesPickerToolStripMenuItem1";
            this.conflictedFilesPickerToolStripMenuItem1.Size = new System.Drawing.Size(217, 22);
            this.conflictedFilesPickerToolStripMenuItem1.Text = "Conflicted files picker";
            this.conflictedFilesPickerToolStripMenuItem1.Click += new System.EventHandler(this.conflictedFilesPickerToolStripMenuItem_Click);
            // 
            // runASkyProcPatcherToolStripMenuItem1
            // 
            this.runASkyProcPatcherToolStripMenuItem1.Name = "runASkyProcPatcherToolStripMenuItem1";
            this.runASkyProcPatcherToolStripMenuItem1.Size = new System.Drawing.Size(217, 22);
            this.runASkyProcPatcherToolStripMenuItem1.Text = "Run a SkyProc Patcher";
            this.runASkyProcPatcherToolStripMenuItem1.Click += new System.EventHandler(this.runASkyProcPatcherToolStripMenuItem_Click);
            // 
            // openAllModUpdatesSitesToolStripMenuItem1
            // 
            this.openAllModUpdatesSitesToolStripMenuItem1.Name = "openAllModUpdatesSitesToolStripMenuItem1";
            this.openAllModUpdatesSitesToolStripMenuItem1.Size = new System.Drawing.Size(217, 22);
            this.openAllModUpdatesSitesToolStripMenuItem1.Text = "Open all mod updates sites";
            this.openAllModUpdatesSitesToolStripMenuItem1.Click += new System.EventHandler(this.openAllModUpdatesSitesToolStripMenuItem_Click);
            // 
            // openLogFileToolStripMenuItem
            // 
            this.openLogFileToolStripMenuItem.Name = "openLogFileToolStripMenuItem";
            this.openLogFileToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.openLogFileToolStripMenuItem.Text = "Open log file";
            this.openLogFileToolStripMenuItem.Click += new System.EventHandler(this.openLogFileToolStripMenuItem_Click);
            // 
            // BatchContextMenu
            // 
            this.BatchContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activateAllToolStripMenuItem,
            this.deactivateAllToolStripMenuItem,
            this.cleanAllToolStripMenuItem,
            this.cleanFilteredToolStripMenuItem,
            this.tidyDataFolderToolStripMenuItem,
            this.deleteBackupsToolStripMenuItem,
            this.updateConflictsToolStripMenuItem,
            this.rescanEspHeadersToolStripMenuItem,
            this.hideInactiveFToolStripMenuItem,
            this.aquisitionActivateFilteredToolStripMenuItem});
            this.BatchContextMenu.Name = "ActivateContextMenu";
            this.BatchContextMenu.Size = new System.Drawing.Size(219, 224);
            // 
            // activateAllToolStripMenuItem
            // 
            this.activateAllToolStripMenuItem.Name = "activateAllToolStripMenuItem";
            this.activateAllToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.activateAllToolStripMenuItem.Text = "Activate filtered";
            this.activateAllToolStripMenuItem.Click += new System.EventHandler(this.activateAllToolStripMenuItem_Click);
            // 
            // deactivateAllToolStripMenuItem
            // 
            this.deactivateAllToolStripMenuItem.Name = "deactivateAllToolStripMenuItem";
            this.deactivateAllToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.deactivateAllToolStripMenuItem.Text = "Deactivate filtered";
            this.deactivateAllToolStripMenuItem.Click += new System.EventHandler(this.deactivateAllToolStripMenuItem_Click);
            // 
            // cleanAllToolStripMenuItem
            // 
            this.cleanAllToolStripMenuItem.Name = "cleanAllToolStripMenuItem";
            this.cleanAllToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.cleanAllToolStripMenuItem.Text = "Deactivate and clean all";
            this.cleanAllToolStripMenuItem.Click += new System.EventHandler(this.cleanAllToolStripMenuItem_Click);
            // 
            // cleanFilteredToolStripMenuItem
            // 
            this.cleanFilteredToolStripMenuItem.Name = "cleanFilteredToolStripMenuItem";
            this.cleanFilteredToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.cleanFilteredToolStripMenuItem.Text = "Clean filtered";
            this.cleanFilteredToolStripMenuItem.Click += new System.EventHandler(this.cleanFilteredToolStripMenuItem_Click);
            // 
            // tidyDataFolderToolStripMenuItem
            // 
            this.tidyDataFolderToolStripMenuItem.Name = "tidyDataFolderToolStripMenuItem";
            this.tidyDataFolderToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.tidyDataFolderToolStripMenuItem.Text = "Tidy data folder";
            this.tidyDataFolderToolStripMenuItem.Click += new System.EventHandler(this.tidyDataFolderToolStripMenuItem_Click);
            // 
            // deleteBackupsToolStripMenuItem
            // 
            this.deleteBackupsToolStripMenuItem.Name = "deleteBackupsToolStripMenuItem";
            this.deleteBackupsToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.deleteBackupsToolStripMenuItem.Text = "Delete backups";
            this.deleteBackupsToolStripMenuItem.Click += new System.EventHandler(this.deleteBackupsToolStripMenuItem_Click);
            // 
            // updateConflictsToolStripMenuItem
            // 
            this.updateConflictsToolStripMenuItem.Name = "updateConflictsToolStripMenuItem";
            this.updateConflictsToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.updateConflictsToolStripMenuItem.Text = "Update conflicts";
            this.updateConflictsToolStripMenuItem.Click += new System.EventHandler(this.updateConflictsToolStripMenuItem_Click);
            // 
            // rescanEspHeadersToolStripMenuItem
            // 
            this.rescanEspHeadersToolStripMenuItem.Name = "rescanEspHeadersToolStripMenuItem";
            this.rescanEspHeadersToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.rescanEspHeadersToolStripMenuItem.Text = "Rescan esp headers";
            this.rescanEspHeadersToolStripMenuItem.Click += new System.EventHandler(this.rescanEspHeadersToolStripMenuItem_Click);
            // 
            // hideInactiveFToolStripMenuItem
            // 
            this.hideInactiveFToolStripMenuItem.Name = "hideInactiveFToolStripMenuItem";
            this.hideInactiveFToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.hideInactiveFToolStripMenuItem.Text = "Hide inactive filtered";
            this.hideInactiveFToolStripMenuItem.Click += new System.EventHandler(this.hideInactiveFToolStripMenuItem_Click);
            // 
            // aquisitionActivateFilteredToolStripMenuItem
            // 
            this.aquisitionActivateFilteredToolStripMenuItem.Name = "aquisitionActivateFilteredToolStripMenuItem";
            this.aquisitionActivateFilteredToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.aquisitionActivateFilteredToolStripMenuItem.Text = "Acquisition activate filtered";
            this.aquisitionActivateFilteredToolStripMenuItem.Click += new System.EventHandler(this.aquisitionActivateFilteredToolStripMenuItem_Click);
            // 
            // bBatch
            // 
            this.bBatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bBatch.Location = new System.Drawing.Point(722, 251);
            this.bBatch.Name = "bBatch";
            this.bBatch.Size = new System.Drawing.Size(120, 23);
            this.bBatch.TabIndex = 18;
            this.bBatch.Text = "Batch actions";
            this.bBatch.Click += new System.EventHandler(this.bBatch_Click);
            // 
            // bImport
            // 
            this.bImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bImport.Location = new System.Drawing.Point(722, 280);
            this.bImport.Name = "bImport";
            this.bImport.Size = new System.Drawing.Size(120, 23);
            this.bImport.TabIndex = 19;
            this.bImport.Text = "Import/Export";
            this.bImport.UseVisualStyleBackColor = true;
            this.bImport.Click += new System.EventHandler(this.bImport_Click);
            // 
            // ImportContextMenu
            // 
            this.ImportContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadOrderToolStripMenuItem,
            this.omodReportToolStripMenuItem,
            this.exportOmodListToolStripMenuItem,
            this.exportLoadOrderToolStripMenuItem,
            this.exportOmodGroupInformationToolStripMenuItem,
            this.importOmodListToolStripMenuItem,
            this.importLoadOrderToolStripMenuItem,
            this.importOmodGroupInfoToolStripMenuItem});
            this.ImportContextMenu.Name = "ImportContextMenu";
            this.ImportContextMenu.Size = new System.Drawing.Size(205, 180);
            // 
            // loadOrderToolStripMenuItem
            // 
            this.loadOrderToolStripMenuItem.Name = "loadOrderToolStripMenuItem";
            this.loadOrderToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.loadOrderToolStripMenuItem.Text = "View load order";
            this.loadOrderToolStripMenuItem.Click += new System.EventHandler(this.loadOrderToolStripMenuItem_Click);
            // 
            // omodReportToolStripMenuItem
            // 
            this.omodReportToolStripMenuItem.Name = "omodReportToolStripMenuItem";
            this.omodReportToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.omodReportToolStripMenuItem.Text = "Create omod report";
            this.omodReportToolStripMenuItem.Click += new System.EventHandler(this.omodReportToolStripMenuItem_Click);
            // 
            // exportOmodListToolStripMenuItem
            // 
            this.exportOmodListToolStripMenuItem.Name = "exportOmodListToolStripMenuItem";
            this.exportOmodListToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.exportOmodListToolStripMenuItem.Text = "Export active omod list";
            this.exportOmodListToolStripMenuItem.Click += new System.EventHandler(this.exportOmodListToolStripMenuItem_Click);
            // 
            // exportLoadOrderToolStripMenuItem
            // 
            this.exportLoadOrderToolStripMenuItem.Name = "exportLoadOrderToolStripMenuItem";
            this.exportLoadOrderToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.exportLoadOrderToolStripMenuItem.Text = "Export load order";
            this.exportLoadOrderToolStripMenuItem.Click += new System.EventHandler(this.exportLoadOrderToolStripMenuItem_Click);
            // 
            // exportOmodGroupInformationToolStripMenuItem
            // 
            this.exportOmodGroupInformationToolStripMenuItem.Name = "exportOmodGroupInformationToolStripMenuItem";
            this.exportOmodGroupInformationToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.exportOmodGroupInformationToolStripMenuItem.Text = "Export omod group info";
            this.exportOmodGroupInformationToolStripMenuItem.Click += new System.EventHandler(this.exportOmodGroupInformationToolStripMenuItem_Click);
            // 
            // importOmodListToolStripMenuItem
            // 
            this.importOmodListToolStripMenuItem.Name = "importOmodListToolStripMenuItem";
            this.importOmodListToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.importOmodListToolStripMenuItem.Text = "Import active omod list";
            this.importOmodListToolStripMenuItem.Click += new System.EventHandler(this.importOmodListToolStripMenuItem_Click);
            // 
            // importLoadOrderToolStripMenuItem
            // 
            this.importLoadOrderToolStripMenuItem.Name = "importLoadOrderToolStripMenuItem";
            this.importLoadOrderToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.importLoadOrderToolStripMenuItem.Text = "Import load order";
            this.importLoadOrderToolStripMenuItem.Click += new System.EventHandler(this.importLoadOrderToolStripMenuItem_Click);
            // 
            // importOmodGroupInfoToolStripMenuItem
            // 
            this.importOmodGroupInfoToolStripMenuItem.Name = "importOmodGroupInfoToolStripMenuItem";
            this.importOmodGroupInfoToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.importOmodGroupInfoToolStripMenuItem.Text = "Import omod group info";
            this.importOmodGroupInfoToolStripMenuItem.Click += new System.EventHandler(this.importOmodGroupInfoToolStripMenuItem_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(722, 368);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(120, 39);
            this.lblStatus.TabIndex = 20;
            // 
            // btnMoarSettings
            // 
            this.btnMoarSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoarSettings.Location = new System.Drawing.Point(722, 342);
            this.btnMoarSettings.Name = "btnMoarSettings";
            this.btnMoarSettings.Size = new System.Drawing.Size(120, 23);
            this.btnMoarSettings.TabIndex = 21;
            this.btnMoarSettings.Text = "Extended Utilities";
            this.btnMoarSettings.Visible = false;
            this.btnMoarSettings.Click += new System.EventHandler(this.BtnMoarSettingsClick);
            // 
            // cmdExtended
            // 
            this.cmdExtended.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForModUpdatesToolStripMenuItem1,
            this.oCDSearchToolStripMenuItem1,
            this.oCDEditorToolStripMenuItem,
            this.moreSettingsToolStripMenuItem,
            this.conflictedFilesPickerToolStripMenuItem,
            this.runASkyProcPatcherToolStripMenuItem,
            this.openAllModUpdatesSitesToolStripMenuItem});
            this.cmdExtended.Name = "cmdExtended";
            this.cmdExtended.Size = new System.Drawing.Size(218, 158);
            // 
            // checkForModUpdatesToolStripMenuItem1
            // 
            this.checkForModUpdatesToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("checkForModUpdatesToolStripMenuItem1.Image")));
            this.checkForModUpdatesToolStripMenuItem1.Name = "checkForModUpdatesToolStripMenuItem1";
            this.checkForModUpdatesToolStripMenuItem1.Size = new System.Drawing.Size(217, 22);
            this.checkForModUpdatesToolStripMenuItem1.Text = "Check for Mod Updates";
            this.checkForModUpdatesToolStripMenuItem1.Click += new System.EventHandler(this.CheckForModUpdatesToolStripMenuItem1Click);
            // 
            // oCDSearchToolStripMenuItem1
            // 
            this.oCDSearchToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("oCDSearchToolStripMenuItem1.Image")));
            this.oCDSearchToolStripMenuItem1.Name = "oCDSearchToolStripMenuItem1";
            this.oCDSearchToolStripMenuItem1.Size = new System.Drawing.Size(217, 22);
            this.oCDSearchToolStripMenuItem1.Text = "OCD Search";
            this.oCDSearchToolStripMenuItem1.Click += new System.EventHandler(this.OCDSearchToolStripMenuItem1Click);
            // 
            // oCDEditorToolStripMenuItem
            // 
            this.oCDEditorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("oCDEditorToolStripMenuItem.Image")));
            this.oCDEditorToolStripMenuItem.Name = "oCDEditorToolStripMenuItem";
            this.oCDEditorToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.oCDEditorToolStripMenuItem.Text = "OCD Editor";
            this.oCDEditorToolStripMenuItem.Click += new System.EventHandler(this.OCDEditorToolStripMenuItemClick);
            // 
            // moreSettingsToolStripMenuItem
            // 
            this.moreSettingsToolStripMenuItem.Name = "moreSettingsToolStripMenuItem";
            this.moreSettingsToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.moreSettingsToolStripMenuItem.Text = "More Settings";
            this.moreSettingsToolStripMenuItem.Click += new System.EventHandler(this.MoreSettingsToolStripMenuItemClick);
            // 
            // conflictedFilesPickerToolStripMenuItem
            // 
            this.conflictedFilesPickerToolStripMenuItem.Name = "conflictedFilesPickerToolStripMenuItem";
            this.conflictedFilesPickerToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.conflictedFilesPickerToolStripMenuItem.Text = "Conflicted files picker";
            this.conflictedFilesPickerToolStripMenuItem.Click += new System.EventHandler(this.conflictedFilesPickerToolStripMenuItem_Click);
            // 
            // runASkyProcPatcherToolStripMenuItem
            // 
            this.runASkyProcPatcherToolStripMenuItem.Name = "runASkyProcPatcherToolStripMenuItem";
            this.runASkyProcPatcherToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.runASkyProcPatcherToolStripMenuItem.Text = "Run a SkyProc Patcher";
            this.runASkyProcPatcherToolStripMenuItem.Visible = false;
            this.runASkyProcPatcherToolStripMenuItem.Click += new System.EventHandler(this.runASkyProcPatcherToolStripMenuItem_Click);
            // 
            // openAllModUpdatesSitesToolStripMenuItem
            // 
            this.openAllModUpdatesSitesToolStripMenuItem.Name = "openAllModUpdatesSitesToolStripMenuItem";
            this.openAllModUpdatesSitesToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.openAllModUpdatesSitesToolStripMenuItem.Text = "Open all mod updates sites";
            this.openAllModUpdatesSitesToolStripMenuItem.Click += new System.EventHandler(this.openAllModUpdatesSitesToolStripMenuItem_Click);
            // 
            // checkForUpdatesToolStripMenuItem1
            // 
            this.checkForUpdatesToolStripMenuItem1.Name = "checkForUpdatesToolStripMenuItem1";
            this.checkForUpdatesToolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(5, 25);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(117, 20);
            this.txtFilter.TabIndex = 22;
            // 
            // cbFilter
            // 
            this.cbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFilter.AutoSize = true;
            this.cbFilter.Location = new System.Drawing.Point(6, 0);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(116, 17);
            this.cbFilter.TabIndex = 25;
            this.cbFilter.Text = "Enable display filter";
            this.cbFilter.UseVisualStyleBackColor = true;
            this.cbFilter.CheckedChanged += new System.EventHandler(this.cbFilter_CheckedChanged);
            // 
            // gbFIlter
            // 
            this.gbFIlter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFIlter.Controls.Add(this.cbFilter);
            this.gbFIlter.Controls.Add(this.txtFilter);
            this.gbFIlter.Location = new System.Drawing.Point(722, 450);
            this.gbFIlter.Name = "gbFIlter";
            this.gbFIlter.Size = new System.Drawing.Size(128, 51);
            this.gbFIlter.TabIndex = 26;
            this.gbFIlter.TabStop = false;
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(310, 532);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 27;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            this.btnImport.MouseHover += new System.EventHandler(this.btnImport_MouseHover);
            // 
            // btnBOSSSortPlugins
            // 
            this.btnBOSSSortPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBOSSSortPlugins.Location = new System.Drawing.Point(8, 505);
            this.btnBOSSSortPlugins.Name = "btnBOSSSortPlugins";
            this.btnBOSSSortPlugins.Size = new System.Drawing.Size(165, 22);
            this.btnBOSSSortPlugins.TabIndex = 28;
            this.btnBOSSSortPlugins.Text = "Auto sort using BOSS";
            this.btnBOSSSortPlugins.UseVisualStyleBackColor = true;
            this.btnBOSSSortPlugins.Visible = false;
            this.btnBOSSSortPlugins.Click += new System.EventHandler(this.btnBOSSSortPlugins_Click);
            // 
            // backgroundModImportWorker
            // 
            this.backgroundModImportWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundModImportWorker_DoWork);
            this.backgroundModImportWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundModImportWorker_ProgressChanged);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadStatusLabel,
            this.downloadProgressBar,
            this.downloadStatusLabel2,
            this.downloadProgressBar2,
            this.importStatusLabel,
            this.toolStripLblScriptExtenderVersion,
            this.updatecheckstatuslabel,
            this.toolStripProcessingStatusLabel,
            this.toolStripStatusLblGUImessage});
            this.statusStrip.Location = new System.Drawing.Point(0, 558);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(862, 22);
            this.statusStrip.TabIndex = 29;
            this.statusStrip.Text = "statusStrip1";
            // 
            // downloadStatusLabel
            // 
            this.downloadStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.downloadStatusLabel.Name = "downloadStatusLabel";
            this.downloadStatusLabel.Size = new System.Drawing.Size(79, 19);
            this.downloadStatusLabel.Text = "No download";
            this.downloadStatusLabel.ToolTipText = "Click here to see and manage downloads";
            this.downloadStatusLabel.Visible = false;
            this.downloadStatusLabel.Click += new System.EventHandler(this.downloadStatusLabel_Click);
            // 
            // downloadProgressBar
            // 
            this.downloadProgressBar.Name = "downloadProgressBar";
            this.downloadProgressBar.Size = new System.Drawing.Size(100, 18);
            this.downloadProgressBar.Visible = false;
            // 
            // downloadStatusLabel2
            // 
            this.downloadStatusLabel2.Name = "downloadStatusLabel2";
            this.downloadStatusLabel2.Size = new System.Drawing.Size(79, 19);
            this.downloadStatusLabel2.Text = "No download";
            this.downloadStatusLabel2.Visible = false;
            // 
            // downloadProgressBar2
            // 
            this.downloadProgressBar2.Name = "downloadProgressBar2";
            this.downloadProgressBar2.Size = new System.Drawing.Size(100, 18);
            this.downloadProgressBar2.Visible = false;
            // 
            // importStatusLabel
            // 
            this.importStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.importStatusLabel.Name = "importStatusLabel";
            this.importStatusLabel.Size = new System.Drawing.Size(62, 17);
            this.importStatusLabel.Text = "No import";
            this.importStatusLabel.Visible = false;
            // 
            // toolStripLblScriptExtenderVersion
            // 
            this.toolStripLblScriptExtenderVersion.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.toolStripLblScriptExtenderVersion.Name = "toolStripLblScriptExtenderVersion";
            this.toolStripLblScriptExtenderVersion.Size = new System.Drawing.Size(104, 17);
            this.toolStripLblScriptExtenderVersion.Text = "No Script Extender";
            this.toolStripLblScriptExtenderVersion.ToolTipText = "Click here to update Script Extender if applicable";
            this.toolStripLblScriptExtenderVersion.Click += new System.EventHandler(this.toolStripLblScriptExtenderVersion_Click);
            // 
            // updatecheckstatuslabel
            // 
            this.updatecheckstatuslabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.updatecheckstatuslabel.Name = "updatecheckstatuslabel";
            this.updatecheckstatuslabel.Size = new System.Drawing.Size(97, 17);
            this.updatecheckstatuslabel.Text = "No update check";
            this.updatecheckstatuslabel.Visible = false;
            this.updatecheckstatuslabel.Click += new System.EventHandler(this.updatecheckstatuslabel_Click);
            // 
            // toolStripProcessingStatusLabel
            // 
            this.toolStripProcessingStatusLabel.Name = "toolStripProcessingStatusLabel";
            this.toolStripProcessingStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLblGUImessage
            // 
            this.toolStripStatusLblGUImessage.Name = "toolStripStatusLblGUImessage";
            this.toolStripStatusLblGUImessage.Size = new System.Drawing.Size(0, 17);
            // 
            // statusUpdateTimer
            // 
            this.statusUpdateTimer.Interval = 1000;
            this.statusUpdateTimer.Tick += new System.EventHandler(this.statusUpdateTimer_Tick);
            // 
            // backgroundModDownloadWorker
            // 
            this.backgroundModDownloadWorker.WorkerSupportsCancellation = true;
            this.backgroundModDownloadWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundModDownloadWorker_DoWork);
            this.backgroundModDownloadWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundModDownloadWorker_ProgressChanged);
            // 
            // backgroundNexusInfoDownloadWorker
            // 
            this.backgroundNexusInfoDownloadWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundNexusInfoDownloadWorker_DoWork);
            // 
            // importFileTimer
            // 
            this.importFileTimer.Tick += new System.EventHandler(this.importFileTimer_Tick);
            // 
            // backgroundNexusModUpdateChecker
            // 
            this.backgroundNexusModUpdateChecker.WorkerReportsProgress = true;
            this.backgroundNexusModUpdateChecker.WorkerSupportsCancellation = true;
            this.backgroundNexusModUpdateChecker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundNexusModUpdateChecker_DoWork);
            this.backgroundNexusModUpdateChecker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundNexusModUpdateChecker_ProgressChanged);
            this.backgroundNexusModUpdateChecker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundNexusModUpdateChecker_RunWorkerCompleted);
            // 
            // backgroundImageLoader
            // 
            this.backgroundImageLoader.WorkerSupportsCancellation = true;
            this.backgroundImageLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundImageLoader_DoWork);
            // 
            // backgroundTesNexusInfoAdder
            // 
            this.backgroundTesNexusInfoAdder.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundTesNexusInfoAdder_DoWork);
            // 
            // btnLaunchCreationKit
            // 
            this.btnLaunchCreationKit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLaunchCreationKit.Location = new System.Drawing.Point(722, 309);
            this.btnLaunchCreationKit.Name = "btnLaunchCreationKit";
            this.btnLaunchCreationKit.Size = new System.Drawing.Size(120, 23);
            this.btnLaunchCreationKit.TabIndex = 30;
            this.btnLaunchCreationKit.Text = "Launch Creation kit";
            this.btnLaunchCreationKit.UseVisualStyleBackColor = true;
            this.btnLaunchCreationKit.Click += new System.EventHandler(this.btnLaunchCreationKit_Click);
            // 
            // backgroundModDownloadWorker2
            // 
            this.backgroundModDownloadWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundModDownloadWorker_DoWork);
            this.backgroundModDownloadWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundModDownloadWorker_ProgressChanged);
            // 
            // btnLootSortPlugins
            // 
            this.btnLootSortPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLootSortPlugins.Location = new System.Drawing.Point(179, 505);
            this.btnLootSortPlugins.Name = "btnLootSortPlugins";
            this.btnLootSortPlugins.Size = new System.Drawing.Size(165, 22);
            this.btnLootSortPlugins.TabIndex = 31;
            this.btnLootSortPlugins.Text = "Auto sort using LOOT";
            this.btnLootSortPlugins.UseVisualStyleBackColor = true;
            this.btnLootSortPlugins.Visible = false;
            this.btnLootSortPlugins.Click += new System.EventHandler(this.btnLootSortPlugins_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(862, 580);
            this.Controls.Add(this.btnLootSortPlugins);
            this.Controls.Add(this.btnLaunchCreationKit);
            this.Controls.Add(this.btnBOSSSortPlugins);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cmbEspSortOrder);
            this.Controls.Add(this.bEdit);
            this.Controls.Add(this.bCreate);
            this.Controls.Add(this.gbFIlter);
            this.Controls.Add(this.cmbGroupFilter);
            this.Controls.Add(this.bLoad);
            this.Controls.Add(this.cmbOmodSortOrder);
            this.Controls.Add(this.bActivate);
            this.Controls.Add(this.bMoveDown);
            this.Controls.Add(this.bMoveUp);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.bImport);
            this.Controls.Add(this.bBatch);
            this.Controls.Add(this.bAbout);
            this.Controls.Add(this.bUtilities);
            this.Controls.Add(this.bHelp);
            this.Controls.Add(this.btnMoarSettings);
            this.Controls.Add(this.bSettings);
            this.Controls.Add(this.bLaunch);
            this.Controls.Add(this.statusStrip);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::OblivionModManager.Properties.Settings.Default, "location", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = global::OblivionModManager.Properties.Settings.Default.location;
            this.MinimumSize = new System.Drawing.Size(773, 400);
            this.Name = "MainForm";
            this.Text = "Tes Mod Manager Extended";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.EspContextMenu.ResumeLayout(false);
            this.omodContextMenu.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PipeFileWatcher)).EndInit();
            this.cmsUtilitiesMenu.ResumeLayout(false);
            this.BatchContextMenu.ResumeLayout(false);
            this.ImportContextMenu.ResumeLayout(false);
            this.cmdExtended.ResumeLayout(false);
            this.gbFIlter.ResumeLayout(false);
            this.gbFIlter.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.ToolStripMenuItem oCDEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oCDSearchToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem checkForModUpdatesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem moreSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip cmdExtended;
        private System.Windows.Forms.Button btnMoarSettings;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;

        #endregion

        public System.Windows.Forms.ListView lvEspList;
        private System.Windows.Forms.ListView lvModList;
        private System.Windows.Forms.ColumnHeader ch1Esp;
        private System.Windows.Forms.ColumnHeader ch1Belongs;
        private System.Windows.Forms.Button bCreate;
        private System.Windows.Forms.OpenFileDialog OpenDialog;
        private System.Windows.Forms.Button bMoveUp;
        private System.Windows.Forms.Button bMoveDown;
        private System.Windows.Forms.Button bActivate;
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.ContextMenuStrip EspContextMenu;
        private System.Windows.Forms.ContextMenuStrip omodContextMenu;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewReadmeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visitWebsiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emailAuthorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ImageList ConflictImageList;
        private System.Windows.Forms.ComboBox cmbEspSortOrder;
        private System.Windows.Forms.Button bAbout;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button bEdit;
        private System.Windows.Forms.Button bLaunch;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button bSettings;
        private System.Windows.Forms.ComboBox cmbOmodSortOrder;
        private System.Windows.Forms.ToolStripMenuItem GroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GroupNoneToolStripMenuItem;
        private System.Windows.Forms.ComboBox cmbGroupFilter;
        private System.Windows.Forms.ToolStripMenuItem moveToTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToBottomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewDataConflictsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forceDisableToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog SaveOmodDialog;
        private System.IO.FileSystemWatcher PipeFileWatcher;
        private System.Windows.Forms.Button bUtilities;
        private System.Windows.Forms.ContextMenuStrip cmsUtilitiesMenu;
        private System.Windows.Forms.ToolStripMenuItem conflictReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bSABrowserToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip BatchContextMenu;
        private System.Windows.Forms.ToolStripMenuItem activateAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deactivateAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activateAllToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deactivateAllToolStripMenuItem1;
        private System.Windows.Forms.Button bBatch;
        private System.Windows.Forms.ToolStripMenuItem cleanAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanFilteredToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tidyDataFolderToolStripMenuItem;
        private System.Windows.Forms.Button bImport;
        private System.Windows.Forms.ContextMenuStrip ImportContextMenu;
        private System.Windows.Forms.ToolStripMenuItem omodReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportOmodListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportLoadOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importOmodListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importLoadOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bSACreatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem archiveInvalidationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteBackupsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateConflictsToolStripMenuItem;
        private System.Windows.Forms.Button bHelp;
        private System.Windows.Forms.ToolStripMenuItem viewDataFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportOmodGroupInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importOmodGroupInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bSAFixerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataFileBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conflictReportToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rescanEspHeadersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideInactiveFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hiddenOmodSwitcherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractToFolderToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem unlinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nifViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aquisitionActivateFilteredToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aquisitionActivateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportOmodConversionDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simulateToolStripMenuItem;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.CheckBox cbFilter;
        private System.Windows.Forms.GroupBox gbFIlter;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btnBOSSSortPlugins;
        private System.Windows.Forms.ToolTip btnToolTip;
        private System.ComponentModel.BackgroundWorker backgroundModImportWorker;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel downloadStatusLabel;
        private System.Windows.Forms.Timer statusUpdateTimer;
        private System.ComponentModel.BackgroundWorker backgroundModDownloadWorker;
        private System.Windows.Forms.ToolStripStatusLabel importStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar downloadProgressBar;
        private System.ComponentModel.BackgroundWorker backgroundNexusInfoDownloadWorker;
        private System.Windows.Forms.Timer importFileTimer;
        private System.Windows.Forms.ToolStripMenuItem toolStripReactivateMod;
        private System.ComponentModel.BackgroundWorker backgroundNexusModUpdateChecker;
        private System.Windows.Forms.ToolStripStatusLabel updatecheckstatuslabel;
        private System.Windows.Forms.RadioButton radDetail;
        private System.Windows.Forms.RadioButton radList;
        private System.Windows.Forms.ColumnHeader ModNameColumn;
        private System.Windows.Forms.ColumnHeader ModAuthorColumn;
        private System.Windows.Forms.ColumnHeader ModVersionColumn;
        private System.Windows.Forms.ColumnHeader ModNbFilesColumn;
        private System.Windows.Forms.ColumnHeader ModNbPluginsColumn;
        private System.Windows.Forms.ColumnHeader DatePackedColumn;
        private System.Windows.Forms.ColumnHeader ConflictLevelColumn;
        private System.Windows.Forms.ColumnHeader FileSizeColumn;
        private System.Windows.Forms.ToolStripMenuItem conflictedFilesPickerToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblScriptExtenderVersion;
        private System.Windows.Forms.ToolStripMenuItem runASkyProcPatcherToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader ScriptExistColumn;
        private System.Windows.Forms.ToolStripMenuItem addInfoFromTesNexusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPictureToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripProcessingStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem openAllModUpdatesSitesToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundImageLoader;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLblGUImessage;
        private System.ComponentModel.BackgroundWorker backgroundTesNexusInfoAdder;
        private System.Windows.Forms.ToolStripMenuItem checkForModUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oCDSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oCDEditorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem conflictedFilesPickerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem runASkyProcPatcherToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openAllModUpdatesSitesToolStripMenuItem1;
        private System.Windows.Forms.ColumnHeader ModIDColumn;
        private System.Windows.Forms.Button btnLaunchCreationKit;
        private System.ComponentModel.BackgroundWorker backgroundModCreator;
        private System.ComponentModel.BackgroundWorker backgroundModDownloadWorker2;
        private System.Windows.Forms.ToolStripStatusLabel downloadStatusLabel2;
        private System.Windows.Forms.ToolStripProgressBar downloadProgressBar2;
        private System.Windows.Forms.Button btnLootSortPlugins;
        private System.Windows.Forms.ToolStripMenuItem openLogFileToolStripMenuItem;
    }
}