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

            return View(model);
        }
    }

    public class AdminremotelibraryeditViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string txtName { get; set; } = string.Empty;
        public List<SelectListItem> cboSourceTypeItems { get; set; } = new();
        public string cboSourceTypeSelected { get; set; } = string.Empty;
        public bool chkActive { get; set; }
        public string txtBaseAddress { get; set; } = string.Empty;
        public string txtUserName { get; set; } = string.Empty;
        public string txtPassword { get; set; } = string.Empty;
        public string txtDisplayBaseAddress { get; set; } = string.Empty;
    }
}
