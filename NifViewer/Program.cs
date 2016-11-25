using System;
using Path=System.IO.Path;
using File=System.IO.File;
using Directory=System.IO.Directory;
using Registry=Microsoft.Win32.Registry;

namespace NifViewer {
    public static class Program {
        public static string OblivionPath=null;

        [STAThread]
        public static int Main(string[] args) {
            Directory.SetCurrentDirectory(System.Windows.Forms.Application.StartupPath);
            string initialmesh;
            if(args.Length>0) initialmesh=args[0]; else initialmesh=null;
            OblivionPath=Path.GetFullPath(Path.Combine(System.Windows.Forms.Application.StartupPath,"..\\data"));
            INIFile INI;
            try { INI=new INIFile("NifViewer.ini"); } catch { INI=null; }
            if(INI!=null) return BasicHLSL.Run(INI.GetInt("adapter", 0), INI.GetInt("aa", 0), INI.GetInt("af", 0),initialmesh);
            else return BasicHLSL.Run(0,0,0,initialmesh);
        }
    }
}
