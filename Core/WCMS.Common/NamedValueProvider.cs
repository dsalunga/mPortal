using System.Collections.Generic;

using WCMS.Common.Utilities;

namespace WCMS.Common
{
    public class NamedValueProvider : INamedValueProvider
    {
        public Dictionary<string, string> Values { get; set; }

        public NamedValueProvider()
        {
            Values = new Dictionary<string, string>();
        }

        public NamedValueProvider(Dictionary<string, string> values)
        {
            Values = values;
        }


        public void Add(string key, string value)
        {
            Values.Add(key, value);
        }

        public void Add(string key, object value)
        {
            string valueString = value == null ? "" : value.ToString();
            Values.Add(key, valueString);
        }

        public string Substitute(string format)
        {
            return Substituter.Substitute(format, this);
        }

        #region IValueProvider Members

        public string GetValue(string key)
        {
            return Values[key];
        }

        public bool ContainsKey(string key)
        {
            return Values.ContainsKey(key);
        }

        public string this[string key]
        {
            get { return Values[key]; }
            set { Values[key] = value; }
        }

        public void Remove(string key)
        {
            Values.Remove(key);
        }

        #endregion
    }
}
