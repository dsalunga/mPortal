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
    public class WebPartProvider : IWebPartProvider
    {
        public WebPartProvider()
        {

        }

        public WPart Get(int partId)
        {
            var sql = "SELECT * FROM WebPart WHERE " + DbSyntax.QuoteIdentifier("PartId") + " = @PartId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartId", partId)))
            {
                if (r.HasRows && r.Read())
                    return (WPart)r;
            }

            return null;
        }

        public WPart Get(string identity)
        {
            var sql = "SELECT * FROM WebPart WHERE " + DbSyntax.QuoteIdentifier("Identity") + " = @Identity";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Identity", identity)))
            {
                if (r.HasRows && r.Read())
                    return (WPart)r;
            }

            return null;
        }

        public IEnumerable<WPart> GetList()
        {
            List<WPart> items = new List<WPart>();

            var sql = "SELECT * FROM WebPart";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                        items.Add((WPart)r);
                }
            }

            return items;
        }

        public IEnumerable<WPart> GetList(int active = -1)
        {
            List<WPart> items = new List<WPart>();

            var sql = "SELECT * FROM WebPart WHERE " + DbSyntax.QuoteIdentifier("Active") + " = @Active";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Active", active)))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                        items.Add((WPart)r);
                }
            }

            return items;
        }

        #region IWebPartDAL Members


        public int Update(WPart item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebPart SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name" + ", " +
                    DbSyntax.QuoteIdentifier("Identity") + " = @Identity" + ", " +
                    DbSyntax.QuoteIdentifier("Active") + " = @Active" +
                    " WHERE " + DbSyntax.QuoteIdentifier("PartId") + " = @PartId";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Identity", item.Identity),
                    DbHelper.CreateParameter("@Active", item.Active),
                    DbHelper.CreateParameter("@PartId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebPart (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("Identity") + ", " +
                    DbSyntax.QuoteIdentifier("Active") +
                    ") VALUES (@Name, @Identity, @Active)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("PartId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Identity", item.Identity),
                    DbHelper.CreateParameter("@Active", item.Active)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        public bool Delete(int partId)
        {
            var sql = "DELETE FROM WebPart WHERE " + DbSyntax.QuoteIdentifier("PartId") + " = @PartId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@PartId", partId));

            return true;
        }

        #endregion

        #region IDataProvider<WebPart> Members


        public WPart Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WPart> GetList(params QueryFilterElement[] filters)
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


        public WPart Refresh(WPart item)
        {
            throw new NotImplementedException();
        }
    }
}
