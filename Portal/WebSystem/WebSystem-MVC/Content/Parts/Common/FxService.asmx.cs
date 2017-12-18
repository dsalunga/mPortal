using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Utilities;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Common
{
    /// <summary>
    /// Summary description for FrameworkService
    /// </summary>
    [WebService(Namespace = "http://someorg.org/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FrameworkService : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public WSUserInfo Login(string userName, string password, bool loginSession = false)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                var user = AccountHelper.ValidateLogin(userName, password);
                if (user != null && user.IsActive)
                {
                    if (loginSession)
                        WSession.Current.Login(user.Id, true);

                    return new WSUserInfo(user);
                }
            }
            else if (WSession.Current.IsLoggedIn)
            {
                return new WSUserInfo(WSession.Current.User);
            }

            return null;
        }

        [WebMethod(EnableSession = true)]
        public bool LogOff()
        {
            WSession.LogOff();

            return true;
        }

        [WebMethod]
        public string PostComment(string commentText, int userId, int targetObjectId, int targetRecordId)
        {
            var poster = WebUser.Get(userId);
            if (poster != null)
            {
                var commentDate = DateTime.Now;
                var comment = new WebComment();
                comment.ObjectId = targetObjectId;
                comment.RecordId = targetRecordId;
                comment.Content = commentText;
                comment.DateCreated = commentDate;
                comment.UserId = userId;
                comment.Update();

                string template = FileHelper.ReadFile(Context.Server.MapPath("~/Content/Parts/Common/Templates/CommentItem.template.htm"));

                var values = new NamedValueProvider();
                values.Add("POSTER_USER_ID", userId);
                values.Add("DELETE_PANEL", "<div class=\"commentDeleteHandle\" style=\"float: right; padding: 5px\"><a class=\"commentDeleteButton\"></a></div>");
                values.Add("COMMENT_ID", comment.Id);
                values.Add("COMMENT_MESSAGE", commentText);
                values.Add("POSTER_NAME", poster.FirstAndLastName);
                values.Add("DATE_POSTED", string.Format("{0}{1}", commentDate.ToString("MMMM d a\t hh:mm"), commentDate.ToString("tt").ToLower()));
                values.Add("POSTER_IMAGE_URL", poster.GetPhotoPath("37x37"));
                return Substituter.Substitute(template, values);
            }

            return null;
        }

        [WebMethod]
        public bool DeleteComment(int id)
        {
            if (id > 0)
            {
                var comment = WebComment.Provider.Get(id);
                if (comment != null)
                    return comment.Delete();
            }

            return false;
        }
    }
}
