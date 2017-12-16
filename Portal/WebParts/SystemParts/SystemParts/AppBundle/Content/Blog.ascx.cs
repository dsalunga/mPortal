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

public partial class _Sections_C_Blog : System.Web.UI.UserControl
{
    ControlHelper ch;
    protected void Page_Load(object sender, EventArgs e)
    {
        ch = new ControlHelper(this);
        {
            LoadData();
        }
    }
    private void LoadData()
    {
        Literal litCustom = new Literal();
        using (SqlDataReader dr = SqlHelper.ExecuteReader("C.LocationSelect", new SqlParameter("@PageType", ch.PageType), new SqlParameter("@ItemID", ch.ItemID), new SqlParameter("@IsOut", false)))
        {
            while (dr.Read())
            {
                string strStyle = dr["Style"].ToString();
                string strContent = dr["Content"].ToString();

                Control ctlContent = LoadControl("Templates/Content.ascx");
                Control ctl = LoadControl("FeedBack.ascx");
                ((Literal)ctlContent.FindControl("litContent")).Text = strContent;

                if (String.IsNullOrEmpty(strStyle))
                {
                    this.Controls.Add(ctlContent);
                    this.Controls.Add(ctl);
                }
                else
                {
                    HtmlGenericControl div = new HtmlGenericControl("div");
                    div.Attributes.Add("style", strStyle);
                    div.Controls.Add(ctlContent);
                    div.Controls.Add(ctl);
                    this.Controls.Add(div);
                }
               
                
            }
        }
    }
}
