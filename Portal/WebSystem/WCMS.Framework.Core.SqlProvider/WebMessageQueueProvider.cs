using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework.Net;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebMessageQueueProvider : GenericSqlDataProviderBase<WebMessageQueue>, IWebMessageQueueProvider
    {
        protected override string SelectProcedure { get { return "WebMessageQueue_Get"; } }
        protected override string DeleteProcedure { get { return "WebMessageQueue_Del"; } }

        protected override WebMessageQueue From(IDataReader r, WebMessageQueue source)
        {
            var item = source ?? new WebMessageQueue();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.FromObjectId = DataHelper.GetId(r, "FromObjectId");
            item.FromRecordId = DataHelper.GetId(r, "FromRecordId");
            item.EmailMessage = DataHelper.Get(r, "EmailMessage");
            item.EmailSubject = DataHelper.Get(r, "EmailSubject");
            item.SmsMessage = DataHelper.Get(r, "SmsMessage");
            item.To = DataHelper.Get(r, "To");
            item.ToExcluded = DataHelper.Get(r, "ToExcluded");
            item.ToFailed = DataHelper.Get(r, "ToFailed");
            item.ToOrBcc = DataHelper.GetInt32(r, "ToOrBcc");
            item.DateCreated = DataHelper.GetDateTime(r, "DateCreated");
            item.DateSent = DataHelper.GetDateTime(r, "DateSent");
            item.Status = DataHelper.GetInt32(r, "Status");
            item.SendVia = DataHelper.GetInt32(r, "SendVia");
            item.EnableMonitor = DataHelper.GetInt32(r, "EnableMonitor") == 1;

            return item;
        }

        public override int Update(WebMessageQueue item)
        {
            var obj = SqlHelper.ExecuteReader("WebMessageQueue_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@FromObjectId", item.FromObjectId),
                new SqlParameter("@FromRecordId", item.FromRecordId),
                new SqlParameter("@EmailMessage", item.EmailMessage),
                new SqlParameter("@EmailSubject", item.EmailSubject),
                new SqlParameter("@SmsMessage", item.SmsMessage),
                new SqlParameter("@To", item.To),
                new SqlParameter("@ToExcluded", item.ToExcluded),
                new SqlParameter("@ToFailed", item.ToFailed),
                new SqlParameter("@ToOrBcc", item.ToOrBcc),
                new SqlParameter("@DateCreated", item.DateCreated),
                new SqlParameter("@DateSent", item.DateSent),
                new SqlParameter("@Status", item.Status),
                new SqlParameter("@SendVia", item.SendVia),
                new SqlParameter("@EnableMonitor", item.EnableMonitor ? 1 : 0));

            item.Id = DataHelper.GetId(obj);
            return item.Id;
        }

        #region IWebMessageQueueProvider Members

        public IEnumerable<WebMessageQueue> GetList(int status = -2)
        {
            var items = new List<WebMessageQueue>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@Status", status)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        #endregion
    }
}
