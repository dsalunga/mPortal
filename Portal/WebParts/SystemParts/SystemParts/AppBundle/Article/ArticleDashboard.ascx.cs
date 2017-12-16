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
using WCMS.Framework.Diagnostics;


namespace WCMS.WebSystem.WebParts.Article
{
    public partial class ArticleDashboard : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var sw = PerformanceLog.StartLog();

            /*string listTmpl = "";
            string itemTmpl = "";
            string firstItemTmpl = "";*/
            //bool hasDateFormatStringOverride = false;

            var context = new WContext(this);
            var element = context.Element;
            var parms = element.Parameters;

            // ShowEmptySubscriptions
            bool showEmptySubs = DataHelper.GetBool(WebParameter.GetValue(parms, "ShowEmptySubscriptions"), true);
            var titleMaxChars = DataHelper.GetInt32(WebParameter.GetValue(parms, "TitleMaxChars"), -1);

            // Tag
            var tagFilter = WebParameter.GetValue(parms, "TagFilter");

            // DateFilter
            bool dateFilterEnabled = false;
            string dateFilterParam = WebParameter.GetValue(parms, "DateFilterParameterName");
            int dateFilterDaysMargin = 2;
            var dateFilter = DateTime.Now;
            if (!string.IsNullOrEmpty(dateFilterParam))
            {
                var dateFilterValue = context.Get(dateFilterParam);
                if (!string.IsNullOrEmpty(dateFilterValue))
                {
                    dateFilter = DataHelper.GetDateTime(dateFilterValue);
                    dateFilterDaysMargin = DataHelper.GetInt32(WebParameter.GetValue(parms, "DateFilterDaysMargin", "2"));
                    dateFilterEnabled = true;
                }
                else
                {
                    DisplayEmptyContent(parms);
                    return;
                }
            }

            // Check if the properties will be overriden from Page Parameters
            /*if (!usePageParm)
            {
                // Display Item Count

                //var dateFormatStringOverride = WebParameter.GetValue(parms, ArticleConstants.DateFormatStringKey);
                //if (!string.IsNullOrEmpty(dateFormatStringOverride))
                //{
                //    hasDateFormatStringOverride = true;
                //    dateFormatString = dateFormatStringOverride;
                //}
            }*/

            var valueProviders = context.ValueProvider;
            string templateFile = string.Empty;

            #region Get subscriptions

            // Get all subscriptions
            var subs = new List<WebSubscription>();
            var ignoreGroups = element.GetParameterValue(ArticleConstants.IgnoreGroupsKey);
            var subMode = element.GetParameterValue(ArticleConstants.ModeKey, SubscriptionModes.AllGroups.ToString());
            var partId = WPart.Get("Article").Id;

            switch (DataHelper.GetInt32(subMode))
            {
                case SubscriptionModes.AllGroups:
                    GetAllUserGroupSubscriptions(subs, partId, ignoreGroups);
                    break;

                case SubscriptionModes.GroupSpecific:
                    var subGroup = element.GetParameterValue(ArticleConstants.GroupKey);
                    if (!string.IsNullOrEmpty(subGroup))
                    {
                        var g = WebGroup.Get(subGroup);
                        if (g != null)
                            subs.AddRange(from sub in WebSubscription.GetList(WebObjects.WebGroup, g.Id, partId)
                                          where sub.IsAllowed
                                          select sub);
                    }
                    break;

                case SubscriptionModes.ByInstance:
                    GetInstanceSubscriptions(context, subs, partId);
                    break;

                case SubscriptionModes.AllGroupsPlusInstance:
                    GetAllUserGroupSubscriptions(subs, partId, ignoreGroups);
                    GetInstanceSubscriptions(context, subs, partId);
                    break;
            }

            #endregion

