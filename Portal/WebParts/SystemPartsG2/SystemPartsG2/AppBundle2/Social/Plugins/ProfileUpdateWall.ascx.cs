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
using WCMS.Framework.Social;

using WCMS.WebSystem.WebParts.Social.ViewModel;

namespace WCMS.WebSystem.WebParts.Social.Plugins
{
    public partial class ProfileUpdateWall : WallEventViewModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void RenderUI()
        {
            //StringBuilder sb = new StringBuilder();

            var user = WebUser.Get(_update.ByRecordId);
            if (user != null)
            {
                var genderAddr = user.Gender == GenderTypes.Male ? "his" : user.Gender == GenderTypes.Female ? "her" : "his/her";

                //sb.AppendFormat("{0} updated {1} profile.", user.FirstAndLastName, genderAddr);


                string template = FileHelper.ReadFile(MapPath("~/Content/Parts/Social/Plugins/ProfileUpdateWall.template.htm"));

                var poster = WebUser.Get(_update.ByRecordId);
                var canDelete = UserIsContentManager || WSession.Current.UserId == _update.ByRecordId;

                NamedValueProvider values = new NamedValueProvider();
                values.Add("POSTER_USER_ID", _update.ByRecordId);
                values.Add("GENDER_ADDRESS", genderAddr);
                values.Add("POSTER_NAME", poster.FirstAndLastName);
                values.Add("DATE_POSTED", string.Format("{0}{1}", _update.UpdateDate.ToString("MMMM d a\\t h:mm"), _update.UpdateDate.ToString("tt").ToLower()));
                values.Add("POSTER_IMAGE_URL", poster.GetPhotoPath("64x64"));
                values.Add("POST_OBJECT_ID", SocialConstants.WallUpdate);
                values.Add("DELETE_PANEL", canDelete ? "<div class=\"postDeleteHandle\" style=\"float: right; padding: 5px\"><a class=\"postDeleteButton\"></a></div>" : string.Empty);
                values.Add("POST_ID", _update.Id);
                values.Add("POST_RECORD_ID", _update.Id);
                values.Add("COMMENTS_CONTROL", WebCommentHelper.PrepareCommentsUI(_update, UserIsContentManager));
                //values.Add("POSTER_IMAGE_URL", "POSTER_IMAGE_URL.jpg");

                output.Text = Substituter.Substitute(template, values);
            }
        }
    }
}
