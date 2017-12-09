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
    public class WebCommentProvider : GenericSqlDataProviderBase<WebComment>, IWebCommentProvider
    {
        protected override string SelectProcedure { get { return "WebComment_Get"; } }
        protected override string DeleteProcedure { get { return "WebComment_Del"; } }

        protected override WebComment From(IDataReader r, WebComment source)
        {
            WebComment item = source ?? new WebComment();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.Content = DataHelper.Get(r, WebColumns.Content);
            item.UserId = DataHelper.GetId(r, WebColumns.UserId);
            item.ObjectId = DataHelper.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataHelper.GetId(r, WebColumns.RecordId);
            item.DateCreated = DataHelper.GetDateTime(r, WebColumns.DateCreated);
            item.ParentId = DataHelper.GetId(r, WebColumns.ParentId);
            item.UserName = DataHelper.Get(r, WebColumns.UserName);
            item.UserEmail = DataHelper.Get(r, "UserEmail");

            return item;
        }

        public override int Update(WebComment item)
        {
            var obj = SqlHelper.ExecuteScalar("WebComment_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Content", item.Content),
                new SqlParameter("@UserId", item.UserId),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@RecordId", item.RecordId),
                new SqlParameter("@DateCreated", item.DateCreated),
                new SqlParameter("@ParentId", item.ParentId),
                new SqlParameter("@UserName", item.UserName),
                new SqlParameter("@UserEmail", item.UserEmail)
            );

            item.Id = DataHelper.GetId(obj);
            return item.Id;
        }

        #region IWebCommentProvider Members

        public IEnumerable<WebComment> GetList(int userId = -2, int objectId = -2, int recordId = -2, int parentId = -2)
        {
            List<WebComment> items = new List<WebComment>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@UserId", userId),
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@ParentId", parentId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        #endregion
    }
}
