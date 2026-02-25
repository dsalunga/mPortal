using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class WallViewComponent : WViewComponent
    {
        public WallViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new WallViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                Posts = new List<WallPostItem>(),
                AllowNewPosts = true
            };

            return await Task.FromResult(View(model));
        }
    }

    public class WallViewModel
    {
        public int ObjectId { get; set; }
        public List<WallPostItem> Posts { get; set; }
        public bool AllowNewPosts { get; set; }
    }

    public class WallPostItem
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string DatePosted { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
    }
}
