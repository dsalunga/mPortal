using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central.WebSites
{
    public partial class WebPageElementsController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var context = new WContext(this);
            int templatePanelId = context.GetId(WebColumns.TemplatePanelId);
            int siteId = context.GetId(WebColumns.SiteId);
            int masterPageId = context.GetId(WebColumns.MasterPageId);

            if (!Page.IsPostBack)
            {
                var panel = WebTemplatePanel.Get(templatePanelId);
                if (panel != null)
                {
                    cboPanel.DataBind();
                    cboPanel.SelectedValue = panel.Id.ToString();
                }
            }
        }


        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            query.Set(WebColumns.TemplatePanelId, cboPanel.SelectedValue);
            query.Redirect(CentralPages.WebPageElement);
        }


        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                var ids = DataHelper.ParseDelimitedStringToList(sChecked, ',');
                foreach (var id in ids)
                {
                    var key = new ObjectKey(id, '/');
                    if (key.ObjectId == WebObjects.WebPageElement)
                    {
                        var element = WebPageElement.Get(key.RecordId);
                        if (element != null)
                            element.Delete();
                    }
                }

                GridView1.DataBind();
            }
        }

        protected void cmdMoveTo_Click(object sender, EventArgs e)
        {
            int templatePanelId = DataHelper.GetId(Request, WebColumns.TemplatePanelId);
            string sChecked = Request.Form["chkChecked"];

            int selectedTemplatePanelId = DataHelper.GetId(cboPlaceholders.SelectedValue);
            if (selectedTemplatePanelId < 1)
                return;
            else
                if (templatePanelId == selectedTemplatePanelId)
                    return;

            if (!string.IsNullOrEmpty(sChecked))
            {
                var ids = DataHelper.ParseDelimitedStringToList(sChecked);
                foreach (var id in ids)
                {
                    var key = new ObjectKey(id, '/');
                    if (key.ObjectId == WebObjects.WebPageElement)
                    {
                        var item = WebPageElement.Get(key.RecordId);
                        if (item != null)
                        {
                            item.TemplatePanelId = selectedTemplatePanelId;
                            item.Update();
                        }
                    }
                }

                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var query = new WQuery(this);
            var key = new ObjectKey(e.CommandArgument.ToString(), '/');

            if (key.ObjectId == WebObjects.WebPage)
                query.Set(WebColumns.PageId, key.RecordId);
            else
                query.Set(WebColumns.PageElementId, key.RecordId);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    if (key.ObjectId == WebObjects.WebPage)
                        query.Redirect(CentralPages.WebPageHome);
                    else
                        query.Redirect(CentralPages.WebPageElementHome);

                    break;

                case "Load_CMS":
                    query.Redirect(CentralPages.LoaderMain);
                    break;
            }
        }

        public DataSet SelectElements(int pageId, int masterPageId, int templatePanelId, bool onlyActive)
        {
            var query = new QueryParser(true);
            var items = new List<WebPageElement>();
            WPage page = null;
            bool addPage = false;
            int templateId = -1;
            WebTemplatePanel panel = null;

            if (pageId > 0)
            {
                page = WPage.Get(pageId);
                if (page.GetEvalTypeId() == PageTypes.Static)
                {
                    // When panel is inside the part template
                    if (templatePanelId > 0)
                    {
                        items.AddRange(WebPageElement.GetList(pageId, WebObjects.WebPage, templatePanelId));
                        panel = WebTemplatePanel.Get(templatePanelId);
                    }
                    else
                    {
                        items.AddRange(WebPageElement.GetList(pageId, WebObjects.WebPage));
                    }
                }
                else
                {
                    var master = page.MasterPage;
                    templateId = master.TemplateId;

                    if (templatePanelId > 0)
                    {
                        items.AddRange(WebPageElement.GetList(pageId, WebObjects.WebPage, templatePanelId));
                        items.AddRange(WebPageElement.GetList(master.Id, WebObjects.WebMasterPage, templatePanelId));

                        panel = WebTemplatePanel.Get(templatePanelId);
                        if (panel != null && panel.Template.PrimaryPanelId == panel.Id)
                            addPage = true;
                    }
                    else
                    {
                        items.AddRange(WebPageElement.GetList(pageId, WebObjects.WebPage));
                        items.AddRange(WebPageElement.GetList(master.Id, WebObjects.WebMasterPage));

                        addPage = true;
                    }
                }
            }
            else if (masterPageId > 0)
            {
                var master = WebMasterPage.Get(masterPageId);

                templateId = master.TemplateId;

                if (templatePanelId > 0)
                    items.AddRange(WebPageElement.GetList(masterPageId, WebObjects.WebMasterPage, templatePanelId));
                else
                    items.AddRange(WebPageElement.GetList(masterPageId, WebObjects.WebMasterPage));
            }
            else
            {
                throw new NotSupportedException("Both PageId and MasterPageId are not present.");
            }

            Func<WebPageElement, WebTemplatePanel, string> GetOwnerString = (item, p) =>
            {
                if (item.ObjectId == WebObjects.WebMasterPage)
                    return (p.ObjectId == WebObjects.WebTemplate && p.RecordId == templateId) ? "Master Page" : "<em>Missing/Parent</em>";
                else
                    return "Web Page";
            };

            WebPartControlTemplate template = null;

            var output = from item in items
                         where !onlyActive || item.IsActive
                         orderby item.Rank
                         select new
                         {
                             item.Id,
                             item.Name,
                             PanelName = (panel = item.Panel) != null ? panel.Name : string.Empty,
                             ObjectName = GetOwnerString(item, panel),
                             PartName = (template = item.PartControlTemplate) != null ? string.Format("{0} <span style=\"color:#507CD1;\">{1}</span> {2}", template.Part.Name, WConstants.Arrow, template.Name) : "&lt;null&gt;",
                             item.Rank,
                             item.Active,
                             Key = item.GetKeyString(),
                             NameUrl = query.Set(WebColumns.PageElementId, item.Id).BuildQuery(CentralPages.WebPageElementHome)
                         };

            if (addPage && page != null)
            {
                List<WPage> i = new List<WPage>();
                i.Add(page);

                query.Remove(WebColumns.PageElementId);

                var q = query.Clone();

                output = output.Union(
                    from item in i
                    where !onlyActive || item.IsActive
                    select new
                    {
                        item.Id,
                        Name = string.Format("<strong>{0}</strong>", item.Name),
                        PanelName = item.Panel.Name,
                        ObjectName = "<em>Not applicable</em>",
                        PartName = (template = item.PartControlTemplate) != null ? string.Format("{0} <span style=\"color:#507CD1;\">{1}</span> {2}", template.Part.Name, WConstants.Arrow, template.Name) : "Undefined",
                        item.Rank,
                        item.Active,
                        Key = item.GetKeyString(),
                        NameUrl = q.BuildQuery(CentralPages.WebPageHome)
                    }
                );
            }

            return DataHelper.ToDataSet(output);
        }

        //public IQueryable<WebPageElement> Select(int pageId, int templatePanelId, int startRowIndex, int maximumRows, string sortExpression)
        //{
        //    var items = (from i in WebPageElement.GetByPanelId(pageId, WebObjects.WebPage, templatePanelId).AsQueryable<WebPageElement>()
        //                 select i).Skip(startRowIndex).Take(maximumRows);

        //    if (!string.IsNullOrEmpty(sortExpression))
        //    {
        //        // Extract the column name
        //        string sortColumn = sortExpression.EndsWith(" DESC", StringComparison.InvariantCultureIgnoreCase) ?
        //            sortExpression.Substring(0, sortExpression.Length - " DESC".Length) : sortExpression;

        //        // Generate an expression for the column
        //        var param = Expression.Parameter(typeof(WebPageElement), "WebPageElement");
        //        var sort = Expression.Lambda<Func<WebPageElement, object>>(Expression.Property(param, sortColumn), param);

        //        // Sort the list
        //        items = (sortColumn.Length != sortExpression.Length) ?
        //            items.OrderByDescending(sort) : items = items.OrderBy(sort);
        //    }

        //    return items;
        //}

        //public int SelectCount(int pageId, int templatePanelId)
        //{
        //    return WebPageElement.GetByPanelId(pageId, WebObjects.WebPage, templatePanelId).Count;
        //}

        public IEnumerable<WebTemplatePanel> GetPanels(int pageId, int masterPageId)
        {
            WebMasterPage masterPage = null;

            if (pageId > 0)
            {
                var page = WPage.Get(pageId);
                if (page.GetEvalTypeId() == PageTypes.Static)
                {
                    return WebTemplatePanel.Provider.GetList(WebObjects.WebPartControlTemplate, page.PartControlTemplateId);
                }
                else
                {
                    masterPage = page.MasterPage;
                }
            }
            else if (masterPageId > 0)
            {
                masterPage = WebMasterPage.Get(masterPageId);
            }

            if (masterPage != null)
                return WebTemplatePanel.Provider.GetList(WebObjects.WebTemplate, masterPage.TemplateId);
            else
                throw new Exception("Cannot find master page.");
        }

        protected void cboPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            query.Set(WebColumns.TemplatePanelId, cboPanel.SelectedValue);
            query.Redirect();
        }

        protected void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }
    }
}