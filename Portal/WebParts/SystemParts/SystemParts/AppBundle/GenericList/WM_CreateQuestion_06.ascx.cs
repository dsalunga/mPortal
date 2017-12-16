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
    ///		Summary description for CreateQuestion.
    /// </summary>
    public partial class CreateQuestion : System.Web.UI.UserControl
    {
        private string sQID;
        private string sSID;
        private string sPID;


        private void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here

            sSID = Request["ListId"];
            sQID = Request["ColumnId"];
            sPID = Request["PartitionId"];

            if (!Page.IsPostBack)
            {
                if (sQID != null && sQID != string.Empty)
                {
                    using (SqlDataReader r = SqlHelper.ExecuteReader(CommandType.Text, "SELECT * FROM GenericListColumn WHERE Id=@ColumnId",
                               new SqlParameter("@ColumnId", int.Parse(sQID))))
                    {
                        if (r.Read())
                        {
                            txtQLabel.Text = r["Label"].ToString();
                            txtQRanking.Text = r["Rank"].ToString();

                            try
                            {
                                chkHorizontal.Checked = DataHelper.GetInt32(r["IsHorizontal"]) == 1;
                            }
                            catch { }

                            try
                            {
                                chkRequired.Checked = DataHelper.GetInt32(r["IsRequired"]) == 1;
                            }
                            catch { }
                        }
                    }
                }
            }
        }

        protected void cmdQUpdate_Click(object sender, System.EventArgs e)
        {
            string sLabel = txtQLabel.Text.Trim();
            int isHorizontal = chkHorizontal.Checked ? 1 : 0;
            int isRequired = chkRequired.Checked ? 1 : 0;
            int iRank = 0;

            var query = new WQuery(this.Request.QueryString);

            try
            {
                iRank = int.Parse(txtQRanking.Text.Trim());
            }
            catch { }

            if (sQID != null && sQID != string.Empty)
            {
                // UPDATE

                SqlHelper.ExecuteNonQuery("GenericListColumn_Set",
                    new SqlParameter("@Label", sLabel),
                    new SqlParameter("@Rank", iRank),
                    new SqlParameter("@PartitionId", int.Parse(sPID)),
                    new SqlParameter("@ColumnId", int.Parse(sQID)),
                    new SqlParameter("@IsHorizontal", isHorizontal),
                    new SqlParameter("@IsRequired", isRequired)
                    );
            }
            else
            {
                // INSERT

                object obj = SqlHelper.ExecuteScalar("GenericListColumn_Set",
                    new SqlParameter("@Label", sLabel),
                    new SqlParameter("@Rank", iRank),
                    new SqlParameter("@PartitionId", int.Parse(sPID)),
                    new SqlParameter("@ListId", int.Parse(sSID)),
                    new SqlParameter("@IsHorizontal", isHorizontal),
                    new SqlParameter("@IsRequired", isRequired)
                    );


            }

            query.Remove("ColumnId");
            query.LoadAndRedirect("WM_SurveyQuestions_05.ascx");
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            var query = new WQuery(this.Request.QueryString);
            query.Remove("ColumnId");
            query.LoadAndRedirect("WM_SurveyQuestions_05.ascx");
        }
    }
}
