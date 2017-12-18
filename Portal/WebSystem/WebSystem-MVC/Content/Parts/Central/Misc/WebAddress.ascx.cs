using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Core.Shared;

namespace WCMS.WebSystem.WebParts.Central.Misc
{
    public partial class WebAddressController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cboCountries.DataSource = Country.GetList();
                cboCountries.DataBind();

                var id = DataHelper.GetId(Request, "Id");
                if (id > 0)
                {
                    WebAddress item = WebAddress.Provider.Get(id);
                    if (item != null)
                    {
                        WebHelper.AspNetAjaxComboBoxSelectText(cboTag, item.Tag);

                        txtAddressLine1.Text = item.AddressLine1;
                        txtAddressLine2.Text = item.AddressLine2;
                        txtCityTown.Text = item.CityTown;
                        cboCountries.SelectedValue = item.CountryCode.ToString();
                        txtZipCode.Text = item.ZipCode;
                        txtPhoneNumber.Text = item.PhoneNumber;

                        BindStateProvince(item.StateProvinceCode, item.StateProvince);
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
            var id = DataHelper.GetId(Request, WebColumns.Id);
            var userId = DataHelper.GetId(Request, WebColumns.UserId);
            var item = id > 0 ? WebAddress.Provider.Get(id) :
                new WebAddress { ObjectId = WebObjects.WebUser, RecordId = userId };

            item.Tag = cboTag.Text;
            item.AddressLine1 = txtAddressLine1.Text.Trim();
            item.AddressLine2 = txtAddressLine2.Text.Trim();
            item.CityTown = txtCityTown.Text.Trim();

            var stateProvinceCode = DataHelper.GetId(cboStateProvince.SelectedValue);
            if (stateProvinceCode > 0)
            {
                item.StateProvinceCode = stateProvinceCode;
                item.StateProvince = string.Empty;
            }
            else
            {
                item.StateProvinceCode = -1;
                item.StateProvince = cboStateProvince.Text;
            }

            item.CountryCode = DataHelper.GetId(cboCountries.SelectedValue);
            item.ZipCode = txtZipCode.Text.Trim();
            item.PhoneNumber = txtPhoneNumber.Text.Trim();
            item.Update();

            this.ReturnPage();
        }

        private void ReturnPage()
        {
            WContext query = new WContext(this);
            query.Remove(WConstants.Open);
            query.Remove(WebColumns.Id);
            query.Redirect();
        }

        protected void cboCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindStateProvince();
        }

        private void BindStateProvince(int stateProvinceCode = -1, string stateProvince = "")
        {
            cboStateProvince.Items.Clear();
            cboStateProvince.ClearSelection();

            int countryCode = DataHelper.GetInt32(cboCountries.SelectedValue);
            var items = CountryState.GetList(countryCode);
            if (items.Count() > 0)
            {
                cboStateProvince.DataSource = CountryState.GetList(countryCode);
                cboStateProvince.DataBind();
            }

            bool hasSelected = false;
            if (stateProvinceCode > 0)
            {
                ListItem item = cboStateProvince.Items.FindByValue(stateProvinceCode.ToString());
                if (item != null)
                {
                    WebHelper.AspNetAjaxComboBoxSelectText(cboStateProvince, item.Text);
                    hasSelected = true;
                }
            }

            if (!hasSelected && !string.IsNullOrEmpty(stateProvince))
                WebHelper.AspNetAjaxComboBoxSelectText(cboStateProvince, stateProvince, "-1");
        }
    }
}