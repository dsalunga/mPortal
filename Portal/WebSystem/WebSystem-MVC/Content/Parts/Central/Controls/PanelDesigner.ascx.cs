using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class PlaceHolderToolbox : UserControl
    {
        private string _containerID = "";
        private string _itemID = "";

        protected void Page_Load(object sender, EventArgs e) { }

        public void SetPermission(int permissionId)
        {
            switch (permissionId)
            {
                case Permissions.ManageContent:
                    panelNewElement.Visible = false;
                    panelPanelConfig.Visible = false;
                    break;

                case Permissions.ManageInstance:
                    break;
            }
        }

        public HtmlTableCell PanelName
        {
            get { return phName; }
        }

        public string NewElementUrl
        {
            get { return linkNewElement.HRef; }
            set { linkNewElement.HRef = value; }
        }

        public string ViewElementsUrl
        {
            get { return linkViewElements.HRef; }
            set { linkViewElements.HRef = value; }
        }

        public string PanelConfigUrl
        {
            get { return linkPanelConfig.HRef; }
            set { linkPanelConfig.HRef = value; }
        }

        public HtmlGenericControl PlaceHolder
        {
            get
            {
                return divPlaceHolder;
            }
        }

        public string ContainerID
        {
            get { return _containerID; }
            set { _containerID = value; }
        }

        public string ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        public void SetAsDefault()
        {
            PanelName.Attributes["class"] = "PlaceHolderNameDefault";
        }

        public int PanelUsageType
        {
            set
            {
                switch (value)
                {
                    case PanelUsage.Inherit: // MasterPage elements only
                        panelDesignerHead.Attributes["class"] = "panel-toolbox panel-inherit";
                        break;

                    case PanelUsage.Override: // Page elements only
                        panelDesignerHead.Attributes["class"] = "panel-toolbox panel-override";
                        break;

                    case PanelUsage.Add: // Combine
                        panelDesignerHead.Attributes["class"] = "panel-toolbox panel-add";
                        break;
                }
            }
        }

        public string Tooltip
        {
            set { panelDesignerHead.Attributes["title"] = value; }
        }
    }
}