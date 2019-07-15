using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using WCMS.WebSystem.Controls;
using WCMS.Framework;
using WCMS.Common.Utilities;

using WCMS.WebSystem.Apps.Integration;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList.Controls
{
    public partial class WebGroupTab : System.Web.UI.UserControl
    {
        public WebGroupTab()
        {
            GroupId = -1;
            RootGroupId = -1;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //QueryParser query = new QueryParser(this);

                //int id = query.GetId(WebColumns.GroupId);
                //int parentId = query.GetId(WebColumns.ParentId);

                //if (0 > id)
                //{
                //    id = parentId;
                //    query.Set(WebColumns.GroupId, id);
                //}

                //WebGroup item = null;
                //if (parentId > 0 && (item = WebGroup.Get(parentId)) != null)
                //    lblHeader.InnerText = item.Name;

                //string groupTabText = parentId > 0 ? "Child Groups" : "Groups";

                //if (parentId > 0)
                //{
                //    TabControl1.AddTab("tabGeneral", "General", query.BuildQuery(CentralPages.WebGroupHome), CentralPages.WebGroupHome);
                //    TabControl1.AddTab("tabUsers", "Users", query.BuildQuery(CentralPages.WebGroupUsers), CentralPages.WebGroupUsers);
                //}

                //TabControl1.AddTab("tabGroups", groupTabText, query.BuildQuery(CentralPages.WebGroups), CentralPages.WebGroups);

                // ObjectKey
                //QueryParser q = query.Clone();
                //q.Set(ObjectKey.KeyString, new ObjectKey(WebObjects.WebGroup, id));
                //q.Set(ObjectKey.KeySource, WebHelper.UrlEncode(query.BuildQuery(CentralPages.WebGroupHome)));

                //TabControl1.SelectTab(query.BasePath);

                BuildBreadcrumb();
            }
        }

        private void BuildBreadcrumb()
        {
            bool titleDisplayed = false;
            bool firstItem = true;

            var sb = new StringBuilder();
            var context = new WContext(this.Parent);

            context.Remove(WebColumns.UserId);

            WebGroup root = null;
            if (RootGroupId == -1)
            {
                var element = context.Element;
                var set = element.GetParameterSet();
                var defaultParentName = ParameterizedWebObject.GetValue("ParentGroup", element, set);
                if (!string.IsNullOrEmpty(defaultParentName))
                {
                    root = WebGroup.SelectNode(defaultParentName);
                    if (root != null)
                        RootGroupId = root.Id;
                }
            }
            else
            {
                root = WebGroup.Get(RootGroupId);
            }

            if (root != null)
            {
                var user = WSession.Current.User;
                if (user.Id == root.OwnerId || root.IsManager(user))
                    hAccess.Value = "1";
            }

            //var managerGroupName = ParameterizedWebObject.GetValue("ManagerGroup", element, set);
            //WebGroup managerGroup = string.IsNullOrEmpty(managerGroupName) ? null : WebGroup.SelectNode(managerGroupName);
            //if (managerGroup != null && WSession.Current.User.IsMemberOf(managerGroup.Id))
            //    hAccess.Value = "1";

            var rootId = RootGroupId;
            var groupId = GroupId > 0 ? GroupId : context.GetId(WebColumns.ParentId);
            var q = context.Query.Clone();

            if (groupId == -1)
                groupId = context.GetId(WebColumns.GroupId);

            var parentId = groupId;

            while (groupId > 0 && groupId != rootId)
            {
                WebGroup item = WebGroup.Get(groupId);
                if (item != null)
                {
                    if (!titleDisplayed)
                    {
                        lblTitle.InnerHtml = item.Name;
                        titleDisplayed = true;
                    }

                    context.Set(WebColumns.ParentId, item.Id);

                    if (firstItem)
                    {
                        sb.Insert(0, string.Format("<li><a href=\"{0}\">{1}</a></li>", context.BuildQuery(), item.Name));
                        firstItem = false;
                    }
                    else
                    {
                        sb.Insert(0, string.Format("<li><a href=\"{0}\">{1}</a> <span class=\"divider\">/</span></li>", context.BuildQuery(), item.Name));
                    }

                    groupId = item.ParentId;
                }
                else
                    groupId = -1;
            }

            q.Remove(WebColumns.GroupId);

            if (parentId > 0 && parentId != rootId)
                q.Set(WebColumns.ParentId, parentId);

            // Build Tabs
            q.SetOpen("GroupOverview");
            linkOverview.HRef = q.BuildQuery();

            q.SetOpen("Groups");
            linkSubGroups.HRef = q.BuildQuery();

            q.SetOpen("Members");
            linkMembers.HRef = q.BuildQuery();

            q.SetOpen("Access");
            linkAccess.HRef = q.BuildQuery();

            q.Remove(WebColumns.ParentId);
            q.RemoveOpen();

            if (firstItem)
                sb.Insert(0, string.Format("<li><a href=\"{0}\"><strong>{1}</strong></a></li>", q.BuildQuery(), MasterListConstants.ROOT_NAME));
            else
                sb.Insert(0, string.Format("<li><a href=\"{0}\"><strong>{1}</strong></a> <span class=\"divider\">/</span></li>", q.BuildQuery(), MasterListConstants.ROOT_NAME));

            if (!titleDisplayed)
                lblTitle.InnerHtml = MasterListConstants.ROOT_NAME + (!string.IsNullOrEmpty(Text) ? string.Format(" / {0}", Text) : string.Empty);

            lblBreadcrumb.Text = sb.ToString();
        }

        public int RootGroupId { get; set; }
        public int GroupId { get; set; }
        public string Text { get; set; }
        public MasterListTab SelectedTab { get; set; }
    }
}