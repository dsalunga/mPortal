using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using des.Utils;

public partial class _Sections_P_FeedBack : System.Web.UI.UserControl
{
    protected ControlHelper ch;
    protected void Page_Load(object sender, EventArgs e)
    {
        ch = new ControlHelper(this.Parent);
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtFeed.Text.Trim()))
        {
            string strID ="";
            using (SqlDataReader dr = SqlHelper.ExecuteReader("C.LocationSelect", new SqlParameter("@PageType", ch.PageType), new SqlParameter("@ItemID", ch.ItemID), new SqlParameter("@IsOut", false)))
            {
                if (dr.Read())
                {
                    strID = dr["ID"].ToString();
                }
            }

            if (strID != "")
            {
                SqlHelper.ExecuteNonQuery("C.FeedInsertUpdate", new SqlParameter("@ID", Single.Parse(strID)), new SqlParameter("@Feed", txtFeed.Text.Trim()));
            }
        }
        pnlContent.Visible = false;
        pnlThank.Visible = true;
    }
}
