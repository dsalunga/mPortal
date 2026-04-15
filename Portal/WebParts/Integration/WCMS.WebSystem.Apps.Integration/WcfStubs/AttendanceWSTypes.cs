using System;

namespace WCMS.WebSystem.Apps.Integration.AttendanceWS
{
    /// <summary>
    /// Stub replacement for WCF-generated AttendanceSoapClient.
    /// SOAP service is no longer available; methods return safe defaults.
    /// </summary>
    public class AttendanceSoapClient
    {
        public AttendanceSoapClient() { }
        public AttendanceSoapClient(bool expect100Continue) { }

        public static AttendanceSoapClient GetNewClientInstance() => new();

        public bool Insert3(long memberID, int localeID, int serviceScheduleID, int serviceID,
            string weekNo, string dateTimeIn, string serviceLateTime,
            string status, bool overWriteAttendance) => false;
    }
}
