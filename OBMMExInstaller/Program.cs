/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 23/07/2010
 * Time: 4:27 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Resources;


namespace OBMMExInstaller
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			if (args.Length >= 4 && args[0] == "-generate")
			{
				FileStream fs = new FileStream(args[3], FileMode.Create);
				BinaryWriter bw = new BinaryWriter(fs);
				DirectoryInfo dif = new DirectoryInfo(args[1]);
				
				bw.Write(args[2]);
				bw.Write(DateTime.UtcNow.ToBinary());
				
				{
					DirectoryInfo[] ads = AllDirectories(dif);
					
					bw.Write(ads.Length);
					
					foreach(DirectoryInfo di in ads)
					{
                        Console.WriteLine("Adding " + dif.FullName);
						bw.Write(di.FullName.Substring(dif.FullName.Length+1));
					}
					
				}
				{
					FileInfo[] afs = dif.GetFiles("*", SearchOption.AllDirectories);
					
					bw.Write(afs.Length);
					
					foreach(FileInfo fi in afs)
					{
                        Console.WriteLine("Writing " + dif.FullName);
                        bw.Write(fi.FullName.Substring(dif.FullName.Length + 1));
						byte[] alldata = File.ReadAllBytes(fi.FullName);
						
						bw.Write(alldata.Length);
						bw.Write(alldata);
						bw.Flush();
					}
				}
				
				bw.Close();
			}
			else if (args.Length >= 3 && args[0] == "-report")
			{
				try
				{
					BinaryReader br = new BinaryReader(new FileStream(args[1], FileMode.Open));
					
					TextWriter tw = new StreamWriter(args[2]);
					
					tw.Write("Program Title: ");
					tw.WriteLine(br.ReadString());
					tw.Write("Date: ");
					tw.WriteLine(DateTime.FromBinary(br.ReadInt64()).ToString());
					tw.WriteLine();
					
					int dircount = br.ReadInt32();
					
					tw.Write("Directory Count: ");
					tw.WriteLine(dircount);
					tw.WriteLine();
					
					for(int i=0;i<dircount;i++)
					{
						tw.WriteLine(br.ReadString());
					}
					
					int fcount = br.ReadInt32();
					tw.WriteLine();
					tw.Write("File Count: ");
					tw.WriteLine(fcount);
					tw.WriteLine();
					
					for(int i=0;i<fcount;i++)
					{
						string filename = br.ReadString();
						
						int fsz = br.ReadInt32();
						
						br.ReadBytes(fsz);
						
						tw.Write(filename);
						tw.Write(": ");
						tw.Write(fsz);
						tw.WriteLine(" bytes");
					}
					
					br.Close();
					tw.Close();
				}
				catch(Exception ex)
				{
					MessageBox.Show("Error: " + ex.Message);
				}
			}
			else if (args.Length >= 2 && args[0] == "-embed")
			{
				string outputfile = args[1];
				
				ResXResourceWriter rw = new ResXResourceWriter(outputfile);
				List<IOException> messages = new List<IOException>();
				
				for(int i=3;i<args.Length;i+=2)
				{
					string filename = args[i];
					string embname = args[i-1];
					
					try
					{
						rw.AddResource(embname, File.ReadAllBytes(filename));
					}
					catch(IOException ex)
					{
						messages.Add(ex);
					}
				}
				
				foreach(IOException ex in messages)
					MessageBox.Show(ex.Message);
				
				rw.Close();
			}
			else if (args.Length > 0)
			{
				MessageBox.Show("USAGE: TMMExInstaller -generate <folder path> <installer title> <output>\r\nCreates a generate.ida to distribute with the installer.\r\n"
				                +"TMMExInstaller report <outfile>\r\nCreates a report file for data.dat");
			}
			else
			{
				Application.Run(new MainForm());
			}
		}
		
		
		static DirectoryInfo[] AllDirectories(DirectoryInfo parent)
		{
			DirectoryInfo[] ads = parent.GetDirectories("*", SearchOption.TopDirectoryOnly);
			
			if (ads.Length == 0)
				return new DirectoryInfo[0];
			
			List<DirectoryInfo> rv = new List<DirectoryInfo>();
			rv.AddRange(ads);
			
			foreach(DirectoryInfo di in ads)
			{
				rv.AddRange(AllDirectories(di));
			}
			
			return rv.ToArray();
		}
	}
}
