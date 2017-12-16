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
    public partial class GenericWallPost : WallEventViewModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void RenderUI()
        {
            string template = FileHelper.ReadFile(MapPath("~/Content/Parts/Social/Plugins/GenericWallPost.template.htm"));

            var poster = WebUser.Get(_update.ByRecordId);
            var canDelete = UserIsContentManager || WSession.Current.UserId == _update.ByRecordId;

            NamedValueProvider values = new NamedValueProvider();
            values.Add("POSTER_USER_ID", _update.ByRecordId);
            values.Add("POST_OBJECT_ID", SocialConstants.WallUpdate);
            values.Add("DELETE_PANEL", canDelete ? "<div class=\"postDeleteHandle\" style=\"float: right; padding: 5px\"><a class=\"postDeleteButton\"></a></div>" : string.Empty);
            values.Add("POST_ID", _update.Id);
            values.Add("POST_RECORD_ID", _update.Id);
            values.Add("POST_MESSAGE", _update.Content);
            values.Add("POSTER_NAME", poster.FirstAndLastName);
            values.Add("DATE_POSTED", string.Format("{0}{1}", _update.UpdateDate.ToString("MMMM d a\\t h:mm"), _update.UpdateDate.ToString("tt").ToLower()));
            values.Add("POSTER_IMAGE_URL", poster.GetPhotoPath("64x64"));
            values.Add("COMMENTS_CONTROL", PrepareCommentsUI());
            //values.Add("POSTER_IMAGE_URL", "POSTER_IMAGE_URL.jpg");

            output.Text = Substituter.Substitute(template, values);
        }

        private string PrepareCommentsUI()
        {
            NamedValueProvider values = null;
            var currentUserId = WSession.Current.UserId;
            
            var sb = new StringBuilder();
            var commentsTemplate = FileHelper.ReadFile(MapPath("~/Content/Parts/Social/Comments.template.htm"));
            var itemTemplate = FileHelper.ReadFile(MapPath("~/Content/Parts/Common/Templates/CommentItem.template.htm"));

            WebUser poster = null;

            // Build comment items
            var comments = WebComment.Provider.GetList(-2, SocialConstants.WallUpdate, _update.Id);
            if (comments.Count() > 0)
            {
                foreach (var comment in comments)
                {
                    poster = comment.User;
                    var canDelete = UserIsContentManager || currentUserId == comment.UserId;

                    values = new NamedValueProvider();
                    values.Add("POSTER_USER_ID", comment.UserId);
                    values.Add("DELETE_PANEL", canDelete ? "<div class=\"commentDeleteHandle\" style=\"float: right; padding: 5px\"><a class=\"commentDeleteButton\"></a></div>" : string.Empty);
                    values.Add("COMMENT_ID", comment.Id);
                    values.Add("COMMENT_MESSAGE", comment.Content);
                    values.Add("POSTER_NAME", poster.FirstAndLastName);
                    values.Add("DATE_POSTED", string.Format("{0}{1}", comment.DateCreated.ToString("MMMM d a\\t h:mm"), comment.DateCreated.ToString("tt").ToLower()));
                    values.Add("POSTER_IMAGE_URL", poster.GetPhotoPath("37x37"));

                    sb.Append(Substituter.Substitute(itemTemplate, values));
                }
            }

            

            values = new NamedValueProvider();
            values.Add("POSTER_USER_ID", currentUserId);
            values.Add("COMMENT_ITEMS", sb);

            if (currentUserId > 0)
            {
                poster = WebUser.Get(currentUserId);

                values.Add("POSTER_NAME", poster.FirstAndLastName);
                values.Add("POSTER_IMAGE_URL", poster.GetPhotoPath("64x64"));
            }

            return Substituter.Substitute(commentsTemplate, values);
        }
    }
}
