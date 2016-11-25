namespace OblivionModManager.Forms
{
    partial class SimResults
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimResults));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpDataFiles = new System.Windows.Forms.GroupBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lblDataInstalled = new System.Windows.Forms.Label();
            this.lstDFInstalled = new System.Windows.Forms.ListBox();
            this.lblDataIgnore = new System.Windows.Forms.Label();
            this.lstDFIgnored = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lblPlugInstall = new System.Windows.Forms.Label();
            this.lstPInstalled = new System.Windows.Forms.ListBox();
            this.lblPlugIgnore = new System.Windows.Forms.Label();
            this.lstPIgnored = new System.Windows.Forms.ListBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.grpFlags = new System.Windows.Forms.GroupBox();
            this.chkPlugins = new System.Windows.Forms.CheckBox();
            this.chkCanceled = new System.Windows.Forms.CheckBox();
            this.chkDataFiles = new System.Windows.Forms.CheckBox();
            this.tbEdits = new System.Windows.Forms.TabControl();
            this.tbpLoadOrder = new System.Windows.Forms.TabPage();
            this.lvLoadOrder = new System.Windows.Forms.ListView();
            this.clPlugin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTarget = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clLoadAfter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbpCopyDF = new System.Windows.Forms.TabPage();
            this.lvCopiedDF = new System.Windows.Forms.ListView();
            this.clCopyFrom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clCopyTo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbpCopyESP = new System.Windows.Forms.TabPage();
            this.lvCopiedPF = new System.Windows.Forms.ListView();
            this.clPPlugin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clPCopyTo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbpConflicts = new System.Windows.Forms.TabPage();
            this.lvConflicts = new System.Windows.Forms.ListView();
            this.clConflicts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clCMinVer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clCMaxVer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clCLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clCComment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clCRegex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbpDepends = new System.Windows.Forms.TabPage();
            this.lvDepends = new System.Windows.Forms.ListView();
            this.clDepends = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDMinVer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDMaxVer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDComment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDRegex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbpINIEdit = new System.Windows.Forms.TabPage();
            this.lvINIEdit = new System.Windows.Forms.ListView();
            this.clSection = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clNewValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbpSDP = new System.Windows.Forms.TabPage();
            this.lvSDPEdit = new System.Windows.Forms.ListView();
            this.clPackage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clShader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbpESPEdits = new System.Windows.Forms.TabPage();
            this.lvESPEdit = new System.Windows.Forms.ListView();
            this.clESPPlugin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clESPType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clESPEDID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbpBSAReg = new System.Windows.Forms.TabPage();
            this.lvBSAReg = new System.Windows.Forms.ListView();
            this.clBSAFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbpESPWarnings = new System.Windows.Forms.TabPage();
            this.lvESPWarn = new System.Windows.Forms.ListView();
            this.clPluginWarn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clWarning = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbpDeacESP = new System.Windows.Forms.TabPage();
            this.lvESPUnchecked = new System.Windows.Forms.ListView();
            this.clESPUnchecked = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpDataFiles.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.grpFlags.SuspendLayout();
            this.tbEdits.SuspendLayout();
            this.tbpLoadOrder.SuspendLayout();
            this.tbpCopyDF.SuspendLayout();
            this.tbpCopyESP.SuspendLayout();
            this.tbpConflicts.SuspendLayout();
            this.tbpDepends.SuspendLayout();
            this.tbpINIEdit.SuspendLayout();
            this.tbpSDP.SuspendLayout();
            this.tbpESPEdits.SuspendLayout();
            this.tbpBSAReg.SuspendLayout();
            this.tbpESPWarnings.SuspendLayout();
            this.tbpDeacESP.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grpDataFiles);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(805, 462);
            this.splitContainer1.SplitterDistance = 398;
            this.splitContainer1.SplitterIncrement = 10;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 6;
            // 
            // grpDataFiles
            // 
            this.grpDataFiles.Controls.Add(this.splitContainer3);
            this.grpDataFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDataFiles.Location = new System.Drawing.Point(0, 0);
            this.grpDataFiles.Name = "grpDataFiles";
            this.grpDataFiles.Padding = new System.Windows.Forms.Padding(5);
            this.grpDataFiles.Size = new System.Drawing.Size(398, 462);
            this.grpDataFiles.TabIndex = 6;
            this.grpDataFiles.TabStop = false;
            this.grpDataFiles.Text = "Data Files";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(5, 18);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lblDataInstalled);
            this.splitContainer3.Panel1.Controls.Add(this.lstDFInstalled);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.lblDataIgnore);
            this.splitContainer3.Panel2.Controls.Add(this.lstDFIgnored);
            this.splitContainer3.Size = new System.Drawing.Size(388, 439);
            this.splitContainer3.SplitterDistance = 220;
            this.splitContainer3.TabIndex = 0;
            // 
            // lblDataInstalled
            // 
            this.lblDataInstalled.AutoSize = true;
            this.lblDataInstalled.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDataInstalled.Location = new System.Drawing.Point(0, 0);
            this.lblDataInstalled.Name = "lblDataInstalled";
            this.lblDataInstalled.Size = new System.Drawing.Size(99, 13);
            this.lblDataInstalled.TabIndex = 4;
            this.lblDataInstalled.Text = "Installed Data Files:";
            // 
            // lstDFInstalled
            // 
            this.lstDFInstalled.BackColor = System.Drawing.Color.White;
            this.lstDFInstalled.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstDFInstalled.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lstDFInstalled.FormattingEnabled = true;
            this.lstDFInstalled.Location = new System.Drawing.Point(0, 21);
            this.lstDFInstalled.Name = "lstDFInstalled";
            this.lstDFInstalled.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstDFInstalled.Size = new System.Drawing.Size(388, 199);
            this.lstDFInstalled.TabIndex = 3;
            // 
            // lblDataIgnore
            // 
            this.lblDataIgnore.AutoSize = true;
            this.lblDataIgnore.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDataIgnore.Location = new System.Drawing.Point(0, 0);
            this.lblDataIgnore.Name = "lblDataIgnore";
            this.lblDataIgnore.Size = new System.Drawing.Size(96, 13);
            this.lblDataIgnore.TabIndex = 5;
            this.lblDataIgnore.Text = "Ignored Data Files:";
            // 
            // lstDFIgnored
            // 
            this.lstDFIgnored.BackColor = System.Drawing.Color.White;
            this.lstDFIgnored.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstDFIgnored.ForeColor = System.Drawing.Color.Red;
            this.lstDFIgnored.FormattingEnabled = true;
            this.lstDFIgnored.Location = new System.Drawing.Point(0, 16);
            this.lstDFIgnored.Name = "lstDFIgnored";
            this.lstDFIgnored.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstDFIgnored.Size = new System.Drawing.Size(388, 199);
            this.lstDFIgnored.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(402, 462);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Plugins";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(5, 18);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lblPlugInstall);
            this.splitContainer2.Panel1.Controls.Add(this.lstPInstalled);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lblPlugIgnore);
            this.splitContainer2.Panel2.Controls.Add(this.lstPIgnored);
            this.splitContainer2.Size = new System.Drawing.Size(392, 439);
            this.splitContainer2.SplitterDistance = 220;
            this.splitContainer2.TabIndex = 2;
            // 
            // lblPlugInstall
            // 
            this.lblPlugInstall.AutoSize = true;
            this.lblPlugInstall.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPlugInstall.Location = new System.Drawing.Point(0, 0);
            this.lblPlugInstall.Name = "lblPlugInstall";
            this.lblPlugInstall.Size = new System.Drawing.Size(83, 13);
            this.lblPlugInstall.TabIndex = 5;
            this.lblPlugInstall.Text = "Installed Plugins";
            // 
            // lstPInstalled
            // 
            this.lstPInstalled.BackColor = System.Drawing.Color.White;
            this.lstPInstalled.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstPInstalled.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lstPInstalled.FormattingEnabled = true;
            this.lstPInstalled.Location = new System.Drawing.Point(0, 21);
            this.lstPInstalled.Name = "lstPInstalled";
            this.lstPInstalled.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstPInstalled.Size = new System.Drawing.Size(392, 199);
            this.lstPInstalled.TabIndex = 0;
            // 
            // lblPlugIgnore
            // 
            this.lblPlugIgnore.AutoSize = true;
            this.lblPlugIgnore.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPlugIgnore.Location = new System.Drawing.Point(0, 0);
            this.lblPlugIgnore.Name = "lblPlugIgnore";
            this.lblPlugIgnore.Size = new System.Drawing.Size(80, 13);
            this.lblPlugIgnore.TabIndex = 6;
            this.lblPlugIgnore.Text = "Ignored Plugins";
            // 
            // lstPIgnored
            // 
            this.lstPIgnored.BackColor = System.Drawing.Color.White;
            this.lstPIgnored.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstPIgnored.ForeColor = System.Drawing.Color.Red;
            this.lstPIgnored.FormattingEnabled = true;
            this.lstPIgnored.Location = new System.Drawing.Point(0, 16);
            this.lstPIgnored.Name = "lstPIgnored";
            this.lstPIgnored.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstPIgnored.Size = new System.Drawing.Size(392, 199);
            this.lstPIgnored.TabIndex = 1;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer5);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer4.Size = new System.Drawing.Size(805, 734);
            this.splitContainer4.SplitterDistance = 268;
            this.splitContainer4.TabIndex = 7;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.grpFlags);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.tbEdits);
            this.splitContainer5.Size = new System.Drawing.Size(805, 268);
            this.splitContainer5.SplitterDistance = 172;
            this.splitContainer5.TabIndex = 0;
            // 
            // grpFlags
            // 
            this.grpFlags.Controls.Add(this.chkPlugins);
            this.grpFlags.Controls.Add(this.chkCanceled);
            this.grpFlags.Controls.Add(this.chkDataFiles);
            this.grpFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFlags.Location = new System.Drawing.Point(0, 0);
            this.grpFlags.Name = "grpFlags";
            this.grpFlags.Size = new System.Drawing.Size(172, 268);
            this.grpFlags.TabIndex = 7;
            this.grpFlags.TabStop = false;
            this.grpFlags.Text = "Install Flags";
            // 
            // chkPlugins
            // 
            this.chkPlugins.AutoCheck = false;
            this.chkPlugins.AutoSize = true;
            this.chkPlugins.Location = new System.Drawing.Point(6, 19);
            this.chkPlugins.Name = "chkPlugins";
            this.chkPlugins.Size = new System.Drawing.Size(104, 17);
            this.chkPlugins.TabIndex = 3;
            this.chkPlugins.Text = "Install All Plugins";
            this.chkPlugins.UseVisualStyleBackColor = true;
            // 
            // chkCanceled
            // 
            this.chkCanceled.AutoCheck = false;
            this.chkCanceled.AutoSize = true;
            this.chkCanceled.Location = new System.Drawing.Point(6, 65);
            this.chkCanceled.Name = "chkCanceled";
            this.chkCanceled.Size = new System.Drawing.Size(124, 17);
            this.chkCanceled.TabIndex = 5;
            this.chkCanceled.Text = "Installation Canceled";
            this.chkCanceled.UseVisualStyleBackColor = true;
            // 
            // chkDataFiles
            // 
            this.chkDataFiles.AutoCheck = false;
            this.chkDataFiles.AutoSize = true;
            this.chkDataFiles.Location = new System.Drawing.Point(6, 42);
            this.chkDataFiles.Name = "chkDataFiles";
            this.chkDataFiles.Size = new System.Drawing.Size(117, 17);
            this.chkDataFiles.TabIndex = 4;
            this.chkDataFiles.Text = "Install All Data Files";
            this.chkDataFiles.UseVisualStyleBackColor = true;
            // 
            // tbEdits
            // 
            this.tbEdits.Controls.Add(this.tbpLoadOrder);
            this.tbEdits.Controls.Add(this.tbpCopyDF);
            this.tbEdits.Controls.Add(this.tbpCopyESP);
            this.tbEdits.Controls.Add(this.tbpConflicts);
            this.tbEdits.Controls.Add(this.tbpDepends);
            this.tbEdits.Controls.Add(this.tbpINIEdit);
            this.tbEdits.Controls.Add(this.tbpSDP);
            this.tbEdits.Controls.Add(this.tbpESPEdits);
            this.tbEdits.Controls.Add(this.tbpBSAReg);
            this.tbEdits.Controls.Add(this.tbpESPWarnings);
            this.tbEdits.Controls.Add(this.tbpDeacESP);
            this.tbEdits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbEdits.Location = new System.Drawing.Point(0, 0);
            this.tbEdits.Name = "tbEdits";
            this.tbEdits.SelectedIndex = 0;
            this.tbEdits.Size = new System.Drawing.Size(629, 268);
            this.tbEdits.TabIndex = 0;
            // 
            // tbpLoadOrder
            // 
            this.tbpLoadOrder.Controls.Add(this.lvLoadOrder);
            this.tbpLoadOrder.Location = new System.Drawing.Point(4, 22);
            this.tbpLoadOrder.Name = "tbpLoadOrder";
            this.tbpLoadOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tbpLoadOrder.Size = new System.Drawing.Size(621, 242);
            this.tbpLoadOrder.TabIndex = 0;
            this.tbpLoadOrder.Text = "Load Order";
            this.tbpLoadOrder.UseVisualStyleBackColor = true;
            // 
            // lvLoadOrder
            // 
            this.lvLoadOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clPlugin,
            this.clTarget,
            this.clLoadAfter});
            this.lvLoadOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLoadOrder.Location = new System.Drawing.Point(3, 3);
            this.lvLoadOrder.Name = "lvLoadOrder";
            this.lvLoadOrder.Size = new System.Drawing.Size(615, 236);
            this.lvLoadOrder.TabIndex = 0;
            this.lvLoadOrder.UseCompatibleStateImageBehavior = false;
            this.lvLoadOrder.View = System.Windows.Forms.View.Details;
            // 
            // clPlugin
            // 
            this.clPlugin.Text = "Plugin";
            this.clPlugin.Width = 187;
            // 
            // clTarget
            // 
            this.clTarget.Text = "Target";
            this.clTarget.Width = 164;
            // 
            // clLoadAfter
            // 
            this.clLoadAfter.Text = "Load After";
            this.clLoadAfter.Width = 165;
            // 
            // tbpCopyDF
            // 
            this.tbpCopyDF.Controls.Add(this.lvCopiedDF);
            this.tbpCopyDF.Location = new System.Drawing.Point(4, 22);
            this.tbpCopyDF.Name = "tbpCopyDF";
            this.tbpCopyDF.Size = new System.Drawing.Size(621, 242);
            this.tbpCopyDF.TabIndex = 6;
            this.tbpCopyDF.Text = "Copied Data Files";
            this.tbpCopyDF.UseVisualStyleBackColor = true;
            // 
            // lvCopiedDF
            // 
            this.lvCopiedDF.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clCopyFrom,
            this.clCopyTo});
            this.lvCopiedDF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCopiedDF.Location = new System.Drawing.Point(0, 0);
            this.lvCopiedDF.Name = "lvCopiedDF";
            this.lvCopiedDF.Size = new System.Drawing.Size(621, 242);
            this.lvCopiedDF.TabIndex = 1;
            this.lvCopiedDF.UseCompatibleStateImageBehavior = false;
            this.lvCopiedDF.View = System.Windows.Forms.View.Details;
            // 
            // clCopyFrom
            // 
            this.clCopyFrom.Text = "Data File";
            this.clCopyFrom.Width = 267;
            // 
            // clCopyTo
            // 
            this.clCopyTo.Text = "Copy To";
            this.clCopyTo.Width = 295;
            // 
            // tbpCopyESP
            // 
            this.tbpCopyESP.Controls.Add(this.lvCopiedPF);
            this.tbpCopyESP.Location = new System.Drawing.Point(4, 22);
            this.tbpCopyESP.Name = "tbpCopyESP";
            this.tbpCopyESP.Size = new System.Drawing.Size(621, 242);
            this.tbpCopyESP.TabIndex = 7;
            this.tbpCopyESP.Text = "Copied Plugin Files";
            this.tbpCopyESP.UseVisualStyleBackColor = true;
            // 
            // lvCopiedPF
            // 
            this.lvCopiedPF.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clPPlugin,
            this.clPCopyTo});
            this.lvCopiedPF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCopiedPF.Location = new System.Drawing.Point(0, 0);
            this.lvCopiedPF.Name = "lvCopiedPF";
            this.lvCopiedPF.Size = new System.Drawing.Size(621, 242);
            this.lvCopiedPF.TabIndex = 2;
            this.lvCopiedPF.UseCompatibleStateImageBehavior = false;
            this.lvCopiedPF.View = System.Windows.Forms.View.Details;
            // 
            // clPPlugin
            // 
            this.clPPlugin.Text = "Plugin File";
            this.clPPlugin.Width = 267;
            // 
            // clPCopyTo
            // 
            this.clPCopyTo.Text = "Copy To";
            this.clPCopyTo.Width = 295;
            // 
            // tbpConflicts
            // 
            this.tbpConflicts.Controls.Add(this.lvConflicts);
            this.tbpConflicts.Location = new System.Drawing.Point(4, 22);
            this.tbpConflicts.Name = "tbpConflicts";
            this.tbpConflicts.Padding = new System.Windows.Forms.Padding(3);
            this.tbpConflicts.Size = new System.Drawing.Size(621, 242);
            this.tbpConflicts.TabIndex = 1;
            this.tbpConflicts.Text = "Conflicts";
            this.tbpConflicts.UseVisualStyleBackColor = true;
            // 
            // lvConflicts
            // 
            this.lvConflicts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clConflicts,
            this.clCMinVer,
            this.clCMaxVer,
            this.clCLevel,
            this.clCComment,
            this.clCRegex});
            this.lvConflicts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvConflicts.Location = new System.Drawing.Point(3, 3);
            this.lvConflicts.Name = "lvConflicts";
            this.lvConflicts.Size = new System.Drawing.Size(615, 236);
            this.lvConflicts.TabIndex = 3;
            this.lvConflicts.UseCompatibleStateImageBehavior = false;
            this.lvConflicts.View = System.Windows.Forms.View.Details;
            // 
            // clConflicts
            // 
            this.clConflicts.Text = "Conflicts With";
            this.clConflicts.Width = 178;
            // 
            // clCMinVer
            // 
            this.clCMinVer.Text = "Min Version";
            this.clCMinVer.Width = 72;
            // 
            // clCMaxVer
            // 
            this.clCMaxVer.Text = "Max Version";
            this.clCMaxVer.Width = 76;
            // 
            // clCLevel
            // 
            this.clCLevel.Text = "Level";
            this.clCLevel.Width = 62;
            // 
            // clCComment
            // 
            this.clCComment.Text = "Comment";
            this.clCComment.Width = 121;
            // 
            // clCRegex
            // 
            this.clCRegex.Text = "Regex";
            // 
            // tbpDepends
            // 
            this.tbpDepends.Controls.Add(this.lvDepends);
            this.tbpDepends.Location = new System.Drawing.Point(4, 22);
            this.tbpDepends.Name = "tbpDepends";
            this.tbpDepends.Size = new System.Drawing.Size(621, 242);
            this.tbpDepends.TabIndex = 2;
            this.tbpDepends.Text = "Dependencies";
            this.tbpDepends.UseVisualStyleBackColor = true;
            // 
            // lvDepends
            // 
            this.lvDepends.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clDepends,
            this.clDMinVer,
            this.clDMaxVer,
            this.clDComment,
            this.clDRegex});
            this.lvDepends.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDepends.Location = new System.Drawing.Point(0, 0);
            this.lvDepends.Name = "lvDepends";
            this.lvDepends.Size = new System.Drawing.Size(621, 242);
            this.lvDepends.TabIndex = 4;
            this.lvDepends.UseCompatibleStateImageBehavior = false;
            this.lvDepends.View = System.Windows.Forms.View.Details;
            // 
            // clDepends
            // 
            this.clDepends.Text = "Depends On";
            this.clDepends.Width = 178;
            // 
            // clDMinVer
            // 
            this.clDMinVer.Text = "Min Version";
            this.clDMinVer.Width = 72;
            // 
            // clDMaxVer
            // 
            this.clDMaxVer.Text = "Max Version";
            this.clDMaxVer.Width = 76;
            // 
            // clDComment
            // 
            this.clDComment.Text = "Comment";
            this.clDComment.Width = 179;
            // 
            // clDRegex
            // 
            this.clDRegex.Text = "Regex";
            // 
            // tbpINIEdit
            // 
            this.tbpINIEdit.Controls.Add(this.lvINIEdit);
            this.tbpINIEdit.Location = new System.Drawing.Point(4, 22);
            this.tbpINIEdit.Name = "tbpINIEdit";
            this.tbpINIEdit.Size = new System.Drawing.Size(621, 242);
            this.tbpINIEdit.TabIndex = 8;
            this.tbpINIEdit.Text = "INI Edits";
            this.tbpINIEdit.UseVisualStyleBackColor = true;
            // 
            // lvINIEdit
            // 
            this.lvINIEdit.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clSection,
            this.clKey,
            this.clNewValue});
            this.lvINIEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvINIEdit.Location = new System.Drawing.Point(0, 0);
            this.lvINIEdit.Name = "lvINIEdit";
            this.lvINIEdit.Size = new System.Drawing.Size(621, 242);
            this.lvINIEdit.TabIndex = 0;
            this.lvINIEdit.UseCompatibleStateImageBehavior = false;
            this.lvINIEdit.View = System.Windows.Forms.View.Details;
            // 
            // clSection
            // 
            this.clSection.Text = "Section";
            this.clSection.Width = 163;
            // 
            // clKey
            // 
            this.clKey.Text = "Key";
            this.clKey.Width = 185;
            // 
            // clNewValue
            // 
            this.clNewValue.Text = "New Value";
            this.clNewValue.Width = 201;
            // 
            // tbpSDP
            // 
            this.tbpSDP.Controls.Add(this.lvSDPEdit);
            this.tbpSDP.Location = new System.Drawing.Point(4, 22);
            this.tbpSDP.Name = "tbpSDP";
            this.tbpSDP.Size = new System.Drawing.Size(621, 242);
            this.tbpSDP.TabIndex = 9;
            this.tbpSDP.Text = "Shader Package Edits";
            this.tbpSDP.UseVisualStyleBackColor = true;
            // 
            // lvSDPEdit
            // 
            this.lvSDPEdit.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clPackage,
            this.clShader,
            this.clBin});
            this.lvSDPEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSDPEdit.Location = new System.Drawing.Point(0, 0);
            this.lvSDPEdit.Name = "lvSDPEdit";
            this.lvSDPEdit.Size = new System.Drawing.Size(621, 242);
            this.lvSDPEdit.TabIndex = 1;
            this.lvSDPEdit.UseCompatibleStateImageBehavior = false;
            this.lvSDPEdit.View = System.Windows.Forms.View.Details;
            // 
            // clPackage
            // 
            this.clPackage.Text = "Package";
            this.clPackage.Width = 163;
            // 
            // clShader
            // 
            this.clShader.Text = "Shader";
            this.clShader.Width = 185;
            // 
            // clBin
            // 
            this.clBin.Text = "Binary";
            this.clBin.Width = 201;
            // 
            // tbpESPEdits
            // 
            this.tbpESPEdits.Controls.Add(this.lvESPEdit);
            this.tbpESPEdits.Location = new System.Drawing.Point(4, 22);
            this.tbpESPEdits.Name = "tbpESPEdits";
            this.tbpESPEdits.Size = new System.Drawing.Size(621, 242);
            this.tbpESPEdits.TabIndex = 10;
            this.tbpESPEdits.Text = "ESP Edits";
            this.tbpESPEdits.UseVisualStyleBackColor = true;
            // 
            // lvESPEdit
            // 
            this.lvESPEdit.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clESPPlugin,
            this.clESPType,
            this.clESPEDID,
            this.clValue});
            this.lvESPEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvESPEdit.Location = new System.Drawing.Point(0, 0);
            this.lvESPEdit.Name = "lvESPEdit";
            this.lvESPEdit.Size = new System.Drawing.Size(621, 242);
            this.lvESPEdit.TabIndex = 0;
            this.lvESPEdit.UseCompatibleStateImageBehavior = false;
            this.lvESPEdit.View = System.Windows.Forms.View.Details;
            // 
            // clESPPlugin
            // 
            this.clESPPlugin.Text = "Plugin";
            this.clESPPlugin.Width = 222;
            // 
            // clESPType
            // 
            this.clESPType.Text = "Type";
            this.clESPType.Width = 105;
            // 
            // clESPEDID
            // 
            this.clESPEDID.Text = "EDID";
            this.clESPEDID.Width = 99;
            // 
            // clValue
            // 
            this.clValue.Text = "Value";
            this.clValue.Width = 148;
            // 
            // tbpBSAReg
            // 
            this.tbpBSAReg.Controls.Add(this.lvBSAReg);
            this.tbpBSAReg.Location = new System.Drawing.Point(4, 22);
            this.tbpBSAReg.Name = "tbpBSAReg";
            this.tbpBSAReg.Size = new System.Drawing.Size(621, 242);
            this.tbpBSAReg.TabIndex = 3;
            this.tbpBSAReg.Text = "BSA Registers";
            this.tbpBSAReg.UseVisualStyleBackColor = true;
            // 
            // lvBSAReg
            // 
            this.lvBSAReg.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clBSAFile});
            this.lvBSAReg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvBSAReg.Location = new System.Drawing.Point(0, 0);
            this.lvBSAReg.Name = "lvBSAReg";
            this.lvBSAReg.Size = new System.Drawing.Size(621, 242);
            this.lvBSAReg.TabIndex = 0;
            this.lvBSAReg.UseCompatibleStateImageBehavior = false;
            this.lvBSAReg.View = System.Windows.Forms.View.Details;
            // 
            // clBSAFile
            // 
            this.clBSAFile.Text = "BSA File";
            this.clBSAFile.Width = 306;
            // 
            // tbpESPWarnings
            // 
            this.tbpESPWarnings.Controls.Add(this.lvESPWarn);
            this.tbpESPWarnings.Location = new System.Drawing.Point(4, 22);
            this.tbpESPWarnings.Name = "tbpESPWarnings";
            this.tbpESPWarnings.Size = new System.Drawing.Size(621, 242);
            this.tbpESPWarnings.TabIndex = 5;
            this.tbpESPWarnings.Text = "ESP Deactivation Warnings";
            this.tbpESPWarnings.UseVisualStyleBackColor = true;
            // 
            // lvESPWarn
            // 
            this.lvESPWarn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clPluginWarn,
            this.clWarning});
            this.lvESPWarn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvESPWarn.Location = new System.Drawing.Point(0, 0);
            this.lvESPWarn.Name = "lvESPWarn";
            this.lvESPWarn.Size = new System.Drawing.Size(621, 242);
            this.lvESPWarn.TabIndex = 1;
            this.lvESPWarn.UseCompatibleStateImageBehavior = false;
            this.lvESPWarn.View = System.Windows.Forms.View.Details;
            // 
            // clPluginWarn
            // 
            this.clPluginWarn.Text = "Plugin";
            this.clPluginWarn.Width = 347;
            // 
            // clWarning
            // 
            this.clWarning.Text = "Warning";
            this.clWarning.Width = 136;
            // 
            // tbpDeacESP
            // 
            this.tbpDeacESP.Controls.Add(this.lvESPUnchecked);
            this.tbpDeacESP.Location = new System.Drawing.Point(4, 22);
            this.tbpDeacESP.Name = "tbpDeacESP";
            this.tbpDeacESP.Size = new System.Drawing.Size(621, 242);
            this.tbpDeacESP.TabIndex = 4;
            this.tbpDeacESP.Text = "Unchecked Plugins";
            this.tbpDeacESP.UseVisualStyleBackColor = true;
            // 
            // lvESPUnchecked
            // 
            this.lvESPUnchecked.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clESPUnchecked});
            this.lvESPUnchecked.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvESPUnchecked.Location = new System.Drawing.Point(0, 0);
            this.lvESPUnchecked.Name = "lvESPUnchecked";
            this.lvESPUnchecked.Size = new System.Drawing.Size(621, 242);
            this.lvESPUnchecked.TabIndex = 0;
            this.lvESPUnchecked.UseCompatibleStateImageBehavior = false;
            this.lvESPUnchecked.View = System.Windows.Forms.View.Details;
            // 
            // clESPUnchecked
            // 
            this.clESPUnchecked.Text = "Plugin";
            this.clESPUnchecked.Width = 576;
            // 
            // SimResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 734);
            this.Controls.Add(this.splitContainer4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SimResults";
            this.Text = "Simulation Results";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.grpDataFiles.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.ResumeLayout(false);
            this.grpFlags.ResumeLayout(false);
            this.grpFlags.PerformLayout();
            this.tbEdits.ResumeLayout(false);
            this.tbpLoadOrder.ResumeLayout(false);
            this.tbpCopyDF.ResumeLayout(false);
            this.tbpCopyESP.ResumeLayout(false);
            this.tbpConflicts.ResumeLayout(false);
            this.tbpDepends.ResumeLayout(false);
            this.tbpINIEdit.ResumeLayout(false);
            this.tbpSDP.ResumeLayout(false);
            this.tbpESPEdits.ResumeLayout(false);
            this.tbpBSAReg.ResumeLayout(false);
            this.tbpESPWarnings.ResumeLayout(false);
            this.tbpDeacESP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grpDataFiles;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListBox lstDFInstalled;
        private System.Windows.Forms.ListBox lstDFIgnored;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox lstPInstalled;
        private System.Windows.Forms.ListBox lstPIgnored;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.GroupBox grpFlags;
        private System.Windows.Forms.CheckBox chkPlugins;
        private System.Windows.Forms.CheckBox chkCanceled;
        private System.Windows.Forms.CheckBox chkDataFiles;
        private System.Windows.Forms.TabControl tbEdits;
        private System.Windows.Forms.TabPage tbpLoadOrder;
        private System.Windows.Forms.TabPage tbpConflicts;
        private System.Windows.Forms.TabPage tbpDepends;
        private System.Windows.Forms.TabPage tbpBSAReg;
        private System.Windows.Forms.TabPage tbpDeacESP;
        private System.Windows.Forms.TabPage tbpESPWarnings;
        private System.Windows.Forms.TabPage tbpCopyDF;
        private System.Windows.Forms.TabPage tbpCopyESP;
        private System.Windows.Forms.TabPage tbpINIEdit;
        private System.Windows.Forms.TabPage tbpSDP;
        private System.Windows.Forms.TabPage tbpESPEdits;
        private System.Windows.Forms.ListView lvLoadOrder;
        private System.Windows.Forms.ColumnHeader clPlugin;
        private System.Windows.Forms.ColumnHeader clTarget;
        private System.Windows.Forms.ColumnHeader clLoadAfter;
        private System.Windows.Forms.ListView lvCopiedDF;
        private System.Windows.Forms.ColumnHeader clCopyFrom;
        private System.Windows.Forms.ColumnHeader clCopyTo;
        private System.Windows.Forms.ListView lvCopiedPF;
        private System.Windows.Forms.ColumnHeader clPPlugin;
        private System.Windows.Forms.ColumnHeader clPCopyTo;
        private System.Windows.Forms.ListView lvConflicts;
        private System.Windows.Forms.ColumnHeader clConflicts;
        private System.Windows.Forms.ColumnHeader clCMinVer;
        private System.Windows.Forms.ColumnHeader clCMaxVer;
        private System.Windows.Forms.ColumnHeader clCLevel;
        private System.Windows.Forms.ColumnHeader clCComment;
        private System.Windows.Forms.ColumnHeader clCRegex;
        private System.Windows.Forms.ListView lvDepends;
        private System.Windows.Forms.ColumnHeader clDepends;
        private System.Windows.Forms.ColumnHeader clDMinVer;
        private System.Windows.Forms.ColumnHeader clDMaxVer;
        private System.Windows.Forms.ColumnHeader clDComment;
        private System.Windows.Forms.ColumnHeader clDRegex;
        private System.Windows.Forms.ListView lvINIEdit;
        private System.Windows.Forms.ColumnHeader clSection;
        private System.Windows.Forms.ColumnHeader clKey;
        private System.Windows.Forms.ColumnHeader clNewValue;
        private System.Windows.Forms.ListView lvSDPEdit;
        private System.Windows.Forms.ColumnHeader clPackage;
        private System.Windows.Forms.ColumnHeader clShader;
        private System.Windows.Forms.ColumnHeader clBin;
        private System.Windows.Forms.ListView lvESPEdit;
        private System.Windows.Forms.ColumnHeader clESPPlugin;
        private System.Windows.Forms.ColumnHeader clESPType;
        private System.Windows.Forms.ColumnHeader clESPEDID;
        private System.Windows.Forms.ColumnHeader clValue;
        private System.Windows.Forms.ListView lvBSAReg;
        private System.Windows.Forms.ColumnHeader clBSAFile;
        private System.Windows.Forms.ListView lvESPWarn;
        private System.Windows.Forms.ColumnHeader clPluginWarn;
        private System.Windows.Forms.ColumnHeader clWarning;
        private System.Windows.Forms.ListView lvESPUnchecked;
        private System.Windows.Forms.ColumnHeader clESPUnchecked;
        private System.Windows.Forms.Label lblDataInstalled;
        private System.Windows.Forms.Label lblDataIgnore;
        private System.Windows.Forms.Label lblPlugInstall;
        private System.Windows.Forms.Label lblPlugIgnore;

    }
}