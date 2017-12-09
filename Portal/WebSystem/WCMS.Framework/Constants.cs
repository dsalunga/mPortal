using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Common.Utilities;

namespace WCMS.Framework
{
    public enum NamePrefixes
    {
        Brotherhood = 1
    }

    public enum AlertMessageTypes
    {
        Error = 1,
        Notification = 2,
        Question = 3
    }

    public enum AllowSharing
    {
        Allow = 1,
        Deny = 0,
        Null = -1
    }

    public class DataConstants
    {
        public const string SELECT_MAX = "SELECT MAX({0}) FROM {1}";
        public const string SELECT_COUNT = "SELECT COUNT(1) FROM {0}";
    }

    public struct TemplateConstants
    {
        public const string ItemTemplate = "ItemTemplate";
        public const string ContainerTemplate = "ContainerTemplate";
    }

    public struct RecordStatus
    {
        public const int Null = -1;
        public const int Active = 1;
        public const int Inactive = 0;
    }

    public class ExecutionStatus
    {
        private readonly static Dictionary<int, string> _kv;

        static ExecutionStatus()
        {
            _kv = new Dictionary<int, string>
            {
                {0, "Pending"},
                {1, "Running"},
                {2, "Completed"},
                {3, "Failed"}
            };
        }

        public static Dictionary<int, string> KeyValues
        {
            get { return _kv; }
        }

        public const int Pending = 0;
        public const int Running = 1;
        public const int Completed = 2;
        public const int Failed = 3;

        public static string ToString(int status)
        {
            if (_kv.ContainsKey(status))
                return _kv[status];

            return string.Empty;
        }
    }

    public struct AddressTags
    {
        public const string Home = "Home";
        public const string Work = "Work";
    }

    public struct WContextTypes
    {
        public const int Unknown = -1;
        public const int FrontEnd = 1;
        public const int EditMode = 2;
        public const int AdminMode = 3;
    }

    public struct RemoteItemTypes
    {
        public const int ALL = 0;
        public const int LOCAL = 1;
        public const int REMOTE = 2;
        public const int IDENTICAL = 3;
    }

    public class RecurrenceType
    {
        private static Dictionary<int, string> _kv;

        static RecurrenceType()
        {
            _kv = new Dictionary<int, string>
            {
                {-1, "None"},
                {1, "Daily"},
                {2, "Weekly"},
                {4, "Monthly"},
                {6, "Yearly"},
                {7, "Always"}
            };
        }

        public static Dictionary<int, string> KeyValues
        {
            get { return _kv; }
        }

        /// <summary>
        /// Run Once
        /// </summary>
        public const int None = -1;

        public const int Daily = 1;
        public const int Weekly = 2;
        public const int Monthly = 4;
        public const int Yearly = 6;
        public const int Always = 7;

        public static string GetName(int recurrenceId)
        {
            if (_kv.ContainsKey(recurrenceId))
                return _kv[recurrenceId];

            return string.Empty;
        }
    }

    public struct DbConstants
    {
        public const string OBJECTS_XPATH = "//WebObject";
        public const string XML_FILE = "WebObject.xml";
        public const string OBJECT_NAME_NODE = "Name";
        public const string OBJECT_IDENTITY_NODE = "IdentityColumn";

        public const string DB_PROVIDER_PATH_KEY = "DbProvider.Path";

        public const string CREATE_FILTER_WC = "*.create.sql"; // Wild Card
        public const string CREATE_FILTER = ".create.sql";

        public const string DROP_FILTER_WC = "*.drop.sql";
        public const string DROP_FILTER = ".drop.sql";
    }

    public class AccountStatus
    {
        public const int DRAFT = -1;
        public const int PENDING = 0;
        public const int ACTIVE = 1;
        public const int DISABLED = 2;

        private static readonly Dictionary<int, string> _values;
        static AccountStatus()
        {
            _values = new Dictionary<int, string> {
                {DRAFT, "Draft"},
                {PENDING, "Pending"},
                {ACTIVE, "Active"},
                {DISABLED, "Disabled"},
            };
        }

        public static Dictionary<int, string> Values
        {
            get { return _values; }
        }

        public static string GetText(int status)
        {
            if (_values.ContainsKey(status))
                return _values[status];

            return string.Empty;
        }
    }

