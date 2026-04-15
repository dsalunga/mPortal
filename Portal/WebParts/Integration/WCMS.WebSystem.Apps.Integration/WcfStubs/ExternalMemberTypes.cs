using System;

namespace WCMS.WebSystem.Apps.Integration.ExternalMemberWS
{
    /// <summary>
    /// Stub replacement for WCF-generated MemberSoapClient.
    /// SOAP service is no longer available; methods return safe defaults.
    /// </summary>
    public class MemberSoapClient
    {
        public MemberSoapClient() { }
        public MemberSoapClient(bool expect100Continue) { }

        public static MemberSoapClient GetNewClientInstance() => new();
        public long GetMemberID(string externalIdNo) => -1;
        public MemberPhoto[] GetPhoto(long memberID) => Array.Empty<MemberPhoto>();
        public MemberAttendance[] GetAttendances(long memberID, int localeID, int localeGroupID,
            DateTime serviceStartDate, DateTime serviceEndDate) => Array.Empty<MemberAttendance>();
    }

    /// <summary>
    /// Stub replacement for WCF-generated Member data class + partial class extensions.
    /// </summary>
    public class Member
    {
        public long MemberID { get; set; }
        public string Flag { get; set; } = string.Empty;
        public string ExternalIDNo { get; set; } = string.Empty;
        public string TemporaryIDNo { get; set; } = string.Empty;
        public string NickName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string BloodType { get; set; } = string.Empty;
        public int CivilStatusID { get; set; }
        public int CitizenshipID { get; set; }
        public int RaceID { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Email2 { get; set; } = string.Empty;
        public int IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public int Id { get => (int)MemberID; set => MemberID = value; }

        public string FullName => $"{LastName}, {FirstName}" +
            (string.IsNullOrEmpty(MiddleName) ? "" : $" {MiddleName.Substring(0, 1)}.");

        public string EvalExternalId =>
            !string.IsNullOrEmpty(ExternalIDNo) ? ExternalIDNo : TemporaryIDNo;

        private static ExternalMemberStubProvider _remoteProvider = new();
        public static ExternalMemberStubProvider RemoteProvider => _remoteProvider;
    }

    /// <summary>
    /// Stub provider for Member remote data access. SOAP service not available.
    /// </summary>
    public class ExternalMemberStubProvider
    {
        public Member Get(long memberId) => null;
        public Member Get(int memberId) => null;
    }

    /// <summary>
    /// Stub replacement for WCF-generated MemberPhoto data class.
    /// </summary>
    public class MemberPhoto
    {
        public long MemberPhotoID { get; set; }
        public long MemberID { get; set; }
        public string PhotoFileName { get; set; } = string.Empty;
        public string SignFileName { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;
    }

    /// <summary>
    /// Stub replacement for WCF-generated MemberAttendance data class.
    /// </summary>
    public class MemberAttendance
    {
        public long AttendanceID { get; set; }
        public long MemberID { get; set; }
        public long ServiceScheduleID { get; set; }
        public string WeekNo { get; set; } = string.Empty;
        public string ServiceType { get; set; } = string.Empty;
        public string ServiceDateTime { get; set; } = string.Empty;
        public string DateTimeIn { get; set; } = string.Empty;
        public string DateTimeOut { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
    }
}
