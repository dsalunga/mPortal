using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.Controls
{
    public partial class PhoneNumber : System.Web.UI.UserControl
    {
        public int CountryCode
        {
            get { return DataHelper.GetInt32(hCountryCode.Value); }
            set { hCountryCode.Value = value.ToString(); lblPhoneCode.InnerHtml = string.Format("+{0}", value); }
        }

        public string CountryCodePlus { get { return string.Format("+{0}", hCountryCode.Value); } }

        public int MaxDigits
        {
            get { return DataHelper.GetInt32(hMaxDigits.Value); }
            set
            {
                hMaxDigits.Value = value.ToString();

                if (value > 0)
                {
                    txtPhoneNumber.MaxLength = value;
                    txtPhoneNumber.Columns = value + 2;
                }
            }
        }

        public string GetCompleteNumber()
        {
            var number = Number;
            return string.IsNullOrEmpty(number) ? "" : string.Format("+{0}{1}", CountryCode, Number);
        }

        public TextBox NumberControl { get { return txtPhoneNumber; } }

        public string Number
        {
            get
            {
                var countryCode = hCountryCode.Value;
                var countryCodeInt = DataHelper.GetInt32(countryCode);

                if (countryCodeInt > 0)
                {
                    var phoneNumber = txtPhoneNumber.Text.Trim();
                    var countryCodePlus = "+" + countryCode;

                    if (phoneNumber.StartsWith(countryCodePlus))
                        return phoneNumber.Substring(countryCodePlus.Length);
                    else if (phoneNumber.StartsWith(countryCode) && (phoneNumber.Length + countryCode.Length) > MaxDigits)
                        return phoneNumber.Substring(countryCode.Length);
                    else if (phoneNumber.Length > MaxDigits)
                        return phoneNumber.Substring(0, MaxDigits);

                    return phoneNumber;
                }
                else
                {
                    return txtPhoneNumber.Text.Trim();
                }
            }

            set
            {
                var number = DataHelper.FormatPhoneNumber(value);
                var countryCode = hCountryCode.Value;
                var countryCodePlus = CountryCodePlus;
                var maxDigits = MaxDigits;

                if (value.StartsWith(countryCodePlus) && number.Length > countryCodePlus.Length)
                    txtPhoneNumber.Text = number.Substring(countryCodePlus.Length);
                else if (number.StartsWith(countryCode) && (number.Length + countryCode.Length) >= maxDigits + countryCode.Length)
                    txtPhoneNumber.Text = number.Substring(countryCode.Length);
                else if (number.Length > maxDigits)
                    txtPhoneNumber.Text = number.Substring(0, maxDigits);
                else
                    txtPhoneNumber.Text = number;
            }
        }

        public void Initialize(int countryCode, string phoneNumber = "", int maxDigits = 10)
        {
            CountryCode = countryCode;
            MaxDigits = maxDigits;

            Number = phoneNumber;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}