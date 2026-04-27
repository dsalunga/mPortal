using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCMS.Framework.Middleware;

namespace WCMS.Framework.UnitTests
{
    [TestClass]
    public class PageRenderingMiddlewareTests
    {
        [TestMethod]
        public async Task NoResolvedPage_PassesThrough()
        {
            var middleware = new PageRenderingMiddleware(async (ctx) =>
            {
                ctx.Response.StatusCode = 200;
                await Task.CompletedTask;
            });

            var context = new DefaultHttpContext();
            // No "ResolvedPage" in Items

            await middleware.InvokeAsync(context);

            Assert.AreEqual(200, context.Response.StatusCode);
            Assert.IsNull(context.Items["CmsMasterPage"]);
            Assert.IsNull(context.Items["CmsPageElements"]);
        }

        [TestMethod]
        public async Task NullResolvedPage_PassesThrough()
        {
            var middleware = new PageRenderingMiddleware(async (ctx) =>
            {
                ctx.Response.StatusCode = 200;
                await Task.CompletedTask;
            });

            var context = new DefaultHttpContext();
            context.Items["ResolvedPage"] = null;

            await middleware.InvokeAsync(context);

            Assert.AreEqual(200, context.Response.StatusCode);
            Assert.IsNull(context.Items["CmsMasterPage"]);
        }

        [TestMethod]
        public async Task WrongTypeInResolvedPage_PassesThrough()
        {
            var middleware = new PageRenderingMiddleware(async (ctx) =>
            {
                ctx.Response.StatusCode = 200;
                await Task.CompletedTask;
            });

            var context = new DefaultHttpContext();
            context.Items["ResolvedPage"] = "not-a-page-object";

            await middleware.InvokeAsync(context);

            Assert.AreEqual(200, context.Response.StatusCode);
            Assert.IsNull(context.Items["CmsMasterPage"]);
        }
    }
}
