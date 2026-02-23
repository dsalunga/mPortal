using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Data.SqlClient;

using WCMS.Common.Utilities;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebSubscriptionProvider : GenericSqlDataProviderBase<WebSubscription>, IWebSubscriptionProvider
    {
        protected override string IdParameter { get { return "SubscriptionId"; } }
        protected override string DeleteProcedure { get { return "WebSubscription_Del"; } }
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
            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@PartId", partId),
                new SqlParameter("@PageId", pageId),
                new SqlParameter("@Allow", allow)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public override int Update(WebSubscription item)
        {
            var obj = SqlHelper.ExecuteReader("WebSubscription_Set",
                new SqlParameter("@SubscriptionId", item.Id),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@RecordId", item.RecordId),
                new SqlParameter("@PartId", item.PartId),
                new SqlParameter("@PageId", item.PageId),
                new SqlParameter("@Allow", item.Allow));

            item.Id = DataUtil.GetId(obj);
            return item.Id;
        }

        #endregion
    }
}
