using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Jobs.Providers
{
    public interface IJobProvider : IDataProvider<Job>
    {
        IEnumerable<Job> GetList(string keyword);
    }
}
