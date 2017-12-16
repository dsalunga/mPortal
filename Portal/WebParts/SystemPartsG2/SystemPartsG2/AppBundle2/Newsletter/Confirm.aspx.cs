using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Newsletter
{
	/// <summary>
	/// Summary description for Confirm.
	/// </summary>
	public partial class Confirm : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
                aHome.Title = WebRegistry.SelectNode("System.WebName").Value;
                aHome.InnerHtml = aHome.Title;

				LoadData();
			}
		}
		
		private void LoadData()
		{
			QueryParser qs = new QueryParser(this.Request.QueryString);

			string strEID = qs["NewsletterID"];
			string strVer = qs["RequestCode"];
			string sMode = qs["Mode"];

			if(string.IsNullOrEmpty(strEID) || string.IsNullOrEmpty(strVer) || string.IsNullOrEmpty(sMode))
			{
                pMessage.InnerHtml = WebRegistry.SelectNode("Newsletter.ErrorMessage").Value;
				return;
			}

            using (SqlDataReader dr = SqlHelper.ExecuteReader("Newsletter.SELECT_eNewsLetterVerify",
                new SqlParameter("@eNewsletterID", int.Parse(strEID)),
                new SqlParameter("@VerifyCode", strVer)
                ))
            {
                if (dr.HasRows)
                {
                    //string[] sLinks = litNotify.Text.Trim().Split('$');

                    if (sMode == "1")
                    {
                        SqlHelper.ExecuteNonQuery("Newsletter.UPDATE_eNewsletter",
                            new SqlParameter("@eNewsletterID", int.Parse(strEID))
                            );
                        pMessage.InnerHtml = WebRegistry.SelectNode("Newsletter.Subscribed").Value;

                        //Response.Redirect(SystemSettings.GetSettings("ENL.VerifyMsgURL"), true);
                    }
                    else
                    {
                        // UNSUBSCRIBE

                        SqlHelper.ExecuteNonQuery("Newsletter.DELETE_Email_UNSUBSCRIBE",
                            new SqlParameter("@eNewsletterID", int.Parse(strEID))
                            );

                        //Response.Redirect(SystemSettings.GetSettings("ENL.UnsubscribeMsgURL"), true);
                        pMessage.InnerHtml = WebRegistry.SelectNode("Newsletter.Unsubscribed").Value;
                    }
                }
                else
                {
                    pMessage.InnerHtml = WebRegistry.SelectNode("Newsletter.ErrorMessage").Value;
                }
            }
		}
		
	}
}
