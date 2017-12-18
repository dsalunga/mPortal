using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem;
using WCMS.WebSystem.Controls;

namespace WCMS.WebSystem.WebParts.Central.Security
{
    public partial class UserProfile : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.Element;

                var dataEntryMode = DataHelper.GetBool(element.GetParameterValue("Data-Entry"), false);
                var groupFilter = element.GetParameterValue("GroupFilter");

                FormProfile.SetEntryMode(!dataEntryMode);
                panelGeneralHeading.Visible = !dataEntryMode;

                hGroupFilter.Value = groupFilter;
                hDataEntry.Value = dataEntryMode ? "1" : "0";

                int userId = DataHelper.GetId(Request, WebColumns.UserId);
                WebUser user = null;

                if (userId > 0 && (user = WebUser.Get(userId)) != null)
                {
                    if (!WSession.Current.IsAdministrator && user.IsAdministrator())
                        WQuery.StaticRedirect(WConstants.AbsoluteAccessDeniedPage);

                    FormProfile.LoadData(user.Id);
                    panelSecurity.Visible = false;
                    linkHeader.InnerHtml = string.Format("{0} ({1})", user.FirstAndLastName, user.UserName);
                }
                else if (!dataEntryMode)
                {
                    FormProfile.Initialize();
                    ChangePasswordForm1.LoadData(null);
                    //cmdUpdate.OnClientClick = "return prepareAndSubmit();";
                }
                else
                {
                    panelSecurity.Visible = false;
                }
            }
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void Update(bool addNew = false)
        {
            var query = new WQuery(this);
            WebUser user = null;

            int userId = query.GetId(WebColumns.UserId);
            if (userId > 0 && (user = WebUser.Get(userId)) != null)
            {
                FormProfile.UpdateData();
                //lblStatus.Text = "Update successful";
            }
            else
            {
                var isDataEntry = hDataEntry.Value == "0";

                //if (isDataEntry)
                //    FormProfile.UserName = Guid.NewGuid().ToString("D");

                // Create new user
                user = FormProfile.UpdateData();
                if (isDataEntry)
                    ChangePasswordForm1.UpdateData(user);

                var groupFilter = hGroupFilter.Value.Trim();
                if (!string.IsNullOrEmpty(groupFilter))
                {
                    var group = WebGroup.SelectNode(groupFilter);
                    if (group != null)
                        user.AddToGroup(group.Id);
                }
            }

            
            if (addNew)
            {
                query.Remove(WebColumns.UserId);
                query.Redirect();
            }
            else if (!query.TryReturn())
            {
                query.Remove(WConstants.Open);
                query.Set(WebColumns.UserId, user.Id);
                query.Redirect(CentralPages.WebUserHome);
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void Cancel()
        {
            var query = new WQuery(this);
            if (!query.TryReturn())
            {
                int userId = query.GetId(WebColumns.UserId);
                if (userId > 0)
                {
                    query.Redirect(CentralPages.WebUserHome);
                }
                else if ((userId = DataHelper.GetId(hNewUserId.Value)) > 0)
                {
                    query.Set(WebColumns.UserId, userId);
                    query.Redirect(CentralPages.WebUserHome);
                }
                else
                {
                    query.Redirect(CentralPages.WebUsers);
                }
            }
        }

        protected void cmdUpdateAddNew_Click(object sender, EventArgs e)
        {
            Update(true);
        }
    }
}
