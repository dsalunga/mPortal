using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;

namespace WCMS.Common.Utilities
{
    public class ReflectionUtil
    {
        public static T LoadAndCreateInstance<T>(string typeName)
        {
            var typeArray = typeName.Split(',');

            return (T)Assembly.Load(typeArray[0].Trim()).CreateInstance(typeArray[1].Trim());
        }
    }
}
