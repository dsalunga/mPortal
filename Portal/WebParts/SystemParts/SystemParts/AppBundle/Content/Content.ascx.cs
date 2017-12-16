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
using WCMS.Framework.Diagnostics;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Content
{
    public partial class ContentView : WUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sw = PerformanceLog.StartLog();

            var context = new WContext(this);
            var contentText = ContentHelper.GetElementContent(context);
            if (!string.IsNullOrEmpty(contentText))
            {
                var literal = new Literal();
                literal.EnableViewState = false;
                literal.Text = contentText;
                this.Controls.Add(literal);
            }

            PerformanceLog.EndLog(string.Format("Content: {0}/{1}", context.ObjectId, context.RecordId), sw, context.PageId);
        }
    }
}