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
    class WebMasterPageProvider : IWebMasterPageProvider
    {
        public WebMasterPageProvider() { }

        public IEnumerable<WebMasterPage> GetList(int siteId)
        {
            var items = new List<WebMasterPage>();

            var sql = "SELECT * FROM WebMasterPage WHERE " + DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@SiteId", siteId)))
            {
                while (r.Read())
                    items.Add(this.From(r));
            }

            return items;
        }

        public WebMasterPage Get(int masterPageId)
        {
            var sql = "SELECT * FROM WebMasterPage WHERE " + DbSyntax.QuoteIdentifier("MasterPageId") + " = @MasterPageId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@MasterPageId", masterPageId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public bool Delete(int masterPageId)
        {
            var sql = "DELETE FROM WebMasterPage WHERE " + DbSyntax.QuoteIdentifier("MasterPageId") + " = @MasterPageId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@MasterPageId", masterPageId));

            return true;
        }

        public int Update(WebMasterPage item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebMasterPage SET " +
                    DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId, " +
                    DbSyntax.QuoteIdentifier("TemplateId") + " = @TemplateId, " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("PublicAccess") + " = @PublicAccess, " +
                    DbSyntax.QuoteIdentifier("OwnerPageId") + " = @OwnerPageId, " +
                    DbSyntax.QuoteIdentifier("ManagementAccess") + " = @ManagementAccess, " +
                    DbSyntax.QuoteIdentifier("SkinId") + " = @SkinId, " +
                    DbSyntax.QuoteIdentifier("ThemeId") + " = @ThemeId, " +
                    DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("MasterPageId") + " = @MasterPageId";
                parms = new[] {
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@TemplateId", item.TemplateId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@PublicAccess", item.PublicAccess),
                    DbHelper.CreateParameter("@OwnerPageId", item.OwnerPageId),
                    DbHelper.CreateParameter("@ManagementAccess", item.ManagementAccess),
                    DbHelper.CreateParameter("@SkinId", item.SkinId),
                    DbHelper.CreateParameter("@ThemeId", item.ThemeId),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@MasterPageId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebMasterPage (" +
                    DbSyntax.QuoteIdentifier("SiteId") + ", " +
                    DbSyntax.QuoteIdentifier("TemplateId") + ", " +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("PublicAccess") + ", " +
                    DbSyntax.QuoteIdentifier("OwnerPageId") + ", " +
                    DbSyntax.QuoteIdentifier("ManagementAccess") + ", " +
                    DbSyntax.QuoteIdentifier("SkinId") + ", " +
                    DbSyntax.QuoteIdentifier("ThemeId") + ", " +
                    DbSyntax.QuoteIdentifier("ParentId") +
                    ") VALUES (@SiteId, @TemplateId, @Name, @PublicAccess, @OwnerPageId, @ManagementAccess, @SkinId, @ThemeId, @ParentId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("MasterPageId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@TemplateId", item.TemplateId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@PublicAccess", item.PublicAccess),
                    DbHelper.CreateParameter("@OwnerPageId", item.OwnerPageId),
                    DbHelper.CreateParameter("@ManagementAccess", item.ManagementAccess),
                    DbHelper.CreateParameter("@SkinId", item.SkinId),
                    DbHelper.CreateParameter("@ThemeId", item.ThemeId),
                    DbHelper.CreateParameter("@ParentId", item.ParentId)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj.ToString());
            }

            return item.Id;
        }

        public WebMasterPage From(DbDataReader r)
        {
            var item = new WebMasterPage();
            item.Id = DataUtil.GetId(r["MasterPageId"].ToString());
            item.SiteId = DataUtil.GetId(r[WebColumns.SiteId].ToString());
            item.TemplateId = DataUtil.GetId(r["TemplateId"].ToString());
            item.Name = r["Name"].ToString();
            item.PublicAccess = DataUtil.GetInt32(r["PublicAccess"]);
            item.OwnerPageId = DataUtil.GetInt32(r["OwnerPageId"]);
            item.ManagementAccess = DataUtil.GetInt32(r, "ManagementAccess");
            item.SkinId = DataUtil.GetId(r, WebColumns.SkinId);
            item.ThemeId = DataUtil.GetId(r, WebColumns.ThemeId);
            item.ParentId = DataUtil.GetId(r, WebColumns.ParentId);

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

            var sql = "SELECT * FROM WebMasterPage";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql))
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
