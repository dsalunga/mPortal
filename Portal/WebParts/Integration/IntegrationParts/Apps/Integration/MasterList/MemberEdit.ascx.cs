using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

using WCMS.Framework.Core.Shared;
using WCMS.Framework.Diagnostics;

using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Controls;

namespace WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList
{
    public partial class MemberEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetData();
            }
        }

        private void SetData()
        {
            string photoUrl = WConstants.NoPhotoThumb;

            var context = new WContext(this);
            var element = context.Element;
            var set = context.GetParameterSet();

            WebGroup group = null;
            var groupId = context.GetId(WebColumns.GroupId);
            if (groupId > 0)
            {
                group = WebGroup.Get(groupId);
            }
            else
            {
                var defaultParentName = ParameterizedWebObject.GetValue("ParentGroup", element, set);
                if (!string.IsNullOrEmpty(defaultParentName))
                {
                    var root = WebGroup.SelectNode(defaultParentName);
                    if (root != null)
                    {
                        group = root;
                        WebGroupTab1.RootGroupId = root.Id;
                    }
                }
            }

            hGroupId.Value = group.Id.ToString();


            var userId = context.GetId(WebColumns.UserId);
            WebUser user = userId > 0 ? WebUser.Get(userId) : null;
            if (user != null)
            {
                var isDraft = user.Status == AccountStatus.DRAFT;

                txtFirstName.Text = user.FirstName;
                txtMiddleName.Text = user.MiddleName;
                txtLastName.Text = user.LastName;
                txtEmail.Text = user.Email;

                var isoCountryCode = element.GetParameterValue("ISOCountryCode", "SG");
                var phoneCountry = Country.Provider.GetByISOCode(isoCountryCode);

                var addresses = user.Addresses;
                var homeAddress = addresses.FirstOrDefault(i => i.Tag.Equals(AddressTags.Home, StringComparison.InvariantCultureIgnoreCase));
                if (homeAddress != null)
                {
                    // Home Address
                    txtHomeAddress1.Text = homeAddress.AddressLine1;
                    txtHomeAddress2.Text = homeAddress.AddressLine2;
                    cboHomeAddressCountry.SelectedValue = homeAddress.CountryCode.ToString();
                    txtHomeAddressZipCode.Text = homeAddress.ZipCode;
                    txtHomePhone.Text = homeAddress.PhoneNumber;
                }
                else
                {
                    txtHomePhone.Text = user.TelephoneNumber;
                }

                txtMobileNumber.Text = user.MobileNumber;
                //memberPhoto.Src = link.GetPhotoPathIfNull();

                if (!isDraft)
                {
                    txtExternalID.ReadOnly = true;
                    txtMembershipDate.ReadOnly = true;
                    txtFirstName.ReadOnly = true;
                    txtLastName.ReadOnly = true;
                    txtMiddleName.ReadOnly = true;

                    txtMembershipDateCalendarExtender.Enabled = false;

                    txtEmail.ReadOnly = true;
                    txtMobileNumber.ReadOnly = true;
                    txtHomePhone.ReadOnly = true;
                    txtHomeAddress1.ReadOnly = true;
                    txtHomeAddress2.ReadOnly = true;
                    txtHomeAddressZipCode.ReadOnly = true;
                    cboHomeAddressCountry.Enabled = false;
                }

                var link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null)
                {
                    txtExternalID.Text = link.ExternalIdNo;
                    txtMembershipDate.Text = link.MembershipDate.ToString("yyyy-MM-dd");

                    if (!isDraft)
                        photoUrl = link.GetPhotoPathIfNull();
                }
                else if (!isDraft)
                {
                    photoUrl = user.GetPhotoPath();
                }

                // Position & Voice Designation
                var position = user.GetParameterValue(MasterListConstants.MEMBER_POSITION_KEY);
                if (!string.IsNullOrEmpty(position))
                {
                    WebHelper.SetCboValue(cboOfficerPosition, position);
                    chkIsOfficer.Checked = true;
                }

                var voiceDesignation = user.GetParameterValue(MasterListConstants.MEMBER_VOICE_DESIGNATION_KEY);
                if (!string.IsNullOrEmpty(voiceDesignation))
                    WebHelper.SetCboValue(cboVoiceDesignation, voiceDesignation);

                if (group != null)
                {
                    if (group.OwnerId > 0 && group.OwnerId == user.Id)
                        chkAssignedCouncillor.Checked = true;

                    chkGroupManager.Checked = AccountHelper.IsPresentOrMember(user.Id, group.Managers);

                    // Mentors
                    var mentors = group.GetParameterValue(MasterListConstants.GROUP_MENTORS_KEY);
                    if (!string.IsNullOrEmpty(mentors) && AccountHelper.IsPresentOrMember(user.Id, mentors))
                        chkGroupMentor.Checked = true;

                    // Conductors
                    var conductors = group.GetParameterValue(MasterListConstants.GROUP_CONDUCTORS_KEY);
                    if (!string.IsNullOrEmpty(conductors) && AccountHelper.IsPresentOrMember(user.Id, conductors))
                        chkGroupConductor.Checked = true;
                }
                else
                {
                    cmdSubmit.Enabled = false;
                }

                //else
                //{
                //    lblStatus.Text = "No member record found. Please link your user account to your member account. Profile Update cannot continue.";
                //}

                WebGroupTab1.Text = "Edit Member";
            }
            else
            {
                WebGroupTab1.Text = "New Member";

                //lblStatus.Text = "No member record found. Something wrong with your request.";
            }

            memberPhoto.Src = photoUrl;

            context.SetOpen(MasterListConstants.OPEN_KEY_MEMBERS);
            context.Remove(WebColumns.UserId);
            linkCancel.HRef = context.BuildQuery();

            // Configure Cancel
            //ConfigureCancel();
        }

        //private void ConfigureCancel()
        //{
        //    WContext context = new WContext(this);
        //    string cancelRedirect = context.Element.GetParameterValue(MemberConstants.CancelRedirect);
        //    if (!string.IsNullOrEmpty(cancelRedirect))
        //        linkCancel.Visible = true;
        //}

        public IEnumerable<Country> GetCountries()
        {
            return Country.GetList();
        }

        public IEnumerable<CountryState> GetUSStates()
        {
            return CountryState.GetList();
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            var groupId = DataUtil.GetId(hGroupId.Value);

            var context = new WContext(this);
            var externalId = txtExternalID.Text.Trim();
            var email = txtEmail.Text.Trim();

            var userId = context.GetId(WebColumns.UserId);
            WebUser user = userId > 0 ? WebUser.Get(userId) : null;
            if (user == null)
            {
                if (WebUser.GetByEmail(email) != null)
                {
                    // Error
                    lblStatus.Text = "Email already taken. Please try adding the existing account instead of creating a new one.";
                    return;
                }

                if (MemberLink.Provider.Get(externalId) != null)
                {
                    // Error
                    lblStatus.Text = "Group ID already taken. Please try adding the existing account instead of creating a new one.";
                    return;
                }

                user = new WebUser();
                user.UserName = externalId;
                user.Status = AccountStatus.DRAFT;
            }

            if (user.Id == -1 || user.Status == AccountStatus.DRAFT)
            {
                user.Email = email;
                user.TelephoneNumber = txtHomePhone.Text.Trim();
                user.MobileNumber = txtMobileNumber.Text.Trim();

                user.FirstName = txtFirstName.Text.Trim();
                user.MiddleName = txtMiddleName.Text.Trim();
                user.LastName = txtLastName.Text.Trim();
                user.Update(true);

                user.AddToGroup(groupId);
            }

            if (user.Status == AccountStatus.DRAFT)
            {
                var addresses = user.Addresses;
                var homeAddress = addresses.FirstOrDefault(i => i.Tag.Equals(AddressTags.Home, StringComparison.InvariantCultureIgnoreCase));
                if (homeAddress == null)
                {
                    homeAddress = new WebAddress();
                    homeAddress.Tag = AddressTags.Home;
                    homeAddress.ObjectId = WebObjects.WebUser;
                    homeAddress.RecordId = user.Id;
                }

                homeAddress.AddressLine1 = txtHomeAddress1.Text.Trim();
                homeAddress.AddressLine2 = txtHomeAddress2.Text.Trim();
                homeAddress.CountryCode = DataUtil.GetId(cboHomeAddressCountry.SelectedValue);
                homeAddress.ZipCode = txtHomeAddressZipCode.Text.Trim();
                homeAddress.PhoneNumber = txtHomePhone.Text.Trim();
                homeAddress.Update();

                MemberLink link = MemberLink.Provider.GetByUserId(user.Id);
                if (link == null)
                {
                    link = new MemberLink();
                    link.UserId = user.Id;
                    link.IsApproved = true;
                    link.IsPrivate = true;
                    link.ExternalIdNo = externalId;
                    link.Update(true);
                }
            }

            var voiceDesignationParam = user.GetOrCreateParameter(MasterListConstants.MEMBER_VOICE_DESIGNATION_KEY);
            voiceDesignationParam.Value = cboVoiceDesignation.SelectedValue;
            voiceDesignationParam.Update();

            var positionParam = user.GetOrCreateParameter(MasterListConstants.MEMBER_POSITION_KEY);
            positionParam.Value = cboOfficerPosition.SelectedValue;
            positionParam.Update();


            WebGroup group = null;
            if (groupId > 0 && (group = WebGroup.Get(groupId)) != null)
            {
                bool addManagerAccess = false;

                // Assigned Councillor - ADD/REMOVE
                if (chkAssignedCouncillor.Checked)
                {
                    group.OwnerId = user.Id;
                    addManagerAccess = true;
                }
                else if (group.OwnerId == user.Id && !chkAssignedCouncillor.Checked)
                {
                    group.OwnerId = -1;
                }

                // Manager - ADD/REMOVE
                if (AccountHelper.IsPresentOrMember(user.Id, group.Managers) && !chkGroupManager.Checked){
                    group.Managers = AccountHelper.RemoveAccount(group.Managers, user);
                }
                else if (!AccountHelper.IsPresentOrMember(user.Id, group.Managers) && chkGroupManager.Checked)
                {
                    group.Managers = AccountHelper.AddAccount(group.Managers, user);
                    addManagerAccess = true;
                }

                group.Update();

                if (addManagerAccess)
                {
                    var managerGroupName = ParameterizedWebObject.GetValue("ManagerGroup", context.Element, context.GetParameterSet());
                    WebGroup managerGroup = string.IsNullOrEmpty(managerGroupName) ? null : WebGroup.SelectNode(managerGroupName);
                    if (managerGroup != null) {
                        managerGroup.AddUser(user.Id);
                    }
                }

                // Mentors
                var mentorParam = group.GetOrCreateParameter(MasterListConstants.GROUP_MENTORS_KEY);
                if (chkGroupMentor.Checked && (mentorParam.Id == -1 || !AccountHelper.IsPresentOrMember(user.Id, mentorParam.Value)))
                {
                    mentorParam.Value = AccountHelper.AddAccount(mentorParam.Value, user);
                    mentorParam.Update();
                }
                else if (!chkGroupMentor.Checked && (mentorParam.Id > 0 && AccountHelper.IsPresentOrMember(user.Id, mentorParam.Value)))
                {
                    mentorParam.Value = AccountHelper.RemoveAccount(mentorParam.Value, user);
                    mentorParam.Update();
                }

                // Conductor
                var conductorParam = group.GetOrCreateParameter(MasterListConstants.GROUP_CONDUCTORS_KEY);
                if (chkGroupMentor.Checked && (conductorParam.Id == -1 || !AccountHelper.IsPresentOrMember(user.Id, conductorParam.Value)))
                {
                    conductorParam.Value = AccountHelper.AddAccount(conductorParam.Value, user);
                    conductorParam.Update();
                }
                else if (!chkGroupMentor.Checked && (conductorParam.Id > 0 && AccountHelper.IsPresentOrMember(user.Id, conductorParam.Value)))
                {
                    conductorParam.Value = AccountHelper.RemoveAccount(conductorParam.Value, user);
                    conductorParam.Update();
                }
            }

            context.Remove(WebColumns.UserId);
            context.SetOpen(MasterListConstants.OPEN_KEY_MEMBERS);
            context.Redirect();
        }

        //protected void cmdCancel_Click(object sender, EventArgs e)
        //{
        //    WContext context = new WContext(this);
        //    string cancelRedirect = context.Element.GetParameterValue(MemberConstants.CancelRedirect);

        //    if (!string.IsNullOrEmpty(cancelRedirect))
        //        context.Redirect(cancelRedirect);
        //}

        //protected void cboHomeAddressCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var countryCode = DataHelper.GetInt32(cboHomeAddressCountry.SelectedValue);
        //    if (countryCode > 0)
        //    {
        //        var country = Country.Get(countryCode);
        //        if (country != null)
        //        {
        //            homePhone.CountryCode = country.DialingCode;
        //            mobileNumber.CountryCode = country.DialingCode;
        //        }
        //    }
        //}
    }
}