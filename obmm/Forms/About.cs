/* This file is part of Oblivion Mod Manager.
 * 
 * Oblivion Mod Manager is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Oblivion Mod Manager is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;

namespace OblivionModManager {
	public partial class About : Form
	{
		public About()
		{
			InitializeComponent();
			lblAbout.Text = lblAbout.Text.Replace("$version$", Program.version); //  Program.ExtMaj.ToString() + "." + Program.ExtMin.ToString() + "." + Program.ExtRev.ToString());
		}

        
        void LnkOBMMLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        	Process.Start("http://www.tesnexus.com/downloads/file.php?id=2097");
        }
        
        void LnkUESPLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        	Process.Start("http://uesp.net/");
        }
        
        void LnkSneakyTomatoLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        	Process.Start("http://www.tesnexus.com/downloads/file.php?id=14940");
        }
        
        void LnkOBMMExLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        	Process.Start("http://www.tesnexus.com/downloads/file.php?id=32277");
        }
        
        void BtnCloseClick(object sender, EventArgs e)
        {
        	this.Close();
        }

        private void lnkTesModManager_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://skyrim.nexusmods.com/downloads/file.php?id=5010");
        }
	}
}