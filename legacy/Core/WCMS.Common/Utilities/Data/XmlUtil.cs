using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WCMS.Common.Utilities
{
    public static class XmlUtil
    {
        public static string GetNodeText(XmlNode node, string name, string defaultValue = null)
        {
            var subNode = node.SelectSingleNode(name);
            return subNode == null ? defaultValue : subNode.InnerText;
        }

        public static string GetAttributeValue(XmlNode node, string name)
        {
            var attribute = node.Attributes[name];

            return attribute == null ? null : attribute.Value;
        }

        public static string GetAttributeValue(XmlNode node, string name, string defaultValue)
        {
            var value = GetAttributeValue(node, name);

            if (string.IsNullOrEmpty(value))
                value = defaultValue;

            return value;
        }

        public static string GetValue(XmlNode node, string name)
        {
            var attrValue = GetAttributeValue(node, name);

            return attrValue == null ? GetNodeText(node, name) : attrValue;
        }

        public static Dictionary<string, string> GetValues(XmlNode node)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            if (node != null)
            {
                values.Add("Value", node.InnerText);

                var subNodes = node.ChildNodes;
                if (subNodes.Count > 0)
                    foreach (XmlNode subNode in subNodes)
                        values.Add(subNode.Name, subNode.InnerText);

                var attributes = node.Attributes;
                if (attributes.Count > 0)
                    foreach (XmlAttribute attribute in attributes)
                        values.Add(attribute.Name, attribute.Value);
            }

            return values;
        }
    }
}
