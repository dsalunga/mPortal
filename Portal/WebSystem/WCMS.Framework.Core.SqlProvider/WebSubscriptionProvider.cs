using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebSubscriptionProvider : GenericSqlDataProviderBase<WebSubscription>, IWebSubscriptionProvider
    {
        protected override string IdParameter { get { return "SubscriptionId"; } }
        protected override string DeleteProcedure { get { return "WebSubscription_Del"; } }
        protected override string TableName { get { return "WebSubscription"; } }

        protected override string IdColumn { get { return "SubscriptionId"; } }


        protected override string SelectProcedure { get { return "WebSubscription_Get"; } }

        #region IDataProvider<WebSubscription> Members

        protected override WebSubscription From(IDataReader r, WebSubscription source)
        {
            var item = source ?? new WebSubscription();
            item.Id = DataUtil.GetId(r, WebColumns.SubscriptionId);
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataUtil.GetId(r, WebColumns.RecordId);
            item.PartId = DataUtil.GetId(r, WebColumns.PartId);
            item.PageId = DataUtil.GetId(r, WebColumns.PageId);
            item.Allow = DataUtil.GetId(r, WebColumns.Allow);

            return item;
        }

        public IEnumerable<WebSubscription> GetList(int objectId, int recordId, int partId, int pageId, int allow)
        {
            var items = new List<WebSubscription>();
            using (var r = DbHelper.ExecuteReader(SelectProcedure,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@PartId", partId),
                DbHelper.CreateParameter("@PageId", pageId),
                DbHelper.CreateParameter("@Allow", allow)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public override int Update(WebSubscription item)
        {
            var obj = DbHelper.ExecuteReader("WebSubscription_Set",
                DbHelper.CreateParameter("@SubscriptionId", item.Id),
                DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                DbHelper.CreateParameter("@RecordId", item.RecordId),
                DbHelper.CreateParameter("@PartId", item.PartId),
                DbHelper.CreateParameter("@PageId", item.PageId),
                DbHelper.CreateParameter("@Allow", item.Allow));

            item.Id = DataUtil.GetId(obj);
            return item.Id;
        }

        #endregion
    }
}
