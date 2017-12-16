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

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public partial class AdminLocationManager : System.Web.UI.UserControl
    {
        protected TabControl tabView;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.InitTabControl();
        }

        private void InitTabControl()
        {
            // Setup tab navigation
            tabView.AddTab("tabManage", "Manage Locations");
            tabView.AddTab("tabBooking", "Availability by Date");
            tabView.AddTab("tabAvailable", "Availability by Room");
            tabView.SelectedTabChanged += new EventHandler<TabEventArgs>(
                delegate(object oSender, TabEventArgs args)
                {
                    switch (args.TabName)
                    {
                        case "tabManage":
                            tabView.SelectedIndex = 0;
                            break;

                        case "tabBooking":
                            {
                                tabView.SelectedIndex = 1;
                                break;
                            }

                        case "tabAvailable":
                            {
                                tabView.SelectedIndex = 2;

                                break;
                            }
                    }
                });
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);
            var query = new WQuery(this);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set(WebColumns.Id, id);
                    query.LoadAndRedirect("AdminLocation.ascx");
                    break;

                case "Custom_Delete":
                    var item = CalendarLocation.Provider.Get(id);
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

            return DataHelper.ToDataSet(
                from item in CalendarLocation.Provider.GetList()
                select new
                {
                    item.Id,
                    item.Name,
                    item.Bookable
                }
            );
        }

        protected void cmdNew_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            query.LoadAndRedirect("AdminLocation.ascx");
        }
    }
}