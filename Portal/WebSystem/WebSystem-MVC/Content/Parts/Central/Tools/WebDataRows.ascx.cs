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

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class WebDataRows : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        public DataSet Select(int id)
        {
            if (id > 0)
            {
                try
                {
                    var item = WebObject.Get(id);
                    if (item != null)
                        return SqlHelper.ExecuteDataSet(CommandType.Text, string.Format("SELECT * FROM {0}", item.Name));
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(ex);
                }
            }

            return DataHelper.GetEmptyDataSet();
        }
    }
}