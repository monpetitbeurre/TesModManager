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
using System.Text;
using System.IO;
using System.Windows.Forms;
using IronPython.Hosting;
using IronPython.Runtime;
using IronPython.Compiler;
using System.Security.Permissions;
using MessageBox=System.Windows.Forms.MessageBox;
using mbButtons=System.Windows.Forms.MessageBoxButtons;
using DialogResult=System.Windows.Forms.DialogResult;
using File=System.IO.File;
using Directory=System.IO.Directory;
using Path=System.IO.Path;


namespace OblivionModManager.Scripting {
    public static class pythonScriptHandler {
        private static PythonEngine engine = null;
        private static EngineModule engineModule = null;
        private static Forms.ScriptMessages messages = null;
        private static Stream tspOutput = null;
        private static Stream tspError = null;
        private static bool setup=false;

        private static void Initialize() {

            engine = new PythonEngine();

            //engine.InitializeModules(Application.ExecutablePath,typeof(OblivionModManager.Program).Assembly.Location , "1.1");

            engine.AddToPath(Path.GetDirectoryName(Program.BaseDir));
            engineModule = engine.CreateModule("__main__", new Dictionary<string, object>(), true);
            engine.DefaultModule = engineModule;

            setup=true;
        }

        public static string CheckSyntax(string code) {
            if (!setup) Initialize();
            string errout = "";
            CompiledCode data;
            try {
                data = engine.Compile(code);
            } catch(IronPython.Runtime.Exceptions.PythonSyntaxErrorException e) {
                //Because IronPython has a bug and doesn't print the right line text
                string[] lines = code.Replace("\r", "").Split(new char[] { '\n' });
                string col = "";
                if(e.Column > 0) {
                    col = new String('-', e.Column - 1);
                }
                col += "^";
                errout = "Syntax Error: " + e.Message + Environment.NewLine + lines[e.Line - 1] + Environment.NewLine + col;
            } finally {
                data = null;
            }
            return errout;
        }

        public static void Execute(string python_script, IScriptFunctions sf) {
            if(!setup) Initialize();
            byte[] m;
            string script_data;

            tspOutput = Console.OpenStandardOutput();
            tspError = Console.OpenStandardError();

            if(Settings.ShowScriptWarnings) {
                if(messages == null || messages.IsDisposed) {
                    messages = new Forms.ScriptMessages();
                }

                //Proxy the python standard outputs to the ScriptMessages Form
                tspOutput = new TextStreamProxy(messages.GetOutputBox());
                tspError = new TextStreamProxy(messages.GetErrorBox());
                engine.Sys.stdout = new PythonFile(tspOutput, Encoding.ASCII, "w");
                engine.Sys.stderr = new PythonFile(tspError, Encoding.ASCII, "w");

                messages.ClearData();
                messages.Show();
            }

            //Python Global Variables
            if(engine.Globals.ContainsKey("obmm")) engine.Globals.Remove("obmm");
            engine.Globals.Add("obmm", sf);

            if(engine.Globals.ContainsKey("ConflictLevel")) engine.Globals.Remove("ConflictLevel");
            engine.Globals.Add("ConflictLevel", new ConflictLevel());

            if(engine.Globals.ContainsKey("DeactiveStatus")) engine.Globals.Remove("DeactiveStatus");
            engine.Globals.Add("DeactiveStatus", new DeactiveStatus());

            if(engine.Globals.ContainsKey("Version")) engine.Globals.Remove("Version");
            engine.Globals.Add("Version", new Version());

            script_data = Encoding.Default.GetString(Properties.Resources.IronPythonInterface) + "\n" + python_script;

            System.Security.PermissionSet ps=new System.Security.PermissionSet(PermissionState.None);
            {
                string[] paths=new string[3];
                paths[0]=Program.BaseDir;
                paths[1]=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My games\\Oblivion");
                paths[2]=Program.TempDir;
                ps.AddPermission(new FileIOPermission(FileIOPermissionAccess.AllAccess, paths));
                ps.AddPermission(new UIPermission(UIPermissionWindow.AllWindows));
                //ps.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.ReflectionEmit|ReflectionPermissionFlag.MemberAccess));
                ps.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution|SecurityPermissionFlag.ControlEvidence));
            }
            ps.PermitOnly();
            try {
                engine.Execute(script_data);
            } catch(IronPython.Runtime.Exceptions.PythonSyntaxErrorException e) {
                //Because IronPython has a bug and doesn't print the right line text
                string[] lines = script_data.Replace("\r", "").Split(new char[] { '\n' });
                string col = "";
                if(e.Column > 0) {
                    col = new String('-', e.Column - 1);
                }
                col += "^";
                m = System.Text.Encoding.Default.GetBytes("Syntax Error: " + e.Message + "\n" + lines[e.Line - 1] + "\n" + col);
                tspError.Write(m, 0, m.Length);
                sf.FatalError();
            } catch(ExecutionCancelledException) {
                sf.FatalError();
            } finally {
                m = null;
                System.Security.CodeAccessPermission.RevertPermitOnly();
                Classes.BSAArchive.Clear();
            }
        }
    }

    public class TextStreamProxy : Stream {
        private StringWriter sw;
        private RichTextBox text;

        #region unsupported Read + Seek members
        public override bool CanRead {
            get { return false; }
        }

        public override bool CanSeek {
            get { return false; }
        }

        public override bool CanWrite {
            get { return true; }
        }

        public override void Flush() {
            // nop
        }

        public override long Length {
            get { throw new NotSupportedException("Seek not supported"); } // can't seek 
        }

        public override long Position {
            get {
                throw new NotSupportedException("Seek not supported");  // can't seek 
            }
            set {
                throw new NotSupportedException("Seek not supported");  // can't seek 
            }
        }

        public override int Read(byte[] buffer, int offset, int count) {
            throw new NotSupportedException("Reed not supported"); // can't read
        }

        public override long Seek(long offset, SeekOrigin origin) {
            throw new NotSupportedException("Seek not supported"); // can't seek
        }

        public override void SetLength(long value) {
            throw new NotSupportedException("Seek not supported"); // can't seek
        }
        #endregion

        public TextStreamProxy(RichTextBox tb) {
            if(tb == null) throw new ArgumentException("Must provide text box to constructor");
            text = tb;
            sw = new StringWriter();
        }

        public override void Write(byte[] buffer, int offset, int count) {
            sw.Write(System.Text.Encoding.Default.GetChars(buffer), offset, count);
            update();
        }

        private void update() {
            text.Text = sw.ToString();
        }
    }
}
