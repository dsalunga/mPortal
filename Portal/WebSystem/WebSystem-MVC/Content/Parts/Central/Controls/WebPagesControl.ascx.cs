using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class WebPagesControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            int siteId = DataHelper.GetId(Request, WebColumns.SiteId);
            if (!IsPostBack)
            {
                this.PopulateTreeView(siteId);
                if (!IsSiteAuthor())
                {
                    cmdDelete.Enabled = false;
                    cmdAddFull.Enabled = false;
                    cmdMove.Enabled = false;
                }
            }
        }

        private bool IsSiteAuthor()
        {
            int siteId = DataHelper.GetId(Request, WebColumns.SiteId);
            return WSite.IsUserSiteAuthor(siteId);
        }

        private void PopulateTreeView(int siteId)
        {
            var site = WSite.Get(siteId);
            cboPages.Items.Clear();

            // TREE VIEW
            var tnRoot = new TreeNode(site.Name, "-1");
            tnRoot.Selected = true;

            // COMBO BOX
            var itemRoot = new ListItem(site.Name, "-1");
            cboPages.Items.Add(itemRoot);
            {
                var ds = WPage.GetList(site.Id);
                // START RECURSIVE
                LoadRecursiveTree(-1, ds, tnRoot, "");
            }

            // Include other websites
            var sites = WSite.GetList(-1).Take(50);
            foreach (var s in sites)
            {
                if (s.Id != site.Id)
                    cboPages.Items.Add(new ListItem(s.Name, "-" + s.Id));
            }
        }

        private void LoadRecursiveTree(int parentId, IEnumerable<WPage> table, TreeNode tnRoot, string tab)
        {
            tab += WConstants.TAB;
            var pages = from p in table
                        where p.ParentId == parentId
                        select p;

            foreach (var page in pages)
            {
                // TREE VIEW
                var node = new TreeNode(page.Name, page.Id.ToString());
                node.SelectAction = TreeNodeSelectAction.Select;
                tnRoot.ChildNodes.Add(node);

                // COMBO BOX
                var item = new ListItem(tab + "\u2022\u00a0" + page.Name, page.Id.ToString());
                cboPages.Items.Add(item);
                LoadRecursiveTree(page.Id, table, node, tab);
            }
        }

        protected void cmdAddFull_Click(object sender, System.EventArgs e)
        {
            if (IsSiteAuthor())
            {
                var query = new WQuery(this);
                query.Redirect(CentralPages.WebPage);
            }
        }

        protected void cmdDelete_Click(object sender, System.EventArgs e)
        {
            if (IsSiteAuthor())
            {
                string sChecked = Request.Form["chkChecked"];
                var ids = DataHelper.ParseCommaSeparatedIdList(sChecked);
                foreach (var id in ids)
                {
                    var page = WPage.Get(id);
                    if (page != null)
                        page.Delete(true);
                }

                var query = new QueryParser(this);
                query.Redirect(CentralPages.WebPages);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int pageId = DataHelper.GetId(e.CommandArgument);
            var query = new QueryParser(this);
            query.Set(WebColumns.PageId, pageId);

            switch (e.CommandName)
            {
                case "edit_item":
                    query.Redirect(CentralPages.WebPageHome);
                    break;

                case "cms":
                    query.Redirect(CentralPages.LoaderMain);
                    break;

                case "toggle_active":
                    if (IsSiteAuthor())
                    {
                        WPage page = WPage.Get(pageId);
                        page.Active = page.Active == 1 ? 0 : 1;
                        page.Update();

                        GridView1.DataBind();
                    }
                    break;

                case "Custom_Delete":
                    if (IsSiteAuthor())
                    {
                        var page = WPage.Get(pageId);
                        if (page != null)
                        {
                            page.Delete(true);
                            query.Redirect(CentralPages.WebPages);
                        }
                    }
                    break;
            }
        }
        protected void cmdMove_Click(object sender, EventArgs e)
        {
            int parentId = int.Parse(cboPages.SelectedValue);
            if(parentId != -1)
            {
                if (IsSiteAuthor())
                {
                    string sChecked = Request.Form["chkChecked"];
                    if (!string.IsNullOrEmpty(sChecked))
                    {
                        var ids = DataHelper.ParseCommaSeparatedIdList(sChecked);
                        foreach (int id in ids)
                        {
                            var page = WPage.Get(id);
                            if (parentId < -1)
                            {
                                page.SiteId = parentId * -1;
                                page.ParentId = -1;
                            }
                            else
                            {
                                page.ParentId = parentId;
                            }
                            page.Update();
                        }

                        var query = new WQuery(this);
                        query.Redirect();
                    }
                }
            }
        }

        protected void cmdGO_Click(object sender, EventArgs e)
        {
            var parentId = DataHelper.GetInt32(cboPages.SelectedValue);
            if (parentId >= -1)
            {
                var query = new WQuery(this);
                query.Set(WebColumns.PageId, parentId);
                query.Redirect(CentralPages.WebPages);
            }
        }

        private bool FindTreeNode(TreeNodeCollection nodes, int siteId)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Value == siteId.ToString())
                {
                    node.Select();
                    node.Expand();
                    return true;
                }
                else
                {
                    if (FindTreeNode(node.ChildNodes, siteId))
                    {
                        node.Expand();
                        return true;
                    }
                }
            }

            return false;
        }

        public DataSet GetWebPages(int siteId, int parentId)
        {
            var pages = WPage.FilterPermitted(siteId, parentId);
            WebPartControlTemplate template = null;

            // NOTE: produces an error when PartControlTemplate is null
            var items = from i in pages
                        select new
                        {
                            i.Id,
                            i.Name,
                            i.Active,
                            i.Rank,
                            i.SiteId,
                            Title = i.BuildTitle(),
                            WebPartName = (template = i.PartControlTemplateId > 0 ? i.PartControlTemplate : null) != null ? string.Format("{0} <span style=\"color:#507CD1;\">{1}</span> {2}", template.Part.Name, WConstants.Arrow, template.Name) : "&lt;null&gt;",
                            RelativeUrl = i.BuildRelativeUrl()
                        };

            return DataHelper.ToDataSet(items);
        }
    }
}