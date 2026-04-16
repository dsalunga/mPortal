using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminRemoteLibraryEdit.ascx (SystemParts/FileManager).
    /// </summary>
    public class AdminremotelibraryeditViewComponent : WViewComponent
    {
        public AdminremotelibraryeditViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdminremotelibraryeditViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/FileManager/AdminRemoteLibraryEdit/Default.cshtml", model);
        }
    }

        public class AdminremotelibraryeditViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool Active { get; set; } = false;
        public string BaseAddress { get; set; } = string.Empty;
        public List<SelectOption> CboSourceTypeOptions { get; set; } = new();
        public string DisplayBaseAddress { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SelectedCboSourceType { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
    }
