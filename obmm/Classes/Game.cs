using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OblivionModManager.Classes
{
    public class Game
    {
        public string Name { get; set; } = "";
        public string NexusName { get; set; } = "";
        public string NexusNameNoSpaces
        {
            get { return NexusName.Replace(" ", string.Empty); }
        }
        public int NexusID { get; set; } = -1;
        public string DataFolderName { get; set; } = "";
        public string DataFolderPath {
            get { return Path.Combine(GamePath, DataFolderName); }
            }
        public string IniBaseName { get; set; } = "";
        public string NickName { get; set; } = "";
        public string SaveFolder { get; set; } = "";
        public string ExeName { get; set; } = "";
        public string CreationKitExe { get; set; } = "";
        public string ScriptExtenderName { get; set; } = "";
        public string ScriptExtenderExe { get; set; } = "";
        public string ScriptExtenderDLL { get; set; } = "";
        public string GamePath { get; set; } = "";
        public string GraphicsExtenderPath { get; set; } = "";
        public int TMMNexusID { get; set; } = -1;
    }
}
