/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 3/07/2010
 * Time: 11:54 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OblivionModManager
{
	partial class ImageDownload
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
			this.bgwDownload = new System.ComponentModel.BackgroundWorker();
			this.pbProgress = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// bgwDownload
			// 
			this.bgwDownload.WorkerReportsProgress = true;
			this.bgwDownload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgwDownloadDoWork);
			this.bgwDownload.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BgwDownloadProgressChanged);
			// 
			// pbProgress
			// 
			this.pbProgress.Location = new System.Drawing.Point(12, 12);
			this.pbProgress.Name = "pbProgress";
			this.pbProgress.Size = new System.Drawing.Size(260, 23);
			this.pbProgress.TabIndex = 0;
			// 
			// ImageDownload
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 48);
			this.ControlBox = false;
			this.Controls.Add(this.pbProgress);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ImageDownload";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Downloading Image";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ProgressBar pbProgress;
		private System.ComponentModel.BackgroundWorker bgwDownload;
	}
}
