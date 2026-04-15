using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from FrontNews.ascx (SystemParts/Article/Templates/Multiple).
    /// </summary>
    public class ArticleMultipleMultiFrontnewsViewComponent : WViewComponent
    {
        public ArticleMultipleMultiFrontnewsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ArticleMultipleMultiFrontnewsViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class ArticleMultipleMultiFrontnewsViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string TitleHtml { get; set; } = string.Empty;
        public string LinkHtml { get; set; } = string.Empty;
        public string DescriptionHtml { get; set; } = string.Empty;
        public string ImageHtml { get; set; } = string.Empty;
    }
}
