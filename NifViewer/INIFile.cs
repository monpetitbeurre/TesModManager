using System;
using StringDictionary=System.Collections.Generic.Dictionary<string, string>;
using StringList=System.Collections.Generic.List<string>;

namespace NifViewer {
    public class INIFile {
        private readonly StringDictionary KeyValuePairs;

        private string[] CleanLines(string[] _lines) {
            StringList lines=new StringList(_lines);
            for(int i=0;i<lines.Count;i++) {
                lines[i]=lines[i].Trim().ToLower();
                if(lines[i]==""||lines[i].IndexOf('=')==-1) lines.RemoveAt(i--);
            }
            return lines.ToArray();
        }

        public INIFile(string path) {
            KeyValuePairs=new StringDictionary();
            string[] lines=CleanLines(System.IO.File.ReadAllLines(path));
            foreach(string s in lines) {
                int index=s.IndexOf('=');
                KeyValuePairs.Add(s.Substring(0, index).Trim(), s.Substring(index+1).Trim());
            }
        }

        public int GetInt(string key, int def) {
            if(!KeyValuePairs.ContainsKey(key)) return def;
            int ret;
            if(int.TryParse(KeyValuePairs[key], out ret)) return ret;
            else return def;
        }
    }
}
