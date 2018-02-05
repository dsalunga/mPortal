using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.Apps.Integration
{
    public class MCHelper
    {
        public static string GenerateCode(DateTime datetime)
        {
            return DataHelper.ReverseString(string.Format("{0:X16}", datetime.Ticks));
        }
    }
}
