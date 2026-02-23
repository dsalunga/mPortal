using System;
using System.Data;
using System.Configuration;

namespace WCMS.Common.Utilities
{
    /// <summary>
    /// Summary description for Misc
    /// </summary>
    public abstract class Misc
    {
        public static string FormatCheckIfColumnReserved(string c)
        {
            if (c.Equals("location", StringComparison.InvariantCultureIgnoreCase))
            {
                return "'" + c + "'";
            }
            else
            {
                return c;
            }
        }
    }
}