            if (string.IsNullOrEmpty(context.Get(Article.ArticleKey)))
            {
                bool usePageParm = DataHelper.GetBool(WebParameter.GetValue(parms, ArticleConstants.UsePageParameterKey));
                bool thereAreItems = false;
                foreach (var sub in subs)
                {
                    var page = sub.Page;
                    if (page != null)
                    {
                        var itemCount = DataHelper.GetInt32(WebParameter.GetValue(parms, ArticleConstants.MaxDisplayItemKey), WConstants.NullData);
                        var listTmpl = WebParameter.GetValue(parms, ArticleConstants.ListTemplateKey, ArticleConstants.ListTemplate);
                        var itemTmpl = WebParameter.GetValue(parms, ArticleConstants.ItemTemplateKey, ArticleConstants.ItemTemplate);
                        var dateFormat = WebParameter.GetValue(parms, ArticleConstants.DateFormatStringKey, ArticleConstants.DateFormatString);
                        string firstItemTmpl = WebParameter.GetValue(parms, ArticleConstants.FirstItemTemplateKey, null);

                        var config = ArticleList.Get(WebObjects.WebPage, sub.PageId);

                        // Check if using Page Parameters
                        if (usePageParm)
                        {
                            if (config != null)
                            {
                                var template = config.Template;
                                listTmpl = template.ListTemplate;
                                itemTmpl = template.ListItemTemplate;
                                dateFormat = template.DateFormat;

                                //if (!hasDateFormatStringOverride && !string.IsNullOrEmpty(config.DateFormatString))
                                //    dateFormatString = config.DateFormatString;
                            }
                            else
                            {
                                var refParms = page.Parameters;
                                listTmpl = WebParameter.GetValue(refParms, ArticleConstants.ListTemplateKey, ArticleConstants.ListTemplate);
                                itemTmpl = WebParameter.GetValue(refParms, ArticleConstants.ItemTemplateKey, ArticleConstants.ItemTemplate);
                                dateFormat = WebParameter.GetValue(refParms, ArticleConstants.DateFormatStringKey, ArticleConstants.DateFormatString);

                                if (itemCount == WConstants.NullData)
                                    itemCount = DataHelper.GetInt32(WebParameter.GetValue(refParms, ArticleConstants.MaxDisplayItemKey), WConstants.NullData);

                                if (firstItemTmpl == null)
                                    firstItemTmpl = WebParameter.GetValue(refParms, ArticleConstants.FirstItemTemplateKey, null);
                            }
                        }

                        if (itemCount == WConstants.NullData)
                            itemCount = ArticleConstants.MaxDisplayItem;

                        if (firstItemTmpl == null)
                            firstItemTmpl = ArticleConstants.ItemTemplate;

                        // Filter By Date
                        var dateFilterStart = DateTime.Now;
                        var dateFilterEnd = DateTime.Now;
                        if (dateFilterEnabled)
                        {
                            dateFilterStart = dateFilterDaysMargin > 1 ? dateFilter.AddDays(dateFilterDaysMargin * -1).Date : dateFilter.Date;
                            dateFilterEnd = dateFilterDaysMargin > 1 ? dateFilter.AddDays(dateFilterDaysMargin).Date : dateFilter.Date;
                        }

                        var articles = (from a in config.Articles
                                        where (string.IsNullOrEmpty(tagFilter) || a.Tags.Contains(tagFilter)) &&
                                              (!dateFilterEnabled || (a.Date.Date >= dateFilterStart && a.Date.Date <= dateFilterEnd))
                                        select a)
                                        .OrderByDescending(i => i.Date)
                                        .Take(itemCount);

                        var articleCount = articles.Count();
                        if (showEmptySubs || (!showEmptySubs && articleCount > 0))
                        {
                            string basePath = page.BuildRelativeUrl();

                            if (!thereAreItems && articleCount > 0)
                                thereAreItems = true;

                            //#region Article List Configuration

                            ////templateId = config.TemplateId; // Get Template ID
                            //if (!hasDateFormatStringOverride && config != null && !usePageParm)
                            //{
                            //    if (string.IsNullOrEmpty(config.DateFormatString))
                            //        dateFormatString = ArticleConstants.DateFormatString;
                            //    else
                            //        dateFormatString = config.DateFormatString;
                            //}

                            //#endregion

                            #region Generate List Content

                            context.BasePath = basePath;

                            var listContent = new StringBuilder();
                            bool useFirstItemTemplate = !string.IsNullOrEmpty(firstItemTmpl);

                            foreach (var article in articles)
                            {
                                context.Set(Article.ArticleKey, article.Id);

                                var itemProvider = new NamedValueProvider();
                                itemProvider.Add("Title", titleMaxChars > 0 ? DataHelper.GetStringPreview(article.Title, titleMaxChars) : article.Title);
                                itemProvider.Add("Date", string.Format(dateFormat, article.Date));
                                itemProvider.Add("ShortDate", article.Date.ToString(article.Date.Year == DateTime.Now.Year ? ArticleConstants.ShortDateFormat : ArticleConstants.ShortDateFormatFull));
                                itemProvider.Add("Content", article.Content);
                                itemProvider.Add("Description", article.Description);
                                itemProvider.Add("Image", article.Image);
                                itemProvider.Add("Author", article.Author);
                                itemProvider.Add("Link", context.BuildQuery());

                                valueProviders[Substituter.DefaultProviderKey] = itemProvider;

                                if (useFirstItemTemplate)
                                {
                                    listContent.Append(Substituter.Substitute(firstItemTmpl, valueProviders));
                                    useFirstItemTemplate = false;
                                }
                                else
                                {
                                    listContent.Append(Substituter.Substitute(itemTmpl, valueProviders));
                                }
                            }

                            #endregion

                            #region Apply List Template and add to container

                            var listProvider = new NamedValueProvider();
                            listProvider.Add("PageTitle", page.BuildTitle());
                            listProvider.Add("ItemCount", articles.Count());
                            listProvider.Add("BasePath", basePath);
                            listProvider.Add("Content", listContent.ToString());

                            valueProviders[Substituter.DefaultProviderKey] = listProvider;

                            var litMain = new Literal();
                            litMain.Text = Substituter.Substitute(listTmpl, valueProviders);
                            this.Controls.Add(litMain);

                            #endregion
                        }
                    }
                }

                if (!thereAreItems)
                    DisplayEmptyContent(parms);
            }

