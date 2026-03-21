using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebObjectIPAddressProvider : IWebObjectIPAddressProvider
    {
        private static IDataProvider<WebObjectIPAddress> _provider;

        static WebObjectIPAddressProvider()
        {
            _provider = WebObject.ResolveProvider<WebObjectIPAddress>();
        }

        #region IWebObjectIPAddressDAL Members

        public WebObjectIPAddress Get(int id)
        {
            return _provider.Get(id);
        }

        public IEnumerable<WebObjectIPAddress> GetList(int objectId, int recordId)
        {
            return _provider.GetList(
                new QueryFilterElement { Name = "ObjectId", Value = objectId, NullValue = -1 },
                new QueryFilterElement { Name = "RecordId", Value = recordId, NullValue = -1 }
            );
        }

        public int Update(WebObjectIPAddress item)
        {
            return _provider.Update(item);
        }

        public bool Delete(int id)
        {
            return _provider.Delete(id);
        }

        #endregion
    }
}
