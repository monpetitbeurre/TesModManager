using System;
using System.Collections.Generic;
//using System.IO;
using System.Linq;
using System.Text;

namespace OblivionModManager.MonoFS
{
    public static class MonoFS
    {
        static bool bFSIsCaseSensitive=false;

        static MonoFS()
        {
            isFSCaseSensitive();
        }

        public static bool isFSCaseSensitive()
        {
            string file = Program.CurrentDir + Guid.NewGuid().ToString().ToLower();
            File.WriteAllText(file,"");
            bFSIsCaseSensitive = !File.Exists(file.ToUpper());
            File.Delete(file);

            return bFSIsCaseSensitive;
        }

        /// <summary>
        /// returns the correct casing for a given filename and path if that file already exists
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CorrectCasing(string dir, string name)
        {
            string CorrectName = "";

#if MONO
            if (bFSIsCaseSensitive)
            {
                if (name.Contains("/"))
                {
                    string shortername = name.Substring(name.IndexOf('/') + 1);
                    string dirname = name.Substring(0, name.IndexOf('/'));
                    string newdir = "";
                    string cmpdir = dir.ToLower() + (!dir.EndsWith("/") ? "/" : "") + dirname.ToLower();
                    string tmpdir = "";

                    foreach (string dir2 in System.IO.Directory.GetDirectories(dir))
                    {
                        tmpdir = dir2.Replace("\\", "/");
                        if (tmpdir.ToLower().Equals(cmpdir))
                        {
                            newdir = tmpdir;
                            break;
                        }
                    }
                    if (newdir.Length != 0)
                        CorrectName = CorrectCasing(newdir, shortername);
                    else
                        CorrectName = dir + "/" + name;

                }
                else
                {
                    string filename = "";
                    foreach (string file in System.IO.Directory.GetFiles(dir))
                    {
                        filename = file.Replace("\\", "/");
                        if (filename.ToLower().Equals(dir + "/" + name.ToLower()))
                        {
                            if (filename.StartsWith("./")) filename = filename.Substring(2);
                            CorrectName = filename;
                            break;
                        }
                    }
                    if (CorrectName.Length == 0)
                    {
                        string tmpdir = "";

                        foreach (string dir2 in System.IO.Directory.GetDirectories(dir))
                        {
                            tmpdir = dir2.Replace("\\", "/");
                            if (tmpdir.ToLower().Equals(dir.ToLower() + "/" + name.ToLower()))
                            {
                                if (tmpdir.StartsWith("./")) tmpdir = tmpdir.Substring(2);
                                CorrectName = tmpdir;
                                break;
                            }
                        }
                    }
                }
            }
#endif
            if (CorrectName.Length == 0) CorrectName = dir + "/" + name;
            return CorrectName;
        }
        /// <summary>
        /// Returns the correct casing for a filename
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetCorrectCasing(string name)
        {
            string correctname = "";
#if MONO
            if (bFSIsCaseSensitive)
            {
                if (name.Contains("\\")) name = name.Replace("\\", "/");

                if (name.EndsWith("/")) name = name.Substring(0, name.Length - 1);

                if (name.Contains(":"))
                    correctname = CorrectCasing(name.Substring(0, 3), name.Substring(3, name.Length - 3));
                else if (name.StartsWith("/"))
                    correctname = CorrectCasing("/", name);
                else
                    correctname = CorrectCasing(".", name);

                if (name.EndsWith("/")) correctname += "/";

                // it will not hurt but it does not 100% match the source so let's remove the prefix
                if (!name.StartsWith("./")) correctname = correctname.Replace("./", "");

                if (!correctname.ToLower().Equals(name.ToLower())) System.Windows.Forms.MessageBox.Show("Casing conversion failed: (before)" + name + "!=" + correctname + "(after)");
            }
#else
            correctname = name;
#endif
            return correctname;
        }
    }
    public static class File
    {

        static public bool Exists(string name)
        {
#if MONO
            name = MonoFS.GetCorrectCasing(name);
#endif
            return (System.IO.File.Exists(name));
        }

