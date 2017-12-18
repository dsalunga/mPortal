using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.BranchLocator
{
    public partial class WebOfficeHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var query = new WQuery(this);
                int id = query.GetId(WebColumns.Id);
                if (id > 0)
                {
                    var chapter = MChapter.Provider.Get(id);
                    if (chapter != null)
                        lblName.InnerHtml = chapter.Name;

                    query.Set(WConstants.Load, "FindALocale/Admin/Chapter");
                    linkProperties.HRef = query.BuildQuery();

                    query.BasePath = CentralPages.LoaderRazor;
                    query.Set(WConstants.Load, "FindALocale/Admin/SetLocation");
                    linkSetLocation.HRef = query.BuildQuery();
                }

                lblBreadcrumb.InnerHtml = FALHelper.BuildAdminBreadcrumb(new WQuery(this));
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            int id = query.GetId(WebColumns.Id);
            if (id > 0)
            {
                MChapter.Provider.Delete(id);
                query.Remove(WebColumns.Id);
                query.Set(WConstants.Load, "FindALocale/Admin/Chapters");
                query.Redirect();
            }
        }
    }
}