using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Common
{
    public class ConsoleLogger : ILogger
    {
        #region ILogger Members

        public void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        #endregion
    }
}
