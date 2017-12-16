using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Menu.Providers;

namespace WCMS.WebSystem.WebParts.Menu.Managers
{
    public class MenuManager : StandardDataManager<MenuEntity>, IMenuProvider
    {
        protected IMenuProvider _provider;

        public MenuManager(IMenuProvider provider)
            : base(provider)
        {
            _provider = provider;
        }
    }
}
