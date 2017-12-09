using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Common;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.ViewModel
{
    public interface IStringValueProvider : INamedValueProvider
    {
        Dictionary<string, string> Values { set; get; }
        //List<Dictionary<string, WebParameter>> Sets { get; set; }

        //string GetValue(string key);
        string GetValue(string key, bool fromParent);

        //void SetValue(string key, string value);
        void SetValue(string key, string value);
    }
}
