/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 27/09/2010
 * Time: 3:30 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OblivionModManager
{
	/// <summary>
	/// Description of ExSettijngsForm.
	/// </summary>
	public partial class ESForm : Form
	{
		public ESForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void ESFormLoad(object sender, EventArgs e)
		{
			LoadSettings();
		}
		
		void BtnOKClick(object sender, EventArgs e)
		{
			SaveSettings();
			this.Close();
		}
		
		void SaveSettings()
		{
			GlobalSettings.AlwaysImportOCD = chkOmod.Checked;
			GlobalSettings.AlwaysImportOCDList = chkOCDLIst.Checked;
			GlobalSettings.AlwaysImportTES = chkTESNexus.Checked;
			GlobalSettings.IncludeVersionNumber = chkIncludeVersion.Checked;
			GlobalSettings.ShowOMODNames = chkShowNames.Checked;
		}
		void LoadSettings()
		{
			chkOmod.Checked = GlobalSettings.AlwaysImportOCD;
			chkOCDLIst.Checked = GlobalSettings.AlwaysImportOCDList;
			chkTESNexus.Checked =GlobalSettings.AlwaysImportTES;
			chkIncludeVersion.Checked = GlobalSettings.IncludeVersionNumber;
			chkShowNames.Checked = GlobalSettings.ShowOMODNames;
		}
		
		void BtnCancelClick(object sender, EventArgs e)
		{
			this.Close();
		}

        private void chkOmod_CheckedChanged(object sender, EventArgs e)
        {

        }
	}
}
