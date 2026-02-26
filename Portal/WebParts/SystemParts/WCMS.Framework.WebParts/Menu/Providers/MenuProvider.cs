using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;

using WCMS.Framework.Core;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Menu.Providers
{
    public class MenuProvider : GenericSqlDataProviderBase<MenuEntity>, IMenuProvider
    {
        protected override string TableName { get { return "Menu"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "Menu_Get"; } }
        protected override string DeleteProcedure { get { return "Menu_Del"; } }

        protected override MenuEntity From(IDataReader r, MenuEntity source)
        {
            MenuEntity item = source ?? new MenuEntity();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.IsActive = DataUtil.GetInt32(r, "IsActive");
            item.DateCreated = DataUtil.GetDateTime(r, "DateCreated");
            item.SiteId = DataUtil.GetId(r, WebColumns.SiteId);
            item.UserId = DataUtil.GetId(r, WebColumns.UserId);
            item.PageId = DataUtil.GetId(r, WebColumns.PageId);
            item.IncludeChildren = DataUtil.GetInt32(r, "IncludeChildren");

            return item;
        }

        public override int Update(MenuEntity item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE Menu SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("IsActive") + " = @IsActive, " +
                    DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId, " +
                    DbSyntax.QuoteIdentifier("UserId") + " = @UserId, " +
                    DbSyntax.QuoteIdentifier("PageId") + " = @PageId, " +
                    DbSyntax.QuoteIdentifier("IncludeChildren") + " = @IncludeChildren" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@IsActive", item.IsActive),
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@UserId", item.UserId),
                    DbHelper.CreateParameter("@PageId", item.PageId),
                    DbHelper.CreateParameter("@IncludeChildren", item.IncludeChildren),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO Menu (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("IsActive") + ", " +
                    DbSyntax.QuoteIdentifier("SiteId") + ", " +
                    DbSyntax.QuoteIdentifier("UserId") + ", " +
                    DbSyntax.QuoteIdentifier("PageId") + ", " +
                    DbSyntax.QuoteIdentifier("IncludeChildren") +
                    ") VALUES (@Name, @IsActive, @SiteId, @UserId, @PageId, @IncludeChildren)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@IsActive", item.IsActive),
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@UserId", item.UserId),
                    DbHelper.CreateParameter("@PageId", item.PageId),
                    DbHelper.CreateParameter("@IncludeChildren", item.IncludeChildren)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }
    }
}
