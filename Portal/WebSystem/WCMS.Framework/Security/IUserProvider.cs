using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Framework.Core;

namespace WCMS.Framework.Security
{
    public interface IUserProvider
    {
        ExternalLoginResult LoginCheck(string userName, string password);
        ExternalLoginResult LoginCheck(WebUser user, string password);
        WContext Context { get; set; }
    }
}
