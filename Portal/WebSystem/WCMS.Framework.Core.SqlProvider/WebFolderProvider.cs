using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebFolderProvider : IWebFolderProvider
    {
        #region IDataProvider<WebFolder> Members

        public bool Delete(int id)
        {
            var sql = "DELETE FROM WebFolder WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id));

            return true;
        }

        public WebFolder Get(int id)
        {
            var sql = "SELECT * FROM WebFolder WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public WebFolder Get(int parentId, string name)
        {
            var sql = "SELECT * FROM WebFolder WHERE " + DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId AND " + DbSyntax.QuoteIdentifier("Name") + " = @Name";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ParentId", parentId),
                DbHelper.CreateParameter("@Name", name)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        private WebFolder From(DbDataReader r)
        {
            WebFolder item = new WebFolder();
            item.Id = DataUtil.GetId(r["Id"]);
            item.Name = r["Name"].ToString();
            item.ParentId = DataUtil.GetId(r["ParentId"]);
            item.ShareName = r["ShareName"].ToString();
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.SiteId = DataUtil.GetId(r, WebColumns.SiteId);

            return item;
        }

        public WebFolder Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebFolder> GetList()
        {
            List<WebFolder> items = new List<WebFolder>();

            var sql = "SELECT * FROM WebFolder";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebFolder> GetList(int parentId)
        {
            List<WebFolder> items = new List<WebFolder>();

            var sql = "SELECT * FROM WebFolder WHERE " + DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ParentId", parentId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebFolder> GetList(int objectId, int siteId)
        {
            List<WebFolder> items = new List<WebFolder>();

            var sql = "SELECT * FROM WebFolder WHERE " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " + DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@SiteId", siteId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebFolder> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(WebFolder item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebFolder SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId, " +
                    DbSyntax.QuoteIdentifier("ShareName") + " = @ShareName, " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId, " +
                    DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@ShareName", item.ShareName),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebFolder (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("ParentId") + ", " +
                    DbSyntax.QuoteIdentifier("ShareName") + ", " +
                    DbSyntax.QuoteIdentifier("ObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("SiteId") +
                    ") VALUES (@Name, @ParentId, @ShareName, @ObjectId, @SiteId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@ShareName", item.ShareName),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@SiteId", item.SiteId)
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


        public WebFolder Refresh(WebFolder item)
        {
            throw new NotImplementedException();
        }
    }
}
