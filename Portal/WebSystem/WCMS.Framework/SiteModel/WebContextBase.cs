using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using WCMS.Common.Utilities;

namespace WCMS.Framework
{
    public abstract class WebContextBase
    {
        public HttpContext Context
        {
            get { return HttpContext.Current; }
        }
    }
}
