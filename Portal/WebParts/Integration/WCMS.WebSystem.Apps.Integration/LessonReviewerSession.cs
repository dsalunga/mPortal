using System;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration
{
    public class LessonReviewerSession : WebObjectBase, ISelfManager
    {
        private static LessonReviewerSessionSqlProvider _provider;

        static LessonReviewerSession()
        {
            _provider = new LessonReviewerSessionSqlProvider();
        }

        public LessonReviewerSession()
        {
            ServiceScheduleID = -1;
            ServiceStartDate = WConstants.DateTimeMinValue;
            CouncillorUserId = -1;
            AttendanceType = AttendanceTypes.MakeUp;
            AbsentReason = string.Empty;
            CouncillorNotes = string.Empty;
            AdditionalNotes = string.Empty;
            Extra = string.Empty;
            Status = LessonReviewerSessionStatus.PendingApproval;
            DateApproved = WConstants.DateTimeMinValue;
            PageId = -1;
        }

        public int ServiceScheduleID { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public string ServiceName { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateCompleted { get; set; }
        public int MemberId { get; set; }
        public string AbsentReason { get; set; }
        public string CouncillorNotes { get; set; }
        public int CouncillorUserId { get; set; }
        public int Status { get; set; }
        public DateTime DateApproved { get; set; }
        public string AdditionalNotes { get; set; }
        public int AttendanceType { get; set; }
        public int PageId { get; set; }
        public string Extra { get; set; }

        public override int OBJECT_ID => -1;

        public MemberLink Member => MemberLink.Provider.GetByMemberId(MemberId);
        public WebUser Councillor => WebUser.Get(CouncillorUserId);
        public TimeSpan Duration =>
            (Status == LessonReviewerSessionStatus.Draft ? DateTime.Now : DateCompleted) - DateStarted;

        public static LessonReviewerSessionSqlProvider Provider => _provider;

        // ISelfManager
        public bool Delete() => _provider.Delete(this.Id);
        public int Update() => _provider.Update(this);
    }
}
