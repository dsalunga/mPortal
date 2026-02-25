using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
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
                DbHelper.ExecuteNonQuery("WebFile_Del",
                    DbHelper.CreateParameter("@FileId", id));

            return true;
        }

        public WebFile Get(int id)
        {
            if (id > 0)
                using (var r = DbHelper.ExecuteReader("WebFile_Get",
                    DbHelper.CreateParameter("@FileId", id)))
                {
                    if (r.Read())
                        return From(r);
                }

            return null;
        }

        public WebFile Get(int folderId, int objectId, int recordId)
        {
            if (folderId > 0 && objectId > 0 && recordId > 0)
                using (var r = DbHelper.ExecuteReader("WebFile_Get",
                    DbHelper.CreateParameter("@FolderId", folderId),
                    DbHelper.CreateParameter("@ObjectId", objectId),
                    DbHelper.CreateParameter("@RecordId", recordId)))
                {
                    if (r.Read())
                        return From(r);
                }

            return null;
        }

        public WebFile Get(int objectId, int recordId)
        {
            if (objectId > 0 && recordId > 0)
                using (var r = DbHelper.ExecuteReader("WebFile_Get",
                    DbHelper.CreateParameter("@ObjectId", objectId),
                    DbHelper.CreateParameter("@RecordId", recordId)))
                {
                    if (r.Read())
                        return From(r);
                }

            return null;
        }

        private WebFile From(DbDataReader r)
        {
            WebFile item = new WebFile();
            item.Id = DataUtil.GetId(r, "FileId");
            item.FolderId = DataUtil.GetId(r["FolderId"]);
            item.ObjectId = DataUtil.GetId(r["ObjectId"]);
            item.RecordId = DataUtil.GetId(r["RecordId"]);
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
            using (var r = DbHelper.ExecuteReader("WebFile_Get"))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebFile> GetList(int folderId)
        {
            List<WebFile> items = new List<WebFile>();
            using (var r = DbHelper.ExecuteReader("WebFile_Get",
                DbHelper.CreateParameter("@FolderId", folderId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebFile> GetList(int objectId, int recordId)
        {
            List<WebFile> items = new List<WebFile>();
            using (var r = DbHelper.ExecuteReader("WebFile_Get",
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId)))
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
            var obj = DbHelper.ExecuteScalar("WebFile_Set",
                DbHelper.CreateParameter("@FileId", item.Id),
                DbHelper.CreateParameter("@FolderId", item.FolderId),
                DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                DbHelper.CreateParameter("@RecordId", item.RecordId),
                DbHelper.CreateParameter("@Name", item.Name));

            item.Id = DataUtil.GetId(obj);
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
