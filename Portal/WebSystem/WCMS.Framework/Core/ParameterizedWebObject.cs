using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using WCMS.Common.Utilities;

namespace WCMS.Framework.Core
{
    [Serializable]
    public abstract class ParameterizedWebObject : NamedWebObject, IWebHeaderTarget
    {
        public ParameterizedWebObject() { }

        public IEnumerable<WebParameter> Parameters
        {
            get { return WebParameter.GetList(OBJECT_ID, Id); }
        }

        public WebParameter GetParameter(string name)
        {
            return WebParameter.Get(OBJECT_ID, Id, name);
        }

        //public string GetParameterValue(string name)
        //{
        //    return GetParameterValue(name, null);
        //}

        //public string GetParameterValeu(string name, bool checkParamSet = false)
        //{
        //    return GetParameterValue(name, null, checkParamSet);
        //}

        public string GetParameterValue(string name, string nullEmptyDefaultValue = null, bool checkParamSet = false)
        {
            var param = GetParameter(name);
            if (param == null && checkParamSet)
            {
                var set = GetParameterSet();
                param = set.GetParameter(name);
            }

            return param == null || string.IsNullOrEmpty(param.Value) ? nullEmptyDefaultValue : param.Value;
        }

        public string GetParameterValue(ParameterizedWebObject fallback, string name, string nullEmptyDefaultValue = null)
        {
            var param = GetParameter(name);
            if (param == null)
                return fallback.GetParameterValue(name, nullEmptyDefaultValue);

            return string.IsNullOrEmpty(param.Value) ? nullEmptyDefaultValue : param.Value;
        }

        public string GetParameterValue(IEnumerable<WebParameter> fallback, string name, string nullEmptyDefaultValue = null)
        {
            var param = GetParameter(name);

            WebParameter fallbackParam = null;
            if (param == null && (fallbackParam = fallback.FirstOrDefault(i => i.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))) != null)
                param = fallbackParam;

            return string.IsNullOrEmpty(param.Value) ? nullEmptyDefaultValue : param.Value;
        }

        public static string GetValue(string name, params ParameterizedWebObject[] parms)
        {
            return GetValue(name, null, parms);
        }

        public static string GetValue(string name, string defaultValue, params ParameterizedWebObject[] parms)
        {
            if (parms != null && parms.Length > 0)
            {
                for (int i = 0; i < parms.Length; i++)
                {
                    var parm = parms[i];
                    if (parm != null)
                    {
                        var value = parm.GetParameterValue(name);
                        if (value != null)
                            return value;
                    }
                }
            }

            return defaultValue;
        }

        public static WebParameter GetParameter(string name, params ParameterizedWebObject[] parms)
        {
            if (parms != null && parms.Length > 0)
            {
                for (int i = 0; i < parms.Length; i++)
                {
                    var parm = parms[i];
                    if (parm != null)
                    {
                        var p = parm.GetParameter(name);
                        if (p != null)
                            return p;
                    }
                }
            }

            return null;
        }

        public WebParameter CreateParameter(string name)
        {
            WebParameter param = new WebParameter();
            param.ObjectId = OBJECT_ID;
            param.RecordId = Id;
            param.Name = name;

            return param;
        }

        public WebParameter GetOrCreateParameter(string name, string value = null)
        {
            var param = GetParameter(name);
            if (param == null)
                param = CreateParameter(name);

            if (value != null)
                param.Value = value;

            return param;
        }

        public ParameterizedWebObject GetParameterSet(bool useSelfFallback)
        {
            return GetParameterSet(WConstants.ParameterSetKey, true);
        }

        public ParameterizedWebObject GetParameterSet()
        {
            return GetParameterSet(WConstants.ParameterSetKey);
        }

        public ParameterizedWebObject GetParameterSet(string key, bool useSelfFallback = false)
        {
            var paramSetName = GetParameterValue(key);
            if (!string.IsNullOrEmpty(paramSetName))
            {
                var paramSet = WebParameterSet.Get(paramSetName);
                if (paramSet != null)
                    return paramSet;
            }

            if (useSelfFallback)
                return this;

            return null;
        }

        public void DeleteRelatedObjects()
        {
            // Delete Parameters
            var parms = this.Parameters;
            for (int i = parms.Count() - 1; i >= 0; i--)
                parms.ElementAt(i).Delete();

            // Resources
            var resources = this.Headers;
            for (int i = resources.Count() - 1; i >= 0; i--)
                resources.ElementAt(i).Delete();
        }

        #region IWebHeaderTarget Members

        public bool AddHeader(WebTextResource resource)
        {
            if (resource.Id < 1)
                resource.Update();

            return WebObjectHeader.AddHeader(OBJECT_ID, this.Id, resource.Id);
        }

        public IEnumerable<WebObjectHeader> Headers
        {
            get { return WebObjectHeader.GetList(OBJECT_ID, Id); }
        }

        #endregion

        public static void WriteParameters(ParameterizedWebObject item, XmlWriter writer)
        {
            var parameters = item.Parameters;
            if (parameters.Count() > 0)
            {
                writer.WriteStartElement("Parameters");

                foreach (var parameter in parameters)
                    writer.WriteRaw(DataHelper.ToXml(parameter));

                writer.WriteEndElement();
            }
        }

        public static void RestoreParameters(IWebObject item, XmlNode itemNode)
        {
            var resourceNodes = itemNode.SelectSingleNode("Parameters");
            if (resourceNodes != null)
            {
                var nodes = resourceNodes.ChildNodes;
                if (nodes.Count > 0)
                {
                    var currParms = WebParameter.GetList(item.OBJECT_ID, item.Id);

                    foreach (XmlNode node in nodes)
                    {
                        var param = DataHelper.FromElementXml<WebParameter>(node.OuterXml);
                        if (param != null)
                        {
                            var currParam = currParms.FirstOrDefault(i => i.Name.Equals(param.Name, StringComparison.InvariantCultureIgnoreCase));
                            if (currParam == null)
                            {
                                param.Id = -1;
                                param.ObjectId = item.OBJECT_ID;
                                param.RecordId = item.Id;
                                param.Update();
                            }
                            else
                            {
                                currParam.Value = param.Value;
                                currParam.IsRequired = param.IsRequired;
                                currParam.Update();
                            }
                        }
                    }
                }
            }
        }
    }
}
