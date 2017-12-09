using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public interface IWebUserRoleProvider
    {
        WebUserGroup Get(int userRoleId);
        WebUserGroup Get(int roleId, int userId);
        //List<WebUserGroup> Get();
        IEnumerable<WebUserGroup> GetByUserId(int userId);
        IEnumerable<WebUserGroup> GetByGroupId(int roleId);
        //int Update(WebUserGroup item);
        //bool Delete(int userRoleId);
    }
}
