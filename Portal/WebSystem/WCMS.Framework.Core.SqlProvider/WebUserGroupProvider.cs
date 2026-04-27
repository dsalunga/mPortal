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
    public class WebUserGroupProvider : IWebUserGroupProvider
    {
        private static readonly object ColumnCacheSync = new object();
        private static HashSet<string> _tableColumns;

        public WebUserGroup Get(int userRoleId)
        {
            var sql = "SELECT * FROM WebUserGroup WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", userRoleId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebUserGroup Get(int roleId, int id, bool isGroup = false)
        {
            var sql = "SELECT * FROM WebUserGroup WHERE " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId AND " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " + DbSyntax.QuoteIdentifier("GroupId") + " = @GroupId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@RecordId", id),
                DbHelper.CreateParameter("@ObjectId", isGroup ? WebObjects.WebGroup : WebObjects.WebUser),
                DbHelper.CreateParameter("@GroupId", roleId)
                ))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<WebUserGroup> GetList()
        {
            var items = new List<WebUserGroup>();
            var sql = "SELECT * FROM WebUserGroup";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                        items.Add(this.From(r));
                }
            }

            return items;
        }

        public IEnumerable<WebUserGroup> GetList(int active)
        {
            var items = new List<WebUserGroup>();
            var sql = "SELECT * FROM WebUserGroup WHERE " + DbSyntax.QuoteIdentifier("Active") + " = @Active";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Active", active)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebUserGroup> GetByUserId(int userId, int active)
        {
            var items = new List<WebUserGroup>();
            var sql = "SELECT * FROM WebUserGroup WHERE " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId AND " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " + DbSyntax.QuoteIdentifier("Active") + " = @Active";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@RecordId", userId),
                DbHelper.CreateParameter("@ObjectId", WebObjects.WebUser),
                DbHelper.CreateParameter("@Active", active)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebUserGroup> GetByGroupId(int groupId, int active)
        {
            var items = new List<WebUserGroup>();
            var sql = "SELECT * FROM WebUserGroup WHERE " + DbSyntax.QuoteIdentifier("GroupId") + " = @GroupId AND " + DbSyntax.QuoteIdentifier("Active") + " = @Active";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@GroupId", groupId),
                DbHelper.CreateParameter("@Active", active)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebUserGroup> GetByCreatedById(int groupId, int createdById, int active)
        {
            if (!HasTableColumn("CreatedById"))
                return GetByGroupId(groupId, active);

            var items = new List<WebUserGroup>();
            var sql = "SELECT * FROM WebUserGroup WHERE " + DbSyntax.QuoteIdentifier("GroupId") + " = @GroupId AND " + DbSyntax.QuoteIdentifier("Active") + " = @Active AND " + DbSyntax.QuoteIdentifier("CreatedById") + " = @CreatedById";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@GroupId", groupId),
                DbHelper.CreateParameter("@Active", active),
                DbHelper.CreateParameter("@CreatedById", createdById)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public int Update(WebUserGroup item)
        {
            string sql;
            List<DbParameter> parms;
            var includeCreatedById = HasTableColumn("CreatedById");

            if (item.Id > 0)
            {
                var setParts = new List<string>
                {
                    DbSyntax.QuoteIdentifier("UserId") + " = @UserId",
                    DbSyntax.QuoteIdentifier("GroupId") + " = @GroupId",
                    DbSyntax.QuoteIdentifier("Active") + " = @Active",
                    DbSyntax.QuoteIdentifier("DateJoined") + " = @DateJoined",
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId",
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId",
                    DbSyntax.QuoteIdentifier("Remarks") + " = @Remarks"
                };
                if (includeCreatedById)
                    setParts.Add(DbSyntax.QuoteIdentifier("CreatedById") + " = @CreatedById");

                sql = "UPDATE WebUserGroup SET " +
                    string.Join(", ", setParts) +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new List<DbParameter>
                {
                    DbHelper.CreateParameter("@UserId", -1),
                    DbHelper.CreateParameter("@GroupId", item.GroupId),
                    DbHelper.CreateParameter("@Active", item.Active),
                    DbHelper.CreateParameter("@DateJoined", item.DateJoined),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@Remarks", item.Remarks)
                };
                if (includeCreatedById)
                    parms.Add(DbHelper.CreateParameter("@CreatedById", item.CreatedById));
                parms.Add(DbHelper.CreateParameter("@Id", item.Id));
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms.ToArray());
            }
            else
            {
                var insertColumns = new List<string>
                {
                    DbSyntax.QuoteIdentifier("UserId"),
                    DbSyntax.QuoteIdentifier("GroupId"),
                    DbSyntax.QuoteIdentifier("Active"),
                    DbSyntax.QuoteIdentifier("DateJoined"),
                    DbSyntax.QuoteIdentifier("ObjectId"),
                    DbSyntax.QuoteIdentifier("RecordId"),
                    DbSyntax.QuoteIdentifier("Remarks")
                };
                var insertValues = new List<string>
                {
                    "@UserId",
                    "@GroupId",
                    "@Active",
                    "@DateJoined",
                    "@ObjectId",
                    "@RecordId",
                    "@Remarks"
                };
                if (includeCreatedById)
                {
                    insertColumns.Add(DbSyntax.QuoteIdentifier("CreatedById"));
                    insertValues.Add("@CreatedById");
                }

                sql = "INSERT INTO WebUserGroup (" +
                    string.Join(", ", insertColumns) +
                    ") VALUES (" +
                    string.Join(", ", insertValues) +
                    ")";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new List<DbParameter>
                {
                    DbHelper.CreateParameter("@UserId", -1),
                    DbHelper.CreateParameter("@GroupId", item.GroupId),
                    DbHelper.CreateParameter("@Active", item.Active),
                    DbHelper.CreateParameter("@DateJoined", item.DateJoined),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@Remarks", item.Remarks)
                };
                if (includeCreatedById)
                    parms.Add(DbHelper.CreateParameter("@CreatedById", item.CreatedById));

                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms.ToArray());
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        public bool Delete(int userRoleId)
        {
            var sql = "DELETE FROM WebUserGroup WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", userRoleId)
            );

            return true;
        }

        public bool Delete(int userId, int groupId)
        {
            var sql = "DELETE FROM WebUserGroup WHERE " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId AND " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " + DbSyntax.QuoteIdentifier("GroupId") + " = @GroupId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@RecordId", userId),
                DbHelper.CreateParameter("@ObjectId", WebObjects.WebUser),
                DbHelper.CreateParameter("@GroupId", groupId)
            );

            return true;
        }

        public bool Delete(int groupId, int objectId, int recordId)
        {
            var sql = "DELETE FROM WebUserGroup WHERE " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId AND " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " + DbSyntax.QuoteIdentifier("GroupId") + " = @GroupId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@GroupId", groupId)
            );

            return true;
        }

        private WebUserGroup From(DbDataReader r)
        {
            var hasCreatedById = HasColumn(r, "CreatedById");
            var item = new WebUserGroup();
            item.Id = DataUtil.GetId(r["Id"]);
            item.GroupId = DataUtil.GetId(r["GroupId"]);
            //item.UserId = DataHelper.GetId(r["UserId"]);
            item.Active = DataUtil.GetInt32(r, "Active");
            item.DateJoined = DataUtil.GetDateTime(r, "DateJoined");
            item.RecordId = DataUtil.GetId(r, WebColumns.RecordId);
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.Remarks = DataUtil.Get(r, "Remarks");
            item.CreatedById = hasCreatedById ? DataUtil.GetId(r, "CreatedById") : -1;

            return item;
        }

        private static bool HasColumn(DbDataReader reader, string columnName)
        {
            for (var i = 0; i < reader.FieldCount; i++)
            {
                if (string.Equals(reader.GetName(i), columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        private static bool HasTableColumn(string columnName)
        {
            EnsureTableColumns();
            return _tableColumns != null && _tableColumns.Contains(columnName);
        }

        private static void EnsureTableColumns()
        {
            if (_tableColumns != null)
                return;

            lock (ColumnCacheSync)
            {
                if (_tableColumns != null)
                    return;

                var columns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                using (var reader = DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM WebUserGroup WHERE 1 = 0"))
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                        columns.Add(reader.GetName(i));
                }

                _tableColumns = columns;
            }
        }

        #region IDataProvider<WebUserGroup> Members


        public WebUserGroup Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebUserGroup> GetList(params QueryFilterElement[] filters)
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


        public WebUserGroup Refresh(WebUserGroup item)
        {
            throw new NotImplementedException();
        }
    }
}
