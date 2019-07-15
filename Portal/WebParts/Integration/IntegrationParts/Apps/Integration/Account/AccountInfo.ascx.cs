using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using WCMS.WebSystem.Controls;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Utilities;
using WCMS.Framework.Core.Shared;
using WCMS.Framework.Security;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration.Account
{
    public partial class UserProfileDetails : System.Web.UI.UserControl
    {
        private const string TAB_PROFILE = "tabProfile";
        private const string TAB_ODK = "tabODK";
        private const string TAB_ATTENDANCE = "Attendance";
        private const string TAB_MAKEUP = "tabMakeUp";
        private const string TAB_OTHER = "tabOtherInfo";

        protected TabControl TabControl1;
        protected string UserLocale = "";
        protected bool ViewerIsManager = false;
        protected bool IsCurrentUser = false;
        protected bool IsExternalLinked = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetData();

                // Configure Cancel
                //ConfigureCancel();
            }
        }

        protected void TabControl1_SelectedTabChanged(object sender, TabEventArgs e)
        {
            switch (e.TabName)
            {
                case TAB_PROFILE:
                    MultiView1.SetActiveView(viewProfile);
                    break;

                case TAB_ODK:
                    break;

                case TAB_MAKEUP:
                    break;

                case TAB_OTHER:
                    MultiView1.SetActiveView(viewOtherInfo);
                    break;
            }
        }

        private void SetData()
        {
            var context = new WContext(this);
            int userId = context.GetId(WebColumns.UserId);
            IsCurrentUser = 0 > userId || userId == WSession.Current.UserId;
            bool isAdmin = WSession.Current.IsAdministrator;

            var user = (IsCurrentUser ? WSession.Current.User : WebUser.Get(userId));
            if (user != null)
            {
                var element = context.Element;
                var managers = element.GetParameterValue("Managers");
                var disableAdminTabs = DataHelper.GetBool(element.GetParameterValue("DisableAdminTabs"), false);
                var hideGroups = DataHelper.GetBool(element.GetParameterValue("HideGroups", "0"));
                var forcePrivate = DataHelper.GetBool(element.GetParameterValue("ForcePrivate"), false);

                ViewerIsManager = isAdmin || (!string.IsNullOrEmpty(managers) && AccountHelper.IsPresentOrMember(managers));

                #region Tabs

                if (ViewerIsManager && !disableAdminTabs)
                {
                    TabControl1.AddTab(TAB_PROFILE, "Profile");

                    var odkUrl = element.GetParameterValue("ODKUrl");
                    var makeUpUrl = element.GetParameterValue("MakeUpUrl");
                    var attendanceUrl = element.GetParameterValue("AttendanceUrl");

                    if (!string.IsNullOrEmpty(odkUrl))
                    {
                        var query = new QueryParser(odkUrl);
                        query.Set(WebColumns.UserId, user.Id);

                        TabControl1.AddTab(TAB_ODK, "ODK", query.BuildQuery(), "", HtmlArchorTargets.Blank);
                    }

                    if (!string.IsNullOrEmpty(attendanceUrl))
                    {
                        var query = new QueryParser(attendanceUrl);
                        query.Set(WebColumns.UserId, user.Id);

                        TabControl1.AddTab(TAB_ATTENDANCE, "Attendance", query.BuildQuery(), "", HtmlArchorTargets.Blank);
                    }

                    if (!string.IsNullOrEmpty(makeUpUrl))
                    {
                        var query = new QueryParser(makeUpUrl);
                        query.Set(WebColumns.UserId, user.Id);

                        TabControl1.AddTab(TAB_MAKEUP, "Make-Up", query.BuildQuery(), "", HtmlArchorTargets.Blank);
                    }

                    TabControl1.Visible = true;
                }

                #endregion

                lblHeader.InnerHtml = AccountHelper.GetPrefixedName(user);

                //lblUserFirstName.InnerHtml = user.FirstName;
                lblFullName.InnerHtml = user.FirstName + " " + user.LastName;
                lblLastUpdate.InnerHtml = user.LastUpdate.ToString("d MMMM yyyy h:mm tt");
                lblFirstName.InnerHtml = user.FirstName;

                if (user.Gender != '\0' && user.Gender != GenderTypes.Unspecified)
                {
                    fieldGender.Visible = true;
                    lblGender.InnerHtml = GenderTypes.GetText(user.Gender);
                }

                if (user.MaritalStatusId != MaritalStatus.UnspecifiedId)
                {
                    fieldMS.Visible = true;
                    lblMS.InnerHtml = MaritalStatus.GetText(user.MaritalStatusId);
                }

                // Send Message
                if (!IsCurrentUser)
                {
                    string sendMessagePath = element.GetParameterValue("SendMessagePath");
                    if (!string.IsNullOrWhiteSpace(sendMessagePath))
                    {
                        panelSendMessage.Visible = true;
                        linkSendMessage.HRef = string.Format(sendMessagePath, user.Id);
                    }
                }
                
                if(IsCurrentUser || ViewerIsManager)
                {
                    fieldAccountType.Visible = true;
                    lblAccountType.InnerHtml = user.ProviderId == AccountConstants.DefaultExternalProvider ? "Integration Ext" : "Integration Portal";
                }

                var link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null)
                {
                    var isPrivate = forcePrivate || link.IsPrivate;
                    UserLocale = link.Locale;
                    IsExternalLinked = link.MemberId > 0;

                    // Locale Country
                    var localeCountry = link.LocaleCountry;
                    if (localeCountry != null)
                    {
                        fieldLocaleCountry.Visible = true;
                        lblLocaleCountry.InnerHtml = localeCountry.CountryName;
                    }

                    if (ViewerIsManager || IsCurrentUser || !isPrivate)
                    {
                        lblEmail.InnerHtml = user.Email;
                        panelEmail.Visible = true;

                        if (!string.IsNullOrEmpty(user.Email2))
                        {
                            lblEmail2.InnerHtml = user.Email2;
                            panelEmail2.Visible = true;
                        }
                    }

                    #region Other Info Tab

                    if (ViewerIsManager)
                    {
                        hMemberLinkId.Value = link.MemberLinkId.ToString();

                        txtAdditionalInfo.Text = link.AdditionalInfo;
                        lblOtherInfo.InnerHtml = link.AdditionalInfo;

                        TabControl1.AddTab(TAB_OTHER, "Other Info");
                    }

                    #endregion

                    lblExternalIdNo.InnerHtml = link.ExternalIdNo;
                    lblMembershipDate.InnerHtml = link.MembershipDate.ToString(IsCurrentUser || isAdmin ? "dd-MMM-yyyy" : "MMMM-yyyy");

                    // Show Nickname
                    if (!string.IsNullOrEmpty(link.Nickname))
                    {
                        panelNickname.Visible = true;
                        lblNickname.InnerHtml = link.Nickname;
                    }

                    // User Name
                    if (IsCurrentUser || isAdmin)
                    {
                        lblUserName.InnerHtml = user.UserName;
                        panelUserName.Visible = true;

                        chkPrivate.Checked = isPrivate;
                        panelPrivacy.Visible = true;
                    }


                    if (ViewerIsManager || IsCurrentUser || !isPrivate)
                    {
                        if (!string.IsNullOrEmpty(user.MobileNumber))
                        {
                            panelMobile.Visible = true;
                            txtMobile.InnerHtml = user.MobileNumber; //link.MobileNumber;
                        }

                        panelPersonalAndWorkInfo.Visible = true;

                        var addresses = user.Addresses;
                        var homeAddress = addresses.FirstOrDefault(i => i.Tag.Equals(AddressTags.Home, StringComparison.InvariantCultureIgnoreCase));
                        var workAddress = addresses.FirstOrDefault(i => i.Tag.Equals(AddressTags.Work, StringComparison.InvariantCultureIgnoreCase));

                        #region Start of Personal Information

                        if (homeAddress != null)
                        {
                            if (!string.IsNullOrEmpty(homeAddress.AddressLine1))
                            {
                                panelHomeAddressLine1.Visible = true;
                                txtHomeAddress1.InnerHtml = homeAddress.AddressLine1;
                            }

                            if (!string.IsNullOrEmpty(homeAddress.AddressLine2))
                            {
                                panelHomeAddressLine2.Visible = true;
                                txtHomeAddress2.InnerHtml = homeAddress.AddressLine2;
                            }

                            if (!string.IsNullOrEmpty(homeAddress.ZipCode))
                            {
                                panelHomeZipCode.Visible = true;
                                txtHomeAddressZipCode.InnerHtml = homeAddress.ZipCode;
                            }

                            // Home US State
                            var homeState = homeAddress.StateProvinceString;
                            if (!string.IsNullOrEmpty(homeState))
                            {
                                divHomeState.Visible = true;
                                txtHomeAddressState.InnerHtml = homeState;
                            }

                            var homeCountry = homeAddress.CountryString;
                            if (!string.IsNullOrEmpty(homeCountry))
                            {
                                panelHomeCountry.Visible = true;
                                txtHomeAddressCountry.InnerHtml = homeCountry;
                            }

                            if (!string.IsNullOrEmpty(homeAddress.PhoneNumber))
                            {
                                panelHomePhone.Visible = true;
                                txtHomePhone.InnerHtml = homeAddress.PhoneNumber;
                            }
                        }

                        if (!panelHomePhone.Visible && !string.IsNullOrEmpty(user.TelephoneNumber))
                        {
                            panelHomePhone.Visible = true;
                            txtHomePhone.InnerHtml = user.TelephoneNumber;
                        }

                        #endregion

                        #region Start of Work Information

                        bool hasWorkInfo = false;

                        if (workAddress != null)
                        {
                            if (!string.IsNullOrEmpty(workAddress.AddressLine1))
                            {
                                hasWorkInfo = true;
                                panelWorkAddressLine1.Visible = true;
                                txtWorkAddress1.InnerHtml = workAddress.AddressLine1;
                            }

                            if (!string.IsNullOrEmpty(workAddress.AddressLine2))
                            {
                                hasWorkInfo = true;
                                panelWorkAddressLine2.Visible = true;
                                txtWorkAddress2.InnerHtml = workAddress.AddressLine2;
                            }

                            if (!string.IsNullOrEmpty(workAddress.ZipCode))
                            {
                                hasWorkInfo = true;
                                panelWorkZipCode.Visible = true;
                                txtWorkAddressZipCode.InnerHtml = workAddress.ZipCode;
                            }

                            // Work US State
                            var workState = workAddress.StateProvinceString;
                            if (!string.IsNullOrEmpty(workState))
                            {
                                hasWorkInfo = true;
                                divWorkState.Visible = true;
                                txtWorkAddressState.InnerHtml = workState;
                            }

                            var workCountry = workAddress.CountryString;
                            if (!string.IsNullOrEmpty(workCountry))
                            {
                                hasWorkInfo = true;
                                panelWorkCountry.Visible = true;
                                txtWorkAddressCountry.InnerHtml = workCountry;
                            }

                            if (!string.IsNullOrEmpty(workAddress.PhoneNumber))
                            {
                                hasWorkInfo = true;
                                panelWorkPhone.Visible = true;
                                txtWorkPhone.InnerHtml = workAddress.PhoneNumber;
                            }
                        }

                        // Work Designation
                        if (!string.IsNullOrEmpty(link.WorkDesignation))
                        {
                            hasWorkInfo = true;
                            panelWorkDesignation.Visible = true;
                            txtWorkDesignation.InnerHtml = link.WorkDesignation;
                        }

                        if (!hasWorkInfo && panelWorkInfo != null)
                            panelWorkInfo.Visible = false;

                        #endregion
                    }


                    lblLastUpdate.InnerHtml = link.LastUpdate.ToString("d MMMM yyyy h:mm tt");

                    if (!string.IsNullOrEmpty(user.StatusText))
                    {
                        lblStatusText.InnerText = user.StatusText;
                        panelStatusText.Visible = true;
                    }


                    #region Set Membership information (Groups)

                    if (!hideGroups)
                    {
                        // Instead of getting all groups, just get the links
                        var userGroups = WebUserGroup.GetByUserId(user.Id, IsCurrentUser ? -1 : 1);

                        // Set Ministries
                        string ministriesPath = element.GetParameterValue(MemberConstants.MinistriesPathKey, MemberConstants.MinistriesGroupPath);
                        if (!string.IsNullOrEmpty(ministriesPath))
                        {
                            DisplayGroupMembership(ministriesPath, userGroups, cblMinistries, panelMinistries);
                        }

                        // Set Locale Groups
                        //string localeGroupsPath = element.GetParameterValue(MemberConstants.LocaleGroupPathKey, MemberConstants.LocaleGroupDefaultPath);
                        //if (!string.IsNullOrEmpty(localeGroupsPath))
                        //{
                        //    var g = DisplayGroupMembership(localeGroupsPath, userGroups, rblLocaleGroups, panelLocaleGroups);
                        //    if (g != null)
                        //    {
                        //        var groupServantsGroup = g.GetParameterValue("GroupServants");
                        //        if (!string.IsNullOrEmpty(groupServantsGroup))
                        //        {
                        //            //var userDetailsFormat = MemberConstants.UserProfilePageFormat;

                        //            var officersNode = WebGroup.SelectNode(groupServantsGroup);
                        //            var groupServants = from u in officersNode.Users
                        //                                select new
                        //                                {
                        //                                    u.Id,
                        //                                    Name = AccountHelper.GetPrefixedName(u)
                        //                                };

                        //            if (groupServants.Count() > 0)
                        //            {
                        //                listGS.DataSource = groupServants;
                        //                listGS.DataBind();

                        //                panelGS.Visible = true;
                        //            }
                        //        }
                        //    }
                        //}

                        // Set Special Groups
                        string specialGroupsPath = element.GetParameterValue(MemberConstants.SpecialGroupsPathKey, MemberConstants.SpecialGroupsPath);
                        if (!string.IsNullOrEmpty(specialGroupsPath))
                        {
                            DisplayGroupMembership(specialGroupsPath, userGroups, cblSpecialGroups, panelSpecialGroups);
                        }
                    }

                    #endregion

                    memberPhoto.Src = link.GetPhotoPathIfNull();

                    var photoUpdateUrl = element.GetParameterValue("ProfilePhotoUpdateUrl");
                    if (!string.IsNullOrEmpty(photoUpdateUrl) && IsCurrentUser)
                        linkUpdatePhoto.HRef = photoUpdateUrl;
                    else if (!IsCurrentUser)
                        linkUpdatePhoto.HRef = WebConstants.EMPTY_HREF;
                }
                else
                {
                    lblStatus.Text = "No member record found. Please link your user account to your member account.";
                }
            }
            else
            {
                lblStatus.Text = "Your account has invalid settings or you are not logged in.";
            }
        }

        private WebGroup DisplayGroupMembership(string groupPath, IEnumerable<WebUserGroup> userGroups, DataList cbl, HtmlGenericControl panel)
        {
            WebUserGroup userGroup = null;
            var parentGroup = WebGroup.SelectNode(groupPath);
            if (parentGroup != null)
            {
                var groups = parentGroup.Children;
                if (groups.Count() > 0)
                {
                    var queryGroups = from m in groups
                                      where (userGroup = userGroups.FirstOrDefault(ug => ug.GroupId == m.Id)) != null
                                      select new
                                      {
                                          m.Id,
                                          Name = userGroup.IsActive ? m.Name : m.Name + MemberConstants.PendingApprovalString,
                                          Group = m
                                      };

                    if (queryGroups.Count() > 0)
                    {
                        cbl.DataSource = queryGroups;
                        cbl.DataBind();
                        panel.Visible = true;
                        return queryGroups.First().Group;
                    }
                }
            }

            return null;
        }

        //private void ConfigureCancel()
        //{
        //    WContext context = new WContext(this);
        //    string cancelRedirect = context.Element.GetParameterValue(MemberConstants.CancelRedirect);

        //    if (!string.IsNullOrEmpty(cancelRedirect))
        //        cmdCancel.Visible = true;
        //}

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            string cancelRedirect = context.Element.GetParameterValue(MemberConstants.CancelRedirect);

            if (!string.IsNullOrEmpty(cancelRedirect))
                context.Redirect(cancelRedirect);
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            var memberLinkId = DataUtil.GetId(hMemberLinkId.Value);
            MemberLink link = null;
            if (memberLinkId > 0 && (link = MemberLink.Provider.Get(memberLinkId)) != null)
            {
                link.AdditionalInfo = txtAdditionalInfo.Text.Trim();
                link.Update(false);

                lblOtherInfo.InnerHtml = link.AdditionalInfo;

                MultiViewOtherInfo.SetActiveView(viewOtherInfoReadOnly);
            }
            else
            {
                lblOtherStatus.Text = "Invalid member account.";
            }
        }

        protected void cmdOtherInfoEdit_Click(object sender, EventArgs e)
        {
            MultiViewOtherInfo.SetActiveView(viewOtherInfoEdit);
        }
    }
}