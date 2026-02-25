using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

//using Enu = WCMS.Framework.WebObjectHeaderEnum;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebObjectHeaderProvider : IWebObjectHeaderProvider
    {
        public WebObjectHeaderProvider() { }

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public WebObjectHeader Get(int objectHeaderId)
        {
            var sql = "SELECT * FROM WebObjectHeader WHERE " + DbSyntax.QuoteIdentifier("ObjectHeaderId") + " = @ObjectHeaderId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectHeaderId", objectHeaderId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        // This can contain multiple items, so this implementation should be changed
        public WebObjectHeader Get(int objectId, int recordId, int textResourceId)
        {
            var sql = "SELECT * FROM WebObjectHeader WHERE " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId AND " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " + DbSyntax.QuoteIdentifier("TextResourceId") + " = @TextResourceId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@TextResourceId", textResourceId)
                ))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public int Update(WebObjectHeader item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebObjectHeader SET " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId, " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId, " +
                    DbSyntax.QuoteIdentifier("TextResourceId") + " = @TextResourceId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("ObjectHeaderId") + " = @ObjectHeaderId";
                parms = new[] {
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@TextResourceId", item.TextResourceId),
                    DbHelper.CreateParameter("@ObjectHeaderId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebObjectHeader (" +
                    DbSyntax.QuoteIdentifier("ObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("RecordId") + ", " +
                    DbSyntax.QuoteIdentifier("TextResourceId") +
                    ") VALUES (@ObjectId, @RecordId, @TextResourceId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("ObjectHeaderId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@TextResourceId", item.TextResourceId)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        public bool Delete(int objectHeaderId)
        {
            var sql = "DELETE FROM WebObjectHeader WHERE " + DbSyntax.QuoteIdentifier("ObjectHeaderId") + " = @ObjectHeaderId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectHeaderId", objectHeaderId));

            return true;
        }

        public IEnumerable<WebObjectHeader> GetList(int objectId, int recordId)
        {
            List<WebObjectHeader> items = new List<WebObjectHeader>();

            var sql = "SELECT * FROM WebObjectHeader WHERE " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebObjectHeader> GetList(int textResourceId)
        {
            List<WebObjectHeader> items = new List<WebObjectHeader>();

            var sql = "SELECT * FROM WebObjectHeader WHERE " + DbSyntax.QuoteIdentifier("TextResourceId") + " = @TextResourceId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@TextResourceId", textResourceId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebObjectHeader> GetList()
        {
            List<WebObjectHeader> items = new List<WebObjectHeader>();

            var sql = "SELECT * FROM WebObjectHeader";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public WebObjectHeader From(DbDataReader r)
        {
            WebObjectHeader item = new WebObjectHeader();
            item.Id = DataUtil.GetId(r, "ObjectHeaderId");
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataUtil.GetId(r, WebColumns.RecordId);
            item.TextResourceId = DataUtil.GetId(r, WebColumns.TextResourceId);

            return item;
        }

        #region IDataProvider<WebObjectHeader> Members

        public WebObjectHeader Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebObjectHeader> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        #endregion


        public WebObjectHeader Refresh(WebObjectHeader item)
        {
            throw new NotImplementedException();
        }
    }
}
