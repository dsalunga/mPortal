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
    public class WebPartAdminProvider : IWebPartAdminProvider
    {
        private static readonly object ColumnCacheSync = new object();
        private static HashSet<string> _tableColumns;

        public WebPartAdminProvider() { }

        public IEnumerable<WebPartAdmin> GetList()
        {
            var items = new List<WebPartAdmin>();
            var sql = "SELECT * FROM WebPartAdmin";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }
            return items;
        }

        public IEnumerable<WebPartAdmin> GetList(int partId)
        {
            var items = new List<WebPartAdmin>();
            var sql = "SELECT * FROM WebPartAdmin WHERE " + DbSyntax.QuoteIdentifier("PartId") + " = @PartId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartId", partId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }
            return items;
        }

        public IEnumerable<WebPartAdmin> GetList(int partId, int parentId)
        {
            var items = new List<WebPartAdmin>();
            var sql = "SELECT * FROM WebPartAdmin WHERE " + DbSyntax.QuoteIdentifier("PartId") + " = @PartId AND " + DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartId", partId),
                DbHelper.CreateParameter("@ParentId", parentId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }
            return items;
        }

        public WebPartAdmin Get(int partAdminId)
        {
            var sql = "SELECT * FROM WebPartAdmin WHERE " + DbSyntax.QuoteIdentifier("PartAdminId") + " = @PartAdminId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartAdminId", partAdminId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }
            return null;
        }

        public WebPartAdmin Get(int partId, string name)
        {
            var sql = "SELECT * FROM WebPartAdmin WHERE " + DbSyntax.QuoteIdentifier("PartId") + " = @PartId AND " + DbSyntax.QuoteIdentifier("Name") + " = @Name";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartId", partId),
                DbHelper.CreateParameter("@Name", name)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }
            return null;
        }

        private WebPartAdmin From(DbDataReader r)
        {
            var hasInSiteContext = HasColumn(r, "InSiteContext");
            var hasTemplateEngineId = HasColumn(r, "TemplateEngineId");
            var hasAutoTitle = HasColumn(r, "AutoTitle");
            var item = new WebPartAdmin();
            item.Id = DataUtil.GetId(r["PartAdminId"]);
            item.PartId = DataUtil.GetId(r["PartId"]);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.FileName = r["FileName"].ToString();
            item.ParentId = DataUtil.GetId(r, WebColumns.ParentId);
            item.Active = DataUtil.GetInt32(r, WebColumns.Active);
            item.Visible = DataUtil.GetInt32(r, WebColumns.Visible);
            if (hasInSiteContext)
                item.InSiteContext = DataUtil.GetInt32(r, "InSiteContext");
            if (hasTemplateEngineId)
                item.TemplateEngineId = DataUtil.GetInt32(r, "TemplateEngineId");
            if (hasAutoTitle)
                item.AutoTitle = DataUtil.GetInt32(r, "AutoTitle");

            return item;
        }

        private static bool HasColumn(DbDataReader reader, string columnName)
        {
            for (var i = 0; i < reader.FieldCount; i++)
            {
                if (string.Equals(reader.GetName(i), columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        private static bool HasTableColumn(string columnName)
        {
            EnsureTableColumns();
            return _tableColumns != null && _tableColumns.Contains(columnName);
        }

        private static void EnsureTableColumns()
        {
            if (_tableColumns != null)
                return;

            lock (ColumnCacheSync)
            {
                if (_tableColumns != null)
                    return;

                var columns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                using (var reader = DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM WebPartAdmin WHERE 1 = 0"))
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                        columns.Add(reader.GetName(i));
                }

                _tableColumns = columns;
            }
        }

        #region IWebPartAdminDAL Members

        public bool Delete(int partAdminId)
        {
            var sql = "DELETE FROM WebPartAdmin WHERE " + DbSyntax.QuoteIdentifier("PartAdminId") + " = @PartAdminId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartAdminId", partAdminId)
            );

            return true;
        }

        public int Update(WebPartAdmin item)
        {
            string sql;
            List<DbParameter> parms;
            var includeInSiteContext = HasTableColumn("InSiteContext");
            var includeTemplateEngineId = HasTableColumn("TemplateEngineId");
            var includeAutoTitle = HasTableColumn("AutoTitle");

            if (item.Id > 0)
            {
                var setParts = new List<string>
                {
                    DbSyntax.QuoteIdentifier("PartId") + " = @PartId",
                    DbSyntax.QuoteIdentifier("Name") + " = @Name",
                    DbSyntax.QuoteIdentifier("FileName") + " = @FileName",
                    DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId",
                    DbSyntax.QuoteIdentifier("Active") + " = @Active",
                    DbSyntax.QuoteIdentifier("Visible") + " = @Visible"
                };
                if (includeInSiteContext)
                    setParts.Add(DbSyntax.QuoteIdentifier("InSiteContext") + " = @InSiteContext");
                if (includeTemplateEngineId)
                    setParts.Add(DbSyntax.QuoteIdentifier("TemplateEngineId") + " = @TemplateEngineId");
                if (includeAutoTitle)
                    setParts.Add(DbSyntax.QuoteIdentifier("AutoTitle") + " = @AutoTitle");

                sql = "UPDATE WebPartAdmin SET " +
                    string.Join(", ", setParts) +
                    " WHERE " + DbSyntax.QuoteIdentifier("PartAdminId") + " = @PartAdminId";
                parms = new List<DbParameter>
                {
                    DbHelper.CreateParameter("@PartId", item.PartId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@FileName", item.FileName),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@Active", item.Active),
                    DbHelper.CreateParameter("@Visible", item.Visible)
                };
                if (includeInSiteContext)
                    parms.Add(DbHelper.CreateParameter("@InSiteContext", item.InSiteContext));
                if (includeTemplateEngineId)
                    parms.Add(DbHelper.CreateParameter("@TemplateEngineId", item.TemplateEngineId));
                if (includeAutoTitle)
                    parms.Add(DbHelper.CreateParameter("@AutoTitle", item.AutoTitle));
                parms.Add(DbHelper.CreateParameter("@PartAdminId", item.Id));
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms.ToArray());
            }
            else
            {
                var insertColumns = new List<string>
                {
                    DbSyntax.QuoteIdentifier("PartId"),
                    DbSyntax.QuoteIdentifier("Name"),
                    DbSyntax.QuoteIdentifier("FileName"),
                    DbSyntax.QuoteIdentifier("ParentId"),
                    DbSyntax.QuoteIdentifier("Active"),
                    DbSyntax.QuoteIdentifier("Visible")
                };
                var insertValues = new List<string>
                {
                    "@PartId",
                    "@Name",
                    "@FileName",
                    "@ParentId",
                    "@Active",
                    "@Visible"
                };
                if (includeInSiteContext)
                {
                    insertColumns.Add(DbSyntax.QuoteIdentifier("InSiteContext"));
                    insertValues.Add("@InSiteContext");
                }
                if (includeTemplateEngineId)
                {
                    insertColumns.Add(DbSyntax.QuoteIdentifier("TemplateEngineId"));
                    insertValues.Add("@TemplateEngineId");
                }
                if (includeAutoTitle)
                {
                    insertColumns.Add(DbSyntax.QuoteIdentifier("AutoTitle"));
                    insertValues.Add("@AutoTitle");
                }

                sql = "INSERT INTO WebPartAdmin (" +
                    string.Join(", ", insertColumns) +
                    ") VALUES (" +
                    string.Join(", ", insertValues) +
                    ")";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("PartAdminId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new List<DbParameter>
                {
                    DbHelper.CreateParameter("@PartId", item.PartId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@FileName", item.FileName),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@Active", item.Active),
                    DbHelper.CreateParameter("@Visible", item.Visible)
                };
                if (includeInSiteContext)
                    parms.Add(DbHelper.CreateParameter("@InSiteContext", item.InSiteContext));
                if (includeTemplateEngineId)
                    parms.Add(DbHelper.CreateParameter("@TemplateEngineId", item.TemplateEngineId));
                if (includeAutoTitle)
                    parms.Add(DbHelper.CreateParameter("@AutoTitle", item.AutoTitle));
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms.ToArray());
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        #endregion

        #region IDataProvider<WebPartAdmin> Members

        public WebPartAdmin Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebPartAdmin> GetList(params QueryFilterElement[] filters)
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


        public WebPartAdmin Refresh(WebPartAdmin item)
        {
            throw new NotImplementedException();
        }
    }
}
