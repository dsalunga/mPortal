using System;
using System.Collections.Generic;
using System.Linq;

using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

using WCMS.Framework.Core.Shared;
using WCMS.Framework.Diagnostics;

using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Controls;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile
{
    public partial class ProfileUpdateController : System.Web.UI.UserControl
    {
        //protected PhoneNumber mobileNumber;
        //protected PhoneNumber homePhone;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string key = DataUtil.Get(Request, "key");
                if (string.IsNullOrWhiteSpace(key))
                {
                    SetProfileInformation();
                }
                else
                {
                    // User is confirming new email address
                    var user = WSession.Current.User;
                    if (user != null && !string.IsNullOrWhiteSpace(user.ActivationKey))
                    {
                        if (key.Equals(user.ActivationKey, StringComparison.InvariantCultureIgnoreCase))
                        {
                            user.Email = user.NewEmail;
                            user.NewEmail = string.Empty;
                            user.ActivationKey = string.Empty;
                            user.Update();

                            MultiView1.SetActiveView(viewNewEmailConfirmed);
                            return;
                        }
                    }

                    MultiView1.SetActiveView(viewInvalidRequest);
                }
            }
        }

        private void SetProfileInformation()
        {
            Initialize();

            var user = WSession.Current.User;
            if (user != null)
            {
                var context = new WContext(this);
                var element = context.Element;

                lblLastUpdate.InnerHtml = user.LastUpdate.ToString("d MMMM yyyy h:mm tt");

                txtStatusText.Text = user.StatusText;

                txtFirstName.Text = user.FirstName;
                txtMiddleName.Text = user.MiddleName;
                txtLastName.Text = user.LastName;

                txtEmail.Text = user.Email;
                txtNewEmail.Text = user.NewEmail;

                var forcePrivate = DataUtil.GetBool(element.GetParameterValue("ForcePrivate"), false);
                if (forcePrivate)
                    panelPrivacy.Visible = false;

                var isoCountryCode = element.GetParameterValue("ISOCountryCode", "SG");
                var phoneCountry = Country.Provider.GetByISOCode(isoCountryCode);

                var link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null)
                {
                    // Setup notice
                    if (user.NoLastUpdate)
                    {
                        panelNotice.Visible = true;
                        hFirstLogin.Value = "1";
                    }
                    else
                    {
                        lblLastUpdate.InnerHtml = link.LastUpdate.ToString("d MMMM yyyy h:mm tt");
                        panelLastUpdate.Visible = true;
                    }

                    cmdSubmit.Enabled = true;

                    var addresses = user.Addresses;
                    var homeAddress = addresses.FirstOrDefault(i => i.Tag.Equals(AddressTags.Home, StringComparison.InvariantCultureIgnoreCase));
                    var workAddress = addresses.FirstOrDefault(i => i.Tag.Equals(AddressTags.Work, StringComparison.InvariantCultureIgnoreCase));

                    if (homeAddress != null)
                    {
                        // Home Address
                        txtHomeAddress1.Text = homeAddress.AddressLine1;
                        txtHomeAddress2.Text = homeAddress.AddressLine2;
                        cboHomeAddressState.SelectedValue = homeAddress.StateProvinceCode.ToString();
                        cboHomeAddressCountry.SelectedValue = homeAddress.CountryCode.ToString();
                        txtHomeAddressZipCode.Text = homeAddress.ZipCode;

                        //homePhone.Initialize(phoneCountry.DialingCode, homeAddress.PhoneNumber, phoneCountry.MaxPhoneDigit);
                        txtHomePhone.Text = !string.IsNullOrEmpty(homeAddress.PhoneNumber) ? homeAddress.PhoneNumber : user.TelephoneNumber;
                    }
                    else
                    {
                        txtHomePhone.Text = user.TelephoneNumber;
                        //homePhone.Initialize(phoneCountry.DialingCode, user.TelephoneNumber, phoneCountry.MaxPhoneDigit);
                    }

                    if (workAddress != null)
                    {
                        // Work Address
                        txtWorkAddress1.Text = workAddress.AddressLine1;
                        txtWorkAddress2.Text = workAddress.AddressLine2;
                        cboWorkAddressState.SelectedValue = workAddress.StateProvinceCode.ToString();
                        cboWorkAddressCountry.SelectedValue = workAddress.CountryCode.ToString();
                        txtWorkAddressZipCode.Text = workAddress.ZipCode;
                        txtWorkPhone.Text = workAddress.PhoneNumber;
                    }

                    //mobileNumber.Initialize(phoneCountry.DialingCode, user.MobileNumber, phoneCountry.MaxPhoneDigit);

                    txtMobileNumber.Text = user.MobileNumber;
                    txtWorkDesignation.Text = link.WorkDesignation;
                    txtNickname.Text = link.Nickname;

                    memberPhoto.Src = link.GetPhotoPathIfNull();
                    chkPrivate.Checked = link.IsPrivate;
                }
                else
                {
                    lblStatus.Text = "No member record found. Please link your user account to your member account. Profile Update cannot continue.";
                }
            }
            else
            {
                lblStatus.Text = "You are not logged in. Please login.";
            }

            // Configure Cancel
            ConfigureCancel();
        }

        private void Initialize()
        {
            //lblMobileNumber.Attributes["for"] = mobileNumber.NumberControl.ClientID;
            //lblHomePhone.Attributes["for"] = homePhone.NumberControl.ClientID;

            // Set default combo values
            if (cboHomeAddressState.Items.FindByValue("-1") != null)
                cboHomeAddressState.SelectedValue = "-1";

            if (cboWorkAddressState.Items.FindByValue("-1") != null)
                cboWorkAddressState.SelectedValue = "-1";
        }

        private void ConfigureCancel()
        {
            WContext context = new WContext(this);
            string cancelRedirect = context.Element.GetParameterValue(MemberConstants.CancelRedirect);
            if (!string.IsNullOrEmpty(cancelRedirect))
                cmdCancel.Visible = true;
        }

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
            var user = WSession.Current.User;
            if (user != null)
            {
                var link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null)
                {
                    bool newEmailConfirmSent = false;
                    var context = new WContext(this);
                    var element = context.Element;

                    var addresses = user.Addresses;

                    #region Home Address

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
                    homeAddress.StateProvinceCode = DataUtil.GetId(cboHomeAddressState.SelectedValue);
                    homeAddress.CountryCode = DataUtil.GetId(cboHomeAddressCountry.SelectedValue);
                    homeAddress.ZipCode = txtHomeAddressZipCode.Text.Trim();
                    homeAddress.PhoneNumber = txtHomePhone.Text.Trim(); //homePhone.GetCompleteNumber();
                    homeAddress.Update();

                    #endregion

                    user.TelephoneNumber = txtHomePhone.Text.Trim(); //homePhone.GetCompleteNumber(); // homeAddress.PhoneNumber;
                    user.MobileNumber = txtMobileNumber.Text.Trim(); // mobileNumber.GetCompleteNumber();

                    #region Work Address

                    var workAddress = addresses.FirstOrDefault(i => i.Tag.Equals(AddressTags.Work, StringComparison.InvariantCultureIgnoreCase));
                    if (workAddress == null)
                    {
                        workAddress = new WebAddress();
                        workAddress.Tag = AddressTags.Work;
                        workAddress.ObjectId = WebObjects.WebUser;
                        workAddress.RecordId = user.Id;
                    }

                    workAddress.AddressLine1 = txtWorkAddress1.Text.Trim();
                    workAddress.AddressLine2 = txtWorkAddress2.Text.Trim();
                    workAddress.StateProvinceCode = DataUtil.GetId(cboWorkAddressState.SelectedValue);
                    workAddress.CountryCode = DataUtil.GetId(cboWorkAddressCountry.SelectedValue);
                    workAddress.ZipCode = txtWorkAddressZipCode.Text.Trim();
                    workAddress.PhoneNumber = txtWorkPhone.Text.Trim();
                    workAddress.Update();

                    #endregion

                    link.Nickname = txtNickname.Text.Trim();
                    link.WorkDesignation = txtWorkDesignation.Text.Trim();
                    link.Private = chkPrivate.Checked ? 1 : 0;
                    link.Update(true);

                    //if (user.NoLastUpdate)
                    user.FirstName = txtFirstName.Text.Trim();
                    user.MiddleName = txtMiddleName.Text.Trim();
                    user.LastName = txtLastName.Text.Trim();

                    user.StatusText = txtStatusText.Text.Trim();

                    #region Update Email and Send Confirmation

                    string newEmail = txtNewEmail.Text.Trim();
                    if (!string.IsNullOrEmpty(newEmail) && !user.Email.Equals(newEmail))
                    {
                        user.NewEmail = newEmail;
                        if (string.IsNullOrWhiteSpace(user.ActivationKey))
                            user.RenewActivationKey();

                        // Send email here
                        // Send Approval Email
                        string content = FileHelper.ReadFile(MapPath("~/Content/Parts/Profile/Template/ConfirmNewEmail.htm"));

                        string confirmLink = context.BasePath; //context.PageElement.GetParameterValue(MemberConstants.ConfirmNewEmailUrlKey);
                        if (confirmLink.StartsWith("/"))
                            confirmLink = WebUtil.CombineAddress(WConfig.BaseAddress, confirmLink);

                        if (!string.IsNullOrEmpty(confirmLink))
                        {
                            var query = new WQuery(confirmLink);
                            query.Set("key", user.ActivationKey);

                            confirmLink = query.BuildQuery();
                        }

                        var values = new NamedValueProvider();
                        values.Add("RequesterName", string.IsNullOrWhiteSpace(user.FirstName) ? user.FullName : user.FirstName);
                        values.Add("OldEmail", user.Email);
                        values.Add("NewEmail", newEmail);
                        values.Add("ConfirmLink", confirmLink);

                        content = Substituter.Substitute(content, values);

                        var smtp = new CmsEmail();
                        smtp.MailTo.Add(newEmail);
                        smtp.SubjectAutoPrefix = "Confirm your new email address - " + newEmail;
                        smtp.Message = content;
                        smtp.SendMail();

                        newEmailConfirmSent = true;
                    }

                    #endregion

                    user.Update(true);

                    #region Audit/Wall Update Activity

                    try
                    {
                        if (WConfig.EnableLogging)
                        {
                            var log = new EventLog();
                            log.UserId = user.Id;
                            log.EventName = MemberConstants.ProfileUpdateEvent;
                            log.EventDate = DateTime.Now;
                            log.IPAddress = WHelper.GetUserHostAddress();
                            log.Update();
                        }

                        // if wall is enabled
                        ProfileUpdateEvent update = new ProfileUpdateEvent(user.Id);
                        update.Update();
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(ex);
                    }

                    #endregion Audit Update Activity

                    #region Post-Update routines (Includes Redirection and Status Messages)

                    if (hFirstLogin.Value == "1")
                    {
                        string firstUpdateRedirect = element.GetParameterValue(MemberConstants.FirstUpdateRedirect);
                        if (!string.IsNullOrEmpty(firstUpdateRedirect))
                        {
                            context.Redirect(firstUpdateRedirect);
                            return;
                        }
                    }

                    string updateRedirect = element.GetParameterValue(MemberConstants.UpdateRedirect);
                    if (!string.IsNullOrEmpty(updateRedirect))
                    {
                        context.Redirect(updateRedirect);
                    }
                    else
                    {
                        lblLastUpdate.InnerHtml = link.LastUpdate.ToString("d MMMM yyyy h:mm tt");

                        lblStatus.Text = "Profile Update successful.";
                        if (newEmailConfirmSent)
                            lblStatus.Text += " A verification email has been sent to your new email address. You must confirm your new email address before you can start using it by clicking the link in the verification email.";

                        if (panelNotice.Visible)
                        {
                            panelNotice.Visible = false;
                            hFirstLogin.Value = "0";
                            panelLastUpdate.Visible = true;
                        }
                    }

                    #endregion
                }
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var element = context.Element;
            string cancelRedirect = element.GetParameterValue(MemberConstants.CancelRedirect);
            string updateRedirect = element.GetParameterValue(MemberConstants.UpdateRedirect);

            if (string.IsNullOrEmpty(cancelRedirect) && !string.IsNullOrEmpty(updateRedirect))
                cancelRedirect = updateRedirect;

            if (!string.IsNullOrEmpty(cancelRedirect))
                context.Redirect(cancelRedirect);
        }

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