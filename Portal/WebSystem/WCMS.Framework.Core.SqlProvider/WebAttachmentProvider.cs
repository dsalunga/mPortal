using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebAttachmentProvider : GenericSqlDataProviderBase<WebAttachment>, IWebAttachmentProvider
    {
        protected override string SelectProcedure { get { return "WebAttachment_Get"; } }
        protected override string DeleteProcedure { get { return "WebAttachment_Del"; } }

        protected override WebAttachment From(IDataReader r, WebAttachment source)
        {
            WebAttachment item = source ?? new WebAttachment();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.FilePath = DataHelper.Get(r, "FilePath");
            item.Size = DataHelper.GetInt64(r, WebColumns.Size);
            item.DateUploaded = DataHelper.GetDateTime(r, "DateUploaded");
            item.UserId = DataHelper.GetId(r, WebColumns.UserId);
            item.ObjectId = DataHelper.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataHelper.GetId(r, WebColumns.RecordId);
            item.BatchGuid = DataHelper.Get(r, "BatchGuid");

            return item;
        }

        public override int Update(WebAttachment item)
        {
            var obj = SqlHelper.ExecuteScalar("WebAttachment_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@FilePath", item.FilePath),
                new SqlParameter("@Size", item.Size),
                new SqlParameter("@DateUploaded", item.DateUploaded),
                new SqlParameter("@UserId", item.UserId),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@RecordId", item.RecordId),
                new SqlParameter("@BatchGuid", item.BatchGuid));

            item.Id = DataHelper.GetId(obj);
            return item.Id;
        }

        #region IWebAttachmentProvider Members

        public IEnumerable<WebAttachment> GetList(int userId = -2, int objectId = -2, int recordId = -2)
        {
            List<WebAttachment> items = new List<WebAttachment>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@UserId", userId),
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebAttachment> GetList(string batchGuid)
        {
            List<WebAttachment> items = new List<WebAttachment>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@BatchGuid", batchGuid)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        #endregion
    }
}
