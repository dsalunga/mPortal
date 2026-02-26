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
    public class MenuObjectProvider : GenericSqlDataProviderBase<MenuObject>, IMenuObjectProvider
    {
        protected override string TableName { get { return "MenuObject"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "MenuObject_Get"; } }
        protected override string DeleteProcedure { get { return "MenuObject_Del"; } }

        public MenuObject Get(int objectId, int recordId)
        {
            var sql = "SELECT * FROM MenuObject WHERE " +
                DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " +
                DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        protected override MenuObject From(IDataReader r, MenuObject source)
        {
            MenuObject item = source ?? new MenuObject();

            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.RecordId = DataUtil.GetId(r, WebColumns.RecordId);
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.Width = DataUtil.GetInt32(r, "Width");
            item.Height = DataUtil.GetInt32(r, "Height");
            item.Horizontal = DataUtil.GetInt32(r, "Horizontal");
            item.MenuId = DataUtil.GetId(r, "MenuId");
            item.ParameterSetId = DataUtil.GetId(r, "ParameterSetId");
            item.RenderMode = DataUtil.GetInt32(r, "RenderMode");

            return item;
        }

        public override int Update(MenuObject item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE MenuObject SET " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId, " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId, " +
                    DbSyntax.QuoteIdentifier("Width") + " = @Width, " +
                    DbSyntax.QuoteIdentifier("Height") + " = @Height, " +
                    DbSyntax.QuoteIdentifier("Horizontal") + " = @Horizontal, " +
                    DbSyntax.QuoteIdentifier("MenuId") + " = @MenuId, " +
                    DbSyntax.QuoteIdentifier("ParameterSetId") + " = @ParameterSetId, " +
                    DbSyntax.QuoteIdentifier("RenderMode") + " = @RenderMode" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@Width", item.Width),
                    DbHelper.CreateParameter("@Height", item.Height),
                    DbHelper.CreateParameter("@Horizontal", item.Horizontal),
                    DbHelper.CreateParameter("@MenuId", item.MenuId),
                    DbHelper.CreateParameter("@ParameterSetId", item.ParameterSetId),
                    DbHelper.CreateParameter("@RenderMode", item.RenderMode),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO MenuObject (" +
                    DbSyntax.QuoteIdentifier("ObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("RecordId") + ", " +
                    DbSyntax.QuoteIdentifier("Width") + ", " +
                    DbSyntax.QuoteIdentifier("Height") + ", " +
                    DbSyntax.QuoteIdentifier("Horizontal") + ", " +
                    DbSyntax.QuoteIdentifier("MenuId") + ", " +
                    DbSyntax.QuoteIdentifier("ParameterSetId") + ", " +
                    DbSyntax.QuoteIdentifier("RenderMode") +
                    ") VALUES (@ObjectId, @RecordId, @Width, @Height, @Horizontal, @MenuId, @ParameterSetId, @RenderMode)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@Width", item.Width),
                    DbHelper.CreateParameter("@Height", item.Height),
                    DbHelper.CreateParameter("@Horizontal", item.Horizontal),
                    DbHelper.CreateParameter("@MenuId", item.MenuId),
                    DbHelper.CreateParameter("@ParameterSetId", item.ParameterSetId),
                    DbHelper.CreateParameter("@RenderMode", item.RenderMode)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }
    }
}
