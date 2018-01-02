using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.BibleReader.Providers;

namespace WCMS.WebSystem.WebParts.BibleReader
{
    public class BibleAccess : WebObjectBase, ISelfManager
    {
        private static IBibleAccessProvider _provider;

        static BibleAccess()
        {
            _provider = new BibleAccessSqlProvider();
        }

        public BibleAccess()
        {
            UserId = -1;
            AppAccessCount = -1;
            LastAccessed = DateTime.Now;
            VersionAccesses = new List<BibleVersionAccess>();
        }

        public int UserId { get; set; }
        public int AppAccessCount { get; set; }
        public DateTime LastAccessed { get; set; }
        public List<BibleVersionAccess> VersionAccesses { get; set; }

        public override int OBJECT_ID
        {
            get { return BibleReaderConstants.BibleReaderAccess_OID; }
        }

        public static IBibleAccessProvider Provider { get { return _provider; } }

        public bool Delete()
        {
            return _provider.Delete(Id);
        }

        public int Update()
        {
            this.LastAccessed = DateTime.Now;

            return _provider.Update(this);
        }

        public void PopulateVersionAccesses()
        {
            if (VersionAccesses == null)
                VersionAccesses = BibleVersionAccess.Provider.GetList(this.Id).ToList();
        }
    }
}
