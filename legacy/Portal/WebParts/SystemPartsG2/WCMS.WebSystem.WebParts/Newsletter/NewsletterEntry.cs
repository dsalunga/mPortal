using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Newsletter.Providers;

namespace WCMS.WebSystem.WebParts.Newsletter
{
    public class NewsletterEntry : NamedWebObject, ISelfManager
    {
        private static INewsletterProvider _provider = new NewsletterSqlProvider();

        public NewsletterEntry()
        {
            Email = string.Empty;
            IPAddress = string.Empty;
            SubscribeDate = DateTime.Now;

            ObjectId = -1;
            RecordId = -1;
            SiteId = -1;
            Gender = -1;
        }

        public string Email { get; set; }
        public string IPAddress { get; set; }
        public DateTime SubscribeDate { get; set; }
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int SiteId { get; set; }
        public int Gender { get; set; }

        public static INewsletterProvider Provider { get { return _provider; } }

        public override int OBJECT_ID
        {
            get { return -1; }
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public int Update()
        {
            return _provider.Update(this);
        }
    }
}
