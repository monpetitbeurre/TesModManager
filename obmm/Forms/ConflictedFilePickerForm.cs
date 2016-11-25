/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 2/07/2010
 * Time: 11:10 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace OblivionModManager
{
    

	/// <summary>
	/// Description of OverwriteForm.
	/// </summary>
	public partial class ConflictedFilePickerForm : Form
	{
		private List<DataFileInfo> dfiList;
        private string[] selectedModList;
        private string previewFile = "";
        public List<string> hardlinks = new List<string>(); // any hardlinks created to use file in external viewer?

		public ConflictedFilePickerForm(List<DataFileInfo> dfil)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            dfiList = dfil;
            selectedModList = new string[dfil.Count];

            foreach (DataFileInfo dfi in dfiList)
			{
                string sourcefile = "";
                if (dfi == null || !(File.Exists(Path.Combine(Program.DataFolderName, dfi.FileName)) || File.Exists(dfi.FileName)))
                    continue;
                if (File.Exists(Path.Combine(Program.DataFolderName, dfi.FileName)))
                    sourcefile = Path.Combine(Program.DataFolderName, dfi.FileName);
                else if (File.Exists(dfi.FileName))
                    sourcefile = dfi.FileName;

                chklMods.Items.Add(dfi.FileName);
                selectedModList[chklMods.Items.Count - 1] = "";
                if (dfi.Owners != null)
                {
                    long filesize = new FileInfo(sourcefile).Length;
                    // who is the owner?
                    foreach (string owner in dfi.OwnerList)
                    {
                        if (File.Exists(Path.Combine(Program.ConflictsDir, dfi.FileName + "." + owner)))
                        {
                            FileInfo fi = new FileInfo(Path.Combine(Program.ConflictsDir, dfi.FileName + "." + owner));
                            if (fi.Length == filesize)
                            {
                                if (CompressionHandler.CRC(Path.Combine(Program.ConflictsDir, dfi.FileName + "." + owner)) == dfi.CRC)
                                {
                                    selectedModList[chklMods.Items.Count - 1] = owner;
                                    break;
                                }
                                else
                                {
                                    // wrong file??????? Keep it around just in case
                                    selectedModList[chklMods.Items.Count - 1] = owner;
                                }
                            }
                        }
                    }
                    //if (selectedModList[chklMods.Items.Count - 1].Length == 0)
                    //    selectedModList[chklMods.Items.Count - 1] = "unknown";
                }
			}
		}

		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode )]
            static extern bool CreateHardLink(
            string lpFileName,
            string lpExistingFileName,
            IntPtr lpSecurityAttributes
            );

		void BtnOKClick(object sender, EventArgs e)
		{
			for(int i=0;i<chklMods.Items.Count;i++)
			{
				//dfiList[i][0].Tag = chklMods.GetItemChecked(i);
                if (File.Exists(Path.Combine(Program.ConflictsDir, dfiList[i].FileName + "." + selectedModList[i])))
                {
                    try
                    {
                        File.Copy(Path.Combine(Program.ConflictsDir, dfiList[i].FileName + "." + selectedModList[i]), Path.Combine(Program.DataFolderName, dfiList[i].FileName), true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("could not copy over file: " + ex.Message);
                    }
                }
			}
			this.Close();
		}

        
        private void chklMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbMods.Items.Clear();
            // list the mods in second dialog
            foreach (string modname in dfiList[chklMods.SelectedIndex].OwnerList)
            {
                lbMods.Items.Add(modname);
            }
            lbMods.SelectedItem = selectedModList[chklMods.SelectedIndex];
        }

        private void lbMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedModList[chklMods.SelectedIndex]=(string)lbMods.Items[lbMods.SelectedIndex];

            DataFileInfo dfi = dfiList[chklMods.SelectedIndex];
            string filename = dfi.FileName + "." + lbMods.Items[lbMods.SelectedIndex];
            string filepath = Path.Combine(Program.ConflictsDir, filename);
            lblFilename.Text = "Filename: " + filename;
            lblFileSize.Text = "File size: " + (File.Exists(filepath) ? "" + new FileInfo(filepath).Length : "*File is missing*");

            if (File.Exists(filepath))
            {
                btnOpenFileInExternalViewer.Visible = true;
                btnOpenFileInExternalViewer.Text="Open file in external viewer";
            }
            else if (File.Exists(Path.Combine(Program.ConflictsDir, dfi.FileName)))
            {
                btnOpenFileInExternalViewer.Visible = true;
                btnOpenFileInExternalViewer.Text = "Open current file in external viewer";
            }
            else
            {
                btnOpenFileInExternalViewer.Visible = false;
            }

            if (previewFile.Length != 0)
            {
                // clean up
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
                pictureBox1.Image = new Bitmap(1, 1);
                //File.Delete(previewFile);
            }

            if (File.Exists(filepath))
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
                string fileExtension = Path.GetExtension(dfi.LowerFileName);
                switch (fileExtension)
                {

                    case ".dds":
                        lblImageResolution.Text = "";
                        textBox.Hide();
                        try
                        {
                            previewFile = Path.Combine(Program.TempDir,Path.GetFileName(filename) + ".bmp");

                            // viewer will want a known extension
                            string extension = Path.GetExtension(dfi.FileName);
                            if (!hardlinks.Contains(Path.Combine(Program.ConflictsDir, filename + extension)))
                            {
                                CreateHardLink(Path.Combine(Program.ConflictsDir, filename + extension), filepath, IntPtr.Zero);
                                hardlinks.Add(Path.Combine(Program.ConflictsDir, filename + extension));
                            }

                            DevIL.ImageImporter import = new DevIL.ImageImporter();
                            DevIL.Image img = import.LoadImage(Path.Combine(Program.ConflictsDir, filename + extension));
                            lblImageResolution.Text = "Image resolution: " + img.Width + "x" + img.Height + " at " + img.BitsPerPixel + "BPP";
                            DevIL.ImageExporter export = new DevIL.ImageExporter();
                            export.SaveImage(img, DevIL.ImageType.Bmp, previewFile);

                            import.Dispose();
                            export.Dispose();
                            img.Dispose();

                            //DevIlDotNet.Context.Initialize();
                            //DevIlDotNet.Image img = new DevIlDotNet.Image(Program.DataFolderName+"\\" + filename, DevIlDotNet.ImageExtension.Dds);
                            //img.Save(previewFile, DevIlDotNet.ImageExtension.Bmp);
                            //lblImageResolution.Text = "Image resolution: " + img.Width + "x" + img.Height + " at " + img.Bpp + "BPP";
                            //img.Close();
                            //DevIlDotNet.Context.Close();
                            pictureBox1.Show();
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                            pictureBox1.Image = new Bitmap(previewFile); //DevIL.DevIL.LoadBitmap(Program.DataFolderName+"\\" + filename);
                        }
                        catch (Exception ex)
                        {
                            Program.logger.WriteToLog("Could not load image to preview: " + ex.Message, Logger.LogLevel.Low);
                            if (ex.InnerException != null)
                                Program.logger.WriteToLog("Inner exception: " + ex.InnerException.Message, Logger.LogLevel.High);
                            Program.logger.WriteToLog("Stack trace: " + ex.StackTrace, Logger.LogLevel.High);
                            lblImageResolution.Text = "Image cannot be loaded.\n Please check that devil.dll, msvcp110.dll and msvcr110.dll are present";
                        }
                        break;
                    case ".bmp":
                    case ".jpg":
                    case ".jpeg":
                        try
                        {
                            pictureBox1.Show();
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                            pictureBox1.Image = new Bitmap(filepath);
                            lblImageResolution.Text = "Image resolution: " + pictureBox1.Image.Width + "x" + pictureBox1.Image.Height;
                        }
                        catch (Exception ex)
                        {
                            Program.logger.WriteToLog("Could not load image to preview: " + ex.Message, Logger.LogLevel.Low);
                            if (ex.InnerException != null)
                                Program.logger.WriteToLog("Inner exception: " + ex.InnerException.Message, Logger.LogLevel.High);
                            Program.logger.WriteToLog("Stack trace: " + ex.StackTrace, Logger.LogLevel.High);
                            lblImageResolution.Text = "Image cannot be loaded.\n Please check that devil.dll, msvcp110.dll and msvcr110.dll are present";
                        }
                        break;
                    case ".txt":
                    case ".ini":
                    case ".html":
                    case ".xml":
                        pictureBox1.Hide();
                        StreamReader sr = new StreamReader(File.OpenRead(filepath));
                        textBox.Text = sr.ReadToEnd();
                        textBox.Visible = true;
                        textBox.Show();
                        sr.Close();
                        break;
                    case ".wav":

                        break;
                    default:
                        pictureBox1.Hide();
                        textBox.Hide();
                        lblImageResolution.Text = "";
                        break;
                }
            }
            else
            {
                pictureBox1.Hide();
                lblImageResolution.Text = "";
            }
        }

        private void ConflictedFilePickerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pictureBox1.Image!=null)
                pictureBox1.Image.Dispose();
            pictureBox1.Dispose();

            foreach (string hardlink in hardlinks)
            {
                try
                {
                    File.Delete(hardlink);
                }
                catch
                { }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOpenFileInExternalViewer_Click(object sender, EventArgs e)
        {
            if (lbMods.SelectedIndex == -1)
                lbMods.SelectedIndex = 0;
            DataFileInfo dfi = dfiList[chklMods.SelectedIndex];
            string filename = Path.Combine(Program.ConflictsDir, dfi.FileName + "." + lbMods.Items[lbMods.SelectedIndex]);
            string extension = Path.GetExtension(dfi.LowerFileName);

            try
            {
                if (File.Exists(Program.CurrentDir + filename))
                {
                    // create a hard link to the file to avoid renaming the actual file (cleanup?...)
                    if (CreateHardLink(Path.Combine(Program.CurrentDir, filename + extension), Path.Combine(Program.CurrentDir, filename), IntPtr.Zero))
                    {
                        //Process.Start("cmd.exe", String.Format("/c mklink /H {0} {1}", Program.CurrentDir + filename + "." + extension, Program.CurrentDir + filename));
                        hardlinks.Add(Path.Combine(Program.CurrentDir,filename + extension));
                        Process.Start(Path.Combine(Program.CurrentDir, filename + extension));
                    }
                    else
                    {
                        File.Copy(Path.Combine(Program.CurrentDir, filename), Path.Combine(Program.TempDir, filename + extension));
                        Process.Start(Path.Combine(Program.TempDir,filename + extension));
                    }
                }
                else if (File.Exists(Path.Combine(Program.DataFolderName, dfi.FileName)))
                {
                    Process.Start(Path.Combine(Program.DataFolderName,dfi.FileName));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("could not create hard links!!:"+ex.Message);
            }
        }

        private void btnOpenFileInExternalViewer_MouseHover(object sender, EventArgs e)
        {
            tooltip.SetToolTip(btnOpenFileInExternalViewer, "Click here to open the file in any associated viewer (irfanview, nifskope, etc...)");
        }

        private void btnOverwriteAll_Click(object sender, EventArgs e)
        {
            for (int i=0;i<dfiList.Count;i++)
            {
                if (dfiList[i] == null)
                    continue;
                selectedModList[i] = dfiList[i].OwnerList[dfiList[i].OwnerList.Count-1];
            }
        }
	}
}
