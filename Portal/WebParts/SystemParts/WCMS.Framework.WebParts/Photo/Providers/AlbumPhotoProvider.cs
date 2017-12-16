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
    public class AlbumPhotoProvider : IAlbumPhotoProvider
    {
        #region IDataProvider<AlbumPhoto> Members

        public bool Delete(int id)
        {
            SqlHelper.ExecuteNonQuery("Gallery_Del",
                new SqlParameter("@Id", id)
            );

            return true;
        }

        public AlbumPhoto Get(int id)
        {
            using (SqlDataReader dr = SqlHelper.ExecuteReader("Gallery_Get",
                new SqlParameter("@GalleryID", id)))
            {
                if (dr.Read())
                    return From(dr);
            }

            return null;
        }

        private static AlbumPhoto From(SqlDataReader r)
        {
            AlbumPhoto item = new AlbumPhoto();
            item.Id = DataHelper.GetId(r, "GalleryID");
            item.Caption = r["Caption"].ToString();
            //txtThumbnail.Text = r["Thumbnail"].ToString();
            item.PhotoName = r["ImageURL"].ToString();
            item.DateCreated = DataHelper.GetDateTime(r, "DateCreated");
            item.SiteId = DataHelper.GetId(r, "SiteID");
            item.AlbumId = DataHelper.GetId(r, "CategoryID");
            item.Active = (bool)r["IsActive"] ? 1 : 0;

            return item;
        }

        public AlbumPhoto Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AlbumPhoto> GetList()
        {
            List<AlbumPhoto> items = new List<AlbumPhoto>();

            using (SqlDataReader r = SqlHelper.ExecuteReader("Gallery_Get"))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<AlbumPhoto> GetList(int albumId)
        {
            List<AlbumPhoto> items = new List<AlbumPhoto>();

            using (SqlDataReader dr = SqlHelper.ExecuteReader("Gallery_Get",
                new SqlParameter("@CategoryID", albumId)))
            {
                while (dr.Read())
                    items.Add(From(dr));
            }

            return items;
        }

        public IEnumerable<AlbumPhoto> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(AlbumPhoto item)
        {
            var obj = SqlHelper.ExecuteNonQuery("Gallery_Set",
                new SqlParameter("@GalleryID", item.Id),
                new SqlParameter("@Caption", item.Caption),
                //new SqlParameter("@Thumbnail", imageFile),
                new SqlParameter("@ImageURL", item.PhotoName),
                new SqlParameter("@SiteID", item.SiteId),
                new SqlParameter("@CategoryID", item.AlbumId),
                new SqlParameter("@IsActive", item.IsActive)
            );

            item.Id = DataHelper.GetId(obj);

            return item.Id;
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public AlbumPhoto Refresh(AlbumPhoto item)
        {
            throw new NotImplementedException();
        }
    }
}
