using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;

using WCMS.Common.Utilities;

namespace WCMS.Framework
{
    public abstract class WebContextBase
    {
        private static IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Configures the shared IHttpContextAccessor for all WebContextBase subclasses.
        /// Call from Program.cs after building the service provider.
        /// </summary>
        public static void ConfigureAccessor(IHttpContextAccessor accessor)
        {
            _httpContextAccessor = accessor;
        }

        /// <summary>
        /// Returns the current Microsoft.AspNetCore.Http.HttpContext via IHttpContextAccessor.
        /// </summary>
        public HttpContext Context
        {
            get
            {
                return _httpContextAccessor?.HttpContext ?? HttpContextHelper.Current;
            }
        }
    }
}