    public class AccountConstants
    {
        public const char AccountDelimiter = ';';
        public const char AccountSplitter = '\\';

        public const string ADD_CMD = "/ADD:";
        public const string REMOVE_CMD = "/REMOVE:";

        public const string ModeKey = "Mode";
        public const string ModeLogOff = "LogOff";
        public const string ModeForgot = "Forgot";
        public const string ModeActivate = "Activate";

        public const string LoginHomeUrl = "LoginHomeUrl";
        public const string SignUpUrl = "SignUpUrl";
        public const string FirstLoginUrl = "FirstLoginUrl";
        public const string LoginUrl = "LoginUrl";
        public const string UpdatePasswordUrl = "UpdatePasswordUrl";
        public const string ForgotPasswordUrl = "ForgotPasswordUrl";

        public const string EnableRememberMe = "EnableRememberMe";
        public const string RememberMeText = "RememberMeText";
        public const string SignOutRedirect = "SignOutRedirect";
        public const string EnableExternalAccounts = "EnableExternalAccounts";
        public const string DisableLocalRegistration = "DisableLocalRegistration";
        public const string LoginInfoSessionKey = "UserProvider.LoginInfo";

        public const string OtpCacheKey = "OtpCache";
        public const string OtpVerify = "OtpVerify";

        public const int DefaultExternalProvider = 1;
        public const string DefaultProviderName = "Local Account";

        public const string GROUP_PREFIX = @"GROUP\";
        public const string USER_PREFIX = @"USER\";

        public static readonly string[] BuiltIn = new string[] { "Administrators", "Content Contributors", "Guests", "Site Managers", "Users", "Service Accounts" };
    }

    /// <summary>
    /// This is a runtime state only. Gets set after loading of cache.
    /// </summary>
    public enum CacheStatus
    {
        Empty = 0,
        Partial = 1,
        Full = 2
    }

    public class TemplateEngineTypes
    {
        public const int ASPX = 1;
        public const int Razor = 2;

        public static int GetValue(string ext)
        {
            return ext.EndsWith("ascx", StringComparison.InvariantCultureIgnoreCase) ? ASPX : Razor;
        }
    }

    public struct WConstants
    {

        public const int Active = 1;
        public const int NotActive = 0;
        public const int Approved = 1;
        public const int NotApproved = 0;

        public const int ID_ANY = -2;
        public const int NULL_ID = -1;

        public const string WebRootUrl = "/";
        public const string CentralID = "Central";
        public const string SessionId = "SessionId";
        public const int CentralPartId = 78;
        public const string Open = "Open";
        public const string Load = "Load";

        public const string Arrow = "&#9658;";
        public const string LinkCancel = "javascript: return false;";

        public const string HIDDEN_PAGE_ID = "__hidPageId";
        //public const string SourceParameter = "Source";
        public const string AbsoluteAccessDeniedPage = "/Static/Access-Denied/";

        public const string AccessDeniedPage = "AccessDeniedPage";
        public const string AccessDeniedUrl = "AccessDeniedUrl";

        public const string AbsoluteDynamicRootPath = "/Default.aspx";
        public const string RelativeDynamicRootPath = "~/Default.aspx";
        public const string RelativeRazorRootPath = "~/Default.cshtml";

        public const string AbsoluteStaticRootPath = "/Static.aspx";
        public const string RelativeStaticRootPath = "~/Static.aspx";

        public const string AbsoluteBlankPath = "/Static/Blank/";
        public const string RelativeBlankPath = "~/Static/Blank/";

        public const string AbsoluteErrorPage = "/Error.aspx";

        public const string ReplacerString = "\uFFFC";
        public const char ReplacerChar = '\uFFFC';
        public const string WBREAK = "<br/>";

        public const string FormatStringKey = "FormatString";

        public const int PreviewChars = 50;
        public const int ElementRankInterval = 15;

        public const string MainFrame = "frameMain";

        public const string CustomDeleteCmd = "Custom_Delete";

        public const string TAB = "\u00a0\u00a0";
        public const string BULLET = "\u2022\u00a0";

        public const string ANONYMOUS = "ANONYMOUS";
        public const char IPDelimiter = ';';
        public const char EmailSeparator = ',';
        public const char CHAR_SPACE = ' ';
        public const int NullData = -1;

        public const string AccountActive = "Active";
        public const string AccountInActive = "Activation Required";

