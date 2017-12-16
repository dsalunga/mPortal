using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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
                       || DataHelper.HasMatch(i.Title, loweredKeyword)
                       || DataHelper.HasMatch(i.Content, loweredKeyword)
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
            using (DbDataReader r = SqlHelper.ExecuteReader(Enu.SQL_GET,
                new SqlParameter(AT + WebContentEnum.ContentId, contentId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebContent Get(string title)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader(Enu.SQL_GET,
                new SqlParameter("@Title", title)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<WebContent> GetList()
        {
            List<WebContent> items = new List<WebContent>();
            using (DbDataReader r = SqlHelper.ExecuteReader(Enu.SQL_GET))
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

            using (DbDataReader r = SqlHelper.ExecuteReader(Enu.SQL_GET,
                new SqlParameter("@SiteId", siteId)))
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

            using (DbDataReader r = SqlHelper.ExecuteReader(Enu.SQL_GET,
                new SqlParameter(AT + Enu.ContentId, contentId),
                new SqlParameter(AT + WebContentEnum.VersionOf, versionOf),
                new SqlParameter(AT + WebContentEnum.VersionNo, versionNo),
                new SqlParameter("@DirectoryId", directoryId)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;

        }

        public int Update(WebContent item)
        {
            object o = SqlHelper.ExecuteScalar(WebContentEnum.SQL_SET,
                new SqlParameter(AT + WebContentEnum.ContentId, item.Id),
                new SqlParameter(AT + WebContentEnum.Title, item.Title),
                new SqlParameter(AT + WebContentEnum.Content, item.Content),
                new SqlParameter(AT + WebContentEnum.VersionNo, item.VersionNo),
                new SqlParameter(AT + WebContentEnum.VersionOf, item.VersionOf),
                new SqlParameter("@DirectoryId", item.DirectoryId),
                new SqlParameter("@Active", item.Active),
                new SqlParameter("@SiteId", item.SiteId),
                new SqlParameter("@EditorSensitive", item.EditorSensitive),
                new SqlParameter("@ActiveContent", item.ActiveContent)
            );

            item.Id = DataHelper.GetId(o.ToString());
            return item.Id;
        }

        public bool Delete(int contentId)
        {
            SqlHelper.ExecuteNonQuery(WebContentEnum.SQL_DEL,
                new SqlParameter(AT + WebContentEnum.ContentId, contentId));

            return true;
        }

        public WebContent From(DbDataReader r)
        {
            WebContent item = new WebContent();
            item.Id = DataHelper.GetId(r, WebContentEnum.ContentId);
            item.Title = DataHelper.Get(r, WebContentEnum.Title);
            item.Content = DataHelper.Get(r, WebContentEnum.Content);
            item.VersionOf = DataHelper.GetId(r, WebContentEnum.VersionOf);
            item.VersionNo = DataHelper.GetInt32(r, WebContentEnum.VersionNo);
            item.DirectoryId = DataHelper.GetId(r, "DirectoryId");
            item.Active = DataHelper.GetInt32(r, WebColumns.Active);
            item.DateModified = DataHelper.GetDateTime(r, WebColumns.DateModified);
            item.SiteId = DataHelper.GetId(r, WebColumns.SiteId);
            item.EditorSensitive = DataHelper.GetInt32(r, "EditorSensitive");
            item.ActiveContent = DataHelper.GetInt32(r, "ActiveContent");

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
            throw new NotImplementedException();
        }

        #endregion


        public WebContent Refresh(WebContent item)
        {
            throw new NotImplementedException();
        }
    }
}
