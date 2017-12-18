using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.IO;

using System.Collections.Generic;

namespace WCMS.WebSystem.Controls
{
    public partial class TabControl : UserControl, ITabControl
    {
        public bool HasTabs { get { return Tabs.Count > 0; } }

        // Click Event:
        // - A click on any LinkButton (postback tabs) will invoke the OnClick
        // - OnClick will invoke this event
        public event EventHandler<TabEventArgs> SelectedTabChanged;

        public Dictionary<string, TabElement> Tabs { get; set; }

        public TabControl()
        {
            Tabs = new Dictionary<string, TabElement>();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Page.PreLoad += new EventHandler(Page_PreLoad);
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            // View state loaded. Request instance ready.
            string tabsKey = "Tabs_" + this.UniqueID;
            string selectedKey = "SelectedIndex_" + this.UniqueID;

            if (ViewState[tabsKey] != null)
                Tabs = ViewState[tabsKey] as Dictionary<string, TabElement>;

            if (ViewState[selectedKey] != null)
                _selectedIndex = (int)ViewState[selectedKey];

            for (int i = 0; i < Tabs.Count; i++)
            {
                var tab = Tabs.ElementAt(i).Value;
                AddTabInternal(tab, i);
            }

            // When set from view state
            SelectTab(SelectedIndex);
        }

        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Save the view state here otherwise everything will be ignored

            int selectedIndex = _selectedIndex;
            SelectedIndex = selectedIndex == -1 ? 0 : selectedIndex;

            this.SelectedTabChanged += new EventHandler<TabEventArgs>(OnClick);
            ViewState["Tabs_" + this.UniqueID] = Tabs;
            ViewState["SelectedIndex_" + this.UniqueID] = _selectedIndex;

            // When set by host page
            SelectTab(SelectedIndex);
        }

        protected virtual void OnClick(object sender, TabEventArgs e)
        {
            // Fire the Click event internally
        }

        #region AddTab Methods

        public void AddTab(string tabId, string tabText)
        {
            AddTab(tabId, tabText, false, null, string.Empty);
        }

        public void AddTab(string tabId, string tabText, bool causesValidation)
        {
            this.AddTab(tabId, tabText, causesValidation, null, string.Empty);
        }

        public void AddTab(string tabId, string tabText, bool causesValidation, string tag)
        {
            this.AddTab(tabId, tabText, causesValidation, null, tag);
        }

        public void AddTab(string tabId, string tabText, string href)
        {
            this.AddTab(tabId, tabText, false, href, string.Empty);
        }

        public void AddTab(string tabId, string tabText, string href, string tag)
        {
            this.AddTab(tabId, tabText, false, href, tag);
        }

        public void AddTab(string tabId, string tabText, string href, string tag = "", string target = "")
        {
            this.AddTab(tabId, tabText, false, href, tag, target);
        }

        public void AddTab(string tabId, string tabText, bool causesValidation, string href, string tag, string target = "")
        {
            if (!Tabs.ContainsKey(tabId))
            {
                var tab = new TabElement
                {
                    Name = tabId,
                    Text = tabText,
                    CausesValidation = causesValidation,
                    IsSelected = false,
                    Url = href,
                    Tag = tag,
                    Target = target
                };

                Tabs.Add(tabId, tab);
                AddTabInternal(tab, Tabs.Count - 1);
            }
        }

        #endregion

        public void SelectTab(string tag)
        {
            foreach (var tab in Tabs.Values)
            {
                if (tab.Tag.Equals(tag, StringComparison.InvariantCultureIgnoreCase))
                {
                    int index = Array.IndexOf<TabElement>(Tabs.Values.ToArray(), tab);
                    SelectedIndex = index;
                    break;
                }
            }
        }

        public void SelectTabByName(string name)
        {
            foreach (var tab in Tabs.Values)
            {
                if (tab.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    int index = Array.IndexOf<TabElement>(Tabs.Values.ToArray(), tab);
                    SelectedIndex = index;
                    break;
                }
            }
        }

