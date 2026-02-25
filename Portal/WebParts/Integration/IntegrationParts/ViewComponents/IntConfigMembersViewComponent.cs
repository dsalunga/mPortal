using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class IntConfigMembersViewComponent : WViewComponent
    {
        public IntConfigMembersViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IntConfigMembersModel
            {
                ObjectId = WcmsContext.ObjectId
            };

            return await Task.FromResult(View(model));
        }
    }

    public class IntConfigMembersModel
    {
        public int ObjectId { get; set; }
        public List<MemberConfigItem> Members { get; set; }
        public string SearchTerm { get; set; }
        public int TotalCount { get; set; }
    }

    public class MemberConfigItem
    {
        public string MemberId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }
}
