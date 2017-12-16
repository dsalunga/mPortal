using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Net;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Agent;

namespace WCMS.WebSystem.WebParts.Newsletter
{
    public partial class NewsletterView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var user = WSession.Current.User;
                if (user != null)
                {
                    txtName.Text = user.FirstAndLastName;
                    txtEmail.Text = user.Email;

                    if (user.Gender != GenderTypes.Unspecified)
                    {
                        rblGender.SelectedValue = user.Gender == GenderTypes.Male ? GenderTypes.MaleId.ToString() : GenderTypes.FemaleId.ToString();
                    }
                }
            }
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var element = context.Element;
            var set = context.GetParameterSet();
            var site = context.Site;
            var page = context.Page;

            var objectId = DataHelper.GetId(ParameterizedWebObject.GetValue("ObjectId", element, set));
            var recordId = DataHelper.GetId(ParameterizedWebObject.GetValue("RecordId", element, set));
            if (objectId == -1)
                objectId = WebObjects.WebPage;
            if (recordId == -1)
                recordId = page.Id;

            var name = txtName.Text.Trim();
            var email = txtEmail.Text.Trim();
            var gender = DataHelper.GetInt32(rblGender.SelectedValue);

            var item = NewsletterEntry.Provider.Get(objectId, recordId, email);
            if (item == null)
            {
                item = new NewsletterEntry();
                item.Name = name;
                item.Email = email;
                item.IPAddress = Request.UserHostAddress;
                item.Gender = gender;
                item.ObjectId = objectId;
                item.RecordId = recordId;
                item.SiteId = site == null ? -1 : site.Id;
                item.Update();

                // Send email
                var alertEnabled = DataHelper.GetBool(ParameterizedWebObject.GetValue("SubscribeEmailEnabled", element, set), true);
                var emailContent = ParameterizedWebObject.GetValue("SubscribeEmailContent", element, set);
                var emailSubject = ParameterizedWebObject.GetValue("SubscribeEmailSubject", element, set);
                var replyToAndBCC = ParameterizedWebObject.GetValue("ReplyToAndBCC", element, set);

                if (alertEnabled && !string.IsNullOrEmpty(emailContent) && !string.IsNullOrEmpty(emailSubject))
                {
                    NamedValueProvider provider = new NamedValueProvider();
                    provider.Add("Name", string.Format("{0} {1}", gender == GenderTypes.MaleId ? "Bro." : "Sis.", name));

                    emailSubject = Substituter.Substitute(emailSubject, provider);

                    provider.Add("Subject", emailSubject);

                    WebMailMessage message = new WebMailMessage();
                    message.Subject = emailSubject; // string.Format("Subject Text: $(Name) - $(Subject)", toModeratorEmailSubject, name, subject);
                    message.Body = Substituter.Substitute(emailContent, provider);
                    message.To.Add(email);

                    // ReplyTo
                    if (!string.IsNullOrEmpty(replyToAndBCC))
                    {
                        message.ReplyToList.Clear();
                        message.ReplyToList.Add(replyToAndBCC);
                        message.Bcc.Add(replyToAndBCC);
                    }

                    message.Send();
                }

                MultiView1.SetActiveView(viewSuccess);
            }
            else
            {
                MultiView1.SetActiveView(viewAlreadySubscribed);
            }
        }
    }
}