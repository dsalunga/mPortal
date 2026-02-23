using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Container layout designer. Replaces ContainerDesigner.ascx (SystemParts/Container).
    /// Usage: @await Component.InvokeAsync("ContainerDesigner", new { objectId, recordId })
    /// </summary>
    public class ContainerDesignerViewComponent : WViewComponent
    {
        public ContainerDesignerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ContainerDesignerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                Columns = new List<ContainerColumnModel>(),
                LayoutType = "two-column",
                IsLoggedIn = WcmsSession.IsLoggedIn
            };

            return View(model);
        }
    }

    public class ContainerDesignerViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string LayoutType { get; set; }
        public bool IsLoggedIn { get; set; }
        public List<ContainerColumnModel> Columns { get; set; }
    }

    public class ContainerColumnModel
    {
        public int Index { get; set; }
        public int Width { get; set; }
        public string CssClass { get; set; }
        public string Content { get; set; }
    }
}
