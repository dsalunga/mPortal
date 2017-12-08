using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WCMS.Common.Utilities
{
    class Test
    {
        private static XmlNode SubstituteNode(Dictionary<string, INamedObjectProvider> context, XmlNode node, IDictionary<string, string> values)
        {
            string outerXml = node.OuterXml;
            string configXml = Substituter.Substitute(outerXml, (string target) =>
            {
                return GetNamedValue(target, context);
            });

            if (configXml != outerXml)
            {
                XmlDocumentFragment docFrag = node.OwnerDocument.CreateDocumentFragment();
                docFrag.InnerXml = configXml;

                return docFrag.FirstChild;
            }
            else
            {
                return node;
            }
        }

        public static string GetNamedValue(string sourceName, Dictionary<string, INamedObjectProvider> sourceObject)
        {
            string sourceValue = null;
            if (sourceName.Contains(":"))
            {
                string[] sourceParts = sourceName.Split(':');

                string keyBase = sourceParts.First().ToLower();
                string key = sourceParts[1].ToLower();
                if (sourceParts.Length > 1 && sourceObject.ContainsKey(keyBase))
                {
                    var dict = sourceObject[keyBase];

                    if (dict.ContainsKey(key))
                        sourceValue = dict[key].ToString();
                    else
                        throw new KeyNotFoundException(string.Format("The given key was not present in the dictionary: {0}.", sourceName));
                }
                else
                {
                    sourceValue = string.Format("$({0})", sourceName);
                }
            }
            else
            {
                if (sourceObject.ContainsKey(Substituter.DefaultProviderKey) && sourceObject[Substituter.DefaultProviderKey].ContainsKey(sourceName))
                    sourceValue = sourceObject[Substituter.DefaultProviderKey][sourceName].ToString();
                else
                    throw new KeyNotFoundException(string.Format("The given key was not present in the dictionary: {0}.", sourceName));
            }

            return sourceValue;
        }
    }
}
