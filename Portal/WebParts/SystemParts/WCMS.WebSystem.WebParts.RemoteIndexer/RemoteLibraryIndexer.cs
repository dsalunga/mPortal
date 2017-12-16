using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using WCMS.Common.Net;
using WCMS.Common.Utilities;
using WCMS.WebSystem.WebParts.RemoteIndexer.Common;

namespace WCMS.WebSystem.WebParts.RemoteIndexer
{
    public class RemoteLibraryIndexer
    {
        private int _maxSkip;
        private RemoteIndexerTask _task;
        private int _maxRetry = 3;

        public RemoteLibraryIndexer(int maxSkip, RemoteIndexerTask task)
        {
            _maxSkip = maxSkip;
            _task = task;
        }

        public string TaskName { get; set; }

        private IRemoteIndexer BuildRemoteIndexer(RemoteLibrary library)
        {
            switch (library.SourceTypeId)
            {
                case RemoteSourceTypes.Ftp:
                    return new FtpIndexer(library.BaseAddress, library.UserName, library.Password);

                case RemoteSourceTypes.WindowShare:
                    return new WindowsFileSystemIndexer(library.BaseAddress, library.UserName, library.Password);
            }

            return null;
        }

        public void Start(RemoteLibrary library)
        {
            var indexer = BuildRemoteIndexer(library);
            if (indexer != null)
            {
                long totalSize = 0;

                // Read and update here
                if (library.SourceTypeId == RemoteSourceTypes.WindowShare && !string.IsNullOrEmpty(library.UserName) && !string.IsNullOrEmpty(library.Password))
                {
                    var networkCredential = new NetworkCredential(library.UserName, library.Password);
                    using (new NetworkConnection(library.BaseAddress, networkCredential))
                    {
                        totalSize = CreateIndexesRecursive(library, library.BaseAddress, indexer, -1);
                    }
                }
                else
                {
                    totalSize = CreateIndexesRecursive(library, library.BaseAddress, indexer, -1);
                }

                library.Size = totalSize;
                library.Update();
            }
        }

        private void WriteLog(string format, params object[] args)
        {
            if (_task != null)
                _task.Logger.WriteLine(format, args);
            else
                Console.WriteLine(format, args);
        }

        private long CreateIndexesRecursive(RemoteLibrary library, string relativeAddress, IRemoteIndexer indexer, int parentId)
        {
            long totalSize = 0;

            try
            {
                var indexes = indexer.GetItemList(relativeAddress, library.MaxRetries); // Fetch items from remote
                var items = new List<RemoteItem>();
                items.AddRange(RemoteItem.Provider.GetList(library.Id, parentId)); // Fetch items from db

                foreach (var index in indexes)
                {
                    RemoteItem item = null;
                    var existingItems = items.Where(i => i.IsSimilar(index, relativeAddress, parentId, library.Id));
                    if (existingItems.Count() > 0)
                    {
                        item = existingItems.FirstOrDefault(i => i.Equals(index, relativeAddress, parentId, library.Id));
                        if (item == null)
                        {
                            // Remote item got updated, now update the index
                            WriteLog("[{0}] {1} Updating index for {2}, {3}.", TaskName, DateTime.Now, relativeAddress, index.Name);

                            item = existingItems.FirstOrDefault(i => i.IsSimilar(index, relativeAddress, parentId, library.Id));
                            item.Synch(index, relativeAddress, parentId, library.Id);
                            item.Update();

                            indexer.SaveToCache(item.FullPath, item.BuildCachePath(library), _maxRetry);
                        }
                        else
                        {
                            //WriteLog("[{0}] {1} Index already exists for {2}, {3}.", TaskName, DateTime.Now, relativeAddress, item.Name);
                            
                            // Existing item did not change
                            if(!item.IsCached)
                                indexer.SaveToCache(item.FullPath, item.BuildCachePath(library), _maxRetry);
                        }

                        items.Remove(item); // Remove from temp cache what has been processed (so the remaining items will be the orphans)
                    }
                    else
                    {
                        //WriteLog("[{0}] {1} Creating index for {2}, {3}.", TaskName, DateTime.Now, relativeAddress, index.Name);

                        // Create new indexes
                        item = new RemoteItem(index, relativeAddress, parentId, library.Id);
                        item.Update();

                        indexer.SaveToCache(item.FullPath, item.BuildCachePath(library), _maxRetry);
                    }

                    if (item.IsDirectory)
                    {
                        string newRelativeAddress = FileHelper.Combine(relativeAddress, item.Name, indexer.PathSeparator);
                        WriteLog("[{0}] {1} Fetching children of {2}", TaskName, DateTime.Now, newRelativeAddress);
                        
                        var size = CreateIndexesRecursive(library, newRelativeAddress, indexer, item.Id);
                        item.Size = size;
                        item.Update();
                    }

                    totalSize += item.Size;
                }

                if (items.Count() > 0)
                {
                    // Db items without match -- orphans
                    foreach (var item in items)
                    {
                        WriteLog("[{0}] {1} Deleting index for non-existing object {2}, {3}", TaskName, DateTime.Now, relativeAddress, item.Name);
                        item.DeleteNode();
                    }
                }
            }
            catch (Exception ex)
            {
                if (_maxSkip == 0)
                {
                    throw ex;
                }
                else if (_maxSkip > 0)
                {
                    WriteLog("Exception encountered. Skipping this error... (Skip remaining: {0})", _maxSkip);
                    WriteLog(ex.ToString());

                    _maxSkip--;
                }
            }

            return totalSize;
        }
    }
}
