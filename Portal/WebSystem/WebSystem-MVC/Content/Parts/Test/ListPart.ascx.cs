using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Test
{
    public partial class ListPart : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdFindDetails_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var element = context.Element;

            var pair = element.LocatePair("Details");
            if (pair != null)
                context.Redirect(pair.Page.BuildRelativeUrl());
        }

        protected void cmdFindCategory_Click(object sender, EventArgs e)
        {
            WContext ctx = new WContext(this);
            var element = ctx.Element;

            var pair = element.LocatePair("Category");
            if (pair != null)
                ctx.Redirect(pair.Page.BuildRelativeUrl());
        }
    }
}