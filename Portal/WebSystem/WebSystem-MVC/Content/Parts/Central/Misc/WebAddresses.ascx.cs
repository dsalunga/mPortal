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

namespace WCMS.WebSystem.WebParts.Central.Misc
{
    public partial class WebAddresses : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);
            WContext query = new WContext(this);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set(WebColumns.Id, id);
                    query.SetOpen("Web-Address");
                    query.Redirect();
                    break;

                case "Custom_Delete":
                    var item = WebAddress.Provider.Get(id);
                    if (item != null)
                        item.Delete();

                    GridView1.DataBind();
                    break;
            }
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GridView1.DataBind();
        }

        public DataSet Select(int userId, string keyword)
        {
            string kwl = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();

            return DataHelper.ToDataSet(from i in WebAddress.Provider.GetList(WebObjects.WebUser, userId)
                                        select new
                                        {
                                            i.Id,
                                            i.Tag,
                                        });
        }

        protected void cmdNew_Click(object sender, EventArgs e)
        {
            WContext query = new WContext(this);
            query.SetOpen("Web-Address");
            query.Redirect();
        }
    }
}