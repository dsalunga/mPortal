using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Diagnostics;

using WCMS.WebSystem.ViewModel;
using WCMS.WebSystem.Apps.Integration;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile
{
    public partial class AddressBookBrowser : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var watch = PerformanceLog.StartLog();

                var context = new WContext(this);
                var element = context.Element;

                int groupId = -1;
                var groupPath = element.GetParameterValue(MemberConstants.ParentGroupKey);
                var dropDownVisible = element.GetParameterValue(MemberConstants.DropDownVisibleKey);
                var celebrantsFilter = element.GetParameterValue(MemberConstants.CelebrantsFilterVisible);

                var forcePrivate = DataHelper.GetBool(element.GetParameterValue("ForcePrivate"), false);
                if (forcePrivate)
                    ObjectDataSource1.SelectParameters["forcePrivate"].DefaultValue = "true";

                var managers = element.GetParameterValue("Managers");
                var viewerIsManager = WSession.Current.IsAdministrator || (!string.IsNullOrEmpty(managers) && AccountHelper.IsPresentOrMember(managers));
                if (viewerIsManager)
                    ObjectDataSource1.SelectParameters["viewerIsManager"].DefaultValue = "true";

                var userProfileUrlFormat = element.GetParameterValue("UserProfileUrlFormat", "");
                if (!string.IsNullOrEmpty(userProfileUrlFormat))
                    hUserProfileUrlFormat.Value = userProfileUrlFormat;

                var pageSize = DataUtil.GetInt32(element.GetParameterValue("PageSize"));
                if (pageSize > 0)
                    GridView1.PageSize = pageSize;

                if (!string.IsNullOrEmpty(groupPath))
                {
                    var group = WebGroup.SelectNode(groupPath);
                    if (group != null)
                    {
                        groupId = group.Id;
                        hidBaseGroupId.Value = group.Id.ToString();
                    }
                }

                #region Celebrants Filter

                if (DataHelper.GetBool(celebrantsFilter, true))
                {
                    var now = DateTime.Now;
                    var monthNames = DateTimeHelper.GetMonthNames();

                    cboCelebrants.Items.Add(new ListItem(string.Format("* {0} *", monthNames[now.Month - 1].ToUpper()), now.Month.ToString()));
                    for (int i = 0; i < 12; i++)
                        if (i + 1 != now.Month)
                            cboCelebrants.Items.Add(new ListItem(monthNames[i], (i + 1).ToString()));

                    var celebrants = context.GetInt32("Celebrants");
                    if (celebrants > 0 && celebrants < 13)
                        cboCelebrants.SelectedValue = celebrants.ToString();

                    cboCelebrants.Visible = true;
                }

                #endregion

                #region Group Filter

                if (DataHelper.GetBool(dropDownVisible, true))
                {
                    cboGroups.Items.AddRange(WebGroupViewModel.GenerateListItem(groupId, true).ToArray());

                    int id = DataUtil.GetId(Request, "GroupId");
                    if (id > 0)
                        cboGroups.SelectedValue = id.ToString();

                    cboGroups.Visible = true;
                }

                #endregion

                GridView1.DataBind();

                PerformanceLog.EndLog(string.Format("Integration-AddressBook-Load: {0}/{1}", context.ObjectId, context.RecordId), watch, context.PageId);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        public DataSet Select(int groupId, string keyword, int baseGroupId, bool viewerIsManager, bool forcePrivate, int celebrantsFilter = -1, string userProfileUrlFormat = "")
        {
            if (baseGroupId > 0)
            {
                var userDetailsFormat = !string.IsNullOrEmpty(userProfileUrlFormat) ? userProfileUrlFormat : MemberConstants.UserProfilePageFormat;
                var members = MemberLink.Provider.GetList(MemberAccountStatus.Approved, celebrantsFilter > 0 && celebrantsFilter < 13 ? celebrantsFilter : -1);
                var users = WebUser.GetList(baseGroupId, 1);

                MemberLink memberLink = null;
                WebAddress home = null;

                string loweredKeyword = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();

                var orderedResult = (from i in users
                                     where
                                           i != null
                                        && (memberLink = members.FirstOrDefault(m => m.UserId == i.Id)) != null
                                        && (groupId == -1 || i.IsMemberOf(groupId))
                                        //&& (!(celebrantsFilter > 0 && celebrantsFilter < 13) || memberLink.MembershipDate.Month == celebrantsFilter)
                                        && (string.IsNullOrEmpty(loweredKeyword)
                                            || (DataHelper.HasMatch(i.UserName, loweredKeyword)
                                              || DataHelper.HasMatch(i.FirstName, loweredKeyword)
                                              || DataHelper.HasMatch(i.LastName, loweredKeyword)
                                              || DataHelper.HasMatch(i.MiddleName, loweredKeyword)
                                              || DataHelper.HasMatch(i.Email, loweredKeyword)
                                              || DataHelper.HasMatch(i.MobileNumber, loweredKeyword)
                                              || DataHelper.HasMatch(memberLink.ExternalIdNo, loweredKeyword)
                                              || ((home = i.GetAddress(AddressTags.Home)) != null
                                                   && (DataHelper.HasMatch(home.AddressLine1, loweredKeyword)
                                                        || DataHelper.HasMatch(home.AddressLine2, loweredKeyword)
                                                        || DataHelper.HasMatch(home.ZipCode, loweredKeyword)
                                                      )
                                                  )
                                             )
                                           )
                                     select new
                                     {
                                         i.Id,
                                         i.UserName,
                                         Email = viewerIsManager || (!memberLink.IsPrivate && !forcePrivate) ? i.Email : string.Empty,
                                         FullName = i.FirstAndLastName,
                                         PhotoPath = memberLink.GetPhotoPathIfNull("200x200"),
                                         UserProfileUrl = string.Format(userDetailsFormat, i.Id),
                                         ContactNo = viewerIsManager || (!memberLink.IsPrivate && !forcePrivate) ? (string.IsNullOrWhiteSpace(i.MobileNumber) ? i.TelephoneNumber : i.MobileNumber) : string.Empty,
                                         memberLink.ExternalIdNo,
                                         memberLink.MembershipDate,
                                         i.LastUpdate,
                                         i.StatusText
                                     })
                .OrderByDescending(i => i.LastUpdate).AsEnumerable();

                return DataHelper.ToDataSet(orderedResult);
            }

            return DataHelper.GetEmptyDataSet();
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {

        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
        }

        protected void cboCelebrants_SelectedIndexChanged(object sender, EventArgs e)
        {
            var query = new WQuery(this);

            var filter = DataUtil.GetInt32(cboCelebrants.SelectedValue);
            if (filter > 0 && filter < 13)
                query.Set("Celebrants", filter);
            else
                query.Remove("Celebrants");

            query.Redirect();
        }

        protected void cboGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            var query = new WQuery(this);

            var groupId = DataUtil.GetId(cboGroups.SelectedValue);
            if (groupId > 0)
                query.Set("GroupId", groupId);
            else
                query.Remove("GroupId");

            query.Redirect();
        }
    }
}