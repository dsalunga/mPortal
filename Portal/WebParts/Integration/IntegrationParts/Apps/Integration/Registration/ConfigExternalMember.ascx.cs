using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

using WCMS.WebSystem.Apps.Integration.Providers;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;

namespace WCMS.WebSystem.Apps.Integration
{
    public partial class ConfigExternalMember : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = DataUtil.GetId(Request, WebColumns.Id);
                if (id > 0)
                {
                    var item = Member.RemoteProvider.Get(id);
                    if (item != null)
                    {
                        txtExternalIDNo.Text = item.ExternalIDNo;
                        txtTempExternalID.Text = item.TemporaryIDNo;
                        txtFirstName.Text = item.FirstName;
                        txtMiddleName.Text = item.MiddleName;
                        txtLastName.Text = item.LastName;
                        txtEmail.Text = item.Email;
                        return;
                    }
                }

                lblStatus.Text = "Invalid External Member record.";
                cmdSubmit.Enabled = false;
            }
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            int id = DataUtil.GetId(Request, WebColumns.Id);
            if (id > 0)
            {
                var item = Member.RemoteProvider.Get(id);
                if (item != null)
                {
                    item.ExternalIDNo = txtExternalIDNo.Text.Trim();
                    item.TemporaryIDNo = txtTempExternalID.Text.Trim();
                    item.FirstName = txtFirstName.Text.Trim();
                    item.LastName = txtLastName.Text.Trim();
                    item.MiddleName = txtMiddleName.Text.Trim();
                    item.Email = txtEmail.Text.Trim();

                    Member.RemoteProvider.Update(item);

                    ReturnPage();
                    return;
                }
            }

            lblStatus.Text = "Invalid External Member record.";
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            ReturnPage();
        }

        private void ReturnPage()
        {
            var query = new WQuery(this);
            query.LoadAndRedirect();
        }
    }
}