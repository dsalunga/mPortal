using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebObjectProvider : IWebObjectProvider
    {
        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public WebObject Get(int id)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebObject_Get",
                new SqlParameter("@Id", id)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<WebObject> GetList()
        {
            //return _provider.GetList<WebObject>();
            List<WebObject> items = new List<WebObject>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebObject_Get"))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public int Update(WebObject item)
        {
            //return _provider.Update<WebObject>(item);

            object o = SqlHelper.ExecuteScalar("WebObject_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@IdentityColumn", item.IdentityColumn),
                new SqlParameter("@ObjectType", item.ObjectType),
                new SqlParameter("@LastRecordId", item.LastRecordId),
                new SqlParameter("@MaxCacheCount", item.MaxCacheCount),
                new SqlParameter("@AccessTypeId", item.AccessTypeId),
                new SqlParameter("@CacheTypeId", item.CacheTypeId),
                new SqlParameter("@MaxHistoryCount", item.MaxHistoryCount),
                new SqlParameter("@Owner", item.Owner),
                new SqlParameter("@Prefix", item.Prefix),
                new SqlParameter("@DataProviderName", item.DataProviderName),
                new SqlParameter("@TypeName", item.TypeName),
                new SqlParameter("@CacheInterval", item.CacheInterval),
                new SqlParameter("@NameColumn", item.NameColumn),
                new SqlParameter("@FriendlyName", item.FriendlyName),
                new SqlParameter("@ManagerName", item.ManagerName)
            );

            item.Id = DataHelper.GetId(o);
            return item.Id;
        }

        public bool Delete(int id)
        {
            SqlHelper.ExecuteNonQuery("WebObject_Del",
                new SqlParameter("@Id", id));

            return true;
        }


        private WebObject From(DbDataReader r)
        {
            WebObject item = new WebObject();
            item.Id = DataHelper.GetId(r["Id"].ToString());
            item.Name = r["Name"].ToString();
            item.IdentityColumn = r["IdentityColumn"].ToString();
            item.ObjectType = r["ObjectType"].ToString();
            item.LastRecordId = DataHelper.GetInt32(r["LastRecordId"]);
            item.MaxCacheCount = Convert.ToInt32(r["MaxCacheCount"].ToString());
            item.AccessTypeId = DataHelper.GetId(r["AccessTypeId"]);
            item.CacheTypeId = DataHelper.GetId(r["CacheTypeId"]);
            item.MaxHistoryCount = Convert.ToInt32(r["MaxHistoryCount"].ToString());
            item.Owner = r["Owner"].ToString();
            item.Prefix = r["Prefix"].ToString();
            item.DataProviderName = r["DataProviderName"].ToString();
            item.TypeName = r["TypeName"].ToString();
            item.CacheInterval = DataHelper.GetInt32(r["TypeName"]);
            item.DateModified = (DateTime)r["DateModified"];
            item.ManagerName = r["ManagerName"].ToString();
            item.NameColumn = r["NameColumn"].ToString();
            item.FriendlyName = r["FriendlyName"].ToString();

            return item;
        }

        #region IWebObjectProvider Members


        public bool Update(List<WebObject> items)
        {
            foreach (var item in items)
            {
                item.Update();
            }

            return true;
        }

        #endregion

        #region IDataProvider<WebObject> Members

        public WebObject Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebObject> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        #endregion


        public WebObject Refresh(WebObject item)
        {
            throw new NotImplementedException();
        }
    }
}
