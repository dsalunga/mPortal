using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList
{
    public partial class GroupEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WebGroup parent = null;
                WebGroup item = null;
                var isManager = false;

                var query = new WContext(this);
                var element = query.Element;
                var set = element.GetParameterSet();

                int id = query.GetId(WebColumns.GroupId);
                if (id > 0)
                {
                    item = WebGroup.Get(id);
                }
                //else
                //{
                //    int parentId = query.GetId(WebColumns.ParentId);
                //    if (parentId > 0)
                //        parent = WebGroup.Get(parentId);
                //    else
                //    {
                //        var defaultParentName = ParameterizedWebObject.GetValue("ParentGroup", element, set);
                //        if (!string.IsNullOrEmpty(defaultParentName))
                //        {
                //            item = WebGroup.Get(defaultParentName);

                //            txtParent.ReadOnly = true;
                //            cmdParentBrowse.Visible = false;

                //        }
                //    }
                //}

                var defaultParentName = ParameterizedWebObject.GetValue("ParentGroup", element, set);
                var root = WebGroup.SelectNode(defaultParentName);

                var managerGroupName = ParameterizedWebObject.GetValue("ManagerGroup", element, set);
                WebGroup managerGroup = string.IsNullOrEmpty(managerGroupName) ? null : WebGroup.SelectNode(managerGroupName);
                if (managerGroup != null)
                    hManagerGroupId.Value = managerGroup.Id.ToString();

                WebGroupTab1.RootGroupId = root.Id;

                if (item != null)
                {
                    txtName.Text = item.Name;

                    var owner = item.Owner;
                    if (owner != null)
                        txtOwner.Text = owner.UserName;

                    txtDescription.Text = item.Description;
                    txtManagers.Text = AccountHelper.ToAccountsString(MemberHelper.CollectUsers(item.Managers), false, WConstants.EmailSeparator);

                    var conductors = item.GetParameterValue(MasterListConstants.GROUP_CONDUCTORS_KEY);
                    if (!string.IsNullOrEmpty(conductors))
                        txtConductors.Text = AccountHelper.ToAccountsString(MemberHelper.CollectUsers(conductors), false, WConstants.EmailSeparator);

                    var mentors = item.GetParameterValue(MasterListConstants.GROUP_MENTORS_KEY);
                    if (!string.IsNullOrEmpty(mentors))
                        txtMentors.Text = AccountHelper.ToAccountsString(MemberHelper.CollectUsers(mentors), false, WConstants.EmailSeparator);

                    parent = item.Parent;

                    hGroupId.Value = item.Id.ToString();


                    var isDirectManager = item.IsManager(WSession.Current.User, false);
                    var isParentManager = parent != null && parent.IsManager(WSession.Current.User, true);

                    if (root.Id == item.Id)
                    {
                        txtName.ReadOnly = true;
                        txtParent.ReadOnly = true;
                        cmdParentBrowse.Visible = false;
                    }

                    if (isDirectManager && !isParentManager)
                    {
                        txtName.ReadOnly = true;
                        txtParent.ReadOnly = true;
                        cmdParentBrowse.Visible = false;

                        txtManagers.ReadOnly = true;
                    }

                    if (!isDirectManager && !isParentManager)
                        cmdUpdate.Enabled = false;

                    isManager = isDirectManager || isParentManager;

                    WebGroupTab1.Text = "Edit Group";
                }
                else
                {
                    int parentId = query.GetId(WebColumns.ParentId);
                    if (parentId > 0)
                        parent = WebGroup.Get(parentId);

                    WebGroupTab1.Text = "New Group";
                }

                if (parent != null)
                {
                    txtParent.Text = parent.Name;

                    if (item == null)
                    {
                        isManager = parent.IsManager(WSession.Current.User, true);
                        if (!isManager)
                            cmdUpdate.Enabled = false;
                    }
                }

                if (id > 0)
                {
                    query.RemoveOpen();
                    query.Remove(WebColumns.GroupId);
                    query.Set(WebColumns.ParentId, id);
                }
                else
                {
                    query.SetOpen(MasterListTab.Groups);
                }

                linkCancel.HRef = query.BuildQuery();
            }
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            this.Redirect();
        }

        private void Redirect()
        {
            var query = new WQuery(this);
            var groupId = query.GetId(WebColumns.GroupId);
            if (groupId > 0)
            {
                query.Remove(WebColumns.GroupId);
                query.Set(WebColumns.ParentId, groupId);
                query.RemoveOpen();
            }
            else
            {
                query.SetOpen(MasterListTab.Groups);
            }

            query.Redirect();
        }

        protected void cmdUpdate_Click(object sender, System.EventArgs e)
        {
            var ownerText = txtOwner.Text.Trim();
            var parentText = txtParent.Text.Trim();
            var managersText = txtManagers.Text.Trim();
            var conductorsText = txtConductors.Text.Trim();
            var mentorsText = txtMentors.Text.Trim();

            WebUser owner = string.IsNullOrEmpty(ownerText) ? null : WebUser.Get(ownerText);
            WebGroup parent = string.IsNullOrEmpty(parentText) ? null : WebGroup.Get(parentText);

            var updatedManagers = string.IsNullOrEmpty(managersText) ? new List<WebUser>() : MemberHelper.CollectUsers(managersText);

            int id = DataUtil.GetId(hGroupId.Value);
            WebGroup item = (id > 0) ? WebGroup.Get(id) : new WebGroup();

            var managerGroupId = DataUtil.GetId(hManagerGroupId.Value);
            WebGroup managerGroup = managerGroupId > 0 ? WebGroup.Get(managerGroupId) : null;
            if (managerGroup != null)
            {
                // Add/remove manager permission
                var oldManagers = string.IsNullOrEmpty(item.Managers) ? new List<WebUser>() : AccountHelper.CollectUsers(item.Managers);
                if (item.OwnerId > 0)
                    oldManagers.Add(item.Owner);

                var newManagers = new List<WebUser>(updatedManagers);
                if (owner != null)
                    newManagers.Add(owner);

                //var removedManagers = oldManagers.Except(newManagers, new UserIdEqualityComparer());
                //foreach (var removedManager in removedManagers)
                //    managerGroup.RemoveUser(removedManager.Id);

                var addedManagers = newManagers.Except(oldManagers, new UserIdEqualityComparer());
                foreach (var addedManager in addedManagers)
                    managerGroup.AddUser(addedManager.Id);
            }

            item.Name = txtName.Text.Trim();
            item.Description = txtDescription.Text.Trim();
            item.OwnerId = owner == null ? -1 : owner.Id;
            item.ParentId = parent == null ? -1 : parent.Id;
            item.Managers = string.IsNullOrEmpty(managersText) ? string.Empty : AccountHelper.ToAccountsString(updatedManagers, false);
            item.Update();

            var conductors = item.GetOrCreateParameter(MasterListConstants.GROUP_CONDUCTORS_KEY);
            conductors.Value = string.IsNullOrEmpty(conductorsText) ? string.Empty : AccountHelper.ToAccountsString(MemberHelper.CollectUsers(conductorsText), false);
            conductors.Update();

            var mentors = item.GetOrCreateParameter(MasterListConstants.GROUP_MENTORS_KEY);
            mentors.Value = string.IsNullOrEmpty(mentorsText) ? string.Empty : AccountHelper.ToAccountsString(MemberHelper.CollectUsers(mentorsText), false);
            mentors.Update();

            this.Redirect();
        }
    }
}