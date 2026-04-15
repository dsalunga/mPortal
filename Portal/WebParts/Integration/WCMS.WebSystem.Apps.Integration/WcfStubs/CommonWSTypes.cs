using System;

namespace WCMS.WebSystem.Apps.Integration.CommonWS
{
    /// <summary>
    /// Stub replacement for WCF-generated CommonWSSoapClient.
    /// SOAP service is no longer available; methods return safe defaults.
    /// </summary>
    public class CommonWSSoapClient
    {
        public ServiceSchedule GetServiceSchedule(int serviceScheduleID) => null;
    }

    /// <summary>
    /// Stub base class for WCF-generated CreationDate audit fields.
    /// </summary>
    public abstract class CreationDate
    {
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public int UpdatedBy { get; set; }
    }

    /// <summary>
    /// Stub replacement for WCF-generated ServiceSchedule data class.
    /// </summary>
    public class ServiceSchedule : CreationDate
    {
        public long ServiceScheduleID { get; set; }
        public int LocaleID { get; set; }
        public int ServiceID { get; set; }
        public DateTime StartServiceDateTime { get; set; }
        public DateTime EndServiceDateTime { get; set; }
        public DateTime LateDateTime { get; set; }
        public string WeekNo { get; set; } = string.Empty;
        public int IsMainSchedule { get; set; }
        public long AssignedCouncillor { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public int IsActive { get; set; }
    }
}
