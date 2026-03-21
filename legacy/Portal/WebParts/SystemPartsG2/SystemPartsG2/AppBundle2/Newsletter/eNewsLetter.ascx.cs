namespace WCMS.WebSystem.WebParts.Newsletter
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Text;
	using System.Text.RegularExpressions;
	using System.Configuration;
    using System.Net;
    using System.Net.Mail;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using WCMS.Common.Utilities;
    using WCMS.Framework;
    using WCMS.Framework.Core;

	/// <summary>
	///		Summary description for eNewsletterHome.
	/// </summary>
	public partial class eNewsletter : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!Page.IsPostBack)
            {
                txtEmailAddress.Attributes.Add("onclick", "if(this.value=='Enter Your E-mail'){ this.value=''; }");
                txtEmailAddress.Attributes.Add("onblur", "if(this.value==''){ this.value='Enter Your E-mail'; }");
            }

            /*
            else
            {
                Response.Write(string.Format("<h1>{0}</h1>", "POSTBACK"));
                return;
            }
            */
		}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string sFormatString = WebRegistry.SelectNode("Newsletter.FormatString").Value;
            string sEmail = txtEmailAddress.Text.Trim();

            if (string.IsNullOrEmpty(sEmail))
            {
                litNotify.Text = string.Format(sFormatString, "Provide Email Address");
                return;
            }

            //CHECK WITH REGULAR EXPRESSION
            Regex re = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            if (!re.IsMatch(sEmail))
            {
                // INVALID EMAIL
                litNotify.Text = string.Format(sFormatString, "Enter a valid email");
                return;
            }

            string strVer = Convert.ToString(DateTime.Now.Ticks, 16);
            using (SqlDataReader dr = SqlHelper.ExecuteReader("Newsletter.SELECT_eNewsLetterEmailAddress",
                new SqlParameter("@EmailAddress", sEmail)
                ))
            {
                if (dr.Read())
                {
                    txtEmailAddress.Text = string.Empty;
                    litNotify.Text = string.Format(sFormatString, "E-mail already registered.");
                    return;
                }
            }

            using (SqlDataReader dr = SqlHelper.ExecuteReader("Newsletter.INSERT_eNewsLetter",
                new SqlParameter("@EmailAddress", sEmail),
                new SqlParameter("@VerifyCode", strVer)
                ))
            {
                if (dr.Read())
                {
                    string strEID = dr["eNewsletterID"].ToString();
                    string strLink = WebRegistry.SelectNode("System.WebPath").Value.TrimEnd('/') + "/_Sections/Newsletter/" + "Confirm.aspx?";

                    QueryParser qs = new QueryParser();
                    qs["NewsletterID"] = strEID;
                    qs["RequestCode"] = strVer;
                    qs["Mode"] = "1";

                    string sLinkSubscribe = strLink + qs;
                    qs["Mode"] = "0";
                    string sLinkUnsubscribe = strLink + qs;

                    MailMessage m = new MailMessage();
                    m.From = new MailAddress(WebRegistry.SelectNode("Newsletter.From").Value);
                    m.To.Add(new MailAddress(sEmail));
                    m.Subject = "eNewsletter Confirmation"; //sTitle; //ConfigurationManager.AppSettings["WebAppName"] + ": " + sInquiryType;
                    // EMAIL CONTENT
                    m.Body = string.Format(WebRegistry.SelectNode("Newsletter.Message").Value, sEmail, sLinkSubscribe, sLinkUnsubscribe); //strMail.ToString();
                    m.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();

                    try
                    {
                        smtp.Send(m);
                        txtEmailAddress.Text = string.Empty;
                        litNotify.Text = string.Format(sFormatString, WebRegistry.SelectNode("Newsletter.ThankYou").Value);
                        //IsSent = true;
                    }
                    catch
                    {
                        //Console.WriteLine("NOT SENT: " + ex.Message);
                    }
                }
            }
        }
}
}