using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Text;
using System.Xml;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

namespace WCMS.Framework
{
    public abstract class WHelper
    {
        #region Display Access Denied Page

        public static bool CheckCentralLoaderAccess(dynamic ctx)
        {
            if (WSession.Current.UserId == -1)
            {
                WebRedirector.RedirectToLogin(true);
                return false;
            }
            else if (!WSession.Current.IsSiteManager)
            {
                var context = new WContext(ctx);
                WHelper.ShowAccessDeniedFromCentral(context);
                return false;
            }

            return true;
        }

        public static void ShowAccessDeniedFromCentral(WContext context)
        {
            ShowAccessDenied(context.Site, context.Query);
        }

        public static void ShowAccessDenied(WPage page, QueryParser query)
        {
            // Get defined in parameter
            string accessDeniedPage = page.GetNestedParameterValue(WConstants.AccessDeniedUrl);

            // Get defined in site property
            if (string.IsNullOrWhiteSpace(accessDeniedPage))
                accessDeniedPage = page.GetNestedParameterValue(WConstants.AccessDeniedPage);

            if (string.IsNullOrEmpty(accessDeniedPage))
                accessDeniedPage = WConstants.AbsoluteAccessDeniedPage;

            ShowAccessDenied(query, accessDeniedPage);
        }

        public static void ShowAccessDenied(WebMasterPage masterPage, QueryParser query)
        {
            ShowAccessDenied(masterPage.Site, query);
        }

        public static void ShowAccessDenied(PublicSecurableObject securable, QueryParser query)
        {
            if (securable.OBJECT_ID == WebObjects.WebMasterPage)
                ShowAccessDenied((WebMasterPage)securable, query);
            else if (securable.OBJECT_ID == WebObjects.WebPage)
                ShowAccessDenied((WPage)securable, query);
            else if (securable.OBJECT_ID == WebObjects.WebSite)
                ShowAccessDenied((WSite)securable, query);
        }

        public static void ShowAccessDenied(WSite site, QueryParser query)
        {
            string accessDeniedPage = string.Empty;

            // Get defined in parameter
            if (site != null)
                accessDeniedPage = site.GetNestedParameterValue(WConstants.AccessDeniedPage);

            // Get defined in site property
            //if (string.IsNullOrWhiteSpace(accessDeniedPage))
            //    accessDeniedPage = site.AccessDeniedPage;

            // Get global
            if (string.IsNullOrWhiteSpace(accessDeniedPage))
                accessDeniedPage = WConstants.AbsoluteAccessDeniedPage;

            ShowAccessDenied(query, accessDeniedPage);
        }

        private static void ShowAccessDenied(QueryParser query, string accessDeniedPage)
        {
            QueryParser q = new QueryParser(accessDeniedPage);
            q.SetSource(query.BuildQuery());

            // Access Denied
            QueryParser.StaticRedirect(q.BuildQuery());
        }

        #endregion

        #region Load Resources

        public static StringBuilder LoadResources(IEnumerable<WebObjectHeader> siteHeaders)
        {
            if (siteHeaders.Count() > 0)
            {
                siteHeaders = siteHeaders.OrderBy(item => item.Header.Rank); //.ToList();

                StringBuilder sb = new StringBuilder();
                foreach (var siteHeader in siteHeaders)
                {
                    string headerText = siteHeader.Header.RenderAsText(WConfig.ResourcesExternalMode);
                    sb.AppendFormat("{0}{1}", headerText, Environment.NewLine);
                }

                //Literal pageHeader = new Literal();
                //pageHeader.EnableViewState = false;
                //pageHeader.Text = sb.ToString();

                //return pageHeader;

                return sb;
            }

            return null;
        }

