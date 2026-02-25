using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebFileProvider : IWebFileProvider
    {
        #region IDataProvider<WebFile> Members

        public bool Delete(int id)
        {
            if (id > 0)
            {
                var sql = "DELETE FROM WebFile WHERE " + DbSyntax.QuoteIdentifier("FileId") + " = @FileId";
                DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                    DbHelper.CreateParameter("@FileId", id));
            }

            return true;
        }

        public WebFile Get(int id)
        {
            if (id > 0)
            {
                var sql = "SELECT * FROM WebFile WHERE " + DbSyntax.QuoteIdentifier("FileId") + " = @FileId";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                    DbHelper.CreateParameter("@FileId", id)))
                {
                    if (r.Read())
                        return From(r);
                }
            }

            return null;
        }

        public WebFile Get(int folderId, int objectId, int recordId)
        {
            if (folderId > 0 && objectId > 0 && recordId > 0)
            {
                var sql = "SELECT * FROM WebFile WHERE " + DbSyntax.QuoteIdentifier("FolderId") + " = @FolderId AND " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                    DbHelper.CreateParameter("@FolderId", folderId),
                    DbHelper.CreateParameter("@ObjectId", objectId),
                    DbHelper.CreateParameter("@RecordId", recordId)))
                {
                    if (r.Read())
                        return From(r);
                }
            }

            return null;
        }

        public WebFile Get(int objectId, int recordId)
        {
            if (objectId > 0 && recordId > 0)
            {
                var sql = "SELECT * FROM WebFile WHERE " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                    DbHelper.CreateParameter("@ObjectId", objectId),
                    DbHelper.CreateParameter("@RecordId", recordId)))
                {
                    if (r.Read())
                        return From(r);
                }
            }

            return null;
        }

        private WebFile From(DbDataReader r)
        {
            WebFile item = new WebFile();
            item.Id = DataUtil.GetId(r, "FileId");
            item.FolderId = DataUtil.GetId(r["FolderId"]);
            item.ObjectId = DataUtil.GetId(r["ObjectId"]);
            item.RecordId = DataUtil.GetId(r["RecordId"]);
            item.Name = r["Name"].ToString();

            return item;
        }

        public WebFile Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebFile> GetList()
        {
            List<WebFile> items = new List<WebFile>();
            var sql = "SELECT * FROM WebFile";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebFile> GetList(int folderId)
        {
            List<WebFile> items = new List<WebFile>();
            var sql = "SELECT * FROM WebFile WHERE " + DbSyntax.QuoteIdentifier("FolderId") + " = @FolderId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@FolderId", folderId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebFile> GetList(int objectId, int recordId)
        {
            List<WebFile> items = new List<WebFile>();
            var sql = "SELECT * FROM WebFile WHERE " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebFile> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(WebFile item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebFile SET " +
                    DbSyntax.QuoteIdentifier("FolderId") + " = @FolderId, " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId, " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId, " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name" +
                    " WHERE " + DbSyntax.QuoteIdentifier("FileId") + " = @FileId";
                parms = new[] {
                    DbHelper.CreateParameter("@FolderId", item.FolderId),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@FileId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebFile (" +
                    DbSyntax.QuoteIdentifier("FolderId") + ", " +
                    DbSyntax.QuoteIdentifier("ObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("RecordId") + ", " +
                    DbSyntax.QuoteIdentifier("Name") +
                    ") VALUES (@FolderId, @ObjectId, @RecordId, @Name)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("FileId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@FolderId", item.FolderId),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@Name", item.Name)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public WebFile Refresh(WebFile item)
        {
            throw new NotImplementedException();
        }
    }
}
