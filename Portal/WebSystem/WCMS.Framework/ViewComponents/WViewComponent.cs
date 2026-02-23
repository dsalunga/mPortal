using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WCMS.Framework.ViewComponents
{
    /// <summary>
    /// Base class for all mPortal CMS View Components that replaces the legacy
    /// System.Web.UI.UserControl / WUserControl base class.
    /// Provides access to IWContext and IWSession through dependency injection.
    /// </summary>
    public abstract class WViewComponent : ViewComponent
    {
        protected IWContext WcmsContext { get; }
        protected IWSession WcmsSession => WcmsContext.Session;

        protected WViewComponent(IWContext context)
        {
            WcmsContext = context;
        }

        /// <summary>
        /// Override in subclasses to provide a custom page title (replaces WUserControl.PageTitleOverride).
        /// </summary>
        public virtual string PageTitleOverride => null;
    }
}
