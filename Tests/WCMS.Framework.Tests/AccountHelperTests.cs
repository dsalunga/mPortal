using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCMS.Framework.Utilities;

namespace WCMS.Framework.Tests
{
    [TestClass]
    public class AccountHelperTests
    {
        [TestMethod]
        public void GetObjectId_ParsesUniqueString()
        {
            // Format: "{objectId}\{recordId}" where splitter is backslash
            var result = AccountHelper.GetObjectId(@"21\123");
            Assert.AreEqual(21, result);
        }

        [TestMethod]
        public void GetRecordId_ParsesUniqueString()
        {
            var result = AccountHelper.GetRecordId(@"21\123");
            Assert.AreEqual(123, result);
        }

        [TestMethod]
        public void GetObjectId_ReturnsMinusOneForInvalid()
        {
            var result = AccountHelper.GetObjectId("invalid");
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void GetRecordId_WebGroupFormat()
        {
            // WebGroup = 38
            var result = AccountHelper.GetRecordId(@"38\456");
            Assert.AreEqual(456, result);
        }
    }
}
