using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;

namespace WCMS.WebSystem.Apps.Integration
{
    public partial class ConfigMembers : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataUtil.GetId(e.CommandArgument);
            QueryParser qs = new QueryParser(this);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    qs.Set(WebColumns.Id, id);
                    //qs.Redirect(CentralPages.WebUserHome);
                    break;

                case "Custom_Delete":
                    Member.Provider.Delete(id);
                    GridView1.DataBind();
                    break;
            }
        }

        public DataSet Select()
        {
            return DataUtil.ToDataSet(Member.Provider.GetList());
        }

        protected void cmdSync_Click(object sender, EventArgs e)
        {
            //ExternalMemberWS.MemberSoapClient client = new ExternalMemberWS.MemberSoapClient();
            //var items = ExternalMemberWS.Member.Provider.GetList();

            //foreach (var item in items)
            //{
            //    var wsMember = client.GetProfile(item.ExternalIDNo, item.date
            //}
        }
    }
}