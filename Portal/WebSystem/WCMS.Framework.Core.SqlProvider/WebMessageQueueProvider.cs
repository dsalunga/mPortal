using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;

using WCMS.Framework.Net;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebMessageQueueProvider : GenericSqlDataProviderBase<WebMessageQueue>, IWebMessageQueueProvider
    {
        protected override string TableName { get { return "WebMessageQueue"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "WebMessageQueue_Get"; } }
        protected override string DeleteProcedure { get { return "WebMessageQueue_Del"; } }

        protected override WebMessageQueue From(IDataReader r, WebMessageQueue source)
        {
            var item = source ?? new WebMessageQueue();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.FromObjectId = DataUtil.GetId(r, "FromObjectId");
            item.FromRecordId = DataUtil.GetId(r, "FromRecordId");
            item.EmailMessage = DataUtil.Get(r, "EmailMessage");
            item.EmailSubject = DataUtil.Get(r, "EmailSubject");
            item.SmsMessage = DataUtil.Get(r, "SmsMessage");
            item.To = DataUtil.Get(r, "To");
            item.ToExcluded = DataUtil.Get(r, "ToExcluded");
            item.ToFailed = DataUtil.Get(r, "ToFailed");
            item.ToOrBcc = DataUtil.GetInt32(r, "ToOrBcc");
            item.DateCreated = DataUtil.GetDateTime(r, "DateCreated");
            item.DateSent = DataUtil.GetDateTime(r, "DateSent");
            item.Status = DataUtil.GetInt32(r, "Status");
            item.SendVia = DataUtil.GetInt32(r, "SendVia");
            item.EnableMonitor = DataUtil.GetInt32(r, "EnableMonitor") == 1;

            return item;
        }

        public override int Update(WebMessageQueue item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebMessageQueue SET " +
                    DbSyntax.QuoteIdentifier("FromObjectId") + " = @FromObjectId, " +
                    DbSyntax.QuoteIdentifier("FromRecordId") + " = @FromRecordId, " +
                    DbSyntax.QuoteIdentifier("EmailMessage") + " = @EmailMessage, " +
                    DbSyntax.QuoteIdentifier("EmailSubject") + " = @EmailSubject, " +
                    DbSyntax.QuoteIdentifier("SmsMessage") + " = @SmsMessage, " +
                    DbSyntax.QuoteIdentifier("To") + " = @To, " +
                    DbSyntax.QuoteIdentifier("ToExcluded") + " = @ToExcluded, " +
                    DbSyntax.QuoteIdentifier("ToFailed") + " = @ToFailed, " +
                    DbSyntax.QuoteIdentifier("ToOrBcc") + " = @ToOrBcc, " +
                    DbSyntax.QuoteIdentifier("DateCreated") + " = @DateCreated, " +
                    DbSyntax.QuoteIdentifier("DateSent") + " = @DateSent, " +
                    DbSyntax.QuoteIdentifier("Status") + " = @Status, " +
                    DbSyntax.QuoteIdentifier("SendVia") + " = @SendVia, " +
                    DbSyntax.QuoteIdentifier("EnableMonitor") + " = @EnableMonitor" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@FromObjectId", item.FromObjectId),
                    DbHelper.CreateParameter("@FromRecordId", item.FromRecordId),
                    DbHelper.CreateParameter("@EmailMessage", item.EmailMessage),
                    DbHelper.CreateParameter("@EmailSubject", item.EmailSubject),
                    DbHelper.CreateParameter("@SmsMessage", item.SmsMessage),
                    DbHelper.CreateParameter("@To", item.To),
                    DbHelper.CreateParameter("@ToExcluded", item.ToExcluded),
                    DbHelper.CreateParameter("@ToFailed", item.ToFailed),
                    DbHelper.CreateParameter("@ToOrBcc", item.ToOrBcc),
                    DbHelper.CreateParameter("@DateCreated", item.DateCreated),
                    DbHelper.CreateParameter("@DateSent", item.DateSent),
                    DbHelper.CreateParameter("@Status", item.Status),
                    DbHelper.CreateParameter("@SendVia", item.SendVia),
                    DbHelper.CreateParameter("@EnableMonitor", item.EnableMonitor ? 1 : 0),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebMessageQueue (" +
                    DbSyntax.QuoteIdentifier("FromObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("FromRecordId") + ", " +
                    DbSyntax.QuoteIdentifier("EmailMessage") + ", " +
                    DbSyntax.QuoteIdentifier("EmailSubject") + ", " +
                    DbSyntax.QuoteIdentifier("SmsMessage") + ", " +
                    DbSyntax.QuoteIdentifier("To") + ", " +
                    DbSyntax.QuoteIdentifier("ToExcluded") + ", " +
                    DbSyntax.QuoteIdentifier("ToFailed") + ", " +
                    DbSyntax.QuoteIdentifier("ToOrBcc") + ", " +
                    DbSyntax.QuoteIdentifier("DateCreated") + ", " +
                    DbSyntax.QuoteIdentifier("DateSent") + ", " +
                    DbSyntax.QuoteIdentifier("Status") + ", " +
                    DbSyntax.QuoteIdentifier("SendVia") + ", " +
                    DbSyntax.QuoteIdentifier("EnableMonitor") +
                    ") VALUES (@FromObjectId, @FromRecordId, @EmailMessage, @EmailSubject, @SmsMessage, @To, @ToExcluded, @ToFailed, @ToOrBcc, @DateCreated, @DateSent, @Status, @SendVia, @EnableMonitor)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@FromObjectId", item.FromObjectId),
                    DbHelper.CreateParameter("@FromRecordId", item.FromRecordId),
                    DbHelper.CreateParameter("@EmailMessage", item.EmailMessage),
                    DbHelper.CreateParameter("@EmailSubject", item.EmailSubject),
                    DbHelper.CreateParameter("@SmsMessage", item.SmsMessage),
                    DbHelper.CreateParameter("@To", item.To),
                    DbHelper.CreateParameter("@ToExcluded", item.ToExcluded),
                    DbHelper.CreateParameter("@ToFailed", item.ToFailed),
                    DbHelper.CreateParameter("@ToOrBcc", item.ToOrBcc),
                    DbHelper.CreateParameter("@DateCreated", item.DateCreated),
                    DbHelper.CreateParameter("@DateSent", item.DateSent),
                    DbHelper.CreateParameter("@Status", item.Status),
                    DbHelper.CreateParameter("@SendVia", item.SendVia),
                    DbHelper.CreateParameter("@EnableMonitor", item.EnableMonitor ? 1 : 0)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        #region IWebMessageQueueProvider Members

        public IEnumerable<WebMessageQueue> GetList(int status = -2)
        {
            var items = new List<WebMessageQueue>();

            var sql = "SELECT * FROM WebMessageQueue WHERE " + DbSyntax.QuoteIdentifier("Status") + " = @Status";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Status", status)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        #endregion
    }
}
