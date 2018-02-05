using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration
{
    public class Sportsfest : NamedWebObject, ISelfManager
    {
        private static ISportsfestProvider _provider;

        static Sportsfest()
        {
            _provider = new SportsfestSqlProvider();
        }

        public Sportsfest()
        {
            MemberId = -1;
            CountryCode = -1;
            Age = 0;
            
            EntryDate = DateTime.Now;

            Locale = string.Empty;
            Suggestion = string.Empty;
            GroupColor = string.Empty;
            Mobile = string.Empty;
            Sports = string.Empty;
            ShirtSize = string.Empty;
        }

        public int MemberId { get; set; }
        public string GroupColor { get; set; }
        public int Age { get; set; }
        public string ShirtSize { get; set; }
        public string Mobile { get; set; }
        public DateTime EntryDate { get; set; }
        public string Sports { get; set; }

        public int CountryCode { get; set; }
        public string Locale { get; set; }
        public string Suggestion { get; set; }

        public static ISportsfestProvider Provider
        {
            get { return _provider; }
        }

        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        #endregion

        public override int OBJECT_ID
        {
            get { return -1; }
        }
    }
}
