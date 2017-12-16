namespace WCMS.WebSystem.WebParts.GenericForm
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    using WCMS.Common.Utilities;
    using WCMS.Framework;

    /// <summary>
    ///		Summary description for SurveyStart.
    /// </summary>
    public partial class SurveyStart : System.Web.UI.UserControl
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WContext context = new WContext(this);

                using (SqlDataReader r = SqlHelper.ExecuteReader("GenericListLink_Get",
                           new SqlParameter("@RecordId", context.RecordId),
                           new SqlParameter("@ObjectId", context.ObjectId)))
                {
                    grdSurveys.DataSource = r;
                    grdSurveys.DataBind();
                }
            }
        }

        protected void grdSurveys_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string sID = e.Item.Cells[0].Text;
            WContext query = new WContext(this);

            switch (e.CommandName)
            {
                case "survey":
                    //qs["P"] = "2";
                    //qs["SS"] = "SV";
                    //qs["ListId"] = sID;

                    query.SetOpen("ListItem");
                    query["ListId"] = sID;
                    query.Redirect();
                    break;
            }
        }
    }
}
