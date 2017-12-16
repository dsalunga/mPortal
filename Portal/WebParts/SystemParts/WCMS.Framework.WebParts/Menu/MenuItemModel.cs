using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WCMS.Common;

namespace WCMS.WebSystem.WebParts.Menu
{
    public class MenuItemModel : INamedValueProvider
    {
        public MenuItemModel(MenuItemModel parent, MenuItem menuItem)
        {
            Children = new List<MenuItemModel>();
            Parent = parent;

            this.Text = menuItem.Text;
            this.Url = menuItem.PageUrl;
            this.Target = menuItem.Target;
            this.Rank = menuItem.Rank;
        }

        public string Text { get; set; }
        public string Url { get; set; }
        public int Rank { get; set; }
        public string Target { get; set; }
        public string Tooltip { get; set; }

        public MenuItemModel Parent { get; set; }
        public List<MenuItemModel> Children { get; set; }

        public bool IsRoot { get { return Parent == null; } }
        public bool IsParent { get { return Children.Count > 0; } }

        #region IValueProvider Members

        public string GetValue(string key)
        {
            switch (key)
            {
                case TemplateKeys.Target:
                    return Target;

                case TemplateKeys.Text:
                    return Text;

                case TemplateKeys.Url:
                    return Url;

                default:
                    return string.Empty;
            }
        }

        public bool ContainsKey(string key)
        {
            return true;
        }

        public string this[string key]
        {
            get { return GetValue(key); }
            set { throw new NotImplementedException(); }
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
