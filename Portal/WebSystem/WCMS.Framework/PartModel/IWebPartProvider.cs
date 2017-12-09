using System;
using System.Collections.Generic;

namespace WCMS.Framework.Core
{
    public interface IWebPartProvider : IDataProvider<WPart>
    {
        WPart Get(string identity);
        IEnumerable<WPart> GetList(int active);
    }
}
