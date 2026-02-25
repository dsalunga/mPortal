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
    public class WebAttachmentProvider : GenericSqlDataProviderBase<WebAttachment>, IWebAttachmentProvider
    {
        protected override string TableName { get { return "WebAttachment"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "WebAttachment_Get"; } }
        protected override string DeleteProcedure { get { return "WebAttachment_Del"; } }

        protected override WebAttachment From(IDataReader r, WebAttachment source)
        {
            WebAttachment item = source ?? new WebAttachment();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.FilePath = DataUtil.Get(r, "FilePath");
            item.Size = DataUtil.GetInt64(r, WebColumns.Size);
            item.DateUploaded = DataUtil.GetDateTime(r, "DateUploaded");
            item.UserId = DataUtil.GetId(r, WebColumns.UserId);
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataUtil.GetId(r, WebColumns.RecordId);
            item.BatchGuid = DataUtil.Get(r, "BatchGuid");

            return item;
        }

        public override int Update(WebAttachment item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebAttachment SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name" + ", " +
                    DbSyntax.QuoteIdentifier("FilePath") + " = @FilePath" + ", " +
                    DbSyntax.QuoteIdentifier("Size") + " = @Size" + ", " +
                    DbSyntax.QuoteIdentifier("DateUploaded") + " = @DateUploaded" + ", " +
                    DbSyntax.QuoteIdentifier("UserId") + " = @UserId" + ", " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId" + ", " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId" + ", " +
                    DbSyntax.QuoteIdentifier("BatchGuid") + " = @BatchGuid" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@FilePath", item.FilePath),
                    DbHelper.CreateParameter("@Size", item.Size),
                    DbHelper.CreateParameter("@DateUploaded", item.DateUploaded),
                    DbHelper.CreateParameter("@UserId", item.UserId),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@BatchGuid", item.BatchGuid),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebAttachment (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("FilePath") + ", " +
                    DbSyntax.QuoteIdentifier("Size") + ", " +
                    DbSyntax.QuoteIdentifier("DateUploaded") + ", " +
                    DbSyntax.QuoteIdentifier("UserId") + ", " +
                    DbSyntax.QuoteIdentifier("ObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("RecordId") + ", " +
                    DbSyntax.QuoteIdentifier("BatchGuid") +
                    ") VALUES (@Name, @FilePath, @Size, @DateUploaded, @UserId, @ObjectId, @RecordId, @BatchGuid)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@FilePath", item.FilePath),
                    DbHelper.CreateParameter("@Size", item.Size),
                    DbHelper.CreateParameter("@DateUploaded", item.DateUploaded),
                    DbHelper.CreateParameter("@UserId", item.UserId),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@BatchGuid", item.BatchGuid)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        #region IWebAttachmentProvider Members

        public IEnumerable<WebAttachment> GetList(int userId = -2, int objectId = -2, int recordId = -2)
        {
            List<WebAttachment> items = new List<WebAttachment>();

            var sql = "SELECT * FROM WebAttachment WHERE " +
                    DbSyntax.QuoteIdentifier("UserId") + " = @UserId AND " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@UserId", userId),
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebAttachment> GetList(string batchGuid)
        {
            List<WebAttachment> items = new List<WebAttachment>();

            var sql = "SELECT * FROM WebAttachment WHERE " + DbSyntax.QuoteIdentifier("BatchGuid") + " = @BatchGuid";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@BatchGuid", batchGuid)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        #endregion
    }
}
