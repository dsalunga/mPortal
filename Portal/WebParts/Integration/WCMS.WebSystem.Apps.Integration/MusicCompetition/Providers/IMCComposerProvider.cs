using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Providers
{
    public interface IMCComposerProvider : IDataProvider<MCComposer>
    {
        IEnumerable<MCComposer> GetList(int competitionId);
    }
}
