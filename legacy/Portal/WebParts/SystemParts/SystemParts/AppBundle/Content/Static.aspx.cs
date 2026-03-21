using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.ViewModel;
using WCMS.WebSystem.WebParts.Content;

namespace WCMS.WebSystem
{
    public partial class Static : WLoaderPageBase
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            int pageId = DataHelper.GetId(Request, WebColumns.PageIdInternal);
            WebPage page = null;
            if (pageId > 0 && (page = WebPage.Get(pageId)) != null)
            {
                // Had to declare the Page above and check the security here as the session should be present
                QueryParser query = new QueryParser(this);

                if (!CheckAccess(page, query, Request.RawUrl))
                    return;

                /*
                string loginPage = page.Site.AbsoluteLoginPage;
                int accessCheck = page.GetPublicAccess();

                if (WebSession.Current.IsAdministrator || accessCheck == PublicAccessCheckResult.Granted || (accessCheck == PublicAccessCheckResult.NotLoggedIn && WebSystemHelper.IsSameUrl(query.BasePath, loginPage)))
                {
                    // Granted or this is a login page
                }
                else
                {
                    // Can be NotLoggedIn or Denied
                    if (accessCheck == PublicAccessCheckResult.Denied)
                    {
                        string accessDenied = page.Site.AccessDeniedPage;

                        // Access Denied
                        query.Redirect(string.IsNullOrEmpty(accessDenied) ? WebConstants.AbsoluteAccessDeniedPage : accessDenied);
                    }
                    else if (!string.IsNullOrEmpty(loginPage))
                    {
                        // Login page
                        //qs.SetEncode(QueryParser.SourceKey, Request.RawUrl);
                        query.SetSource(Request.RawUrl);
                        query.Redirect(loginPage);
                    }
                    else
                    {
                        throw new Exception("Dont know where to redirect or login page is blank.");
                    }

                    return;
                }
                */

                if (page.PartControlTemplateId == 1)
                {
                    WebObjectContent objectContent = WebObjectContent
                                    .GetByObjectId(WebObjects.WebPage, pageId);

                    if (objectContent != null && !objectContent.Content.IsDraft)
                    {
                        Literal literal = new Literal();
                        literal.EnableViewState = false;
                        literal.Text = objectContent.Content.Content;

                        this.Controls.Add(literal);
                    }
                }
                else
                {
                    var control = LoadControl(page.PartControlTemplate.Path);

                    control.ID = WebPartContext.GenerateControlId(page.OBJECT_ID, page.Id);

                    this.Controls.Add(control);
                }
            }
        }
    }
}
