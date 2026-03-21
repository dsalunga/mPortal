using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebObjectSecurityPermissionProvider : IWebObjectSecurityPermissionProvider
    {
        private static IDataProvider<WebObjectSecurityPermission> _provider;

        static WebObjectSecurityPermissionProvider()
        {
            _provider = new GenericSqlDataProvider<WebObjectSecurityPermission>(); //WebObject.ResolveProvider<WebObjectSecurityPermission>();
        }

        #region IWebObjectSecurityPermissionDAL Members

        public WebObjectSecurityPermission Get(int id)
        {
            return _provider.Get(id);
        }

        public IEnumerable<WebObjectSecurityPermission> GetList(int objectSecurityId)
        {
            return _provider.GetList(
                new QueryFilterElement { Name = "ObjectSecurityId", Value = objectSecurityId, NullValue = -1 }
            );
        }

        public int Update(WebObjectSecurityPermission item)
        {
            return _provider.Update(item);
        }

        public bool Delete(int id)
        {
            return _provider.Delete(id);
        }

        #endregion

        #region IDataProvider<WebObjectSecurityPermission> Members


        public WebObjectSecurityPermission Get(params QueryFilterElement[] filters)
        {
            //return _provider.Get(filters);
            throw new NotImplementedException();
        }

        public IEnumerable<WebObjectSecurityPermission> GetList()
        {
            return _provider.GetList();
        }

        public IEnumerable<WebObjectSecurityPermission> GetList(params QueryFilterElement[] filters)
        {
            //return _provider.GetList(filters);
            throw new NotImplementedException();
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


        public WebObjectSecurityPermission Refresh(WebObjectSecurityPermission item)
        {
            throw new NotImplementedException();
        }
    }
}
