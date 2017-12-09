using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Diagnostics
{
    public interface IEventLogProvider : IDataProvider<EventLog>
    {
        IEnumerable<EventLog> GetList(DateTime eventDate, int userId = -1);
        bool Delete(DateTime eventDate);
    }
}
