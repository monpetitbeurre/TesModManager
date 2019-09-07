namespace OblivionModManager {
    partial class OptionsForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.cbWarnings = new System.Windows.Forms.CheckBox();
            this.cmbGroups = new System.Windows.Forms.ComboBox();
            this.bDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bAdd = new System.Windows.Forms.Button();
            this.cbIniWarn = new System.Windows.Forms.CheckBox();
            this.cbDataWarnings = new System.Windows.Forms.CheckBox();
            this.cbOmodInfo = new System.Windows.Forms.CheckBox();
            this.cbLockFOV = new System.Windows.Forms.CheckBox();
            this.bMoveModFolder = new System.Windows.Forms.Button();
            this.omodDirDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.cbShowEspWarnings = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCommandLine = new System.Windows.Forms.TextBox();
            this.DudMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bRenameGroup = new System.Windows.Forms.Button();
            this.bClearGroups = new System.Windows.Forms.Button();
            this.bMoveToEnd = new System.Windows.Forms.Button();
            this.cbCompressionBoost = new System.Windows.Forms.CheckBox();
            this.cbAutoupdateConflicts = new System.Windows.Forms.CheckBox();
            this.cbUpdateEsps = new System.Windows.Forms.CheckBox();
            this.bResetTempFolder = new System.Windows.Forms.Button();
            this.bMoveTempFolder = new System.Windows.Forms.Button();
            this.bSetFont = new System.Windows.Forms.Button();
            this.GroupFontDialog = new System.Windows.Forms.FontDialog();
            this.cbUseKiller = new System.Windows.Forms.CheckBox();
            this.cbSafeMode = new System.Windows.Forms.CheckBox();
            this.bReset = new System.Windows.Forms.Button();
            this.cbNewEspsLoadLast = new System.Windows.Forms.CheckBox();
            this.cbAdvSelectMany = new System.Windows.Forms.CheckBox();
            this.cbTrackConflicts = new System.Windows.Forms.CheckBox();
            this.cbAllowInsecureScripts = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cbNeverModifyLoadOrder = new System.Windows.Forms.CheckBox();
            this.cbAskToBeNexusDownloadManager = new System.Windows.Forms.CheckBox();
            this.cbActivateOnDoubleClick = new System.Windows.Forms.CheckBox();
            this.cbLoadOrderAsUTF8 = new System.Windows.Forms.CheckBox();
            this.cbUseTimeStamps = new System.Windows.Forms.CheckBox();
            this.cbEnableDebugLogging = new System.Windows.Forms.CheckBox();
            this.cbGhostInactiveMods = new System.Windows.Forms.CheckBox();
            this.cbShowHidden = new System.Windows.Forms.CheckBox();
            this.cbSaveOmod2AsZip = new System.Windows.Forms.CheckBox();
            this.cbDeactivateMissingOMODs = new System.Windows.Forms.CheckBox();
            this.cbWarnAboutMissingInfo = new System.Windows.Forms.CheckBox();
            this.cbShowSimpleConflictDialog = new System.Windows.Forms.CheckBox();
            this.cbAlwaysImportOmodData = new System.Windows.Forms.CheckBox();
            this.cbAlwaysImportNexusData = new System.Windows.Forms.CheckBox();
            this.cbAlwaysImportOCDData = new System.Windows.Forms.CheckBox();
            this.cbIncludeVersionInModName = new System.Windows.Forms.CheckBox();
            this.cbShowModNameInsteadOfFilename = new System.Windows.Forms.CheckBox();
            this.bAlternateBackupFolder = new System.Windows.Forms.Button();
            this.cbAskIfLoadAsIsOrImport = new System.Windows.Forms.CheckBox();
            this.btnNexusLoginInfo = new System.Windows.Forms.Button();
            this.cbPreventMovingAnESPBeforeAnESM = new System.Windows.Forms.CheckBox();
            this.cbOmod2IsDefault = new System.Windows.Forms.CheckBox();
            this.btnSetGamePath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbWarnings
            // 
            this.cbWarnings.AutoSize = true;
            this.cbWarnings.Location = new System.Drawing.Point(238, 81);
            this.cbWarnings.Name = "cbWarnings";
            this.cbWarnings.Size = new System.Drawing.Size(205, 17);
            this.cbWarnings.TabIndex = 4;
            this.cbWarnings.Text = "Display warnings when running scripts";
            this.toolTip.SetToolTip(this.cbWarnings, "If checked, tmm will display warning dialogs if it encounters errors while runnin" +
        "g omod scripts");
            this.cbWarnings.UseVisualStyleBackColor = true;
            this.cbWarnings.CheckedChanged += new System.EventHandler(this.cbWarnings_CheckedChanged);
            // 
            // cmbGroups
            // 
            this.cmbGroups.FormattingEnabled = true;
            this.cmbGroups.Items.AddRange(new object[] {
            "None"});
            this.cmbGroups.Location = new System.Drawing.Point(12, 453);
            this.cmbGroups.Name = "cmbGroups";
            this.cmbGroups.Size = new System.Drawing.Size(186, 21);
            this.cmbGroups.TabIndex = 17;
            this.cmbGroups.Text = "None";
            this.cmbGroups.SelectedIndexChanged += new System.EventHandler(this.cmbGroups_SelectedIndexChanged);
            this.cmbGroups.TextChanged += new System.EventHandler(this.cmbGroups_TextChanged);
            // 
            // bDelete
            // 
            this.bDelete.Enabled = false;
            this.bDelete.Location = new System.Drawing.Point(12, 480);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(90, 23);
            this.bDelete.TabIndex = 18;
            this.bDelete.Text = "Delete";
            this.bDelete.UseVisualStyleBackColor = true;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 437);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "omod groups";
            // 
            // bAdd
            // 
            this.bAdd.Enabled = false;
            this.bAdd.Location = new System.Drawing.Point(108, 480);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(90, 23);
            this.bAdd.TabIndex = 19;
            this.bAdd.Text = "Add";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // cbIniWarn
            // 
            this.cbIniWarn.AutoSize = true;
            this.cbIniWarn.Location = new System.Drawing.Point(11, 35);
            this.cbIniWarn.Name = "cbIniWarn";
            this.cbIniWarn.Size = new System.Drawing.Size(128, 17);
            this.cbIniWarn.TabIndex = 2;
            this.cbIniWarn.Text = "Warn on script ini edit";
            this.toolTip.SetToolTip(this.cbIniWarn, "If checked, omod scripts will require your permission to make modifications to ob" +
        "livion.ini or skyrim.ini");
            this.cbIniWarn.UseVisualStyleBackColor = true;
            this.cbIniWarn.CheckedChanged += new System.EventHandler(this.cbIniWarn_CheckedChanged);
            // 
            // cbDataWarnings
            // 
            this.cbDataWarnings.AutoSize = true;
            this.cbDataWarnings.Location = new System.Drawing.Point(238, 35);
            this.cbDataWarnings.Name = "cbDataWarnings";
            this.cbDataWarnings.Size = new System.Drawing.Size(122, 17);
            this.cbDataWarnings.TabIndex = 3;
            this.cbDataWarnings.Text = "Show misc warnings";
            this.toolTip.SetToolTip(this.cbDataWarnings, "Unchecking this disables several miscellaneous confirmation dialogs, such as the " +
        "\'are you sure you want to deactivate this omod\' prompt");
            this.cbDataWarnings.UseVisualStyleBackColor = true;
            this.cbDataWarnings.CheckedChanged += new System.EventHandler(this.cbDataWarnings_CheckedChanged);
            // 
            // cbOmodInfo
            // 
            this.cbOmodInfo.AutoSize = true;
            this.cbOmodInfo.Location = new System.Drawing.Point(238, 12);
            this.cbOmodInfo.Name = "cbOmodInfo";
            this.cbOmodInfo.Size = new System.Drawing.Size(194, 17);
            this.cbOmodInfo.TabIndex = 1;
            this.cbOmodInfo.Text = "Display omod info before installation";
            this.toolTip.SetToolTip(this.cbOmodInfo, "If checked, information about an omod will be displayed when it is double clicked" +
        " in windows explorer, instead of being automatically installed");
            this.cbOmodInfo.UseVisualStyleBackColor = true;
            this.cbOmodInfo.CheckedChanged += new System.EventHandler(this.cbOmodInfo_CheckedChanged);
            // 
            // cbLockFOV
            // 
            this.cbLockFOV.AutoSize = true;
            this.cbLockFOV.Location = new System.Drawing.Point(11, 12);
            this.cbLockFOV.Name = "cbLockFOV";
            this.cbLockFOV.Size = new System.Drawing.Size(101, 17);
            this.cbLockFOV.TabIndex = 0;
            this.cbLockFOV.Text = "Lock FOV at 75";
            this.toolTip.SetToolTip(this.cbLockFOV, "\"Resets the field of view setting in oblivion.ini to 75 each time tmm is closed d" +
        "own");
            this.cbLockFOV.UseVisualStyleBackColor = true;
            this.cbLockFOV.CheckedChanged += new System.EventHandler(this.cbLockFOV_CheckedChanged);
            // 
            // bMoveModFolder
            // 
            this.bMoveModFolder.Location = new System.Drawing.Point(221, 480);
            this.bMoveModFolder.Name = "bMoveModFolder";
            this.bMoveModFolder.Size = new System.Drawing.Size(194, 23);
            this.bMoveModFolder.TabIndex = 25;
            this.bMoveModFolder.Text = "Move omod directory";
            this.bMoveModFolder.UseVisualStyleBackColor = true;
            this.bMoveModFolder.Click += new System.EventHandler(this.bMoveModFolder_Click);
            this.bMoveModFolder.MouseHover += new System.EventHandler(this.bMoveModFolder_MouseHover);
            // 
            // omodDirDialog
            // 
            this.omodDirDialog.Description = "Select a new home for your omods";
            // 
            // cbShowEspWarnings
            // 
            this.cbShowEspWarnings.AutoSize = true;
            this.cbShowEspWarnings.Location = new System.Drawing.Point(238, 58);
            this.cbShowEspWarnings.Name = "cbShowEspWarnings";
            this.cbShowEspWarnings.Size = new System.Drawing.Size(179, 17);
            this.cbShowEspWarnings.TabIndex = 5;
            this.cbShowEspWarnings.Text = "Show esp deactivation warnings";
            this.toolTip.SetToolTip(this.cbShowEspWarnings, "If checked, tmm will display a confirmation dialog if you try and uncheck an esp " +
        "which is part of an omod");
            this.cbShowEspWarnings.UseVisualStyleBackColor = true;
            this.cbShowEspWarnings.CheckedChanged += new System.EventHandler(this.cbShowEspWarnings_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 437);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Game command line";
            // 
            // tbCommandLine
            // 
            this.tbCommandLine.Location = new System.Drawing.Point(221, 453);
            this.tbCommandLine.Name = "tbCommandLine";
            this.tbCommandLine.Size = new System.Drawing.Size(194, 20);
            this.tbCommandLine.TabIndex = 24;
            this.tbCommandLine.TextChanged += new System.EventHandler(this.tbCommandLine_TextChanged);
            // 
            // DudMenu
            // 
            this.DudMenu.Name = "DudMenu";
            this.DudMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // bRenameGroup
            // 
            this.bRenameGroup.Enabled = false;
            this.bRenameGroup.Location = new System.Drawing.Point(12, 509);
            this.bRenameGroup.Name = "bRenameGroup";
            this.bRenameGroup.Size = new System.Drawing.Size(90, 23);
            this.bRenameGroup.TabIndex = 20;
            this.bRenameGroup.Text = "Rename";
            this.bRenameGroup.UseVisualStyleBackColor = true;
            this.bRenameGroup.Click += new System.EventHandler(this.bRenameGroup_Click);
            // 
            // bClearGroups
            // 
            this.bClearGroups.Location = new System.Drawing.Point(108, 509);
            this.bClearGroups.Name = "bClearGroups";
            this.bClearGroups.Size = new System.Drawing.Size(90, 23);
            this.bClearGroups.TabIndex = 21;
            this.bClearGroups.Text = "Clear";
            this.bClearGroups.UseVisualStyleBackColor = true;
            this.bClearGroups.Click += new System.EventHandler(this.bClearGroups_Click);
            // 
            // bMoveToEnd
            // 
            this.bMoveToEnd.Enabled = false;
            this.bMoveToEnd.Location = new System.Drawing.Point(12, 538);
            this.bMoveToEnd.Name = "bMoveToEnd";
            this.bMoveToEnd.Size = new System.Drawing.Size(90, 23);
            this.bMoveToEnd.TabIndex = 22;
            this.bMoveToEnd.Text = "Move to end";
            this.bMoveToEnd.UseVisualStyleBackColor = true;
            this.bMoveToEnd.Click += new System.EventHandler(this.bMoveToEnd_Click);
            // 
            // cbCompressionBoost
            // 
            this.cbCompressionBoost.AutoSize = true;
            this.cbCompressionBoost.Location = new System.Drawing.Point(11, 58);
            this.cbCompressionBoost.Name = "cbCompressionBoost";
            this.cbCompressionBoost.Size = new System.Drawing.Size(115, 17);
            this.cbCompressionBoost.TabIndex = 6;
            this.cbCompressionBoost.Text = "Compression boost";
            this.toolTip.SetToolTip(this.cbCompressionBoost, resources.GetString("cbCompressionBoost.ToolTip"));
            this.cbCompressionBoost.UseVisualStyleBackColor = true;
            this.cbCompressionBoost.CheckedChanged += new System.EventHandler(this.cbCompressionBoost_CheckedChanged);
            // 
            // cbAutoupdateConflicts
            // 
            this.cbAutoupdateConflicts.AutoSize = true;
            this.cbAutoupdateConflicts.Location = new System.Drawing.Point(238, 127);
            this.cbAutoupdateConflicts.Name = "cbAutoupdateConflicts";
            this.cbAutoupdateConflicts.Size = new System.Drawing.Size(123, 17);
            this.cbAutoupdateConflicts.TabIndex = 9;
            this.cbAutoupdateConflicts.Text = "Autoupdate conflicts";
            this.toolTip.SetToolTip(this.cbAutoupdateConflicts, "If checked, does the equivilent of clicking \'Batch actions|Update conflicts\' ever" +
        "y time an omod is activated or deactivated\r\nUncheck this to speed up omod activa" +
        "tion/deactivatio");
            this.cbAutoupdateConflicts.UseVisualStyleBackColor = true;
            this.cbAutoupdateConflicts.CheckedChanged += new System.EventHandler(this.cbAutoupdateConflicts_CheckedChanged);
            // 
            // cbUpdateEsps
            // 
            this.cbUpdateEsps.AutoSize = true;
            this.cbUpdateEsps.Location = new System.Drawing.Point(11, 81);
            this.cbUpdateEsps.Name = "cbUpdateEsps";
            this.cbUpdateEsps.Size = new System.Drawing.Size(179, 17);
            this.cbUpdateEsps.TabIndex = 8;
            this.cbUpdateEsps.Text = "Update unparented esp headers";
            this.toolTip.SetToolTip(this.cbUpdateEsps, resources.GetString("cbUpdateEsps.ToolTip"));
            this.cbUpdateEsps.UseVisualStyleBackColor = true;
            this.cbUpdateEsps.CheckedChanged += new System.EventHandler(this.cbUpdateEsps_CheckedChanged);
            // 
            // bResetTempFolder
            // 
            this.bResetTempFolder.Location = new System.Drawing.Point(221, 538);
            this.bResetTempFolder.Name = "bResetTempFolder";
            this.bResetTempFolder.Size = new System.Drawing.Size(194, 23);
            this.bResetTempFolder.TabIndex = 27;
            this.bResetTempFolder.Text = "Use default temp folder";
            this.bResetTempFolder.UseVisualStyleBackColor = true;
            this.bResetTempFolder.Click += new System.EventHandler(this.bResetTempFolder_Click);
            // 
            // bMoveTempFolder
            // 
            this.bMoveTempFolder.Location = new System.Drawing.Point(221, 509);
            this.bMoveTempFolder.Name = "bMoveTempFolder";
            this.bMoveTempFolder.Size = new System.Drawing.Size(194, 23);
            this.bMoveTempFolder.TabIndex = 26;
            this.bMoveTempFolder.Text = "Move temp directory";
            this.bMoveTempFolder.UseVisualStyleBackColor = true;
            this.bMoveTempFolder.Click += new System.EventHandler(this.bMoveTempFolder_Click);
            this.bMoveTempFolder.MouseHover += new System.EventHandler(this.bMoveTempFolder_MouseHover);
            // 
            // bSetFont
            // 
            this.bSetFont.Enabled = false;
            this.bSetFont.Location = new System.Drawing.Point(108, 539);
            this.bSetFont.Name = "bSetFont";
            this.bSetFont.Size = new System.Drawing.Size(90, 23);
            this.bSetFont.TabIndex = 23;
            this.bSetFont.Text = "Set font";
            this.bSetFont.UseVisualStyleBackColor = true;
            this.bSetFont.Click += new System.EventHandler(this.bSetFont_Click);
            // 
            // GroupFontDialog
            // 
            this.GroupFontDialog.Color = System.Drawing.SystemColors.ControlText;
            this.GroupFontDialog.FontMustExist = true;
            this.GroupFontDialog.ShowColor = true;
            // 
            // cbUseKiller
            // 
            this.cbUseKiller.AutoSize = true;
            this.cbUseKiller.Location = new System.Drawing.Point(11, 104);
            this.cbUseKiller.Name = "cbUseKiller";
            this.cbUseKiller.Size = new System.Drawing.Size(169, 17);
            this.cbUseKiller.TabIndex = 10;
            this.cbUseKiller.Text = "Use background process killer";
            this.toolTip.SetToolTip(this.cbUseKiller, "Enabled the use of tmm\'s process killer\r\nPlease read tmm\'s readme before use");
            this.cbUseKiller.UseVisualStyleBackColor = true;
            this.cbUseKiller.CheckedChanged += new System.EventHandler(this.cbUseKiller_CheckedChanged);
            // 
            // cbSafeMode
            // 
            this.cbSafeMode.AutoSize = true;
            this.cbSafeMode.Location = new System.Drawing.Point(11, 242);
            this.cbSafeMode.Name = "cbSafeMode";
            this.cbSafeMode.Size = new System.Drawing.Size(77, 17);
            this.cbSafeMode.TabIndex = 11;
            this.cbSafeMode.Text = "Safe mode";
            this.toolTip.SetToolTip(this.cbSafeMode, resources.GetString("cbSafeMode.ToolTip"));
            this.cbSafeMode.UseVisualStyleBackColor = true;
            this.cbSafeMode.CheckedChanged += new System.EventHandler(this.cbSafeMode_CheckedChanged);
            // 
            // bReset
            // 
            this.bReset.Location = new System.Drawing.Point(220, 616);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(194, 23);
            this.bReset.TabIndex = 28;
            this.bReset.Text = "Reset all settings to defaults";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // cbNewEspsLoadLast
            // 
            this.cbNewEspsLoadLast.AutoSize = true;
            this.cbNewEspsLoadLast.Location = new System.Drawing.Point(238, 196);
            this.cbNewEspsLoadLast.Name = "cbNewEspsLoadLast";
            this.cbNewEspsLoadLast.Size = new System.Drawing.Size(150, 17);
            this.cbNewEspsLoadLast.TabIndex = 13;
            this.cbNewEspsLoadLast.Text = "New esps always load last";
            this.toolTip.SetToolTip(this.cbNewEspsLoadLast, resources.GetString("cbNewEspsLoadLast.ToolTip"));
            this.cbNewEspsLoadLast.UseVisualStyleBackColor = true;
            this.cbNewEspsLoadLast.CheckedChanged += new System.EventHandler(this.cbNewEspsLoadLast_CheckedChanged);
            // 
            // cbAdvSelectMany
            // 
            this.cbAdvSelectMany.AutoSize = true;
            this.cbAdvSelectMany.Location = new System.Drawing.Point(11, 127);
            this.cbAdvSelectMany.Name = "cbAdvSelectMany";
            this.cbAdvSelectMany.Size = new System.Drawing.Size(195, 17);
            this.cbAdvSelectMany.TabIndex = 12;
            this.cbAdvSelectMany.Text = "Allow shift/ctrl on SelectMany menu";
            this.toolTip.SetToolTip(this.cbAdvSelectMany, "If checked, allows you to use windows explorer style shift/control shortcuts on a" +
        "n obmm scripts SelectMany dialogs");
            this.cbAdvSelectMany.UseVisualStyleBackColor = true;
            this.cbAdvSelectMany.CheckedChanged += new System.EventHandler(this.cbAdvSelectMany_CheckedChanged);
            // 
            // cbTrackConflicts
            // 
            this.cbTrackConflicts.AutoSize = true;
            this.cbTrackConflicts.Location = new System.Drawing.Point(238, 104);
            this.cbTrackConflicts.Name = "cbTrackConflicts";
            this.cbTrackConflicts.Size = new System.Drawing.Size(137, 17);
            this.cbTrackConflicts.TabIndex = 7;
            this.cbTrackConflicts.Text = "Enable conflict tracking";
            this.toolTip.SetToolTip(this.cbTrackConflicts, "If checked, tmm will keep track of which omods contain shared data files, and col" +
        "our code them orange/red/black as appropriate\r\nncheck this to greatly speed up t" +
        "mm");
            this.cbTrackConflicts.UseVisualStyleBackColor = true;
            this.cbTrackConflicts.CheckedChanged += new System.EventHandler(this.cbTrackConflicts_CheckedChanged);
            // 
            // cbAllowInsecureScripts
            // 
            this.cbAllowInsecureScripts.AutoSize = true;
            this.cbAllowInsecureScripts.Location = new System.Drawing.Point(11, 150);
            this.cbAllowInsecureScripts.Name = "cbAllowInsecureScripts";
            this.cbAllowInsecureScripts.Size = new System.Drawing.Size(155, 17);
            this.cbAllowInsecureScripts.TabIndex = 14;
            this.cbAllowInsecureScripts.Text = "Allow additional script types";
            this.toolTip.SetToolTip(this.cbAllowInsecureScripts, "If checked, omods are allowed to use python, C# or vb for scripting");
            this.cbAllowInsecureScripts.UseVisualStyleBackColor = true;
            this.cbAllowInsecureScripts.CheckedChanged += new System.EventHandler(this.cbAllowInsecureScripts_CheckedChanged);
            // 
            // cbNeverModifyLoadOrder
            // 
            this.cbNeverModifyLoadOrder.AutoSize = true;
            this.cbNeverModifyLoadOrder.Location = new System.Drawing.Point(238, 242);
            this.cbNeverModifyLoadOrder.Name = "cbNeverModifyLoadOrder";
            this.cbNeverModifyLoadOrder.Size = new System.Drawing.Size(138, 17);
            this.cbNeverModifyLoadOrder.TabIndex = 29;
            this.cbNeverModifyLoadOrder.Text = "Never modify load order";
            this.toolTip.SetToolTip(this.cbNeverModifyLoadOrder, "If checked, tmm will never change the last modified date on an esp, even if you o" +
        "r a script tries to change the load order");
            this.cbNeverModifyLoadOrder.UseVisualStyleBackColor = true;
            this.cbNeverModifyLoadOrder.CheckedChanged += new System.EventHandler(this.cbNeverModifyLoadOrder_CheckedChanged);
            // 
            // cbAskToBeNexusDownloadManager
            // 
            this.cbAskToBeNexusDownloadManager.AutoSize = true;
            this.cbAskToBeNexusDownloadManager.Checked = true;
            this.cbAskToBeNexusDownloadManager.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAskToBeNexusDownloadManager.Location = new System.Drawing.Point(11, 173);
            this.cbAskToBeNexusDownloadManager.Name = "cbAskToBeNexusDownloadManager";
            this.cbAskToBeNexusDownloadManager.Size = new System.Drawing.Size(200, 17);
            this.cbAskToBeNexusDownloadManager.TabIndex = 30;
            this.cbAskToBeNexusDownloadManager.Text = "Ask to be Nexus Download Manager";
            this.toolTip.SetToolTip(this.cbAskToBeNexusDownloadManager, "If checked, tmm will ask if you want to set it to handle Nexus Download Manager");
            this.cbAskToBeNexusDownloadManager.UseVisualStyleBackColor = true;
            this.cbAskToBeNexusDownloadManager.CheckedChanged += new System.EventHandler(this.cbAskToBeNexusDownloadManager_CheckedChanged);
            // 
            // cbActivateOnDoubleClick
            // 
            this.cbActivateOnDoubleClick.AutoSize = true;
            this.cbActivateOnDoubleClick.Checked = true;
            this.cbActivateOnDoubleClick.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActivateOnDoubleClick.Location = new System.Drawing.Point(238, 150);
            this.cbActivateOnDoubleClick.Name = "cbActivateOnDoubleClick";
            this.cbActivateOnDoubleClick.Size = new System.Drawing.Size(180, 17);
            this.cbActivateOnDoubleClick.TabIndex = 31;
            this.cbActivateOnDoubleClick.Text = "(de)activate mod on double-click";
            this.toolTip.SetToolTip(this.cbActivateOnDoubleClick, "If checked, tmm will let you activate or deactivate mods by double-clicking them");
            this.cbActivateOnDoubleClick.UseVisualStyleBackColor = true;
            this.cbActivateOnDoubleClick.CheckedChanged += new System.EventHandler(this.cbActivateOnDoubleClick_CheckedChanged);
            // 
            // cbLoadOrderAsUTF8
            // 
            this.cbLoadOrderAsUTF8.AutoSize = true;
            this.cbLoadOrderAsUTF8.Location = new System.Drawing.Point(11, 196);
            this.cbLoadOrderAsUTF8.Name = "cbLoadOrderAsUTF8";
            this.cbLoadOrderAsUTF8.Size = new System.Drawing.Size(174, 17);
            this.cbLoadOrderAsUTF8.TabIndex = 32;
            this.cbLoadOrderAsUTF8.Text = "Save loadorder in UTF-8 format";
            this.toolTip.SetToolTip(this.cbLoadOrderAsUTF8, "If checked, tmm will save the loadorder in UTF-8 format");
            this.cbLoadOrderAsUTF8.UseVisualStyleBackColor = true;
            this.cbLoadOrderAsUTF8.CheckedChanged += new System.EventHandler(this.cbLoadOrderAsUTF8_CheckedChanged);
            // 
            // cbUseTimeStamps
            // 
            this.cbUseTimeStamps.AutoSize = true;
            this.cbUseTimeStamps.Checked = true;
            this.cbUseTimeStamps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseTimeStamps.Location = new System.Drawing.Point(238, 265);
            this.cbUseTimeStamps.Name = "cbUseTimeStamps";
            this.cbUseTimeStamps.Size = new System.Drawing.Size(193, 17);
            this.cbUseTimeStamps.TabIndex = 33;
            this.cbUseTimeStamps.Text = "Enforce loadorder using timestamps";
            this.toolTip.SetToolTip(this.cbUseTimeStamps, "If checked, tmm will change the timestamps of esp/esm and BSA to enforce loadorde" +
        "r");
            this.cbUseTimeStamps.UseVisualStyleBackColor = true;
            this.cbUseTimeStamps.CheckedChanged += new System.EventHandler(this.cbUseTimeStamps_CheckedChanged);
            // 
            // cbEnableDebugLogging
            // 
            this.cbEnableDebugLogging.AutoSize = true;
            this.cbEnableDebugLogging.Location = new System.Drawing.Point(11, 219);
            this.cbEnableDebugLogging.Name = "cbEnableDebugLogging";
            this.cbEnableDebugLogging.Size = new System.Drawing.Size(129, 17);
            this.cbEnableDebugLogging.TabIndex = 34;
            this.cbEnableDebugLogging.Text = "Enable debug logging";
            this.toolTip.SetToolTip(this.cbEnableDebugLogging, "If checked, tmm will log debug information into obmm.log in the game directory");
            this.cbEnableDebugLogging.UseVisualStyleBackColor = true;
            this.cbEnableDebugLogging.CheckedChanged += new System.EventHandler(this.cbEnableDebugLogging_CheckedChanged);
            // 
            // cbGhostInactiveMods
            // 
            this.cbGhostInactiveMods.AutoSize = true;
            this.cbGhostInactiveMods.Location = new System.Drawing.Point(238, 311);
            this.cbGhostInactiveMods.Name = "cbGhostInactiveMods";
            this.cbGhostInactiveMods.Size = new System.Drawing.Size(122, 17);
            this.cbGhostInactiveMods.TabIndex = 35;
            this.cbGhostInactiveMods.Text = "Ghost inactive mods";
            this.toolTip.SetToolTip(this.cbGhostInactiveMods, "If checked, tmm will hide ESPs, ESMs and BSAs by adding .ghost to the name. This " +
        "can speed up game load");
            this.cbGhostInactiveMods.UseVisualStyleBackColor = true;
            this.cbGhostInactiveMods.CheckedChanged += new System.EventHandler(this.cbGhostInactiveMods_CheckedChanged);
            // 
            // cbShowHidden
            // 
            this.cbShowHidden.AutoSize = true;
            this.cbShowHidden.Location = new System.Drawing.Point(238, 288);
            this.cbShowHidden.Name = "cbShowHidden";
            this.cbShowHidden.Size = new System.Drawing.Size(116, 17);
            this.cbShowHidden.TabIndex = 36;
            this.cbShowHidden.Text = "Show hidden mods";
            this.toolTip.SetToolTip(this.cbShowHidden, "If checked, tmm will show all hidden mods in the mod list.");
            this.cbShowHidden.UseVisualStyleBackColor = true;
            this.cbShowHidden.CheckedChanged += new System.EventHandler(this.cbShowHidden_CheckedChanged);
            // 
            // cbSaveOmod2AsZip
            // 
            this.cbSaveOmod2AsZip.AutoSize = true;
            this.cbSaveOmod2AsZip.Location = new System.Drawing.Point(238, 380);
            this.cbSaveOmod2AsZip.Name = "cbSaveOmod2AsZip";
            this.cbSaveOmod2AsZip.Size = new System.Drawing.Size(175, 17);
            this.cbSaveOmod2AsZip.TabIndex = 38;
            this.cbSaveOmod2AsZip.Text = "Use zip/7z extension for omod2";
            this.toolTip.SetToolTip(this.cbSaveOmod2AsZip, "If checked, omod2 will have a ZIP extension for compatibility with 3rd party (lik" +
        "e NMM)");
            this.cbSaveOmod2AsZip.UseVisualStyleBackColor = true;
            this.cbSaveOmod2AsZip.CheckedChanged += new System.EventHandler(this.cbSaveOmod2AsZip_CheckedChanged);
            // 
            // cbDeactivateMissingOMODs
            // 
            this.cbDeactivateMissingOMODs.AutoSize = true;
            this.cbDeactivateMissingOMODs.Location = new System.Drawing.Point(11, 265);
            this.cbDeactivateMissingOMODs.Name = "cbDeactivateMissingOMODs";
            this.cbDeactivateMissingOMODs.Size = new System.Drawing.Size(156, 17);
            this.cbDeactivateMissingOMODs.TabIndex = 39;
            this.cbDeactivateMissingOMODs.Text = "Deactivate missing OMODs";
            this.toolTip.SetToolTip(this.cbDeactivateMissingOMODs, "Remove the mod files from the game if the OMOD is not found");
            this.cbDeactivateMissingOMODs.UseVisualStyleBackColor = true;
            this.cbDeactivateMissingOMODs.CheckedChanged += new System.EventHandler(this.cbDeactivateMissingOMODs_CheckedChanged);
            // 
            // cbWarnAboutMissingInfo
            // 
            this.cbWarnAboutMissingInfo.AutoSize = true;
            this.cbWarnAboutMissingInfo.Checked = true;
            this.cbWarnAboutMissingInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWarnAboutMissingInfo.Location = new System.Drawing.Point(238, 334);
            this.cbWarnAboutMissingInfo.Name = "cbWarnAboutMissingInfo";
            this.cbWarnAboutMissingInfo.Size = new System.Drawing.Size(211, 17);
            this.cbWarnAboutMissingInfo.TabIndex = 40;
            this.cbWarnAboutMissingInfo.Text = "Warn about missing Author/Description";
            this.toolTip.SetToolTip(this.cbWarnAboutMissingInfo, "If Checked displays a dialog box asking if you want to continue with no author na" +
        "me or no description");
            this.cbWarnAboutMissingInfo.UseVisualStyleBackColor = true;
            this.cbWarnAboutMissingInfo.CheckedChanged += new System.EventHandler(this.cbWarnAboutMissingInfo_CheckedChanged);
            // 
            // cbShowSimpleConflictDialog
            // 
            this.cbShowSimpleConflictDialog.AutoSize = true;
            this.cbShowSimpleConflictDialog.Location = new System.Drawing.Point(11, 288);
            this.cbShowSimpleConflictDialog.Name = "cbShowSimpleConflictDialog";
            this.cbShowSimpleConflictDialog.Size = new System.Drawing.Size(173, 17);
            this.cbShowSimpleConflictDialog.TabIndex = 41;
            this.cbShowSimpleConflictDialog.Text = "Use simple file conflict handling";
            this.toolTip.SetToolTip(this.cbShowSimpleConflictDialog, "Show the simple overwrite dialog instead of chosing conflicted files");
            this.cbShowSimpleConflictDialog.UseVisualStyleBackColor = true;
            this.cbShowSimpleConflictDialog.CheckedChanged += new System.EventHandler(this.cbShowSimpleConflictDialog_CheckedChanged);
            // 
            // cbAlwaysImportOmodData
            // 
            this.cbAlwaysImportOmodData.AutoSize = true;
            this.cbAlwaysImportOmodData.Location = new System.Drawing.Point(11, 311);
            this.cbAlwaysImportOmodData.Name = "cbAlwaysImportOmodData";
            this.cbAlwaysImportOmodData.Size = new System.Drawing.Size(145, 17);
            this.cbAlwaysImportOmodData.TabIndex = 42;
            this.cbAlwaysImportOmodData.Text = "Always import Omod data";
            this.toolTip.SetToolTip(this.cbAlwaysImportOmodData, "Import OMOD Conversion data if the mod includes it");
            this.cbAlwaysImportOmodData.UseVisualStyleBackColor = true;
            this.cbAlwaysImportOmodData.CheckedChanged += new System.EventHandler(this.cbAlwaysImportOmodData_CheckedChanged);
            // 
            // cbAlwaysImportNexusData
            // 
            this.cbAlwaysImportNexusData.AutoSize = true;
            this.cbAlwaysImportNexusData.Location = new System.Drawing.Point(11, 334);
            this.cbAlwaysImportNexusData.Name = "cbAlwaysImportNexusData";
            this.cbAlwaysImportNexusData.Size = new System.Drawing.Size(147, 17);
            this.cbAlwaysImportNexusData.TabIndex = 43;
            this.cbAlwaysImportNexusData.Text = "Always import Nexus data";
            this.toolTip.SetToolTip(this.cbAlwaysImportNexusData, "Retrieve mod data from TES Nexus");
            this.cbAlwaysImportNexusData.UseVisualStyleBackColor = true;
            this.cbAlwaysImportNexusData.CheckedChanged += new System.EventHandler(this.cbAlwaysImportNexusData_CheckedChanged);
            // 
            // cbAlwaysImportOCDData
            // 
            this.cbAlwaysImportOCDData.AutoSize = true;
            this.cbAlwaysImportOCDData.Location = new System.Drawing.Point(11, 357);
            this.cbAlwaysImportOCDData.Name = "cbAlwaysImportOCDData";
            this.cbAlwaysImportOCDData.Size = new System.Drawing.Size(143, 17);
            this.cbAlwaysImportOCDData.TabIndex = 44;
            this.cbAlwaysImportOCDData.Text = "Always Import OCD Data";
            this.toolTip.SetToolTip(this.cbAlwaysImportOCDData, "Import mod data from already downloaded Omod Conversion Data files");
            this.cbAlwaysImportOCDData.UseVisualStyleBackColor = true;
            this.cbAlwaysImportOCDData.CheckedChanged += new System.EventHandler(this.cbAlwaysImportOCDData_CheckedChanged);
            // 
            // cbIncludeVersionInModName
            // 
            this.cbIncludeVersionInModName.AutoSize = true;
            this.cbIncludeVersionInModName.Location = new System.Drawing.Point(12, 403);
            this.cbIncludeVersionInModName.Name = "cbIncludeVersionInModName";
            this.cbIncludeVersionInModName.Size = new System.Drawing.Size(162, 17);
            this.cbIncludeVersionInModName.TabIndex = 45;
            this.cbIncludeVersionInModName.Text = "Include Version in mod name";
            this.toolTip.SetToolTip(this.cbIncludeVersionInModName, "Add the mod version in the name (requires restart)");
            this.cbIncludeVersionInModName.UseVisualStyleBackColor = true;
            this.cbIncludeVersionInModName.CheckedChanged += new System.EventHandler(this.cbIncludeVersionInModName_CheckedChanged);
            // 
            // cbShowModNameInsteadOfFilename
            // 
            this.cbShowModNameInsteadOfFilename.AutoSize = true;
            this.cbShowModNameInsteadOfFilename.Location = new System.Drawing.Point(238, 357);
            this.cbShowModNameInsteadOfFilename.Name = "cbShowModNameInsteadOfFilename";
            this.cbShowModNameInsteadOfFilename.Size = new System.Drawing.Size(196, 17);
            this.cbShowModNameInsteadOfFilename.TabIndex = 46;
            this.cbShowModNameInsteadOfFilename.Text = "Show mod name instead of filename";
            this.toolTip.SetToolTip(this.cbShowModNameInsteadOfFilename, "Show the mod name in the list instead of the mod filename (requires restart)");
            this.cbShowModNameInsteadOfFilename.UseVisualStyleBackColor = true;
            this.cbShowModNameInsteadOfFilename.CheckedChanged += new System.EventHandler(this.cbShowModNameInsteadOfFilename_CheckedChanged);
            // 
            // bAlternateBackupFolder
            // 
            this.bAlternateBackupFolder.Location = new System.Drawing.Point(221, 567);
            this.bAlternateBackupFolder.Name = "bAlternateBackupFolder";
            this.bAlternateBackupFolder.Size = new System.Drawing.Size(194, 23);
            this.bAlternateBackupFolder.TabIndex = 49;
            this.bAlternateBackupFolder.Text = "Set alternate backup folder";
            this.toolTip.SetToolTip(this.bAlternateBackupFolder, "Allows you to set a different place to store backups of conflicting files");
            this.bAlternateBackupFolder.UseVisualStyleBackColor = true;
            this.bAlternateBackupFolder.Click += new System.EventHandler(this.bAlternateBackupFolder_Click);
            // 
            // cbAskIfLoadAsIsOrImport
            // 
            this.cbAskIfLoadAsIsOrImport.AutoSize = true;
            this.cbAskIfLoadAsIsOrImport.Location = new System.Drawing.Point(11, 380);
            this.cbAskIfLoadAsIsOrImport.Name = "cbAskIfLoadAsIsOrImport";
            this.cbAskIfLoadAsIsOrImport.Size = new System.Drawing.Size(193, 17);
            this.cbAskIfLoadAsIsOrImport.TabIndex = 50;
            this.cbAskIfLoadAsIsOrImport.Text = "Always import Nexus files for editing";
            this.toolTip.SetToolTip(this.cbAskIfLoadAsIsOrImport, "Unchecked asks if load as is is wanted");
            this.cbAskIfLoadAsIsOrImport.UseVisualStyleBackColor = true;
            this.cbAskIfLoadAsIsOrImport.CheckedChanged += new System.EventHandler(this.cbAskIfLoadAsIsOrImport_CheckedChanged);
            // 
            // btnNexusLoginInfo
            // 
            this.btnNexusLoginInfo.Location = new System.Drawing.Point(11, 616);
            this.btnNexusLoginInfo.Name = "btnNexusLoginInfo";
            this.btnNexusLoginInfo.Size = new System.Drawing.Size(186, 23);
            this.btnNexusLoginInfo.TabIndex = 37;
            this.btnNexusLoginInfo.Text = "Nexus login information";
            this.btnNexusLoginInfo.UseVisualStyleBackColor = true;
            this.btnNexusLoginInfo.Click += new System.EventHandler(this.btnNexusLoginInfo_Click);
            // 
            // cbPreventMovingAnESPBeforeAnESM
            // 
            this.cbPreventMovingAnESPBeforeAnESM.AutoSize = true;
            this.cbPreventMovingAnESPBeforeAnESM.Location = new System.Drawing.Point(238, 219);
            this.cbPreventMovingAnESPBeforeAnESM.Name = "cbPreventMovingAnESPBeforeAnESM";
            this.cbPreventMovingAnESPBeforeAnESM.Size = new System.Drawing.Size(213, 17);
            this.cbPreventMovingAnESPBeforeAnESM.TabIndex = 47;
            this.cbPreventMovingAnESPBeforeAnESM.Text = "Prevent moving an ESP before an ESM";
            this.cbPreventMovingAnESPBeforeAnESM.UseVisualStyleBackColor = true;
            this.cbPreventMovingAnESPBeforeAnESM.CheckedChanged += new System.EventHandler(this.cbPreventMovingAnESPBeforeAnESM_CheckedChanged);
            // 
            // cbOmod2IsDefault
            // 
            this.cbOmod2IsDefault.AutoSize = true;
            this.cbOmod2IsDefault.Location = new System.Drawing.Point(238, 403);
            this.cbOmod2IsDefault.Name = "cbOmod2IsDefault";
            this.cbOmod2IsDefault.Size = new System.Drawing.Size(160, 17);
            this.cbOmod2IsDefault.TabIndex = 48;
            this.cbOmod2IsDefault.Text = "Default mod format is omod2";
            this.cbOmod2IsDefault.UseVisualStyleBackColor = true;
            this.cbOmod2IsDefault.CheckedChanged += new System.EventHandler(this.cbOmod2IsDefault_CheckedChanged);
            // 
            // btnSetGamePath
            // 
            this.btnSetGamePath.Location = new System.Drawing.Point(11, 568);
            this.btnSetGamePath.Name = "btnSetGamePath";
            this.btnSetGamePath.Size = new System.Drawing.Size(186, 23);
            this.btnSetGamePath.TabIndex = 51;
            this.btnSetGamePath.Text = "Set game path";
            this.btnSetGamePath.UseVisualStyleBackColor = true;
            this.btnSetGamePath.Click += new System.EventHandler(this.btnSetGamePath_Click);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 646);
            this.Controls.Add(this.btnSetGamePath);
            this.Controls.Add(this.cbAskIfLoadAsIsOrImport);
            this.Controls.Add(this.bAlternateBackupFolder);
            this.Controls.Add(this.cbOmod2IsDefault);
            this.Controls.Add(this.cbPreventMovingAnESPBeforeAnESM);
            this.Controls.Add(this.cbShowModNameInsteadOfFilename);
            this.Controls.Add(this.cbIncludeVersionInModName);
            this.Controls.Add(this.cbAlwaysImportOCDData);
            this.Controls.Add(this.cbAlwaysImportNexusData);
            this.Controls.Add(this.cbAlwaysImportOmodData);
            this.Controls.Add(this.cbShowSimpleConflictDialog);
            this.Controls.Add(this.cbWarnAboutMissingInfo);
            this.Controls.Add(this.cbDeactivateMissingOMODs);
            this.Controls.Add(this.cbSaveOmod2AsZip);
            this.Controls.Add(this.btnNexusLoginInfo);
            this.Controls.Add(this.cbShowHidden);
            this.Controls.Add(this.cbGhostInactiveMods);
            this.Controls.Add(this.cbEnableDebugLogging);
            this.Controls.Add(this.cbUseTimeStamps);
            this.Controls.Add(this.cbLoadOrderAsUTF8);
            this.Controls.Add(this.cbActivateOnDoubleClick);
            this.Controls.Add(this.cbAskToBeNexusDownloadManager);
            this.Controls.Add(this.cbNeverModifyLoadOrder);
            this.Controls.Add(this.cbAllowInsecureScripts);
            this.Controls.Add(this.cbTrackConflicts);
            this.Controls.Add(this.cbAdvSelectMany);
            this.Controls.Add(this.cbNewEspsLoadLast);
            this.Controls.Add(this.cbSafeMode);
            this.Controls.Add(this.cbUseKiller);
            this.Controls.Add(this.cbUpdateEsps);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.cbCompressionBoost);
            this.Controls.Add(this.cbAutoupdateConflicts);
            this.Controls.Add(this.cbShowEspWarnings);
            this.Controls.Add(this.bSetFont);
            this.Controls.Add(this.bMoveTempFolder);
            this.Controls.Add(this.bResetTempFolder);
            this.Controls.Add(this.cbLockFOV);
            this.Controls.Add(this.cbOmodInfo);
            this.Controls.Add(this.cbDataWarnings);
            this.Controls.Add(this.cbIniWarn);
            this.Controls.Add(this.cbWarnings);
            this.Controls.Add(this.tbCommandLine);
            this.Controls.Add(this.bMoveToEnd);
            this.Controls.Add(this.cmbGroups);
            this.Controls.Add(this.bClearGroups);
            this.Controls.Add(this.bRenameGroup);
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bMoveModFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "OptionsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbWarnings;
        private System.Windows.Forms.ComboBox cmbGroups;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.CheckBox cbIniWarn;
        private System.Windows.Forms.CheckBox cbDataWarnings;
        private System.Windows.Forms.CheckBox cbOmodInfo;
        private System.Windows.Forms.CheckBox cbLockFOV;
        private System.Windows.Forms.Button bMoveModFolder;
        private System.Windows.Forms.FolderBrowserDialog omodDirDialog;
        private System.Windows.Forms.CheckBox cbShowEspWarnings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCommandLine;
        private System.Windows.Forms.ContextMenuStrip DudMenu;
        private System.Windows.Forms.Button bRenameGroup;
        private System.Windows.Forms.Button bClearGroups;
        private System.Windows.Forms.Button bMoveToEnd;
        private System.Windows.Forms.CheckBox cbCompressionBoost;
        private System.Windows.Forms.CheckBox cbAutoupdateConflicts;
        private System.Windows.Forms.CheckBox cbUpdateEsps;
        private System.Windows.Forms.Button bResetTempFolder;
        private System.Windows.Forms.Button bMoveTempFolder;
        private System.Windows.Forms.Button bSetFont;
        private System.Windows.Forms.FontDialog GroupFontDialog;
        private System.Windows.Forms.CheckBox cbUseKiller;
        private System.Windows.Forms.CheckBox cbSafeMode;
        private System.Windows.Forms.Button bReset;
        private System.Windows.Forms.CheckBox cbNewEspsLoadLast;
        private System.Windows.Forms.CheckBox cbAdvSelectMany;
        private System.Windows.Forms.CheckBox cbTrackConflicts;
        private System.Windows.Forms.CheckBox cbAllowInsecureScripts;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox cbNeverModifyLoadOrder;
        private System.Windows.Forms.CheckBox cbAskToBeNexusDownloadManager;
        private System.Windows.Forms.CheckBox cbActivateOnDoubleClick;
        private System.Windows.Forms.CheckBox cbLoadOrderAsUTF8;
        private System.Windows.Forms.CheckBox cbUseTimeStamps;
        private System.Windows.Forms.CheckBox cbEnableDebugLogging;
        private System.Windows.Forms.CheckBox cbGhostInactiveMods;
        private System.Windows.Forms.CheckBox cbShowHidden;
        private System.Windows.Forms.Button btnNexusLoginInfo;
        private System.Windows.Forms.CheckBox cbSaveOmod2AsZip;
        private System.Windows.Forms.CheckBox cbDeactivateMissingOMODs;
        private System.Windows.Forms.CheckBox cbWarnAboutMissingInfo;
        private System.Windows.Forms.CheckBox cbShowSimpleConflictDialog;
        private System.Windows.Forms.CheckBox cbAlwaysImportOmodData;
        private System.Windows.Forms.CheckBox cbAlwaysImportNexusData;
        private System.Windows.Forms.CheckBox cbAlwaysImportOCDData;
        private System.Windows.Forms.CheckBox cbIncludeVersionInModName;
        private System.Windows.Forms.CheckBox cbShowModNameInsteadOfFilename;
        private System.Windows.Forms.CheckBox cbPreventMovingAnESPBeforeAnESM;
        private System.Windows.Forms.CheckBox cbOmod2IsDefault;
        private System.Windows.Forms.Button bAlternateBackupFolder;
        private System.Windows.Forms.CheckBox cbAskIfLoadAsIsOrImport;
        private System.Windows.Forms.Button btnSetGamePath;
    }
}