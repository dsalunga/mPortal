using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from content_media_02.ascx (AppBundle2/Media).
    /// </summary>
    public class ContentMedia02ViewComponent : WViewComponent
    {
        public ContentMedia02ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ContentMedia02ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/Media/content_media_02/ContentMedia02/Default.cshtml", model);
        }
    }

        public class ContentMedia02ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool Active { get; set; } = false;
        public string Agency { get; set; } = string.Empty;
        public List<SelectOption> CboRankOptions { get; set; } = new();
        public List<SelectOption> CboSitesOptions { get; set; } = new();
        public string Filename { get; set; } = string.Empty;
        public string Length { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SelectedCboRank { get; set; } = string.Empty;
        public string SelectedCboSites { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
    }
    }
