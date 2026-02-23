using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class IntRegisterV2ViewComponent : WViewComponent
    {
        public IntRegisterV2ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IntRegisterV2Model
            {
                ObjectId = WcmsContext.ObjectId
            };

            return View(model);
        }
    }

    public class IntRegisterV2Model
    {
        public int ObjectId { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool ShowOrganization { get; set; }
        public bool RequirePhone { get; set; }
    }
}
