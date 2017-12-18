using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Windows
{
    public partial class ObjectBrowser : System.Web.UI.Page
    {
        /// <summary>
        /// Saves an object, therefore selecting a folder
        /// </summary>
        private const string ActionSave = "Save";

        /// <summary>
        /// Opens an object
        /// </summary>
        private const string ActionOpen = "Open";

        private const string ActionKey = "Action";

        protected void Page_Load(object sender, EventArgs e)
        {
            int objectId = DataHelper.GetId(Request, WebColumns.ObjectId);

            if (!Page.IsPostBack)
            {
                BuildTree();
                BuildBreadcrumb();

                PopulateBox(objectId);

                if (CurrentAction == ActionSave)
                    cmdOpen.Enabled = true;
            }
        }

        public string CurrentAction
        {
            get
            {
                var action = Request[ActionKey];

                // Default is Open
                return string.IsNullOrEmpty(action) ? ActionOpen : action;
            }
        }

        private void PopulateBox(int objectId)
        {
            var objects = WebObject.GetList();

            foreach (WebObject item in objects)
                cboType.Items.Add(new ListItem(item.Name, item.Id.ToString()));

            cboType.SelectedValue = objectId.ToString();
        }

        private void BuildTree()
        {
            QueryParser query = new QueryParser(this);
            int folderId = query.GetId(WebColumns.FolderId);


            //TreeNode tnRoot = new TreeNode(WebRegistry.SelectNode(WebRegistry.WEB_NAME).Value);
            //tnRoot.SelectAction = TreeNodeSelectAction.Expand;

            //TreeNode tnPhysical = new TreeNode("Physical Namespace", "-1");
            //tnPhysical.SelectAction = TreeNodeSelectAction.Expand;
            //{
            //}

            TreeNode nodeLogical = new TreeNode("Logical Namespace", "-1");
            //tnLogical.SelectAction = TreeNodeSelectAction.Expand;
            {
                TreeNode node = new TreeNode("Uncategorized", "-2");
                nodeLogical.ChildNodes.Add(node);

                var items = WebFolder.Provider.GetList();
                Action<int, TreeNode> GenerateTree = null;
                GenerateTree = (parentId, parentNode) =>
                {
                    var childItems = items.Where(item => item.ParentId == parentId);
                    if (childItems.Count() > 0)
                    {
                        foreach (WebFolder child in childItems)
                        {
                            TreeNode childNode = new TreeNode(child.Name, child.Id.ToString());
                            parentNode.ChildNodes.Add(childNode);
                            if (folderId > 0 && child.Id == folderId)
                                childNode.Selected = true;

                            GenerateTree(child.Id, childNode);
                        }
                    }
                };

                GenerateTree(-1, nodeLogical);
            }

            //tnRoot.ChildNodes.Add(tnPhysical);
            //tnRoot.ChildNodes.Add(tnLogical);
            tvNavigation.Nodes.Add(nodeLogical);
        }

        //private void BindByDirectory(int directoryId)
        //{
        //    // Temporary code, revise in the future for flexibility (Implement builder pattern)
        //    int objectId = DataHelper.GetId(cboType.SelectedValue);

        //    switch (objectId)
        //    {
        //        case WebObjects.WebContent:
        //            {
        //                var item = WebObject.Get(WebObjects.WebContent);

        //                var objectItems = from i in WebContent.GetByDirectory(directoryId)
        //                                  select new
        //                                  {
        //                                      i.Id,
        //                                      RecordId = i.Id,
        //                                      ObjectId = item.Id,
        //                                      Name = i.Title,
        //                                      ObjectName = item.FriendlyNameEval
        //                                  };

        //                grdObjects.DataSource = DataHelper.ToDataSet(objectItems);
        //                grdObjects.DataBind();

        //                break;
        //            }

        //        case WebObjects.WebTextResource:
        //            {
        //                var item = WebObject.Get(WebObjects.WebTextResource);

        //                var objectItems = from i in WebTextResource.GetByDirectory(directoryId)
        //                                  select new
        //                                  {
        //                                      i.Id,
        //                                      RecordId = i.Id,
        //                                      ObjectId = item.Id,
        //                                      Name = i.Title,
        //                                      ObjectName = item.FriendlyNameEval
        //                                  };

        //                grdObjects.DataSource = DataHelper.ToDataSet(objectItems);
        //                grdObjects.DataBind();

        //                break;
        //            }
        //    }
        //}

        private void BuildBreadcrumb()
        {
            StringBuilder sb = new StringBuilder();
            QueryParser query = new QueryParser(this);
            int folderId = DataHelper.GetId(tvNavigation.SelectedValue); //ctx.GetId(WebColumns.ParentId);

            while (folderId > 0)
            {
                WebFolder item = WebFolder.Provider.Get(folderId);
                if (item != null)
                {
                    query.Set(WebColumns.FolderId, item.Id);
                    sb.Insert(0, string.Format(@"&nbsp;<span id=""cms_path_separator"">{2}</span>&nbsp;<a href='{0}' title='{1}'>{1}</a>", query.BuildQuery(), item.Name, WConstants.Arrow));

                    folderId = item.ParentId;
                }
                else
                {
                    folderId = -1;
                }
            }

            query.Remove(WebColumns.FolderId);
            sb.Insert(0, string.Format("<a href='{0}' title='{1}' style='font-weight: bold;'>{1}</a>", query.BuildQuery(), "Browser"));

            lblBreadcrumb.InnerHtml = sb.ToString();
        }

        protected void tvNavigation_SelectedNodeChanged(object sender, EventArgs e)
        {
            //int directoryId = DataHelper.GetId(tvNavigation.SelectedValue);

            //if (directoryId > 0)
            //{
            //    lblPath.InnerHtml = WebFolder.Provider.Get(directoryId).BuildRelativePath();
            //}
            //else
            //{
            //    lblPath.InnerHtml = "/";
            //}

            //this.BindByDirectory(directoryId);

            grdObjects.DataBind();

            BuildBreadcrumb();
        }

        protected void grdObjects_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Custom_Edit":
                    if (CurrentAction == ActionOpen)
                        hidId.Value = e.CommandArgument.ToString();
                    break;
            }
        }

        protected void cmdOpen_Click(object sender, EventArgs e)
        {
            if (CurrentAction == ActionSave)
            {
                int id = DataHelper.GetId(tvNavigation.SelectedValue);

                WebFolder item = WebFolder.Provider.Get(id);
                if (item != null)
                    hidId.Value = item.BuildRelativePath();
                else
                    hidId.Value = "/";
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);
            QueryParser query = new QueryParser(this);

            switch (e.CommandName)
            {
                //case "Custom_Edit":
                //    qs.Set(WebColumns.FolderId, id);
                //    qs.Redirect(CentralPages.WebFolder);
                //    break;

                case "View_ChildNodes":
                    query.Set(WebColumns.FolderId, id);
                    query.Redirect();
                    break;

                //case "Custom_Delete":
                //    WebFolder.Provider.Delete(id);
                //    GridView1.DataBind();
                //    break;
            }
        }

        protected void cmdUp_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            int folderId = query.GetId(WebColumns.FolderId);

            if (folderId > 0)
            {
                query.Set(WebColumns.FolderId, WebFolder.Provider.Get(folderId).ParentId);
                query.Redirect();
            }
        }

        public DataSet Select(int folderId, string keyword)
        {
            string loweredKeyword = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();
            bool noSearch = string.IsNullOrEmpty(loweredKeyword);

            var items = from i in WebFolder.Provider.GetList(folderId == -2 ? -1 : folderId)
                        where noSearch || DataHelper.HasMatch(i.Name, loweredKeyword)
                        select i;

            return DataHelper.ToDataSet(items);
        }

        public DataSet SelectFiles(int folderId, int objectId, string keyword)
        {
            // Temporary code, revise in the future for flexibility (Implement builder pattern)

            string loweredKeyword = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();
            bool noSearch = string.IsNullOrEmpty(loweredKeyword);

            switch (objectId)
            {
                case WebObjects.WebContent:
                    {
                        var item = WebObject.Get(WebObjects.WebContent);

                        var items = item.DataManager.GetByDirectory(folderId, loweredKeyword);

                        //var items = from i in WebContent.GetByDirectory(folderId)
                        //            where noSearch
                        //                || DataHelper.HasMatch(i.Title, loweredKeyword)
                        //                || DataHelper.HasMatch(i.Content, loweredKeyword)
                        //            orderby i.DateModified descending
                        //            select new
                        //            {
                        //                i.Id,
                        //                RecordId = i.Id,
                        //                ObjectId = item.Id,
                        //                Name = i.Title,
                        //                ObjectName = item.FriendlyNameEval,
                        //                i.DateModified
                        //            };

                        return DataHelper.ToDataSet(items);
                    }

                case WebObjects.WebTextResource:
                    {
                        var item = WebObject.Get(WebObjects.WebTextResource);

                        var items = from i in WebTextResource.GetByDirectory(folderId)
                                    where noSearch
                                        || DataHelper.HasMatch(i.Title, loweredKeyword)
                                        || DataHelper.HasMatch(i.Content, loweredKeyword)
                                    orderby i.DateModified descending
                                    select new
                                    {
                                        i.Id,
                                        RecordId = i.Id,
                                        ObjectId = item.Id,
                                        Name = i.Title,
                                        ObjectName = item.FriendlyNameEval,
                                        i.DateModified
                                    };

                        return DataHelper.ToDataSet(items);
                    }

                default:
                    {
                        var items = from i in WebFile.Provider.GetList(folderId)
                                    where (objectId == -1 || i.ObjectId == objectId)
                                        && (noSearch
                                            || DataHelper.HasMatch(i.Name, loweredKeyword))
                                    select new
                                    {
                                        i.Id,
                                        i.RecordId,
                                        i.ObjectId,
                                        i.Name,
                                        ObjectName = WebObject.Get(i.ObjectId).FriendlyNameEval,
                                        DateModified = WConstants.DateTimeMinValue
                                    };

                        return DataHelper.ToDataSet(items);
                    }
            }
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;

            GridView1.DataBind();
        }
    }
}
