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
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;

namespace OblivionModManager {
    public partial class SaveForm : Form {
        public static string SaveFolder=null;
        public int ActiveEsps;

        private class SaveFile {
            public DateTime saved;
            public string FileName;
            public string Player;
            public string Location;
            public string race;
            public string gametime;

            //public byte[] face;
            //public int FaceOffset;

            public byte[] ImageData;
            public int ImageWidth;
            public int ImageHeight;
            public string[] plugins;
            private Bitmap image;
            public Bitmap Image {
                get {
                    if (image != null)
                    {
                        // test if data is valid. If not fall through to reload image
                        try
                        {
                            int width = image.Width;
                            return image;
                        }
                        catch { };
                    }
                    if (ImageData == null)
                    {
                        // empty image
                        image = new Bitmap(640, 480, PixelFormat.Format24bppRgb);
                    }
                    else
                    {
                        try
                        {
                            image = new Bitmap(ImageWidth, ImageHeight, PixelFormat.Format24bppRgb);
                            BitmapData bd = image.LockBits(new Rectangle(0, 0, ImageWidth, ImageHeight), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                            System.Runtime.InteropServices.Marshal.Copy(ImageData, 0, bd.Scan0, ImageData.Length);
                            image.UnlockBits(bd);
                        }
                        catch
                        {
                            image = new Bitmap(640, 480, PixelFormat.Format24bppRgb);
                        }
                    }
                    return image;
                }
            }
        }

        public enum SaveSortOrder { Name, Player, Location, Date, FileSize }
        public class SaveListSorter : System.Collections.IComparer {
            public static SaveSortOrder order=SaveSortOrder.Name;
            public static bool ascending = true;
            public int Compare(object a, object b) {
                SaveFile sa=(SaveFile)((ListViewItem)a).Tag;
                SaveFile sb=(SaveFile)((ListViewItem)b).Tag;
                switch(order) {
                case SaveSortOrder.Name:
                    return string.Compare(sa.FileName, sb.FileName)*(ascending?1:-1);
                case SaveSortOrder.Player:
                    return string.Compare(sa.Player, sb.Player) * (ascending ? 1 : -1);
                case SaveSortOrder.Location:
                    return string.Compare(sa.Location, sb.Location) * (ascending ? 1 : -1);
                case SaveSortOrder.Date:
                    return DateTime.Compare(sa.saved, sb.saved) * (ascending ? 1 : -1);
                case SaveSortOrder.FileSize:
                    long sizea=(new System.IO.FileInfo(SaveFolder+sa.FileName)).Length;
                    long sizeb=(new System.IO.FileInfo(SaveFolder+sb.FileName)).Length;
                    if(sizea==sizeb) return 0;
                    if(sizea>sizeb) return -1; else return 1;
                default: return 0;
                }
            }
        }

        private readonly List<SaveFile> saves=new List<SaveFile>();

        private void FindSaveFolder() {
            if(INI.GetINIValue("[general]", "bUseMyGamesDirectory")=="0") {
                SaveFolder=Path.Combine(Program.currentGame.GamePath,"saves");
            } else {
                string saveSubDir = "";

                if (!string.IsNullOrWhiteSpace(INI.GetINIValue("[general]", "SLocalSavePath")))
                {
                    saveSubDir = INI.GetINIValue("[general]", "SLocalSavePath");
                }
                else
                    saveSubDir = "saves";

                SaveFolder =Path.Combine(Program.INIDir,saveSubDir);
            }
        }

        private void CountActiveEsps() {
            ActiveEsps=0;
            foreach(EspInfo ei in Program.Data.Esps) {
                if(ei.Active) ActiveEsps++;
            }
        }

        SaveFile DecodeSaveFile(string file)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(file));
            if (br.BaseStream.Length < 12)
            {
                br.Close();
                return null;
            }
            string s = "";
            for (int i = 0; i < 13; i++) s += (char)br.ReadByte();
            if (!s.StartsWith("TES"))
            {
                br.Close();
                return null;
            }
            SaveFile sf = new SaveFile();
            sf.FileName = Path.GetFileName(file);