        public static readonly DateTime DateTimeMinValue = new DateTime(1800, 1, 1);
        public static readonly DateTime ExpiredPasswordDate = new DateTime(1900, 1, 1);

        public static readonly char[] InvalidPathChars = new char[]{
                ' ', '/', '?', '\"', '\'', '<', '>', '[', ']', '&', '@', '\\'};

        public static string ParameterSetKey = "ParameterSet";

        public const string ASPX = ".aspx";

        public const string CommentViewAscx = "~/Content/Parts/Common/Comments.ascx";
        public const string ResourcePhysicalPathStart = "~/Content/Assets/Resources/";
        public const string ResourcePermalinkFormat = "/content/handlers/Resource.ashx?Id={0}&{1}&{2}";

        public const string NoPhotoThumb = "/content/assets/images/nophoto.png";
        public const string NoPhotoFile = "nophoto.png";
        public const string AdminDataFolder = "/Content/Admin/Data/";

        public const string MIME = "MIME";
        public const string DebugTemplateId = "___tid";
        public const string DEFAULT_THEME_TEMPLATE = "~/Content/Themes/Default/Default.ascx";

        public static string[] UrlConsts = { "/default.aspx", "/static.aspx" };

        public const string PART_RENDER_PATH_FORMAT = "~/Content/Parts/{0}/{1}";
    }

    public struct SubscriptionModes
    {
        public const int AllGroups = 0;
        public const int SpecificGroup = 1;
        public const int DashboardInstance = 2;
        public const int GroupsPlusInstance = 3;
    }

    public struct WebPageKeys
    {
        public const string Id = "Id";
        public const string Name = "Name";
        public const string Url = "Url";
        public const string AbsoluteUrl = "AbsoluteUrl";
        public const string RelativeUrl = "RelativeUrl";
        public const string Identity = "Identity";
        public const string Active = "Active";
        public const string Rank = "Rank";
        public const string Title = "Title";
    }

    public struct PublicAccessCheckResult
    {
        public const int Denied = 0;
        public const int NotLoggedIn = 1;
        public const int Granted = 100;
    }

    public struct WebBoolean
    {
        public const int Yes = 1;
        public const int No = 0;
    }

    public struct WebColumns
    {
        public const string _SiteId = "_SiteId";
        public const string _PageId = "_PageId";
        public const string PageIdInternal = "___pid";

        public const string Id = "Id";
        public const string OwnerId = "OwnerId";
        public const string OfficeId = "OfficeId";
        public const string Allow = "Allow";
        public const string SubscriptionId = "SubscriptionId";
        public const string GroupId = "GroupId";
        public const string ParameterId = "ParameterId";
        public const string ParameterSetId = "ParameterSetId";
        public const string PartId = "PartId";
        public const string UserId = "UserId";
        public const string TemplateId = "TemplateId";
        public const string TemplatePanelId = "TemplatePanelId";
        public const string SiteId = "SiteId";
        public const string ParentSiteId = "ParentSiteId";
        public const string Size = "Size";
        public const string TextResourceId = "TextResourceId";
        public const string PageId = "PageId";
        public const string MasterPageId = "MasterPageId";
        public const string PageElementId = "PageElementId";
        public const string PartAdminId = "PartAdminId";
        public const string ObjectId = "ObjectId";
        public const string PartControlId = "PartControlId";
        public const string ParentId = "ParentId";
        public const string PartControlTemplateId = "PartControlTemplateId";
        public const string SkinId = "SkinId";
        public const string ThemeId = "ThemeId";
        public const string PartConfigId = "PartConfigId";
        public const string PermissionId = "PermissionId";
        public const string PermissionSetId = "PermissionSetId";
        public const string RoleId = "RoleId";
        public const string RegistryId = "RegistryId";
        public const string RecordId = "RecordId";
        public const string PagePanelId = "PagePanelId";
        public const string GlobalPolicyId = "GlobalPolicyId";
        public const string FolderId = "FolderId";

        public const string Name = "Name";
        public const string Status = "Status";
        public const string FirstName = "FirstName";
        public const string LastName = "LastName";
        public const string MiddleName = "MiddleName";
        public const string Email = "Email";
        public const string Value = "Value";
        public const string Content = "Content";
        public const string Description = "Description";
        public const string Rank = "Rank";
        public const string Active = "Active";
        public const string Visible = "Visible";
        public const string UserName = "UserName";
        public const string Password = "Password";
        public const string DateCreated = "DateCreated";
        public const string DateModified = "DateModified";
        public const string Identity = "Identity";
        public const string Title = "Title";
        public const string Locale = "Locale";
        public const string Work = "Work";
    }

