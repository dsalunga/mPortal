using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.WebParts.Common
{
    public partial class UserPhotoUpload : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdUpload_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var element = context.Element;
            var user = WSession.Current.User;

            var returnUrl = element.GetParameterValue("ReturnUrl", "/");
            //var photoPath = WConfig.UserPhotoPath; //site.GetParameterValue("WCMS.UserPhotoPath", "/Content/Assets/User-Photos");
            var photoSize = DataHelper.GetInt32(element.GetParameterValue("PhotoSize"), 600);

            if (!string.IsNullOrEmpty(returnUrl))
                hReturnUrl.Value = returnUrl;

            if (photoUpload.HasFile)
            {
                var fileName = photoUpload.PostedFile.FileName;
                if (ImageUtil.IsValidImage(fileName))
                {
                    hExtension.Value = Path.GetExtension(fileName);
                    imagePreview.ImageUrl = AccountHelper.UploadPhotoForPreview(user.Id, photoUpload, photoSize);
                    MultiView1.SetActiveView(viewPreview);
                }
                else
                {
                    lblMsg.InnerHtml = "The file you uploaded is not a valid image file.";
                }
            }
        }

        protected void cmdAccept_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var element = context.Element;
            var user = WSession.Current.User;

            //var photoPathUrl = WConfig.UserPhotoPath; //site.GetParameterValue("WCMS.UserPhotoPath", "/Content/Assets/User-Photos");
            var thumbSize = DataHelper.GetInt32(element.GetParameterValue("ThumbSize"), 200);
            var ext = hExtension.Value;

            AccountHelper.FinalizePhotoUpload(user, -1, ext, thumbSize);

            var returnUrl = hReturnUrl.Value;

            var query = new WQuery(returnUrl);
            query.Add(context.Query);
            query.Redirect();
        }

        protected void cmdReUpload_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            query.Redirect();
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        protected void cmdUploadCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void Cancel()
        {
            var context = new WContext(this);
            var returnUrl = context.Element.GetParameterValue("ReturnUrl", hReturnUrl.Value);

            var query = new WQuery(returnUrl);
            query.Add(context.Query);
            query.Redirect();
        }
    }
}