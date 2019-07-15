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
using WCMS.Framework.Core.Shared;
using WCMS.WebSystem.Apps.Integration;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile
{
    public partial class WebGroupUsers : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this.Context);
                var element = context.Element;

                var groupPath = element.GetParameterValue(MemberConstants.ParentGroupKey);
                if (!string.IsNullOrEmpty(groupPath))
                {
                    var group = WebGroup.SelectNode(groupPath);
                    if (group != null)
                        hidBaseGroupId.Value = group.Id.ToString();

                    #region Celebrants Filter

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

                    #endregion

                    GridView1.DataBind();

                    lblCount.InnerHtml = Count().ToString();
                }
            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            var query = new QueryParser(this);
            bool success = false;
            int id = DataUtil.GetId(hidBaseGroupId.Value);
            var item = WebGroup.Get(id);

            string name = txtId.Text.Trim();
            WebUser user = null;

            if (WebUser.IsValidUniqueName(name))
                user = WebUser.GetByUniqueName(name);
            else
                user = WebUser.Get(name);

            if (user != null && item != null)
            {
                if (!item.IsMember(user.Id))
                {
                    item.AddUser(user.Id);
                    success = true;
                }
            }

            if (success)
            {
                txtId.Text = string.Empty;
                GridView1.DataBind();
            }
        }

        public DataSet Select(int groupId, string keyword, int celebrantsFilter = -1)
        {
            if (groupId > 0)
            {
                string keywordL = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();

                var userDetailsFormat = MemberConstants.UserProfilePageFormat;

                var members = MemberLink.Provider.GetList(MemberAccountStatus.Approved);
                WebUser user = null;
                MemberLink member = null;

                var items = from u in WebUserGroup.GetByGroupId(groupId, -1)
                            where (user = u.User) != null
                                && ((member = members.FirstOrDefault(m => m.UserId > 0 && m.UserId == user.Id)) != null || true)
                                && (string.IsNullOrEmpty(keywordL) ||
                                                (user.UserName.ToLower().Contains(keywordL) ||
                                                    user.FullName.ToLower().Contains(keywordL) ||
                                                    user.Email.ToLower().Contains(keywordL)
                                                    || (member == null || member.ExternalIdNo.ToLower().Contains(keywordL))
                                                )
                                    )
                                && (!(celebrantsFilter > 0 && celebrantsFilter < 13) || member != null && member.MembershipDate.Month == celebrantsFilter)
                            select new
                            {
                                user.Id,
                                user.UserName,
                                user.Email,
                                user.MobileNumber,
                                user.FirstName,
                                user.LastName,
                                user.MiddleName,
                                u.DateJoined,
                                u.Active,
                                Gender = user.Gender,
                                ExternalIDNo = member == null ? string.Empty : member.ExternalIdNo,
                                UserProfileUrl = string.Format(userDetailsFormat, user.Id)
                            };

                return DataHelper.ToDataSet(items);
            }

            return DataHelper.GetEmptyDataSet();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var query = new QueryParser(this);
            int userId = DataUtil.GetId(e.CommandArgument);
            int id = DataUtil.GetId(hidBaseGroupId.Value);

            switch (e.CommandName)
            {
                case "Custom_Delete":
                    WebUserGroup.Delete(userId, id);
                    GridView1.DataBind();
                    break;
            }
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GridView1.DataBind();
        }

        private int Count()
        {
            int groupId = DataUtil.GetId(hidBaseGroupId.Value);
            WebGroup g = null;
            if (groupId > 0 && (g = WebGroup.Get(groupId)) != null)
            {
                var members = MemberLink.Provider.GetList(MemberAccountStatus.Approved);

                WebUser user = null;
                MemberLink member = null;

                var items = from u in WebUserGroup.GetByGroupId(groupId, 1)
                            where (user = u.User) != null &&
                                (member = members.FirstOrDefault(m => m.UserId > 0 && m.UserId == user.Id)) != null
                            select u;

                return items.Count();
            }

            return 0;
        }

        private WebAddress GetHomeAddress(WebUser user)
        {
            var address = user.GetAddress(AddressTags.Home);

            if (address == null)
                address = new WebAddress();

            return address;
        }

        protected void cmdDownload_Click(object sender, EventArgs e)
        {
            int groupId = DataUtil.GetId(hidBaseGroupId.Value);
            WebGroup g = null;
            if (groupId > 0 && (g = WebGroup.Get(groupId)) != null)
            {
                var celebrantsFilter = DataUtil.GetInt32(cboCelebrants.SelectedValue, -1);
                var members = MemberLink.Provider.GetList(MemberAccountStatus.Approved);

                WebUser user = null;
                MemberLink member = null;
                WebAddress address = null;

                var items = from u in WebUserGroup.GetByGroupId(groupId, -1)
                            where (user = u.User) != null
                                && ((member = members.FirstOrDefault(m => m.UserId > 0 && m.UserId == user.Id)) != null)
                                && (address = GetHomeAddress(user)) != null
                                && (!(celebrantsFilter > 0 && celebrantsFilter < 13) || member.MembershipDate.Month == celebrantsFilter)
                            select new
                            {
                                user.Id,
                                user.UserName,
                                user.Email,
                                user.LastName,
                                user.FirstName,
                                user.MiddleName,
                                member.ExternalIdNo,
                                Group = g.Name,
                                u.DateJoined,
                                u.Active,
                                DateOfMembership = member.MembershipDate.ToString("yyyy-MM-dd"),
                                address.AddressLine1,
                                address.AddressLine2,
                                address.ZipCode,
                                CityOrTown = address.CityTown,
                                ProvinceOrState = address.StateProvinceString,
                                Country = address.CountryString,
                                user.MobileNumber,
                                user.TelephoneNumber,
                                member.WorkDesignation,
                                user.DateCreated,
                                member.LastUpdate,
                                member.PhotoPath,
                                Salutation = AccountHelper.GetNamePrefix(user, NamePrefixes.Brotherhood)
                            };

                var users = DataHelper.ToDataSet(items);

                WebHelper.DownloadAsCsv(users, "Group_Users");
            }
        }

        protected void cboCelebrants_SelectedIndexChanged(object sender, EventArgs e)
        {
            var filter = DataUtil.GetInt32(cboCelebrants.SelectedValue);
            var query = new QueryParser(this);

            if (filter > 0 && filter < 13)
                query.Set("Celebrants", filter);
            else
                query.Remove("Celebrants");

            query.Redirect();
        }
    }
}