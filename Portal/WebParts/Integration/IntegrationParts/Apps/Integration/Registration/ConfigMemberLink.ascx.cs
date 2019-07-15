using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Net;
using WCMS.Framework.Utilities;

using WCMS.Framework.Core.Shared;

using WCMS.WebSystem.Agent;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;

namespace WCMS.WebSystem.WebParts.Profile
{
    public partial class ConfigMemberLink : System.Web.UI.UserControl
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
            var context = new WContext(this);
            int id = context.GetId(WebColumns.Id);

            // Setup Links
            linkRefresh.HRef = context.BuildQuery();

            context.Set(WConstants.Load, "ConfigMemberLinkEdit.ascx");
            linkEdit.HRef = context.BuildQuery();

            context.Remove(WConstants.Load);
            context.Remove(WebColumns.Id);
            linkClose.HRef = context.BuildQuery();

            var link = MemberLink.Provider.Get(id);
            if (link != null)
            {
                lblDateOfMembership.InnerHtml = link.MembershipDate.ToString("dd-MMM-yyyy");

                if (0 >= link.MemberId)
                {
                    lblExternalIdNo.Visible = false;
                    panelLinkExternal.Visible = true;
                    txtExternalID.Text = link.ExternalIdNo;
                }
                else
                {
                    lblExternalIdNo.InnerHtml = link.ExternalIdNo;
                }

                var user = (link.UserId > 0 ? WebUser.Get(link.UserId) : WSession.Current.User);
                if (user != null)
                {
                    var addresses = user.Addresses;
                    var homeAddress = addresses.FirstOrDefault(i => i.Tag.Equals(AddressTags.Home, StringComparison.InvariantCultureIgnoreCase));
                    var workAddress = addresses.FirstOrDefault(i => i.Tag.Equals(AddressTags.Work, StringComparison.InvariantCultureIgnoreCase));

                    #region Start of Personal Information

                    if (homeAddress != null)
                    {
                        txtHomeAddress1.InnerHtml = homeAddress.AddressLine1;

                        if (!string.IsNullOrEmpty(homeAddress.AddressLine2))
                            txtHomeAddress2.InnerHtml = homeAddress.AddressLine2;

                        txtHomeAddressZipCode.InnerHtml = homeAddress.ZipCode;

                        // Home US State
                        var homeState = homeAddress.StateProvinceString;
                        if (!string.IsNullOrEmpty(homeState))
                        {
                            divHomeState.Visible = true;
                            txtHomeAddressState.InnerHtml = homeState;
                        }

                        var homeCountry = homeAddress.CountryString;
                        if (!string.IsNullOrEmpty(homeCountry))
                            txtHomeAddressCountry.InnerHtml = homeCountry;

                        if (!string.IsNullOrEmpty(homeAddress.PhoneNumber))
                            txtHomePhone.InnerHtml = homeAddress.PhoneNumber;
                    }

                    if (!string.IsNullOrEmpty(user.TelephoneNumber))
                        txtHomePhone.InnerHtml = user.TelephoneNumber;

                    #endregion

                    #region Start of Work Information

                    if (workAddress != null)
                    {
                        txtWorkAddress1.InnerHtml = workAddress.AddressLine1;

                        if (!string.IsNullOrEmpty(workAddress.AddressLine2))
                            txtWorkAddress2.InnerHtml = workAddress.AddressLine2;

                        txtWorkAddressZipCode.InnerHtml = workAddress.ZipCode;

                        // Work US State
                        var workState = workAddress.StateProvinceString;
                        if (!string.IsNullOrEmpty(workState))
                        {
                            divWorkState.Visible = true;
                            txtWorkAddressState.InnerHtml = workState;
                        }

                        var workCountry = workAddress.CountryString;
                        if (!string.IsNullOrEmpty(workCountry))
                            txtWorkAddressCountry.InnerHtml = workCountry;

                        if (!string.IsNullOrEmpty(workAddress.PhoneNumber))
                            txtWorkPhone.InnerHtml = workAddress.PhoneNumber;
                    }

                    #endregion

                    txtMobile.InnerHtml = user.MobileNumber;
                }

                //txtMobile.InnerHtml = link.MobileNumber;
                txtWorkDesignation.InnerHtml = link.WorkDesignation;
                //txtWorkPhone.InnerHtml = link.WorkPhone;
                //lblNickname.InnerHtml = link.Nickname;

                lblLastUpdate.InnerHtml = link.LastUpdate.ToString("d MMMM yyyy h:mm tt");
                memberPhoto.Src = link.GetPhotoPathIfNull();

                // Integration Portal Status
                lblApproved.InnerHtml = link.Approved == MemberAccountStatus.Approved ? MemberAccountStatus.ApprovedString : MemberAccountStatus.PendingString;
                panelIntegrationApproval.Visible = link.Approved != MemberAccountStatus.Approved;

                if (user != null)
                {
                    lblUserName.InnerHtml = user.UserName;
                    lblFullName.InnerHtml = user.FirstName + " " + user.LastName;
                    lblLastUpdate.InnerHtml = user.LastUpdate.ToString("d MMMM yyyy h:mm tt");
                    lblEmailAddress.InnerHtml = user.Email;

                    if (!user.IsActive)
                    {
                        // CMS Account Status
                        lblCMSAccount.InnerHtml = WConstants.AccountInActive;
                        panelCMSApproval.Visible = true;
                        if (link.IsApproved)
                            chkAccountSendEmail.Enabled = true;

                        //if (string.IsNullOrWhiteSpace(user.ActivationKey))
                        //    lblCMSAccount.InnerHtml = "Partially Active";
                        //else
                        //{

                        //}
                    }
                    else
                    {
                        lblCMSAccount.InnerHtml = WConstants.AccountActive;
                    }

                    // Enable Integration approval email sending
                    if (string.IsNullOrWhiteSpace(user.ActivationKey))
                        chkPortalSendEmail.Enabled = true;

                    // Set Membership information

                    string ministriesPath = WebRegistry.SelectNodeValue(MemberConstants.MinistriesRegistryPath, MemberConstants.MinistriesGroupPath);
                    string specialGroupsPath = WebRegistry.SelectNodeValue(MemberConstants.SpecialGroupsRegPath, MemberConstants.SpecialGroupsPath);
                    string groupsPath = WebRegistry.SelectNodeValue(MemberConstants.LocaleGroupRegPath, MemberConstants.LocaleGroupPath);

                    var userGroups = WebUserGroup.GetByUserId(user.Id, -1); //WebGroup.GetByUserId(user.Id, -1);
                    WebGroup g = null;

                    // Set Ministries
                    var ministries = WebGroup.SelectNode(ministriesPath).Children;
                    cblMinistries.DataSource = from ug in userGroups
                                               where ministries.FirstOrDefault(m => (g = m).Id == ug.GroupId) != null
                                               select new
                                               {
                                                   Id = ug.GroupId,
                                                   Name = ug.IsActive ? g.Name : g.Name + MemberConstants.PendingApprovalString
                                               };
                    cblMinistries.DataBind();

                    // Set Special Groups
                    var specialGroups = WebGroup.SelectNode(specialGroupsPath).Children;
                    cblSpecialGroups.DataSource = from ug in userGroups
                                                  where specialGroups.FirstOrDefault(m => (g = m).Id == ug.GroupId) != null
                                                  select new
                                                  {
                                                      Id = ug.GroupId,
                                                      Name = ug.IsActive ? g.Name : g.Name + MemberConstants.PendingApprovalString
                                                  };
                    cblSpecialGroups.DataBind();

                    // Set Group
                    var groups = WebGroup.SelectNode(groupsPath).Children;
                    rblLocaleGroups.DataSource = from ug in userGroups
                                                 where groups.FirstOrDefault(m => (g = m).Id == ug.GroupId) != null
                                                 select new
                                                     {
                                                         Id = ug.GroupId,
                                                         Name = ug.IsActive ? g.Name : g.Name + MemberConstants.PendingApprovalString
                                                     };
                    rblLocaleGroups.DataBind();

                    // Setup Account Link
                    context.Remove(WebColumns.PartAdminId);
                    context.Remove(WConstants.Load);
                    context.Remove(WebColumns.Id);
                    context.Set(WebColumns.UserId, user.Id);
                    linkAccount.HRef = context.Query.BuildQuery(CentralPages.WebUserHome);
                }
                else
                {
                    lblCMSAccount.InnerHtml = "Missing Account";
                    lblStatus.Text = "Could not locate CMS Account";
                }
            }
            else
            {
                lblStatus.Text = "No member record found. Please link your user account to your member account. Profile Update cannot continue.";
            }
        }

        private void ConfigureCancel()
        {

        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {

        }

        protected void cmdApprove_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            int id = context.GetId(WebColumns.Id);

            var link = MemberLink.Provider.Get(id);
            if (link != null)
            {
                link.Approved = MemberAccountStatus.Approved;
                link.Update();

                // Activate User Account
                var user = link.User;
                if (user != null)
                {
                    if (!user.IsActive && string.IsNullOrWhiteSpace(user.ActivationKey))
                    {
                        user.IsActive = true;
                        user.Update();
                    }

                    if (user.IsActive && chkPortalSendEmail.Checked)
                        SendApprovalNotification(context, link);

                    context.Redirect();
                }
                else
                {
                    lblStatus.Text = "CMS Account doesn't exist or already active.";
                }
            }
            else
            {
                lblStatus.Text = "Member already activated.";
            }
        }

        private bool SendApprovalNotification(WContext context, MemberLink link)
        {
            var user = link.User;
            var member = link.Member;

            if (user.IsActive)
            {
                var part = context.PartAdminId > 0 ? context.PartAdmin.Part : null;
                var paramSetName = part.GetParameterValue("Registration-ParameterSet");
                WebParameterSet paramSet = !string.IsNullOrEmpty(paramSetName) ? WebParameterSet.Get(paramSetName) : null;

                if (paramSet != null)
                {
                    string emailTemplate = paramSet.GetParameterValue("AccountApprovedAlert-Email");
                    string smsTemplate = paramSet.GetParameterValue("AccountApprovedAlert-SMS");
                    string approvalStatus = (link.Approved == MemberAccountStatus.Approved ? MemberAccountStatus.ApprovedString : MemberAccountStatus.RejectedString).ToUpper();
                    string loginUrl = paramSet.GetParameterValue("LoginUrl", "/public/Login/");
                    var country = link.LocaleCountry;
                    if (loginUrl.StartsWith("/"))
                        loginUrl = WebHelper.CombineAddress(WConfig.BaseAddress, loginUrl);

                    var provider = new NamedValueProvider();
                    provider.Add("CHURCH_ID_NO", link.ExternalIdNo);
                    provider.Add("FIRST_NAME", user.FirstName);
                    provider.Add("LAST_NAME", user.LastName);
                    provider.Add("MEMBERSHIP_DATE", link.MembershipDate.ToString("dd-MMM-yyyy"));
                    provider.Add("MOBILE", user.MobileNumber);
                    provider.Add("EMAIL", user.Email);
                    provider.Add("PHOTO_URL", link.GetPhotoPathIfNull("200x200"));
                    provider.Add("USER_NAME", user.UserName);
                    provider.Add("PASSWORD", user.Password);
                    provider.Add("APPROVAL_STATUS", approvalStatus);
                    provider.Add("LOCALE", link.Locale);
                    provider.Add("COUNTRY", country != null ? country.CountryName : "");
                    provider.Add("URL", loginUrl);

                    var emailContent = Substituter.Substitute(emailTemplate, provider);
                    var emailSubject = WebMailMessage.PrefixSubject("Your New Account is " + approvalStatus);
                    var smsContent = Substituter.Substitute(smsTemplate, provider);
                    var sendVia = DataUtil.GetInt32(paramSet.GetParameterValue("Send-Via", "2")); // Default is Both

                    var smtp = new WebMailMessage();
                    smtp.To.Add(user.Email);
                    smtp.SubjectAutoPrefix = "Your New Account is " + approvalStatus;
                    smtp.Body = emailContent;
                    smtp.AlwaysBcc = true;
                    smtp.Send();

                    var to = string.Format("{0};{1}", user.Email, user.MobileNumber);
                    var msg = WebMessageQueue.Create(emailContent, smsContent, sendVia, to, emailSubject, WSession.Current.User);
                    msg.Update();

                    try
                    {
                        AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(ex);
                        lblStatus.Text = "There was an error sending your message. However, it was placed on queue by the messaging server and will be sent on the next schedule.";
                        return false;
                    }

                    return true;
                }
                else
                {
                    lblStatus.Text = "Unable to get the parameter set.";
                }
            }
            else
            {
                lblStatus.Text = "E-mail not sent because user is not active.";
            }

            return false;
        }

        protected void cmdAccountApprove_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            int id = context.GetId(WebColumns.Id);

            var link = MemberLink.Provider.Get(id);
            if (link != null)
            {
                // Activate User Account
                var user = link.User;
                if (user != null)
                {
                    // Add user to groups
                    string groups = WebRegistry.SelectNodeValue(MemberConstants.GroupsToAddPath);
                    user.AddToGroups(groups);

                    if (link.IsApproved)
                    {
                        // Full activation
                        user.Activate();
                        if (chkAccountSendEmail.Checked)
                            if (!SendApprovalNotification(context, link))
                                return;
                    }
                    else
                    {
                        // Partial activate (account only)
                        user.IsActive = true;
                        user.Update();
                    }

                    context.Redirect();
                }
                else
                {
                    lblStatus.Text = "CMS Account doesn't exist or already active.";
                }
            }
            else
            {
                lblStatus.Text = "Member already activated.";
            }
        }

        protected void cmdLinkExternal_Click(object sender, EventArgs e)
        {
            string externalId = txtExternalID.Text.Trim();
            if (!string.IsNullOrWhiteSpace(externalId))
            {
                var context = new WContext(this);
                int id = context.GetId(WebColumns.Id);

                var link = MemberLink.Provider.Get(id);
                if (link != null)
                {
                    var client = new MemberSoapClient(false);
                    var item = client.GetProfile(externalId, link.MembershipDate);
                    if (item != null)
                    {
                        link.MemberId = (int)item.MemberID;
                        //link.PhotoPath = item.PhotoPath;
                        link.ExternalIdNo = item.EvalExternalId;

                        link.TryPopulateHomeAddressFromExt(client);
                        link.TryPopulateGroupsFromExt(client);
                        link.TryPopulateProfileFromExt(client);
                        link.TryStatusFromExt(client);
                        link.Update();

                        var photoPath = item.PhotoPath;
                        var user = link.User;
                        if (!string.IsNullOrEmpty(photoPath) && user != null)
                        {
                            user.PhotoPath = photoPath;
                            user.Update(true);
                        }

                        context.Redirect();
                        return;
                    }
                }
            }
            else
            {
                lblStatus.Text = "Please enter the Member's Group ID";
                return;
            }

            lblStatus.Text = "Unable to link to External.";
        }

        protected void cmdSync_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            int id = context.GetId(WebColumns.Id);
            var client = new MemberSoapClient(false);

            var item = MemberLink.Provider.Get(id);
            if (item != null)
            {
                item.TryPopulateGroupsFromExt(client);
                item.TryPopulateHomeAddressFromExt(client);
                item.TryPopulatePhotoFromExt(client);
                item.TryPopulateProfileFromExt(client);
                item.TryStatusFromExt(client);
                item.Update();

                lblStatus.Text = "Sync completed!";
                return;
            }

            lblStatus.Text = "Sync failed!";
        }
    }
}