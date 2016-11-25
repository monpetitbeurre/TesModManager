using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OblivionModManager
{
    public partial class DownloadsForm : Form
    {
        Dictionary<string, NexusModFileInfo> infolist = new Dictionary<string, NexusModFileInfo>();
        public DownloadsForm()
        {
            InitializeComponent();
            refreshlist();
            downloadsRefreshTimer.Start();
        }

        private void refreshlist()
        {
            downloadsList.Rows.Clear();

            System.Threading.Monitor.Enter(Program.downloadingList);
            foreach (FileDownload dl in Program.downloadingList.Values)
            {
                string strURL = dl.nxm;
                DataGridViewRow dgvr = new DataGridViewRow();
                dgvr.CreateCells(downloadsList);
                dgvr.Cells[dgEnabledColumn.Index].Value = false;
                dgvr.Cells[dgURLColumn.Index].Value = strURL;

                dgvr.Cells[dgStatusColumn.Index].Value = "Active";

                if (!infolist.ContainsKey(strURL))
                {
                    NexusModFileInfo info = Program.GetFileInfo(Path.GetFileName(strURL));
                    infolist.Add(strURL, info);
                }
                if (infolist.ContainsKey(strURL) && infolist[strURL]!=null)
                    dgvr.Cells[dgModNameColumn.Index].Value = infolist[strURL].Name;
                dgvr.Cells[dgProgressColumn.Index].Value = ""+dl.progress+"%";

                dgvr.Tag = dl;

                downloadsList.Rows.Add(dgvr);
            }
            System.Threading.Monitor.Exit(Program.downloadingList);
            System.Threading.Monitor.Enter(Program.downloadList);
            foreach (FileDownload dl in Program.downloadList.Values)
            {
                string strURL = dl.nxm;
                DataGridViewRow dgvr = new DataGridViewRow();
                dgvr.CreateCells(downloadsList);
                dgvr.Cells[dgEnabledColumn.Index].Value = false;
                dgvr.Cells[dgURLColumn.Index].Value = strURL;
                 
                dgvr.Cells[dgStatusColumn.Index].Value = "Queued";

                if (!infolist.ContainsKey(strURL) || infolist[strURL]==null)
                {
                    NexusModFileInfo info = Program.GetFileInfo(Path.GetFileName(strURL));
                    if (infolist.ContainsKey(strURL))
                        infolist.Remove(strURL);
                    infolist.Add(strURL, info);
                }
                if (infolist.ContainsKey(strURL) && infolist[strURL] != null)
                    dgvr.Cells[dgModNameColumn.Index].Value = infolist[strURL].Name;

                dgvr.Tag = dl;

                downloadsList.Rows.Add(dgvr);
            }
            System.Threading.Monitor.Exit(Program.downloadList);
            foreach (FileDownload dl in Program.pausedList.Values)
            {
                string strURL = dl.nxm;
                DataGridViewRow dgvr = new DataGridViewRow();
                dgvr.CreateCells(downloadsList);
                dgvr.Cells[dgEnabledColumn.Index].Value = false;
                dgvr.Cells[dgURLColumn.Index].Value = strURL;

                dgvr.Cells[dgStatusColumn.Index].Value = "Paused";

                if (!infolist.ContainsKey(strURL) || infolist[strURL] == null)
                {
                    NexusModFileInfo info = Program.GetFileInfo(Path.GetFileName(strURL));
                    if (infolist.ContainsKey(strURL))
                        infolist.Remove(strURL);
                    infolist.Add(strURL, info);
                }
                if (infolist.ContainsKey(strURL) && infolist[strURL] != null)
                    dgvr.Cells[dgModNameColumn.Index].Value = infolist[strURL].Name;

                dgvr.Tag = dl;

                downloadsList.Rows.Add(dgvr);
            }

        }
        private void downloadsRefreshTimer_Tick(object sender, EventArgs e)
        {
            // preserve the selected rows
            List<string> selectedURLs = new List<string>();
            foreach (DataGridViewRow dgvr in downloadsList.SelectedRows)
            {
                selectedURLs.Add(dgvr.Cells[dgURLColumn.Index].Value.ToString());
            }
            refreshlist();
            foreach (DataGridViewRow dgvr in downloadsList.SelectedRows)
            {
                foreach (string url in selectedURLs)
                {
                    if (dgvr.Cells[dgURLColumn.Index].Value.ToString().CompareTo(url) == 0)
                        dgvr.Selected = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (downloadsList.SelectedRows.Count > 0)
            {
                downloadsRefreshTimer.Stop();
                System.Threading.Monitor.Enter(Program.downloadList);
                foreach (DataGridViewRow dgr in downloadsList.SelectedRows)
                {
                    FileDownload dl = dgr.Tag as FileDownload;
                    //string strURL = dgr.Cells[dgURLColumn.Index].Value.ToString();
                    switch (dgr.Cells[dgStatusColumn.Index].Value.ToString().ToUpper())
                    {
                        case "PAUSED":
                            Program.pausedList.Remove(dl.nxm); //strURL);
                            break;
                        case "QUEUED":
                            Program.downloadList.Remove(dl.nxm); //strURL);
                            break;
                        case "ACTIVE":
                            dl.bCancelled = true;
                            //Program.strDownloadToCancel = dl.nxm; //strURL;
                            //Program.bCancelDownload = true;
                            break;
                        default:
                            break;
                    }
                    infolist.Remove(dl.nxm); //strURL);
                }
                System.Threading.Monitor.Exit(Program.downloadList);
                refreshlist();
                downloadsRefreshTimer.Start();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (downloadsList.SelectedRows.Count > 0)
            {
                downloadsRefreshTimer.Stop();
                System.Threading.Monitor.Enter(Program.downloadList);
                foreach (DataGridViewRow dgr in downloadsList.SelectedRows)
                {
                    Program.pausedList.Add(dgr.Cells[dgURLColumn.Index].Value.ToString(),dgr.Tag as FileDownload);
                    Program.downloadList.Remove(dgr.Cells[dgURLColumn.Index].Value.ToString());
                    if (dgr.Cells[dgStatusColumn.Index].Value.ToString() == "Active")
                    {
                        FileDownload dl = dgr.Tag as FileDownload;
                        dl.bCancelled = true;
                        //Program.bCancelDownload = true;
                    }
                    dgr.Cells[dgStatusColumn.Index].Value = "Paused";

                }
                System.Threading.Monitor.Exit(Program.downloadList);
                refreshlist();
                downloadsRefreshTimer.Start();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (downloadsList.SelectedRows.Count > 0)
            {
                downloadsRefreshTimer.Stop();
                System.Threading.Monitor.Enter(Program.downloadList);
                foreach (DataGridViewRow dgr in downloadsList.SelectedRows)
                {
                    FileDownload dl = dgr.Tag as FileDownload;
                    //string strURL = dgr.Cells["dgURLColumn"].Value.ToString();
                    Program.pausedList.Remove(dl.nxm); //strURL);
                    if (!Program.downloadList.ContainsKey(dl.nxm)) //strURL))
                        Program.downloadList.Add(dl.nxm,dl); //strURL);
                    dgr.Cells["dgStatusColumn"].Value = "Queued";
                }
                System.Threading.Monitor.Exit(Program.downloadList);
                refreshlist();
                downloadsRefreshTimer.Start();
            }
        }
    }
}
