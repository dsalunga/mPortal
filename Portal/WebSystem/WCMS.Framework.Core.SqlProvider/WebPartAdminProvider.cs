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
            var item = new WebPartAdmin();
            item.Id = DataUtil.GetId(r["PartAdminId"]);
            item.PartId = DataUtil.GetId(r["PartId"]);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.FileName = r["FileName"].ToString();
            item.ParentId = DataUtil.GetId(r, WebColumns.ParentId);
            item.Active = DataUtil.GetInt32(r, WebColumns.Active);
            item.Visible = DataUtil.GetInt32(r, WebColumns.Visible);
            item.InSiteContext = DataUtil.GetInt32(r, "InSiteContext");
            item.TemplateEngineId = DataUtil.GetInt32(r, "TemplateEngineId");
            item.AutoTitle = DataUtil.GetInt32(r, "AutoTitle");

            return item;
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
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebPartAdmin SET " +
                    DbSyntax.QuoteIdentifier("PartId") + " = @PartId, " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("FileName") + " = @FileName, " +
                    DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId, " +
                    DbSyntax.QuoteIdentifier("Active") + " = @Active, " +
                    DbSyntax.QuoteIdentifier("Visible") + " = @Visible, " +
                    DbSyntax.QuoteIdentifier("InSiteContext") + " = @InSiteContext, " +
                    DbSyntax.QuoteIdentifier("TemplateEngineId") + " = @TemplateEngineId, " +
                    DbSyntax.QuoteIdentifier("AutoTitle") + " = @AutoTitle" +
                    " WHERE " + DbSyntax.QuoteIdentifier("PartAdminId") + " = @PartAdminId";
                parms = new[] {
                    DbHelper.CreateParameter("@PartId", item.PartId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@FileName", item.FileName),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@Active", item.Active),
                    DbHelper.CreateParameter("@Visible", item.Visible),
                    DbHelper.CreateParameter("@InSiteContext", item.InSiteContext),
                    DbHelper.CreateParameter("@TemplateEngineId", item.TemplateEngineId),
                    DbHelper.CreateParameter("@AutoTitle", item.AutoTitle),
                    DbHelper.CreateParameter("@PartAdminId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebPartAdmin (" +
                    DbSyntax.QuoteIdentifier("PartId") + ", " +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("FileName") + ", " +
                    DbSyntax.QuoteIdentifier("ParentId") + ", " +
                    DbSyntax.QuoteIdentifier("Active") + ", " +
                    DbSyntax.QuoteIdentifier("Visible") + ", " +
                    DbSyntax.QuoteIdentifier("InSiteContext") + ", " +
                    DbSyntax.QuoteIdentifier("TemplateEngineId") + ", " +
                    DbSyntax.QuoteIdentifier("AutoTitle") +
                    ") VALUES (@PartId, @Name, @FileName, @ParentId, @Active, @Visible, @InSiteContext, @TemplateEngineId, @AutoTitle)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("PartAdminId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@PartId", item.PartId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@FileName", item.FileName),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@Active", item.Active),
                    DbHelper.CreateParameter("@Visible", item.Visible),
                    DbHelper.CreateParameter("@InSiteContext", item.InSiteContext),
                    DbHelper.CreateParameter("@TemplateEngineId", item.TemplateEngineId),
                    DbHelper.CreateParameter("@AutoTitle", item.AutoTitle)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
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
