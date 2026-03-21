using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public class FileVersion : WebObjectBase, ISelfManager
    {
        private static IFileVersionProvider _provider;

        static FileVersion()
        {
            _provider = new FileVersionProvider();
        }

        public FileVersion()
        {
            FileId = -1;
            VersionDate = DateTime.Now;
            Activity = 0;
            UserId = -1;
        }

        public int FileId { get; set; }
        public DateTime VersionDate { get; set; }
        public int Activity { get; set; }
        public int UserId { get; set; }

        public WebUser User
        {
            get { return WebUser.Get(UserId); }
        }

        public static IFileVersionProvider Provider
        {
            get { return _provider; }
        }

        public override int OBJECT_ID
        {
            get { return -1; }
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
    }
}
