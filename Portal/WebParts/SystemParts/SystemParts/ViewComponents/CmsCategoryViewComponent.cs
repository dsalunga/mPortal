using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from CMS_Category.ascx (SystemParts/Photo).
    /// </summary>
    public class CmsCategoryViewComponent : WViewComponent
    {
        public CmsCategoryViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new CmsCategoryViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class CmsCategoryViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string hObjectId { get; set; } = string.Empty;
        public string hRecordId { get; set; } = string.Empty;
        public List<object> ObjectDataSource2Data { get; set; } = new();
        public List<object> ObjectDataSource1Data { get; set; } = new();
        public List<SelectListItem> cboControlsItems { get; set; } = new();
        public string cboControlsSelected { get; set; } = string.Empty;
        public string txtAlbumColumns { get; set; } = string.Empty;
        public string txtAlbumPadding { get; set; } = string.Empty;
        public string txtThumbColumns { get; set; } = string.Empty;
        public string txtThumbRows { get; set; } = string.Empty;
        public string txtMaxPhotoWidth { get; set; } = string.Empty;
        public string lblStatus { get; set; } = string.Empty;
    }
}
