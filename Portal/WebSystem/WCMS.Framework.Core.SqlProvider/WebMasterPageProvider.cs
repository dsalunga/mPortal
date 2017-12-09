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
    class WebMasterPageProvider : IWebMasterPageProvider
    {
        public WebMasterPageProvider() { }

        public IEnumerable<WebMasterPage> GetList(int siteId)
        {
            var items = new List<WebMasterPage>();

            using (var r = SqlHelper.ExecuteReader("WebMasterPage_Get",
                new SqlParameter("@SiteId", siteId)))
            {
                while (r.Read())
                    items.Add(this.From(r));
            }

            return items;
        }

        public WebMasterPage Get(int masterPageId)
        {
            using (var r = SqlHelper.ExecuteReader("WebMasterPage_Get",
                new SqlParameter("@MasterPageId", masterPageId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public bool Delete(int masterPageId)
        {
            SqlHelper.ExecuteNonQuery("WebMasterPage_Del",
                new SqlParameter("@MasterPageId", masterPageId));

            return true;
        }

        public int Update(WebMasterPage item)
        {
            object o = SqlHelper.ExecuteScalar("WebMasterPage_Set",
                new SqlParameter("@MasterPageId", item.Id),
                new SqlParameter("@SiteId", item.SiteId),
                new SqlParameter("@TemplateId", item.TemplateId),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@PublicAccess", item.PublicAccess),
                new SqlParameter("@OwnerPageId", item.OwnerPageId),
                new SqlParameter("@ManagementAccess", item.ManagementAccess),
                new SqlParameter("@SkinId", item.SkinId),
                new SqlParameter("@ThemeId", item.ThemeId),
                new SqlParameter("@ParentId", item.ParentId)
            );

            if (o != null)
            {
                item.Id = DataHelper.GetId(o.ToString());
                return item.Id;
            }

            return -1;
        }

        public WebMasterPage From(DbDataReader r)
        {
            var item = new WebMasterPage();
            item.Id = DataHelper.GetId(r["MasterPageId"].ToString());
            item.SiteId = DataHelper.GetId(r[WebColumns.SiteId].ToString());
            item.TemplateId = DataHelper.GetId(r["TemplateId"].ToString());
            item.Name = r["Name"].ToString();
            item.PublicAccess = DataHelper.GetInt32(r["PublicAccess"]);
            item.OwnerPageId = DataHelper.GetInt32(r["OwnerPageId"]);
            item.ManagementAccess = DataHelper.GetInt32(r, "ManagementAccess");
            item.SkinId = DataHelper.GetId(r, WebColumns.SkinId);
            item.ThemeId = DataHelper.GetId(r, WebColumns.ThemeId);
            item.ParentId = DataHelper.GetId(r, WebColumns.ParentId);

            return item;
        }

        #region IDataProvider<WebMasterPage> Members


        public WebMasterPage Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebMasterPage> GetList()
        {
            List<WebMasterPage> items = new List<WebMasterPage>();

            using (var r = SqlHelper.ExecuteReader("WebMasterPage_Get"))
            {
                while (r.Read())
                    items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebMasterPage> GetList(params QueryFilterElement[] filters)
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


        public WebMasterPage Refresh(WebMasterPage item)
        {
            throw new NotImplementedException();
        }
    }
}
