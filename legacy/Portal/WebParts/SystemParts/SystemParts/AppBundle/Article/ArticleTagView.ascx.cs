using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Diagnostics;


namespace WCMS.WebSystem.WebParts.Article
{
    public partial class ArticleTagController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sw = PerformanceLog.StartLog();

            WContext context = new WContext(this);
            ArticleList config = ArticleList.Get(WebObjects.WebPage, context.PageId);
            int articleId = context.GetId(Article.ArticleKey);
            var articles = config.Articles;

            // Collect unique tags
            List<string> tagList = new List<string>();
            foreach (var article in articles)
            {
                if (!string.IsNullOrEmpty(article.Tags))
                {
                    var tags = article.Tags.Split(',');
                    if (tags.Length > 0)
                    {
                        foreach (var tagTemp in tags)
                        {
                            var tag = tagTemp.Trim();
                            if (!string.IsNullOrEmpty(tag))
                                if (!tagList.Contains(tag))
                                    tagList.Add(tag);
                        }
                    }
                }
            }

            // Begin render tags

            var parms = context.Element.Parameters;

            // Prepare list templates
            var parm = parms.FirstOrDefault(i => i.Name == ArticleConstants.ItemTemplateKey);
            var itemTemplate = parm == null ? dataItemTemplate.InnerHtml : parm.Value;

            parm = parms.FirstOrDefault(i => i.Name == ArticleConstants.ListTemplateKey);
            var listTemplate = parm == null ? dataListTemplate.InnerHtml : parm.Value;

            QueryParser query = new QueryParser();
            query.BasePath = context.Page.BuildRelativeUrl();

            var orderTags = tagList.OrderBy(i => i);

            StringBuilder output = new StringBuilder();
            NamedValueProvider valueProvider = new NamedValueProvider();
            foreach (var tag in orderTags)
            {
                query.Set(ArticleConstants.TagKey, tag);

                valueProvider.Add(ArticleConstants.TagKey, tag);
                valueProvider.Add("Link", query.BuildQuery());

                output.Append(Substituter.Substitute(itemTemplate, valueProvider));

                valueProvider.Remove(ArticleConstants.TagKey);
                valueProvider.Remove("Link");
            }

            query.Remove(ArticleConstants.TagKey);

            valueProvider.Remove("Link");

            valueProvider.Add("Link", query.BuildQuery());
            valueProvider.Add(Substituter.DefaultKey, output.ToString());

            // Append the content
            Literal content = new Literal();
            content.Text = Substituter.Substitute(listTemplate, valueProvider);
            this.Controls.Add(content);

            PerformanceLog.EndLog(string.Format("Article-TagView: {0}/{1}", context.ObjectId, context.RecordId), sw, context.PageId);
        }
    }
}