    public struct GlobalPolicies
    {
        public const int WebSiteManagement = 1;
        public const int Administration = 2;
        public const int WebPartManagement = 3;
        public const int SecurityManagement = 4;
    }

    public struct WebObjects
    {
        public const int WebAddress = 104;
        //public const int WebMasterPageItem = 2;
        public const int WebPage = 3;
        public const int WebSite = 4;
        public const int WebMasterPage = 5;
        public const int WebSiteIdentity = 16;
        public const int WebFolder = 23;
        public const int WebContent = 6;
        public const int WebObject = 9;
        public const int WebTextResource = 20;
        public const int WebPageElement = 22;
        public const int WebPagePanel = 35;
        public const int WebGroup = 38;
        public const int WebUser = 21;
        public const int WebRole = 25;
        public const int WebUserGroup = 27;
        public const int WebPartAdmin = 10;
        public const int WebPartConfig = 11;
        public const int WebPartControl = 12;
        public const int WebPartControlTemplate = 13;
        public const int WebPart = 14;
        public const int WebTemplatePanel = 18;
        public const int WebTemplate = 19;
        public const int WebTheme = 90;
        public const int WebSkin = 91;
        public const int WebParameter = 62;
        public const int WebParameterSet = 95;
        public const int WebPermission = 26;
        public const int USState = 53;
        public const int Country = 52;
        public const int WebObjectContent = 7;
        public const int WebObjectHeader = 8;
        public const int WebObjectSecurity = 37;
        public const int WebRegistry = 15;
        public const int WebConstant = 1;
        public const int WebPermissionSet = 50;
        public const int ArticleLocation = 43;
        public const int WebGlobalPolicy = 47;
        public const int WebFile = 88;
        public const int WebSubscription = 93;
        public const int WebOffice = 94;
        public const int WebJob = 101;
        public const int WebShare = 130;
        public const int WebMessageQueue = 109;
    }

    public struct WebContentEnum
    {
        public const string SQL_GET = "WebContent_Get";
        public const string SQL_SET = "WebContent_Set";
        public const string SQL_DEL = "WebContent_Del";

        public const string ContentId = "ContentId";
        public const string Title = "Title";
        public const string Content = "Content";
        public const string VersionOf = "VersionOf";
        public const string VersionNo = "VersionNo";
    }

    //public struct WebObjectHeaderEnum
    //{
    //    public const string ObjectHeaderId = "ObjectHeaderId";
    //    public const string ObjectId = "ObjectId";
    //    public const string RecordId = "RecordId";
    //    public const string TextResourceId = "TextResourceId";
    //}

    public class WebPublicAccess
    {
        private static readonly Dictionary<int, string> _values;

        static WebPublicAccess()
        {
            _values = new Dictionary<int, string> {
                {128, "Inherit"},
                {1, "Anonymous Access"},
                {2, "Account Authentication"},
                {4, "IP Address Authentication"},
                {8, "Account Or IP Address Authentication"},
                {16, "Account And IP Address Authentication"}
            };
        }

        public const int Inherit = 128;
        public const int Anonymous = 1;
        public const int Account = 2;
        public const int IPAddress = 4;
        public const int AccountOrIPAddress = 8;
        public const int AccountAndIPAddress = 16;
        public const int AllIPAddressExceptEntries = 32;
        public const int AllAccountExceptEntries = 64;

        public static bool IsEnabled(int access, int securityCheck)
        {
            return (access & securityCheck) > 0;
        }

        public static Dictionary<int, string> Values
        {
            get { return _values; }
        }
    }

    public struct WebMgmtAccess
    {
        private static readonly Dictionary<int, string> _values;

        static WebMgmtAccess()
        {
            _values = new Dictionary<int, string> {
                {0, "Inherit"},
                {1, "Specific"}
            };
        }

        public static Dictionary<int, string> Values
        {
            get { return _values; }
        }

        public const int Inherit = 0;
        public const int Specific = 1;
    }

