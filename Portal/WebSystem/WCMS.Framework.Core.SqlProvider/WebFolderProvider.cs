using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebFolderProvider : IWebFolderProvider
    {
        #region IDataProvider<WebFolder> Members

        public bool Delete(int id)
        {
            DbHelper.ExecuteNonQuery("WebFolder_Del",
                DbHelper.CreateParameter("@Id", id));

            return true;
        }

        public WebFolder Get(int id)
        {
            using (var r = DbHelper.ExecuteReader("WebFolder_Get",
                DbHelper.CreateParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public WebFolder Get(int parentId, string name)
        {
            using (var r = DbHelper.ExecuteReader("WebFolder_Get",
                DbHelper.CreateParameter("@ParentId", parentId),
                DbHelper.CreateParameter("@Name", name)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        private WebFolder From(DbDataReader r)
        {
            WebFolder item = new WebFolder();
            item.Id = DataUtil.GetId(r["Id"]);
            item.Name = r["Name"].ToString();
            item.ParentId = DataUtil.GetId(r["ParentId"]);
            item.ShareName = r["ShareName"].ToString();
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.SiteId = DataUtil.GetId(r, WebColumns.SiteId);

            return item;
        }

        public WebFolder Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebFolder> GetList()
        {
            List<WebFolder> items = new List<WebFolder>();

            using (var r = DbHelper.ExecuteReader("WebFolder_Get"))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebFolder> GetList(int parentId)
        {
            List<WebFolder> items = new List<WebFolder>();

            using (var r = DbHelper.ExecuteReader("WebFolder_Get",
                DbHelper.CreateParameter("@ParentId", parentId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebFolder> GetList(int objectId, int siteId)
        {
            List<WebFolder> items = new List<WebFolder>();

            using (var r = DbHelper.ExecuteReader("WebFolder_Get",
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@SiteId", siteId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebFolder> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(WebFolder item)
        {
            object obj = DbHelper.ExecuteScalar("WebFolder_Set",
                DbHelper.CreateParameter("@Id", item.Id),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@ParentId", item.ParentId),
                DbHelper.CreateParameter("@ShareName", item.ShareName),
                DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                DbHelper.CreateParameter("@SiteId", item.SiteId));

            item.Id = DataUtil.GetId(obj);
            return item.Id;
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public WebFolder Refresh(WebFolder item)
        {
            throw new NotImplementedException();
        }
    }
}
