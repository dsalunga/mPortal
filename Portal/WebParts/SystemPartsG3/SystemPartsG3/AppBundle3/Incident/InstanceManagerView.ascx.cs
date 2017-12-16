using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.WebSystem.Controls;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Incident
{
    public partial class InstanceManagerView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataUtil.GetId(e.CommandArgument);
            var query = new WQuery(this);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set(WebColumns.Id, id);
                    query.LoadAndRedirect("InstanceEditView.ascx");
                    break;

                case "Custom_Delete":
                    var item = IncidentInstance.Provider.Get(id);
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

        public DataSet Select(string keyword)
        {
            string kwl = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();

            return DataUtil.ToDataSet(
                from item in IncidentInstance.Provider.GetList()
                select new
                {
                    item.Id,
                    item.Name
                });
        }

        protected void cmdNew_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            query.LoadAndRedirect("InstanceEditView.ascx");
        }
    }
}