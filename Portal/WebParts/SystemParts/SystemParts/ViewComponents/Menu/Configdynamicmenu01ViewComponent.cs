using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from ConfigDynamicMenu01.ascx (SystemParts/Menu).
    /// </summary>
    public class Configdynamicmenu01ViewComponent : WViewComponent
    {
        public Configdynamicmenu01ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new Configdynamicmenu01ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/Menu/ConfigDynamicMenu01/Default.cshtml", model);
        }
    }

        public class Configdynamicmenu01ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectOption> CboMenuOptions { get; set; } = new();
        public List<SelectOption> CboOrientationOptions { get; set; } = new();
        public List<SelectOption> CboRenderModeOptions { get; set; } = new();
        public string Height { get; set; } = string.Empty;
        public string SelectedCboMenu { get; set; } = string.Empty;
        public string SelectedCboOrientation { get; set; } = string.Empty;
        public string SelectedCboRenderMode { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
        public string Width { get; set; } = string.Empty;
    }
    }
