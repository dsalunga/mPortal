using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Common;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem
{
    public class TemplateFragment : NamedValueProvider
    {
        protected TemplateFragment()
        {
            Fragments = new List<TemplateFragment>();
        }

        public TemplateFragment(string templateString)
            : this()
        {
            TemplateString = templateString;
        }

        public string TemplateString { get; set; }
        public string ContentKey { get; set; }

        public override string ToString()
        {
            // SubFragments
            if (Fragments.Count > 0)
            {
                var sb = new StringBuilder();
                foreach (var fragment in Fragments)
                {
                    sb.Append(fragment.ToString());
                }

                // Add the collection of subFragments to provider
                this.Add(ContentKey, sb.ToString());
            }

            return Substituter.Substitute(TemplateString, this);
        }

        public List<TemplateFragment> Fragments { get; set; }
    }
}