            if (s.StartsWith("TESV_SAVEGAME"))
            {
                br.BaseStream.Position = 0;
                string magic = Program.ReadBString(br, "TESV_SAVEGAME".Length);
                if (magic != "TESV_SAVEGAME")
                {
                    return null;
                }
                uint headerSize = br.ReadUInt32();

                // header
                uint version = br.ReadUInt32();
                uint saveNumber = br.ReadUInt32();
                int b = br.ReadInt16();
                sf.Player = ""; // br.ReadString();
                for (int i = 0; i < b; i++) sf.Player += (char)br.ReadByte();
                uint level = br.ReadUInt32(); //Read the level
                b = br.ReadInt16();
                sf.Location = "";
                for (int i = 0; i < b; i++) sf.Location += (char)br.ReadByte();
                b = br.ReadInt16();
                sf.gametime = "";
                for (int i = 0; i < b; i++) sf.gametime += (char)br.ReadByte();
                b = br.ReadInt16();
                sf.race = "";
                for (int i = 0; i < b; i++) sf.race += (char)br.ReadByte();

                int sex = br.ReadUInt16(); // 1=female, 0=male

                //br.ReadInt16();
                float currentExperience = (float)br.ReadUInt32();
                //br.ReadInt32();
                float levelUpExperience = (float)br.ReadUInt32();
                // br.ReadInt32();

                sf.saved = DateTime.FromFileTime(br.ReadInt64());
                sf.ImageWidth = (int)br.ReadUInt32();
                sf.ImageHeight = (int)br.ReadUInt32();
                // end header

                // br.BaseStream.Position = headerSize + 4 + "TESV_SAVEGAME".Length;
                sf.ImageData = new byte[sf.ImageHeight * sf.ImageWidth * 3];
                int bpp = (version == 9 ? 24 : 32);
                byte[] imageData = new byte[sf.ImageHeight * sf.ImageWidth * bpp/8];

                if (version == 12)
                {
                    uint unknown = br.ReadUInt16();
                }

                br.Read(imageData, 0, imageData.Length);
                
                for (int i = 0; i < sf.ImageWidth * sf.ImageHeight; i++)
                {
                    sf.ImageData[i * 3 + 1] = imageData[i * bpp / 8 + 1];
                    if (version == 9) // image is BGR instead of RGB
                    {
                        sf.ImageData[i * 3] = imageData[i * bpp / 8 + 2];
                        sf.ImageData[i * 3 + 2] = imageData[i * bpp / 8];
                    }
                    else
                    {
                        sf.ImageData[i * 3] = imageData[i * bpp / 8];
                        sf.ImageData[i * 3 + 2] = imageData[i * bpp / 8 + 2];
                    }
                }
                int formVersion = br.ReadByte();
                if (version == 12)
                {
                    // some bytes/words to go. What are they?
                    uint un = br.ReadUInt32();
                    uint deux = br.ReadUInt32();
                    uint trois = br.ReadUInt16();
                }
                uint pluginInfoSize = br.ReadUInt32();
                sf.plugins = new string[br.ReadByte()];
                for (int i = 0; i < sf.plugins.Length; i++)
                {
                    b = br.ReadInt16();
                    sf.plugins[i] = "";
                    for (int j = 0; j < b; j++) sf.plugins[i] += (char)br.ReadByte();
                }
            }
            else if (s.StartsWith("TES3"))
            {
                try
                {
                    sf.saved = (new FileInfo(file)).LastWriteTime;
                    br.BaseStream.Position = 0;
                    uint size;
                    s = OblivionModManager.Program.ReadBString(br, 4);
                    if (s != "TES3") return null;
                    size = br.ReadUInt32();
                    br.BaseStream.Position += 8;
                    s = OblivionModManager.Program.ReadBString(br, 4); // should be HEDR
                    size = br.ReadUInt32();
                    uint fversion = br.ReadUInt32(); // supposed to be a 4bytes float
                    uint fournull = br.ReadUInt32();
                    Program.ReadBString(br, 32);
                    Program.ReadBString(br, 260);
                    List<string> pluginsList = new List<string>();
                    s = OblivionModManager.Program.ReadBString(br, 4); // should be MAST
                    while (s == "MAST")
                    {
                        size = br.ReadUInt32(); // master file name length
                        string plugin = Program.ReadBString(br, (int)size - 1);
                        pluginsList.Add(plugin);
                        Program.ReadBString(br, 1); // read the terminating NULL character
                        s = OblivionModManager.Program.ReadBString(br, 4); // should be DATA
                        size = br.ReadUInt32(); // Data length
                        double data = br.ReadDouble(); // data itself
                        s = OblivionModManager.Program.ReadBString(br, 4); // should be DATA
                    }
                    sf.plugins = new string[pluginsList.Count];
                    for (int curPlugin = 0; curPlugin < pluginsList.Count; curPlugin++)
                    {
                        sf.plugins[curPlugin] = pluginsList[curPlugin];
                    }
                    if (s == "GMDT")
                    {
                        size = br.ReadUInt32();
                        br.ReadUInt32();
                        br.ReadUInt32();
                        br.ReadUInt32();
                        br.ReadUInt32();
                        br.ReadUInt32();
                        br.ReadUInt32();
                        sf.Location = Program.ReadBString(br, 66);
                        sf.Location = sf.Location.Substring(0, sf.Location.IndexOf('\0'));
                        br.ReadUInt16();
                        sf.Player = Program.ReadBString(br, 32);
                        sf.Player = sf.Player.Substring(0, sf.Player.IndexOf('\0'));
                    }
                    s = OblivionModManager.Program.ReadBString(br, 4);
                    if (s == "SCRD")
                    {
                        size = br.ReadUInt32();
                        Program.ReadBString(br, (int)size);
                        sf.ImageWidth = 128;
                        sf.ImageHeight = 128;
                    }
                    s = OblivionModManager.Program.ReadBString(br, 4);
                    if (s == "SCRS")
                    {
                        size = br.ReadUInt32();
                        // Program.ReadBString(br, (int)size);
                        byte[] imageData = new byte[size];
                        sf.ImageData = new byte[sf.ImageHeight * sf.ImageWidth * 3];
                        br.Read(imageData, 0, imageData.Length);
                        int bpp = 32;
                        for (int i = 0; i < sf.ImageHeight * sf.ImageWidth; i++) // in pixels
                        {
                            sf.ImageData[i * 3 + 1] = imageData[i * bpp / 8 + 1];
                            //if (version == 9) // image is BGR instead of RGB
                            //{
                            //    sf.ImageData[i * 3] = imageData[i * bpp / 8 + 2];
                            //    sf.ImageData[i * 3 + 2] = imageData[i * bpp / 8];
                            //}
                            //else
                            {
                                sf.ImageData[i * 3] = imageData[i * bpp / 8];
                                sf.ImageData[i * 3 + 2] = imageData[i * bpp / 8 + 2];
                            }
                        }
                    }
                }
                finally
                {
                    br.Close();
                }
            }
            else
            {
                br.BaseStream.Position = 0;
                string magic = Program.ReadBString(br, "TES4SAVEGAME".Length);
                if (magic != "TES4SAVEGAME")
                {
                    return null;
                }

                br.BaseStream.Position = 42;

                byte b = br.ReadByte();
                sf.Player = "";
                for (int i = 1; i < b; i++) sf.Player += (char)br.ReadByte();
                br.ReadByte(); //Read the terminating \0
                br.ReadInt16();
                b = br.ReadByte();
                sf.Location = "";
                for (int i = 1; i < b; i++) sf.Location += (char)br.ReadByte();
                br.ReadByte(); //Read the terminating \0
                br.ReadSingle();
                br.ReadInt32();
                short year, month, day, hour, minute, second;
                year = br.ReadInt16();
                month = br.ReadInt16();
                br.ReadInt16();
                day = br.ReadInt16();
                hour = br.ReadInt16();
                minute = br.ReadInt16();
                second = br.ReadInt16();
                br.ReadInt16();
                sf.saved = new DateTime(year, month, day, hour, minute, second);

                br.ReadInt32();
                sf.ImageWidth = br.ReadInt32();
                sf.ImageHeight = br.ReadInt32();
                sf.ImageData = new byte[sf.ImageHeight * sf.ImageWidth * 3];
                br.Read(sf.ImageData, 0, sf.ImageData.Length);
                //Flip the blue and red channels
                for (int i = 0; i < sf.ImageWidth * sf.ImageHeight; i++)
                {
                    byte temp = sf.ImageData[i * 3];
                    sf.ImageData[i * 3] = sf.ImageData[i * 3 + 2];
                    sf.ImageData[i * 3 + 2] = temp;
                }
                sf.plugins = new string[br.ReadByte()];
                for (int i = 0; i < sf.plugins.Length; i++)
                {
                    b = br.ReadByte();
                    sf.plugins[i] = "";
                    for (int j = 0; j < b; j++) sf.plugins[i] += (char)br.ReadByte();
                }
                /*{
                    br.ReadInt32();
                    int changerecs=br.ReadInt32();
                    br.BaseStream.Position+=32;
                    br.BaseStream.Position+=br.ReadUInt16()*8;  //globals
                    br.BaseStream.Position+=br.ReadUInt16();    //Class data
                    br.BaseStream.Position+=br.ReadUInt16();    //prcoesses data
                    br.BaseStream.Position+=br.ReadUInt16();    //spectator data
                    br.BaseStream.Position+=br.ReadUInt16();    //weather
                    br.ReadInt32();
                    for(int i=0;i<br.ReadInt32();i++) {
                        s=""+(char)br.ReadByte()+(char)br.ReadByte()+(char)br.ReadByte()+(char)br.ReadByte();
                        if(s=="GRUP") br.BaseStream.Position+=br.ReadInt32()-4;
                        else br.BaseStream.Position+=br.ReadInt32()+12;
                    }
                    br.BaseStream.Position+=br.ReadUInt16();    //Quickkeys
                    br.BaseStream.Position+=br.ReadUInt16();    //HUD
                    br.BaseStream.Position+=br.ReadUInt16();    //Interface
                    br.BaseStream.Position+=br.ReadUInt16();    //Regions
                    for(int i=0;i<6;i++) {
                        int formid=br.ReadInt32();
                        br.BaseStream.Position+=6;
                        //br.BaseStream.Position+=10;
                        br.BaseStream.Position+=br.ReadUInt16();
                    }
                    uint playerid=br.ReadUInt32();
                    byte type=br.ReadByte();
                    br.BaseStream.Position+=5;
                    ushort playersize=br.ReadUInt16();
                    int upto=0;
                    for(int i=0;i<playersize;i++) {
                        if(br.ReadByte()==(byte)sf.Player[upto]) {
                            if(++upto==sf.Player.Length) {
                                sf.FaceOffset=(int)br.BaseStream.Position-(sf.Player.Length+542);
                                br.BaseStream.Position=sf.FaceOffset;
                                sf.face=br.ReadBytes(520);
                            }
                        } else upto=0;
                    }
                }*/
            }
            br.Close();
            return sf;
        }
        public SaveForm() {
            if(SaveFolder==null) FindSaveFolder();
            InitializeComponent();
            cmbSort.SelectedIndex = (int)SaveSortOrder.Date;
            lvSaves.ListViewItemSorter=new SaveListSorter();
            CountActiveEsps();
            foreach(string file in Directory.GetFiles(SaveFolder)) {
                SaveFile sf = DecodeSaveFile(file);
                if (sf != null) saves.Add(sf);
            }
            UpdateSaveList();
        }

