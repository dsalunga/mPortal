using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Photo.Providers
{
    public class AlbumPhotoProvider : IAlbumPhotoProvider
    {
        #region IDataProvider<AlbumPhoto> Members

        public bool Delete(int id)
        {
            var sql = "DELETE FROM " + DbSyntax.QuoteIdentifier("Gallery") +
                " WHERE " + DbSyntax.QuoteIdentifier("GalleryID") + " = @GalleryID";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@GalleryID", id));

            return true;
        }

        public AlbumPhoto Get(int id)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("Gallery") +
                " WHERE " + DbSyntax.QuoteIdentifier("GalleryID") + " = @GalleryID";
            using (DbDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@GalleryID", id)))
            {
                if (dr.Read())
                    return From(dr);
            }

            return null;
        }

        private static AlbumPhoto From(DbDataReader r)
        {
            AlbumPhoto item = new AlbumPhoto();
            item.Id = DataUtil.GetId(r, "GalleryID");
            item.Caption = r["Caption"].ToString();
            //txtThumbnail.Text = r["Thumbnail"].ToString();
            item.PhotoName = r["ImageURL"].ToString();
            item.DateCreated = DataUtil.GetDateTime(r, "DateCreated");
            item.SiteId = DataUtil.GetId(r, "SiteID");
            item.AlbumId = DataUtil.GetId(r, "CategoryID");
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
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("Gallery");
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<AlbumPhoto> GetList(int albumId)
        {
            List<AlbumPhoto> items = new List<AlbumPhoto>();
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("Gallery") +
                " WHERE " + DbSyntax.QuoteIdentifier("CategoryID") + " = @CategoryID";
            using (DbDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@CategoryID", albumId)))
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
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("Gallery") + " SET " +
                    DbSyntax.QuoteIdentifier("Caption") + " = @Caption, " +
                    DbSyntax.QuoteIdentifier("ImageURL") + " = @ImageURL, " +
                    DbSyntax.QuoteIdentifier("SiteID") + " = @SiteID, " +
                    DbSyntax.QuoteIdentifier("CategoryID") + " = @CategoryID, " +
                    DbSyntax.QuoteIdentifier("IsActive") + " = @IsActive" +
                    " WHERE " + DbSyntax.QuoteIdentifier("GalleryID") + " = @GalleryID";
                parms = new[] {
                    DbHelper.CreateParameter("@Caption", item.Caption),
                    DbHelper.CreateParameter("@ImageURL", item.PhotoName),
                    DbHelper.CreateParameter("@SiteID", item.SiteId),
                    DbHelper.CreateParameter("@CategoryID", item.AlbumId),
                    DbHelper.CreateParameter("@IsActive", item.IsActive),
                    DbHelper.CreateParameter("@GalleryID", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("Gallery") + " (" +
                    DbSyntax.QuoteIdentifier("Caption") + ", " +
                    DbSyntax.QuoteIdentifier("ImageURL") + ", " +
                    DbSyntax.QuoteIdentifier("SiteID") + ", " +
                    DbSyntax.QuoteIdentifier("CategoryID") + ", " +
                    DbSyntax.QuoteIdentifier("IsActive") + ", " +
                    DbSyntax.QuoteIdentifier("DateCreated") +
                    ") VALUES (@Caption, @ImageURL, @SiteID, @CategoryID, @IsActive, @DateCreated)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("GalleryID");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Caption", item.Caption),
                    DbHelper.CreateParameter("@ImageURL", item.PhotoName),
                    DbHelper.CreateParameter("@SiteID", item.SiteId),
                    DbHelper.CreateParameter("@CategoryID", item.AlbumId),
                    DbHelper.CreateParameter("@IsActive", item.IsActive),
                    DbHelper.CreateParameter("@DateCreated", DateTime.Now)
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


        public AlbumPhoto Refresh(AlbumPhoto item)
        {
            throw new NotImplementedException();
        }
    }
}
