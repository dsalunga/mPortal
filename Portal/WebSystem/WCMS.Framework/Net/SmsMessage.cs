using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Net;

namespace WCMS.Framework.Net
{
    public class SmsMessage
    {
        public SmsMessage( string recipient, string message)
        {
            this.Recipient = recipient;
            this.Message = message;
        }

        public string Recipient { get; set; }
        public string Message { get; set; }

        public bool Send(SmsConfig config)
        {
            return SmsHelper.SendMessage(WConfig.HttpSmsUrl, Recipient, Message);
        }
    }
}
