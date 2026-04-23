using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WCMS.Integration.Tests
{
    [TestClass]
    public class AuthenticationFlowTests
    {
        private static WebApplicationFactory<Program> _factory;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("WConfig:AllowCache", "false");
                    builder.UseSetting("WCMS:AllowCache", "false");
                });
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _factory?.Dispose();
        }

        [TestMethod]
        public async Task AccountLogin_InvalidCredentials_RedirectsWithLoginError()
        {
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            var formData = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["userName"] = "invalid-user",
                ["password"] = "invalid-password"
            });

            var response = await client.PostAsync("/account/login", formData);
            Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);

            var location = response.Headers.Location?.ToString() ?? string.Empty;
            StringAssert.Contains(location, "LoginError=");
        }

        [TestMethod]
        public async Task Logout_AlwaysRedirectsToRoot()
        {
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            var response = await client.GetAsync("/logout");
            Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);

            var location = response.Headers.Location?.ToString() ?? string.Empty;
            Assert.IsTrue(location == "/" || location.StartsWith("/?", System.StringComparison.Ordinal));
        }
    }
}
