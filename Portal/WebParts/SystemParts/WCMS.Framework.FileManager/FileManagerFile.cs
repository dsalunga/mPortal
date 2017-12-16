using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public class FileManagerFile
    {
        private static FileManagerFileProvider _provider;

        static FileManagerFile()
        {
            _provider = new FileManagerFileProvider();
        }

        public string Name { get; set; }
        public string FullPath { get; set; }
        public long Size { get; set; }
        public DateTime DateModified { get; set; }
        public string Extension { get; set; }

        public string SizeString
        {
            get
            {
                return FileHelper.GetSizeString(Size);
            }
        }

        public static IEnumerable<FileManagerFile> GetList(string path)
        {
            return _provider.GetList(path);
        }

        public static bool Delete(string fileName, string path)
        {
            return _provider.Delete(fileName, path);
        }

        public static bool Delete(string filePath)
        {
            return _provider.Delete(filePath);
        }
    }
}
