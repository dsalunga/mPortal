using System;
using System.Collections.Generic;

namespace WCMS.Framework.Core
{
    public interface IDataManager<T> where T : IWebObject
    {
        bool Delete(int id);
        T Get(int id);
        T Get(params QueryFilterElement[] filters);
        IEnumerable<T> GetList();
        IEnumerable<T> GetList(params QueryFilterElement[] filters);
        int Update(T item);
        int UpdateAllFromCache();
        int GetCount();
        T Refresh(T item);

        IDataProvider<T> Provider { get; }
    }

    /// <summary>
    /// For now, used only in WebObject.
    /// </summary>
    public interface IDataManager
    {
        bool Delete<T>(int id) where T : IWebObject;
        T Get<T>(int id) where T : IWebObject;
        T Get<T>(params QueryFilterElement[] filters) where T : IWebObject;
        IEnumerable<T> GetList<T>() where T : IWebObject;
        IEnumerable<T> GetList<T>(params QueryFilterElement[] filters) where T : IWebObject;
        int Update<T>(T item) where T : IWebObject;
        int UpdateAllFromCache<T>() where T : IWebObject;
    }
}
