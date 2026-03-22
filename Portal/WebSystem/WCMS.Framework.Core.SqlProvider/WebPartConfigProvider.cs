using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

#pragma warning disable CS0612 // WebPartConfig is intentionally [Obsolete] but still used by this provider
namespace WCMS.Framework.Core.SqlProvider
{
    public class WebPartConfigProvider : IWebPartConfigProvider
    {
        public WebPartConfigProvider() { }

        public IEnumerable<WebPartConfig> GetList()
        {
            List<WebPartConfig> items = new List<WebPartConfig>();

            var sql = "SELECT * FROM WebPartConfig";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public WebPartConfig Get(int partConfigId)
        {
            var sql = "SELECT * FROM WebPartConfig WHERE " + DbSyntax.QuoteIdentifier("PartConfigId") + " = @PartConfigId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartConfigId", partConfigId)
                ))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<WebPartConfig> GetList(int partId)
        {
            List<WebPartConfig> items = new List<WebPartConfig>();

            var sql = "SELECT * FROM WebPartConfig WHERE " + DbSyntax.QuoteIdentifier("PartId") + " = @PartId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartId", partId)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        private WebPartConfig From(DbDataReader r)
        {
            WebPartConfig item = new WebPartConfig();
            item.Id = DataUtil.GetId(r["PartConfigId"]);
            item.PartId = DataUtil.GetId(r["PartId"]);
            item.Name = r["Name"].ToString();
            item.FileName = r["FileName"].ToString();

            return item;
        }

        #region IWebPartConfigDAL Members


        public int Update(WebPartConfig item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebPartConfig SET " +
                    DbSyntax.QuoteIdentifier("PartId") + " = @PartId, " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("FileName") + " = @FileName" +
                    " WHERE " + DbSyntax.QuoteIdentifier("PartConfigId") + " = @PartConfigId";
                parms = new[] {
                    DbHelper.CreateParameter("@PartId", item.PartId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@FileName", item.FileName),
                    DbHelper.CreateParameter("@PartConfigId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebPartConfig (" +
                    DbSyntax.QuoteIdentifier("PartId") + ", " +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("FileName") +
                    ") VALUES (@PartId, @Name, @FileName)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("PartConfigId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@PartId", item.PartId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@FileName", item.FileName)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        public bool Delete(int partConfigId)
        {
            var sql = "DELETE FROM WebPartConfig WHERE " + DbSyntax.QuoteIdentifier("PartConfigId") + " = @PartConfigId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartConfigId", partConfigId));

            return true;
        }

        #endregion

        #region IDataProvider<WebPartConfig> Members


        public WebPartConfig Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebPartConfig> GetList(params QueryFilterElement[] filters)
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


        public WebPartConfig Refresh(WebPartConfig item)
        {
            throw new NotImplementedException();
        }
    }
}
#pragma warning restore CS0612
