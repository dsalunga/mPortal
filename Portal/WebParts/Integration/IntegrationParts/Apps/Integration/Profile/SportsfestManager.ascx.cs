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
    public partial class SportsfestManager : System.Web.UI.UserControl
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
            QueryParser qs = new QueryParser(this);

            switch (e.CommandName)
            {
                case "Custom_Delete":
                    var item = Sportsfest.Provider.Get(id);
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

            var countries = Country.GetList();
            Country country = null;

            return DataHelper.ToDataSet(
                from item in Sportsfest.Provider.GetList()
                select new
                {
                    item.Id,
                    item.Name,
                    item.GroupColor,
                    item.ShirtSize,
                    item.Age,
                    item.Mobile,
                    item.EntryDate,
                    item.Sports,
                    item.Locale,
                    item.Suggestion,
                    Country = (country = countries.FirstOrDefault(i => item.CountryCode > 0 && i.CountryCode == item.CountryCode)) != null ? country.CountryName : string.Empty
                }
            );
        }

        protected void cmdDownload_Click(object sender, EventArgs e)
        {
            WebHelper.DownloadAsCsv(Select(), "Sportsfest");
        }

        protected void cmdDownloadXml_Click(object sender, EventArgs e)
        {
            WebHelper.DownloadAsXml(Select(), "Sportsfest", "Registration");
        }
    }
}