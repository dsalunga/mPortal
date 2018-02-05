using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Core.Interfaces;
using WCMS.WebSystem.Apps.Integration.Registration.MemberWS;

namespace WCMS.WebSystem.Apps.Integration.Registration
{
    public class MemberLink : ISelfManager
    {
        private static MemberLinkProvider _provider;

        public MemberLink()
        {
            MemberLinkId = -1;
            UserId = -1;
            MemberId = -1;
            ExternalIdNo = "";
        }

        static MemberLink()
        {
            _provider = new MemberLinkProvider();
        }

        #region Properties

        [ObjectColumn(true)]
        public int MemberLinkId { get; set; }

        [ObjectColumn]
        public int UserId { get; set; }

        [ObjectColumn]
        public int MemberId { get; set; }

        [ObjectColumn]
        public string ExternalIdNo { get; set; }

        

        #endregion

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return Delete(this.MemberLinkId);
        }

        #region Static Methods

        public static MemberLink Get(int memberLinkId)
        {
            return _provider.Get(memberLinkId);
        }

        public static MemberLink Get(int userId, int memberId)
        {
            return _provider.Get(userId, memberId);
        }

        public static List<MemberLink> GetList()
        {
            return _provider.GetList();
        }

        public static bool Delete(int memberLinkId)
        {
            return _provider.Delete(memberLinkId);
        }

        #endregion
    }
}
