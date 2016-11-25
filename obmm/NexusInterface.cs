using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Runtime.Serialization;

namespace OblivionModManager
{
    public static class NexusClient
    {
        public const string version = "Nexus Client v0.52.3.0";
    }

    /// <summary>
    /// Describes the methods and properties of the Nexus mod repository.
    /// </summary>
    /// <remarks>
    /// The Nexus mod repository is the repository hosted with the Nexus group of websites.
    /// </remarks>
    [ServiceContract]
    public interface INexusModRepositoryApi
    {
        /// <summary>
        /// Logs a user into the repository.
        /// </summary>
        /// <param name="p_strUsername">The username to authenticate.</param>
        /// <param name="p_strPassword">The password to authenticate.</param>
        /// <returns>An authentication token if the credentials are valid; <c>null</c> otherwise.</returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Sessions/?Login&username={p_strUsername}&password={p_strPassword}",
            ResponseFormat = WebMessageFormat.Json)]
        string Login(string p_strUsername, string p_strPassword);

        /// <summary>
        /// Validates the current security tokens.
        /// </summary>
        /// <returns>An authentication token if the tokens are valid; <c>null</c> otherwise.</returns>
        [OperationContract]
        [WebInvoke(
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Sessions/?Validate",
            ResponseFormat = WebMessageFormat.Json)]
        string ValidateTokens();

        /// <summary>
        /// Toggles the mod Endorsement state.
        /// </summary>
        /// <param name="p_strModId">The mod ID.</param>
        /// <param name="p_intLocalState">The local Endorsement state.</param>
        /// <returns>The updated online Endorsement state.</returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Mods/toggleendorsement/{p_strModId}?lvote={p_intLocalState}&game_id={p_intGameId}",
            ResponseFormat = WebMessageFormat.Json)]
        bool ToggleEndorsement(string p_strModId, int p_intLocalState, int p_intGameId);

        /// <summary>
        /// Gets the info about the specified mod from the repository.
        /// </summary>
        /// <param name="p_strModId">The id of the mod for which to retrieved the metadata.</param>
        /// <returns>The info about the specified mod from the repository.</returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Mods/{p_strModId}/?game_id={p_intGameId}",
            ResponseFormat = WebMessageFormat.Json)]
        NexusModInfo GetModInfo(string p_strModId, int p_intGameId);

        /// <summary>
        /// Gets the info about the specified mod list from the repository.
        /// </summary>
        /// <param name="p_strModList">The mod list for which to retrieved the metadata.</param>
        /// <returns>The info about the specified mod list from the repository.</returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Mods/GetUpdates?ModList={p_strModList}&game_id={p_intGameId}",
            ResponseFormat = WebMessageFormat.Json)]
        NexusModInfo[] GetModListInfo(string p_strModList, int p_intGameId);

        /// <summary>
        /// Gets the files associated with the specified mod from the repository.
        /// </summary>
        /// <param name="p_strModId">The id of the mod for which to retrieved the associated files.</param>
        /// <returns>The files associated with the specified mod from the repository.</returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Files/indexfrommod/{p_strModId}?game_id={p_intGameId}",
            ResponseFormat = WebMessageFormat.Json)]
        List<NexusModFileInfo> GetModFiles(string p_strModId, int p_intGameId);

        /// <summary>
        /// Gets the file info for the specified download file of the specified mod.
        /// </summary>
        /// <param name="p_strFileId">The id of the download file whose metadata is to be retrieved.</param>
        /// <returns>The file info for the specified download file of the specified mod.</returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Files/{p_strFileId}/?game_id={p_intGameId}",
            ResponseFormat = WebMessageFormat.Json)]
        NexusModFileInfo GetModFile(string p_strFileId, int p_intGameId);

        /// <summary>
        /// Gets the download URLs of all the parts associated with the specified file.
        /// </summary>
        /// <param name="p_strFileId">The id of the file for which to retrieve the URLs.</param>
        /// <returns>The download URLs of all the parts associated with the specified file.</returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Files/download/{p_strFileId}/?game_id={p_intGameId}",
            ResponseFormat = WebMessageFormat.Json)]
        List<FileserverInfo> GetModFileDownloadUrls(string p_strFileId, int p_intGameId);

        /// <summary>
        /// Gets the user credentials.
        /// </summary>
        /// <returns>The user credentials (User ID, Name and Status).</returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Core/Libs/Flamework/Entities/User?GetCredentials&game_id={p_intGameId}",
            ResponseFormat = WebMessageFormat.Json)]
        string[] GetCredentials(int p_intGameId);

        /// <summary>
        /// Finds the mods containing the given search terms.
        /// </summary>
        /// <param name="p_strModNameSearchString">The terms to use to search for mods.</param>
        /// <param name="p_strType">Whether the returned mods' names should include all of
        /// the given search terms, or any of the terms.</param>
        /// <returns>The mod info for the mods matching the given search criteria.</returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Mods/?Find&name={p_strModNameSearchString}&type={p_strType}&game_id={p_intGameId}",
            ResponseFormat = WebMessageFormat.Json)]
        List<NexusModInfo> FindMods(string p_strModNameSearchString, string p_strType, int p_intGameId);

        /// <summary>
        /// Finds the mods for the given Author.
        /// </summary>
        /// <param name="p_strModNameSearchString">The terms to use to search for mods.</param>
        /// <param name="p_strType">Whether the returned mods' names should include all of
        /// the given search terms, or any of the terms.</param>
        /// <param name="p_strAuthorSearchString">The Author to use to search for mods.</param>
        /// <returns>The mod info for the mods matching the given search criteria.</returns>
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Mods/?Find&name={p_strModNameSearchString}&author={p_strAuthorSearchString}&type={p_strType}&game_id={p_intGameId}",
            ResponseFormat = WebMessageFormat.Json)]
        List<NexusModInfo> FindModsAuthor(string p_strModNameSearchString, string p_strType, string p_strAuthorSearchString, int p_intGameId);
    }


    /// <summary>
    /// Describes the metadata of a mod in the Nexus repository.
    /// </summary>
    [DataContract]
    public class NexusModInfo
    {
        #region Properties

        /// <summary>
        /// Gets or sets whether the mod contains adult material.
        /// </summary>
        /// <value>Whether the mod contains adult material.</value>
        [DataMember(Name = "adult")]
        public bool IsAdult { get; set; }

        /// <summary>
        /// Gets or sets the category of the mod.
        /// </summary>
        /// <value>The category of the mod.</value>
        [DataMember(Name = "category_id")]
        public Int32 CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the description of the mod.
        /// </summary>
        /// <value>The description of the mod.</value>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Id of the mod.
        /// </summary>
        /// <value>The Id of the mod.</value>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the last updated date of the mod.
        /// </summary>
        /// <value>The last updated date of the mod.</value>
        [DataMember(Name = "lastupdate")]
        public DateTime? LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the name of the mod.
        /// </summary>
        /// <value>The name of the mod.</value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the author of the mod.
        /// </summary>
        /// <value>The author of the mod.</value>
        [DataMember(Name = "author")]
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the owner of the mod.
        /// </summary>
        /// <value>The owner of the mod.</value>
        [DataMember(Name = "owner_id")]
        public Int32 OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the summary of the mod.
        /// </summary>
        /// <value>The summary of the mod.</value>
        [DataMember(Name = "summary")]
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the human readable mod version.
        /// </summary>
        /// <value>The human readable mod version.</value>
        [DataMember(Name = "version")]
        public string HumanReadableVersion { get; set; }

        /// <summary>
        /// Gets or sets the endorsement state.
        /// </summary>
        /// <value>The endorsement state.</value>
        [DataMember(Name = "voted_by_user")]
        public bool IsEndorsed { get; set; }

        #endregion
    }

    /// <summary>
    /// Describes the metadata of a file of a mod in the Nexus repository.
    /// </summary>
    [DataContract]
    public class NexusModFileInfo : IModFileInfo
    {
        #region Properties

        /// <summary>
        /// Gets or sets the file id.
        /// </summary>
        /// <value>The file id.</value>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the id of the mod to which the file belongs.
        /// </summary>
        /// <value>The id of the mod to which the file belongs.</value>
        [DataMember(Name = "mod_id")]
        public Int32 ModId { get; set; }

        /// <summary>
        /// Gets or sets the owner of the file.
        /// </summary>
        /// <value>The owner of the file.</value>
        [DataMember(Name = "owner_id")]
        public Int32 OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the file cateogry.
        /// </summary>
        /// <value>The file cateogry.</value>
        [DataMember(Name = "category_id")]
        public ModFileCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the display name of the file.
        /// </summary>
        /// <value>The display name of the file.</value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the file.
        /// </summary>
        /// <value>The description of the file.</value>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        /// <value>The filename.</value>
        [DataMember(Name = "uri")]
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the file size.
        /// </summary>
        /// <value>The file size.</value>
        [DataMember(Name = "size")]
        public UInt32 Size { get; set; }

        /// <summary>
        /// Gets or sets the human readable version of the mod to which the file belongs.
        /// </summary>
        /// <value>The human readable version of the mod to which the file belongs.</value>
        [DataMember(Name = "version")]
        public string HumanReadableVersion { get; set; }

        /// <summary>
        /// Gets or sets the date the file was loaded into the repository.
        /// </summary>
        /// <value>The date the file was loaded into the repository.</value>
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        #endregion
    }

    /// <summary>
    /// The mod file categories.
    /// </summary>
    public enum ModFileCategory
    {
        /// <summary>
        /// Indicates the file ia a main file in the mod.
        /// </summary>
        MainFiles = 1,

        /// <summary>
        /// Indicates the files is an update for the mod.
        /// </summary>
        Updates = 2,

        /// <summary>
        /// Indicates teh file is optional for the mod.
        /// </summary>
        OptionalFiles = 3,

        /// <summary>
        /// Indicates the file is for an old version of the mod.
        /// </summary>
        OldVersions = 4,

        /// <summary>
        /// Indicates the files is a support file for the mod.
        /// </summary>
        Misc = 5
    }
    /// <summary>
    /// Describes the metadata of a file of a mod in a repository.
    /// </summary>
    public interface IModFileInfo
    {
        #region Properties

        /// <summary>
        /// Gets the file id.
        /// </summary>
        /// <value>The file id.</value>
        string Id { get; }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        /// <value>The filename.</value>
        string Filename { get; }

        /// <summary>
        /// Gets the friendly name of the file.
        /// </summary>
        /// <value>The friendly name of the file.</value>
        string Name { get; }

        /// <summary>
        /// Gets or sets the human readable version of the mod to which the file belongs.
        /// </summary>
        /// <value>The human readable version of the mod to which the file belongs.</value>
        string HumanReadableVersion { get; }

        #endregion
    }

    /// <summary>
    /// Describes the metadata of a fileserver.
    /// </summary>
    [DataContract]
    public class FileserverInfo
    {
        #region Properties

        /// <summary>
        /// Gets the download link.
        /// </summary>
        /// <value>The download link.</value>
        [DataMember(Name = "URI")]
        public string DownloadLink { get; set; }

        /// <summary>
        /// Gets whether the server is Premium or Normal.
        /// </summary>
        /// <value>True if the server is Premium.</value>
        [DataMember(Name = "IsPremium")]
        public bool IsPremium { get; set; }

        /// <summary>
        /// Gets the fileserver name.
        /// </summary>
        /// <value>The fileserver name.</value>
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets the fileserver country.
        /// </summary>
        /// <value>The fileserver country.</value>
        [DataMember(Name = "Country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets the number of users currently connected to the server.
        /// </summary>
        /// <value>The number of users currently connected to the server.</value>
        [DataMember(Name = "ConnectedUsers")]
        public Int32 ConnectedUsers { get; set; }

        #endregion
    }

    /// <summary>
    /// An HTTP Endpoint Behaviour that sets the user-agent for the service call.
    /// </summary>
    public class HttpUserAgentEndpointBehaviour : WebHttpBehavior
    {
        private string m_userAgent;

        #region Constructors

        /// <summary>
        /// A simple constructor that initializes the object with the given values.
        /// </summary>
        /// <param name="userAgent">The user agent to use for the service calls.</param>
        public HttpUserAgentEndpointBehaviour(string userAgent)
        {
            this.m_userAgent = userAgent;
        }

        #endregion

        /// <summary>
        /// Injects the user agent into the service request.
        /// </summary>
        /// <param name="endpoint">The enpoint of the service for which to set the user-agent.</param>
        /// <param name="clientRuntime">The client runtime.</param>
        public override void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            base.ApplyClientBehavior(endpoint, clientRuntime);
            HttpUserAgentMessageInspector amiInspector = new HttpUserAgentMessageInspector(this.m_userAgent);
            clientRuntime.MessageInspectors.Add(amiInspector);

        }
    }

    /// <summary>
    /// An Client Message Inspector that sets the user-agent for the service call.
    /// </summary>
    public class HttpUserAgentMessageInspector : IClientMessageInspector
    {
        private const string USER_AGENT_HTTP_HEADER = "user-agent";
        private string m_userAgent = null;

        #region Constructors

        /// <summary>
        /// A simple constructor that initializes the object with the given values.
        /// </summary>
        /// <param name="userAgent">The user agent to use for the service calls.</param>
        public HttpUserAgentMessageInspector(string userAgent)
        {
            this.m_userAgent = userAgent;
        }

        #endregion

        #region IClientMessageInspector Members

        /// <summary>
        /// Processes the reply message.
        /// </summary>
        /// <remarks>
        /// This does nothing.
        /// </remarks>
        /// <param name="reply">The received reply to process.</param>
        /// <param name="correlationState">The correlation state.</param>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {

        }

        /// <summary>
        /// Processes the request message.
        /// </summary>
        /// <remarks>
        /// This adds the specified user-agent to the request.
        /// </remarks>
        /// <param name="request">The request to process.</param>
        /// <param name="channel">The client channel.</param>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            HttpRequestMessageProperty httpRequestMessage;
            object httpRequestMessageObject;
            if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpRequestMessageObject))
            {
                httpRequestMessage = httpRequestMessageObject as HttpRequestMessageProperty;
                if (string.IsNullOrEmpty(httpRequestMessage.Headers[USER_AGENT_HTTP_HEADER]))
                {
                    httpRequestMessage.Headers[USER_AGENT_HTTP_HEADER] = this.m_userAgent;
                }
            }
            else
            {
                httpRequestMessage = new HttpRequestMessageProperty();
                httpRequestMessage.Headers.Add(USER_AGENT_HTTP_HEADER, this.m_userAgent);
                request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessage);
            }
            return null;
        }

        #endregion
    }


    /// <summary>
    /// An HTTP Endpoint Behaviour that sets cookies for the service call.
    /// </summary>
    public class CookieEndpointBehaviour : WebHttpBehavior
    {
        private Dictionary<string, string> m_dicAuthenticationCookies = null;

        #region Constructors

        /// <summary>
        /// A simple constructor that initializes the object with the given values.
        /// </summary>
        /// <param name="p_dicAuthenticationCookies">The cookies to add to the request.</param>
        public CookieEndpointBehaviour(Dictionary<string, string> p_dicAuthenticationCookies)
        {
            m_dicAuthenticationCookies = p_dicAuthenticationCookies;
        }

        #endregion

        /// <summary>
        /// Injects the user agent into the service request.
        /// </summary>
        /// <param name="endpoint">The enpoint of the service for which to set the user-agent.</param>
        /// <param name="clientRuntime">The client runtime.</param>
        public override void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            base.ApplyClientBehavior(endpoint, clientRuntime);
            CookieMessageInspector cmiInspector = new CookieMessageInspector(m_dicAuthenticationCookies);
            clientRuntime.MessageInspectors.Add(cmiInspector);

        }
    }

    /// <summary>
    /// An Client Message Inspector that sets cookies for the service call.
    /// </summary>
    public class CookieMessageInspector : IClientMessageInspector
    {
        private const string COOKIE_HTTP_HEADER = "Cookie";
        private Dictionary<string, string> m_dicAuthenticationCookies = null;

        #region Constructors

        /// <summary>
        /// A simple constructor that initializes the object with the given values.
        /// </summary>
        /// <param name="p_dicAuthenticationCookies">The cookies to add to the request.</param>
        public CookieMessageInspector(Dictionary<string, string> p_dicAuthenticationCookies)
        {
            m_dicAuthenticationCookies = p_dicAuthenticationCookies;
        }

        #endregion

        #region IClientMessageInspector Members

        /// <summary>
        /// Processes the reply message.
        /// </summary>
        /// <remarks>
        /// This does nothing.
        /// </remarks>
        /// <param name="reply">The received reply to process.</param>
        /// <param name="correlationState">The correlation state.</param>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {

        }

        /// <summary>
        /// Processes the request message.
        /// </summary>
        /// <remarks>
        /// This adds the specified cookies to the request.
        /// </remarks>
        /// <param name="request">The request to process.</param>
        /// <param name="channel">The client channel.</param>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            HttpRequestMessageProperty httpRequestMessage = null;
            object httpRequestMessageObject;
            if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpRequestMessageObject))
                httpRequestMessage = httpRequestMessageObject as HttpRequestMessageProperty;
            if (httpRequestMessage == null)
            {
                httpRequestMessage = new HttpRequestMessageProperty();
                request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessage);
            }
            string strCookie = httpRequestMessage.Headers[COOKIE_HTTP_HEADER];
            if ((m_dicAuthenticationCookies != null) && (m_dicAuthenticationCookies.Count > 0))
            {
                if (!String.IsNullOrEmpty(strCookie) && !strCookie.EndsWith(";"))
                    strCookie += ";";
                foreach (KeyValuePair<string, string> kvpCookie in m_dicAuthenticationCookies)
                    strCookie += String.Format("{0}={1};", kvpCookie.Key, kvpCookie.Value);
                httpRequestMessage.Headers[COOKIE_HTTP_HEADER] = strCookie;

            }
            return null;
        }

        #endregion
    }

    // OLD VERSION

    ///// <summary>
    ///// Describes the methods and properties of the Nexus mod repository.
    ///// </summary>
    ///// <remarks>
    ///// The Nexus mod repository is the repository hosted with the Nexus group of websites.
    ///// </remarks>
    //[ServiceContract]
    //public interface INexusModRepositoryApi
    //{
    //    /// <summary>
    //    /// Gets the info about the specified mod from the repository.
    //    /// </summary>
    //    /// <param name="p_strModId">The id of the mod for which to retrieved the metadata.</param>
    //    /// <returns>The info about the specified mod from the repository.</returns>
    //    [OperationContract]
    //    [WebGet(
    //        BodyStyle = WebMessageBodyStyle.Bare,
    //        UriTemplate = "Mods/{p_strModId}/",
    //        ResponseFormat = WebMessageFormat.Json)]
    //    NexusModInfo GetModInfo(string p_strModId);

    //    /// <summary>
    //    /// Gets the files associated with the specified mod from the repository.
    //    /// </summary>
    //    /// <param name="p_strModId">The id of the mod for which to retrieved the associated files.</param>
    //    /// <returns>The files associated with the specified mod from the repository.</returns>
    //    [OperationContract]
    //    [WebGet(
    //        BodyStyle = WebMessageBodyStyle.Bare,
    //        UriTemplate = "Mods/{p_strModId}/Files/",
    //        ResponseFormat = WebMessageFormat.Json)]
    //    List<NexusModFileInfo> GetModFiles(string p_strModId);

    //    /// <summary>
    //    /// Gets the file info for the specified download file of the specified mod.
    //    /// </summary>
    //    /// <param name="p_strModId">The id of the mod the whose file's metadata is to be retrieved.</param>
    //    /// <param name="p_strFileId">The id of the download file whose metadata is to be retrieved.</param>
    //    /// <returns>The file info for the specified download file of the specified mod.</returns>
    //    [OperationContract]
    //    [WebGet(
    //        BodyStyle = WebMessageBodyStyle.Bare,
    //        UriTemplate = "Mods/{p_strModId}/Files/{p_strFileId}/",
    //        ResponseFormat = WebMessageFormat.Json)]
    //    NexusModFileInfo GetModFile(string p_strModId, string p_strFileId);

    //    /// <summary>
    //    /// Gets the download URLs of all the parts associated with the specified file.
    //    /// </summary>
    //    /// <param name="p_strModId">The id of the mod with which the file for which to retrieve the URLs is associated.</param>
    //    /// <param name="p_strFileId">The id of the file for which to retrieve the URLs.</param>
    //    /// <returns>The download URLs of all the parts associated with the specified file.</returns>
    //    [OperationContract]
    //    [WebGet(
    //        BodyStyle = WebMessageBodyStyle.Bare,
    //        UriTemplate = "Mods/{p_strModId}/Files/{p_strFileId}/DownloadMulti/",
    //        ResponseFormat = WebMessageFormat.Json)]
    //    string[] GetModFileDownloadUrls(string p_strModId, string p_strFileId);

    //    /// <summary>
    //    /// Finds the mods containing the given search terms.
    //    /// </summary>
    //    /// <param name="p_strModNameSearchString">The terms to use to search for mods.</param>
    //    /// <param name="p_strType">Whether the returned mods' names should include all of
    //    /// the given search terms, or any of the terms.</param>
    //    /// <returns>The mod info for the mods matching the given search criteria.</returns>
    //    [OperationContract]
    //    [WebGet(
    //        BodyStyle = WebMessageBodyStyle.Bare,
    //        UriTemplate = "Mods/?Find&name={p_strModNameSearchString}&type={p_strType}",
    //        ResponseFormat = WebMessageFormat.Json)]
    //    List<NexusModInfo> FindMods(string p_strModNameSearchString, string p_strType);
    //}

    ///// <summary>
    ///// Describes the metadata of a file of a mod in a repository.
    ///// </summary>
    //public interface IModFileInfo
    //{
    //    #region Properties

    //    /// <summary>
    //    /// Gets the file id.
    //    /// </summary>
    //    /// <value>The file id.</value>
    //    string Id { get; }

    //    /// <summary>
    //    /// Gets the filename.
    //    /// </summary>
    //    /// <value>The filename.</value>
    //    string Filename { get; }

    //    /// <summary>
    //    /// Gets the friendly name of the file.
    //    /// </summary>
    //    /// <value>The friendly name of the file.</value>
    //    string Name { get; }

    //    /// <summary>
    //    /// Gets or sets the human readable version of the mod to which the file belongs.
    //    /// </summary>
    //    /// <value>The human readable version of the mod to which the file belongs.</value>
    //    string HumanReadableVersion { get; }

    //    #endregion
    //}

    //[DataContractAttribute]
    //public class NexusModInfo
    //{
    //    #region Properties

    //    /// <summary>
    //    /// Gets or sets whether the mod contains adult material.
    //    /// </summary>
    //    /// <value>Whether the mod contains adult material.</value>
    //    [DataMember(Name = "adult")]
    //    public bool IsAdult { get; set; }

    //    /// <summary>
    //    /// Gets or sets the category of the mod.
    //    /// </summary>
    //    /// <value>The category of the mod.</value>
    //    [DataMember(Name = "category_id")]
    //    public Int32 CategoryId { get; set; }

    //    /// <summary>
    //    /// Gets or sets the description of the mod.
    //    /// </summary>
    //    /// <value>The description of the mod.</value>
    //    [DataMember(Name = "description")]
    //    public string Description { get; set; }

    //    /// <summary>
    //    /// Gets or sets the Id of the mod.
    //    /// </summary>
    //    /// <value>The Id of the mod.</value>
    //    [DataMember(Name = "id")]
    //    public string Id { get; set; }

    //    /// <summary>
    //    /// Gets or sets the last updated date of the mod.
    //    /// </summary>
    //    /// <value>The last updated date of the mod.</value>
    //    [DataMember(Name = "lastupdate")]
    //    public DateTime? LastUpdated { get; set; }

    //    /// <summary>
    //    /// Gets or sets the name of the mod.
    //    /// </summary>
    //    /// <value>The name of the mod.</value>
    //    [DataMember(Name = "name")]
    //    public string Name { get; set; }

    //    /// <summary>
    //    /// Gets or sets the author of the mod.
    //    /// </summary>
    //    /// <value>The author of the mod.</value>
    //    [DataMember(Name = "author")]
    //    public string Author { get; set; }

    //    /// <summary>
    //    /// Gets or sets the owner of the mod.
    //    /// </summary>
    //    /// <value>The owner of the mod.</value>
    //    [DataMember(Name = "owner_id")]
    //    public Int32 OwnerId { get; set; }

    //    /// <summary>
    //    /// Gets or sets the summary of the mod.
    //    /// </summary>
    //    /// <value>The summary of the mod.</value>
    //    [DataMember(Name = "summary")]
    //    public string Summary { get; set; }

    //    /// <summary>
    //    /// Gets or sets the human readable mod version.
    //    /// </summary>
    //    /// <value>The human readable mod version.</value>
    //    [DataMember(Name = "version")]
    //    public string HumanReadableVersion { get; set; }

    //    #endregion
    //}

    ///// <summary>
    ///// The mod file categories.
    ///// </summary>
    //public enum ModFileCategory
    //{
    //    /// <summary>
    //    /// Indicates the file ia a main file in the mod.
    //    /// </summary>
    //    MainFiles = 1,

    //    /// <summary>
    //    /// Indicates the files is an update for the mod.
    //    /// </summary>
    //    Updates = 2,

    //    /// <summary>
    //    /// Indicates teh file is optional for the mod.
    //    /// </summary>
    //    OptionalFiles = 3,

    //    /// <summary>
    //    /// Indicates the file is for an old version of the mod.
    //    /// </summary>
    //    OldVersions = 4,

    //    /// <summary>
    //    /// Indicates the files is a support file for the mod.
    //    /// </summary>
    //    Misc = 5
    //}


    //[DataContract]
    //public class NexusModFileInfo : IModFileInfo
    //{
    //    #region Properties

    //    /// <summary>
    //    /// Gets or sets the file id.
    //    /// </summary>
    //    /// <value>The file id.</value>
    //    [DataMember(Name = "id")]
    //    public string Id { get; set; }

    //    /// <summary>
    //    /// Gets or sets the id of the mod to which the file belongs.
    //    /// </summary>
    //    /// <value>The id of the mod to which the file belongs.</value>
    //    [DataMember(Name = "mod_id")]
    //    public Int32 ModId { get; set; }

    //    /// <summary>
    //    /// Gets or sets the owner of the file.
    //    /// </summary>
    //    /// <value>The owner of the file.</value>
    //    [DataMember(Name = "owner_id")]
    //    public Int32 OwnerId { get; set; }

    //    /// <summary>
    //    /// Gets or sets the file cateogry.
    //    /// </summary>
    //    /// <value>The file cateogry.</value>
    //    [DataMember(Name = "category_id")]
    //    public ModFileCategory Category { get; set; }

    //    /// <summary>
    //    /// Gets or sets the display name of the file.
    //    /// </summary>
    //    /// <value>The display name of the file.</value>
    //    [DataMember(Name = "name")]
    //    public string Name { get; set; }

    //    /// <summary>
    //    /// Gets or sets the description of the file.
    //    /// </summary>
    //    /// <value>The description of the file.</value>
    //    [DataMember(Name = "description")]
    //    public string Description { get; set; }

    //    /// <summary>
    //    /// Gets or sets the filename.
    //    /// </summary>
    //    /// <value>The filename.</value>
    //    [DataMember(Name = "uri")]
    //    public string Filename { get; set; }

    //    /// <summary>
    //    /// Gets or sets the file size.
    //    /// </summary>
    //    /// <value>The file size.</value>
    //    [DataMember(Name = "size")]
    //    public UInt32 Size { get; set; }

    //    /// <summary>
    //    /// Gets or sets the human readable version of the mod to which the file belongs.
    //    /// </summary>
    //    /// <value>The human readable version of the mod to which the file belongs.</value>
    //    [DataMember(Name = "version")]
    //    public string HumanReadableVersion { get; set; }

    //    /// <summary>
    //    /// Gets or sets the date the file was loaded into the repository.
    //    /// </summary>
    //    /// <value>The date the file was loaded into the repository.</value>
    //    [DataMember(Name = "date")]
    //    public DateTime Date { get; set; }

    //    #endregion
    //}

    ///// <summary>
    ///// An HTTP Endpoint Behaviour that sets the user-agent for the service call.
    ///// </summary>
    //public class HttpUserAgentEndpointBehaviour : WebHttpBehavior
    //{
    //    private string m_userAgent;

    //    #region Constructors

    //    /// <summary>
    //    /// A simple constructor that initializes the object with the given values.
    //    /// </summary>
    //    /// <param name="userAgent">The user agent to use for the service calls.</param>
    //    public HttpUserAgentEndpointBehaviour(string userAgent)
    //    {
    //        this.m_userAgent = userAgent;
    //    }

    //    #endregion

    //    /// <summary>
    //    /// Injects the user agent into the service request.
    //    /// </summary>
    //    /// <param name="endpoint">The enpoint of the service for which to set the user-agent.</param>
    //    /// <param name="clientRuntime">The client runtime.</param>
    //    public override void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
    //    {
    //        base.ApplyClientBehavior(endpoint, clientRuntime);
    //        HttpUserAgentMessageInspector amiInspector = new HttpUserAgentMessageInspector(this.m_userAgent);
    //        clientRuntime.MessageInspectors.Add(amiInspector);

    //    }
    //}

    ///// <summary>
    ///// An Client Message Inspector that sets the user-agent for the service call.
    ///// </summary>
    //public class HttpUserAgentMessageInspector : IClientMessageInspector
    //{
    //    private const string USER_AGENT_HTTP_HEADER = "user-agent";
    //    private string m_userAgent = null;

    //    #region Constructors

    //    /// <summary>
    //    /// A simple constructor that initializes the object with the given values.
    //    /// </summary>
    //    /// <param name="userAgent">The user agent to use for the service calls.</param>
    //    public HttpUserAgentMessageInspector(string userAgent)
    //    {
    //        this.m_userAgent = userAgent;
    //    }

    //    #endregion

    //    #region IClientMessageInspector Members

    //    /// <summary>
    //    /// Processes the reply message.
    //    /// </summary>
    //    /// <remarks>
    //    /// This does nothing.
    //    /// </remarks>
    //    /// <param name="reply">The received reply to process.</param>
    //    /// <param name="correlationState">The correlation state.</param>
    //    public void AfterReceiveReply(ref Message reply, object correlationState)
    //    {

    //    }

    //    /// <summary>
    //    /// Processes the request message.
    //    /// </summary>
    //    /// <remarks>
    //    /// This adds the specified user-agent to the request.
    //    /// </remarks>
    //    /// <param name="request">The request to process.</param>
    //    /// <param name="channel">The client channel.</param>
    //    public object BeforeSendRequest(ref Message request, IClientChannel channel)
    //    {
    //        HttpRequestMessageProperty httpRequestMessage;
    //        object httpRequestMessageObject;
    //        if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpRequestMessageObject))
    //        {
    //            httpRequestMessage = httpRequestMessageObject as HttpRequestMessageProperty;
    //            if (string.IsNullOrEmpty(httpRequestMessage.Headers[USER_AGENT_HTTP_HEADER]))
    //            {
    //                httpRequestMessage.Headers[USER_AGENT_HTTP_HEADER] = this.m_userAgent;
    //            }
    //        }
    //        else
    //        {
    //            httpRequestMessage = new HttpRequestMessageProperty();
    //            httpRequestMessage.Headers.Add(USER_AGENT_HTTP_HEADER, this.m_userAgent);
    //            request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessage);
    //        }
    //        return null;
    //    }

    //    #endregion
    //}

    ///// <summary>
    ///// An Client Message Inspector that sets cookies for the service call.
    ///// </summary>
    //public class CookieMessageInspector : IClientMessageInspector
    //{
    //    private const string COOKIE_HTTP_HEADER = "Cookie";
    //    private Dictionary<string, string> m_dicAuthenticationCookies = null;

    //    #region Constructors

    //    /// <summary>
    //    /// A simple constructor that initializes the object with the given values.
    //    /// </summary>
    //    /// <param name="p_dicAuthenticationCookies">The cookies to add to the request.</param>
    //    public CookieMessageInspector(Dictionary<string, string> p_dicAuthenticationCookies)
    //    {
    //        m_dicAuthenticationCookies = p_dicAuthenticationCookies;
    //    }

    //    #endregion

    //    #region IClientMessageInspector Members

    //    /// <summary>
    //    /// Processes the reply message.
    //    /// </summary>
    //    /// <remarks>
    //    /// This does nothing.
    //    /// </remarks>
    //    /// <param name="reply">The received reply to process.</param>
    //    /// <param name="correlationState">The correlation state.</param>
    //    public void AfterReceiveReply(ref Message reply, object correlationState)
    //    {

    //    }

    //    /// <summary>
    //    /// Processes the request message.
    //    /// </summary>
    //    /// <remarks>
    //    /// This adds the specified cookies to the request.
    //    /// </remarks>
    //    /// <param name="request">The request to process.</param>
    //    /// <param name="channel">The client channel.</param>
    //    public object BeforeSendRequest(ref Message request, IClientChannel channel)
    //    {
    //        HttpRequestMessageProperty httpRequestMessage = null;
    //        object httpRequestMessageObject;
    //        if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpRequestMessageObject))
    //            httpRequestMessage = httpRequestMessageObject as HttpRequestMessageProperty;
    //        if (httpRequestMessage == null)
    //        {
    //            httpRequestMessage = new HttpRequestMessageProperty();
    //            request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessage);
    //        }
    //        string strCookie = httpRequestMessage.Headers[COOKIE_HTTP_HEADER];
    //        if (!String.IsNullOrEmpty(strCookie) && !strCookie.EndsWith(";"))
    //            strCookie += ";";
    //        foreach (KeyValuePair<string, string> kvpCookie in m_dicAuthenticationCookies)
    //            strCookie += String.Format("{0}={1};", kvpCookie.Key, kvpCookie.Value);
    //        httpRequestMessage.Headers[COOKIE_HTTP_HEADER] = strCookie;
    //        return null;
    //    }

    //    #endregion
    //}
    //public class CookieEndpointBehaviour : WebHttpBehavior
    //{
    //    private Dictionary<string, string> m_dicAuthenticationCookies = null;

    //    #region Constructors

    //    /// <summary>
    //    /// A simple constructor that initializes the object with the given values.
    //    /// </summary>
    //    /// <param name="p_dicAuthenticationCookies">The cookies to add to the request.</param>
    //    public CookieEndpointBehaviour(Dictionary<string, string> p_dicAuthenticationCookies)
    //    {
    //        m_dicAuthenticationCookies = p_dicAuthenticationCookies;
    //    }

    //    #endregion

    //    /// <summary>
    //    /// Injects the user agent into the service request.
    //    /// </summary>
    //    /// <param name="endpoint">The enpoint of the service for which to set the user-agent.</param>
    //    /// <param name="clientRuntime">The client runtime.</param>
    //    public override void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
    //    {
    //        base.ApplyClientBehavior(endpoint, clientRuntime);
    //        CookieMessageInspector cmiInspector = new CookieMessageInspector(m_dicAuthenticationCookies);
    //        clientRuntime.MessageInspectors.Add(cmiInspector);

    //    }
    //}
}
