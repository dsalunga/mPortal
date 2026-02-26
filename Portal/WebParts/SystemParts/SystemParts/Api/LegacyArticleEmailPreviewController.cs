using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;
using ArticlePart = WCMS.WebSystem.WebParts.Article;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Legacy-compatible replacement for AppBundle/Article/EmailPreview.ashx.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacyArticleEmailPreviewController : ControllerBase
    {
        [HttpGet("/Content/Parts/Article/EmailPreview.ashx")]
        [HttpGet("/Content/Parts/AppBundle/Article/EmailPreview.ashx")]
        [HttpGet("/_Sections/Article/EmailPreview.ashx")]
        public IActionResult Preview([FromQuery(Name = "ArticleId")] int articleId, [FromQuery(Name = "PageId")] int pageId)
        {
            if (articleId <= 0 || pageId <= 0)
                return NotFound();

            var article = ArticlePart.Article.Get(articleId);
            var page = WPage.Get(pageId);
            if (article == null || page == null)
                return NotFound();

            var site = page.Site;
            if (site == null)
                return NotFound();

            var values = new NamedValueProvider();
            values.Add("Title", article.Title);
            values.Add("PublishedDate", string.Format("{0:MMMM d, yyyy}", article.Date));
            values.Add("Content", article.Content);
            values.Add("BaseAddress", site.BuildAbsoluteUrl());
            values.Add("Permalink", string.Format("{0}?ArticleId={1}", page.BuildAbsoluteUrl(), article.Id));

            var emailTemplatePath = page.GetParameterValue(ArticlePart.ArticleConstants.EmailTemplateFileKey, "");
            if (string.IsNullOrWhiteSpace(emailTemplatePath))
                emailTemplatePath = site.GetPartConfig(ArticlePart.Article.ArticleIdentity, ArticlePart.ArticleConstants.EmailTemplateFileKey);

            if (string.IsNullOrWhiteSpace(emailTemplatePath))
                emailTemplatePath = "~/AppBundle/Article/Templates/EmailAlert.htm";

            var html = FileHelper.ReadFile(PathMapper.MapPath(emailTemplatePath));
            if (string.IsNullOrWhiteSpace(html))
                return NotFound();

            html = Substituter.Substitute(html, values);
            return Content(html, "text/html");
        }
    }
}
