using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using System.Text;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebParts : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblStatus.Visible = false;

            if (!Page.IsPostBack)
            {
                if (!HasMgmtPermission())
                    rowControlBox.Visible = false;
            }
        }

        private bool HasMgmtPermission()
        {
            return WebGlobalPolicy.IsUserPermitted(GlobalPolicies.WebPartManagement, Permissions.ManageInstance);
        }

        protected void cmdAdd_Click(object sender, System.EventArgs e)
        {
            WebHelper.Redirect(CentralPages.WebPart, Context);
        }

        public DataSet Select()
        {
            if (WSession.Current.IsAdministrator)
                return DataHelper.ToDataSet(WPart.GetList());
            else
                return DataHelper.ToDataSet(WPart.GetPermissibleList(WSession.Current.UserId));
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var query = new QueryParser(this);
            int partId = DataHelper.GetId(e.CommandArgument);

            query.Set(WebColumns.PartId, partId);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Redirect(CentralPages.WebPartHome);
                    break;

                case "Toggle_Active":
                    //adapter.ToggleActiveState(Convert.ToInt32(sCommonSectionID));
                    GridView1.DataBind();
                    break;

                case "Custom_Delete":
                    if (HasMgmtPermission())
                    {
                        WPart.Delete(partId);
                        GridView1.DataBind();
                    }
                    break;
            }
        }

        protected void cmdParse_Click(object sender, EventArgs e)
        {
            bool hasSync = false;

            //var parts = WebPart.GetList();
            var partFolders = Directory.EnumerateDirectories(MapPath("~/Content/Parts"));
            foreach (var partFolder in partFolders)
            {
                var xmlPath = FileHelper.Combine(partFolder, "part.xml");
                if (File.Exists(xmlPath))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(xmlPath);

                    var partNode = xdoc.SelectSingleNode("/WebPart");
                    if (partNode != null)
                    {
                        var partIdentity = XmlUtil.GetValue(partNode, "Identity");
                        if (!string.IsNullOrEmpty(partIdentity))
                        {
                            var part = WPart.Get(partIdentity);
                            if (part != null)
                            {
                                var controlNodes = partNode.SelectNodes("./Controls/Control");
                                if (controlNodes.Count > 0)
                                {
                                    var controls = part.Controls; // Existing Part Controls
                                    foreach (XmlNode controlNode in controlNodes)
                                    {
                                        WebPartControl control = null;
                                        var name = XmlUtil.GetValue(controlNode, "Name");
                                        var identity = XmlUtil.GetValue(controlNode, "Identity");

                                        if ((control = controls.FirstOrDefault(i => i.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)
                                            && i.Identity.Equals(identity, StringComparison.InvariantCultureIgnoreCase))) == null)
                                        {
                                            var isEntryPoint = DataHelper.GetBool(XmlUtil.GetValue(controlNode, "IsEntryPoint"));

                                            control = new WebPartControl(part);
                                            control.Name = name;
                                            control.Identity = identity;
                                            control.IsEntryPoint = isEntryPoint;
                                            control.Update();

                                            if (!hasSync)
                                                hasSync = true;
                                        }

                                        var templateNodes = controlNode.SelectNodes("./Templates/Template");
                                        if (templateNodes.Count > 0)
                                        {
                                            foreach (XmlNode templateNode in templateNodes)
                                            {
                                                name = XmlUtil.GetValue(templateNode, "Name");
                                                identity = XmlUtil.GetValue(templateNode, "Identity");
                                                var path = XmlUtil.GetValue(templateNode, "Path");

                                                var template = new WebPartControlTemplate(control);
                                                template.Name = name;
                                                template.Identity = identity;
                                                template.Path = string.Format("~/Content/Parts/{0}/{1}", part.Identity, path);
                                                template.FileName = Path.GetFileName(path);
                                                template.Update();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    // No Xml config yet.
                    var identity = Path.GetFileName(partFolder);

                    var part = WPart.Get(identity);
                    if (part != null)
                    {
                        StringBuilder output = new StringBuilder();
                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.NewLineOnAttributes = false;
                        settings.Indent = true;
                        settings.OmitXmlDeclaration = true;
                        settings.Encoding = Encoding.Unicode;

                        XmlWriter writer = XmlWriter.Create(output, settings);

                        writer.WriteStartElement("WebPart");
                        writer.WriteAttributeString("Name", part.Name);
                        writer.WriteAttributeString("Identity", part.Identity);
                        // writer.WriteRaw(DataHelper.ToXml<WebParameterSet>(item, "Item"));

                        writer.WriteEndElement();
                        writer.Flush();

                        FileHelper.WriteFile(output.ToString(), xmlPath);
                    }
                }
            }

            if (hasSync)
                DisplayMessage("Sync successful!");
            else
                DisplayMessage("Nothing to sync.");
        }

        private void DisplayMessage(string message)
        {
            lblStatus.InnerHtml = message;
            lblStatus.Visible = true;
        }
    }
}