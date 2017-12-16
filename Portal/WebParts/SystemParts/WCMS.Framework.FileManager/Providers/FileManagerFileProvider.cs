using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public class FileManagerFileProvider : WebContextBase
    {
        public IEnumerable<FileManagerFile> GetList(string path)
        {
            List<FileManagerFile> items = new List<FileManagerFile>();

            var files = Directory.EnumerateFiles(WebHelper.MapPath(path)); // + @"\" + sFileType); // images / documents
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                FileManagerFile item = new FileManagerFile();
                item.Name = Path.GetFileName(file); ;
                item.FullPath = WebHelper.CombineAddress(path, item.Name);
                item.Size = fileInfo.Length;
                item.Extension = fileInfo.Extension.TrimStart('.');
                item.DateModified = fileInfo.LastWriteTime;

                items.Add(item);
            }

            return items;
        }

        public bool Delete(string sFile, string sPath)
        {
            string virtualFile = WebHelper.CombineAddress(sPath, sFile);

            return Delete(virtualFile);
        }

        public bool Delete(string sVirtualFile)
        {
            try
            {
                File.Delete(WebHelper.MapPath(sVirtualFile));
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }
    }
}
