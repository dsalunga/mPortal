using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Misc
{
    public partial class SubscriptionManager : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cmdBrowse.OnClientClick = "BrowseLink('" + txtNavigateURL.ClientID + "',GetComboValue(WCMS.Dom.Get('" + cboWebParts.ClientID + "'))); return false;";
            }
        }

        protected void cmdAddFull_Click(object sender, EventArgs e)
        {
            string pageUrl = txtNavigateURL.Text.Trim();
            int partId = DataHelper.GetId(cboWebParts.SelectedValue);
            int groupId = DataHelper.GetId(Request, WebColumns.GroupId);

            if (!string.IsNullOrEmpty(pageUrl) && groupId > 0 && partId > 0)
            {
                var page = WebRewriter.ResolvePage(pageUrl);
                if (page != null)
                {
                    var item = new WebSubscription();
                    item.ObjectId = WebObjects.WebGroup;
                    item.RecordId = groupId;
                    item.PageId = page.Id;
                    item.PartId = partId;
                    item.Allow = 1;
                    item.Update();

                    txtNavigateURL.Text = string.Empty;
                    GridView1.DataBind();
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            string selected = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(selected))
            {
                var ids = DataHelper.ParseCommaSeparatedIdList(selected);
                foreach (var id in ids)
                {
                    WebSubscription.Provider.Delete(id);
                }

                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case WConstants.CustomDeleteCmd:
                    int id = DataHelper.GetId(e.CommandArgument);
                    if (id > 0)
                    {
                        WebSubscription.Provider.Delete(id);
                        GridView1.DataBind();
                    }
                    break;
            }
        }

        public DataSet Select(int groupId, int partId)
        {
            WPage page = null;
            var items = WebSubscription.Provider.GetList(WebObjects.WebGroup, groupId, partId, -1, -1);
            var subItems = from i in items
                           select new
                           {
                               i.Id,
                               GroupName = WebGroup.Get(i.RecordId).Name,
                               PageUrl = (page = WPage.Get(i.PageId)) != null ? page.BuildRelativeUrl() : "",
                               PartName = WPart.Get(i.PartId).Name
                           };

            return DataHelper.ToDataSet(subItems);
        }

        public IEnumerable<WPart> SelectPart()
        {
            return WPart.GetList();
        }

        protected void cboWebParts_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }
    }
}