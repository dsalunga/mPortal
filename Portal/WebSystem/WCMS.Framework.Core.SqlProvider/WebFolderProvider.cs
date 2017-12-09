using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebFolderProvider : IWebFolderProvider
    {
        #region IDataProvider<WebFolder> Members

        public bool Delete(int id)
        {
            SqlHelper.ExecuteNonQuery("WebFolder_Del",
                new SqlParameter("@Id", id));

            return true;
        }

        public WebFolder Get(int id)
        {
            using (var r = SqlHelper.ExecuteReader("WebFolder_Get",
                new SqlParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public WebFolder Get(int parentId, string name)
        {
            using (var r = SqlHelper.ExecuteReader("WebFolder_Get",
                new SqlParameter("@ParentId", parentId),
                new SqlParameter("@Name", name)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        private WebFolder From(SqlDataReader r)
        {
            WebFolder item = new WebFolder();
            item.Id = DataHelper.GetId(r["Id"]);
            item.Name = r["Name"].ToString();
            item.ParentId = DataHelper.GetId(r["ParentId"]);
            item.ShareName = r["ShareName"].ToString();
            item.ObjectId = DataHelper.GetId(r, WebColumns.ObjectId);
            item.SiteId = DataHelper.GetId(r, WebColumns.SiteId);

            return item;
        }

        public WebFolder Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebFolder> GetList()
        {
            List<WebFolder> items = new List<WebFolder>();

            using (var r = SqlHelper.ExecuteReader("WebFolder_Get"))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebFolder> GetList(int parentId)
        {
            List<WebFolder> items = new List<WebFolder>();

            using (var r = SqlHelper.ExecuteReader("WebFolder_Get",
                new SqlParameter("@ParentId", parentId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebFolder> GetList(int objectId, int siteId)
        {
            List<WebFolder> items = new List<WebFolder>();

            using (var r = SqlHelper.ExecuteReader("WebFolder_Get",
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@SiteId", siteId)))
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
            object obj = SqlHelper.ExecuteScalar("WebFolder_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@ParentId", item.ParentId),
                new SqlParameter("@ShareName", item.ShareName),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@SiteId", item.SiteId));

            item.Id = DataHelper.GetId(obj);
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
