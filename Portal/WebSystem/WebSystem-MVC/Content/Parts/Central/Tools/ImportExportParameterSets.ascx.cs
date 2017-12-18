using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.Text;

using WCMS.WebSystem.Controls;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using System.IO;

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class ExportImportParameterSets : System.Web.UI.UserControl
    {
        private const string TAB_EXPORT = "tabExport";
        private const string TAB_IMPORT = "tabImport";

        protected TabControl TabControl1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TabControl1.AddTab(TAB_EXPORT, "Export");
                TabControl1.AddTab(TAB_IMPORT, "Import");
            }
        }

        public DataSet GetList()
        {
            return DataHelper.ToDataSet(
                from item in WebParameterSet.Provider.GetList()
                select new
                {
                    item.Id,
                    item.Name
                }
            );
        }

        protected void TabControl1_SelectedTabChanged(object oSender, TabEventArgs args)
        {
            switch (args.TabName)
            {
                case TAB_EXPORT:
                    MultiView1.SetActiveView(viewExport);
                    break;

                case TAB_IMPORT:
                    MultiView1.SetActiveView(viewImport);
                    break;
            }
        }

        protected void cmdExport_Click(object sender, EventArgs e)
        {
            var id = DataHelper.GetId(cboParamSets.SelectedValue);
            if (id > 0)
            {
                var item = WebParameterSet.Provider.Get(id);
                if (item != null)
                {
                    StringBuilder output = new StringBuilder();
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.NewLineOnAttributes = false;
                    settings.Indent = true;
                    settings.OmitXmlDeclaration = true;
                    settings.Encoding = Encoding.Unicode;

                    XmlWriter writer = XmlWriter.Create(output, settings);

                    writer.WriteStartElement("ParameterSets");
                    writer.WriteStartElement("ParameterSet");

                    writer.WriteRaw(DataHelper.ToXml<WebParameterSet>(item, "Item"));

                    ParameterizedWebObject.WriteParameters(item, writer);

                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.Flush();

                    var webPath = WebHelper.CombineAddress(WConfig.TempFolder, string.Format("ParameterSet-XML-{0:yyyyMMdd}.xml", DateTime.Now));
                    var path = WebHelper.MapPath(webPath);

                    FileHelper.WriteFile(output.ToString(), path);

                    WebHelper.DownloadFile(path, needMapping: false);
                }
            }
        }

        protected void cmdImport_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
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

                var paramSetNodes = doc.SelectNodes("//ParameterSets/ParameterSet");
                if (paramSetNodes.Count > 0)
                {
                    foreach (XmlNode paramSetNode in paramSetNodes)
                    {
                        var item = paramSetNode.SelectSingleNode("Item");
                        if (item != null)
                        {
                            var parmSet = DataHelper.FromElementXml<WebParameterSet>(item.OuterXml, "Item");
                            if (parmSet != null)
                            {
                                var currParmSet = WebParameterSet.Get(parmSet.Name);
                                if (currParmSet == null)
                                {
                                    parmSet.Id = -1;
                                    parmSet.Update();
                                }
                                else
                                {
                                    parmSet = currParmSet;
                                }

                                ParameterizedWebObject.RestoreParameters(parmSet, paramSetNode);
                            }
                        }
                    }
                }
                // Import here

                lblStatus.Text = "Import completed successfully!";

                cboParamSets.DataBind();
            }
        }
    }
}