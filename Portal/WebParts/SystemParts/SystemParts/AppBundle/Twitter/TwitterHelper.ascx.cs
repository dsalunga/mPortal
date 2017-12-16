using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.Web.Helpers;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.WebHelpers
{
    public partial class TwitterHelper : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var element = context.Element;

            var twitterUserName = element.GetParameterValue("TwitterUserName", null);
            if (!string.IsNullOrEmpty(twitterUserName))
            {
                var width = DataHelper.GetInt32(element.GetParameterValue("Width", "250"));
                var height = DataHelper.GetInt32(element.GetParameterValue("Height", "300"));
                var backgroundShellColor = element.GetParameterValue("BackgroundShellColor", "#333");
                var shellColor = element.GetParameterValue("ShellColor", "#fff");
                var tweetsBackgroundColor = element.GetParameterValue("TweetsBackgroundColor", "#000");
                var tweetsColor = element.GetParameterValue("TWeetsColor", "#fff");
                var tweetsLinksColor = element.GetParameterValue("TweetsLinksColor", "#4aed05");
                var numberOfTweets = DataHelper.GetInt32(element.GetParameterValue("NumberOfTweets", "4"));
                var scrollBar = DataHelper.GetBool(element.GetParameterValue("ScrollBar", "false"));
                var loop = DataHelper.GetBool(element.GetParameterValue("Loop", "false"));
                var live = DataHelper.GetBool(element.GetParameterValue("Live", "false"));

                var placeholder = new Literal();
                placeholder.Text = Twitter.Profile(twitterUserName, width, height,
                    backgroundShellColor, shellColor, tweetsBackgroundColor, tweetsColor,
                    tweetsLinksColor, numberOfTweets, scrollBar, loop, live).ToHtmlString();

                this.Controls.Add(placeholder);
            }
        }
    }
}