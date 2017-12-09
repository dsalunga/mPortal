using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Interfaces;

namespace WCMS.Framework
{
    public class WebSubstituter
    {
        public static string Substitute(string template, Dictionary<string, INamedValueProvider> context)
        {
            // key formats:
            // $(Key)
            // $(Object.Key)
            // Default object = Current

            return string.Empty;
        }
    }
}
