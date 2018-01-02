using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Diagnostics;
using WCMS.WebSystem.Agent;

namespace WCMS.WebSystem
{
    public class Global : System.Web.HttpApplication
    {
        //private void RegisterGlobalFilters(GlobalFilterCollection filters)
        //{
        //    filters.Add(new HandleErrorAttribute());
        //}

        #region ASP.NET MVC methods

        //private void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        //    routes.MapRoute(
        //        "Default", // Route name
        //        "{controller}/{action}/{id}", // URL with parameters
        //        new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
        //    );
        //}

        //private void ExecuteMvcRouties()
        //{
        //    AreaRegistration.RegisterAllAreas();

        //    RegisterGlobalFilters(GlobalFilters.Filters);
        //    RegisterRoutes(RouteTable.Routes);
        //}

        #endregion

        //private void ConfigureRemoting()
        //{
        //    string path = Server.MapPath("Web.config");
        //    System.Runtime.Remoting.RemotingConfiguration.Configure(path, false);
        //}

        private void Application_Start(object sender, EventArgs e)
        {
            #region FIX: Disable AppDomain restart

            //FIX disable AppDomain restart when deleting subdirectory
            //This code will turn off monitoring from the root website directory.
            //Monitoring of Bin, App_Themes and other folders will still be operational, so updated DLLs will still auto deploy.

            var p = typeof(HttpRuntime).GetProperty("FileChangesMonitor",
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
			// for OSX
			if (p != null)
			{
				var o = p.GetValue(null, null);
				var f = o.GetType().GetField("_dirMonSubdirs",
					BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
				var monitor = f.GetValue(o);
				var m = monitor.GetType().GetMethod("StopMonitoring",
					BindingFlags.Instance | BindingFlags.NonPublic); m.Invoke(monitor, new object[] { });
			}

            #endregion

            GlobalConfiguration.Configure(WebApiConfig.Register);

            // ASP.NET MVC required-code
            //ExecuteMvcRouties();

            //ConfigureRemoting();

            //ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            //{
            //    Path = "~/Content/Assets/Scripts/jquery-1.8.2.min.js",
            //    DebugPath = "~/Content/Assets/Scripts/jquery-1.8.2.min.js",
            //    CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.8.2.min.js",
            //    CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.8.2.js"
            //});

            Action starterMethod = () =>
            {
                if (WConfig.AllowCache)
                {
                    WebObject.Initialize();
                    WebObject.LoadCache();
                }

                if (DataHelper.GetBool(ConfigHelper.Get("WCMS:AgentAutoStart"), false))
                    AgentHelper.ExecuteAutoStart();
            };

            starterMethod();

            //Thread starter = new Thread(new ThreadStart(starterMethod));
            //starter.Start();


            //Remove All Engine
            ViewEngines.Engines.Clear();
            //Add Razor Engine
            //ViewEngines.Engines.Add(new RazorViewEngine());
        }

        private void Application_End(object sender, EventArgs e) { }

        private void Session_End(object sender, EventArgs e)
        {
            var session = this.Session[WSession.DefaultName] as WSession;
            if (session != null && session.UserId > 0 && WSession.UserSessions.Contains(session.UserId))
            {
                WSession.LogSessionEvent(session.UserId, EventLogConstants.EndSession);
                WSession.UserSessions.End(session.UserId, this.Session.SessionID);
            }
        }

        private void Application_Error(object sender, EventArgs e)
        {
            var lastError = Server.GetLastError();
            var httpEx = lastError as HttpException;
            if (httpEx != null && httpEx.GetHttpCode() == 404)
            {
                //do what you want for 404 errors
                return;
            }

            var error = lastError.GetBaseException();
            var requestUrl = Request.Url.ToString();
            var userAgent = Request.UserAgent;
            var ip = Request.UserHostAddress;

            var errorMsg = string.Format(
                "Error in: {1},{0}User Agent: {4},{0}IP: {5},{0}Error Message: {2},{0}Stack Trace: {3}{0}{0}",
                Environment.NewLine,
                requestUrl,
                error.Message,
                error.StackTrace,
                userAgent,
                ip
            );

            WHelper.WriteLogAndSendEmail(errorMsg, error.Message);

            //Exception err = Server.GetLastError();
            //Session.Add("LastError", err);

            try
            {
                string configPath = "/";
                var config = WebConfigurationManager.OpenWebConfiguration(configPath);
                var customErrorsSection = (CustomErrorsSection)config.GetSection("system.web/customErrors");

                if (customErrorsSection.Mode == CustomErrorsMode.On ||
                    (customErrorsSection.Mode == CustomErrorsMode.RemoteOnly && !Context.Request.IsLocal))
                {
                    // Display Error page
                    string description = error.Message;
                    Server.ClearError();

                    var url = string.Format("/Error.aspx?Message={0}&Url={1}", Server.UrlEncode(description), Server.UrlEncode(requestUrl));
                    WebHelper.Redirect(url, Context);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }

        private void Session_Start(object sender, EventArgs e)
        {
            try
            {
                if (WConfig.AutoLogin)
                {
                    // For testing only
                    string userName = WConfig.DebuggingNode.SelectSingleNodeValue("LoginUserName");
                    WSession.Current.Login(WebUser.Get(userName).Id, false);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                WebHelper.Redirect(CentralPages.Setup, Context);
            }
        }

        /*
        private void Application_PostAcquireRequestState(object sender, EventArgs e)
        {
            string path = Request.Path.ToLower();
            // string ext = Path.GetExtension(path);

            // For physical pages
            if (path.EndsWith(".aspx"))
            {
                // Login.aspx should come from Registry
                if (path.Contains("/content/parts/central/") && !path.EndsWith("/login/") && !path.EndsWith("/queryanalyzer/"))
                {
                    //if (WSession.Current.UserId > 0)
                    //{
                    //    if (WSession.Current.IsAdministrator) // User is an admin, under the admin console
                    //        return;

                    //    //RedirectToAccessDenied();
                    //}
                    //else

                    if (WSession.Current.UserId == -1)
                        WebRedirector.RedirectToLogin();
                }
            }
            else
            {
                // Request not a .aspx page
            }
        }
        */

        private void Application_BeginRequest(object sender, EventArgs e)
        {
            string urlPath = Request.Url.AbsolutePath.ToLower();

            /* !urlPath.Equals("/default.aspx") && !urlPath.Equals("/static.aspx") && */
            /* && hasNoExt */
            /* ext.Equals(WConstants.ASPX) */

            if (urlPath.StartsWith("/u/") || urlPath.StartsWith("/content/") || urlPath.StartsWith("/api/") || WConstants.UrlConsts.Contains(urlPath))
                return;

            string ext = VirtualPathUtility.GetExtension(urlPath);
            if (ext.Equals(WConfig.PageExt) || !WConfig.HasPageExt && !ext.Contains("."))
            {
                WPage page = null;
                Tuple<WPage, string> result = null;

                PerformanceLog.Log(() =>
                {
                    result = WebRewriter.ResolvePageLowered(Context, urlPath);
                    page = result.Item1;
                    return page == null ? -1 : page.Id;
                }, "Rewrite URL");

                // Last page or Home Page
                if (page != null && (page.IsActive || WSession.Current.IsAdministrator))
                {
                    // Request is a rewritten page
                    var query = new WQuery(this);
                    query.Set(WebColumns.PageIdInternal, page.Id.ToString());

                    if (page.GetEvalTemplateEngineId() == TemplateEngineTypes.ASPX)
                        query.BasePath = WConstants.RelativeDynamicRootPath; //page.GetEvalTypeId() == PageTypes.Static ? WebConstants.RelativeStaticRootPath : WebConstants.RelativeDynamicRootPath;
                    else
                        query.BasePath = WConstants.RelativeRazorRootPath;

                    Context.RewritePath(query.BuildQuery(), false);
                }
                else if (result.Item2 != null)
                {
                    WebHelper.Redirect(result.Item2, Context);
                }

                //else if (!File.Exists(Request.MapPath(urlPath))) // TODO: Cache the physical files so no need to check disk
                //{
                //    // Invalid URL or Page does not exist
                //    QueryParser query = new QueryParser(this);
                //    query.SetSource(query.BuildQuery());
                //    query.Redirect(WebConstants.RelativeBlankPath);
                //}

                // Page is a physical file
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }
    }
}