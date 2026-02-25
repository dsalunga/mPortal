using System;
using System.Data;
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
                using (var r = DbHelper.ExecuteReader("WebGroup_Get",
                    DbHelper.CreateParameter("@Id", id)))
                {
                    if (r.HasRows && r.Read())
                        return this.From(r);
                }
            }

            return null;
        }

        public WebGroup Get(int parentId, string name)
        {
            using (var r = DbHelper.ExecuteReader("WebGroup_Get",
                DbHelper.CreateParameter("@ParentId", parentId),
                DbHelper.CreateParameter("@Name", name)))
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
                using (var r = DbHelper.ExecuteReader("WebGroup_Get",
                    DbHelper.CreateParameter("@Name", name)))
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
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.ParentId = DataUtil.GetId(r, WebColumns.ParentId);
            item.IsSystem = DataUtil.GetInt32(r, "IsSystem");
            item.OwnerId = DataUtil.GetId(r, WebColumns.OwnerId);
            item.JoinApproval = DataUtil.GetInt32(r, "JoinApproval");
            item.JoinAlert = DataUtil.GetInt32(r, "JoinAlert");
            item.PageUrl = DataUtil.Get(r, "PageUrl");
            item.PageId = DataUtil.GetId(r, WebColumns.PageId);
            item.Description = DataUtil.Get(r, WebColumns.Description);
            item.DateModified = DataUtil.GetDateTime(r, WebColumns.DateModified);
            item.Managers = DataUtil.Get(r, "Managers");

            return item;
        }

        public IEnumerable<WebGroup> GetList()
        {
            List<WebGroup> items = new List<WebGroup>();

            using (DbDataReader r = DbHelper.ExecuteReader("WebGroup_Get"))
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

            using (DbDataReader r = DbHelper.ExecuteReader("WebGroup_Get",
                DbHelper.CreateParameter("@ParentId", parentId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public int Update(WebGroup item)
        {
            object o = DbHelper.ExecuteScalar("WebGroup_Set",
                DbHelper.CreateParameter("@Id", item.Id),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@IsSystem", item.IsSystem),
                DbHelper.CreateParameter("@OwnerId", item.OwnerId),
                DbHelper.CreateParameter("@ParentId", item.ParentId),
                DbHelper.CreateParameter("@JoinApproval", item.JoinApproval),
                DbHelper.CreateParameter("@JoinAlert", item.JoinAlert),
                DbHelper.CreateParameter("@PageUrl", item.PageUrl),
                DbHelper.CreateParameter("@PageId", item.PageId),
                DbHelper.CreateParameter("@Description", item.Description),
                DbHelper.CreateParameter("@Managers", item.Managers)
            );

            item.Id = DataUtil.GetId(o);
            return item.Id;
        }

        public bool Delete(int id)
        {
            DbHelper.ExecuteNonQuery("WebGroup_Del",
                DbHelper.CreateParameter("@Id", id)
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
