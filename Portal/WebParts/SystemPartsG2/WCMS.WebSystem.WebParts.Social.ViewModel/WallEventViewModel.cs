using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

using WCMS.Framework;
using WCMS.Framework.Social;

namespace WCMS.WebSystem.WebParts.Social.ViewModel
{
    public abstract class WallEventViewModel : UserControl
    {
        protected WallUpdate _update;

        public WallEventViewModel()
        {

        }

        public WallEventViewModel(WallUpdate update)
        {
            _update = update;
        }

        public WallUpdate WallUpdate { set { _update = value; } }

        public bool UserIsContentManager { get; set; }
        public WContext PartContext { get; set; }

        public abstract void RenderUI();
    }
}
