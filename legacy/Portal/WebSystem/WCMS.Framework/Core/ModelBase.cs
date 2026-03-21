using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    public abstract class ModelBase : ISelfManager
    {
        #region ISelfServicing Members

        public int Update()
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        #endregion


        public bool Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
