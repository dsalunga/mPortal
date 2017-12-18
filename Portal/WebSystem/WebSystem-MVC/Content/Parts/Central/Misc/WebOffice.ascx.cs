using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Misc
{
    public partial class WebOfficeController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                QueryParser qs = new QueryParser(this);
                int id = qs.GetId(WebColumns.OfficeId);
                int parentId = qs.GetId(WebColumns.ParentId);

                if (id > 0)
                {
                    WebOffice item = WebOffice.Get(id);
                    if (item != null)
                    {
                        txtName.Text = item.Name;
                        txtAddressLine1.Text = item.AddressLine1;
                        txtPhoneNumber.Text = item.PhoneNumber;
                        txtMobileNumber.Text = item.MobileNumber;
                        txtEmailAddress.Text = item.EmailAddress;
                        txtContactPerson.Text = item.ContactPerson;

                        var parent = item.Parent;
                        if (parent != null)
                            txtParent.Text = parent.Id.ToString();
                    }
                }
                else if (parentId > 0)
                {
                    txtParent.Text = parentId.ToString();
                }

            }
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            this.Redirect();
        }

        private void Redirect()
        {
            QueryParser query = new QueryParser(this);
            int id = query.GetId(WebColumns.OfficeId);

            if (id > 0)
                query.Redirect(CentralPages.WebOfficeHome);
            else
                query.Redirect(CentralPages.WebOffices);
        }

        protected void cmdUpdate_Click(object sender, System.EventArgs e)
        {
            int id = DataHelper.GetId(Request, WebColumns.OfficeId);
            int parentId = DataHelper.GetId(txtParent.Text.Trim());

            WebOffice parent = WebOffice.Get(parentId); //txtParent.Text.Trim());

            WebOffice item = (id > 0) ? WebOffice.Get(id) : new WebOffice();
            item.Name = txtName.Text.Trim();
            item.AddressLine1 = txtAddressLine1.Text.Trim();
            item.PhoneNumber = txtPhoneNumber.Text.Trim();
            item.MobileNumber = txtMobileNumber.Text.Trim();
            item.EmailAddress = txtEmailAddress.Text.Trim();
            item.ContactPerson = txtContactPerson.Text.Trim();
            item.ParentId = parent == null ? -1 : parent.Id;
            item.Update();

            this.Redirect();
        }
    }
}