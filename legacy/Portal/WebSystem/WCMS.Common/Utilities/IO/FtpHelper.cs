using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WCMS.Common.Utilities
{
    public class FtpHelper
    {
        public static string UrlEncode(string path)
        {
            var p = path.Replace("#", "%23"); //HttpUtility.UrlPathEncode(path).Replace("#", "%23");
            return p;
        }
    }
}
