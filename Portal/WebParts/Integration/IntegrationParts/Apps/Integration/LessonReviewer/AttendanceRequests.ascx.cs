using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Diagnostics;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Integration.LessonReviewer
{
    public partial class AttendanceRequests : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var sw = PerformanceLog.StartLog();
                var context = new WContext(this);
                var element = context.Element;
                var set = element.GetParameterSet();
                var groupPath = element.GetParameterValue(MemberConstants.ParentGroupKey);
                var status = context.GetInt32("Show", -2);

                if (status != -2 && cboStatus.Items.FindByValue(status.ToString()) != null)
                    cboStatus.SelectedValue = status.ToString();
                hPageId.Value = ParameterizedWebObject.GetValue("PageId", "-1", set, element);

                //if (!string.IsNullOrEmpty(groupPath))
                //{
                //    var group = WebGroup.SelectNode(groupPath);
                //    if (group != null)
                //        hidBaseGroupId.Value = group.Id.ToString();
                //}

                GridView1.DataBind();
                PerformanceLog.EndLog(string.Format("Integration-MakeUpRequests-Load: {0}/{1}", context.ObjectId, context.RecordId), sw, context.PageId);
            }
        }

        public DataSet Select(int status, string keyword, int userId = -1, int pageId = -1)
        {
            MemberLink link = userId > 0 ? MemberLink.Provider.GetByUserId(userId) : null;
            WebUser user = null;

            var userDetailsFormat = MemberConstants.UserProfilePageFormat;
            var keywordLowered = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();
            var attendances = link == null && userId > 0 ? new List<LessonReviewerSession>() : LessonReviewerSession.Provider.GetList(link == null ? -1 : link.MemberId, status, -1, pageId);
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
                                     ServiceType = string.Format("{0} - {1}", MemberHelper.GetShortService(a.ServiceName), AttendanceTypes.GetText(a.AttendanceType)),
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
                _context.SetOpen("Attendance-Request");
            }
            _context.Set("Id", id);
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