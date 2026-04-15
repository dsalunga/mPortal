using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from CCMS_Category.ascx (SystemParts/Photo).
    /// </summary>
    public class CcmsCategoryViewComponent : WViewComponent
    {
        public CcmsCategoryViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new CcmsCategoryViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class CcmsCategoryViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<object> SqlDataSource1Data { get; set; } = new();
        public string litID { get; set; } = string.Empty;
        public string txtTitle { get; set; } = string.Empty;
        public string txtFolderName { get; set; } = string.Empty;
        public string txtImageURL { get; set; } = string.Empty;
        public string txtWidth { get; set; } = string.Empty;
        public string txtPhotoHeight { get; set; } = string.Empty;
        public string lblNotify { get; set; } = string.Empty;
    }
}
