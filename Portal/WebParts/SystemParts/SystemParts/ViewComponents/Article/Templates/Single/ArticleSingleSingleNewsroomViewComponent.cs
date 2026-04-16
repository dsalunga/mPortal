using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from Newsroom.ascx (SystemParts/Article/Templates/Single).
    /// </summary>
    public class ArticleSingleSingleNewsroomViewComponent : WViewComponent
    {
        public ArticleSingleSingleNewsroomViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ArticleSingleSingleNewsroomViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/Article/Templates/Single/Newsroom/Default.cshtml", model);
        }
    }

    public class ArticleSingleSingleNewsroomViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string TitleHtml { get; set; } = string.Empty;
        public string LinkHtml { get; set; } = string.Empty;
        public string DateHtml { get; set; } = string.Empty;
        public string DescriptionHtml { get; set; } = string.Empty;
        public string ImageHtml { get; set; } = string.Empty;
    }
}