        public static StringBuilder LoadResources(WPage page)
        {
            var masterPage = page.MasterPage;

            var headers = new List<WebObjectHeader>();

            // Add Resources in specific order to enable override
            var template = masterPage.Template;

            var theme = page.SkinId > 0 ? page.Skin : masterPage.SkinId > 0 ? masterPage.Skin : template.SkinId > 0 ? template.Skin : null;
            if (theme != null)
                headers.AddRange(theme.Headers);

            headers.AddRange(template.Headers);
            headers.AddRange(masterPage.Headers);
            headers.AddRange(page.Site.Headers);
            headers.AddRange(page.Headers);

            return LoadResources(headers);
        }

        public static StringBuilder LoadResources(int objectId, int recordId)
        {
            var siteHeaders = WebObjectHeader.GetList(objectId, recordId);

            return LoadResources(siteHeaders);
        }

        public static StringBuilder LoadResources(IWebObject part)
        {
            return WHelper.LoadResources(part.OBJECT_ID, part.Id);
        }

        #endregion

        public static string ToAbsPath(string relPath)
        {
            if (relPath.StartsWith("/"))
                return WebHelper.CombineAddress(WConfig.BaseAddress, relPath);
            return relPath;
        }

        public static WPage GetPageOrDefault(int pageId)
        {
            WPage page = null;
            if (!(pageId > 0 && ((page = WPage.Get(pageId)) != null)))
                page = WConfig.DefaultSite.HomePage; // If no parameters, load the default site

            return page;
        }

        //public static string ToAbsoluteUrl(string relativeUrl, WSite site)
        //{
        //    if (site != null && relativeUrl.StartsWith("/"))
        //        return WebHelper.CombineAddress(site.GetBaseAddress(), relativeUrl);

        //    return relativeUrl;
        //}

        public static void DeleteElementData(IPageElement element)
        {
            var managers = GetPartDataManagers();
            if (managers.Count > 0)
            {
                var partControl = element.PartControlTemplate.PartControl;

                PartDataManagerModel model = null;
                PartDataManagerModel firstModel = null;

                foreach (var manager in managers)
                {
                    if (manager.PartId == partControl.PartId)
                    {
                        if (manager.PartControlId == partControl.Id)
                        {
                            model = manager;
                            break;
                        }
                        else if (firstModel == null && manager.PartControlId == -1)
                        {
                            firstModel = manager;
                        }
                    }
                }

                if (model == null && firstModel != null)
                    model = firstModel;

                if (model != null)
                {
                    var manager = model.GetManager();
                    if (manager != null)
                        manager.DeleteElementData(element);
                }
            }
        }

        public static int GetUserMgmtPermission()
        {
            if (WSession.Current.IsAdministrator)
                return Permissions.ManageInstance;

            IPageElement item = WHelper.GetCurrentWebElement();
            if (item != null)
            {
                if (item.OBJECT_ID == WebObjects.WebPage)
                {
                    // WebPage
                    var page = item as WPage;
                    if (page != null)
                    {
                        var perms = WebObjectSecurity.GetUserPermissions(page, 0);

                        if (WebObjectSecurity.IsPermitted(perms, Permissions.ManageInstance))
                            return Permissions.ManageInstance;
                        else if (WebObjectSecurity.IsPermitted(perms, Permissions.ManageContent))
                            return Permissions.ManageContent;
                    }
                }
                else
                {
                    // WebPageElement
                    var element = item as WebPageElement;
                    if (element != null)
                    {
                        if (element.OwnerIsPage)
                        {
                            var page = element.Page;
                            if (page != null)
                            {
                                var perms = WebObjectSecurity.GetUserPermissions(page, 0);

                                if (WebObjectSecurity.IsPermitted(perms, Permissions.ManageInstance))
                                    return Permissions.ManageInstance;
                                else if (WebObjectSecurity.IsPermitted(perms, Permissions.ManageContent))
                                    return Permissions.ManageContent;
                            }
                        }
                        else
                        {
                            // Owner is MasterPage
                            var masterPage = element.MasterPage;
                            if (masterPage != null)
                            {
                                var perms = WebObjectSecurity.GetUserPermissions(masterPage, 0);

                                if (WebObjectSecurity.IsPermitted(perms, Permissions.ManageInstance))
                                    return Permissions.ManageInstance;
                                else if (WebObjectSecurity.IsPermitted(perms, Permissions.ManageContent))
                                    return Permissions.ManageContent;
                            }
                        }
                    }
                }
            }
            else
            {
            }

            return Permissions.None;
        }

