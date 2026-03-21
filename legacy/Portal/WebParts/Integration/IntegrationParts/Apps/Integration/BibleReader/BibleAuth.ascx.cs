using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.BibleReader
{
    public partial class BibleAuth : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WContext context = new WContext(this);
                var element = context.Element;

                var bibleUrl = element.GetParameterValue(BibleReaderConstants.BIBLE_URL_KEY);
                if (!string.IsNullOrEmpty(bibleUrl))
                {
                    string returnUrl = element.GetParameterValue(BibleReaderConstants.RETURN_URL_KEY);
                    BibleUserSession userSession = new BibleUserSession(WSession.Current.UserId);

                    if (userSession.UserId > 0)
                    {
                        BibleUserSession.Add(userSession);

                        QueryParser query = new QueryParser(bibleUrl);
                        query.Set(BibleReaderConstants.SESSION_PARAM_KEY, userSession.ID);

                        if (!string.IsNullOrEmpty(returnUrl))
                            query.Set(BibleReaderConstants.RETURN_URL_KEY, returnUrl);
                        
                        query.Redirect();
                    }
                }
            }
        }
    }
}