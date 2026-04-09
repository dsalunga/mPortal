using WCMS.BibleReader.Core.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Common.Data;

namespace WCMS.BibleReader.Core
{
    public class BibleVersionLanguage : NamedDataObject
    {
        private static BibleVersionLanguageProvider _provider = new BibleVersionLanguageProvider();

        public BibleVersionLanguage()
        {

        }

        public static BibleVersionLanguageProvider Provider { get { return _provider; } }
    }
}
