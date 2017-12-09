using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebFileProvider : IWebFileProvider
    {
        #region IDataProvider<WebFile> Members

        public bool Delete(int id)
        {
            if (id > 0)
                SqlHelper.ExecuteNonQuery("WebFile_Del",
                    new SqlParameter("@FileId", id));

            return true;
        }

        public WebFile Get(int id)
        {
            if (id > 0)
                using (var r = SqlHelper.ExecuteReader("WebFile_Get",
                    new SqlParameter("@FileId", id)))
                {
                    if (r.Read())
                        return From(r);
                }

            return null;
        }

        public WebFile Get(int folderId, int objectId, int recordId)
        {
            if (folderId > 0 && objectId > 0 && recordId > 0)
                using (var r = SqlHelper.ExecuteReader("WebFile_Get",
                    new SqlParameter("@FolderId", folderId),
                    new SqlParameter("@ObjectId", objectId),
                    new SqlParameter("@RecordId", recordId)))
                {
                    if (r.Read())
                        return From(r);
                }

            return null;
        }

        public WebFile Get(int objectId, int recordId)
        {
            if (objectId > 0 && recordId > 0)
                using (var r = SqlHelper.ExecuteReader("WebFile_Get",
                    new SqlParameter("@ObjectId", objectId),
                    new SqlParameter("@RecordId", recordId)))
                {
                    if (r.Read())
                        return From(r);
                }

            return null;
        }

        private WebFile From(SqlDataReader r)
        {
            WebFile item = new WebFile();
            item.Id = DataHelper.GetId(r, "FileId");
            item.FolderId = DataHelper.GetId(r["FolderId"]);
            item.ObjectId = DataHelper.GetId(r["ObjectId"]);
            item.RecordId = DataHelper.GetId(r["RecordId"]);
            item.Name = r["Name"].ToString();

            return item;
        }

        public WebFile Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebFile> GetList()
        {
            List<WebFile> items = new List<WebFile>();
            using (var r = SqlHelper.ExecuteReader("WebFile_Get"))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebFile> GetList(int folderId)
        {
            List<WebFile> items = new List<WebFile>();
            using (var r = SqlHelper.ExecuteReader("WebFile_Get",
                new SqlParameter("@FolderId", folderId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebFile> GetList(int objectId, int recordId)
        {
            List<WebFile> items = new List<WebFile>();
            using (var r = SqlHelper.ExecuteReader("WebFile_Get",
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebFile> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(WebFile item)
        {
            var obj = SqlHelper.ExecuteScalar("WebFile_Set",
                new SqlParameter("@FileId", item.Id),
                new SqlParameter("@FolderId", item.FolderId),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@RecordId", item.RecordId),
                new SqlParameter("@Name", item.Name));

            item.Id = DataHelper.GetId(obj);
            return item.Id;
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public WebFile Refresh(WebFile item)
        {
            throw new NotImplementedException();
        }
    }
}
