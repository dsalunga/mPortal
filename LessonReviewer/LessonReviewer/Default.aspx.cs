using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

using WCMS.Common.Utilities;
using WCMS.Common.Media;
using WCMS.WebSystem.Apps.Integration.Net;

using WCMS.LessonReviewer.Core;

namespace WCMS.LessonReviewer
{
    public partial class _Default : System.Web.UI.Page
    {
        protected string debugString = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var query = new QueryParser(this);

                // Intranet Check
                var session = Session[MakeUpServiceSession.SessionKey] as MakeUpServiceSession;
                var intranetMode = DataUtil.GetBool(ConfigManager.Get("IntranetMode"));
                var byPassUrlEnabled = DataUtil.GetBool(ConfigManager.Get("EnableAutoByPassUrl"), true);
                if (!intranetMode && (!byPassUrlEnabled || (byPassUrlEnabled && !query.GetBool("Intranet", false))))
                {
                    if ((session != null && !session.BypassPortal) || (session == null && !intranetMode))
                    {
                        var portalAjaxHandlerUrl = ConfigManager.Get("PortalAjaxHandlerUrl");
                        var requestUrl = string.Format("{0}?Method=Status&Ticks={1}", portalAjaxHandlerUrl, DateTime.Now.Ticks);
                        var response = WebUtil.GetResponseString(requestUrl);
                        if (!string.IsNullOrEmpty(response) && response.Equals("OK", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var portalMakeUpHomeUrl = ConfigManager.Get("PortalMakeUpHomeUrl");
                            Response.Redirect(portalMakeUpHomeUrl, true);
                            return;
                        }
                        else
                        {
                            debugString = "Portal Handler returned not OK.";
                        }
                    }
                    else if (session != null && session.BypassPortal)
                    {
                        hEnableKeepAlive.Value = "1";
                    }
                }
                else
                {
                    debugString = "Portal check not enabled.";
                }

                ServiceDefinition serviceDef = null;
                var baseFolder = ConfigManager.Get("MCGI.MakeUp.BaseFolder");
                var baseHttp = ConfigManager.Get("MCGI.MakeUp.BaseHttp");
                var servicesPath = HttpContext.Current.Server.MapPath("~/App_Data/Services.xml");
                baseFolder = WebHelper.MapPath(baseFolder, true);

                #region Setup ServiceTypes

                var st = new Dictionary<string, string>();
                var serviceTypes = new Dictionary<string, ServiceDefinition>();

                var xdoc = new XmlDocument();
                xdoc.Load(servicesPath);

                XmlNodeList nodes = xdoc.SelectNodes("//Service");
                foreach (XmlNode node in nodes)
                {
                    serviceDef = PlaybackHelper.GetService(node);
                    if (serviceDef != null && !string.IsNullOrEmpty(serviceDef.Value))
                    {
                        serviceTypes.Add(serviceDef.Value, serviceDef);

                        var servicePath = FileHelper.Combine(baseFolder, serviceDef.Value, '\\');
                        if (Directory.Exists(servicePath))
                        {
                            if (serviceDef.InstancesByDate)
                            {
                                if (Directory.GetDirectories(servicePath, "????-??-??").Count() > 0)
                                    st.Add(serviceDef.Value, serviceDef.Text);
                            }
                            else
                            {
                                if (Directory.GetDirectories(servicePath).Count() > 0)
                                    st.Add(serviceDef.Value, serviceDef.Text);
                            }
                        }
                    }
                }

                serviceDef = null;

                #endregion

                cboCategory.DataSource = st;
                cboCategory.DataBind();

                var browser = Request.Browser;
                var browserIsIE = browser.Browser.Equals("IE", StringComparison.InvariantCultureIgnoreCase) || browser.Browser.Equals("InternetExplorer");
                if (!browserIsIE)
                {
                    panelOtherPlayer.Visible = true;
                    panelWMPlayer.Visible = false;
                }

                var s = query.Get("ServiceType");
                if (!string.IsNullOrEmpty(s))
                {
                    cboCategory.Items.RemoveAt(0);
                    cboCategory.SelectedValue = s;
                    if (serviceTypes.ContainsKey(s))
                    {
                        serviceDef = serviceTypes[s];
                        cboServices.DataSource = SelectServiceInstances(serviceDef);
                        cboServices.DataBind();
                    }
                }
                else
                {
                    cboServices.Enabled = false;
                }

                var dateOrFolder = query.Get("Date");
                if (!string.IsNullOrEmpty(dateOrFolder))
                {
                    cboServices.Items.RemoveAt(0);
                    cboServices.SelectedValue = dateOrFolder;
                }
                else
                {
                    cboLanguage.Enabled = false;
                }

                var language = query.Get("Language");
                if (!string.IsNullOrEmpty(language))
                {
                    cboLanguage.Items.RemoveAt(0);
                    cboLanguage.SelectedValue = language;
                }
                if (serviceDef != null && !string.IsNullOrEmpty(language) && !string.IsNullOrEmpty(dateOrFolder))
                {
                    bool hasFiles = false;
                    if (!string.IsNullOrEmpty(baseFolder))
                    {
                        var serviceFolder = FileHelper.Combine(baseFolder, string.Format("{0}\\{1}", serviceDef.Value, dateOrFolder), '\\');
                        var videoExists = Directory.Exists(serviceFolder);
                        if (videoExists)
                        {
                            if (serviceDef.InstancesByDate)
                            {
                                var files = Directory.GetFiles(serviceFolder);
                                if (files.Count() > 0)
                                {
                                    var playbackList = new PlaybackMasterList(serviceDef.Value, dateOrFolder, baseHttp);
                                    foreach (var file in files)
                                        playbackList.Add(Path.GetFileName(file));

                                    var pbFiles = playbackList.GetFiles(language);
                                    if (pbFiles.Count() > 0)
                                        hasFiles = true;
                                }
                            }
                            else
                            {
                                var folders = Directory.GetDirectories(serviceFolder);
                                if (folders.Count() > 0)
                                {
                                    if (!string.IsNullOrEmpty(language))
                                    {
                                        foreach (var folder in folders)
                                        {
                                            var folderLang = Path.GetFileName(folder);
                                            if (PlaybackLanguages.Values.ContainsKey(folderLang) && folderLang.Equals(language, StringComparison.InvariantCultureIgnoreCase))
                                            {
                                                var files = Directory.GetFiles(folder);
                                                if (files.Count() > 0)
                                                {
                                                    foreach (var file in files)
                                                    {
                                                        var fileName = Path.GetFileName(file);
                                                        var ext = Path.GetExtension(fileName);
                                                        if (!string.IsNullOrEmpty(ext) && !ext.Equals(".db", StringComparison.InvariantCultureIgnoreCase))
                                                            cboPlaylist.Items.Add(new ListItem(Path.GetFileNameWithoutExtension(fileName), fileName));
                                                    }
                                                }
                                                panelPlaylist.Visible = true;

                                                var play = query.Get("Play");
                                                var item = cboPlaylist.Items.FindByValue(play);
                                                if (item != null)
                                                    cboPlaylist.SelectedValue = item.Value;
                                                hasFiles = true;
                                                break;
                                            }
                                        }
                                    }
                                }

                                if (!hasFiles)
                                {
                                    // No Language folders
                                    var files = Directory.GetFiles(serviceFolder);
                                    if (files.Count() > 0)
                                    {
                                        var playbackList = new PlaybackMasterList(serviceDef.Value, dateOrFolder, baseHttp);
                                        foreach (var file in files)
                                            playbackList.Add(Path.GetFileName(file));
                                        var pbFiles = playbackList.GetFiles(language);
                                        if (pbFiles.Count() > 0)
                                        {
                                            hasFiles = true;
                                            panelPlaylist.Visible = true;
                                            foreach (var pbFile in pbFiles)
                                            {
                                                var ext = Path.GetExtension(pbFile.Filename);
                                                if (!string.IsNullOrEmpty(ext) && !ext.Equals(".db", StringComparison.InvariantCultureIgnoreCase))
                                                    cboPlaylist.Items.Add(new ListItem(Path.GetFileNameWithoutExtension(pbFile.Filename), pbFile.Filename));
                                            }

                                            var play = query.Get("Play");
                                            var item = cboPlaylist.Items.FindByValue(play);
                                            if (item != null)
                                                cboPlaylist.SelectedValue = item.Value;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //var hasFiles = PlaybackHelper.HasFiles(query);
                    if (hasFiles)
                    {
                        var play = query.Get("Play");
                        var playQuery = new QueryParser("Handlers/Playback.ashx");
                        playQuery.Set("ServiceType", serviceDef.Value);
                        playQuery.Set("Date", dateOrFolder);
                        playQuery.Set("Language", language);
                        playQuery.Set("Ticks", DateTime.Now.Ticks);

                        if (session != null && session.OverrideSeek)
                            playQuery.Set("FetchMode", "true");

                        if (!string.IsNullOrEmpty(play))
                            playQuery.Set("Play", play);

                        if (browserIsIE)
                            mediaPlayer.MovieURL = playQuery.BuildQuery();
                        else
                            paramUrl.Attributes["value"] = playQuery.BuildQuery();

                        panelToggleFullscreen.Visible = true;
                        lblHeader.InnerHtml = string.Format("{0} - {2} ({1})", cboCategory.SelectedItem.Text, PlaybackLanguages.GetText(language), serviceDef.InstancesByDate ? DataHelper.GetDateTime(dateOrFolder).ToString("MMMM d, yyyy") : dateOrFolder);

                        #region Process Segments (Go to...)

                        if (serviceDef != null && serviceDef.AllowSegmented)
                        {
                            if (!string.IsNullOrEmpty(baseFolder))
                            {
                                baseFolder = WebHelper.MapPath(baseFolder);
                                var serviceFolder = FileHelper.Combine(baseFolder, string.Format("{0}\\{1}", serviceDef.Value, dateOrFolder), '\\');
                                var videoExists = Directory.Exists(serviceFolder);
                                if (videoExists)
                                {
                                    var files = Directory.GetFiles(serviceFolder);
                                    if (files.Count() > 0)
                                    {
                                        PlaybackMasterList playbackList = new PlaybackMasterList(serviceDef.Value, dateOrFolder, baseHttp);
                                        foreach (var file in files)
                                            playbackList.Add(Path.GetFileName(file));
                                        var segments = playbackList.GetUniqueSegments(language);
                                        if (segments.Count > 1)
                                        {
                                            var segmentSelected = !string.IsNullOrEmpty(play) && play.StartsWith("-PART") && play.EndsWith("-");
                                            if (browserIsIE && !segmentSelected)
                                                panelParts.Visible = true;

                                            panelPlaylist.Visible = true;
                                            foreach (var segment in segments)
                                            {
                                                if (browserIsIE && !segmentSelected)
                                                    cboParts.Items.Add(new ListItem(segment.GetDisplayText(), segment.MediaIndex.ToString()));
                                                cboPlaylist.Items.Add(new ListItem(segment.GetDisplayText(), string.Format("-PART{0}-", segment.GetSegmentNumber())));
                                            }

                                            if (segmentSelected)
                                            {
                                                var item = cboPlaylist.Items.FindByValue(play);
                                                if (item != null)
                                                    cboPlaylist.SelectedValue = play;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        lblMsg.Text = string.Format("Sorry, there is no \"{0}\" version available.", PlaybackLanguages.GetText(language));
                    }
                }
                else
                {
                    //lblMsg.Text = "Error: Missing or incorrect parameters.";
                }
            }
        }

        //private Dictionary<string, string> SelectServiceTypes()
        //{
        //    Dictionary<string, string> items = new Dictionary<string, string>();
        //    Dictionary<string, ServiceDefinition> services = new Dictionary<string, ServiceDefinition>();

        //    var baseFolder = ConfigManager.Get("MCGI.MakeUp.BaseFolder");
        //    var servicesPath = HttpContext.Current.Server.MapPath("~/App_Data/Services.xml");

        //    baseFolder = WebHelper.EvalOrMapPath(baseFolder);

        //    XmlDocument xdoc = new XmlDocument();
        //    xdoc.Load(servicesPath);

        //    XmlNodeList nodes = xdoc.SelectNodes("//Service");
        //    foreach (XmlNode node in nodes)
        //    {
        //        var service = PlaybackHelper.GetService(node);
        //        if (service != null && !string.IsNullOrEmpty(service.Value))
        //        {
        //            services.Add(service.Value, service);

        //            var servicePath = FileHelper.Combine(baseFolder, service.Value, '\\');
        //            if (Directory.Exists(servicePath))
        //            {
        //                if (service.InstancesByDate)
        //                {
        //                    if (Directory.EnumerateDirectories(servicePath, "????-??-??").Count() > 0)
        //                        items.Add(service.Value, service.Text);
        //                }
        //                else
        //                {
        //                    if (Directory.EnumerateDirectories(servicePath).Count() > 0)
        //                        items.Add(service.Value, service.Text);
        //                }
        //            }
        //        }
        //    }

        //    return items;
        //}

        private Dictionary<string, string> SelectServiceInstances(ServiceDefinition service)
        {
            var items = new Dictionary<string, string>();
            var baseFolder = ConfigManager.Get("MCGI.MakeUp.BaseFolder");
            if (service != null && !string.IsNullOrEmpty(baseFolder))
            {
                baseFolder = WebHelper.MapPath(baseFolder, true);
                var serviceFolder = FileHelper.Combine(baseFolder, string.Format("{0}", service.Value), '\\');
                var videoExists = Directory.Exists(serviceFolder);
                if (videoExists)
                {
                    var folders = from f in Directory.GetDirectories(serviceFolder)
                                  orderby f descending
                                  select f;
                    foreach (var folder in folders)
                    {
                        var folderName = Path.GetFileName(folder);
                        if (service.InstancesByDate && folderName.Contains("-") || !service.InstancesByDate)
                            items.Add(folderName, folderName);
                    }
                }
            }
            return items;
        }

        protected void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var query = new QueryParser(this);
            query.Set("Language", cboLanguage.SelectedValue);
            query.Remove("Play");
            query.Redirect();
        }

        protected void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            var query = new QueryParser(this);
            query.Set("ServiceType", cboCategory.SelectedValue);
            query.Remove("Date");
            query.Remove("Play");

            query.Redirect();
        }

        protected void cboServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            query.Set("Date", cboServices.SelectedValue);

            query.Remove("Play");

            query.Redirect();
        }

        protected void cboPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            var playFile = cboPlaylist.SelectedValue;

            QueryParser query = new QueryParser(this);
            if (!string.IsNullOrEmpty(playFile))
                query.Set("Play", playFile);
            else
                query.Remove("Play");

            query.Redirect();
        }
    }
}