        public static PageElementBase GetCurrentWebElement()
        {
            var query = new QueryParser(HttpContext.Current);

            int pageId = query.GetId(WebColumns.PageId);
            int pageElementId = query.GetId(WebColumns.PageElementId);
            int siteId = query.GetId(WebColumns.SiteId);

            if (pageElementId > 0)
            {
                WebPageElement item = WebPageElement.Get(pageElementId);
                if (item != null)
                    return item;
            }
            else if (pageId > 0)
            {
                WPage page = WPage.Get(pageId);
                if (page != null)
                    return page;
            }
            //else if (siteId > 0)
            //{
            //    var site = WebSite.Get(siteId);
            //    if (site != null)
            //        return site;
            //}

            return null;
        }

        public static ObjectKey GetObjectKey()
        {
            var query = new QueryParser(HttpContext.Current);
            string keyString = query.Get(ObjectKey.KeyString);

            if (!string.IsNullOrEmpty(keyString))
            {
                var key = new ObjectKey(keyString);
                return key;
            }
            else
            {
                var pair = WHelper.GetObjectStruct();
                return new ObjectKey(pair.ObjectId, pair.RecordId);
            }
        }

        public static ObjectKey GetObjectStruct()
        {
            var query = new QueryParser(HttpContext.Current);

            var item = new ObjectKey();
            int siteId = query.GetId(WebColumns.SiteId);
            int pageId = query.GetId(WebColumns.PageId);
            int masterPageId = query.GetId(WebColumns.MasterPageId);
            int partId = query.GetId(WebColumns.PartId);
            int partAdminId = query.GetId(WebColumns.PartAdminId);
            int pageElementId = query.GetId(WebColumns.PageElementId);

            if (pageElementId > 0)
            {
                item.ObjectId = WebObjects.WebPageElement;
                item.RecordId = pageElementId;
            }
            else if (pageId > 0)
            {
                item.ObjectId = WebObjects.WebPage;
                item.RecordId = pageId;
            }
            else if (masterPageId > 0)
            {
                item.ObjectId = WebObjects.WebMasterPage;
                item.RecordId = masterPageId;
            }
            else if (siteId > 0)
            {
                item.ObjectId = WebObjects.WebSite;
                item.RecordId = siteId;
            }
            else if (partAdminId > 0)
            {
                item.ObjectId = WebObjects.WebPartAdmin;
                item.RecordId = partAdminId;
            }
            else if (partId > 0)
            {
                item.ObjectId = WebObjects.WebPart;
                item.RecordId = partId;
            }

            return item;
        }

        public static string GetUserHostAddress()
        {
            var context = HttpContext.Current;
            if (context != null)
                return context.Request.UserHostAddress;
            return
                string.Empty;
        }

        public static string GenerateTempPath(string fileName = "")
        {
            return WebHelper.CombineAddress(WConfig.TempFolder, FileHelper.GenerateTempFileName(fileName));
        }

        public static List<LinkedPart> GetLinkedParts(int pageId)
        {
            List<LinkedPart> items = new List<LinkedPart>();

            var linkedPartSchema = WebRegistry.SelectNodeValue("/System/LinkedParts");
            if (!string.IsNullOrEmpty(linkedPartSchema))
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(linkedPartSchema);

                XmlNodeList lpNodes = xdoc.SelectNodes("//LinkedPart");
                foreach (XmlNode lpNode in lpNodes)
                {
                    LinkedPart item = new LinkedPart();
                    item.PartConfigId = DataHelper.GetId(XmlUtil.GetAttributeValue(lpNode, "PartConfigId", "-1"));
                    item.LinkedPartControlId = DataHelper.GetId(XmlUtil.GetAttributeValue(lpNode, "LinkedPartControlId", "-1"));
                    item.TargetObjectId = DataHelper.GetId(XmlUtil.GetAttributeValue(lpNode, "TargetObjectId", "-1"));

                    items.Add(item);
                }
            }

