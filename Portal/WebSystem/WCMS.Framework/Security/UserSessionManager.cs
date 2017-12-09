using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WCMS.Framework
{
    public class UserSessionManager
    {
        private MemoryCache<UserSession> _sessionCache = new MemoryCache<UserSession>();
        private Dictionary<string, UserSessionBrowser> _browserCache = new Dictionary<string, UserSessionBrowser>();

        public UserSessionManager() { }

        public MemoryCache<UserSession> SessionCache { get { return _sessionCache; } }

        public Dictionary<string, UserSessionBrowser> BrowserCache { get { return _browserCache; } }

        public IEnumerable<UserSession> Sessions
        {
            get
            {
                return from i in _sessionCache.ObjectCache.Values
                       select i;
            }
        }

        public void Update(UserSession session)
        {
            Update(WSession.Context, session.UserId);
        }

        public void Update(int userId, int pageId, string rawUrl = null)
        {
            Update(WSession.Context, userId, pageId, rawUrl);
        }

        //public void Update(string aspNetSessionId, UserSessionBrowser cache)
        //{
        //    if (_browserCache.ContainsKey(aspNetSessionId))
        //    {
        //        var old = _browserCache[aspNetSessionId];
        //    }
        //    else
        //    {
        //        _browserCache.Add(aspNetSessionId, cache);
        //    }
        //}

        public void Update(HttpContext context, int userId, int pageId = -1, string rawUrl = null)
        {
            //if (WConfig.EnableLogging)
            {
                //UserSession session = null;
                if (_sessionCache.ContainsKey(userId))
                {
                    var aspNetSessionID = context.Session.SessionID;
                    UserSessionBrowser browser = null;
                    if (_browserCache.ContainsKey(aspNetSessionID))
                    {
                        browser = _browserCache[aspNetSessionID];
                    }
                    else
                    {
                        browser = new UserSessionBrowser(aspNetSessionID, userId, pageId);
                        _browserCache.Add(aspNetSessionID, browser);
                    }
                    //session = _sessionCache[userId];
                    browser.IPAddress = context.Request.UserHostAddress;
                    browser.UserAgent = context.Request.UserAgent;
                    browser.LastPageId = pageId;
                    browser.LastPageUrl = rawUrl == null ? (context.Request.IsSecureConnection ? "https://" : "http://") + context.Request.ServerVariables["HTTP_HOST"] + context.Request.RawUrl : rawUrl;
                    browser.LastActivityDate = DateTime.Now;
                    //return session;
                }
                //else
                //{
                //    return _cache.Add(userId, new UserSession(userId, pageId));
                //}

                //return null;
            }
        }

        public UserSession Create(HttpContext context, int userId, int pageId, string rawUrl = null)
        {
            //if (WConfig.EnableLogging)
            {
                UserSession session = null;
                UserSessionBrowser browser = null;
                var aspNetSessionID = context.Session.SessionID;
                if (_sessionCache.ContainsKey(userId))
                {
                    session = _sessionCache[userId];
                    if (_browserCache.ContainsKey(aspNetSessionID))
                    {
                        browser = _browserCache[aspNetSessionID];
                    }
                    else
                    {
                        browser = new UserSessionBrowser(aspNetSessionID, userId, pageId);
                        _browserCache.Add(aspNetSessionID, browser);
                    }

                    //session = _sessionCache[userId];
                    browser.LastPageId = pageId;
                    browser.LastPageUrl = rawUrl == null ? (context.Request.IsSecureConnection ? "https://" : "http://") + context.Request.ServerVariables["HTTP_HOST"] + context.Request.RawUrl : rawUrl;
                    browser.LastActivityDate = DateTime.Now;
                    session.LastBrowserSession = browser;
                }
                else
                {
                    browser = new UserSessionBrowser(aspNetSessionID, userId, pageId);
                    if (!_browserCache.ContainsKey(aspNetSessionID))
                        _browserCache.Add(aspNetSessionID, browser);
                    session = _sessionCache.Add(userId, new UserSession(userId, browser));
                }

                browser.IPAddress = context.Request.UserHostAddress;
                browser.UserAgent = context.Request.UserAgent;

                return session;
            }
        }

        public void End(int userId, string aspNetSessionId = "")
        {
            //if (WConfig.EnableLogging)
            {
                if (string.IsNullOrEmpty(aspNetSessionId))
                {
                    if (_sessionCache.ContainsKey(userId))
                        _sessionCache.Remove(userId);

                    var toRemove = new List<UserSessionBrowser>();
                    foreach (var cache in _browserCache.Values)
                    {
                        if (cache.UserId == userId)
                            toRemove.Add(cache);
                    }

                    foreach (var cache in toRemove)
                    {
                        if (_browserCache.ContainsKey(cache.AspNetSessionID))
                            _browserCache.Remove(cache.AspNetSessionID);
                    }
                }
                else
                {
                    if (_browserCache.ContainsKey(aspNetSessionId))
                    {
                        var toRemove = new List<UserSessionBrowser>();
                        var bc2 = _browserCache[aspNetSessionId];
                        foreach (var cache in _browserCache.Values)
                        {
                            if (cache.UserId == bc2.UserId && cache.UserAgent == bc2.UserAgent && cache.IPAddress == bc2.IPAddress)
                                toRemove.Add(cache);
                        }

                        foreach (var cache in toRemove)
                        {
                            if (_browserCache.ContainsKey(cache.AspNetSessionID))
                                _browserCache.Remove(cache.AspNetSessionID);
                        }
                    }

                    // can still be optimized, reusing above code
                    bool hasMatch = false;
                    UserSessionBrowser bc = null;
                    foreach (var cache in _browserCache)
                    {
                        if (cache.Value.UserId == userId)
                        {
                            bc = cache.Value;
                            hasMatch = true;
                            break;
                        }
                    }

                    if (_sessionCache.ContainsKey(userId))
                    {
                        if (!hasMatch)
                        {
                            _sessionCache.Remove(userId);
                        }
                        else
                        {
                            var cache = _sessionCache[userId];
                            if (cache.LastBrowserSession.AspNetSessionID.Equals(aspNetSessionId))
                                cache.LastBrowserSession = bc;
                        }
                    }
                }
            }
        }

        public bool Contains(int userId)
        {
            return _sessionCache.ContainsKey(userId);
        }

        public bool Contains(string aspNetSessionId)
        {
            return _browserCache.ContainsKey(aspNetSessionId);
        }

        public bool ContainsSessionId(string sessionId)
        {
            return _sessionCache.ObjectCache.Values.FirstOrDefault(i => i.SessionId.Equals(sessionId)) != null;
        }
    }
}
