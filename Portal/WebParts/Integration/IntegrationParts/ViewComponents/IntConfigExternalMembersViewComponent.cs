using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class IntConfigExternalMembersViewComponent : WViewComponent
    {
        public IntConfigExternalMembersViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IntConfigExternalMembersModel
            {
                ObjectId = WcmsContext.ObjectId
            };

            return await Task.FromResult(View(model));
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
