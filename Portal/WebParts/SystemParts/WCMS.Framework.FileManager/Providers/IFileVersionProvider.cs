using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public interface IFileVersionProvider : IDataProvider<FileVersion>
    {
        IEnumerable<FileVersion> GetList(int fileId);
    }
}
