using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration
{
    public interface ISportsfestProvider : IDataProvider<Sportsfest>
    {
        Sportsfest Get(string name);
    }
}
