using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.RemoteIndexer.Common
{
    public class WindowsFileSystemIndexer : IRemoteIndexer
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BaseAddress { get; set; }

        public char PathSeparator { get { return IndexerConstants.FileSeparator; } }

        private IWebProxy _proxy = null;

        public WindowsFileSystemIndexer()
        {
            _proxy = WebRequest.GetSystemWebProxy();
        }

        public WindowsFileSystemIndexer(string baseAddress, string userName, string password)
            : this()
        {
            BaseAddress = baseAddress;
            UserName = userName;
            Password = password;
        }

        public bool SaveToCache(string itemAddress, string target, int maxRetries)
        {
            return true;
        }

        public void DeleteCache(string cachePath)
        {
        }

        public List<FileStruct> GetItemList(string relativeOrPartialAddress, int maxRetries)
        {
            int remRetries = maxRetries > 0 ? maxRetries : 0;
            List<FileStruct> itemList = new List<FileStruct>();
            string absPath = relativeOrPartialAddress.StartsWith(BaseAddress) ? relativeOrPartialAddress : FileHelper.Combine(BaseAddress, relativeOrPartialAddress, PathSeparator);
            Exception exception = null;

            do
            {
                try
                {
                    DirectoryInfo rootDir = new DirectoryInfo(absPath);
                    var dirs = rootDir.EnumerateDirectories();
                    var files = rootDir.EnumerateFiles();

                    foreach (var dir in dirs)
                    {
                        FileStruct item = new FileStruct();
                        item.DateModified = dir.LastWriteTime;
                        item.IsDirectory = true;
                        item.Name = dir.Name;

                        itemList.Add(item);
                    }
                    
                    foreach (var file in files)
                    {
                        FileStruct item = new FileStruct();
                        item.DateModified = file.LastWriteTime;
                        item.IsDirectory = false;
                        item.Size = file.Length;
                        item.Name = file.Name;
                        itemList.Add(item);
                    }

                    exception = null;
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                remRetries--;
            }
            while (remRetries > 0 && exception != null);

            if (exception != null)
                throw exception;

            return itemList;
        }
    }
}
