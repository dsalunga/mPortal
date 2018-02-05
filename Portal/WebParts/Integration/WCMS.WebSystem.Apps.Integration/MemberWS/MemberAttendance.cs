using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.Apps.Integration.ExternalMemberWS
{
    public partial class MemberAttendance
    {
        public const int MakeUp_AttendanceStatusID = 4;

        [XmlIgnore]
        public string ExternalIdNo { get; set; }

        //public bool CreateAttendance(DateTime timeIn, DateTime timeOut)
        //{
        //    if (this.AttendanceID <= 0 && this.ServiceScheduleID > 0)
        //    {
        //        try
        //        {
        //            ExternalDBEntities db = new ExternalDBEntities();

        //            var serviceSchedule = db.ServiceSchedules.FirstOrDefault(i => i.ServiceScheduleID == this.ServiceScheduleID);
        //            if (serviceSchedule != null)
        //            {
        //                Attendance attendance = db.Attendances.CreateObject();
        //                attendance.ServiceScheduleID = this.ServiceScheduleID;
        //                attendance.ServiceID = serviceSchedule.ServiceID;
        //                attendance.WeekNo = this.WeekNo;
        //                attendance.MemberID = this.MemberID;
        //                attendance.DateTimeIn = timeIn;
        //                attendance.DateTimeOut = timeOut.ToString();
        //                attendance.AttendanceStatusID = MakeUp_AttendanceStatusID;
        //                attendance.Remarks = "Make Up";
        //                attendance.DateCreated = DateTime.Now;
        //                attendance.CreatedBy = 0;
        //                attendance.DateUpdated = DateTime.Now;
        //                attendance.UpdatedBy = 0;
        //                db.Attendances.AddObject(attendance);

        //                db.SaveChanges();

        //                return true;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            LogHelper.WriteLog(ex);
        //        }
        //    }

        //    return false;
        //}

        public static bool DeleteAttendance(int attendanceId)
        {
            try
            {
                SqlHelper.ExecuteNonQuery("ExternalDB_Attendance_Del",
                        new SqlParameter("@Id", attendanceId));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
    }
}
