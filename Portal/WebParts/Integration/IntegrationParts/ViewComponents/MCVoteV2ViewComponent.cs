using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class MCVoteV2ViewComponent : WViewComponent
    {
        public MCVoteV2ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new MCVoteV2Model
            {
                ObjectId = WcmsContext.ObjectId
            };

            return View(model);
        }
    }

    public class MCVoteV2Model
    {
        public int ObjectId { get; set; }
    }
}
