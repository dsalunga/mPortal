using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.Framework.Core.Shared;

namespace WCMS.WebSystem.WebParts.Office
{
    public partial class OfficeDetails : System.Web.UI.UserControl
    {
        private const string UpdateRedirect = "UpdateRedirect";
        private const string CancelRedirect = "CancelRedirect";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetInformation();

                // Configure Cancel
                ConfigureCancel();
            }
        }

        private void SetInformation()
        {
            WContext context = new WContext(this);
            int id = context.GetId(WebColumns.OfficeId);

            var item = WebOffice.Get(id);
            if (item != null)
            {
                lblName.InnerHtml = item.Name;
                lblEmailAddress.InnerHtml = item.EmailAddress;
                txtMobileNumber.InnerHtml = item.MobileNumber;
                txtPhoneNumber.InnerHtml = item.PhoneNumber;
                txtAddressLine1.InnerHtml = item.AddressLine1;
                txtContactPerson.InnerHtml = item.ContactPerson;
            }
            else
            {
                lblStatus.Text = "You are not logged in. Please login to view this information.";
            }
        }

        private void ConfigureCancel()
        {
            WContext context = new WContext(this);
            string cancelRedirect = context.Element.GetParameterValue(CancelRedirect);
            if (!string.IsNullOrEmpty(cancelRedirect))
            {
                cmdCancel.Visible = true;
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            WContext context = new WContext(this);
            string cancelRedirect = context.Element.GetParameterValue(CancelRedirect);
            if (!string.IsNullOrEmpty(cancelRedirect))
            {
                context.Redirect(cancelRedirect);
            }
        }
    }
}