using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Article
{
    /// <summary>
    /// Summary description for EmailPreview1
    /// </summary>
    public class EmailPreview1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            int id = DataHelper.GetId(context.Request, "ArticleId");
            int pageId = DataHelper.GetId(context.Request, "PageId");
            Article item = Article.Get(id);
            var page = WPage.Get(pageId);
            if (item != null && page != null)
            {
                var site = page.Site;

                // Prepare Email
                NamedValueProvider values = new NamedValueProvider();
                values.Add("Title", item.Title);
                values.Add("PublishedDate", string.Format("{0:MMMM d, yyyy}", item.Date));
                values.Add("Content", item.Content);
                values.Add("BaseAddress", site.BuildAbsoluteUrl());
                values.Add("Permalink", string.Format("{0}?ArticleId={1}", page.BuildAbsoluteUrl(), item.Id));

                string emailPath = page.GetParameterValue(ArticleConstants.EmailTemplateFileKey, "");
                if (string.IsNullOrWhiteSpace(emailPath))
                    emailPath = site.GetPartConfig(Article.ArticleIdentity, ArticleConstants.EmailTemplateFileKey);

                if (string.IsNullOrEmpty(emailPath))
                    emailPath = "~/Content/Parts/Article/Templates/EmailAlert.htm";

                string email = FileHelper.ReadFile(context.Server.MapPath(emailPath));
                email = Substituter.Substitute(email, values);

                context.Response.Write(email);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}