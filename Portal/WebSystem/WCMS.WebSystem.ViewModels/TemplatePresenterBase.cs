using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem
{
    public abstract class TemplatePresenterBase
    {
        public const string GLOBAL_KEY = "Global";

        public TemplatePresenterBase()
        {
            Sections = new Dictionary<string, string>();
            //Parameters = new Dictionary<string, NamedValueProvider>();
        }

        public TemplatePresenterBase(string sourceXmlPath)
            : this()
        {
            string path = WebHelper.MapPath(sourceXmlPath);

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(path);

            XmlNodeList nodes = xdoc.SelectNodes("//Template-Sections/Section");
            foreach (XmlNode node in nodes)
            {
                Sections.Add(XmlUtil.GetAttributeValue(node, "Name"), node.InnerText);
            }
        }

        public string GetSection(string name)
        {
            if (Sections.ContainsKey(name))
                return Sections[name];

            return null;
        }

        //public NamedValueProvider GetParameterSet(string key)
        //{
        //    NamedValueProvider items = new NamedValueProvider();

        //    if (Parameters.ContainsKey(key))
        //    {
        //        var provider = Parameters[key];

        //        foreach (var item in provider.Values) // must iterate keyValuePair
        //        {
        //            items.Add(item.Key, item.Value);
        //        }
        //    }

        //    if (Parameters.ContainsKey(GLOBAL_KEY))
        //    {
        //        var provider = Parameters[GLOBAL_KEY];

        //        foreach (var item in provider.Values)
        //        {
        //            items.Add(item.Key, item.Value);
        //        }
        //    }

        //    return items;
        //}

        public Dictionary<string, string> Sections { get; set; }

        //public Dictionary<string, NamedValueProvider> Parameters { get; set; }
    }
}
