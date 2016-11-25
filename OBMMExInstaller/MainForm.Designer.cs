/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 23/07/2010
 * Time: 4:27 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OBMMExInstaller
{
	partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.txtOblivionPath = new System.Windows.Forms.TextBox();
            this.btnOblivionBrowse = new System.Windows.Forms.Button();
            this.fbdBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.btnInstall = new System.Windows.Forms.Button();
            this.chkDesktop = new System.Windows.Forms.CheckBox();
            this.chkStartMenu = new System.Windows.Forms.CheckBox();
            this.txtSkyrimPath = new System.Windows.Forms.TextBox();
            this.btnSkyrimBrowse = new System.Windows.Forms.Button();
            this.lblOblivionInstallDir = new System.Windows.Forms.Label();
            this.lblSkyrimInstallDir = new System.Windows.Forms.Label();
            this.cbInstallForOblivion = new System.Windows.Forms.CheckBox();
            this.cbInstallForSkyrim = new System.Windows.Forms.CheckBox();
            this.cbRegisterAsNexusDownloadMgr = new System.Windows.Forms.CheckBox();
            this.cbInstallForMorrowind = new System.Windows.Forms.CheckBox();
            this.lblMorrowindInstallDir = new System.Windows.Forms.Label();
            this.btnMorrowindBrowse = new System.Windows.Forms.Button();
            this.txtMorrowindPath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtOblivionPath
            // 
            this.txtOblivionPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOblivionPath.Enabled = false;
            this.txtOblivionPath.Location = new System.Drawing.Point(9, 25);
            this.txtOblivionPath.Name = "txtOblivionPath";
            this.txtOblivionPath.Size = new System.Drawing.Size(314, 20);
            this.txtOblivionPath.TabIndex = 0;
            // 
            // btnOblivionBrowse
            // 
            this.btnOblivionBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOblivionBrowse.Location = new System.Drawing.Point(332, 25);
            this.btnOblivionBrowse.Name = "btnOblivionBrowse";
            this.btnOblivionBrowse.Size = new System.Drawing.Size(28, 23);
            this.btnOblivionBrowse.TabIndex = 2;
            this.btnOblivionBrowse.Text = "...";
            this.btnOblivionBrowse.UseVisualStyleBackColor = true;
            this.btnOblivionBrowse.Click += new System.EventHandler(this.BtnOblivionBrowseClick);
            // 
            // fbdBrowse
            // 
            this.fbdBrowse.Description = "Select the directory the game is installed in";
            // 
            // btnInstall
            // 
            this.btnInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInstall.Location = new System.Drawing.Point(285, 201);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(75, 23);
            this.btnInstall.TabIndex = 3;
            this.btnInstall.Text = "&Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.BtnInstallClick);
            // 
            // chkDesktop
            // 
            this.chkDesktop.AutoSize = true;
            this.chkDesktop.Location = new System.Drawing.Point(9, 160);
            this.chkDesktop.Name = "chkDesktop";
            this.chkDesktop.Size = new System.Drawing.Size(109, 17);
            this.chkDesktop.TabIndex = 4;
            this.chkDesktop.Text = "&Desktop Shortcut";
            this.chkDesktop.UseVisualStyleBackColor = true;
            // 
            // chkStartMenu
            // 
            this.chkStartMenu.AutoSize = true;
            this.chkStartMenu.Location = new System.Drawing.Point(9, 181);
            this.chkStartMenu.Name = "chkStartMenu";
            this.chkStartMenu.Size = new System.Drawing.Size(121, 17);
            this.chkStartMenu.TabIndex = 5;
            this.chkStartMenu.Text = "&Start Menu Shortcut";
            this.chkStartMenu.UseVisualStyleBackColor = true;
            // 
            // txtSkyrimPath
            // 
            this.txtSkyrimPath.Enabled = false;
            this.txtSkyrimPath.Location = new System.Drawing.Point(9, 75);
            this.txtSkyrimPath.Name = "txtSkyrimPath";
            this.txtSkyrimPath.Size = new System.Drawing.Size(314, 20);
            this.txtSkyrimPath.TabIndex = 6;
            // 
            // btnSkyrimBrowse
            // 
            this.btnSkyrimBrowse.Location = new System.Drawing.Point(332, 75);
            this.btnSkyrimBrowse.Name = "btnSkyrimBrowse";
            this.btnSkyrimBrowse.Size = new System.Drawing.Size(28, 23);
            this.btnSkyrimBrowse.TabIndex = 7;
            this.btnSkyrimBrowse.Text = "...";
            this.btnSkyrimBrowse.UseVisualStyleBackColor = true;
            this.btnSkyrimBrowse.Click += new System.EventHandler(this.btnSkyrimBrowse_Click);
            // 
            // lblOblivionInstallDir
            // 
            this.lblOblivionInstallDir.AutoSize = true;
            this.lblOblivionInstallDir.Location = new System.Drawing.Point(180, 9);
            this.lblOblivionInstallDir.Name = "lblOblivionInstallDir";
            this.lblOblivionInstallDir.Size = new System.Drawing.Size(143, 13);
            this.lblOblivionInstallDir.TabIndex = 8;
            this.lblOblivionInstallDir.Text = "Oblivion installation directory:";
            // 
            // lblSkyrimInstallDir
            // 
            this.lblSkyrimInstallDir.AutoSize = true;
            this.lblSkyrimInstallDir.Location = new System.Drawing.Point(187, 59);
            this.lblSkyrimInstallDir.Name = "lblSkyrimInstallDir";
            this.lblSkyrimInstallDir.Size = new System.Drawing.Size(136, 13);
            this.lblSkyrimInstallDir.TabIndex = 9;
            this.lblSkyrimInstallDir.Text = "Skyrim installation directory:";
            // 
            // cbInstallForOblivion
            // 
            this.cbInstallForOblivion.AutoSize = true;
            this.cbInstallForOblivion.Location = new System.Drawing.Point(6, 7);
            this.cbInstallForOblivion.Name = "cbInstallForOblivion";
            this.cbInstallForOblivion.Size = new System.Drawing.Size(109, 17);
            this.cbInstallForOblivion.TabIndex = 10;
            this.cbInstallForOblivion.Text = "Install for Oblivion";
            this.cbInstallForOblivion.UseVisualStyleBackColor = true;
            this.cbInstallForOblivion.CheckedChanged += new System.EventHandler(this.cbInstallForOblivion_CheckedChanged);
            // 
            // cbInstallForSkyrim
            // 
            this.cbInstallForSkyrim.AutoSize = true;
            this.cbInstallForSkyrim.Location = new System.Drawing.Point(8, 58);
            this.cbInstallForSkyrim.Name = "cbInstallForSkyrim";
            this.cbInstallForSkyrim.Size = new System.Drawing.Size(102, 17);
            this.cbInstallForSkyrim.TabIndex = 11;
            this.cbInstallForSkyrim.Text = "Install for Skyrim";
            this.cbInstallForSkyrim.UseVisualStyleBackColor = true;
            this.cbInstallForSkyrim.CheckedChanged += new System.EventHandler(this.cbInstallForSkyrim_CheckedChanged);
            // 
            // cbRegisterAsNexusDownloadMgr
            // 
            this.cbRegisterAsNexusDownloadMgr.AutoSize = true;
            this.cbRegisterAsNexusDownloadMgr.Checked = true;
            this.cbRegisterAsNexusDownloadMgr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRegisterAsNexusDownloadMgr.Location = new System.Drawing.Point(9, 202);
            this.cbRegisterAsNexusDownloadMgr.Name = "cbRegisterAsNexusDownloadMgr";
            this.cbRegisterAsNexusDownloadMgr.Size = new System.Drawing.Size(208, 17);
            this.cbRegisterAsNexusDownloadMgr.TabIndex = 12;
            this.cbRegisterAsNexusDownloadMgr.Text = "&Register as Nexus Download Manager";
            this.cbRegisterAsNexusDownloadMgr.UseVisualStyleBackColor = true;
            // 
            // cbInstallForMorrowind
            // 
            this.cbInstallForMorrowind.AutoSize = true;
            this.cbInstallForMorrowind.Location = new System.Drawing.Point(7, 106);
            this.cbInstallForMorrowind.Name = "cbInstallForMorrowind";
            this.cbInstallForMorrowind.Size = new System.Drawing.Size(120, 17);
            this.cbInstallForMorrowind.TabIndex = 16;
            this.cbInstallForMorrowind.Text = "Install for Morrowind";
            this.cbInstallForMorrowind.UseVisualStyleBackColor = true;
            this.cbInstallForMorrowind.CheckedChanged += new System.EventHandler(this.cbInstallForMorrowind_CheckedChanged);
            // 
            // lblMorrowindInstallDir
            // 
            this.lblMorrowindInstallDir.AutoSize = true;
            this.lblMorrowindInstallDir.Location = new System.Drawing.Point(169, 107);
            this.lblMorrowindInstallDir.Name = "lblMorrowindInstallDir";
            this.lblMorrowindInstallDir.Size = new System.Drawing.Size(154, 13);
            this.lblMorrowindInstallDir.TabIndex = 15;
            this.lblMorrowindInstallDir.Text = "Morrowind installation directory:";
            // 
            // btnMorrowindBrowse
            // 
            this.btnMorrowindBrowse.Location = new System.Drawing.Point(331, 123);
            this.btnMorrowindBrowse.Name = "btnMorrowindBrowse";
            this.btnMorrowindBrowse.Size = new System.Drawing.Size(28, 23);
            this.btnMorrowindBrowse.TabIndex = 14;
            this.btnMorrowindBrowse.Text = "...";
            this.btnMorrowindBrowse.UseVisualStyleBackColor = true;
            this.btnMorrowindBrowse.Click += new System.EventHandler(this.btnMorrowindBrowse_Click);
            // 
            // txtMorrowindPath
            // 
            this.txtMorrowindPath.Enabled = false;
            this.txtMorrowindPath.Location = new System.Drawing.Point(8, 123);
            this.txtMorrowindPath.Name = "txtMorrowindPath";
            this.txtMorrowindPath.Size = new System.Drawing.Size(314, 20);
            this.txtMorrowindPath.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 231);
            this.Controls.Add(this.cbInstallForMorrowind);
            this.Controls.Add(this.lblMorrowindInstallDir);
            this.Controls.Add(this.btnMorrowindBrowse);
            this.Controls.Add(this.txtMorrowindPath);
            this.Controls.Add(this.cbRegisterAsNexusDownloadMgr);
            this.Controls.Add(this.cbInstallForSkyrim);
            this.Controls.Add(this.cbInstallForOblivion);
            this.Controls.Add(this.lblSkyrimInstallDir);
            this.Controls.Add(this.lblOblivionInstallDir);
            this.Controls.Add(this.btnSkyrimBrowse);
            this.Controls.Add(this.txtSkyrimPath);
            this.Controls.Add(this.chkStartMenu);
            this.Controls.Add(this.chkDesktop);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.btnOblivionBrowse);
            this.Controls.Add(this.txtOblivionPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tes Mod Manager installer";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.Shown += new System.EventHandler(this.MainFormShown);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.CheckBox chkStartMenu;
		private System.Windows.Forms.CheckBox chkDesktop;
		private System.Windows.Forms.Button btnInstall;
		private System.Windows.Forms.TextBox txtOblivionPath;
		private System.Windows.Forms.FolderBrowserDialog fbdBrowse;
        private System.Windows.Forms.Button btnOblivionBrowse;
        private System.Windows.Forms.TextBox txtSkyrimPath;
        private System.Windows.Forms.Button btnSkyrimBrowse;
        private System.Windows.Forms.Label lblOblivionInstallDir;
        private System.Windows.Forms.Label lblSkyrimInstallDir;
        private System.Windows.Forms.CheckBox cbInstallForOblivion;
        private System.Windows.Forms.CheckBox cbInstallForSkyrim;
        private System.Windows.Forms.CheckBox cbRegisterAsNexusDownloadMgr;
        private System.Windows.Forms.CheckBox cbInstallForMorrowind;
        private System.Windows.Forms.Label lblMorrowindInstallDir;
        private System.Windows.Forms.Button btnMorrowindBrowse;
        private System.Windows.Forms.TextBox txtMorrowindPath;
	}
}
