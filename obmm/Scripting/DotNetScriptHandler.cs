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
using System.Security.Policy;
using System.CodeDom.Compiler;
using Assembly=System.Reflection.Assembly;
using sList=System.Collections.Generic.List<string>;
using System.IO;
using System.Collections.Generic;

namespace OblivionModManager.Scripting {
    public static class DotNetScriptHandler {
        private static readonly Microsoft.CSharp.CSharpCodeProvider csCompiler=new Microsoft.CSharp.CSharpCodeProvider();
        private static readonly Microsoft.VisualBasic.VBCodeProvider vbCompiler=new Microsoft.VisualBasic.VBCodeProvider();
        private static readonly CompilerParameters cParams;
        private static readonly Evidence evidence;

        private static readonly string ScriptOutputPath=Path.Combine(Program.TempDir, "dotnetscript.dll");

        static DotNetScriptHandler() {
            cParams=new CompilerParameters();
            cParams.GenerateExecutable=false;
            cParams.GenerateInMemory=false;
            cParams.IncludeDebugInformation=false;
            cParams.OutputAssembly=ScriptOutputPath;
            cParams.ReferencedAssemblies.Add("TesModManager.exe");
            cParams.ReferencedAssemblies.Add("System.dll");
            cParams.ReferencedAssemblies.Add("System.Drawing.dll");
            cParams.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            cParams.ReferencedAssemblies.Add("System.Xml.dll");
			
            try
			{
				if (!new FileInfo(Path.Combine(Program.BaseDir, "dll.txt")).Exists)
				{
					File.WriteAllText(Path.Combine(Program.BaseDir, "dll.txt"), "");
				}
				if (!new FileInfo(Path.Combine(Program.BaseDir, "resx.txt")).Exists)
				{
					File.WriteAllText(Path.Combine(Program.BaseDir, "resx.txt"), "");
				}
				
				//
				{
					string[] dlls = File.ReadAllLines(Path.Combine(Program.BaseDir, "dll.txt"));
					
					foreach(string s in dlls)
					{
						string strim = s.Trim();
						if (strim.Length > 0)
						{
							cParams.ReferencedAssemblies.Add(strim);
						}
					}
				}
				
				//
				{
					string[] dlls = File.ReadAllLines(Path.Combine(Program.BaseDir,"resx.txt"));
					
					foreach(string s in dlls)
					{
						string strim = s.Trim();
						if (strim.Length > 0)
						{
							cParams.LinkedResources.Add(strim);
						}
					}
				}
			}
			catch(Exception)
			{
			}
            
            evidence=new Evidence();
            evidence.AddHostEvidence(new Zone(System.Security.SecurityZone.Internet));
        }

        private static byte[] Compile(string code, ScriptType language) {
            string[] errors, warnings;
            string stdout;
            return Compile(code, out errors, out warnings, out stdout, language);
        }
        private static byte[] Compile(string code, out string[] errors, out string[] warnings, out string stdout, ScriptType language) {
            CompilerResults results;
            switch(language) {
            case ScriptType.cSharp:
                results=csCompiler.CompileAssemblyFromSource(cParams, code);
                break;
            case ScriptType.vb:
                results=vbCompiler.CompileAssemblyFromSource(cParams, code);
                break;
            default:
                throw new obmmException("Invalid language specified for .NET script compiler");
            }
            stdout="";
            for(int i=0;i<results.Output.Count;i++) stdout+=results.Output[i]+Environment.NewLine;
            if(results.Errors.HasErrors) {
                sList msgs=new sList();
                foreach(CompilerError ce in results.Errors) {
                    if(!ce.IsWarning) msgs.Add("Error on Line " + ce.Line + ": "+ ce.ErrorText);
                }
                errors=msgs.ToArray();
                Program.logger.WriteToLog("Script failed to compile!!", Logger.LogLevel.Warning);
                foreach (string error in errors)
                {
                    Program.logger.WriteToLog(error,Logger.LogLevel.Low);
                }
            } else errors=null;
            if(results.Errors.HasWarnings) {
                sList msgs=new sList();
                foreach(CompilerError ce in results.Errors) {
                    if(ce.IsWarning) msgs.Add("Warning on Line " + ce.Line + ": " + ce.ErrorText);
                }
                warnings=msgs.ToArray();
            } else warnings=null;
            if(results.Errors.HasErrors) {
                return null;
            } else {
                byte[] data=System.IO.File.ReadAllBytes(results.PathToAssembly);
                System.IO.File.Delete(results.PathToAssembly);
                return data;
            }
        }

