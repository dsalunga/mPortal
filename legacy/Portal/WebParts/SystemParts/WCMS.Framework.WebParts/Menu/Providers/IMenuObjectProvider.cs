using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Menu.Providers
{
    public interface IMenuObjectProvider : IDataProvider<MenuObject>
    {
        MenuObject Get(int objectId, int recordId);
    }
}
