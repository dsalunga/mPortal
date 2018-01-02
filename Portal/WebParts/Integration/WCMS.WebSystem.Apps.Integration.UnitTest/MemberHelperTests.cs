using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;
using WCMS.Framework;

namespace WCMS.WebSystem.Apps.Integration.UnitTest
{
    [TestClass]
    public class MemberHelperTests
    {
        [TestMethod]
        public void TestCreateDraftUser()
        {
            var member = new Member();
            member.FirstName = "FN";
            member.LastName = "LN";
            member.Gender = "Male";
            var user = MemberHelper.CreateDraftUser(member);
            Assert.IsNotNull(user);
            Assert.Equals(member.FirstName, user.FirstName);
            Assert.Equals(member.LastName, user.LastName);
            Assert.Equals(user.Gender, 'M');
        }
    }
}
