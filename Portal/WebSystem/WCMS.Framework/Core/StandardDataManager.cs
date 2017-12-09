using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;
using WCMS.Framework.Diagnostics;

namespace WCMS.Framework.Core
{
    public class StandardDataManager<T> : IDataManager<T> where T : IWebObject
    {
        private IDataProvider<T> _provider;

        protected MemoryCache<T> _cache;
        protected WebObject _object;
        protected Type _type;

        public int GetCachedItemCount()
        {
            return _cache == null ? 0 : _cache.Count;
        }

        public StandardDataManager(IDataProvider<T> provider)
        {
            try
            {
                _cache = new MemoryCache<T>();
                _provider = provider;
                _type = typeof(T);
                _object = _type == WebObject.Type ? new WebObject { Id = WebObjects.WebObject, CacheTypeId = CacheTypes.Full } : WebObject.Get<T>();

                WebObject.CacheFlagUpdated += CacheFlagUpdated;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw;
            }
        }

        public void LoadCache()
        {
            if (_object.IsFullCache)
            {
                var sw = PerformanceLog.StartLogNoCheck();

                _cache.ObjectCache.Clear();

                var items = _provider.GetList();
                foreach (var item in items)
                {
                    _cache.Add(item.Id, item);
                }

                _cache.CacheStatus = CacheStatus.Full;

                PerformanceLog.EndLogNoCheck(string.Format("Cache Init: {0} - {1}", _object.Name, _object.Id), sw, -1);
            }
        }

        public void UnloadCache()
        {
            _cache.ObjectCache.Clear();
            _cache.CacheStatus = CacheStatus.Empty;
        }

        private void CacheFlagUpdated(object sender, WebObjectCacheFlagUpdateEventArgs args)
        {
            if (args.Item != null && args.Item.Id == _object.Id && args.Item.CacheTypeId != args.OldCacheFlag)
            {
                _cache = new MemoryCache<T>();

                if (args.Item.IsFullCache)
                    LoadCache();
            }
        }

        public virtual IDataProvider<T> Provider { get { return _provider; } }

        public virtual T Get(int id)
        {
            if (id >= 0)
            {
                if (_cache.CacheStatus == CacheStatus.Full)
                {
                    if(_cache.ContainsKey(id))
                        return _cache[id];
                    else
                        return default(T);
                }
                else if (_cache.CacheStatus == CacheStatus.Partial && _cache.ContainsKey(id))
                    return _cache[id];

                return _provider.Get(id);
            }

            return default(T);
        }

        public virtual T Refresh(T item)
        {
            return _provider.Refresh(item);
        }

        public virtual int GetCount()
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.Count;

            return _provider.GetCount();
        }

        public virtual T Get(params QueryFilterElement[] filters)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                foreach (T item in _cache.ObjectCache.Values)
                {
                    bool satisfied = true;
                    foreach (QueryFilterElement filter in filters)
                    {
                        PropertyInfo info = _type.GetProperties().FirstOrDefault(p => p.Name == filter.Name);
                        object oValue = info.GetValue(item, null);
                        if (!filter.Equals(oValue))
                        {
                            satisfied = false;
                            break;
                        }
                    }

                    if (satisfied)
                        return item;
                }

                return default(T);
            }

            return _provider.Get(filters);
        }

        public virtual IEnumerable<T> GetList(params QueryFilterElement[] filters)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                List<T> items = new List<T>();
                foreach (T item in _cache.ObjectCache.Values)
                {
                    bool satisfied = true;
                    foreach (QueryFilterElement filter in filters)
                    {
                        PropertyInfo info = _type.GetProperties().FirstOrDefault(p => p.Name == filter.Name);
                        object oValue = info.GetValue(item, null);

                        if (!filter.Equals(oValue))
                        {
                            satisfied = false;
                            break;
                        }
                    }

                    if (satisfied)
                        items.Add(item);
                }

                return items;
            }

            return _provider.GetList(filters);
        }

        public virtual bool Delete(int id)
        {
            if (_object.AllowCache)
            {
                if (_cache.ContainsKey(id))
                    _cache.Remove(id);
            }

            return _provider.Delete(id);
        }

        public virtual int Update(T item)
        {
            int id = _provider.Update(item);

            if (_object.AllowCache)
            {
                if (_cache.ContainsKey(id))
                    _cache[id] = item;
                else
                    _cache.Add(id, item);
            }

            return id;
        }

        public virtual IEnumerable<T> GetList()
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return from i in _cache.ObjectCache.Values
                       select i;

            return _provider.GetList();
        }

        public virtual IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            return _provider.GetByDirectory(directoryId, loweredKeyword);
        }

        #region IDataManager Members

        public int UpdateAllFromCache()
        {
            throw new NotImplementedException();
        }

        #endregion

        public MemoryCache<T> Cache
        {
            get { return _cache; }
        }
    }

    /// <summary>
    /// For now, used only by WebObject.
    /// </summary>
    public class StandardDataManager : IDataManager
    {
        private Dictionary<int, MemoryCache<IWebObject>> _cache;
        private IDataProvider _manager;

        public StandardDataManager(IDataProvider dataAccess)
        {
            _manager = dataAccess;
            _cache = new Dictionary<int, MemoryCache<IWebObject>>();
        }

        public T Get<T>(int id) where T : IWebObject
        {
            return _manager.Get<T>(id);
        }

        public T Get<T>(params QueryFilterElement[] filters) where T : IWebObject
        {
            return _manager.Get<T>(filters);
        }

        public IEnumerable<T> GetList<T>(params QueryFilterElement[] filters) where T : IWebObject
        {
            return _manager.GetList<T>(filters);
        }

        public bool Delete<T>(int id) where T : IWebObject
        {
            return _manager.Delete<T>(id);
        }

        public int Update<T>(T item) where T : IWebObject
        {
            return _manager.Update<T>(item);
        }

        public IEnumerable<T> GetList<T>() where T : IWebObject
        {
            return _manager.GetList<T>();
        }

        #region IDataManager Members


        public int UpdateAllFromCache<T>() where T : IWebObject
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
