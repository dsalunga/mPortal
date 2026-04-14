using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration
{
    /// <summary>
    /// Data synchronization service contract.
    /// Migrated from WCF ServiceContract to plain interface for ASP.NET Core.
    /// </summary>
    public interface IDataSync
    {
        void DoWork();

        List<MemberLinkContainer> GetMemberLinkList();

        MemberLinkContainer GetMemberLinkComplete(string userName);

        void SetMemberLinkComplete(MemberLinkContainer container);
    }
}
