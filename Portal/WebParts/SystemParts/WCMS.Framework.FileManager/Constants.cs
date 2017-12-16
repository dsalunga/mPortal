using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public class FileActivities
    {
        public const int CheckOut = 0;
        public const int CheckIn = 1;
        //public const int CancelCheckOut = 2;
        public const int Upload = 2;
        public const int Delete = 3;

        private static Dictionary<int, string> _kv;

        static FileActivities()
        {
            _kv = new Dictionary<int, string>()
            {
                {0, "Check-Out"},
                {1, "Check-In"},
                {2, "Upload"},
                {3, "Delete"}
            };
        }

        public static Dictionary<int, string> KeyValues
        {
            get { return _kv; }
        }
    }

    public class FileManagerConstants
    {
        public const string StorageQuotaKey = "StorageQuota";
        public const string RootPathKey = "RootPath";
        public const string RootTextKey = "RootText";
        public const string UNLIMITED = "UNLIMITED";
        public const string QuotaWarningPercentageKey = "QuotaWarningPercentage";

        public const string EnableVersioningKey = "EnableVersioning";
        public const string EnableUploadArchiveAndPasswordKey = "EnableUploadArchiveAndPassword";
        public const string PasswordComplexityRequirement = "PasswordComplexityRequirement";
        public const string ViewFile = "View-file";

        public const string PathKey = "Path";
        public const string DefaultRoot = "~";
    }
}
