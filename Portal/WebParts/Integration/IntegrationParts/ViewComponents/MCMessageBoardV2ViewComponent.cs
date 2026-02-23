using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class MCMessageBoardV2ViewComponent : WViewComponent
    {
        public MCMessageBoardV2ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new MCMessageBoardV2Model
            {
                ObjectId = WcmsContext.ObjectId
            };

            return View(model);
        }
    }

    public class MCMessageBoardV2Model
    {
        public int ObjectId { get; set; }
    }
}