        static public System.IO.FileStream Create(string name)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return System.IO.File.Create(name);
        }

        static public System.IO.FileStream OpenRead(string name)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return System.IO.File.OpenRead(name);
        }

        static public System.IO.FileStream OpenWrite(string name)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return System.IO.File.OpenWrite(name);
        }

        static public System.IO.FileStream Open(string name, System.IO.FileMode mode)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return System.IO.File.Open(name, mode);
        }

        static public System.IO.FileStream Open(string name, System.IO.FileMode mode, System.IO.FileAccess access)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return System.IO.File.Open(name, mode, access);
        }

        static public void WriteAllLines(string name, string[] args)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            System.IO.File.WriteAllLines(name, args);
        }
        static public string[] ReadAllLines(string name, System.Text.Encoding encoding)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return System.IO.File.ReadAllLines(name, encoding);
        }
        static public string[] ReadAllLines(string name)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return System.IO.File.ReadAllLines(name);
        }
        static public byte[] ReadAllBytes(string name)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return System.IO.File.ReadAllBytes(name);
        }
        static public void WriteAllBytes(string name, byte[] data)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            System.IO.File.WriteAllBytes(name, data);
        }
        static public string ReadAllText(string name, System.Text.Encoding encoding)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return System.IO.File.ReadAllText(name, encoding);
        }
        static public string ReadAllText(string name)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return System.IO.File.ReadAllText(name);
        }
        static public void WriteAllText(string name, string str)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            System.IO.File.WriteAllText(name, str);
        }
        static public void WriteAllText(string name, string str, System.Text.Encoding encoding)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            System.IO.File.WriteAllText(name, str, encoding);
        }
        static public DateTime GetLastWriteTime(string name)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return System.IO.File.GetLastWriteTime(name);
        }
        static public void SetLastWriteTime(string name, DateTime lastWriteTime)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            System.IO.File.SetLastWriteTime(name, lastWriteTime);
        }

        static public void Move(string from, string to)
        {
#if MONO
        to = MonoFS.GetCorrectCasing(to);
#endif
            System.IO.File.Move(from, to);
        }

        static public void Copy(string from, string to)
        {
#if MONO
        to = MonoFS.GetCorrectCasing(to);
#endif
            System.IO.File.Copy(from, to);
        }

        static public void Copy(string from, string to, bool flag)
        {
#if MONO
        to = MonoFS.GetCorrectCasing(to);
#endif
            System.IO.File.Copy(from, to, flag);
        }

        static public void Delete(string name)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            System.IO.File.Delete(name);
        }
    }

    public static class Directory
    {

        // Directory
        static public bool Exists(string name)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return (System.IO.Directory.Exists(name));
        }

        static public string[] GetFiles(string name, string pattern, System.IO.SearchOption searchoption)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return (System.IO.Directory.GetFiles(name, pattern, searchoption));
        }

        static public string[] GetFiles(string name, string pattern)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return (System.IO.Directory.GetFiles(name, pattern));
        }


        static public string[] GetFiles(string name)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return (System.IO.Directory.GetFiles(name));
        }

        static public string[] GetDirectories(string name)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return System.IO.Directory.GetDirectories(name);
        }
        static public string[] GetDirectories(string name, string pattern, System.IO.SearchOption option)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return System.IO.Directory.GetDirectories(name, pattern, option);
        }
        static public string[] GetFileSystemEntries(string name)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return System.IO.Directory.GetFileSystemEntries(name);
        }

        static public void Delete(string name)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            System.IO.Directory.Delete(name);
        }

        static public void Delete(string name, bool flag)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            System.IO.Directory.Delete(name, flag);
        }

        static public System.IO.DirectoryInfo CreateDirectory(string name)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            return System.IO.Directory.CreateDirectory(name);
        }

        static public void SetCurrentDirectory(string name)
        {
#if MONO
        name = MonoFS.GetCorrectCasing(name);
#endif
            System.IO.Directory.SetCurrentDirectory(name);
        }
    }

}
