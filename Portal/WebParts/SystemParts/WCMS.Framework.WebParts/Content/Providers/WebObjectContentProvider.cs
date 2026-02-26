using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Content.Providers
{
    public class WebObjectContentProvider : IWebObjectContentProvider
    {
        public WebObjectContentProvider() { }

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public WebObjectContent Get(int objectContentId)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("WebObjectContent") +
                " WHERE " + DbSyntax.QuoteIdentifier("ObjectContentId") + " = @ObjectContentId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectContentId", objectContentId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebObjectContent GetByObjectId(int objectId, int recordId)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("WebObjectContent") +
                " WHERE " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " +
                DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId)
                ))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<WebObjectContent> GetList(int objectId)
        {
            List<WebObjectContent> items = new List<WebObjectContent>();
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("WebObjectContent") +
                " WHERE " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebObjectContent> GetList()
        {
            List<WebObjectContent> items = new List<WebObjectContent>();
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("WebObjectContent");
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public int Update(WebObjectContent item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("WebObjectContent") + " SET " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId, " +
                    DbSyntax.QuoteIdentifier("ContentId") + " = @ContentId, " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("ObjectContentId") + " = @ObjectContentId";
                parms = new[] {
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@ContentId", item.ContentId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@ObjectContentId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("WebObjectContent") + " (" +
                    DbSyntax.QuoteIdentifier("ObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("ContentId") + ", " +
                    DbSyntax.QuoteIdentifier("RecordId") +
                    ") VALUES (@ObjectId, @ContentId, @RecordId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("ObjectContentId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@ContentId", item.ContentId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId)
                };
                var o = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(o);
            }

            return item.Id;
        }

        public bool Delete(int objectContentId)
        {
            var sql = "DELETE FROM " + DbSyntax.QuoteIdentifier("WebObjectContent") +
                " WHERE " + DbSyntax.QuoteIdentifier("ObjectContentId") + " = @ObjectContentId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectContentId", objectContentId));

            return true;
        }

        public WebObjectContent From(DbDataReader r)
        {
            WebObjectContent item = new WebObjectContent();
            item.Id = DataUtil.GetId(r["ObjectContentId"].ToString());
            item.ContentId = DataUtil.GetId(r["ContentId"].ToString());
            item.ObjectId = DataUtil.GetId(r["ObjectId"].ToString());
            item.RecordId = DataUtil.GetId(r["RecordId"].ToString());

            return item;
        }

        public WebObjectContent From(DataRow r)
        {
            WebObjectContent item = new WebObjectContent();
            item.Id = DataUtil.GetId(r["ObjectContentId"].ToString());
            item.ContentId = DataUtil.GetId(r["ContentId"].ToString());
            item.ObjectId = DataUtil.GetId(r["ObjectId"].ToString());
            item.RecordId = DataUtil.GetId(r["RecordId"].ToString());

            return item;
        }

        #region IDataProvider<WebObjectContent> Members

        public WebObjectContent Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebObjectContent> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        #endregion


        public WebObjectContent Refresh(WebObjectContent item)
        {
            throw new NotImplementedException();
        }
    }
}
