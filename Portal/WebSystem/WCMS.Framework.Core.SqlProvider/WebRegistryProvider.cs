using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Xml;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebRegistryProvider : IWebRegistryProvider
    {

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebRegistry> GetList()
        {
            List<WebRegistry> items = new List<WebRegistry>();
            using (DbDataReader r = SqlHelper.ExecuteReader("WebRegistry_Get"))
            {
                while (r.Read())
                {
                    items.Add(this.From(r));
                }
            }

            return items;
        }

        public WebRegistry Get(string key)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebRegistry_Get",
                new SqlParameter("@Key", key)))
            {
                if (r.HasRows && r.Read())
                {
                    return this.From(r);
                }
            }

            return null;
        }

        public WebRegistry Get(string key, int parentId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebRegistry_Get",
                new SqlParameter("@Key", key),
                new SqlParameter("@ParentId", parentId)
                ))
            {
                if (r.HasRows && r.Read())
                {
                    return this.From(r);
                }
            }

            return null;
        }

        public WebRegistry Get(int registryId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebRegistry_Get",
                new SqlParameter("@RegistryId", registryId)))
            {
                if (r.HasRows && r.Read())
                {
                    return this.From(r);
                }
            }

            return null;
        }

        public IEnumerable<WebRegistry> GetList(int parentId)
        {
            List<WebRegistry> items = new List<WebRegistry>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebRegistry_Get",
                new SqlParameter("@ParentId", parentId)))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        items.Add(this.From(r));
                    }
                }
            }

            return items;
        }

        private WebRegistry From(DbDataReader r)
        {
            WebRegistry item = new WebRegistry();
            item.Id = DataHelper.GetId(r["RegistryId"]);
            item.Key = r["Key"].ToString();
            item.Value = r["Value"].ToString();
            item.ParentId = DataHelper.GetId(r["ParentId"]);
            item.StageId = DataHelper.GetId(r, "StageId");

            return item;
        }

        public int Update(string key, string value)
        {
            WebRegistry item = WebRegistry.Get(key);
            if (item == null)
            {
                item = new WebRegistry();
            }

            item.Key = key;
            item.Value = value;

            return this.Update(item);
        }

        public int Update(WebRegistry item)
        {
            object o = SqlHelper.ExecuteScalar("WebRegistry_Set",
                new SqlParameter("@RegistryId", item.Id),
                new SqlParameter("@Key", item.Key),
                new SqlParameter("@Value", item.Value),
                new SqlParameter("@ParentId", item.ParentId),
                new SqlParameter("@StageId", item.StageId)
            );

            item.Id = DataHelper.GetId(o);
            return item.Id;
        }

        public bool Delete(string key)
        {
            SqlHelper.ExecuteNonQuery("WebRegistry_Del",
                new SqlParameter("@Key", key));

            return true;
        }

        public bool Delete(int registryId)
        {
            SqlHelper.ExecuteNonQuery("WebRegistry_Del",
                new SqlParameter("@RegistryId", registryId));

            return true;
        }

        #region IDataProvider<WebRegistry> Members

        public WebRegistry Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebRegistry> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        #endregion


        public WebRegistry Refresh(WebRegistry item)
        {
            throw new NotImplementedException();
        }
    }
}
