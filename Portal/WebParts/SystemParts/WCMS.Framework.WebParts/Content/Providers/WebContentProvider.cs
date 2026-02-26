using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

using Enu = WCMS.Framework.WebContentEnum;

namespace WCMS.WebSystem.WebParts.Content.Providers
{
    class WebContentProvider : IWebContentProvider
    {
        public const string AT = "@";

        public WebContentProvider() { }

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            var item = WebObject.Get(WebObjects.WebContent);
            bool noSearch = string.IsNullOrEmpty(loweredKeyword);

            return from i in GetList(-1, -1, -1, directoryId)
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

        public WebContent Get(int contentId)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("WebContent") +
                " WHERE " + DbSyntax.QuoteIdentifier(Enu.ContentId) + " = @" + Enu.ContentId;
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter(AT + Enu.ContentId, contentId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebContent Get(string title)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("WebContent") +
                " WHERE " + DbSyntax.QuoteIdentifier(Enu.Title) + " = @Title";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Title", title)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<WebContent> GetList()
        {
            List<WebContent> items = new List<WebContent>();
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("WebContent");
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebContent> GetList(int siteId)
        {
            List<WebContent> items = new List<WebContent>();
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("WebContent") +
                " WHERE " + DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@SiteId", siteId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;

        }

        public IEnumerable<WebContent> GetList(int contentId, int versionOf, int versionNo, int directoryId)
        {
            List<WebContent> items = new List<WebContent>();
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("WebContent");
            var conditions = new List<string>();
            var parms = new List<DbParameter>();

            if (contentId != -1)
            {
                conditions.Add(DbSyntax.QuoteIdentifier(Enu.ContentId) + " = @" + Enu.ContentId);
                parms.Add(DbHelper.CreateParameter(AT + Enu.ContentId, contentId));
            }
            if (versionOf != -1)
            {
                conditions.Add(DbSyntax.QuoteIdentifier(Enu.VersionOf) + " = @" + Enu.VersionOf);
                parms.Add(DbHelper.CreateParameter(AT + Enu.VersionOf, versionOf));
            }
            if (versionNo != -1)
            {
                conditions.Add(DbSyntax.QuoteIdentifier(Enu.VersionNo) + " = @" + Enu.VersionNo);
                parms.Add(DbHelper.CreateParameter(AT + Enu.VersionNo, versionNo));
            }
            if (directoryId != -1)
            {
                conditions.Add(DbSyntax.QuoteIdentifier("DirectoryId") + " = @DirectoryId");
                parms.Add(DbHelper.CreateParameter("@DirectoryId", directoryId));
            }

            if (conditions.Count > 0)
                sql += " WHERE " + string.Join(" AND ", conditions);

            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql, parms.ToArray()))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;

        }

