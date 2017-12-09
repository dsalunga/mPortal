using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    [Serializable]
    public abstract class NamedWebObject: WebObjectBase, INameWebObject
    {
        #region INameWebObject Members

        [ObjectColumn]
        public virtual string Name { get; set; }

        #endregion
    }
}
