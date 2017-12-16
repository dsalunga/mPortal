using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

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
            SqlHelper.ExecuteNonQuery("RemoteLibrary_Del",
                new SqlParameter("@Id", id));

            return true;
        }

        public RemoteLibrary Get(int id)
        {
            if (id > 0)
            {
                using (var r = SqlHelper.ExecuteReader("RemoteLibrary_Get",
                    new SqlParameter("@Id", id)))
                {
                    if (r.Read())
                        return From(r);
                }
            }

            return null;
        }

        private RemoteLibrary From(SqlDataReader r)
        {
            var item = new RemoteLibrary();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.SourceTypeId = DataHelper.GetInt32(r, "SourceTypeId");
            item.BaseAddress = DataHelper.Get(r, "BaseAddress");
            item.UserName = DataHelper.Get(r, "UserName");
            item.Password = DataHelper.Get(r, "Password");
            item.LastIndexDate = DataHelper.GetDateTime(r, "LastIndexDate");
            item.Active = DataHelper.GetInt32(r, WebColumns.Active);
            item.DisplayBaseAddress = DataHelper.Get(r, "DisplayBaseAddress");
            item.FileCacheEnabled = DataHelper.GetInt32(r, "FileCacheEnabled");
            item.DownloadCountSince = DataHelper.GetDateTime(r, "DownloadCountSince");
            item.FileCacheFolder = DataHelper.Get(r, "FileCacheFolder");
            item.FileCacheMinDownloadCount = DataHelper.GetInt32(r, "FileCacheMinDownloadCount");
            item.FileCacheCeilingSize = DataHelper.GetInt32(r, "FileCacheCeilingSize");
            item.FileCacheMaxSize = DataHelper.GetInt32(r, "FileCacheMaxSize");
            item.FileCacheMinDiskFreeMB = DataHelper.GetInt32(r, "FileCacheMinDiskFreeMB");
            item.Size = DataHelper.GetInt64(r, WebColumns.Size);

            return item;
        }

        public RemoteLibrary Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RemoteLibrary> GetList()
        {
            var items = new List<RemoteLibrary>();
            using (var r = SqlHelper.ExecuteReader("RemoteLibrary_Get"))
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
            var obj = SqlHelper.ExecuteScalar("RemoteLibrary_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@SourceTypeId", item.SourceTypeId),
                new SqlParameter("@BaseAddress", item.BaseAddress),
                new SqlParameter("@UserName", item.UserName),
                new SqlParameter("@Password", item.Password),
                new SqlParameter("@LastIndexDate", item.LastIndexDate),
                new SqlParameter("@Active", item.Active),
                new SqlParameter("@DisplayBaseAddress", item.DisplayBaseAddress),
                new SqlParameter("@DownloadCountSince", item.DownloadCountSince),
                new SqlParameter("@FileCacheEnabled", item.FileCacheEnabled),
                new SqlParameter("@FileCacheFolder", item.FileCacheFolder),
                new SqlParameter("@FileCacheMinDownloadCount", item.FileCacheMinDownloadCount),
                new SqlParameter("@FileCacheCeilingSize", item.FileCacheCeilingSize),
                new SqlParameter("@FileCacheMaxSize", item.FileCacheMaxSize),
                new SqlParameter("@FileCacheMinDiskFreeMB", item.FileCacheMinDiskFreeMB),
                new SqlParameter("@Size", item.Size)
            );

            item.Id = DataHelper.GetId(obj);

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
