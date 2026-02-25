using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class IntGroupUserManagerViewComponent : WViewComponent
    {
        public IntGroupUserManagerViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IntGroupUserManagerModel
            {
                ObjectId = WcmsContext.ObjectId
            };

            return await Task.FromResult(View(model));
        }
    }

    public class IntGroupUserManagerModel
    {
        public int ObjectId { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string GroupName { get; set; }
    }
}