            return items;
        }

        public static List<PartDataManagerModel> GetPartDataManagers()
        {
            List<PartDataManagerModel> items = new List<PartDataManagerModel>();

            var partDataManagersXml = WebRegistry.SelectNodeValue("/System/PartDataManagers");
            if (!string.IsNullOrEmpty(partDataManagersXml))
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(partDataManagersXml);

                XmlNodeList itemNodes = xdoc.SelectNodes("//Item");
                foreach (XmlNode itemNode in itemNodes)
                {
                    PartDataManagerModel item = new PartDataManagerModel();
                    item.PartId = DataHelper.GetId(XmlUtil.GetAttributeValue(itemNode, WebColumns.PartId, "-1"));
                    item.PartControlId = DataHelper.GetId(XmlUtil.GetAttributeValue(itemNode, WebColumns.PartControlId, "-1"));
                    item.TypeName = XmlUtil.GetAttributeValue(itemNode, "TypeName", "");

                    items.Add(item);
                }
            }

            return items;
        }

        public static void WriteLogAndSendEmail(Exception error)
        {
            WriteLogAndSendEmail(error.ToString(), error.Message);
        }

        public static void WriteLogAndSendEmail(string errorMsg, string subject = "")
        {
            int userId = WSession.Current.UserId;
            if (userId > 0)
                errorMsg = string.Format("UserId: {1},{0}{2}", Environment.NewLine, userId, errorMsg);

            LogHelper.WriteLog(false, false, errorMsg);

            if (DataHelper.GetBool(WebRegistry.SelectNodeValue("/System/Debugging/SendEmail"), false))
            {
                var notifyEmail = WebRegistry.SelectNodeValue("/System/Debugging/NotifyEmail");
                if (!string.IsNullOrEmpty(notifyEmail))
                {
                    try
                    {
                        var email = new WebMailMessage();
                        email.To.Add(notifyEmail);

                        try
                        {
                            email.SubjectAutoPrefix = string.IsNullOrEmpty(subject) ? "System Error" : "Error - " + subject;
                        }
                        catch
                        {
                            email.SubjectAutoPrefix = "System Error";
                        }

                        email.Body = errorMsg;
                        email.Send();
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(ex);
                    }
                }
            }
        }

        public static string GetRedirectUrl(WebUser user, WContext context, PageElementBase element)
        {
            string redirUrl;
            string src = context.GetSource();
            if (!string.IsNullOrEmpty(src))
            {
                // Redirect to prevous Url before the login
                redirUrl = src;
            }
            else
            {
                string firstLoginUrl = element.GetParameterValue(AccountConstants.FirstLoginUrl);
                if (!string.IsNullOrEmpty(firstLoginUrl) && user.HaveNotLoggedIn)
                {
                    // Redirect to FirstLoginUrl
                    redirUrl = firstLoginUrl;
                }
                else
                {
                    string userLoginHomeUrl = ParameterizedWebObject.GetValue(AccountConstants.LoginHomeUrl, user, element, context.Site);
                    // User's login homepage is defined, redirect to this Url
                    redirUrl = !string.IsNullOrEmpty(userLoginHomeUrl) ? userLoginHomeUrl : WConstants.WebRootUrl;
                }
            }

            if (!WebHelper.IsSameDomain(context.Context, redirUrl))
            {
                var session = WSession.Current.UserSession;
                //var browserSession = session == null ? null : WSession.UserSessions.BrowserCache.Values
                //        .FirstOrDefault(i => i.UserId == session.UserId && i.IPAddress == context.Context.Request.UserHostAddress);
                if (session != null)
                {
                    WSession.UserSessions.Update(session);
                    var q = new WQuery(redirUrl);
                    q.Set(WConstants.SessionId, session.SessionId);
                    redirUrl = q.BuildQuery(true);
                }
                //else
                //{
                //    return null;
                //}
            }

            return redirUrl;
        }
    }
}
