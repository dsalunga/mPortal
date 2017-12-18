using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Framework;
using WCMS.Common.Utilities;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class ChangePasswordForm : System.Web.UI.UserControl
    {
        public ChangePasswordForm()
        {
            EnableAdminOldPwdExemption = true;

            ConfirmInvalidMessage = "Password confirmation does not match.";
            InvalidOldMessage = "Current password is incorrect.";
            SuccessMessage = "Password has been updated successfully!";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadData(WebUser user)
        {
            if (user != null)
            {
                this.hiddenUserName.Value = user.Id.ToString();

                if (user.IsPasswordExpired)
                    labelMsg.InnerHtml = "Your password has expired, please update it.";

                if (string.IsNullOrEmpty(user.Password))
                {
                    rfvPassword.Enabled = false;
                    rfvPassword.Visible = false;
                }

                var isAdmin = WSession.Current.IsAdministrator;
                if (isAdmin && EnableAdminOldPwdExemption)
                {
                    trPassword.Visible = false;
                    trSeparator.Visible = false;
                }

                if (!isAdmin || user.Id == WSession.Current.UserId)
                {
                    panelRequireNewPwd.Visible = false;
                }
                else if (isAdmin)
                {
                    if (!EnableAdminOldPwdExemption)
                        txtPassword.Attributes["value"] = user.Password;

                    txtNewPassword.Attributes["value"] = user.Password;
                    txtConfirmNewPassword.Attributes["value"] = user.Password;
                }
            }
            else
            {
                trPassword.Visible = false;
            }

            //MembershipUser user = Membership.GetUser(userName);
            //txtSecurityQuestion.Text = user.PasswordQuestion;
        }

        public string UpdateData(WebUser user)
        {
            if (!_isValidated)
                ValidateNewPassword();

            int userId = DataHelper.GetId(hiddenUserName.Value);

            if (user == null)
                user = WebUser.Get(userId);

            if (user != null)
            {
                bool toUpdate = false;
                if (trPassword.Visible)
                {
                    if (user.Password == Password)
                    {
                        string newPwd = NewPassword;
                        if (newPwd == ConfirmNewPassword)
                        {
                            toUpdate = true;
                            user.Password = newPwd;
                        }
                        else
                            return ConfirmInvalidMessage;
                    }
                    else
                        return InvalidOldMessage;
                }
                else
                {
                    string newPwd = NewPassword;
                    if (newPwd == NewPassword)
                    {
                        toUpdate = true;
                        user.Password = newPwd;
                    }
                    else
                        return ConfirmInvalidMessage;
                }

                if (toUpdate)
                {
                    if (WSession.Current.IsAdministrator && chkRequireNewPwd.Checked)
                        user.PasswordExpiryDate = WConstants.DateTimeMinValue.AddYears(100);
                    else
                        user.PasswordExpiryDate = WConstants.DateTimeMinValue;

                    user.Update();
                    return string.Empty;
                }
            }
            else
                return "Account invalid or not available.";

            // labelMsg.InnerHtml = SuccessMessage;

            return "Failed updating password.";

            //if (hiddenUserName.Value != string.Empty)
            //{
            //    MembershipUser user = Membership.GetUser(hiddenUserName.Value);
            //    user.ChangePassword(txtPassword.Text, txtNewPassword.Text);
            //    user.ChangePasswordQuestionAndAnswer(txtNewPassword.Text, txtSecurityQuestion.Text.Trim(), txtSecurityAnswer.Text);
            //}
            //else
            //{
            //    // Insert
            //}
        }

        public string UpdateData()
        {
            return UpdateData(null);
        }

        private bool _isValidated = false;
        public string ValidateNewPassword()
        {
            _isValidated = true;

            //if (txtNewPassword.Text.Length < Membership.MinRequiredPasswordLength) return "Minimum password length is " + Membership.MinRequiredPasswordLength + ".";
            //if (txtConfirmNewPassword.Text.Length < Membership.MinRequiredPasswordLength) return "Minimum password length is " + Membership.MinRequiredPasswordLength + ".";

            string pwd = txtPassword.Text;
            string newPwd = txtNewPassword.Text;
            string confirmNewPwd = txtConfirmNewPassword.Text;

            if (!Request.IsSecureConnection)
            {
                var items = LoginSecurity.Decode(new string[] { pwd, newPwd, confirmNewPwd });
                if (items.Count > 0)
                {
                    _password = items[0];
                    _newPassword = items[1];
                    _confirmNewPassword = items[2];
                }
                else
                {
                    return "There was a problem reading the new password.";
                }
            }
            else
            {
                _password = pwd;
                _newPassword = newPwd;
                _confirmNewPassword = confirmNewPwd;
            }

            if (_newPassword != _confirmNewPassword)
                return "New Password and Confirm New Password must match.";

            if (_newPassword.Contains("'") || _newPassword.Contains("\""))
                return "New Password must not contain single quotes (') or double quotes (\").";

            return string.Empty;
        }

        private string _password = null;
        public string Password
        {
            get
            {
                if (_password == null)
                {
                    if (!_isValidated)
                        ValidateNewPassword();

                    _password = txtPassword.Text;
                }

                return _password;
            }
        }

        private string _newPassword = null;
        public string NewPassword
        {
            get
            {
                if (_newPassword == null)
                {
                    if (!_isValidated)
                        ValidateNewPassword();

                    _newPassword = txtNewPassword.Text;
                }

                return _newPassword;
            }
        }

        private string _confirmNewPassword = null;
        public string ConfirmNewPassword
        {
            get
            {
                if (_confirmNewPassword == null)
                {
                    if (!_isValidated)
                        ValidateNewPassword();

                    _confirmNewPassword = txtConfirmNewPassword.Text;
                }

                return _confirmNewPassword;
            }
        }

        public bool EnableAdminOldPwdExemption
        {
            get
            {
                var p = ViewState["EnableAdminOldPwdExemption"];
                if (p != null)
                    return p.ToString() == "1";
                else
                    return false;
            }

            set { ViewState["EnableAdminOldPwdExemption"] = value ? "1" : "0"; }
        }

        public string SuccessMessage
        {
            get
            {
                var p = ViewState["SuccessMessage"];
                if (p != null)
                    return p.ToString();
                else
                    return string.Empty;
            }

            set { ViewState["SuccessMessage"] = value; }
        }

        public string InvalidOldMessage
        {
            get
            {
                var p = ViewState["InvalidOldMessage"];
                if (p != null)
                    return p.ToString();
                else
                    return string.Empty;
            }

            set { ViewState["InvalidOldMessage"] = value; }
        }

        public string ConfirmInvalidMessage
        {
            get
            {
                var p = ViewState["ConfirmInvalidMessage"];
                if (p != null)
                    return p.ToString();
                else
                    return string.Empty;
            }

            set { ViewState["ConfirmInvalidMessage"] = value; }
        }
    }
}