        private static string CheckSyntax(string code, out string stdout, ScriptType language) {
            string[] errors;
            string[] warnings;
            byte[] data;
            string errout = "";

            data = Compile(code, out errors, out warnings, out stdout, language);
            if(data == null) {
                for(int i = 0;i < errors.Length;i++) {
                    errout += errors[i] + Environment.NewLine;
                }
                return errout;
            }
            return "";
        }
        private static void Execute(string script, IScriptFunctions functions, ScriptType language)
		{
			Execute(script, functions, language, evidence);
		}
        public static bool ShowError = true;
		private static void Execute(string script, IScriptFunctions functions, ScriptType language, Evidence ev) {
			byte[] data=Compile(script, language);
			if(data==null)
			{
				if (ShowError)
					System.Windows.Forms.MessageBox.Show("Visual " + ((language == ScriptType.cSharp) ? "C#" : "Basic.NET") + " Script failed to compile", "Error");
				else
					File.AppendAllText("serr.log", "Visual " + ((language == ScriptType.cSharp) ? "C#" : "Basic.NET") + " Script failed to compile");
				//return;
                throw new Exception("Failed to compile script");
			}
			Assembly asm=AppDomain.CurrentDomain.Load(data, null); //, ev);
			
			//IScript s=asm.CreateInstance("Script") as IScript;
			bool anyScripts = false;
			
			Type[] types = asm.GetTypes();
			
			if (types != null)
			{
				try
				{
					foreach(Type t in types)
					{
						if (t.IsClass)
						{
							bool isscript = false;
                            bool isfomodscript = false;
							
							foreach(Type ifac in t.GetInterfaces())
							{
								if (ifac.FullName == "OblivionModManager.Scripting.IScript")
								{
									isscript = true;
									break;
								}
							}
                            if (t.BaseType.FullName.Contains("fomm.Scripting"))
                            {
                                isfomodscript = true;
                            }
								
							if (isscript)
							{
								anyScripts = true;
								object o = Activator.CreateInstance(t);
								
								OblivionModManager.Scripting.IScript s = (OblivionModManager.Scripting.IScript)o;
								
								s.Execute(functions);
							}
                            else if (isfomodscript)
                            {
                                anyScripts = true;
//                                fomm.Scripting.FalloutNewVegasBaseScript o = Activator.CreateInstance(t) as fomm.Scripting.FalloutNewVegasBaseScript;

                                // script default
                                functions.DontInstallAnyDataFiles();
                                functions.DontInstallAnyPlugins();

                                //object o = null;
                                System.Reflection.MethodInfo[] meth = t.GetMethods();
                                object[] args = new object[5];
                                args[0] = functions.getsrd();
                                args[1] = functions.getdataFileList();
                                args[2] = functions.getpluginList();
                                args[3] = functions.getdataFilePath();
                                args[4] = functions.getpluginPath();
//                                t.InvokeMember("_ScriptFunctions2", System.Reflection.BindingFlags.InvokeMethod, null, o, args);
//                                t.InvokeMember("OnActivate", System.Reflection.BindingFlags.InvokeMethod, null, o, null);

                                //Assembly asmScript = Assembly.Load(data);
                                object s = asm.CreateInstance("Script");
                                System.Reflection.MethodInfo mifMethod = null;
                                Type tpeScriptType = null;
                                for (tpeScriptType = s.GetType(); mifMethod == null; tpeScriptType = tpeScriptType.BaseType)
                                    mifMethod = tpeScriptType.GetMethod("Setup");
                                mifMethod.Invoke(s, args);
                                //for (Type tpeScriptType = s.GetType(); mifMethod == null; tpeScriptType = tpeScriptType.BaseType)
                                //    mifMethod = tpeScriptType.GetMethod("Setup", new Type[] { typeof(CSharpScriptFunctionProxy) });
                                //mifMethod.Invoke(s, new object[] { m_csfFunctions });
                                try
                                {
                                    s.GetType().GetMethod("OnActivate").Invoke(s, null);
                                }
                                catch (Exception Exception)
                                {
                                    throw new Exception("Script could not execute: " + Exception.Message);
                                }
                                try
                                {

                                    mifMethod = null;
                                    for (tpeScriptType = s.GetType(); mifMethod == null; tpeScriptType = tpeScriptType.BaseType)
                                        mifMethod = tpeScriptType.GetMethod("GetScriptReturnData");
                                    functions.setsrd((ScriptReturnData)(mifMethod.Invoke(s, null)));
                                }
                                catch { };
                                try {
                                    mifMethod = null;
                                    for (tpeScriptType = s.GetType(); mifMethod == null; tpeScriptType = tpeScriptType.BaseType)
                                        mifMethod = tpeScriptType.GetMethod("GetPluginList");
                                    functions.setpluginList((string[])(mifMethod.Invoke(s, null)));
                                }
                                catch { };
                                try {
                                    mifMethod = null;
                                    for (tpeScriptType = s.GetType(); mifMethod == null; tpeScriptType = tpeScriptType.BaseType)
                                        mifMethod = tpeScriptType.GetMethod("GetDataFileList");
                                    functions.setdataFileList((string[])(mifMethod.Invoke(s, null)));
                                }
                                catch { };

                            }
						}
					}
				}
				catch(ExecutionCancelledException)
				{
					functions.FatalError();
				}
				finally
				{
					Classes.BSAArchive.Clear();
				}
			}
			
			if(!anyScripts)
			{
				System.Windows.Forms.MessageBox.Show("Visual C# or Visual Basic.NET script did not contain a 'Script' class in the root namespace, or IScript was not implemented",
				                                     "Error");
				functions.FatalError();
				return;
			}
			
		}

