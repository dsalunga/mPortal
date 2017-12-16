using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public class FileManagerFolderProvider : WebContextBase
    {
        public FileManagerFolderProvider() { }

        public IEnumerable<FileManagerFolder> GetList(string path)
        {
            List<FileManagerFolder> items = new List<FileManagerFolder>();

            var parentFolder = WebHelper.MapPath(path);

            if (true || Directory.Exists(parentFolder)) // Check folder exists ???
            {
                var folders = Directory.EnumerateDirectories(parentFolder);
                foreach (string folder in folders)
                {
                    DirectoryInfo folderInfo = new DirectoryInfo(folder);
                    FileManagerFolder item = new FileManagerFolder();
                    item.Name = Path.GetFileName(folder);
                    item.DateModified = folderInfo.LastWriteTime;

                    items.Add(item);
                }
            }

            return items;
        }

        public bool Delete(string path)
        {
            try
            {
                Directory.Delete(WebHelper.MapPath(path), true);
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }
    }
}
