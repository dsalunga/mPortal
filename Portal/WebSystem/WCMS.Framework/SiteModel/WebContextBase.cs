using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Http;

using WCMS.Common.Utilities;

namespace WCMS.Framework
{
    public abstract class WebContextBase
    {
        private static Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Configures the shared IHttpContextAccessor for all WebContextBase subclasses.
        /// Call from Program.cs after building the service provider.
        /// </summary>
        public static void ConfigureAccessor(Microsoft.AspNetCore.Http.IHttpContextAccessor accessor)
        {
            _httpContextAccessor = accessor;
        }

        /// <summary>
        /// Returns the current System.Web.HttpContext via IHttpContextAccessor (preferred)
        /// or falls back to HttpContext.Current (SystemWebAdapters shim).
        /// </summary>
        public System.Web.HttpContext Context
        {
            get
            {
                // Prefer IHttpContextAccessor resolution
                if (_httpContextAccessor?.HttpContext != null)
                {
                    // SystemWebAdapters wraps ASP.NET Core HttpContext as System.Web.HttpContext
                    return System.Web.HttpContext.Current;
                }
                return System.Web.HttpContext.Current;
            }
        }
    }
}
