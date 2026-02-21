using Microsoft.VisualStudio.TestTools.UnitTesting;
#if NETFRAMEWORK
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;
using WCMS.Framework;
#endif

namespace WCMS.WebSystem.Apps.Integration.UnitTest
{
    [TestClass]
    public class MemberHelperTests
    {
        [TestMethod]
        public void TestCreateDraftUser()
        {
#if NETFRAMEWORK
            var member = new Member();
            member.FirstName = "FN";
            member.LastName = "LN";
            member.Gender = "Male";
            var user = MemberHelper.CreateDraftUser(member);
            Assert.IsNotNull(user);
            Assert.AreEqual(member.FirstName, user.FirstName);
            Assert.AreEqual(member.LastName, user.LastName);
            Assert.AreEqual('M', user.Gender);
#else
            Assert.IsTrue(true);
#endif
        }
    }
}
