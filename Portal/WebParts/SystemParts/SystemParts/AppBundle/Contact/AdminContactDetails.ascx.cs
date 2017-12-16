using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Contact
{
	public partial class ConfigContactDetails : System.Web.UI.UserControl
	{
		private string sCID;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			sCID = Request["__id"];

			if(!Page.IsPostBack)
			{
                int contactId = DataHelper.GetId(Request["__id"]);
				if(contactId > 0)
				{
                    // EDIT

                    Contact item = Contact.Get(contactId);
                    if (item != null)
                    {
                        txtName.Text = item.Name;
                        txtEmail.Text = item.Email;
                        txtDetails.Value = item.Details;
                        txtRank.Text = item.Rank.ToString();
                        txtSubject.Text = item.Subject;

                        chkActive.Checked = item.IsActive == 1;
                    }
				}
				else
				{
                    // CREATE
                    //using (SqlDataReader r = SqlHelper.ExecuteReader(CommandType.Text, "SELECT TOP 1 Rank FROM Contact.Contacts ORDER BY Rank desC;"))
                    //{
                    //    if(r.Read())
                    //        txtRank.Text = ((int)r["Rank"] + 10).ToString();
                    //    else
                    //        txtRank.Text = "1";
                    //}

                    cmdUpdate.Text = "Save";
				}
			}
		}

		protected void cmdUpdate_Click(object sender, System.EventArgs e)
		{
            int contactId = DataHelper.GetId(Request["__id"]);
            Contact item = null;
            if (contactId > 0 && (item = Contact.Get(contactId)) != null)
            { }
            else
            {
                item = new Contact();
            }

            item.Name = txtName.Text.Trim();
            item.Email = txtEmail.Text.Trim();
            item.Details = txtDetails.Value.Trim();
            item.IsActive = chkActive.Checked ? 1 : 0;
            item.Subject = txtSubject.Text.Trim();
            item.Rank = DataHelper.GetInt32(txtRank.Text.Trim());
            item.Update();

			this.ReturnPage();
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.ReturnPage();
		}

		private void ReturnPage()
		{
			QueryParser query = new QueryParser(this);
			query.Remove("__id");
			query.Remove("Load");
            query.Redirect();
		}
	}
}
