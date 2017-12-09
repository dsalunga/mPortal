using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;
using WCMS.Framework.Core.Manager;

namespace WCMS.Framework
{
    public class WebPagePanel : IWebObject, ISelfManager
    {
        private static IWebPagePanelProvider _manager;

        static WebPagePanel()
        {
            _manager = WebObject.ResolveManager<WebPagePanel, IWebPagePanelProvider>(WebObject.ResolveProvider<WebPagePanel, IWebPagePanelProvider>());
        }

        public WebPagePanel()
        {
            Id = -1;
            TemplatePanelId = -1;
            PageId = -1;
            UsageTypeId = -1;
        }

        [ObjectColumn(true)]
        public int Id { get; set; }

        [ObjectColumn]
        public int TemplatePanelId { get; set; }

        [ObjectColumn]
        public int PageId { get; set; }

        [ObjectColumn]
        public int UsageTypeId { get; set; }

        public int Update()
        {
            return _manager.Update(this);
        }

        public bool Inherit
        {
            get { return UsageTypeId == PanelUsage.Inherit; }
        }

        public bool Add
        {
            get { return UsageTypeId == PanelUsage.Add; }
        }

        public bool Override
        {
            get { return UsageTypeId == PanelUsage.Override; }
        }

        public static WebPagePanel Get(int pagePanelId)
        {
            return _manager.Get(pagePanelId);
        }

        public static IEnumerable<WebPagePanel> GetList(int pageId)
        {
            return _manager.GetList(pageId);
        }

        public static WebPagePanel Get(int templatePanelId, int pageId)
        {
            return _manager.Get(templatePanelId, pageId);
        }

        public static bool Delete(int pagePanelId)
        {
            return _manager.Delete(pagePanelId);
        }

        #region IWebObject Members


        public int OBJECT_ID
        {
            get { return WebObjects.WebPagePanel; }
        }

        #endregion


        public bool Delete()
        {
            return Delete(this.Id);
        }
    }
}
