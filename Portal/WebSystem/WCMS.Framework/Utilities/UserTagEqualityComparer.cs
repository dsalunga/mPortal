using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCMS.Framework.Utilities
{
    public class UserTagEqualityComparer : IEqualityComparer<UserDataTag>
    {
        #region IEqualityComparer<UserDataTag> Members

        public bool Equals(UserDataTag x, UserDataTag y)
        {
            return x.Tag.Equals(y.Tag, StringComparison.InvariantCultureIgnoreCase);
        }

        public int GetHashCode(UserDataTag obj)
        {
            return obj.Tag.GetHashCode();
        }

        #endregion
    }
}
