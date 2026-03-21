using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.ViewModel
{
    public abstract class WebPartBase : UserControl
    {
        public abstract int OBJECT_ID { get; }
        public List<WebParameter> Parameters
        {
            get { return null; }
        }
    }
}
