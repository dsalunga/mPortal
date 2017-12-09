using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WCMS.Framework
{
    public class NullCache<T> : ICacheManager<T>
    {
        public DataSet AsDataSet()
        {
            return null;
        }

        public T Get(int id)
        {
            return default(T);
        }

        public T Add(int key, T value)
        {
            return default(T);
        }

        public CacheStatus CacheStatus { get; set; }

        public int Count
        {
            get
            {
                return -1;
            }
        }

        public bool ContainsKey(int key)
        {
            return false;
        }

        public bool Remove(int key)
        {
            return false;
        }

        public T this[int key]
        {
            get { return default(T); }

            set { }
        }

        #region ICacheManager<T> Members


        public Dictionary<int, T> AsDictionary()
        {
            return null;
        }

        public Dictionary<int, T> ObjectCache
        {
            get { return null; }
        }

        public List<T> AsList()
        {
            return null;
        }

        #endregion
    }
}
