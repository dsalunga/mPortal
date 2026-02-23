using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class AjaxScriptManagerViewComponent : WViewComponent
    {
        public AjaxScriptManagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AjaxScriptManagerViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                Scripts = new List<ScriptReference>(),
                EnableAsync = true
            };

            return View(model);
        }
    }

    public class AjaxScriptManagerViewModel
    {
        public int ObjectId { get; set; }
        public List<ScriptReference> Scripts { get; set; }
        public bool EnableAsync { get; set; }
    }

    public class ScriptReference
    {
        public string Src { get; set; }
        public bool IsAsync { get; set; }
        public bool IsDefer { get; set; }
        public int LoadOrder { get; set; }
    }
}
