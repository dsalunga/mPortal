using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.WebParts.Contact
{
    public struct ContactConstants
    {
        public const string RecipientsKey = "Recipients";
        public const string ToInquirerSubjectKey = "ToInquirerSubject";
        public const string ToModeratorSubjectKey = "ToModeratorSubject";
        public const string SendReplyFlagKey = "SendReply";
        public const string ReplyEmailTemplateKey = "ReplyEmailTemplate";
        public const string ReplyEmailSenderKey = "ReplyEmailSender";

        public const string CONST_Inquiries = "Inquiries";

        public const string DefaultInquiryRecipientPath = "/Apps/Contact/DefaultInquiryRecipient";
        public const string EmailSubjectPath = "/Apps/Contact/EmailSubject";
        public const string ThankYouMsgPath = "/Apps/Contact/ThankYouMsg";
        public const string FromPath = "/Apps/Contact/From";
    }

    public struct ContactModes
    {
        public const int AutoMode = -1;
        public const int PublicMode = 0;
        public const int AuthenticatedMode = 1;
    }
}
