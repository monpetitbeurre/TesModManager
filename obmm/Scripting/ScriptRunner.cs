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
using MessageBox=System.Windows.Forms.MessageBox;
using mbButtons=System.Windows.Forms.MessageBoxButtons;
using DialogResult=System.Windows.Forms.DialogResult;
using File=System.IO.File;
using Directory=System.IO.Directory;
using Path=System.IO.Path;

namespace OblivionModManager.Scripting {
    public static class ScriptRunner {
		public static string ScriptName(ScriptType t)
		{
			switch(t)
			{
					case ScriptType.cSharp: return "Visual C#";
					case ScriptType.obmmScript: return "OBMM";
					case ScriptType.vb: return "Visual Basic .NET";
					case ScriptType.Python: return "IronPython";
					default: return "Unknown";
			}
		}
        public static ScriptReturnData ExecuteScript(string script, string DataPath, string PluginsPath) {
            if (script==null||script.Length==0) return new ScriptReturnData();

            ScriptType type;
            if (script[0] == '<') type = ScriptType.xml;
            else if (script[0] == '/') type = ScriptType.cSharp;
            else if (script == "BAIN") type = ScriptType.BAIN;
            else if ((byte)script[0] >= (byte)ScriptType.Count) type = ScriptType.obmmScript;
            else {
                type=(ScriptType)script[0];
                script=script.Substring(1);
            }
            if(type==ScriptType.obmmScript) {
                return obmmScriptHandler.Execute(script, DataPath, PluginsPath);
            }

            ScriptReturnData srd=new ScriptReturnData();

            if(!Settings.AllowInsecureScripts) {
                MessageBox.Show("That omod uses a script type which you have disallowed from running", "Error");
                srd.CancelInstall=true;
                return srd;
            }

            ScriptFunctions sf=new ScriptFunctions(srd, DataPath, PluginsPath);

            switch(type) {
            case ScriptType.Python:
                pythonScriptHandler.Execute(script, sf);
                break;
            case ScriptType.cSharp:
                DotNetScriptHandler.ExecuteCS(script, sf);
                break;
            case ScriptType.xml:
                DotNetScriptHandler.ExecuteXML(script, sf);
                break;
            case ScriptType.vb:
                DotNetScriptHandler.ExecuteVB(script, sf);
                break;
            case ScriptType.BAIN:
                DotNetScriptHandler.ExecuteBAIN(sf);
                break;
            }
            return srd;
        }
		public static ScriptReturnData ExecuteScript(string script, string DataPath, string PluginsPath, ScriptType type, System.Security.Policy.Evidence e) {
			if(script==null||script.Length==0) return new ScriptReturnData();

			if(type==ScriptType.obmmScript) {
				return obmmScriptHandler.Execute(script, DataPath, PluginsPath);
			}

			ScriptReturnData srd=new ScriptReturnData();

			if(!Settings.AllowInsecureScripts)
			{
				MessageBox.Show("That Oblivion Mod uses a " + ScriptName(type) + " Script, which you have disallowed from running", "Error");
				srd.CancelInstall=true;
				return srd;
			}

			ScriptFunctions sf=new ScriptFunctions(srd, DataPath, PluginsPath);

			switch(type) {
				case ScriptType.Python:
					pythonScriptHandler.Execute(script, sf);
					break;
				case ScriptType.cSharp:
					DotNetScriptHandler.ExecuteCS(script, sf, e);
					break;
				case ScriptType.vb:
					DotNetScriptHandler.ExecuteVB(script, sf, e);
					break;
			}
			return srd;
		}

        public static ScriptReturnData SimulateScript(string script, string[] Data, string[] Plugins) {
            if(script==null||script.Length==0) return new ScriptReturnData(); ;

            ScriptType type;
            if((byte)script[0]>=(byte)ScriptType.Count) type=ScriptType.obmmScript;
            else {
                type=(ScriptType)script[0];
                script=script.Substring(1);
            }
            if(type==ScriptType.obmmScript) throw new obmmException("Cannot simulate an obmm script");

            ScriptReturnData srd=new ScriptReturnData();

            if(!Settings.AllowInsecureScripts) {
                MessageBox.Show("That omod uses a script type which you have disallowed from running", "Error");
                srd.CancelInstall=true;
                return srd;
            }

            ScriptFunctions sf=new ScriptFunctions(srd, Data, Plugins);

            switch(type) {
            case ScriptType.Python:
                pythonScriptHandler.Execute(script, sf);
                break;
            case ScriptType.cSharp:
                DotNetScriptHandler.ExecuteCS(script, sf);
                break;
            case ScriptType.vb:
                DotNetScriptHandler.ExecuteVB(script, sf);
                break;
            }
            return srd;
        }

    }
}
