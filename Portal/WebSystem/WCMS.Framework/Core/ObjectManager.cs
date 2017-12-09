using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WCMS.Framework.Core
{
    public class ObjectManager
    {
        public static int GetObjectId<T>()
        {
            Type type = typeof(T);
            object[] attribs = type.GetCustomAttributes(typeof(WebObjectAttribute), false);
            if (attribs.Length > 0)
            {
                WebObjectAttribute attrib = attribs[0] as WebObjectAttribute;
                return attrib.Id;
            }

            return -1;
        }

        public static WebObject GetObject<T>()
        {
            int objectId = GetObjectId<T>();
            if (objectId > 0)
            {
                return WebObject.Get(objectId);
            }

            return null;
        }
    }
}
