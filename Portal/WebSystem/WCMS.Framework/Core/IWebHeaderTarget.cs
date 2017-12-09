using System.Collections.Generic;

namespace WCMS.Framework.Core
{
    public interface IWebHeaderTarget
    {
        bool AddHeader(WebTextResource resource);

        IEnumerable<WebObjectHeader> Headers { get; }
    }
}
