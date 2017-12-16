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
    public class RemoteItemSqlProvider : IRemoteItemProvider
    {
        public IEnumerable<RemoteItem> GetList(int libraryId, int parentId = -2)
        {
            var items = new List<RemoteItem>();

            using (var r = SqlHelper.ExecuteReader("RemoteItem_Get",
                new SqlParameter("@LibraryId", libraryId),
                new SqlParameter("@ParentId", parentId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        private RemoteItem From(SqlDataReader r)
        {
            var item = new RemoteItem();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.LibraryId = DataHelper.GetId(r, "LibraryId");
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.RelativePath = DataHelper.Get(r, "RelativePath");
            item.TypeId = DataHelper.GetInt32(r, "TypeId");
            item.DateModified = DataHelper.GetDateTime(r, "DateModified");
            item.Size = DataHelper.GetInt64(r, "Size");
            item.Content = DataHelper.Get(r, "Content");
            item.ParentId = DataHelper.GetId(r, WebColumns.ParentId);
            item.DownloadCount = DataHelper.GetInt32(r, "DownloadCount");
            item.DisplayName = DataHelper.Get(r, "DisplayName");
            item.IndexDateModified = DataHelper.GetDateTime(r, "IndexDateModified");
            item.FileCacheEnabled = DataHelper.GetInt32(r, "FileCacheEnabled");
            item.Cached = DataHelper.GetInt32(r, "Cached");

            return item;
        }

        public bool Delete(int id)
        {
            SqlHelper.ExecuteNonQuery("RemoteItem_Del",
                new SqlParameter("@Id", id));

            return true;
        }

        public RemoteItem Get(int id)
        {
            if (id > 0)
            {
                using (var r = SqlHelper.ExecuteReader("RemoteItem_Get",
                    new SqlParameter("@Id", id)))
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
            using (var r = SqlHelper.ExecuteReader("RemoteItem_Get"))
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
            var obj = SqlHelper.ExecuteScalar("RemoteItem_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@LibraryId", item.LibraryId),
                new SqlParameter("@RelativePath", item.RelativePath),
                new SqlParameter("@TypeId", item.TypeId),
                new SqlParameter("@DateModified", item.DateModified),
                new SqlParameter("@Size", item.Size),
                new SqlParameter("@Content", item.Content),
                new SqlParameter("@ParentId", item.ParentId),
                new SqlParameter("@DownloadCount", item.DownloadCount),
                new SqlParameter("@DisplayName", item.DisplayName),
                new SqlParameter("@IndexDateModified", item.IndexDateModified),
                new SqlParameter("@FileCacheEnabled", item.FileCacheEnabled),
                new SqlParameter("@Cached", item.Cached)
            );

            item.Id = DataHelper.GetId(obj);
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
