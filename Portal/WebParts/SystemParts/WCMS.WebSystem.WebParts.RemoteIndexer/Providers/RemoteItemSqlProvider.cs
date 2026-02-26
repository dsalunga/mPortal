using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.RemoteIndexer.Providers
{
    public class RemoteItemSqlProvider : IRemoteItemProvider
    {
        public IEnumerable<RemoteItem> GetList(int libraryId, int parentId = -2)
        {
            var items = new List<RemoteItem>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("RemoteItem") +
                " WHERE " + DbSyntax.QuoteIdentifier("LibraryId") + " = @LibraryId";
            var parms = new List<DbParameter>();
            parms.Add(DbHelper.CreateParameter("@LibraryId", libraryId));
            if (parentId != -2)
            {
                sql += " AND " + DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId";
                parms.Add(DbHelper.CreateParameter("@ParentId", parentId));
            }

            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql, parms.ToArray()))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        private RemoteItem From(IDataReader r)
        {
            var item = new RemoteItem();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.LibraryId = DataUtil.GetId(r, "LibraryId");
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.RelativePath = DataUtil.Get(r, "RelativePath");
            item.TypeId = DataUtil.GetInt32(r, "TypeId");
            item.DateModified = DataUtil.GetDateTime(r, "DateModified");
            item.Size = DataUtil.GetInt64(r, "Size");
            item.Content = DataUtil.Get(r, "Content");
            item.ParentId = DataUtil.GetId(r, WebColumns.ParentId);
            item.DownloadCount = DataUtil.GetInt32(r, "DownloadCount");
            item.DisplayName = DataUtil.Get(r, "DisplayName");
            item.IndexDateModified = DataUtil.GetDateTime(r, "IndexDateModified");
            item.FileCacheEnabled = DataUtil.GetInt32(r, "FileCacheEnabled");
            item.Cached = DataUtil.GetInt32(r, "Cached");

            return item;
        }

        public bool Delete(int id)
        {
            var sql = "DELETE FROM " + DbSyntax.QuoteIdentifier("RemoteItem") +
                " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id));

            return true;
        }

        public RemoteItem Get(int id)
        {
            if (id > 0)
            {
                var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("RemoteItem") +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                    DbHelper.CreateParameter("@Id", id)))
                {
                    if (r.Read())
                        return From(r);
                }
            }

            return null;
        }

        public RemoteItem Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RemoteItem> GetList()
        {
            var items = new List<RemoteItem>();
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("RemoteItem");
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<RemoteItem> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(RemoteItem item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("RemoteItem") + " SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("LibraryId") + " = @LibraryId, " +
                    DbSyntax.QuoteIdentifier("RelativePath") + " = @RelativePath, " +
                    DbSyntax.QuoteIdentifier("TypeId") + " = @TypeId, " +
                    DbSyntax.QuoteIdentifier("DateModified") + " = @DateModified, " +
                    DbSyntax.QuoteIdentifier("Size") + " = @Size, " +
                    DbSyntax.QuoteIdentifier("Content") + " = @Content, " +
                    DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId, " +
                    DbSyntax.QuoteIdentifier("DownloadCount") + " = @DownloadCount, " +
                    DbSyntax.QuoteIdentifier("DisplayName") + " = @DisplayName, " +
                    DbSyntax.QuoteIdentifier("IndexDateModified") + " = @IndexDateModified, " +
                    DbSyntax.QuoteIdentifier("FileCacheEnabled") + " = @FileCacheEnabled, " +
                    DbSyntax.QuoteIdentifier("Cached") + " = @Cached" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@LibraryId", item.LibraryId),
                    DbHelper.CreateParameter("@RelativePath", item.RelativePath),
                    DbHelper.CreateParameter("@TypeId", item.TypeId),
                    DbHelper.CreateParameter("@DateModified", item.DateModified),
                    DbHelper.CreateParameter("@Size", item.Size),
                    DbHelper.CreateParameter("@Content", item.Content),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@DownloadCount", item.DownloadCount),
                    DbHelper.CreateParameter("@DisplayName", item.DisplayName),
                    DbHelper.CreateParameter("@IndexDateModified", item.IndexDateModified),
                    DbHelper.CreateParameter("@FileCacheEnabled", item.FileCacheEnabled),
                    DbHelper.CreateParameter("@Cached", item.Cached),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("RemoteItem") + " (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("LibraryId") + ", " +
                    DbSyntax.QuoteIdentifier("RelativePath") + ", " +
                    DbSyntax.QuoteIdentifier("TypeId") + ", " +
                    DbSyntax.QuoteIdentifier("DateModified") + ", " +
                    DbSyntax.QuoteIdentifier("Size") + ", " +
                    DbSyntax.QuoteIdentifier("Content") + ", " +
                    DbSyntax.QuoteIdentifier("ParentId") + ", " +
                    DbSyntax.QuoteIdentifier("DownloadCount") + ", " +
                    DbSyntax.QuoteIdentifier("DisplayName") + ", " +
                    DbSyntax.QuoteIdentifier("IndexDateModified") + ", " +
                    DbSyntax.QuoteIdentifier("FileCacheEnabled") + ", " +
                    DbSyntax.QuoteIdentifier("Cached") +
                    ") VALUES (@Name, @LibraryId, @RelativePath, @TypeId, @DateModified, @Size, @Content, @ParentId, @DownloadCount, @DisplayName, @IndexDateModified, @FileCacheEnabled, @Cached)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@LibraryId", item.LibraryId),
                    DbHelper.CreateParameter("@RelativePath", item.RelativePath),
                    DbHelper.CreateParameter("@TypeId", item.TypeId),
                    DbHelper.CreateParameter("@DateModified", item.DateModified),
                    DbHelper.CreateParameter("@Size", item.Size),
                    DbHelper.CreateParameter("@Content", item.Content),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@DownloadCount", item.DownloadCount),
                    DbHelper.CreateParameter("@DisplayName", item.DisplayName),
                    DbHelper.CreateParameter("@IndexDateModified", item.IndexDateModified),
                    DbHelper.CreateParameter("@FileCacheEnabled", item.FileCacheEnabled),
                    DbHelper.CreateParameter("@Cached", item.Cached)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public RemoteItem Refresh(RemoteItem item)
        {
            throw new NotImplementedException();
        }
    }
}
