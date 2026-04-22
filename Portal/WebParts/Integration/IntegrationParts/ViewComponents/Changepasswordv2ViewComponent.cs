using System;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from ChangePasswordV2.ascx (Apps/Integration/Account).
    /// User-facing change-password form with optional global (e-mail) password sync
    /// for accounts whose e-mail matches the configured domain filter. Renders one of
    /// three views (Update form, Success note, Update-not-supported note) based on the
    /// authenticated user's provider and the parent element's parameters.
    /// </summary>
    public class Changepasswordv2ViewComponent : WViewComponent
    {
        public Changepasswordv2ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new Changepasswordv2ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            var element = WcmsContext.Element;
            var session = WcmsSession;
            if (!session.IsLoggedIn || session.User == null)
            {
                model.View = Changepasswordv2View.Note;
                model.NoteHtml = "You must be logged in to change your password.";
                return View(model);
            }

            var user = session.User;
            model.UserId = user.Id;
            model.UserName = user.UserName;

            var isExternalAccount = user.ProviderId == AccountConstants.DefaultExternalProvider;
            if (isExternalAccount)
            {
                model.View = Changepasswordv2View.Note;
                model.NoteHtml = element?.GetParameterValue("UpdateNotSupportedNote")
                                  ?? "Please update your password via your identity provider.";
                return View(model);
            }

            var emailDomainFilter = element?.GetParameterValue("EmailDomainFilter");
            var updateGlobalPwd = DataUtil.GetBool(element?.GetParameterValue("UpdateEmailPassword"), false);
            var emailFilterPassed = string.IsNullOrEmpty(emailDomainFilter)
                || (user.Email != null && user.Email.EndsWith(emailDomainFilter, StringComparison.OrdinalIgnoreCase));
            model.UpdateGlobalPassword = updateGlobalPwd && emailFilterPassed && !string.IsNullOrEmpty(emailDomainFilter);
            model.EmailDomainFilter = emailDomainFilter;

            var view = WcmsContext.Get("View");
            if (!string.IsNullOrEmpty(view) && view.Equals("Success", StringComparison.OrdinalIgnoreCase))
            {
                var successNote = element?.GetParameterValue("SuccessUpdateNote");
                var successWithEmail = element?.GetParameterValue("SuccessUpdateWithEmailNote");
                if (model.UpdateGlobalPassword && !string.IsNullOrEmpty(successWithEmail))
                    successNote = successWithEmail;
                model.View = Changepasswordv2View.Note;
                model.NoteHtml = successNote ?? "Your password was updated successfully.";
                return View(model);
            }

            var beforeNote = element?.GetParameterValue("BeforeUpdateNote");
            var beforeWithEmail = element?.GetParameterValue("BeforeUpdateWithEmailNote");
            if (model.UpdateGlobalPassword && !string.IsNullOrEmpty(beforeWithEmail))
                beforeNote = beforeWithEmail;

            model.View = Changepasswordv2View.Update;
            model.BeforeUpdateNoteHtml = beforeNote ?? string.Empty;
            model.PostAction = "ChangePassword";
            model.PostController = "Account";
            return View(model);
        }
    }

    public enum Changepasswordv2View
    {
        Update = 0,
        Note = 1
    }

    public class Changepasswordv2ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public Changepasswordv2View View { get; set; } = Changepasswordv2View.Update;
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string EmailDomainFilter { get; set; } = string.Empty;
        public bool UpdateGlobalPassword { get; set; }
        public string BeforeUpdateNoteHtml { get; set; } = string.Empty;
        public string NoteHtml { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public string PostAction { get; set; } = "ChangePassword";
        public string PostController { get; set; } = "Account";
    }
}
