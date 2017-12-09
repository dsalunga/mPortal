using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;
using WCMS.Framework.Net;

namespace WCMS.Framework
{
    public enum SystemEnvironment
    {
        NULL,
        DEV,
        UAT,
        PROD,
        DEV_ISOLATED
    }

    public static class WConfig
    {
        public const string RelativeTemplatePath = "~/Content/Themes";
        public const string PAGE_EXT_KEY = "WCMS:PageExt";
        public const string AGENT_ENABLED = "WCMS:AgentEnabled";
        public const string ENVIRONMENT = "WCMS:Environment";
        public const string USER_PHOTO_PATH = "WCMS.UserPhotoPath";
        public const string UAT = "UAT";
        public const string PROD = "PROD";
        public const string DEV_ISOLATED = "DEV_ISOLATED";
        public const string SYSTEM_NODE = "System";
        public const string DEBUGGING_NODE = "Debugging";
        public const string FILE_CACHE_PATH = "WCMS:FileCachePath";
        public const string MIN_DISK_FREE_MB = "WCMS:MinDiskFreeMB";
        public const string ALLOW_CACHE = "WCMS:AllowCache";

        private static int AUTO_LOGIN_ID = -1;
        private static int PANEL_EXPANDED_ID = -1;
        private static int ENABLE_INLINE_EDITOR_ID = -1;
        private static int RESOURCES_EXTERNAL_MODE = -1;
        private static int ALLOW_FULL_CACHE = -1;
        //private static int ENABLE_PERF_LOGGING_ID = -1;

        static WConfig()
        {
            _defaultLoginPage = null;
            WebRegistry.Updated += RegistryNodeUpdated;
        }

        private static void RegistryNodeUpdated(object sender, WebRegistryUpdateEventArgs args)
        {
            var id = args.UpdatedNode.Id;
            if (id == AUTO_LOGIN_ID)
            {
                _autoLogin = null;
                var value = AutoLogin;
            }
            else if (id == PANEL_EXPANDED_ID)
            {
                _panelExpanded = null;
                var value = PanelExpanded;
            }
            else if (id == ENABLE_INLINE_EDITOR_ID)
            {
                _enableInlineEditor = null;
                var value = EnableInlineEditor;
            }
            else if (id == RESOURCES_EXTERNAL_MODE)
            {
                _resExternalMode = null;
                var value = ResourcesExternalMode;
            }
            else if (id == ALLOW_FULL_CACHE)
            {
                _allowFullCache = null;
                var value = AllowCache;
            }
            //else if (id == ENABLE_PERF_LOGGING_ID)
            //{
            //    _enablePerfLogging = null;

            //    var value = EnablePerfLogging;
            //}
        }

        public static T GetEnvValue<T>(T prod, T dev)
        {
            return WConfig.Environment != SystemEnvironment.PROD ? dev : prod;
        }

        private static bool? _hasPageExt;
        public static bool HasPageExt
        {
            get
            {
                return (_hasPageExt ?? (_hasPageExt = !string.IsNullOrEmpty(PageExt))).Value;
            }
        }

        public static string EvalPageExt { get { return HasPageExt ? PageExt : "/"; } }

        private static bool? _agentEnabled;
        public static bool AgentEnabled { get { return (_agentEnabled ?? (_agentEnabled = ConfigHelper.GetBool(AGENT_ENABLED, true))).Value; } }

        private static string _pageExt;
        public static String PageExt { get { return _pageExt ?? (_pageExt = ConfigHelper.Get(PAGE_EXT_KEY)); } }

        private static SystemEnvironment _sysEnv = SystemEnvironment.NULL;
        public static SystemEnvironment Environment
        {
            get
            {
                if (_sysEnv == SystemEnvironment.NULL)
                {
                    var sysEnv = ConfigHelper.Get(ENVIRONMENT).ToUpper();
                    switch (sysEnv)
                    {
                        case UAT:
                            _sysEnv = SystemEnvironment.UAT;
                            break;

                        case PROD:
                            _sysEnv = SystemEnvironment.PROD;
                            break;

                        case DEV_ISOLATED:
                            _sysEnv = SystemEnvironment.DEV_ISOLATED;
                            break;

                        default:
                            _sysEnv = SystemEnvironment.DEV;
                            break;
                    }
                }

                return _sysEnv;
            }
        }