        public static string CheckSyntaxCS(string code, out string stdout) {
            return CheckSyntax(code, out stdout, ScriptType.cSharp);
        }
        public static string CheckSyntaxVB(string code, out string stdout) {
            return CheckSyntax(code, out stdout, ScriptType.vb);
        }

        public static void ExecuteBAIN(IScriptFunctions functions)
        {
            functions.DontInstallAnyDataFiles();
            functions.DontInstallAnyPlugins();

            // install everything in 00
            string[] files = functions.GetDataFiles("", "*.*", true);
            List<string> folders = new List<string>();
            string lastseenfolder = "";
            foreach (string file in files)
            {
                string path=file.Substring(1);
                if (path.StartsWith("00"))
                {
                    // needs to be installed minus the base folder name of course
                    string lowerfilename = path.ToLower();
                    if (lowerfilename.IndexOf(Program.currentGame.DataFolderName + "\\") !=-1)
                        lowerfilename = lowerfilename.Substring(lowerfilename.IndexOf(Program.currentGame.DataFolderName + "\\") + (Program.currentGame.DataFolderName + "\\").Length);
                    else if (lowerfilename.IndexOf("\\")!=-1)
                        lowerfilename = lowerfilename.Substring(lowerfilename.IndexOf("\\") + 1);
                    else if (lowerfilename.IndexOf("/") != -1)
                        lowerfilename = lowerfilename.Substring(lowerfilename.IndexOf("/") + 1);

                    if (lowerfilename.EndsWith(".esp") || lowerfilename.EndsWith(".esm") || lowerfilename.EndsWith(".esl"))
                        functions.CopyPlugin(file.Substring(1), lowerfilename);
                    else
                        functions.CopyDataFile(file.Substring(1), lowerfilename);
                }
                else if (lastseenfolder.Length>0 && path.IndexOf("\\")!=-1 && lastseenfolder != path.Substring(0, path.IndexOf("\\")))
                {
                    lastseenfolder = path.Substring(0, path.IndexOf("\\"));
                    folders.Add(lastseenfolder);
                }

            }
            // everything else is optional
            string[] selected = functions.Select(folders.ToArray(), null, null, "BAIN options", true, false);

            List<string> selectedoptions = new List<string>(selected);
            foreach (string file in files)
            {
                string path = file.Substring(1);
                if (path.IndexOf("\\")!=-1)
                    path = path.Substring(0, path.IndexOf("\\"));
                if (selectedoptions.Contains(path))
                {
                    // needs to be installed minus the base folder name of course
                    string lowerfilename = file.Substring(1).ToLower();
                    if (lowerfilename.IndexOf(Program.currentGame.DataFolderName + "\\") != -1)
                        lowerfilename = lowerfilename.Substring(lowerfilename.IndexOf(Program.currentGame.DataFolderName +"\\") + (Program.currentGame.DataFolderName +"Data\\").Length);
                    else if (lowerfilename.IndexOf("\\") != -1)
                        lowerfilename = lowerfilename.Substring(lowerfilename.IndexOf("\\") + 1);
                    else if (lowerfilename.IndexOf("/") != -1)
                        lowerfilename = lowerfilename.Substring(lowerfilename.IndexOf("/") + 1);
                    if (lowerfilename.EndsWith(".esp") || lowerfilename.EndsWith(".esm") || lowerfilename.EndsWith(".esl"))
                        functions.CopyPlugin(file.Substring(1), lowerfilename);
                    else
                        functions.CopyDataFile(file.Substring(1), lowerfilename);
                }

            }

        }

