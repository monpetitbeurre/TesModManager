/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 13/07/2010
 * Time: 11:17 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using BaseTools.Configuration;
using BaseTools.Configuration.Parsers;
using SV = BaseTools.Searching.StringValidator;
using System.Text;
using System.IO;

namespace OblivionModManager
{
	/// <summary>
	/// Description of SearchForm.
	/// </summary>
	public partial class SearchForm : Form
	{
		public SearchForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void BtnSearchClick(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			
			FileInfo[] files = new DirectoryInfo(@"obmm\ocdlist").GetFiles("*.xbt", SearchOption.AllDirectories);
			
			foreach(FileInfo fi in files)
			{
				ConfigList cl = new GeneralConfig().LoadConfiguration(fi.FullName);
				
				foreach(ConfigPair cp in cl)
				{
					if (cp.DataIsList)
					{
						if (OCDMatch(cp.Key, cp.DataAsList))
						{
							sb.Append(fi.Name);
							sb.Append(": ");
							sb.AppendLine(cp.Key);
						}
					}
				}
			}
			
			txtResults.Text = sb.ToString();
		}
		
		bool OCDMatch(string filename, ConfigList cl)
		{
			if (chkFilename.Checked)
			{
				if (filename.IndexOf(txtFilename.Text, StringComparison.CurrentCultureIgnoreCase) == -1)
					return false;
			}
			
			if (chkName.Checked)
			{
				string name = cl["Name"];
				
				if (name == null || (name.IndexOf(txtName.Text, StringComparison.CurrentCultureIgnoreCase) == -1))
					return false;
			}
			
			if (chkAuthor.Checked)
			{
				string author = cl["Author"];
				
				if (author == null || (author.IndexOf(txtAuthor.Text, StringComparison.CurrentCultureIgnoreCase) == -1))
					return false;
			}
			
			return true;
		}
	}
}
