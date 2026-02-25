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
    public class WebCommentProvider : GenericSqlDataProviderBase<WebComment>, IWebCommentProvider
    {
        protected override string TableName { get { return "WebComment"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "WebComment_Get"; } }
        protected override string DeleteProcedure { get { return "WebComment_Del"; } }

        protected override WebComment From(IDataReader r, WebComment source)
        {
            WebComment item = source ?? new WebComment();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Content = DataUtil.Get(r, WebColumns.Content);
            item.UserId = DataUtil.GetId(r, WebColumns.UserId);
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataUtil.GetId(r, WebColumns.RecordId);
            item.DateCreated = DataUtil.GetDateTime(r, WebColumns.DateCreated);
            item.ParentId = DataUtil.GetId(r, WebColumns.ParentId);
            item.UserName = DataUtil.Get(r, WebColumns.UserName);
            item.UserEmail = DataUtil.Get(r, "UserEmail");

            return item;
        }

        public override int Update(WebComment item)
        {
            var obj = DbHelper.ExecuteScalar("WebComment_Set",
                DbHelper.CreateParameter("@Id", item.Id),
                DbHelper.CreateParameter("@Content", item.Content),
                DbHelper.CreateParameter("@UserId", item.UserId),
                DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                DbHelper.CreateParameter("@RecordId", item.RecordId),
                DbHelper.CreateParameter("@DateCreated", item.DateCreated),
                DbHelper.CreateParameter("@ParentId", item.ParentId),
                DbHelper.CreateParameter("@UserName", item.UserName),
                DbHelper.CreateParameter("@UserEmail", item.UserEmail)
            );

            item.Id = DataUtil.GetId(obj);
            return item.Id;
        }

        #region IWebCommentProvider Members

        public IEnumerable<WebComment> GetList(int userId = -2, int objectId = -2, int recordId = -2, int parentId = -2)
        {
            List<WebComment> items = new List<WebComment>();

            using (var r = DbHelper.ExecuteReader(SelectProcedure,
                DbHelper.CreateParameter("@UserId", userId),
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@ParentId", parentId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        #endregion
    }
}
