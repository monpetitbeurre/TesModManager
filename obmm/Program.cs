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
using System.IO;
using System.Collections.Generic;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;
using Formatter = System.Runtime.Serialization.Formatters.Binary.BinaryFormatter;
using Mutex = System.Threading.Mutex;
using System.Security.Principal;
using System.ServiceModel;
using System.Net;
using System.Windows.Forms;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;
//TODO: Scripts shouldn't have write access to the root oblivion folder. Make it read/discovery only
//TODO: BSA browser should be able to preview text files

namespace OblivionModManager {
	public enum LaunchType { None, Game };
	public static class Program {
//		public const byte MajorVersion=1;
//		public const byte MinorVersion=1;
//		public const byte BuildNumber=18;
		public const byte CurrentOmodVersion=4; // omod file version
		public const string version="1.6.0"; // MajorVersion.ToString()+"."+MinorVersion.ToString()+"."+BuildNumber.ToString(); // ;
		public static MainForm ProgramForm = null;
        public static Logger logger = new Logger();

        public static string BaseDir = @"obmm\";
        public static string CorruptDir=@"obmm\corrupt\";
		public static string BackupDir =@"obmm\backup\";
		public static string DataFile =@"obmm\data";
        public static string DataFile2 = @"obmm\data2";
        public static string DataFile3 = @"obmm\data3";
        public static string DataFile4 = @"obmm\data4";
        public static string DataFile5 = @"obmm\data5";
        public static string DataFile6 = @"obmm\data6";
        public static string DataFile7 = @"obmm\data7";
        public static string SettingsFile = @"obmm\settings2";
		public static string BSAEditFile =@"obmm\BSAEdits";
		public const string omodConversionData=@"omod conversion data\";
		public static string HelpPath ="obmm\\obmm.chm";
        public static List<string> loadOrderList = new List<string>();
        //public static int[] progress = { 0, 0 };
        //public static long[] downloadedBytes = { 0, 0 };
        //public static long[] totalBytesToDownload = { 0, 0 };
        public static ProgressForm pf = null;
        public static string nexususername="";
        public static string nexuspassword="";
        public static Dictionary<string, string> dicAuthenticationTokens = null;
        public static string BOSSpath = "";
        public static string LOOTpath = "";
        //        public static List<string> SteamModList = new List<string>();
        public static Dictionary<string, string> SteamModList = new Dictionary<string, string>();
        public static Dictionary<string, FileDownload> downloadList = new Dictionary<string, FileDownload>();
        public static Dictionary<string, FileDownload> downloadingList = new Dictionary<string, FileDownload>();
        public static Dictionary<string, FileDownload> pausedList = new Dictionary<string, FileDownload>();
        public static List<string> importList = new List<string>();
        public static List<string> nexusInfoDownloadList = new List<string>();
        public static List<omod> OmodImagePreloadList = new List<omod>();
        //public static bool bCancelDownload = false;
        //public static string strDownloadToCancel = "";
        public static bool bEnableFileSystemWatcher = false;

//        public const int ExtMaj = MajorVersion, ExtMin = MinorVersion, ExtRev = BuildNumber;

		public static readonly string[] BannedFiles={
			@"shaders\shaderpackage001.sdp",
			@"shaders\shaderpackage002.sdp",
			@"shaders\shaderpackage003.sdp",
			@"shaders\shaderpackage004.sdp",
			@"shaders\shaderpackage005.sdp",
			@"shaders\shaderpackage006.sdp",
			@"shaders\shaderpackage007.sdp",
			@"shaders\shaderpackage008.sdp",
			@"shaders\shaderpackage009.sdp",
			@"shaders\shaderpackage010.sdp",
			@"shaders\shaderpackage011.sdp",
			@"shaders\shaderpackage012.sdp",
			@"shaders\shaderpackage013.sdp",
			@"shaders\shaderpackage014.sdp",
			@"shaders\shaderpackage015.sdp",
			@"shaders\shaderpackage016.sdp",
			@"shaders\shaderpackage017.sdp",
			@"shaders\shaderpackage018.sdp",
			@"shaders\shaderpackage019.sdp",
            //@"video\2k games.bik",
            //@"video\bethesda softworks HD720p.bik",
            //@"video\CreditsMenu.bik",
            //@"video\game studios.bik",
            //@"video\Map loop.bik",
            //@"video\Oblivion iv logo.bik",
            //@"video\Oblivion Legal.bik",
            //@"video\OblivionIntro.bik",
            //@"video\OblivionOutro.bik",
            //@"video\BGS_Logo.bik",
			@"oblivion.esm",
            @"skyrim.esm"
		};

        class serializerThread
        {
            string filename = "";
            Object objToSerialize = null;

            public serializerThread(string filename, Object obj)
            {
                this.filename = filename;
                objToSerialize = obj;
            }

            public void threadRun()
            {
                Stream s = File.Open(filename, FileMode.Create);
                Formatter f = new Formatter();
                f.Serialize(s, objToSerialize);
                s.Close();
            }

        }

        public static string gameName = (Program.bSkyrimMode ? "skyrim" : (Program.bMorrowind ? "morrowind" : "oblivion"));
        public static string gamePath = "";
        public static string DataFolderPath = "Data";
        public static string DataFolderName = "Data";

