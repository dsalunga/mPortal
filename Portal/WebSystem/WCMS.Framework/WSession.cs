using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

using WCMS.Common.Utilities;
using WCMS.Framework.Diagnostics;
using WCMS.Framework.Security;

namespace WCMS.Framework
{
    [Serializable]
    public class WSession
    {
        public const string DefaultName = "WSession";

        private static UserSessionManager _userSessions = new UserSessionManager();

        public WSession()
            : this(DefaultName) { }

        public WSession(string name)
        {
            Name = name;
            InDesignPanelLeft = 15;
            InDesignPanelTop = 60;
            UserId = -1;
        }

        #region Properties

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                this.Update();
            }
        }

        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                this.Update();
            }
        }

        #region Designer

        private int _inDesign = DesignerConstants.PreviewMode;
        public int InDesign
        {
            get { return _inDesign; }
            set
            {
                _inDesign = value;
                this.Update();
            }
        }

        public bool IsInDesign
        {
            get { return _inDesign > -2; }
        }

        private bool _inDesignPanelExpanded;
        public bool InDesignPanelExpanded
        {
            get { return _inDesignPanelExpanded; }
            set
            {
                _inDesignPanelExpanded = value;
                this.Update();
            }
        }

        public int InDesignPanelLeft { get; set; }
        public int InDesignPanelTop { get; set; }
        public bool IsDesignInitiated { get; set; }

        #endregion Designer

        private int _workingSessionId;
        public int WorkingSessionId
        {
            get { return _workingSessionId; }
            set
            {
                _workingSessionId = value;
                this.Update();
            }
        }

        public UserSession UserSession
        {
            get
            {
                if (_userId > 0 && _userSessions.Contains(_userId))
                    return _userSessions.SessionCache[_userId];

                return null;
            }
        }

        public static WSession Current
        {
            get
            {
                WSession current = Context.Session == null ? null : Context.Session[DefaultName] as WSession;
                if (current == null)
                {
                    current = new WSession();
                    current.InDesignPanelExpanded = WConfig.PanelExpanded;
                    Context.Session[DefaultName] = current;
                }

                return current;
            }
        }

        public static HttpSessionState Session
        {
            get { return Context.Session; }
        }

        public static UserSessionManager UserSessions
        {
            get { return _userSessions; }
        }

        public bool IsLoggedIn
        {
            get { return UserId > 0 /*&& User != null*/; }
        }

        public WebUser User
        {
            get { return UserId > 0 ? WebUser.Get(UserId) : null; }
        }

        //private bool? _isAdmin = null;
        private int _userType = -1;

        /// <summary>
        /// Using this as a basis for most permissions should be depreciated in the near future. Specific permissions should be checked.
        /// </summary>
        public bool IsAdministrator { get { return UserType == UserTypes.Administrator; } }
        public bool IsSiteManager { get { return IsAdministrator || UserType == UserTypes.SiteManager; } }

        public int UserType
        {
            get
            {
                if (_userType == -1)
                {
                    if (_userId > 0)
                    {
                        var user = User;
                        if (user != null)
                        {
                            //_isAdmin = user.IsAdministrator();
                            _userType = user.GetUserType();
                        }
                        else
                            return UserTypes.User;
                    }
                    else
                        return UserTypes.User;
                }

                return _userType;
            }
        }

        public static HttpContext Context
        {
            get { return HttpContext.Current; }
        }

        #endregion

        public void Update()
        {
            Context.Session[Name] = this;
        }


        #region Security Methods

        public bool UserHasAccess(int pageElementId, int access)
        {
            if (IsLoggedIn)
            {

            }

            return false;
        }

        #endregion


        public static void LogOff(int userId = -1, bool force = false)
        {
            var session = WSession.Current;

            var uid = userId;
            if (userId == -1)
                uid = session.UserId;

            if (uid > 0)
            {
                LogSessionEvent(uid, EventLogConstants.EndSession);
                _userSessions.End(uid, force ? "" : Session.SessionID);
            }

            if (userId == -1)
            {
                session.UserId = -1;
                Context.Session.Abandon();
                ClearLoginCookie(Context);
            }
        }

        public static void LogSessionEvent(int userId, string eventName)
        {
            if (WConfig.EnableLogging)
            {
                var log = new EventLog();
                log.UserId = userId;
                log.EventName = eventName;
                log.EventDate = DateTime.Now;
                log.IPAddress = WHelper.GetUserHostAddress();
                log.Update();
            }
        }

        public UserSessionBrowser Login(int userId, bool logSession = true)
        {
            if (logSession)
                return Login(WebUser.Get(userId), logSession);
            else
                return Login(userId);
        }

        private UserSessionBrowser Login(int userId)
        {
            UserId = userId;
            _userType = -1;

            var session = _userSessions.Create(Context, userId, -1);
            var browser = session.LastBrowserSession;
            browser.IPAddress = WHelper.GetUserHostAddress();
            browser.UserAgent = Context.Request.UserAgent;
            return browser;
        }

        public UserSessionBrowser Login(WebUser user, bool logSession = true)
        {
            var browser = Login(user.Id);

            if (logSession)
            {
                if (WConfig.EnableLogging || user.HaveNotLoggedIn)
                {
                    user.LastLogin = DateTime.Now;
                    user.Update();
                }

                LogSessionEvent(UserId, EventLogConstants.StartSession);
            }

            return browser;
        }

        public static string MapPath(string relPath)
        {
            return Context.Server.MapPath(relPath);
        }


        private const string APP_COOKIE_NAME = "WCMS.Login";

        public static WebUser CheckLoginCookie(HttpContext context, bool clearIfInvalid = true)
        {
            var ctx = (context ?? Context);
            var cookie = ctx.Request.Cookies[APP_COOKIE_NAME];
            if (cookie != null)
            {
                var manager = new LoginCookieManager();
                var user = manager.IsValidAuthCookieValue(cookie.Value);
                if (user != null)
                    return user;

                ClearLoginCookie(ctx, cookie);
            }

            return null;
        }

        public static bool IsLoginCookiePresent(HttpContext context = null)
        {
            return (context ?? Context).Request.Cookies[APP_COOKIE_NAME] != null;
        }

        public static void ClearLoginCookie(HttpContext context = null)
        {
            var ctx = (context ?? Context);
            var cookie = ctx.Request.Cookies[APP_COOKIE_NAME];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-1);
                ctx.Response.Cookies.Add(cookie);
            }
        }

        public static void ClearLoginCookie(HttpContext context, HttpCookie cookie)
        {
            cookie.Expires = DateTime.Now.AddYears(-1);
            context.Response.Cookies.Add(cookie);
        }

        public static void RememberLogin(WebUser user, string password, HttpContext context = null)
        {
            var ctx = (context ?? Context);
            var manager = new LoginCookieManager();
            var cookie = ctx.Request.Cookies[APP_COOKIE_NAME];
            if (cookie == null) cookie = new HttpCookie(APP_COOKIE_NAME);

            cookie.Expires = DateTime.Now.AddDays(60);
            cookie.Value = manager.GetAuthCookieValue(user.Id, password);
            ctx.Response.Cookies.Add(cookie);
        }
    }
}
