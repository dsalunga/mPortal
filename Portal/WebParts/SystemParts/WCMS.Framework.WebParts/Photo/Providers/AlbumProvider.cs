using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Photo.Providers
{
    public class AlbumProvider : IAlbumProvider
    {
        #region IDataProvider<Album> Members

        public bool Delete(int id)
        {
            SqlHelper.ExecuteNonQuery("GalleryCategory_Del",
                new SqlParameter("@Id", id)
            );

            return true;
        }

        public Album Get(int id)
        {
            using (SqlDataReader dr = SqlHelper.ExecuteReader("GalleryCategory_Get",
                new SqlParameter("@CategoryID", id)))
            {
                if (dr.Read())
                    return From(dr);
            }

            return null;
        }

        public Album Get(string title)
        {
            using (SqlDataReader dr = SqlHelper.ExecuteReader("GalleryCategory_Get",
                new SqlParameter("@Title", title)))
            {
                if (dr.Read())
                    return From(dr);
            }

            return null;
        }

        private static Album From(SqlDataReader r)
        {
            Album item = new Album();

            item.Id = DataHelper.GetId(r, "CategoryID");
            item.Title = r["Title"].ToString();
            item.ImageFile = r["ImageURL"].ToString();
            item.FolderName = r["FolderName"].ToString();
            item.Width = DataHelper.GetId(r, "Width");
            item.PhotoHeight = DataHelper.GetId(r, "PhotoHeight");
            item.PhotoWidth = DataHelper.GetId(r, "PhotoWidth");
            item.DateModified = DataHelper.GetDateTime(r, "DateModified");

            return item;
        }

        public Album Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Album> GetList()
        {
            List<Album> items = new List<Album>();

            using (SqlDataReader dr = SqlHelper.ExecuteReader("GalleryCategory_Get"))
            {
                while (dr.Read())
                    items.Add(From(dr));
            }

            return items;
        }

        public IEnumerable<Album> GetList(int objectId, int recordId)
        {
            List<Album> items = new List<Album>();

            using (SqlDataReader dr = SqlHelper.ExecuteReader("GalleryCategory_Get",
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId)
            ))
            {
                while (dr.Read())
                    items.Add(From(dr));
            }

            return items;
        }

        public IEnumerable<Album> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(Album item)
        {
            var obj = SqlHelper.ExecuteNonQuery("GalleryCategory_Set",
                new SqlParameter("@CategoryID", item.Id),
                new SqlParameter("@Title", item.Title),
                new SqlParameter("@ImageURL", item.ImageFile),
                new SqlParameter("@Width", item.Width),
                new SqlParameter("@PhotoHeight", item.PhotoHeight),
                new SqlParameter("@FolderName", item.FolderName),
                new SqlParameter("@PhotoWidth", item.PhotoWidth),
                new SqlParameter("@DateModified", item.DateModified)
            );

            item.Id = DataHelper.GetId(obj);

            return item.Id;
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public Album Refresh(Album item)
        {
            throw new NotImplementedException();
        }
    }
}