        public static void ExecuteXML(string script, IScriptFunctions functions)
        {
            if ((byte)script[0]=='\xfd' && (byte)script[1] == '\xfd')
            {
                script = script.Substring(2);
                System.Text.UTF8Encoding utf8Encoding = new System.Text.UTF8Encoding();
                System.Text.UnicodeEncoding unicodeEncoding = new System.Text.UnicodeEncoding();
                script = unicodeEncoding.GetString(utf8Encoding.GetBytes(script));
            }

            if (script.Contains("?<?xml version=\"1.0\" encoding=\"UTF-16\" ?>"))
            {
                script=script.Replace("?<?xml version=\"1.0\" encoding=\"UTF-16\" ?>","");
            }
            if (script.Contains("<?xml version=\"1.0\" encoding=\"UTF-16\" ?>"))
            {
                script = script.Replace("<?xml version=\"1.0\" encoding=\"UTF-16\" ?>", "");
            }
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Text.UTF8Encoding  encoding=new System.Text.UTF8Encoding();
            MemoryStream ms = new MemoryStream(encoding.GetBytes(script));

            // load the whole thing
            System.Xml.Serialization.XmlSerializer oXmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(fomod.moduleConfiguration));
            fomod.moduleConfiguration mc = (fomod.moduleConfiguration)oXmlSerializer.Deserialize(ms);

            functions.DontInstallAnyDataFiles();
            functions.DontInstallAnyPlugins();

            if (mc == null)
                return;

            if (mc!=null && mc.requiredInstallFiles != null && mc.requiredInstallFiles.Items != null)
            {
                // install that one ((fomod.conditionFlagList)(step.optionalFileGroups.group[0].plugins.plugin[0].Items[1])).Items
                for (int k = 0; k < mc.requiredInstallFiles.Items.GetLength(0); k++)
                {
                    if (mc.requiredInstallFiles.ItemsElementName[k] == fomod.ItemsChoiceType1.file)
                    {
                        string source = mc.requiredInstallFiles.Items[k].source.ToLower();
                        string dest = mc.requiredInstallFiles.Items[k].destination;
                        if (source.EndsWith(".esp") || source.EndsWith(".esm") || source.EndsWith(".esl"))
                            functions.CopyPlugin(source, dest);
                        else if (source.EndsWith(".bsa"))
                            functions.CopyDataFile(source, dest);
                        else
                            functions.CopyDataFolder(source, dest, true);
                    }
                    else
                    {
                        string sourcedir = mc.requiredInstallFiles.Items[k].source.ToLower();
                        string destdir = mc.requiredInstallFiles.Items[k].destination;
                        //string[] files = Directory.GetFiles(sourcedir, "*.*", SearchOption.AllDirectories);
                        //foreach (string file in files)
                        //{
                        //    if (file.EndsWith(".esp") || file.EndsWith(".esm"))
                        //        functions.CopyPlugin(file, destdir);
                        //    else
                        //        functions.CopyDataFile(file, destdir);
                        //}
                        functions.CopyDataFolder(sourcedir, destdir, true);
                    }
                }
            }

