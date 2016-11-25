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

namespace OblivionModManager.Forms {
	public partial class SelectForm : Form {

        public bool bCreateInstallationMenuSelected = false;
        bool bAtLeastOne = false;
        int currentindex = -1;

        public SelectForm(string[] items, string title, bool multi, string[] previews, string[] tooltips, bool atleastone)
            : this(items, title, multi, previews, tooltips, false, atleastone)
        {
        }
        public SelectForm(string[] items, string title, bool multi, string[] previews, string[] tooltips)
            : this(items, title, multi, previews, tooltips, false, false)
        {
        }
		public SelectForm(string[] items,string title,bool multi,string[] previews,string[] tooltips, bool bShowModInstallationOption, bool bForceAtLeastOne) {

			InitializeComponent();

//            if (Properties.Settings.Default.SelectFormX > 0 && Properties.Settings.Default.SelectFormY > 0)
//            {
                this.StartPosition = FormStartPosition.CenterParent;
//            }
            if (Properties.Settings.Default.SelectFormW > 0 && Properties.Settings.Default.SelectFormH > 0)
            {
                this.Width = Properties.Settings.Default.SelectFormW;
                this.Height = Properties.Settings.Default.SelectFormH;
            }

            if (bShowModInstallationOption)
                cbCreateInstallMenu.Visible = true;

            bAtLeastOne = bForceAtLeastOne;

            if (bAtLeastOne && !Multi)
            {
                lbSelect.SelectionMode = SelectionMode.One;
            }
            else if (Multi)
            {
                lbSelect.SelectionMode = SelectionMode.MultiExtended;
            }
            else
            {
                lbSelect.SelectionMode = SelectionMode.One;
            }

            Text = title;
			toolTips=tooltips;
			Multi=multi;
			System.Collections.Generic.List<int> selected=new System.Collections.Generic.List<int>();
			for(int i=0;i<items.Length;i++) {
				if(items[i].Length==0) continue;
				if(items[i][0]=='|') {
					items[i]=items[i].Substring(1);
					selected.Add(i);
					if(!Multi) break;
				}
			}
			lbSelect.Items.AddRange(items);
            for (int i=0;i<items.Length;i++)
            {
                object[] obj = new object[2];
                obj[0] = false;
                obj[1] = items[i];
                dgSelect.Rows.Add(obj);
            }
			if(Multi) {
				if(Settings.AdvSelectMany) {
					lbSelect.SelectionMode=SelectionMode.MultiExtended;
				} else {
					lbSelect.SelectionMode=SelectionMode.MultiSimple;
				}
				label1.Text="Select any number of options";
				if(selected.Count>0) foreach(int i in selected) lbSelect.SetSelected(i, true);
			} else {
                if (selected.Count == 0)
                {
                    if (bAtLeastOne)
                    {
                        lbSelect.SelectedIndex = 0;
                        dgSelect.Rows[0].Cells[0].Value = true;
                    }
                    else
                        lbSelect.SelectedIndex = -1;
                }
                else lbSelect.SelectedIndex = selected[0];
			}
            if (Multi && bAtLeastOne)
                label1.Text = "Select at least one option";
            else if (!Multi && bAtLeastOne)
                label1.Text = "Select exactly one option";
            else if (Multi)
                label1.Text = "Select any number of options";

            if (toolTips == null)
            {
                //bDescription.Visible = false;
                splitTextContainer.SplitterDistance = splitTextContainer.Height; // hide the splitter
            }
            else
            {
                //bDescription.Visible = true;
            }
            if (previews != null)
            {
                Previews = new System.Drawing.Image[previews.Length];
                for (int i = 0; i < previews.Length; i++)
                {
                    if (previews[i] == null || previews[i] == "") Previews[i] = null;
                    else Previews[i] = System.Drawing.Image.FromFile(previews[i]);
                }
                if (lbSelect.SelectedIndex != -1 && lbSelect.SelectedIndices.Count == 1)
                    bPreview.Enabled = true;
            }
            else
            {
                bPreview.Visible = false;
                splitContainer.SplitterDistance = splitContainer.Width;

            }
			lbSelect_SelectedIndexChanged(null, null);
            dgSelect_CellClick(null, new DataGridViewCellEventArgs(0, 0));
        }

		private bool blockClose=true;
		public int[] SelectedIndex={ 0 };
		private System.Drawing.Image[] Previews=null;
		private string[] toolTips;
		private bool ShowingDesc=false;
		private bool Multi;

