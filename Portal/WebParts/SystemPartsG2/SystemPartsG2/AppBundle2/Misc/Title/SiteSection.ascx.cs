using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Misc
{
    public partial class _Sections_LABEL_SiteSection : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);

            if (!string.IsNullOrEmpty(query["SS"]))
            {
                int iSS;

                if (int.TryParse(query["SS"], out iSS))
                {
                    object obj = SqlHelper.ExecuteScalar(CommandType.Text, "SELECT SiteSectionName FROM CMS.SiteSections WHERE SiteSectionID=@SiteSectionID",
                        new SqlParameter("@SiteSectionID", iSS)
                        );

                    if (obj != null)
                    {
                        lName.Text = obj.ToString();
                    }
                }
            }
        }
    }
}