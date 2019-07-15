using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration
{
    public partial class MemberVisitEntry : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.Element;
                var tagFilter = element.GetParameterValue("TagFilter");

                hBaseGroup.Value = element.GetParameterValue("BaseGroup");
                hTagFilter.Value = tagFilter;

                // Set Membership information
                var localeGroupsPath = element.GetParameterValue(MemberConstants.LocaleGroupPathKey, MemberConstants.LocaleGroupPath);
                cboGroup.DataSource = WebGroup.SelectNode(localeGroupsPath).Children;
                cboGroup.DataBind();

                panelTags.Visible = string.IsNullOrEmpty(tagFilter);

                int id = context.GetId(WebColumns.Id);
                var item = id > 0 ? MemberVisit.Provider.Get(id) : null;
                if (item != null)
                {
                    txtDateVisited.Text = item.DateVisited.ToString("yyyy-MMM-dd");

                    var user = item.VisitedUser;
                    if (user != null)
                        hUserName.Value = user.Name;

                    SetupMember(user, item);
                    cboGroup.SelectedValue = item.GroupId.ToString();

                    string timesVisited = item.TimesVisited.ToString();
                    if (cboTimesVisited.Items.FindByText(timesVisited) == null)
                        cboTimesVisited.Items.Add(new ListItem(timesVisited));

                    cboTimesVisited.Text = timesVisited;
                    txtStatus.Text = item.Status;
                    txtActualReport.Text = item.ActualReport;
                    txtActionTaken.Text = item.ActionTaken;

                    if(!string.IsNullOrEmpty(item.Tags))
                    {
                        var listItem = cboTag.Items.FindByText(item.Tags);
                        if (listItem == null)
                            cboTag.Items.Add(item.Tags);
                        cboTag.SelectedValue = item.Tags;
                    }

                    lblDateEntered.InnerHtml = item.DateCreated.ToString("yyyy-MMM-dd");
                    var createdUser = item.CreatedUser;
                    if (createdUser != null)
                        lblEnteredBy.InnerHtml = AccountHelper.GetPrefixedName(item.CreatedUser);
                }
                else
                {
                    cmdReset.Visible = false;
                    txtDateVisited.Text = DateTime.Now.ToString("yyyy-MMM-dd");
                    lblDateEntered.InnerHtml = DateTime.Now.ToString("yyyy-MMM-dd");
                    lblEnteredBy.InnerHtml = AccountHelper.GetPrefixedName(WSession.Current.User);

                    var groupId = context.Get(WebColumns.GroupId);
                    if (!string.IsNullOrEmpty(groupId))
                    {
                        var listItem = cboGroup.Items.FindByValue(groupId);
                        if (listItem != null)
                            listItem.Selected = true;
                    }

                    if (!string.IsNullOrEmpty(tagFilter))
                    {
                        var listItem = cboTag.Items.FindByText(tagFilter);
                        if (listItem == null)
                            cboTag.Items.Add(tagFilter);

                        cboTag.SelectedValue = tagFilter;
                    }
                }

                SetAccess();
            }
        }

        private void SetAccess()
        {
            var context = new WContext(this);
            if (!context.Element.IsUserMgmtPermitted(Permissions.ManageContent)) //if (ctx.PageElement.GetPublicAccountPermissionMax() != Permissions.PublicWrite)
            {
                // Read only mode
                cmdUpdate.Visible = false;
                cmdCancel.Text = "Close";

                if (string.IsNullOrEmpty(lblName.InnerHtml))
                    lblName.InnerHtml = txtMember.Text;

                cboGroup.Enabled = false;
                txtContactNo.Enabled = false;
                txtAddress.Enabled = false;
                txtMembershipDate.Enabled = false;
                cboTimesVisited.Enabled = false;
                txtStatus.Enabled = false;
                panelMemberInput.Visible = false;
                txtDateVisited.Enabled = false;
                txtActualReport.Enabled = false;
                txtActionTaken.Enabled = false;
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            Return();
        }

        private void Return()
        {
            var context = new WContext(this);
            context.Remove(WebColumns.Id);
            context.Open();
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            var tagFilter = hTagFilter.Value;
            var context = new WContext(this);
            int id = context.GetId(WebColumns.Id);
            var item = id > 0 ? MemberVisit.Provider.Get(id) : null;
            if (item == null)
            {
                item = new MemberVisit();
                item.CreatedUserId = WSession.Current.UserId;
                if (!string.IsNullOrEmpty(tagFilter))
                    item.Tags = tagFilter;
            }

            if (string.IsNullOrEmpty(tagFilter))
                item.Tags = cboTag.SelectedValue;

            item.Address = txtAddress.Text.Trim();
            item.ContactNo = txtContactNo.Text.Trim();
            item.MembershipDate = DataUtil.GetDateTime(txtMembershipDate.Text.Trim());

            var name = hUserName.Value.Trim();
            var user = WebUser.GetByUniqueName(name);
            if (user != null)
            {
                item.VisitedUserId = user.Id;
                item.Name = string.Empty;
                var link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null)
                {
                    if (!string.IsNullOrEmpty(item.Address) && item.Address == link.SingleLineHomeAddress)
                        item.Address = string.Empty;
                    if (!string.IsNullOrEmpty(item.ContactNo) && item.ContactNo == link.ContactNoEval)
                        item.ContactNo = string.Empty;
                }
            }
            else
            {
                item.VisitedUserId = -1;
                item.Name = txtMember.Text.Trim();
            }

            item.DateVisited = DataUtil.GetDateTime(txtDateVisited.Text.Trim());
            item.TimesVisited = DataUtil.GetInt32(cboTimesVisited.Text);
            item.GroupId = DataUtil.GetId(cboGroup.SelectedValue);
            item.Status = txtStatus.Text;
            item.ActualReport = txtActualReport.Text.Trim();
            item.ActionTaken = txtActionTaken.Text.Trim();
            item.Update();

            Return();
        }

        protected void cmdVerify_Click(object sender, EventArgs e)
        {
            var name = hUserName.Value.Trim();
            var user = WebUser.GetByUniqueName(name);
            SetupMember(user, null);
        }

        private void SetupMember(WebUser user, MemberVisit item)
        {
            bool userIsNull = user == null;
            bool visitIsNull = item == null;

            cmdReset.Visible = !userIsNull;
            linkProfile.Visible = !userIsNull;
            txtMember.Visible = userIsNull;
            txtMember.Text = !userIsNull ? user.FirstAndLastName : visitIsNull ? "" : item.Name;

            if (!userIsNull)
            {
                // A valid Portal Member was selected

                var userDetailsFormat = MemberConstants.UserProfilePageFormat;
                lblName.InnerHtml = AccountHelper.GetPrefixedName(user);
                linkProfile.HRef = string.Format(userDetailsFormat, user.Id);

                var link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null)
                {
                    txtAddress.Text = item == null ? link.SingleLineHomeAddress : item.Address;
                    txtContactNo.Text = item == null ? link.ContactNoEval : item.ContactNo;
                    txtMembershipDate.Text = (item == null ? link.MembershipDate : item.MembershipDate).ToString("yyyy-MMM-dd");
                }
                else
                {
                    txtAddress.Text = string.Empty;
                    txtContactNo.Text = string.Empty;
                    txtMembershipDate.Text = string.Empty;
                }
            }
            else
            {
                // Memeber is not registered

                lblName.InnerHtml = string.Empty;
                hUserName.Value = string.Empty;

                if (!visitIsNull)
                {
                    // Visit entry is available
                    txtAddress.Text = item.Address;
                    txtContactNo.Text = item.ContactNo;
                    txtMembershipDate.Text = item.MembershipDate.ToString("yyyy-MMM-dd");
                }
                else
                {
                    // Set everying to empty
                    txtAddress.Text = string.Empty;
                    txtContactNo.Text = string.Empty;
                    txtMembershipDate.Text = string.Empty;
                }
            }
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            SetupMember(null, null);
        }
    }
}