        private void AddTabInternal(TabElement tab, int index)
        {
            string tabTextFormatted = tab.Text.Replace(" ", "&nbsp;");

            var li = new HtmlGenericControl("li");
            //var div = new HtmlGenericControl("div");
            //var tdSpacer = new HtmlTableCell();
            //div.Attributes["class"] = "TabButtonBlur";
            li.Attributes["data-tabindex"] = index.ToString();
            li.Attributes["role"] = "presentation";
            li.Attributes["data-tabname"] = tab.Name; //string.Format("{0}", tab.Name);
            //li.ClientIDMode = ClientIDMode.Static;
            li.ID = "Tab" + index;

            //td.Controls.Add(div);

            if (!string.IsNullOrEmpty(tab.Url))
            {
                var link = new HtmlAnchor();
                link.InnerHtml = tabTextFormatted;
                link.Title = HttpUtility.HtmlDecode(tab.Text);
                link.CausesValidation = tab.CausesValidation;
                link.HRef = tab.Url;
                link.Attributes["role"] = "tab";
                //link.Attributes["data-toggle"] = "tab";
                //lb.Click += new EventHandler(TabButton_Click);
                link.ID = tab.Name;

                if (!string.IsNullOrEmpty(tab.Target))
                    link.Target = tab.Target;

                li.Controls.Add(link);
            }
            else
            {
                var link = new LinkButton();
                link.Text = tabTextFormatted;
                link.ToolTip = HttpUtility.HtmlDecode(tab.Text);
                link.CausesValidation = tab.CausesValidation;
                link.Attributes["role"] = "tab";
                //link.Attributes["data-toggle"] = "tab";
                link.Click += new EventHandler(TabButton_Click); // Bind click event
                link.ID = tab.Name;
                li.Controls.Add(link);
            }

            //tdSpacer.Style["width"] = "3px";
            //tdSpacer.InnerHtml = "&nbsp;";
            //tdSpacer.Attributes["nowrap"] = "nowrap";

            //panelTab.Cells.Insert(panelTab.Cells.Count - 1, td);
            panelTab.Controls.Add(li);
            //panelTab.Cells.Insert(panelTab.Cells.Count - 1, tdSpacer);
        }

        private void TabButton_Click(object sender, EventArgs e)
        {
            var link = (LinkButton)sender;
            var li = link.Parent as HtmlGenericControl;
            SelectedIndex = Convert.ToInt32(li.Attributes["data-tabindex"]);

            // Fire the selected event
            var tabArgs = new TabEventArgs();
            tabArgs.TabName = link.Attributes["data-tabname"];

            if (SelectedTabChanged != null)
                this.SelectedTabChanged(sender, tabArgs);
        }

        private int _selectedIndex = 0;

        /// <summary>
        /// NOTE: This does not invoke the SelectedTabChanged event. Use SelectedTab instead.
        /// </summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (value != _selectedIndex)
                {
                    var tab = SelectTab(value);
                    _selectedIndex = value;

                    // Fire the selected event
                    if (tab != null)
                    {
                        var tabArgs = new TabEventArgs();
                        tabArgs.TabName = tab.Name;
                        if (SelectedTabChanged != null)
                            this.SelectedTabChanged(null, tabArgs);
                    }
                }
            }
        }

        public string ThemeName
        {
            set { } //panelTabPanel.Attributes["class"] = "tab-control " + value; }
        }

        public string CssClass
        {
            set { panelTabPanel.Attributes["class"] = "portal-tabpanel " + value; }
        }

        public string SelectedTab
        {
            get { return Tabs.ElementAt(SelectedIndex).Value.Name; }
            set
            {
                //string tabName = "";
                if (Tabs.ContainsKey(value))
                {
                    var tab = Tabs[value];
                    int index = Array.IndexOf<TabElement>(Tabs.Values.ToArray(), tab);
                    //tabName = tab.Name;
                    SelectedIndex = index;
                }
                else
                {
                    SelectedIndex = -1;
                }
            }
        }

        private TabElement SelectTab(int tabIndex)
        {
            int selected = _selectedIndex;
            var count = panelTab.Controls.Count;

            HtmlGenericControl li = null;
            if (selected > -1 && count > 0)
            {
                li = panelTab.Controls[selected] as HtmlGenericControl; // [selected * 2].Controls[0] as HtmlGenericControl;
                if (li != null)
                    li.Attributes["class"] = "";
            }

            if (count > tabIndex)
            {
                li = panelTab.Controls[tabIndex] as HtmlGenericControl;
                if (li != null)
                    li.Attributes["class"] = "active";
            }

            TabElement tab = null;
            // Set selected
            if (Tabs.Count > tabIndex)
            {
                tab = Tabs.Values.ElementAt(tabIndex);
                tab.IsSelected = true;
            }

            if (Tabs.Count > selected)
                Tabs.Values.ElementAt(selected).IsSelected = false;
            return tab;
        }
    }
}