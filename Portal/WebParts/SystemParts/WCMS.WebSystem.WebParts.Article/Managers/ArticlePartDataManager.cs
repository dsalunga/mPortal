using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;


namespace WCMS.WebSystem.WebParts.Article.Managers
{
    public class ArticlePartDataManager : IPartDataManager
    {
        public void DeleteElementData(IWebObject element)
        {
            var links = ArticleLink.GetList(element.OBJECT_ID, element.Id);
            if (links.Count() > 0)
            {
                for (int i = links.Count() - 1; i >= 0; i--)
                {
                    var link = links.ElementAt(i);
                    link.Delete();
                }
            }

            var articleObject = ArticleList.Get(element.OBJECT_ID, element.Id);
            if (articleObject != null)
                articleObject.Delete();
        }

        public string ExportData()
        {
            return string.Empty;
        }

        public string ExportElementData(IWebObject element, bool exportData = true)
        {
            StringBuilder output = new StringBuilder();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.NewLineOnAttributes = false;
            settings.Indent = false;
            settings.OmitXmlDeclaration = true;
            settings.Encoding = Encoding.Unicode;

            XmlWriter writer = XmlWriter.Create(output, settings);

            var articleObject = ArticleList.Get(element.OBJECT_ID, element.Id);
            if (articleObject != null)
                writer.WriteRaw(DataHelper.ToXml<ArticleList>(articleObject, "Object"));

            var links = ArticleLink.GetList(element.OBJECT_ID, element.Id);

            if (links.Count() > 0)
            {
                writer.WriteStartElement("Links");

                foreach (var link in links)
                    writer.WriteRaw(DataHelper.ToXml<ArticleLink>(link));

                writer.WriteEndElement();
            }

            writer.Flush();

            return output.ToString();
        }

        public bool ImportData(WSite site)
        {
            return true;
        }

        public bool ImportElementData(IWebObject element, XmlNode elementDataNode)
        {
            var objectNode = elementDataNode.SelectSingleNode("Object");
            if (objectNode != null)
            {
                var item = DataHelper.FromElementXml<ArticleList>(objectNode.OuterXml, "Object");
                if (item != null)
                {
                    item.Id = -1;
                    item.ObjectId = element.OBJECT_ID;
                    item.RecordId = element.Id;
                    //item.SiteId = -1;
                    item.Update();
                }

                var linksNode = elementDataNode.SelectSingleNode("Links");
                if (linksNode != null)
                {
                    var linkNodes = linksNode.ChildNodes;
                    if (linkNodes.Count > 0)
                    {
                        foreach (XmlNode linkNode in linkNodes)
                        {
                            var link = DataHelper.FromElementXml<ArticleLink>(linkNode.OuterXml);
                            if (link != null)
                            {
                                link.Id = -1;
                                link.ObjectId = element.OBJECT_ID;
                                link.RecordId = element.Id;
                                link.Update();
                            }
                        }
                    }
                }

                return true;
            }

            return false;
        }

        public void InitImport(XmlNode dataNode)
        {
            
        }

        public bool PerformDataCleanUp()
        {
            return true;
        }
    }
}
