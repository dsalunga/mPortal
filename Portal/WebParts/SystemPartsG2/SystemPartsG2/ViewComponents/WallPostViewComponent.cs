using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class WallPostViewComponent : WViewComponent
    {
        public WallPostViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new WallPostViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                RecordId = WcmsContext.RecordId,
                Author = string.Empty,
                Content = string.Empty,
                DatePosted = string.Empty,
                LikeCount = 0,
                Comments = new List<WallPostComment>()
            };

            return await Task.FromResult(View(model));
        }
    }

    public class WallPostViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string DatePosted { get; set; }
        public int LikeCount { get; set; }
        public List<WallPostComment> Comments { get; set; }
    }

    public class WallPostComment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public string DatePosted { get; set; }
    }
}
