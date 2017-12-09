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