		private int selectedIndex;
		private System.Collections.Generic.List<int> selected=new System.Collections.Generic.List<int>();

		private void bOK_Click(object sender, EventArgs e) {
			SelectedIndex=new int[lbSelect.SelectedIndices.Count];
			for(int i=0;i<SelectedIndex.Length;i++) SelectedIndex[i]=lbSelect.SelectedIndices[i];
            int totalCount = 0;
            for (int i = 0; i < dgSelect.Rows.Count; i++)
            {
                if ((bool)dgSelect.Rows[i].Cells[0].Value)
                    totalCount++;
            }
            if (bAtLeastOne && totalCount == 0)
            {
                MessageBox.Show("Select the right number of options", "Invalid selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            blockClose = false;
            SelectedIndex = new int[totalCount];
            for (int i = 0, j=0; i < dgSelect.Rows.Count; i++)
            {
                if ((bool)dgSelect.Rows[i].Cells[0].Value)
                    SelectedIndex[j++] = i;
            }
            if (Previews != null)
            {
				foreach(System.Drawing.Image i in Previews) if(i!=null) i.Dispose();
			}
            bCreateInstallationMenuSelected = cbCreateInstallMenu.Checked;
            Properties.Settings.Default.SelectFormW=this.Width;
            Properties.Settings.Default.SelectFormH = this.Height;
            Close();
		}

		private void SelectForm_FormClosing(object sender, FormClosingEventArgs e) {
			e.Cancel=blockClose;
			
			if (imgform != null)
				imgform.Close();
		}

		private void lbSelect_SelectedIndexChanged(object sender, EventArgs e) {
			if(lbSelect.SelectedItems.Count==0) {
				selected.Clear();
				selectedIndex=-1;
				//bDescription.Enabled=false;
				//bPreview.Enabled=false;
				return;
			}
            if (selectedIndex==lbSelect.SelectedIndex && lbSelect.SelectedIndices.Contains(selectedIndex))
                lbSelect.SelectedIndices.Remove(selectedIndex);
            if (selectedIndex != -1 && !lbSelect.SelectedIndices.Contains(selectedIndex)) selectedIndex = -1;
			for(int i=0;i<selected.Count;i++) { if(!lbSelect.SelectedIndices.Contains(selected[i])) selected.RemoveAt(i--); }
			for(int i=0;i<lbSelect.SelectedIndices.Count;i++) {
				if(!selected.Contains(lbSelect.SelectedIndices[i])) {
					selectedIndex=lbSelect.SelectedIndices[i];
					selected.Add(lbSelect.SelectedIndices[i]);
				}
			}
			if(Previews!=null) {
				if(selectedIndex!=-1&&Previews[selectedIndex]!=null) {
					//bPreview.Enabled=true;
                    pbPreview.Enabled = true;
                    pbPreview.Image = Previews[selectedIndex];
                    pbPreview.SizeMode = PictureBoxSizeMode.Zoom;
					if (imgform != null && !imgform.IsDisposed)
					{
						imgform.SetImage(Previews[selectedIndex]);
						imgform.Focus();
					}
				} else {
					//bPreview.Enabled=false;
					
					if (imgform != null && !imgform.IsDisposed)
						imgform.Close();
				}
			}
            if (toolTips != null)
            {
                if (selectedIndex != -1 && toolTips[selectedIndex] != null)
                {
                    txtDescription.Enabled = true;
                    txtDescription.ReadOnly = false;
                    txtDescription.Text = toolTips[selectedIndex].Trim();
                    txtDescription.ReadOnly = true;
                    //bDescription.Enabled = true;
                    if (!pbPreview.Enabled)
                    {
                        // enlarge text window
//                        txtDescription.Size = new System.Drawing.Size(txtDescription.Size.Width,txtDescription.Size.Height + pbPreview.Size.Height);
                    }
                }
                else
                {
                    //bDescription.Enabled = false;
                }
            }
        }
		
		ImageForm imgform = null;

		private void bPreview_Click(object sender, EventArgs e)
		{
			//(new ImageForm(Previews[selectedIndex])).ShowDialog();
			if (imgform == null || imgform.IsDisposed)
				imgform = new ImageForm(Previews[selectedIndex]);
			else
				imgform.SetImage(Previews[selectedIndex]);
			
			
			if (!imgform.Visible)
				imgform.Show();
		}

		private void bDescription_Click(object sender, EventArgs e) {
			if(ShowingDesc) {
				lbSelect.Visible=true;
				tbDesc.Visible=false;
				//bDescription.Text="Description";
				ShowingDesc=false;
			} else if(toolTips[selectedIndex]!=null) {
				tbDesc.Text=toolTips[selectedIndex];
				tbDesc.Select(0, 0);
				tbDesc.ScrollToCaret();
				tbDesc.Visible=true;
				lbSelect.Visible=false;
				//bDescription.Text="Options";
				ShowingDesc=true;
			} //else bDescription.Enabled=false;
		}

		private void lbSelect_KeyDown(object sender, KeyEventArgs e) {
			if(e.Control&&e.KeyCode==Keys.A&&Multi) {
				//No function to do this all in one go?
				SuspendLayout();
				lbSelect.SelectedIndices.Clear();
				for(int i=0;i<lbSelect.Items.Count;i++) {
					lbSelect.SelectedIndices.Add(i);
				}
				ResumeLayout();
			}
		}

        private void tbDesc_Click(object sender, EventArgs e)
        {

        }

        private void lbSelect_Click(object sender, EventArgs e)
        {
        }

        private void lbSelect_MouseDown(object sender, MouseEventArgs e)
        {
            currentindex=lbSelect.IndexFromPoint(e.X,e.Y);
        /*
            if (lbSelect.SelectedIndices.Contains(index) &&
                ((bAtLeastOne && lbSelect.SelectedIndices.Count>0) || !bAtLeastOne))
            {
                lbSelect.SelectedIndices.Remove(index);
            }
 * */
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void pbPreview_Click(object sender, EventArgs e)
        {

        }

        private void tbDesc_TextChanged(object sender, EventArgs e)
        {

        }

        private void bCancel_Click(object sender, EventArgs e)
        {

        }

        private void dgSelect_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Previews != null)
            {
                if (e.RowIndex != -1 && Previews[e.RowIndex] != null)
                {
                    //bPreview.Enabled = true;
                    pbPreview.Enabled = true;
                    pbPreview.Image = Previews[e.RowIndex];
                    pbPreview.SizeMode = PictureBoxSizeMode.Zoom;
                    if (imgform != null && !imgform.IsDisposed)
                    {
                        imgform.SetImage(Previews[e.RowIndex]);
                        imgform.Focus();
                    }
                }
                else
                {
                    //bPreview.Enabled = false;

                    if (imgform != null && !imgform.IsDisposed)
                        imgform.Close();
                }
            }
            if (toolTips != null)
            {
                if (e.RowIndex != -1 && toolTips[e.RowIndex] != null)
                {
                    txtDescription.Enabled = true;
                    txtDescription.ReadOnly = false;
                    txtDescription.Text = toolTips[e.RowIndex].Trim();
                    txtDescription.ReadOnly = true;
                    //bDescription.Enabled = true;
                    if (!pbPreview.Enabled)
                    {
                        // enlarge text window
                        //                        txtDescription.Size = new System.Drawing.Size(txtDescription.Size.Width,txtDescription.Size.Height + pbPreview.Size.Height);
                    }
                }
                else
                {
                    //bDescription.Enabled = false;
                }
            }
        }

        private void dgSelect_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                bool bChecked = (bool)dgSelect.Rows[e.RowIndex].Cells[0].Value;
                if (!Multi && !bChecked) //&& dgSelect.Rows[e.RowIndex].Selected && bChecked)
                {
                    for (int i = 0; i < dgSelect.Rows.Count; i++)
                    {
                        //if (i == e.RowIndex)
                        //    continue;
                        if (dgSelect.Rows[i].Selected)
                            dgSelect.Rows[i].Cells[0].Value = true;
                        else
                            dgSelect.Rows[i].Cells[0].Value = false;
                    }
                }
            }
        }

        private void dgSelect_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 0 && e.RowIndex>-1)
            //{
            //    bool bChecked = (bool)dgSelect.Rows[e.RowIndex].Cells[0].Value;
            //    if (!Multi && bChecked) //&& dgSelect.Rows[e.RowIndex].Selected && bChecked)
            //    {
            //        for (int i = 0; i < dgSelect.Rows.Count; i++)
            //        {
            //            //if (i == e.RowIndex)
            //            //    continue;
            //            if (dgSelect.Rows[i].Selected)
            //                dgSelect.Rows[i].Cells[0].Value = true;
            //            else
            //                dgSelect.Rows[i].Cells[0].Value = false;
            //        }
            //    }
            //}

        }
	}
}