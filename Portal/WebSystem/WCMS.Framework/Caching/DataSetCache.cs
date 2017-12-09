using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WCMS.Framework
{
    public class DataSetCache<T> : ICacheManager<T>
    {
        public DataSet AsDataSet()
        {
            return null;
        }

        public CacheStatus CacheStatus { get; set; }

        private DataSet _objectCache = new DataSet();

        public T Add(T value) { return default(T); }

        public T Add(int key, T value) { return default(T); }

        public int Count
        {
            get
            {
                return _objectCache.Tables[0].Rows.Count;
            }
        }

        public bool ContainsKey(int key)
        {
            return false;
        }

        #region ICacheManager<T> Members


        public T this[int key]
        {
            get { return default(T); }

            set
            {
            }
        }

        public bool Remove(int key)
        {
            return false;
        }

        public Dictionary<int, T> AsDictionary()
        {
            return null;
        }

        public List<T> AsList()
        {
            return null;
        }

        #endregion

        #region ICacheManager Members


        public Dictionary<int, T> ObjectCache
        {
            get { return null; }
        }


        public T Get(int key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
