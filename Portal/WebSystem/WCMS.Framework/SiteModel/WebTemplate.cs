using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WCMS.Common.Utilities;

using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public class WebTemplate : ParameterizedWebObject, ISelfManager
    {
        private static IWebTemplateProvider _manager;

        static WebTemplate()
        {
            _manager = WebObject.ResolveManager<WebTemplate, IWebTemplateProvider>(WebObject.ResolveProvider<WebTemplate, IWebTemplateProvider>());
        }

        public WebTemplate()
        {
            PrimaryPanelId = -1;
            SkinId = -1;
            ThemeId = -1;
            ParentId = -1;
            TemplateEngineId = 1;

            VersionOf = -1;
            Version = 0;

            Standalone = 0;

            DateModified = DateTime.Now;

            Content = string.Empty;
        }


        #region Properties

        [ObjectColumn]
        public string FileName { get; set; }

        [ObjectColumn]
        public string Identity { get; set; }

        [ObjectColumn]
        public int PrimaryPanelId { get; set; }

        [ObjectColumn]
        public int Version { get; set; }

        [ObjectColumn]
        public int VersionOf { get; set; }

        [ObjectColumn]
        public string Content { get; set; }

        [ObjectColumn]
        public DateTime DateModified { get; set; }

        [ObjectColumn]
        public int SkinId { get; set; }

        [ObjectColumn]
        public int Standalone { get; set; }

        [ObjectColumn]
        public int ParentId { get; set; }

        [ObjectColumn]
        public int ThemeId { get; set; }

        [ObjectColumn]
        public int TemplateEngineId { get; set; }

        public string GetRelativePath()
        {
            return string.Format("~/Content/Themes/{0}/{1}", this.Identity, this.FileName);
        }

        public bool IsLatestVersion { get { return VersionOf == 1; } }

        public bool IsStandalone
        {
            get { return Standalone == 1; }
            set { Standalone = value ? 1 : 0; }
        }

        public WebTemplate Parent
        {
            get { return ParentId > 0 ? WebTemplate.Get(ParentId) : null; }
            set { ParentId = value == null ? -1 : value.Id; }
        }

        public WebSkin Skin { get { return WebSkin.Provider.Get(this.SkinId); } }
        public WebTheme Theme { get { return WebTheme.Provider.Get(this.ThemeId); } }

        public WebTemplatePanel PrimaryPanel { get { return PrimaryPanelId > 0 ? WebTemplatePanel.Get(PrimaryPanelId) : null; } }

        //public WebSkin GetPrimaryTheme()
        //{
        //    return this.SkinId > 0 ? WebSkin.Provider.Get(this.SkinId) : null;
        //}

        public IEnumerable<WebTemplatePanel> Panels { get { return WebTemplatePanel.Provider.GetList(WebObjects.WebTemplate, Id); } }

        #endregion


        #region Static Methods

        public static WebTemplate Get(int templateId)
        {
            return _manager.Get(templateId);
        }

        //public static IEnumerable<WebTemplate> GetList()
        //{
        //    return _manager.GetList();
        //}

        public static bool Delete(int templateId)
        {
            return _manager.Delete(templateId);
        }

        public static IWebTemplateProvider Provider { get { return _manager; } }

        #endregion

        public int Update()
        {
            return _manager.Update(this);
        }

        public bool Delete()
        {
            return _manager.Delete(this.Id);
        }

        public override int OBJECT_ID
        {
            get { return WebObjects.WebTemplate; }
        }
    }
}
