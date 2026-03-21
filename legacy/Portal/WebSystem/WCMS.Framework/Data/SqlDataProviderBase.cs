using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Common.Utilities;

namespace WCMS.Framework.Core
{
    public abstract class SqlDataProviderBase
    {
        protected abstract string OBJECT_NAME { get; }

        public int GetCount()
        {
            var o = SqlHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(1) FROM {0}", OBJECT_NAME));
            if (o != null)
                return Convert.ToInt32(o.ToString());
            return -1;
        }
    }
}
