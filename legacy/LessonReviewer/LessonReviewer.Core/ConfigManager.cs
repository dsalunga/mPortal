using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

using WCMS.Common.Utilities;

namespace WCMS.LessonReviewer.Core
{
    public abstract class ConfigManager
    {
        public static string Get(string key)
        {
            var configPath = HttpContext.Current.Server.MapPath("~/App_Data/Config.xml");

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(configPath);

            return XmlUtil.GetValue(xdoc.SelectSingleNode(string.Format("//Add[@Key='{0}']", key)), "Value");
        }
    }
}
