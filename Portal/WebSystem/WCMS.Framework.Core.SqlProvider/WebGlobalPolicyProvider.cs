using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebGlobalPolicyProvider : IWebGlobalPolicyProvider
    {
        #region IDataProvider<WebGlobalPolicy> Members

        public bool Delete(int id)
        {
            SqlHelper.ExecuteNonQuery("WebGlobalPolicy_Del",
                new SqlParameter("@GlobalPolicyId", id));

            return true;
        }

        public WebGlobalPolicy Get(int id)
        {
            using (var r = SqlHelper.ExecuteReader("WebGlobalPolicy_Get",
                new SqlParameter("@GlobalPolicyId", id)))
            {
                if (r.Read())
                {
                    return From(r);
                }
            }

            return null;
        }

        private WebGlobalPolicy From(SqlDataReader r)
        {
            WebGlobalPolicy item = new WebGlobalPolicy();
            item.Id = DataHelper.GetId(r["GlobalPolicyId"]);
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

            using (var r = SqlHelper.ExecuteReader("WebGlobalPolicy_Get"))
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
            var obj = SqlHelper.ExecuteScalar("WebGlobalPolicy_Set",
                new SqlParameter("@GlobalPolicyId", item.Id),
                new SqlParameter("@Name", item.Name));

            item.Id = DataHelper.GetId(obj);
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
