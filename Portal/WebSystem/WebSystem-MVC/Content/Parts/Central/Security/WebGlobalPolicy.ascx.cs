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

namespace WCMS.WebSystem.WebParts.Central.Security
{
    public partial class WebGlobalPolicyController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }

        public DataSet Select()
        {
            return DataHelper.ToDataSet(WebGlobalPolicy.Provider.GetList());
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            WContext context = new WContext(this);
            int id = DataHelper.GetId(e.CommandArgument);

            context.Set(WebColumns.GlobalPolicyId, id);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    context.Redirect(CentralPages.WebGlobalPolicyHome);
                    break;
            }
        }
    }
}