﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using WCMS.Framework.Core;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem
{
    public abstract class WPageControl : Page, IObjectValueProvider
    {
        public Dictionary<string, object> Values { get; set; }
        //public List<Dictionary<string, WebParameter>> Sets { get; set; }

        public WPageControl()
        {
            Values = new Dictionary<string, object>();
            //Sets = new List<Dictionary<string, WebParameter>>();
        }

        public object GetValue(string key)
        {
            if (Values.ContainsKey(key))
                return Values[key];

            //foreach (var set in Sets)
            //{
            //    if (set.ContainsKey(key))
            //        return set[key].Value;
            //}

            return null;
        }

        public object GetValue(string key, bool fromParent)
        {
            return GetValue(key);
        }

        public void SetValueRange(IEnumerable<WebParameter> items)
        {
            //Sets.Add(items.ToDictionary(i => i.Name));

            foreach (var item in items)
                Values.Add(item.Name, item.Value);
        }

        //public void SetValue(string key, string value)
        //{
        //    Values.Add(key, value);
        //}

        public void SetValue(string key, object value)
        {
            Values.Add(key, value);
        }


        public bool ContainsKey(string key)
        {
            return Values.ContainsKey(key);
        }

        public object this[string key]
        {
            get
            {
                return GetValue(key);
            }
            set
            {
                SetValue(key, value);
            }
        }

        public void Remove(string key)
        {
            Values.Remove(key);
        }
    }
}