        public static string TempDir {
			get {
				if(Settings.tempDir.Length==0)
                    Settings.tempDir=Path.GetTempPath()+gameName+@"MM\";
                return Settings.tempDir;
			}
		}
        public static string ConflictsDir
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Settings.conflictsBackupDir))
                    Settings.conflictsBackupDir = Program.DataFolderPath;
                return Settings.conflictsBackupDir;
            }
        }

		public static readonly string CurrentDir=(Path.GetDirectoryName(Application.ExecutablePath)+"\\").ToLower();
        public static string INIDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My games\\" + gameName + "\\");
        public static string ESPDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), gameName+"\\");
        public static readonly string VistaVirtualStore = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "VirtualStore\\"), CurrentDir.Remove(0, 3));

        public static bool bSkyrimMode = false;
        public static bool bSkyrimSEMode = false;
        public static bool bMorrowind = false;
		public static sData Data;
        public static sData Data2;
		public static bool IsLimited=false;
		private static Mutex mutex=null;
		public static LaunchType Launch=LaunchType.None;
        public static System.Windows.Forms.ListView lvEspList = null;


        public static string FindSoftware(string key, string subkey)
        {
            Microsoft.Win32.RegistryKey lm=null, lu=null;
            try
            {
                lm = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\" + key, false);

                lu = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\" + key, false);
            }
            catch
            {
            }

            object o;

            if ((lm != null) && (o = lm.GetValue(subkey)) != null)
                return o.ToString();
            else if ((lu != null) && (o = lu.GetValue(subkey)) != null)
                return o.ToString();
            else
                return null;

        }

		[STAThread]
		public static void Main(string[] args) {

            string apppath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            apppath = apppath.Replace("file:///", "");
            apppath = Path.GetDirectoryName(apppath);
            if (File.Exists(Path.Combine(apppath, "tesv.exe")))
            {
                bSkyrimMode = true;
            }
            else if (File.Exists(Path.Combine(apppath, "SkyrimSE.exe")))
                bSkyrimSEMode = true;
            else if (File.Exists(Path.Combine(apppath, "morrowind.exe")))
                bMorrowind = true;
            gameName = (Program.bSkyrimMode ? "skyrim" : (Program.bMorrowind ? "morrowind" : "oblivion"));
            DataFolderName = Program.bMorrowind ?"Data Files":"Data";

			if(!PreInit(args)) return;
			if(Launch!=LaunchType.None) {
				PostExit();
				return;
			}
			if(!Init()) return;

            nexususername = Properties.Settings.Default.NexusUsername;
            nexuspassword = Properties.Settings.Default.NexusPassword;
//            ProtectedMemory.UnProtect(nexuspassword, MemoryProtectionScope.SameLogon);
            importList = new List<string>(Directory.GetFiles(Path.Combine(Program.BaseDir, "downloads")));
            if (File.Exists(Path.Combine(Program.BaseDir, "downloadlist.txt")))
            {
                List<string> urls= new List<string>(File.ReadAllLines(Path.Combine(Program.BaseDir, "downloadlist.txt")));
                foreach(string url in urls)
                {
                    downloadList.Add(url,new FileDownload(url));
                }
            }
            if (File.Exists(Path.Combine(Program.BaseDir, "pausedlist.txt")))
            {
                List<string> urls = new List<string>(File.ReadAllLines(Path.Combine(Program.BaseDir, "pausedlist.txt")));
                foreach (string url in urls)
                {
                    pausedList.Add(url, new FileDownload(url));
                }
            }
            RunCommandLine(args);
			if(Settings.SafeMode) PostInit();
			//Run!
			if(IsLimited) {
				MessageBox.Show("You appear to be running as a limited user.\n"+
				                "Most of tmm's functionality will be unavailable.\n"+
				                "If using windows vista, remember to check the 'run as administrator' checkbox in tmm's compatibility settings (see note 16 in the FAQ)", "Warning");
				Forms.LimitedUserForm mf=new Forms.LimitedUserForm();
				Application.Run(mf);
			} else {
				Conflicts.UpdateConflicts();
				MainForm mf=new MainForm();
				Application.Run(mf);
			}
			Exit();
			PostExit();
            List<string> dlist = new List<string>(downloadList.Keys);
            File.WriteAllLines(Path.Combine(Program.BaseDir, "downloadlist.txt"), dlist.ToArray());
            List<string> plist = new List<string>(pausedList.Keys);
            File.WriteAllLines(Path.Combine(Program.BaseDir, "pausedlist.txt"), plist.ToArray());

		}

        /// <summary>
        /// Logs the user into the mod repository.
        /// </summary>
        /// <param name="p_strUsername">The username of the account with which to login.</param>
        /// <param name="p_strPassword">The password of the account with which to login.</param>
        /// <param name="p_dicTokens">The returned tokens that can be used to login instead of the username/password
        /// credentials.</param>
        /// <returns><c>true</c> if the login was successful;
        /// <c>fales</c> otherwise.</returns>
        /// <exception cref="RepositoryUnavailableException">Thrown if the repository is not available.</exception>
        public static bool Login(string strWebsite, string UserAgent, string p_strUsername, string p_strPassword, out Dictionary<string, string> p_dicTokens)
        {
            string strSite = strWebsite;
            string strLoginUrl = String.Format("http://{0}/modules/login/do_login.php", strSite);
            HttpWebRequest hwrLogin = (HttpWebRequest)WebRequest.Create(strLoginUrl);
            CookieContainer ckcCookies = new CookieContainer();
            hwrLogin.CookieContainer = ckcCookies;
            hwrLogin.Method = WebRequestMethods.Http.Post;
            hwrLogin.ContentType = "application/x-www-form-urlencoded";
            hwrLogin.UserAgent = UserAgent;

            string strFields = String.Format("user={0}&pass={1}", p_strUsername, p_strPassword);
            byte[] bteFields = System.Text.Encoding.UTF8.GetBytes(strFields);
            hwrLogin.ContentLength = bteFields.Length;

            try
            {
                hwrLogin.GetRequestStream().Write(bteFields, 0, bteFields.Length);
                string strLoginResultPage = null;
                using (WebResponse wrpLoginResultPage = hwrLogin.GetResponse())
                {
                    if (((HttpWebResponse)wrpLoginResultPage).StatusCode != HttpStatusCode.OK)
                        throw new Exception("Request to the login page failed with HTTP error: " + ((HttpWebResponse)wrpLoginResultPage).StatusCode);

                    Stream stmLoginResultPage = wrpLoginResultPage.GetResponseStream();
                    using (StreamReader srdLoginResultPage = new StreamReader(stmLoginResultPage))
                    {
                        strLoginResultPage = srdLoginResultPage.ReadToEnd();
                        srdLoginResultPage.Close();
                    }
                    wrpLoginResultPage.Close();
                }
            }
            catch (WebException e)
            {
                throw new Exception(String.Format("Cannot reach the {0} login server: {1}", "Nexus", strLoginUrl), e);
            }
            //Dictionary<string, string>  
            dicAuthenticationTokens = new Dictionary<string, string>();
            foreach (Cookie ckeToken in ckcCookies.GetCookies(new Uri("http://" + strSite)))
                if (ckeToken.Name.EndsWith("_Member") || ckeToken.Name.EndsWith("_Premium"))
                    dicAuthenticationTokens[ckeToken.Name] = ckeToken.Value;
            p_dicTokens = dicAuthenticationTokens;
            return dicAuthenticationTokens.Count > 0;
        }


        /// <summary>
        /// Returns a factory that is used to create proxies to the repository.
        /// </summary>
        /// <param name="p_booIsGatekeeper">Whether or not we are communicating with the gatekeeper.</param>
        /// <returns>A factory that is used to create proxies to the repository.</returns>
        private static ChannelFactory<INexusModRepositoryApi> GetProxyFactory(bool p_booIsGatekeeper)
        {
            string remoteAddress="";
            if (p_booIsGatekeeper) // "GatekeeperNexusREST" : (Program.bSkyrimMode ? "SKYRIMNexusREST" : "OBNexusREST")
                remoteAddress = "http://nmm.nexusmods.com/"; //"http://gatekeeper.nexusmods.com/";
            else
                remoteAddress = "http://nmm.nexusmods.com/"+Program.gameName+"/";// "http://oblivion.nexusmods.com/";

            System.ServiceModel.WebHttpBinding binding = new WebHttpBinding();
            binding.MaxReceivedMessageSize = 2147483647;
            binding.ReaderQuotas.MaxStringContentLength = 2147483647;
            binding.SendTimeout = TimeSpan.Parse("00:02:00");
            ChannelFactory<INexusModRepositoryApi> cftProxyFactory = new ChannelFactory<INexusModRepositoryApi>(binding, remoteAddress);
            cftProxyFactory.Endpoint.Behaviors.Add(new HttpUserAgentEndpointBehaviour(NexusClient.version)); // version is like "Nexus Client v0.47.1"
            cftProxyFactory.Endpoint.Behaviors.Add(new CookieEndpointBehaviour(dicAuthenticationTokens));
            return cftProxyFactory;
        }

        static string[] GetFileDownloadURLs(string fileid)
        {
            List<string> bestURL = new List<string>();
            using (IDisposable dspProxy = (IDisposable)GetProxyFactory(false).CreateChannel())
            {
                INexusModRepositoryApi nmrApi = (INexusModRepositoryApi)dspProxy;

                int tries = 0;
                List<FileserverInfo> fileservers = new List<FileserverInfo>();
                while (tries < 3)
                {
                    try
                    {
                        fileservers = nmrApi.GetModFileDownloadUrls(fileid, (Program.bSkyrimMode ? 110 : Program.bMorrowind ? 100 : 101));
                        break;
                    }
                    catch (Exception ex)
                    {
                        Program.logger.WriteToLog("Error contacting Nexus server: " + ex.Message, Logger.LogLevel.Low);
                        System.Threading.Thread.Sleep(1000);
                        tries++;
                    }
                }

                if (fileservers.Count == 0)
                    logger.WriteToLog("No server currently hosts file " + fileid, Logger.LogLevel.High);
                else
                {
                    string country = System.Globalization.CultureInfo.CurrentCulture.Name;
                    country = country.Substring(country.IndexOf('-') + 1).ToLower();
                    int connectedUsers = -1;

                    // does my country have servers?
                    bool bCountryHasServers = false;
                    foreach (FileserverInfo fi in fileservers)
                    {
                        if (fi.Country.ToLower().StartsWith(country)) //"us."))
                            bCountryHasServers = true;
                    }
                    //if (!bCountryHasServers)
                    //{
                    //    country = "us"; // default to US servers
                    //}
                    foreach (FileserverInfo fi in fileservers)
                    {
                        if (!bCountryHasServers || fi.Country.ToLower().StartsWith(country)) //"us."))
                        {
                            if (connectedUsers == -1)
                            {
                                connectedUsers = fi.ConnectedUsers;
                                bestURL.Add(fi.DownloadLink);
                            }
                            else if (fi.ConnectedUsers < connectedUsers) // best so far, go to top
                            {
                                connectedUsers = fi.ConnectedUsers;
                                bestURL.Insert(0, fi.DownloadLink);
                            }
                            else
                                bestURL.Add(fi.DownloadLink);
                        }
                    }
                }
                //if (fileservers.Count>0)
                //    return fileservers[0].DownloadLink;
            }
            return bestURL.ToArray();
        }
        public static string GetModIDFromWebsite(string website)
        {
            string id = "";
            if (website!=null && website.Length > 0 && (website.Contains("?id=") || website.Contains("nexusmods.com/")))
            {
                id = website;
                if (website.Contains("?id="))
                {
                    id = id.Substring(id.LastIndexOf("?id="));
                    id = id.Replace("?id=", "");
                }
                else
                {
                    id = id.Substring(id.LastIndexOf("/mods/"));
                    id = id.Replace("/mods/", "");
                }
                if (id.IndexOf("/")!=-1)
                    id = id.Substring(0, id.IndexOf("/"));
            }
            return id;
        }
        public static string GetModID(string filename)
        {
            if (filename == null)
                return "";
            if (filename.Contains("id="))
            {
                filename = filename.Substring(filename.IndexOf("id="));
                filename = filename.Replace("id=", "");
            }
            if (filename.EndsWith("/"))
                filename = filename.Substring(0, filename.LastIndexOf("/"));
            if (filename.EndsWith("/?"))
                filename = filename.Substring(0, filename.LastIndexOf("/?"));
            string shortname = Path.GetFileNameWithoutExtension(filename);
            shortname = shortname.Substring(shortname.IndexOf('-') + 1); // remove main part of filename to get just numbers part
            string[] chunks = shortname.Split('-');

            string nexusFileid = "";
            // check the chunks to see if they are numeric only
            foreach (string chunk in chunks)
            {
                bool isnumeric = true;
                for (int i = 0; i < chunk.Length; i++)
                {
                    if ("0123456789".IndexOf(chunk[i]) == -1)
                    {
                        isnumeric = false;
                        break;
                    }
                }
                if (isnumeric && chunk.Length > 1) // any file ID is at least 99
                {
                    nexusFileid = chunk;
                    break;
                }
            }
            return nexusFileid;
        }

        static string GetFileID(string modid, string filename)
        {
            string lowerfilename = Path.GetFileName(filename).ToLower();
            try
            {
                using (IDisposable dspProxy = (IDisposable)GetProxyFactory(false).CreateChannel())
                {
                    INexusModRepositoryApi nmrApi = (INexusModRepositoryApi)dspProxy;
                    List<NexusModFileInfo> fileinfolist = new List<NexusModFileInfo>();
                    int tries = 0;
                    while (tries < 3)
                    {
                        try
                        {
                            fileinfolist = nmrApi.GetModFiles(modid, (Program.bSkyrimMode ? 110 : Program.bMorrowind?100: 101));
                            break;
                        }
                        catch (Exception ex)
                        {
                            Program.logger.WriteToLog("Error contacting Nexus server: " + ex.Message, Logger.LogLevel.Low);
                            System.Threading.Thread.Sleep(1000);
                            tries++;
                        }
                    }

                    foreach (NexusModFileInfo fileinfo in fileinfolist)
                    {
                        if (fileinfo.Filename.ToLower().CompareTo(lowerfilename) == 0)
                            return fileinfo.Id;
                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        static string GetFileName(string fileid)
        {
            try
            {
                using (IDisposable dspProxy = (IDisposable)GetProxyFactory(false).CreateChannel())
                {
                    INexusModRepositoryApi nmrApi = (INexusModRepositoryApi)dspProxy;

                    int tries = 0;
                    NexusModFileInfo fileinfo = null;
                    while (fileinfo==null && tries < 3)
                    {
                        try
                        {
                            fileinfo = nmrApi.GetModFile(fileid, (Program.bSkyrimMode ? 110 : Program.bMorrowind ? 100 : 101));
                            //break;
                        }
                        catch (Exception ex)
                        {
                            Program.logger.WriteToLog("Error contacting Nexus server: " + ex.Message, Logger.LogLevel.Low);
                        }
                        if (fileinfo == null)
                        {
                            System.Threading.Thread.Sleep(1000);
                            tries++;
                        }
                        else
                            break;
                    }

                    if (fileinfo != null)
                        return fileinfo.Filename;
                    else
                        return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static void GetModInfo(string modid, out string strAuthor, out string strModName, out string strVersion, out string strDescription)
        {
            strAuthor = "";
            strModName = "";
            strVersion = "";
            strDescription = "";
            try
            {
                using (IDisposable dspProxy = (IDisposable)GetProxyFactory(false).CreateChannel())
                {
                    INexusModRepositoryApi nmrApi = (INexusModRepositoryApi)dspProxy;
                    int tries=0;
                    NexusModInfo modinfo = null;
                    while (tries < 3)
                    {
                        try
                        {
                            modinfo = nmrApi.GetModInfo(modid, (Program.bSkyrimMode ? 110 : Program.bMorrowind?100 : 101));
                            break;
                        }
                        catch (Exception ex)
                        {
                            Program.logger.WriteToLog("Error contacting Nexus server: " + ex.Message, Logger.LogLevel.Low);
                            System.Threading.Thread.Sleep(1000);
                            tries++;
                        }
                    }
                    
                    if (modinfo != null)
                    {
                        strAuthor = modinfo.Author;
                        strModName = modinfo.Name;
                        strVersion = modinfo.HumanReadableVersion;
                        strDescription = modinfo.Description;
                    }
                }
            }
            catch
            {
            }
        }

        public static NexusModFileInfo GetFileInfo(string fileid)
        {
            NexusModFileInfo fileinfo = null;
            using (IDisposable dspProxy = (IDisposable)GetProxyFactory(false).CreateChannel())
            {
                INexusModRepositoryApi nmrApi = (INexusModRepositoryApi)dspProxy;

                int tries = 0;
                while (tries < 3)
                {
                    try
                    {
                        fileinfo = nmrApi.GetModFile(fileid, (Program.bSkyrimMode ? 110 : Program.bMorrowind ? 100 : 101));
                        break;
                    }
                    catch (Exception ex)
                    {
                        Program.logger.WriteToLog("Error contacting Nexus server: " + ex.Message, Logger.LogLevel.Low);
                        System.Threading.Thread.Sleep(1000);
                        tries++;
                    }
                }
            }
            return fileinfo;
        }

        public static void GetFileInfo(string fileid, out string strModName, out string strVersion, out string strDescription)
        {
            strModName = "";
            strVersion = "";
            strDescription = "";
            NexusModFileInfo fileinfo = GetFileInfo(fileid);
            if (fileinfo != null)
            {
                //strAuthor = fileinfo.OwnerId;
                strModName = fileinfo.Name;
                strVersion = fileinfo.HumanReadableVersion;
                strDescription = fileinfo.Description;
            }
        }

        public static bool IsLoginValid(ref Dictionary<string, string> p_dicTokens)
        {
            string strCookie = null;
            using (IDisposable dspProxy = (IDisposable)GetProxyFactory(false).CreateChannel())
            {
                INexusModRepositoryApi nmrApi = (INexusModRepositoryApi)dspProxy;

                int tries = 0;
                while (tries < 3)
                {
                    try
                    {
                        strCookie = nmrApi.ValidateTokens();
                        break;
                    }
                    catch (Exception ex)
                    {
                        Program.logger.WriteToLog("Error contacting Nexus server: " + ex.Message, Logger.LogLevel.Low);
                        System.Threading.Thread.Sleep(1000);
                        tries++;
                    }
                }


                if (strCookie != null)
                {
                    dicAuthenticationTokens = new Dictionary<string, string>();
                    if (!String.IsNullOrEmpty(strCookie))
                        dicAuthenticationTokens["sid"] = strCookie;
                    p_dicTokens = dicAuthenticationTokens;
                }
            }

            return strCookie != null;
        }

        /// <summary>
        /// Logs the user into the mod repository.
        /// </summary>
        /// <param name="p_strUsername">The username of the account with which to login.</param>
        /// <param name="p_strPassword">The password of the account with which to login.</param>
        public static string Login(string p_strUsername, string p_strPassword, out Dictionary<string, string> p_dicTokens)
        {
            string strCookie = null;
            try
            {
                using (IDisposable dspProxy = (IDisposable)GetProxyFactory(true).CreateChannel())
                {
                    INexusModRepositoryApi nmrApi = (INexusModRepositoryApi)dspProxy;
                    int tries = 0;

                    while (tries < 3)
                    {
                        try
                        {
                            strCookie = nmrApi.Login(p_strUsername, p_strPassword);
                            break;
                        }
                        catch (Exception ex)
                        {
                            if (tries == 3)
                                throw ex;
                            else
                            {
                                Program.logger.WriteToLog("Error contacting Nexus server: " + ex.Message, Logger.LogLevel.Low);
                                System.Threading.Thread.Sleep(1000);
                                tries++;
                            }
                        }
                    }
                }
            }
            catch (TimeoutException e)
            {
                throw new Exception(String.Format("Cannot reach the login server."), e);
            }
            catch (CommunicationException e)
            {
                if ((((System.Exception)(e)).InnerException != null) && (((System.Net.WebException)(((System.Exception)(e)).InnerException)).Response != null) && (((System.Net.WebException)(((System.Exception)(e)).InnerException)).Response.Headers != null))
                {
                    WebHeaderCollection whcHeaders = ((System.Net.WebException)(((System.Exception)(e)).InnerException)).Response.Headers;
                    string strNexusError = String.Empty;
                    string strNexusErrorInfo = String.Empty;
                    foreach (string Header in whcHeaders.Keys)
                    {
                        switch (Header)
                        {
                            case "NexusError":
                                strNexusError = whcHeaders.GetValues(Header)[0];
                                break;
                            case "NexusErrorInfo":
                                strNexusErrorInfo = whcHeaders.GetValues(Header)[0];
                                break;
                        }
                    }

                    if (!string.IsNullOrEmpty(strNexusError) && (strNexusError == "666"))
                        throw new Exception(strNexusErrorInfo + Environment.NewLine, e);

                }
                throw new Exception(String.Format("Cannot reach the login server."), e);
            }
            catch (System.Runtime.Serialization.SerializationException e)
            {
                throw new Exception(String.Format("Cannot reach the login server."), e);
            }
            //Dictionary<string, string> 
            dicAuthenticationTokens = new Dictionary<string, string>();
            if (!String.IsNullOrEmpty(strCookie))
                dicAuthenticationTokens["sid"] = strCookie;
            p_dicTokens = dicAuthenticationTokens;

            return strCookie;
        }

        //private static void downloadFile(string URL, Dictionary<string,string> Cookies, string UserAgent, string filepath)
        //{
        //    bool booRetry = true;
        //    Int32 intLineTracker = 0;
        //    string ErrorCode="";
        //    string ErrorInfo="";
        //    try
        //    {
        //        for (Int32 i = 0; i < 10 && booRetry; i++)
        //        {
        //            booRetry = false;
        //            HttpWebRequest hwrDownload = (HttpWebRequest)WebRequest.Create(URL);
        //            intLineTracker = 1;
        //            CookieContainer ckcCookies = new CookieContainer();
        //            string host = URL; host = host.Replace("http://", ""); host = host.Substring(0, host.IndexOf('/'));
        //            foreach (KeyValuePair<string, string> kvpCookie in Cookies)
        //                ckcCookies.Add(new Cookie(kvpCookie.Key, kvpCookie.Value, "/", host));
        //            intLineTracker = 2;
        //            hwrDownload.CookieContainer = ckcCookies;
        //            hwrDownload.Method = "GET";
        //            hwrDownload.UserAgent = UserAgent;
        //            hwrDownload.AllowAutoRedirect = true;
        //            intLineTracker = 3;
        //            //hwrDownload.AddRange(p_rngBlockToDownload.StartByte, p_rngBlockToDownload.EndByte);
        //            intLineTracker = 4;
        //            //if (!String.IsNullOrEmpty(m_fmdInfo.ETag))
        //            //    hwrDownload.Headers.Add("If-Match", m_fmdInfo.ETag);
        //            intLineTracker = 5;

        //            try
        //            {
        //                using (HttpWebResponse wrpDownload = (HttpWebResponse)hwrDownload.GetResponse())
        //                {
        //                    intLineTracker = 6;
        //                    if ((wrpDownload.StatusCode != HttpStatusCode.PartialContent) && (wrpDownload.StatusCode != HttpStatusCode.OK))
        //                        return;
        //                    intLineTracker = 7;

        //                    //make sure we have the right file
        //                    string strETag = wrpDownload.GetResponseHeader("ETag");
        //                    //if (!String.Equals(m_fmdInfo.ETag ?? "", strETag ?? ""))
        //                    //    return;
        //                    intLineTracker = 8;

        //                    //make sure we have the right range
        //                    Int32 intTotalFileLength = -1;
        //                    //Range rngRetrievedRange = null;
        //                    if (wrpDownload.StatusCode == HttpStatusCode.PartialContent)
        //                    {
        //                        //intLineTracker = 9;
        //                        //string strRangeValue = wrpDownload.GetResponseHeader("Content-Range");
        //                        //if (String.IsNullOrEmpty(strRangeValue))
        //                        //    return;
        //                        //intLineTracker = 10;
        //                        //string[] strRange = strRangeValue.Split(' ', '-', '/');
        //                        //intLineTracker = 11;
        //                        //if (!strRange[0].Equals("bytes"))
        //                        //    return;
        //                        //intLineTracker = 12;
        //                        //rngRetrievedRange = new Range(Int32.Parse(strRange[1]), Int32.Parse(strRange[2]));
        //                        //intTotalFileLength = Int32.Parse(strRange[3]);
        //                        intLineTracker = 13;
        //                    }
        //                    else if (wrpDownload.StatusCode == HttpStatusCode.OK)
        //                    {
        //                        intLineTracker = 14;
        //                        string strLengthValue = wrpDownload.GetResponseHeader("Content-length");
        //                        if (String.IsNullOrEmpty(strLengthValue))
        //                            return;
        //                        intLineTracker = 15;
        //                        intTotalFileLength = Int32.Parse(strLengthValue);
        //                        intLineTracker = 16;
        //    //									rngRetrievedRange = new Range(0, intTotalFileLength - 1);
        //                        intLineTracker = 17;
        //                    }
        //                    intLineTracker = 18;
        //                    //if (intTotalFileLength != m_fmdInfo.Length)
        //                    //    return;
        //                    intLineTracker = 19;
        //                    //if (!rngRetrievedRange.IsSuperRangeOf(p_rngBlockToDownload))
        //                    //    return;
        //                    intLineTracker = 20;

        //                    using (Stream stmData = wrpDownload.GetResponseStream())
        //                    {
        //                        intLineTracker = 21;
        //                        int iBufSize=10*4*1024;
        //                        using (BufferedStream bsmBufferedData = new BufferedStream(stmData,iBufSize))
        //                        {
        //                            intLineTracker = 22;
        //                            byte[] bteBuffer = new byte[stmData.Length];// m_intBufferSize];
        //                            Int32 intReadCount = 0;
        //                            Int32 intTotalRead = 0;
        //                            intLineTracker = 23;
        //                            bool bKeepRunning=true;
        //                            int iDownloadedByteCount=0;
        //                            FileStream output = File.Create(filepath);
        //                            BinaryWriter bw = new BinaryWriter(output);
        //                            while (bKeepRunning && ((intReadCount = bsmBufferedData.Read(bteBuffer, 0, iBufSize)) > 0))
        //                            {
        //                                intLineTracker = 24;
        //                                byte[] bteData = new byte[intReadCount];
        //                                Array.Copy(bteBuffer, 0, bteData, 0, intReadCount);
        //    //											m_fwrWriter.EnqueueBlock(rngRetrievedRange.StartByte + intTotalRead, bteData);
        //                                bw.Write(bteData);
        //                                intLineTracker = 25;
        //                                intTotalRead += intReadCount;
        //                                iDownloadedByteCount += intReadCount;
        //                            }
        //                            bw.Close();
        //                            intLineTracker = 26;
        //                        }
        //                        intLineTracker = 27;
        //                    }
        //                    intLineTracker = 28;
        //                }
        //                intLineTracker = 29;
        //            }
        //            catch (WebException e)
        //            {
        //                intLineTracker = 31;
        //                //Trace.TraceError(String.Format("[{0}] Block Downloader - Problem getting the block. Status: {1}, Message: {2}", m_fdrFileDownloader.URL, e.Status, e.Message));
        //                if (e.Response != null)
        //                {
        //                    using (HttpWebResponse wrpDownload = (HttpWebResponse)e.Response)
        //                    {
        //                        intLineTracker = 32;
        //                        switch (wrpDownload.StatusCode)
        //                        {
        //                            case HttpStatusCode.ServiceUnavailable:
        //                                foreach (string strKey in wrpDownload.Headers.Keys)
        //                                {
        //                                    switch (strKey)
        //                                    {
        //                                        case "NexusError":
        //                                            ErrorCode = wrpDownload.Headers.GetValues(strKey)[0];
        //                                            break;
        //                                        case "NexusErrorInfo":
        //                                            ErrorInfo = wrpDownload.Headers.GetValues(strKey)[0];
        //                                            break;
        //                                    }
        //                                }

        //                                if (ErrorCode == "666")
        //                                {
        //                                    booRetry = false;
        //                                }
        //                                else if (wrpDownload.Headers.AllKeys[1] == "Retry-After")
        //                                    booRetry = true;
        //                                else
        //                                {
        //                                    intLineTracker = 33;
        //                                    booRetry = false;
        //                                    //this likely means the server has reached it's max
        //                                    // connection limit, so just do nothing
        //                                }
        //                                break;
        //                        }
        //                        intLineTracker = 34;
        //                    }
        //                }
        //                intLineTracker = 35;
        //            }
        //            catch (IOException e)
        //            {
        //                intLineTracker = 36;
        //                //Trace.TraceError(String.Format("[{0}] Block Downloader - Problem getting the block. Message: {1}", m_fdrFileDownloader.URL, e.Message));
        //                intLineTracker = 37;
        //            }
        //            intLineTracker = 38;
        //        }
        //    }
        //    catch (ArgumentOutOfRangeException)
        //    {
        //        //Trace.TraceError(String.Format("[{0}] Block Downloader: ArgumentOutOfRangeException: LineTracker: {1}: Block Range {2}-{3}", m_fdrFileDownloader.URL, intLineTracker, p_rngBlockToDownload.StartByte, p_rngBlockToDownload.EndByte));
        //        throw;
        //    }
        //    catch (NullReferenceException)
        //    {
        //        //Trace.TraceError(String.Format("[{0}] Block Downloader: NullReferenceException: LineTracker: {1}", m_fdrFileDownloader.URL, intLineTracker));
        //        throw;
        //    }
        //    return;
        //}


        public static string getFile(FileDownload download, Dictionary<string, string> cookies, string UserAgent, string dir, System.ComponentModel.BackgroundWorker downloadBackGroundWorker, int threadnumber) //ProgressForm pf)
        {

            string fullpath = "";
            try
            {
                //string fileid = nxmURL.Substring(nxmURL.LastIndexOf("/files/")).Replace("/files/", "");
                //string modid = nxmURL.Substring(nxmURL.LastIndexOf("/mods/")).Replace("/mods/", "");modid=modid.Substring(0,modid.IndexOf('/'));
                //string filename = GetFileName(fileid);
                //if (filename==null)
                //    throw new Exception ("File "+fileid+" of Mod "+modid+" cannot be found ");
                //string url = GetFileDownloadURL(fileid);

                logger.WriteToLog("Downloading " + download.url, Logger.LogLevel.High);
                string strCookie = "";
                foreach (KeyValuePair<string, string> kvpCookie in cookies)
                    strCookie += String.Format("{0}={1};", kvpCookie.Key, kvpCookie.Value);
                if (strCookie.Length == 0)
                    strCookie = "sid=vEBDvvwwFAACCysHvCCtCstFHDxCyxzyCvEusxHy;";

                //bool bAbortDownload = false;
                fullpath = Path.Combine(dir, download.filename);
                WebClient webClient = new WebClient();
                webClient.Headers["Cookie"] = strCookie;

                Uri uri = new Uri(download.url); //response);
                //if (threadnumber == 0)
                //{
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
                    webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadFileCompletedCallback);
                //}
                //else
                //{
                //    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback2);
                //    webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadFileCompletedCallback2);
                //}

                webClient.DownloadFileAsync(uri, fullpath, download);
                //            webClient.DownloadFile(response, fullpath);
                bool bDone = false;
                int lastprogress = 0;
                download.downloadedBytes = 0;
                download.progress = 0;
                download.bCancelled = false;
                System.Threading.Thread.Sleep(10);
                while (!bDone && !download.bCancelled) //bAbortDownload) //(bCancelDownload && nxm == strDownloadToCancel)) //  && !pf.bCancelled) //  && webClient.IsBusy
                {
                    System.Threading.Thread.Sleep(10);
                    Application.DoEvents();
                    //bAbortDownload = (bCancelDownload && download.nxm == strDownloadToCancel);
                    if (download.progress == -1 || (download.totalBytesToDownload == download.downloadedBytes && download.totalBytesToDownload != 0) || !webClient.IsBusy)
                        bDone = true;
                    else if (download.progress > lastprogress)
                    {
                        lastprogress = download.progress;
                        downloadBackGroundWorker.ReportProgress(download.progress, download);
                    //    pf.UpdateProgress(20 + progress / 4);
                    }
                }
                //pf.UpdateProgress(45);
                downloadBackGroundWorker.ReportProgress(download.progress, download);

                if (download.bCancelled) //bAbortDownload)
                {
                    webClient.CancelAsync();
                    webClient.Dispose();

                    try { File.Delete(fullpath); }
                    catch { };
                    fullpath = null;
                }
                else
                    webClient.Dispose();

                if (download.totalBytesToDownload == 0)
                {
                    string servername = download.url;
                    servername = servername.Replace("http://", "");
                    servername = servername.Substring(0, servername.IndexOf('/'));
                    logger.WriteToLog("File is not yet mirrored on server (" + servername + ")", Logger.LogLevel.High);
                    //throw new Exception("File is not yet mirrored on server (" + servername + "). Try again later");
                }
                else if (!download.bCancelled) //bAbortDownload)
                {
                    try
                    {
                        FileStream fs = File.OpenRead(fullpath);
                        fs.Seek(0, SeekOrigin.Begin);
                        byte[] byteHeader = new byte[20]; //fs.Length];
                        fs.Read(byteHeader, 0, (int)byteHeader.Length);
                        fs.Close();
                        string header = "";
                        for (int i = 0; i < byteHeader.Length; i++) header += (char)byteHeader[i];
                        header = header.ToLower();
                        if (header.StartsWith("<!doctype html>"))
                        {
                            if (header.Contains("not logged in"))
                                // could not download file. Not logged in?
                                throw new Exception("Not logged in");
                            else
                                throw new Exception("Unknown error");
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not download " + download.url + ": " + ex.Message);
                logger.WriteToLog("Could not download " + download.url + ": " + ex.Message, Logger.LogLevel.Low);
                //MessageBox.Show("Could not download " + nxmURL + " from nexus: " + ex.Message, "Could not download file from nexus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try { File.Delete(fullpath); }
                catch { };
                fullpath = null;
                throw ex;
            }
            return fullpath;
        }

        private static void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            FileDownload dl = (FileDownload)e.UserState;
            // Displays the operation identifier, and the transfer progress.
            if (e.ProgressPercentage % 10 == 0 && dl.progress != e.ProgressPercentage)
                Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...",
                dl.filename,
                e.BytesReceived,
                e.TotalBytesToReceive,
                e.ProgressPercentage);
            if (dl.progress != -1)
            {
                dl.progress = e.ProgressPercentage;
                dl.downloadedBytes = e.BytesReceived;
                dl.totalBytesToDownload = e.TotalBytesToReceive;

                if (e.BytesReceived == e.TotalBytesToReceive)
                    dl.progress = -1;
            }
        }
        private static void DownloadFileCompletedCallback(Object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            FileDownload dl = (FileDownload)e.UserState;
            Console.WriteLine("Download complete for " + dl.filename);
            dl.progress = -1;
            //progress[0] = -1;
        }
        //private static void DownloadProgressCallback2(object sender, DownloadProgressChangedEventArgs e)
        //{
        //    FileDownload dl = (FileDownload)e.UserState;
        //    // Displays the operation identifier, and the transfer progress.
        //    Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...",
        //        dl.filename,
        //        e.BytesReceived,
        //        e.TotalBytesToReceive,
        //        e.ProgressPercentage);
        //    if (progress[1] != -1)
        //    {
        //        progress[1] = e.ProgressPercentage;
        //        downloadedBytes[1] = e.BytesReceived;
        //        totalBytesToDownload[1] = e.TotalBytesToReceive;

        //        if (e.BytesReceived == e.TotalBytesToReceive)
        //            progress[1] = -1;
        //    }
        //}
        //private static void DownloadFileCompletedCallback2(Object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        //{
        //    Console.WriteLine("Download complete for " + ((FileDownload)e.UserState).filename);
        //    //progress[1] = -1;
        //}
        public static void importFile(string filename, ProgressForm progressform)
        {
            try
            {
                pf = progressform;

                string strTmpDir = "";
                string extension = filename.Substring(filename.LastIndexOf('.') + 1);
                switch (extension)
                {
                    case "zip":
                    case "7z":
                    case "rar":
                        // first extract all files
                        strTmpDir = Program.CreateTempDirectory();
                        SevenZip.SevenZipExtractor zextract = new SevenZip.SevenZipExtractor(filename);

                        string fomodpath = "";
                        bool bBainPackage = false;
                        bool bOmodConversionDataPresent = false;
                        foreach (string file in zextract.ArchiveFileNames)
                        {
                            string lowerfile=file.ToLower();
                            if (lowerfile.Contains("fomod\\info.xml") || lowerfile.Contains("fomod\\moduleconfig.xml") || lowerfile.Contains("fomod\\script.cs")) // || file.ToLower().Contains("fomod\\info.xml")) // info.xml is ONLY mod information, not install information
                            {
                                fomodpath = file;
                            }
                            if (Path.GetFileName(Path.GetDirectoryName(file)).StartsWith("00"))
                            {
                                bBainPackage = true;
                            }
                            if (lowerfile.StartsWith(Program.omodConversionData))
                                bOmodConversionDataPresent = true;
                        }
                        if (fomodpath.Length > 0)
                        {
                            // archive is in fomod format
                            if (DialogResult.Yes == MessageBox.Show("Archive is of type fomod. It can be directly loaded without modification. Do you want to keep it as Fomod?", "Import FOMOD?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                Program.LoadNewOmod(filename);

                                // create a cached file of Nexus info
                                System.Threading.Monitor.Enter(nexusInfoDownloadList);
                                nexusInfoDownloadList.Add(Path.GetFileName(filename));
                                System.Threading.Monitor.Exit(nexusInfoDownloadList);
                                return;
                            }
                        }
                        if (bBainPackage)
                        {
                            // archive is in BAIN format
                            if (DialogResult.Yes == MessageBox.Show("Archive has BAIN structure"+(bOmodConversionDataPresent?" and Omod Conversion data":"")+". It can be directly loaded without modification as BAIN package. Do you want to keep it as BAIN?", "Import BAIN package?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                Program.LoadNewOmod(filename);

                                // create a cached file of Nexus info
                                System.Threading.Monitor.Enter(nexusInfoDownloadList);
                                nexusInfoDownloadList.Add(Path.GetFileName(filename));
                                System.Threading.Monitor.Exit(nexusInfoDownloadList);
                                return;
                            }
                        }



                        //                    sevenZipExtract.PreserveDirectoryStructure = true;
                        //                        int[] indexlist = new int[zextract.ArchiveFileNames.Count];
                        //                        for (int i = 0; i < zextract.ArchiveFileNames.Count; i++)
                        //                            indexlist[i] = i;

                        if (pf != null)
                        {
                            pf.Focus();
                            pf.SetProgressRange(zextract.ArchiveFileNames.Count * 2);
                            pf.UpdateRatio(0.25f);
                            pf.Text = "Extracting " + filename;
                        }
                        FileSystemWatcher fswatch = new FileSystemWatcher(strTmpDir, "*.*");
                        fswatch.IncludeSubdirectories = true;
                        fswatch.Created += fswatch_Created;
                        fswatch.EnableRaisingEvents = true;
                        bEnableFileSystemWatcher = true;

                        //zextract.FileExtractionFinished += new EventHandler<SevenZip.FileInfoEventArgs>(sevenZipExtract_FileExtractionFinished);
                        zextract.Extracting += zextract_Extracting;
                        try
                        {
                            zextract.ExtractFiles(strTmpDir, new List<string>(zextract.ArchiveFileNames).ToArray()); //  (new SevenZip.ExtractFileCallback(extractFileCallback));
                        }
                        finally
                        {
                            zextract.Dispose();
                        }
                        bEnableFileSystemWatcher = false;
                        fswatch.EnableRaisingEvents = false;
                        Application.DoEvents();
                        //fswatch.Dispose();

                        // make sure that the current dir did not change
                        Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));

                        if (pf != null)
                        {
                            pf.Focus();
                            pf.UpdateRatio(0.75f);
                            pf.Text = "Importing data files and plugins... ";
                        }


                        //                    CreateModForm createModForm = new CreateModForm(strTmpDir); // this expects the file to have textures and meshes at the root
                        CreateModForm createModForm = new CreateModForm();
                        createModForm.Text = "Create mod from " + Path.GetFileName(filename);
                        // first check if there is an omod conversion folder or a fomod folder
                        string omodCD = strTmpDir + Program.omodConversionData;
                        string fomodCD = Path.Combine(strTmpDir, "fomod");
                        if ((Directory.Exists(omodCD) || Directory.Exists(fomodCD)) && DialogResult.Yes == MessageBox.Show("Conversion data detected. Do you want to use the automatic import?", "Automatic or manual import", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            createModForm.AddFilesFromFolder(strTmpDir, filename, true);
                        }
                        else
                        {
                            // does the zip contain only a Data folder?
                            List<string> filelist = new List<string>();
                            filelist.AddRange(Directory.GetFiles(strTmpDir,"*.esp"));
                            filelist.AddRange(Directory.GetFiles(strTmpDir, "*.esm"));
                            filelist.AddRange(Directory.GetFiles(strTmpDir, "*.bsa"));
                            string[] txtfiles = Directory.GetFiles(strTmpDir, "*.txt");
                            if (txtfiles.Length > 0)
                                createModForm.ops.readme = File.ReadAllText(txtfiles[0]);
                            if (filelist.Count == 0 && Directory.GetDirectories(strTmpDir, "*.*").Length == 1 && Directory.Exists(Path.Combine(strTmpDir, "Data")))
                                strTmpDir = Path.Combine(strTmpDir,"Data");


                            string[] texturesdirlist = Directory.GetDirectories(strTmpDir, "textures", SearchOption.AllDirectories);
                            string[] meshesdirlist = Directory.GetDirectories(strTmpDir, "meshes", SearchOption.AllDirectories);
                            string[] sounddirlist = Directory.GetDirectories(strTmpDir, "sound", SearchOption.AllDirectories);
                            string[] scriptsdirlist = Directory.GetDirectories(strTmpDir, "scripts", SearchOption.AllDirectories);
                            string[] skyproclist = Directory.GetDirectories(strTmpDir, "SkyProc Patchers", SearchOption.AllDirectories);
                            List<string> dirlist = new List<string>();
                            foreach (string dir in texturesdirlist)
                            {
                                // get the parent directory
                                string dir2 = dir.Substring(0, dir.LastIndexOf('\\') + 1);
                                if (!dirlist.Contains(dir2))
                                    dirlist.Add(dir2);
                            }
                            foreach (string dir in meshesdirlist)
                            {
                                // get the parent directory
                                string dir2 = dir.Substring(0, dir.LastIndexOf('\\') + 1);
                                if (!dirlist.Contains(dir2))
                                    dirlist.Add(dir2);
                            }
                            foreach (string dir in sounddirlist)
                            {
                                // get the parent directory
                                string dir2 = dir.Substring(0, dir.LastIndexOf('\\') + 1);
                                if (!dirlist.Contains(dir2))
                                    dirlist.Add(dir2);
                            }
                            foreach (string dir in skyproclist)
                            {
                                // get the parent directory
                                string dir2 = dir.Substring(0, dir.LastIndexOf('\\') + 1);
                                if (!dirlist.Contains(dir2))
                                    dirlist.Add(dir2);
                            }
                            foreach (string dir in scriptsdirlist)
                            {
                                // get the parent directory
                                string dir2 = dir.Substring(0, dir.LastIndexOf('\\') + 1);
                                if (!dirlist.Contains(dir2))
                                    dirlist.Add(dir2);
                            }
                            bool bPickOptionsAtInstallationTime = false;
                            if (dirlist.Count == 0)
                            {
                                string[] datadirlist = Directory.GetDirectories(strTmpDir, Path.GetFileName(DataFolderPath), SearchOption.AllDirectories);
                                foreach (string dir in datadirlist)
                                {
                                    dirlist.Add(dir);
                                }
                            }
                            if (dirlist.Count > 0)
                            {
                                if (dirlist.Count > 1)
                                {
                                    string[] dirs = dirlist.ToArray();
                                    string[] folderlist = dirlist.ToArray();
                                    for (int curfolder = 0; curfolder < folderlist.Length; curfolder++)
                                    {
                                        if (folderlist[curfolder].EndsWith("\\"))
                                            folderlist[curfolder] = folderlist[curfolder].Substring(0, folderlist[curfolder].Length - 1);
                                        //                                    folderlist[curfolder] = Path.GetFileName(folderlist[curfolder]);
                                        folderlist[curfolder] = folderlist[curfolder].Replace(strTmpDir, "");
                                    }
                                    string[] emptylist = new string[dirs.Length];
                                    Forms.SelectForm sf = new Forms.SelectForm(folderlist, "Pick the directories to include in the mod", true, emptylist, emptylist, false, false); // true);

                                    try
                                    {
                                        Application.UseWaitCursor = false;
                                        sf.ShowDialog();
                                        string[] result = new string[sf.SelectedIndex.Length];
                                        dirlist.Clear();
                                        for (int i = 0; i < sf.SelectedIndex.Length; i++)
                                        {
                                            dirlist.Add(dirs[sf.SelectedIndex[i]]);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.WriteToLog("Could not import file: " + ex.Message, Logger.LogLevel.Low);
                                        if (pf != null)
                                        {
                                            pf.bCancelled = true;
                                            pf.Close();
                                            pf.Dispose();
                                        }
                                        return;
                                    }
                                    finally
                                    {
                                        Application.UseWaitCursor = true;
                                    }
                                    bPickOptionsAtInstallationTime = sf.bCreateInstallationMenuSelected;

                                    //                            MessageBox.Show("More than one data directory has been found. They will be merged but some may contain options that could overwrite each other.","Complex mod warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                                }
                                foreach (string dir in dirlist)
                                {
                                    // import from the directory above except if the installation option has been picked
                                    string dir2 = dir.Substring(0, dir.LastIndexOf('\\') + 1);
                                    if (bPickOptionsAtInstallationTime)
                                    {
                                        dir2 = dir2.Substring(0, dir2.LastIndexOf('\\')); // go above one more level
                                        dir2 = dir2.Substring(0, dir2.LastIndexOf('\\'));
                                        if (Path.GetFileName(dir2).ToLower().CompareTo(DataFolderPath) == 0)
                                        {
                                            dir2 = dir2.Substring(0, dir2.LastIndexOf('\\') + 1); // go above one more level
                                        }
                                    }
                                    createModForm.AddFilesFromFolder(dir2, filename, false); // we need to be able to move ESP/ESMs to base
                                }
                            }


                            string[] bsalist = Directory.GetFiles(strTmpDir, "*.bsa", SearchOption.AllDirectories);
                            string[] esmlist = Directory.GetFiles(strTmpDir, "*.esm", SearchOption.AllDirectories);
                            string[] esplist = Directory.GetFiles(strTmpDir, "*.esp", SearchOption.AllDirectories);
                            List<string> pluginslist = new List<string>(createModForm.ops.esps);
                            List<string> espPathList = new List<string>(createModForm.ops.espPaths);
                            List<string> espSourcesList = new List<string>(createModForm.ops.espSources);

                            foreach (string esm in esmlist)
                            {
                                if (!pluginslist.Contains(esm))
                                {
                                    pluginslist.Add(esm);
                                    espPathList.Add(Path.GetFileName(esm));
                                    espSourcesList.Add(Path.GetFileName(filename));
                                }
                            }
                            foreach (string esp in esplist)
                            {
                                if (!pluginslist.Contains(esp))
                                {
                                    pluginslist.Add(esp);
                                    espPathList.Add(Path.GetFileName(esp));
                                    espSourcesList.Add(Path.GetFileName(filename));
                                }
                            }
                            createModForm.ops.esps = pluginslist.ToArray();
                            createModForm.ops.espPaths = espPathList.ToArray();
                            createModForm.ops.espSources = espSourcesList.ToArray();
                            createModForm.UpdateListView();

                            //                    pluginslist.AddRange(esmlist);
                            //                    pluginslist.AddRange(esplist);
                            //                    createModForm.ops.esps = pluginslist.ToArray();
                            //                    for (int curplugin = 0; curplugin < pluginslist.Count; curplugin++)
                            //                    {
                            //                        pluginslist[curplugin] = Path.GetFileName(pluginslist[curplugin]);
                            //                    }
                            //                    createModForm.ops.espPaths = pluginslist.ToArray();
                            //                    for (int curplugin = 0; curplugin < pluginslist.Count; curplugin++)
                            //                    {
                            //                        pluginslist[curplugin] = Path.GetFileName(filename);
                            //                    }
                            //                    createModForm.ops.espSources = pluginslist.ToArray();



                            List<string> datalist = new List<string>();
                            string[] datafiles = createModForm.ops.DataFiles; // grab the files already stored
                            datalist.AddRange(datafiles);
                            //datalist.AddRange(bsalist);
                            foreach (string bsa in bsalist)
                            {
                                if (!datalist.Contains(bsa))
                                    datalist.Add(bsa);
                            }

                            createModForm.ops.DataFiles = datalist.ToArray();

                            List<string> datafilePathsList = new List<string>(createModForm.ops.DataFilePaths); // grab the files already stored
                            for (int curbsa = 0; curbsa < bsalist.Length; curbsa++)
                            {
                                datafilePathsList.Add(Path.GetFileName(bsalist[curbsa]));
                            }
                            createModForm.ops.DataFilePaths = datafilePathsList.ToArray();

                            List<string> dataFileSourceList = new List<string>(createModForm.ops.DataFilePaths); // grab the files already stored
                            for (int curdata = 0; curdata < bsalist.Length; curdata++)
                            {
                                dataFileSourceList.Add(Path.GetFileName(filename));
                            }
                            createModForm.ops.DataSources = dataFileSourceList.ToArray();

                            if (createModForm.ops.DataFiles.Length == 0 && createModForm.ops.esps.Length == 0)
                            {
                                // nothing found? Let's just add the root folder then
                                createModForm.AddFilesFromFolder(strTmpDir, filename, false);
                            }

                            if (pf != null)
                            {
                                pf.Focus();
                                pf.UpdateRatio(0.95f);
                                pf.Text = "Importing Nexus info... ";
                            }

                            string nexusFileid = Program.GetModID(filename);
                            if (nexusFileid.Length > 0)
                            {
                                if ((GlobalSettings.AlwaysImportTES || MessageBox.Show("Import info from Nexus?", "Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                                    == DialogResult.Yes))
                                {
                                    if (Program.KeyPressed(16))
                                        GlobalSettings.AlwaysImportTES = true;
                                    try
                                    {
                                        createModForm.ApplyTESNexus(filename, false);
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.WriteToLog("Could not get Nexus info: " + ex.Message, Logger.LogLevel.Medium);
                                    }
                                    GlobalSettings.LastTNID = nexusFileid;
                                }
                            }
                            if (pf != null)
                            {
                                pf.Focus();
                                pf.UpdateRatio(1f);
                                pf.Text = "Done";
                                pf.Hide();
                                pf.Close();
                                pf.Dispose();
                                pf = null;
                            }
                        }

                        if (createModForm.ops.Name != null)
                            createModForm.ops.Name = createModForm.ops.Name.Trim();
                        // now check what we have here
                        if (createModForm.ShowForm(false))
                        {
                            //                        UpdateOmodList();
                        }
                        break;
                    case "omod":
                    case "omod2":
                    case "fomod":
                        if (null != Program.LoadNewOmod(filename))
                        {
                            //                        UpdateOmodList();
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not import mod " + filename + ": " + ex.Message, "Error importing mod", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (pf != null)
                {
                    pf.Hide();
                    pf.Close();
                    pf = null;
                }
            }
        }

        static public void fswatch_Created(object sender, FileSystemEventArgs e)
        {
            if (bEnableFileSystemWatcher)
            {
                Console.WriteLine("Created " + e.FullPath);
                if (pf != null)
                    pf.UpdateProgress();
                Application.DoEvents();
            }
        }

        static void zextract_Extracting(object sender, SevenZip.ProgressEventArgs e)
        {
            if (pf != null) pf.UpdateProgress(55 + e.PercentDone / 5);
            Application.DoEvents();
        }

        public static void sevenZipExtract_FileExtractionFinished(object sender, SevenZip.FileInfoEventArgs args)
        {
            Console.WriteLine("Extracted "+args.FileInfo.FileName);
            if (pf!=null) pf.UpdateProgress(55+args.PercentDone/5);
            Application.DoEvents();
        }


        public static bool handleNXMLink(FileDownload download, System.ComponentModel.BackgroundWorker downloadBackgroundWorker, int threadnumber)
        {
            bool bret = false;
            string filename = "";
//            string strWebsite = "";
            //if (Program.bSkyrimMode)
            //    strWebsite = "skyrim.nexusmods.com";
            //else
            //    strWebsite = "oblivion.nexusmods.com";
            string UserAgent = "TesModManager";
            //Dictionary<string, string> 
            

            if (nexususername.Length == 0 || nexuspassword.Length == 0)
            {
                Forms.frmNexusLogin nexuslogin = new Forms.frmNexusLogin(nexususername, nexuspassword);
                nexuslogin.ShowDialog();
                if (!nexuslogin.bCancelled)
                {
                    nexususername = nexuslogin.username;
                    nexuspassword = nexuslogin.password;

                    if (nexuslogin.bRemember)
                    {
                        Properties.Settings.Default.NexusUsername = nexususername;
                        Properties.Settings.Default.NexusPassword = nexuspassword;
                        Properties.Settings.Default.AutoLoginToNexus = true;
                        Properties.Settings.Default.Save();
                    }
                }
            }
            if (nexususername.Length != 0 && nexuspassword.Length != 0)
            {
                //if (pf != null)
                //    pf.Text = "Importing file from Nexus - logging in";
                if (dicAuthenticationTokens == null || !Program.IsLoginValid(ref dicAuthenticationTokens))
                {
                    dicAuthenticationTokens = new Dictionary<string, string>();
                    Login(nexususername, nexuspassword, out dicAuthenticationTokens);
                }


                //pf.UpdateProgress(5);
                Application.DoEvents();
                //pf.Text = "Importing file from Nexus - Downloading...";

                string dir = Path.Combine(Program.BaseDir, "downloads"); //  Program.CreateTempDirectory();
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                try
                {
                    string file = "";
                    string nxmlink = download.nxm;
                    string fileid = nxmlink.Substring(nxmlink.LastIndexOf("/files/")).Replace("/files/", "");
                    string modid = nxmlink.Substring(nxmlink.LastIndexOf("/mods/")).Replace("/mods/", ""); modid = modid.Substring(0, modid.IndexOf('/'));
                    filename = GetFileName(fileid);
                    if (filename == null)
                        throw new Exception("File " + fileid + " of Mod " + modid + " cannot be found ");
                    string[] urllist = GetFileDownloadURLs(fileid);

                    // try all servers if need be
                    foreach (string url in urllist)
                    {
                        download.url = url;
                        download.filename = filename;
                        file = getFile(download, dicAuthenticationTokens, UserAgent, dir, downloadBackgroundWorker, threadnumber);

                        if (file != null)
                            filename = Path.GetFileName(file);

                        //if (!pf.bCancelled)
                        //{
                        //    pf.UpdateProgress(50);
                        //    pf.Text = "Importing file from Nexus - Importing...";

                        if (file != null && file.Length > 0 && (new FileInfo(file).Length > 0))
                        {
                            break;
                        }
                        else
                        {
                            if (File.Exists(file))
                                File.Delete(file);
                        }

                        if (download.bCancelled) //bCancelDownload && nxmlink==strDownloadToCancel)
                            break;
                    }
                    if (file != null && file.Length > 0 && (new FileInfo(file).Length > 0))
                    {
                        System.Threading.Monitor.Enter(importList);
                        importList.Add(file);
                        System.Threading.Monitor.Exit(importList);
                        bret = true;
                    }
                    else if (!download.bCancelled) //bCancelDownload && nxmlink==strDownloadToCancel))
                    {
                        throw new Exception("Could not download file " + (filename.Length > 0 ? filename : "")+(urllist.Length==0?" as no server has it yet. Please retry later.":""));
                    }
                }
                catch (Exception ex)
                {
                    //if (DialogResult.Yes == MessageBox.Show("File could not be retrieved: "+ex.Message+". Remove from list?", "Server error", MessageBoxButtons.YesNo, MessageBoxIcon.Error))
                    //{
                    //    bret = true;
                    //}
                    //else
                        bret = false;
                    logger.WriteToLog("File "+download.filename + " from " +download.nxm+" could not be retrieved: " + ex.Message, Logger.LogLevel.Low);
                }
                //}
                //pf.Hide();
                //pf.Close();
                //pf.Dispose();
                //Application.UseWaitCursor = false;
                //pf = null;
                Application.DoEvents();
            }
            return bret;
        }

		public static void RunCommandLine(string[] args) {
			bool DisplayedWarning=false;
			foreach(string s in args) {
                
                if (s.Contains("nxm:"))
                {
                    System.Threading.Monitor.Enter(downloadList);
                    if (!downloadList.ContainsKey(s) && !importList.Contains(s))
                        downloadList.Add(s,new FileDownload(s));
                    System.Threading.Monitor.Exit(downloadList);

//                    handleNXMLink(s);
                    // we dealt with this one
                    continue;
                }
                 
                switch (Path.GetExtension(s).ToLower()) {
					case ".omod":
                    case ".omod2":
                    case ".fomod":
                        if (IsLimited)
                        {
							if(!DisplayedWarning) {
								MessageBox.Show("Limited users cannot install new mods.\n"+
								                "Please log on as an administrator.", "Error");
							}
							break;
						}
						LoadNewOmod(s);
						break;
					case ".bsa":
						try {
							(new BSABrowser(s)).ShowDialog();
						} catch(Exception e) {
							MessageBox.Show("An error occured\n"+e.Message);
						}
						break;
					default:
						// if(!s.StartsWith("-")) MessageBox.Show("Cannot open '"+s+"'\nUnsupported file type.");
						break;
				}
			}
            if (pf != null)
            {
                pf.Hide();
                pf.Close();
                pf.Dispose();
                pf = null;
            }
		}

		private static bool PreInit(string[] args) {
            string oblivionpath = "";
            string skyrimpath = "";
            string skyrimsepath = "";
            string morrowindpath = "";
            string path = "";

            if ((oblivionpath = FindSoftware(@"Bethesda Softworks\Oblivion", "Installed Path")) != null)
            {
                if (oblivionpath.EndsWith(@"\"))
                    oblivionpath = oblivionpath.Substring(0, oblivionpath.Length - 1);
            }
            else
                oblivionpath = "";
            logger.WriteToLog("Oblivion path: " + oblivionpath, Logger.LogLevel.High);

            if ((path = FindSoftware(@"Valve\Steam", "InstallPath")) != null)
            {
                DirectoryInfo di = new DirectoryInfo(Path.Combine(path, @"steamapps\common\skyrim"));

                if (di.Exists)
                {
                    skyrimpath = di.FullName;
                }
                if (Directory.Exists(Path.Combine(path, @"steamapps\common\Skyrim Special Edition")))
                {
                    skyrimsepath = Path.Combine(path, @"steamapps\common\Skyrim Special Edition");
                }
                if (Directory.Exists(Path.Combine(path, @"steamapps\common\morrowind")))
                {
                    morrowindpath = Path.Combine(path, @"steamapps\common\morrowind");
                }
            }
            logger.WriteToLog("Skyrim path: " + skyrimpath, Logger.LogLevel.High);
            logger.WriteToLog("Morrowind path: " + morrowindpath, Logger.LogLevel.High);

            bool bSkyrimFound = !string.IsNullOrWhiteSpace(skyrimpath);
            bool bSkyrimSEFound = !string.IsNullOrWhiteSpace(skyrimsepath);
            bool bOblivionFound = !string.IsNullOrWhiteSpace(oblivionpath);
            bool bMorrowindFound = !string.IsNullOrWhiteSpace(morrowindpath);

            if ((bSkyrimFound || bSkyrimSEFound) && !bOblivionFound && !bMorrowindFound)
            {
                bSkyrimMode = bSkyrimFound;
                bSkyrimSEMode = bSkyrimSEFound;
                bMorrowind = false;
            }
            else if (!(bSkyrimFound || bSkyrimSEFound)  && bOblivionFound && !bMorrowindFound)
            {
                bSkyrimMode = bSkyrimFound;
                bSkyrimSEMode = bSkyrimSEFound;
                bMorrowind = false;
            }
            else if (!(bSkyrimFound || bSkyrimSEFound)  && !bOblivionFound && bMorrowindFound)
            {
                bSkyrimMode = bSkyrimFound;
                bSkyrimSEMode = bSkyrimSEFound;
                bMorrowind = true;
            }

            // Mode is obvious if TMM is started directly from the game dir
            if (Program.CurrentDir.ToLower() == skyrimpath.ToLower())
            {
                bSkyrimMode = true;
                bSkyrimSEMode = false;
                bMorrowind = false;
            }
            else if (Program.CurrentDir.ToLower() == skyrimsepath.ToLower())
            {
                bSkyrimMode = false;
                bSkyrimSEMode = true;
                bMorrowind = false;
            }
            else if (Program.CurrentDir.ToLower() == morrowindpath.ToLower())
            {
                bSkyrimMode = false;
                bSkyrimSEMode = false;
                bMorrowind = true;
            }
            else if (Program.CurrentDir.ToLower() == oblivionpath.ToLower())
            {
                bSkyrimMode = false;
                bSkyrimSEMode = false;
                bMorrowind = false;
            }

            //Run arguments
            if (args.Length > 0)
            {
                foreach (string arg in args)
                {
                    switch (arg.ToLower())
                    {
                        case "-safe":
                            Settings.SafeMode = true;
                            logger.WriteToLog("SafeMode=true", Logger.LogLevel.High);
                            return true;
                        case "-bsa-creator":
                            logger.WriteToLog("Started in BSACreator Mode", Logger.LogLevel.High);
                            new Forms.BSACreator().ShowDialog();
                            return false;
                        case "-bsa-browser":
                            logger.WriteToLog("Started in BSABrowser Mode", Logger.LogLevel.High);
                            new BSABrowser().ShowDialog();
                            return false;
                        case "-conflict-detector":
                            logger.WriteToLog("Started in conflict detector Mode", Logger.LogLevel.High);
                            new ConflictReport.NewReportGenerator().ShowDialog();
                            return false;
                        case "-launch":
                            logger.WriteToLog("Started in launch game Mode", Logger.LogLevel.High);
                            Launch = LaunchType.Game;
                            return true;
                        case "skyrim":
                            logger.WriteToLog("Skyrim Mode", Logger.LogLevel.High);
                            bSkyrimMode = true;
                            break;
                        case "skyrimse":
                            logger.WriteToLog("Skyrim Special Edition Mode", Logger.LogLevel.High);
                            bSkyrimSEMode = true;
                            break;
                        case "morrowind":
                            logger.WriteToLog("Morrowind Mode", Logger.LogLevel.High);
                            bMorrowind = true;
                            break;
                        case "-d":
                            logger.setLogLevel("high");
                            break;
                    }
                }
            }

            gameName = (Program.bSkyrimMode || Program.bSkyrimSEMode ? "skyrim" : (Program.bMorrowind ? "morrowind" : "oblivion"));


            //if (args.Length>0 && args[0] == "-d")
            //    logger.setLogLevel("high");


            gamePath = (Program.bSkyrimMode ? skyrimpath : (Program.bSkyrimSEMode ? skyrimsepath : (Program.bMorrowind ? morrowindpath : oblivionpath)));

            Program.bSkyrimMode = Program.bSkyrimSEMode;
            DataFolderName = Program.bMorrowind ? "Data Files" : "Data";
            DataFolderPath = Path.Combine(gamePath, DataFolderName);
            BaseDir = Path.Combine(gamePath, "obmm");
            CorruptDir = Path.Combine(BaseDir, "Corrupt");
            BackupDir = Path.Combine(BaseDir, "Backup");
            DataFile = Path.Combine(BaseDir, "Data");
            DataFile2 = DataFile + "2";
            DataFile3 = DataFile + "3";
            DataFile4 = DataFile + "4";
            DataFile5 = DataFile + "5";
            DataFile6 = DataFile + "6";
            DataFile7 = DataFile + "7";
            SettingsFile = Path.Combine(BaseDir, "Settings2");
            BSAEditFile = Path.Combine(BaseDir, "BSAEdits");
            HelpPath = Path.Combine(BaseDir, "obmm.chm");
            Settings.conflictsBackupDir = Program.DataFolderPath;

            if (args.Length > 0 && args[0].Contains("nxm:"))
            {
                if (bSkyrimMode)
                {
                    if (args[0].Contains("Oblivion"))
                    {
                        // start TesModManager for Oblivion
                        System.Diagnostics.Process tmm = new System.Diagnostics.Process();
                        tmm.StartInfo.FileName = oblivionpath + "\\tesmodmanager.exe";
                        tmm.StartInfo.Arguments = args[0];
                        tmm.Start();
                        return false; // not for us
                    }
                    else if (args[0].Contains("Morrowind"))
                    {
                        // start TesModManager for Morrowind
                        System.Diagnostics.Process tmm = new System.Diagnostics.Process();
                        tmm.StartInfo.FileName = morrowindpath + "\\tesmodmanager.exe";
                        tmm.StartInfo.Arguments = args[0];
                        tmm.Start();
                        return false; // not for us
                    }
                }
                else if (bMorrowind)
                {
                    if (args[0].Contains("Oblivion"))
                    {
                        // start TesModManager for Oblivion
                        System.Diagnostics.Process tmm = new System.Diagnostics.Process();
                        tmm.StartInfo.FileName = oblivionpath + "\\tesmodmanager.exe";
                        tmm.StartInfo.Arguments = args[0];
                        tmm.Start();
                        return false; // not for us
                    }
                    else if (args[0].Contains("Skyrim"))
                    {
                        // start TesModManager for Skyrim
                        System.Diagnostics.Process tmm = new System.Diagnostics.Process();
                        tmm.StartInfo.FileName = skyrimpath + "\\tesmodmanager.exe";
                        tmm.StartInfo.Arguments = args[0];
                        tmm.Start();
                        return false; // not for us
                    }
                }
                else
                {
                    if (args[0].Contains("Morrowind"))
                    {
                        // start TesModManager for Morrowind
                        System.Diagnostics.Process tmm = new System.Diagnostics.Process();
                        tmm.StartInfo.FileName = morrowindpath + "\\tesmodmanager.exe";
                        tmm.StartInfo.Arguments = args[0];
                        tmm.Start();
                        return false; // not for us
                    }
                    else if (args[0].Contains("Skyrim"))
                    {
                        // start TesModManager for Skyrim
                        System.Diagnostics.Process tmm = new System.Diagnostics.Process();
                        tmm.StartInfo.FileName = skyrimpath + "\\tesmodmanager.exe";
                        tmm.StartInfo.Arguments = args[0];
                        tmm.Start();
                        return false; // not for us
                    }
                }
            }

            BOSSpath = FindSoftware(@"BOSS", "Installed Path");
            logger.WriteToLog("BOSS path: " + BOSSpath, Logger.LogLevel.High);

            LOOTpath = FindSoftware(@"LOOT", "Installed Path");
            logger.WriteToLog("Loot path: " + LOOTpath, Logger.LogLevel.High);

            if (bSkyrimMode)
            {
                INIDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My games\\skyrim\\");
                ESPDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "skyrim\\");
            }
            else if (bMorrowind)
            {
                INIDir = Program.gamePath;// Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "");
                ESPDir = Program.gamePath;// Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "");
            }
            else
            {
                INIDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My games\\oblivion\\");
                ESPDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "oblivion\\");
            }
            logger.WriteToLog("Ini path: " + INIDir, Logger.LogLevel.High);
            logger.WriteToLog("ESP path: " + ESPDir, Logger.LogLevel.High);



			Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));
			Application.EnableVisualStyles();
			AppDomain.CurrentDomain.UnhandledException+=new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			Application.ThreadException+=new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
			//Check for mutex
			bool creatednew;
			mutex=new Mutex(false, Program.gameName+"MM_mutex", out creatednew);
			if(!creatednew) {
				if(args.Length>0) {
                    File.WriteAllLines("obmm\\pipe", args);
					return false;
				} else {
					MessageBox.Show("Only one instance of TesModManager may be open at any one time.", "Error");
					return false;
				}
			}

			////check it's being run from the game's directory
   //         if ((!Directory.Exists("data") || !File.Exists("oblivion.exe") || !File.Exists("oblivion_default.ini")) &&
   //             (!Directory.Exists("data") || !File.Exists("tesv.exe") || !File.Exists("skyrim_default.ini")) &&
   //             (!Directory.Exists("data files") || !File.Exists("morrowind.exe") || !File.Exists("morrowind.ini")))

   //         {
   //             MessageBox.Show("Starting in '"+Directory.GetCurrentDirectory() +"'. Tes mod manager must be installed to the game's base directory.\n\n" +
   //                         "If you have moved or renamed 'oblivion_default.ini' or 'skyrim_default.ini', please replace it.", "Error");
   //             return false;
   //         }
   //         else
   //         {
   //             logger.WriteToLog((File.Exists("oblivion.exe") ? "oblivion.exe found " : "") + (File.Exists("tesv.exe") ? "tesv.exe found " : "") + (File.Exists("morrowind.exe") ? "morrowind.exe found " : "") +
   //                 (File.Exists("oblivion_default.ini") ? "oblivion_default.ini found " : "") + (File.Exists("skyrim_default.ini") ? "skyrim_default.ini found " : "") + (File.Exists("morrowind_default.ini") ? "morrowind_default.ini found " : ""), Logger.LogLevel.High);
   //         }


            ESPM.RestoreESPM();
			
			//Load settings
			Settings.LoadSettings();
            logger.WriteToLog("*************************************************************", Logger.LogLevel.Low);
            logger.WriteToLog("TesModManager version "+ Program.version+ " started in " + Program.gameName + " mode",Logger.LogLevel.Low);
			////Run arguments
			//if(args.Length>0)
   //         {
   //             foreach (string arg in args)
   //             {
   //                 switch (arg.ToLower())
   //                 {
   //                     case "-safe":
   //                         Settings.SafeMode = true;
   //                         logger.WriteToLog("SafeMode=true", Logger.LogLevel.High);
   //                         return true;
   //                     case "-bsa-creator":
   //                         logger.WriteToLog("Started in BSACreator Mode", Logger.LogLevel.High);
   //                         new Forms.BSACreator().ShowDialog();
   //                         return false;
   //                     case "-bsa-browser":
   //                         logger.WriteToLog("Started in BSABrowser Mode", Logger.LogLevel.High);
   //                         new BSABrowser().ShowDialog();
   //                         return false;
   //                     case "-conflict-detector":
   //                         logger.WriteToLog("Started in conflict detector Mode", Logger.LogLevel.High);
   //                         new ConflictReport.NewReportGenerator().ShowDialog();
   //                         return false;
   //                     case "-launch":
   //                         logger.WriteToLog("Started in launch game Mode", Logger.LogLevel.High);
   //                         Launch = LaunchType.Game;
   //                         return true;
   //                     case "skyrim":
   //                         logger.WriteToLog("Skyrim Mode", Logger.LogLevel.High);
   //                         bSkyrimMode = true;
   //                         break;
   //                     case "morrowind":
   //                         logger.WriteToLog("Morrowind Mode", Logger.LogLevel.High);
   //                         bMorrowind = true;
   //                         break;
   //                 }
   //             }
			//}

            // check registry
            try
            {
                if (Settings.bAskToBeNexusDownloadManager)
                {
                    if (!IsUserWindowsAdministrator())
                    {
                        MessageBox.Show("You need to start TesModManager as administrator if you want to configure it as Nexus Download Manager link handler", "Administrative rights needed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        string regkey = "HKEY_LOCAL_MACHINE\\Software\\Classes\\nxm\\shell\\open\\command";
                        object value = Microsoft.Win32.Registry.GetValue(regkey, "", null);

                        if (value != null)
                        {
                            string cmd = (value as string);
                            logger.WriteToLog(regkey + "=" + cmd, Logger.LogLevel.High);
                            cmd = cmd.ToLower();
                            if (!cmd.Contains("tesmodmanager.exe") && Settings.bAskToBeNexusDownloadManager)
                            {
                                // nxm protocol is registered to another application
                                DialogResult dlgres = MessageBox.Show("Another application is registered as Nexus Download Manager. Do you want to replace it? (This prompt can be disabled in the Settings dialog)", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (dlgres == DialogResult.Yes)
                                {
                                    value = null;
                                }
                            }
                        }
                        if (value == null)
                        {
                            logger.WriteToLog("Registering TMM to handle nxm:// links", Logger.LogLevel.High);
                            string apppath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
                            apppath = apppath.Replace("file:///", "");
                            apppath = Path.GetFullPath(apppath);
                            Microsoft.Win32.Registry.SetValue("HKEY_LOCAL_MACHINE\\Software\\Classes\\nxm", "", "URL: Nexus mod protocol");
                            Microsoft.Win32.Registry.SetValue("HKEY_LOCAL_MACHINE\\Software\\Classes\\nxm", "URL Protocol", Microsoft.Win32.RegistryValueKind.String);
                            Microsoft.Win32.Registry.SetValue("HKEY_LOCAL_MACHINE\\Software\\Classes\\nxm\\DefaultIcon", "", apppath + ",0");
                            Microsoft.Win32.Registry.SetValue("HKEY_LOCAL_MACHINE\\Software\\Classes\\nxm\\shell\\open\\command", "", "\"" + apppath + "\" \"%1\"");
                            logger.WriteToLog("Registered TMM to handle nxm:// links", Logger.LogLevel.High);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // no rights to registry probably
                logger.WriteToLog("Could not register to handle nxm:// : " + ex.Message, Logger.LogLevel.Low);
            }
            return true;
		}



        public static void loadSteamModList()
        {
            if (!Program.SteamModList.ContainsKey("oblivion.esm"))
                Program.SteamModList.Add("oblivion.esm", "Oblivion");
            if (!Program.SteamModList.ContainsKey("bashed patch, 0.esp"))
                Program.SteamModList.Add("bashed patch, 0.esp", "Wrye Bash");

            if (File.Exists(OblivionESP.steammodlist))
            {
                if (!Program.SteamModList.ContainsKey("skyrim.esm")) Program.SteamModList.Add("skyrim.esm", "Skyrim");
                if (!Program.SteamModList.ContainsKey("update.esm")) Program.SteamModList.Add("update.esm", "Skyrim");
                try
                {
                    StreamReader sdlclist = new StreamReader(OblivionESP.steammodlist);
                    while (!sdlclist.EndOfStream)
                    {
                        string espname = sdlclist.ReadLine().ToLower();
                        string modname = sdlclist.ReadLine().ToLower();
                        if (!Program.SteamModList.ContainsKey(espname))
                        {
//                            logger.WriteToLog("Adding "+espname+" to steam mod list", Logger.LogLevel.High);
                            Program.SteamModList.Add(espname, "(Steam) " + modname);
                        }
                        sdlclist.ReadLine(); // skip number
                    }
                    sdlclist.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Steam Mod List cannot be imported: " + ex.Message + ". Please close any application locking SteamModList.txt.");
                    logger.WriteToLog("Steam Mod List cannot be imported: " + ex.Message, Logger.LogLevel.Error);
                }
            }
            if (File.Exists(OblivionESP.dlclist))
            {
                try
                {
                    StreamReader sdlclist = new StreamReader(OblivionESP.dlclist);
                    while (!sdlclist.EndOfStream)
                    {
                        string espname=sdlclist.ReadLine().ToLower();
                        if (!Program.SteamModList.ContainsKey(espname))
                            Program.SteamModList.Add(espname, "(Steam) DLC mod");
                    }
                    sdlclist.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("DLC List cannot be imported: "+ex.Message+". Please close any application locking dlclist.txt.");
                    logger.WriteToLog("DLC Mod List cannot be imported: " + ex.Message, Logger.LogLevel.Error);
                }
            }

        }
        public static void loadLoadOrderTxtFile()
        {
            if (bSkyrimMode)
            {
                if (File.Exists(OblivionESP.loadorder))
                {
                    Program.loadOrderList.Clear();
                    try
                    {
                        StreamReader sloadOrder = new StreamReader(OblivionESP.loadorder);
                        while (!sloadOrder.EndOfStream)
                        {
                            Program.loadOrderList.Add(sloadOrder.ReadLine().ToLower());
                        }
                        sloadOrder.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Load order cannot be imported: " + ex.Message + ". Please close any application locking loadorder.txt.");
                        logger.WriteToLog("Load order cannot be imported: " + ex.Message, Logger.LogLevel.Error);
                    }
                }
            }
            else if (bMorrowind)
            {
                string valuename = "GameFile";
                int curini = 0;
                string esp = "";
                while (esp!=null)
                {
                    esp = OblivionModManager.INI.GetINIValue("[Game Files]", valuename + curini++);
                    if (esp!=null && esp.Length > 0)
                        Program.loadOrderList.Add(esp.ToLower());
                }

            }
            else
            {
                // build it based on file ordering
                // Program.loadOrderList = new List<string>(Directory.GetFiles(Program.ESPDir, "*.es*"));
                // ACTUALLY IGNORE THIS and ONLY use timestamp (otherwise we will miss external modifications
            }
        }

        /// <summary>
        /// Determines whether or not the user belongs to the Windows Administrator group.
        /// </summary>
        public static bool IsUserWindowsAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            try
            {
                //get the currently logged in user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
                if (!isAdmin)
                    isAdmin = principal.IsInRole("BUILTIN\\Administrators");
            }
            catch
            {
                isAdmin = false;
            }
            return isAdmin;
        }

		private static bool Init() {
			//Clear out any tempory files obmm may have created
			ClearTempFiles();
			//Check shader packages are not read only
			if(Directory.Exists(Path.Combine(DataFolderPath,"shaders"))) {
                foreach (FileInfo fi in new DirectoryInfo(Path.Combine(DataFolderPath, "shaders")).GetFiles("*.sdp"))
                {
					if((fi.Attributes&FileAttributes.ReadOnly)>0) fi.Attributes^=FileAttributes.ReadOnly;
				}
			}
			//Create required directories
			try {

                if (!Directory.Exists(BaseDir)) Directory.CreateDirectory(BaseDir);
				if(!Directory.Exists(Settings.omodDir)) Directory.CreateDirectory(Settings.omodDir);
                if (!Directory.Exists(CorruptDir)) Directory.CreateDirectory(CorruptDir);
				if(!Directory.Exists(BackupDir)) Directory.CreateDirectory(BackupDir);
                if (!Directory.Exists(Path.Combine(BaseDir, "downloads")))
                    Directory.CreateDirectory(Path.Combine(BaseDir, "downloads"));
                if (!Directory.Exists(Path.Combine(BaseDir, "cache")))
                    Directory.CreateDirectory(Path.Combine(BaseDir, "cache"));
                if (!Directory.Exists(Path.Combine(Settings.omodDir, "info")))
                    Directory.CreateDirectory(Path.Combine(Settings.omodDir, "info"));

                File.Delete("obmm\\pipe");
			} catch(Exception ex) {
				MessageBox.Show("One or more of Tes Mod Manager's directories do not exist and cannot be created.\n"+
				                "Error: "+ex.Message, "Error");
                logger.WriteToLog("One or more of Tes Mod Manager's directories do not exist and cannot be created.\n" +
                                "Error: " + ex.Message, Logger.LogLevel.Error);
                return false;
			}
			try {
                if (!Directory.Exists(INIDir)) Directory.CreateDirectory(INIDir);
                if (!Directory.Exists(ESPDir)) Directory.CreateDirectory(ESPDir);
            }
            catch (Exception ex)
            {
				MessageBox.Show("One or more of the game directories do not exist and cannot be created.\n"+
				                "Error: "+ex.Message, "Error");
				return false;
			}
			if(!OblivionESP.CreateList()) {
				MessageBox.Show("The game's active esp list is missing and could not be created.", "Error");
				return false;
			}
			if(!INI.CreateINI()) {
				MessageBox.Show("The game's ini file is missing and could not be created.", "Error");
				return false;
			}
			
			try {
				File.Delete("obmm\\limited");
				if(File.Exists(VistaVirtualStore+"obmm\\limited")) File.Delete(VistaVirtualStore+"obmm\\limited");
				FileStream fs=File.Create("obmm\\limited");
				fs.Close();
				if(File.Exists(VistaVirtualStore+"obmm\\limited")) {
					File.Delete("obmm\\limited");
					throw new Exception();
				}
				File.Delete("obmm\\limited");
			} catch {
				IsLimited=true;
                logger.WriteToLog("TMM started in limited mode", Logger.LogLevel.Low);
                Data = new sData();
				return true;
			}
			//If using windows vista, check that the virtual store is empty
			if(Directory.Exists(VistaVirtualStore)) {
				if(new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator)) {
					if(MessageBox.Show("Vista appears to have moved some of the game files to the virtual store.\n"+
					                   "Do you wish to move them back?\n"+
					                   "If you don't know what the virtual store is, please read entry 16 in the FAQ.",
					                   "Warning", System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.Yes) {
						RecersiveDirectoryMove(VistaVirtualStore, CurrentDir, true);
						Directory.Delete(VistaVirtualStore, true);
					}
				} else {
					MessageBox.Show("Vista appears to have moved some of the game files to the virtual store.\n"+
					                "Since obmm doesn't have administrative privileges, it can't move them back\n"+
					                "If you have problems with mods not showing up in game, or omods vanishing from obmm, please read entry 16 in the FAQ.",
					                "Warning");
				}
			}
			//Delete old save files
			if(File.Exists(Path.Combine(BaseDir, "settings"))) {
				MessageBox.Show("obmm 0.8 cannot read save files from 0.7.x or earlier.\n"+
				                "If you did not deactivate your omods before upgrading, it may be necessary to use 'clean all' to tidy up the mess", "Warning");
				File.Delete(Path.Combine(BaseDir, "settings"));
				File.Delete(SettingsFile);
				File.Delete(DataFile);
			}
			//Load more saved settings
			OblivionBSA.ReadArchives();
            if (File.Exists(DataFile7))
            {
                Stream s = File.Open(DataFile7, FileMode.Open);
                try
                {
                    Data = new sData(new BinaryReader(s), 7);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not load save data.\n" +
                        "If you weren't expecting this message, please read entry 15 in the FAQ.\n" +
                        "Error: " + ex.Message, "Error");
                    Data = new sData();
                }
                s.Close();
            }
            if ((Data == null) && File.Exists(DataFile6))
            {
                Stream s = File.Open(DataFile6, FileMode.Open);
                try
                {
                    Data = new sData(new BinaryReader(s), 6);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not load save data.\n" +
                        "If you weren't expecting this message, please read entry 15 in the FAQ.\n" +
                        "Error: " + ex.Message, "Error");
                    Data = new sData();
                }
                s.Close();
            }
            if ((Data == null) && File.Exists(DataFile5))
            {
                Stream s = File.Open(DataFile5, FileMode.Open);
                try
                {
                    Data = new sData(new BinaryReader(s), 5);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not load save data.\n" +
                        "If you weren't expecting this message, please read entry 15 in the FAQ.\n" +
                        "Error: " + ex.Message, "Error");
                    Data = new sData();
                }
                s.Close();
            }
            if ((Data == null) && File.Exists(DataFile4))
            {
                Stream s = File.Open(DataFile4, FileMode.Open);
                try
                {
                    Data = new sData(new BinaryReader(s), 4);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not load save data.\n" +
                        "If you weren't expecting this message, please read entry 15 in the FAQ.\n" +
                        "Error: " + ex.Message, "Error");
                    Data = new sData();
                }
                s.Close();
            }
            if ((Data == null) && File.Exists(DataFile3))
            {
                Stream s = File.Open(DataFile3, FileMode.Open);
                try
                {
                    Data = new sData(new BinaryReader(s),2);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not load save data.\n" +
                        "If you weren't expecting this message, please read entry 15 in the FAQ.\n" +
                        "Error: " + ex.Message, "Error");
                    Data = new sData();
                }
                s.Close();
            }
            if ((Data == null) && (File.Exists(DataFile2)))
            {
                Stream s = File.Open(DataFile2, FileMode.Open);
                try
                {
                    Data = new sData(new BinaryReader(s),1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not load save data.\n" +
                        "If you weren't expecting this message, please read entry 15 in the FAQ.\n" +
                        "Error: " + ex.Message, "Error");
                    Data = new sData();
                }
                s.Close();
            }
            if ((Data==null) && (File.Exists(DataFile)))
            {
                Formatter f = new Formatter();
                Stream s=File.OpenRead(DataFile);
                try
                {
                    Data = (sData)f.Deserialize(s);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not load save data.\n" +
                        "If you weren't expecting this message, please read entry 15 in the FAQ.\n" +
                        "Error: " + ex.Message, "Error");
                }
                s.Close();
            }
            if (Data==null) Data = new sData();

 			//recreate the unserialized data in omods
			foreach(omod o in Data.omods) o.RecreatePrivateData();

            loadLoadOrderTxtFile();

            loadSteamModList();

			//finished sucessfully
			return true;
		}

		private static void PostInit() {
			//Remove any omods that have gone missing from the omod directory
			if(!IsLimited) {
                bool bMissingMods = false;
				for(int i=0;i<Data.omods.Count;i++) {
                    if (!(File.Exists(Path.Combine(Settings.omodDir,((omod)Data.omods[i]).FileName)) ||
                        File.Exists(Path.Combine(Settings.omodDir, ((omod)Data.omods[i]).FileName + ".ghost"))))
                    {
                        logger.WriteToLog(Path.Combine(Settings.omodDir, ((omod)Data.omods[i]).FileName) + " is missing from the mods folder", Logger.LogLevel.Low);
                        if (Settings.bDeactivateMissingOMODs)
                        {
                            if (Data.omods[i].Conflict == ConflictLevel.Active)
                            {
                                //logger.WriteToLog("Deactivating " + ((omod)Data.omods[i]).FileName, Logger.LogLevel.High);
                                Data.omods[i].DeletionDeactivate();
                            }
                            else
                                Data.omods.RemoveAt(i);
                            i--;
                        }
                        bMissingMods = true;
					}
				}
                if (bMissingMods)
                {
                    logger.WriteToLog("Files found:", Logger.LogLevel.High);
                    string[] mods = Directory.GetFiles(Settings.omodDir, "*.*", SearchOption.AllDirectories);
                    foreach (string mod in mods)
                    {
                        logger.WriteToLog(mod, Logger.LogLevel.High);
                    }
                }
                ProgressForm pf = new ProgressForm("Loading mods from folder "+Settings.omodDir, false);
                pf.SetProgressRange(Directory.GetFiles(Settings.omodDir, "*.*").Length);
                pf.Show();
                int curfile = 0;
                try
                {
                    //Load any omods manually placed in the mods directory
                    foreach (string s in Directory.GetFiles(Settings.omodDir, "*.*"))
                    {
                        pf.UpdateProgress(curfile++);
                        if (s.Contains(".xml") || s.Contains(".bak")) continue;
                        if (!Data.DoesModExist(Path.GetFileName(s)))
                        {
                            logger.WriteToLog("Loading new mod "+s+" found in omod directory", Logger.LogLevel.High);
                            LoadNewOmod(Path.Combine(Settings.omodDir,Path.GetFileName(s)));
                            logger.WriteToLog("omod count: "+Program.Data.omods.Count, Logger.LogLevel.High);
                        }
                    }
                }
                finally
                {
                    pf.Hide();
                    pf.Close();
                }
            }
			//Rescan esp headers
			if(Settings.UpdateEsps) { foreach(EspInfo ei in Data.Esps) { if(ei.Parent==null) { ei.GetHeader(); } } }
			//Search for any esps the have appeared since the last time this was run
			List<string> Plugins=new List<string>();
			foreach(string s in Directory.GetFiles(DataFolderPath+"")) {
				if(Path.GetExtension(s)!=".esp"&&Path.GetExtension(s)!=".esm") continue;
				if(!Data.DoesEspExist(Path.GetFileName(s))) {
					Plugins.Add(Path.GetFileName(s));
				}
				FileInfo fi=new FileInfo(s);
				if((fi.Attributes&FileAttributes.ReadOnly)>0) fi.Attributes^=FileAttributes.ReadOnly;
			}
			if(Plugins.Count>0) {
				string[] files=Plugins.ToArray();
				for(int i=0;i<files.Length;i++) {
					EspInfo ei;
					try {
						ei=new EspInfo(files[i]);
					} catch {
						MessageBox.Show("File "+files[i]+" does not appear to be a valid TES plugin", "Error");
						continue;
					}
					ei.Active=false;
					Data.Esps.Add(ei);
				}
			}
			//Check that the plugins we think are active are active
			Plugins.Clear();
			foreach(EspInfo ei in Data.Esps) Plugins.Add(ei.FileName);
			bool[] b=OblivionESP.ArePluginsActive(Plugins.ToArray());
			for(int i=0;i<b.Length;i++) Data.Esps[i].Active=b[i];
			if(!IsLimited) {
				//Remove any plugins which have been deleted since the last time obmm was run
				for(int i=0;i<Data.Esps.Count;i++) {
					if(!File.Exists(Path.Combine(DataFolderPath,Data.Esps[i].FileName))) {
						Data.Esps.RemoveAt(i--);
					}
				}
                ////Remove any data files which have been deleted since the last time obmm was run
                //for(int i=0;i<Data.DataFiles.Count;i++) {
                //    if(!File.Exists(Path.Combine(Program.DataFolderName,Data.DataFiles[i].FileName)) {
                //        foreach(omod o in Data.omods) {
                //            if(o.Conflict==ConflictLevel.Active) {
                //                int j=Array.IndexOf<DataFileInfo>(o.DataFiles, Data.DataFiles[i]);
                //                if(j!=-1) {
                //                    for(int k=j;k<o.DataFiles.Length-1;k++) {
                //                        o.DataFiles[k]=o.DataFiles[k+1];
                //                    }
                //                    Array.Resize<DataFileInfo>(ref o.DataFiles, o.DataFiles.Length-1);
                //                }
                //            }
                //        }
                //        Data.DataFiles.RemoveAt(i--);
                //    }
                //}
			}
			Data.SortEsps();
		}

		private static void Exit() {
            // enforce home directory
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));
			ClearTempFiles();
			if(Settings.LockFOV) {
				try {
					INI.WriteINIValue("[Display]", "fDefaultFOV", "75.0000");
				} catch(UnauthorizedAccessException) {
					MessageBox.Show("Unable to set FOV to 75\n"+
					                "You appear to have the ini file write protected\n"+
					                "The lock FOV option has been disabled, and can be reenabled from the options menu", "Error");
					Settings.LockFOV=false;
				}
			}
            
//			OblivionESP.SetActivePlugins();
			if(!IsLimited) {
				try {
					OblivionBSA.CommitArchives();
				} catch(UnauthorizedAccessException) {
					MessageBox.Show("TesModManager was unable to commit the BSA list to the ini file\n"+
					                "You appear to have the ini file write protected\n"+
					                "The BSA list will now be incorrect, and some mods may not work as intended", "Error");
				}
				if(Settings.UpdateInvalidation) {
					try {
						OblivionBSA.UpdateInvalidationFile();
					} catch(UnauthorizedAccessException) {
						MessageBox.Show("Unable to update archiveinvalidation file\n"+
						                "The file appears to be write protected\n"+
						                "The autoupdate invalidation file option has been disabled, and can be reenabled from the options menu", "Error");
						Settings.UpdateInvalidation=false;

					}
				}
				//Save settings,
				Settings.SaveSettings();
//				Stream s=File.Open(DataFile, FileMode.Create);
//				Formatter f=new Formatter();
//				f.Serialize(s, Data);
//				s.Close();
//                Stream s2 = File.Open(DataFile2, FileMode.Create);
//                Stream s2 = File.Open(DataFile4, FileMode.Create);
                //Stream s2 = File.Open(DataFile5, FileMode.Create);
                //Stream s2 = File.Open(DataFile6, FileMode.Create);
                Stream s2 = File.Open(DataFile7, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(s2);
                Data.WriteTo(bw);
                s2.Close();
			}
		}

		public static void PostExit()
		{
			try
			{
				ESPM.HideESPM();
			}
			catch (Exception ex)
			{
                logger.WriteToLog("Could not hide ESPM: " + ex.Message, Logger.LogLevel.Low);
			}
			switch(Launch)
			{
				case LaunchType.Game:
					if(Settings.UseProcessKiller==1)
					{
						if ((new Forms.ProcessKiller()).ShowDialog() != System.Windows.Forms.DialogResult.Yes)
							break;
						Settings.SaveSettings();
					}
					try
					{

                        ProcessStartInfo pinfo = new ProcessStartInfo();
                        pinfo.WorkingDirectory = Program.gamePath;

                        if (!string.IsNullOrWhiteSpace(Settings.OblivionCommandLine))
						{
                            pinfo.FileName = Path.Combine(System.Environment.SystemDirectory, "cmd.exe");
                            pinfo.Arguments = "/c " + Settings.OblivionCommandLine;
						}
//						else if (File.Exists(@"..\..\..\Steam.exe"))
//						{
//                            if (Program.bSkyrimMode)
//                                System.Diagnostics.Process.Start("steam://rungameid/72850");
//                            else
//                                System.Diagnostics.Process.Start("steam://rungameid/22330");
//                        }
						else if(File.Exists(Path.Combine(Program.gamePath, "obse_loader.exe")))
						{
                            pinfo.FileName = Path.Combine(Program.gamePath, "obse_loader.exe");
						}
                        else if (File.Exists(Path.Combine(Program.gamePath, "skse_loader.exe")))
                        {
                            pinfo.FileName = Path.Combine(Program.gamePath, "skse_loader.exe");
                        }
                        else if (File.Exists(Path.Combine(Program.gamePath, "mwse.exe")))
                        {
                            pinfo.FileName = Path.Combine(Program.gamePath, "mwse.exe");
                        }
                        else
						{
                            if (Program.bSkyrimSEMode)
                                pinfo.FileName = Path.Combine(Program.gamePath, "skyrimSE.exe");
                            else if (Program.bSkyrimMode)
                                pinfo.FileName = Path.Combine(Program.gamePath, "tesv.exe");
                            else if (Program.bMorrowind)
                                pinfo.FileName = Path.Combine(Program.gamePath, "morrowind.exe");
                            else
							    pinfo.FileName = Path.Combine(Program.gamePath, "oblivion.exe");
						}
                        if (!string.IsNullOrWhiteSpace(pinfo.FileName))
                        {
                            System.Diagnostics.Process.Start(pinfo);
                        }
                    }
					catch(Exception ex)
					{
						MessageBox.Show("An error occurred attempting to start the game.\n"+ex.Message, "Error");
					}
					break;
			}
			mutex.Close();
		}

		public static bool IsSafeFileName(string s) {
            if (s==null || s == "") return true;
			s=s.Replace('/', '\\');
			if(s.IndexOfAny(Path.GetInvalidPathChars())!=-1) return false;
			if(Path.IsPathRooted(s)) return false;
			if(s.StartsWith(".")||Array.IndexOf<char>(Path.GetInvalidFileNameChars(), s[0])!=-1) return false;
			if(s.Contains("\\..\\")) return false;
			if(s.EndsWith(".")||Array.IndexOf<char>(Path.GetInvalidFileNameChars(), s[s.Length-1])!=-1) return false;
			return true;
		}

		public static bool IsSafeFolderName(string s) {
			if(s.Length==0) return true;
			s=s.Replace('/', '\\');
			if(s.IndexOfAny(Path.GetInvalidPathChars())!=-1) return false;
			if(Path.IsPathRooted(s)) return false;
            if (Array.IndexOf<char>(Path.GetInvalidFileNameChars(), s[0]) != -1) return false;
			//if(s.StartsWith(".")||Array.IndexOf<char>(Path.GetInvalidFileNameChars(), s[0])!=-1) return false;
			if(s.Contains("\\..\\")) return false;
			//if(s.EndsWith(".")) return false;
			return true;
		}

		public static string MakeValidFolderPath(string s) {
			s=s.Replace('/', '\\');
			if(!s.EndsWith("\\")) s+="\\";
            if (s.StartsWith("\\"))
                s = s.Substring(1);
			return s;
		}

		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
			UnhandledExceptionEventArgs e2=new UnhandledExceptionEventArgs(e.Exception, false);
			CurrentDomain_UnhandledException(sender, e2);
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
			string s1=
				"An unhandled exception occurred."+
				Environment.NewLine+
				"Extra information should have been saved to 'tmm_crashdump.txt' in the game's base directory."+
				Environment.NewLine+Environment.NewLine+
				"Error message: "+((Exception)e.ExceptionObject).Message+Environment.NewLine;

			string s2=
				DateTime.Now.ToLongDateString()+" - "+DateTime.Now.ToLongTimeString()+Environment.NewLine+
				version+(Settings.SafeMode?" (Safe mode)":"")+Environment.NewLine+"OS version: "+Environment.OSVersion.ToString()+
				Environment.NewLine+Environment.NewLine;

			Exception ex=(Exception)e.ExceptionObject;
			while(ex!=null) {
				s2+=
					"Type: "+ex.GetType().ToString()+Environment.NewLine+
					"Error message: "+ex.Message+Environment.NewLine+
					"Stack trace: "+ex.StackTrace+Environment.NewLine;

				ex=ex.InnerException;
				if(ex!=null) s2+=Environment.NewLine+"Inner exception: "+Environment.NewLine;
			}
			MessageBox.Show(s1, "Fatal error");
			try
			{
				File.WriteAllText("tmm_crashdump.txt", s2);
			} catch { }
		}

		public static bool strArrayContains(List<string> a, string s) {
			s=s.ToLower();
			foreach(string s2 in a) {
				if(s2.ToLower()==s) return true;
			}
			return false;
		}
		public static bool strArrayContains(List<DataFileInfo> a, string s) {
			s=s.ToLower();
			for(int i=0;i<a.Count;i++) {
				if(a[i].LowerFileName==s) return true;
			}
			return false;
		}

		public static DataFileInfo strArrayGet(DataFileInfo[] a, string s) {
			s=s.ToLower();
			for(int i=0;i<a.Length;i++) {
				if(a[i].LowerFileName==s) return a[i];
			}
			return null;
		}

		public static void strArrayRemove(List<string> a, string s) {
			s=s.ToLower();
			for(int i=0;i<a.Count;i++) {
				if(a[i].ToLower()==s) {
					a.RemoveAt(i);
					return;
				}
			}
		}
		public static void strArrayRemove(List<DataFileInfo> a, string s) {
			s=s.ToLower();
			for(int i=0;i<a.Count;i++) {
				if(a[i].LowerFileName==s) {
					a.RemoveAt(i);
					return;
				}
			}
		}

		public static void ArrayRemoveAt<T>(ref T[] array, int pos) {
			for(int i=pos+1;i<array.Length;i++) array[i-1]=array[i];
			Array.Resize<T>(ref array, array.Length-1);
		}

		public static omod LoadNewOmod(string s) {
            Program.logger.WriteToLog("LoadNewOmod: " + s, Logger.LogLevel.High);

			//Perform some sanity checking before trying to load the omod
			if(!File.Exists(s)) {
                logger.WriteToLog("Unable to find file '" + s + "'", Logger.LogLevel.High);
                MessageBox.Show("Unable to find file '" + s + "'", "Error");
				return null;
			}
//            if (Path.GetExtension(s) != ".omod" && Path.GetExtension(s) != ".omod2" && Path.GetExtension(s) != ".fomod")
//            {
//				MessageBox.Show("Unrecognized file type. Oblivion mod manager only supports '.omod', '.omod2' and '.fomod' "+
//				                "files", "Error");
//				return null;
//			}
			if(Path.GetDirectoryName(s).ToLower()== Path.GetDirectoryName(Settings.omodDir).ToLower()) {
				return null;
			}
			if(Data.DoesModExist(Path.GetFileName(s))) {
                logger.WriteToLog("The mod '" + Path.GetFileName(s) + "' appears to already be installed.", Logger.LogLevel.High);
                MessageBox.Show("The mod '" + Path.GetFileName(s) + "' appears to already be installed.",
				                "Error");
				return null;
			}
            // Is omod in the mods directory?
            //logger.WriteToLog("Full omod path: " + Path.GetFullPath(s).ToLower(), Logger.LogLevel.High);
            //logger.WriteToLog("Tested path: " + Path.GetFullPath(Settings.omodDir + Path.GetFileName(s)).ToLower(), Logger.LogLevel.High);
            if (Path.GetFullPath(s).ToLower() == Path.Combine(Path.GetFullPath(Settings.omodDir),Path.GetFileName(s)).ToLower())
            {
                logger.WriteToLog("Registering a newly created omod: "+s, Logger.LogLevel.High);
                //Register a newly created omod
				omod newmod=null;
				try {
					newmod=new omod(Path.GetFileName(s),true);
					FileInfo fi=new FileInfo(Path.Combine(Settings.omodDir,Path.GetFileName(s)));
					fi.CreationTime=DateTime.Now;
                    Data.omods.Add(newmod);
                    
					return newmod;
				} catch(Exception ex) {
					if(newmod!=null) newmod.Close();
                    logger.WriteToLog("Error loading '" + s + "'" + ex.Message,Logger.LogLevel.Low);
					MessageBox.Show("Error loading '"+s+"'\n"+ex.Message, "Error");
                    try { File.Delete(CorruptDir + Path.GetFileName(s)); File.Move(s, CorruptDir + Path.GetFileName(s)); }
                    catch { }
					return null;
				}
			} else {
                logger.WriteToLog("Registering a newly downloaded omod: "+s, Logger.LogLevel.High);
                //Register a newly downloaded omod
				try {
					if(Settings.ShowNewModInfo) {
						if(MessageBox.Show("Show omod info for '"+s+"'?", "Question", System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.Yes) {
							(new TextEditor(s, new omod(s,false).GetInfo(), false, false)).ShowDialog();
						}
						if(MessageBox.Show("Install '"+s+"'?", "Question", System.Windows.Forms.MessageBoxButtons.YesNo)!=System.Windows.Forms.DialogResult.Yes) {
							return null;
						}
					}
                    string filename = Path.GetFileName(s);
                    File.Copy(s, Path.Combine(Settings.omodDir,filename),true); //Path.GetFileName(s));
                    omod newmod = new omod(filename, true); // Path.GetFileName(s), true);

                    if (newmod.ModName == null)
                    {
                        string nexusid="";
                        if (newmod.Website!=null)
                            nexusid=newmod.Website.Substring(newmod.Website.LastIndexOf("/")+1);
                        else
                            nexusid = Program.GetModID(newmod.FileName);
                        if (nexusid.Length > 0)
                            newmod.ModName = newmod.FileName.Substring(0, newmod.FileName.IndexOf(nexusid) - 1);
                        else
                            newmod.ModName = Path.GetFileNameWithoutExtension(newmod.FileName);
                    }
                    //if (newmod.CreationTime.Year == 1)
                    //{
                    //    newmod.CreationTime. = new FileInfo(filename).CreationTime;
                    //}
					newmod.Close();
                    FileInfo fi = new FileInfo(Path.Combine(Settings.omodDir,filename)); //Path.GetFileName(s));
					if((fi.Attributes&FileAttributes.ReadOnly)>0) fi.Attributes^=FileAttributes.ReadOnly;
					fi.CreationTime=DateTime.Now;
                    // check if it's an update
                    omod oldmod = Data.GetModByName(newmod.ModName);
                    if (oldmod != null)
                    {
                        if (MessageBox.Show("'" + newmod.ModName + "' version "+newmod.Version+" already exists with version " + oldmod.Version + ".\n\n" +
                                            "* If this is an upgrade then the existing mod will be removed and this one will be installed instead. \n\n" +
                                            "* If this is a patch or an option, you should keep the existing mod in place and install this one next to it. \n\n" +
                                            "* If the existing mod is active and you chose to update, the existing one will be deactivated and the new one activated.\n\n" +
                                           "Do you wish to remove the existing mod and put this one instead?", "Mod exists", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            if (oldmod.Conflict == ConflictLevel.Active)
                            {
                                oldmod.DeletionDeactivate();
                                omod.Remove(oldmod.LowerFileName);
                                Data.omods.Remove(oldmod);
                                Data.omods.Add(newmod);
                                newmod.Activate(true);
                            }
                            else
                            {
                                omod.Remove(oldmod.LowerFileName);
                                Data.omods.Remove(oldmod);
                                Data.omods.Add(newmod);
                            }
                            if (oldmod.Hidden)
                                newmod.Hide(); // conserve the hidden aspect
                            if (newmod.group == 0)
                                newmod.group = oldmod.group; // conserve the groups
                            Conflicts.UpdateConflict(newmod);
                        }
                        else
                        {
                            OblivionModManager.Forms.ModRename frm = new Forms.ModRename(newmod.ModName, filename);
                            frm.ShowDialog();
                            newmod.ModName = frm.newModName;
                        }
                    }
                    else
                    {
                        Conflicts.UpdateConflict(newmod);
                        Data.omods.Add(newmod);
                    }
                    return newmod;
				} catch(Exception ex) {
                    logger.WriteToLog("Error loading '" + s + "'" + ex.Message, Logger.LogLevel.Low);
                    MessageBox.Show("Error loading '" + s + "'\n" + ex.Message, "Error");
					try { File.Delete(Path.Combine(Settings.omodDir,Path.GetFileName(s))); } catch { }
					return null;
				}
			}
		}

		public static FileStream CreateTempFile() {
			string s;
			return CreateTempFile(out s);
		}
		public static FileStream CreateTempFile(out string path) {
			int i=0;
			for(i=0;i<32000;i++) {
				if(!File.Exists(TempDir+"tmp"+i.ToString())) {
					path=TempDir+"tmp"+i.ToString();
					return File.Create(path);
				}
			}
			throw new obmmException("Could not create temp file because directory is full");
		}

		public static string CreateTempDirectory() {
            string dir = "";
			for(int i=0;i<32000;i++) {
//                System.Threading.Monitor.Enter(TempDir);
				if(!Directory.Exists(TempDir+i.ToString())) {
					Directory.CreateDirectory(TempDir+i.ToString()+"\\");
                    dir=TempDir + i.ToString() + "\\";
//                    System.Threading.Monitor.Exit(TempDir);
                    break;
				}
//                System.Threading.Monitor.Exit(TempDir);
            }
            if (dir.Length > 0)
                return dir;
            else
			    throw new obmmException("Could not create temp folder because directory is full");
		}

		public static void ClearTempFiles() { ClearTempFiles(""); }
		public static void ClearTempFiles(string subfolder) {
			if(!Directory.Exists(TempDir) && TempDir.Length>0) Directory.CreateDirectory(TempDir);
			if(!Directory.Exists(TempDir+subfolder)) return;
            try
            {
                foreach (string file in Directory.GetFiles(TempDir + subfolder))
                {
                    try { File.Delete(file); }
                    catch { }
                }
            }
            catch
            { }
			try { if (Directory.Exists(TempDir+subfolder)) Directory.Delete(TempDir+subfolder, true); } catch { }
			if(!Directory.Exists(TempDir)) Directory.CreateDirectory(TempDir);
		}
		
		public static string ReadAllText(string file) {
			if(!File.Exists(file)) return null;
			return File.ReadAllText(file, System.Text.Encoding.Default);
		}

		public static string ReadBString(BinaryReader br, int len) {
			string s="";
			byte[] bs=br.ReadBytes(len);
			foreach(byte b in bs) s+=(char)b;
			return s;
		}
		public static string ReadCString(BinaryReader br) {
			string s="";
			while(true) {
				byte b=br.ReadByte();
				if(b==0) return s;
				s+=(char)b;
			}
		}

		public static void RecersiveDirectoryMove(string from, string to, bool overwrite) {
			from=Path.GetFullPath(from);
			to=Path.GetFullPath(to);
			foreach(string s in Directory.GetFiles(from, "*", SearchOption.AllDirectories)) {
				string subpath=s.Substring(from.Length);
				string topath=Path.Combine(to, subpath);
				string todir=Path.GetDirectoryName(topath);
				if(!Directory.Exists(todir)) Directory.CreateDirectory(todir);
				if(File.Exists(topath)) File.Delete(topath);
				File.Move(s, topath);
			}
		}

		public static bool KeyPressed(int code) {
			GetAsyncKeyState(code);
			return GetAsyncKeyState(code) != 0;
		}

		[System.Security.SuppressUnmanagedCodeSecurity]
		[System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint="GetAsyncKeyState")]
		private static extern short GetAsyncKeyState(int code);

		public static bool IsImageAnimated(System.Drawing.Image image) {
			if(image!=null && image.FrameDimensionsList.Length>0) {
				if(Array.IndexOf<Guid>(image.FrameDimensionsList,System.Drawing.Imaging.FrameDimension.Page.Guid)!=-1) {
					if(image.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page)!=1) return true;
				}
				if(Array.IndexOf<Guid>(image.FrameDimensionsList, System.Drawing.Imaging.FrameDimension.Resolution.Guid)!=-1) {
					if(image.GetFrameCount(System.Drawing.Imaging.FrameDimension.Resolution)!=1) return true;
				}
				if(Array.IndexOf<Guid>(image.FrameDimensionsList, System.Drawing.Imaging.FrameDimension.Time.Guid)!=-1) {
					if(image.GetFrameCount(System.Drawing.Imaging.FrameDimension.Time)!=1) return true;
				}
			}
			return false;
		}


        static TextReader DownloadFile(string url, bool bSilent)
        {
            if (!bSilent)
            {
                Stream s = DownloadForm.DownloadFile(url, bSilent);
                if (s != null)
                    return new StreamReader(s);
                else
                {
                    return null;
                }
            }
            else
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                MemoryStream ms = new MemoryStream(wc.DownloadData(url));
                TextReader tr = new StreamReader(ms);
                return tr;
            }
        }
        //public static string GetTESVersionMP(string fileid, bool bSilent)
        //{
        //    try
        //    {
        //        if (fileid != null)
        //        {
        //            System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //            for (int i = 0; i < fileid.Length; i++)
        //            {
        //                char c = fileid[i];

        //                if ("0123456789".IndexOf(c) != -1)
        //                    sb.Append(c);
        //            }
        //            fileid = sb.ToString();
        //        }

        //        if (fileid != null && fileid.Length > 0)
        //        {
        //            TextReader tr;
        //            tr = DownloadFile("http://" + (Program.bSkyrimMode ? "skyrim" : "oblivion") + ".nexusmods.com/mods/" + fileid, bSilent); // DownloadFile("http://www.tesnexus.com/downloads/file.php?id=" + fileid);

        //            string modVersion = null;
        //            string modName = "File not found";
        //            if (tr != null)
        //            {
        //                string modAuthor = "";
        //                string modImage = null;

        //                Program.getNexusInfo(fileid, tr, ref modName, ref modVersion, ref modAuthor, ref modImage, bSilent);
        //                tr.Close();
        //            }

        //            if (modName != "File not found" && (modVersion==null || modVersion.Length==0))
        //                modVersion = modName;

        //            return modVersion;
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Program.logger.WriteToLog("Could not get TES Version: " + ex.Message, Logger.LogLevel.Low);
        //        return null;
        //    }
        //}
        public static string GetTESVersion(string fileid, bool bSilent)
        {
            string version = "";
            //			CreateModForm.getNexusInfo(fileid,tr,ref modNamw, ref modVersion, ref modAuthor, ref modImage)
            try
            {
                int[] tvers = new int[] { -1, -1, -1 };
                if (fileid != null)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    for (int i = 0; i < fileid.Length; i++)
                    {
                        char c = fileid[i];

                        if ("0123456789".IndexOf(c) != -1)
                            sb.Append(c);
                    }
                    fileid = sb.ToString();
                }

                if (fileid != null && fileid.Length > 0)
                {
                    TextReader tr;
                    tr = DownloadFile("http://www.nexusmods.com/"+Program.gameName+"/ajax/modactionlog/?id=" + fileid, bSilent);
                    bool bVersion = false;
                    string lastline = "";
                    string line = "";
                    if (tr != null)
                    {
                        while ((line = tr.ReadLine()) != null && !bVersion)
                        {
                            if (line.EndsWith("</li>"))
                            {
                                if (line.Contains("file version changed to"))
                                {
                                    version = line.Substring(line.IndexOf('f'));
                                    version = version.Substring(version.IndexOf('\'') + 1);
                                    if (version.IndexOf('\'') != -1)
                                        version = version.Substring(0, version.IndexOf('\''));
                                    version.Replace("'", "");
                                    //string[] stvers = version.Split('.');
                                    //tvers[0] = Convert.ToInt32(stvers[0]);
                                    //tvers[1] = Convert.ToInt32(stvers[1]);
                                    //tvers[2] = Convert.ToInt32(stvers[2]);
                                    bVersion = true;
                                }
                                if (!bVersion && line.Replace("\t", "").Trim().StartsWith("New file:"))
                                {
                                    lastline = lastline.Replace("<span>", "").Replace("</span>", "").Trim().Replace("\t", "");
                                    DateTime convertedDate = new DateTime(); ;
                                    try { convertedDate = DateTime.Parse(lastline); }
                                    catch { };

                                    tvers[0] = convertedDate.Year;
                                    tvers[1] = convertedDate.Month;
                                    tvers[2] = convertedDate.Day;
                                    version = "" + convertedDate.Year + "." + convertedDate.Month + "." + convertedDate.Day;
                                }
                            }
                            lastline = line;
                        }
                    }

                    return version;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static void getNexusInfo(string fileid, TextReader tr, ref string modName, ref string modVersion, ref string modAuthor, ref string modImage, bool bSilent)
        {
            string line;
            modName = null;
            modAuthor = null;
            modVersion = null;
            modImage = null;
            int linecounter = 0;
            bool bFileNotFound = false;
            bool bFileHidden = false;

            if (tr == null)
                return;
            while ((line = tr.ReadLine()) != null)
            {
                linecounter++;
                line = line.Trim();
                if (line.StartsWith("<title>") && line.EndsWith("</title>") && modName == null)
                {
                    modName = line.Replace("<title>", "").Replace("</title>", "");
                }
                else if (line.Contains("header-author") && modAuthor == null && !line.Contains("Site error"))
                {
                    //                    line = tr.ReadLine();

                    //int pos = line.IndexOf("<h1>");
                    //int pos2 = line.IndexOf("<", pos + 1);
                    //if (!line.Contains("File hidden"))
                    //    modName = line.Substring(pos + 4, pos2 - pos - 4);
                    int pos = line.IndexOf("<strong>") + "<strong>".Length;

                    if (!modName.Contains("Adult-only"))
                    {
                        modAuthor = line.Substring(pos); //, line.Length - pos - 8 - 22);
                        modAuthor = modAuthor.Substring(0, modAuthor.IndexOf('<'));
                    }
                }
                else if (line.Contains("file-version") && modVersion == null)
                {
                    line = tr.ReadLine();
                    int pos;
                    pos = line.IndexOf("<strong>");
                    int pos2;
                    pos2 = line.IndexOf("</strong>");
                    modVersion = line.Substring(pos + "<strong>".Length, pos2 - pos - "<strong>".Length);
                }
                else if (line.Contains(".nexusmods.com/"+Program.gameName+"/mods/images/") && modImage == null)
                {
                    line = line.Replace("<a href=\"", "").Replace("\" onclick=\"return hs.expand(this)\">", "");
                    modImage = line;
                }
                else if (line.Contains("File not found"))
                {
                    bFileNotFound = true;
                }
                else if (line.Contains("File hidden"))
                {
                    bFileHidden = true;
                }
            }
            tr.Close();

            if (bFileNotFound)
            {
                modName = "File not found";
                modVersion = "File not found";
                modAuthor = "";
                return;
            }
            if (bFileHidden)
            {
                modName = "File hidden";
                modVersion = "File hidden";
                modAuthor = "";
                return;
            }
            if (modName != null)
            {
                modName = modName.Replace(" - Skyrim mods and community", "");
                modName = modName.Replace(" - Oblivion mods and community", "");
                modName = modName.Replace(" - mods and community", "");
                modName = modName.Replace(" at Skyrim Nexus", "");
                modName = modName.Replace(" at Oblivion Nexus", "");
                modName = modName.Replace(" at Morrowind Nexus", "");
            }
            if (modName != null && modName.Contains("Adult-only") || modVersion == null || modVersion == "")
            {
                modAuthor = Program.gameName+"Nexus";
                tr = DownloadFile("http://www.nexusmods.com/" + Program.gameName+"/ajax/modactionlog/?id=" + fileid, bSilent);
                bool bVersion = false;
                string lastline = "";
                while (tr!=null && (line = tr.ReadLine()) != null)
                {
                    //line = line.Trim();

                    if (line.EndsWith("</title>") && line.Contains("Action log for"))
                    {
                        string actionlog = "<title>Action log for";
                        string onnexus = (!Program.bSkyrimMode ? "on TESNexus</title>" : "on SkyrimNexus</title>");
                        modName = line.Replace("\t", "").Replace(actionlog, "").Replace(onnexus, "").Trim();
                    }
                    else if (!bVersion && line.EndsWith("</li>"))
                    {
                        if (line.Contains("file version changed to"))
                        {
                            string version = line.Substring(line.IndexOf("file version changed to"));
                            version = version.Replace("file version changed to", "").Trim().Replace("'", "");
                            version = System.Net.WebUtility.HtmlDecode(version.Trim()); //.Substring(0, version.IndexOf('<')).Trim();
                            if (version.IndexOf('<') != -1)
                                version = version.Substring(0, version.IndexOf('<')).Trim();
                            if (version.IndexOf('\'') != -1)
                                version = version.Replace("''", "").Trim();
                            if (version.IndexOf('\t') != -1)
                                version = version.Substring(0, version.IndexOf('\t')).Trim();
                            modVersion = version.Replace("'", "");
                            bVersion = true;
                        }
                        if (!bVersion && line.Replace("\t", "").Trim().StartsWith("New file:") && modVersion == "")
                        {
                            lastline = lastline.Replace("<span>", "").Replace("</span>", "").Trim().Replace("\t", "");
                            DateTime convertedDate = new DateTime(); ;
                            try { convertedDate = DateTime.Parse(lastline); }
                            catch { };

                            modVersion = convertedDate.Year + "." + convertedDate.Month + "." + convertedDate.Day;
                        }
                    }
                    lastline = line;
                }
            }
            if (modName == null || modName == "Skyrim Nexus - Skyrim mods and community" || modName == "(Oblivion Nexus - Oblivion mods and community")
            {
                // mod was not found
                if (!bSilent)
                    MessageBox.Show("Mod was not found", "Unknown mod", MessageBoxButtons.OK, MessageBoxIcon.Error);
                modName = "File not found";
                modVersion = "";
                modAuthor = "";
            }
            else
                modName = modName.Replace("</title>", ""); // make sure extra stuff is rmoved

        }

        public static string getModDescription(TextReader tr)
        {
            bool sr = false;
            string line="";
            string modDescription="";
            while ((line = tr.ReadLine()) != null && (modDescription == null || modDescription.Length==0))
            {
                line = line.Trim();
                if (sr)
                {
                    if (line.Length > 0)
                    {
                        line = line.Replace("<br />", "\r\n").Replace("&nbsp;", " ").Replace("&quot;", "\"")
                            .Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        bool ignore = false;
                        for (int i = 0; i < line.Length; i++)
                        {
                            char c = line[i];
                            if (c == '<')
                                ignore = true;
                            else if (c == '>')
                                ignore = false;
                            else if (!ignore)
                                sb.Append(c);
                        }

                        modDescription = sb.ToString();
                    }
                }
                else if (line.Contains("<div class=\"bb-content\">"))
                {
                    sr = true;
                }
            }
            tr.Close();

            return modDescription;
        }

        public static string getModImage(string modid)
        {

            string imagepath = "";

            //List<MemoryStream> mses = DownloadForm.DownloadFiles((!Program.bSkyrimMode ?
            //    new string[] { "http://www.oblivion.nexusmods.com/ajax/modimages/?user=0&id=" + modid } :
            //    new string[] { "http://www.skyrim.nexusmods.com/ajax/modimages/?user=0&id=" + modid }), false);
            TextReader tr;
            string line;
            List<object> modImages = new List<object>();

            string imagespage = "http://www.nexusmods.com/"+Program.gameName + "/ajax/modimages/?user=0&id=" + modid;
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] bytepage = wc.DownloadData(imagespage);
            MemoryStream ms = new MemoryStream(bytepage);


            if (ms != null)
            {
                tr = new StreamReader(ms);
                while ((line = tr.ReadLine()) != null)
                {
                    if (line.Contains("class=\"image\""))
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        bool ignore = true;
                        for (int i = 0; i < line.Length; i++)
                        {
                            char c = line[i];
                            if (ignore)
                            {
                                if (c == '"')
                                {
                                    ignore = false;
                                }
                            }
                            else
                            {
                                if (c == '"')
                                    break;
                                else
                                    sb.Append(c);
                            }
                        }

                        modImages.Add(sb.ToString());
                    }
                }
                tr.Close();
            }

            if (modImages.Count > 0)
            {
                string omodImage;

                if (modImages.Count == 1)
                    omodImage = (string)modImages[0];
                else
                {
                    Random r = new Random();
                    omodImage = modImages[r.Next() % modImages.Count].ToString();
                }

                // check for relative URL
                if (omodImage[0] == '/')
                    omodImage = "http://www.nexusmods.com/" + Program.gameName  + omodImage;
                string path = Program.CreateTempDirectory();
                //System.Net.WebClient wc = new System.Net.WebClient();
                bytepage = wc.DownloadData(omodImage);
                imagepath = Path.Combine(path, "image.jpg");
                File.WriteAllBytes(imagepath, bytepage);

                // verify the content
                try
                {
                    FileStream fs = File.OpenRead(imagepath);
                    fs.Seek(0, SeekOrigin.Begin);
                    byte[] byteHeader = new byte[20]; //fs.Length];
                    fs.Read(byteHeader, 0, (int)byteHeader.Length);
                    fs.Close();
                    string header = "";
                    for (int i = 0; i < byteHeader.Length; i++) header += (char)byteHeader[i];
                    header = header.ToLower();
                    if (header.StartsWith("<!doctype html>"))
                    {
                        // not an image...
                        File.Delete(imagepath);
                        imagepath = "";
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }

            return imagepath;
        }

        /// <summary>
        /// Retrieves mod information
        ///  imagefile can be set to null if not image retrieval is needed
        /// </summary>
        /// <param name="modfile"></param>
        /// <param name="modName"></param>
        /// <param name="modVersion"></param>
        /// <param name="modDescription"></param>
        /// <param name="modAuthor"></param>
        /// <param name="modWebsite"></param>
        /// <param name="imagefile"></param>
        /// <param name="bSilent"></param>
        public static void GetNexusModInfo(string modfile, ref string modName, ref string modVersion, ref string modDescription, ref string modAuthor, ref string modWebsite, ref string imagefile, bool bSilent)
        {
            try
            {
                string modid="";
                if (modfile != null)
                {
                    modid = Program.GetModID(modfile);
                }

                if (modid != null && modid.Length > 0)
                {
                    if (Program.nexususername.Length > 0 && Program.nexuspassword.Length > 0)
                    {
                        if (Program.dicAuthenticationTokens == null || Program.dicAuthenticationTokens.Count == 0)
                            Program.Login(Program.nexususername, Program.nexuspassword, out Program.dicAuthenticationTokens);

                        // get fileid from ModID
                        //string fileid = Program.GetFileID(modid, filename);
                        string strAuthor="", strModName="", strModFileName="", strVersion="", strModVersion="", strDescription="", strModDescription="";
                        Program.GetModInfo(modid, out strAuthor, out strModName, out strModVersion, out strModDescription);
                        string fileid = Program.GetFileID(modid, modfile);
                        if (fileid!=null && fileid.Length>0)
                            Program.GetFileInfo(fileid, out strModFileName, out strVersion, out strDescription);
                        modName = strModName; // (strModFileName.Length > 0 ? strModFileName : strModName); // Mod file name may not be very descriptive...
                        modAuthor = strAuthor;
                        modVersion = (strVersion.Length > 0 ? strVersion : strModVersion);
                        modDescription = strModDescription; // (strDescription.Length > 0 ? strDescription : strModDescription); // Mod file description may not be very descriptive
                        modWebsite = "http://www.nexusmods.com/" + Program.gameName + "/mods/" + modid;
                    }

                    if (modName == null || modName.Length == 0)
                    {

                        List<MemoryStream> mses = DownloadForm.DownloadFiles
                        ((new string[] {
						 	"http://www.nexusmods.com/"+Program.gameName+"/mods/" + modid,
						 	"http://www.nexusmods.com/"+Program.gameName+"/ajax/moddescription/?id=" + modid
						 }), bSilent);

                        if (mses.Count==0 || (mses.Count==1 && mses[0]==null) || (mses.Count==2 && mses[0]==null && mses[1]==null))
                        {
                            throw new Exception("Could not retrieve files from server. Server may be down.");
                        }
                        List<object> modImages = new List<object>();
                        string modImage = null;

                        if (mses.Count > 0 && mses[0] != null)
                        {
                            getNexusInfo(modid, new StreamReader(mses[0]), ref modName, ref modVersion, ref modAuthor, ref modImage, bSilent);
                        }
                        if (mses.Count>1 && mses[1] != null)
                        {
                            modDescription = getModDescription(new StreamReader(mses[1]));
                        }

                        // remove letters
                        string[] tvers = new string[0];

                        if (modVersion == null)
                        {
                            modVersion = Program.GetTESVersion(modid, bSilent);
                        }

                        modWebsite = "http://www.nexusmods.com/"+Program.gameName+"/mods/" + modid;
                    }

                    // was image requested?
                    if (imagefile!=null)
                        imagefile = getModImage(modid);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class FileDownload
    {
        public string url = "";
        public string nxm = "";
        public string filename = "";
        public int progress = 0;
        public int filesize = 0;
        public long downloadedBytes=0;
        public long totalBytesToDownload = 0;
        public bool bCancelled=false;

        public FileDownload(string nxm)
        {
            this.nxm = nxm;
        }
        public FileDownload(string nxm, string fileurl)
        {
            this.nxm = nxm;
            this.url = fileurl;
        }
    }

}
