using System;

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