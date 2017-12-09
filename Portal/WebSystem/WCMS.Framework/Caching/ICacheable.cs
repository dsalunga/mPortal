using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework
{
    public interface ICacheable<T>
    {
        ICacheManager<T> CacheManager { get; set; }
    }
}
