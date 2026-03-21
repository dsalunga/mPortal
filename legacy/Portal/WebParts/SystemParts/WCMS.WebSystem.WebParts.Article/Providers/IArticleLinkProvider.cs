using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Article.Providers
{
    public interface IArticleLinkProvider : IDataProvider<ArticleLink>
    {
        ArticleLink Get(int objectId, int recordId, int articleId);
        IEnumerable<ArticleLink> GetList(int objectId = -2, int recordId = -2);
        IEnumerable<ArticleLink> GetList(int articleId);
        IEnumerable<ArticleLink> GetList(int objectId = -2, int recordId = -2, int siteId = -2);
    }
}
