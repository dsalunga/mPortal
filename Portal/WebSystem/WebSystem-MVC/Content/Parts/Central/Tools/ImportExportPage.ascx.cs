using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using System.IO;

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class ImportExportPage : System.Web.UI.UserControl
    {
        List<PartDataManagerModel> partDataManagers = new List<PartDataManagerModel>();
        List<PartDataManagerModel> usedManagerModels = new List<PartDataManagerModel>();

        Dictionary<int, int> masterPageMapping = new Dictionary<int, int>();
        Dictionary<int, int> pageMapping = new Dictionary<int, int>();

        XmlNode referenceDataNode = null;
        WSite currentSite = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        private IPartDataManager GetPartDataManager(IPageElement element, bool init = false)
        {
            if (element != null)
            {
                var partTemplate = element.PartControlTemplate;
                if (partTemplate != null)
                {
                    var partControl = partTemplate.PartControl;
                    if (partControl != null)
                    {
                        PartDataManagerModel model = null;
                        PartDataManagerModel firstModel = null;

                        foreach (var manager in partDataManagers)
                        {
                            if (manager.PartId == partControl.PartId)
                            {
                                if (manager.PartControlId == partControl.Id)
                                {
                                    model = manager;
                                    break;
                                }
                                else if (firstModel == null && manager.PartControlId == -1)
                                {
                                    firstModel = manager;
                                }
                            }
                        }

                        if (model == null && firstModel != null)
                            model = firstModel;

                        if (model != null)
                        {
                            if (usedManagerModels.Find(i => i.PartId == model.PartId && i.PartControlId == model.PartControlId) == null)
                            {
                                if (init)
                                {
                                    var manager = model.GetManager();
                                    if (manager != null)
                                    {
                                        manager.InitImport(referenceDataNode);

                                        if (chkPartData.Checked)
                                            manager.ImportData(currentSite);
                                    }
                                }

                                usedManagerModels.Add(model);
                            }

                            return model.GetManager();
                        }
                    }
                }
            }

            return null;
        }

        protected void cmdExport_Click(object sender, EventArgs e)
        {
            string pageUrl = txtPageUrl.Text.Trim();
            if (!string.IsNullOrEmpty(pageUrl))
            {
                WPage page = WPage.Resolve(pageUrl);
                if (page != null)
                {
                    var site = page.Site;
                    var webSite = chkWebSite.Checked;

                    currentSite = site;


                    // Part Data Managers
                    if (chkElementData.Checked)
                    {
                        usedManagerModels = new List<PartDataManagerModel>();
                        partDataManagers = WHelper.GetPartDataManagers();
                    }


                    StringBuilder output = new StringBuilder();
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.NewLineOnAttributes = false;
                    settings.Indent = true;
                    settings.OmitXmlDeclaration = false;
                    settings.Encoding = Encoding.Unicode;
                    settings.ConformanceLevel = ConformanceLevel.Auto;

                    XmlWriter writer = XmlWriter.Create(output, settings);

                    writer.WriteStartElement("WebBackupXML");
                    writer.WriteAttributeString("DateCreated", DateTime.Now.ToString());
                    writer.WriteAttributeString("CreatedBy", WSession.Current.User.FirstAndLastName);
                    writer.WriteAttributeString("RelativePageUrl", pageUrl);

                    writer.WriteStartElement("ObjectModel");

                    writer.WriteStartElement("Sites");

                    writer.WriteStartElement("Site");
                    writer.WriteAttributeString("Identity", site.Identity);

                    if (webSite)
                        writer.WriteRaw(DataHelper.ToXml(site, "Item"));


                    if (chkWebPages.Checked || chkCurrentPage.Checked)
                    {
                        writer.WriteStartElement("Pages");

                        if (chkWebPages.Checked)
                        {
                            var pages = page.Site.RootPages;
                            foreach (var p in pages)
                                WritePageRecursive(p, writer);
                        }
                        else if (chkCurrentPage.Checked)
                        {
                            WritePageRecursive(page, writer);
                        }

                        writer.WriteEndElement(); // Pages
                    }

                    // MasterPages
                    if (chkMasterPages.Checked)
                    {
                        var masterPages = site.MasterPages;
                        if (masterPages.Count() > 0)
                        {
                            writer.WriteStartElement("MasterPages");

                            foreach (var masterPage in masterPages)
                            {
                                writer.WriteStartElement("MasterPage");
                                writer.WriteAttributeString("Name", masterPage.Name);

                                writer.WriteRaw(DataHelper.ToXml(masterPage, "Item"));

                                if (chkElements.Checked)
                                    WriteElements(masterPage.Elements, writer);

                                if (chkResources.Checked)
                                    WriteResources(masterPage, writer); // Resources

                                if (chkSecurity.Checked)
                                    WriteSecurity(masterPage, writer); // Security

                                if (chkParameters.Checked)
                                    ParameterizedWebObject.WriteParameters(masterPage, writer); // Parameters

                                writer.WriteEndElement();
                            }

                            writer.WriteEndElement();
                        }
                    }

                    if (webSite)
                    {
                        if (chkResources.Checked)
                            WriteResources(site, writer); // Resources

                        if (chkSecurity.Checked)
                            WriteSecurity(site, writer); // Security

                        if (chkParameters.Checked)
                            ParameterizedWebObject.WriteParameters(site, writer); // Parameters

                        // Identities

                        if (chkSiteIdentities.Checked)
                        {
                            var identities = site.Identities;
                            if (identities.Count() > 0)
                            {
                                writer.WriteStartElement("Identities");

                                foreach (var identity in identities)
                                    writer.WriteRaw(DataHelper.ToXml(identity));

                                writer.WriteEndElement();
                            }
                        }
                    }

                    writer.WriteEndElement(); // Site

                    writer.WriteEndElement(); // Sites

                    writer.WriteEndElement(); // ObjectModel

                    // ReferenceData
                    writer.WriteStartElement("ReferenceData");

                    if (chkPartData.Checked)
                    {
                        foreach (var managerModel in usedManagerModels)
                        {
                            var manager = managerModel.GetManager();
                            if (manager != null)
                            {
                                var partData = manager.ExportData();
                                if (!string.IsNullOrEmpty(partData))
                                    writer.WriteRaw(partData);
                            }
                        }
                    }

                    writer.WriteEndElement();

                    writer.WriteEndElement(); // WebBackupXML

                    writer.Flush();

                    var webPath = WebHelper.CombineAddress(WConfig.TempFolder, string.Format("Backup-XML-{0:yyyyMMdd}.xml", DateTime.Now));
                    var path = WebHelper.MapPath(webPath);

                    FileHelper.WriteFile(output.ToString(), path);

                    WebHelper.DownloadFile(path, needMapping: false);
                }
            }
        }

        private void WritePageRecursive(WPage page, XmlWriter writer)
        {
            writer.WriteStartElement("Page");
            writer.WriteAttributeString("Identity", page.Identity);

            // Page Item
            var pageXml = DataHelper.ToXml(page, "Item");
            writer.WriteRaw(pageXml);

            // Panels
            if (chkPanels.Checked)
            {
                var panels = page.Panels;
                if (panels.Count() > 0)
                {
                    writer.WriteStartElement("Panels");

                    foreach (var panel in panels)
                        writer.WriteRaw(DataHelper.ToXml(panel));

                    writer.WriteEndElement();
                }
            }

            // Element Data
            if (chkElementData.Checked)
                WriteElementData(page, writer);

            // Elements
            if (chkElements.Checked)
                WriteElements(page.Elements, writer);

            // Resources
            if (chkResources.Checked)
                WriteResources(page, writer);

            // Security
            if (chkSecurity.Checked)
                WriteSecurity(page, writer);

            // Parameters
            if (chkParameters.Checked)
                ParameterizedWebObject.WriteParameters(page, writer);

            if (chkChildren.Checked)
            {
                var children = page.Children;
                if (children.Count() > 0)
                {
                    writer.WriteStartElement("Children");

                    foreach (var child in children)
                        WritePageRecursive(child, writer);

                    writer.WriteEndElement();
                }
            }

            writer.WriteEndElement(); // Page
        }

        private void WriteElementData(IPageElement page, XmlWriter writer)
        {
            if (chkElementData.Checked)
            {
                var manager = GetPartDataManager(page);
                if (manager != null)
                {
                    var rawDataXml = manager.ExportElementData(page);
                    if (!string.IsNullOrEmpty(rawDataXml))
                    {
                        writer.WriteStartElement("Data");

                        writer.WriteRaw(rawDataXml);

                        writer.WriteEndElement();
                    }
                }
            }
        }

        private void WriteElements(IEnumerable<WebPageElement> elements, XmlWriter writer)
        {
            if (elements.Count() > 0)
            {
                writer.WriteStartElement("Elements");

                foreach (var element in elements)
                {
                    writer.WriteStartElement("Element");

                    var elementXml = DataHelper.ToXml(element, "Item");
                    writer.WriteRaw(elementXml);

                    // Element Data
                    if (chkElementData.Checked)
                        WriteElementData(element, writer);

                    // Resources
                    if (chkResources.Checked)
                        WriteResources(element, writer);

                    // Security
                    if (chkSecurity.Checked)
                        WriteSecurity(element, writer);

                    // Parameters
                    if (chkParameters.Checked)
                        ParameterizedWebObject.WriteParameters(element, writer);

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }

        private void WriteSecurity(SecurableObject item, XmlWriter writer)
        {
            var securities = item.GetObjectSecurities();
            if (securities.Count() > 0)
            {
                writer.WriteStartElement("Security");
                writer.WriteStartElement("ObjectSecurities");

                foreach (var security in securities)
                {
                    writer.WriteStartElement("ObjectSecurity");

                    writer.WriteRaw(DataHelper.ToXml(security, "Item"));

                    var perms = security.ObjectSecurityPermissions;
                    if (perms.Count() > 0)
                    {
                        writer.WriteStartElement("SecurityPermissions");

                        foreach (var perm in perms)
                            writer.WriteRaw(DataHelper.ToXml(perm));

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        private void WriteResources(ParameterizedWebObject item, XmlWriter writer)
        {
            var resources = item.Headers;
            if (resources.Count() > 0)
            {
                writer.WriteStartElement("Resources");

                foreach (var resource in resources)
                    writer.WriteRaw(DataHelper.ToXml(resource));

                writer.WriteEndElement();
            }
        }

        protected void cmdImport_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string pageUrl = txtPageUrl.Text.Trim();
                if (!string.IsNullOrEmpty(pageUrl))
                {
                    WSite site = null;
                    WPage refPage = null;
                    int parentId = -1;

                    var restore = DataHelper.GetInt32(cboRestore.SelectedValue);

                    // Make sure TempFolder is present
                    var absTempPath = WebHelper.MapPath(WConfig.TempFolder);
                    if (!Directory.Exists(absTempPath))
                        Directory.CreateDirectory(absTempPath);

                    var fileName = FileUpload1.FileName;
                    var webPath = WebHelper.CombineAddress(WConfig.TempFolder, fileName);
                    var path = WebHelper.MapPath(webPath);

                    FileUpload1.PostedFile.SaveAs(path);

                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);

                    referenceDataNode = doc.SelectSingleNode("//ReferenceData");

                    var siteNode = doc.SelectSingleNode("//Sites/Site");

                    if (pageUrl == "/" && chkWebSite.Checked)
                    {
                        site = DataHelper.FromElementXml<WSite>(siteNode.SelectSingleNode("Item").OuterXml, "Item");
                        if (site != null)
                        {
                            site.Id = -1;
                            site.ParentId = -1;
                            site.Update();
                        }
                    }
                    else
                    {
                        refPage = WPage.Resolve(pageUrl);
                        if (refPage != null)
                        {
                            site = refPage.Site;

                            parentId = restore == 0 ? -1 : restore == 1 ? refPage.Id : refPage.ParentId;
                        }
                    }

                    currentSite = site;

                    // Part Data Managers
                    if (chkElementData.Checked)
                    {
                        usedManagerModels = new List<PartDataManagerModel>();
                        partDataManagers = WHelper.GetPartDataManagers();
                    }

                    // MasterPages
                    RestoreMasterPages(site, siteNode);

                    // Pages

                    pageMapping = new Dictionary<int, int>();
                    if (chkWebPages.Checked)
                    {
                        XmlNodeList pageNodes = siteNode.SelectNodes("Pages/Page");
                        if (pageNodes.Count > 0)
                            RestorePages(site, parentId, pageNodes);
                    }

                    lblStatus.Text = "Restore completed.";
                }
            }
        }

        private void RestorePages(WSite site, int parentId, XmlNodeList pageNodes)
        {
            if (chkWebPages.Checked)
            {
                var siblings = WPage.GetList(site.Id, parentId);
                foreach (XmlNode pageNode in pageNodes)
                {
                    var page = DataHelper.FromElementXml<WPage>(pageNode.SelectSingleNode("Item").OuterXml, "Item");
                    if (page != null)
                    {
                        var currPage = siblings.FirstOrDefault(i => i.Identity.Equals(page.Identity, StringComparison.InvariantCultureIgnoreCase));
                        if (currPage == null)
                        {
                            var oldId = page.Id;

                            // Reset
                            page.Id = -1;
                            page.SiteId = site.Id;
                            page.ParentId = parentId;

                            if (page.MasterPageId > 0 && masterPageMapping.Count > 0 && masterPageMapping.ContainsKey(page.MasterPageId))
                                page.MasterPageId = masterPageMapping[page.MasterPageId];

                            page.Update();

                            pageMapping.Add(oldId, page.Id);

                            if (site.HomePageId == oldId)
                            {
                                site.HomePageId = page.Id;
                                site.Update();
                            }

                            var childNodes = pageNode.SelectNodes("Children/Page");
                            if (childNodes.Count > 0)
                                RestorePages(site, page.Id, childNodes);

                            // Panels
                            RestorePanels(page, pageNode);

                            // Data
                            RestoreElementData(page, pageNode);

                            // Restore Parameters
                            RestoreParameters(page, pageNode);

                            // Resources
                            RestoreResources(page, pageNode);

                            // Security
                            RestoreSecurity(page, pageNode);

                            // Elements
                            RestoreElements(page, pageNode);
                        }
                    }
                }
            }
        }

        private void RestorePanels(WPage page, XmlNode pageNode)
        {
            if (chkPanels.Checked)
            {
                var panelsNode = pageNode.SelectSingleNode("Panels");
                if (panelsNode != null)
                {
                    var panelNodes = panelsNode.ChildNodes;
                    if (panelsNode.ChildNodes.Count > 0)
                    {
                        foreach (XmlNode panelNode in panelNodes)
                        {
                            var panel = DataHelper.FromElementXml<WebPagePanel>(panelNode.OuterXml);
                            if (panel != null)
                            {
                                panel.Id = -1;

                                if (pageMapping.Count > 0 && pageMapping.ContainsKey(panel.PageId))
                                    panel.PageId = pageMapping[panel.PageId];

                                panel.Update();
                            }
                        }
                    }
                }
            }
        }

        private void RestoreElementData(IPageElement element, XmlNode pageNode)
        {
            if (chkElementData.Checked)
            {
                var dataNode = pageNode.SelectSingleNode("Data");
                if (dataNode != null)
                {
                    var manager = GetPartDataManager(element, true);
                    if (manager != null)
                        manager.ImportElementData(element, dataNode);
                }
            }
        }

        private void RestoreMasterPages(WSite site, XmlNode siteNode)
        {
            masterPageMapping = new Dictionary<int, int>();

            if (chkMasterPages.Checked)
            {
                XmlNodeList masterPageNodes = siteNode.SelectNodes("MasterPages/MasterPage");
                if (masterPageNodes.Count > 0)
                {
                    foreach (XmlNode masterPageNode in masterPageNodes)
                    {
                        var masterPage = DataHelper.FromElementXml<WebMasterPage>(masterPageNode.SelectSingleNode("Item").OuterXml, "Item");
                        if (masterPage != null)
                        {
                            var oldId = masterPage.Id;

                            // Reset
                            masterPage.Id = -1;
                            masterPage.SiteId = site.Id;
                            masterPage.Update();

                            masterPageMapping.Add(oldId, masterPage.Id);

                            if (site.DefaultMasterPageId == oldId)
                            {
                                site.DefaultMasterPageId = masterPage.Id;
                                site.Update();
                            }

                            // Restore Parameters
                            RestoreParameters(masterPage, masterPageNode);

                            // Resources
                            RestoreResources(masterPage, masterPageNode);

                            // Security
                            RestoreSecurity(masterPage, masterPageNode);

                            // Elements
                            RestoreElements(masterPage, masterPageNode);
                        }
                    }
                }
            }
        }

        private void RestoreElements(IWebObject parent, XmlNode parentNode)
        {
            if (chkElements.Checked)
            {
                XmlNodeList elementNodes = parentNode.SelectNodes("Elements/Element");
                if (elementNodes.Count > 0)
                {
                    foreach (XmlNode elementNode in elementNodes)
                    {
                        var element = DataHelper.FromElementXml<WebPageElement>(elementNode.SelectSingleNode("Item").OuterXml, "Item");
                        if (element != null)
                        {
                            // Reset
                            element.Id = -1;

                            if (element.ObjectId == WebObjects.WebPage)
                                element.RecordId = parent.Id;
                            else if (element.ObjectId == WebObjects.WebMasterPage && masterPageMapping.Count > 0 && masterPageMapping.ContainsKey(element.RecordId))
                                element.RecordId = masterPageMapping[element.RecordId];

                            element.Update();

                            // Data
                            RestoreElementData(element, elementNode);

                            // Restore Parameters
                            RestoreParameters(element, elementNode);

                            // Resources
                            RestoreResources(element, elementNode);

                            // Security
                            RestoreSecurity(element, elementNode);
                        }
                    }
                }
            }
        }

        private void RestoreSecurity(IWebObject item, XmlNode itemNode)
        {
            if (chkSecurity.Checked)
            {
                var objectSecurityNodes = itemNode.SelectNodes("Security/ObjectSecurities/ObjectSecurity");
                if (objectSecurityNodes.Count > 0)
                {
                    foreach (XmlNode objectSecurityNode in objectSecurityNodes)
                    {
                        XmlNode node = objectSecurityNode.SelectSingleNode("Item");

                        if (node != null)
                        {
                            var objectSecurity = DataHelper.FromElementXml<WebObjectSecurity>(node.OuterXml, "Item");
                            if (objectSecurity != null)
                            {
                                objectSecurity.Id = -1;
                                objectSecurity.ObjectId = item.OBJECT_ID;
                                objectSecurity.RecordId = item.Id;
                                objectSecurity.Update();

                                // Permissions
                                var securityPermissionNodes = objectSecurityNode.SelectSingleNode("SecurityPermissions");
                                if (securityPermissionNodes != null)
                                {
                                    var permissionNodes = securityPermissionNodes.ChildNodes;
                                    if (permissionNodes.Count > 0)
                                    {
                                        foreach (XmlNode permissionNode in permissionNodes)
                                        {
                                            var securityPermission = DataHelper.FromElementXml<WebObjectSecurityPermission>(permissionNode.OuterXml);
                                            if (securityPermission != null)
                                            {
                                                securityPermission.Id = -1;
                                                securityPermission.ObjectSecurityId = objectSecurity.Id;
                                                securityPermission.Update();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void RestoreParameters(IWebObject item, XmlNode itemNode)
        {
            if (chkParameters.Checked)
                ParameterizedWebObject.RestoreParameters(item, itemNode);
        }

        private void RestoreResources(IWebObject item, XmlNode itemNode)
        {
            if (chkResources.Checked)
            {
                var resourceNodes = itemNode.SelectSingleNode("Resources");
                if (resourceNodes != null)
                {
                    var nodes = resourceNodes.ChildNodes;
                    if (nodes.Count > 0)
                    {
                        foreach (XmlNode node in nodes)
                        {
                            var param = DataHelper.FromElementXml<WebObjectHeader>(node.OuterXml);
                            if (param != null)
                            {
                                param.Id = -1;
                                param.ObjectId = item.OBJECT_ID;
                                param.RecordId = item.Id;
                                param.Update();
                            }
                        }
                    }
                }
            }
        }
    }
}
