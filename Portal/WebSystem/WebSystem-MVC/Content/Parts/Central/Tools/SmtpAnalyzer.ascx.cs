using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.Net;
using System.Net.Mail;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class SmtpAnalyzer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }

        protected void cmdSend_Click(object sender, EventArgs e)
        {
            try
            {
                string host = txtHost.Text.Trim();
                int port = DataHelper.GetInt32(txtPort.Text.Trim());
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                string from = txtFrom.Text.Trim();
                string to = txtTo.Text.Trim();
                string subject = txtSubject.Text.Trim();
                string message = txtMessage.Text.Trim();

                MailMessage mail = new MailMessage();

                if (!string.IsNullOrEmpty(from))
                    mail.From = new MailAddress(from);

                mail.To.Add(new MailAddress(to));
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = chkHtml.Checked;

                SmtpClient smtp = new SmtpClient(host);

                if (port > 0 && port != 25)
                    smtp.Port = port;

                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                    smtp.Credentials = new NetworkCredential(username, password);

                if(chkEnableSSL.Checked)
                    smtp.EnableSsl = true;

                smtp.Send(mail);

                lblStatus.Text = "Message sent: " + DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.ToString();
            }
        }
    }
}