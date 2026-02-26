using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public class FileIdentityProvider : IFileIdentityProvider
    {
        #region IDataProvider<FileIdentity> Members

        public bool Delete(int id)
        {
            var sql = "DELETE FROM " + DbSyntax.QuoteIdentifier("FileIdentity") +
                " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id));

            return true;
        }

        public FileIdentity Get(int id)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("FileIdentity") +
                " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public FileIdentity Get(string filePath, string name, int objectId, int recordId)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("FileIdentity") +
                " WHERE " + DbSyntax.QuoteIdentifier("FilePath") + " = @FilePath" +
                " AND " + DbSyntax.QuoteIdentifier("Name") + " = @Name" +
                " AND " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId" +
                " AND " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@FilePath", filePath),
                DbHelper.CreateParameter("@Name", name),
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        private FileIdentity From(IDataReader r)
        {
            FileIdentity item = new FileIdentity();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataUtil.GetId(r, WebColumns.RecordId);
            item.LibraryId = DataUtil.GetId(r, "LibraryId");
            item.FilePath = DataUtil.Get(r, "FilePath");
            item.Name = DataUtil.Get(r, WebColumns.Name);

            return item;
        }

        public FileIdentity Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileIdentity> GetList()
        {
            List<FileIdentity> items = new List<FileIdentity>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("FileIdentity");
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (r.Read())
                {
                    items.Add(From(r));
                }
            }

            return items;
        }

        public IEnumerable<FileIdentity> GetList(string filePath, int objectId, int recordId)
        {
            List<FileIdentity> items = new List<FileIdentity>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("FileIdentity") +
                " WHERE " + DbSyntax.QuoteIdentifier("FilePath") + " = @FilePath" +
                " AND " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId" +
                " AND " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@FilePath", filePath),
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId)))
            {
                while (r.Read())
                {
                    items.Add(From(r));
                }
            }

            return items;
        }

        public IEnumerable<FileIdentity> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(FileIdentity item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("FileIdentity") + " SET " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId, " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId, " +
                    DbSyntax.QuoteIdentifier("LibraryId") + " = @LibraryId, " +
                    DbSyntax.QuoteIdentifier("FilePath") + " = @FilePath, " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@LibraryId", item.LibraryId),
                    DbHelper.CreateParameter("@FilePath", item.FilePath),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("FileIdentity") + " (" +
                    DbSyntax.QuoteIdentifier("ObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("RecordId") + ", " +
                    DbSyntax.QuoteIdentifier("LibraryId") + ", " +
                    DbSyntax.QuoteIdentifier("FilePath") + ", " +
                    DbSyntax.QuoteIdentifier("Name") +
                    ") VALUES (@ObjectId, @RecordId, @LibraryId, @FilePath, @Name)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@LibraryId", item.LibraryId),
                    DbHelper.CreateParameter("@FilePath", item.FilePath),
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


        public FileIdentity Refresh(FileIdentity item)
        {
            throw new NotImplementedException();
        }
    }
}
