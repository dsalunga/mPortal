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
    public class WebSkinProvider : GenericSqlDataProviderBase<WebSkin>, IWebSkinProvider
    {
        protected override string TableName { get { return "WebSkin"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "WebSkin_Get"; } }
        protected override string DeleteProcedure { get { return "WebSkin_Del"; } }

        protected override WebSkin From(IDataReader r, WebSkin source)
        {
            WebSkin item = source ?? new WebSkin();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataUtil.GetId(r, WebColumns.RecordId);
            item.Rank = DataUtil.GetInt32(r, WebColumns.Rank);

            return item;
        }

        public override int Update(WebSkin item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebSkin SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId, " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId, " +
                    DbSyntax.QuoteIdentifier("Rank") + " = @Rank" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@Rank", item.Rank),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebSkin (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("ObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("RecordId") + ", " +
                    DbSyntax.QuoteIdentifier("Rank") +
                    ") VALUES (@Name, @ObjectId, @RecordId, @Rank)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@Rank", item.Rank)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        #region IWebThemeProvider Members

        public IEnumerable<WebSkin> GetList(int objectId, int recordId)
        {
            List<WebSkin> items = new List<WebSkin>();

            if (objectId > -2 || recordId > -2)
            {
                var sql = "SELECT * FROM WebSkin WHERE " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                    DbHelper.CreateParameter("@ObjectId", objectId),
                    DbHelper.CreateParameter("@RecordId", recordId)
                ))
                {
                    while (r.Read())
                        items.Add(From(r));
                }
            }

            return items;
        }

        #endregion
    }
}
