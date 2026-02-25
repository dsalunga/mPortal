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

            using (DbDataReader r = DbHelper.ExecuteReader("WebPage_Get",
                DbHelper.CreateParameter("@SiteId", siteId),
                DbHelper.CreateParameter("@ParentId", parentId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public int GetCount(int siteId)
        {
            object o = DbHelper.ExecuteScalar("WebPage_GetCount",
                DbHelper.CreateParameter("@SiteId", siteId));

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
            using (DbDataReader r = DbHelper.ExecuteReader("WebPage_Get",
                DbHelper.CreateParameter("@SiteId", siteId)))
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
                using (DbDataReader r = DbHelper.ExecuteReader("WebPage_Get",
                    DbHelper.CreateParameter("@PageId", pageId)))
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
                using (var r = DbHelper.ExecuteReader("WebPage_Get",
                    DbHelper.CreateParameter("@ParentId", parentId),
                    DbHelper.CreateParameter("@Identity", identity),
                    DbHelper.CreateParameter("@SiteId", siteId)))
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
            using (DbDataReader r = DbHelper.ExecuteReader("WebPage_Get"))
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
                DbHelper.ExecuteNonQuery("WebPage_Del",
                    DbHelper.CreateParameter("@PageId", pageId));

                return true;
            }

            return false;
        }

        public int Update(WPage item)
        {
            object o = DbHelper.ExecuteScalar("WebPage_Set",
                DbHelper.CreateParameter("@PageId", item.Id),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@SiteId", item.SiteId),
                DbHelper.CreateParameter("@Rank", item.Rank),
                DbHelper.CreateParameter("@Active", item.Active),
                DbHelper.CreateParameter("@Identity", item.Identity),
                DbHelper.CreateParameter("@ParentId", item.ParentId),
                DbHelper.CreateParameter("@Title", item.Title),
                DbHelper.CreateParameter("@MasterPageId", item.MasterPageId),
                DbHelper.CreateParameter("@PartControlTemplateId", item.PartControlTemplateId),
                DbHelper.CreateParameter("@PublicAccess", item.PublicAccess),
                DbHelper.CreateParameter("@PageType", item.PageType),
                DbHelper.CreateParameter("@UsePartTemplatePath", item.UsePartTemplatePath),
                DbHelper.CreateParameter("@ManagementAccess", item.ManagementAccess),
                DbHelper.CreateParameter("@ThemeId", item.ThemeId),
                DbHelper.CreateParameter("@SkinId", item.SkinId)
                );

            item.Id = DataUtil.GetId(o.ToString());

            return item.Id;
        }

        public WPage From(DbDataReader r)
        {
            WPage item = new WPage();
            item.Id = DataUtil.GetId(r, WebColumns.PageId);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.SiteId = DataUtil.GetId(r, WebColumns.SiteId);
            item.Rank = DataUtil.GetInt32(r, WebColumns.Rank);
            item.Active = DataUtil.GetInt32(r, WebColumns.Active);
            item.Identity = DataUtil.Get(r, WebColumns.Identity);
            item.ParentId = DataUtil.GetId(r, WebColumns.ParentId);
            item.Title = DataUtil.Get(r, WebColumns.Title);
            item.MasterPageId = DataUtil.GetInt32(r, WebColumns.MasterPageId);
            item.PartControlTemplateId = DataUtil.GetId(r, WebColumns.PartControlTemplateId);
            item.PublicAccess = DataUtil.GetInt32(r, "PublicAccess");
            item.PageType = DataUtil.GetInt32(r, "PageType");
            item.UsePartTemplatePath = DataUtil.GetInt32(r, "UsePartTemplatePath");
            item.ManagementAccess = DataUtil.GetInt32(r, "ManagementAccess");
            item.SkinId = DataUtil.GetId(r, WebColumns.SkinId);
            item.ThemeId = DataUtil.GetId(r, WebColumns.ThemeId);

            return item;
        }

        public int GetMaxRank(int siteId)
        {
            object result = DbHelper.ExecuteScalar("WebPage_GetMaxRank",
                DbHelper.CreateParameter("@SiteId", siteId));

            return DataUtil.GetId(result);
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