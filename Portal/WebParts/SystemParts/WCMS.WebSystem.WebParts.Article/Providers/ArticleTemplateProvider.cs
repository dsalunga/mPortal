using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using WCMS.Framework;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Article.Providers
{
    public class ArticleTemplateProvider : GenericSqlDataProviderBase<ArticleTemplate>, IArticleTemplateProvider
    {
        protected override ArticleTemplate From(IDataReader r, ArticleTemplate source)
        {
            var item = source ?? new ArticleTemplate();
            item.Id = DataHelper.GetId(r, "Id");
            item.Name = r["Name"].ToString();
            item.Date = (DateTime)r["Date"];
            item.File = r["File"].ToString();
            item.ImageUrl = r["ImageUrl"].ToString();
            item.ListItemTemplate = r["ListItemTemplate"].ToString();
            item.ListTemplate = r["ListTemplate"].ToString();
            item.DetailsTemplate = r["DetailsTemplate"].ToString();
            item.DateFormat = DataHelper.Get(r, "DateFormat");

            return item;
        }

        public override int Update(ArticleTemplate item)
        {
            var obj = SqlHelper.ExecuteScalar("ArticleTemplate_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@Date", item.Date),
                new SqlParameter("@File", item.File),
                new SqlParameter("@ImageUrl", item.ImageUrl),
                new SqlParameter("@ListItemTemplate", item.ListItemTemplate),
                new SqlParameter("@ListTemplate", item.ListTemplate),
                new SqlParameter("@DetailsTemplate", item.DetailsTemplate),
                new SqlParameter("@DateFormat", item.DateFormat)
            );

            item.Id = DataHelper.GetId(obj);
            return item.Id;
        }

        protected override string DeleteProcedure { get { return "ArticleTemplate_Del"; } }
        protected override string SelectProcedure { get { return "ArticleTemplate_Get"; } }
    }
}
