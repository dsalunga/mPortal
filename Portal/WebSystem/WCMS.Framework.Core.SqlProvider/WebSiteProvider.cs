using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

#pragma warning disable CS0612 // WSite.AccessDeniedPage is [Obsolete] but still needed for SQL parameter binding
namespace WCMS.Framework.Core.SqlProvider
{
    public class WebSiteProvider : IWebSiteProvider
    {
        public WebSiteProvider() { }

        public IEnumerable<WSite> GetList()
        {
            var items = new List<WSite>();
            var sql = "SELECT * FROM WebSite";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql))
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

            var sql = "SELECT * FROM WebSite WHERE " + DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ParentId", parentId)))
            {
                while (r.Read())
                    items.Add((WSite)r);
            }

            return items;
        }

        public WSite Get(int siteId)
        {
            var sql = "SELECT * FROM WebSite WHERE " + DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@SiteId", siteId)))
            {
                if (r.HasRows && r.Read())
                    return (WSite)r;
            }

            return null;
        }

        public WSite Get(string identity)
        {
            var sql = "SELECT * FROM WebSite WHERE " + DbSyntax.QuoteIdentifier("Identity") + " = @Identity";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Identity", identity)))
            {
                if(r.Read())
                    return (WSite)r;
            }

            return null;
        }

        public int GetCount()
        {
            var sql = "SELECT COUNT(1) FROM WebSite";
            object o = DbHelper.ExecuteScalar(CommandType.Text, sql);
            if (o != null)
                return Convert.ToInt32(o.ToString());

            return -1;
        }

        public int Update(WSite item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebSite SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name" + ", " +
                    DbSyntax.QuoteIdentifier("Rank") + " = @Rank" + ", " +
                    DbSyntax.QuoteIdentifier("Active") + " = @Active" + ", " +
                    DbSyntax.QuoteIdentifier("Identity") + " = @Identity" + ", " +
                    DbSyntax.QuoteIdentifier("Title") + " = @Title" + ", " +
                    DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId" + ", " +
                    DbSyntax.QuoteIdentifier("HomePageId") + " = @HomePageId" + ", " +
                    DbSyntax.QuoteIdentifier("DefaultMasterPageId") + " = @DefaultMasterPageId" + ", " +
                    DbSyntax.QuoteIdentifier("HostName") + " = @HostName" + ", " +
                    DbSyntax.QuoteIdentifier("PublicAccess") + " = @PublicAccess" + ", " +
                    DbSyntax.QuoteIdentifier("LoginPage") + " = @LoginPage" + ", " +
                    DbSyntax.QuoteIdentifier("AccessDeniedPage") + " = @AccessDeniedPage" + ", " +
                    DbSyntax.QuoteIdentifier("PageTitleFormat") + " = @PageTitleFormat" + ", " +
                    DbSyntax.QuoteIdentifier("ManagementAccess") + " = @ManagementAccess" + ", " +
                    DbSyntax.QuoteIdentifier("BaseAddress") + " = @BaseAddress" + ", " +
                    DbSyntax.QuoteIdentifier("ThemeId") + " = @ThemeId" + ", " +
                    DbSyntax.QuoteIdentifier("SkinId") + " = @SkinId" + ", " +
                    DbSyntax.QuoteIdentifier("PrimaryIdentityId") + " = @PrimaryIdentityId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId";
                parms = new[] {
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
                    DbHelper.CreateParameter("@PrimaryIdentityId", item.PrimaryIdentityId),
                    DbHelper.CreateParameter("@SiteId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebSite (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("Rank") + ", " +
                    DbSyntax.QuoteIdentifier("Active") + ", " +
                    DbSyntax.QuoteIdentifier("Identity") + ", " +
                    DbSyntax.QuoteIdentifier("Title") + ", " +
                    DbSyntax.QuoteIdentifier("ParentId") + ", " +
                    DbSyntax.QuoteIdentifier("HomePageId") + ", " +
                    DbSyntax.QuoteIdentifier("DefaultMasterPageId") + ", " +
                    DbSyntax.QuoteIdentifier("HostName") + ", " +
                    DbSyntax.QuoteIdentifier("PublicAccess") + ", " +
                    DbSyntax.QuoteIdentifier("LoginPage") + ", " +
                    DbSyntax.QuoteIdentifier("AccessDeniedPage") + ", " +
                    DbSyntax.QuoteIdentifier("PageTitleFormat") + ", " +
                    DbSyntax.QuoteIdentifier("ManagementAccess") + ", " +
                    DbSyntax.QuoteIdentifier("BaseAddress") + ", " +
                    DbSyntax.QuoteIdentifier("ThemeId") + ", " +
                    DbSyntax.QuoteIdentifier("SkinId") + ", " +
                    DbSyntax.QuoteIdentifier("PrimaryIdentityId") +
                    ") VALUES (@Name, @Rank, @Active, @Identity, @Title, @ParentId, @HomePageId, @DefaultMasterPageId, @HostName, @PublicAccess, @LoginPage, @AccessDeniedPage, @PageTitleFormat, @ManagementAccess, @BaseAddress, @ThemeId, @SkinId, @PrimaryIdentityId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("SiteId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
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
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj.ToString());
            }

            return item.Id;
        }

        public bool Delete(int siteId)
        {
            var sql = "DELETE FROM WebSite WHERE " + DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
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
            var sql = "SELECT MAX(" + DbSyntax.QuoteIdentifier("Rank") + ") FROM WebSite";
            object result = DbHelper.ExecuteScalar(CommandType.Text, sql);
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
#pragma warning restore CS0612
