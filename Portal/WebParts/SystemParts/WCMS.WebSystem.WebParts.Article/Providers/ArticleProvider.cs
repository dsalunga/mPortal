using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Article.Providers
{
    public class ArticleProvider : GenericSqlDataProviderBase<Article>, IArticleProvider
    {
        public IEnumerable<Article> GetList(int siteId = -2)
        {
            var items = new List<Article>();
            using (var r = SqlHelper.ExecuteReader("Articles_Get",
                new SqlParameter("@SiteId", siteId)))
            {
                while (r.Read())
                    items.Add(this.From(r));
            }

            return items;
        }

        public override int Update(Article item)
        {
            object o = SqlHelper.ExecuteScalar("Articles_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Title", item.Title),
                new SqlParameter("@Image", item.Image),
                new SqlParameter("@Description", item.Description),
                new SqlParameter("@Date", item.Date),
                new SqlParameter("@Content", item.Content),
                new SqlParameter("@Author", item.Author),
                new SqlParameter("@SiteId", item.SiteId),
                new SqlParameter("@Active", item.Active),
                new SqlParameter("@DateModified", item.DateModified),
                new SqlParameter("@UserId", item.UserId),
                new SqlParameter("@ModifiedUserId", item.ModifiedUserId),
                new SqlParameter("@Tags", item.Tags)
            );

            item.Id = DataHelper.GetId(o);
            return item.Id;
        }

        protected override Article From(IDataReader r, Article source)
        {
            Article item = source ?? new Article();
            item.Id = DataHelper.GetId(r["Id"]);
            item.Title = r["Title"].ToString();
            item.Image = r["Image"].ToString();
            item.Description = r["Description"].ToString();
            item.Date = (DateTime)r["Date"];
            item.Content = r["Content"].ToString();
            item.Author = r["Author"].ToString();
            item.SiteId = DataHelper.GetId(r[WebColumns.SiteId]);
            item.Active = Convert.ToInt32(r["Active"].ToString());
            item.DateModified = (DateTime)r["DateModified"];
            item.UserId = DataHelper.GetId(r["UserId"]);
            item.ModifiedUserId = DataHelper.GetId(r["ModifiedUserId"]);
            item.Tags = r["Tags"].ToString();

            return item;
        }

        protected override string DeleteProcedure { get { return "Articles_Del"; } }
        protected override string SelectProcedure { get { return "Articles_Get"; } }
    }
}
