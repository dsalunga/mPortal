namespace WCMS.WebSystem.WebParts.Newsletter
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Diagnostics;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using WCMS.Common.Utilities;

	/// <summary>
	///		Summary description for MailSender.
	/// </summary>
	public partial class MailSender : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			if(!Page.IsPostBack)
			{
				this.ViewSenderInfo();
			}
		}

		private void ViewProcesses()
		{
			string s = string.Empty;
			Process[] processes = Process.GetProcesses();
			
			foreach(Process process in processes)
			{
				s += process.ProcessName + "<br>";
			}

			lProcesses.Text = s;
		}

		private void ViewSenderInfo()
		{
			string s = string.Empty;
            Process[] ms = Process.GetProcessesByName("WCMS.WebSystem.Controls.Newsletter");
			
			if(ms.Length > 0)
			{
				foreach(Process m in ms)
				{
					//s += "Private Memory used: " + m.PrivateMemorySize.ToString() + "<br>";
					//s += "Virtual Memory Used: " + m.VirtualMemorySize.ToString() + "<br>";
                    s += "Sending in progress... sending started at " + m.StartTime.ToString("MMMM dd, yyyy h:mm tt") + "<br>";
					//s += "CPU Time: " + m.TotalProcessorTime.ToString() + "<br>";
					//s += "<hr><br>";
				}
			}
			else
			{
				s = "<span style=\"color: green;\">eNewsletter Sender not running.</span>";
			}

			lProcesses.Text = s;
		}

		protected void cmdSend_Click(object sender, System.EventArgs e)
		{
			string sEmailID = this.cboNewsletters.SelectedValue;

			if(sEmailID != null)
			{
				string s = string.Empty;
				Process m = new Process();
			
				m.StartInfo.Arguments = sEmailID;
                m.StartInfo.FileName = Server.MapPath("/_Sections/Newsletter/bin/des.Web.Controls.Newsletter.exe");
				m.Start();

				s += "Sending in progress... sending started at " + m.StartTime.ToString("MMMM dd, yyyy h:mm tt") + "<br>";

				lProcesses.Text = s;
			}
		}

		protected void cmdRefresh_Click(object sender, System.EventArgs e)
		{
            GridView1.DataBind();
			this.ViewSenderInfo();
		}

		protected void cmdSendPending_Click(object sender, System.EventArgs e)
		{
			if(cboSelect.SelectedValue != "All")
			{
				string sChecked = Request.Form["chkChecked"];
				if(!string.IsNullOrEmpty(sChecked))
				{
					SqlHelper.ExecuteNonQuery(CommandType.Text,
                        "UPDATE Newsletter.eNewsletter SET IsDone=0, IsSuccess=0 WHERE eNewsletterID IN (" + sChecked + ");");
                    GridView1.DataBind();
				}
			}
			else
			{
				SqlHelper.ExecuteNonQuery(CommandType.Text,
                    "UPDATE Newsletter.eNewsletter SET IsDone=0 WHERE IsActive=1");
                GridView1.DataBind();
			}
		}

		protected void cmdSent_Click(object sender, System.EventArgs e)
		{
			if(cboSelect.SelectedValue != "All")
			{
				string sChecked = Request.Form["chkChecked"];
				if(!string.IsNullOrEmpty(sChecked))
				{
					SqlHelper.ExecuteNonQuery(CommandType.Text,
                        "UPDATE Newsletter.eNewsletter SET IsDone=1 WHERE eNewsletterID IN (" + sChecked + ");");
                    GridView1.DataBind();
				}
			}
			else
			{
				SqlHelper.ExecuteNonQuery(CommandType.Text,
                    "UPDATE Newsletter.eNewsletter SET IsDone=1 WHERE IsActive=1");
                GridView1.DataBind();
			}
		}

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
}
}
