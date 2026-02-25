using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class IntMemberVisitViewViewComponent : WViewComponent
    {
        public IntMemberVisitViewViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IntMemberVisitViewModel
            {
                ObjectId = WcmsContext.ObjectId
            };

            return await Task.FromResult(View(model));
        }
    }

    public class IntMemberVisitViewModel
    {
        public int ObjectId { get; set; }
        public List<MemberVisitItem> Visits { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }
    }

    public class MemberVisitItem
    {
        public string VisitId { get; set; }
        public string VisitDate { get; set; }
        public string Notes { get; set; }
        public string RecordedBy { get; set; }
    }
}
