using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCMS.Framework;

namespace WCMS.Framework.UnitTests
{
    [TestClass]
    public class WQueryTests
    {
        [TestMethod]
        public void Constructor_ParsesQueryString()
        {
            var query = new WQuery("/page?key1=value1&key2=value2");
            Assert.AreEqual("value1", query.Get("key1"));
            Assert.AreEqual("value2", query.Get("key2"));
        }

        [TestMethod]
        public void Constructor_ParsesPathOnly()
        {
            var query = new WQuery("/some/path");
            Assert.AreEqual("/some/path", query.BasePath);
        }

        [TestMethod]
        public void Set_AddsNewParameter()
        {
            var query = new WQuery("/page");
            query.Set("testKey", "testValue");
            Assert.AreEqual("testValue", query.Get("testKey"));
        }

        [TestMethod]
        public void Set_OverwritesExistingParameter()
        {
            var query = new WQuery("/page?key=old");
            query.Set("key", "new");
            Assert.AreEqual("new", query.Get("key"));
        }

        [TestMethod]
        public void Remove_DeletesParameter()
        {
            var query = new WQuery("/page?key=value");
            query.Remove("key");
            Assert.IsTrue(string.IsNullOrEmpty(query.Get("key")));
        }

        [TestMethod]
        public void Clone_CreatesIndependentCopy()
        {
            var original = new WQuery("/page?key=value");
            var clone = original.Clone();
            clone.Set("key", "modified");
            Assert.AreEqual("value", original.Get("key"));
            Assert.AreEqual("modified", clone.Get("key"));
        }

        [TestMethod]
        public void BuildQuery_IncludesParameters()
        {
            var query = new WQuery("/page");
            query.Set("a", "1");
            var result = query.BuildQuery();
            Assert.IsTrue(result.Contains("a=1"));
        }

        [TestMethod]
        public void GetId_ReturnsMinusOneForMissingKey()
        {
            var query = new WQuery("/page");
            var result = query.GetId("nonexistent");
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Constructor_EmptyString()
        {
            var query = new WQuery("");
            Assert.IsNotNull(query);
        }
    }
}
