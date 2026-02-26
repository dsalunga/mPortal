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
    public class AlbumProvider : IAlbumProvider
    {
        #region IDataProvider<Album> Members

        public bool Delete(int id)
        {
            var sql = "DELETE FROM " + DbSyntax.QuoteIdentifier("GalleryCategory") +
                " WHERE " + DbSyntax.QuoteIdentifier("CategoryID") + " = @CategoryID";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@CategoryID", id));

            return true;
        }

        public Album Get(int id)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("GalleryCategory") +
                " WHERE " + DbSyntax.QuoteIdentifier("CategoryID") + " = @CategoryID";
            using (DbDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@CategoryID", id)))
            {
                if (dr.Read())
                    return From(dr);
            }

            return null;
        }

        public Album Get(string title)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("GalleryCategory") +
                " WHERE " + DbSyntax.QuoteIdentifier("Title") + " = @Title";
            using (DbDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Title", title)))
            {
                if (dr.Read())
                    return From(dr);
            }

            return null;
        }

        private static Album From(DbDataReader r)
        {
            Album item = new Album();

            item.Id = DataUtil.GetId(r, "CategoryID");
            item.Title = r["Title"].ToString();
            item.ImageFile = r["ImageURL"].ToString();
            item.FolderName = r["FolderName"].ToString();
            item.Width = DataUtil.GetId(r, "Width");
            item.PhotoHeight = DataUtil.GetId(r, "PhotoHeight");
            item.PhotoWidth = DataUtil.GetId(r, "PhotoWidth");
            item.DateModified = DataUtil.GetDateTime(r, "DateModified");

            return item;
        }

        public Album Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Album> GetList()
        {
            List<Album> items = new List<Album>();
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("GalleryCategory");
            using (DbDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (dr.Read())
                    items.Add(From(dr));
            }

            return items;
        }

        public IEnumerable<Album> GetList(int objectId, int recordId)
        {
            List<Album> items = new List<Album>();
            var sql = "SELECT gc.* FROM " + DbSyntax.QuoteIdentifier("GalleryCategory") + " gc" +
                " INNER JOIN " + DbSyntax.QuoteIdentifier("GalleryCategoryLink") + " gcl" +
                " ON gc." + DbSyntax.QuoteIdentifier("CategoryID") + " = gcl." + DbSyntax.QuoteIdentifier("CategoryId") +
                " WHERE gcl." + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId" +
                " AND gcl." + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId";
            using (DbDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId)
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
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("GalleryCategory") + " SET " +
                    DbSyntax.QuoteIdentifier("Title") + " = @Title, " +
                    DbSyntax.QuoteIdentifier("ImageURL") + " = @ImageURL, " +
                    DbSyntax.QuoteIdentifier("Width") + " = @Width, " +
                    DbSyntax.QuoteIdentifier("PhotoHeight") + " = @PhotoHeight, " +
                    DbSyntax.QuoteIdentifier("FolderName") + " = @FolderName, " +
                    DbSyntax.QuoteIdentifier("PhotoWidth") + " = @PhotoWidth, " +
                    DbSyntax.QuoteIdentifier("DateModified") + " = @DateModified" +
                    " WHERE " + DbSyntax.QuoteIdentifier("CategoryID") + " = @CategoryID";
                parms = new[] {
                    DbHelper.CreateParameter("@Title", item.Title),
                    DbHelper.CreateParameter("@ImageURL", item.ImageFile),
                    DbHelper.CreateParameter("@Width", item.Width),
                    DbHelper.CreateParameter("@PhotoHeight", item.PhotoHeight),
                    DbHelper.CreateParameter("@FolderName", item.FolderName),
                    DbHelper.CreateParameter("@PhotoWidth", item.PhotoWidth),
                    DbHelper.CreateParameter("@DateModified", item.DateModified),
                    DbHelper.CreateParameter("@CategoryID", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("GalleryCategory") + " (" +
                    DbSyntax.QuoteIdentifier("Title") + ", " +
                    DbSyntax.QuoteIdentifier("ImageURL") + ", " +
                    DbSyntax.QuoteIdentifier("Width") + ", " +
                    DbSyntax.QuoteIdentifier("PhotoHeight") + ", " +
                    DbSyntax.QuoteIdentifier("FolderName") + ", " +
                    DbSyntax.QuoteIdentifier("PhotoWidth") + ", " +
                    DbSyntax.QuoteIdentifier("DateModified") +
                    ") VALUES (@Title, @ImageURL, @Width, @PhotoHeight, @FolderName, @PhotoWidth, @DateModified)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("CategoryID");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Title", item.Title),
                    DbHelper.CreateParameter("@ImageURL", item.ImageFile),
                    DbHelper.CreateParameter("@Width", item.Width),
                    DbHelper.CreateParameter("@PhotoHeight", item.PhotoHeight),
                    DbHelper.CreateParameter("@FolderName", item.FolderName),
                    DbHelper.CreateParameter("@PhotoWidth", item.PhotoWidth),
                    DbHelper.CreateParameter("@DateModified", item.DateModified)
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


        public Album Refresh(Album item)
        {
            throw new NotImplementedException();
        }
    }
}
