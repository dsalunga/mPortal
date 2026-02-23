using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class IntConfigMemberLinksViewComponent : WViewComponent
    {
        public IntConfigMemberLinksViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IntConfigMemberLinksModel
            {
                ObjectId = WcmsContext.ObjectId
            };

            return View(model);
        }
    }

    public class IntConfigMemberLinksModel
    {
        public int ObjectId { get; set; }
        public List<MemberLinkItem> Links { get; set; }
        public string SearchTerm { get; set; }
    }

    public class MemberLinkItem
    {
        public string LinkId { get; set; }
        public string MemberId { get; set; }
        public string LinkType { get; set; }
        public string LinkedMemberId { get; set; }
        public string Status { get; set; }
    }
}
