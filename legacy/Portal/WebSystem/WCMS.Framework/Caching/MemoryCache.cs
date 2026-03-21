using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WCMS.Framework
{
    public class MemoryCache<T> : ICacheManager<T>
    {
        public DataSet AsDataSet()
        {
            return null;
        }

        public CacheStatus CacheStatus { get; set; }

        private Dictionary<int, T> _objectCache = new Dictionary<int, T>();

        public Dictionary<int, T> ObjectCache
        {
            get { return _objectCache; }
        }

        public T Add(int key, T value)
        {
            _objectCache.Add(key, value);

            return value;
        }

        public int Count
        {
            get
            {
                return _objectCache.Count;
            }
        }

        public bool ContainsKey(int key)
        {
            return _objectCache.ContainsKey(key);
        }

        #region ICacheManager<T> Members

        public T this[int key]
        {
            get { return _objectCache[key]; }

            set
            {
                _objectCache[key] = value;
            }
        }

        public bool Remove(int key)
        {
            return _objectCache.Remove(key);
        }


        public Dictionary<int, T> AsDictionary()
        {
            return _objectCache;
        }

        //public List<T> AsList()
        //{
        //    return _objectCache.Values.ToList();
        //}

        public T Get(int key)
        {
            return _objectCache[key];
        }

        #endregion
    }
}
