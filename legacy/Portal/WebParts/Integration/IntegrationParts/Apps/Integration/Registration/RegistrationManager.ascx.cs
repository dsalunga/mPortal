using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core.Shared;

using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile
{
    public partial class RegistrationManager : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataUtil.GetId(e.CommandArgument);
            QueryParser query = new QueryParser(this);

            switch (e.CommandName)
            {
                case "Custom_Delete":
                    var item = GenericRegistration.Provider.Get(id);
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

        public DataSet Select(string keyword = "")
        {
            string kwl = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();

            return DataUtil.ToDataSet(
                from item in GenericRegistration.Provider.GetList()
                select new
                {
                    item.Id,
                    item.Country,
                    item.Locale,
                    item.ExternalId,
                    item.Name,
                    item.Age,
                    item.Designation,
                    item.ArrivalDate,
                    item.Airline,
                    item.FlightNo,
                    item.DepartureDate,
                    item.Address,
                    item.PlaceType,
                    item.EntryDate,
                    item.Gender
                }
            );
        }

        protected void cmdDownload_Click(object sender, EventArgs e)
        {
            WebUtil.DownloadAsCsv(Select(), "Registration");
        }

        protected void cmdDownloadXml_Click(object sender, EventArgs e)
        {
            WebUtil.DownloadAsXml(Select(), "Registration", "Registration");
        }
    }
}