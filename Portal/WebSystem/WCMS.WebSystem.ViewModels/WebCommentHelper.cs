using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem
{
    public class WebCommentHelper
    {
        public static string PrepareCommentsUI(WebObjectBase update, bool userIsContentManager)
        {
            var context = HttpContext.Current;
            NamedValueProvider values = null;
            var currentUserId = WSession.Current.UserId;

            var sb = new StringBuilder();
            var commentsTemplate = FileHelper.ReadFile(context.Server.MapPath("~/Content/Parts/Social/Comments.template.htm"));
            var itemTemplate = FileHelper.ReadFile(context.Server.MapPath("~/Content/Parts/Common/Templates/CommentItem.template.htm"));

            WebUser poster = null;

            // Build comment items
            var comments = WebComment.Provider.GetList(-2, update.OBJECT_ID, update.Id);
            if (comments.Count() > 0)
            {
                foreach (var comment in comments)
                {
                    poster = comment.User;
                    var canDelete = userIsContentManager || currentUserId == comment.UserId;

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

            poster = WebUser.Get(currentUserId);

            values = new NamedValueProvider();
            values.Add("POSTER_USER_ID", currentUserId);
            values.Add("COMMENT_ITEMS", sb);
            values.Add("POSTER_NAME", poster.FirstAndLastName);
            values.Add("POSTER_IMAGE_URL", poster.GetPhotoPath("64x64"));

            return Substituter.Substitute(commentsTemplate, values);
        }
    }
}
