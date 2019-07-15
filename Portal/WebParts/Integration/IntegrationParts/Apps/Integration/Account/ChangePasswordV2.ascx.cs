using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.WebParts.Central.Controls;

namespace WCMS.WebSystem.WebParts.Profile
{
    public partial class ChangePasswordV2 : System.Web.UI.UserControl
    {
        /*
         * PARAMETERS:
         * - UpdateNotSupportedNote
         * - EmailDomainFilter
         * - BeforeUpdateNote
         * - BeforeUpdateWithEmailNote
         * - SuccessUpdateNote
         * - SuccessUpdateWithEmailNote
         * - UpdateEmailPassword
         */

        protected ChangePasswordForm ChangePasswordForm1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.Element;
                var user = WSession.Current.User;
                var isExternalAccount = user.ProviderId == AccountConstants.DefaultExternalProvider;

                if (isExternalAccount)
                {
                    // Update not supported note

                    var cannotUpdateNote = element.GetParameterValue("UpdateNotSupportedNote");
                    if (!string.IsNullOrEmpty(cannotUpdateNote))
                        panelPageNote.InnerHtml = cannotUpdateNote;
                    else
                        panelPageNote.InnerHtml = "Please update your ONE Password via the ONE website.";

                    MultiView1.SetActiveView(viewDisplayNote);
                }
                else
                {
                    var updateGlobalPwd = DataHelper.GetBool(element.GetParameterValue("UpdateEmailPassword"), false);
                    var emailDomainFilter = element.GetParameterValue("EmailDomainFilter");
                    var emailFilterPassed = string.IsNullOrEmpty(emailDomainFilter) || user.Email.EndsWith(emailDomainFilter, StringComparison.InvariantCultureIgnoreCase);

                    string view = DataHelper.Get(Request, "View");
                    if (!string.IsNullOrEmpty(view) && view.Equals("Success", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var successUpdateNote = element.GetParameterValue("SuccessUpdateNote");
                        var successUpdateWithEmailNote = element.GetParameterValue("SuccessUpdateWithEmailNote");
                        if (updateGlobalPwd && !string.IsNullOrEmpty(successUpdateWithEmailNote) && (!string.IsNullOrEmpty(emailDomainFilter) && emailFilterPassed))
                            successUpdateNote = successUpdateWithEmailNote;

                        if (!string.IsNullOrEmpty(successUpdateNote))
                            panelPageNote.InnerHtml = successUpdateNote;

                        MultiView1.SetActiveView(viewDisplayNote);

                        //var homeUrl = element.GetParameterValue("HomeUrl");
                        //if (!string.IsNullOrEmpty(homeUrl))
                        //    linkHome.HRef = homeUrl;
                    }
                    else
                    {
                        #region Before Update Note

                        var beforeUpdateNote = element.GetParameterValue("BeforeUpdateNote");
                        var beforeUpdateWithEmailNote = element.GetParameterValue("BeforeUpdateWithEmailNote");
                        if (updateGlobalPwd && !string.IsNullOrEmpty(beforeUpdateWithEmailNote) && (!string.IsNullOrEmpty(emailDomainFilter) && emailFilterPassed))
                            beforeUpdateNote = beforeUpdateWithEmailNote;

                        if (!string.IsNullOrEmpty(beforeUpdateNote))
                            panelUpdateNote.InnerHtml = beforeUpdateNote;

                        // lblAfterNotApplicable.Visible = !emailFilterPassed;
                        // lblBeforeNotApplicable.Visible = !emailFilterPassed;

                        #endregion

                        ChangePasswordForm1.EnableAdminOldPwdExemption = false;
                        ChangePasswordForm1.SuccessMessage = "Your password was updated successfully!";

                        ChangePasswordForm1.LoadData(user);

                        hUserId.Value = user.Id.ToString();
                        hEmailDomainFilter.Value = emailDomainFilter;
                    }
                }
            }
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            var validationMsg = ChangePasswordForm1.ValidateNewPassword();
            if (string.IsNullOrEmpty(validationMsg))
            {
                var userId = DataUtil.GetId(hUserId.Value);

                WebUser user = null;
                if (userId > 0 && (user = WebUser.Get(userId)) != null)
                {
                    // Update Account Password
                    validationMsg = ChangePasswordForm1.UpdateData();
                    if (string.IsNullOrEmpty(validationMsg))
                    {
                        bool extEmailPwdUpdated = true;

                        var context = new WContext(this);
                        var element = context.Element;
                        var emailDomainFilter = element.GetParameterValue("EmailDomainFilter");
                        var updateGlobalPwd = DataHelper.GetBool(element.GetParameterValue("UpdateEmailPassword"), false);

                        // Update Global Password
                        if (updateGlobalPwd && !string.IsNullOrEmpty(emailDomainFilter) && user.Email.EndsWith(emailDomainFilter, StringComparison.InvariantCultureIgnoreCase))
                        {
                            validationMsg = MemberHelper.UpdateGlobalPassword(user, ChangePasswordForm1.NewPassword);
                            if (!string.IsNullOrEmpty(validationMsg))
                                extEmailPwdUpdated = false;
                        }

                        if (extEmailPwdUpdated)
                        {
                            string returnUrl = context.Get(QueryParser.SourceKey);
                            if (!string.IsNullOrEmpty(returnUrl))
                            {
                                returnUrl = HttpUtility.UrlDecode(returnUrl);
                                WQuery.StaticBaseRedirect(returnUrl);
                            }
                            else
                            {
                                context.Set("View", "Success");
                                context.Redirect();
                            }

                            return;
                        }
                        else
                            sb.AppendFormat(" Failed updating your e-mail password: {0}.", validationMsg);
                    }
                    else
                        sb.AppendFormat("Failed updating your account password: {0}.", validationMsg);
                }
            }
            else
                sb.Append(validationMsg);

            if (sb.Length == 0)
                sb.Append("Failed updating one of your passwords. Please contact the ADDCIT team for assistance.");

            lblError.InnerHtml = sb.ToString();
            panelError.Visible = true;
        }
    }
}