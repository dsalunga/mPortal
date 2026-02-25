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
    public class WebPartControlTemplateProvider : IWebPartControlTemplateProvider
    {
        public WebPartControlTemplateProvider() { }

        public WebPartControlTemplate Get(int partControlTemplateId)
        {
            var sql = "SELECT * FROM WebPartControlTemplate WHERE " + DbSyntax.QuoteIdentifier("PartControlTemplateId") + " = @PartControlTemplateId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartControlTemplateId", partControlTemplateId)))
            {
                if (r.HasRows && r.Read())
                    return From(r);
            }

            return null;
        }

        public WebPartControlTemplate Get(int partControlId, string identity)
        {
            var sql = "SELECT * FROM WebPartControlTemplate WHERE " + DbSyntax.QuoteIdentifier("PartControlId") + " = @PartControlId AND " + DbSyntax.QuoteIdentifier("Identity") + " = @Identity";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartControlId", partControlId),
                DbHelper.CreateParameter("@Identity", identity)))
            {
                if (r.HasRows && r.Read())
                    return From(r);
            }

            return null;
        }

        public IEnumerable<WebPartControlTemplate> GetList(int partControlId)
        {
            List<WebPartControlTemplate> items = new List<WebPartControlTemplate>();

            var sql = "SELECT * FROM WebPartControlTemplate WHERE " + DbSyntax.QuoteIdentifier("PartControlId") + " = @PartControlId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartControlId", partControlId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(From(r));
            }

            return items;
        }

        public static WebPartControlTemplate From(DbDataReader r)
        {
            WebPartControlTemplate item = new WebPartControlTemplate();
            item.Id = DataUtil.GetId(r, "PartControlTemplateId");
            item.PartControlId = DataUtil.GetId(r, "PartControlId");
            item.Name = r["Name"].ToString();
            item.FileName = r["FileName"].ToString();
            item.Identity = r["Identity"].ToString();
            item.Path = r["Path"].ToString();
            item.Standalone = DataUtil.GetInt32(r, "Standalone");
            item.TemplateEngineId = DataUtil.GetInt32(r, "TemplateEngineId");

            return item;
        }

        public int Update(WebPartControlTemplate item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebPartControlTemplate SET " +
                    DbSyntax.QuoteIdentifier("PartControlId") + " = @PartControlId, " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("FileName") + " = @FileName, " +
                    DbSyntax.QuoteIdentifier("Identity") + " = @Identity, " +
                    DbSyntax.QuoteIdentifier("Path") + " = @Path, " +
                    DbSyntax.QuoteIdentifier("Standalone") + " = @Standalone, " +
                    DbSyntax.QuoteIdentifier("TemplateEngineId") + " = @TemplateEngineId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("PartControlTemplateId") + " = @PartControlTemplateId";
                parms = new[] {
                    DbHelper.CreateParameter("@PartControlId", item.PartControlId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@FileName", item.FileName),
                    DbHelper.CreateParameter("@Identity", item.Identity),
                    DbHelper.CreateParameter("@Path", item.Path),
                    DbHelper.CreateParameter("@Standalone", item.Standalone),
                    DbHelper.CreateParameter("@TemplateEngineId", item.TemplateEngineId),
                    DbHelper.CreateParameter("@PartControlTemplateId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebPartControlTemplate (" +
                    DbSyntax.QuoteIdentifier("PartControlId") + ", " +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("FileName") + ", " +
                    DbSyntax.QuoteIdentifier("Identity") + ", " +
                    DbSyntax.QuoteIdentifier("Path") + ", " +
                    DbSyntax.QuoteIdentifier("Standalone") + ", " +
                    DbSyntax.QuoteIdentifier("TemplateEngineId") +
                    ") VALUES (@PartControlId, @Name, @FileName, @Identity, @Path, @Standalone, @TemplateEngineId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("PartControlTemplateId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@PartControlId", item.PartControlId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@FileName", item.FileName),
                    DbHelper.CreateParameter("@Identity", item.Identity),
                    DbHelper.CreateParameter("@Path", item.Path),
                    DbHelper.CreateParameter("@Standalone", item.Standalone),
                    DbHelper.CreateParameter("@TemplateEngineId", item.TemplateEngineId)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        public bool Delete(int partControlTemplateId)
        {
            var sql = "DELETE FROM WebPartControlTemplate WHERE " + DbSyntax.QuoteIdentifier("PartControlTemplateId") + " = @PartControlTemplateId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartControlTemplateId", partControlTemplateId)
            );

            return true;
        }

        public WebPartControlTemplate Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebPartControlTemplate> GetList()
        {
            List<WebPartControlTemplate> items = new List<WebPartControlTemplate>();

            var sql = "SELECT * FROM WebPartControlTemplate";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        items.Add(From(r));
                    }
                }
            }

            return items;
        }

        public IEnumerable<WebPartControlTemplate> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public WebPartControlTemplate Refresh(WebPartControlTemplate item)
        {
            throw new NotImplementedException();
        }
    }
}
