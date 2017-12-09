using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public interface IWebUserGroupProvider : IDataProvider<WebUserGroup>
    {
        WebUserGroup Get(int groupId, int id, bool isGroup = false);
        IEnumerable<WebUserGroup> GetByUserId(int userId, int active);
        IEnumerable<WebUserGroup> GetByGroupId(int groupId, int active);
        IEnumerable<WebUserGroup> GetByCreatedById(int groupId, int createdById, int active);
        bool Delete(int userId, int groupId);
        bool Delete(int groupId, int objectId, int recordId);
        IEnumerable<WebUserGroup> GetList(int active);
    }
}
