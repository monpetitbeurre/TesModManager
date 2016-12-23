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
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Color=System.Drawing.Color;

namespace OblivionModManager.Forms {
    public partial class DataFileBrowser : Form {
        [Flags]
        private enum TreeNodeType { Folder=1, BSA=2, Unparented=4, Parented=8, Any=0xFF }
        public class TreeViewSorter : System.Collections.IComparer {
            public int Compare(object a, object b) {
                TreeNode tna=(TreeNode)a;
                TreeNode tnb=(TreeNode)b;
                if((tna.Nodes.Count>0)!=(tnb.Nodes.Count>0)) return tna.Nodes.Count>0?-1:1;
                else return string.Compare(((TreeNode)a).Text, ((TreeNode)b).Text);
            }
        }
        public DataFileBrowser(bool ShowBSAs) {
            InitializeComponent();
            BSAs=ShowBSAs;
            BuildTreeView();
        }
        private bool BSAs;
        private TreeNode RootNode;
        private TreeNode FullBackup;

        private void BuildTreeView() {
            RootNode=new TreeNode(Program.currentGame.DataFolderPath);
            RootNode.Name=Program.currentGame.DataFolderPath;
            RootNode.Tag=TreeNodeType.Any;
            if(BSAs) {
                foreach(string file in Directory.GetFiles(Program.currentGame.DataFolderPath, "*.bsa")) {
                    string[] files=OblivionBSA.GetBSAEntries(file);
                    string tooltip="Contained in "+Path.GetFileName(file);
                    foreach(string s in files) AddNode(s, tooltip, TreeNodeType.BSA);
                }
            }
            foreach(string file in Directory.GetFiles(Program.currentGame.DataFolderPath, "*", SearchOption.AllDirectories)) {
                switch(Path.GetExtension(file)) {
                case ".esp":
                case ".esm":
                    continue;
                }
                string file2;
                if(Path.IsPathRooted(file)) file2=file.Substring((Program.currentGame.DataFolderPath+"\\").Length);
                else file2=file.Substring(5);
                DataFileInfo dfi=Program.Data.GetDataFile(file2);
                long len=(new FileInfo(file)).Length;
                if(dfi==null) {
                    AddNode(file2, "Unparented\nSize: "+len+" bytes", TreeNodeType.Unparented);
                } else {
                    AddNode(file2, "Parented to "+dfi.Owners+"\nSize: "+len+" bytes\nCRC: "+dfi.CRC.ToString("x"), TreeNodeType.Parented);
                }
            }
            FullBackup=(TreeNode)RootNode.Clone();
            treeView.Nodes.Add(RootNode);
            RootNode.Expand();
        }

        private void AddNode(string file, string tooltip, TreeNodeType type) {
            string[] path=file.Split(new char[] { '\\' });
            TreeNode parent=RootNode;
            for(int i=0;i<path.Length-1;i++) {
                if(parent.Nodes.ContainsKey(path[i])) {
                    parent=parent.Nodes.Find(path[i], false)[0];
                    parent.Tag=(TreeNodeType)parent.Tag|type;
                } else {
                    TreeNode temp=new TreeNode(path[i]);
                    temp.Name=path[i];
                    temp.Tag=TreeNodeType.Folder|type;
                    parent.Nodes.Add(temp);
                    parent=temp;
                }
            }
            TreeNode tn=new TreeNode(path[path.Length-1]);
            tn.ToolTipText=tooltip;
            switch(type) {
            case TreeNodeType.BSA: tn.ForeColor=Color.Blue; break;
            case TreeNodeType.Parented: tn.ForeColor=Color.Green; break;
            case TreeNodeType.Unparented: tn.ForeColor=Color.Red; break;
            }
            tn.Tag=type;
            if(parent==null) treeView.Nodes.Add(tn);
            else parent.Nodes.Add(tn);
        }

        private void bSort_Click(object sender, EventArgs e) {
            if(BSAs&&MessageBox.Show("Sorting will take a few minutes to complete.\nAre you sure you wish to continue?", "Warning",
                MessageBoxButtons.YesNo)!=DialogResult.Yes) return;
            treeView.TreeViewNodeSorter=new TreeViewSorter();
            treeView.TreeViewNodeSorter=null;
            FullBackup=(TreeNode)RootNode.Clone();
            bSort.Enabled=false;
        }

        private void cmbShow_SelectedIndexChanged(object sender, EventArgs e) {
            if(!BSAs) {
                if(cmbShow.SelectedIndex==1) {
                    MessageBox.Show("You must choose to include BSAs to use this option", "Error");
                    cmbShow.SelectedIndex=0;
                    return;
                }
            }
            TreeNodeType tnt;
            switch(cmbShow.SelectedIndex) {
            case 0: tnt=TreeNodeType.Any; break;
            case 1: tnt=TreeNodeType.BSA; break;
            case 2: tnt=TreeNodeType.Parented|TreeNodeType.Unparented; break;
            case 3: tnt=TreeNodeType.Parented; break;
            case 4: tnt=TreeNodeType.Unparented; break;
            case 5: tnt=TreeNodeType.Folder; break;
            default: tnt=TreeNodeType.Any; break;
            }
            treeView.Nodes.Clear();
            RootNode=FullBackup;
            FullBackup=(TreeNode)RootNode.Clone();
            Recurse(RootNode, tnt);
            treeView.Nodes.Add(RootNode);
            RootNode.Expand();
        }

        private bool Recurse(TreeNode tn,TreeNodeType type) {
            if((((TreeNodeType)tn.Tag)&type)==0) {
                tn.Remove();
                return true;
            }
            if(tn.Nodes.Count>0) {
                for(int i=0;i<tn.Nodes.Count;i++) { if(Recurse(tn.Nodes[i], type)) i--; }
            }
            return false;
        }


    }
}