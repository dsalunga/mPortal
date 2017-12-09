using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public class WebPartControlTemplate : SecurableObject
    {
        private static IWebPartControlTemplateProvider _manager;

        static WebPartControlTemplate()
        {
            _manager = WebObject.ResolveManager<WebPartControlTemplate, IWebPartControlTemplateProvider>(WebObject.ResolveProvider<WebPartControlTemplate, IWebPartControlTemplateProvider>());
        }

        public WebPartControlTemplate()
        {
            Id = -1;
            PartControlId = -1;
            Standalone = 0;
            TemplateEngineId = TemplateEngineTypes.ASPX;
        }

        public WebPartControlTemplate(WebPartControl control)
            : this()
        {
            PartControlId = control.Id;
        }

        [ObjectColumn]
        public int PartControlId { get; set; }

        [ObjectColumn]
        public string FileName { get; set; }

        [ObjectColumn]
        public string Identity { get; set; }

        [ObjectColumn]
        public string Path { get; set; }

        [ObjectColumn]
        public int Standalone { get; set; }

        [ObjectColumn]
        public int TemplateEngineId { get; set; }

        public WebPartControl PartControl { get { return WebPartControl.Get(PartControlId); } }
        public WPart Part { get { return PartControl.Part; } }
        public bool IsStandalone { get { return Standalone == 1; } set { Standalone = value ? 1 : 0; } }
        public IEnumerable<WebTemplatePanel> Panels { get { return WebTemplatePanel.Provider.GetList(WebObjects.WebPartControlTemplate, Id); } }

        public string GetRenderPath()
        {
            if (Path.StartsWith("~") || Path.StartsWith("/"))
                return Path;

            return string.Format(WConstants.PART_RENDER_PATH_FORMAT, Part.Identity, Path);
        }

        public int Update()
        {
            return _manager.Update(this);
        }

        public bool Delete()
        {
            this.DeleteRelatedObjects();
            this.DeleteSecurityObjects();

            return _manager.Delete(this.Id);
        }

        public static WebPartControlTemplate Get(int partControlTemplateId)
        {
            return _manager.Get(partControlTemplateId);
        }

        public static WebPartControlTemplate Get(int partControlId, string identity)
        {
            return _manager.Get(partControlId, identity);
        }

        public static IEnumerable<WebPartControlTemplate> GetList(int partControlId)
        {
            return _manager.GetList(partControlId);
        }

        public static bool Delete(int partControlTemplateId)
        {
            var template = WebPartControlTemplate.Get(partControlTemplateId);
            if (template != null)
                return template.Delete();

            return false;
        }

        #region IWebObject Members


        public override int OBJECT_ID
        {
            get { return WebObjects.WebPartControlTemplate; }
        }

        #endregion
    }
}
