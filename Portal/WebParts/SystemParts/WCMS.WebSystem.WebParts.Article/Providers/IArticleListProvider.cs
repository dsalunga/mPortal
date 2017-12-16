using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Article.Providers
{
    public interface IArticleListProvider : IDataProvider<ArticleList>
    {
        ArticleList Get(int objectId, int recordId);
    }
}
