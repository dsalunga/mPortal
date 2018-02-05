using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace WCMS.WebSystem.Apps.Integration.ExternalMemberWS
{
    public partial class MemberSoapClient
    {
        public MemberSoapClient(bool expect100Continue)
        {
            ServicePointManager.Expect100Continue = expect100Continue;
        }

        public static MemberSoapClient GetNewClientInstance()
        {
            var client = new MemberSoapClient(false);
            return client;
        }
    }
}
