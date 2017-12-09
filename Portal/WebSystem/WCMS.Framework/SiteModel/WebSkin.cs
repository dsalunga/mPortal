using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public class WebSkin : ParameterizedWebObject, ISelfManager
    {
        private static IWebSkinProvider _manager;

        static WebSkin()
        {
            _manager = WebObject.ResolveManager<WebSkin, IWebSkinProvider>(WebObject.ResolveProvider<WebSkin, IWebSkinProvider>());
        }

        public WebSkin()
        {
            //TemplateId = -1;
            Rank = 0;
        }

        //public int TemplateId { get; set; }
        public int Rank { get; set; }

        public int ObjectId { get; set; }
        public int RecordId { get; set; }

        //public WebTemplate Template { get { return TemplateId > 0 ? WebTemplate.Get(TemplateId) : null; } }

        public override int OBJECT_ID
        {
            get { return WebObjects.WebSkin; }
        }

        public static IWebSkinProvider Provider { get { return _manager; } }

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
