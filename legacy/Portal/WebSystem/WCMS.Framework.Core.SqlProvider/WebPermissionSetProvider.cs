using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using WCMS.Framework.Core;
using WCMS.Common.Utilities;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebPermissionSetProvider : IWebPermissionSetProvider
    {
        private static IDataProvider _provider;

        static WebPermissionSetProvider()
        {
            _provider = WebObject.ResolveProvider(typeof(WebPermissionSet));
        }

        #region IWebPermissionSetDAL Members

        public WebPermissionSet Get(int id)
        {
            return _provider.Get<WebPermissionSet>(id);
        }

        //private WebPermissionSet From(DbDataReader r)
        //{
        //    WebPermissionSet item = new WebPermissionSet();
        //    item.Id = DataHelper.GetDbId(r["Id"]);
        //    item.ObjectId = DataHelper.GetDbId(r["ObjectId"]);
        //    item.PermissionId = DataHelper.GetDbId(r["PermissionId"]);

        //    return item;
        //}

        public IEnumerable<WebPermissionSet> GetList()
        {
            return _provider.GetList<WebPermissionSet>();
        }

        public IEnumerable<WebPermissionSet> GetList(int objectId, int recordId, int public2)
        {
            var items = _provider.GetList<WebPermissionSet>(
                    new QueryFilterElement { Name = "ObjectId", Value = objectId, NullValue = -1 },
                    new QueryFilterElement { Name = "RecordId", Value = -1, NullValue = -2 },
                    new QueryFilterElement { Name = "Public", Value = public2, NullValue = -1 });

            if (recordId > 0)
            {
                return items.Concat(_provider.GetList<WebPermissionSet>(
                    new QueryFilterElement { Name = "ObjectId", Value = objectId, NullValue = -1 },
                    new QueryFilterElement { Name = "RecordId", Value = recordId, NullValue = -1 },
                    new QueryFilterElement { Name = "Public", Value = public2, NullValue = -1 }
                    ));   
            }

            return items;
        }

        public int Update(WebPermissionSet item)
        {
            return _provider.Update<WebPermissionSet>(item);
        }

        public bool Delete(int id)
        {
            return _provider.Delete<WebPermissionSet>(id);
        }

        #endregion

        #region IDataProvider<WebPermissionSet> Members


        public WebPermissionSet Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebPermissionSet> GetList(params QueryFilterElement[] filters)
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


        public WebPermissionSet Refresh(WebPermissionSet item)
        {
            throw new NotImplementedException();
        }
    }
}
