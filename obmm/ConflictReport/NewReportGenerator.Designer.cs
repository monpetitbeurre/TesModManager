namespace OblivionModManager.ConflictReport {
    partial class NewReportGenerator {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private static bool bCancelled = false;
        private ProgressForm pf = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewReportGenerator));
            this.bActive = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.bCancel = new System.Windows.Forms.Button();
            this.cbEDIDs = new System.Windows.Forms.CheckBox();
            this.treeView = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbShowMain = new System.Windows.Forms.CheckBox();
            this.cbNonConflicting = new System.Windows.Forms.CheckBox();
            this.tbDesc = new System.Windows.Forms.TextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bActive
            // 
            this.bActive.Location = new System.Drawing.Point(12, 96);
            this.bActive.Name = "bActive";
            this.bActive.Size = new System.Drawing.Size(190, 23);
            this.bActive.TabIndex = 0;
            this.bActive.Text = "Test active mods";
            this.bActive.UseVisualStyleBackColor = true;
            this.bActive.Click += new System.EventHandler(this.bActive_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(11, 126);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(190, 23);
            this.bCancel.TabIndex = 4;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // cbEDIDs
            // 
            this.cbEDIDs.AutoSize = true;
            this.cbEDIDs.Checked = true;
            this.cbEDIDs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEDIDs.Location = new System.Drawing.Point(12, 12);
            this.cbEDIDs.Name = "cbEDIDs";
            this.cbEDIDs.Size = new System.Drawing.Size(81, 17);
            this.cbEDIDs.TabIndex = 5;
            this.cbEDIDs.Text = "read EDIDs";
            this.cbEDIDs.UseVisualStyleBackColor = true;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(213, 168);
            this.treeView.TabIndex = 6;
            this.treeView.Visible = false;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cbShowMain);
            this.splitContainer1.Panel1.Controls.Add(this.cbNonConflicting);
            this.splitContainer1.Panel1.Controls.Add(this.treeView);
            this.splitContainer1.Panel1.Controls.Add(this.cbEDIDs);
            this.splitContainer1.Panel1.Controls.Add(this.bActive);
            this.splitContainer1.Panel1.Controls.Add(this.bCancel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbDesc);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(213, 168);
            this.splitContainer1.SplitterDistance = 109;
            this.splitContainer1.TabIndex = 7;
            // 
            // cbShowMain
            // 
            this.cbShowMain.AutoSize = true;
            this.cbShowMain.Location = new System.Drawing.Point(12, 35);
            this.cbShowMain.Name = "cbShowMain";
            this.cbShowMain.Size = new System.Drawing.Size(152, 17);
            this.cbShowMain.TabIndex = 8;
            this.cbShowMain.Text = "Include main esm in results";
            this.cbShowMain.UseVisualStyleBackColor = true;
            // 
            // cbNonConflicting
            // 
            this.cbNonConflicting.AutoSize = true;
            this.cbNonConflicting.Location = new System.Drawing.Point(12, 58);
            this.cbNonConflicting.Name = "cbNonConflicting";
            this.cbNonConflicting.Size = new System.Drawing.Size(170, 17);
            this.cbNonConflicting.TabIndex = 7;
            this.cbNonConflicting.Text = "Display non conflicting records";
            this.cbNonConflicting.UseVisualStyleBackColor = true;
            // 
            // tbDesc
            // 
            this.tbDesc.BackColor = System.Drawing.SystemColors.Window;
            this.tbDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDesc.Location = new System.Drawing.Point(0, 0);
            this.tbDesc.Multiline = true;
            this.tbDesc.Name = "tbDesc";
            this.tbDesc.ReadOnly = true;
            this.tbDesc.Size = new System.Drawing.Size(96, 100);
            this.tbDesc.TabIndex = 0;
            // 
            // NewReportGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 168);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewReportGenerator";
            this.Text = "Conflict detector";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bActive;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.CheckBox cbEDIDs;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tbDesc;
        private System.Windows.Forms.CheckBox cbShowMain;
        private System.Windows.Forms.CheckBox cbNonConflicting;
    }
}