using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminMenuEdit.ascx (SystemParts/Menu).
    /// </summary>
    public class AdminmenueditViewComponent : WViewComponent
    {
        public AdminmenueditViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdminmenueditViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/Menu/AdminMenuEdit/Default.cshtml", model);
        }
    }

        public class AdminmenueditViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Caption { get; set; } = string.Empty;
        public List<SelectOption> CboSitesOptions { get; set; } = new();
        public bool IsActive { get; set; }
        public string SelectedCboSites { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
    }
