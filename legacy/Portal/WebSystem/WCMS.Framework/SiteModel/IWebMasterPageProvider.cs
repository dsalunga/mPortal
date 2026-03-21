using System;
using System.Data;
using System.Collections.Generic;


namespace WCMS.Framework.Core
{
    public interface IWebMasterPageProvider : IDataProvider<WebMasterPage>
    {
        IEnumerable<WebMasterPage> GetList(int siteId);
    }
}
