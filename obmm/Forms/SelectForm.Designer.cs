namespace OblivionModManager.Forms {
    partial class SelectForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectForm));
            this.lbSelect = new System.Windows.Forms.ListBox();
            this.bOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bPreview = new System.Windows.Forms.Button();
            this.bDescription = new System.Windows.Forms.Button();
            this.tbDesc = new System.Windows.Forms.RichTextBox();
            this.cbCreateInstallMenu = new System.Windows.Forms.CheckBox();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitTextContainer = new System.Windows.Forms.SplitContainer();
            this.dgSelect = new System.Windows.Forms.DataGridView();
            this.cbSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txtOptionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitTextContainer)).BeginInit();
            this.splitTextContainer.Panel1.SuspendLayout();
            this.splitTextContainer.Panel2.SuspendLayout();
            this.splitTextContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSelect)).BeginInit();
            this.SuspendLayout();
            // 
            // lbSelect
            // 
            this.lbSelect.FormattingEnabled = true;
            this.lbSelect.IntegralHeight = false;
            this.lbSelect.Location = new System.Drawing.Point(17, 31);
            this.lbSelect.Name = "lbSelect";
            this.lbSelect.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbSelect.Size = new System.Drawing.Size(300, 69);
            this.lbSelect.TabIndex = 0;
            this.lbSelect.Click += new System.EventHandler(this.lbSelect_Click);
            this.lbSelect.SelectedIndexChanged += new System.EventHandler(this.lbSelect_SelectedIndexChanged);
            this.lbSelect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbSelect_KeyDown);
            this.lbSelect.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbSelect_MouseDown);
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bOK.Location = new System.Drawing.Point(826, 459);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select one option";
            // 
            // bPreview
            // 
            this.bPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bPreview.Enabled = false;
            this.bPreview.Location = new System.Drawing.Point(93, 459);
            this.bPreview.Name = "bPreview";
            this.bPreview.Size = new System.Drawing.Size(75, 23);
            this.bPreview.TabIndex = 3;
            this.bPreview.Text = "Preview";
            this.bPreview.UseVisualStyleBackColor = true;
            this.bPreview.Visible = false;
            this.bPreview.Click += new System.EventHandler(this.bPreview_Click);
            // 
            // bDescription
            // 
            this.bDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bDescription.Enabled = false;
            this.bDescription.Location = new System.Drawing.Point(12, 459);
            this.bDescription.Name = "bDescription";
            this.bDescription.Size = new System.Drawing.Size(75, 23);
            this.bDescription.TabIndex = 4;
            this.bDescription.Text = "Description";
            this.bDescription.UseVisualStyleBackColor = true;
            this.bDescription.Visible = false;
            this.bDescription.Click += new System.EventHandler(this.bDescription_Click);
            // 
            // tbDesc
            // 
            this.tbDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbDesc.BackColor = System.Drawing.SystemColors.Window;
            this.tbDesc.DetectUrls = false;
            this.tbDesc.Location = new System.Drawing.Point(-199, 155);
            this.tbDesc.Name = "tbDesc";
            this.tbDesc.ReadOnly = true;
            this.tbDesc.Size = new System.Drawing.Size(184, 210);
            this.tbDesc.TabIndex = 5;
            this.tbDesc.Text = "";
            this.tbDesc.Visible = false;
            this.tbDesc.Click += new System.EventHandler(this.tbDesc_Click);
            this.tbDesc.TextChanged += new System.EventHandler(this.tbDesc_TextChanged);
            // 
            // cbCreateInstallMenu
            // 
            this.cbCreateInstallMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbCreateInstallMenu.AutoSize = true;
            this.cbCreateInstallMenu.Location = new System.Drawing.Point(12, 436);
            this.cbCreateInstallMenu.Name = "cbCreateInstallMenu";
            this.cbCreateInstallMenu.Size = new System.Drawing.Size(268, 17);
            this.cbCreateInstallMenu.TabIndex = 6;
            this.cbCreateInstallMenu.Text = "Ask to pick selected options at mod installation time";
            this.cbCreateInstallMenu.UseVisualStyleBackColor = true;
            this.cbCreateInstallMenu.Visible = false;
            // 
            // pbPreview
            // 
            this.pbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPreview.Enabled = false;
            this.pbPreview.Location = new System.Drawing.Point(0, 0);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(599, 377);
            this.pbPreview.TabIndex = 8;
            this.pbPreview.TabStop = false;
            this.pbPreview.Click += new System.EventHandler(this.pbPreview_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Enabled = false;
            this.txtDescription.Location = new System.Drawing.Point(0, 0);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDescription.Size = new System.Drawing.Size(286, 227);
            this.txtDescription.TabIndex = 9;
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(12, 34);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.splitTextContainer);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tbDesc);
            this.splitContainer.Panel2.Controls.Add(this.pbPreview);
            this.splitContainer.Size = new System.Drawing.Size(889, 377);
            this.splitContainer.SplitterDistance = 286;
            this.splitContainer.TabIndex = 10;
            this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // splitTextContainer
            // 
            this.splitTextContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTextContainer.Location = new System.Drawing.Point(0, 0);
            this.splitTextContainer.Name = "splitTextContainer";
            this.splitTextContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitTextContainer.Panel1
            // 
            this.splitTextContainer.Panel1.Controls.Add(this.dgSelect);
            this.splitTextContainer.Panel1.Controls.Add(this.lbSelect);
            // 
            // splitTextContainer.Panel2
            // 
            this.splitTextContainer.Panel2.Controls.Add(this.txtDescription);
            this.splitTextContainer.Size = new System.Drawing.Size(286, 377);
            this.splitTextContainer.SplitterDistance = 146;
            this.splitTextContainer.TabIndex = 0;
            // 
            // dgSelect
            // 
            this.dgSelect.AllowUserToAddRows = false;
            this.dgSelect.AllowUserToDeleteRows = false;
            this.dgSelect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSelect.ColumnHeadersVisible = false;
            this.dgSelect.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cbSelected,
            this.txtOptionName});
            this.dgSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSelect.Location = new System.Drawing.Point(0, 0);
            this.dgSelect.Name = "dgSelect";
            this.dgSelect.RowHeadersVisible = false;
            this.dgSelect.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSelect.Size = new System.Drawing.Size(286, 146);
            this.dgSelect.TabIndex = 1;
            this.dgSelect.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSelect_CellClick);
            this.dgSelect.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSelect_CellContentClick);
            this.dgSelect.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSelect_CellValueChanged);
            // 
            // cbSelected
            // 
            this.cbSelected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cbSelected.FillWeight = 20F;
            this.cbSelected.HeaderText = "cbSelected";
            this.cbSelected.Name = "cbSelected";
            this.cbSelected.Width = 40;
            // 
            // txtOptionName
            // 
            this.txtOptionName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.txtOptionName.HeaderText = "txtOptionName";
            this.txtOptionName.Name = "txtOptionName";
            this.txtOptionName.ReadOnly = true;
            this.txtOptionName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.Location = new System.Drawing.Point(745, 459);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 11;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Visible = false;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // SelectForm
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bOK;
            this.ClientSize = new System.Drawing.Size(913, 494);
            this.ControlBox = false;
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.cbCreateInstallMenu);
            this.Controls.Add(this.bDescription);
            this.Controls.Add(this.bPreview);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.splitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "SelectForm";
            this.Text = "Select Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.splitTextContainer.Panel1.ResumeLayout(false);
            this.splitTextContainer.Panel2.ResumeLayout(false);
            this.splitTextContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitTextContainer)).EndInit();
            this.splitTextContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSelect)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbSelect;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bPreview;
        private System.Windows.Forms.Button bDescription;
        private System.Windows.Forms.RichTextBox tbDesc;
        private System.Windows.Forms.CheckBox cbCreateInstallMenu;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.SplitContainer splitTextContainer;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.DataGridView dgSelect;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cbSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtOptionName;
    }
}