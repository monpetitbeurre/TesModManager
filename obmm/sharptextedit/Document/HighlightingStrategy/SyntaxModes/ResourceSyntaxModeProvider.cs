// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision: 1965 $</version>
// </file>

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace ICSharpCode.TextEditor.Document
{
	public class ResourceSyntaxModeProvider : ISyntaxModeFileProvider
	{
		List<SyntaxMode> syntaxModes = null;
		
		public ICollection<SyntaxMode> SyntaxModes {
			get {
				return syntaxModes;
			}
		}
		
		public ResourceSyntaxModeProvider()
		{
            syntaxModes = new List<SyntaxMode>();
            syntaxModes.Add(new SyntaxMode("obmmScript", "obmmScript", "obmmScript"));
            syntaxModes.Add(new SyntaxMode("python", "python", "python"));
            syntaxModes.Add(new SyntaxMode("cSharp", "cSharp", "cSharp"));
            syntaxModes.Add(new SyntaxMode("vb", "vb", "vb"));
		}
		
		public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
		{
            if(syntaxMode.FileName=="obmmScript") return new XmlTextReader(new MemoryStream(OblivionModManager.Properties.Resources.obmmScript));
            if(syntaxMode.FileName == "python") return new XmlTextReader(new MemoryStream(OblivionModManager.Properties.Resources.python));
            if(syntaxMode.FileName == "cSharp") return new XmlTextReader(new MemoryStream(OblivionModManager.Properties.Resources.cSharp));
            if(syntaxMode.FileName == "vb") return new XmlTextReader(new MemoryStream(OblivionModManager.Properties.Resources.vb));
            return null;
		}
		
		public void UpdateSyntaxModeList()
		{
			// resources don't change during runtime
		}
	}
}
