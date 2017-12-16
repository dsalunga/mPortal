using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.WebParts.Menu;

namespace WCMS.WebSystem.WebParts.Menu
{
    public partial class ConfigLinkedMenuEdit : System.Web.UI.UserControl
    {
        private int menuItemId = -1;
        private int currItemMenuId = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            menuItemId = DataHelper.GetId(Request, "MenuItemId");

            if (!Page.IsPostBack)
            {
                // Bind Menus
                cboMenus.DataSource = MenuEntity.Provider.GetList();
                cboMenus.DataBind();

                if (menuItemId > 0)
                {
                    MenuItem item = MenuItem.Provider.Get(menuItemId);
                    if (item != null)
                    {
                        this.cboTarget.DataBind();

                        if (cboMenus.Items.FindByValue(item.MenuId.ToString()) != null)
                        {
                            cboMenus.SelectedValue = item.MenuId.ToString();

                            // Select Menu Item
                            BindMenuItems(item.MenuId, item.Id);

                            if (cboMenuItems.Items.Count > 0)
                                SetItemPosition(item);
                        }

                        txtCaption.Text = item.Text;
                        chkIsActive.Checked = item.Active == 1;
                        chkCheckPermission.Checked = item.CheckPermission == 1;

                        if (cboTarget.Items.FindByValue(item.Target) != null)
                            cboTarget.SelectedValue = item.Target;

                        currItemMenuId = item.MenuId;
                    }
                }

                // Display Page Url
                int pageId = DataHelper.GetId(Request, WebColumns.PageId);
                WPage page = null;
                if (pageId > 0 && (page = WPage.Get(pageId)) != null)
                {
                    txtNavigateURL.Text = page.BuildRelativeUrl();

                    if (string.IsNullOrEmpty(txtCaption.Text.Trim()))
                        txtCaption.Text = page.Name;
                }
            }
        }

        private void SetItemPosition(MenuItem item)
        {
            Action<List<MenuItem>> DetermineIfBeforeOrAfter = (List<MenuItem> menuItems) =>
            {
                var currItem = menuItems.FirstOrDefault(i => i.Id == item.Id);
                if (currItem != null)
                {
                    var currIdx = menuItems.IndexOf(currItem);
                    if (currIdx == 0)
                    {
                        // First item, set to Before
                        cboMenuItems.SelectedValue = menuItems[1].Id.ToString();
                        cboItemPostion.SelectedValue = ItemPositions.Before.ToString();
                    }
                    else
                    {
                        cboMenuItems.SelectedValue = menuItems[currIdx - 1].Id.ToString();
                        cboItemPostion.SelectedValue = ItemPositions.After.ToString();
                    }
                }
            };

            List<MenuItem> items = new List<MenuItem>();

            if (item.ParentId > 0)
            {
                items.AddRange(MenuItem.Provider.GetList(item.MenuId, item.ParentId));
                if (items.Count() == 1)
                {
                    cboMenuItems.SelectedValue = item.Parent.Id.ToString();
                    cboItemPostion.SelectedValue = ItemPositions.Child.ToString();
                }
                else
                {
                    // Before or After
                    DetermineIfBeforeOrAfter(items);
                }
            }
            else
            {
                items.AddRange(MenuItem.Provider.GetList(item.MenuId, item.ParentId));
                DetermineIfBeforeOrAfter(items);
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.ReturnPage();
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            int pageId = DataHelper.GetId(Request, WebColumns.PageId);
            if (pageId > 0)
            {
                MenuItem item = null;

                if (menuItemId > 0 && (item = MenuItem.Provider.Get(menuItemId)) != null) { }
                else
                {
                    /* INSERT */
                    item = new MenuItem();
                }

                item.MenuId = DataHelper.GetId(cboMenus.SelectedValue);
                item.Text = txtCaption.Text.Trim();
                item.Active = chkIsActive.Checked ? 1 : 0;
                item.CheckPermission = chkCheckPermission.Checked ? 1 : 0;

                // Set PageId / PageUrl
                item.NavigateUrl = string.Empty;
                item.PageId = pageId;

                item.Target = cboTarget.SelectedValue;

                // Set Parent and Rank
                int relItemId = DataHelper.GetId(cboMenuItems.SelectedValue);
                MenuItem relItem = null;
                List<MenuItem> items = new List<MenuItem>();
                int relPosition = DataHelper.GetInt32(cboItemPostion.SelectedValue);

                if (relItemId > 0)
                {
                    switch (relPosition)
                    {
                        case ItemPositions.Child:
                            {
                                item.ParentId = relItemId;
                                items.AddRange(MenuItem.Provider.GetList(item.MenuId, relItemId));

                                if (items.Count == 0 || items.First().Id != item.Id)
                                    item.Rank = 25;
                                else
                                    item.Rank = items.Last().Rank + 25;

                                break;
                            }

                        case ItemPositions.Before:
                            {
                                relItem = MenuItem.Provider.Get(relItemId);

                                items.AddRange(MenuItem.Provider.GetList(item.MenuId, relItem.ParentId));
                                relItem = items.Find(i => i.Id == relItem.Id);

                                item.ParentId = relItem.ParentId;

                                var relItemIndex = items.IndexOf(relItem);
                                if (items.Count == 1 || relItemIndex == 0)
                                {
                                    item.Rank = relItem.Rank - 25;
                                }
                                else
                                {
                                    var relItemBefore = items[relItemIndex - 1];
                                    item.Rank = relItem.Rank - (relItem.Rank - relItemBefore.Rank) / 2;
                                }

                                break;
                            }

                        case ItemPositions.After:
                            {
                                relItem = MenuItem.Provider.Get(relItemId);

                                items.AddRange(MenuItem.Provider.GetList(item.MenuId, relItem.ParentId));
                                relItem = items.Find(i => i.Id == relItem.Id);

                                item.ParentId = relItem.ParentId;

                                if (items.Count == 1)
                                {
                                    item.Rank = relItem.Rank + 25;
                                }
                                else
                                {
                                    var relItemIndex = items.IndexOf(relItem);
                                    if (items.Count > relItemIndex + 1)
                                    {
                                        var relItemAfter = items[relItemIndex + 1];
                                        item.Rank = relItem.Rank + (relItemAfter.Rank - relItem.Rank) / 2;
                                    }
                                    else
                                    {
                                        item.Rank = relItem.Rank + 25;
                                    }
                                }

                                break;
                            }
                    }
                }
                else
                {
                    item.Rank = 25;
                }

                item.Update();

                this.ReturnPage();
            }
        }

        private void ReturnPage()
        {
            var query = new WQuery(this);
            query.Remove("MenuItemId");
            query.Remove(WConstants.Load);
            query.Redirect();
        }

        public DataSet GetLinkTargets()
        {
            return DataHelper.ToDataSet(WebConstant.Provider.GetList("LinkTargets"));
        }

        public void BindMenuItems(int menuId, int excludeId = -1)
        {
            cboMenuItems.Items.Clear();

            MenuHelper.PopulateCboMenuItems(cboMenuItems, menuId, false, excludeId);
        }

        protected void cboMenus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int menuId = DataHelper.GetId(cboMenus.SelectedValue);
            if (menuId > 0)
            {
                BindMenuItems(menuId);

                if (menuItemId > 0 && currItemMenuId == menuId)
                {
                    var item = MenuItem.Provider.Get(menuItemId);
                    if (item != null)
                        SetItemPosition(item);
                }
            }
        }
    }
}