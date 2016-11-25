/*
Copyright (C) 13/06/2010  Matthew "Scent Tree" Perry
scent.tree@gmail.com

This library/program is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

This libary/program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
namespace OblivionModManager
{
	partial class OCDForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OCDForm));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.btnTES = new System.Windows.Forms.Button();
			this.btnClone = new System.Windows.Forms.Button();
			this.btnMenu = new System.Windows.Forms.Button();
			this.btnDefRen = new System.Windows.Forms.Button();
			this.btnDefDel = new System.Windows.Forms.Button();
			this.btnDefAdd = new System.Windows.Forms.Button();
			this.lstDefinitions = new System.Windows.Forms.ListBox();
			this.grpOCD = new System.Windows.Forms.GroupBox();
			this.btnResources = new System.Windows.Forms.Button();
			this.btnScript = new System.Windows.Forms.Button();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.btnApply = new System.Windows.Forms.Button();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.txtImage = new System.Windows.Forms.TextBox();
			this.lblImage = new System.Windows.Forms.Label();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.lblEmail = new System.Windows.Forms.Label();
			this.txtWebsite = new System.Windows.Forms.TextBox();
			this.lblWebsite = new System.Windows.Forms.Label();
			this.txtVersion = new System.Windows.Forms.TextBox();
			this.lblVersion = new System.Windows.Forms.Label();
			this.txtAuthor = new System.Windows.Forms.TextBox();
			this.lblAuthor = new System.Windows.Forms.Label();
			this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sortItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportOCDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cloneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tESNexusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.grpOCD.SuspendLayout();
			this.cmsMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.btnTES);
			this.splitContainer1.Panel1.Controls.Add(this.btnClone);
			this.splitContainer1.Panel1.Controls.Add(this.btnMenu);
			this.splitContainer1.Panel1.Controls.Add(this.btnDefRen);
			this.splitContainer1.Panel1.Controls.Add(this.btnDefDel);
			this.splitContainer1.Panel1.Controls.Add(this.btnDefAdd);
			this.splitContainer1.Panel1.Controls.Add(this.lstDefinitions);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.grpOCD);
			this.splitContainer1.Size = new System.Drawing.Size(724, 570);
			this.splitContainer1.SplitterDistance = 183;
			this.splitContainer1.TabIndex = 0;
			// 
			// btnTES
			// 
			this.btnTES.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnTES.Image = ((System.Drawing.Image)(resources.GetObject("btnTES.Image")));
			this.btnTES.Location = new System.Drawing.Point(119, 544);
			this.btnTES.Name = "btnTES";
			this.btnTES.Size = new System.Drawing.Size(23, 23);
			this.btnTES.TabIndex = 7;
			this.btnTES.UseVisualStyleBackColor = true;
			this.btnTES.Click += new System.EventHandler(this.BtnTESClick);
			// 
			// btnClone
			// 
			this.btnClone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClone.Image = ((System.Drawing.Image)(resources.GetObject("btnClone.Image")));
			this.btnClone.Location = new System.Drawing.Point(90, 544);
			this.btnClone.Name = "btnClone";
			this.btnClone.Size = new System.Drawing.Size(23, 23);
			this.btnClone.TabIndex = 6;
			this.btnClone.UseVisualStyleBackColor = true;
			this.btnClone.Click += new System.EventHandler(this.BtnCloneClick);
			// 
			// btnMenu
			// 
			this.btnMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnMenu.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu.Image")));
			this.btnMenu.Location = new System.Drawing.Point(148, 543);
			this.btnMenu.Name = "btnMenu";
			this.btnMenu.Size = new System.Drawing.Size(23, 23);
			this.btnMenu.TabIndex = 4;
			this.btnMenu.UseVisualStyleBackColor = true;
			this.btnMenu.Click += new System.EventHandler(this.BtnMenuClick);
			// 
			// btnDefRen
			// 
			this.btnDefRen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDefRen.Image = ((System.Drawing.Image)(resources.GetObject("btnDefRen.Image")));
			this.btnDefRen.Location = new System.Drawing.Point(61, 544);
			this.btnDefRen.Name = "btnDefRen";
			this.btnDefRen.Size = new System.Drawing.Size(23, 23);
			this.btnDefRen.TabIndex = 3;
			this.btnDefRen.UseVisualStyleBackColor = true;
			this.btnDefRen.Click += new System.EventHandler(this.DefsRename);
			// 
			// btnDefDel
			// 
			this.btnDefDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDefDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDefDel.Image")));
			this.btnDefDel.Location = new System.Drawing.Point(3, 544);
			this.btnDefDel.Name = "btnDefDel";
			this.btnDefDel.Size = new System.Drawing.Size(23, 23);
			this.btnDefDel.TabIndex = 1;
			this.btnDefDel.UseVisualStyleBackColor = true;
			this.btnDefDel.Click += new System.EventHandler(this.DefRem);
			// 
			// btnDefAdd
			// 
			this.btnDefAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDefAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnDefAdd.Image")));
			this.btnDefAdd.Location = new System.Drawing.Point(32, 544);
			this.btnDefAdd.Name = "btnDefAdd";
			this.btnDefAdd.Size = new System.Drawing.Size(23, 23);
			this.btnDefAdd.TabIndex = 2;
			this.btnDefAdd.UseVisualStyleBackColor = true;
			this.btnDefAdd.Click += new System.EventHandler(this.BtnDefAddClick);
			// 
			// lstDefinitions
			// 
			this.lstDefinitions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lstDefinitions.FormattingEnabled = true;
			this.lstDefinitions.Location = new System.Drawing.Point(3, 0);
			this.lstDefinitions.Name = "lstDefinitions";
			this.lstDefinitions.Size = new System.Drawing.Size(176, 537);
			this.lstDefinitions.TabIndex = 5;
			this.lstDefinitions.SelectedIndexChanged += new System.EventHandler(this.LstDefinitionsSelectedIndexChanged);
			this.lstDefinitions.DoubleClick += new System.EventHandler(this.DefsRename);
			this.lstDefinitions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstDefinitionsKeyDown);
			// 
			// grpOCD
			// 
			this.grpOCD.Controls.Add(this.btnResources);
			this.grpOCD.Controls.Add(this.btnScript);
			this.grpOCD.Controls.Add(this.txtName);
			this.grpOCD.Controls.Add(this.lblName);
			this.grpOCD.Controls.Add(this.btnApply);
			this.grpOCD.Controls.Add(this.txtDescription);
			this.grpOCD.Controls.Add(this.lblDescription);
			this.grpOCD.Controls.Add(this.txtImage);
			this.grpOCD.Controls.Add(this.lblImage);
			this.grpOCD.Controls.Add(this.txtEmail);
			this.grpOCD.Controls.Add(this.lblEmail);
			this.grpOCD.Controls.Add(this.txtWebsite);
			this.grpOCD.Controls.Add(this.lblWebsite);
			this.grpOCD.Controls.Add(this.txtVersion);
			this.grpOCD.Controls.Add(this.lblVersion);
			this.grpOCD.Controls.Add(this.txtAuthor);
			this.grpOCD.Controls.Add(this.lblAuthor);
			this.grpOCD.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpOCD.Location = new System.Drawing.Point(0, 0);
			this.grpOCD.Name = "grpOCD";
			this.grpOCD.Size = new System.Drawing.Size(537, 570);
			this.grpOCD.TabIndex = 0;
			this.grpOCD.TabStop = false;
			this.grpOCD.Text = "Conversion Data";
			// 
			// btnResources
			// 
			this.btnResources.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnResources.Image = ((System.Drawing.Image)(resources.GetObject("btnResources.Image")));
			this.btnResources.Location = new System.Drawing.Point(277, 535);
			this.btnResources.Name = "btnResources";
			this.btnResources.Size = new System.Drawing.Size(87, 23);
			this.btnResources.TabIndex = 10;
			this.btnResources.Text = "&Resources";
			this.btnResources.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnResources.UseVisualStyleBackColor = true;
			this.btnResources.Click += new System.EventHandler(this.BtnResourcesClick);
			// 
			// btnScript
			// 
			this.btnScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnScript.Image = ((System.Drawing.Image)(resources.GetObject("btnScript.Image")));
			this.btnScript.Location = new System.Drawing.Point(370, 535);
			this.btnScript.Name = "btnScript";
			this.btnScript.Size = new System.Drawing.Size(75, 23);
			this.btnScript.TabIndex = 8;
			this.btnScript.Text = "&Script";
			this.btnScript.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnScript.UseVisualStyleBackColor = true;
			this.btnScript.Click += new System.EventHandler(this.BtnScriptClick);
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Location = new System.Drawing.Point(8, 43);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(519, 20);
			this.txtName.TabIndex = 1;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(8, 26);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(35, 13);
			this.lblName.TabIndex = 0;
			this.lblName.Text = "&Name";
			this.lblName.Click += new System.EventHandler(this.SelectKBS);
			// 
			// btnApply
			// 
			this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnApply.Image = ((System.Drawing.Image)(resources.GetObject("btnApply.Image")));
			this.btnApply.Location = new System.Drawing.Point(451, 535);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 9;
			this.btnApply.Text = "&Apply";
			this.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.BtnApplyClick);
			// 
			// txtDescription
			// 
			this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDescription.Location = new System.Drawing.Point(8, 363);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtDescription.Size = new System.Drawing.Size(518, 161);
			this.txtDescription.TabIndex = 7;
			// 
			// lblDescription
			// 
			this.lblDescription.AutoSize = true;
			this.lblDescription.Location = new System.Drawing.Point(8, 347);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(60, 13);
			this.lblDescription.TabIndex = 0;
			this.lblDescription.Text = "&Description";
			this.lblDescription.Click += new System.EventHandler(this.SelectKBS);
			// 
			// txtImage
			// 
			this.txtImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtImage.Location = new System.Drawing.Point(8, 244);
			this.txtImage.Multiline = true;
			this.txtImage.Name = "txtImage";
			this.txtImage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtImage.Size = new System.Drawing.Size(518, 100);
			this.txtImage.TabIndex = 6;
			// 
			// lblImage
			// 
			this.lblImage.AutoSize = true;
			this.lblImage.Location = new System.Drawing.Point(7, 227);
			this.lblImage.Name = "lblImage";
			this.lblImage.Size = new System.Drawing.Size(47, 13);
			this.lblImage.TabIndex = 0;
			this.lblImage.Text = "&Image(s)";
			this.lblImage.Click += new System.EventHandler(this.SelectKBS);
			// 
			// txtEmail
			// 
			this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtEmail.Location = new System.Drawing.Point(7, 204);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(519, 20);
			this.txtEmail.TabIndex = 5;
			// 
			// lblEmail
			// 
			this.lblEmail.AutoSize = true;
			this.lblEmail.Location = new System.Drawing.Point(7, 187);
			this.lblEmail.Name = "lblEmail";
			this.lblEmail.Size = new System.Drawing.Size(32, 13);
			this.lblEmail.TabIndex = 0;
			this.lblEmail.Text = "&Email";
			this.lblEmail.Click += new System.EventHandler(this.SelectKBS);
			// 
			// txtWebsite
			// 
			this.txtWebsite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtWebsite.Location = new System.Drawing.Point(8, 163);
			this.txtWebsite.Name = "txtWebsite";
			this.txtWebsite.Size = new System.Drawing.Size(518, 20);
			this.txtWebsite.TabIndex = 4;
			this.txtWebsite.TextChanged += new System.EventHandler(this.TxtWebsiteTextChanged);
			// 
			// lblWebsite
			// 
			this.lblWebsite.AutoSize = true;
			this.lblWebsite.Location = new System.Drawing.Point(7, 146);
			this.lblWebsite.Name = "lblWebsite";
			this.lblWebsite.Size = new System.Drawing.Size(46, 13);
			this.lblWebsite.TabIndex = 0;
			this.lblWebsite.Text = "&Website";
			this.lblWebsite.DoubleClick += new System.EventHandler(this.LblWebsiteDoubleClick);
			this.lblWebsite.Click += new System.EventHandler(this.SelectKBS);
			// 
			// txtVersion
			// 
			this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtVersion.Location = new System.Drawing.Point(7, 83);
			this.txtVersion.Name = "txtVersion";
			this.txtVersion.Size = new System.Drawing.Size(519, 20);
			this.txtVersion.TabIndex = 2;
			// 
			// lblVersion
			// 
			this.lblVersion.AutoSize = true;
			this.lblVersion.Location = new System.Drawing.Point(7, 66);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(42, 13);
			this.lblVersion.TabIndex = 0;
			this.lblVersion.Text = "&Version";
			this.lblVersion.Click += new System.EventHandler(this.SelectKBS);
			// 
			// txtAuthor
			// 
			this.txtAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtAuthor.Location = new System.Drawing.Point(7, 123);
			this.txtAuthor.Name = "txtAuthor";
			this.txtAuthor.Size = new System.Drawing.Size(519, 20);
			this.txtAuthor.TabIndex = 3;
			// 
			// lblAuthor
			// 
			this.lblAuthor.AutoSize = true;
			this.lblAuthor.Location = new System.Drawing.Point(7, 106);
			this.lblAuthor.Name = "lblAuthor";
			this.lblAuthor.Size = new System.Drawing.Size(38, 13);
			this.lblAuthor.TabIndex = 0;
			this.lblAuthor.Text = "&Author";
			this.lblAuthor.Click += new System.EventHandler(this.SelectKBS);
			// 
			// cmsMenu
			// 
			this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.newToolStripMenuItem,
									this.loadToolStripMenuItem,
									this.saveToolStripMenuItem,
									this.saveAsToolStripMenuItem,
									this.addItemToolStripMenuItem,
									this.importDataToolStripMenuItem,
									this.sortItemsToolStripMenuItem,
									this.exportOCDToolStripMenuItem,
									this.cloneToolStripMenuItem,
									this.tESNexusToolStripMenuItem});
			this.cmsMenu.Name = "cmsMenu";
			this.cmsMenu.Size = new System.Drawing.Size(138, 224);
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.newToolStripMenuItem.Text = "New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItemClick);
			// 
			// loadToolStripMenuItem
			// 
			this.loadToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("loadToolStripMenuItem.Image")));
			this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			this.loadToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.loadToolStripMenuItem.Text = "Open";
			this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItemClick);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAsToolStripMenuItem.Image")));
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.saveAsToolStripMenuItem.Text = "Save As...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsAction);
			// 
			// addItemToolStripMenuItem
			// 
			this.addItemToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addItemToolStripMenuItem.Image")));
			this.addItemToolStripMenuItem.Name = "addItemToolStripMenuItem";
			this.addItemToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.addItemToolStripMenuItem.Text = "Add Item";
			this.addItemToolStripMenuItem.Click += new System.EventHandler(this.AddItemToolStripMenuItemClick);
			// 
			// importDataToolStripMenuItem
			// 
			this.importDataToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("importDataToolStripMenuItem.Image")));
			this.importDataToolStripMenuItem.Name = "importDataToolStripMenuItem";
			this.importDataToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.importDataToolStripMenuItem.Text = "Import Data";
			this.importDataToolStripMenuItem.Click += new System.EventHandler(this.ImportDataToolStripMenuItemClick);
			// 
			// sortItemsToolStripMenuItem
			// 
			this.sortItemsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sortItemsToolStripMenuItem.Image")));
			this.sortItemsToolStripMenuItem.Name = "sortItemsToolStripMenuItem";
			this.sortItemsToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.sortItemsToolStripMenuItem.Text = "Sort Items";
			this.sortItemsToolStripMenuItem.Click += new System.EventHandler(this.SortItemsToolStripMenuItemClick);
			// 
			// exportOCDToolStripMenuItem
			// 
			this.exportOCDToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exportOCDToolStripMenuItem.Image")));
			this.exportOCDToolStripMenuItem.Name = "exportOCDToolStripMenuItem";
			this.exportOCDToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.exportOCDToolStripMenuItem.Text = "Export OCD";
			this.exportOCDToolStripMenuItem.Click += new System.EventHandler(this.ExportOCDToolStripMenuItemClick);
			// 
			// cloneToolStripMenuItem
			// 
			this.cloneToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cloneToolStripMenuItem.Image")));
			this.cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
			this.cloneToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.cloneToolStripMenuItem.Text = "&Clone";
			this.cloneToolStripMenuItem.Click += new System.EventHandler(this.CloneToolStripMenuItemClick);
			// 
			// tESNexusToolStripMenuItem
			// 
			this.tESNexusToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("tESNexusToolStripMenuItem.Image")));
			this.tESNexusToolStripMenuItem.Name = "tESNexusToolStripMenuItem";
			this.tESNexusToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.tESNexusToolStripMenuItem.Text = "&TESNexus";
			this.tESNexusToolStripMenuItem.Click += new System.EventHandler(this.TESNexusToolStripMenuItemClick);
			// 
			// OCDForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(724, 570);
			this.Controls.Add(this.splitContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "OCDForm";
			this.Text = "OCD Creator - Untitled";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OCDFormFormClosing);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.grpOCD.ResumeLayout(false);
			this.grpOCD.PerformLayout();
			this.cmsMenu.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button btnResources;
		private System.Windows.Forms.Button btnTES;
		private System.Windows.Forms.Button btnClone;
		private System.Windows.Forms.ToolStripMenuItem tESNexusToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cloneToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportOCDToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sortItemsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importDataToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addItemToolStripMenuItem;
		private System.Windows.Forms.Button btnScript;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip cmsMenu;
		private System.Windows.Forms.Button btnMenu;
		private System.Windows.Forms.Button btnDefDel;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Button btnDefRen;
		private System.Windows.Forms.Button btnDefAdd;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.TextBox txtImage;
		private System.Windows.Forms.Label lblImage;
		private System.Windows.Forms.Label lblEmail;
		private System.Windows.Forms.TextBox txtEmail;
		private System.Windows.Forms.TextBox txtWebsite;
		private System.Windows.Forms.Label lblWebsite;
		private System.Windows.Forms.TextBox txtVersion;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.TextBox txtAuthor;
		private System.Windows.Forms.Label lblAuthor;
		private System.Windows.Forms.GroupBox grpOCD;
		private System.Windows.Forms.ListBox lstDefinitions;
		private System.Windows.Forms.SplitContainer splitContainer1;
	}
}
