using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public interface IWebPartAdminProvider: IDataProvider<WebPartAdmin>
    {
        IEnumerable<WebPartAdmin> GetList(int partId);
        IEnumerable<WebPartAdmin> GetList(int partId, int parentId);
        WebPartAdmin Get(int partId, string name);
    }
}
