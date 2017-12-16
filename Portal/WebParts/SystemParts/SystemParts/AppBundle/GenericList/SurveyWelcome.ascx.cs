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
    ///		Summary description for SurveyWelcome.
    /// </summary>
    public partial class SurveyWelcome : System.Web.UI.UserControl
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WContext ctx = new WContext(this);
                int surveyId = ctx.GetId("ListId");

                if (surveyId > 0)
                {
                    using (SqlDataReader r = SqlHelper.ExecuteReader("GenericList_Get",
                               new SqlParameter("@ListId", surveyId)
                               ))
                    {
                        if (r.Read())
                        {
                            ViewState["ListId"] = r["Id"].ToString();
                            lblTitle.Text = r["Title"].ToString();
                            lblMessage.Text = r["Description"].ToString();
                        }
                    }
                }
                else
                {
                    using (SqlDataReader r = SqlHelper.ExecuteReader("GenericListLink_Get",
                               new SqlParameter("@RecordId", ctx.RecordId),
                               new SqlParameter("@ObjectId", ctx.ObjectId)))
                    {
                        if (r.Read())
                        {
                            ViewState["ListId"] = r["ListId"].ToString();
                            lblTitle.Text = r["Title"].ToString();
                            lblMessage.Text = r["Description"].ToString();
                        }
                    }
                }
            }
        }

        protected void cmdStart_Click(object sender, System.EventArgs e)
        {
            WContext query = new WContext(this);

            int siteId = DataHelper.GetId(ViewState["ListId"]);

            /*
            if(Session["ResponseID"] == null)
            {
                // CREATE NEW
                object obj = SqlHelper.ExecuteScalar("CustomForms.UPDATE_Responses",
                    new SqlParameter("@ListId", int.Parse(sSurveyID)),
                    new SqlParameter("@IsCompleted", false)
                    );

                //Session["ResponseID"] = obj.ToString();
                qs["_R" + sSurveyID] = obj.ToString();
            }
            else
            {
                // DONT
            }
            */

            if (siteId > 0)
            {
                query.SetOpen("ListItem");
                query["ListId"] = siteId.ToString();
                query.Redirect();
            }
        }
    }
}
