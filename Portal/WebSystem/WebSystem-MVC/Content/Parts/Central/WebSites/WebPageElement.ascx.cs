using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

using WCMS.WebSystem.Controls;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Central.WebSites
{
    public partial class WebPageElementController : System.Web.UI.UserControl
    {
        protected void tabNavigation_SelectedTabChanged(object oSender, TabEventArgs args)
        {
            switch (args.TabName)
            {
                case "tabProperties":
                    MultiView1.SetActiveView(viewBasic);
                    break;

                case "tabExtended":
                    MultiView1.SetActiveView(viewAdvanced);
                    break;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                tabNavigation.AddTab("tabProperties", "Properties");
                tabNavigation.AddTab("tabExtended", "Extended Properties");

                var query = new QueryParser(this);
                int elementId = query.GetId(WebColumns.PageElementId);
                int panelRecordId = -1;
                int panelObjectId = WebObjects.WebTemplate;

                WebPageElement element = elementId > 0 ? WebPageElement.Get(elementId) : null;
                ObjectKey pair = element == null ? this.GetObjectKeyPair() : new ObjectKey(element.ObjectId, element.RecordId);

                int templatePanelId = element != null ? element.TemplatePanelId : query.GetId(WebColumns.TemplatePanelId);

                WebMasterPage masterPage = null;
                WPage page = null;
                if (pair.ObjectId == WebObjects.WebMasterPage)
                {
                    masterPage = WebMasterPage.Get(pair.RecordId);
                    panelRecordId = masterPage.TemplateId;
                }
                else if (pair.ObjectId == WebObjects.WebPage)
                {
                    page = WPage.Get(pair.RecordId);
                    if (page.GetEvalTypeId() == PageTypes.Static)
                    {
                        panelObjectId = WebObjects.WebPartControlTemplate;
                        panelRecordId = page.PartControlTemplateId;
                    }
                    else
                    {
                        masterPage = page.MasterPage;
                        panelRecordId = masterPage.TemplateId;
                    }
                }
                else
                {
                    throw new Exception("Not a WebPage and neither a WebMasterPage.");
                }

                // Rank
                int rank = WebPageElement.GetMaxRank(pair.RecordId, pair.ObjectId, templatePanelId);
                if (rank < 1)
                    rank = 1;

                // Load Panels
                var panels = WebTemplatePanel.Provider.GetList(panelObjectId, panelRecordId);
                IEnumerable<WebTemplatePanel> parentPanels = null;
                // Load Parent panels
                if (masterPage != null)
                {
                    var parent = masterPage.Template.Parent;
                    if (parent != null)
                    {
                        parentPanels = parent.Panels;
                    }
                }

                cboPanel.DataSource = parentPanels == null ? panels : panels.Concat(parentPanels);
                cboPanel.DataBind();

                if (templatePanelId > 0)
                    cboPanel.SelectedValue = templatePanelId.ToString();
                else if (panels.Count() > 0)
                    templatePanelId = panels.First().Id;

                // Load Siblings
                if (masterPage != null)
                {
                    SetupSiblingsAndRank(templatePanelId, masterPage.Id, element);
                }
                else
                {
                    chkCustomRank.Enabled = true;
                    chkCustomRank.Checked = true;
                }

                // Owners
                cboOwner.Items.Add(new ListItem("Web Page", WebObjects.WebPage.ToString()));
                cboOwner.Items.Add(new ListItem("Master Page", WebObjects.WebMasterPage.ToString()));

                int partControlTemplateId = 1;

                // IF EDITING ONLY THEN
                if (element != null)
                {
                    // Load element info
                    txtName.Text = element.Name;
                    hiddenCSITID.Value = element.PartControlTemplateId.ToString(); //test only
                    partControlTemplateId = element.PartControlTemplateId;

                    // Public Security
                    int publicAccess = element.PublicAccess;
                    if ((publicAccess & WebPublicAccess.AllIPAddressExceptEntries) > 0)
                    {
                        chkIPAddress.Checked = true;
                        publicAccess -= WebPublicAccess.AllIPAddressExceptEntries;
                    }
                    if ((publicAccess & WebPublicAccess.AllAccountExceptEntries) > 0)
                    {
                        chkAccount.Checked = true;
                        publicAccess -= WebPublicAccess.AllAccountExceptEntries;
                    }
                    cboPublicAccess.SelectedValue = publicAccess.ToString();

                    txtRank.Text = element.Rank.ToString();
                    chkIsActive.Checked = element.IsActive;

                    cboOwner.SelectedValue = element.ObjectId.ToString();

                    chkUsePartTemplatePath.Checked = element.UsePartTemplatePath == 1;
                    ManagementSecurityOption1.Value = element.ManagementAccess;
                }
                else
                {
                    txtRank.Text = (rank + 5).ToString();
                    hiddenCSITID.Value = partControlTemplateId.ToString();

                    int masterPageId = query.GetId(WebColumns.MasterPageId);
                    int pageId = query.GetId(WebColumns.PageId);

                    if (pageId > 0)
                    {
                        WebHelper.SetCboValue(cboOwner, WebObjects.WebPage);

                        /*
                        var panel = WebPagePanel.Get(templatePanelId, pageId);
                        if (panel == null || panel.UsageTypeId == PanelUsage.Inherit)
                            WebHelper.SetCboValue(cboOwner, WebObjects.WebMasterPage); // Not overriden so default is MasterPage
                        else
                            WebHelper.SetCboValue(cboOwner, WebObjects.WebPage);
                        */
                    }
                    else if (masterPageId > 0)
                    {
                        WebHelper.SetCboValue(cboOwner, WebObjects.WebMasterPage);
                    }
                }

                if (partControlTemplateId > 0)
                    this.DisplayTemplateInfo(partControlTemplateId);

                if (masterPage != null)
                    hMasterPageId.Value = masterPage.Id.ToString();
                hElementId.Value = elementId.ToString();
            }
        }

        private void SetupSiblingsAndRank(int templatePanelId, int masterPageId, WebPageElement element)
        {
            QueryParser query = new QueryParser(this);
            int pageId = query.GetId(WebColumns.PageId);

            WebPagePanel panel = pageId > 0 ? WebPagePanel.Get(templatePanelId, pageId) : null;

            var panelUsage = panel != null ? panel.UsageTypeId : PanelUsage.Inherit;

            List<WebPageElement> siblings = new List<WebPageElement>();

            if (panelUsage != PanelUsage.Override)
                WebPageElement.GetList(masterPageId, WebObjects.WebMasterPage, templatePanelId).Where(i => i.IsActive);

            if (panelUsage != PanelUsage.Inherit && pageId > 0)
                siblings.AddRange(WebPageElement.GetList(pageId, WebObjects.WebPage, templatePanelId).Where(i => i.IsActive));

            WebPageElement self = null;
            if (siblings.Count > 0 && element != null && (self = siblings.Find(i => i.Id == element.Id)) != null)
                siblings.Remove(self);

            bool hasSiblings = siblings.Count > 0;
            var orderedSiblings = siblings.OrderBy(i => i.Rank);

            //cboSiblings.Enabled = hasSiblings;
            //cboItemPostion.Enabled = hasSiblings;
            //chkCustomRank.Enabled = hasSiblings;
            //chkCustomRank.Checked = !hasSiblings;

            cboSiblings.Enabled = false;
            cboItemPostion.Enabled = false;
            chkCustomRank.Enabled = false;
            chkCustomRank.Checked = true;

            if (hasSiblings)
            {
                cboSiblings.DataSource = from i in orderedSiblings
                                         select new
                                         {
                                             i.Id,
                                             Name = string.Format("{0} [{1}]", i.Name, i.Rank)
                                         };
                cboSiblings.DataBind();


                // Set Item Position
                var count = orderedSiblings.Count();
                if (self != null)
                {
                    for (int i = 0; i < count; i++)
                    {
                        WebPageElement orderedSibling = orderedSiblings.ElementAt(i);
                        if (self.Rank <= orderedSibling.Rank)
                        {
                            cboItemPostion.SelectedIndex = 1;
                            cboSiblings.SelectedIndex = i;
                            break;
                        }
                        else if (self.Rank >= orderedSibling.Rank && (count - 1 == i))
                        {
                            cboItemPostion.SelectedIndex = 0;
                            cboSiblings.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    cboSiblings.SelectedIndex = count - 1;
                }
            }
        }

        private ObjectKey GetObjectKeyPair()
        {
            return GetObjectKeyPair(false);
        }

        private ObjectKey GetObjectKeyPair(bool onUpdate)
        {
            ObjectKey pair = new ObjectKey();
            QueryParser query = new QueryParser(this);

            int pageId = query.GetId(WebColumns.PageId);
            int masterPageId = query.GetId(WebColumns.MasterPageId);

            if (onUpdate)
            {
                pair.ObjectId = DataHelper.GetId(cboOwner.SelectedValue);

                if (pair.ObjectId == WebObjects.WebMasterPage)
                {
                    if (masterPageId == -1)
                    {
                        // Make sure Master Id is present
                        masterPageId = WPage.Get(pageId).MasterPage.Id;
                    }

                    pair.RecordId = masterPageId;
                }
                else if (pair.ObjectId == WebObjects.WebPage)
                {
                    pair.RecordId = pageId;
                }
                else
                {
                    throw new Exception("PageId or MasterPageId not supplied.");
                }
            }
            else
            {
                if (masterPageId > 0)
                {
                    pair.ObjectId = WebObjects.WebMasterPage;
                    pair.RecordId = masterPageId;
                }
                else if (pageId > 0)
                {
                    pair.ObjectId = WebObjects.WebPage;
                    pair.RecordId = pageId;
                }
                else
                {
                    throw new Exception("PageId or MasterPageId not supplied.");
                }
            }

            return pair;
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            Return();
        }

        private void Return(int elementId = -1)
        {
            var query = new QueryParser(this);
            int id = elementId > 0 ? elementId : query.GetId(WebColumns.PageElementId);
            if (id > 0)
            {
                query.Set(WebColumns.PageElementId, id);
                query.Redirect(CentralPages.WebPageElementHome);
            }
            else
            {
                query.Redirect(CentralPages.WebPageElements);
            }
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            Return(this.SaveElement());
        }

        protected void cmdNext_Click(object sender, EventArgs e)
        {
            var query = new QueryParser(this);
            query.Set(WebColumns.PageElementId, this.SaveElement());
            query.Redirect(CentralPages.LoaderMain);
        }

        private int SaveElement()
        {
            var query = new QueryParser(this);
            var pair = this.GetObjectKeyPair(true);

            int id = DataHelper.GetId(Request, WebColumns.PageElementId);
            WebPageElement item = null;

            var templateId = DataHelper.GetId(hiddenCSITID.Value);
            if (templateId <= 0)
                throw new Exception("A WebPart must be selected");

            if (id > 0)
                item = WebPageElement.Get(id);
            else
                item = new WebPageElement();

            item.ObjectId = pair.ObjectId;
            item.RecordId = pair.RecordId;
            item.TemplatePanelId = DataHelper.GetId(cboPanel.SelectedValue);
            item.Name = txtName.Text.Trim();
            item.Rank = Convert.ToInt32(txtRank.Text.Trim());
            item.PartControlTemplateId = templateId;
            item.Active = chkIsActive.Checked ? 1 : 0;
            item.UsePartTemplatePath = chkUsePartTemplatePath.Checked ? 1 : 0;

            // Public Security
            int publicAccess = DataHelper.GetInt32(cboPublicAccess.SelectedValue);
            if (chkAccount.Checked) publicAccess += WebPublicAccess.AllAccountExceptEntries;
            if (chkIPAddress.Checked) publicAccess += WebPublicAccess.AllIPAddressExceptEntries;
            item.PublicAccess = publicAccess;
            item.Update();

            if (item.ObjectId == WebObjects.WebPage && item.IsActive) // && id == -1)
            {
                var items = WebPageElement.GetList(item.ObjectId, item.RecordId, item.TemplatePanelId);
                if (items.Count() == 1)
                {
                    var panel = WebPagePanel.Get(item.TemplatePanelId, item.RecordId);
                    if (panel == null)
                    {
                        panel = new WebPagePanel();
                        panel.PageId = item.RecordId;
                        panel.TemplatePanelId = item.TemplatePanelId;
                    }

                    if (panel.TemplatePanelId == PanelUsage.Inherit)
                    {
                        panel.TemplatePanelId = PanelUsage.Add;
                        panel.Update();
                    }
                }
            }

            return item.Id;
        }

        protected void viewModuleChooser_Activate(object sender, EventArgs e)
        {
            this.PopulateModuleTree();

            //// HIDE OBJECTS
            trControlBox.Visible = false;
            tabNavigation.Visible = false;
        }

        protected void imgPreview_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.SetActiveView(viewModuleChooser);
        }

        protected void cmdOK_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(viewBasic);

            trControlBox.Visible = true;
            tabNavigation.Visible = true;

            hiddenCSITID.Value = hiddenTempCSITID.Value;
            this.DisplayTemplateInfo(DataHelper.GetId(hiddenCSITID.Value));
        }

        protected void cmdTemplateCancel_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(viewBasic);

            trControlBox.Visible = true;
            tabNavigation.Visible = true;
        }

        private void PopulateModuleTree()
        {
            // LOAD MODULES
            int partControlTemplateId = DataHelper.GetId(hiddenCSITID.Value);

            TreeNode tnRoot = WebPartViewModel.GenerateModuleChooserTree(partControlTemplateId, hiddenTempCSITID.ClientID, chkShowAll.Checked);
            tv1.Nodes.Clear();
            tv1.Nodes.Add(tnRoot);
        }

        private void DisplayTemplateInfo(int partControlTemplateId)
        {
            var partControlTemplate = WebPartControlTemplate.Get(partControlTemplateId);
            if (partControlTemplate != null)
            {
                var partControl = partControlTemplate.PartControl;

                // MODULE INFO
                imgPreview.ImageUrl = "~/Content/Assets/Images/PartThumb.jpg";
                lTemplateName.Text = string.Format("<strong style=\"font-size: larger;\">{0}</strong><br />{1}<br />{2}",
                    partControlTemplate.Name, partControl.Name, partControl.Part.Name);
            }
        }

        protected void cboPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            int elementId = DataHelper.GetId(hElementId.Value);
            int masterPageId = DataHelper.GetId(hMasterPageId.Value);
            int templatePanelId = DataHelper.GetId(cboPanel.SelectedValue);

            if (masterPageId > 0)
            {
                WebPageElement element = elementId > 0 ? WebPageElement.Get(elementId) : null;
                SetupSiblingsAndRank(templatePanelId, masterPageId, element);
            }
        }

        protected void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            PopulateModuleTree();
        }
    }
}