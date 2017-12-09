using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public interface IPublicSecurable : IWebObject
    {
        int PublicAccess { get; set; }

        int GetPublicAccess(WSession session);
    }
}
