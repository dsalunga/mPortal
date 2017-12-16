using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Menu.Managers
{
    public class MenuPartDataManager : IPartDataManager
    {
        Dictionary<int, MenuEntity> _menus = new Dictionary<int, MenuEntity>();
        Dictionary<int, List<MenuItem>> _menuItems = new Dictionary<int, List<MenuItem>>();
        Dictionary<int, int> _menuMapping = new Dictionary<int, int>(); // old > new

        Dictionary<int, WebParameterSet> _sets = new Dictionary<int, WebParameterSet>();
        Dictionary<int, List<WebParameter>> _params = new Dictionary<int, List<WebParameter>>();
        Dictionary<int, int> _setMapping = new Dictionary<int, int>();

        public Dictionary<int, MenuEntity> GetMenus()
        {
            return _menus;
        }

        #region IPartDataManager Members

        public string ExportData()
        {
            if (_menus.Count > 0)
            {
                StringBuilder output = new StringBuilder();

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.NewLineOnAttributes = false;
                settings.Indent = false;
                settings.OmitXmlDeclaration = true;
                settings.Encoding = Encoding.Unicode;
                settings.ConformanceLevel = ConformanceLevel.Auto;

                XmlWriter writer = XmlWriter.Create(output, settings);

                // Menus
                writer.WriteStartElement("Menus");

                foreach (var menu in _menus.Values)
                {
                    writer.WriteStartElement("Menu");
                    writer.WriteRaw(DataHelper.ToXml(menu, "Item"));

                    var items = menu.Children; //_dataItems.FindAll(i => i.MenuId == menu.Id);
                    if (items.Count() > 0)
                    {
                        writer.WriteStartElement("MenuItems");

                        foreach (var menuItem in items)
                        {
                            var navigateUrl = menuItem.NavigateUrl;

                            var pageUrl = menuItem.PageUrl;
                            if (!string.IsNullOrEmpty(pageUrl))
                                menuItem.NavigateUrl = pageUrl;

                            writer.WriteRaw(DataHelper.ToXml(menuItem, "Item"));

                            menuItem.NavigateUrl = navigateUrl;
                        }

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                // ParameterSets
                if (_sets.Count > 0)
                {
                    writer.WriteStartElement("ParameterSets");

                    foreach (var parmSet in _sets.Values)
                    {
                        writer.WriteStartElement("ParameterSet");
                        writer.WriteRaw(DataHelper.ToXml(parmSet, "Item"));

                        var items = parmSet.Parameters; //_dataItems.FindAll(i => i.MenuId == menu.Id);
                        if (items.Count() > 0)
                        {
                            writer.WriteStartElement("Parameters");

                            foreach (var parameter in items)
                                writer.WriteRaw(DataHelper.ToXml(parameter, "Item"));

                            writer.WriteEndElement();
                        }

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }

                writer.Flush();

                return output.ToString();
            }

            return string.Empty;
        }

        public string ExportElementData(IWebObject element, bool exportData = true)
        {
            var menuObject = MenuObject.Provider.Get(element.OBJECT_ID, element.Id);
            if (menuObject != null)
            {
                if (exportData)
                {
                    if (!_menus.ContainsKey(menuObject.MenuId))
                    {
                        var menu = menuObject.GetMenu();
                        if (menu != null)
                            _menus.Add(menu.Id, menu);
                    }

                    if (menuObject.ParameterSetId > 0 && !_sets.ContainsKey(menuObject.ParameterSetId))
                    {
                        var set = WebParameterSet.Provider.Get(menuObject.ParameterSetId);
                        if (set != null)
                            _sets.Add(set.Id, set);
                    }
                }

                return DataHelper.ToXml<MenuObject>(menuObject, "Item");
            }

            return string.Empty;
        }

        // A one-shot persistence of all data
        public bool ImportData(WSite site)
        {
            if (_menus.Count > 0)
            {
                var currMenus = MenuEntity.Provider.GetList();
                foreach (var m in _menus.Values)
                {
                    var menu = m;
                    var oldId = menu.Id;

                    var currMenu = currMenus.FirstOrDefault(i => i.Name.Equals(menu.Name, StringComparison.InvariantCultureIgnoreCase));
                    if (currMenu == null)
                    {
                        // Menu
                        menu.Id = -1;

                        if (site != null)
                            menu.SiteId = site.Id;

                        menu.Update();
                    }
                    else
                    {
                        menu = currMenu;
                    }

                    // MenuItems
                    var items = _menuItems.ContainsKey(oldId) ? _menuItems[oldId] : new List<MenuItem>();
                    if (items.Count > 0)
                    {
                        Dictionary<int, int> itemMapping = new Dictionary<int, int>();

                        foreach (var menuItem in items)
                        {
                            var oldItemId = menuItem.Id;

                            menuItem.Id = -1;
                            menuItem.MenuId = menu.Id;

                            if (!string.IsNullOrEmpty(menuItem.NavigateUrl))
                            {
                                var page = WPage.Resolve(menuItem.NavigateUrl);
                                if (page != null)
                                {
                                    menuItem.PageId = page.Id;

                                    var pageUrl = page.BuildRelativeUrl();
                                    if (pageUrl.Equals(menuItem.NavigateUrl, StringComparison.InvariantCultureIgnoreCase))
                                        menuItem.NavigateUrl = string.Empty;
                                }
                            }

                            menuItem.Update();

                            itemMapping.Add(oldItemId, menuItem.Id);
                        }

                        // Update MenuItem mapping
                        foreach (var menuItem in items)
                        {
                            if (itemMapping.ContainsKey(menuItem.ParentId))
                            {
                                menuItem.ParentId = itemMapping[menuItem.ParentId];
                                menuItem.Update();
                            }
                        }
                    }

                    if (!_menuMapping.ContainsKey(oldId))
                        _menuMapping.Add(oldId, menu.Id);
                }
            }

            if (_sets.Count > 0)
            {
                foreach (var parmSet in _sets.Values)
                {
                    var oldId = parmSet.Id;

                    // Menu
                    parmSet.Id = -1;
                    parmSet.Update();

                    // MenuItems
                    var parms = _params.ContainsKey(oldId) ? _params[oldId] : new List<WebParameter>();
                    if (parms.Count > 0)
                    {
                        foreach (var parm in parms)
                        {
                            var oldItemId = parm.Id;

                            parm.Id = -1;
                            parm.RecordId = parmSet.Id;
                            parm.Update();
                        }
                    }

                    if (!_setMapping.ContainsKey(oldId))
                        _setMapping.Add(oldId, parmSet.Id);
                }
            }

            return true;
        }

        public bool ImportElementData(IWebObject element, XmlNode elementDataNode)
        {
            var itemNode = elementDataNode.SelectSingleNode("Item");
            if (itemNode != null)
            {
                var item = DataHelper.FromElementXml<MenuObject>(itemNode.OuterXml, "Item");

                item.Id = -1;
                item.ObjectId = element.OBJECT_ID;
                item.RecordId = element.Id;

                if (_menuMapping.ContainsKey(item.MenuId))
                    item.MenuId = _menuMapping[item.MenuId];

                if (_setMapping.ContainsKey(item.ParameterSetId))
                    item.ParameterSetId = _setMapping[item.ParameterSetId];

                item.Update();

                return true;
            }

            return false;
        }

        private bool inited = false;
        public void InitImport(XmlNode dataNode)
        {
            if (!inited)
            {
                if (dataNode != null)
                {
                    // Menus
                    _menus = new Dictionary<int, MenuEntity>();
                    _menuItems = new Dictionary<int, List<MenuItem>>();

                    XmlNodeList itemNodes = dataNode.SelectNodes("Menus/Menu");
                    if (itemNodes.Count > 0)
                    {
                        foreach (XmlNode itemNode in itemNodes)
                        {
                            var item = DataHelper.FromElementXml<MenuEntity>(itemNode.OuterXml, "Item");
                            if (item != null)
                                _menus.Add(item.Id, item);

                            // MenuItems
                            var menuItemNodes = itemNode.SelectNodes("MenuItems/Item");
                            if (menuItemNodes.Count > 0)
                            {
                                List<MenuItem> menuItems = new List<MenuItem>();
                                foreach (XmlNode menuItemNode in menuItemNodes)
                                {
                                    var menuItem = DataHelper.FromElementXml<MenuItem>(menuItemNode.OuterXml, "Item");
                                    if (menuItem != null)
                                        menuItems.Add(menuItem);
                                }

                                _menuItems.Add(item.Id, menuItems);
                            }
                        }
                    }

                    // ParameterSets

                    _sets = new Dictionary<int, WebParameterSet>();
                    _params = new Dictionary<int, List<WebParameter>>();

                    itemNodes = dataNode.SelectNodes("ParameterSets/ParameterSet");
                    if (itemNodes.Count > 0)
                    {
                        foreach (XmlNode itemNode in itemNodes)
                        {
                            var item = DataHelper.FromElementXml<WebParameterSet>(itemNode.OuterXml, "Item");
                            if (item != null)
                                _sets.Add(item.Id, item);

                            // MenuItems
                            var parmsNodes = itemNode.SelectNodes("Parameters");
                            if (parmsNodes.Count > 0)
                            {
                                List<WebParameter> parms = new List<WebParameter>();
                                foreach (XmlNode parmNode in parmsNodes)
                                {
                                    var parm = DataHelper.FromElementXml<WebParameter>(parmNode.OuterXml, "Item");
                                    if (parm != null)
                                        parms.Add(parm);
                                }

                                _params.Add(item.Id, parms);
                            }
                        }
                    }
                }

                inited = true;
            }
        }

        public bool PerformDataCleanUp()
        {
            return false;
        }

        public void DeleteElementData(IWebObject element)
        {
            var menuObject = MenuObject.Provider.Get(element.OBJECT_ID, element.Id);
            if (menuObject != null)
                menuObject.Delete();
        }

        #endregion
    }
}
