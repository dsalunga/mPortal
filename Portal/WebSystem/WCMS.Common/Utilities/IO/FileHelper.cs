using System;
using System.IO;
using System.Text;

namespace WCMS.Common.Utilities
{
    /// <summary>
    /// Summary description for FileHelper.
    /// </summary>
    public abstract class FileHelper
    {
        private static readonly long Gigabyte = 1024 * 1024 * 1024;
        private static readonly long Megabyte = 1024 * 1024;
        private static readonly long Kilobyte = 1024;

        public static bool WriteFile(string content, string fileName)
        {
            return WriteFile(content, fileName, Encoding.Unicode);
        }

        public static bool WriteFile(string content, string fileName, Encoding encoding)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(stream, encoding))
                    writer.Write(content);
            }

            return true;
        }

        public static string GetSizeString(long size)
        {
            long l = size;

            if (l >= Gigabyte * 1024)
                return ((double)l / (Gigabyte * 1024)).ToString("N") + " TB";
            else if (l >= Gigabyte)
                return ((double)l / Gigabyte).ToString("N") + " GB";
            else if (l >= Megabyte)
                return ((double)l / Megabyte).ToString("N") + " MB";
            else if (l >= Kilobyte)
                return ((double)l / Kilobyte).ToString("N") + " KB";
            else if (l >= 0)
                return l.ToString();
            else
                return "-" + GetSizeString(l * -1);
        }

        public static long GetDirectorySize(string path)
        {
            return GetDirectorySize(new DirectoryInfo(path));
        }

        public static long GetDirectorySize(DirectoryInfo d)
        {
            long Size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                Size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                Size += GetDirectorySize(di);
            }
            return (Size);
        }


        public static long GetSizeFromString(string sizeString)
        {
            long size = -1;

            if (!long.TryParse(sizeString, out size))
            {
                if (sizeString.EndsWith("KB", StringComparison.InvariantCultureIgnoreCase)
                    || sizeString.EndsWith("MB", StringComparison.InvariantCultureIgnoreCase)
                    || sizeString.EndsWith("GB", StringComparison.InvariantCultureIgnoreCase)
                    || sizeString.EndsWith("TB", StringComparison.InvariantCultureIgnoreCase))
                {
                    string sizeUnit = sizeString.Substring(sizeString.Length - 2).ToUpper();
                    string sizeLong = sizeString.Substring(0, sizeString.Length - 2);

                    double sizeDouble = double.Parse(sizeLong);

                    switch (sizeUnit)
                    {
                        case "KB":
                            size = (long)(sizeDouble * Kilobyte);
                            break;

                        case "MB":
                            size = (long)(sizeDouble * Megabyte);
                            break;

                        case "GB":
                            size = (long)(sizeDouble * Gigabyte);
                            break;

                        case "TB":
                            size = (long)(sizeDouble * Gigabyte * 1024);
                            break;
                    }
                }
            }

            return size;
        }

        public static string ReadFile(string path)
        {
            string txt = string.Empty;

            //try
            //{
            if (File.Exists(path))
            {
                using (TextReader r = new StreamReader(path))
                {
                    txt = r.ReadToEnd();
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.LogErrors(ex);
            //}

            return txt;
        }

        public static void CreateFolderOrDeleteAllFiles(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            else
            {
                // Delete all files inside "Database" backup directory.
                var deleteFiles = Directory.GetFiles(folder);
                foreach (string deleteFile in deleteFiles)
                    File.Delete(deleteFile);
            }
        }

        public static bool IsAbsolutePath(string path)
        {
            return path.Contains(":") || path.StartsWith(@"\\");
        }

        public static string EvalPath(string path, bool createDirectory = true)
        {
            if (string.IsNullOrEmpty(path) || path == "~")
                path = @".\";

            //if (path.StartsWith("."))
            //    fullPath = Environment.CurrentDirectory + path.Substring(1);
            //else

            var fullPath = Path.GetFullPath(path);
            if (createDirectory)
            {
                var folder = GetFolder(fullPath);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
            }

            return fullPath;
        }

        public static bool IsFolder(string path)
        {
            // get the file attributes for file or directory 
            FileAttributes attr = File.GetAttributes(path);

            //detect whether its a directory or file 
            return (attr & FileAttributes.Directory) == FileAttributes.Directory;
        }

        public static string GetFolder(string folder, char separator = '\\')
        {
            var index = folder.LastIndexOf(separator);
            return index > -1 ? folder.Substring(0, index) : folder;
        }

        public static string Combine(string currentFolder, string newName, char separator = '\\')
        {
            var otherSep = separator == '/' ? '\\' : '/';
            return (string.IsNullOrEmpty(newName) ? currentFolder : Path.Combine(currentFolder, newName)).Replace(otherSep, separator);
        }

        public static string Combine(string currentFolder, char separator = '\\', params string[] paths)
        {
            var otherSep = separator == '/' ? '\\' : '/';
            return (paths.Length == 0 ? currentFolder : Path.Combine(currentFolder, Path.Combine(paths))).Replace(otherSep, separator);
        }

        public static string GenerateTempFileName(string fileName = "")
        {
            string ext = ".tmp";
            string fileNameWithoutExt = Guid.NewGuid().ToString();

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                var tmpExt = Path.GetExtension(fileName);
                if (!string.IsNullOrEmpty(tmpExt))
                    ext = tmpExt;

                var tmpFileName = Path.GetFileNameWithoutExtension(fileName);
                if (!string.IsNullOrEmpty(tmpFileName))
                    fileNameWithoutExt = string.Format("{0}_{1}", tmpFileName, fileNameWithoutExt);
            }

            return fileNameWithoutExt + ext;
        }

        public static string ChangeExtension(string filePath, string newExt)
        {
            var currExt = Path.GetExtension(filePath);
            if (!string.IsNullOrEmpty(currExt) && !currExt.StartsWith("."))
                currExt = "." + currExt;

            if (!string.IsNullOrEmpty(currExt))
            {
                var index = filePath.LastIndexOf(currExt);

                return filePath.Substring(0, index) + newExt;
            }
            else
            {
                return filePath + newExt;
            }
        }
    }
}
