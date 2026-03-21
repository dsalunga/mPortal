using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public interface IPartDataManager
    {
        bool PerformDataCleanUp();

        string ExportData();
        string ExportElementData(IWebObject element, bool exportData = true);

        void InitImport(XmlNode dataNode);
        bool ImportElementData(IWebObject element, XmlNode elementDataNode);
        bool ImportData(WSite site);
        void DeleteElementData(IWebObject element);
    }
}
