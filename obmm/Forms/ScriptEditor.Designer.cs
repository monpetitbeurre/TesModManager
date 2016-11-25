namespace OblivionModManager.Forms {
    partial class ScriptEditor {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptEditor));
            this.ToolBar = new System.Windows.Forms.ToolStrip();
            this.tsbSaveExit = new System.Windows.Forms.ToolStripButton();
            this.bOpen = new System.Windows.Forms.ToolStripButton();
            this.bSave = new System.Windows.Forms.ToolStripButton();
            this.tsbPrefabs = new System.Windows.Forms.ToolStripButton();
            this.tsScriptType = new System.Windows.Forms.ToolStripDropDownButton();
            this.obmmScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ironpythonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bFind = new System.Windows.Forms.ToolStripButton();
            this.bFindNext = new System.Windows.Forms.ToolStripButton();
            this.bCheckForErrors = new System.Windows.Forms.ToolStripButton();
            this.bSimulate = new System.Windows.Forms.ToolStripButton();
            this.bHelp = new System.Windows.Forms.ToolStripButton();
            this.OpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.scriptContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.teEdit = new ICSharpCode.TextEditor.TextEditorControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbErrors = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.tbpFiles = new System.Windows.Forms.TabPage();
            this.txtFiles = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.xmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolBar.SuspendLayout();
            this.scriptContextMenu.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tbpFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolBar
            // 
            this.ToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSaveExit,
            this.bOpen,
            this.bSave,
            this.tsbPrefabs,
            this.tsScriptType,
            this.bFind,
            this.bFindNext,
            this.bCheckForErrors,
            this.bSimulate,
            this.bHelp});
            this.ToolBar.Location = new System.Drawing.Point(0, 0);
            this.ToolBar.Name = "ToolBar";
            this.ToolBar.Size = new System.Drawing.Size(527, 25);
            this.ToolBar.TabIndex = 2;
            // 
            // tsbSaveExit
            // 
            this.tsbSaveExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveExit.Image")));
            this.tsbSaveExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveExit.Name = "tsbSaveExit";
            this.tsbSaveExit.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveExit.Text = "Save and Exit";
            this.tsbSaveExit.Click += new System.EventHandler(this.TsbSaveExitClick);
            // 
            // bOpen
            // 
            this.bOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bOpen.Image = ((System.Drawing.Image)(resources.GetObject("bOpen.Image")));
            this.bOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(23, 22);
            this.bOpen.Text = "Open file";
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // bSave
            // 
            this.bSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bSave.Image = ((System.Drawing.Image)(resources.GetObject("bSave.Image")));
            this.bSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(23, 22);
            this.bSave.Text = "Save file";
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // tsbPrefabs
            // 
            this.tsbPrefabs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrefabs.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrefabs.Image")));
            this.tsbPrefabs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrefabs.Name = "tsbPrefabs";
            this.tsbPrefabs.Size = new System.Drawing.Size(23, 22);
            this.tsbPrefabs.Text = "Prefabs";
            this.tsbPrefabs.Click += new System.EventHandler(this.TsbPrefabsClick);
            // 
            // tsScriptType
            // 
            this.tsScriptType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsScriptType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.obmmScriptToolStripMenuItem,
            this.ironpythonToolStripMenuItem,
            this.cToolStripMenuItem,
            this.vBToolStripMenuItem,
            this.xmlToolStripMenuItem});
            this.tsScriptType.Image = ((System.Drawing.Image)(resources.GetObject("tsScriptType.Image")));
            this.tsScriptType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsScriptType.Name = "tsScriptType";
            this.tsScriptType.Size = new System.Drawing.Size(79, 22);
            this.tsScriptType.Text = "Script Type";
            // 
            // obmmScriptToolStripMenuItem
            // 
            this.obmmScriptToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.obmmScriptToolStripMenuItem.Name = "obmmScriptToolStripMenuItem";
            this.obmmScriptToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.obmmScriptToolStripMenuItem.Text = "OBMM";
            this.obmmScriptToolStripMenuItem.Click += new System.EventHandler(this.ScriptTypeChanged);
            // 
            // ironpythonToolStripMenuItem
            // 
            this.ironpythonToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ironpythonToolStripMenuItem.Name = "ironpythonToolStripMenuItem";
            this.ironpythonToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ironpythonToolStripMenuItem.Text = "IronPython";
            this.ironpythonToolStripMenuItem.Click += new System.EventHandler(this.ScriptTypeChanged);
            // 
            // cToolStripMenuItem
            // 
            this.cToolStripMenuItem.BackColor = System.Drawing.Color.Lime;
            this.cToolStripMenuItem.Name = "cToolStripMenuItem";
            this.cToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cToolStripMenuItem.Text = "C#";
            this.cToolStripMenuItem.Click += new System.EventHandler(this.ScriptTypeChanged);
            // 
            // vBToolStripMenuItem
            // 
            this.vBToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.vBToolStripMenuItem.Name = "vBToolStripMenuItem";
            this.vBToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.vBToolStripMenuItem.Text = "VB";
            this.vBToolStripMenuItem.Click += new System.EventHandler(this.ScriptTypeChanged);
            // 
            // bFind
            // 
            this.bFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bFind.Name = "bFind";
            this.bFind.Size = new System.Drawing.Size(46, 22);
            this.bFind.Text = "Search";
            this.bFind.Click += new System.EventHandler(this.bFind_Click);
            // 
            // bFindNext
            // 
            this.bFindNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bFindNext.Enabled = false;
            this.bFindNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bFindNext.Name = "bFindNext";
            this.bFindNext.Size = new System.Drawing.Size(59, 22);
            this.bFindNext.Text = "Find next";
            this.bFindNext.Click += new System.EventHandler(this.bFindNext_Click);
            // 
            // bCheckForErrors
            // 
            this.bCheckForErrors.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bCheckForErrors.Image = ((System.Drawing.Image)(resources.GetObject("bCheckForErrors.Image")));
            this.bCheckForErrors.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bCheckForErrors.Name = "bCheckForErrors";
            this.bCheckForErrors.Size = new System.Drawing.Size(115, 22);
            this.bCheckForErrors.Text = "Check Flow Control";
            this.bCheckForErrors.Click += new System.EventHandler(this.bCheckForErrors_Click);
            // 
            // bSimulate
            // 
            this.bSimulate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bSimulate.Image = ((System.Drawing.Image)(resources.GetObject("bSimulate.Image")));
            this.bSimulate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bSimulate.Name = "bSimulate";
            this.bSimulate.Size = new System.Drawing.Size(57, 22);
            this.bSimulate.Text = "Simulate";
            this.bSimulate.Click += new System.EventHandler(this.bSimulate_Click);
            // 
            // bHelp
            // 
            this.bHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bHelp.Image = ((System.Drawing.Image)(resources.GetObject("bHelp.Image")));
            this.bHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bHelp.Name = "bHelp";
            this.bHelp.Size = new System.Drawing.Size(36, 22);
            this.bHelp.Text = "Help";
            this.bHelp.Click += new System.EventHandler(this.Help_Click);
            // 
            // OpenDialog
            // 
            this.OpenDialog.Filter = "text files (*.txt, *.rtf)|*.txt;*.rtf|All files|*.*";
            this.OpenDialog.RestoreDirectory = true;
            this.OpenDialog.Title = "Choose file to import";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Rich text file (*.rtf)|*.rtf|Plain text (*.txt)|*.txt";
            this.saveFileDialog1.RestoreDirectory = true;
            this.saveFileDialog1.Title = "Save file as";
            // 
            // scriptContextMenu
            // 
            this.scriptContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.scriptContextMenu.Name = "scriptContextMenu";
            this.scriptContextMenu.Size = new System.Drawing.Size(121, 136);
            this.scriptContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.scriptContextMenu_Opening);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.selectAllToolStripMenuItem.Text = "Select all";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // teEdit
            // 
            this.teEdit.ContextMenuStrip = this.scriptContextMenu;
            this.teEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.teEdit.Location = new System.Drawing.Point(0, 0);
            this.teEdit.Name = "teEdit";
            this.teEdit.ShowEOLMarkers = true;
            this.teEdit.ShowInvalidLines = false;
            this.teEdit.ShowMatchingBracket = false;
            this.teEdit.ShowSpaces = true;
            this.teEdit.ShowTabs = true;
            this.teEdit.ShowVRuler = true;
            this.teEdit.Size = new System.Drawing.Size(527, 308);
            this.teEdit.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tbpFiles);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(527, 62);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbErrors);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(519, 36);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Errors";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbErrors
            // 
            this.tbErrors.BackColor = System.Drawing.SystemColors.Window;
            this.tbErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbErrors.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbErrors.Location = new System.Drawing.Point(3, 3);
            this.tbErrors.Multiline = true;
            this.tbErrors.Name = "tbErrors";
            this.tbErrors.ReadOnly = true;
            this.tbErrors.Size = new System.Drawing.Size(513, 30);
            this.tbErrors.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbOutput);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(519, 36);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Output";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbOutput
            // 
            this.tbOutput.BackColor = System.Drawing.SystemColors.Window;
            this.tbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOutput.Location = new System.Drawing.Point(3, 3);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.Size = new System.Drawing.Size(513, 30);
            this.tbOutput.TabIndex = 0;
            // 
            // tbpFiles
            // 
            this.tbpFiles.Controls.Add(this.txtFiles);
            this.tbpFiles.Location = new System.Drawing.Point(4, 4);
            this.tbpFiles.Name = "tbpFiles";
            this.tbpFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tbpFiles.Size = new System.Drawing.Size(519, 36);
            this.tbpFiles.TabIndex = 2;
            this.tbpFiles.Text = "Files";
            this.tbpFiles.UseVisualStyleBackColor = true;
            // 
            // txtFiles
            // 
            this.txtFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFiles.Location = new System.Drawing.Point(3, 3);
            this.txtFiles.Multiline = true;
            this.txtFiles.Name = "txtFiles";
            this.txtFiles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFiles.Size = new System.Drawing.Size(513, 30);
            this.txtFiles.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.teEdit);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(527, 374);
            this.splitContainer1.SplitterDistance = 308;
            this.splitContainer1.TabIndex = 5;
            // 
            // xmlToolStripMenuItem
            // 
            this.xmlToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.xmlToolStripMenuItem.Name = "xmlToolStripMenuItem";
            this.xmlToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.xmlToolStripMenuItem.Text = "XML";
            // 
            // ScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 399);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ToolBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "ScriptEditor";
            this.Text = "Script Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScriptEditor_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScriptEditor_KeyDown);
            this.ToolBar.ResumeLayout(false);
            this.ToolBar.PerformLayout();
            this.scriptContextMenu.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tbpFiles.ResumeLayout(false);
            this.tbpFiles.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.ToolStripButton tsbSaveExit;
        private System.Windows.Forms.TextBox txtFiles;
        private System.Windows.Forms.TabPage tbpFiles;
        private System.Windows.Forms.ToolStripButton tsbPrefabs;

        #endregion

        private System.Windows.Forms.ToolStrip ToolBar;
        private System.Windows.Forms.ToolStripButton bOpen;
        private System.Windows.Forms.ToolStripButton bSave;
        private System.Windows.Forms.OpenFileDialog OpenDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private ICSharpCode.TextEditor.TextEditorControl teEdit;
        private System.Windows.Forms.ToolStripButton bFind;
        private System.Windows.Forms.ToolStripButton bFindNext;
        private System.Windows.Forms.ToolStripButton bHelp;
        private System.Windows.Forms.ToolStripButton bCheckForErrors;
        private System.Windows.Forms.ContextMenuStrip scriptContextMenu;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton tsScriptType;
        private System.Windows.Forms.ToolStripMenuItem obmmScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ironpythonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vBToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox tbErrors;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton bSimulate;
        private System.Windows.Forms.ToolStripMenuItem xmlToolStripMenuItem;

    }
}