        private void UpdateSaveList() {
            lvSaves.OwnerDraw = true;
            lvSaves.SuspendLayout();
            lvSaves.Items.Clear();
            lvSaves.BeginUpdate();
            foreach(SaveFile sf in saves) {
                ListViewItem lvi=new ListViewItem(sf.FileName);

                lvi.SubItems.Add(sf.Player);
                lvi.SubItems.Add(sf.Location);
                lvi.SubItems.Add(sf.saved.ToString());
                lvi.SubItems.Add(new FileInfo(Path.Combine(SaveFolder,sf.FileName)).Length.ToString());

                lvi.ToolTipText="Player: "+sf.Player+"\nLocation: "+sf.Location+
                    "\nDate saved: "+sf.saved.ToString()+"\nNumber of plugins: "+sf.plugins.Length.ToString();
                lvi.Tag=sf;
                bool match=false;
                for(int i=0;i<sf.plugins.Length;i++) {
                    EspInfo ei=Program.Data.GetEsp(sf.plugins[i]);
                    if(ei==null||!ei.Active) {
                        lvi.ImageIndex=1;
                        match=true;
                        break;
                    }
                }
                if(!match) {
                    if(ActiveEsps>sf.plugins.Length) lvi.ImageIndex=2;
                    else lvi.ImageIndex=3;
                }
                lvSaves.Items.Add(lvi);
            }
            lvSaves.EndUpdate();
            lvSaves.ResumeLayout();
            lvSaves.OwnerDraw = false;
        }

