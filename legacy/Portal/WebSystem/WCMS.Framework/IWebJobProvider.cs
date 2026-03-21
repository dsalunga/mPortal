using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public interface IWebJobProvider : IDataProvider<WebJob>
    {
        WebJob Get(string name);
    }
}
