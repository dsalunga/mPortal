using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using WCMS.Common.Utilities;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Menu
{
    public partial class ConfigLinkedMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Display Page Url
                int pageId = DataHelper.GetId(Request, WebColumns.PageId);
                WPage page = null;
                if (pageId > 0 && (page = WPage.Get(pageId)) != null)
                    lblHeader.InnerHtml = string.Format("{0} {1} Menu Items", page.Name, WConstants.Arrow);
            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            WContext context = new WContext(this);
            context.SetLoadAndRedirect("ConfigLinkedMenuEdit.ascx");
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Action_Edit":
                    WContext context = new WContext(this);
                    context.Set("MenuItemId", id);
                    context.SetLoadAndRedirect("ConfigLinkedMenuEdit.ascx");
                    break;

                case "Action_Delete":
                    MenuItem.Provider.Delete(id);
                    GridView1.DataBind();
                    break;
            }
        }

        public DataSet Select(int pageId)
        {
            MenuEntity menu = null;

            return DataHelper.ToDataSet(
                from item in MenuItem.GetListByPageId(pageId)
                select new
                {
                    item.Id,
                    Text = BuildMenuPath(item),
                    IsActive = item.Active,
                    MenuName = (menu = item.Menu) != null ? menu.Name : string.Empty
                }
            );
        }

        private string BuildMenuPath(MenuItem item)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("<a href=\"{1}\" target=\"_blank\" alt=\"{0}\"><strong>{0}</strong></a>", item.Text, item.PageUrl);
            item = item.Parent;

            while (item != null)
            {
                sb.Insert(0, string.Format("<a href=\"{1}\" target=\"_blank\" alt=\"{0}\">{0}</a> {2} ", item.Text, item.PageUrl, WConstants.Arrow));
                item = item.Parent;
            }

            return sb.ToString();
        }
    }
}