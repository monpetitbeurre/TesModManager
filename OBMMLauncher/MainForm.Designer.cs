/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 11/07/2010
 * Time: 1:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ESPMHider
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
			this.btnHideInactive = new System.Windows.Forms.Button();
			this.btnRestore = new System.Windows.Forms.Button();
			this.btnOblivionLauncher = new System.Windows.Forms.Button();
			this.btnOblivion = new System.Windows.Forms.Button();
			this.btnOBMM = new System.Windows.Forms.Button();
			this.btnTESCS = new System.Windows.Forms.Button();
			this.btnWryeBash = new System.Windows.Forms.Button();
			this.btnTES4Edit = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnHideInactive
			// 
			this.btnHideInactive.Image = ((System.Drawing.Image)(resources.GetObject("btnHideInactive.Image")));
			this.btnHideInactive.Location = new System.Drawing.Point(12, 397);
			this.btnHideInactive.Name = "btnHideInactive";
			this.btnHideInactive.Size = new System.Drawing.Size(208, 57);
			this.btnHideInactive.TabIndex = 1;
			this.btnHideInactive.Text = "&Hide Inactive ESPMs";
			this.btnHideInactive.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.btnHideInactive.UseVisualStyleBackColor = true;
			this.btnHideInactive.Click += new System.EventHandler(this.BtnHideInactiveClick);
			// 
			// btnRestore
			// 
			this.btnRestore.Image = ((System.Drawing.Image)(resources.GetObject("btnRestore.Image")));
			this.btnRestore.Location = new System.Drawing.Point(12, 460);
			this.btnRestore.Name = "btnRestore";
			this.btnRestore.Size = new System.Drawing.Size(208, 57);
			this.btnRestore.TabIndex = 2;
			this.btnRestore.Text = "&Restore Inactive ESPMs";
			this.btnRestore.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.btnRestore.UseVisualStyleBackColor = true;
			this.btnRestore.Click += new System.EventHandler(this.BtnRestoreClick);
			// 
			// btnOblivionLauncher
			// 
			this.btnOblivionLauncher.Image = ((System.Drawing.Image)(resources.GetObject("btnOblivionLauncher.Image")));
			this.btnOblivionLauncher.Location = new System.Drawing.Point(13, 208);
			this.btnOblivionLauncher.Name = "btnOblivionLauncher";
			this.btnOblivionLauncher.Size = new System.Drawing.Size(208, 57);
			this.btnOblivionLauncher.TabIndex = 3;
			this.btnOblivionLauncher.Text = "Oblivion &Launcher";
			this.btnOblivionLauncher.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.btnOblivionLauncher.UseVisualStyleBackColor = true;
			this.btnOblivionLauncher.Click += new System.EventHandler(this.BtnOblivionLauncherClick);
			// 
			// btnOblivion
			// 
			this.btnOblivion.Image = ((System.Drawing.Image)(resources.GetObject("btnOblivion.Image")));
			this.btnOblivion.Location = new System.Drawing.Point(12, 19);
			this.btnOblivion.Name = "btnOblivion";
			this.btnOblivion.Size = new System.Drawing.Size(208, 57);
			this.btnOblivion.TabIndex = 4;
			this.btnOblivion.Text = "&Oblivion";
			this.btnOblivion.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.btnOblivion.UseVisualStyleBackColor = true;
			this.btnOblivion.Click += new System.EventHandler(this.BtnOblivionClick);
			// 
			// btnOBMM
			// 
			this.btnOBMM.Image = ((System.Drawing.Image)(resources.GetObject("btnOBMM.Image")));
			this.btnOBMM.Location = new System.Drawing.Point(12, 82);
			this.btnOBMM.Name = "btnOBMM";
			this.btnOBMM.Size = new System.Drawing.Size(208, 57);
			this.btnOBMM.TabIndex = 5;
			this.btnOBMM.Text = "Oblivion &Mod Manager";
			this.btnOBMM.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.btnOBMM.UseVisualStyleBackColor = true;
			this.btnOBMM.Click += new System.EventHandler(this.BtnOBMMClick);
			// 
			// btnTESCS
			// 
			this.btnTESCS.Image = ((System.Drawing.Image)(resources.GetObject("btnTESCS.Image")));
			this.btnTESCS.Location = new System.Drawing.Point(13, 271);
			this.btnTESCS.Name = "btnTESCS";
			this.btnTESCS.Size = new System.Drawing.Size(208, 57);
			this.btnTESCS.TabIndex = 6;
			this.btnTESCS.Text = "Elder Scrolls &Construction Set";
			this.btnTESCS.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.btnTESCS.UseVisualStyleBackColor = true;
			this.btnTESCS.Click += new System.EventHandler(this.BtnTESCSClick);
			// 
			// btnWryeBash
			// 
			this.btnWryeBash.Image = ((System.Drawing.Image)(resources.GetObject("btnWryeBash.Image")));
			this.btnWryeBash.Location = new System.Drawing.Point(13, 145);
			this.btnWryeBash.Name = "btnWryeBash";
			this.btnWryeBash.Size = new System.Drawing.Size(208, 57);
			this.btnWryeBash.TabIndex = 7;
			this.btnWryeBash.Text = "&Wrye Bash";
			this.btnWryeBash.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.btnWryeBash.UseVisualStyleBackColor = true;
			this.btnWryeBash.Click += new System.EventHandler(this.BtnWryeBashClick);
			// 
			// btnTES4Edit
			// 
			this.btnTES4Edit.Image = ((System.Drawing.Image)(resources.GetObject("btnTES4Edit.Image")));
			this.btnTES4Edit.Location = new System.Drawing.Point(13, 334);
			this.btnTES4Edit.Name = "btnTES4Edit";
			this.btnTES4Edit.Size = new System.Drawing.Size(208, 57);
			this.btnTES4Edit.TabIndex = 8;
			this.btnTES4Edit.Text = "TES4Edit";
			this.btnTES4Edit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.btnTES4Edit.UseVisualStyleBackColor = true;
			this.btnTES4Edit.Click += new System.EventHandler(this.BtnTES4EditClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(233, 530);
			this.Controls.Add(this.btnTES4Edit);
			this.Controls.Add(this.btnWryeBash);
			this.Controls.Add(this.btnTESCS);
			this.Controls.Add(this.btnOBMM);
			this.Controls.Add(this.btnOblivion);
			this.Controls.Add(this.btnOblivionLauncher);
			this.Controls.Add(this.btnRestore);
			this.Controls.Add(this.btnHideInactive);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Oblivion Launcher";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button btnTES4Edit;
		private System.Windows.Forms.Button btnWryeBash;
		private System.Windows.Forms.Button btnTESCS;
		private System.Windows.Forms.Button btnOblivion;
		private System.Windows.Forms.Button btnOBMM;
		private System.Windows.Forms.Button btnOblivionLauncher;
		private System.Windows.Forms.Button btnRestore;
		private System.Windows.Forms.Button btnHideInactive;
	}
}
