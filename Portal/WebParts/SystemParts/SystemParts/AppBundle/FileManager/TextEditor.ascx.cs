using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public partial class TextEditorPresenter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var query = new QueryParser(this);
                string src = query.GetSource();
                if (!string.IsNullOrEmpty(src))
                {
                    txtContent.Text = FileHelper.ReadFile(MapPath(src));
                }
            }
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            this.ReturnParentPage();
        }

        private void ReturnParentPage()
        {
            var query = new QueryParser(this);
            var returnUrl = query.GetDecode(QueryParser.SourceKey);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                query.Remove(QueryParser.SourceKey);
                query.Redirect(returnUrl);
            }
        }

        protected void cmdUpdate_Click(object sender, System.EventArgs e)
        {
            var query = new QueryParser(this);
            string src = query.GetSource();

            if (!string.IsNullOrEmpty(src))
            {
                FileHelper.WriteFile(txtContent.Text, WebHelper.MapPath(src));
            }

            this.ReturnParentPage();
        }
    }
}