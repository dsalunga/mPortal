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

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class UserSessionManagerView : System.Web.UI.UserControl
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
            UserSessionBrowser bs = null;

            return DataHelper.ToDataSet(
                from i in WSession.UserSessions.Sessions
                where (bs = i.LastBrowserSession) != null && ((page = bs.LastPageId > 0 ? WPage.Get(bs.LastPageId) : null) == null || true)
                    && ((user = i.UserId > 0 ? WebUser.Get(i.UserId) : null) != null || true)
                select new
                {
                    i.UserId,
                    bs.ActivityStartDate,
                    bs.LastActivityDate,
                    bs.LastPageId,
                    IdleTime = now - bs.LastActivityDate,
                    SessionTime = now - bs.ActivityStartDate,
                    PageUrl = bs.LastPageUrl,
                    PageId = page == null ? "" : page.Id.ToString(),
                    PageName = page == null ? "(Custom)" : page.Name,
                    IPAddress = bs.IPAddress + (string.IsNullOrEmpty(bs.IPLocation) ? "" : string.Format(" ({0})", bs.IPLocation)),
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

        protected void cmdEndSession_Click(object sender, EventArgs e)
        {
            string checkedIds = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(checkedIds))
            {
                var ids = DataHelper.ParseCommaSeparatedIdList(checkedIds);
                if (ids.Count > 0)
                {
                    var manager = WSession.UserSessions;
                    foreach (int id in ids)
                        WSession.LogOff(id, true);
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}
