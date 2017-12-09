using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebPermissionProvider : IWebPermissionProvider
    {
        private static IDataProvider<WebPermission> _provider;

        static WebPermissionProvider()
        {
            //_provider = WebObject.ResolveProvider(typeof(WebPermission));
            _provider = new GenericSqlDataProvider<WebPermission>();
        }

        #region IWebPermissionDAL Members

        public WebPermission Get(int id)
        {
            return _provider.Get(id);
        }

        public IEnumerable<WebPermission> GetList()
        {
            return _provider.GetList();
        }

        public int Update(WebPermission item)
        {
            return _provider.Update(item);
        }

        public bool Delete(int id)
        {
            return _provider.Delete(id);
        }

        #endregion

        #region IDataProvider<WebPermission> Members


        public WebPermission Get(params QueryFilterElement[] filters)
        {
            return _provider.Get(filters);
        }

        public IEnumerable<WebPermission> GetList(params QueryFilterElement[] filters)
        {
            return _provider.GetList(filters);
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public WebPermission Refresh(WebPermission item)
        {
            throw new NotImplementedException();
        }
    }
}
