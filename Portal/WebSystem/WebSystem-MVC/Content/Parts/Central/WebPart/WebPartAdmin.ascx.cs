using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebPartAdminController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int partId = DataHelper.GetId(Request, WebColumns.PartId);
                WPart part = null;
                if (partId > 0 && (part = WPart.Get(partId)) != null)
                    lblName.InnerHtml = part.Name;

                this.PopulateTreeView();

                // LOCATE THE SELECTED NODE
                int parentId = DataHelper.GetId(Request, WebColumns.ParentId);
                if (parentId > 0)
                    this.FindTreeNode(tvSections.Nodes, parentId);

                this.SelectSection(parentId);
            }
        }

        private void SelectSection(int partAdminId)
        {
            WebPartAdmin item = null;
            if (partAdminId > 0 && (item = WebPartAdmin.Get(partAdminId)) != null)
            {
                lblName.InnerHtml = item.Name;
                tdSection.Visible = true;
            }
            else
            {
                tdSection.Visible = false;
            }
        }

        private void PopulateTreeView()
        {
            var query = new QueryParser(this);
            int partId = query.GetId(WebColumns.PartId);
            var part = WPart.Get(partId);

            tvSections.Nodes.Clear();
            cboSections.Items.Clear();

            query.Set(WebColumns.ParentId, -1);

            // TREE VIEW
            var tnRoot = new TreeNode(part.Name, "-1");
            tnRoot.NavigateUrl = query.BuildQuery();
            tnRoot.ImageUrl = "~/Content/Assets/Images/TreeView/tl.gif";
            tnRoot.Selected = true;
            tvSections.Nodes.Add(tnRoot);

            // COMBO BOX
            var itemRoot = new ListItem(part.Name, "-1");
            cboSections.Items.Add(itemRoot);
            {
                var items = WebPartAdmin.GetList(part.Id);

                // START RECURSIVE
                LoadRecursiveWM(-1, items, tnRoot, "");
            }

            tvSections.CollapseAll();
            tvSections.Nodes[0].Expanded = true;
        }

        private bool LoadRecursiveWM(int parentId, IEnumerable<WebPartAdmin> items, TreeNode tn2, string tab)
        {
            var query = new QueryParser(this);
            tab += WConstants.TAB;

            var subItems = items.Where(item => item.ParentId == parentId);
            foreach (WebPartAdmin item in subItems)
            {
                query.Set(WebColumns.ParentId, item.Id);
                var tn3 = new TreeNode(item.Name, item.Id.ToString());
                tn3.ImageUrl = "~/Content/Assets/Images/TreeView/cl.gif";
                tn3.SelectAction = TreeNodeSelectAction.Select;
                tn3.NavigateUrl = query.BuildQuery();
                tn2.ChildNodes.Add(tn3);

                var item1 = new ListItem(tab + "\u2022\u00a0" + item.Name, item.Id.ToString());
                cboSections.Items.Add(item1);

                this.LoadRecursiveWM(item.Id, items, tn3, tab);
            }

            return true;
        }

        protected void cmdAddFull_Click(object sender, System.EventArgs e)
        {
            var query = new QueryParser(this);
            query.Set(WebColumns.ParentId, tvSections.SelectedNode.Value);
            query.Redirect(CentralPages.WebPartAdminEntry);
        }

        protected void cmdDelete_Click(object sender, System.EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                foreach (string s in sChecked.Split(','))
                {
                    int partAdminId = DataHelper.GetId(s);
                    if (partAdminId > 0)
                        WebPartAdmin.Delete(partAdminId);
                }

                var query = new QueryParser(this);
                query.Redirect(); //"WebParts.aspx");
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int partAdminId = DataHelper.GetId(e.CommandArgument);
            var query = new QueryParser(this);
            query.Set(WebColumns.PartAdminId, partAdminId);

            switch (e.CommandName)
            {
                case "edit_item":
                    query.Redirect(CentralPages.WebPartAdminHome);
                    break;

                case "Custom_Delete":
                    WebPartAdmin.Delete(partAdminId);
                    GridView1.DataBind();
                    this.PopulateTreeView();
                    break;

                //case "controls":
                //    qs.Redirect("ContentCMSControls.aspx");
                //    break;
            }
        }
        protected void cmdMove_Click(object sender, EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                int parentId = DataHelper.GetId(cboSections.SelectedValue);
                if (parentId > 0)
                {
                    var items = DataHelper.ParseCommaSeparatedIdList(sChecked);
                    foreach (var id in items)
                    {
                        var item = WebPartAdmin.Get(id);
                        if (item != null)
                        {
                            item.ParentId = parentId;
                            item.Update();
                        }
                    }
                    var query = new QueryParser(this);
                    query.Redirect();
                }
            }
        }

        protected void cmdGO_Click(object sender, EventArgs e)
        {
            this.FindTreeNode(tvSections.Nodes, DataHelper.GetId(cboSections.SelectedValue));
        }

        private bool FindTreeNode(TreeNodeCollection nodes, int sValue)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Value == sValue.ToString())
                {
                    node.Select();
                    node.Expand();
                    return true;
                }
                else
                {
                    if (FindTreeNode(node.ChildNodes, sValue))
                    {
                        node.Expand();
                        return true;
                    }
                }
            }

            return false;
        }

        protected void tvSections_SelectedNodeChanged(object sender, EventArgs e)
        {
            this.SelectSection(DataHelper.GetId(tvSections.SelectedValue));
            tvSections.SelectedNode.Expand();
        }

        protected void cmdEdit_Click(object sender, EventArgs e)
        {
            var query = new QueryParser(this);
            string partAdminId = tvSections.SelectedValue;

            query.Set(WebColumns.ParentId, partAdminId);
            query.Set(WebColumns.PartAdminId, partAdminId);

            query.Redirect(CentralPages.WebPartAdminHome);
        }

        protected void cmdCMS_Click(object sender, EventArgs e)
        {
            //QueryParser qs = new QueryParser(this);
            //string sCSCCID = tvSections.SelectedValue;

            //qs[WebColumns.ParentId] = sCSCCID;
            //qs[WebColumns.PartAdminId] = sCSCCID;

            //qs.Redirect("WebPartAdmin.aspx");
        }

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            this.PopulateTreeView();
        }

        protected void cmdBack_Click(object sender, EventArgs e)
        {
            var query = new QueryParser(this);
            query.Redirect(CentralPages.WebPartHome);
        }

        public DataSet Select(int partId, int parentId)
        {
            if (partId > 0)
                return DataHelper.ToDataSet(WebPartAdmin.GetList(partId, parentId));
            else
                return null;
        }
    }
}