using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using WCMS.Common.Utilities;

using WCMS.Framework.Core;
using WCMS.WebSystem.WebParts.RemoteIndexer.Providers;

namespace WCMS.WebSystem.WebParts.RemoteIndexer
{
    public class RemoteItem : NamedWebObject, ISelfManager
    {
        private static IRemoteItemProvider _provider;

        private char pathSeparator = IndexerConstants.WebSeparator;

        public void SetPathSeparator(char separator)
        {
            pathSeparator = separator;
        }

        static RemoteItem()
        {
            _provider = new RemoteItemSqlProvider();
        }

        public RemoteItem()
        {
            LibraryId = -1;
            TypeId = -1;
            FileCacheEnabled = -1;
            Cached = 0;
            Size = 0;
            DownloadCount = 0;
            ParentId = -1;

            DateModified = DateTime.Now;
            IndexDateModified = DateTime.Now;

            Content = string.Empty;
            DisplayName = string.Empty;
        }

        public RemoteItem(FileStruct file, string relativePath, int parentId, int libraryId)
            : this()
        {
            Synch(file, relativePath, parentId, libraryId);
        }

        #region Properties

        public int LibraryId { get; set; }
        public string RelativePath { get; set; }
        public int TypeId { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime IndexDateModified { get; set; }
        public long Size { get; set; }
        public string Content { get; set; }
        public int ParentId { get; set; }
        public int DownloadCount { get; set; }
        public string DisplayName { get; set; }
        public int FileCacheEnabled { get; set; }
        public int Cached { get; set; }

        public override int OBJECT_ID { get { return IndexerConstants.RemoteItem_OID; } }
        public static IRemoteItemProvider Provider { get { return _provider; } }
        public string Extension { get { return IsDirectory ? string.Empty : Path.GetExtension(Name).TrimStart('.'); } }
        public IEnumerable<RemoteItem> Children { get { return Provider.GetList(this.LibraryId, this.Id); } }

        #endregion


        public void Synch(FileStruct file, string relativePath, int parentId, int libraryId)
        {
            if (this.Id > -1 && (this.DateModified != file.DateModified || this.Size != file.Size || !this.Name.Equals(file.Name, StringComparison.InvariantCulture)))
                this.IndexDateModified = DateTime.Now;

            this.LibraryId = libraryId;
            this.DateModified = file.DateModified;
            this.Size = file.Size;
            this.ParentId = parentId;
            this.TypeId = file.IsDirectory ? 0 : 1;
            this.Name = file.Name;
            this.RelativePath = relativePath;
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public bool DeleteNode()
        {
            if (IsDirectory)
            {
                var children = this.Children;
                foreach (var child in children)
                    child.DeleteNode();
            }

            Delete();

            return true;
        }

        public bool IsFileCacheEnabled
        {
            get { return FileCacheEnabled == 1; }
            set { FileCacheEnabled = value ? 1 : 0; }
        }

        public bool IsCached
        {
            get { return Cached == 1; }
            set { Cached = value ? 1 : 0; }
        }

        public bool IsDirectory
        {
            get { return TypeId == 0; }
        }

        public RemoteLibrary Library
        {
            get { return RemoteLibrary.Provider.Get(LibraryId); }
        }

        public string FullPath
        {
            get { return FileHelper.Combine(RelativePath, Name, pathSeparator); }
        }

        public string BuildCachePath(RemoteLibrary library)
        {
            var libraryCachePath = library.GetCachePath();
            var itemPath = FullPath;
            var itemCachePath = FileHelper.Combine(libraryCachePath, itemPath.Replace(library.BaseAddress, string.Empty));

            return itemCachePath;
        }

        public string BuildDisplayPath(RemoteLibrary library)
        {
            string fullPath = FullPath;
            if (!string.IsNullOrEmpty(library.DisplayBaseAddress))
                fullPath = fullPath.Replace(library.BaseAddress, library.DisplayBaseAddress);

            return fullPath;
        }

        public static RemoteItem Find(int libraryId, string path)
        {
            var library = RemoteLibrary.Provider.Get(libraryId);
            if (library != null)
                return Find(library, path);

            return null;
        }

        public static RemoteItem Find(RemoteLibrary library, string path)
        {
            if (path.EndsWith("/"))
                path = path.TrimEnd('/');

            if (!path.StartsWith(library.BaseAddress))
                path = FileHelper.Combine(library.BaseAddress, path);

            var items = Provider.GetList(library.Id)
                .Where(i => path.EndsWith(i.Name) && FileHelper.Combine(i.RelativePath, i.Name)
                    .Equals(path, StringComparison.InvariantCultureIgnoreCase));

            return items.FirstOrDefault();
        }

        public string SizeString
        {
            get { return FileHelper.GetSizeString(this.Size); }
        }

        public bool Equals(FileStruct file, string relativePath, int parentId, int libraryId)
        {
            if (parentId == this.ParentId && libraryId == this.LibraryId &&
                relativePath.Equals(this.RelativePath, StringComparison.InvariantCultureIgnoreCase))
            {
                // file.DateModified == this.DateModified && 
                return file.Size == this.Size &&
                    file.IsDirectory == this.IsDirectory && file.Name.Equals(this.Name, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        public bool IsSimilar(FileStruct file, string relativePath, int parentId, int libraryId)
        {
            if (parentId == this.ParentId && libraryId == this.LibraryId &&
                relativePath.Equals(this.RelativePath, StringComparison.InvariantCultureIgnoreCase))
            {
                // file.DateModified == this.DateModified && 
                return file.IsDirectory == this.IsDirectory && file.Name.Equals(this.Name, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }
    }
}
