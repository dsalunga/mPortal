using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WCMS.WebSystem.Windows
{
    public partial class _CMS_Utils_StyleBuilder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MultiView1.SetActiveView(viewFont);
            }
        }
        protected void cmdBackground_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(viewBackground);
        }
        protected void cmdText_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(viewText);
        }
        protected void cmdEdges_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(viewEdges);
        }
        protected void cmdOther_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(viewOther);
        }
        protected void cmdFont_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(viewFont);
        }
    }
}