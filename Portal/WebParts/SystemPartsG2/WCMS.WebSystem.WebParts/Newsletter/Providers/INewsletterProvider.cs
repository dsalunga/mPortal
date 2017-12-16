using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Newsletter.Providers
{
    public interface INewsletterProvider : IDataProvider<NewsletterEntry>
    {
        NewsletterEntry Get(int objectId, int recordId, string email);
    }
}
