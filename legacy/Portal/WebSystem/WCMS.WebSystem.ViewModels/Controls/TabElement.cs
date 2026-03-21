using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCMS.WebSystem.Controls
{
    [Serializable]
    public class TabElement
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public bool CausesValidation { get; set; }
        public string Url { get; set; }
        public string Tag { get; set; }
        public bool IsSelected { get; set; }
        public string Target { get; set; }
    }
}
