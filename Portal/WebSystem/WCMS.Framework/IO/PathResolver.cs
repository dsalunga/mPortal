using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public class PathResolver
    {
        public static string ResolvePathFromNonWebCall(string virtualPath)
        {
            string webLocalPath = WebRegistry.SelectNode("/System/LocalPath").Value;
            string path = virtualPath;

            if (path.StartsWith("~")) path = path.Substring(1);
            path = path.Replace("/", @"\");
            path = path.Trim('\\');

            return Path.Combine(webLocalPath, path);
        }
    }
}
