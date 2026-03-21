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

namespace WCMS.WebSystem.Apps.Integration.Account
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
                    if (user != null)
                    {
                        var keys = user.GetActivationKeys();
                        var emails = user.GetNewEmails();
                        for (int i = 0; i < 2; i++)
                        {
                            var k = keys[i];
                            if (key.Equals(k, StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (i == 0)
                                    user.Email = emails[0];
                                else
                                    user.Email2 = emails[1];

                                keys[i] = string.Empty;
                                emails[i] = string.Empty;

                                user.SetNewEmails(emails);
                                user.SetActivationKeys(keys);
                                user.Update();

                                linkContinue.HRef = (new WContext(this)).Element.GetParameterValue(MemberConstants.UpdateRedirect);
                                MultiView1.SetActiveView(viewNewEmailConfirmed);
                                return;
                            }
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
                WebUtil.SetCboValue(cboGender, user.Gender);
                WebUtil.SetCboValue(cboMaritalStatus, user.MaritalStatusId);

                txtFirstName.Text = user.FirstName;
                txtMiddleName.Text = user.MiddleName;
                txtLastName.Text = user.LastName;

                var newEmails = user.GetNewEmails();
                var newEmail = newEmails.First();
                var newEmail2 = newEmails[1];
                if (!string.IsNullOrEmpty(newEmail))
                {
                    txtEmail.Text = newEmail;
                    panelEmailPending.Visible = true;
                    lblActiveEmail.InnerHtml = user.Email;
                }
                else
                {
                    txtEmail.Text = user.Email;
                }
                if (!string.IsNullOrEmpty(newEmail2))
                {
                    txtEmail2.Text = newEmail2;
                    panelEmailPending2.Visible = true;
                    lblActiveEmail2.InnerHtml = user.Email2;
                }
                else
                {
                    txtEmail2.Text = user.Email2;
                }


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
            WebUtil.SetCboValue(cboHomeAddressState, -1);
            WebUtil.SetCboValue(cboWorkAddressState, -1);
        }

        private void ConfigureCancel()
        {
            var context = new WContext(this);
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

                    // Probably temporary
                    link.LocaleCountryCode = homeAddress.CountryCode;

                    link.Nickname = txtNickname.Text.Trim();
                    link.WorkDesignation = txtWorkDesignation.Text.Trim();
                    link.Private = chkPrivate.Checked ? 1 : 0;
                    link.Update(true);

                    //if (user.NoLastUpdate)
                    user.FirstName = txtFirstName.Text.Trim();
                    user.MiddleName = txtMiddleName.Text.Trim();
                    user.LastName = txtLastName.Text.Trim();

                    user.StatusText = txtStatusText.Text.Trim();
                    user.Gender = DataUtil.GetChar(cboGender.SelectedValue);
                    user.MaritalStatusId = DataUtil.GetId(cboMaritalStatus.SelectedValue);

                    #region Update Email and Send Confirmation - spag code

                    var newEmails = user.GetNewEmails();
                    var keys = user.GetActivationKeys();
                    var newEmail = txtEmail.Text.Trim();
                    var newEmail2 = txtEmail2.Text.Trim();

                    var isNewEmail = !string.IsNullOrEmpty(newEmail) && !user.Email.Equals(newEmail, StringComparison.InvariantCultureIgnoreCase);
                    var isNewEmail2 = !string.IsNullOrEmpty(newEmail2) && !user.Email2.Equals(newEmail2, StringComparison.InvariantCultureIgnoreCase);
                    if (isNewEmail || isNewEmail2)
                    {
                        if (isNewEmail)
                        {
                            newEmails[0] = newEmail;
                            if (string.IsNullOrWhiteSpace(keys.First()))
                                keys[0] = user.RenewActivationKey();
                        }

                        if (isNewEmail2)
                        {
                            newEmails[1] = newEmail2;
                            if (string.IsNullOrWhiteSpace(keys[1]))
                                keys[1] = user.RenewActivationKey2();
                        }

                        // Send email here
                        // Send Approval Email
                        string content = FileHelper.ReadFile(MapPath("~/Content/Parts/Profile/Template/ConfirmNewEmail.htm"));
                        var requesterName = string.IsNullOrWhiteSpace(user.FirstName) ? user.FullName : user.FirstName;
                        var basePath = context.BasePath;
                        basePath = basePath.StartsWith("/") ? WebUtil.CombineAddress(context.Site.BuildAbsoluteUrl(), basePath) : basePath; //context.PageElement.GetParameterValue(MemberConstants.ConfirmNewEmailUrlKey);

                        string e1 = "", e2 = "", key = "";
                        for (int i = 0; i < 2; i++)
                        {
                            if (i == 0)
                            {
                                if (!isNewEmail)
                                    continue;

                                e1 = string.IsNullOrEmpty(user.Email) ? user.Email2 : user.Email;
                                e2 = newEmail;
                                key = keys[0];
                            }

                            if (i == 1)
                            {
                                if (!isNewEmail2)
                                    break;

                                e1 = string.IsNullOrEmpty(user.Email2) ? user.Email : user.Email2;
                                e2 = newEmail2;
                                key = keys[1];
                            }

                            var query = new WQuery(basePath);
                            query.Set("key", key);
                            var confirmLink = query.BuildQuery(true);

                            var values = new NamedValueProvider();
                            values.Add("RequesterName", requesterName);
                            values.Add("OldEmail", e1);
                            values.Add("NewEmail", e2);
                            values.Add("ConfirmLink", confirmLink);
                            var emailContent = Substituter.Substitute(content, values);

                            var smtp = new WebMailMessage();
                            smtp.To.Add(e2);
                            smtp.SubjectAutoPrefix = "Confirm your new email address - " + e2;
                            smtp.Body = emailContent;
                            smtp.Send();

                            newEmailConfirmSent = true;
                        }
                    }

                    if (!isNewEmail)
                    {
                        newEmails[0] = "";
                        keys[0] = "";

                        if (!string.IsNullOrEmpty(newEmail) && !newEmail.Equals(user.Email))
                            user.Email = newEmail; // Just a character case update
                    }

                    if (!isNewEmail2)
                    {
                        newEmails[1] = "";
                        keys[1] = "";

                        if (!string.IsNullOrEmpty(newEmail2) && !newEmail2.Equals(user.Email2))
                            user.Email2 = newEmail2; // Just a character case update
                    }

                    #endregion

                    user.SetActivationKeys(keys);
                    user.SetNewEmails(newEmails);
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
                        var update = new ProfileUpdateEvent(user.Id);
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