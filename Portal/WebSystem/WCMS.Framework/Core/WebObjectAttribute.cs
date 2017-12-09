using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class WebObjectAttribute : Attribute
    {
        public WebObjectAttribute()
        {
            Id = -1;
        }

        public int Id { get; set; }
    }
}
