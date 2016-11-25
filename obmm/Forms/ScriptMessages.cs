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

namespace OblivionModManager.Forms
{
    public partial class ScriptMessages : Form
    {
        public ScriptMessages()
        {
            InitializeComponent();
        }

        public RichTextBox GetOutputBox()
        {  
            return this.rtfOutput;
        }

        public RichTextBox GetErrorBox()
        {
            return this.rtfErrors;
        }
        public void ClearData()
        {
            this.rtfErrors.Text = "";
            this.rtfOutput.Text = "";
        }

        public void FocusTab(int tab)
        {
            if (tab == 0)
                tabOutput.Select();
            if (tab == 1)
                tabError.Select();
        }
    }
}