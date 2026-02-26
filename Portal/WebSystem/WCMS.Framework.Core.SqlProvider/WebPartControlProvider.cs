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
    public class WebPartControlProvider : IWebPartControlProvider
    {
        public WebPartControlProvider() { }

        public WebPartControl Get(int partControlId)
        {
            var sql = "SELECT * FROM WebPartControl WHERE " + DbSyntax.QuoteIdentifier("PartControlId") + " = @PartControlId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartControlId", partControlId)))
            {
                if (r.HasRows && r.Read())
                    return (WebPartControl)r;
            }

            return null;
        }

        public WebPartControl Get(int partId, string identity)
        {
            var sql = "SELECT * FROM WebPartControl WHERE " + DbSyntax.QuoteIdentifier("PartId") + " = @PartId AND " + DbSyntax.QuoteIdentifier("Identity") + " = @Identity";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartId", partId),
                DbHelper.CreateParameter("@Identity", identity)))
            {
                if (r.HasRows && r.Read())
                    return (WebPartControl)r;
            }

            return null;
        }

        public IEnumerable<WebPartControl> GetList(int partId)
        {
            List<WebPartControl> items = new List<WebPartControl>();

            var sql = "SELECT * FROM WebPartControl WHERE " + DbSyntax.QuoteIdentifier("PartId") + " = @PartId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartId", partId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add((WebPartControl)r);
            }

            return items;
        }

        public IEnumerable<WebPartControl> GetListByParentId(int parentId)
        {
            List<WebPartControl> items = new List<WebPartControl>();

            var sql = "SELECT * FROM WebPartControl WHERE " + DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ParentId", parentId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add((WebPartControl)r);
            }

            return items;
        }

        #region IWebPartControlDAL Members

        public int Update(WebPartControl item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebPartControl SET " +
                    DbSyntax.QuoteIdentifier("PartId") + " = @PartId, " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("Identity") + " = @Identity, " +
                    DbSyntax.QuoteIdentifier("ConfigFileName") + " = @ConfigFileName, " +
                    DbSyntax.QuoteIdentifier("PartAdminId") + " = @PartAdminId, " +
                    DbSyntax.QuoteIdentifier("EntryPoint") + " = @EntryPoint, " +
                    DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("PartControlId") + " = @PartControlId";
                parms = new[] {
                    DbHelper.CreateParameter("@PartId", item.PartId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Identity", item.Identity),
                    DbHelper.CreateParameter("@ConfigFileName", item.ConfigFileName),
                    DbHelper.CreateParameter("@PartAdminId", item.PartAdminId),
                    DbHelper.CreateParameter("@EntryPoint", item.EntryPoint),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@PartControlId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebPartControl (" +
                    DbSyntax.QuoteIdentifier("PartId") + ", " +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("Identity") + ", " +
                    DbSyntax.QuoteIdentifier("ConfigFileName") + ", " +
                    DbSyntax.QuoteIdentifier("PartAdminId") + ", " +
                    DbSyntax.QuoteIdentifier("EntryPoint") + ", " +
                    DbSyntax.QuoteIdentifier("ParentId") +
                    ") VALUES (@PartId, @Name, @Identity, @ConfigFileName, @PartAdminId, @EntryPoint, @ParentId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("PartControlId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@PartId", item.PartId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Identity", item.Identity),
                    DbHelper.CreateParameter("@ConfigFileName", item.ConfigFileName),
                    DbHelper.CreateParameter("@PartAdminId", item.PartAdminId),
                    DbHelper.CreateParameter("@EntryPoint", item.EntryPoint),
                    DbHelper.CreateParameter("@ParentId", item.ParentId)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        public bool Delete(int partControlId)
        {
            var sql = "DELETE FROM WebPartControl WHERE " + DbSyntax.QuoteIdentifier("PartControlId") + " = @PartControlId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartControlId", partControlId));

            return true;
        }

        #endregion

        #region IDataProvider<WebPartControl> Members

        public WebPartControl Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebPartControl> GetList()
        {
            List<WebPartControl> items = new List<WebPartControl>();
            var sql = "SELECT * FROM WebPartControl";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add((WebPartControl)r);
            }

            return items;
        }

        public IEnumerable<WebPartControl> GetList(params QueryFilterElement[] filters)
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


        public WebPartControl Refresh(WebPartControl item)
        {
            throw new NotImplementedException();
        }
    }
}
