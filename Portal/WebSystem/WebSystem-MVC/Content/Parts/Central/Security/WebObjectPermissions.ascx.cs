using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.Security
{
    public partial class WebObjectPermissions : System.Web.UI.UserControl
    {
        private string sRoleID;

        protected void Page_Load(object sender, EventArgs e)
        {
            QueryParser qs = new QueryParser(this);
            sRoleID = qs[WebColumns.RoleId];

            if (!Page.IsPostBack)
            {
                //Role role = Role.FromID(new Guid(sRoleID));
                //divRole.InnerHtml += ": " + sRoleID;

                hidRecordId.Value = sRoleID;
                GridView1.DataBind();
            }
        }

        protected void cmdNew_Click(object sender, EventArgs e)
        {

        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM CMS.RolePermissions WHERE RolePermissionID IN (" + sChecked + ")");
                GridView1.DataBind();
                GridView2.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void cmdDone_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            query.Remove(WebColumns.RoleId);
            query.Redirect(CentralPages.WebRoles);
        }

        protected void cmdInsert_Click(object sender, EventArgs e)
        {
            string sChecked = Request.Form["chkChecked2"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                foreach (string sPermissionID in sChecked.Split(','))
                {
                    //RolePermission rp = new RolePermission();
                    //rp.PermissionID = int.Parse(sPermissionID);
                    //rp.RoleName = sRoleID;
                    //rp.Allow = true;
                    //rp.Update();
                }

                GridView1.DataBind();
                GridView2.DataBind();
            }
        }

        public DataSet Select()
        {
            // ObjectId
            // RecordId

            return DataHelper.ToDataSet(WebPermission.GetList());
        }
    }
}