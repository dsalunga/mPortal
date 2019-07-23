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
    public partial class ConfigExternalMembers : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string keyword = DataUtil.Get(Request, "Keyword");
                if (!string.IsNullOrEmpty(keyword))
                {
                    txtSearch.Text = keyword;
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataUtil.GetId(e.CommandArgument);
            var query = new WQuery(this);
            string keyword = txtSearch.Text;

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set(WebColumns.Id, id);
                    if (!string.IsNullOrEmpty(keyword))
                        query.Set("Keyword", keyword);

                    query.LoadAndRedirect("ConfigExternalMember.ascx");
                    break;

                case "Custom_Delete":
                    Member.Provider.Delete(id);
                    GridView1.DataBind();
                    break;
            }
        }

        public IEnumerable<Member> Select(string sortBy, int startRowIndex, int maximumRows, string keyword)
        {
            var items = Select(keyword);
            return DataUtil.PageWithSort(items, sortBy, startRowIndex, maximumRows);
        }

        private IEnumerable<Member> Select(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return Member.RemoteProvider.GetList();
            else
                return Member.RemoteProvider.GetList(keyword);
        }

        public int SelectCount(string keyword)
        {
            return Select(keyword).Count();
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

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GridView1.DataBind();
        }
    }
}