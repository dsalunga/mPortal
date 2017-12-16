using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public interface IFileIdentityProvider : IDataProvider<FileIdentity>
    {
        FileIdentity Get(string filePath, string name, int objectId, int recordId);
        IEnumerable<FileIdentity> GetList(string filePath, int objectId, int recordId);
    }
}
