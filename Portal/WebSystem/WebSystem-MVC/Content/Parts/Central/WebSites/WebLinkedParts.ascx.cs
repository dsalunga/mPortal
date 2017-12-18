using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.WebSites
{
    public partial class WebLinkedParts : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public DataSet Select(int pageId)
        {
            WebPartConfig config = null;

            return DataHelper.ToDataSet(
                from item in WHelper.GetLinkedParts(pageId)
                where (config = item.PartConfig) != null
                select new
                {
                    Id = item.PartConfigId,
                    item.PartConfigId,
                    item.LinkedPartControlId,
                    PartConfigName = config.Name,
                    PartName = config.Part.Name
                }
            );
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);
            WContext context = new WContext(this);
            context.Set("PartConfigId", id);

            switch (e.CommandName)
            {
                case "Custom_Exec":
                    context.Redirect(CentralPages.LoaderMain);
                    break;
            }
        }
    }
}