using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central.Template
{
    public partial class WebTemplatePanels : System.Web.UI.UserControl
    {
        IEnumerable<WebTemplatePanel> panels = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) { }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            var query = new QueryParser(this);
            query.Redirect(CentralPages.WebTemplatePanel);
        }

        protected void cmdDelete_Click(object sender, System.EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                var ids = DataHelper.ParseCommaSeparatedIdList(sChecked);
                if (ids.Count > 0)
                {
                    foreach (var id in ids)
                        WebTemplatePanel.Delete(id);

                    GridView1.DataBind();
                }
            }
        }

        protected void cmdDone_Click(object sender, EventArgs e)
        {
            WebHelper.Redirect(CentralPages.WebTemplates, Context);
        }

        protected void cmdParse_Click(object sender, EventArgs e)
        {
            string templatePath = string.Empty;

            // GET TEMPLATE FILE
            int templateId = DataHelper.GetId(Request, WebColumns.TemplateId);
            var template = WebTemplate.Get(templateId);
            if (template != null)
                templatePath = string.Format("~/Content/Themes/{0}/{1}", template.Identity, template.FileName);

            if (!File.Exists(MapPath(templatePath)))
            {
                // FILE NOT FOUND
                lblStatus.Text = "[ Template file not found. ]";
                return;
            }

            // GET THE NEXT PLACEHOLDERID #

            Action<Control, int> LoadRecursive = null;
            LoadRecursive = (ctrlParent, id) =>
            {
                foreach (Control control in ctrlParent.Controls)
                {
                    var type = control.GetType();
                    if ((type == typeof(PlaceHolder) || type == typeof(HtmlGenericControl) || type.Equals(typeof(HtmlTableCell)))
                        && (panels == null || panels.FirstOrDefault(p => p.PanelName.Equals(control.ID)) == null))
                    {
                        string placeHolderID = control.ID;
                        string placeHolderName = control.ID;

                        if (placeHolderName.StartsWith("ph"))
                            placeHolderName = placeHolderName.Substring(2);
                        else if (placeHolderName.StartsWith("plh"))
                            placeHolderName = placeHolderName.Substring(3);
                        else if (placeHolderName.StartsWith("panel"))
                            placeHolderName = placeHolderName.Substring(5);

                        var item = new WebTemplatePanel();
                        item.Name = placeHolderName;
                        item.PanelName = placeHolderID;
                        item.ObjectId = WebObjects.WebTemplate;
                        item.RecordId = id;
                        item.Update();
                    }
                    else
                    {
                        if (control.Controls.Count > 0)
                            LoadRecursive(control, id);
                    }
                }
            };


            var parent = LoadControl(templatePath);
            if (parent != null)
            {
                panels = WebTemplatePanel.Provider.GetList(WebObjects.WebTemplate, templateId);

                LoadRecursive(parent, templateId);
            }

            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Custom_Edit":
                    string id = e.CommandArgument.ToString();

                    var query = new QueryParser(this);
                    query.Set(WebColumns.TemplatePanelId, id);
                    query.Redirect(CentralPages.WebTemplatePanel);

                    break;
            }
        }

        public DataSet GetDataSet(int templateId)
        {
            var template = WebTemplate.Get(templateId);

            return DataHelper.ToDataSet(
                from i in WebTemplatePanel.Provider.GetList(WebObjects.WebTemplate, templateId)
                select new
                {
                    i.Id,
                    i.Name,
                    i.PanelName,
                    i.Rank,
                    IsDefault = template.PrimaryPanelId == i.Id ? 1 : 0
                });
        }
    }
}