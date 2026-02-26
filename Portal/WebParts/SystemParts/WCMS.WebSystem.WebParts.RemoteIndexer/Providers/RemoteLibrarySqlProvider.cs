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
    public class RemoteLibrarySqlProvider : IRemoteLibraryProvider
    {
        #region IDataProvider<RemoteLibrary> Members

        public bool Delete(int id)
        {
            var sql = "DELETE FROM " + DbSyntax.QuoteIdentifier("RemoteLibrary") +
                " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id));

            return true;
        }

        public RemoteLibrary Get(int id)
        {
            if (id > 0)
            {
                var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("RemoteLibrary") +
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

        private RemoteLibrary From(IDataReader r)
        {
            var item = new RemoteLibrary();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.SourceTypeId = DataUtil.GetInt32(r, "SourceTypeId");
            item.BaseAddress = DataUtil.Get(r, "BaseAddress");
            item.UserName = DataUtil.Get(r, "UserName");
            item.Password = DataUtil.Get(r, "Password");
            item.LastIndexDate = DataUtil.GetDateTime(r, "LastIndexDate");
            item.Active = DataUtil.GetInt32(r, WebColumns.Active);
            item.DisplayBaseAddress = DataUtil.Get(r, "DisplayBaseAddress");
            item.FileCacheEnabled = DataUtil.GetInt32(r, "FileCacheEnabled");
            item.DownloadCountSince = DataUtil.GetDateTime(r, "DownloadCountSince");
            item.FileCacheFolder = DataUtil.Get(r, "FileCacheFolder");
            item.FileCacheMinDownloadCount = DataUtil.GetInt32(r, "FileCacheMinDownloadCount");
            item.FileCacheCeilingSize = DataUtil.GetInt32(r, "FileCacheCeilingSize");
            item.FileCacheMaxSize = DataUtil.GetInt32(r, "FileCacheMaxSize");
            item.FileCacheMinDiskFreeMB = DataUtil.GetInt32(r, "FileCacheMinDiskFreeMB");
            item.Size = DataUtil.GetInt64(r, WebColumns.Size);

            return item;
        }

        public RemoteLibrary Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RemoteLibrary> GetList()
        {
            var items = new List<RemoteLibrary>();
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("RemoteLibrary");
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<RemoteLibrary> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(RemoteLibrary item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("RemoteLibrary") + " SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("SourceTypeId") + " = @SourceTypeId, " +
                    DbSyntax.QuoteIdentifier("BaseAddress") + " = @BaseAddress, " +
                    DbSyntax.QuoteIdentifier("UserName") + " = @UserName, " +
                    DbSyntax.QuoteIdentifier("Password") + " = @Password, " +
                    DbSyntax.QuoteIdentifier("LastIndexDate") + " = @LastIndexDate, " +
                    DbSyntax.QuoteIdentifier("Active") + " = @Active, " +
                    DbSyntax.QuoteIdentifier("DisplayBaseAddress") + " = @DisplayBaseAddress, " +
                    DbSyntax.QuoteIdentifier("DownloadCountSince") + " = @DownloadCountSince, " +
                    DbSyntax.QuoteIdentifier("FileCacheEnabled") + " = @FileCacheEnabled, " +
                    DbSyntax.QuoteIdentifier("FileCacheFolder") + " = @FileCacheFolder, " +
                    DbSyntax.QuoteIdentifier("FileCacheMinDownloadCount") + " = @FileCacheMinDownloadCount, " +
                    DbSyntax.QuoteIdentifier("FileCacheCeilingSize") + " = @FileCacheCeilingSize, " +
                    DbSyntax.QuoteIdentifier("FileCacheMaxSize") + " = @FileCacheMaxSize, " +
                    DbSyntax.QuoteIdentifier("FileCacheMinDiskFreeMB") + " = @FileCacheMinDiskFreeMB, " +
                    DbSyntax.QuoteIdentifier("Size") + " = @Size" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@SourceTypeId", item.SourceTypeId),
                    DbHelper.CreateParameter("@BaseAddress", item.BaseAddress),
                    DbHelper.CreateParameter("@UserName", item.UserName),
                    DbHelper.CreateParameter("@Password", item.Password),
                    DbHelper.CreateParameter("@LastIndexDate", item.LastIndexDate),
                    DbHelper.CreateParameter("@Active", item.Active),
                    DbHelper.CreateParameter("@DisplayBaseAddress", item.DisplayBaseAddress),
                    DbHelper.CreateParameter("@DownloadCountSince", item.DownloadCountSince),
                    DbHelper.CreateParameter("@FileCacheEnabled", item.FileCacheEnabled),
                    DbHelper.CreateParameter("@FileCacheFolder", item.FileCacheFolder),
                    DbHelper.CreateParameter("@FileCacheMinDownloadCount", item.FileCacheMinDownloadCount),
                    DbHelper.CreateParameter("@FileCacheCeilingSize", item.FileCacheCeilingSize),
                    DbHelper.CreateParameter("@FileCacheMaxSize", item.FileCacheMaxSize),
                    DbHelper.CreateParameter("@FileCacheMinDiskFreeMB", item.FileCacheMinDiskFreeMB),
                    DbHelper.CreateParameter("@Size", item.Size),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("RemoteLibrary") + " (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("SourceTypeId") + ", " +
                    DbSyntax.QuoteIdentifier("BaseAddress") + ", " +
                    DbSyntax.QuoteIdentifier("UserName") + ", " +
                    DbSyntax.QuoteIdentifier("Password") + ", " +
                    DbSyntax.QuoteIdentifier("LastIndexDate") + ", " +
                    DbSyntax.QuoteIdentifier("Active") + ", " +
                    DbSyntax.QuoteIdentifier("DisplayBaseAddress") + ", " +
                    DbSyntax.QuoteIdentifier("DownloadCountSince") + ", " +
                    DbSyntax.QuoteIdentifier("FileCacheEnabled") + ", " +
                    DbSyntax.QuoteIdentifier("FileCacheFolder") + ", " +
                    DbSyntax.QuoteIdentifier("FileCacheMinDownloadCount") + ", " +
                    DbSyntax.QuoteIdentifier("FileCacheCeilingSize") + ", " +
                    DbSyntax.QuoteIdentifier("FileCacheMaxSize") + ", " +
                    DbSyntax.QuoteIdentifier("FileCacheMinDiskFreeMB") + ", " +
                    DbSyntax.QuoteIdentifier("Size") +
                    ") VALUES (@Name, @SourceTypeId, @BaseAddress, @UserName, @Password, @LastIndexDate, @Active, @DisplayBaseAddress, @DownloadCountSince, @FileCacheEnabled, @FileCacheFolder, @FileCacheMinDownloadCount, @FileCacheCeilingSize, @FileCacheMaxSize, @FileCacheMinDiskFreeMB, @Size)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@SourceTypeId", item.SourceTypeId),
                    DbHelper.CreateParameter("@BaseAddress", item.BaseAddress),
                    DbHelper.CreateParameter("@UserName", item.UserName),
                    DbHelper.CreateParameter("@Password", item.Password),
                    DbHelper.CreateParameter("@LastIndexDate", item.LastIndexDate),
                    DbHelper.CreateParameter("@Active", item.Active),
                    DbHelper.CreateParameter("@DisplayBaseAddress", item.DisplayBaseAddress),
                    DbHelper.CreateParameter("@DownloadCountSince", item.DownloadCountSince),
                    DbHelper.CreateParameter("@FileCacheEnabled", item.FileCacheEnabled),
                    DbHelper.CreateParameter("@FileCacheFolder", item.FileCacheFolder),
                    DbHelper.CreateParameter("@FileCacheMinDownloadCount", item.FileCacheMinDownloadCount),
                    DbHelper.CreateParameter("@FileCacheCeilingSize", item.FileCacheCeilingSize),
                    DbHelper.CreateParameter("@FileCacheMaxSize", item.FileCacheMaxSize),
                    DbHelper.CreateParameter("@FileCacheMinDiskFreeMB", item.FileCacheMinDiskFreeMB),
                    DbHelper.CreateParameter("@Size", item.Size)
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


        public RemoteLibrary Refresh(RemoteLibrary item)
        {
            throw new NotImplementedException();
        }
    }
}
