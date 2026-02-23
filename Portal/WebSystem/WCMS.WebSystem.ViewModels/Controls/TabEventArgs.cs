using System;

namespace WCMS.WebSystem.Controls
{
    /// <summary>
    /// Summary description for TabEventArgs
    /// </summary>
    public class TabEventArgs : EventArgs
    {
        public TabEventArgs() { }

        private string _tabName;

        public string TabName
        {
            get { return _tabName; }
            set { _tabName = value; }
        }

    }
}