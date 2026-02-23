using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class IntChangePasswordViewComponent : WViewComponent
    {
        public IntChangePasswordViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IntChangePasswordModel
            {
                ObjectId = WcmsContext.ObjectId
            };

            return View(model);
        }
    }

    public class IntChangePasswordModel
    {
        public int ObjectId { get; set; }
        public string UserName { get; set; }
        public bool RequireCurrentPassword { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
