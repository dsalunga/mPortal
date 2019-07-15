using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Utilities;
using WCMS.Framework.Core.Shared;
using WCMS.Framework.Diagnostics;

using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile
{
    public partial class UpdateMyGroups : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetProfileInformation();

                // Configure Cancel
                ConfigureCancel();
            }
        }

        private void SetProfileInformation()
        {
            var user = WSession.Current.User;
            if (user != null)
            {
                lblLastUpdate.InnerHtml = user.LastUpdate.ToString("d MMMM yyyy h:mm tt");

                var link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null)
                {
                    var context = new WContext(this);

                    bool firstUpdate = context.Get("Status") == "FirstUpdate";
                    // Setup notice
                    if (firstUpdate || user.NoLastUpdate)
                    {
                        panelNotice.Visible = true;
                    }
                    else
                    {
                        lblLastUpdate.InnerHtml = link.LastUpdate.ToString("d MMMM yyyy h:mm tt");
                        panelLastUpdate.Visible = true;
                    }
                    cmdSubmit.Enabled = true;

                    // Set Membership information

                    var userGroups = WebUserGroup.GetByUserId(user.Id, -1); //WebGroup.GetByUserId(user.Id);
                    var element = context.Element;

                    // Ministries
                    string ministriesPath = element.GetParameterValue(MemberConstants.MinistriesPathKey, MemberConstants.MinistriesGroupPath);
                    SetupGroupForUpdate(userGroups, ministriesPath, cblMinistries, panelMinistries);

                    // Special Groups
                    string specialGroupsPath = element.GetParameterValue(MemberConstants.SpecialGroupsPathKey, MemberConstants.SpecialGroupsPath);
                    SetupGroupForUpdate(userGroups, specialGroupsPath, cblSpecialGroups, panelSpecialGroups);


                    // Locale Groups
                    string groupsPath = element.GetParameterValue(MemberConstants.LocaleGroupPathKey, MemberConstants.LocaleGroupPath);
                    var groups = WebGroup.SelectNode(groupsPath).Children;
                    if (groups.Count() > 0)
                    {
                        WebUserGroup wug = null;
                        rblLocaleGroups.DataSource = groups;
                        rblLocaleGroups.DataBind();

                        // Set Locale Group
                        foreach (var group in groups)
                        {
                            if ((wug = userGroups.FirstOrDefault(g => g.GroupId == group.Id)) != null)
                            {
                                var item = rblLocaleGroups.Items.FindByValue(group.Id.ToString());
                                if (item != null && !item.Selected)
                                {
                                    item.Selected = true;

                                    if (!wug.IsActive)
                                        item.Text += MemberConstants.PendingApprovalString;

                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        panelLocaleGroups.Visible = false;
                    }
                }
                else
                {
                    lblStatus.Text = "No member record found. Please link your user account to your member account. Update My Groups cannot continue.";
                }
            }
            else
            {
                lblStatus.Text = "You are not logged in. Please login.";
            }
        }

        private void SetupGroupForUpdate(IEnumerable<WebUserGroup> userGroups, string groupPath, CheckBoxList cbl, HtmlGenericControl panel)
        {
            var groups = WebGroup.SelectNode(groupPath).Children;
            if (groups.Count() > 0)
            {
                WebUserGroup wug = null;

                cbl.DataSource = groups;
                cbl.DataBind();

                // Set Ministries
                foreach (var group in groups)
                {
                    if ((wug = userGroups.FirstOrDefault(g => g.GroupId == group.Id)) != null)
                    {
                        var item = cbl.Items.FindByValue(group.Id.ToString());
                        if (item != null && !item.Selected)
                        {
                            item.Selected = true;

                            if (!wug.IsActive)
                                item.Text += MemberConstants.PendingApprovalString;
                        }
                    }
                }
            }
            else
            {
                panel.Visible = false;
            }
        }

        private void ConfigureCancel()
        {
            var context = new WContext(this);
            string cancelRedirect = context.Element.GetParameterValue(MemberConstants.CancelRedirect);
            if (!string.IsNullOrEmpty(cancelRedirect))
            {
                cmdCancel.Visible = true;
            }
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            var user = WSession.Current.User;
            if (user != null)
            {
                MemberLink link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null)
                {
                    WContext context = new WContext(this);

                    // Update Locale Groups
                    var items = rblLocaleGroups.Items;
                    ProcessGroupListUpdate(user, items);

                    // Update Ministries
                    items = cblMinistries.Items;
                    ProcessGroupListUpdate(user, items);

                    // Special Groups
                    items = cblSpecialGroups.Items;
                    ProcessGroupListUpdate(user, items);

                    if (user.NoLastUpdate)
                        user.Update(true);

                    #region Audit Update Activity

                    try
                    {
                        if (WConfig.EnableLogging)
                        {
                            EventLog log = new EventLog();
                            log.UserId = user.Id;
                            log.EventName = MemberConstants.GroupUpdateEvent;
                            log.EventDate = DateTime.Now;
                            log.IPAddress = WHelper.GetUserHostAddress();
                            log.Update();
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(ex);
                    }

                    #endregion Audit Update Activity

                    string updateRedirect = context.Element.GetParameterValue(MemberConstants.UpdateRedirect);
                    if (!string.IsNullOrEmpty(updateRedirect))
                    {
                        context.Redirect(updateRedirect);
                    }
                    else
                    {
                        lblLastUpdate.InnerHtml = link.LastUpdate.ToString("d MMMM yyyy h:mm tt");

                        lblStatus.Text = "Profile Update successful.";

                        if (panelNotice.Visible)
                        {
                            panelNotice.Visible = false;
                            panelLastUpdate.Visible = true;
                        }
                    }
                }
            }
        }

        private void ProcessGroupListUpdate(WebUser user, ListItemCollection items)
        {
            // Update User Groups
            var userGroups = WebUserGroup.GetByUserId(user.Id, -1);

            WebGroup grp = null;
            foreach (ListItem item in items)
            {
                var id = DataUtil.GetId(item.Value);
                if (id > 0)
                {
                    if (item.Selected)
                    {
                        if (userGroups.FirstOrDefault(g => g.GroupId == id) == null)
                        {
                            // Adding a new group to user
                            grp = WebGroup.Get(id);

                            if (grp.RequireApproval)
                            {
                                var approvers = grp.GetParameterValue(MemberConstants.ApproversKey);
                                bool requireApproval = !AccountHelper.IsPresentOrMember(approvers);

                                user.AddToGroup(id, requireApproval ? 0 : 1);

                                if (requireApproval)
                                {
                                    item.Text += MemberConstants.PendingApprovalString;

                                    if (!string.IsNullOrWhiteSpace(approvers))
                                    {
                                        var emails = AccountHelper.CollectEmails(approvers);
                                        if (emails.Count > 0)
                                        {
                                            // Send Approval Email
                                            string content = FileHelper.ReadFile(MapPath("~/Content/Parts/Profile/Template/ApprovalEmailToGroupHead.htm"));

                                            string userProfileUrl = string.Format(MemberConstants.UserProfilePageFormat, user.Id);
                                            if (userProfileUrl.StartsWith("/"))
                                                userProfileUrl = WebHelper.CombineAddress(WConfig.BaseAddress, userProfileUrl);

                                            string approvalLink = MemberConstants.GroupApprovalLink;
                                            if (approvalLink.StartsWith("/"))
                                                approvalLink = WebHelper.CombineAddress(WConfig.BaseAddress, approvalLink);

                                            NamedValueProvider values = new NamedValueProvider();
                                            values.Add("ManagerName", "Managers");
                                            values.Add("MemberProfileLink", userProfileUrl);
                                            values.Add("MemberName", user.FirstAndLastName);
                                            values.Add("GroupName", grp.Name);
                                            values.Add("ApprovalLink", approvalLink);

                                            content = Substituter.Substitute(content, values);

                                            WebMailMessage smtp = new WebMailMessage();
                                            smtp.AddTo(emails);
                                            smtp.SubjectAutoPrefix = "A member wants to join your group";
                                            smtp.Body = content;
                                            smtp.Send();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                user.AddToGroup(id, 1);
                            }
                        }
                    }
                    else
                    {
                        if (userGroups.FirstOrDefault(g => g.GroupId == id) != null)
                        {
                            // Removing a group from user

                            user.RemoveToGroup(id);

                            grp = WebGroup.Get(id);
                            item.Text = grp.Name;
                        }
                    }
                }
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            WContext context = new WContext(this);
            string cancelRedirect = context.Element.GetParameterValue(MemberConstants.CancelRedirect);
            if (!string.IsNullOrEmpty(cancelRedirect))
            {
                context.Redirect(cancelRedirect);
            }
        }
    }
}