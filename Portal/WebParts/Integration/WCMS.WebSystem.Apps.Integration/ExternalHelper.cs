using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;
using WCMS.WebSystem.Apps.Integration.AttendanceWS;
using WCMS.WebSystem.Apps.Integration.CommonWS;

namespace WCMS.WebSystem.Apps.Integration
{
    public enum AttendanceStatus
    {
        Success = 0,
        InvalidSchedule,
        AccountNotLinked,
        SubmitError,
        UnknownError
    }

    public struct ExternalAttendanceStatus
    {
        public const string Normal = "normal";
        public const string HookUp = "hookup";
        public const string MakeUp = "makeup";
        public const string PlayBack = "playback";
        public const string MP3 = "mp3";
    }

    public class LogAttendanceResult
    {
        public LogAttendanceResult()
        {
            Status = 0;
        }

        public ServiceSchedule Schedule { get; set; }
        public int Status { get; set; }
    }

    public class ExternalHelper
    {
        public static ServiceSchedule GetServiceSchedule(int id)
        {
            var commonClient = new CommonWSSoapClient();
            var serviceSchedule = commonClient.GetServiceSchedule(id);
            return serviceSchedule;
        }

        public static LogAttendanceResult LogAttendance(int memberId, int serviceScheduleId)
        {
            var result = new LogAttendanceResult();
            var attendanceClient = new AttendanceSoapClient(false);
            var serviceSchedule = GetServiceSchedule(serviceScheduleId);
            result.Schedule = serviceSchedule;

            if (attendanceClient.Insert3((long)memberId, serviceSchedule.LocaleID, serviceScheduleId, serviceSchedule.ServiceID, serviceSchedule.WeekNo, DateTime.Now.ToString("u"), serviceSchedule.LateDateTime.ToString("u"), ExternalAttendanceStatus.Normal, true))
                result.Status = 200;
            return result;

            
        }

        public static IEnumerable<MemberAttendance> GetAttendanceList(DateTime serviceStartDate, DateTime serviceEndDate, int memberID = -1, int localeID = -1, int localeGroupID = -1)
        {
            var client = new MemberSoapClient(false);
            return client.GetAttendances(memberID, localeID, localeGroupID, serviceStartDate, serviceEndDate);
        }

        public static LogAttendanceResult LogAttendance(int memberId, int serviceScheduleId, DateTime timeIn, string status)
        {
            var result = new LogAttendanceResult();
            var attendanceClient = new AttendanceSoapClient(false);
            var commonClient = new CommonWSSoapClient();
            var serviceSchedule = commonClient.GetServiceSchedule(serviceScheduleId);
            result.Schedule = serviceSchedule;

            if (attendanceClient.Insert3((long)memberId, serviceSchedule.LocaleID, serviceScheduleId, serviceSchedule.ServiceID, serviceSchedule.WeekNo, timeIn.ToString("u"), serviceSchedule.LateDateTime.ToString("u"), status, true))
                result.Status = 200;
            return result;
        }
    }
}
