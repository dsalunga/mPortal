using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebObjectSecurityProvider : IWebObjectSecurityProvider
    {
        private static IDataProvider<WebObjectSecurity> _provider;

        static WebObjectSecurityProvider()
        {
            _provider = new GenericSqlDataProvider<WebObjectSecurity>();
        }

        #region IWebObjectSecurityDAL Members

        public WebObjectSecurity Get(int id)
        {
            return _provider.Get(id);
        }

        public IEnumerable<WebObjectSecurity> GetList()
        {
            return _provider.GetList();
        }

        public IEnumerable<WebObjectSecurity> GetList(int objectId, int recordId,
            int securityObjectId, int securityRecordId, int public2)
        {
            return _provider.GetList(
                new QueryFilterElement { Name = "ObjectId", Value = objectId, NullValue = -1 },
                new QueryFilterElement { Name = "RecordId", Value = recordId, NullValue = -1 },
                new QueryFilterElement { Name = "SecurityObjectId", Value = securityObjectId, NullValue = -1 },
                new QueryFilterElement { Name = "SecurityRecordId", Value = securityRecordId, NullValue = -1 },
                new QueryFilterElement { Name = "Public", Value = public2, NullValue = -1 }
            );
        }

        public IEnumerable<WebObjectSecurity> GetList(int objectId, int recordId)
        {
            return _provider.GetList(
                new QueryFilterElement { Name = "ObjectId", Value = objectId, NullValue = -1 },
                new QueryFilterElement { Name = "RecordId", Value = recordId, NullValue = -1 }
            );
        }


        public WebObjectSecurity Get(int objectId, int recordId,
            int securityObjectId, int securityRecordId, int public2)
        {
            return _provider.Get(
                new QueryFilterElement("ObjectId", objectId),
                new QueryFilterElement("RecordId", recordId),
                new QueryFilterElement("SecurityObjectId", securityObjectId),
                new QueryFilterElement("SecurityRecordId", securityRecordId),
                new QueryFilterElement { Name = "Public", Value = public2, NullValue = -1 }
            );
        }

        public int Update(WebObjectSecurity item)
        {
            return _provider.Update(item);
        }

        public bool Delete(int id)
        {
            return _provider.Delete(id);
        }

        #endregion

        #region IDataProvider<WebObjectSecurity> Members


        public WebObjectSecurity Get(params QueryFilterElement[] filters)
        {
            return _provider.Get(filters);
        }

        public IEnumerable<WebObjectSecurity> GetList(params QueryFilterElement[] filters)
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


        public WebObjectSecurity Refresh(WebObjectSecurity item)
        {
            throw new NotImplementedException();
        }
    }
}
