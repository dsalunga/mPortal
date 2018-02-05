using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Common.Utilities;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;

namespace WCMS.WebSystem.Apps.Integration
{
    /// <summary>
    /// Temporary storage of makeup session. This is created only during runtime.
    /// </summary>
    public class MakeUpSession
    {
        public const string SessionKey = "MakeUpSession";

        public MakeUpSession(MemberAttendance attendance, DateTime dateStarted)
        {
            DateStarted = dateStarted;
            DateCompleted = dateStarted;
            this.Attendance = attendance;
        }

        public DateTime DateStarted { get; set; }
        public DateTime DateCompleted { get; set; }
        public MemberAttendance Attendance { get; set; }

        public LessonReviewerSession GetItem()
        {
            var attendance = Attendance;
            var items = LessonReviewerSession.Provider.GetList((int)attendance.MemberID, LessonReviewerSessionStatus.Draft, (int)attendance.ServiceScheduleID);
            if (items.Count > 0)
            {
                return items.First();
            }
            else
            {
                var item = new LessonReviewerSession();
                item.ServiceScheduleID = (int)attendance.ServiceScheduleID;
                item.ServiceStartDate = DataUtil.GetDateTime(attendance.ServiceDateTime);
                item.ServiceName = attendance.ServiceType;
                item.DateStarted = DateStarted;
                item.DateCompleted = DateCompleted;
                item.AttendanceType = AttendanceTypes.MakeUp;
                item.MemberId = (int)attendance.MemberID;
                return item;
            }
        }
    }
}
