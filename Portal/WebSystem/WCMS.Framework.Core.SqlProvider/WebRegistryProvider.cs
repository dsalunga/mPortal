using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
            using (DbDataReader r = DbHelper.ExecuteReader("WebRegistry_Get"))
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
            using (DbDataReader r = DbHelper.ExecuteReader("WebRegistry_Get",
                DbHelper.CreateParameter("@Key", key)))
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
            using (DbDataReader r = DbHelper.ExecuteReader("WebRegistry_Get",
                DbHelper.CreateParameter("@Key", key),
                DbHelper.CreateParameter("@ParentId", parentId)
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
            using (DbDataReader r = DbHelper.ExecuteReader("WebRegistry_Get",
                DbHelper.CreateParameter("@RegistryId", registryId)))
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

            using (DbDataReader r = DbHelper.ExecuteReader("WebRegistry_Get",
                DbHelper.CreateParameter("@ParentId", parentId)))
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
            item.Id = DataUtil.GetId(r["RegistryId"]);
            item.Key = r["Key"].ToString();
            item.Value = r["Value"].ToString();
            item.ParentId = DataUtil.GetId(r["ParentId"]);
            item.StageId = DataUtil.GetId(r, "StageId");

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
            object o = DbHelper.ExecuteScalar("WebRegistry_Set",
                DbHelper.CreateParameter("@RegistryId", item.Id),
                DbHelper.CreateParameter("@Key", item.Key),
                DbHelper.CreateParameter("@Value", item.Value),
                DbHelper.CreateParameter("@ParentId", item.ParentId),
                DbHelper.CreateParameter("@StageId", item.StageId)
            );

            item.Id = DataUtil.GetId(o);
            return item.Id;
        }

        public bool Delete(string key)
        {
            DbHelper.ExecuteNonQuery("WebRegistry_Del",
                DbHelper.CreateParameter("@Key", key));

            return true;
        }

        public bool Delete(int registryId)
        {
            DbHelper.ExecuteNonQuery("WebRegistry_Del",
                DbHelper.CreateParameter("@RegistryId", registryId));

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
