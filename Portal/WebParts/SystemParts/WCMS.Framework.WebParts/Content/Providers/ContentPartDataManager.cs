using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Content.Providers
{
    public class ContentPartDataManager : IPartDataManager
    {
        Dictionary<int, WebContent> _data = new Dictionary<int, WebContent>();
        Dictionary<int, int> _idMapping = new Dictionary<int, int>(); // old > new

        #region IPartDataManager Members

        public string ExportElementData(IWebObject element, bool exportData = true)
        {
            WebObjectContent objectContent = WebObjectContent
                            .GetByObjectId(element.OBJECT_ID, element.Id);

            if (objectContent != null)
            {
                if (exportData)
                {
                    var content = objectContent.Content;
                    if (content != null && !_data.ContainsKey(content.Id))
                        _data.Add(content.Id, content);
                }

                return DataHelper.ToXml<WebObjectContent>(objectContent, "Item");
            }

            return string.Empty;
        }

        public string ExportData()
        {
            if (_data.Count > 0)
                return DataHelper.ToXml(_data.Values.AsEnumerable(), "Item", "WebContent");

            return string.Empty;
        }

        public bool ImportData(WSite site)
        {
            if (_data.Count > 0)
            {
                foreach (var item in _data.Values)
                {
                    var oldId = item.Id;

                    item.Id = -1;
                    item.SiteId = site == null ? -1 : site.Id;
                    item.Update();

                    if (!_idMapping.ContainsKey(oldId))
                        _idMapping.Add(oldId, item.Id);
                }
            }

            return true;
        }

        public bool ImportElementData(IWebObject element, XmlNode elementDataNode)
        {
            var itemNode = elementDataNode.SelectSingleNode("Item");

            if (itemNode != null)
            {
                var item = DataHelper.FromElementXml<WebObjectContent>(itemNode.OuterXml, "Item");

                item.Id = -1;
                item.ObjectId = element.OBJECT_ID;
                item.RecordId = element.Id;

                if (_idMapping.ContainsKey(item.ContentId))
                    item.ContentId = _idMapping[item.ContentId];

                item.Update();

                return true;
            }

            return false;
        }

        private bool inited = false;
        public void InitImport(XmlNode dataNode)
        {
            if (!inited)
            {
                if (dataNode != null)
                {
                    _data = new Dictionary<int, WebContent>();

                    XmlNodeList itemNodes = dataNode.SelectNodes("WebContent");
                    if (itemNodes.Count > 0)
                    {
                        foreach (XmlNode itemNode in itemNodes)
                        {
                            var item = DataHelper.FromElementXml<WebContent>(itemNode.OuterXml, "Item");
                            if (item != null)
                                _data.Add(item.Id, item);
                        }
                    }
                }

                inited = true;
            }
        }

        public bool PerformDataCleanUp()
        {
            return false;
        }

        public void DeleteElementData(IWebObject element)
        {
            WebObjectContent objectContent = WebObjectContent
                            .GetByObjectId(element.OBJECT_ID, element.Id);

            if (objectContent != null)
                objectContent.Delete();
        }

        #endregion
    }
}
