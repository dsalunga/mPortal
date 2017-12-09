using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Utilities
{
    public abstract class HtmlHelper
    {
        static HtmlHelper()
        {
            _hyperLinkFull = WebRegistry.SelectNodeValue("/Content/Templates/Hyperlink");
            _hyperLinkBasic = WebRegistry.SelectNodeValue("/Content/Templates/Hyperlink/Basic");
        }

        private static string _hyperLinkFull;
        private static string _hyperLinkBasic;

        public static string BuildHyperlink(string href, string content, string title, string target)
        {
            if (string.IsNullOrEmpty(_hyperLinkFull))
                return null;

            var provider = new NamedValueProvider();
            provider.Add("Href", href);
            provider.Add("Content", content);
            provider.Add("Title", title);
            provider.Add("Target", target);

            return Substituter.Substitute(_hyperLinkFull, provider);
        }

        public static string BuildHyperlink(string href, string content, string title)
        {
            if (string.IsNullOrEmpty(_hyperLinkBasic))
                return null;

            var provider = new NamedValueProvider();
            provider.Add("Href", href);
            provider.Add("Content", content);
            provider.Add("Title", title);

            return Substituter.Substitute(_hyperLinkBasic, provider);
        }
    }
}
