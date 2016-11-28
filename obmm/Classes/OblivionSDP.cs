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
using File=System.IO.File;
using BinaryReader=System.IO.BinaryReader;
using BinaryWriter=System.IO.BinaryWriter;
using Stream=System.IO.Stream;
using MessageBox=System.Windows.Forms.MessageBox;
using MessageBoxButtons=System.Windows.Forms.MessageBoxButtons;
using DialogResult=System.Windows.Forms.DialogResult;
using Formatter=System.Runtime.Serialization.Formatters.Binary.BinaryFormatter;

namespace OblivionModManager.Classes {

    public static class OblivionSDP {
        private static readonly string EditedShaderFile = System.IO.Path.Combine(Program.BaseDir, @"EditedShaders");
        private static string Path=Program.DataFolderPath+"\\shaders\\shaderpackage";
        private const string Ext=".sdp";

        [Serializable]
        private class EditedShader {
            public readonly byte Package;
            public readonly string Name;
            public readonly byte[] OldData;
            public readonly uint CRC;
            public readonly string Mod;

            public EditedShader(byte package, string name, byte[] old, uint crc, string mod) {
                Package=package;
                Name=name;
                OldData=old;
                CRC=crc;
                Mod=mod;
            }
        }

        private static List<EditedShader> EditedShaders=null;

        private static void Deserialize() {
            if(!File.Exists(EditedShaderFile)) {
                EditedShaders=new List<EditedShader>();
                return;
            }
            Formatter f = new Formatter();
            Stream s = File.OpenRead(EditedShaderFile);
            try
            {
                EditedShaders = (List<EditedShader>)f.Deserialize(s);
                s.Close();
            }
            catch
            {
                s.Close();
                File.Copy(EditedShaderFile, EditedShaderFile + "bad",true);
                File.Delete(EditedShaderFile);
                EditedShaders = new List<EditedShader>();
            }
        }

        private static void Serialize() {
            Formatter f=new Formatter();
            Stream s=File.Create(EditedShaderFile);
            f.Serialize(s, EditedShaders);
            s.Close();
        }

        private static bool IsEdited(byte package, string shader, out string name) {
            foreach(EditedShader es in EditedShaders) {
                if(es.Package==package&&es.Name==shader) { name=es.Mod; return true; }
            }
            name=null;
            return false;
        }

        private static bool ReplaceShader(string file, string shader, byte[] newdata, uint CRC, out byte[] OldData) {
            DateTime timeStamp=File.GetLastWriteTime(file);
            File.Delete(Program.TempDir+"tempshader");
            File.Move(file, Program.TempDir+"tempshader");
            BinaryReader br=new BinaryReader(File.OpenRead(Program.TempDir+"tempshader"), System.Text.Encoding.Default);
            BinaryWriter bw=new BinaryWriter(File.Create(file), System.Text.Encoding.Default);
            bw.Write(br.ReadInt32());
            int num=br.ReadInt32();
            bw.Write(num);
            long sizeoffset=br.BaseStream.Position;
            bw.Write(br.ReadInt32());
            bool found=false;
            OldData=null;
            for(int i=0;i<num;i++) {
                char[] name=br.ReadChars(0x100);
                int size=br.ReadInt32();
                byte[] data=br.ReadBytes(size);
                bw.Write(name);
                string sname="";
                for(int i2=0;i2<100;i2++) { if(name[i2]=='\0') break; sname+=name[i2]; }
                sname=sname.ToLower();
                if(!found&&sname==shader&&(CRC==0||CompressionHandler.CRC(data)==CRC)) {
                    bw.Write(newdata.Length);
                    bw.Write(newdata);
                    found=true;
                    OldData=data;
                } else {
                    bw.Write(size);
                    bw.Write(data);
                }
            }
            bw.BaseStream.Position=sizeoffset;
            bw.Write(bw.BaseStream.Length-12);
            br.Close();
            bw.Close();
            File.Delete(Program.TempDir+"tempshader");
            File.SetLastWriteTime(file, timeStamp);
            return found;
        }

        public static bool EditShader(byte package, string name, string newshader, string mod) {
            Deserialize();
            bool result=false;
            name=name.ToLower();
            string path=Path+package.ToString().PadLeft(3, '0')+Ext;
            if(!File.Exists(newshader)) {
//                MessageBox.Show("Error editing shader package.\nFile '"+newshader+"' was not found", "Error");
                Program.logger.WriteToLog("Error editing shader package.\nFile '" + newshader + "' was not found", Logger.LogLevel.Error);
            }
            else if (!File.Exists(path))
            {
//                MessageBox.Show("Error editing shader package.\nFile '"+path+"' was not found", "Error");
                Program.logger.WriteToLog("Error editing shader package.\nFile '" + path + "' was not found", Logger.LogLevel.Error);
            }
            else
            {
                string oldmod;
                if(IsEdited(package, name, out oldmod)) {
                    if(MessageBox.Show("Shader '"+name+"' in file '"+path+"' has already been modified by '"+oldmod+"'\n"+
                        "Overwrite changes?", "Warning", MessageBoxButtons.YesNo)!=DialogResult.Yes) {
                        Serialize();
                        return false;
                    } else {
                        RestoreShaderInner(package, name);
                        omod o=Program.Data.GetMod(name);
                        if(o!=null) {
                            for(int i=0;i<o.SDPEdits.Count;i++) {
                                if(o.SDPEdits[i].Package==package&&o.SDPEdits[i].Shader==name) o.SDPEdits.RemoveAt(i--);
                            }
                        }
                    }
                }
                byte[] NewData=File.ReadAllBytes(newshader);
                byte[] OldData;
                if(ReplaceShader(path, name, NewData, 0, out OldData)) {
                    EditedShaders.Add(new EditedShader(package, name, OldData, CompressionHandler.CRC(NewData), mod));
                    result=true;
                } else {
//                    MessageBox.Show("Error editing shader package.\nCheck that the shader '"+name+"' exists", "Error");
                    Program.logger.WriteToLog("Error editing shader package.\nCheck that the shader '" + name + "' exists", Logger.LogLevel.Error);
                }
            }

            Serialize();
            return result;
        }

        private static void RestoreShaderInner(byte package, string name) {
            string path=Path+package.ToString().PadLeft(3, '0')+Ext;
            EditedShader removed=null;
            foreach(EditedShader es in EditedShaders) {
                if(es.Package==package&&es.Name==name) {
                    byte[] unused;
                    ReplaceShader(path, name, es.OldData, es.CRC, out unused);
                    removed=es;
                    break;
                }
            }
            if(removed!=null) EditedShaders.Remove(removed);
        }

        public static void RestoreShader(byte package, string name) {
            Deserialize();
            name=name.ToLower();
            RestoreShaderInner(package, name);
            Serialize();
        }
    }
}
