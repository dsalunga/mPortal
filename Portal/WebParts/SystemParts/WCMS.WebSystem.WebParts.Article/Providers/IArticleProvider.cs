using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Article.Providers
{
    public interface IArticleProvider : IDataProvider<Article>
    {
        IEnumerable<Article> GetList(int siteId);
    }
}
