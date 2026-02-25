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
            var obj = DbHelper.ExecuteReader("WebMessageQueue_Set",
                DbHelper.CreateParameter("@Id", item.Id),
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
                DbHelper.CreateParameter("@EnableMonitor", item.EnableMonitor ? 1 : 0));

            item.Id = DataUtil.GetId(obj);
            return item.Id;
        }

        #region IWebMessageQueueProvider Members

        public IEnumerable<WebMessageQueue> GetList(int status = -2)
        {
            var items = new List<WebMessageQueue>();

            using (var r = DbHelper.ExecuteReader(SelectProcedure,
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
