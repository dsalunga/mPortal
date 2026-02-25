using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
            using (DbDataReader r = DbHelper.ExecuteReader("WebObject_Get",
                DbHelper.CreateParameter("@Id", id)))
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

            using (DbDataReader r = DbHelper.ExecuteReader("WebObject_Get"))
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

            object o = DbHelper.ExecuteScalar("WebObject_Set",
                DbHelper.CreateParameter("@Id", item.Id),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@IdentityColumn", item.IdentityColumn),
                DbHelper.CreateParameter("@ObjectType", item.ObjectType),
                DbHelper.CreateParameter("@LastRecordId", item.LastRecordId),
                DbHelper.CreateParameter("@MaxCacheCount", item.MaxCacheCount),
                DbHelper.CreateParameter("@AccessTypeId", item.AccessTypeId),
                DbHelper.CreateParameter("@CacheTypeId", item.CacheTypeId),
                DbHelper.CreateParameter("@MaxHistoryCount", item.MaxHistoryCount),
                DbHelper.CreateParameter("@Owner", item.Owner),
                DbHelper.CreateParameter("@Prefix", item.Prefix),
                DbHelper.CreateParameter("@DataProviderName", item.DataProviderName),
                DbHelper.CreateParameter("@TypeName", item.TypeName),
                DbHelper.CreateParameter("@CacheInterval", item.CacheInterval),
                DbHelper.CreateParameter("@NameColumn", item.NameColumn),
                DbHelper.CreateParameter("@FriendlyName", item.FriendlyName),
                DbHelper.CreateParameter("@ManagerName", item.ManagerName)
            );

            item.Id = DataUtil.GetId(o);
            return item.Id;
        }

        public bool Delete(int id)
        {
            DbHelper.ExecuteNonQuery("WebObject_Del",
                DbHelper.CreateParameter("@Id", id));

            return true;
        }


        private WebObject From(DbDataReader r)
        {
            WebObject item = new WebObject();
            item.Id = DataUtil.GetId(r["Id"].ToString());
            item.Name = r["Name"].ToString();
            item.IdentityColumn = r["IdentityColumn"].ToString();
            item.ObjectType = r["ObjectType"].ToString();
            item.LastRecordId = DataUtil.GetInt32(r["LastRecordId"]);
            item.MaxCacheCount = Convert.ToInt32(r["MaxCacheCount"].ToString());
            item.AccessTypeId = DataUtil.GetId(r["AccessTypeId"]);
            item.CacheTypeId = DataUtil.GetId(r["CacheTypeId"]);
            item.MaxHistoryCount = Convert.ToInt32(r["MaxHistoryCount"].ToString());
            item.Owner = r["Owner"].ToString();
            item.Prefix = r["Prefix"].ToString();
            item.DataProviderName = r["DataProviderName"].ToString();
            item.TypeName = r["TypeName"].ToString();
            item.CacheInterval = DataUtil.GetInt32(r["TypeName"]);
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
