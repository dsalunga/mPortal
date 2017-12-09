using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public class WebTheme : ParameterizedWebObject, ISelfManager
    {
        private static IWebThemeProvider _manager;

        static WebTheme()
        {
            _manager = WebObject.ResolveManager<WebTheme, IWebThemeProvider>(WebObject.ResolveProvider<WebTheme, IWebThemeProvider>());
        }

        public WebTheme()
        {
            TemplateId = -1;
            SkinId = -1;
            ParentId = -1;
        }

        public int TemplateId { get; set; }
        public int ParentId { get; set; }
        public string Identity { get; set; }
        public int SkinId { get; set; }

        public WebTemplate Template { get { return TemplateId > 0 ? WebTemplate.Get(TemplateId) : null; } }
        public WebTheme Parent { get { return ParentId > 0 ? WebTheme.Provider.Get(ParentId) : null; } }
        public WebSkin Skin { get { return SkinId > 0 ? WebSkin.Provider.Get(SkinId) : null; } }

        public override int OBJECT_ID
        {
            get { return WebObjects.WebTheme; }
        }

        public static IWebThemeProvider Provider { get { return _manager; } }

        #region ISelfManager Members

        public int Update()
        {
            return _manager.Update(this);
        }

        public bool Delete()
        {
            return _manager.Delete(this.Id);
        }

        #endregion
    }
}
