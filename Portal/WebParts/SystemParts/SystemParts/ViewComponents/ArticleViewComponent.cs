using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Diagnostics;
using WCMS.Framework.ViewComponents;
using WCMS.WebSystem.ViewModel;
using ArticleNs = WCMS.WebSystem.WebParts.Article;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Renders article lists with paging or article detail views.
    /// Replaces Article.ascx (SystemParts/Article).
    /// Usage: @await Component.InvokeAsync("Article", new { objectId, recordId })
    /// </summary>
    public class ArticleViewComponent : WViewComponent
    {
        public ArticleViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            var sw = PerformanceLog.StartLog();

            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var list = ArticleNs.ArticleList.Get(WcmsContext.ObjectId, WcmsContext.RecordId);
            int articleId = WcmsContext.GetId(ArticleNs.Article.ArticleKey);

            var model = new ArticleComponentModel();

            string dateFormatString = ArticleNs.ArticleConstants.DateFormatString;
            string listTemplate, itemTemplate, detailsTemplate;

            if (list == null || !(list.TemplateId > 0))
            {
                var parms = WcmsContext.Element?.Parameters;
                listTemplate = parms?.FirstOrDefault(i => i.Name == ArticleNs.ArticleConstants.ListTemplateKey)?.Value ?? ArticleNs.ArticleConstants.ListTemplate;
                itemTemplate = parms?.FirstOrDefault(i => i.Name == ArticleNs.ArticleConstants.ItemTemplateKey)?.Value ?? ArticleNs.ArticleConstants.ItemTemplate;
                detailsTemplate = parms?.FirstOrDefault(i => i.Name == ArticleNs.ArticleConstants.DetailsTemplateKey)?.Value ?? ArticleNs.ArticleConstants.DetailsTemplate;
                var dateFmt = parms?.FirstOrDefault(i => i.Name == ArticleNs.ArticleConstants.DateFormatStringKey)?.Value;
                if (!string.IsNullOrEmpty(dateFmt)) dateFormatString = dateFmt;
            }
            else
            {
                var template = list.Template;
                listTemplate = template.ListTemplate;
                itemTemplate = template.ListItemTemplate;
                detailsTemplate = template.DetailsTemplate;
                if (!string.IsNullOrEmpty(template.DateFormat))
                    dateFormatString = template.DateFormat;
            }

            if (!(articleId > 0) || WcmsContext.ObjectId != WebObjects.WebPage)
            {
                // List view
                model.IsDetailView = false;
                model.Articles = new List<ArticleItemModel>();

                if (list != null)
                {
                    string tag = WcmsContext.Get(ArticleNs.ArticleConstants.TagKey);
                    var articles = !string.IsNullOrEmpty(tag)
                        ? list.Articles.Where(a => a.Tags.Contains(tag)).OrderByDescending(i => i.Date)
                        : list.Articles.OrderByDescending(i => i.Date);

                    bool forPaging = string.IsNullOrEmpty(WcmsContext.Get("PageFor")) || WcmsContext.Get("PageFor") == WcmsContext.RecordId.ToString();
                    int page = string.IsNullOrEmpty(WcmsContext.Get("ShowP")) || !forPaging ? -1 : Convert.ToInt32(WcmsContext.Get("ShowP"));

                    int currentItem = 0;
                    int visibleItems = 0;
                    int itemCount = 0;

                    foreach (var article in articles)
                    {
                        itemCount++;
                        if (page == -1 || page == (currentItem / list.PageSize + 1))
                        {
                            if (visibleItems < list.PageSize)
                            {
                                model.Articles.Add(new ArticleItemModel
                                {
                                    Id = article.Id,
                                    Title = article.Title,
                                    Date = string.Format(dateFormatString, article.Date),
                                    Content = article.Content,
                                    Description = article.Description,
                                    Image = article.Image,
                                    Author = article.Author
                                });
                            }
                            visibleItems++;
                        }
                        currentItem++;
                    }

                    model.TotalItems = itemCount;
                    model.PageSize = list.PageSize;
                    model.CurrentPage = page > 0 ? page : 1;
                }
            }
            else
            {
                // Detail view
                model.IsDetailView = true;
                ArticleNs.Article article = null;

                if (list != null)
                {
                    article = list.FolderId > 0
                        ? list.GetArticle(articleId)
                        : ArticleNs.ArticleLink.Get(list.ObjectId, list.RecordId, articleId)?.Article;
                }
                else
                {
                    article = ArticleNs.ArticleLink.Get(WcmsContext.ObjectId, WcmsContext.RecordId, articleId)?.Article;
                }

                if (article != null)
                {
                    model.DetailArticle = new ArticleItemModel
                    {
                        Id = article.Id,
                        Title = article.Title,
                        Date = string.Format(dateFormatString, article.Date),
                        Content = article.Content,
                        Description = article.Description,
                        Image = article.Image,
                        Author = article.Author
                    };
                }
            }

            PerformanceLog.EndLog(
                string.Format("Article: {0}/{1}", WcmsContext.ObjectId, WcmsContext.RecordId),
                sw, WcmsContext.PageId);

            return View(model);
        }

        public override string PageTitleOverride
        {
            get
            {
                int articleId = WcmsContext.GetId(ArticleNs.Article.ArticleKey);
                if (articleId > 0 && WcmsContext.ObjectId == WebObjects.WebPage)
                {
                    var list = ArticleNs.ArticleList.Get(WcmsContext.ObjectId, WcmsContext.RecordId);
                    ArticleNs.Article article = null;
                    if (list != null)
                        article = list.GetArticle(articleId);
                    else
                        article = ArticleNs.ArticleLink.Get(WcmsContext.ObjectId, WcmsContext.RecordId, articleId)?.Article;

                    return article?.Title;
                }
                return null;
            }
        }
    }

    public class ArticleComponentModel
    {
        public bool IsDetailView { get; set; }
        public List<ArticleItemModel> Articles { get; set; }
        public ArticleItemModel DetailArticle { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }

    public class ArticleItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
    }
}
