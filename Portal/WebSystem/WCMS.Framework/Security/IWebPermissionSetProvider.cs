using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public interface IWebPermissionSetProvider : IDataProvider<WebPermissionSet>
    {
        //WebPermissionSet Get(int id);
        //List<WebPermissionSet> GetList();
        IEnumerable<WebPermissionSet> GetList(int objectId, int recordId, int public2);
        //int Update(WebPermissionSet item);
        //bool Delete(int id);
    }
}
