using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Common
{
    public interface IWcmsLogger
    {
        void WriteLine(string format, params object[] args);
    }
}
