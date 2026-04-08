using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;

using WCMS.Common.Utilities;

namespace WCMS.Common.Net
{
    public class SmsHelper
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public static bool SendMessage(string httpSmsUrl, string number, string message, bool checkResponse = false)
        {
            var httpAddress = !string.IsNullOrEmpty(httpSmsUrl) ? httpSmsUrl : ConfigUtil.Get("HttpSmsUrl");
            if (!string.IsNullOrEmpty(httpAddress))
            {
                httpAddress = httpAddress.Replace("&amp;", "&");

                string mobileNumber;
                //if (number.StartsWith("+65"))
                //    mobileNumber = number.Substring(3);
                //else
                mobileNumber = number.Replace("+", "%2B").Replace("-", "").Replace(" ", "");

                string completedUrl = string.Format(httpAddress, mobileNumber, WebUtility.UrlEncode(message));

                try
                {
                    var response = HttpClient.GetAsync(completedUrl).GetAwaiter().GetResult();

                    if (checkResponse)
                    {
                        var responseFromServer = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        return responseFromServer.IndexOf("Message Submitted", StringComparison.InvariantCultureIgnoreCase) >= 0;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(ex);

                    return false;
                }
            }

            return !checkResponse || false;
        }
    }
}
