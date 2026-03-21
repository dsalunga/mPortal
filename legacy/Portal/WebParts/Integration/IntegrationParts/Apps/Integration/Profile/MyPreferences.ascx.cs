using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile
{
    public partial class MyPreferences : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
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
                // Set Membership information

                WContext context = new WContext(this);
                var element = context.Element;
                string ministriesPath = element.GetParameterValue(MemberConstants.MinistriesPathKey, MemberConstants.MinistriesGroupPath);
                string groupsPath = element.GetParameterValue(MemberConstants.LocaleGroupPathKey, MemberConstants.LocaleGroupPath);
                string specialGroupsPath = element.GetParameterValue(MemberConstants.SpecialGroupsPathKey, MemberConstants.SpecialGroupsPath);

                var ministries = WebGroup.SelectNode(ministriesPath).Children;
                var specialGroups = WebGroup.SelectNode(specialGroupsPath).Children;
                var groups = WebGroup.SelectNode(groupsPath).Children;

                var userGroups = user.Groups;

                List<WebGroup> managerGroups = new List<WebGroup>();
                List<WebGroup> managerCommittees = new List<WebGroup>();
                List<WebGroup> managerSpecialGroups = new List<WebGroup>();

                foreach (var userGroup in userGroups)
                {
                    string approverOf = userGroup.GetParameterValue("ApproverOf");
                    if (!string.IsNullOrWhiteSpace(approverOf))
                    {
                        WebGroup group = null;
                        if ((group = ministries.FirstOrDefault(i => i.Name.Equals(approverOf, StringComparison.InvariantCultureIgnoreCase))) != null)
                            managerCommittees.Add(group);
                        else if ((group = groups.FirstOrDefault(i => i.Name.Equals(approverOf, StringComparison.InvariantCultureIgnoreCase))) != null)
                            managerGroups.Add(group);
                        else if ((group = specialGroups.FirstOrDefault(i => i.Name.Equals(approverOf, StringComparison.InvariantCultureIgnoreCase))) != null)
                            managerSpecialGroups.Add(group);
                    }
                }


                bool enableCommittess = SetupGroups(managerCommittees, cblMinistries, panelCommittees); // = managerCommittees.Count > 0;
                bool enableGroups = SetupGroups(managerGroups, cblLocaleGroups, panelLocaleGroups);
                bool enableSpecialGroups = SetupGroups(managerSpecialGroups, cblSpecialGroups, panelSpecialGroups);

                if (!enableGroups && !enableCommittess && !enableSpecialGroups)
                {
                    cmdSubmit.Enabled = false;
                    lblStatus.Text = "You are not a manager of any group.";
                }
            }
            else
            {
                lblStatus.Text = "You are not logged in. Please login try again.";
            }
        }

        private bool SetupGroups(List<WebGroup> managerGroups, CheckBoxList cbl, HtmlGenericControl panel)
        {
            bool enableGroups = managerGroups.Count > 0;
            if (enableGroups)
            {
                cbl.DataSource = managerGroups;
                cbl.DataBind();

                // Set Group
                foreach (var group in managerGroups)
                {
                    if (group.RequireApproval)
                    {
                        var item = cbl.Items.FindByValue(group.Id.ToString());
                        if (item != null && !item.Selected)
                            item.Selected = true;
                    }
                }

                panel.Visible = true;
            }

            return enableGroups;
        }

        private void ConfigureCancel()
        {
            WContext context = new WContext(this);
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
                Dictionary<string, WebGroup> managerGroups = new Dictionary<string, WebGroup>();
                var userGroups = user.Groups;

                foreach (var userGroup in userGroups)
                {
                    string approverOf = userGroup.GetParameterValue("ApproverOf");
                    if (!string.IsNullOrWhiteSpace(approverOf))
                    {
                        managerGroups.Add(approverOf, userGroup);
                    }
                }

                // Update Locale Groups
                var items = cblLocaleGroups.Items;
                UpdatePrivateAccess(items, managerGroups);

                // Update Ministries
                items = cblMinistries.Items;
                UpdatePrivateAccess(items, managerGroups);

                WContext context = new WContext(this);
                string updateRedirect = context.Element.GetParameterValue(MemberConstants.UpdateRedirect);
                if (!string.IsNullOrEmpty(updateRedirect))
                    context.Redirect(updateRedirect);
                else
                    lblStatus.Text = "Update successful.";
            }
        }

        private static void UpdatePrivateAccess(ListItemCollection items, Dictionary<string, WebGroup> managerGroups)
        {
            foreach (ListItem item in items)
            {
                var id = DataUtil.GetId(item.Value);
                if (id > 0)
                {
                    WebGroup group = WebGroup.Get(id);
                    if (group != null && group.RequireApproval != item.Selected)
                    {
                        group.JoinApproval = item.Selected ? 1 : 0;
                        group.Update();

                        var page = group.Page;
                        if (page != null)
                        {
                            if (item.Selected)
                            {
                                // Enable account authentication and add only the permitted Group
                                if (!page.IsAccountAccessEnabled)
                                {
                                    page.PublicAccess = WebPublicAccess.Account;
                                    page.Update();

                                    // Manager group
                                    WebGroup manager = null;
                                    if(managerGroups.ContainsKey(group.Name))
                                        manager = managerGroups[group.Name];

                                    // Group Permission
                                    var securities = page.GetObjectSecurities(WebObjects.WebGroup, 1);
                                    if (securities.FirstOrDefault(i => i.SecurityRecordId == group.Id) == null)
                                    {
                                        WebObjectSecurity security = page.AddObjectSecurity(group.OBJECT_ID, group.Id, 1);

                                        security.AddPermission(Permissions.PublicRead, 1);
                                        security.AddPermission(Permissions.PublicWrite, 1);
                                    }

                                    // Manager permission
                                    if(manager != null)
                                    {
                                        if (securities.FirstOrDefault(i => i.SecurityRecordId == manager.Id) == null)
                                        {
                                            WebObjectSecurity security = page.AddObjectSecurity(manager.OBJECT_ID, manager.Id, 1);

                                            security.AddPermission(Permissions.PublicRead, 1);
                                            security.AddPermission(Permissions.PublicWrite, 1);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (page.IsAccountAccessEnabled)
                                {
                                    page.PublicAccess = WebPublicAccess.Inherit;
                                    page.Update();
                                }
                            }
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