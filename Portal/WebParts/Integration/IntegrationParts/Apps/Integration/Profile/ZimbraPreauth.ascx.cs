using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Security.Cryptography;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Profile
{
    public partial class ZimbraPreauth : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                /*
                 *  This page authenticates a user to zimbra using zimbra's preauth method.
                 * A user should be authenticated before reaching this page via some external 
                 * method which populates LOGON_USER, then a one time key is generated and forwarded the user
                 * is forwarded to zimbra.  Zimbra validates the key, generates a cookie for the 
                 * user, and they are then authenticated.
                 */

                // warning.Text = Request.ServerVariables["ALL_HTTP"];

                WContext context = new WContext(this);
                var element = context.Element;

                string zimbraUrl = element.GetParameterValue("ZimbraUrl");

                if (WSession.Current.IsLoggedIn)
                {
                    var user = WSession.Current.User;
                    var emailDomainFilter = element.GetParameterValue("EmailDomainFilter", "@someorg.org.sg");
                    if (user.Email.EndsWith(emailDomainFilter, StringComparison.InvariantCultureIgnoreCase))
                    {
                        string preauthKey = element.GetParameterValue("PreauthKey"); //"3298a239b8983c723874893274..."; //copied from zimbra domain config
                        string preauthUrl = element.GetParameterValue("PreauthUrl"); // "https://mail.someorg.org.sg/service/preauth";
                        string redirectURL = element.GetParameterValue("redirectURL");
                        string email = user.Email.ToLower(); //Request.ServerVariables["LOGON_USER"] + "@zimbra.example.com";

                        string preauthValue; //holds the string to be encoded
                        string preauthEncoded; //holds the preauthvalue after it has been sha1-hmac'd with preauthkey
                        string timeStamp; //millisecs since epoch


                        //convert key to byte form, as required for hmac
                        ASCIIEncoding encoding = new ASCIIEncoding();
                        HMACSHA1 hmacsha1 = new HMACSHA1(encoding.GetBytes(preauthKey));

                        //get timestamp
                        DateTime d1 = new DateTime(1970, 1, 1);
                        DateTime d2 = DateTime.Now;
                        TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);

                        //correct .net timezone
                        TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(d2);
                        Int64 tsint = (long)ts.TotalMilliseconds - (long)utcOffset.TotalMilliseconds;
                        timeStamp = tsint.ToString();

                        //have everything we need to make our preauth string
                        preauthValue = email + "|name|0|" + timeStamp;

                        //encode our preauth string
                        byte[] preauthvaluebytes = encoding.GetBytes(preauthValue);
                        byte[] hashmessage = hmacsha1.ComputeHash(preauthvaluebytes);
                        StringBuilder sb = new StringBuilder(hashmessage.Length * 2);

                        foreach (byte b in hashmessage)
                            sb.AppendFormat("{0:x2}", b);

                        preauthEncoded = sb.ToString();

                        //send the user over to zimbra.  hope all is well.
                        //Response.Redirect(preauthUrl + "?account=" + email + "&by=name&timestamp=" + timeStamp + "&expires=0&preauth=" + preauthEncoded);

                        QueryParser query = new QueryParser(preauthUrl);
                        query.Set("account", email);
                        query.Set("by", "name");
                        query.Set("timestamp", timeStamp);
                        query.Set("expires", "0");
                        query.Set("preauth", preauthEncoded);

                        if (!string.IsNullOrEmpty(redirectURL))
                            query.Set("redirectURL", redirectURL);

                        query.Redirect();
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(zimbraUrl))
                    QueryParser.StaticBaseRedirect(zimbraUrl);
            }
        }
    }
}