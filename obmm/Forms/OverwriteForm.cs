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

namespace OblivionModManager
{
	/// <summary>
	/// Description of OverwriteForm.
	/// </summary>
	public partial class OverwriteForm : Form
	{
		private List<DataFileInfo[]> dfiList;
		public OverwriteForm(List<DataFileInfo[]> dfil)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			dfiList = dfil;
			
			foreach(DataFileInfo[] dfi in dfiList)
			{
				DataFileInfo mainFile = dfi[0];
				if (dfi[1] == null)
					chklMods.Items.Add(mainFile.FileName);
				else
					chklMods.Items.Add(mainFile.FileName + " - " + dfi[1].Owners);
			}
		}
		
		void BtnOKClick(object sender, EventArgs e)
		{
			for(int i=0;i<chklMods.Items.Count;i++)
			{
				dfiList[i][0].Tag = chklMods.GetItemChecked(i);
			}
			this.Close();
		}
		
		void BtnSelectAllClick(object sender, EventArgs e)
		{
			for(int i=0;i<chklMods.Items.Count;i++)
				chklMods.SetItemChecked(i, true);
		}
		
		void BtnDeselectAllClick(object sender, EventArgs e)
		{
			for(int i=0;i<chklMods.Items.Count;i++)
				chklMods.SetItemChecked(i, false);
		}
		
		void BtnInvertClick(object sender, EventArgs e)
		{
			for(int i=0;i<chklMods.Items.Count;i++)
				chklMods.SetItemChecked(i, !chklMods.GetItemChecked(i));
		}
	}
}
