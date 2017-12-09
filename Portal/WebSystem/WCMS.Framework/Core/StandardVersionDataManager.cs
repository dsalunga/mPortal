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

namespace WCMS.Framework.Core
{
    public class GenericVersionDataManager<T> : IVersionDataManager<T> where T : IVersionWebObject
    {
        private ICacheManager<T> _cache;
        private IVersionDataProvider<T> _provider;
        private WebObject _object;
        private Type _type;

        public GenericVersionDataManager(IVersionDataProvider<T> provider)
        {
            _provider = provider;
            _cache = new MemoryCache<T>();
            _object = WebObject.Get<T>();
            _type = typeof(T); //_object.TypeInstance;

            if (_object.CacheTypeId == CacheTypes.Full)
            {
                var items = _provider.GetList();
                foreach (var item in items)
                    _cache.Add(item.Id, item);
            }
        }

        public IVersionDataProvider<T> VersionProvider
        {
            get { return _provider; }
        }

        public IDataProvider<T> Provider
        {
            get { return _provider.BaseProvider; }
        }

        public T Get(int id)
        {
            if (_object.CacheTypeId == CacheTypes.Full && _cache.ContainsKey(id))
                return _cache[id];

            return _provider.Get(id);
        }

        public T Refresh(T item)
        {
            return _provider.Refresh(item);
        }

        public int GetCount()
        {
            if (_object.CacheTypeId == CacheTypes.Full)
                return _cache.Count;

            return _provider.GetCount();
        }

        public T Get(params QueryFilterElement[] filters)
        {
            if (_object.CacheTypeId == CacheTypes.Full)
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

        public IEnumerable<T> GetList(params QueryFilterElement[] filters)
        {
            if (_object.CacheTypeId == CacheTypes.Full)
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

        public bool Delete(int id)
        {
            if (_object.CacheTypeId == CacheTypes.Full)
            {
                if (_cache.ContainsKey(id))
                    _cache.Remove(id);
            }

            return _provider.Delete(id);
        }

        public int Update(T item)
        {
            int id = _provider.Update(item);

            if (_object.CacheTypeId == CacheTypes.Full)
            {
                if (_cache.ContainsKey(id))
                    _cache[id] = item;
                else
                    _cache.Add(id, item);
            }

            return id;
        }

        public IEnumerable<T> GetList()
        {
            if (_object.CacheTypeId == CacheTypes.Full)
                return from i in _cache.ObjectCache.Values
                       select i;

            return _provider.GetList();
        }

        #region IDataManager Members


        public int UpdateAllFromCache()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    /// <summary>
    /// For now, used only by WebObject.
    /// </summary>
    public class StandardVersionDataManager : IDataManager
    {
        private Dictionary<int, MemoryCache<IWebObject>> _cache;
        private IDataProvider _manager;

        public StandardVersionDataManager(IDataProvider dataAccess)
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
