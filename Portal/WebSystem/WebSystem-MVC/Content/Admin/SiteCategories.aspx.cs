using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using des.Utils;

namespace CMS
{
    public partial class SiteCategories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here

            if (!Page.IsPostBack)
            {
                cmdDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete?');");

                this.PopulateTreeView();

                string sParentID = Request.QueryString["ParentID"];
                if (!string.IsNullOrEmpty(sParentID))
                {
                    this.FindTreeNode(TreeView1.Nodes, sParentID);
                }
            }
        }

        private void PopulateTreeView()
        {
            //string sRootName = des.CMS.Frameworks.SystemSettings.GetSettings("System.WebName"); //"Portal";

            //TreeView1.Nodes.Clear();
            //DropDownList1.Items.Clear();

            //// TREE VIEW
            //TreeNode tnRoot = new TreeNode(sRootName, "0");
            //tnRoot.Selected = true;
            //TreeView1.Nodes.Add(tnRoot);

            //// COMBO BOX
            //ListItem itemRoot = new ListItem(sRootName, "0");
            //DropDownList1.Items.Add(itemRoot);
            //{
            //    DataSet ds = SqlHelper.ExecuteDataSet("CMS.SELECT_SiteCategories");

            //    // START RECURSIVE
            //    LoadRecursiveTree(0, ds.Tables[0], tnRoot, "");
            //}

            //TreeView1.CollapseAll();
            //TreeView1.Nodes[0].Expanded = true;
        }

        private void LoadRecursiveTree(int iParentID, DataTable table, TreeNode tnRoot, string sTab)
        {
            sTab += "\u00a0\u00a0";

            DataRow[] rows = table.Select("ParentID=" + iParentID);
            foreach (DataRow row in rows)
            {
                string sSiteName = row["SiteCategoryName"].ToString();
                string sSiteID = row["SiteCategoryID"].ToString();

                // TREE VIEW
                TreeNode tn1 = new TreeNode(sSiteName, sSiteID);
                tn1.SelectAction = TreeNodeSelectAction.Select;
                tnRoot.ChildNodes.Add(tn1);

                // COMBO BOX
                ListItem item1 = new ListItem(sTab + "\u2022\u00a0" + sSiteName, sSiteID);
                DropDownList1.Items.Add(item1);

                LoadRecursiveTree((int)row["SiteCategoryID"], table, tn1, sTab);
            }
        }

        protected void cmdAdd_Click(object sender, System.EventArgs e)
        {
            QSParser qs = new QSParser(this.Request.QueryString);
            qs["ParentID"] = TreeView1.SelectedNode.Value;

            Response.Redirect("SiteCategory.aspx?" + qs, true);
        }

        protected void cmdDelete_Click(object sender, System.EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                SqlHelper.ExecuteNonQuery("CMS.DELETE_SiteCategories",
                    new SqlParameter("@SiteCategoryID", sChecked)
                );

                QSParser qs = new QSParser(this.Request.QueryString);
                Response.Redirect("SiteCategories.aspx?" + qs, true);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sCmdArgs = e.CommandArgument.ToString();
            QSParser qs = new QSParser(this.Request.QueryString);

            qs["SiteCategoryID"] = sCmdArgs;
            qs["ParentID"] = TreeView1.SelectedNode.Value;

            switch (e.CommandName)
            {
                case "edit_item":
                    Response.Redirect("SiteCategory.aspx?" + qs, true);
                    break;
            }
        }
        protected void cmdMove_Click(object sender, EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                int iParentID = int.Parse(DropDownList1.SelectedValue);

                SqlHelper.ExecuteNonQuery(CommandType.Text, "UPDATE CMS.SiteCategories SET ParentID=@ParentID WHERE SiteCategoryID<>@ParentID AND SiteCategoryID IN (" + sChecked + ")",
                    new SqlParameter("@ParentID", iParentID)
                );

                QSParser qs = new QSParser(this.Request.QueryString);
                Response.Redirect("SiteCategories.aspx?" + qs, true);
            }
        }

        protected void cmdGO_Click(object sender, EventArgs e)
        {
            this.FindTreeNode(TreeView1.Nodes, DropDownList1.SelectedValue);
        }

        private bool FindTreeNode(TreeNodeCollection nodes, string sValue)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Value == sValue)
                {
                    node.Expand();
                    node.Select();
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

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeView1.SelectedNode.Expand();
        }
}
}
