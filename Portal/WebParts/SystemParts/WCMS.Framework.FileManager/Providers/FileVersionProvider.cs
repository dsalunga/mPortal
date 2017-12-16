using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public class FileVersionProvider : IFileVersionProvider
    {
        #region IDataProvider<FileVersion> Members

        public bool Delete(int id)
        {
            SqlHelper.ExecuteNonQuery("FileVersion_Del",
                new SqlParameter("@Id", id));

            return true;
        }

        public FileVersion Get(int id)
        {
            using (var r = SqlHelper.ExecuteReader("FileVersion_Get",
                new SqlParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        private FileVersion From(SqlDataReader r)
        {
            FileVersion item = new FileVersion();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.FileId = DataHelper.GetId(r, "FileId");
            item.VersionDate = DataHelper.GetDateTime(r, "VersionDate");
            item.Activity = DataHelper.GetInt32(r, "Activity");
            item.UserId = DataHelper.GetId(r, WebColumns.UserId);

            return item;
        }

        public FileVersion Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileVersion> GetList()
        {
            List<FileVersion> items = new List<FileVersion>();

            using (var r = SqlHelper.ExecuteReader("FileVersion_Get"))
            {
                while (r.Read())
                {
                    items.Add(From(r));
                }
            }

            return items;
        }

        public IEnumerable<FileVersion> GetList(int fileId)
        {
            List<FileVersion> items = new List<FileVersion>();

            if (fileId > 0)
            {
                using (var r = SqlHelper.ExecuteReader("FileVersion_Get",
                    new SqlParameter("@FileId", fileId)))
                {
                    while (r.Read())
                    {
                        items.Add(From(r));
                    }
                }
            }

            return items;
        }

        public IEnumerable<FileVersion> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(FileVersion item)
        {
            var obj = SqlHelper.ExecuteScalar("FileVersion_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@FileId", item.FileId),
                new SqlParameter("@VersionDate", item.VersionDate),
                new SqlParameter("@Activity", item.Activity),
                new SqlParameter("@UserId", item.UserId));

            item.Id = DataHelper.GetId(obj);
            return item.Id;
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public FileVersion Refresh(FileVersion item)
        {
            throw new NotImplementedException();
        }
    }
}
