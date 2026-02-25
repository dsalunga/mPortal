using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using Microsoft.Extensions.Caching.Distributed;

namespace WCMS.Framework
{
    public class UserSessionManager
    {
        private MemoryCache<UserSession> _sessionCache = new MemoryCache<UserSession>();
        private Dictionary<string, UserSessionBrowser> _browserCache = new Dictionary<string, UserSessionBrowser>();
        private readonly IDistributedCache _distributedCache;

        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        public UserSessionManager() : this(null) { }

        public UserSessionManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

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

        #region Distributed cache helpers

        private static string DistributedKey(int userId) => $"wcms:usersession:{userId}";

        private bool SessionContainsKey(int userId)
        {
            if (_sessionCache.ContainsKey(userId))
                return true;

            if (_distributedCache != null)
            {
                var bytes = _distributedCache.Get(DistributedKey(userId));
                if (bytes != null)
                {
                    var session = JsonSerializer.Deserialize<UserSession>(bytes, _jsonOptions);
                    _sessionCache.Add(userId, session);
                    return true;
                }
            }

            return false;
        }

        private UserSession SessionGet(int userId)
        {
            if (_sessionCache.ContainsKey(userId))
                return _sessionCache[userId];

            if (_distributedCache != null)
            {
                var bytes = _distributedCache.Get(DistributedKey(userId));
                if (bytes != null)
                {
                    var session = JsonSerializer.Deserialize<UserSession>(bytes, _jsonOptions);
                    _sessionCache.Add(userId, session);
                    return session;
                }
            }

            return default;
        }

        private UserSession SessionAdd(int userId, UserSession session)
        {
            if (!_sessionCache.ContainsKey(userId))
                _sessionCache.Add(userId, session);
            else
                _sessionCache[userId] = session;

            if (_distributedCache != null)
                _distributedCache.Set(DistributedKey(userId), JsonSerializer.SerializeToUtf8Bytes(session, _jsonOptions));

            return session;
        }

        private void SessionRemove(int userId)
        {
            if (_sessionCache.ContainsKey(userId))
                _sessionCache.Remove(userId);

            _distributedCache?.Remove(DistributedKey(userId));
        }

        #endregion

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
                if (SessionContainsKey(userId))
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
                if (SessionContainsKey(userId))
                {
                    session = SessionGet(userId);
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
                    SessionAdd(userId, session);
                }
                else
                {
                    browser = new UserSessionBrowser(aspNetSessionID, userId, pageId);
                    if (!_browserCache.ContainsKey(aspNetSessionID))
                        _browserCache.Add(aspNetSessionID, browser);
                    session = SessionAdd(userId, new UserSession(userId, browser));
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
                    if (SessionContainsKey(userId))
                        SessionRemove(userId);

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

                    if (SessionContainsKey(userId))
                    {
                        if (!hasMatch)
                        {
                            SessionRemove(userId);
                        }
                        else
                        {
                            var cache = SessionGet(userId);
                            if (cache.LastBrowserSession.AspNetSessionID.Equals(aspNetSessionId))
                            {
                                cache.LastBrowserSession = bc;
                                SessionAdd(userId, cache);
                            }
                        }
                    }
                }
            }
        }

        public bool Contains(int userId)
        {
            return SessionContainsKey(userId);
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
