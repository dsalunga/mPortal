using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCMS.Common.Utilities;

namespace WCMS.Framework.UnitTests
{
    [TestClass]
    public class ConfigUtilTests
    {
        [TestMethod]
        public void GetInt32_ReturnsDefaultWhenKeyMissing()
        {
            var result = ConfigUtil.GetInt32("NonExistentKey_" + Guid.NewGuid());
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetBool_ReturnsTrueDefault()
        {
            var result = ConfigUtil.GetBool("NonExistentKey_" + Guid.NewGuid(), true);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetBool_ReturnsFalseDefault()
        {
            var result = ConfigUtil.GetBool("NonExistentKey_" + Guid.NewGuid(), false);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Get_ReturnsNullForMissingKey()
        {
            var result = ConfigUtil.Get("NonExistentKey_" + Guid.NewGuid());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SetConfiguration_AcceptsNull()
        {
            // Should not throw
            ConfigUtil.SetConfiguration(null);
        }
    }
}
