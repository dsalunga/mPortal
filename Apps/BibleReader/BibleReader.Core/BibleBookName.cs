using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Data;
using WCMS.BibleReader.Core.Providers;

namespace WCMS.BibleReader.Core
{
    /// <summary>
    /// Entity class for BibleBook table
    /// </summary>
    public class BibleBookName : NamedDataObject
    {
        private static BibleBookNameProvider _provider;

        static BibleBookName()
        {
            _provider = new BibleBookNameProvider();
        }

        public BibleBookName()
        {

        }

        public int BookNameCode { get; set; }
        public int BookCode { get; set; }
        public int MaxChapter { get; set; }
        public string ShortName { get; set; }

        public string GetShortName()
        {
            if (string.IsNullOrEmpty(ShortName))
                return Name;
            return ShortName;
        }

        public static BibleBookNameProvider Provider { get { return _provider; } }
    }
}
