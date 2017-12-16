using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public class FileManagerFolder
    {
        private static FileManagerFolderProvider _provider;

        static FileManagerFolder()
        {
            _provider = new FileManagerFolderProvider();
        }

        public FileManagerFolder(){}

        public string Name { get; set; }
        public string FullPath { get; set; }
        public DateTime DateModified { get; set; }

        public static IEnumerable<FileManagerFolder> GetList(string path)
        {
            return _provider.GetList(path);
        }

        public static bool Delete(string path)
        {
            return _provider.Delete(path);
        }
    }
}
