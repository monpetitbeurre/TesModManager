/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 3/07/2010
 * Time: 11:54 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace OblivionModManager
{
	/// <summary>
	/// Description of ImageDownload.
	/// </summary>
	public partial class ImageDownload : Form
	{
		CreateModForm notifyform;
		public ImageDownload(string imageurl, CreateModForm notifyform)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			this.imageurl = imageurl;
			this.notifyform = notifyform;
			bgwDownload.RunWorkerAsync();
		}
		string imageurl;

        delegate void closeDialogDelegate();
        void closeDialog()
        {
            this.Close();
        }
		
		void BgwDownloadDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			try
			{
				// the URL to download the file from
				string sUrlToReadFileFrom = imageurl;

				// the path to write the file to
				string sFilePathToWriteFileTo = Path.Combine(Program.BaseDir, "temp.png");

				// first, we need to get the exact size (in bytes) of the file we are downloading
				Uri url = new Uri(sUrlToReadFileFrom);

				System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
				System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

				response.Close();

				// gets the size of the file in bytes
				Int64 iSize = response.ContentLength;

                if (iSize > 0)
                {
                    // keeps track of the total bytes downloaded so we can update the progress bar
                    Int64 iRunningByteTotal = 0;

                    System.Net.ServicePointManager.Expect100Continue = true;
                    System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;

                    // use the webclient object to download the file
                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        // open the file at the remote URL for reading
                        using (System.IO.Stream streamRemote = client.OpenRead(new Uri(sUrlToReadFileFrom)))
                        {
                            // using the FileStream object, we can write the downloaded bytes to the file system

                            using (Stream streamLocal = new FileStream(sFilePathToWriteFileTo, FileMode.Create, FileAccess.Write, FileShare.None))
                            {

                                // loop the stream and get the file into the byte buffer

                                int iByteSize = 0;

                                byte[] byteBuffer = new byte[iSize];

                                while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                                {
                                    // write the bytes to the file system at the file path specified
                                    streamLocal.Write(byteBuffer, 0, iByteSize);

                                    iRunningByteTotal += iByteSize;
                                    // calculate the progress out of a base "100"

                                    double dIndex = (double)(iRunningByteTotal);

                                    double dTotal = (double)byteBuffer.Length;

                                    double dProgressPercentage = (dIndex / dTotal);

                                    int iProgressPercentage = (int)(dProgressPercentage * 100);
                                    // update the progress bar

                                    bgwDownload.ReportProgress(iProgressPercentage);
                                }

                                // clean up the file stream
                                streamLocal.Close();

                            }

                            // close the connection to the remote server

                            streamRemote.Close();

                        }
                    }
                    notifyform.NotifyImage();

                }
				
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
            closeDialogDelegate closedg = new closeDialogDelegate(closeDialog);
            this.Invoke(closedg);
		}
		
		void BgwDownloadProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			pbProgress.Value = e.ProgressPercentage;
		}
	}
}
