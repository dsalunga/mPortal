using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    public class WebSiteIdentity : WebObjectBase, ISelfManager
    {
        private static IWebSiteIdentityProvider _provider;

        static WebSiteIdentity()
        {
            _provider = WebObject.ResolveManager<WebSiteIdentity, IWebSiteIdentityProvider>(WebObject.ResolveProvider<WebSiteIdentity, IWebSiteIdentityProvider>());
        }

        public WebSiteIdentity()
        {
            SiteId = -1;
            ProtocolId = 0;
            Port = 80;

            HostName = string.Empty;
            UrlPath = string.Empty;
            IPAddress = string.Empty;
            RedirectUrl = string.Empty;
        }

        [ObjectColumn]
        public int SiteId { get; set; }

        [ObjectColumn]
        public string HostName { get; set; }

        [ObjectColumn]
        public string UrlPath { get; set; }

        [ObjectColumn]
        public int Port { get; set; }

        [ObjectColumn]
        public string IPAddress { get; set; }

        [ObjectColumn]
        public string RedirectUrl { get; set; }

        [ObjectColumn]
        public int ProtocolId { get; set; }

        public WSite Site
        {
            get { return WSite.Get(SiteId); }
        }

        public string Build(bool includePath = false)
        {
            var protocol = ProtocolId == 0 ? "http" : "https";
            var port = Port == 80 && ProtocolId == 0 || Port == 443 && ProtocolId == 1 ? "" : ":" + Port;
            return string.Format("{0}://{1}{2}{3}", protocol, HostName, port, includePath ? UrlPath : "");
        }

        public override int OBJECT_ID
        {
            get { return WebObjects.WebSiteIdentity; }
        }

        public static IWebSiteIdentityProvider Provider
        {
            get { return _provider; }
        }

        public static int GetDefaultSite(string hostName, int port)
        {
            var item = _provider.GetList().FirstOrDefault(i => i.HostName.Equals(hostName, StringComparison.InvariantCultureIgnoreCase) && port == i.Port);

            return item != null ? item.SiteId : WConfig.DefaultSite.Id;
        }


        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(Id);
        }

        #endregion
    }
}