        private static WebRegistry _systemNode;
        public static WebRegistry SystemNode
        {
            get
            {
                if (_systemNode == null)
                    _systemNode = WebRegistry.Get(SYSTEM_NODE, -1);
                return _systemNode;
            }
        }

        private static WebRegistry _debuggingNode;
        public static WebRegistry DebuggingNode
        {
            get
            {
                if (_debuggingNode == null)
                    _debuggingNode = SystemNode.SelectSingleNode(DEBUGGING_NODE);
                return _debuggingNode;
            }
        }

        private static SmsConfig _smsConfigNode;
        public static SmsConfig SMSConfig
        {
            get
            {
                if (_smsConfigNode == null)
                {
                    _smsConfigNode = new SmsConfig();
                    var configValue = SystemNode.SelectSingleNodeValue("SMSConfig");
                    if (!string.IsNullOrEmpty(configValue))
                    {
                        var xdoc = new XmlDocument();
                        xdoc.LoadXml(configValue);

                        _smsConfigNode.IsFilterBlock = DataHelper.GetBool(XmlUtil.GetAttributeValue(xdoc, "IsFilterBlock"));
                        _smsConfigNode.RecipientFilter = XmlUtil.GetAttributeValue(xdoc, "RecipientFilter");
                    }
                }

                return _smsConfigNode;
            }
        }

        private static string _defaultLoginPage;
        public static string DefaultLoginPage
        {
            get
            {
                if (_defaultLoginPage == null)
                {
                    _defaultLoginPage = SystemNode.SelectSingleNodeValue("DefaultLoginPage");
                    if (_defaultLoginPage == null)
                        _defaultLoginPage = string.Empty;
                }
                return _defaultLoginPage;
            }
        }

