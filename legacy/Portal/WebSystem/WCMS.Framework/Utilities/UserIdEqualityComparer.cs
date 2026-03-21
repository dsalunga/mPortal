using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCMS.Framework.Utilities
{
    public class UserIdEqualityComparer : IEqualityComparer<WebUser>
    {
        public bool Equals(WebUser x, WebUser y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(WebUser obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
