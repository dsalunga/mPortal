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

            var sql = "SELECT * FROM WebPage WHERE " + DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId AND " + DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
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
            var sql = "SELECT COUNT(1) FROM WebPage WHERE " + DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId";
            object o = DbHelper.ExecuteScalar(CommandType.Text, sql,
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
            var sql = "SELECT * FROM WebPage WHERE " + DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
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
                var sql = "SELECT * FROM WebPage WHERE " + DbSyntax.QuoteIdentifier("PageId") + " = @PageId";
                using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
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
                var sql = "SELECT * FROM WebPage WHERE " + DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId AND " + DbSyntax.QuoteIdentifier("Identity") + " = @Identity AND " + DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
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
            var sql = "SELECT * FROM WebPage";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
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
                var sql = "DELETE FROM WebPage WHERE " + DbSyntax.QuoteIdentifier("PageId") + " = @PageId";
                DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                    DbHelper.CreateParameter("@PageId", pageId));

                return true;
            }

            return false;
        }

        public int Update(WPage item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebPage SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name" + ", " +
                    DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId" + ", " +
                    DbSyntax.QuoteIdentifier("Rank") + " = @Rank" + ", " +
                    DbSyntax.QuoteIdentifier("Active") + " = @Active" + ", " +
                    DbSyntax.QuoteIdentifier("Identity") + " = @Identity" + ", " +
                    DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId" + ", " +
                    DbSyntax.QuoteIdentifier("Title") + " = @Title" + ", " +
                    DbSyntax.QuoteIdentifier("MasterPageId") + " = @MasterPageId" + ", " +
                    DbSyntax.QuoteIdentifier("PartControlTemplateId") + " = @PartControlTemplateId" + ", " +
                    DbSyntax.QuoteIdentifier("PublicAccess") + " = @PublicAccess" + ", " +
                    DbSyntax.QuoteIdentifier("PageType") + " = @PageType" + ", " +
                    DbSyntax.QuoteIdentifier("UsePartTemplatePath") + " = @UsePartTemplatePath" + ", " +
                    DbSyntax.QuoteIdentifier("ManagementAccess") + " = @ManagementAccess" + ", " +
                    DbSyntax.QuoteIdentifier("ThemeId") + " = @ThemeId" + ", " +
                    DbSyntax.QuoteIdentifier("SkinId") + " = @SkinId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("PageId") + " = @PageId";
                parms = new[] {
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
                    DbHelper.CreateParameter("@SkinId", item.SkinId),
                    DbHelper.CreateParameter("@PageId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebPage (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("SiteId") + ", " +
                    DbSyntax.QuoteIdentifier("Rank") + ", " +
                    DbSyntax.QuoteIdentifier("Active") + ", " +
                    DbSyntax.QuoteIdentifier("Identity") + ", " +
                    DbSyntax.QuoteIdentifier("ParentId") + ", " +
                    DbSyntax.QuoteIdentifier("Title") + ", " +
                    DbSyntax.QuoteIdentifier("MasterPageId") + ", " +
                    DbSyntax.QuoteIdentifier("PartControlTemplateId") + ", " +
                    DbSyntax.QuoteIdentifier("PublicAccess") + ", " +
                    DbSyntax.QuoteIdentifier("PageType") + ", " +
                    DbSyntax.QuoteIdentifier("UsePartTemplatePath") + ", " +
                    DbSyntax.QuoteIdentifier("ManagementAccess") + ", " +
                    DbSyntax.QuoteIdentifier("ThemeId") + ", " +
                    DbSyntax.QuoteIdentifier("SkinId") +
                    ") VALUES (@Name, @SiteId, @Rank, @Active, @Identity, @ParentId, @Title, @MasterPageId, @PartControlTemplateId, @PublicAccess, @PageType, @UsePartTemplatePath, @ManagementAccess, @ThemeId, @SkinId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("PageId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
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
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj.ToString());
            }

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
            var sql = "SELECT MAX(" + DbSyntax.QuoteIdentifier("Rank") + ") FROM WebPage WHERE " + DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId";
            object result = DbHelper.ExecuteScalar(CommandType.Text, sql,
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