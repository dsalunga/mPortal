using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from CCMS_Gallery_02.ascx (SystemParts/Photo).
    /// </summary>
    public class CcmsGallery02ViewComponent : WViewComponent
    {
        public CcmsGallery02ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new CcmsGallery02ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class CcmsGallery02ViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectListItem> SqlDataSource2Items { get; set; } = new();
        public string SqlDataSource2Selected { get; set; } = string.Empty;
        public List<object> ObjectDataSource1Data { get; set; } = new();
        public string litID { get; set; } = string.Empty;
        public string txtCaption { get; set; } = string.Empty;
        public string txtThumbnail { get; set; } = string.Empty;
        public string txtImageURL { get; set; } = string.Empty;
        public List<SelectListItem> ddlSitesItems { get; set; } = new();
        public string ddlSitesSelected { get; set; } = string.Empty;
        public bool chkIsActive { get; set; }
        public string litDateCreated { get; set; } = string.Empty;
        public string txtPhotoCollection { get; set; } = string.Empty;
        public string lblBatchUploadStatus { get; set; } = string.Empty;
        public string lblNotify { get; set; } = string.Empty;
    }
}
