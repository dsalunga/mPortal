using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.WebParts.BibleReader
{
    public sealed class BibleReaderConstants
    {
        public const string SESSION_KEY = "BibleReaderUserSession";
        public const string SESSIONS_KEY = "BibleReaderSessions";
        public const string RETURN_URL_KEY = "ReturnUrl";
        public const string SESSION_PARAM_KEY = "SessionId";
        public const string BIBLE_URL_KEY = "BibleUrl";

        public const int BibleReaderAccess_OID = 119;
        public const int BibleReaderVersionaccess_OID = 120;
    }

    public sealed class BibleAccessStatuses
    {
        public const int UNINITIALIZED = -1;
        public const int SUCCESS = 0;
        public const int DENIED = 1;
        public const int ERROR = 2;
    }
}
