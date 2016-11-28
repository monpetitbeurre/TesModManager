using OblivionModManager;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.IO;
using BaseTools.XS;
using BaseTools.Configuration;
using ISV = BaseTools.Searching.StringValidator;

namespace DescriptionPanel
{
	public class DescPanel
	{
		private TextBox descriptionTB = null;
        //private RichTextBox descriptionTB = null;
        private SplitContainer altPanel = null;
		public void Execute(MainForm mainForm)
		{
			
			int sd = -9001;
			
            try
            {
                sd = OblivionModManager.Settings.altPanelSplitterDistance;
            }
            catch
            { }
   //         XConf.EnsureXConf();
   //         ConfigList lst = null;
   //         string cfg = Path.Combine(Program.BaseDir, "obmm.xbt");
   //         FileInfo fi = new FileInfo(cfg);
   //         ConfigList rt = XConf.LoadConfig(fi.FullName);
			
			//ConfigPair p;
			
			//p = rt.GetPair(new ISV("altpanel"));
			
			//if (p != null)
			//{
			//	lst = p.Data as ConfigList;
				
			//	if (lst != null)
			//	{
			//		p = lst.GetPair(new ISV("splitterDistance"));
			//		if (p != null)
			//		{
			//			if (p.DataIsInteger)
			//			{
			//				sd = p.DataAsInteger;
			//			}
			//		}
			//	}
			//}


            //descriptionTB = new RichTextBox();
            descriptionTB = new TextBox();
            descriptionTB.Name = "descriptionTB";
            descriptionTB.ReadOnly = true;
            descriptionTB.Multiline = true;
            descriptionTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            //descriptionTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            descriptionTB.Dock = System.Windows.Forms.DockStyle.Fill;
/*			
			SplitContainer sc = mainForm.MainContainer;
			SplitterPanel panel = sc.Panel1;
			
			altPanel = new SplitContainer();
			
			altPanel.Name = "altPanel";
			altPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			
			panel.Controls.Remove(mainForm.ESPList);
			panel.Controls.Add(altPanel);
			altPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			
			altPanel.Panel1.Controls.Add(mainForm.ESPList);
			altPanel.Panel2.Controls.Add(descriptionTB);
			
			if (sd == -9001)
				sd = altPanel.Size.Height - 80;
			altPanel.SplitterDistance = sd;
*/
            altPanel = (SplitContainer)mainForm.MainContainer.Panel1.Controls[0];
            altPanel.Panel1.Controls.Add(mainForm.ESPList);
            altPanel.Panel2.Controls.Add(descriptionTB);

            if (sd == -9001)
                sd = altPanel.Size.Height - 80;
            altPanel.SplitterDistance = sd;


			descriptionTB.BackColor = Color.White;
			mainForm.ESPList.SelectedIndexChanged += new System.EventHandler(this.UpdateDesc);
			mainForm.ModList.SelectedIndexChanged += new System.EventHandler(this.UpdateDesc);
			
			mainForm.AddExit(new System.Windows.Forms.FormClosingEventHandler(this.ClosePanel));
		}
		void UpdateDesc(object sender, EventArgs e)
		{
			ListView lv = sender as ListView;

            if (lv != null && lv.SelectedItems.Count==1)
			{
				foreach(ListViewItem lvi in lv.SelectedItems)
				{
//                    descriptionTB.DocumentText = "<html><body>"+ ((lvi.ToolTipText == null) ? "" : lvi.ToolTipText.Replace("\n", "\r\n"))+"</body></html>";
					descriptionTB.Text = (lvi.ToolTipText == null) ? "" : lvi.ToolTipText.Replace("\n", "\r\n");
				}
			}
		}
		private void ClosePanel(object sender, FormClosingEventArgs e)
		{
            OblivionModManager.Settings.altPanelSplitterDistance = altPanel.SplitterDistance;
            //ConfigList lst = new ConfigList();
            //ConfigList rt = XConf.LoadConfig(Path.Combine(Program.BaseDir, "obmm.xbt"));
            //rt.SetPair("altpanel", new ConfigPair("altpanel", lst));

            //lst.AddPair(new ConfigPair("splitterDistance", altPanel.SplitterDistance));

            //XConf.SaveConfig(Path.Combine(Program.BaseDir, "obmm.xbt"), rt);
        }
	}
}