using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebRoleProvider : IWebRoleProvider
    {
        private static IDataProvider _provider;

        static WebRoleProvider()
        {
            _provider = WebObject.ResolveProvider(typeof(WebRole));
        }

        #region IWebGroupDAL Members

        public WebRole Get(int id)
        {
            return _provider.Get<WebRole>(id);
        }

        public IEnumerable<WebRole> GetList()
        {
            return _provider.GetList<WebRole>();
        }

        public int Update(WebRole item)
        {
            return _provider.Update<WebRole>(item);
        }

        public bool Delete(int id)
        {
            return _provider.Delete<WebRole>(id);
        }

        #endregion

        #region IDataProvider<WebRole> Members


        public WebRole Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebRole> GetList(params QueryFilterElement[] filters)
        {
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


        public WebRole Refresh(WebRole item)
        {
            throw new NotImplementedException();
        }
    }
}
