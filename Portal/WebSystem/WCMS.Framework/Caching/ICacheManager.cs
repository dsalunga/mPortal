using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WCMS.Framework
{
    public interface ICacheManager<T>
    {
        DataSet AsDataSet();
        Dictionary<int, T> AsDictionary();

        Dictionary<int, T> ObjectCache { get; }
        CacheStatus CacheStatus { get; set; }

        T Add(int key, T value);
        int Count { get; }
        bool ContainsKey(int key);
        T this[int key] { get; set; }
        T Get(int key);
        bool Remove(int key);
    }
}