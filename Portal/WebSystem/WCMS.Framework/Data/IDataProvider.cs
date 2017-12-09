using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public interface IDataProvider<T> where T : IWebObject
    {
        bool Delete(int id);
        T Get(int id);
        T Get(params QueryFilterElement[] filters);
        IEnumerable<T> GetList();
        IEnumerable<T> GetList(params QueryFilterElement[] filters);
        int GetCount();
        int Update(T item);
        T Refresh(T item);

        IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword);
    }

    public interface IDataProvider
    {
        bool Delete<T>(int id) where T : IWebObject;
        T Get<T>(int id) where T : IWebObject;
        T Get<T>(params QueryFilterElement[] filters) where T : IWebObject;
        IEnumerable<T> GetList<T>() where T : IWebObject;
        IEnumerable<T> GetList<T>(params QueryFilterElement[] filters) where T : IWebObject;
        int GetCount<T>() where T : IWebObject;
        int Update<T>(T item) where T : IWebObject;

        IEnumerable<WebDirectoryEntry> GetByDirectory<T>(int directoryId, string loweredKeyword) where T : IWebObject;
    }
}
