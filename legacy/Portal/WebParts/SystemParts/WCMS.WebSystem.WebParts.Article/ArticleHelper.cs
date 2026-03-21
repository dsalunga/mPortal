using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Article
{
    public abstract class ArticleHelper
    {
        private const string DOUBLE_TAB = WConstants.TAB;

        public static void ManageAttributes(string[] elementStyle, HtmlGenericControl control, int styleId)
        {
            if (!string.IsNullOrEmpty(elementStyle[styleId]))
                control.Attributes.Add("style", elementStyle[styleId]);
        }

        /// <summary>
        /// Get field value
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="strName"></param>
        /// <param name="isSingle"></param>
        /// <returns></returns>
        //public static string GetFieldValue(UserControl presenter, string basePath, int templateId, string[] elementStyle, bool titleIsLink, int articleId, string fieldName, bool isSingle, out int columnId)
        //{
        //    WContext context = new WContext(presenter);
        //    context.BasePath = basePath;

        //    ArticleColumn column = ArticleColumn.Get(fieldName, templateId, isSingle ? 1 : 0);
        //    columnId = (column != null) ? column.Id : 0;

        //    if (columnId > LinkColumns.SAuthor)
        //    {
        //        #region Some links

        //        if (columnId == LinkColumns.ItemLink)
        //        {
        //            // # For Article Link on list #
        //            columnId = LinkColumns.ItemDiv;
        //            context.Remove("ShowP");
        //            context.Remove("PageFor");
        //            context[Article.ArticleKey] = articleId.ToString();

        //            var query = LocatePair(context);

        //            return string.Format(ArticleConstants.LinkFormat, elementStyle[LinkColumns.ItemStyleOrClass], query.BuildQuery(), elementStyle[LinkColumns.ItemLink]);
        //        }
        //        else if (columnId == LinkColumns.BackLink)
        //        {
        //            // For Back Link
        //            columnId = LinkColumns.BackDiv;
        //            context.Remove(Article.ArticleKey);
        //            return string.Format(ArticleConstants.LinkFormat, elementStyle[LinkColumns.BackStyleOrClass], context.BuildQuery(), elementStyle[LinkColumns.BackLink]);
        //        }

        //        return string.Empty;

        //        #endregion
        //    }
        //    else
        //    {
        //        Article item = Article.Get(articleId);
        //        if (item != null)
        //        {
        //            switch (columnId)
        //            {
        //                case 0:
        //                    return string.Empty;

        //                case LinkColumns.SID:
        //                    return item.Id.ToString();

        //                case LinkColumns.STitle:
        //                    #region Title

        //                    if (titleIsLink && !isSingle)
        //                    {
        //                        // # For Article Details Link #
        //                        context.Remove("ShowP");
        //                        context.Remove("PageFor");
        //                        context[Article.ArticleKey] = articleId.ToString();

        //                        var query = LocatePair(context);

        //                        return string.Format(ArticleConstants.ListLinkFormat, query.BuildQuery(), elementStyle[2], item.Title);
        //                    }
        //                    else
        //                    {
        //                        return item.Title;
        //                    }

        //                    #endregion

        //                case LinkColumns.SImage:
        //                    return item.Image;

        //                case LinkColumns.SDescription:
        //                    return item.Description;

        //                case LinkColumns.SDate:
        //                    return string.Format(ArticleConstants.DateFormatString, item.Date);

        //                case LinkColumns.SContent:
        //                    return item.Content;

        //                case LinkColumns.SAuthor:
        //                    return item.Author;

        //                default:
        //                    return string.Empty;
        //            }
        //        }

        //        return string.Empty;
        //    }
        //}

        public static QueryParser LocatePair(WContext context)
        {
            QueryParser query = context.Query.Clone();
            var element = context.Element;
            if (element.OBJECT_ID == WebObjects.WebPageElement)
            {
                var pair = element.LocatePair("FullView");
                if (pair != null)
                    query.BasePath = pair.Page.BuildRelativeUrl();
            }
            return query;
        }

        public static ListItem[] PopulateRecursiveControl(bool includeRoot, int iParentID)
        {
            string siteName = WebRegistry.SelectNode(WebRegistry.WebNamePath).Value;
            string sTab = string.Empty;

            ListItemCollection items = new ListItemCollection();

            // COMBO BOX
            if (includeRoot)
            {
                ListItem itemRoot = new ListItem(siteName, "-1");
                items.Add(itemRoot);
                sTab = DOUBLE_TAB;
            }

            var sites = WSite.GetList();

            // START RECURSIVE
            LoadRecursiveList(iParentID, sites, items, sTab);

            ListItem[] lists = new ListItem[items.Count];
            items.CopyTo(lists, 0);

            return lists;
        }

        public static ListItem[] PopulateRecursiveControl(bool includeRoot)
        {
            return PopulateRecursiveControl(includeRoot, -1);
        }

        private static void LoadRecursiveList(int iParentID, IEnumerable<WSite> sites, ListItemCollection items, string sTab)
        {
            //DataRow[] rows = table.Select("ParentID=" + iParentID, "SiteName");
            var selectedSites = sites.Where(site => site.ParentId == iParentID);

            foreach (WSite site in selectedSites)
            {
                // COMBO BOX
                ListItem item1 = new ListItem(FormatTab(sTab) + site.Name, site.Id.ToString());
                items.Add(item1);

                LoadRecursiveList(site.Id, sites, items, sTab + DOUBLE_TAB);
            }
        }

        private static string FormatTab(string sTab)
        {
            return (sTab != string.Empty) ? sTab + "\u2022\u00a0" : string.Empty;
        }
    }
}
