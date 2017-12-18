using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class PartDesignTemplate : UserControl
    {
        private string _containerID = "";
        private string _itemID = "";

        protected void Page_Load(object sender, EventArgs e) { }

        protected void imageDelete_Click(object sender, ImageClickEventArgs e)
        {
            int id = int.Parse(imageDelete.CommandArgument);

            // Just delete, it won't be shown anyway if it it's a Page
            WebPageElement.Delete(id);

            QueryParser query = new QueryParser(this);
            query.Redirect();
        }

        public void SetPermission(int permissionId)
        {
            switch (permissionId)
            {
                case Permissions.ManageContent:
                    DeleteCell.Visible = false;
                    break;
            }
        }

        public ImageButton ImageDelete { get { return imageDelete; } }

        public string ConfigureUrl
        {
            get { return linkConfigure.HRef; }
            set { linkConfigure.HRef = value; }
        }

        public string EditModeUrl
        {
            get { return linkEditMode.HRef; }
            set { linkEditMode.HRef = value; }
        }

        public HtmlTableCell LabelModuleName { get { return labelModuleName; } }

        // Added 29-Jan-2010
        public HtmlGenericControl ItemContainer { get { return itemContainer; } }
        public HtmlTableCell DeleteCell { get { return tdDelete; } }

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

        public void SetAsWebPage()
        {
            LabelModuleName.Attributes["class"] = "WebPageName";
        }

        public string Tooltip
        {
            set { panelDesignerHead.Attributes["title"] = value; }
        }

        public int OwnerType
        {
            set
            {
                switch (value)
                {
                    case WebObjects.WebPage:
                        panelDesignerHead.Attributes["class"] = "element-designer-head designer-element-page-owned";
                        break;

                    case WebObjects.WebMasterPage:
                        panelDesignerHead.Attributes["class"] = "element-designer-head designer-element-master-owned";
                        break;
                }
                
            }
        }
    }
}