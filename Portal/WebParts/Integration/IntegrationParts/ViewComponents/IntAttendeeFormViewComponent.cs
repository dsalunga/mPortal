using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class IntAttendeeFormViewComponent : WViewComponent
    {
        public IntAttendeeFormViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IntAttendeeFormModel
            {
                ObjectId = WcmsContext.ObjectId
            };

            return await Task.FromResult(View(model));
        }
    }

    public class IntAttendeeFormModel
    {
        public int ObjectId { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string AttendeeName { get; set; }
    }
}
