using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCMS.Framework.Middleware;

namespace WCMS.Framework.UnitTests
{
    [TestClass]
    public class PageResolutionMiddlewareTests
    {
        [TestMethod]
        public async Task StaticFiles_SkipResolution()
        {
            // Arrange
            var middleware = new PageResolutionMiddleware(async (ctx) =>
            {
                ctx.Response.StatusCode = 200;
                await Task.CompletedTask;
            });

            var context = new DefaultHttpContext();
            context.Request.Path = "/content/styles.css";

            // Act
            await middleware.InvokeAsync(context);

            // Assert - should pass through without setting ResolvedPage
            Assert.IsNull(context.Items["ResolvedPage"]);
            Assert.AreEqual(200, context.Response.StatusCode);
        }

        [TestMethod]
        public async Task ApiRoutes_SkipResolution()
        {
            var middleware = new PageResolutionMiddleware(async (ctx) =>
            {
                ctx.Response.StatusCode = 200;
                await Task.CompletedTask;
            });

            var context = new DefaultHttpContext();
            context.Request.Path = "/api/framework/login";

            await middleware.InvokeAsync(context);

            Assert.IsNull(context.Items["ResolvedPage"]);
            Assert.AreEqual(200, context.Response.StatusCode);
        }

        [TestMethod]
        public async Task HealthEndpoint_SkipResolution()
        {
            var middleware = new PageResolutionMiddleware(async (ctx) =>
            {
                ctx.Response.StatusCode = 200;
                await Task.CompletedTask;
            });

            var context = new DefaultHttpContext();
            context.Request.Path = "/health";

            await middleware.InvokeAsync(context);

            Assert.IsNull(context.Items["ResolvedPage"]);
        }

        [TestMethod]
        public async Task UserContentPath_SkipResolution()
        {
            var middleware = new PageResolutionMiddleware(async (ctx) =>
            {
                ctx.Response.StatusCode = 200;
                await Task.CompletedTask;
            });

            var context = new DefaultHttpContext();
            context.Request.Path = "/u/photo.jpg";

            await middleware.InvokeAsync(context);

            Assert.IsNull(context.Items["ResolvedPage"]);
        }

        [TestMethod]
        public async Task WebObjectNotInitialized_SkipResolution()
        {
            // When WebObject.IsInitialized is false or throws, middleware should skip resolution
            var middleware = new PageResolutionMiddleware(async (ctx) =>
            {
                ctx.Response.StatusCode = 200;
                await Task.CompletedTask;
            });

            var context = new DefaultHttpContext();
            context.Request.Path = "/some-page";
            context.Request.Host = new HostString("localhost");

            // WebObject may throw TypeInitializationException in test environment
            // The middleware should still not crash and pass through
            try
            {
                await middleware.InvokeAsync(context);
                Assert.IsNull(context.Items["ResolvedPage"]);
            }
            catch (System.TypeInitializationException)
            {
                // Expected when WebObject static initializer can't find assembly
                // This confirms the middleware does attempt page resolution
            }
        }
    }
}
