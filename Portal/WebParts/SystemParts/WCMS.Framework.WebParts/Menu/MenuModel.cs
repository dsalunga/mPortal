using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using WCMS.Common;
using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Menu
{
    public class MenuModel
    {
        public MenuModel()
        {
            MenuItems = new List<MenuItemModel>();
            Children = new List<MenuItemModel>();
            RootItemId = -1;
        }

        public MenuModel(string itemTemplate)
            : this()
        {
            ItemTemplate = itemTemplate;
        }

        //public string Header { get; set; }
        //public string Footer { get; set; }
        public string Separator { get; set; }
        
        /// <summary>
        /// A custom separator item
        /// </summary>
        public string SeparatorItemTemplate { get; set; }
        
        /// <summary>
        /// Header text separator item
        /// </summary>
        public string HeaderItemTemplate { get; set; }

        public string ItemTemplate { get; set; }
        //public string BodyTemplate { get; set; }
        public string FirstItemTemplate { get; set; }
        public string SelectedItemTemplate { get; set; }

        public string UpOneLevelTemplate { get; set; }
        public string SeeAlsoTemplate { get; set; }

        public string AlternateItemTemplate { get; set; }
        public string AlternateRootItemTemplate { get; set; }
        public string AlternateParentItemTemplate { get; set; }

        public string ParentItemTemplate { get; set; }
        public string SelectedParentItemTemplate { get; set; }
        public string RootItemTemplate { get; set; }
        public string RootItemNoChildrenTemplate { get; set; }
        public string SelectedRootItemTemplate { get; set; }
        public string ContainerTemplate { get; set; }

        public int RootItemId { get; set; }

        public List<MenuItemModel> MenuItems { get; set; }
        public MenuItemModel SelectedItem { get; set; }

        /// <summary>
        /// Show in "See also"
        /// </summary>
        public List<MenuItemModel> Children { get; set; }


        // For checking if to include children
        public bool IsParentItemEnabled { get { return !string.IsNullOrEmpty(ParentItemTemplate); } }
        public bool IsAlternateParentItemEnabled { get { return !string.IsNullOrEmpty(AlternateParentItemTemplate); } }
        public bool IsRootItemEnabled { get { return !string.IsNullOrEmpty(RootItemTemplate); } }

        public void BuildItems(int menuId, string pageUrl, int renderMode = RenderModes.Absolute, bool isSiteMapMode = false)
        {
            // Fetch menu items
            var items = MenuItem.Provider.GetList(menuId);
            var selected = items.FirstOrDefault(i => i.PageUrl.IndexOf(pageUrl, StringComparison.CurrentCultureIgnoreCase) > -1); //.StartsWith(pageUrl, StringComparison.InvariantCultureIgnoreCase));

            // Render behaviour
            if (renderMode == RenderModes.Relative)
            {
                if (selected != null)
                {
                    if (selected.ParentId > 0)
                    {
                        RootItemId = selected.ParentId;
                        items = MenuItem.Provider.GetList(menuId, selected.ParentId);
                    }

                    // Add children for display in "See also"
                    if (!string.IsNullOrEmpty(SeeAlsoTemplate))
                    {
                        var children = selected.Children;
                        foreach (var child in children)
                        {
                            if (child.Active == 1 && child.IsVisible)
                            {
                                MenuItemModel itemModel = new MenuItemModel(null, child);
                                Children.Add(itemModel);
                            }
                        }
                    }
                }
            }

            Func<int, IEnumerable<MenuItem>, MenuItemModel, List<MenuItemModel>> BuildLogicalMenuRecursive = null;
            BuildLogicalMenuRecursive = (parentId, subMenuItems, parentMenu) =>
            {
                var subItems = new List<MenuItemModel>();
                var rows = subMenuItems.Where(i => i.ParentId == parentId);
                foreach (var row in rows)
                {
                    if (row.Active == 1 && row.IsVisible)
                    {
                        var menuItem = new MenuItemModel(parentMenu, row);
                        subItems.Add(menuItem);

                        // Check whether to include child items
                        if (IsParentItemEnabled || IsAlternateParentItemEnabled || IsRootItemEnabled)
                        {
                            var subs = BuildLogicalMenuRecursive(row.Id, subMenuItems, menuItem);
                            if (subs.Count > 0)
                                menuItem.Children.AddRange(subs);
                        }

                        // Keep the selected item
                        if (selected != null && selected.Id == row.Id)
                            SelectedItem = menuItem;
                    }
                    else if (isSiteMapMode)
                    {
                        BuildLogicalMenuRecursive(row.Id, subMenuItems, parentMenu);
                    }
                }

                return subItems;
            };

            MenuItems.AddRange(BuildLogicalMenuRecursive(RootItemId, items, null));
        }

        public string RenderAsText()
        {
            bool rootItemEnabled = !string.IsNullOrEmpty(RootItemTemplate);
            bool rootItemNoChildrenEnabled = !string.IsNullOrEmpty(RootItemNoChildrenTemplate);
            bool parentItemEnabled = !string.IsNullOrEmpty(ParentItemTemplate);
            bool itemEnabled = !string.IsNullOrEmpty(ItemTemplate);

            bool firstItemEnabled = !string.IsNullOrEmpty(FirstItemTemplate);
            bool selectedItemEnabled = !string.IsNullOrEmpty(SelectedItemTemplate) && SelectedItem != null;
            bool selectedParentItemEnabled = !string.IsNullOrEmpty(SelectedParentItemTemplate) && SelectedItem != null;
            bool selectedRootItemEnabled = !string.IsNullOrEmpty(SelectedRootItemTemplate) && SelectedItem != null;

            bool upOneLevelEnabled = !string.IsNullOrEmpty(UpOneLevelTemplate) && RootItemId > 0;
            bool seeAlsoEnabled = !string.IsNullOrEmpty(SeeAlsoTemplate) && Children.Count > 0;

            bool alternateEnabled = !string.IsNullOrEmpty(AlternateItemTemplate);
            bool alternateRootItemEnabled = !string.IsNullOrEmpty(AlternateRootItemTemplate);
            bool alternateParentItemEnabled = !string.IsNullOrEmpty(AlternateParentItemTemplate);

            bool separatorEnabled = !string.IsNullOrEmpty(Separator);
            bool separatorItemEnabled = !string.IsNullOrEmpty(SeparatorItemTemplate);
            bool headerItemEnabled = !string.IsNullOrEmpty(HeaderItemTemplate);

            var output = new StringBuilder();

            // Header
            //if (!string.IsNullOrEmpty(Header))
            //    output.Append(Header);

            // BodyTemplate

            // ParentItemTemplate, RootItemTemplate, ItemTemplate, Separator, AlternateItemTemplate
            Func<List<MenuItemModel>, string> RenderItemsRecursive = null;
            RenderItemsRecursive = (items) =>
            {
                var context = new Dictionary<string, INamedValueProvider>();
                var sb = new StringBuilder();
                int counter = 0;
                int maxIndex = items.Count - 1;

                foreach (var item in items)
                {
                    // Prepare Substituter parameters
                    var valueProvider = new NamedValueProvider();
                    valueProvider.Add(TemplateKeys.Target, item.Target);
                    valueProvider.Add(TemplateKeys.TargetProperty,
                        string.IsNullOrEmpty(item.Target) ? string.Empty : string.Format(@" target=""{0}""", item.Target));
                    valueProvider.Add(TemplateKeys.Text, item.Text);
                    valueProvider.Add(TemplateKeys.Url, item.Url);
                    context[Substituter.DefaultProviderKey] = valueProvider;

                    // Separator. Append separator content in between items
                    if (separatorEnabled && counter > 0 && counter > maxIndex)
                        sb.Append(Separator);

                    var children = item.Children;
                    var isParent = children.Count > 0;
                    if (separatorItemEnabled && item.Text == "-")
                    {
                        sb.Append(Substituter.Substitute(SeparatorItemTemplate, context));
                    }
                    else if (headerItemEnabled && item.Url == "--header")
                    {
                        sb.Append(Substituter.Substitute(HeaderItemTemplate, context));
                    }
                    else if (isParent && selectedParentItemEnabled && SelectedItem == item)
                    {
                        // Child Items
                        var stringChildItems = RenderItemsRecursive(children);
                        valueProvider.Add(ContextKeys.Children, stringChildItems);

                        // SelectedItemTemplate
                        sb.Append(Substituter.Substitute(SelectedParentItemTemplate, context));
                        selectedItemEnabled = false;
                        selectedRootItemEnabled = false;
                        selectedParentItemEnabled = false;
                    }
                    else if (selectedRootItemEnabled && item.IsRoot && SelectedItem == item)
                    {
                        // SelectedItemTemplate
                        sb.Append(Substituter.Substitute(SelectedRootItemTemplate, context));
                        selectedItemEnabled = false;
                        selectedRootItemEnabled = false;
                        selectedParentItemEnabled = false;
                    }
                    else if (selectedItemEnabled && SelectedItem == item)
                    {
                        // SelectedItemTemplate
                        sb.Append(Substituter.Substitute(SelectedItemTemplate, context));
                        selectedItemEnabled = false;
                        selectedRootItemEnabled = false;
                        selectedParentItemEnabled = false;
                    }
                    else if (counter == 0 && item.IsRoot && (firstItemEnabled || (selectedItemEnabled && SelectedItem == null)))
                    {
                        if (firstItemEnabled)
                        {
                            // FirstItemTemplate
                            sb.Append(Substituter.Substitute(FirstItemTemplate, context));
                            firstItemEnabled = false;
                        }
                        else // SelectedItemTemplate
                        {
                            sb.Append(Substituter.Substitute(SelectedItemTemplate, context));
                            selectedItemEnabled = false;
                        }
                    }
                    else
                    {
                        if (!alternateEnabled || counter % 2 == 0)
                        {
                            // No Alternation

                            if (item.Parent == null && rootItemEnabled)
                            {
                                // Child Items
                                if (children.Count > 0)
                                {
                                    var stringChildItems = RenderItemsRecursive(children);
                                    valueProvider.Add(ContextKeys.Children, stringChildItems);
                                }
                                else
                                {
                                    valueProvider.Add(ContextKeys.Children, string.Empty);
                                }

                                // RootItemTemplate
                                sb.Append(Substituter.Substitute(rootItemNoChildrenEnabled && children.Count == 0 ? RootItemNoChildrenTemplate : RootItemTemplate, context));
                            }
                            else if (parentItemEnabled && isParent)
                            {
                                // ParentItemTemplate

                                // Child Items
                                var stringChildItems = RenderItemsRecursive(children);
                                valueProvider.Add(ContextKeys.Children, stringChildItems);

                                sb.Append(Substituter.Substitute(ParentItemTemplate, context));
                            }
                            else if (itemEnabled)
                            {
                                // ItemTemplate
                                sb.Append(Substituter.Substitute(ItemTemplate, context));
                            }
                        }
                        else
                        {
                            // AlternateItem enabled
                            // Odd

                            if (alternateParentItemEnabled && isParent)
                            {
                                // ParentItemTemplate

                                // Child Items
                                var stringChildItems = RenderItemsRecursive(children);
                                valueProvider.Add(ContextKeys.Children, stringChildItems);

                                sb.Append(Substituter.Substitute(AlternateParentItemTemplate, context));
                            }
                            else if (item.Parent == null && alternateRootItemEnabled)
                            {
                                // RootItemTemplate
                                sb.Append(Substituter.Substitute(AlternateRootItemTemplate, context));
                            }
                            else if (alternateEnabled)
                            {
                                // ItemTemplate
                                sb.Append(Substituter.Substitute(AlternateItemTemplate, context));
                            }
                        }
                    }
                    counter++;
                }
                return sb.ToString();
            };

            output.Append(RenderItemsRecursive(MenuItems));


            // Footer
            //if (!string.IsNullOrEmpty(Footer))
            //    output.Append(Footer);

            string upOneLevel = string.Empty;
            if (upOneLevelEnabled)
            {
                var rootItem = MenuItem.Provider.Get(RootItemId);
                if (rootItem != null)
                {
                    // Prepare Substituter parameters
                    var item = new MenuItemModel(null, rootItem);
                    var context = new Dictionary<string, INamedValueProvider>();

                    var provider = new NamedValueProvider();
                    provider.Add(TemplateKeys.Target, item.Target);
                    provider.Add(TemplateKeys.TargetProperty,
                        string.IsNullOrEmpty(item.Target) ? string.Empty : string.Format(@" target=""{0}""", item.Target));
                    provider.Add(TemplateKeys.Text, item.Text);
                    provider.Add(TemplateKeys.Url, item.Url);
                    context[Substituter.DefaultProviderKey] = provider;

                    upOneLevel = Substituter.Substitute(UpOneLevelTemplate, context);
                }
            }

            // Build "See also"
            string seeAlso = string.Empty;
            if (seeAlsoEnabled)
            {
                var sbSeeAlso = new StringBuilder();
                var context = new Dictionary<string, INamedValueProvider>();
                NamedValueProvider provider = null;
                foreach (var item in Children)
                {
                    // Prepare Substituter parameters
                    provider = new NamedValueProvider();
                    provider.Add(TemplateKeys.Target, item.Target);
                    provider.Add(TemplateKeys.TargetProperty,
                        string.IsNullOrEmpty(item.Target) ? string.Empty : string.Format(@" target=""{0}""", item.Target));
                    provider.Add(TemplateKeys.Text, item.Text);
                    provider.Add(TemplateKeys.Url, item.Url);

                    context[Substituter.DefaultProviderKey] = provider;
                    sbSeeAlso.Append(Substituter.Substitute(ItemTemplate, context));
                }

                // See also container
                provider = new NamedValueProvider();
                provider.Add(ContextKeys.Content, sbSeeAlso.ToString());

                context[Substituter.DefaultProviderKey] = provider;
                seeAlso = Substituter.Substitute(SeeAlsoTemplate, context);
            }

            // ContainerTemplate
            if (!string.IsNullOrEmpty(ContainerTemplate))
            {
                var provider = new NamedValueProvider();
                provider.Add(ContextKeys.Content, output.ToString());
                provider.Add(GenericMenuConstants.UpOneLevelKey, upOneLevel);
                provider.Add(GenericMenuConstants.SeeAlsoKey, seeAlso);

                var context = new Dictionary<string, INamedValueProvider>();
                context[Substituter.DefaultProviderKey] = provider;
                return Substituter.Substitute(ContainerTemplate, context);
            }

            return output.ToString();
        }
    }
}
