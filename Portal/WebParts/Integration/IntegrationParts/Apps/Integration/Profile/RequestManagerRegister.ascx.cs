using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Core.Shared;
using WCMS.Framework.Utilities;

using WCMS.WebSystem.Apps.Integration;
using WCMS.Framework.Net;
using WCMS.WebSystem.Agent;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile
{
    /// <summary>
    /// Request Manager for new account registration. Being used by MMC registration.
    /// </summary>
    public partial class RequestManagerRegister : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.ParameterizedObject;

                var userProfileUrlFormat = element.GetParameterValue("UserProfileUrlFormat", "");
                if (!string.IsNullOrEmpty(userProfileUrlFormat))
                    hUserProfileUrlFormat.Value = userProfileUrlFormat;

                var centralProfileUrl = element.GetParameterValue("CentralProfileUrl", "");
                if (!string.IsNullOrEmpty(centralProfileUrl))
                    hCentralProfileUrl.Value = centralProfileUrl;

                var id = context.GetId(WebColumns.Id);
                if (id > 0)
                {
                    var userGroup = WebUserGroup.Get(id);
                    var user = userGroup.User;
                    var userDetailsFormat = !string.IsNullOrEmpty(userProfileUrlFormat) ? userProfileUrlFormat : MemberConstants.UserProfilePageFormat;

                    lblRequestor.InnerHtml = AccountHelper.GetPrefixedName(user, false);
                    imagePhotoPath.Src = user.GetPhotoPath("200x200");
                    linkUserProfileUrl.HRef = string.Format(userDetailsFormat, user.Id);

                    MultiView1.SetActiveView(viewReject);
                }
                else
                {
                    var membershipGroup = element.GetParameterValue("MembershipGroup");
                    if (!string.IsNullOrEmpty(membershipGroup))
                        ObjectDataSource1.SelectParameters["group"].DefaultValue = membershipGroup;

                    var approverGroups = element.GetParameterValue("ApproverGroups");
                    if (!string.IsNullOrEmpty(approverGroups))
                        ObjectDataSource1.SelectParameters["approvers"].DefaultValue = approverGroups;

                    GridView1.DataBind();
                    MultiView1.SetActiveView(viewGrid);

                    cboCountries.DataSource = Country.GetList();
                    cboCountries.DataBind();
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Approve") || e.CommandName.Equals("Reject"))
            {
                int id = DataUtil.GetInt32(e.CommandArgument);
                if (id > 0)
                {
                    var context = new WContext(this);
                    switch (e.CommandName)
                    {
                        case "Approve":
                            var ug = WebUserGroup.Get(id);
                            if (MemberHelper.ActivateAccount(ug.UserId, ug.GroupId, context.GetParameterSet(), context.Context))
                                GridView1.DataBind();
                            break;

                        case "Reject":

                            /*
                            if (user != null && group != null)
                            {
                                // Send Notification Email
                                var content = FileHelper.ReadFile(MapPath(paramSet.GetParameterValue("AccountRejectedEmailToUser")));
                                var subject = paramSet.GetParameterValue("AccountRejectedEmailToUserSubject", "Sorry, your request to join $(GroupName) has been REJECTED");

                                NamedValueProvider values = new NamedValueProvider();
                                values.Add("RequesterName", string.IsNullOrWhiteSpace(user.FirstName) ? user.FullName : user.FirstName);
                                values.Add("MEMBER_NAME", AccountHelper.GetPrefixedName(user, true));
                                values.Add("GroupName", group.Name);

                                content = Substituter.Substitute(content, values);
                                subject = Substituter.Substitute(subject, values);

                                WebMessageQueue.CreateEmail(content, subject, user.Email).Update();
                                AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
                            }

                            if (item != null)
                                item.Delete();

                            if (!user.IsActive)
                                user.Delete();
                            */

                            context.Set(WebColumns.Id, id);
                            context.Redirect();

                            break;
                    }
                }
            }
        }

        public DataSet Select(string group, string approvers, string keyword, int countryCode, string userProfileUrlFormat = "", string centralProfileUrl = "")
        {
            if (WSession.Current.IsAdministrator || AccountHelper.IsPresentOrMember(approvers))
            {
                var userDetailsFormat = !string.IsNullOrEmpty(userProfileUrlFormat) ? userProfileUrlFormat : MemberConstants.UserProfilePageFormat;
                var g = WebGroup.Get(group);
                var userGroups = WebUserGroup.GetByGroupId(g.Id);
                WebUser i = null;
                string loweredKeyword = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();
                WebAddress home = null;
                MemberLink memberLink = null;
                Country country = null;

                return DataHelper.ToDataSet(from userGroup in userGroups
                                            where (i = userGroup.User) != null

                                                && ((i.Status == AccountStatus.PENDING) || !userGroup.IsActive && i.Status == AccountStatus.ACTIVE)

                                                && (memberLink = MemberLink.Provider.GetByUserId(i.Id)) != null

                                                && ((country = Country.Get(memberLink.LocaleCountryCode)) != null || true)

                                                && (countryCode > 0 && country != null && country.CountryCode == countryCode || countryCode <= 0)

                                                && (string.IsNullOrEmpty(loweredKeyword)
                                                  || (DataHelper.HasMatch(i.UserName, loweredKeyword)
                                                  || DataHelper.HasMatch(i.FirstName, loweredKeyword)
                                                  || DataHelper.HasMatch(i.LastName, loweredKeyword)
                                                  || DataHelper.HasMatch(i.MiddleName, loweredKeyword)
                                                  || DataHelper.HasMatch(i.Email, loweredKeyword)
                                                  || DataHelper.HasMatch(i.MobileNumber, loweredKeyword)
                                                  || (memberLink != null && DataHelper.HasMatch(memberLink.ExternalIdNo, loweredKeyword))
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
                                                userGroup.Id,
                                                i.Email,
                                                i.FirstName,
                                                i.LastName,
                                                i.UserName,
                                                i.DateCreated,
                                                CountryName = country != null ? country.CountryName : "",
                                                PhotoPath = i.GetPhotoPath("200x200"),
                                                UserProfileUrl = string.Format(userDetailsFormat, i.Id),
                                                CentralProfileUrl = memberLink != null && !string.IsNullOrEmpty(centralProfileUrl) ? string.Format(centralProfileUrl, memberLink.Id) : ""
                                            }
                                        );
            }

            return DataHelper.GetEmptyDataSet();
        }

        protected void cmdRejectSubmit_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var id = context.GetId(WebColumns.Id);

            var item = WebUserGroup.Get(id);
            if (item != null)
            {
                var user = item.User;
                var group = item.Group;
                var link = MemberLink.Provider.GetByUserId(user.Id);

                var paramSet = context.GetParameterSet();
                var baseAddress = paramSet.GetParameterValue("BaseAddress");

                if (string.IsNullOrEmpty(baseAddress))
                    baseAddress = WConfig.BaseAddress;

                if (user != null && group != null)
                {
                    // Send Notification Email
                    var content = FileHelper.ReadFile(MapPath(paramSet.GetParameterValue("AccountRejectedEmailToUser")));
                    var subject = paramSet.GetParameterValue("AccountRejectedEmailToUserSubject", "Integration Portal: Sorry, your request to join $(GroupName) group was REJECTED");
                    var values = new NamedValueProvider();
                    values.Add("RequesterName", string.IsNullOrWhiteSpace(user.FirstName) ? user.FullName : user.FirstName);
                    values.Add("MEMBER_NAME", AccountHelper.GetPrefixedName(user, true));
                    values.Add("GroupName", group.Name);
                    values.Add("REASON", txtReason.Text.Trim());

                    content = Substituter.Substitute(content, values);
                    subject = Substituter.Substitute(subject, values);

                    WebMessageQueue.CreateEmail(content, subject, user.Email).Update();
                    AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
                }

                if (item != null)
                    item.Delete();

                if (link != null)
                    link.Delete();

                if (!user.IsActive)
                    user.Delete();
            }

            context.Remove(WebColumns.Id);
            context.Redirect();
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {

        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GridView1.DataBind();
        }

        protected void cboCountries_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}