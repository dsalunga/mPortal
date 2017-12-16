using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public class FileIdentity : NamedWebObject, ISelfManager
    {
        private static IFileIdentityProvider _provider;

        static FileIdentity()
        {
            _provider = new FileIdentityProvider();
        }

        public FileIdentity()
        {
            ObjectId = -1;
            RecordId = -1;
            LibraryId = -1;
        }

        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int LibraryId { get; set; }
        public string FilePath { get; set; }

        public string EvalPath
        {
            get
            {
                return FileHelper.Combine(FilePath, Name, '/');
            }

            set
            {
                Name = Path.GetFileName(value);
                FilePath = FileHelper.GetFolder(value, '/');
            }
        }

        public FileVersion AddVersion(int activity, int userId)
        {
            FileVersion version = new FileVersion();
            version.FileId = this.Id;
            //version.VersionDate
            version.Activity = activity;
            version.UserId = userId;
            version.Update();

            return version;
        }

        public static FileIdentity GetByPath(string filePath, int objectId, int recordId)
        {
            var name = Path.GetFileName(filePath);
            var folder = FileHelper.GetFolder(filePath, '/');

            return _provider.Get(folder, name, objectId, recordId);
        }

        public override int OBJECT_ID
        {
            get { return -1; }
        }

        public IEnumerable<FileVersion> Versions
        {
            get
            {
                return FileVersion.Provider.GetList(Id);
            }
        }

        public static IFileIdentityProvider Provider
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
    }
}
