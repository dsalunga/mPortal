using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using WCMS.Common.Utilities;

namespace WCMS.Framework.Middleware
{
    /// <summary>
    /// Aligns ASP.NET Core cookie authentication with legacy WSession state.
    /// Ensures WSession.Current is hydrated early in the pipeline so downstream
    /// legacy access checks and permission paths operate on a logged-in session.
    /// </summary>
    public class WcmsSessionHydrationMiddleware
    {
        private readonly RequestDelegate _next;

        public WcmsSessionHydrationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var session = WSession.Current;
            if (!session.IsLoggedIn)
            {
                var claimUserId = DataUtil.GetId(context.User?.FindFirstValue(ClaimTypes.NameIdentifier));
                if (claimUserId > 0)
                {
                    session.Login(claimUserId, logSession: false);
                }
                else
                {
                    var rememberedUser = WSession.CheckLoginCookie(context, clearIfInvalid: true);
                    if (rememberedUser != null && rememberedUser.Id > 0)
                    {
                        session.Login(rememberedUser.Id, logSession: false);
                    }
                }
            }

            await _next(context);
        }
    }

    public static class WcmsSessionHydrationMiddlewareExtensions
    {
        public static IApplicationBuilder UseWcmsSessionHydration(this IApplicationBuilder app)
        {
            return app.UseMiddleware<WcmsSessionHydrationMiddleware>();
        }
    }
}
