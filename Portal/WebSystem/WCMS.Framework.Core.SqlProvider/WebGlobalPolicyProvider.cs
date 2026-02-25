using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebGlobalPolicyProvider : IWebGlobalPolicyProvider
    {
        #region IDataProvider<WebGlobalPolicy> Members

        public bool Delete(int id)
        {
            DbHelper.ExecuteNonQuery("WebGlobalPolicy_Del",
                DbHelper.CreateParameter("@GlobalPolicyId", id));

            return true;
        }

        public WebGlobalPolicy Get(int id)
        {
            using (var r = DbHelper.ExecuteReader("WebGlobalPolicy_Get",
                DbHelper.CreateParameter("@GlobalPolicyId", id)))
            {
                if (r.Read())
                {
                    return From(r);
                }
            }

            return null;
        }

        private WebGlobalPolicy From(DbDataReader r)
        {
            WebGlobalPolicy item = new WebGlobalPolicy();
            item.Id = DataUtil.GetId(r["GlobalPolicyId"]);
            item.Name = r["Name"].ToString();
            return item;
        }

        public WebGlobalPolicy Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebGlobalPolicy> GetList()
        {
            List<WebGlobalPolicy> items = new List<WebGlobalPolicy>();

            using (var r = DbHelper.ExecuteReader("WebGlobalPolicy_Get"))
            {
                while (r.Read())
                {
                    items.Add(From(r));
                }
            }

            return items;
        }

        public IEnumerable<WebGlobalPolicy> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(WebGlobalPolicy item)
        {
            var obj = DbHelper.ExecuteScalar("WebGlobalPolicy_Set",
                DbHelper.CreateParameter("@GlobalPolicyId", item.Id),
                DbHelper.CreateParameter("@Name", item.Name));

            item.Id = DataUtil.GetId(obj);
            return item.Id;
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public WebGlobalPolicy Refresh(WebGlobalPolicy item)
        {
            throw new NotImplementedException();
        }
    }
}
