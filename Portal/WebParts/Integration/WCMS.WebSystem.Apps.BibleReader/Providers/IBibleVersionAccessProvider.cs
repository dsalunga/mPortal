using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.BibleReader.Providers
{
    public interface IBibleVersionAccessProvider : IDataProvider<BibleVersionAccess>
    {
        IEnumerable<BibleVersionAccess> GetList(int accessId);
    }
}
