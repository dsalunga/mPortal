using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.WebSites
{
    public partial class WebPagePanels : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public DataSet Select(int pageId, int masterId)
        {
            WebPagePanel tempPanel = null;


            IEnumerable<WebTemplatePanel> panels = null;

            if (pageId > 0)
            {
                var page = WPage.Get(pageId);
                var panel = page.Panel;
                panels = WebTemplatePanel.Provider.GetList(panel.ObjectId, panel.RecordId);
            }
            else if(masterId > 0)
            {
                var masterPage = WebMasterPage.Get(masterId);
                panels = WebTemplatePanel.Provider.GetList(WebObjects.WebTemplate, masterPage.TemplateId);
            }

            var resultPanels = from panel in panels
                               select new
                               {
                                   panel.Id,
                                   panel.Name,
                                   panel.PanelName,
                                   PanelUsage = PanelUsage.ToString((tempPanel = WebPagePanel.Get(panel.Id, pageId)) == null ? -1 : tempPanel.UsageTypeId)
                               };

            return DataHelper.ToDataSet(resultPanels);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            QueryParser query = new QueryParser(this);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set(WebColumns.TemplatePanelId, e.CommandArgument);
                    query.Redirect(CentralPages.WebPagePanelHome);

                    break;

                case "View-Elements":
                    query.Set(WebColumns.TemplatePanelId, e.CommandArgument);
                    query.Redirect(CentralPages.WebPageElements);
                    break;
            }
        }
    }
}