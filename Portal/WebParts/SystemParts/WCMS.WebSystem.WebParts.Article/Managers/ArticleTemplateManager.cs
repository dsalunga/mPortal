using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Article.Providers;

namespace WCMS.WebSystem.WebParts.Article.Managers
{
    public class ArticleTemplateManager: StandardDataManager<ArticleTemplate>, IArticleTemplateProvider
    {
        protected IArticleTemplateProvider _provider;

        public ArticleTemplateManager(IArticleTemplateProvider provider)
            : base(provider)
        {
            _provider = provider;
        }
    }
}
