using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class CommentsViewComponent : WViewComponent
    {
        public CommentsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new CommentsViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                RecordId = WcmsContext.RecordId,
                Comments = new List<CommentItem>(),
                AllowNewComments = true
            };

            return View(model);
        }
    }

    public class CommentsViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<CommentItem> Comments { get; set; }
        public bool AllowNewComments { get; set; }
    }

    public class CommentItem
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public string DatePosted { get; set; }
    }
}
