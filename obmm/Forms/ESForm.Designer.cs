/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 27/09/2010
 * Time: 3:30 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OblivionModManager
{
	partial class ESForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ESForm));
            this.grpImport = new System.Windows.Forms.GroupBox();
            this.chkOCDLIst = new System.Windows.Forms.CheckBox();
            this.chkTESNexus = new System.Windows.Forms.CheckBox();
            this.chkOmod = new System.Windows.Forms.CheckBox();
            this.grpOMODList = new System.Windows.Forms.GroupBox();
            this.chkShowNames = new System.Windows.Forms.CheckBox();
            this.chkIncludeVersion = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpImport.SuspendLayout();
            this.grpOMODList.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpImport
            // 
            this.grpImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpImport.Controls.Add(this.chkOCDLIst);
            this.grpImport.Controls.Add(this.chkTESNexus);
            this.grpImport.Controls.Add(this.chkOmod);
            this.grpImport.Location = new System.Drawing.Point(13, 13);
            this.grpImport.Name = "grpImport";
            this.grpImport.Size = new System.Drawing.Size(265, 116);
            this.grpImport.TabIndex = 0;
            this.grpImport.TabStop = false;
            this.grpImport.Text = "Always Import";
            // 
            // chkOCDLIst
            // 
            this.chkOCDLIst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOCDLIst.Location = new System.Drawing.Point(7, 82);
            this.chkOCDLIst.Name = "chkOCDLIst";
            this.chkOCDLIst.Size = new System.Drawing.Size(252, 24);
            this.chkOCDLIst.TabIndex = 2;
            this.chkOCDLIst.Text = "OCD &List";
            this.chkOCDLIst.UseVisualStyleBackColor = true;
            // 
            // chkTESNexus
            // 
            this.chkTESNexus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTESNexus.Location = new System.Drawing.Point(7, 51);
            this.chkTESNexus.Name = "chkTESNexus";
            this.chkTESNexus.Size = new System.Drawing.Size(252, 24);
            this.chkTESNexus.TabIndex = 1;
            this.chkTESNexus.Text = "&TESNexus";
            this.chkTESNexus.UseVisualStyleBackColor = true;
            // 
            // chkOmod
            // 
            this.chkOmod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOmod.Location = new System.Drawing.Point(7, 20);
            this.chkOmod.Name = "chkOmod";
            this.chkOmod.Size = new System.Drawing.Size(252, 24);
            this.chkOmod.TabIndex = 0;
            this.chkOmod.Text = "OMOD &Conversion Data";
            this.chkOmod.UseVisualStyleBackColor = true;
            this.chkOmod.CheckedChanged += new System.EventHandler(this.chkOmod_CheckedChanged);
            // 
            // grpOMODList
            // 
            this.grpOMODList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOMODList.Controls.Add(this.chkShowNames);
            this.grpOMODList.Controls.Add(this.chkIncludeVersion);
            this.grpOMODList.Location = new System.Drawing.Point(13, 135);
            this.grpOMODList.Name = "grpOMODList";
            this.grpOMODList.Size = new System.Drawing.Size(265, 86);
            this.grpOMODList.TabIndex = 1;
            this.grpOMODList.TabStop = false;
            this.grpOMODList.Text = "Mod List (Requires restart)";
            // 
            // chkShowNames
            // 
            this.chkShowNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowNames.Location = new System.Drawing.Point(7, 49);
            this.chkShowNames.Name = "chkShowNames";
            this.chkShowNames.Size = new System.Drawing.Size(252, 24);
            this.chkShowNames.TabIndex = 4;
            this.chkShowNames.Text = "Show OMOD &Names instead of Filenames";
            this.chkShowNames.UseVisualStyleBackColor = true;
            // 
            // chkIncludeVersion
            // 
            this.chkIncludeVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIncludeVersion.Location = new System.Drawing.Point(7, 19);
            this.chkIncludeVersion.Name = "chkIncludeVersion";
            this.chkIncludeVersion.Size = new System.Drawing.Size(252, 24);
            this.chkIncludeVersion.TabIndex = 3;
            this.chkIncludeVersion.Text = "Include &Version Number when creating mod";
            this.chkIncludeVersion.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(203, 268);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOKClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(122, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "C&ancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // ESForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(290, 303);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpOMODList);
            this.Controls.Add(this.grpImport);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ESForm";
            this.Text = "Extended Settings";
            this.Load += new System.EventHandler(this.ESFormLoad);
            this.grpImport.ResumeLayout(false);
            this.grpOMODList.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.GroupBox grpOMODList;
		private System.Windows.Forms.CheckBox chkIncludeVersion;
		private System.Windows.Forms.CheckBox chkShowNames;
		private System.Windows.Forms.CheckBox chkOmod;
		private System.Windows.Forms.CheckBox chkTESNexus;
		private System.Windows.Forms.CheckBox chkOCDLIst;
		private System.Windows.Forms.GroupBox grpImport;
	}
}