            System.Collections.Generic.List<string> flaglist = new System.Collections.Generic.List<string>(); // flag for next installation step
            System.Collections.Generic.List<string> flagvaluelist = new System.Collections.Generic.List<string>();
            // go through all the steps
            bool bInstallstep = false;
            bool bMissingDependency = false;
            foreach (fomod.installStep step in mc.installSteps.installStep)
            {
                bInstallstep = false;
                bMissingDependency = false;

                if (step.visible == null)
                    bInstallstep = true;
                else if (step.visible.Items != null && step.visible.Items.Length > 0)
                {
                    if (step.visible.Items[0] is fomod.compositeDependency)
                    {
                        foreach (Object o in ((fomod.compositeDependency)step.visible.Items[0]).Items)
                        {
                            if (o is fomod.flagDependency)
                            {
                                if (flaglist.Contains(((fomod.flagDependency)o).flag.ToLower()))
                                {
                                    bInstallstep = true;
                                }
                                else
                                {
                                    bMissingDependency = true;
                                    break;
                                }
                            }
                            else if (o is fomod.fileDependency)
                            {
                                if (File.Exists(Path.Combine(Program.currentGame.DataFolderPath,((fomod.fileDependency)o).file)))
                                {
                                    bInstallstep = true;
                                }
                                else
                                {
                                    bMissingDependency = true;
                                    break;
                                }
                            }
                            else if (o is fomod.versionDependency)
                            {
                                // ???
                                bInstallstep = true;
                            }
                        }
                    }
                    else if (step.visible.Items[0] is fomod.flagDependency)
                    {
                        if (flaglist.Contains(((fomod.flagDependency)(step.visible.Items[0])).flag.ToLower()))
                        {
                            bInstallstep = true;
                        }
                        else
                        {
                            bMissingDependency = true;
                        }
                    }
                }

                // check: <visible> <flagDependency value="On" flag="<previousChoiceItDependsOn>"/> </visible> to find if dependent
                if (bInstallstep && !bMissingDependency) // step.visible == null || (flaglist.Contains(((fomod.flagDependency)(step.visible.Items[0])).flag.ToLower()))) // && flagvaluelist[index])
                {
                    for (int grp = 0; grp < step.optionalFileGroups.group.GetLength(0); grp++)
                    {
                        string[] optionlist = new string[step.optionalFileGroups.group[grp].plugins.plugin.GetLength(0)];
                        string[] previewlist = new string[optionlist.Length];
                        string[] desclist = new string[optionlist.Length];
                        for (int i = 0; i < optionlist.Length; i++)
                        {
                            try { optionlist[i] = step.optionalFileGroups.group[grp].plugins.plugin[i].name; }catch { };
                            try { desclist[i] = step.optionalFileGroups.group[grp].plugins.plugin[i].description; }catch { };
                            if (step.optionalFileGroups.group[grp].plugins.plugin[i].image!=null)
                                previewlist[i] = (step.optionalFileGroups.group[grp].plugins.plugin[i].image.path).Replace("\\\\", "\\");
                        }
                        bool bMulti= step.optionalFileGroups.group[grp].type == fomod.groupType.SelectAny;
                        bMulti = bMulti || step.optionalFileGroups.group[grp].type == fomod.groupType.SelectAtLeastOne;
                        bMulti = bMulti && !(step.optionalFileGroups.group[grp].type == fomod.groupType.SelectExactlyOne || step.optionalFileGroups.group[grp].type == fomod.groupType.SelectAtMostOne);
                        bool bAtLeastOne = step.optionalFileGroups.group[grp].type == fomod.groupType.SelectAtLeastOne;
                        bAtLeastOne = bAtLeastOne || step.optionalFileGroups.group[grp].type == fomod.groupType.SelectExactlyOne;
                        string[] selected = functions.Select(optionlist, previewlist, desclist, step.optionalFileGroups.group[grp].name, bMulti ,bAtLeastOne);
                        //                    Forms.SelectForm sf = new Forms.SelectForm(optionlist, step.name, step.optionalFileGroups.group[0].type == fomod.groupType.SelectAtLeastOne, previewlist, desclist, false); // true);
                        //                    sf.ShowDialog();
                        // install all selected
                        for (int i = 0; i < optionlist.Length; i++)
                        {
                            for (int j = 0; j < selected.Length; j++)
                            {
                                if (optionlist[i] == selected[j])
                                {
                                    for (int item = 0; item < step.optionalFileGroups.group[grp].plugins.plugin[i].Items?.GetLength(0); item++)
                                    {
                                        if (step.optionalFileGroups.group[grp].plugins.plugin[i].Items[item].GetType() == typeof(fomod.conditionFlagList))
                                        {
                                            foreach (fomod.setConditionFlag flag in ((fomod.conditionFlagList)(step.optionalFileGroups.group[grp].plugins.plugin[i].Items[item])).flag)
                                            {
                                                // set the flag ((fomod.conditionFlagList)(step.optionalFileGroups.group[0].plugins.plugin[0].Items[0])).flag
                                                //if (flag.Value.ToLower() == "on" || flag.Value.ToLower() == "yes")
                                                {
                                                    flaglist.Add(flag.name.ToLower());
                                                    flagvaluelist.Add(flag.Value.ToLower());
                                                }
//                                                flaglist.Add(((fomod.conditionFlagList)(step.optionalFileGroups.group[grp].plugins.plugin[i].Items[item])).flag[0].name.ToLower());
//                                                flagvaluelist.Add(((fomod.conditionFlagList)(step.optionalFileGroups.group[grp].plugins.plugin[i].Items[item])).flag[0].Value.ToLower());
                                            }
                                        }
                                        else if (step.optionalFileGroups.group[grp].plugins.plugin[i].Items[item].GetType() == typeof(fomod.fileList))
                                        {
                                            if (((fomod.fileList)(step.optionalFileGroups.group[grp].plugins.plugin[i].Items[item])).Items != null)
                                            {
                                                // install that one ((fomod.conditionFlagList)(step.optionalFileGroups.group[0].plugins.plugin[0].Items[1])).Items
                                                for (int k = 0; k < ((fomod.fileList)(step.optionalFileGroups.group[grp].plugins.plugin[i].Items[item])).Items.GetLength(0); k++)
                                                {
                                                    string source = ((fomod.fileList)(step.optionalFileGroups.group[grp].plugins.plugin[i].Items[item])).Items[k].source;
                                                    string dest = ((fomod.fileList)(step.optionalFileGroups.group[grp].plugins.plugin[i].Items[item])).Items[k].destination;
                                                    if (source.EndsWith(".esp") || source.EndsWith(".esm") || source.EndsWith(".esl"))
                                                        functions.CopyPlugin(source, dest);
                                                    else if (source.EndsWith(".bsa"))
                                                        functions.CopyDataFile(source, dest);
                                                    else
                                                        functions.CopyDataFolder(source, dest, true);
                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (mc.conditionalFileInstalls != null && mc.conditionalFileInstalls.patterns!=null)
            {
                foreach (fomod.conditionalInstallPattern pattern in mc.conditionalFileInstalls.patterns)
                {
                    // dependency flag
                    int nbflags = pattern.dependencies.Items.Length;

                    bool patterncondition = true;

                    foreach ( object obj in pattern.dependencies.Items)
                    {
                        bool condition = true;

                        if (obj.GetType() == typeof(fomod.flagDependency))
                        {
                            fomod.flagDependency flagdep = obj as fomod.flagDependency;
                            string flag = flagdep.flag.ToLower(); ;
                            string value = flagdep.value.ToLower(); ;
                            // an OFF flag is not stored
                            // the dependency can be on a flag being off
                            if ((flaglist.Contains(flag) && (value == "on" || value == "yes" || value.Length == 0)) || (!flaglist.Contains(flag) && (value == "off" || value == "no"))
                                || (flaglist.Contains(flag) && flaglist.IndexOf(flag) > -1 && value == flagvaluelist[flaglist.IndexOf(flag)]))
                                condition = true;
                            else
                                condition = false;
                        }
                        else if (obj.GetType() == typeof(fomod.fileDependency))
                        {
                            fomod.fileDependency filedep = obj as fomod.fileDependency;
                            string lowerfilename = filedep.file.ToLower();

                            // check if the plugins the file depends on are present already or will be copied
                            List<string> pluginlist = new List<string>(functions.GetActiveEspNames());
                            pluginlist.AddRange(functions.getpluginList());

                            bool bFound = false;
                            foreach (string plugin in pluginlist)
                            {
                                if (plugin.ToLower().Contains(lowerfilename))
                                {
                                    bFound = true;
                                    if (filedep.state == fomod.fileDependencyState.Active)
                                    {
                                        condition = true;
                                        break;
                                    }
                                    else if (filedep.state == fomod.fileDependencyState.Inactive)
                                    {
                                        condition = false;
                                    }
                                    else
                                        condition = false;
                                    break;
                                }
                            }
                            if (!bFound && (filedep.state == fomod.fileDependencyState.Inactive || filedep.state == fomod.fileDependencyState.Missing))
                                condition = true;
                            else
                                condition = false;
                        }
                        else if (obj.GetType() == typeof(fomod.compositeDependency))
                        {
                            fomod.compositeDependency composite = obj as fomod.compositeDependency;

                            foreach (object item in composite.Items)
                            {
                                bool itemcondition = true;

                                if (item.GetType() == typeof(fomod.flagDependency))
                                {
                                    fomod.flagDependency flagdep = item as fomod.flagDependency;
                                    string flag = flagdep.flag.ToLower(); ;
                                    string value = flagdep.value.ToLower(); ;
                                    // an OFF flag is not stored
                                    // the dependency can be on a flag being off
                                    if ((flaglist.Contains(flag) && (value == "on" || value == "yes" || value.Length == 0)) || (!flaglist.Contains(flag) && (value == "off" || value == "no"))
                                        || (flaglist.Contains(flag) && flaglist.IndexOf(flag) > -1 && value == flagvaluelist[flaglist.IndexOf(flag)]))
                                        itemcondition = true;
                                    else
                                        itemcondition = false;
                                }
                                else if (item.GetType() == typeof(fomod.fileDependency))
                                {
                                    fomod.fileDependency filedep = item as fomod.fileDependency;
                                    string lowerfilename = filedep.file.ToLower();

                                    // check if the plugins the file depends on are present already or will be copied
                                    List<string> pluginlist = new List<string>(functions.GetActiveEspNames());
                                    pluginlist.AddRange(functions.getpluginList());

                                    bool bFound = false;
                                    foreach (string plugin in pluginlist)
                                    {
                                        if (plugin.ToLower().Contains(lowerfilename))
                                        {
                                            bFound = true;
                                            if (filedep.state == fomod.fileDependencyState.Active)
                                            {
                                                itemcondition = true;
                                                break;
                                            }
                                            else if (filedep.state == fomod.fileDependencyState.Inactive)
                                            {
                                                itemcondition = false;
                                            }
                                            else
                                                itemcondition = false;
                                            break;
                                        }
                                    }
                                    if (!bFound && (filedep.state == fomod.fileDependencyState.Inactive || filedep.state == fomod.fileDependencyState.Missing))
                                        itemcondition = true;
                                    else
                                        itemcondition = false;
                                }
                                if (composite.@operator == fomod.compositeDependencyOperator.And)
                                    condition &= itemcondition;
                                else if (composite.@operator == fomod.compositeDependencyOperator.Or)
                                    condition |= itemcondition;
                            }
                        }
                        if (pattern.dependencies.@operator == fomod.compositeDependencyOperator.Or)
                            patterncondition |= condition;
                        else if (pattern.dependencies.@operator == fomod.compositeDependencyOperator.And)
                            patterncondition &= condition;
                    }


                    //                string flag = ((fomod.flagDependency)(pattern.dependencies.Items[0])).flag;
                    //                string value = ((fomod.flagDependency)(pattern.dependencies.Items[0])).value.ToLower();

                    if (patterncondition)
                    {
                        foreach (fomod.fileSystemItem fsitem in pattern.files.Items)
                        {
                            // copy all files from source to destination
                            string source = fsitem.source.Replace("/","\\");// ((fomod.fileSystemItem)(pattern.files.Items[0])).source;
                            string dest = fsitem.destination.Replace("/", "\\"); // ((fomod.fileSystemItem)(pattern.files.Items[0])).destination;

                            if (source.EndsWith(".esp") || source.EndsWith(".esm") || source.EndsWith(".esl"))
                                functions.CopyPlugin(source, dest);
                            else if (source.EndsWith(".bsa"))
                                functions.CopyDataFile(source, dest);
                            else
                            {
                                string[] esplist = functions.getpluginList();
                                foreach (string esp in esplist)
                                {
                                    if (esp.Contains(source + "\\"))
                                    {
                                        functions.CopyPlugin(esp, Path.GetFileName(esp));
                                    }
                                }
                                // check if there are actual data files to copy
                                string[] datalist = functions.getdataFileList();
                                foreach (string data in datalist)
                                {
                                    if (data.Contains(source + "\\"))
                                    {
                                        functions.CopyDataFolder(source, dest, true);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void ExecuteCS(string script, IScriptFunctions functions) {
            Execute(script, functions, ScriptType.cSharp);
        }
        public static void ExecuteVB(string script, IScriptFunctions functions) {
            Execute(script, functions, ScriptType.vb);
        }
        
        public static void ExecuteCS(string script, IScriptFunctions functions, Evidence e) {
			Execute(script, functions, ScriptType.cSharp, e);
		}
		public static void ExecuteVB(string script, IScriptFunctions functions, Evidence e) {
			Execute(script, functions, ScriptType.vb, e);
		}
    }
}
