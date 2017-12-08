using System.Collections.Generic;

using WCMS.Common.Utilities;

namespace WCMS.Common
{
    public class NamedObjectProvider : INamedObjectProvider
    {
        public Dictionary<string, object> Values { get; set; }

        public NamedObjectProvider()
        {
            Values = new Dictionary<string, object>();
        }

        public NamedObjectProvider(Dictionary<string, object> values)
        {
            Values = values;
        }


        public void Add(string key, object value)
        {
            Values.Add(key, value);
        }

        //public void Add(string key, object value)
        //{
        //    string valueString = value == null ? "" : value.ToString();
        //    Values.Add(key, valueString);
        //}

        //public string Substitute(string format)
        //{
        //    return Substituter.Substitute(format, this);
        //}

        #region IValueProvider Members

        public object GetValue(string key)
        {
            return Values[key];
        }

        public bool ContainsKey(string key)
        {
            return Values.ContainsKey(key);
        }

        public object this[string key]
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
