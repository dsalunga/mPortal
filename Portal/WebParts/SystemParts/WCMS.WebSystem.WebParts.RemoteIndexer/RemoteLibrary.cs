using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.WebParts.RemoteIndexer.Providers;

namespace WCMS.WebSystem.WebParts.RemoteIndexer
{
    /// <summary>
    /// Represents a Remote Library like an FTP site or HTTP-FTP site
    /// </summary>
    public class RemoteLibrary : NamedWebObject, ISelfManager
    {
        private static IRemoteLibraryProvider _provider;

        static RemoteLibrary()
        {
            _provider = new RemoteLibrarySqlProvider();
        }

        public RemoteLibrary()
        {
            SourceTypeId = -1;
            FileCacheEnabled = 0;
            FileCacheFolder = string.Empty;
            FileCacheMinDownloadCount = -1;
            FileCacheCeilingSize = -1;
            FileCacheMaxSize = -1;
            FileCacheMinDiskFreeMB = -1;
            Size = 0;

            MaxRetries = 3;
            MaxSkip = 8;
            LastIndexDate = WConstants.DateTimeMinValue;
            DownloadCountSince = DateTime.Now;
        }

        public int SourceTypeId { get; set; }
        public string BaseAddress { get; set; }
        public string DisplayBaseAddress { get; set; }

        [XmlIgnore]
        public string UserName { get; set; }

        [XmlIgnore]
        public string Password { get; set; }

        public DateTime LastIndexDate { get; set; }
        public int Active { get; set; }
        public DateTime DownloadCountSince { get; set; }
        public int FileCacheEnabled { get; set; }
        public string FileCacheFolder { get; set; }
        public int FileCacheMinDownloadCount { get; set; }
        public int FileCacheCeilingSize { get; set; }
        public int FileCacheMaxSize { get; set; }
        public int FileCacheMinDiskFreeMB { get; set; }
        public Int64 Size { get; set; }

        /// <summary>
        /// Maximum retries in fetching an address
        /// </summary>
        public int MaxRetries { get; set; }

        /// <summary>
        /// Maximum times to skip a failed fetch (after the retries) and continue to the next address
        /// +N: Number of times to skip before raising the Exception
        /// 0: Exit on failure
        /// -1: Always skip infinitely
        /// </summary>
        public int MaxSkip { get; set; }

        public bool IsActive
        {
            get { return Active == 1; }
            set { Active = value ? 1 : 0; }
        }

        public bool IsFileCacheEnabled
        {
            get { return FileCacheEnabled == 1; }
            set { FileCacheEnabled = value ? 1 : 0; }
        }

        private string _cachePath = null;
        public string GetCachePath()
        {
            if (_cachePath == null)
            {
                _cachePath = FileHelper.Combine(WebHelper.MapPath(WConfig.FileCachePath), Path.DirectorySeparatorChar, "Indexer", this.Id.ToString());

                if (!Directory.Exists(_cachePath))
                    Directory.CreateDirectory(_cachePath);
            }

            return _cachePath;
        }

        public override int OBJECT_ID { get { return IndexerConstants.RemoteLibrary_OID; } }

        public static IRemoteLibraryProvider Provider { get { return _provider; } }

        public void RunIndexer(string taskName, RemoteIndexerTask task = null)
        {
            RemoteLibraryIndexer idxr = new RemoteLibraryIndexer(this.MaxSkip, task);
            idxr.TaskName = taskName;
            idxr.Start(this);

            this.LastIndexDate = DateTime.Now;
            this.Update();
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }
    }
}
