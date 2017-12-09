using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCMS.Framework.Utilities
{
    public class UserNameEqualityComparer : IEqualityComparer<WebUser>
    {
        public bool Equals(WebUser x, WebUser y)
        {
            return x.UserName.Equals(y.UserName, StringComparison.InvariantCultureIgnoreCase);
        }

        public int GetHashCode(WebUser obj)
        {
            return obj.UserName.ToLower().GetHashCode();
        }
    }
}
