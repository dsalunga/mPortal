namespace WCMS.WebSystem.WebParts.Search
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    using WCMS.Common.Utilities;
    using WCMS.Framework;

    /// <summary>
    ///		Summary description for GenericSearch.
    /// </summary>
    public partial class SearchView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //txtSearch.Text = "Enter text";
                //txtSearch.Attributes.Add("onclick", "if(this.value=='Enter text'){ this.value=''; }");
                //txtSearch.Attributes.Add("onblur", "if(this.value==''){ this.value='Enter text'; }");
            }
        }

        protected void cmdFind_Click(object sender, System.EventArgs e)
        {
            string sSearch = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(sSearch)) // || sSearch == "Enter text")
            {
                txtSearch.Text = "Enter valid keyword";
                return;
            }

            WContext context = new WContext(this);
            context["SS"] = "Search"; // TODO: change this to load the specific control
            context["Query"] = sSearch;
            context["P"] = "2";

            Control rbl = this.FindControl("RadioButtonList1");
            if (rbl != null)
            {
                if (((RadioButtonList)rbl).Items[1].Selected)
                {
                    context["Search"] = "All";
                }
            }

            context.Redirect();
        }
    }
}