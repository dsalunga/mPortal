using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList
{
    public partial class GroupManager : System.Web.UI.UserControl
    {
        public bool IsManager { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblStatus.Visible = false;

            if (!Page.IsPostBack)
            {
                WebGroup group = null;

                var context = new WContext(this);
                var element = context.Element;
                var set = element.GetParameterSet();
                var parentId = context.GetId(WebColumns.ParentId);
                var groupId = parentId;

                var defaultParentName = ParameterizedWebObject.GetValue("ParentGroup", element, set);
                if (!string.IsNullOrEmpty(defaultParentName))
                {
                    group = WebGroup.SelectNode(defaultParentName);
                    if (group != null)
                    {
                        ObjectDataSource1.SelectParameters["parentId"].DefaultValue = group.Id.ToString();
                        WebGroupTab1.RootGroupId = group.Id;

                        if (groupId == -1)
                            groupId = group.Id;
                    }
                }

                hUserProfileUrl.Value = ParameterizedWebObject.GetValue("UserProfileUrl", element, set);
                hGroupId.Value = groupId.ToString();

                if (group == null && groupId > 0)
                    group = WebGroup.Get(groupId);

                if (group != null)
                {
                    IsManager = group.IsManager(WSession.Current.User, true);
                    hIsManager.Value = IsManager ? "1" : "0";

                    if (!IsManager)
                        GridView1.Columns[0].Visible = false;
                }

                context.SetOpen("GroupEdit");
                if (parentId == -1)
                    context.Set(WebColumns.ParentId, groupId);
                linkNewGroup.HRef = context.BuildQuery();

                GridView1.DataBind();
            }
            else
            {
                IsManager = hIsManager.Value == "1";
            }
        }

        private void DisplayMessage(string message)
        {
            lblStatus.Visible = true;
            lblStatus.InnerHtml = message;
        }

        public DataSet Get(int parentId)
        {
            WebUser owner = null;
            var isAdmin = WSession.Current.IsAdministrator;

            //WebUser user = null;
            var items = from i in WebGroup.Provider.GetList(parentId)
                        where (isAdmin || (i.Id != SystemGroups.ADMINS_GROUP_ID))
                        select new
                        {
                            i.Id,
                            i.Name,
                            Owner = (owner = i.Owner) != null ? owner.FirstAndLastName : string.Empty,
                            Active = -1,
                            i.OwnerId,
                            i.DateModified
                        };

            return DataUtil.ToDataSet(items);
        }

        protected void cmdRemove_Click(object sender, EventArgs e)
        {
            var selected = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(selected))
            {
                var groupId = DataUtil.GetInt32(hGroupId.Value);
                var ids = DataUtil.ParseCommaSeparatedIdList(selected);
                foreach (var id in ids)
                {
                    var g = WebGroup.Get(id);
                    if (g != null)
                        g.Delete();
                }

                GridView1.DataBind();
            }
        }
    }
}