    public struct Permissions
    {
        public const int None = -1;
        public const int FullControl = 1;
        public const int ManageInstance = 2;
        public const int ManageContent = 3;
        public const int PublicWrite = 4;
        public const int PublicRead = 5;
        public const int AuthorWebSite = 6;
        public const int UsersManagement = 7;
        public const int GroupsManagement = 8;
    }

    public struct WPID
    {
        public const string ShortUrlEdit = "ShortUrlEdit";
    }

    public struct CentralPages
    {
        public const string HeaderPanel = CENTRAL + "/HeaderPanel/";

        public const string CONTENT = "/Content";
        public const string CENTRAL = "/Central";
        public const string SECURITY = "/Security";
        public const string APP = "/Part";
        public const string APPS = "/Parts";
        public const string TOOLS = "/Tools";
        public const string MISC = "/Misc";
        public const string TEMPLATE = "/Template";

        /// <summary>
        /// Central Admin's Home
        /// </summary>
        public const string CentrlHome = CENTRAL + "/Dashboard/";

        /// <summary>
        /// Loader Main: WebParts/Central/Default.aspx
        /// </summary>
        public const string LoaderMain = CONTENT + APPS + CENTRAL + "/";
        public const string LoaderRazor = CONTENT + APPS + CENTRAL + "/Loader";
        public const string LoaderMain2 = "/Content/Parts/Central/default.aspx";
        public const string Setup = CONTENT + "/Setup.aspx";

        public const string TreePanel = CENTRAL + "/TreePanel/";
        public const string WebSiteTree = CENTRAL + "/WebSiteTree/";
        public const string SiteMap = CENTRAL + "/SiteMap/";

        public const string Login = "/Central/Security/Login/";
        public const string CreateUser = "/Central/Security/CreateUser/";
        public const string ChangePassword = "/Central/Security/ChangePassword/";
        public const string WebUserGroups = "/Central/Security/WebUserGroups/";
        public const string UserProfile = "/Central/Security/UserProfile/";
        public const string WebGroups = "/Central/Security/WebGroups/";
        public const string WebGroup = "/Central/Security/WebGroup/";
        public const string WebGroupHome = "/Central/Security/WebGroupHome/";
        public const string WebGroupUsers = "/Central/Security/WebGroupUsers/";
        public const string WebRoles = "/Central/Security/WebRoles/";
        public const string WebRoleHome = "/Central/Security/WebRoleHome/";
        public const string WebRole = "/Central/Security/WebRole/";
        public const string WebSecurity = "/Central/Security/WebSecurity/";
        public const string WebSecurityTree = "/Central/Security/WebSecurityTree/";
        public const string WebPermissions = "/Central/Security/WebPermissions/";
        public const string WebUserHome = "/Central/Security/WebUserHome/";
        public const string WebUserRoles = "/Central/Security/WebUserRoles/";
        public const string WebUserPermissions = "/Central/Security/WebUserPermissions/";
        public const string WebUsers = "/Central/Security/WebUsers/";
        public const string WebGlobalPolicy = "/Central/Security/WebGlobalPolicy/";
        public const string WebGlobalPolicyHome = "/Central/Security/WebGlobalPolicyHome/";

        public const string WebPart = "/Central/Part/WebPart/";
        public const string WebPartAdmin = "/Central/Part/WebPartAdmin/";
        public const string WebPartAdminEntry = "/Central/Part/WebPartAdminEntry/";
        public const string WebPartConfig = "/Central/Part/WebPartConfig/";
        public const string WebPartControl = "/Central/Part/WebPartControl/";
        public const string WebPartControls = "/Central/Part/WebPartControls/";
        public const string WebPartControlHome = "/Central/Part/WebPartControlHome/";
        public const string WebPartControlTemplate = "/Central/Part/WebPartControlTemplate/";
        public const string WebPartControlTemplateHome = "/Central/Part/WebPartControlTemplateHome/";
        public const string WebPartControlTemplates = "/Central/Part/WebPartControlTemplates/";
        public const string WebPartTemplatePanel = "/Central/Part/WebPartTemplatePanel/";
        public const string WebPartTemplatePanels = "/Central/Part/WebPartTemplatePanels/";
        public const string WebPartHome = "/Central/Part/WebPartHome/";
        public const string WebParts = "/Central/Part/WebParts/";
        public const string WebPartConfigEntry = "/Central/Part/WebPartConfigEntry/";
        public const string WebPartAdminHome = "/Central/Part/WebPartAdminHome/";
        public const string WebPartPreview = "/Central/Part/WebPartPreview/";
        public const string WebPartManagement = "/Central/Part/Management/";

