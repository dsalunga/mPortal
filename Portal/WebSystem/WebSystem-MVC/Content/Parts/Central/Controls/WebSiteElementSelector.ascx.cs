using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class WebSiteElementSelector : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateWebSites()
        {
            cboWebSites.Items.Clear();
            cboWebSites.Items.AddRange(WebSiteViewModel.GenerateListItem(-1).ToArray());
        }

        public List<ObjectKey> GetSelectedObjects()
        {
            List<ObjectKey> selectedObjects = new List<ObjectKey>();

            var nodes = treeElements.CheckedNodes;
            foreach (TreeNode node in nodes)
                if (ObjectKey.IsValid(node.Value))
                    selectedObjects.Add(new ObjectKey(node.Value));

            return selectedObjects;
        }

        public List<ObjectKey> GetUnselectedObjects()
        {
            List<ObjectKey> unselectedObjects = new List<ObjectKey>();

            Action<TreeNodeCollection> SetObjectsRecursive = null;
            SetObjectsRecursive = (childNodes) =>
            {
                foreach (TreeNode node in childNodes)
                {
                    if (ObjectKey.IsValid(node.Value) && !node.Checked)
                        unselectedObjects.Add(new ObjectKey(node.Value));

                    SetObjectsRecursive(node.ChildNodes);
                }
            };

            SetObjectsRecursive(treeElements.Nodes);

            return unselectedObjects;
        }

        public List<ObjectKey> SelectedObjects
        {
            get
            {
                object selectedObjects = ViewState["SelectedObjects"];
                if (selectedObjects != null)
                    return (List<ObjectKey>)selectedObjects;
                else
                    return new List<ObjectKey>();
            }

            set { ViewState["SelectedObjects"] = value; }
        }

        private void SetSelectedObjects()
        {
            List<ObjectKey> selectedObjects = SelectedObjects;
            HashSet<String> hs = new HashSet<string>();
            foreach (var item in selectedObjects)
                hs.Add(item.ToString());

            Action<TreeNodeCollection> SetObjectsRecursive = null;
            SetObjectsRecursive = (childNodes) =>
                {
                    foreach (TreeNode node in childNodes)
                    {
                        SetObjectsRecursive(node.ChildNodes);

                        if (ObjectKey.IsValid(node.Value))
                        {
                            if (hs.Contains(new ObjectKey(node.Value).ToString()))
                            {
                                node.Checked = true;
                                continue;
                            }
                        }

                        node.Checked = false;
                    }
                };

            SetObjectsRecursive(treeElements.Nodes);
        }


        // List<IWebObject> selected
        public void BuildTree()
        {
            treeElements.Nodes.Clear();

            int partId = SelectionPartId;
            int siteId = DataHelper.GetId(cboWebSites.SelectedValue);

            if (siteId > 0)
            {
                WSite site = WSite.Get(siteId);
                var pages = site.Pages;

                TreeNode siteNode = new TreeNode(site.Name, site.Id.ToString());
                //siteNode.ShowCheckBox = true;
                treeElements.Nodes.Add(siteNode);

                TreeNode pagesNode = new TreeNode("Web Pages", "-1");
                //pagesNode.ShowCheckBox = true;
                siteNode.ChildNodes.Add(pagesNode);

                // Recursive WebPages method
                Func<int, TreeNode, bool> BuildPagesRecursive = null;
                BuildPagesRecursive = (parentId, parentNode) =>
                    {
                        bool isValid = false;
                        var levelPages = pages.Where(i => i.ParentId == parentId);

                        // Iterate all WebPages
                        foreach (var page in levelPages)
                        {
                            bool localIsValid = false;

                            // # WebPage #
                            TreeNode currentNode = new TreeNode(page.Name, "-1");

                            // isValid if has children or
                            if (BuildPagesRecursive(page.Id, currentNode))
                                localIsValid = true;

                            // Check and add elements
                            var elements = page.Elements;
                            foreach (var element in elements)
                            {
                                if (partId == -1 || element.PartControlTemplate.Part.Id == partId)
                                {
                                    // # WebPageElement #
                                    TreeNode elementNode = new TreeNode(element.Name, new ObjectKey(WebObjects.WebPageElement, element.Id).ToString());
                                    elementNode.ShowCheckBox = true;
                                    currentNode.ChildNodes.Add(elementNode);

                                    if (!localIsValid)
                                        localIsValid = true;
                                }
                            }

                            // isValid if matched the partId
                            var partControlTemplate = page.PartControlTemplate;
                            if (partId == -1 || (partControlTemplate != null && partControlTemplate.Part.Id == partId))
                            {
                                currentNode.ShowCheckBox = true;
                                currentNode.Value = new ObjectKey(WebObjects.WebPage, page.Id).ToString();
                                if (!localIsValid)
                                    localIsValid = true;
                            }

                            // Add only if matched the partId or has valid child
                            if (localIsValid)
                                parentNode.ChildNodes.Add(currentNode);

                            if (!isValid && localIsValid)
                                isValid = true;
                        }

                        return isValid;
                    };

                BuildPagesRecursive(-1, pagesNode);

                TreeNode masterPagesNode = new TreeNode("Master Pages", "-1");
                //masterPagesNode.ShowCheckBox = true;
                siteNode.ChildNodes.Add(masterPagesNode);

                var masterPages = site.MasterPages;
                foreach (var masterPage in masterPages)
                {
                    bool isValid = false;
                    TreeNode masterPageNode = new TreeNode(masterPage.Name, masterPage.Id.ToString());
                    //masterPageNode.ShowCheckBox = true;

                    var elements = masterPage.Elements;
                    foreach (var element in elements)
                    {
                        if (partId == -1 || element.PartControlTemplate.Part.Id == partId)
                        {
                            // # WebMasterPageElement #
                            TreeNode elementNode = new TreeNode(element.Name, new ObjectKey(WebObjects.WebPageElement, element.Id).ToString());
                            elementNode.ShowCheckBox = true;
                            masterPageNode.ChildNodes.Add(elementNode);

                            if (!isValid)
                                isValid = true;
                        }
                    }

                    if (isValid)
                    {
                        masterPagesNode.ChildNodes.Add(masterPageNode);
                        //masterPageNode.ShowCheckBox = true;
                    }
                }
            }

            treeElements.ExpandAll();

            SetSelectedObjects();
        }

        protected void cboWebSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildTree();
        }

        public int SelectionPartId
        {
            get { return DataHelper.GetId(hidPartId.Value); }
            set { hidPartId.Value = value.ToString(); }
        }

        public int SiteId
        {
            set { cboWebSites.SelectedValue = value.ToString(); }
            get { return DataHelper.GetId(cboWebSites.SelectedValue); }
        }
    }
}