        private static string _defaultDataProvider;
        public static string DefaultDataProvider
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultDataProvider))
                    _defaultDataProvider = SystemNode.SelectSingleNodeValue("DefaultDataProvider");
                return _defaultDataProvider;
            }
        }

        private static string _systemName = null;
        public static string SystemName
        {
            get { return _systemName ?? (_systemName = SystemNode.SelectSingleNodeValue("Name")); }
        }

        private static string _tempFolder = null;
        public static string TempFolder
        {
            get { return _tempFolder ?? ((_tempFolder = SystemNode.SelectSingleNodeValue("TempFolder")) ?? WebHelper.TEMP_DATA_PATH); }
        }

        private static string _baseAddress = null;
        public static string BaseAddress
        {
            get
            {
                if (_baseAddress == null)
                {
                    _baseAddress = SystemNode.SelectSingleNodeValue("BaseAddress");
                    if (_baseAddress != null)
                        _baseAddress = _baseAddress.TrimEnd('/');
                }

                return _baseAddress;
            }
        }

        private static string _attachmentBasePath = null;
        public static string AttachmentBasePath
        {
            get
            {
                if (_attachmentBasePath == null)
                {
                    _attachmentBasePath = SystemNode.SelectSingleNodeValue("AttachmentBasePath");
                    if (_attachmentBasePath != null)
                        _attachmentBasePath = _attachmentBasePath.TrimEnd('/');
                }

                return _attachmentBasePath;
            }
        }

        private static bool? _resExternalMode = null;
        public static bool ResourcesExternalMode
        {
            get
            {
                if (_resExternalMode == null)
                {
                    var node = SystemNode.SelectSingleNode("Resources.ExternalMode");
                    if (node != null)
                    {
                        _resExternalMode = DataHelper.GetBool(node.Value, false);
                        RESOURCES_EXTERNAL_MODE = node.Id;
                    }
                    else
                    {
                        _resExternalMode = false;
                    }
                }

                return _resExternalMode.Value;
            }
        }

        private static string _httpSmsUrl = null;
        public static string HttpSmsUrl
        {
            get { return _httpSmsUrl ?? (_httpSmsUrl = SystemNode.SelectSingleNodeValue("HttpSmsUrl")); }
        }

        private static WSite _defaultSite = null;
        public static WSite DefaultSite
        {
            get
            {
                if (_defaultSite == null)
                    _defaultSite = WSite.Get(DataHelper.GetId(SystemNode.SelectSingleNodeValue("DefaultSite")));

                return _defaultSite;
            }
        }

        private static string _subjectPrefix = null;
        public static string SubjectPrefix
        {
            get { return _subjectPrefix ?? (_subjectPrefix = SystemNode.SelectSingleNodeValue("SMTP/SubjectPrefix")); }
        }

        private static bool? _enableLogging = null;
        public static bool EnableLogging
        {
            get
            {
                if (_enableLogging == null)
                    _enableLogging = DataHelper.GetBool(DebuggingNode.SelectSingleNodeValue("EnableLogging"), false);
                return _enableLogging.Value;
            }
        }

        private static bool? _autoLogin = null;
        public static bool AutoLogin
        {
            get
            {
                if (_autoLogin == null)
                {
                    var node = DebuggingNode.SelectSingleNode("AutoLogin");
                    if (node != null)
                    {
                        _autoLogin = DataHelper.GetBool(node.Value, false);
                        AUTO_LOGIN_ID = node.Id;
                    }
                    else
                    {
                        _autoLogin = false;
                    }
                }
                return _autoLogin.Value;
            }
        }

        public static bool EnablePerfLogging { get; set; }

        //private static bool? _enablePerfLogging = null;
        //public static bool EnablePerfLogging
        //{
        //    get
        //    {
        //        if (_enablePerfLogging == null)
        //        {
        //            var node = DebuggingNode.SelectSingleNode("EnablePerfLogging");
        //            if (node != null)
        //            {
        //                _enablePerfLogging = DataHelper.GetBool(node.Value, false);

        //                ENABLE_PERF_LOGGING_ID = node.Id;
        //            }
        //            else
        //            {
        //                _enablePerfLogging = false;
        //            }
        //        }

        //        return _enablePerfLogging.Value;
        //    }
        //}

        private static bool? _panelExpanded = null;
        public static bool PanelExpanded
        {
            get
            {
                if (_panelExpanded == null)
                {
                    var node = SystemNode.SelectSingleNode("Designer.PanelExpanded");
                    if (node != null)
                    {
                        _panelExpanded = DataHelper.GetBool(node.Value, true);
                        PANEL_EXPANDED_ID = node.Id;
                    }
                    else
                    {
                        _panelExpanded = false;
                    }
                }
                return _panelExpanded.Value;
            }
        }

        private static bool? _enableInlineEditor = null;
        public static bool EnableInlineEditor
        {
            get
            {
                if (_enableInlineEditor == null)
                {
                    var node = SystemNode.SelectSingleNode("Central.EnableInlineEditor");
                    if (node != null)
                    {
                        _enableInlineEditor = DataHelper.GetBool(node.Value, false);
                        ENABLE_INLINE_EDITOR_ID = node.Id;
                    }
                    else
                    {
                        _enableInlineEditor = false;
                    }
                }
                return _enableInlineEditor.Value;
            }
        }

        private static bool? _allowFullCache = null;
        public static bool AllowCache
        {
            get
            {
                if (_allowFullCache == null)
                {
                    var wcfg = ConfigHelper.Get(ALLOW_CACHE);
                    if (!string.IsNullOrEmpty(wcfg))
                    {
                        _allowFullCache = DataHelper.GetBool(wcfg, false);
                    }
                    else
                    {
                        var node = SystemNode.SelectSingleNode("AllowCache");
                        if (node != null)
                        {
                            _allowFullCache = DataHelper.GetBool(node.Value, false);
                            ALLOW_FULL_CACHE = node.Id;
                        }
                        else
                        {
                            _allowFullCache = false;
                        }
                    }
                }
                return _allowFullCache.Value;
            }
        }

        private static string _userPhotoPath;
        public static string UserPhotoPath
        {
            get
            {
                if (_userPhotoPath == null)
                    _userPhotoPath = ConfigHelper.Get(USER_PHOTO_PATH);
                return _userPhotoPath;
            }
        }

        private static string _fileCachePath;
        public static string FileCachePath
        {
            get
            {
                if (_fileCachePath == null)
                {
                    _fileCachePath = ConfigHelper.Get(FILE_CACHE_PATH);
                    if(string.IsNullOrEmpty(_fileCachePath))
                        _fileCachePath = @"C:\WCMS-Cache";
                }
                return _fileCachePath;
            }
        }

        private static int _minDiskFreeMB = -1;
        public static int MinDiskFreeMB
        {
            get
            {
                if (0 > _minDiskFreeMB)
                    _minDiskFreeMB = DataHelper.GetInt32(ConfigHelper.Get(MIN_DISK_FREE_MB), 10240);
                return _minDiskFreeMB;
            }
        }
    }
}