        // Tools
        public const string WebResourceManager = "/Central/Tools/WebResourceManager/";
        public const string WebRegistry = "/Central/Tools/WebRegistry/";
        public const string WebRegistryEntry = "/Central/Tools/WebRegistryEntry/";
        public const string TextEditor = "/Central/Tools/TextEditor/";
        public const string FileManager = "/Central/Tools/FileManager/";
        public const string SmtpAnalyzer = "/Central/Tools/SmtpAnalyzer/";
        public const string WebFolder = "/Central/Tools/WebFolder/";
        public const string WebDataExplorer = "/Central/Tools/WebDataExplorer/";
        public const string QueryAnalyzer = "/Central/Tools/QueryAnalyzer/";
        public const string WebDataRows = "/Central/Tools/WebDataRows/";
        public const string WebToolsTree = "/Central/Tools/WebToolsTree/";
        public const string ShortUrlManager = "/Central/Tools/ShortUrlManager/";
        public const string ShortUrlEdit = "/Central/Tools/ShortUrlEdit/";
        public const string WebObjectManager = "/Central/Tools/WebDataStoreManager/";
        //public const string EventManager = "/Central/Tools/EventManager/";

        public const string WebResource = "/Central/Misc/WebResource/";
        public const string WebResources = "/Central/Misc/WebResources/";
        public const string WebParameter = "/Central/Misc/WebParameter/";
        public const string WebParameters = "/Central/Misc/WebParameters/";
        public const string WebParameterSet = "/Central/Misc/WebParameterSet/";
        public const string WebParameterSetHome = "/Central/Misc/WebParameterSetHome/";
        public const string WebParameterSets = "/Central/Misc/WebParameterSets/";

        public const string WebIdentity = "/Central/Site/WebIdentity/";
        public const string WebIdentities = "/Central/Site/WebIdentities/";
        public const string WebMasterPage = "/Central/Site/WebMasterPage/";
        public const string WebMasterPageHome = "/Central/Site/WebMasterPageHome/";
        public const string WebMasterPages = "/Central/Site/WebMasterPages/";
        public const string WebPage = "/Central/Site/WebPage/";
        public const string WebPageElement = "/Central/Site/WebPageElement/";
        public const string WebPageElements = "/Central/Site/WebPageElements/";
        public const string WebPageElementHome = "/Central/Site/WebPageElementHome/";
        public const string WebPageHome = "/Central/Site/WebPageHome/";
        public const string WebPagePanel = "/Central/Site/WebPagePanel/";
        public const string WebPagePanels = "/Central/Site/WebPagePanels/";
        public const string WebPagePanelHome = "/Central/Site/WebPagePanelHome/";
        public const string WebPages = "/Central/Site/WebPages/";
        public const string WebSite = "/Central/Site/WebSite/";
        public const string WebSiteHeaders = "/Central/Site/WebSiteHeaders/";
        public const string WebSiteHome = "/Central/Site/WebSiteHome/";
        public const string WebSiteManage = "/Central/Site/WebSiteManage/";
        public const string WebSites = "/Central/Site/WebSites/";
        public const string WebChildSites = "/Central/Site/WebChildSites/";
        public const string WebChildPages = "/Central/Site/WebChildPages/";

        public const string WebLinkedParts = "/Central/WebLinkedParts/";
        public const string WebOpen = "/Central/WebOpen/";
        public const string WebSystemDashboard = "/Central/Dashboard/";

        public const string WebTemplate = "/Central/Template/WebTemplate/";
        public const string WebTemplateEditor = "/Central/Template/WebTemplateEditor/";
        public const string WebTemplateHome = "/Central/Template/WebTemplateHome/";
        public const string WebTemplatePanel = "/Central/Template/WebTemplatePanel/";
        public const string WebTemplatePanels = "/Central/Template/WebTemplatePanels/";
        public const string WebTemplates = "/Central/Template/WebTemplates/";
        public const string WebSkins = "/Central/Template/WebSkins/";
        public const string WebSkin = "/Central/Template/WebSkin/";
        public const string WebSkinHome = "/Central/Template/WebSkinHome/";
        public const string WebThemes = "/Central/Template/WebThemes/";
        public const string WebTheme = "/Central/Template/WebTheme/";
        public const string WebThemeHome = "/Central/Template/WebThemeHome/";

