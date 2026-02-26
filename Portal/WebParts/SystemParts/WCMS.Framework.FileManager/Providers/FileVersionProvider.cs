using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Common;

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
            var sql = "DELETE FROM " + DbSyntax.QuoteIdentifier("FileVersion") +
                " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id));

            return true;
        }

        public FileVersion Get(int id)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("FileVersion") +
                " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        private FileVersion From(IDataReader r)
        {
            FileVersion item = new FileVersion();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.FileId = DataUtil.GetId(r, "FileId");
            item.VersionDate = DataUtil.GetDateTime(r, "VersionDate");
            item.Activity = DataUtil.GetInt32(r, "Activity");
            item.UserId = DataUtil.GetId(r, WebColumns.UserId);

            return item;
        }

        public FileVersion Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileVersion> GetList()
        {
            List<FileVersion> items = new List<FileVersion>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("FileVersion");
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql))
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
                var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("FileVersion") +
                    " WHERE " + DbSyntax.QuoteIdentifier("FileId") + " = @FileId";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                    DbHelper.CreateParameter("@FileId", fileId)))
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
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("FileVersion") + " SET " +
                    DbSyntax.QuoteIdentifier("FileId") + " = @FileId, " +
                    DbSyntax.QuoteIdentifier("VersionDate") + " = @VersionDate, " +
                    DbSyntax.QuoteIdentifier("Activity") + " = @Activity, " +
                    DbSyntax.QuoteIdentifier("UserId") + " = @UserId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@FileId", item.FileId),
                    DbHelper.CreateParameter("@VersionDate", item.VersionDate),
                    DbHelper.CreateParameter("@Activity", item.Activity),
                    DbHelper.CreateParameter("@UserId", item.UserId),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("FileVersion") + " (" +
                    DbSyntax.QuoteIdentifier("FileId") + ", " +
                    DbSyntax.QuoteIdentifier("VersionDate") + ", " +
                    DbSyntax.QuoteIdentifier("Activity") + ", " +
                    DbSyntax.QuoteIdentifier("UserId") +
                    ") VALUES (@FileId, @VersionDate, @Activity, @UserId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@FileId", item.FileId),
                    DbHelper.CreateParameter("@VersionDate", item.VersionDate),
                    DbHelper.CreateParameter("@Activity", item.Activity),
                    DbHelper.CreateParameter("@UserId", item.UserId)
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


        public FileVersion Refresh(FileVersion item)
        {
            throw new NotImplementedException();
        }
    }
}
