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
    ///		Summary description for CreateSurvey.
    /// </summary>
    public partial class CreateSurvey : System.Web.UI.UserControl
    {
        private string sSID;

        private void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here

            sSID = Request.QueryString["ListId"];

            if (!Page.IsPostBack)
            {

                if (sSID != null && sSID != string.Empty)
                {
                    using (SqlDataReader r = SqlHelper.ExecuteReader(CommandType.Text, "SELECT * FROM GenericList WHERE Id=@Id",
                               new SqlParameter("@Id", int.Parse(sSID))))
                    {
                        if (r.Read())
                        {
                            txtTitle.Text = r["Title"].ToString();
                            txtdesc.Value = r["Description"].ToString();
                            txtEnding.Value = r["EndingText"].ToString();

                            try
                            {
                                chkActive.Checked = DataHelper.GetInt32(r["IsActive"]) == 1;
                            }
                            catch { }

                            try
                            {
                                chkPageTitle.Checked = DataHelper.GetInt32(r["ShowPageCaption"]) == 1;
                            }
                            catch { }
                        }
                    }
                }
            }
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            var query = new WQuery(this.Request.QueryString);
            query.Remove("ListId");
            query.LoadAndRedirect("WM_Surveys_01.ascx");
        }

        protected void cmdCreate_Click(object sender, System.EventArgs e)
        {
            string sTitle = txtTitle.Text.Trim();
            string sdesc = txtdesc.Value.Trim();
            string sEnding = txtEnding.Value.Trim();
            int isActive = chkActive.Checked ? 1 : 0;
            int showPageCaption = chkPageTitle.Checked ? 1 : 0;

            if (sSID != null && sSID != string.Empty)
            {
                // UPDATE

                SqlHelper.ExecuteNonQuery("GenericList_Set",
                    new SqlParameter("@ListId", int.Parse(sSID)),
                    new SqlParameter("@Title", sTitle),
                    new SqlParameter("@description", sdesc),
                    new SqlParameter("@IsActive", isActive),
                    new SqlParameter("@EndingText", sEnding),
                    new SqlParameter("@ShowPageCaption", showPageCaption)
                );

                //qs["_
            }
            else
            {
                // INSERT 

                object obj = SqlHelper.ExecuteScalar("GenericList_Set",
                    new SqlParameter("@Title", sTitle),
                    new SqlParameter("@description", sdesc),
                    new SqlParameter("@IsActive", isActive),
                    new SqlParameter("@EndingText", sEnding),
                    new SqlParameter("@ShowPageCaption", showPageCaption)
                );
            }

            var query = new WQuery(this.Request.QueryString);
            query.Remove("ListId");
            query.LoadAndRedirect("WM_Surveys_01.ascx");
        }
    }
}
