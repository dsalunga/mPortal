using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;

namespace WCMS.WebSystem.Apps.Integration.MakeUpView
{
    public partial class MemberSessionView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var partAdmin = context.PartAdmin;
                if (partAdmin != null)
                {
                    hUserFormatString.Value = partAdmin.GetParameterValue("UserDataFormatString");
                    GridView1.DataBind();
                }
            }
        }

        public DataSet Select(string userFormatString)
        {
            WPage page = null;
            WebUser user = null;
            DateTime now = DateTime.Now;

            MemberLink link = null;
            LessonReviewerSession makeup = null;
            UserSessionBrowser bs = null;
            var sessions = LessonReviewerSession.Provider.GetList(-1, LessonReviewerSessionStatus.Draft);

            return DataHelper.ToDataSet(
                from i in WSession.UserSessions.Sessions
                where (bs = i.LastBrowserSession) != null && ((page = bs.LastPageId > 0 ? WPage.Get(bs.LastPageId) : null) == null || true)
                    && ((user = i.UserId > 0 ? WebUser.Get(i.UserId) : null) != null || true)
                    && ((link = MemberLink.Provider.GetByUserId(i.UserId)) != null && (makeup = sessions.FirstOrDefault(s => s.MemberId == link.MemberId)) != null)
                select new
                {
                    i.UserId,
                    MakeUpStartDate = makeup.DateStarted,
                    bs.LastPageId,
                    IdleTime = now - bs.LastActivityDate,
                    //SessionTime = now - i.ActivityStartDate,
                    MakeUpTime = makeup.Duration,
                    PageUrl = bs.LastPageUrl,
                    PageId = page == null ? "" : page.Id.ToString(),
                    PageName = page == null ? "(Custom)" : page.Name,
                    Name = FormatUserData(user, userFormatString) + " <span class='req-data' data-req=\"" + i.SessionId + "," + bs.IPAddress + "," + bs.UserAgent + "\">[@]</span>"
                }
            );
        }

        private static string FormatUserData(WebUser user, string formatString)
        {
            if (user != null)
            {
                var provider = new NamedValueProvider();
                provider.Add("UserId", user.Id);
                provider.Add("Name", user.FirstAndLastName);
                return Substituter.Substitute(string.IsNullOrEmpty(formatString) ? "$(Name)" : formatString, provider);
            }

            return null;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}
