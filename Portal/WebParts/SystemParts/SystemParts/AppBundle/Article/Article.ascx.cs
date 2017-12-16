using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Linq;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Diagnostics;

using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Article
{
    public partial class ArticleController : WUserControl
    {
        private string dateFormatString = ArticleConstants.DateFormatString;

        WContext context;
        ArticleList list;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void SetupObjects()
        {
            if (context == null)
                context = new WContext(this);

            if (list == null)
                list = ArticleList.Get(context.ObjectId, context.RecordId);
        }

        private void LoadData()
        {
            var stopWatch = PerformanceLog.StartLog();

            SetupObjects();

            var valueProviders = context.ValueProvider;
            int articleId = context.GetId(Article.ArticleKey);

            string templateFile = string.Empty;
            string listTemplate;
            string itemTemplate;
            string detailsTemplate;

            // Styles / CSS
            if (list == null || !(list.TemplateId > 0))
            {
                var parms = context.Element.Parameters;

                var parm = parms.FirstOrDefault(i => i.Name == ArticleConstants.ListTemplateKey);
                listTemplate = parm == null ? ArticleConstants.ListTemplate : parm.Value;

                parm = parms.FirstOrDefault(i => i.Name == ArticleConstants.ItemTemplateKey);
                itemTemplate = parm == null ? ArticleConstants.ItemTemplate : parm.Value;

                parm = parms.FirstOrDefault(i => i.Name == ArticleConstants.DetailsTemplateKey);
                detailsTemplate = parm == null ? ArticleConstants.DetailsTemplate : parm.Value;

                parm = parms.FirstOrDefault(i => i.Name == ArticleConstants.DateFormatStringKey);
                dateFormatString = parm == null ? ArticleConstants.DateFormatString : parm.Value;
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

            HtmlGenericControl divMain = new HtmlGenericControl("div");
            HtmlGenericControl divPage = new HtmlGenericControl("div");

            divPage.Attributes["class"] = "table-pager";

            int itemCount = 0; // Article Count

            if (!(articleId > 0) || context.ObjectId != WebObjects.WebPage)
            {
                #region List view

                int currentItem = 0;
                int iVisibleItems = 0;

                bool forPaging = string.IsNullOrEmpty(context.Get("PageFor")) ? true : context.Get("PageFor") == context.RecordId.ToString();
                int iPage = string.IsNullOrEmpty(context.Get("ShowP")) || !forPaging ? -1 : Convert.ToInt32(context.Get("ShowP"));


                StringBuilder listContent = new StringBuilder();
                if (list != null)
                {
                    IOrderedEnumerable<Article> articles = null;

                    // Check Tag parameter
                    string tag = context.Get(ArticleConstants.TagKey);
                    if (!string.IsNullOrEmpty(tag))
                    {
                        var tmpArticles = list.Articles;
                        articles = (from a in tmpArticles
                                    where a.Tags.Contains(tag)
                                    select a).OrderByDescending(i => i.Date);
                    }
                    else
                    {
                        articles = list.Articles.OrderByDescending(i => i.Date);
                    }

                    foreach (var article in articles)
                    {
                        itemCount += 1;

                        if ((iPage == -1 || iPage == (currentItem / list.PageSize + 1))) // Check if item should be visible depending on paging
                        {
                            if (iVisibleItems < list.PageSize)
                            {
                                context.Set(Article.ArticleKey, article.Id);

                                NamedValueProvider itemProvider = new NamedValueProvider();
                                itemProvider.Add("Title", article.Title);
                                itemProvider.Add("Date", string.Format(dateFormatString, article.Date));
                                itemProvider.Add("Content", article.Content);
                                itemProvider.Add("Description", article.Description);
                                itemProvider.Add("Image", article.Image);
                                itemProvider.Add("Author", article.Author);
                                itemProvider.Add("Link", context.BuildQuery());

                                valueProviders[Substituter.DefaultProviderKey] = itemProvider;

                                listContent.Append(Substituter.Substitute(itemTemplate, valueProviders));
                            }

                            iVisibleItems += 1;
                        }

                        currentItem += 1;
                    }
                }

                #endregion

                #region List Template

                NamedValueProvider listProvider = new NamedValueProvider();
                listProvider.Add("BasePath", context.BasePath);
                listProvider.Add("Content", listContent.ToString());

                valueProviders[Substituter.DefaultProviderKey] = listProvider;

                divMain.InnerHtml = Substituter.Substitute(listTemplate, valueProviders);

                #endregion

                #region Generate Paging

                if (list != null && itemCount > list.PageSize)
                {
                    StringBuilder sbPage = new StringBuilder();
                    context.Remove(Article.ArticleKey);
                    divPage.InnerHtml = "&nbsp;&nbsp;Pages:&nbsp;";

                    int pageCount = (int)Math.Ceiling(((double)itemCount / list.PageSize));
                    int iVisiblePage = iPage != -1 ? iPage : 1;

                    for (int i = 1; i <= pageCount; i++)
                    {
                        string page;
                        if (iVisiblePage == i)
                        {
                            page = i + "&nbsp;";
                        }
                        else
                        {
                            context["ShowP"] = i.ToString();
                            context["PageFor"] = context.RecordId.ToString(); // Set what item fired paging, ItemID only without ItemType
                            page = string.Format(ArticleConstants.LinkFormat + "&nbsp;", context.BuildQuery(), i);
                        }

                        sbPage.Append(page);
                    }

                    divPage.InnerHtml = sbPage.ToString();
                }

                #endregion
            }
            else
            {
                // # Display Item Details #
                #region Item Details

                Article article = null;
                ArticleLink link = null;

                if (list != null)
                {
                    if (list.FolderId > 0)
                        article = list.GetArticle(articleId);
                    else
                    {
                        link = ArticleLink.Get(list.ObjectId, list.RecordId, articleId);
                        if (link != null)
                            article = link.Article;
                    }
                }
                else
                {
                    link = ArticleLink.Get(context.ObjectId, context.RecordId, articleId);
                    if (link != null)
                        article = link.Article;
                }

                if (article != null)
                {
                    NamedValueProvider itemProvider = new NamedValueProvider();
                    itemProvider.Add("Title", article.Title);
                    itemProvider.Add("Date", string.Format(dateFormatString, article.Date));
                    itemProvider.Add("Content", article.Content);
                    itemProvider.Add("Description", article.Description);
                    itemProvider.Add("Image", article.Image);
                    itemProvider.Add("Author", article.Author);
                    itemProvider.Add("BasePath", context.BasePath);

                    valueProviders[Substituter.DefaultProviderKey] = itemProvider;

                    divMain.InnerHtml = Substituter.Substitute(detailsTemplate, valueProviders);

                    if (link != null && (link.CommentOn == 1 || (link.CommentOn == -1 && list.CommentOn == 1)))
                    {
                        Control commentView = LoadControl(WConstants.CommentViewAscx);

                        var partView = commentView as IElementPartView;
                        if (partView != null)
                        {
                            partView.ObjectID = article.OBJECT_ID;
                            partView.RecordId = article.Id;
                            partView.Initialize();

                            divMain.Controls.Add(commentView);
                        }
                    }
                }

                #endregion
            }

            this.Controls.Add(divMain);
            this.Controls.Add(divPage);

            PerformanceLog.EndLog(string.Format("Article: {0}/{1}", context.ObjectId, context.RecordId), stopWatch, context.PageId);
        }

        #region IWebPartControl Members

        public override string PageTitleOverride
        {
            get
            {
                SetupObjects();

                int articleId = context.GetId(Article.ArticleKey);
                if (articleId > 0 && context.ObjectId == WebObjects.WebPage)
                {
                    Article article = null;
                    if (list != null)
                    {
                        article = list.GetArticle(articleId);
                    }
                    else
                    {
                        var link = ArticleLink.Get(context.ObjectId, context.RecordId, articleId);
                        if (link != null)
                            article = link.Article;
                    }

                    if (article != null)
                        return article.Title;
                }

                return null;
            }
        }

        #endregion
    }
}