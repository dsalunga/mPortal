using System;
using System.Collections.Generic;

namespace WCMS.Framework.Core
{
    public interface IWebUserProvider : IDataProvider<WebUser>
    {
        WebUser Get(string userName);
        WebUser GetByEmail(string email);
        bool Delete(string userName);
        IEnumerable<WebUser> GetList(int active);
        WebUser GetByEmailId(string emailId);
    }
}
