using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.WebSites
{
    public partial class WebPagePanelHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                QueryParser qs = new QueryParser(this);
                int id = qs.GetId(WebColumns.TemplatePanelId);
                if (id > 0)
                {
                    WebTemplatePanel panel = WebTemplatePanel.Get(id);
                    if (panel != null)
                    {
                        int pageId = qs.GetId(WebColumns.PageId);

                        // Check permission
                        var page = WPage.Get(pageId);
                        if (page != null)
                        {
                            var masterPage = page.MasterPage;
                            if (masterPage != null)
                            {
                                if (masterPage.IsUserMgmtPermitted(Permissions.ManageInstance)) { }
                                else if (masterPage.IsUserMgmtPermitted(Permissions.ManageContent))
                                    rowProperties.Visible = false;
                                else
                                    WHelper.ShowAccessDenied(masterPage.Site, qs);
                            }
                        }

                        lblHeader.InnerHtml = panel.Name;

                        linkProperties.HRef = qs.BuildQuery(CentralPages.WebPagePanel);

                        // PagePanelId must be present

                        
                        WebPagePanel item = WebPagePanel.Get(id, pageId);
                        if (item == null)
                        {
                            item = new WebPagePanel();
                            item.PageId = pageId;
                            item.TemplatePanelId = id;
                            item.Update();
                        }

                        QueryParser q = new QueryParser(qs);
                        q.Set(ObjectKey.KeyString, (new ObjectKey(WebObjects.WebPagePanel, item.Id)).ToString());
                        q.Set(ObjectKey.KeySource, qs.EncodedBasePath);

                        linkParameters.HRef = q.BuildQuery(CentralPages.WebParameters);
                    }
                }
            }
        }
    }
}