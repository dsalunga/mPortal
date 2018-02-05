using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration
{
    public class IntegrationAppSessions
    {
        public const string MemberProfile = "Integration.Profile";
        public const string EXT_USER_INFO_KEY = "UserProvider.EXT_USER_INFO";
    }

    public class IntegrationColumns
    {
        public const string MemberId = "MemberId";
        public const string ExternalId = "ExternalId";
    }

    //public class AccessSteps
    //{
    //    public const string Step10_EnterCIDEmail = "1.0";
    //    public const string Step11_EnterPwd = "1.1";

    //    public const string Step20_ForgotEnterEmail = "2.0";
    //    public const string Step21_ForgotEnterCID = "2.1";
    //    public const string Step22_ForgotEnterDOB = "2.2";

    //    public const string Step30_RegEnterBasic = "3.0";
    //}

    public class AccountRecoverStatus
    {
        public const int NULL = -1;
        public const int InformAdmin = 0;
        public const int ResetExt = 1;
        public const int ResetPortal = 2;
        public const int NewUserWithONE = 3;
        public const int RegisterSG = 4;
        public const int RegisterNew = 5;
    }


    public class ParameterKeys
    {
        public const string ItemTemplate = "ItemTemplate";
        public const string ContainerTemplate = "ContainerTemplate";
        public const string MaxItems = "MaxItems";
        public const string CelebrantsFilter = "CelebrantsFilter";
        public const string MainPageUrl = "MainPageUrl";
        public const string CustomFilter = "CustomFilter";
        public const string ParentGroup = "ParentGroup";
        public const string EmptyTemplate = "EmptyTemplate";
    }

    public class TemplateKeys
    {
        public const string ProfileUrl = "ProfileUrl";
        public const string DisplayName = "DisplayName";
        public const string PhotoUrl = "PhotoUrl";

        public const string CelebrantsMonth = "CelebrantsMonth";
    }

    public class IntegrationConstants
    {
        public const string REG_ProfileNode = "/Apps/Integration/Profile";

        public const int MusicCompetition = 127;
        public const int MCCandidate = 123;
        public const int MCVote = 124;

        public const string MEMBERS_GROUP_NAME = "Members";
        public const string MCDefaultFinalistPhoto = "_finalist.jpg";

        private static string _MCBasePath;
        public static string MCBasePath
        {
            get
            {
                if (_MCBasePath == null)
                    _MCBasePath = ConfigHelper.Get("Integration:MCBasePath");

                return _MCBasePath;
            }
        }
    }

    public enum MasterListTab
    {
        GroupOverview = 0,
        Groups = 1,
        Members = 2,
        Access = 3
    }

    public class MasterListConstants
    {
        public const string GROUP_MENTORS_KEY = "Integration-MM:Mentors";
        public const string GROUP_CONDUCTORS_KEY = "Integration-MM:Conductors";
        public const string MEMBER_VOICE_DESIGNATION_KEY = "Integration-MM:VoiceDesignation";
        public const string MEMBER_POSITION_KEY = "Integration-MM:Position";

        public const string OPEN_KEY_MEMBERS = "Members";
        public const string OPEN_KEY_MEMBER_EDIT = "MemberEdit";
        public const string OPEN_KEY_GROUP_EDIT = "GroupEdit";

        public const string ROOT_NAME = "Music Ministry";
    }

    public class UnixCommand
    {
        public string Server { get; set; }
        public string Username { get; set; }
        public string PrivateKeyPath { get; set; }
        public string Command { get; set; }
    }

    public static class MemberConfig
    {
        private static UnixCommand _emailUnixCmd = null;
        public static UnixCommand GetEmailUnixCommand()
        {
            if (_emailUnixCmd == null)
            {
                var reg = WebRegistry.SelectNodeValue("/Apps/Integration/Profile/Integration-Email-UNIX");
                if (!string.IsNullOrEmpty(reg))
                {
                    var xdoc = new XmlDocument();
                    xdoc.LoadXml(reg);

                    var firstNode = xdoc.SelectSingleNode("//Email-Unix-Info");
                    _emailUnixCmd = new UnixCommand();
                    _emailUnixCmd.Server = XmlUtil.GetValue(firstNode, "Server");
                    _emailUnixCmd.Username = XmlUtil.GetValue(firstNode, "Username");
                    _emailUnixCmd.PrivateKeyPath = WebHelper.MapPath(XmlUtil.GetValue(firstNode, "PrivateKeyPath"));
                    _emailUnixCmd.Command = XmlUtil.GetValue(firstNode, "Command");
                }
            }

            return _emailUnixCmd;
        }
    }

    public struct ExtConstants
    {
        //public const int SGLocale = 1442;
        public const string ServicesCacheKey = "WCMS.Integration.External.Services";
    }

    public struct MemberConstants
    {
        public const string ParentGroupKey = "ParentGroup";

        public const string NoPhotoPathKey = "/Apps/Integration/External/NoPhotoPath";
        public const string PhotoPathKey = "/Apps/Integration/External/PhotoPath";
        public const string BaseUrlKey = "/Apps/Integration/External/BaseUrl";

        public const string LoginUrlKey = "LoginUrl";
        public const string ActivateAccountUrlKey = "ActivateAccountUrl";
        //public const string ConfirmNewEmailUrlKey = "ConfirmNewEmailUrl";
        public const string ActivationBCCPath = "/Apps/Integration/External/ActivationBCC";
        public const string GroupsToAddPath = "/Apps/Integration/External/GroupsToAdd";

        public static string UserProfilePageFormat = WebRegistry.SelectNodeValue("/Apps/Integration/Profile/UserInfoPageFormat");
        public static string GroupApprovalLink = WebRegistry.SelectNodeValue("/Apps/Integration/Profile/GroupApprovalLink");

        public const string PendingApprovalString = " <em>(Pending Approval)</em>";

        public const string LocaleGroupPathKey = "LocaleGroupPath";
        public const string MinistriesPathKey = "MinistriesPath";
        public const string SpecialGroupsPathKey = "SpecialGroupsPath";

        public const string LocaleGroupPath = "/Integration/Chapters/Singapore/Groups";
        public const string LocaleGroupRegPath = "/Apps/Integration/Profile/LocaleGroupPath";

        public const string MinistriesGroupPath = "/Integration/Chapters/Singapore/Ministries";
        public const string MinistriesRegistryPath = "/Apps/Integration/Profile/MinistriesPath";

        public const string SpecialGroupsPath = "/Integration/Chapters/Singapore/Special-Groups";
        public const string SpecialGroupsRegPath = "/Apps/Integration/Profile/SpecialGroupsPath";

        public const string UpdateRedirect = "UpdateRedirect";
        public const string FirstUpdateRedirect = "FirstUpdateRedirect";
        public const string CancelRedirect = "CancelRedirect";

        public const string ApproversKey = "Approvers";

        public const int ODKStatusPrevChars = 30;
        public const string ODKPrintTemplateFileKey = "ODKPrintTemplateFile";
        public const string ProfileUpdateEvent = "PROFILE UPDATE";
        public const string GroupUpdateEvent = "GROUP UPDATE";

        public const string DropDownVisibleKey = "DropDownVisible";
        public const string CelebrantsFilterVisible = "CelebrantsFilterVisible";

        public const string External_AUTH_URL_KEY = "AMSAuthServiceUrl";
        public const string External_AUTH_RETURN_URL_KEY = "AMSAuthServiceReturnUrl";
        public const string External_AUTH_PARAM_KEY = "AuthKey";

        public const string REMOTE_SYNC_CONFIG = "Integration.RemoteInstance";
    }

    public struct MCConstants
    {
        //public const string SCORE_LOCK = "JudgesScoreLocked";
    }

    public struct MemberAccountStatus
    {
        public const int Approved = 1;
        public const int Pending = 0;
        public const int Rejected = 2;

        public const string ApprovedString = "Approved";
        public const string PendingString = "Pending Approval";
        public const string RejectedString = "Rejected";
    }

    public struct AttendanceConstants
    {
        public const string ABSENT = "ABSENT";
        public const string MAKE_UP = "MAKE-UP";
        public const string ON_TIME = "ON-TIME";
        public const string LATE = "LATE";
    }

    public struct LessonReviewerSessionStatus
    {
        public const int PendingApproval = 0;
        public const int Approved = 1;
        public const int Rejected = 2;
        public const int Draft = 3;
        public const int PendingAutoSubmit = 4;

        public static string GetName(int value)
        {
            switch (value)
            {
                case PendingApproval:
                    return "Pending Approval";
                case Approved:
                    return "Approved";
                case Rejected:
                    return "Rejected";
                case Draft:
                    return "Draft Request";
                case PendingAutoSubmit:
                    return "Pending Approval (Auto-submit)";

                default:
                    return string.Empty;
            }
        }
    }

    public struct AttendanceTypes
    {
        public const int Live = 0;
        public const int MakeUp = 1;

        public static string GetText(int id)
        {
            switch (id)
            {
                case Live:
                    return "Live";
                case MakeUp:
                    return "MakeUp";
                default:
                    return string.Empty;
            }
        }
    }
}
