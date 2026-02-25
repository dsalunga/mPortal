using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Common;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    class WebTextResourceProvider : IWebTextResourceProvider
    {
        public WebTextResourceProvider() { }

        public WebTextResource Get(int textResourceId)
        {
            var sql = "SELECT * FROM WebTextResource WHERE " + DbSyntax.QuoteIdentifier("TextResourceId") + " = @TextResourceId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@TextResourceId", textResourceId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebTextResource Get(string title)
        {
            var sql = "SELECT * FROM WebTextResource WHERE " + DbSyntax.QuoteIdentifier("Title") + " = @Title";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Title", title)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<WebTextResource> GetList()
        {
            List<WebTextResource> items = new List<WebTextResource>();

            var sql = "SELECT * FROM WebTextResource";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebTextResource> GetList(int contentTypeId = -2)
        {
            List<WebTextResource> items = new List<WebTextResource>();

            var sql = "SELECT * FROM WebTextResource WHERE " + DbSyntax.QuoteIdentifier("ContentTypeId") + " = @ContentTypeId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ContentTypeId", contentTypeId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebTextResource> GetByDirectory(int directoryId)
        {
            List<WebTextResource> items = new List<WebTextResource>();

            var sql = "SELECT * FROM WebTextResource WHERE " + DbSyntax.QuoteIdentifier("DirectoryId") + " = @DirectoryId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@DirectoryId", directoryId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public int Update(WebTextResource item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebTextResource SET " +
                    DbSyntax.QuoteIdentifier("ContentTypeId") + " = @ContentTypeId, " +
                    DbSyntax.QuoteIdentifier("Title") + " = @Title, " +
                    DbSyntax.QuoteIdentifier("Content") + " = @Content, " +
                    DbSyntax.QuoteIdentifier("DirectoryId") + " = @DirectoryId, " +
                    DbSyntax.QuoteIdentifier("Rank") + " = @Rank, " +
                    DbSyntax.QuoteIdentifier("DatePersisted") + " = @DatePersisted, " +
                    DbSyntax.QuoteIdentifier("PhysicalPath") + " = @PhysicalPath" +
                    " WHERE " + DbSyntax.QuoteIdentifier("TextResourceId") + " = @TextResourceId";
                parms = new[] {
                    DbHelper.CreateParameter("@ContentTypeId", item.ContentTypeId),
                    DbHelper.CreateParameter("@Title", item.Title),
                    DbHelper.CreateParameter("@Content", item.Content),
                    DbHelper.CreateParameter("@DirectoryId", item.DirectoryId),
                    DbHelper.CreateParameter("@Rank", item.Rank),
                    DbHelper.CreateParameter("@DatePersisted", item.DatePersisted),
                    DbHelper.CreateParameter("@PhysicalPath", item.PhysicalPath),
                    DbHelper.CreateParameter("@TextResourceId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebTextResource (" +
                    DbSyntax.QuoteIdentifier("ContentTypeId") + ", " +
                    DbSyntax.QuoteIdentifier("Title") + ", " +
                    DbSyntax.QuoteIdentifier("Content") + ", " +
                    DbSyntax.QuoteIdentifier("DirectoryId") + ", " +
                    DbSyntax.QuoteIdentifier("Rank") + ", " +
                    DbSyntax.QuoteIdentifier("DatePersisted") + ", " +
                    DbSyntax.QuoteIdentifier("PhysicalPath") +
                    ") VALUES (@ContentTypeId, @Title, @Content, @DirectoryId, @Rank, @DatePersisted, @PhysicalPath)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("TextResourceId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@ContentTypeId", item.ContentTypeId),
                    DbHelper.CreateParameter("@Title", item.Title),
                    DbHelper.CreateParameter("@Content", item.Content),
                    DbHelper.CreateParameter("@DirectoryId", item.DirectoryId),
                    DbHelper.CreateParameter("@Rank", item.Rank),
                    DbHelper.CreateParameter("@DatePersisted", item.DatePersisted),
                    DbHelper.CreateParameter("@PhysicalPath", item.PhysicalPath)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        public bool Delete(int textResourceId)
        {
            var sql = "DELETE FROM WebTextResource WHERE " + DbSyntax.QuoteIdentifier("TextResourceId") + " = @TextResourceId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@TextResourceId", textResourceId));

            return true;
        }

        private WebTextResource From(DbDataReader r)
        {
            WebTextResource item = new WebTextResource();
            item.Id = DataUtil.GetId(r["TextResourceId"]);
            item.ContentTypeId = DataUtil.GetId(r["ContentTypeId"]);
            item.Title = r["Title"].ToString();
            item.Content = r["Content"].ToString();
            item.DirectoryId = DataUtil.GetId(r["DirectoryId"]);
            item.Rank = DataUtil.GetInt32(r["Rank"]);
            item.DateModified = (DateTime)r["DateModified"];
            item.DatePersisted = (DateTime)r["DatePersisted"];
            item.PhysicalPath = DataUtil.Get(r, "PhysicalPath");

            return item;
        }

        #region IDataProvider<WebTextResource> Members


        public WebTextResource Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebTextResource> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            var item = WebObject.Get(WebObjects.WebTextResource);
            bool noSearch = string.IsNullOrEmpty(loweredKeyword);

            return from i in GetByDirectory(directoryId)
                   where noSearch
                       || DataUtil.HasMatch(i.Title, loweredKeyword)
                       || DataUtil.HasMatch(i.Content, loweredKeyword)
                   orderby i.DateModified descending
                   select new WebDirectoryEntry
                   {
                       Id = i.Id,
                       RecordId = i.Id,
                       ObjectId = item.Id,
                       Name = i.Title,
                       ObjectName = item.FriendlyNameEval,
                       DateModified = i.DateModified
                   };
        }


        public WebTextResource Refresh(WebTextResource item)
        {
            throw new NotImplementedException();
        }
    }
}
