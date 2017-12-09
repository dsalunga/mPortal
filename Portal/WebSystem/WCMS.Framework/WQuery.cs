using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

using WCMS.Common.Utilities;

namespace WCMS.Framework
{
    public class WQuery : QueryParser
    {
        public WQuery() { }

        public WQuery(NameValueCollection queryString) : base(queryString) { }
        public WQuery(WQuery toCopy) : base(toCopy) { }
        public WQuery(bool useHttpContext) : base(useHttpContext) { }
        public WQuery(HttpRequest request) : base(request) { }
        public WQuery(Page p)
            : this(p.Request) { }
        public WQuery(UserControl c)
            : this(c.Request) { }
        public WQuery(Control c)
            : this(c.Page.Request) { }
        public WQuery(HttpApplication a)
            : this(a.Request) { }
        public WQuery(HttpContext c)
            : this(c.Request) { }
        public WQuery(string queryStringOrPath) : base(queryStringOrPath) { }

        public WQuery(HttpRequestBase request) : base(request) { }
        public WQuery(System.Web.WebPages.WebPage page) : base(page) { }

        /// <summary>
        /// Sets which webpart to load (in relation to the configured part for current element). This is used only in front-end. 
        /// In Admin, Load parameter is the one required.
        /// </summary>
        /// <param name="value"></param>
        public WQuery SetOpen(object value)
        {
            this[WConstants.Open] = value.ToString();
            return this;
        }

        public string GetOpen()
        {
            return this[WConstants.Open];
        }

        /// <summary>
        /// Loads an admin control
        /// </summary>
        /// <param name="partAdmin"></param>
        public void LoadAndRedirect(string partAdmin)
        {
            this[WConstants.Load] = partAdmin;
            this.Redirect("~/Content/Parts/Central/");
        }

        /// <summary>
        /// Sets the "Load" key parameter (for Admin loading of components)
        /// </summary>
        /// <param name="cmd"></param>
        public void SetCmd(string cmd)
        {
            this[WConstants.Load] = cmd;
        }

        /// <summary>
        /// Removes the loaded control and redirects the page to admin control loader
        /// </summary>
        public void UnloadAndRedirect()
        {
            this.Remove(WConstants.Load);
            this.Redirect("~/Content/Parts/Central/");
        }

        public void LoadAndRedirect()
        {
            UnloadAndRedirect();
        }

        public WQuery SetLoad(string value)
        {
            this[WConstants.Load] = value;
            return this;
        }

        /// <summary>
        /// Open is used in the Front-End modules
        /// </summary>
        /// <returns></returns>
        public WQuery RemoveOpen()
        {
            this.Remove(WConstants.Open);
            return this;
        }

        /// <summary>
        /// Load is used in Central/Admin
        /// </summary>
        /// <returns></returns>
        public WQuery RemoveLoad()
        {
            this.Remove(WConstants.Load);
            return this;
        }

        public new WQuery Clone()
        {
            return new WQuery(this);
        }

        public new WQuery Remove(string name)
        {
            base.Remove(name);
            return this;
        }

        public Uri CreateUri()
        {
            return new Uri(BuildQuery(true));
        }

        public new WQuery Set(string name, object value)
        {
            base[name] = value == null ? "" : value.ToString();

            return this;
        }

        //public override string ToString()
        //{
        //    return BuildQuery();
        //}

        public string ToString(string name, object value)
        {
            var query = this.Clone();
            query.Set(name, value);

            return query.BuildQuery();
        }
    }
}
