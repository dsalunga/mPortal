using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;

using WCMS.Common.Utilities;
using WCMS.Framework.Diagnostics;
using WCMS.Framework.Security;

namespace WCMS.Framework
{
    [Serializable]
    public class WSession : IWSession
    {
        public const string DefaultName = "WSession";

        private static UserSessionManager _userSessions = new UserSessionManager();
        private static IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Configures WSession to resolve IWSession from DI when available.
        /// Call from application startup after building the service provider.
        /// </summary>
        public static void Configure(IHttpContextAccessor accessor)
        {
            _httpContextAccessor = accessor;
        }

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

        private int _inDesignPanelLeft;
        public int InDesignPanelLeft
        {
            get { return _inDesignPanelLeft; }
            set
            {
                _inDesignPanelLeft = value;
                this.Update();
            }
        }

        private int _inDesignPanelTop;
        public int InDesignPanelTop
        {
            get { return _inDesignPanelTop; }
            set
            {
                _inDesignPanelTop = value;
                this.Update();
            }
        }

        private bool _isDesignInitiated;
        public bool IsDesignInitiated
        {
            get { return _isDesignInitiated; }
            set
            {
                _isDesignInitiated = value;
                this.Update();
            }
        }

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
                // Try DI resolution first when configured
                if (_httpContextAccessor?.HttpContext?.RequestServices != null)
                {
                    var resolved = _httpContextAccessor.HttpContext.RequestServices
                        .GetService(typeof(IWSession)) as WSession;
                    if (resolved != null)
                        return resolved;
                }

                // Fall back to HttpContext.Items for per-request caching
                var ctx = Context;
                if (ctx?.Items != null && ctx.Items.TryGetValue(DefaultName, out var cached) && cached is WSession existing)
                    return existing;

                var current = new WSession();
                current.InDesignPanelExpanded = WConfig.PanelExpanded;
                if (ctx?.Items != null)
                    ctx.Items[DefaultName] = current;

                return current;
            }
        }

        public static ISession Session
        {
            get { return Context?.Session; }
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
            get { return HttpContextHelper.Current; }
        }

        #endregion

        public void Update()
        {
            var ctx = Context;
            if (ctx?.Items != null)
                ctx.Items[Name] = this;
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
            var session = Current;

            var uid = userId;
            if (userId == -1)
                uid = session.UserId;

            if (uid > 0)
            {
                LogSessionEvent(uid, EventLogConstants.EndSession);
                _userSessions.End(uid, force ? "" : (Session?.Id ?? ""));
            }

            if (userId == -1)
            {
                session.UserId = -1;
                Context?.Session?.Clear();
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
            browser.UserAgent = Context?.Request?.Headers["User-Agent"].ToString() ?? "";
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

        #region IWSession

        void IWSession.Login(int userId, bool rememberLogin)
        {
            Login(userId, logSession: true);
            // TODO: rememberLogin support requires password context;
            // wire up RememberLogin() when auth flow is refactored for DI.
        }

        void IWSession.Logout()
        {
            LogOff();
        }

        #endregion

        public static string MapPath(string relPath)
        {
            return PathMapper.MapPath(relPath);
        }


        private const string APP_COOKIE_NAME = "WCMS.Login";

        public static WebUser CheckLoginCookie(HttpContext context, bool clearIfInvalid = true)
        {
            var ctx = (context ?? Context);
            var cookieValue = ctx.Request.Cookies[APP_COOKIE_NAME];
            if (cookieValue != null)
            {
                var manager = new LoginCookieManager();
                var user = manager.IsValidAuthCookieValue(cookieValue);
                if (user != null)
                    return user;

                ClearLoginCookie(ctx);
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
            if (ctx.Request.Cookies[APP_COOKIE_NAME] != null)
            {
                ctx.Response.Cookies.Delete(APP_COOKIE_NAME);
            }
        }

        public static void RememberLogin(WebUser user, string password, HttpContext context = null)
        {
            var ctx = (context ?? Context);
            var manager = new LoginCookieManager();
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(60)
            };
            ctx.Response.Cookies.Append(APP_COOKIE_NAME, manager.GetAuthCookieValue(user.Id, password), cookieOptions);
        }
    }
}
