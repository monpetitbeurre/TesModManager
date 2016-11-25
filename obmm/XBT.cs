/*
XBaseTools
Copyright (C) 2010 Matthew Perry

This library/program is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

This libary/program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using BaseTools.Configuration;
using BaseTools.Configuration.Parsers;

namespace OblivionModManager
{
	/// <summary>
	/// Description of XBT.
	/// </summary>
	public class XBT
	{
		public static ConfigList LoadConfiguration(string filename)
		{
			return ConfigType(filename).LoadConfiguration(filename);
		}
		public static IConfig ConfigType(string filename)
		{
			if (filename.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase))
				return new JSONConfig();
			else if (filename.EndsWith(".ini", StringComparison.CurrentCultureIgnoreCase))
				return new IniConfig();
			else if (filename.EndsWith(".cidb", StringComparison.CurrentCultureIgnoreCase))
				return new BuilderConfig();
			else
				return new GeneralConfig();
		}
		public static void SaveConfiguration(string filename, ConfigList cl)
		{
			ConfigType(filename).SaveConfiguration(filename, cl);
		}
	}
}
