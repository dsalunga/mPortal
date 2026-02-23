using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class IntConfigExternalMembersViewComponent : WViewComponent
    {
        public IntConfigExternalMembersViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IntConfigExternalMembersModel
            {
                ObjectId = WcmsContext.ObjectId
            };

            return View(model);
        }
    }

    public class IntConfigExternalMembersModel
    {
        public int ObjectId { get; set; }
        public List<ExternalMemberItem> Members { get; set; }
        public string SearchTerm { get; set; }
    }

    public class ExternalMemberItem
    {
        public string MemberId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }
}
