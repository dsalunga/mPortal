using WCMS.Common.Utilities;
using WCMS.Framework.Core.Shared;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Contact
{
    /// <summary>
    ///		Summary description for CMS_Inquiries_02.
    /// </summary>
    public partial class AdminInquiriesDetails : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string IID = Request["__id"];

			if(!Page.IsPostBack)
			{
                int inquiryId = DataUtil.GetId(Request["__id"]);
                ContactInquiry item;
                if (inquiryId > 0 && (item = ContactInquiry.Get(inquiryId)) != null)
                {
                    var country = Country.Get(item.CountryCode);
                    var countryState = CountryState.Get(item.StateCode);

                    lblSubject.Text = item.Subject;
                    lblInquiryType.Text = item.InquiryType;
                    lblName.Text = item.SenderName;
                    lblEmail.Text = item.Email;
                    lblAddressLine1.Text = item.Address1;
                    lblAddressLine2.Text = item.Address2;
                    lblCity.Text = item.City;
                    lblCountry.Text = country != null ? country.CountryName : "";
                    lblState.Text = countryState != null ? countryState.StateName : "";
                    lblZipCode.Text = item.ZipCode;

                    lblPhone.Text = item.Phone;
                    lblFax.Text = item.Fax;
                    lblSendTo.Text = item.SendTo;
                    lblSendToEmail.Text = item.SendToEmail;
                    lblDateTime.Text = item.InqDateTime.ToString();
                    lblMessage.Text = item.Message;
                }
			}
		}

		protected void cmdReturn_Click(object sender, System.EventArgs e)
		{
			WQuery query = new WQuery(this);
			query.Remove("Load");
			query.Remove("__id");
            query.Redirect();
		}
	}
}