        private void UpdatePluginList(string[] plugins) {
            lvPlugins.SuspendLayout();
            lvPlugins.Items.Clear();
            foreach (string s in plugins)
            {
                ListViewItem lvi=new ListViewItem(s);
                EspInfo ei=Program.Data.GetEsp(s);
                if(ei==null) {
                    lvi.ImageIndex=0;
                    lvi.ToolTipText="File not found";
                } else {
                    if(ei.Active) lvi.ImageIndex=3;
                    else lvi.ImageIndex=1;
                    lvi.Tag=ei;
                    lvi.ToolTipText=ei.FileName+"\nAuthor: "+ei.header.Author+"\n\n"+ei.header.Description;
                }
                lvPlugins.Items.Add(lvi);
            }

            lvPlugins.ResumeLayout();
        }

        private void lvSaves_SelectedIndexChanged(object sender, EventArgs e) {
            if(lvSaves.SelectedItems.Count!=1) return;
            SaveFile sf=(SaveFile)lvSaves.SelectedItems[0].Tag;
            lName.Text="Name: "+sf.Player;
            if (sf.race != null && sf.race.Length > 0) lName.Text += " (" + sf.race + ")";
            lLocation.Text="Location: "+sf.Location;
            lDate.Text="Date saved: "+sf.saved.ToString();
            if (sf.gametime != null && sf.gametime.Length > 0)
                lGametime.Text = "Game time: " + sf.gametime;
            else
                lGametime.Text = "";
            UpdatePluginList(sf.plugins);
            //if (pictureBox1.Image != null)
            //{
            //    pictureBox1.Image.Dispose();
            //    pictureBox1.Image = null;
            //}
            try
            {
                pictureBox1.Image = sf.Image;
            }
            catch { }
        }

