namespace WCMS.WebSystem.WebParts.Newsletter
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    using WCMS.Common.Utilities;
    using WCMS.Framework;
    using WCMS.Framework.Core;

    /// <summary>
    ///		Summary description for CONTENTCMS_eNewsletter.
    /// </summary>
    public partial class CONTENTCMS_eNewsletter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                btnDelete.Attributes.Add("onclick", "return confirm('Delete Selected Item(s)');");

                MultiView1.SetActiveView(viewGrid);
            }
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            MultiView1.SetActiveView(viewEdit);

            btnSave.Visible = true;
            btnUpdate.Visible = false;
            litID.Text = string.Empty;
            txtTitle.Text = string.Empty;
            fckContent.Value = string.Empty;
            lblNotify.Text = string.Empty;
        }

        protected void btnCancel_Click(object sender, System.EventArgs e)
        {
            MultiView1.SetActiveView(viewGrid);
        }

        protected void btnDelete_Click(object sender, System.EventArgs e)
        {
            string strForm = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(strForm))
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, 
                    "DELETE Newsletter.eNewsletterEmail WHERE(ENLEmailID) IN(" + strForm + ");"
                );

                GridView1.DataBind();
            }
        }

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            int intID = 0;
            using (SqlDataReader dr = SqlHelper.ExecuteReader("Newsletter.SELECT_eNewsletterEmailDuplicate", 
                new SqlParameter("@ENLEmailID", intID), 
                new SqlParameter("@Title", txtTitle.Text)
                ))
            {
                if (dr.HasRows)
                {
                    lblNotify.Text = "Entry Exists";
                    return;
                }
            }

            // BEGIN UPDATE
            SqlHelper.ExecuteNonQuery("Newsletter.INSERT_eNewsletterEmail", 
                new SqlParameter("@Title", txtTitle.Text),
                new SqlParameter("@SenderEmail", txtEmail.Text.Trim()),
                new SqlParameter("@Content", fckContent.Value)
                );

            MultiView1.SetActiveView(viewGrid);
            GridView1.DataBind();
        }

        protected void btnUpdate_Click(object sender, System.EventArgs e)
        {
            using (SqlDataReader dr = SqlHelper.ExecuteReader("Newsletter.SELECT_eNewsletterEmailDuplicate", 
                new SqlParameter("@ENLEmailID", int.Parse(litID.Text)), 
                new SqlParameter("@Title", txtTitle.Text)
                ))
            {
                if (dr.HasRows)
                {
                    lblNotify.Text = "Entry Exists";
                    return;
                }
            }

            SqlHelper.ExecuteNonQuery("Newsletter.UPDATE_eNewsletterEmail", 
                new SqlParameter("@ENLEmailID", int.Parse(litID.Text)), 
                new SqlParameter("@Title", txtTitle.Text), 
                new SqlParameter("@SenderEmail", txtEmail.Text.Trim()),
                new SqlParameter("@Content", fckContent.Value)
                );

            MultiView1.SetActiveView(viewGrid);
            GridView1.DataBind();
        }

        protected void cmdParse_Click(object sender, System.EventArgs e)
        {
            this.fckContent.Value = EncodeAbsolutePath(this.fckContent.Value);
        }

        private string EncodeAbsolutePath(string src)
        {
            string dest = src;

            string sPath = WebRegistry.SelectNode("System.WebPath").Value.TrimEnd('/');

            dest = dest.Replace("=\"/Assets/Uploads/", "=\"" + sPath + "/Assets/Uploads/");
            dest = dest.Replace("=\"Assets/Uploads/", "=\"" + sPath + "/Assets/Uploads/");
            dest = dest.Replace("=\"/_CSS/", "=\"" + sPath + "/_CSS/");
			dest = dest.Replace("=\"_CSS/", "=\"" + sPath + "/_CSS/");
            dest = dest.Replace("=\"_js/", "=\"" + sPath + "/_js/");
			dest = dest.Replace("=\"/_js/", "=\"" + sPath + "/_js/");
			dest = dest.Replace("=\"?", "=\"" + sPath + "/?");
			dest = dest.Replace("=\".?", "=\"" + sPath + "/?");

			return dest;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strID = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                case "edit_item":
                    using (SqlDataReader dr = SqlHelper.ExecuteReader("Newsletter.SELECT_eNewsletterEmailID", 
                        new SqlParameter("@ENLEmailID", int.Parse(strID))
                        ))
                    {
                        if (dr.Read())
                        {
                            litID.Text = dr["ENLEmailID"].ToString();
                            txtTitle.Text = dr["Title"].ToString();
                            txtEmail.Text = dr["SenderEmail"].ToString();
                            fckContent.Value = dr["Content"].ToString();

                            MultiView1.SetActiveView(viewEdit);

                            btnSave.Visible = false;
                            btnUpdate.Visible = true;
                            lblNotify.Text = string.Empty;
                        }

                    }
                    break;
                case "download_item":
                    string strhtm = "eNewsletter.htm";
                    string strPATH = Server.MapPath("/_CMS/Temp/Newsletter/" + strhtm);
                    using (SqlDataReader dr = SqlHelper.ExecuteReader("Newsletter.SELECT_eNewsletterEmailID", 
                        new SqlParameter("@ENLEmailID", int.Parse(strID))
                        ))
                    {
                        if (dr.Read())
                        {
                            string sDir = Path.GetDirectoryName(strPATH);
                            if (!Directory.Exists(sDir))
                            {
                                Directory.CreateDirectory(sDir);
                            }

                            FileStream fs = new FileStream(strPATH, FileMode.Create, FileAccess.Write);
                            StreamWriter sw = new StreamWriter(fs);
                            sw.Write(dr["Content"].ToString());
                            sw.WriteLine(string.Empty);
                            sw.Close();
                        }
                        else
                        {
                            return;
                        }
                    }
                    Response.AppendHeader("content-disposition", "attachment; filename=" + strhtm);
                    Response.WriteFile(strPATH);
                    Response.End();
                    break;
            }
        }
    }
}