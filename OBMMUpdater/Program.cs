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
using System.Threading;
using System.Linq;
using System.Diagnostics;
using System.IO;

namespace OBMMUpdater
{
	class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length < 3)
			{
				Console.Error.WriteLine("USAGE: OBMMUpdater PROCESS TITLE SOURCE");
			}
			else
			{
				Console.BackgroundColor = ConsoleColor.Blue;
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine(new String('=', args[1].Length + 4));
				Console.WriteLine("= " + args[1] + " =");
				Console.WriteLine(new String('=', args[1].Length + 4));
				Console.ResetColor();
				int k=1;
				Console.ForegroundColor = ConsoleColor.Green;
				while(Process.GetProcesses().Any(prc => prc.ProcessName.Contains(args[0])))
				{
					Thread.Sleep(1000);
					Console.WriteLine((k++).ToString() + "...");
				}
				Console.ResetColor();
				
				try
				{
					args[2] = args[2].Replace('/', '\\');
					if (args[2].EndsWith("\\"))
						args[2] = args[2].Substring(0, args[2].Length-1);
					FileInfo[] files = new DirectoryInfo(args[2]).GetFiles("*", SearchOption.AllDirectories);
					int red = new DirectoryInfo(args[2]).FullName.Length + 1;
					Console.ForegroundColor = ConsoleColor.Cyan;
					foreach(FileInfo fi in files)
					{
						string relname = fi.FullName.Substring(red);
						
						if (File.Exists(relname))
							File.Delete(relname);
						
						Console.WriteLine(relname);
						
						fi.MoveTo(relname);
					}
					Console.ResetColor();
					
					
					if (File.Exists(args[0] + ".exe"))
						Process.Start(args[0] + ".exe");
				}
				catch(Exception ex)
				{
					Console.BackgroundColor = ConsoleColor.Red;
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine("ERROR: " + ex.Message);
					Console.ResetColor();
					Console.ReadKey();
				}
			}
		}
	}
}