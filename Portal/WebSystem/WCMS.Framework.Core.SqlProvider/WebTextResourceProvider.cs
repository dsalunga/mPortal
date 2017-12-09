using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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
            using (DbDataReader r = SqlHelper.ExecuteReader("WebTextResource_Get",
                new SqlParameter("@TextResourceId", textResourceId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebTextResource Get(string title)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebTextResource_Get",
                new SqlParameter("@Title", title)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<WebTextResource> GetList()
        {
            List<WebTextResource> items = new List<WebTextResource>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebTextResource_Get"))
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

            using (DbDataReader r = SqlHelper.ExecuteReader("WebTextResource_Get",
                new SqlParameter("@ContentTypeId", contentTypeId)))
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

            using (DbDataReader r = SqlHelper.ExecuteReader("WebTextResource_Get",
                new SqlParameter("@DirectoryId", directoryId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public int Update(WebTextResource item)
        {
            object o = SqlHelper.ExecuteScalar("WebTextResource_Set",
                new SqlParameter("@TextResourceId", item.Id),
                new SqlParameter("@ContentTypeId", item.ContentTypeId),
                new SqlParameter("@Title", item.Title),
                new SqlParameter("@Content", item.Content),
                new SqlParameter("@DirectoryId", item.DirectoryId),
                new SqlParameter("@Rank", item.Rank),
                new SqlParameter("@DatePersisted", item.DatePersisted),
                new SqlParameter("@PhysicalPath", item.PhysicalPath)
            );

            item.Id = DataHelper.GetId(o);

            return item.Id;
        }

        public bool Delete(int textResourceId)
        {
            SqlHelper.ExecuteNonQuery("WebTextResource_Del",
                new SqlParameter("@TextResourceId", textResourceId));

            return true;
        }

        private WebTextResource From(DbDataReader r)
        {
            WebTextResource item = new WebTextResource();
            item.Id = DataHelper.GetId(r["TextResourceId"]);
            item.ContentTypeId = DataHelper.GetId(r["ContentTypeId"]);
            item.Title = r["Title"].ToString();
            item.Content = r["Content"].ToString();
            item.DirectoryId = DataHelper.GetId(r["DirectoryId"]);
            item.Rank = DataHelper.GetInt32(r["Rank"]);
            item.DateModified = (DateTime)r["DateModified"];
            item.DatePersisted = (DateTime)r["DatePersisted"];
            item.PhysicalPath = DataHelper.Get(r, "PhysicalPath");

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


        public WebTextResource Refresh(WebTextResource item)
        {
            throw new NotImplementedException();
        }
    }
}
