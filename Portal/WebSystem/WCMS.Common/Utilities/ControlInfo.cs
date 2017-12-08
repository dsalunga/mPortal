using System;
using System.Collections;
using System.Collections.Specialized;

namespace WCMS.Common.Utilities
{
    public enum ControlInfoEnum { TemplateIdentity, LocationType, ItemID, SectionIdentity };
    /// <summary>
    /// Summary description for ControlInfo.
    /// </summary>
    public class ControlInfo
    {
        //private NameValueCollection c;
        string[] sKeys;

        public ControlInfo(string sInfo)
        {
            //
            // TODO: Add constructor logic here
            //
            //sKeys = sInfo.Split(',');
            sKeys = sInfo.Replace("_C_", "\ufff9").Split('\ufff9');

            /*
            c = new NameValueCollection();
            foreach(string s in sKeys)
            {
                string[] sPair = s.Split('.');
                string sName = string.Empty;
                string sValue = string.Empty;

                try
                {
                    sName = sPair[0];
                    sValue = sPair[1];
                }
                catch{}
                c[sName] = sValue;
            }
            */
        }

        public string Key(ControlInfoEnum itemKey)
        {
            switch (itemKey)
            {
                case ControlInfoEnum.LocationType:
                    //return c["L"];
                    return sKeys[0];
                case ControlInfoEnum.ItemID:
                    //return c["II"];
                    return sKeys[1];
                case ControlInfoEnum.TemplateIdentity:
                    //return c["TI"];
                    return sKeys[2];
                case ControlInfoEnum.SectionIdentity:
                    //return c["SI"];
                    //return sKeys[3];
                    return sKeys[3].Replace("_D_", "-");
                default:
                    return null;
            }
        }
    }
}
