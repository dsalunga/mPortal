using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCMS.Common.Utilities;

namespace WCMS.Framework.Tests
{
    [TestClass]
    public class SqlBuilderTests
    {
        [TestInitialize]
        public void Setup()
        {
            // Configure DbHelper for SqlServer for testing
            var field = typeof(DbHelper).GetField("_instance",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            field?.SetValue(null, null);
            DbHelper.ConfigureFromSettings(DatabaseProvider.SqlServer, "Server=test;Database=test;");
        }

        [TestCleanup]
        public void Cleanup()
        {
            var field = typeof(DbHelper).GetField("_instance",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            field?.SetValue(null, null);
        }

        [TestMethod]
        public void BuildSelect_NoWhere_ReturnsSelectAll()
        {
            var (sql, parms) = SqlBuilder.For("WebSkin").BuildSelect();
            Assert.AreEqual("SELECT * FROM WebSkin", sql);
            Assert.AreEqual(0, parms.Length);
        }

        [TestMethod]
        public void BuildSelect_WithWhere_SqlServer()
        {
            var (sql, parms) = SqlBuilder.For("WebSkin")
                .Where("Id", "Id", 42)
                .BuildSelect();
            Assert.AreEqual("SELECT * FROM WebSkin WHERE [Id] = @Id", sql);
            Assert.AreEqual(1, parms.Length);
            Assert.AreEqual("@Id", parms[0].ParameterName);
            Assert.AreEqual(42, parms[0].Value);
        }

        [TestMethod]
        public void BuildSelect_MultipleWhere_SqlServer()
        {
            var (sql, parms) = SqlBuilder.For("WebSkin")
                .Where("ObjectId", "ObjectId", 1)
                .Where("RecordId", "RecordId", 2)
                .BuildSelect();
            Assert.AreEqual("SELECT * FROM WebSkin WHERE [ObjectId] = @ObjectId AND [RecordId] = @RecordId", sql);
            Assert.AreEqual(2, parms.Length);
        }

        [TestMethod]
        public void BuildSelect_WithColumns_SqlServer()
        {
            var (sql, parms) = SqlBuilder.For("WebSkin")
                .Where("Id", "Id", 1)
                .BuildSelect("Id", "Name");
            Assert.AreEqual("SELECT [Id], [Name] FROM WebSkin WHERE [Id] = @Id", sql);
        }

        [TestMethod]
        public void BuildDelete_SqlServer()
        {
            var (sql, parms) = SqlBuilder.For("WebSkin")
                .Where("Id", "Id", 42)
                .BuildDelete();
            Assert.AreEqual("DELETE FROM WebSkin WHERE [Id] = @Id", sql);
            Assert.AreEqual(1, parms.Length);
        }

        [TestMethod]
        public void BuildCount_NoWhere_SqlServer()
        {
            var (sql, parms) = SqlBuilder.For("WebSkin").BuildCount();
            Assert.AreEqual("SELECT COUNT(1) FROM WebSkin", sql);
            Assert.AreEqual(0, parms.Length);
        }

        [TestMethod]
        public void BuildCount_WithWhere_SqlServer()
        {
            var (sql, parms) = SqlBuilder.For("WebPage")
                .Where("SiteId", "SiteId", 5)
                .BuildCount();
            Assert.AreEqual("SELECT COUNT(1) FROM WebPage WHERE [SiteId] = @SiteId", sql);
            Assert.AreEqual(1, parms.Length);
        }

        [TestMethod]
        public void BuildMax_SqlServer()
        {
            var (sql, parms) = SqlBuilder.For("WebPage")
                .Where("SiteId", "SiteId", 5)
                .BuildMax("Rank");
            Assert.AreEqual("SELECT MAX([Rank]) FROM WebPage WHERE [SiteId] = @SiteId", sql);
            Assert.AreEqual(1, parms.Length);
        }

        [TestMethod]
        public void BuildUpdate_SqlServer()
        {
            var (sql, parms) = SqlBuilder.For("WebSkin")
                .Set("Name", "Name", "Test")
                .Set("Rank", "Rank", 1)
                .Where("Id", "Id", 42)
                .BuildUpdate();
            Assert.AreEqual("UPDATE WebSkin SET [Name] = @Name, [Rank] = @Rank WHERE [Id] = @Id", sql);
            Assert.AreEqual(3, parms.Length);
        }

        [TestMethod]
        public void BuildInsert_SqlServer()
        {
            var (sql, parms) = SqlBuilder.For("WebSkin")
                .Set("Name", "Name", "Test")
                .Set("Rank", "Rank", 1)
                .BuildInsert();
            Assert.AreEqual("INSERT INTO WebSkin ([Name], [Rank]) VALUES (@Name, @Rank)", sql);
            Assert.AreEqual(2, parms.Length);
        }

        [TestMethod]
        public void BuildUpsert_Insert_SqlServer()
        {
            var (sql, parms, isInsert) = SqlBuilder.For("WebSkin")
                .Set("Name", "Name", "Test")
                .Set("Rank", "Rank", 1)
                .BuildUpsert("Id", 0);
            Assert.IsTrue(isInsert);
            Assert.IsTrue(sql.Contains("INSERT INTO WebSkin"));
            Assert.IsTrue(sql.Contains("SELECT SCOPE_IDENTITY()"));
        }

        [TestMethod]
        public void BuildUpsert_Update_SqlServer()
        {
            var (sql, parms, isInsert) = SqlBuilder.For("WebSkin")
                .Set("Name", "Name", "Test")
                .Set("Rank", "Rank", 1)
                .BuildUpsert("Id", 42);
            Assert.IsFalse(isInsert);
            Assert.IsTrue(sql.Contains("UPDATE WebSkin SET"));
            Assert.IsTrue(sql.Contains("[Id] = @Id"));
        }

        [TestMethod]
        public void BuildSelect_PostgreSql_UsesDoubleQuotes()
        {
            // Reconfigure for PostgreSQL
            var field = typeof(DbHelper).GetField("_instance",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            field?.SetValue(null, null);
            DbHelper.ConfigureFromSettings(DatabaseProvider.PostgreSql, "Host=localhost;Database=test;");

            var (sql, parms) = SqlBuilder.For("WebSkin")
                .Where("Id", "Id", 42)
                .BuildSelect();
            Assert.AreEqual("SELECT * FROM WebSkin WHERE \"Id\" = @Id", sql);
        }

        [TestMethod]
        public void BuildUpsert_Insert_PostgreSql_UsesReturning()
        {
            var field = typeof(DbHelper).GetField("_instance",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            field?.SetValue(null, null);
            DbHelper.ConfigureFromSettings(DatabaseProvider.PostgreSql, "Host=localhost;Database=test;");

            var (sql, parms, isInsert) = SqlBuilder.For("WebSkin")
                .Set("Name", "Name", "Test")
                .BuildUpsert("Id", 0);
            Assert.IsTrue(isInsert);
            Assert.IsTrue(sql.Contains("RETURNING"));
            Assert.IsFalse(sql.Contains("SCOPE_IDENTITY"));
        }

        [TestMethod]
        public void For_ReturnsNewBuilder()
        {
            var builder = SqlBuilder.For("TestTable");
            Assert.IsNotNull(builder);
        }
    }
}
