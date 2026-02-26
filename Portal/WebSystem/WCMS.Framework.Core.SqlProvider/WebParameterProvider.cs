using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebParameterProvider : GenericSqlDataProviderBase<WebParameter>, IWebParameterProvider
    {
        protected override string TableName { get { return "WebParameter"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "WebParameter_Get"; } }
        protected override string DeleteProcedure { get { return "WebParameter_Del"; } }

        protected override WebParameter From(IDataReader r, WebParameter source)
        {
            WebParameter item = source ?? new WebParameter();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataUtil.GetId(r, WebColumns.RecordId);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.Value = DataUtil.Get(r, WebColumns.Value);
            item.IsRequired = DataUtil.GetInt32(r, "IsRequired");

            return item;
        }

        public override int Update(WebParameter item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebParameter SET " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId, " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId, " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("Value") + " = @Value, " +
                    DbSyntax.QuoteIdentifier("IsRequired") + " = @IsRequired" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Value", item.Value),
                    DbHelper.CreateParameter("@IsRequired", item.IsRequired),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebParameter (" +
                    DbSyntax.QuoteIdentifier("ObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("RecordId") + ", " +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("Value") + ", " +
                    DbSyntax.QuoteIdentifier("IsRequired") +
                    ") VALUES (@ObjectId, @RecordId, @Name, @Value, @IsRequired)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Value", item.Value),
                    DbHelper.CreateParameter("@IsRequired", item.IsRequired)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                return UpdatePostProcess(item, obj);
            }

            return UpdatePostProcess(item, item.Id);
        }

        #region IWebParameterProvider Members

        public IEnumerable<WebParameter> GetList(int objectId, int recordId)
        {
            List<WebParameter> items = new List<WebParameter>();

            var sql = "SELECT * FROM WebParameter WHERE " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public WebParameter Get(int objectId, int recordId, string name)
        {
            var sql = "SELECT * FROM WebParameter WHERE " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId AND " + DbSyntax.QuoteIdentifier("Name") + " = @Name";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@Name", name)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        #endregion
    }
}
