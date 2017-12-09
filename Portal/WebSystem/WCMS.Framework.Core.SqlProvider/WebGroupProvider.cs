using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebGroupProvider : IWebGroupProvider
    {
        public WebGroup Get(int id)
        {
            if (id > 0)
            {
                using (var r = SqlHelper.ExecuteReader("WebGroup_Get",
                    new SqlParameter("@Id", id)))
                {
                    if (r.HasRows && r.Read())
                        return this.From(r);
                }
            }

            return null;
        }

        public WebGroup Get(int parentId, string name)
        {
            using (var r = SqlHelper.ExecuteReader("WebGroup_Get",
                new SqlParameter("@ParentId", parentId),
                new SqlParameter("@Name", name)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebGroup Get(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                using (var r = SqlHelper.ExecuteReader("WebGroup_Get",
                    new SqlParameter("@Name", name)))
                {
                    if (r.HasRows && r.Read())
                        return this.From(r);
                }
            }

            return null;
        }

        private WebGroup From(DbDataReader r)
        {
            WebGroup item = new WebGroup();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.ParentId = DataHelper.GetId(r, WebColumns.ParentId);
            item.IsSystem = DataHelper.GetInt32(r, "IsSystem");
            item.OwnerId = DataHelper.GetId(r, WebColumns.OwnerId);
            item.JoinApproval = DataHelper.GetInt32(r, "JoinApproval");
            item.JoinAlert = DataHelper.GetInt32(r, "JoinAlert");
            item.PageUrl = DataHelper.Get(r, "PageUrl");
            item.PageId = DataHelper.GetId(r, WebColumns.PageId);
            item.Description = DataHelper.Get(r, WebColumns.Description);
            item.DateModified = DataHelper.GetDateTime(r, WebColumns.DateModified);
            item.Managers = DataHelper.Get(r, "Managers");

            return item;
        }

        public IEnumerable<WebGroup> GetList()
        {
            List<WebGroup> items = new List<WebGroup>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebGroup_Get"))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebGroup> GetList(int parentId)
        {
            List<WebGroup> items = new List<WebGroup>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebGroup_Get",
                new SqlParameter("@ParentId", parentId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public int Update(WebGroup item)
        {
            object o = SqlHelper.ExecuteScalar("WebGroup_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@IsSystem", item.IsSystem),
                new SqlParameter("@OwnerId", item.OwnerId),
                new SqlParameter("@ParentId", item.ParentId),
                new SqlParameter("@JoinApproval", item.JoinApproval),
                new SqlParameter("@JoinAlert", item.JoinAlert),
                new SqlParameter("@PageUrl", item.PageUrl),
                new SqlParameter("@PageId", item.PageId),
                new SqlParameter("@Description", item.Description),
                new SqlParameter("@Managers", item.Managers)
            );

            item.Id = DataHelper.GetId(o);
            return item.Id;
        }

        public bool Delete(int id)
        {
            SqlHelper.ExecuteNonQuery("WebGroup_Del",
                new SqlParameter("@Id", id)
            );

            return true;
        }

        #region IDataProvider<WebGroup> Members


        public WebGroup Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebGroup> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
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


        public WebGroup Refresh(WebGroup item)
        {
            throw new NotImplementedException();
        }
    }
}
