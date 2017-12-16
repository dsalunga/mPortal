using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Article
{
    public partial class EmailPreview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int id = DataHelper.GetId(Request, "ArticleId");
                int pageId = DataHelper.GetId(Request, "PageId");
                Article item = Article.Get(id);
                var page = WPage.Get(pageId);
                if (item != null && page != null)
                {
                    string permalink = string.Format("{0}?ArticleId={1}", page.BuildAbsoluteUrl(), item.Id);

                    lblTitle.InnerHtml = item.Title;
                    this.Page.Title = item.Title;
                    lblContent.InnerHtml = item.Content;
                    lblPublishedDate.InnerHtml = string.Format("Date Published:&nbsp;{0:MMMM dd, yyyy}", item.Date);
                    linkPermalink.InnerHtml = permalink;
                    linkPermalink.HRef = permalink;
                }
            }
        }
    }
}