            PerformanceLog.EndLog(string.Format("Article Dashboard: {0}/{1}", context.ObjectId, context.RecordId), sw, context.PageId);
        }

        private void DisplayEmptyContent(IEnumerable<WebParameter> parms)
        {
            var emptyContentTmpl = WebParameter.GetValue(parms, "EmptyContentTemplate");
            if (!string.IsNullOrEmpty(emptyContentTmpl))
            {
                var litMain = new Literal();
                litMain.Text = emptyContentTmpl;
                this.Controls.Add(litMain);
            }
        }

        private static void GetInstanceSubscriptions(WContext context, List<WebSubscription> subs, int partId)
        {
            subs.AddRange(from sub in WebSubscription.GetList(context.ObjectId, context.RecordId, partId)
                          where sub.IsAllowed
                          select sub);
        }

        private static void GetAllUserGroupSubscriptions(List<WebSubscription> subs, int partId, string ignoreGroups)
        {
            var ignoreList = DataHelper.ParseDelimitedStringToList(ignoreGroups, AccountConstants.AccountDelimiter);
            var groups = WSession.Current.User.Groups;

            foreach (var g in groups)
            {
                if (ignoreList.Count == 0 || !ignoreList.Contains(g.Name))
                {
                    subs.AddRange(from sub in WebSubscription.GetList(WebObjects.WebGroup, g.Id, partId)
                                  where sub.IsAllowed
                                  select sub);
                }
            }
        }
    }
}