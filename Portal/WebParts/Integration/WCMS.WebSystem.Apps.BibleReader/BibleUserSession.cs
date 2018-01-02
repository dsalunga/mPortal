using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;

using WCMS.Framework;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.WebParts.BibleReader
{
    public class BibleUserSession
    {
        public static Dictionary<string, BibleUserSession> Cache
        {
            get
            {
                var cache = MemoryCache.Default[BibleReaderConstants.SESSIONS_KEY] as Dictionary<string, BibleUserSession>;
                if (cache == null)
                    cache = new Dictionary<string, BibleUserSession>();

                return cache;
            }

            set
            {
                var policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(20);

                MemoryCache.Default.Set(BibleReaderConstants.SESSIONS_KEY, value, policy);
            }
        }

        public BibleUserSession()
        {
            ID = Guid.NewGuid();
        }

        public BibleUserSession(int userId)
            : this()
        {
            this.UserId = userId;

            if (userId > 0)
            {
                var user = WebUser.Get(userId);
                if (user != null)
                {
                    this.UserDisplayName = AccountHelper.GetPrefixedName(user);
                    this.UserName = user.UserName;
                }
            }
        }

        public Guid ID { get; set; }
        public string UserDisplayName { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }

        public static void Add(BibleUserSession session)
        {
            if (session != null)
            {
                var cache = Cache;
                var guid = session.ID.ToString();
                if (!cache.ContainsKey(guid))
                {
                    cache.Add(guid, session);
                    Cache = cache;
                }
            }
        }

        public static void Remove(string guid)
        {
            var cache = Cache;
            if (cache.ContainsKey(guid))
            {
                cache.Remove(guid);

                Cache = cache;
            }
        }

        public static BibleUserSession Get(string guid)
        {
            var cache = Cache;
            if (cache.ContainsKey(guid))
            {
                var session = cache[guid];

                cache.Remove(guid);

                Cache = cache;

                return session;
            }

            return null;
        }
    }
}
