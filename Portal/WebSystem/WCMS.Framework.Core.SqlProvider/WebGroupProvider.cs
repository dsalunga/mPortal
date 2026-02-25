using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebGroupProvider : IWebGroupProvider
    {
        public WebGroup Get(int id)
        {
            if (id > 0)
            {
                var sql = "SELECT * FROM WebGroup WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                    DbHelper.CreateParameter("@Id", id)))
                {
                    if (r.HasRows && r.Read())
                        return this.From(r);
                }
            }

            return null;
        }

        public WebGroup Get(int parentId, string name)
        {
            var sql = "SELECT * FROM WebGroup WHERE " + DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId AND " + DbSyntax.QuoteIdentifier("Name") + " = @Name";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ParentId", parentId),
                DbHelper.CreateParameter("@Name", name)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebGroup Get(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var sql = "SELECT * FROM WebGroup WHERE " + DbSyntax.QuoteIdentifier("Name") + " = @Name";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                    DbHelper.CreateParameter("@Name", name)))
                {
                    if (r.HasRows && r.Read())
                        return this.From(r);
                }
            }

            return null;
        }

        private WebGroup From(DbDataReader r)
        {
            WebGroup item = new WebGroup();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.ParentId = DataUtil.GetId(r, WebColumns.ParentId);
            item.IsSystem = DataUtil.GetInt32(r, "IsSystem");
            item.OwnerId = DataUtil.GetId(r, WebColumns.OwnerId);
            item.JoinApproval = DataUtil.GetInt32(r, "JoinApproval");
            item.JoinAlert = DataUtil.GetInt32(r, "JoinAlert");
            item.PageUrl = DataUtil.Get(r, "PageUrl");
            item.PageId = DataUtil.GetId(r, WebColumns.PageId);
            item.Description = DataUtil.Get(r, WebColumns.Description);
            item.DateModified = DataUtil.GetDateTime(r, WebColumns.DateModified);
            item.Managers = DataUtil.Get(r, "Managers");

            return item;
        }

        public IEnumerable<WebGroup> GetList()
        {
            List<WebGroup> items = new List<WebGroup>();

            var sql = "SELECT * FROM WebGroup";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebGroup> GetList(int parentId)
        {
            List<WebGroup> items = new List<WebGroup>();

            var sql = "SELECT * FROM WebGroup WHERE " + DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ParentId", parentId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public int Update(WebGroup item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebGroup SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("IsSystem") + " = @IsSystem, " +
                    DbSyntax.QuoteIdentifier("OwnerId") + " = @OwnerId, " +
                    DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId, " +
                    DbSyntax.QuoteIdentifier("JoinApproval") + " = @JoinApproval, " +
                    DbSyntax.QuoteIdentifier("JoinAlert") + " = @JoinAlert, " +
                    DbSyntax.QuoteIdentifier("PageUrl") + " = @PageUrl, " +
                    DbSyntax.QuoteIdentifier("PageId") + " = @PageId, " +
                    DbSyntax.QuoteIdentifier("Description") + " = @Description, " +
                    DbSyntax.QuoteIdentifier("Managers") + " = @Managers" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@IsSystem", item.IsSystem),
                    DbHelper.CreateParameter("@OwnerId", item.OwnerId),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@JoinApproval", item.JoinApproval),
                    DbHelper.CreateParameter("@JoinAlert", item.JoinAlert),
                    DbHelper.CreateParameter("@PageUrl", item.PageUrl),
                    DbHelper.CreateParameter("@PageId", item.PageId),
                    DbHelper.CreateParameter("@Description", item.Description),
                    DbHelper.CreateParameter("@Managers", item.Managers),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebGroup (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("IsSystem") + ", " +
                    DbSyntax.QuoteIdentifier("OwnerId") + ", " +
                    DbSyntax.QuoteIdentifier("ParentId") + ", " +
                    DbSyntax.QuoteIdentifier("JoinApproval") + ", " +
                    DbSyntax.QuoteIdentifier("JoinAlert") + ", " +
                    DbSyntax.QuoteIdentifier("PageUrl") + ", " +
                    DbSyntax.QuoteIdentifier("PageId") + ", " +
                    DbSyntax.QuoteIdentifier("Description") + ", " +
                    DbSyntax.QuoteIdentifier("Managers") +
                    ") VALUES (@Name, @IsSystem, @OwnerId, @ParentId, @JoinApproval, @JoinAlert, @PageUrl, @PageId, @Description, @Managers)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@IsSystem", item.IsSystem),
                    DbHelper.CreateParameter("@OwnerId", item.OwnerId),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@JoinApproval", item.JoinApproval),
                    DbHelper.CreateParameter("@JoinAlert", item.JoinAlert),
                    DbHelper.CreateParameter("@PageUrl", item.PageUrl),
                    DbHelper.CreateParameter("@PageId", item.PageId),
                    DbHelper.CreateParameter("@Description", item.Description),
                    DbHelper.CreateParameter("@Managers", item.Managers)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        public bool Delete(int id)
        {
            var sql = "DELETE FROM WebGroup WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id)
            );

            return true;
        }

        #region IDataProvider<WebGroup> Members


        public WebGroup Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebGroup> GetList(params QueryFilterElement[] filters)
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
            throw new NotImplementedException();
        }


        public WebGroup Refresh(WebGroup item)
        {
            throw new NotImplementedException();
        }
    }
}
