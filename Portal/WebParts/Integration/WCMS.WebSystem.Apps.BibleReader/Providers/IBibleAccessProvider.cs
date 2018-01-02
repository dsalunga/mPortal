using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.BibleReader.Providers
{
    public interface IBibleAccessProvider : IDataProvider<BibleAccess>
    {
        new BibleAccess Get(int userId);
    }
}
