using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Diagnostics;
using WCMS.Framework.Utilities;

using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile.LessonReviewer
{
    public partial class ConfigAttendanceRequests : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var sw = PerformanceLog.StartLog();
                var context = new WContext(this);
                var element = context.Element;
                var groupPath = element.GetParameterValue(MemberConstants.ParentGroupKey);
                var status = context.GetInt32("Show", -2);
                if (status != -2 && cboStatus.Items.FindByValue(status.ToString()) != null)
                    cboStatus.SelectedValue = status.ToString();

                if (!string.IsNullOrEmpty(groupPath))
                {
                    var group = WebGroup.SelectNode(groupPath);
                    if (group != null)
                        hidBaseGroupId.Value = group.Id.ToString();
                }
                GridView1.DataBind();
                PerformanceLog.EndLog(string.Format("Integration-MakeUpRequests-Load: {0}/{1}", context.ObjectId, context.RecordId), sw, context.PageId);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int id = DataHelper.GetId(e.CommandArgument);

            //switch (e.CommandName)
            //{
            //    case "ViewItem":
            //        WContext context = new WContext(this);
            //        context.Set("LessonReviewerSessionId", id);
            //        context.SetOpen("MU-Session");
            //        context.Redirect();
            //        break;
            //}
        }

        public DataSet Select(int status, string keyword, int userId = -1)
        {
            MemberLink link = userId > 0 ? MemberLink.Provider.GetByUserId(userId) : null;
            WebUser user = null;
            var userDetailsFormat = MemberConstants.UserProfilePageFormat;
            var keywordLowered = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();
            var attendances = link == null && userId > 0 ? new List<LessonReviewerSession>() : LessonReviewerSession.Provider.GetList(link == null ? -1 : link.MemberId, status, AttendanceTypes.MakeUp);

            var orderedResult = (from a in attendances
                                 where (userId > 0 && link != null || (link = a.Member) != null)
                                 && (user = link.User) != null
                                 && (string.IsNullOrEmpty(keywordLowered) ||
                                     (DataUtil.HasMatch(user.UserName, keywordLowered) || //i.UserName.ToLower().Contains(kwl) ||
                                         DataUtil.HasMatch(user.FirstName, keywordLowered) || //i.FirstName.ToLower().Contains(kwl) ||
                                         DataUtil.HasMatch(user.LastName, keywordLowered) || //i.LastName.ToLower().Contains(kwl) ||
                                         DataUtil.HasMatch(user.MiddleName, keywordLowered) || //i.MiddleName.ToLower().Contains(kwl) ||
                                         DataUtil.HasMatch(user.Email, keywordLowered) || //i.Email.ToLower().Contains(kwl) ||
                                         DataUtil.HasMatch(user.MobileNumber, keywordLowered) || //i.MobileNumber.ToLower().Contains(kwl) ||
                                         DataUtil.HasMatch(link.ExternalIdNo, keywordLowered) ||
                                         DataUtil.HasMatch(a.ServiceName, keywordLowered)))  //memberLink.ExternalIdNo.ToLower().Contains(kwl)))
                                 select new
                                 {
                                     a.Id,
                                     ServiceType = MemberHelper.GetShortService(a.ServiceName),
                                     a.ServiceStartDate,
                                     a.DateStarted,
                                     a.Duration,
                                     FullName = AccountHelper.GetPrefixedName(user),
                                     PhotoPath = link.GetPhotoPathIfNull("200x200"),
                                     UserProfileUrl = string.Format(userDetailsFormat, user.Id),
                                     a.Status
                                 }).OrderByDescending(i => i.DateStarted).AsEnumerable();
            return DataUtil.ToDataSet(orderedResult);
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {

        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
        }

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            context.Redirect();
        }

        private WContext _context;
        public string BuildUrl(object id)
        {
            if (_context == null)
            {
                _context = new WContext(this);
                _context.SetOpen("MU-Session");
            }
            _context.Set("LessonReviewerSessionId", id);
            return _context.BuildQuery();
        }

        protected void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var show = DataUtil.GetInt32(cboStatus.SelectedValue);
            if (show == 0)
                context.Remove("Show");
            else
                context.Set("Show", cboStatus.SelectedValue);
            context.Redirect();
        }
    }
}