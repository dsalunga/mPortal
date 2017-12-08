using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;

using WCMS.Common.Utilities;

namespace WCMS.Common.Net
{
    public class SmsHelper
    {
        public static bool SendMessage(string httpSmsUrl, string number, string message, bool checkResponse = false)
        {
            var httpAddress = !string.IsNullOrEmpty(httpSmsUrl) ? httpSmsUrl : ConfigHelper.Get("HttpSmsUrl");
            if (!string.IsNullOrEmpty(httpAddress))
            {
                httpAddress = httpAddress.Replace("&amp;", "&");

                string mobileNumber;
                //if (number.StartsWith("+65"))
                //    mobileNumber = number.Substring(3);
                //else
                mobileNumber = number.Replace("+", "%2B").Replace("-", "").Replace(" ", "");

                string completedUrl = string.Format(httpAddress, mobileNumber, HttpUtility.UrlEncode(message));

                try
                {
                    // Create a request for the URL. 		
                    WebRequest request = WebRequest.Create(completedUrl);
                    // If required by the server, set the credentials.
                    request.Credentials = CredentialCache.DefaultCredentials;

                    // Get the response.
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        if (checkResponse)
                        {
                            // Display the status.
                            Console.WriteLine(response.StatusDescription);

                            // Open the stream using a StreamReader for easy access.
                            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                            {
                                // Read the content.
                                string responseFromServer = reader.ReadToEnd();

                                return responseFromServer.IndexOf("Message Submitted", StringComparison.InvariantCultureIgnoreCase) >= 0;
                            }
                        }
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
