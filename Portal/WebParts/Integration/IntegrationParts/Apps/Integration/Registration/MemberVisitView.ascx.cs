using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Utilities;

using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration
{
    public partial class MemberVisitView : System.Web.UI.UserControl
    {
        private WContext context;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            context = new WContext(this);

            if (!IsPostBack)
            {
                var q = context.Query.Clone();
                q.SetOpen("Visit-Entry");
                linkNew.HRef = q.BuildQuery();

                this.LoadData();
                this.SetAccess();
            }
        }

        private void SetAccess()
        {
            if (!context.Element.IsUserMgmtPermitted(Permissions.ManageContent))
            {
                // Read only mode
                ObjectDataSourceVisits.SelectParameters["readOnly"].DefaultValue = "1";
                gridView.DataBind();
                linkNew.Visible = false;
            }
        }

        private void LoadData()
        {
            // Set Membership information
            var element = context.Element;

            hTagFilter.Value = element.GetParameterValue("TagFilter");

            string localeGroupsPath = element.GetParameterValue(MemberConstants.LocaleGroupPathKey, MemberConstants.LocaleGroupPath);
            if (!string.IsNullOrEmpty(localeGroupsPath))
            {
                var group = WebGroup.SelectNode(localeGroupsPath);
                if (group != null)
                {
                    cboGroup.DataSource = group.Children;
                    cboGroup.DataBind();
                }
            }

            var groupId = context.Get(WebColumns.GroupId);
            if (!string.IsNullOrEmpty(groupId))
            {
                var item = cboGroup.Items.FindByValue(groupId);
                if (item != null)
                    item.Selected = true;
            }
        }

        //public IEnumerable<object> Select(string sortBy, int startRowIndex, int maximumRows, int groupId, int readOnly)
        //{
        //    var items = Select(groupId, readOnly);
        //    return DataHelper.PageWithSort(items, sortBy, startRowIndex, maximumRows);
        //}

        public DataSet Select(int groupId, int readOnly, string keyword, int userId, string tagFilter)
        {
            var q = new WQuery(true);
            var loweredKeyword = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();
            var userDetailsFormat = MemberConstants.UserProfilePageFormat;
            var items = MemberVisit.Provider.GetList(groupId, userId, tagFilter); // groupId > 0 ? ODKVisit.Provider.GetList(groupId) : (userId > 0 ? ODKVisit.Provider.GetListByUserId(userId) : ODKVisit.Provider.GetList());

            q.SetOpen("Visit-Entry");

            var subItems = from i in items
                           let user = i.VisitedUserId > 0 ? WebUser.Get(i.VisitedUserId) : null
                           let g = i.GroupId > 0 ? WebGroup.Get(i.GroupId) : null
                           where
                            (string.IsNullOrEmpty(loweredKeyword) || (
                                   (user != null &&
                                       (DataUtil.HasMatch(user.UserName, loweredKeyword) ||
                                       DataUtil.HasMatch(user.FirstName, loweredKeyword) ||
                                       DataUtil.HasMatch(user.LastName, loweredKeyword) ||
                                       DataUtil.HasMatch(user.MiddleName, loweredKeyword) ||
                                       DataUtil.HasMatch(user.Email, loweredKeyword) ||
                                       DataUtil.HasMatch(user.MobileNumber, loweredKeyword))
                                   ) ||
                                   (user == null && DataUtil.HasMatch(i.Name, loweredKeyword)) ||
                                   (g != null && DataUtil.HasMatch(g.Name, loweredKeyword)) ||
                                   DataUtil.HasMatch(i.Status, loweredKeyword) ||
                                   DataUtil.HasMatch(i.ActionTaken, loweredKeyword) ||
                                   DataUtil.HasMatch(i.ActualReport, loweredKeyword)
                               ))
                           select new
                           {
                               i.Id,
                               Status = DataUtil.GetStringPreview(i.Status, MemberConstants.ODKStatusPrevChars - 3),
                               i.DateVisited,
                               PhotoUrl = user != null ? user.GetPhotoPath("200x200") : WConstants.NoPhotoThumb,
                               GroupName = g != null ? g.Name : "",
                               MemberName = user != null ? AccountHelper.GetPrefixedName(user) : i.Name,
                               EditUrl = q.Set(WebColumns.Id, i.Id).BuildQuery(),
                               //Name = user != null ? HtmlHelper.BuildHyperlink(
                               //        string.Format(userDetailsFormat, user.Id), 
                               //        AccountHelper.GetPrefixedName(user), 
                               //        "View Member Profile", "_blank")
                               //    : i.Name,
                               ReadOnly = readOnly == 1
                           };

            return DataUtil.ToDataSet(subItems);
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataUtil.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    context.Set("VisitId", id);
                    context.Open("Visit-Entry");
                    break;

                case "Custom_Delete":
                    if (id > 0)
                    {
                        MemberVisit.Provider.Delete(id);
                        gridView.DataBind();
                    }
                    break;
            }
        }

        protected void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            var groupId = DataUtil.GetId(cboGroup.SelectedValue);

            if (groupId > 0)
                context.Set(WebColumns.GroupId, groupId);
            else
                context.Remove(WebColumns.GroupId);

            context.Redirect();
        }

        protected void cmdDownload_Click(object sender, EventArgs e)
        {
            WebGroup g = null;
            WebUser user = null;

            var groupId = DataUtil.GetId(cboGroup.SelectedValue);
            var tag = hTagFilter.Value;

            var items = from u in MemberVisit.Provider.GetList(groupId, -2, tag)
                        where
                            ((user = u.VisitedUser) != null || true) &&
                            ((g = u.Group) != null || true)
                        select new
                        {
                            u.Id,
                            Name = user != null ? user.FirstAndLastName : u.Name,
                            Email = user != null ? user.Email : string.Empty,
                            GroupName = g != null ? g.Name : string.Empty,
                            u.ContactNo,
                            u.Address,
                            MembershipDate = u.MembershipDate.ToString("yyyy-MMM-dd"),
                            DateVisited = u.DateVisited.ToString("yyyy-MMM-dd"),
                            u.TimesVisited,
                            CaseStatus = u.Status,
                            CouncillorsObservation = u.ActualReport,
                            u.ActionTaken,
                            EnteredBy = u.CreatedUser != null ? u.CreatedUser.FirstAndLastName : string.Empty,
                            DateEntered = u.DateCreated.ToString(),
                            u.Tags
                        };

            var users = DataUtil.ToDataSet(items);

            WebUtil.DownloadAsXml(users, "MemberVisit", "Visit Sheet");
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            gridView.DataBind();
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            var groupId = DataUtil.GetId(cboGroup.SelectedValue);

            context.Remove(WebColumns.GroupId);
            context.Redirect();
        }
    }
}