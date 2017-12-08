using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using WCMS.Common.Utilities;

namespace WCMS.Common
{
    public class FileLogger : ILogger
    {
        private string _filePath;
        private string _extension;
        private string _fileNameWithoutExt;

        public FileLogger()
        {

        }

        public string FilePath
        {
            get
            {
                return string.Format(@"{0}\{1}_{2}{3}",
                    _filePath,
                    _fileNameWithoutExt,
                    DateTime.Now.ToString("yyyyMMdd"),
                    _extension
                );
            }
        }

        public FileLogger(string filePath)
        {
            if (filePath.StartsWith("~"))
                filePath = WebHelper.MapPath(filePath);

            string fileName = Path.GetFileName(filePath);

            _filePath = Path.GetDirectoryName(filePath);
            _extension = Path.GetExtension(fileName);
            _fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
        }

        #region ILogger Members

        public void WriteLine(string format, params object[] args)
        {
            LogHelper.WriteLog(FilePath, format, args);
        }

        #endregion
    }
}