        public int Update(WebContent item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("WebContent") + " SET " +
                    DbSyntax.QuoteIdentifier(Enu.Title) + " = @" + Enu.Title + ", " +
                    DbSyntax.QuoteIdentifier(Enu.Content) + " = @" + Enu.Content + ", " +
                    DbSyntax.QuoteIdentifier(Enu.VersionNo) + " = @" + Enu.VersionNo + ", " +
                    DbSyntax.QuoteIdentifier(Enu.VersionOf) + " = @" + Enu.VersionOf + ", " +
                    DbSyntax.QuoteIdentifier("DirectoryId") + " = @DirectoryId, " +
                    DbSyntax.QuoteIdentifier("Active") + " = @Active, " +
                    DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId, " +
                    DbSyntax.QuoteIdentifier("EditorSensitive") + " = @EditorSensitive, " +
                    DbSyntax.QuoteIdentifier("ActiveContent") + " = @ActiveContent, " +
                    DbSyntax.QuoteIdentifier("DateModified") + " = @DateModified" +
                    " WHERE " + DbSyntax.QuoteIdentifier(Enu.ContentId) + " = @" + Enu.ContentId;
                parms = new[] {
                    DbHelper.CreateParameter(AT + Enu.Title, item.Title),
                    DbHelper.CreateParameter(AT + Enu.Content, item.Content),
                    DbHelper.CreateParameter(AT + Enu.VersionNo, item.VersionNo),
                    DbHelper.CreateParameter(AT + Enu.VersionOf, item.VersionOf),
                    DbHelper.CreateParameter("@DirectoryId", item.DirectoryId),
                    DbHelper.CreateParameter("@Active", item.Active),
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@EditorSensitive", item.EditorSensitive),
                    DbHelper.CreateParameter("@ActiveContent", item.ActiveContent),
                    DbHelper.CreateParameter("@DateModified", DateTime.Now),
                    DbHelper.CreateParameter(AT + Enu.ContentId, item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("WebContent") + " (" +
                    DbSyntax.QuoteIdentifier(Enu.Title) + ", " +
                    DbSyntax.QuoteIdentifier(Enu.Content) + ", " +
                    DbSyntax.QuoteIdentifier(Enu.VersionNo) + ", " +
                    DbSyntax.QuoteIdentifier(Enu.VersionOf) + ", " +
                    DbSyntax.QuoteIdentifier("DirectoryId") + ", " +
                    DbSyntax.QuoteIdentifier("Active") + ", " +
                    DbSyntax.QuoteIdentifier("SiteId") + ", " +
                    DbSyntax.QuoteIdentifier("EditorSensitive") + ", " +
                    DbSyntax.QuoteIdentifier("ActiveContent") + ", " +
                    DbSyntax.QuoteIdentifier("DateModified") +
                    ") VALUES (@" + Enu.Title + ", @" + Enu.Content + ", @" + Enu.VersionNo +
                    ", @" + Enu.VersionOf + ", @DirectoryId, @Active, @SiteId, @EditorSensitive, @ActiveContent, @DateModified)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier(Enu.ContentId);
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter(AT + Enu.Title, item.Title),
                    DbHelper.CreateParameter(AT + Enu.Content, item.Content),
                    DbHelper.CreateParameter(AT + Enu.VersionNo, item.VersionNo),
                    DbHelper.CreateParameter(AT + Enu.VersionOf, item.VersionOf),
                    DbHelper.CreateParameter("@DirectoryId", item.DirectoryId),
                    DbHelper.CreateParameter("@Active", item.Active),
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@EditorSensitive", item.EditorSensitive),
                    DbHelper.CreateParameter("@ActiveContent", item.ActiveContent),
                    DbHelper.CreateParameter("@DateModified", DateTime.Now)
                };
                var o = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(o);
            }

            return item.Id;
        }

        public bool Delete(int contentId)
        {
            var sql = "DELETE FROM " + DbSyntax.QuoteIdentifier("WebContent") +
                " WHERE " + DbSyntax.QuoteIdentifier(Enu.ContentId) + " = @" + Enu.ContentId;
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter(AT + Enu.ContentId, contentId));

            return true;
        }

        public WebContent From(DbDataReader r)
        {
            WebContent item = new WebContent();
            item.Id = DataUtil.GetId(r, WebContentEnum.ContentId);
            item.Title = DataUtil.Get(r, WebContentEnum.Title);
            item.Content = DataUtil.Get(r, WebContentEnum.Content);
            item.VersionOf = DataUtil.GetId(r, WebContentEnum.VersionOf);
            item.VersionNo = DataUtil.GetInt32(r, WebContentEnum.VersionNo);
            item.DirectoryId = DataUtil.GetId(r, "DirectoryId");
            item.Active = DataUtil.GetInt32(r, WebColumns.Active);
            item.DateModified = DataUtil.GetDateTime(r, WebColumns.DateModified);
            item.SiteId = DataUtil.GetId(r, WebColumns.SiteId);
            item.EditorSensitive = DataUtil.GetInt32(r, "EditorSensitive");
            item.ActiveContent = DataUtil.GetInt32(r, "ActiveContent");

            return item;
        }

        #region IDataProvider<WebContent> Members

        public WebContent Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebContent> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        #endregion


        public WebContent Refresh(WebContent item)
        {
            throw new NotImplementedException();
        }
    }
}
