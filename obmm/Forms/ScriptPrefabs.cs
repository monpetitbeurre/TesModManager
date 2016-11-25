using OblivionModManager;
using OblivionModManager.Scripting;
using System.Windows.Forms;
using System;
using System.IO;
using System.Drawing;
using BaseTools.Configuration;
using BaseTools.Configuration.Parsers;
using SV = BaseTools.Searching.StringValidator;
using BaseTools.Dialog;

// COMPILED WITH OBMM UTILITES COMPILER FOR SPEED

namespace OblivionModManager.Forms
{
	public class PrefabForm : Form
	{
		OblivionModManager.Forms.ScriptEditor scriptEditor;
		ConfigList prefabs;
		public const string PREFAB_FILE = @"obmm\prefabs.xbt";
		
		public PrefabForm(ScriptEditor se)
		{
			this.scriptEditor = se;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			if (!File.Exists(PREFAB_FILE))
			{
				prefabs = new ConfigList();
			}
			else
			{
				prefabs = new GeneralConfig().LoadConfiguration(PREFAB_FILE);
				
				foreach(ConfigPair cp in prefabs)
				{
					if (cp.DataIsString)
						lstPrefabs.Items.Add(cp.Key);
				}
			}
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			
		}
		
		void BtnSavePrefabClick(object sender, EventArgs e)
		{
			string name;
			if ((name = InputBox.Show("New Name", "New Name:")) != null)
			{
				if (prefabs.HasPair(new SV(name, false)))
				{
					MessageBox.Show("That name is already used", "Already In Use", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					prefabs.AddString(name, scriptEditor.ScriptText);
					lstPrefabs.Items.Add(name);
				}
			}
		}
		
		void BtnLoadPrefabClick(object sender, EventArgs e)
		{
			if (lstPrefabs.SelectedIndex != -1)
			{
				string name = lstPrefabs.Items[lstPrefabs.SelectedIndex].ToString();
				scriptEditor.ScriptText = prefabs[name];
				this.Close();
			}
		}
		
		void BtnDeletePrefabClick(object sender, EventArgs e)
		{
			if (lstPrefabs.SelectedIndex != -1)
			{
				string name = lstPrefabs.Items[lstPrefabs.SelectedIndex].ToString();
				prefabs.RemoveKey(name);
				lstPrefabs.Items.RemoveAt(lstPrefabs.SelectedIndex);
			}
		}
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			new GeneralConfig().SaveConfiguration(PREFAB_FILE, prefabs);
		}
		
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
			this.lstPrefabs = new System.Windows.Forms.ListBox();
			this.btnSavePrefab = new System.Windows.Forms.Button();
			this.btnLoadPrefab = new System.Windows.Forms.Button();
			this.btnDeletePrefab = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lstPrefabs
			// 
			this.lstPrefabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lstPrefabs.FormattingEnabled = true;
			this.lstPrefabs.Location = new System.Drawing.Point(12, 12);
			this.lstPrefabs.Name = "lstPrefabs";
			this.lstPrefabs.Size = new System.Drawing.Size(219, 212);
			this.lstPrefabs.TabIndex = 0;
			// 
			// btnSavePrefab
			// 
			this.btnSavePrefab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSavePrefab.Location = new System.Drawing.Point(170, 230);
			this.btnSavePrefab.Name = "btnSavePrefab";
			this.btnSavePrefab.Size = new System.Drawing.Size(61, 23);
			this.btnSavePrefab.TabIndex = 1;
			this.btnSavePrefab.Text = "Save";
			this.btnSavePrefab.UseVisualStyleBackColor = true;
			this.btnSavePrefab.Click += new System.EventHandler(this.BtnSavePrefabClick);
			// 
			// btnLoadPrefab
			// 
			this.btnLoadPrefab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoadPrefab.Location = new System.Drawing.Point(108, 230);
			this.btnLoadPrefab.Name = "btnLoadPrefab";
			this.btnLoadPrefab.Size = new System.Drawing.Size(56, 23);
			this.btnLoadPrefab.TabIndex = 2;
			this.btnLoadPrefab.Text = "Load";
			this.btnLoadPrefab.UseVisualStyleBackColor = true;
			this.btnLoadPrefab.Click += new System.EventHandler(this.BtnLoadPrefabClick);
			// 
			// btnDeletePrefab
			// 
			this.btnDeletePrefab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDeletePrefab.Location = new System.Drawing.Point(47, 230);
			this.btnDeletePrefab.Name = "btnDeletePrefab";
			this.btnDeletePrefab.Size = new System.Drawing.Size(55, 23);
			this.btnDeletePrefab.TabIndex = 3;
			this.btnDeletePrefab.Text = "Delete";
			this.btnDeletePrefab.UseVisualStyleBackColor = true;
			this.btnDeletePrefab.Click += new System.EventHandler(this.BtnDeletePrefabClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(243, 261);
			this.Controls.Add(this.btnDeletePrefab);
			this.Controls.Add(this.btnLoadPrefab);
			this.Controls.Add(this.btnSavePrefab);
			this.Controls.Add(this.lstPrefabs);
			this.Name = "MainForm";
			this.Text = "Manage Prefabs";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button btnDeletePrefab;
		private System.Windows.Forms.ListBox lstPrefabs;
		private System.Windows.Forms.Button btnLoadPrefab;
		private System.Windows.Forms.Button btnSavePrefab;
	}
}