        private void cmbSort_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled=true;
        }

        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e) {
            SaveListSorter.order=(SaveSortOrder)cmbSort.SelectedIndex;
            //SaveListSorter.ascending = (lvSaves.Sorting == SortOrder.Ascending);
            lvSaves.Sort();
        }

        private void bSync_Click(object sender, EventArgs e) {
            if(lvSaves.SelectedItems.Count!=1) return;
            if(lvSaves.SelectedItems[0].ImageIndex==0) {
                if(MessageBox.Show("One or more plugins required by that save could not be found.\n"+
                    "Sync anyway?", "Warning", MessageBoxButtons.YesNo)!=DialogResult.Yes) return;
            }
            ListViewItem saveEntry = lvSaves.SelectedItems[0];
            foreach(EspInfo ei in Program.Data.Esps) ei.Active=false;
            foreach(string s in ((SaveFile)lvSaves.SelectedItems[0].Tag).plugins) {
                EspInfo ei=Program.Data.GetEsp(s);
                if(ei!=null) ei.Active=true;
            }
            CountActiveEsps();
            UpdatePluginList(((SaveFile)lvSaves.SelectedItems[0].Tag).plugins);


/////////////////////////////////////////////////////

            // change order
            if (Program.currentGame.NickName.StartsWith("skyrim"))
            {
                List<string> oldloadorderlist = new List<string>(Program.loadOrderList);
                string[] plugins = ((SaveFile)lvSaves.SelectedItems[0].Tag).plugins;
                foreach (string s in plugins)
                {
                    oldloadorderlist.Remove(s.ToLower());
                }
                // change the loadOrderList
                string[] oldlist = new string[Program.loadOrderList.Count];
                Program.loadOrderList.CopyTo(oldlist);
                Program.loadOrderList.Clear();

                for (int i = 0; i < plugins.Length; i++)
                {
                    Program.loadOrderList.Add(plugins[i]);
                }
                // add any missing
                foreach (string s in oldloadorderlist)
                {
                    Program.loadOrderList.Add(s);
                }

                // rewrite loadorder.txt
                StreamWriter sw = null;
                try
                {
                    if (!Settings.NeverTouchLoadOrder)
                    {
                        sw = new StreamWriter(OblivionESP.loadorder, false, (Settings.bLoadOrderAsUTF8 ? System.Text.Encoding.UTF8 : System.Text.Encoding.Default));
                        foreach (string s in Program.loadOrderList)
                        {
                            sw.WriteLine(s);
                        }
                        sw.Close();
                    }
                }
                catch { }
                finally { if (sw != null) sw.Close(); }


            }
            else
            {
                // Oblivion
                string[] esps = OblivionESP.GetActivePlugins();
                ListView lvEspList2 = Program.lvEspList;
                Program.lvEspList = new ListView();

                // first move Skyrim.esm (or Oblivion.esm)
                foreach (ListViewItem lvi in lvEspList2.Items)
                {
                    string modname = lvi.Text.ToLower();
                    if (modname == Program.currentGame.NickName+".esm")
                    {
                        lvEspList2.Items.Remove(lvi);
                        Program.lvEspList.Items.Add(lvi);
                    }
                }

                // reinsert in right order
                foreach (string s in Program.loadOrderList)
                {
                    foreach (ListViewItem lvi in lvEspList2.Items)
                    {
                        string modname = lvi.Text.ToLower();
                        if (modname == s)
                        {
                            lvEspList2.Items.Remove(lvi);
                            Program.lvEspList.Items.Add(lvi);
                        }
                    }
                }
                // not everybody? (how can that be?
                if (Program.lvEspList.Items.Count < lvEspList2.Items.Count)
                {
                    // add the missing ones
                    foreach (ListViewItem lvi in lvEspList2.Items)
                    {
                        string modname = lvi.Text.ToLower();
                        if (!Program.lvEspList.Items.Contains(lvi))
                        {
                            lvEspList2.Items.Remove(lvi);
                            Program.lvEspList.Items.Add(lvi);
                        }
                    }
                }
            }
            Program.Data.SortEsps();
///////////// done trying to change load order

            UpdateSaveList();
            saveEntry.Selected = true;
        }

        private void btnActivateMissingMods_Click(object sender, EventArgs e)
        {
            if (lvSaves.SelectedItems.Count != 1) return;
            if (lvSaves.SelectedItems[0].ImageIndex == 0)
            {
                if (MessageBox.Show("One or more plugins required by that save could not be found.\n" +
                    "Sync anyway?", "Warning", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            }
            foreach (string s in ((SaveFile)lvSaves.SelectedItems[0].Tag).plugins)
            {
                
                EspInfo ei = Program.Data.GetEsp(s);
                if (ei != null)
                    ei.Active = true;
                else
                {
                    bool bFound = false;
                    string lowers = s.ToLower();
                    foreach (omod o in Program.Data.omods)
                    {
                        //DataFileInfo dfi = (from file in o.AllDataFiles
                        //                    select file);
                        foreach (string plugin in o.Plugins)
                        {
                            if (plugin.ToLower() == lowers)
                            {
                                // found the mod. Activate!
                                o.Activate(true);
                                bFound = true;
                                break;
                            }
                        }
                        if (bFound) break;
                    }
                    if (!bFound)
                    {
                        if (DialogResult.Cancel == MessageBox.Show("Could not find the mod for " + s + ". Continue?", "Mod not found", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                            break;
                    }
                }
            }
            CountActiveEsps();
            UpdatePluginList(((SaveFile)lvSaves.SelectedItems[0].Tag).plugins);

            // sync plugin order
            bSync_Click(null, null);
       }

        private void radList_CheckedChanged(object sender, EventArgs e)
        {
            lvSaves.View = View.List;
            lvPlugins.View = View.List;
        }

        private void radDetails_CheckedChanged(object sender, EventArgs e)
        {
            lvSaves.View = View.Details;
            lvPlugins.View = View.Details;
        }

        private void lvSaves_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            
            if (e.Column== SaveDateSavedColumn.Index)
            {
                if (cmbSort.SelectedIndex == (int)SaveSortOrder.Date)
                    SaveListSorter.ascending = !SaveListSorter.ascending;
                else
                    cmbSort.SelectedIndex = (int)SaveSortOrder.Date;
            }
            else if (e.Column== SaveFileNameColumn.Index)
            {
                if (cmbSort.SelectedIndex == (int)SaveSortOrder.Name)
                    SaveListSorter.ascending = !SaveListSorter.ascending;
                else
                    cmbSort.SelectedIndex = (int)SaveSortOrder.Name;
            }
            else if (e.Column== SaveLocationColumn.Index)
            {
                if (cmbSort.SelectedIndex == (int)SaveSortOrder.Location)
                    SaveListSorter.ascending = !SaveListSorter.ascending;
                else
                    cmbSort.SelectedIndex = (int)SaveSortOrder.Location;
            }
            else if (e.Column== SavePlayerNameColumn.Index)
            {
                if (cmbSort.SelectedIndex == (int)SaveSortOrder.Player)
                    SaveListSorter.ascending = !SaveListSorter.ascending;
                else
                    cmbSort.SelectedIndex = (int)SaveSortOrder.Player;
            }
            else if (e.Column== SaveFileSizeColumn.Index)
            {
                if (cmbSort.SelectedIndex == (int)SaveSortOrder.FileSize)
                    SaveListSorter.ascending = !SaveListSorter.ascending;
                else
                    cmbSort.SelectedIndex = (int)SaveSortOrder.FileSize;
            }
            lvSaves.Sort();
        }
    }
}