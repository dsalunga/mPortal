using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;


namespace WCMS.WebSystem.WebParts.Contact
{
    public class ContactPartDataManager : IPartDataManager
    {
        #region IPartDataManager Members

        public string ExportElementData(IWebObject element, bool exportData = true)
        {
            var item = ContactLink.Get(element.OBJECT_ID, element.Id);

            if (item != null)
                return DataHelper.ToXml<ContactLink>(item, "Item");

            return string.Empty;
        }

        public string ExportData()
        {
            return string.Empty;
        }

        public bool ImportData(WSite site)
        {
            return true;
        }

        public bool ImportElementData(IWebObject element, XmlNode elementDataNode)
        {
            var itemNode = elementDataNode.SelectSingleNode("Item");

            if (itemNode != null)
            {
                var item = DataHelper.FromElementXml<ContactLink>(itemNode.OuterXml, "Item");

                item.Id = -1;
                item.ObjectId = element.OBJECT_ID;
                item.RecordId = element.Id;

                item.Update();

                return true;
            }

            return false;
        }

        private bool inited = false;
        public void InitImport(XmlNode dataNode)
        {
            if (!inited)
                inited = true;
        }

        public bool PerformDataCleanUp()
        {
            return false;
        }

        public void DeleteElementData(IWebObject element)
        {
            var item = ContactLink.Get(element.OBJECT_ID, element.Id);

            if (item != null)
                item.Delete();
        }

        #endregion
    }
}
