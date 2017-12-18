using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class ShortUrlManager : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { }
        }

        public DataSet Select()
        {
            WPage page = null;
            string url;

            return DataHelper.ToDataSet(
                from i in WebShortUrl.Provider.GetList()
                select new
                {
                    i.Id,
                    i.Name,
                    i.PageId,
                    PageUrl = (url = i.EvalUrl),
                    PageName = (page = i.Page) != null ? page.Name : DataHelper.GetStringPreview(url, 30),
                }
            );
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var context = new WContext(this);
            int id = DataHelper.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    context.Set("Id", id);
                    context.SetOpen(WPID.ShortUrlEdit);
                    context.Redirect();
                    break;

                case "Custom_Delete":
                    if (id > 0)
                    {
                        WebShortUrl.Provider.Delete(id);
                        GridView1.DataBind();
                    }
                    break;
            }
        }

        protected void cmdCreate_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            context.Remove("Id");
            context.SetOpen(WPID.ShortUrlEdit);
            context.Redirect();
        }
    }
}