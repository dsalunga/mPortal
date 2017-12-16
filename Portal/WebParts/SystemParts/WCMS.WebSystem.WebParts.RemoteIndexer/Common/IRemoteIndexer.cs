using System;
using System.Collections.Generic;

namespace WCMS.WebSystem.WebParts.RemoteIndexer.Common
{
    interface IRemoteIndexer
    {
        string BaseAddress { get; set; }
        List<FileStruct> GetItemList(string address, int maxRetries);
        string Password { get; set; }
        string UserName { get; set; }

        bool SaveToCache(string itemAddress, string target, int maxRetries);
        void DeleteCache(string cachePath);

        char PathSeparator { get; }
    }
}
