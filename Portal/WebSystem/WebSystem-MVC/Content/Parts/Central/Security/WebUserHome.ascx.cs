using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Security;

namespace WCMS.WebSystem.WebParts.Central.Security
{
    public partial class WebUserHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!WSession.Current.IsSiteManager)
                    WQuery.StaticRedirect(WConstants.AbsoluteAccessDeniedPage);

                var query = new WQuery(this);
                int id = query.GetId(WebColumns.UserId);
                WebUser user = id > 0 ? WebUser.Get(id) : null;

                if (user != null)
                {
                    if (!WSession.Current.IsAdministrator && (!WebGlobalPolicy.IsUserPermitted(GlobalPolicies.Administration, Permissions.UsersManagement) || WSession.Current.UserId == id))
                    {
                        rowProperties.Visible = false;
                        rowDelete.Visible = false;
                        rowParameters.Visible = false;
                        panelFixOrientation.Visible = false;
                        //rowGroups.Visible = false;
                    }
                    //else if (!isAdmin && user.IsAdministrator())
                    //{
                    //    QueryParser.StaticRedirect(WConstants.AbsoluteAccessDeniedPage);
                    //}
                    else
                    {
                        linkProperties.HRef = query.BuildQuery(CentralPages.UserProfile);
                    }

                    query.Set(WQuery.SourceKey, CentralPages.WebUserHome);
                    linkSecurity.HRef = query.BuildQuery(CentralPages.ChangePassword);
                    linkGroups.HRef = query.BuildQuery(CentralPages.WebUserGroups);

                    var q = new WQuery(query);
                    q.Set(ObjectKey.KeyString, (new ObjectKey(WebObjects.WebUser, id)).ToString());
                    q.Set(ObjectKey.KeySource, query.EncodedBasePath);

                    linkParameters.HRef = q.BuildQuery(CentralPages.WebParameters);

                    lblFullName.InnerHtml = user.FirstAndLastName;
                    lblEmail.InnerHtml = user.Email;
                    lblMobileNumber.InnerHtml = user.MobileNumber;
                    lblLastLogin.InnerHtml = user.LastLogin.ToString();
                    lblLastUpdated.InnerHtml = user.LastUpdate.ToString();
                    lblDateCreated.InnerHtml = user.DateCreated.ToString();
                    lblActive.InnerHtml = AccountStatus.GetText(user.Status);
                    lblPasswordExpiry.InnerHtml = user.IsPasswordExpired ? "Expired" : "Active (not expired)";
                    lblProvider.InnerHtml = user.ProviderId > 0 ? UserProvider.Provider.Get(user.ProviderId).Name : AccountConstants.DefaultProviderName;
                    accountPhoto.Src = user.GetPhotoPath(); //"200x200"
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            int id = query.GetId(WebColumns.UserId);
            if (id > 0)
            {
                WebUser.Delete(id);
                query.Remove(WebColumns.UserId);
                query.Redirect(CentralPages.WebUsers);
            }
        }

        protected void cmdFixOrientation_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            int id = query.GetId(WebColumns.UserId);
            if (id > 0)
            {
                var item = WebUser.Get(id);
                if (item.HasPhoto)
                {
                    //var photoPath = item.PhotoOriginalPath;
                    // TODO: Fix the original file, not the thumbnail

                    var photoPath = item.PhotoPath;
                    if (!WebHelper.IsAbsUrl(photoPath))
                    {
                        var absPhotoPath = MapPath(photoPath);
                        using (var image = ImageUtil.FixOrientation(absPhotoPath))
                            ImageUtil.SaveToFile(image, absPhotoPath, ImageUtil.GetImageFormat(absPhotoPath));
                        query.Redirect();
                        return;
                    }
                }
            }

            lblMessage.InnerHtml = "Photo file is not writable.";
            lblMessage.Visible = true;
        }
    }
}