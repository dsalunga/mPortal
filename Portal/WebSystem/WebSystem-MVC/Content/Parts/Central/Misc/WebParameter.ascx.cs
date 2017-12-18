using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Xml;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebParameterController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var query = new WQuery(this);
                var key = new ObjectKey(query.Get(ObjectKey.KeyString));

                int id = query.GetId(WebColumns.ParameterId);
                var parameters = WebParameter.GetList(key.ObjectId, key.RecordId);
                var schemaList = new List<string>();

                // WebSite schema
                if (key.ObjectId == WebObjects.WebSite)
                {
                    var schema = WebRegistry.SelectNodeValue("/System/Parameter-Schema/WebSite");
                    if (!string.IsNullOrEmpty(schema))
                        schemaList.Add(schema);
                }

                // WebPage schema
                if (key.ObjectId == WebObjects.WebPage)
                {
                    var schema = WebRegistry.SelectNodeValue("/System/Parameter-Schema/WebPage");
                    if (!string.IsNullOrEmpty(schema))
                        schemaList.Add(schema);
                }

                // WebPage schema
                if (key.ObjectId == WebObjects.WebPageElement)
                {
                    var schema = WebRegistry.SelectNodeValue("/System/Parameter-Schema/WebPageElement");
                    if (!string.IsNullOrEmpty(schema))
                        schemaList.Add(schema);
                }

                // WebPage schema
                if (key.ObjectId == WebObjects.WebPartControl)
                {
                    var schema = WebRegistry.SelectNodeValue("/System/Parameter-Schema/WebPartControl");
                    if (!string.IsNullOrEmpty(schema))
                        schemaList.Add(schema);
                }

                // WebPage schema
                if (key.ObjectId == WebObjects.WebTemplate)
                {
                    var schema = WebRegistry.SelectNodeValue("/System/Parameter-Schema/WebTemplate");
                    if (!string.IsNullOrEmpty(schema))
                        schemaList.Add(schema);
                }

                // ParameterSet Schema
                if (key.ObjectId == WebObjects.WebParameterSet)
                {
                    var set = WebParameterSet.Provider.Get(key.RecordId);
                    if (set != null)
                    {
                        var parm = set.GetSchemaParameter();
                        if (parm != null)
                        {
                            var schema = parm.Value;
                            if (!string.IsNullOrEmpty(schema))
                                schemaList.Add(schema);
                        }
                    }
                }

                // Preload Properties based on Schema
                PageElementBase element = key.ObjectId == WebObjects.WebPageElement ? (PageElementBase)WebPageElement.Get(key.RecordId) :
                    key.ObjectId == WebObjects.WebPage ? (PageElementBase)WPage.Get(key.RecordId) : null;
                if (element != null)
                {
                    // WebPartControl Schema
                    var partControlSchema = element.PartControlTemplate.PartControl.GetParameterValue("Parameter-Schema");
                    if (!string.IsNullOrEmpty(partControlSchema))
                        schemaList.Add(partControlSchema);
                }

                foreach (var schemaDef in schemaList)
                {
                    var xdoc = new XmlDocument();
                    xdoc.LoadXml(schemaDef);

                    var nodes = xdoc.SelectNodes("//Parameter");
                    foreach (XmlNode node in nodes)
                    {
                        var name = XmlUtil.GetAttributeValue(node, "Name");
                        var value = XmlUtil.GetValue(node, "Value");

                        if ( name.Equals(WConstants.FormatStringKey))
                        {
                            WSite site = null;
                            switch (key.ObjectId)
                            {
                                case WebObjects.WebPage:
                                    var p = WPage.Get(key.RecordId);
                                    if (p != null)
                                        site = p.Site;
                                    break;
                                case WebObjects.WebPageElement:
                                    var el = WebPageElement.Get(key.RecordId);
                                    if (el != null)
                                        site = el.Site;
                                    break;
                            }

                            if (site != null)
                            {
                                var v = site.GetParameterValue(WConstants.FormatStringKey);
                                if (!string.IsNullOrEmpty(v))
                                    value = v;
                            }
                        }

                        var parameter = parameters.FirstOrDefault(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                        if (parameter == null || parameter.Id == id)
                            cboName.Items.Add(new ListItem(name, value));
                    }
                }

                cboName.AutoPostBack = cboName.Items.Count > 0;

                WebParameter param = null;
                if (id > 0 && (param = WebParameter.Get(id)) != null)
                {
                    WebHelper.AspNetAjaxComboBoxSelectText(cboName, param.Name);
                    //cboName.SelectedIndex = 1;
                    txtValue.Text = param.Value;
                    chkRequired.Checked = param.IsRequired == 1;
                    //txtName.ReadOnly = true;
                }
                else
                {
                    cboName.Items.Insert(0, string.Empty);
                }
            }
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (Update())
                this.Return();
        }

        private bool Update()
        {
            string name = cboName.SelectedItem.Text.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                var query = new WQuery(this);
                var key = new ObjectKey(query.Get(ObjectKey.KeyString));
                int id = query.GetId(WebColumns.ParameterId);
                WebParameter item = null;

                if (id > 0 && (item = WebParameter.Get(id)) != null)
                {
                    // Update
                }
                else
                {
                    // New
                    item = new WebParameter();
                    item.ObjectId = key.ObjectId;
                    item.RecordId = key.RecordId;
                }

                item.Name = name;
                item.Value = txtValue.Text;
                item.IsRequired = chkRequired.Checked ? 1 : 0;
                item.Update();
                return true;
            }
            else { }
            return false;
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Return();
        }

        private void Return()
        {
            var query = new WQuery(this);
            query.Remove(WebColumns.ParameterId);
            query.Redirect(CentralPages.WebParameters);
        }

        protected void cboName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = cboName.SelectedItem;
            if (item != null && string.IsNullOrWhiteSpace(txtValue.Text))
                txtValue.Text = item.Value;
        }

        protected void cmdUpdateAndAddNew_Click(object sender, EventArgs e)
        {
            if (Update())
            {
                var query = new WQuery(this);
                query.Remove(WebColumns.ParameterId);
                query.Redirect();
            }
        }
    }
}