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
    ///		Summary description for c_Surveys.
    /// </summary>
    public partial class c_Surveys : System.Web.UI.UserControl
    {
        //private string sSID, sSSID, sIID, sLocation;
        //private int iID;

        private void Page_Load(object sender, System.EventArgs e)
        {
            //sIID = Request["SitePageItemID"];
            //sSSID = Request["SiteSectionID"];
            //sSID = Request["SiteID"];
            //sLocation = Request["PageType"];

            //if(sSSID==null || sSSID==string.Empty)
            //{
            //    //Response.Redirect(".");
            //}

            //switch(sSSID)
            //{
            //    //case "HOME":
            //    //    iID = int.Parse(sIID);
            //    //    //sLocation = "H";
            //    //    break;

            //    case "CONTENT":
            //        iID = int.Parse(sIID);
            //        //sLocation = "L";
            //        break;

            //    default:
            //        iID = int.Parse(sSSID);
            //        //sLocation = "S";
            //        break;
            //}

            if (!Page.IsPostBack)
            {
                cmdDelete.Attributes.Add("onclick", "return confirm('Remove Selected Items?');");

                // OTHER DATA (COMBO, ETC)
                this.LoadEtcData();

                // GRID DATA
                LoadData(0);
            }
        }

        private void LoadEtcData()
        {
            // LOAD SITES
            /*
            using (SqlDataReader r = SqlHelper.ExecuteReader("SPC_SELECT_Sites"))
            {
                cboSites.DataSource = r;
                cboSites.DataTextField = "SiteName";
                cboSites.DataValueField = "SiteID";
                cboSites.DataBind();
            }
            cboSites.Items.Insert(0, new ListItem("[ ALL ]", "-1"));
            cboSites.SelectedValue = sSID;
            */
        }

        private void LoadDataAll(int iPageIndex)
        {
            QueryParser qs = new QueryParser(this);
            ObjectKey key = ObjectKey.TryCreate(qs);

            //string sSiteID = cboSites.SelectedValue;

            // SHOW ALL UNINSERTED
            DataSet ds = SqlHelper.ExecuteDataSet("GenericList_GetLinkAll",
                new SqlParameter("@RecordId", key.RecordId),
                new SqlParameter("@ObjectId", key.ObjectId));

            /*
            if(sSiteID != "-1")
                ds.Tables[0].DefaultView.RowFilter = "SiteID = " + sSiteID;
            */

            if (iPageIndex != -1)
                grdAds.CurrentPageIndex = iPageIndex;

            // SORT & FILTER
            //ds.Tables[0].DefaultView.Sort = cboSort.SelectedValue + " " + cboOrder.SelectedValue;

            grdAds.DataSource = ds.Tables[0].DefaultView;
            grdAds.DataBind();
        }

        private void LoadDataInserted(int iPageIndex)
        {
            QueryParser qs = new QueryParser(this);
            ObjectKey key = ObjectKey.TryCreate(qs);

            // INSERTED ITEMS
            DataSet dsInserted = SqlHelper.ExecuteDataSet("GenericListLink_Get",
                new SqlParameter("@RecordId", key.RecordId),
                new SqlParameter("@ObjectId", key.ObjectId));

            /*
            if(iPageIndex != -1)
                grd.CurrentPageIndex = iPageIndex;	
            */

            grd.DataSource = dsInserted.Tables[0].DefaultView;
            grd.DataBind();
        }


        private void LoadData(int iPageIndex)
        {
            this.LoadDataInserted(iPageIndex);
            this.LoadDataAll(iPageIndex);
        }

        /*
        private void dtgArticles_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string strSiteArticleID = e.Item.Cells[0].Text;
        }

        private void dtgAllContents_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string strArticleID = e.Item.Cells[0].Text;
        }
        */

        private void grdAds_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.LoadDataAll(e.NewPageIndex);
        }

        private void cmdDelete_Click(object sender, System.EventArgs e)
        {
            QueryParser qs = new QueryParser(this);
            ObjectKey key = ObjectKey.TryCreate(qs);

            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM GenericListLink WHERE RecordId=" + key.RecordId +
                    " AND ObjectId=" + key.ObjectId +
                    " AND ListId IN (" + sChecked + ")");

                this.LoadData(0);
            }
        }

        private void cboSites_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.LoadDataAll(0);
        }

        private void grd_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.LoadDataInserted(e.NewPageIndex);
        }

        private void grdAds_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string ID = e.Item.Cells[0].Text;

            switch (e.CommandName)
            {
                case "edit":
                    var query = new WQuery(this);
                    query.LoadAndRedirect("WM_CreateSurvey_02.ascx");
                    query.Set("__p", ID);
                    query.Redirect();
                    break;
            }
        }

        private void cmdSort_Click(object sender, System.EventArgs e)
        {
            this.LoadDataAll(0);
        }

        protected void cmdInsert_Click(object sender, System.EventArgs e)
        {
            var query = new WQuery(this);
            var key = ObjectKey.TryCreate(query);
            var page = WPage.Get(query.GetId("PartitionId"));

            string sChecked = Request.Form["chkCheckedAll"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                var ids = DataHelper.ParseCommaSeparatedIdList(sChecked);
                foreach (int id in ids)
                {
                    SqlHelper.ExecuteNonQuery("GenericListLink_Set",
                        new SqlParameter("@RecordId", key.RecordId),
                        new SqlParameter("@ObjectId", key.ObjectId),
                        new SqlParameter("@ListId", id),
                        new SqlParameter("@SiteId", page.SiteId));
                }

                this.LoadData(0);
            }
        }

    }
}
