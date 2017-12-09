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
    public class WebPageProvider : IWebPageProvider
    {
        public WebPageProvider() { }

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WPage> GetList(int siteId, int parentId)
        {
            List<WPage> items = new List<WPage>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebPage_Get",
                new SqlParameter("@SiteId", siteId),
                new SqlParameter("@ParentId", parentId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public int GetCount(int siteId)
        {
            object o = SqlHelper.ExecuteScalar("WebPage_GetCount",
                new SqlParameter("@SiteId", siteId));

            if (o != null)
                return Convert.ToInt32(o.ToString());

            return 0;
        }

        public int GetCount()
        {
            return GetCount(-1);
        }

        public IEnumerable<WPage> GetList(int siteId)
        {
            List<WPage> items = new List<WPage>();
            using (DbDataReader r = SqlHelper.ExecuteReader("WebPage_Get",
                new SqlParameter("@SiteId", siteId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public WPage Get(int pageId)
        {
            if (pageId > 0)
            {
                using (DbDataReader r = SqlHelper.ExecuteReader("WebPage_Get",
                    new SqlParameter("@PageId", pageId)))
                {
                    if (r.HasRows && r.Read())
                        return this.From(r);
                }
            }

            return null;
        }

        public WPage Get(int siteId, int parentId, string identity)
        {
            if (!string.IsNullOrEmpty(identity))
            {
                using (var r = SqlHelper.ExecuteReader("WebPage_Get",
                    new SqlParameter("@ParentId", parentId),
                    new SqlParameter("@Identity", identity),
                    new SqlParameter("@SiteId", siteId)))
                {
                    if (r.HasRows && r.Read())
                        return this.From(r);
                }
            }

            return null;
        }

        public IEnumerable<WPage> GetList()
        {
            List<WPage> items = new List<WPage>();
            using (DbDataReader r = SqlHelper.ExecuteReader("WebPage_Get"))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public bool Delete(int pageId)
        {
            if (pageId > 0)
            {
                SqlHelper.ExecuteNonQuery("WebPage_Del",
                    new SqlParameter("@PageId", pageId));

                return true;
            }

            return false;
        }

        public int Update(WPage item)
        {
            object o = SqlHelper.ExecuteScalar("WebPage_Set",
                new SqlParameter("@PageId", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@SiteId", item.SiteId),
                new SqlParameter("@Rank", item.Rank),
                new SqlParameter("@Active", item.Active),
                new SqlParameter("@Identity", item.Identity),
                new SqlParameter("@ParentId", item.ParentId),
                new SqlParameter("@Title", item.Title),
                new SqlParameter("@MasterPageId", item.MasterPageId),
                new SqlParameter("@PartControlTemplateId", item.PartControlTemplateId),
                new SqlParameter("@PublicAccess", item.PublicAccess),
                new SqlParameter("@PageType", item.PageType),
                new SqlParameter("@UsePartTemplatePath", item.UsePartTemplatePath),
                new SqlParameter("@ManagementAccess", item.ManagementAccess),
                new SqlParameter("@ThemeId", item.ThemeId),
                new SqlParameter("@SkinId", item.SkinId)
                );

            item.Id = DataHelper.GetId(o.ToString());

            return item.Id;
        }

        public WPage From(DbDataReader r)
        {
            WPage item = new WPage();
            item.Id = DataHelper.GetId(r, WebColumns.PageId);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.SiteId = DataHelper.GetId(r, WebColumns.SiteId);
            item.Rank = DataHelper.GetInt32(r, WebColumns.Rank);
            item.Active = DataHelper.GetInt32(r, WebColumns.Active);
            item.Identity = DataHelper.Get(r, WebColumns.Identity);
            item.ParentId = DataHelper.GetId(r, WebColumns.ParentId);
            item.Title = DataHelper.Get(r, WebColumns.Title);
            item.MasterPageId = DataHelper.GetInt32(r, WebColumns.MasterPageId);
            item.PartControlTemplateId = DataHelper.GetId(r, WebColumns.PartControlTemplateId);
            item.PublicAccess = DataHelper.GetInt32(r, "PublicAccess");
            item.PageType = DataHelper.GetInt32(r, "PageType");
            item.UsePartTemplatePath = DataHelper.GetInt32(r, "UsePartTemplatePath");
            item.ManagementAccess = DataHelper.GetInt32(r, "ManagementAccess");
            item.SkinId = DataHelper.GetId(r, WebColumns.SkinId);
            item.ThemeId = DataHelper.GetId(r, WebColumns.ThemeId);

            return item;
        }

        public int GetMaxRank(int siteId)
        {
            object result = SqlHelper.ExecuteScalar("WebPage_GetMaxRank",
                new SqlParameter("@SiteId", siteId));

            return DataHelper.GetId(result);
        }

        #region IDataProvider<WebPage> Members

        public WPage Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WPage> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        #endregion

        public WPage Refresh(WPage item)
        {
            throw new NotImplementedException();
        }
    }
}