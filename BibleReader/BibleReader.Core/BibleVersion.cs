using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;

using WCMS.BibleReader.Core.Providers;
using WCMS.Common.Data;

namespace WCMS.BibleReader.Core
{
    public class BibleVersion : NamedDataObject
    {
        private static BibleVersionProvider _provider;

        static BibleVersion()
        {
            _provider = new BibleVersionProvider();
        }

        public BibleVersion()
        {

        }

        public string BibleTableName { get; set; }
        public int BookNameCode { get; set; }
        public string ShortName { get; set; }
        public int OldAndNew { get; set; }
        public int Active { get; set; }
        public int LanguageType { get; set; }
        public int TranslationType { get; set; }

        public static BibleVersionProvider Provider { get { return _provider; } }

        public BibleBook GetFirstBook()
        {
            /// TODO: add implementation

            return null;
        }

        public BibleBook GetLastBook()
        {
            /// TODO: add implementation

            return null;
        }
    }
}
