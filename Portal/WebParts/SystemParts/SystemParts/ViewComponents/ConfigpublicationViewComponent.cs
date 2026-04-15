using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from ConfigPublication.ascx (SystemParts/Article).
    /// </summary>
    public class ConfigpublicationViewComponent : WViewComponent
    {
        public ConfigpublicationViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ConfigpublicationViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class ConfigpublicationViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string hidObjectId { get; set; } = string.Empty;
        public string hidRecordId { get; set; } = string.Empty;
        public List<object> ObjectDataSourceInsertedArticlesData { get; set; } = new();
        public List<SelectListItem> cboSitesItems { get; set; } = new();
        public string cboSitesSelected { get; set; } = string.Empty;
        public List<object> ObjectDataSourceAllArticlesData { get; set; } = new();
        public string txtTitle { get; set; } = string.Empty;
        public List<SelectListItem> ddlMonthItems { get; set; } = new();
        public string ddlMonthSelected { get; set; } = string.Empty;
        public List<SelectListItem> ddlDayItems { get; set; } = new();
        public string ddlDaySelected { get; set; } = string.Empty;
        public List<SelectListItem> ddlYearItems { get; set; } = new();
        public string ddlYearSelected { get; set; } = string.Empty;
        public string txtTags { get; set; } = string.Empty;
        public List<SelectListItem> cboCommentOnItems { get; set; } = new();
        public string cboCommentOnSelected { get; set; } = string.Empty;
        public bool chkSendEmail { get; set; }
        public string litID { get; set; } = string.Empty;
        public string txtAuthor { get; set; } = string.Empty;
        public List<SelectListItem> ddlRankItems { get; set; } = new();
        public string ddlRankSelected { get; set; } = string.Empty;
        public bool chkIsActive { get; set; }
        public List<SelectListItem> cboSiteIdItems { get; set; } = new();
        public string cboSiteIdSelected { get; set; } = string.Empty;
        public string txtDateFormat { get; set; } = string.Empty;
        public string txtPageSize { get; set; } = string.Empty;
        public bool chkComments { get; set; }
        public List<SelectListItem> ObjectDataSourceTemplatesItems { get; set; } = new();
        public string ObjectDataSourceTemplatesSelected { get; set; } = string.Empty;
        public string lblStatus { get; set; } = string.Empty;
    }
}
