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
using System.Collections.Generic;
using System.Net;
using System.ComponentModel;
using System.Threading;

namespace OblivionModManager
{
	/// <summary>
	/// Description of ImageDownload.
	/// </summary>
	public partial class DownloadForm : Form
	{
		public DownloadForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public static List<MemoryStream> DownloadFiles(string[] urls, bool bSilent)
		{
			DownloadForm dlf = new DownloadForm();
			
			dlf.urls = new Queue<string>(urls);
			
			foreach(string s in urls)
				dlf.lstDownloads.Items.Add(s);
			
			dlf.bgwDownload.RunWorkerAsync();
			
            //if (!bSilent)
			    dlf.ShowDialog();
            //else
            {
               // dlf.Visible = false;
               // dlf.Show();
                //while (dlf.bgwDownload.IsBusy)
                {
                //    Thread.Sleep(10);
                }
            }
			
			return dlf.streams;
		}
        
        delegate void removeTopFileFromListDelegate();

        void removeTopFileFromList()
        {
           lstDownloads.Items.RemoveAt(0);
        }

        delegate void closeDialogDelegate();

        void closeDialog()
        {
            this.Close();
        }

		public static MemoryStream DownloadFile(string url, bool bSilent)
		{
			return DownloadFiles(new string[] {url}, bSilent)[0];
		}
		
		
		
		Queue<string> urls = null;
		List<MemoryStream> streams = new List<MemoryStream>();
		
		void BgwDownloadDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			while(urls.Count > 0)
			{
				string fileurl = urls.Dequeue();
				MemoryStream streamLocal = null;
				try
				{
					// the URL to download the file from

					// first, we need to get the exact size (in bytes) of the file we are downloading
					Uri url = new Uri(fileurl);

					System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
					//MessageBox.Show(request.Timeout.ToString());
					System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

					response.Close();

					// gets the size of the file in bytes
					Int64 iSize = response.ContentLength;
					//MessageBox.Show(response.ContentLength.ToString());
					
					//	if (iSize == -1)
					//		iSize = 4096;

					// keeps track of the total bytes downloaded so we can update the progress bar
					Int64 iRunningByteTotal = 0;
					
					if (iSize != -1)
					{
						// use the webclient object to download the file
						using (System.Net.WebClient client = new System.Net.WebClient())
						{
							// open the file at the remote URL for reading
							using (System.IO.Stream streamRemote = client.OpenRead(new Uri(fileurl)))
							{
								// using the FileStream object, we can write the downloaded bytes to the file system

								streamLocal = new MemoryStream();
								
								

								// loop the stream and get the file into the byte buffer

								int iByteSize = 0;
								
								//MessageBox.Show(iSize.ToString());

								byte[] byteBuffer = new byte[iSize];

								while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
								{
									// write the bytes to the file system at the file path specified
									streamLocal.Write(byteBuffer, 0, iByteSize);

									iRunningByteTotal += iByteSize;
									// calculate the progress out of a base "100"

									double dIndex = (double)(iRunningByteTotal);

									double dTotal = (double)byteBuffer.Length;
									//MessageBox.Show(dIndex.ToString() + " " + dTotal.ToString());
									double dProgressPercentage = (dIndex / dTotal);

									int iProgressPercentage = (int)(dProgressPercentage * 100);
									// update the progress bar

									bgwDownload.ReportProgress(iProgressPercentage);
								}

								// clean up the file stream
								//streamLocal.Close();

								// close the connection to the remote server

								streamRemote.Close();
								
								streamLocal.Seek(0, SeekOrigin.Begin);

							}

							
						}
					}
					else
					{
						WebClient wc = new WebClient();
						//wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(BgwDownloadProgressChanged);
						
						streamLocal = new MemoryStream(wc.DownloadData(fileurl));
					}
                    streams.Add(streamLocal);
                }
				catch(Exception ex)
				{
                    //MessageBox.Show("Could not download file from "+fileurl+": "+ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.logger.WriteToLog("Could not download file from " + fileurl + ": " + ex.Message, Logger.LogLevel.Low);
                }
                removeTopFileFromListDelegate removeTopFile = new removeTopFileFromListDelegate(removeTopFileFromList);
                this.Invoke(removeTopFile);
			}
            closeDialogDelegate closedg = new closeDialogDelegate(closeDialog);
            this.Invoke(closedg);
		}
		
		void BgwDownloadProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			//MessageBox.Show(e.ProgressPercentage.ToString());
			pbProgress.Value = e.ProgressPercentage;
		}
	}
}
