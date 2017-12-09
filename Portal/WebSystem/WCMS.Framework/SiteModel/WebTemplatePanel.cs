using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;
using WCMS.Framework.Core.Manager;

namespace WCMS.Framework
{
    public class WebTemplatePanel : IWebObject
    {
        private static IWebTemplatePanelProvider _manager;

        static WebTemplatePanel()
        {
            _manager = WebObject.ResolveManager<WebTemplatePanel, IWebTemplatePanelProvider>(WebObject.ResolveProvider<WebTemplatePanel, IWebTemplatePanelProvider>());
        }

        public static IWebTemplatePanelProvider Provider { get { return _manager; } }

        public WebTemplatePanel()
        {
            Id = -1;
            Rank = 0;
        }

        /// <summary>
        /// TemplatePanelId
        /// </summary>
        [ObjectColumn(true)]
        public int Id { get; set; }

        [ObjectColumn]
        public string Name { get; set; }

        [ObjectColumn]
        public int RecordId { get; set; }

        [ObjectColumn]
        public int ObjectId { get; set; }

        [ObjectColumn]
        public string PanelName { get; set; }

        [ObjectColumn]
        public int Rank { get; set; }

        public WebTemplate Template { get { return RecordId > 0 && ObjectId==WebObjects.WebTemplate ? WebTemplate.Get(RecordId) : null; } }

        #region Static Methods

        public static WebTemplatePanel Get(int templatePanelId)
        {
            return _manager.Get(templatePanelId);
        }

        public int Update()
        {
            return _manager.Update(this);
        }

        public static bool Delete(int templatePanelId)
        {
            return _manager.Delete(templatePanelId);
        }

        #endregion

        #region IWebObject Members


        public int OBJECT_ID
        {
            get { return WebObjects.WebTemplatePanel; }
        }

        #endregion
    }
}
