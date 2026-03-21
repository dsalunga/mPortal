using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.BibleReader.Providers;

namespace WCMS.WebSystem.WebParts.BibleReader
{
    public class BibleVersionAccess : WebObjectBase, ISelfManager
    {
        private static IBibleVersionAccessProvider _provider;

        static BibleVersionAccess()
        {
            _provider = new BibleVersionAccessSqlProvider();
        }

        public BibleVersionAccess()
        {
            BibleAccessId = -1;
            BibleVersionId = -1;
            VersionAccessCount = -1;

            BibleVersionName = string.Empty;
            LastAccessed = DateTime.Now;
        }

        public int BibleAccessId { get; set; }
        public int BibleVersionId { get; set; }
        public string BibleVersionName { get; set; }
        public int VersionAccessCount { get; set; }
        public DateTime LastAccessed { get; set; }

        public override int OBJECT_ID
        {
            get { return BibleReaderConstants.BibleReaderVersionaccess_OID; }
        }

        public static IBibleVersionAccessProvider Provider { get { return _provider; } }

        public bool Delete()
        {
            return _provider.Delete(Id);
        }

        public int Update()
        {
            this.LastAccessed = DateTime.Now;

            return _provider.Update(this);
        }
    }
}
