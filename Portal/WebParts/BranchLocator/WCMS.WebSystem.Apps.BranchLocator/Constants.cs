using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCMS.WebSystem.Apps.BranchLocator
{
    public class MChapterConstants
    {
        public const string CENTRAL_NAME = "Central Chapter";
    }

    public enum ChapterTypes
    {
        Regular = 0,
        District = 1,
        Division = 2
    }

    //public enum ChapterAccessTypes
    //{
    //    Public = 0,
    //    Internal = 1,
    //    Restricted = 2
    //}

    public class ChapterAccess
    {
        public const int PUBLIC = 0;
        public const int INTERNAL = 1;
        public const int RESTRICTED = 2;

        public static string Get(int id)
        {
            if (_values.ContainsKey(id))
                return _values[id];
            return string.Empty;
        }

        private static Dictionary<int, string> _values = new Dictionary<int, string>
        {
            {PUBLIC, "Public"}, {INTERNAL, "Internal"}, {RESTRICTED, "Restricted"}
        };
    }
}
