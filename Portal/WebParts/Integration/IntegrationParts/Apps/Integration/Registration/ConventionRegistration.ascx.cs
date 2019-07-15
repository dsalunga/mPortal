using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core.Shared;

using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile
{
    public partial class ConventionRegistrationController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                for (int i = 4; i < 81; i++)
                    cboAge.Items.Add(i.ToString());

                WContext context = new WContext(this);
                var element = context.Element;

                var title = element.GetParameterValue("Title");
                if (!string.IsNullOrEmpty(title))
                {
                    linkReturn.InnerHtml = string.Format("Return to {0}", title);

                    this.Page.Title = title;
                }
            }
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var registerOnce = hRegisterOnce.Value == "1";

            int age = 0;
            if (!int.TryParse(cboAge.SelectedItem.Text, out age) || age < 1)
            {
                lblStatus.Text = "Please select your AGE.";
                return;
            }

            var item = GenericRegistration.Provider.Get(name);
            if (!registerOnce || item == null)
            {
                item = new GenericRegistration();
                item.Name = name;
                item.Age = age;
                item.Gender = cboGender.SelectedValue;
                item.Country = cboCountry.SelectedValue;
                item.Locale = txtLocale.Text.Trim();
                item.ExternalId = txtExternalId.Text.Trim();
                item.Designation = txtDesignation.Text.Trim();
                item.ArrivalDate = DataUtil.GetDateTime(txtArrivalDate.Text.Trim(), new CultureInfo("en-US"));
                item.Airline = txtAirline.Text.Trim();
                item.FlightNo = txtFlightNo.Text.Trim();
                item.DepartureDate = DataUtil.GetDateTime(txtDepartureDate.Text.Trim(), new CultureInfo("en-US"));
                item.Address = txtAddress.Text.Trim();
                item.PlaceType = txtPlaceType.Text.Trim();

                if (item.ArrivalDate.Equals(DataHelper.MinDate))
                {
                    lblStatus.Text = "Please enter a valid DATE OF ARRIVAL.";
                    return;
                }

                if (item.DepartureDate.Equals(DataHelper.MinDate))
                {
                    lblStatus.Text = "Please enter a valid DATE OF DEPARTURE.";
                    return;
                }

                item.Update();

                panelRegistrationForm.Visible = false;
                panelDone.Visible = true;


                linkReturn.HRef = (new WContext(this)).BuildQuery();
            }
            else
            {
                lblStatus.Text = "Sorry, you can register only once.";
                return;
            }
        }
    }
}