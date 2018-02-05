using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration
{
    public class LessonReviewerSessionSqlProvider : GenericSqlDataProviderBase<LessonReviewerSession>
    {
        protected override string DeleteProcedure { get { return "LessonReviewerSession_Del"; } }
        protected override string SelectProcedure { get { return "LessonReviewerSession_Get"; } }

        protected override LessonReviewerSession From(IDataReader r, LessonReviewerSession source)
        {
            var item = source ?? new LessonReviewerSession();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.ServiceScheduleID = DataUtil.GetInt32(r, "ServiceScheduleID");
            item.ServiceStartDate = DataUtil.GetDateTime(r, "ServiceStartDate");
            item.ServiceName = DataHelper.Get(r, "ServiceName");
            item.DateStarted = DataUtil.GetDateTime(r, "DateStarted");
            item.DateCompleted = DataUtil.GetDateTime(r, "DateCompleted");
            item.MemberId = DataUtil.GetId(r, "MemberId");
            item.AbsentReason = DataHelper.Get(r, "AbsentReason");
            item.CouncillorNotes = DataHelper.Get(r, "CouncillorNotes");
            item.CouncillorUserId = DataUtil.GetId(r, "CouncillorUserId");
            item.Status = DataUtil.GetInt32(r, "Status");
            item.DateApproved = DataUtil.GetDateTime(r, "DateApproved");
            item.AdditionalNotes = DataHelper.Get(r, "AdditionalNotes");
            item.AttendanceType = DataUtil.GetInt32(r, "AttendanceType");
            item.PageId = DataUtil.GetId(r, WebColumns.PageId);
            item.Extra = DataHelper.Get(r, "Extra");
            return item;
        }

        public List<LessonReviewerSession> GetList(int memberId = -1, int status = -1, int serviceScheduleId = -1, int pageId = WConstants.NULL_ID, int attendanceType = WConstants.NULL_ID)
        {
            var items = new List<LessonReviewerSession>();
            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@MemberId", memberId),
                new SqlParameter("@Status", status),
                new SqlParameter("@ServiceScheduleId", serviceScheduleId),
                new SqlParameter("@PageId", pageId),
                new SqlParameter("@AttendanceType", attendanceType)))
            {
                while (r.Read())
                    items.Add(From(r));
            }
            return items;
        }

        public List<LessonReviewerSession> GetList(DateTime dateStart, DateTime dateEnd, int memberId = -1, int status = -1, int attendanceType = WConstants.NULL_ID)
        {
            var items = new List<LessonReviewerSession>();
            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@MemberId", memberId),
                new SqlParameter("@Status", status),
                new SqlParameter("@DateStart", dateStart),
                new SqlParameter("@DateEnd", dateEnd),
                new SqlParameter("@AttendanceType", attendanceType)))
            {
                while (r.Read())
                    items.Add(From(r));
            }
            return items;
        }

        public override int Update(LessonReviewerSession item)
        {
            var obj = SqlHelper.ExecuteScalar("LessonReviewerSession_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@ServiceScheduleID", item.ServiceScheduleID),
                new SqlParameter("@ServiceStartDate", item.ServiceStartDate),
                new SqlParameter("@ServiceName", item.ServiceName),
                new SqlParameter("@DateStarted", item.DateStarted),
                new SqlParameter("@DateCompleted", item.DateCompleted),
                new SqlParameter("@MemberId", item.MemberId),
                new SqlParameter("@AbsentReason", item.AbsentReason),
                new SqlParameter("@CouncillorNotes", item.CouncillorNotes),
                new SqlParameter("@CouncillorUserId", item.CouncillorUserId),
                new SqlParameter("@Status", item.Status),
                new SqlParameter("@DateApproved", item.DateApproved),
                new SqlParameter("@AdditionalNotes", item.AdditionalNotes),
                new SqlParameter("@AttendanceType", item.AttendanceType),
                new SqlParameter("@PageId", item.PageId),
                new SqlParameter("@Extra", item.Extra)
            );

            item.Id = DataUtil.GetId(obj);
            return item.Id;
        }
    }
}
