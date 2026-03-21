using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;

using WCMS.Common.Utilities;
using WCMS.WebSystem.Apps.Integration.ExtApp;

namespace WCMS.WebSystem.WebParts.Profile
{
    public partial class AdminUtilities : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            int id = DataUtil.GetId(txtAttendanceId.Text.Trim());
            if (id > 0)
            {
                try
                {
                    MemberAttendance.DeleteAttendance(id);

                    lblMessage.InnerHtml = string.Format("Attendance entry {0} deleted successfully!", id);
                }
                catch (Exception ex)
                {
                    lblMessage.InnerHtml = ex.ToString();
                }
            }
            else
            {
                lblMessage.InnerHtml = "Please provide a valid AttendanceID.";
            }

            lblMessage.Visible = true;
        }

        protected void cmdONESubmit_Click(object sender, EventArgs e)
        {
            var externalId = txtSearchExternalId.Text.Trim();
            if (!string.IsNullOrEmpty(externalId))
            {
                var userInfo = ExtAppProvider.GetUserInfo(externalId);
                if (userInfo != null)
                {
                    lblUsername.InnerHtml = userInfo.UserName;
                    lblExternalId.InnerHtml = userInfo.ExternalId;
                    lblEmail.InnerHtml = userInfo.Email;
                    lblLastName.InnerHtml = userInfo.LastName;
                    lblFirstName.InnerHtml = userInfo.FirstName;
                    lblMiddleName.InnerHtml = userInfo.MiddleName;
                }
            }
        }
    }
}