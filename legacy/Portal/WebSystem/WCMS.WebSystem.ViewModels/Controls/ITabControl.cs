using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WCMS.WebSystem.Controls
{
    /// <summary>
    /// Summary description for ITabControl
    /// </summary>
    public interface ITabControl
    {
        event EventHandler<TabEventArgs> SelectedTabChanged;

        void AddTab(string tabID, string tabText);
        void AddTab(string tabId, string tabText, bool causesValidation);
        void AddTab(string tabId, string tabText, bool causesValidation, string tag);
        void AddTab(string tabId, string tabText, string href);
        void AddTab(string tabId, string tabText, string href, string tag);

        int SelectedIndex { get; set; }
    }
}