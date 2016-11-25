
namespace OblivionModManager
{
	partial class ConflictedFilePickerForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConflictedFilePickerForm));
            this.chklMods = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lbMods = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOpenFileInExternalViewer = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblImageResolution = new System.Windows.Forms.Label();
            this.lblFileSize = new System.Windows.Forms.Label();
            this.lblFilename = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblToDo = new System.Windows.Forms.Label();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerLists = new System.Windows.Forms.SplitContainer();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.btnOverwriteAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLists)).BeginInit();
            this.splitContainerLists.Panel1.SuspendLayout();
            this.splitContainerLists.Panel2.SuspendLayout();
            this.splitContainerLists.SuspendLayout();
            this.SuspendLayout();
            // 
            // chklMods
            // 
            this.chklMods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chklMods.FormattingEnabled = true;
            this.chklMods.HorizontalScrollbar = true;
            this.chklMods.Location = new System.Drawing.Point(0, 0);
            this.chklMods.Name = "chklMods";
            this.chklMods.Size = new System.Drawing.Size(200, 413);
            this.chklMods.TabIndex = 0;
            this.chklMods.SelectedIndexChanged += new System.EventHandler(this.chklMods_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(679, 442);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOKClick);
            // 
            // lbMods
            // 
            this.lbMods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMods.FormattingEnabled = true;
            this.lbMods.HorizontalScrollbar = true;
            this.lbMods.Location = new System.Drawing.Point(0, 0);
            this.lbMods.Name = "lbMods";
            this.lbMods.Size = new System.Drawing.Size(196, 413);
            this.lbMods.TabIndex = 5;
            this.lbMods.SelectedIndexChanged += new System.EventHandler(this.lbMods_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.textBox);
            this.panel1.Controls.Add(this.btnOpenFileInExternalViewer);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblImageResolution);
            this.panel1.Controls.Add(this.lblFileSize);
            this.panel1.Controls.Add(this.lblFilename);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(379, 410);
            this.panel1.TabIndex = 6;
            // 
            // btnOpenFileInExternalViewer
            // 
            this.btnOpenFileInExternalViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFileInExternalViewer.Location = new System.Drawing.Point(160, 24);
            this.btnOpenFileInExternalViewer.Name = "btnOpenFileInExternalViewer";
            this.btnOpenFileInExternalViewer.Size = new System.Drawing.Size(191, 24);
            this.btnOpenFileInExternalViewer.TabIndex = 4;
            this.btnOpenFileInExternalViewer.Text = "Open file in external viewer";
            this.btnOpenFileInExternalViewer.UseVisualStyleBackColor = true;
            this.btnOpenFileInExternalViewer.Visible = false;
            this.btnOpenFileInExternalViewer.Click += new System.EventHandler(this.btnOpenFileInExternalViewer_Click);
            this.btnOpenFileInExternalViewer.MouseHover += new System.EventHandler(this.btnOpenFileInExternalViewer_MouseHover);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(4, 88);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(350, 319);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // lblImageResolution
            // 
            this.lblImageResolution.AutoSize = true;
            this.lblImageResolution.Location = new System.Drawing.Point(7, 55);
            this.lblImageResolution.Name = "lblImageResolution";
            this.lblImageResolution.Size = new System.Drawing.Size(87, 13);
            this.lblImageResolution.TabIndex = 2;
            this.lblImageResolution.Text = "Image resolution:";
            // 
            // lblFileSize
            // 
            this.lblFileSize.AutoSize = true;
            this.lblFileSize.Location = new System.Drawing.Point(7, 30);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.Size = new System.Drawing.Size(47, 13);
            this.lblFileSize.TabIndex = 1;
            this.lblFileSize.Text = "File size:";
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSize = true;
            this.lblFilename.Location = new System.Drawing.Point(7, 9);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(52, 13);
            this.lblFilename.TabIndex = 0;
            this.lblFilename.Text = "Filename:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(598, 442);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblToDo
            // 
            this.lblToDo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblToDo.AutoSize = true;
            this.lblToDo.Location = new System.Drawing.Point(12, 428);
            this.lblToDo.Name = "lblToDo";
            this.lblToDo.Size = new System.Drawing.Size(497, 13);
            this.lblToDo.TabIndex = 8;
            this.lblToDo.Text = "Select the file that will be installed for each conflict by selecting the proper " +
    "mod name in the center panel";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMain.Location = new System.Drawing.Point(-1, 12);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerLists);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panel1);
            this.splitContainerMain.Size = new System.Drawing.Size(758, 413);
            this.splitContainerMain.SplitterDistance = 400;
            this.splitContainerMain.TabIndex = 9;
            // 
            // splitContainerLists
            // 
            this.splitContainerLists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLists.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLists.Name = "splitContainerLists";
            // 
            // splitContainerLists.Panel1
            // 
            this.splitContainerLists.Panel1.Controls.Add(this.chklMods);
            // 
            // splitContainerLists.Panel2
            // 
            this.splitContainerLists.Panel2.Controls.Add(this.lbMods);
            this.splitContainerLists.Size = new System.Drawing.Size(400, 413);
            this.splitContainerLists.SplitterDistance = 200;
            this.splitContainerLists.TabIndex = 10;
            // 
            // btnOverwriteAll
            // 
            this.btnOverwriteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOverwriteAll.Location = new System.Drawing.Point(127, 445);
            this.btnOverwriteAll.Name = "btnOverwriteAll";
            this.btnOverwriteAll.Size = new System.Drawing.Size(224, 19);
            this.btnOverwriteAll.TabIndex = 10;
            this.btnOverwriteAll.Text = "Overwrite all with new mod files";
            this.btnOverwriteAll.UseVisualStyleBackColor = true;
            this.btnOverwriteAll.Click += new System.EventHandler(this.btnOverwriteAll_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 445);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = " or click on this button";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(6, 80);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(344, 326);
            this.textBox.TabIndex = 5;
            this.textBox.Visible = false;
            // 
            // ConflictedFilePickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 473);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOverwriteAll);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.lblToDo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConflictedFilePickerForm";
            this.Text = "Pick Files";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConflictedFilePickerForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerLists.Panel1.ResumeLayout(false);
            this.splitContainerLists.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLists)).EndInit();
            this.splitContainerLists.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ListBox chklMods;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblImageResolution;
        private System.Windows.Forms.Label lblFileSize;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.ListBox lbMods;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblToDo;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.SplitContainer splitContainerLists;
        private System.Windows.Forms.Button btnOpenFileInExternalViewer;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.Button btnOverwriteAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox;
	}
}
