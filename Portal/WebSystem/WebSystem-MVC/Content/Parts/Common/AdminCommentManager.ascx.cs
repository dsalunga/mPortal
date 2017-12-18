using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.WebParts.Common
{
    public partial class AdminCommentManager : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int pageId = DataHelper.GetId(Request, WebColumns.PageId);
                int elementId = DataHelper.GetId(Request, WebColumns.PageElementId);

                if (elementId > 0)
                {
                    hObjectId.Value = WebObjects.WebPageElement.ToString();
                    hRecordId.Value = elementId.ToString();
                }
                else if (pageId > 0)
                {
                    hObjectId.Value = WebObjects.WebPage.ToString();
                    hRecordId.Value = pageId.ToString();
                }
                else
                {
                    lblStatus.InnerHtml = "Invalid Object ID!";
                    GridView1.Visible = false;
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);
            QueryParser query = new QueryParser(this);

            switch (e.CommandName)
            {
                case "Custom_Delete":
                    var item = WebComment.Provider.Get(id);
                    if (item != null)
                        item.Delete();

                    GridView1.DataBind();
                    break;
            }
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GridView1.DataBind();
        }

        public DataSet Select(int objectId, int recordId, string keyword)
        {
            //var kwl = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();
            var items = WebComment.Provider.GetList(-2, objectId, recordId, -2);

            WebUser user = null;

            var result = from i in items
                         orderby i.DateCreated descending
                         select new
                         {
                             i.Id,
                             Content = DataHelper.GetStringPreview(i.Content),
                             i.DateCreated,
                             UserName = (user = i.User) != null ? string.Format("{0} ({1})", user.FirstAndLastName, user.UserName) : i.UserName,
                             UserEmail = user != null ? user.Email : i.UserEmail
                         };

            return DataHelper.ToDataSet(result);
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            var checkedValues = Request.Form.Get("chkChecked");
            if (!string.IsNullOrEmpty(checkedValues))
            {
                var ids = DataHelper.ParseCommaSeparatedIdList(checkedValues);
                if (ids.Count > 0)
                {
                    foreach (var id in ids)
                        WebComment.Provider.Delete(id);

                    GridView1.DataBind();
                }
            }
        }
    }
}