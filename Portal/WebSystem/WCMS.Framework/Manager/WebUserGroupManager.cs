using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebUserGroupManager : StandardDataManager<WebUserGroup>, IWebUserGroupProvider
    {
        protected IWebUserGroupProvider _provider;

        public WebUserGroupManager(IWebUserGroupProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebUserGroupProvider Members

        public WebUserGroup Get(int groupId, int id, bool isGroup = false)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values.FirstOrDefault(i =>
                    (groupId == -1 || i.GroupId == groupId) &&
                    (id == -1 || i.RecordId == id && (isGroup && i.ObjectId == WebObjects.WebGroup || i.ObjectId == WebObjects.WebUser)));
            }

            return _provider.Get(groupId, id);
        }

        public IEnumerable<WebUserGroup> GetByUserId(int userId, int active)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(i => (userId == -1 || i.RecordId == userId && i.ObjectId == WebObjects.WebUser) &&
                                (active == -1 || i.Active == active));
            }

            return _provider.GetByUserId(userId, active);
        }

        public IEnumerable<WebUserGroup> GetByGroupId(int groupId, int active)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values.Where(i => (groupId == -1 || i.GroupId == groupId) &&
                                (active == -1 || i.Active == active));
            }

            return _provider.GetByGroupId(groupId, active);
        }

        public IEnumerable<WebUserGroup> GetByCreatedById(int groupId, int createdById, int active)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values.Where(i => (groupId == -1 || i.GroupId == groupId) &&
                    (createdById == -2 || i.CreatedById == createdById) &&
                    (active == -1 || i.Active == active));
            }

            return _provider.GetByCreatedById(groupId, createdById, active);
        }

        public IEnumerable<WebUserGroup> GetList(int active)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(i => active == -1 || i.Active == active);
            }

            return _provider.GetList(active);
        }

        public bool Delete(int userId, int groupId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                var item = _cache.ObjectCache.FirstOrDefault(i =>
                    i.Value.RecordId == userId &&
                    i.Value.ObjectId == WebObjects.WebUser &&
                    i.Value.GroupId == groupId);

                if (item.Value != null)
                    _cache.ObjectCache.Remove(item.Key);
            }

            return _provider.Delete(userId, groupId);
        }

        public bool Delete(int groupId, int objectId, int recordId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                var item = _cache.ObjectCache.FirstOrDefault(i =>
                    i.Value.RecordId == recordId &&
                    i.Value.ObjectId == objectId &&
                    i.Value.GroupId == groupId);

                if (item.Value != null)
                    _cache.ObjectCache.Remove(item.Key);
            }

            return _provider.Delete(groupId, objectId, recordId);
        }

        #endregion
    }
}
