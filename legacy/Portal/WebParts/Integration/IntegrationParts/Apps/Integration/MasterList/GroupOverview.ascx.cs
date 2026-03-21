using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList
{
    public partial class GroupOverview : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WebGroup group = null;

                var context = new WContext(this);
                var element = context.Element;
                var set = element.GetParameterSet();
                var groupId = context.GetId(WebColumns.ParentId);

                if (groupId > 0)
                    group = WebGroup.Get(groupId);

                var defaultParentName = ParameterizedWebObject.GetValue("ParentGroup", element, set);
                if (!string.IsNullOrEmpty(defaultParentName))
                {
                    var root = WebGroup.SelectNode(defaultParentName);
                    if (root != null)
                    {
                        WebGroupTab1.RootGroupId = root.Id;

                        if (group == null)
                            group = root;
                    }
                }

                if (group != null)
                {
                    var userFormatString = ParameterizedWebObject.GetValue("UserDisplayFormatString", element, set);

                    var owner = group.Owner;
                    var parent = group.Parent;
                    var mentors = group.GetParameterValue(MasterListConstants.GROUP_MENTORS_KEY);
                    var conductors = group.GetParameterValue(MasterListConstants.GROUP_CONDUCTORS_KEY);

                    lblName.InnerHtml = group.Name;
                    lblOwner.InnerHtml = owner == null ? string.Empty : AccountHelper.FormatForDisplay(owner, userFormatString);
                    lblParent.InnerHtml = parent == null ? string.Empty : parent.Name;
                    lblManagers.InnerHtml = AccountHelper.FormatForDisplay(group.Managers, userFormatString);
                    lblMentors.InnerHtml = AccountHelper.FormatForDisplay(mentors, userFormatString);
                    lblConductors.InnerHtml = AccountHelper.FormatForDisplay(conductors, userFormatString);
                    lblDescription.InnerHtml = group.Description;
                    lblDateModified.InnerHtml = group.DateModified.ToString();

                    if (WSession.Current.IsAdministrator || group.IsManager(WSession.Current.User, true))
                    {
                        context.SetOpen("GroupEdit");
                        context.Remove(WebColumns.ParentId);
                        context.Set(WebColumns.GroupId, group.Id);
                        linkUpdate.HRef = context.BuildQuery();

                        panelUpdate.Visible = true;
                    }
                }
            }
        }
    }
}