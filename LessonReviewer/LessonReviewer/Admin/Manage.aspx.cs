using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

using WCMS.Common.Utilities;

using WCMS.LessonReviewer.Core;

namespace WCMS.LessonReviewer.Admin
{
    public partial class Manage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(MapPath("~/App_Data/Config.xml"));

                chkIntranetMode.Checked = DataUtil.GetBool(GetConfigValue(xdoc, "IntranetMode"), false);
                chkAutoBypassUrl.Checked = DataUtil.GetBool(GetConfigValue(xdoc, "EnableAutoByPassUrl"), true);

                txtAdminUserName.Text = GetConfigValue(xdoc, "AdminUserName");
                txtAdminPassword.Attributes["value"] = GetConfigValue(xdoc, "AdminPassword");
                txtPortalAjaxHandlerUrl.Text = GetConfigValue(xdoc, "PortalAjaxHandlerUrl");
                txtPortalMakeUpHomeUrl.Text = GetConfigValue(xdoc, "PortalMakeUpHomeUrl");
                txtBaseFolder.Text = GetConfigValue(xdoc, "MCGI.MakeUp.BaseFolder");
                txtBaseHttp.Text = GetConfigValue(xdoc, "MCGI.MakeUp.BaseHttp");
            }
        }

        private string GetConfigValue(XmlDocument xdoc, string key)
        {
            return XmlUtil.GetValue(xdoc.SelectSingleNode(string.Format("//Add[@Key='{0}']", key)), "Value");
        }

        private void SetNodeValue(XmlDocument xdoc, string key, string value)
        {
            XmlNode node = xdoc.SelectSingleNode(string.Format("//Add[@Key='{0}']", key));
            node.Attributes["Value"].Value = value;
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            Session[MakeUpServiceSession.SessionKey] = null;
            Response.Redirect("~/Admin/Login.aspx", true);
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(MapPath("~/App_Data/Config.xml"));

            SetNodeValue(xdoc, "IntranetMode", chkIntranetMode.Checked ? "true" : "false");
            SetNodeValue(xdoc, "EnableAutoByPassUrl", chkAutoBypassUrl.Checked ? "true" : "false");
            SetNodeValue(xdoc, "AdminUserName", txtAdminUserName.Text.Trim());
            SetNodeValue(xdoc, "AdminPassword", txtAdminPassword.Text);
            SetNodeValue(xdoc, "PortalAjaxHandlerUrl", txtPortalAjaxHandlerUrl.Text.Trim());
            SetNodeValue(xdoc, "PortalMakeUpHomeUrl", txtPortalMakeUpHomeUrl.Text.Trim());
            SetNodeValue(xdoc, "MCGI.MakeUp.BaseFolder", txtBaseFolder.Text.Trim());
            SetNodeValue(xdoc, "MCGI.MakeUp.BaseHttp", txtBaseHttp.Text.Trim());

            xdoc.Save(MapPath("~/App_Data/Config.xml"));

            lblMsg.Text = "Update successful!";
        }
    }
}