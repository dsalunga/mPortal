using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebPartConfigController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdAddCMS_Click(object sender, System.EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            query.Redirect(CentralPages.WebPartConfigEntry);
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            QueryParser query = new QueryParser(this);
            int partConfigId = DataHelper.GetId(e.CommandArgument);
            query.Set(WebColumns.PartConfigId, partConfigId);

            switch (e.CommandName)
            {
                case "edit_item":
                    query.Redirect(CentralPages.WebPartConfigEntry);
                    break;

                //case "Templates":
                //    qs["PageType"] = "CC";
                //    qs.Redirect("TemplateEditor.aspx");
                //    break;

                case "Custom_Delete":
                    WebPartConfig.Delete(partConfigId);
                    GridView2.DataBind();
                    break;
            }
        }

        public DataSet SelectConfig(int partId)
        {
            return DataHelper.ToDataSet(WebPartConfig.GetList(partId));
        }
    }
}