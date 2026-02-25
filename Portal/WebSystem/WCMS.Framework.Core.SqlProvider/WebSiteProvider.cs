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
    public class WebSiteProvider : IWebSiteProvider
    {
        public WebSiteProvider() { }

        public IEnumerable<WSite> GetList()
        {
            var items = new List<WSite>();
            using (var r = DbHelper.ExecuteReader("WebSite_Get"))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add((WSite)r);
            }

            return items;
        }

        public IEnumerable<WSite> GetList(int parentId)
        {
            var items = new List<WSite>();

            using (var r = DbHelper.ExecuteReader("WebSite_Get",
                DbHelper.CreateParameter("@ParentId", parentId)))
            {
                while (r.Read())
                    items.Add((WSite)r);
            }

            return items;
        }

        public WSite Get(int siteId)
        {
            using (DbDataReader r = DbHelper.ExecuteReader("WebSite_Get",
                DbHelper.CreateParameter("@SiteId", siteId)))
            {
                if (r.HasRows && r.Read())
                    return (WSite)r;
            }

            return null;
        }

        public WSite Get(string identity)
        {
            using (var r = DbHelper.ExecuteReader("WebSite_Get",
                DbHelper.CreateParameter("@Identity", identity)))
            {
                if(r.Read())
                    return (WSite)r;
            }

            return null;
        }

        public int GetCount()
        {
            object o = DbHelper.ExecuteScalar("WebSite_GetCount");
            if (o != null)
                return Convert.ToInt32(o.ToString());

            return -1;
        }

        public int Update(WSite item)
        {
            object o = DbHelper.ExecuteScalar("WebSite_Set",
                DbHelper.CreateParameter("@SiteId", item.Id),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@Rank", item.Rank),
                DbHelper.CreateParameter("@Active", item.Active),
                DbHelper.CreateParameter("@Identity", item.Identity),
                DbHelper.CreateParameter("@Title", item.Title),
                DbHelper.CreateParameter("@ParentId", item.ParentId),
                DbHelper.CreateParameter("@HomePageId", item.HomePageId),
                DbHelper.CreateParameter("@DefaultMasterPageId", item.DefaultMasterPageId),
                DbHelper.CreateParameter("@HostName", item.HostName),
                DbHelper.CreateParameter("@PublicAccess", item.PublicAccess),
                DbHelper.CreateParameter("@LoginPage", item.LoginPage),
                DbHelper.CreateParameter("@AccessDeniedPage", item.AccessDeniedPage),
                DbHelper.CreateParameter("@PageTitleFormat", item.PageTitleFormat),
                DbHelper.CreateParameter("@ManagementAccess", item.ManagementAccess),
                DbHelper.CreateParameter("@BaseAddress", item.BaseAddress),
                DbHelper.CreateParameter("@ThemeId", item.ThemeId),
                DbHelper.CreateParameter("@SkinId", item.SkinId),
                DbHelper.CreateParameter("@PrimaryIdentityId", item.PrimaryIdentityId)
            );

            item.Id = DataUtil.GetId(o.ToString());
            return item.Id;
        }

        public bool Delete(int siteId)
        {
            DbHelper.ExecuteNonQuery("WebSite_Del",
                DbHelper.CreateParameter("@SiteId", siteId)
            );

            return true;
        }

        public WSite Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WSite> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetMaxRank()
        {
            object result = DbHelper.ExecuteScalar("WebSite_GetMaxRank");
            return DataUtil.GetId(result);
        }

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public WSite Refresh(WSite item)
        {
            throw new NotImplementedException();
        }
    }
}
