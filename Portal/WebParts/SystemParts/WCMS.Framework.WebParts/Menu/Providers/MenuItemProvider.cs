using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;


namespace WCMS.WebSystem.WebParts.Menu.Providers
{
    public class MenuItemProvider : GenericSqlDataProviderBase<MenuItem>, IMenuItemProvider
    {
        #region IDataProvider<MenuItem> Members

        protected override MenuItem From(IDataReader r, MenuItem source)
        {
            MenuItem item = source ?? new MenuItem();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.NavigateUrl = r["NavigateUrl"].ToString();
            item.Text = r["Text"].ToString();
            item.Target = DataUtil.Get(r, "Target");
            item.ParentId = DataUtil.GetId(r, WebColumns.ParentId);
            item.MenuId = DataUtil.GetId(r, "MenuId");
            item.Active = DataUtil.GetInt32(r, "IsActive");
            item.Rank = DataUtil.GetInt32(r, WebColumns.Rank);
            item.PageId = DataUtil.GetId(r, WebColumns.PageId);
            item.Type = DataUtil.GetInt32(r, "Type");
            item.CheckPermission = DataUtil.GetInt32(r, "CheckPermission");

            return item;
        }

        public IEnumerable<MenuItem> GetList(int menuId = -2, int parentId = -2, int pageId = -2)
        {
            List<MenuItem> items = new List<MenuItem>();
            var conditions = new List<string>();
            var parameters = new List<DbParameter>();

            if (menuId != -2)
            {
                conditions.Add(DbSyntax.QuoteIdentifier("MenuId") + " = @MenuId");
                parameters.Add(DbHelper.CreateParameter("@MenuId", menuId));
            }
            if (parentId != -2)
            {
                conditions.Add(DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId");
                parameters.Add(DbHelper.CreateParameter("@ParentId", parentId));
            }
            if (pageId != -2)
            {
                conditions.Add(DbSyntax.QuoteIdentifier("PageId") + " = @PageId");
                parameters.Add(DbHelper.CreateParameter("@PageId", pageId));
            }

            var sql = "SELECT * FROM MenuItem";
            if (conditions.Count > 0)
                sql += " WHERE " + string.Join(" AND ", conditions);

            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql, parameters.ToArray()))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public override int Update(MenuItem item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE MenuItem SET " +
                    DbSyntax.QuoteIdentifier("MenuId") + " = @MenuId, " +
                    DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId, " +
                    DbSyntax.QuoteIdentifier("Text") + " = @Text, " +
                    DbSyntax.QuoteIdentifier("NavigateURL") + " = @NavigateURL, " +
                    DbSyntax.QuoteIdentifier("Target") + " = @Target, " +
                    DbSyntax.QuoteIdentifier("IsActive") + " = @IsActive, " +
                    DbSyntax.QuoteIdentifier("Rank") + " = @Rank, " +
                    DbSyntax.QuoteIdentifier("PageId") + " = @PageId, " +
                    DbSyntax.QuoteIdentifier("Type") + " = @Type, " +
                    DbSyntax.QuoteIdentifier("CheckPermission") + " = @CheckPermission" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@MenuId", item.MenuId),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@Text", item.Text),
                    DbHelper.CreateParameter("@NavigateURL", item.NavigateUrl),
                    DbHelper.CreateParameter("@Target", item.Target),
                    DbHelper.CreateParameter("@IsActive", item.Active),
                    DbHelper.CreateParameter("@Rank", item.Rank),
                    DbHelper.CreateParameter("@PageId", item.PageId),
                    DbHelper.CreateParameter("@Type", item.Type),
                    DbHelper.CreateParameter("@CheckPermission", item.CheckPermission),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO MenuItem (" +
                    DbSyntax.QuoteIdentifier("MenuId") + ", " +
                    DbSyntax.QuoteIdentifier("ParentId") + ", " +
                    DbSyntax.QuoteIdentifier("Text") + ", " +
                    DbSyntax.QuoteIdentifier("NavigateURL") + ", " +
                    DbSyntax.QuoteIdentifier("Target") + ", " +
                    DbSyntax.QuoteIdentifier("IsActive") + ", " +
                    DbSyntax.QuoteIdentifier("Rank") + ", " +
                    DbSyntax.QuoteIdentifier("PageId") + ", " +
                    DbSyntax.QuoteIdentifier("Type") + ", " +
                    DbSyntax.QuoteIdentifier("CheckPermission") +
                    ") VALUES (@MenuId, @ParentId, @Text, @NavigateURL, @Target, @IsActive, @Rank, @PageId, @Type, @CheckPermission)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@MenuId", item.MenuId),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@Text", item.Text),
                    DbHelper.CreateParameter("@NavigateURL", item.NavigateUrl),
                    DbHelper.CreateParameter("@Target", item.Target),
                    DbHelper.CreateParameter("@IsActive", item.Active),
                    DbHelper.CreateParameter("@Rank", item.Rank),
                    DbHelper.CreateParameter("@PageId", item.PageId),
                    DbHelper.CreateParameter("@Type", item.Type),
                    DbHelper.CreateParameter("@CheckPermission", item.CheckPermission)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        #endregion

        protected override string TableName { get { return "MenuItem"; } }


        protected override string IdColumn { get { return "Id"; } }



        protected override string SelectProcedure { get { return "MenuItem_Get"; } }
        protected override string DeleteProcedure { get { return "MenuItem_Del"; } }
    }
}
