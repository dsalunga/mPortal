using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Article.Providers
{
    public class ArticleListProvider : GenericSqlDataProviderBase<ArticleList>, IArticleListProvider
    {
        public ArticleList Get(int objectId, int recordId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("ArticleList_Get",
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId)
            ))
            {
                if (r.Read())
                    return this.From(r);
            }

            return null;
        }

        public override int Update(ArticleList item)
        {
            var o = SqlHelper.ExecuteScalar("ArticleList_Set",
                new SqlParameter("@ListId", item.Id),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@RecordId", item.RecordId),
                new SqlParameter("@SiteId", item.SiteId),
                new SqlParameter("@TemplateId", item.TemplateId),
                new SqlParameter("@PageSize", item.PageSize),
                //new SqlParameter("@DateFormatString", item.DateFormatString),
                new SqlParameter("@FolderId", item.FolderId),
                new SqlParameter("@CommentOn", item.CommentOn)
            );

            item.Id = DataHelper.GetId(o);
            return item.Id;
        }

        public override bool Delete(int id)
        {
            SqlHelper.ExecuteNonQuery("ArticleList_Del",
                new SqlParameter("@ListId", id));

            return true;
        }

        protected override ArticleList From(IDataReader r, ArticleList source)
        {
            ArticleList item = source ?? new ArticleList();
            item.Id = DataHelper.GetId(r["ListId"]);
            item.PageSize = (int)r["PageSize"];
            //item.DateFormatString = r["DateFormatString"].ToString();
            item.ObjectId = DataHelper.GetId(r["ObjectId"]);
            item.RecordId = DataHelper.GetId(r["RecordId"]);
            item.TemplateId = DataHelper.GetId(r, WebColumns.TemplateId);
            item.SiteId = DataHelper.GetId(r, WebColumns.SiteId);
            item.FolderId = DataHelper.GetId(r, WebColumns.FolderId);
            item.CommentOn = DataHelper.GetInt32(r, "CommentOn");

            return item;
        }

        protected override string DeleteProcedure { get { return "ArticleList_Del"; } }
        protected override string SelectProcedure { get { return "ArticleList_Get"; } }
    }
}
