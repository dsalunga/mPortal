using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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
            using (var r = SqlHelper.ExecuteReader("WebSite_Get"))
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

            using (var r = SqlHelper.ExecuteReader("WebSite_Get",
                new SqlParameter("@ParentId", parentId)))
            {
                while (r.Read())
                    items.Add((WSite)r);
            }

            return items;
        }

        public WSite Get(int siteId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebSite_Get",
                new SqlParameter("@SiteId", siteId)))
            {
                if (r.HasRows && r.Read())
                    return (WSite)r;
            }

            return null;
        }

        public WSite Get(string identity)
        {
            using (var r = SqlHelper.ExecuteReader("WebSite_Get",
                new SqlParameter("@Identity", identity)))
            {
                if(r.Read())
                    return (WSite)r;
            }

            return null;
        }

        public int GetCount()
        {
            object o = SqlHelper.ExecuteScalar("WebSite_GetCount");
            if (o != null)
                return Convert.ToInt32(o.ToString());

            return -1;
        }

        public int Update(WSite item)
        {
            object o = SqlHelper.ExecuteScalar("WebSite_Set",
                new SqlParameter("@SiteId", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@Rank", item.Rank),
                new SqlParameter("@Active", item.Active),
                new SqlParameter("@Identity", item.Identity),
                new SqlParameter("@Title", item.Title),
                new SqlParameter("@ParentId", item.ParentId),
                new SqlParameter("@HomePageId", item.HomePageId),
                new SqlParameter("@DefaultMasterPageId", item.DefaultMasterPageId),
                new SqlParameter("@HostName", item.HostName),
                new SqlParameter("@PublicAccess", item.PublicAccess),
                new SqlParameter("@LoginPage", item.LoginPage),
                new SqlParameter("@AccessDeniedPage", item.AccessDeniedPage),
                new SqlParameter("@PageTitleFormat", item.PageTitleFormat),
                new SqlParameter("@ManagementAccess", item.ManagementAccess),
                new SqlParameter("@BaseAddress", item.BaseAddress),
                new SqlParameter("@ThemeId", item.ThemeId),
                new SqlParameter("@SkinId", item.SkinId),
                new SqlParameter("@PrimaryIdentityId", item.PrimaryIdentityId)
            );

            item.Id = DataHelper.GetId(o.ToString());
            return item.Id;
        }

        public bool Delete(int siteId)
        {
            SqlHelper.ExecuteNonQuery("WebSite_Del",
                new SqlParameter("@SiteId", siteId)
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
            object result = SqlHelper.ExecuteScalar("WebSite_GetMaxRank");
            return DataHelper.GetId(result);
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
