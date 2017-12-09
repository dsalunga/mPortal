using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Framework.Core;

namespace WCMS.Framework.Security
{
    public interface IUserProviderProvider : IDataProvider<UserProvider>
    {
        UserProvider Get(string name);
    }
}
