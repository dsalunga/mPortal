using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public class FileIdentityProvider : IFileIdentityProvider
    {
        #region IDataProvider<FileIdentity> Members

        public bool Delete(int id)
        {
            SqlHelper.ExecuteNonQuery("FileIdentity_Del",
                new SqlParameter("@Id", id));

            return true;
        }

        public FileIdentity Get(int id)
        {
            using (var r = SqlHelper.ExecuteReader("FileIdentity_Get",
                new SqlParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public FileIdentity Get(string filePath, string name, int objectId, int recordId)
        {
            using (var r = SqlHelper.ExecuteReader("FileIdentity_Get",
                new SqlParameter("@FilePath", filePath),
                new SqlParameter("@Name", name),
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        private FileIdentity From(SqlDataReader r)
        {
            FileIdentity item = new FileIdentity();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.ObjectId = DataHelper.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataHelper.GetId(r, WebColumns.RecordId);
            item.LibraryId = DataHelper.GetId(r, "LibraryId");
            item.FilePath = DataHelper.Get(r, "FilePath");
            item.Name = DataHelper.Get(r, WebColumns.Name);

            return item;
        }

        public FileIdentity Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileIdentity> GetList()
        {
            List<FileIdentity> items = new List<FileIdentity>();

            using (var r = SqlHelper.ExecuteReader("FileIdentity_Get"))
            {
                while (r.Read())
                {
                    items.Add(From(r));
                }
            }

            return items;
        }

        public IEnumerable<FileIdentity> GetList(string filePath, int objectId, int recordId)
        {
            List<FileIdentity> items = new List<FileIdentity>();

            using (var r = SqlHelper.ExecuteReader("FileIdentity_Get",
                new SqlParameter("@FilePath", filePath),
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId)))
            {
                while (r.Read())
                {
                    items.Add(From(r));
                }
            }

            return items;
        }

        public IEnumerable<FileIdentity> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(FileIdentity item)
        {
            var obj = SqlHelper.ExecuteScalar("FileIdentity_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@RecordId", item.RecordId),
                new SqlParameter("@LibraryId", item.LibraryId),
                new SqlParameter("@FilePath", item.FilePath),
                new SqlParameter("@Name", item.Name));

            item.Id = DataHelper.GetId(obj);
            return item.Id;
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public FileIdentity Refresh(FileIdentity item)
        {
            throw new NotImplementedException();
        }
    }
}
