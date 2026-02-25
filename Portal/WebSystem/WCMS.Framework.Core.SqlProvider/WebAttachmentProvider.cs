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
            var obj = DbHelper.ExecuteScalar("WebAttachment_Set",
                DbHelper.CreateParameter("@Id", item.Id),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@FilePath", item.FilePath),
                DbHelper.CreateParameter("@Size", item.Size),
                DbHelper.CreateParameter("@DateUploaded", item.DateUploaded),
                DbHelper.CreateParameter("@UserId", item.UserId),
                DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                DbHelper.CreateParameter("@RecordId", item.RecordId),
                DbHelper.CreateParameter("@BatchGuid", item.BatchGuid));

            item.Id = DataUtil.GetId(obj);
            return item.Id;
        }

        #region IWebAttachmentProvider Members

        public IEnumerable<WebAttachment> GetList(int userId = -2, int objectId = -2, int recordId = -2)
        {
            List<WebAttachment> items = new List<WebAttachment>();

            using (var r = DbHelper.ExecuteReader(SelectProcedure,
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

            using (var r = DbHelper.ExecuteReader(SelectProcedure,
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
