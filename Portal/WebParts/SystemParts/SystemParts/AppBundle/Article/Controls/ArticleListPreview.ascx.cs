using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;


namespace WCMS.WebSystem.WebParts.Article.Controls
{
    public partial class ArticleListPreview : System.Web.UI.UserControl
    {
        private string dateFormatString = ArticleConstants.DateFormatString;

        public ArticleListPreview()
        {
            IsPermitted = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((PageId > 0 || (!string.IsNullOrEmpty(PageUrl) && PageUrl != "#")) && IsPermitted) //this.Visible)
            {
                LoadData();
            }
            else if (!string.IsNullOrWhiteSpace(AccessDeniedContent))
            {
                Literal litMain = new Literal();
                litMain.Text = AccessDeniedContent;
                this.Controls.Add(litMain);
            }
            else
            {
                this.Visible = false;
            }
        }

        private void LoadData()
        {
            var page = PageId > 0 ? WPage.Get(PageId) : WebRewriter.ResolvePage(PageUrl);
            if (page != null)
            {
                var config = ArticleList.Get(WebObjects.WebPage, page.Id);
                if (config != null)
                {
                    int itemCount = 5;
                    string listTemplate = "";
                    string itemTemplate = "";
                    string firstItemTemplate = "";

                    WContext context = new WContext(WContext.GetParent(this));
                    var parms = context.Element.Parameters;

                    // Check which ParameterSet to use
                    var parm = parms.FirstOrDefault(i => i.Name == ArticleConstants.ParameterSetNameKey);
                    var parmSetName = parm == null ? "" : parm.Value;

                    var element = string.IsNullOrEmpty(parmSetName) ? context.Element : (ParameterizedWebObject)WebParameterSet.Get(parmSetName);

                    // Display Item Count
                    itemCount = DataHelper.GetInt32(element.GetParameterValue(ArticleConstants.MaxDisplayItemKey, ArticleConstants.MaxDisplayItem.ToString()));
                    listTemplate = element.GetParameterValue(ArticleConstants.ListTemplateKey, ArticleConstants.ListTemplate);
                    itemTemplate = element.GetParameterValue(ArticleConstants.ItemTemplateKey, ArticleConstants.ItemTemplate);
                    firstItemTemplate = element.GetParameterValue(ArticleConstants.FirstItemTemplateKey, ArticleConstants.ItemTemplate);
                    dateFormatString = element.GetParameterValue(ArticleConstants.DateFormatStringKey, ArticleConstants.DateFormatString);

                    var articles = config.Articles.OrderByDescending(i => i.Date).Take(itemCount);
                    if (articles.Count() > 0)
                    {
                        var valueProviders = context.ValueProvider;

                        // Check WebPage parameter
                        parm = parms.FirstOrDefault(i => i.Name == ArticleConstants.UsePageParameterKey);
                        bool usePageParam = (parm != null && parm.Value == "1");

                        // Check if using Page Parameters
                        //if (usePageParam)
                        //    if (config != null)
                        //        if (!string.IsNullOrEmpty(config.DateFormatString))
                        //            dateFormatString = config.DateFormatString;


                        string basePath = page.BuildRelativeUrl();

                        //if (!usePageParam)
                        //{
                        //    dateFormatString = config.DateFormatString;

                        //    if (config.DateFormatString == string.Empty)
                        //        dateFormatString = ArticleConstants.DateFormatString;
                        //}

                        #region Generate List Content

                        context.BasePath = basePath;

                        StringBuilder listContent = new StringBuilder();
                        bool useFirstItemTemplate = !string.IsNullOrEmpty(firstItemTemplate);

                        foreach (var article in articles)
                        {
                            context.Set(Article.ArticleKey, article.Id);

                            NamedValueProvider itemProvider = new NamedValueProvider();
                            itemProvider.Add("Title", article.Title);
                            itemProvider.Add("Date", string.Format(dateFormatString, article.Date));
                            itemProvider.Add("ShortDate", article.Date.ToString(article.Date.Year == DateTime.Now.Year ? ArticleConstants.ShortDateFormat : ArticleConstants.ShortDateFormatFull));
                            itemProvider.Add("Content", article.Content);
                            itemProvider.Add("Description", article.Description);
                            itemProvider.Add("Image", article.Image);
                            itemProvider.Add("Author", article.Author);
                            itemProvider.Add("Link", context.BuildQuery());

                            valueProviders[Substituter.DefaultProviderKey] = itemProvider;

                            if (useFirstItemTemplate)
                            {
                                listContent.Append(Substituter.Substitute(firstItemTemplate, valueProviders));
                                useFirstItemTemplate = false;
                            }
                            else
                            {
                                listContent.Append(Substituter.Substitute(itemTemplate, valueProviders));
                            }
                        }

                        #endregion

                        #region Apply List Template and add to container

                        NamedValueProvider listProvider = new NamedValueProvider();
                        listProvider.Add("PageTitle", page.BuildTitle());
                        listProvider.Add("ItemCount", articles.Count());
                        listProvider.Add("BasePath", basePath);
                        listProvider.Add("Content", listContent.ToString());

                        valueProviders[Substituter.DefaultProviderKey] = listProvider;

                        Literal litMain = new Literal();
                        litMain.Text = Substituter.Substitute(listTemplate, valueProviders);
                        this.Controls.Add(litMain);

                        #endregion

                    }
                }
            }
        }

        public string PageUrl { get; set; }

        public int PageId { get; set; }

        public bool IsPermitted { get; set; }

        public string AccessDeniedContent { get; set; }
    }
}