        public const string SubscriptionManager = "/Central/Misc/SubscriptionManager/";
        public const string WebOfficeHome = "/Central/Misc/Web-Office-Home/";
        public const string WebOffices = "/Central/Misc/Web-Offices/";
        public const string WebOffice = "/Central/Misc/Web-Office/";
        public const string WebOfficeTree = "/Central/Misc/Web-Office-Tree/";
        public const string WebAddresses = "/Central/Misc/WebAddresses/";
    }

    public struct CacheTypes
    {
        private static readonly Dictionary<int, string> _values;

        static CacheTypes()
        {
            _values = new Dictionary<int, string> {
                {-1, "None"},
                {2, "Partial"},
                {1, "Full"}
            };
        }

        public static string GetText(int key)
        {
            if (_values.ContainsKey(key))
                return _values[key];

            return string.Empty;
        }

        public static Dictionary<int, string> Values
        {
            get { return _values; }
        }

        public const int Full = 1;
        public const int Partial = 2;
        public const int None = -1;
    }

    public struct AccessTypes
    {
        public const int Private = 1;
        public const int Relative = 2;
        public const int Public = 3;
    }

    public struct PanelUsage
    {
        /// <summary>
        /// Inherit all elements from MasterPage. Exclude Page elements.
        /// </summary>
        public const int Inherit = -1;

        /// <summary>
        /// Inherit all elements from MasterPage and combine Page elements.
        /// </summary>
        public const int Add = 0;

        /// <summary>
        /// Use only Page elements.
        /// </summary>
        public const int Override = 1;

        public static string ToString(int usage)
        {
            switch (usage)
            {
                case -1:
                    return "Inherit";

                case 0:
                    return "Add";

                case 1:
                    return "Override";

                default:
                    return "Unknown";
            }
        }

        public static string GetText(int usage)
        {
            return ToString(usage);
        }
    }

    public struct MasterPageOption
    {
        public const int None = -3;
        public const int ParentPage = -2;
        public const int WebSite = -1;
    }

    public struct DesignerConstants
    {
        public const int PreviewMode = -2;
        public const int AllPanels = -1;
    }

    public struct SystemGroups
    {
        public const string Administrators = "Administrators";
        public const string SiteManagers = "Site Managers";
        public const string Users = "Users";

        public const int ADMINS_GROUP_ID = 1;
        public const int USERS_GROUP_ID = 2;
        public const int MANAGERS_GROUP_ID = 3;
    }

    public struct UserTypes
    {
        public const int User = 0;
        public const int Administrator = 1;
        public const int SiteManager = 2;
    }


    public struct WebContentTemplateKeys
    {
        public const string PrimaryContent = "Content";
    }

    public struct GenderTypes
    {
        private static readonly Dictionary<char, string> _values;

        static GenderTypes()
        {
            _values = new Dictionary<char, string> {
                {Unspecified, ""},
                {Male, "Male"},
                {Female, "Female"}
            };
        }

        public static string GetText(char key)
        {
            if (_values.ContainsKey(key))
                return _values[key];

            return string.Empty;
        }

        public const char Unspecified = 'U';
        public const char Male = 'M';
        public const char Female = 'F';

        public const int UnspecifiedId = -1;
        public const int MaleId = 1;
        public const int FemaleId = 0;
    }

    public struct MaritalStatus
    {
        private static readonly Dictionary<int, string> _values;

        static MaritalStatus()
        {
            _values = new Dictionary<int, string> {
                {UnspecifiedId, ""},
                {SingleId, "Single"},
                {MarriedId, "Married"},
                {WidowedId, "Widowed"}
            };
        }

        public static string GetText(int key)
        {
            if (_values.ContainsKey(key))
                return _values[key];

            return string.Empty;
        }

        public const int UnspecifiedId = -1;
        public const int SingleId = 1;
        public const int MarriedId = 2;
        public const int WidowedId = 3;
    }

    public struct EventLogConstants
    {
        public const string StartSession = "START SESSION";
        public const string EndSession = "END SESSION";
    }
}
