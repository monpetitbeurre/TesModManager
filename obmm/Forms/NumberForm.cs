/*
Copyright (C) 8/06/2010  Matthew "Scent Tree" Perry
scent.tree@gmail.com

This library/program is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

This libary/program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OblivionModManager
{
	/// <summary>
	/// Description of NumberForm.
	/// </summary>
	public partial class NumberForm : Form
	{
		public NumberForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public void SetupNumber(string caption, string message, double init, double min, double max, double step, int sigfigs)
		{
			this.Text = caption;
			lblMessage.Text = message;
			nudValue.Minimum = (decimal)min;
			nudValue.Maximum = (decimal)max;
			nudValue.Value = (decimal)init;
			nudValue.DecimalPlaces = sigfigs;
			nudValue.Increment = (decimal)step;
		}
		public static double SelectNumber(string caption, string message, double init, double min, double max, double step, int sigfigs)
		{
			NumberForm nf = new NumberForm();
			
			nf.SetupNumber(caption, message, init, min, max, step, sigfigs);
			
			nf.ShowDialog();
			
			double n = (double)nf.nudValue.Value;
			
			nf.Close();
			
			return n;
		}
		
		void BtnOKClick(object sender, EventArgs e)
		{
			this.Hide();
		}
	}
}
