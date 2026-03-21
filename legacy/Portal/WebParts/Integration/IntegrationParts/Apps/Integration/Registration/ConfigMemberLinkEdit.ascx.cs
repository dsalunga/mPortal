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
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration
{
    public partial class ConfigMemberLinkEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int id = DataUtil.GetId(Request, WebColumns.Id);
                if (id > 0)
                {
                    var item = MemberLink.Provider.Get(id);
                    if (item != null)
                    {
                        var photoPath = item.GetPhotoPathIfNull(); //item.PhotoPath; //

                        txtExternalIDNo.Text = item.ExternalIdNo;
                        txtMembershipDate.Text = item.MembershipDate.ToString("yyyy-MM-dd");
                        txtPhotoPath.Text = photoPath;

                        memberPhoto.Src = photoPath;

                        var user = item.User;
                        if (user != null)
                        {
                            txtName.Text = user.FullName;
                            txtEmail.Text = user.Email;
                        }

                        if (item.MemberId > 0)
                            panelExternalSync.Visible = true;

                        return;
                    }
                }

                lblStatus.Text = "Invalid Member Link record.";
                cmdSubmit.Enabled = false;
            }
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            int id = DataUtil.GetId(Request, WebColumns.Id);
            if (id > 0)
            {
                var item = MemberLink.Provider.Get(id);
                if (item != null)
                {
                    item.ExternalIdNo = txtExternalIDNo.Text.Trim();
                    item.MembershipDate = DataUtil.GetDateTime(txtMembershipDate.Text.Trim());
                    //item.PhotoPath = txtPhotoPath.Text.Trim();
                    item.Update(true);

                    var user = item.User;
                    if (user != null)
                    {
                        user.PhotoPath = txtPhotoPath.Text.Trim();
                        user.Update(true);
                    }

                    ReturnPage();
                    return;
                }
            }

            lblStatus.Text = "Invalid Member Link record.";
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            ReturnPage();
        }

        private void ReturnPage()
        {
            var query = new WQuery(this);
            query.LoadAndRedirect("ConfigMemberLink.ascx");
        }

        protected void cmdExternalSync_Click(object sender, EventArgs e)
        {
            int id = DataUtil.GetId(Request, WebColumns.Id);
            if (id > 0)
            {
                var item = MemberLink.Provider.Get(id);
                if (item != null)
                {
                    var photoPath = MemberLink.GetPhotoPath(item.MemberId);
                    memberPhoto.Src = photoPath;
                    txtPhotoPath.Text = photoPath;
                    return;
                }
            }

            lblStatus.Text = "Member record link to External is invalid.";
        }
    }
}