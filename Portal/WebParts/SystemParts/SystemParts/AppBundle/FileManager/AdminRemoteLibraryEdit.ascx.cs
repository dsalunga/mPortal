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

using WCMS.WebSystem.WebParts.RemoteIndexer;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public partial class AdminRemoteLibraryEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                foreach (var sourceType in RemoteSourceTypes.Values)
                    cboSourceType.Items.Add(new ListItem(sourceType.Value, sourceType.Key.ToString()));

                var id = DataHelper.GetId(Request, "LibraryId");
                if (id > 0)
                {
                    var item = RemoteLibrary.Provider.Get(id);
                    if (item != null)
                    {
                        txtName.Text = item.Name;
                        txtBaseAddress.Text = item.BaseAddress;
                        txtUserName.Text = item.UserName;
                        //txtPassword.Text = item.Password;
                        txtDisplayBaseAddress.Text = item.DisplayBaseAddress;
                        chkActive.Checked = item.Active == 1;

                        txtPassword.Attributes["value"] = item.Password;

                        WebHelper.SetCboValue(cboSourceType, item.SourceTypeId);
                    }
                }
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.ReturnPage();
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            RemoteLibrary item = null;
            int id = DataHelper.GetId(Request, "LibraryId");
            if (id > 0 && (item = RemoteLibrary.Provider.Get(id)) != null) { }
            else
            {
                /* INSERT */
                item = new RemoteLibrary();
            }

            item.Name = txtName.Text.Trim();
            item.BaseAddress = txtBaseAddress.Text.Trim();
            item.UserName = txtUserName.Text.Trim();
            item.Password = txtPassword.Text;
            item.DisplayBaseAddress = txtDisplayBaseAddress.Text.Trim();
            item.Active = chkActive.Checked ? 1 : 0;
            item.SourceTypeId = DataHelper.GetInt32(cboSourceType.SelectedValue);
            item.Update();

            this.ReturnPage();
        }

        private void ReturnPage()
        {
            var query = new WQuery(this);
            query.Remove("LibraryId");
            query.Remove(WConstants.Load);
            query.Redirect();
        }
    }
}