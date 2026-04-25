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
    public class WebTemplateProvider : IWebTemplateProvider
    {
        private static readonly string Table = DbSyntax.QuoteIdentifier("WebTemplate");

        public WebTemplateProvider() { }

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public WebTemplate Get(int templateId)
        {
            var sql = "SELECT * FROM " + Table + " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", templateId)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public WebTemplate From(DbDataReader r)
        {
            WebTemplate item = new WebTemplate();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.FileName = r["FileName"].ToString();
            item.Identity = r["Identity"].ToString();
            item.PrimaryPanelId = DataUtil.GetId(r["PrimaryPanelId"].ToString());
            item.DateModified = (DateTime)r["DateModified"];
            item.SkinId = DataUtil.GetId(r, WebColumns.SkinId);
            item.Content = DataUtil.Get(r, WebColumns.Content);
            item.Standalone = DataUtil.GetInt32(r, "Standalone");
            item.ParentId = DataUtil.GetId(r, WebColumns.ParentId);
            item.ThemeId = DataUtil.GetId(r, WebColumns.ThemeId);
            item.TemplateEngineId = DataUtil.GetInt32(r, "TemplateEngineId");

            return item;
        }

        public IEnumerable<WebTemplate> GetList()
        {
            List<WebTemplate> items = new List<WebTemplate>();

            var sql = "SELECT * FROM " + Table;
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebTemplate> GetList(int themeId = -2)
        {
            var items = new List<WebTemplate>();

            var sql = "SELECT * FROM " + Table + " WHERE " + DbSyntax.QuoteIdentifier("ThemeId") + " = @ThemeId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ThemeId", themeId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public bool Delete(int templateId)
        {
            var sql = "DELETE FROM " + Table + " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", templateId));

            return true;
        }

        public int Update(WebTemplate item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + Table + " SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("Content") + " = @Content, " +
                    DbSyntax.QuoteIdentifier("FileName") + " = @FileName, " +
                    DbSyntax.QuoteIdentifier("Identity") + " = @Identity, " +
                    DbSyntax.QuoteIdentifier("PrimaryPanelId") + " = @PrimaryPanelId, " +
                    DbSyntax.QuoteIdentifier("DateModified") + " = @DateModified, " +
                    DbSyntax.QuoteIdentifier("SkinId") + " = @SkinId, " +
                    DbSyntax.QuoteIdentifier("Standalone") + " = @Standalone, " +
                    DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId, " +
                    DbSyntax.QuoteIdentifier("ThemeId") + " = @ThemeId, " +
                    DbSyntax.QuoteIdentifier("TemplateEngineId") + " = @TemplateEngineId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Content", item.Content),
                    DbHelper.CreateParameter("@FileName", item.FileName),
                    DbHelper.CreateParameter("@Identity", item.Identity),
                    DbHelper.CreateParameter("@PrimaryPanelId", item.PrimaryPanelId),
                    DbHelper.CreateParameter("@DateModified", item.DateModified),
                    DbHelper.CreateParameter("@SkinId", item.SkinId),
                    DbHelper.CreateParameter("@Standalone", item.Standalone),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@ThemeId", item.ThemeId),
                    DbHelper.CreateParameter("@TemplateEngineId", item.TemplateEngineId),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + Table + " (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("Content") + ", " +
                    DbSyntax.QuoteIdentifier("FileName") + ", " +
                    DbSyntax.QuoteIdentifier("Identity") + ", " +
                    DbSyntax.QuoteIdentifier("PrimaryPanelId") + ", " +
                    DbSyntax.QuoteIdentifier("DateModified") + ", " +
                    DbSyntax.QuoteIdentifier("SkinId") + ", " +
                    DbSyntax.QuoteIdentifier("Standalone") + ", " +
                    DbSyntax.QuoteIdentifier("ParentId") + ", " +
                    DbSyntax.QuoteIdentifier("ThemeId") + ", " +
                    DbSyntax.QuoteIdentifier("TemplateEngineId") +
                    ") VALUES (@Name, @Content, @FileName, @Identity, @PrimaryPanelId, @DateModified, @SkinId, @Standalone, @ParentId, @ThemeId, @TemplateEngineId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Content", item.Content),
                    DbHelper.CreateParameter("@FileName", item.FileName),
                    DbHelper.CreateParameter("@Identity", item.Identity),
                    DbHelper.CreateParameter("@PrimaryPanelId", item.PrimaryPanelId),
                    DbHelper.CreateParameter("@DateModified", item.DateModified),
                    DbHelper.CreateParameter("@SkinId", item.SkinId),
                    DbHelper.CreateParameter("@Standalone", item.Standalone),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@ThemeId", item.ThemeId),
                    DbHelper.CreateParameter("@TemplateEngineId", item.TemplateEngineId)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj.ToString());
            }

            return item.Id;
        }

        #region IDataProvider<WebTemplate> Members


        public WebTemplate Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebTemplate> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int UpdateAllFromCache()
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        #endregion


        public WebTemplate Refresh(WebTemplate item)
        {
            throw new NotImplementedException();
        }
    }
}
