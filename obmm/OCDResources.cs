/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 15/07/2010
 * Time: 11:59 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using BaseTools.Configuration;
using BaseTools.Configuration.Parsers;

namespace OblivionModManager
{
	/// <summary>
	/// Description of OCDResources.
	/// </summary>
	public static class OCDResources
	{
		static Dictionary<string, object> res = new Dictionary<string, object>();
		public static object GetResource(string name)
		{
			return res[name];
		}
		public static void SetResources(Dictionary<string, object> resx)
		{
			res = resx;
		}
		public static void ClearResources()
		{
			res.Clear();
		}
		public static void AddResource(string name, object resource)
		{
			res[name] = resource;
		}
	}
}
