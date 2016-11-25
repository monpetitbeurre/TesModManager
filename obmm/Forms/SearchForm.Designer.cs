/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 13/07/2010
 * Time: 11:17 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OblivionModManager
{
	partial class SearchForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForm));
			this.btnSearch = new System.Windows.Forms.Button();
			this.txtFilename = new System.Windows.Forms.TextBox();
			this.chkFilename = new System.Windows.Forms.CheckBox();
			this.chkName = new System.Windows.Forms.CheckBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.chkAuthor = new System.Windows.Forms.CheckBox();
			this.txtAuthor = new System.Windows.Forms.TextBox();
			this.txtResults = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnSearch
			// 
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.Location = new System.Drawing.Point(208, 411);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(75, 23);
			this.btnSearch.TabIndex = 0;
			this.btnSearch.Text = "&Search";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.BtnSearchClick);
			// 
			// txtFilename
			// 
			this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtFilename.Location = new System.Drawing.Point(12, 33);
			this.txtFilename.Name = "txtFilename";
			this.txtFilename.Size = new System.Drawing.Size(271, 20);
			this.txtFilename.TabIndex = 1;
			// 
			// chkFilename
			// 
			this.chkFilename.AutoSize = true;
			this.chkFilename.Location = new System.Drawing.Point(12, 12);
			this.chkFilename.Name = "chkFilename";
			this.chkFilename.Size = new System.Drawing.Size(115, 17);
			this.chkFilename.TabIndex = 2;
			this.chkFilename.Text = "Filename Contains:";
			this.chkFilename.UseVisualStyleBackColor = true;
			// 
			// chkName
			// 
			this.chkName.AutoSize = true;
			this.chkName.Location = new System.Drawing.Point(12, 59);
			this.chkName.Name = "chkName";
			this.chkName.Size = new System.Drawing.Size(101, 17);
			this.chkName.TabIndex = 4;
			this.chkName.Text = "Name Contains:";
			this.chkName.UseVisualStyleBackColor = true;
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Location = new System.Drawing.Point(12, 80);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(271, 20);
			this.txtName.TabIndex = 3;
			// 
			// chkAuthor
			// 
			this.chkAuthor.AutoSize = true;
			this.chkAuthor.Location = new System.Drawing.Point(12, 106);
			this.chkAuthor.Name = "chkAuthor";
			this.chkAuthor.Size = new System.Drawing.Size(104, 17);
			this.chkAuthor.TabIndex = 6;
			this.chkAuthor.Text = "Author Contains:";
			this.chkAuthor.UseVisualStyleBackColor = true;
			// 
			// txtAuthor
			// 
			this.txtAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtAuthor.Location = new System.Drawing.Point(12, 127);
			this.txtAuthor.Name = "txtAuthor";
			this.txtAuthor.Size = new System.Drawing.Size(271, 20);
			this.txtAuthor.TabIndex = 5;
			// 
			// txtResults
			// 
			this.txtResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtResults.Location = new System.Drawing.Point(12, 164);
			this.txtResults.Multiline = true;
			this.txtResults.Name = "txtResults";
			this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtResults.Size = new System.Drawing.Size(271, 241);
			this.txtResults.TabIndex = 7;
			// 
			// SearchForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(295, 446);
			this.Controls.Add(this.txtResults);
			this.Controls.Add(this.chkAuthor);
			this.Controls.Add(this.txtAuthor);
			this.Controls.Add(this.chkName);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.chkFilename);
			this.Controls.Add(this.txtFilename);
			this.Controls.Add(this.btnSearch);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SearchForm";
			this.Text = "Search OCD List";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtResults;
		private System.Windows.Forms.TextBox txtAuthor;
		private System.Windows.Forms.CheckBox chkAuthor;
		private System.Windows.Forms.CheckBox chkFilename;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.CheckBox chkName;
		private System.Windows.Forms.TextBox txtFilename;
	}
}
