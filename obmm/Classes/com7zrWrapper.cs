using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace OblivionModManager
{
    public class comp7zrWrapper
    {
        private string zipname = "";
        private List<string> filelist = new List<string>();
        private bool bDone = false;
        private string exename = "7za.exe";
        private static string exepath = ""; // obmm\\7za.exe";

        public comp7zrWrapper(string archivename)
        {
            zipname = archivename;

            if (exepath == "")
            {
                if (File.Exists("c:\\program files\\7-Zip\\7z.exe"))
                    exepath = "c:\\program files\\7-Zip\\7z.exe";
                else
                {
                    string regkey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\7-Zip";
                    object value = Microsoft.Win32.Registry.GetValue(regkey, "Path", null);
                    if (value != null)
                    {
                        exepath = (value as string);
                        if (!File.Exists(Path.Combine(exepath, Path.GetFileName(exename))))
                        {
                            throw new Exception(exename + " not found. Make sure that 7z is installed.");
                        }
                        else
                            exepath = Path.Combine(exepath, Path.GetFileName(exename));
                    }
                    else
                    {
                        exepath = Path.Combine(Program.BaseDir,"7za.exe");
                    }
                }
            }

            if (File.Exists(archivename))
            {
                string cmd = "l \"" + archivename+"\"";
                string output=runcommandWithOutput(cmd);
                List<string> files = new List<string>();

                output=output.Replace("\n","");
                string[] lines = output.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

                bool bInListing = false;
                int filenameoffset = 0;
                foreach (string line in lines)
                {
                    if (line.StartsWith("---"))
                    {
                        bInListing = !bInListing;
                        if (bInListing)
                            filenameoffset = line.LastIndexOf(' ')+1;
                    }
                    else if (bInListing)
                    {
//                        string[] filedata = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        files.Add(line.Substring(filenameoffset));//filedata[filedata.Length - 1]);
                    }
                }

                filelist = files;
            }
        }

        public List<string> ArchiveFileNames
        {
            get { return filelist; }
        }
        public List<string> getFileList()
        {
            return filelist;
        }

        public void ExtractFile(string filename, Stream targetStream)
        {
            string temp = Path.GetTempPath();
            string targettemp = Path.Combine(temp,Path.GetFileName(filename));
            ExtractFile(filename, targettemp);
            FileInfo tempfile = new FileInfo(targettemp);
            targetStream.Write(File.ReadAllBytes(targettemp), 0, (int)tempfile.Length);
        }
        public void ExtractFile(string filename, string target)
        {
            string temp = Path.GetTempPath();
            string cmd = "x -y -o\"" + temp + "\" \"" + zipname + "\" \"" + filename + "\"";
            runcommandWithProgress(cmd);
            File.Delete(target);
            File.Copy(Path.Combine(temp, filename), target, true);
            File.Delete(Path.Combine(temp, filename));
        }

        public void ReplaceFile(string filename)
        {
            string cmd = "u -y \"" + zipname + "\" \"" + filename + "\"";
            runcommandWithProgress(cmd);
        }

        public void ExtractAllFiles(string targetDirectory)
        {
            string cmd = "x -y -o" + targetDirectory + " \"" + zipname + "\"";
            runcommandWithProgress(cmd);
        }

        public void ExtractFiles(string targetDirectory, string[]filenames)
        {
            string temp = Path.GetTempFileName();
            StreamWriter sw = new StreamWriter(temp);
            for (int i = 0; i < filenames.Length; i++)
            {
                sw.WriteLine(filenames[i]);
            }
            sw.Close();
            string cmd = "x -y -o" + targetDirectory + " \"" + zipname + "\" \"@"+temp+"\"";
            runcommandWithProgress(cmd);
        }
        public void CompressDirectory(string sourceDirectory)
        {
            string cmd = "a -y -t7z -mx=" + (Settings.CompressionBoost ? "9" : "7") + " \"" + zipname + "\" \"" + sourceDirectory + "\\*\"";
            filelist = new List<string>(Directory.GetFiles(sourceDirectory,"*",SearchOption.AllDirectories));
            runcommandWithProgress(cmd);
        }

        private string runcommandWithOutput(string cmd)
        {
            string output = "";
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = exepath;
            psi.Arguments = cmd;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;

            Process p = new Process();
            p.StartInfo = psi;
            p.EnableRaisingEvents = true;
            p.Exited += new EventHandler(done);

            bDone = false;
            p.Start();
            StreamReader stdout = p.StandardOutput;

            string line = "";
            while (!bDone)
            {
                Application.DoEvents();
                Thread.Sleep(10);
                line=stdout.ReadLine();
                output += line + "\r\n";
            }
            output += stdout.ReadToEnd();
//            Console.WriteLine("7zr output = " + output);

            return output;
        }

        private void runcommandWithProgress(string cmd)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = exepath;
            psi.Arguments = cmd;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;

            Process p = new Process();
            p.StartInfo = psi;
            p.EnableRaisingEvents = true;
            p.Exited += new EventHandler(done);

            bDone=false;
            p.Start();
            StreamReader stdout = p.StandardOutput;
            StreamReader stderr = p.StandardError;


            int linecount = 0;
            while (!bDone)
            {
                Application.DoEvents();
                Thread.Sleep(10);
                if (stdout.Peek()>-1)
                {
                    Console.WriteLine(linecount + " 7zr status = " + stdout.ReadLine());
                    linecount++;

                    if (filelist.Count != 0)
                    {
                        int percentdone = (linecount * 100) / filelist.Count;
                        ProgressEventArgs e = new ProgressEventArgs(percentdone);
                        OnProgress(e);
                    }
                }
            }
            if (stdout.Peek() > -1)
            {
//                string output=stdout.ReadToEnd();
//                Console.WriteLine(" stdout=" + output);
            }
            if (stderr.Peek() > -1)
            {
                Console.WriteLine(" stderr=" + stderr.ReadToEnd());
            }
            OnProgress(new ProgressEventArgs(100));
        }
        private void done(object o, EventArgs e)
        {
            bDone = true;
        }

        public void Dispose()
        {
        }


        public class ProgressEventArgs : EventArgs
        {
            private int progress = 0;
            // Constructor.
            public ProgressEventArgs(int progress)
            {
                this.progress = progress;
            }
            public int Progress
            {
                get { return this.progress; }
            }
        }

        public delegate void ProgressEventHandler(object sender, ProgressEventArgs e);
        public event ProgressEventHandler Progress;
        protected virtual void OnProgress(ProgressEventArgs e)
        {
            if (Progress != null) // is there a handler attached?
                Progress(this, e);
        }

    }
}
