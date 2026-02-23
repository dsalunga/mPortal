using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class IntConventionRegistrationViewComponent : WViewComponent
    {
        public IntConventionRegistrationViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IntConventionRegistrationModel
            {
                ObjectId = WcmsContext.ObjectId
            };

            return View(model);
        }
    }

    public class IntConventionRegistrationModel
    {
        public int ObjectId { get; set; }
        public string EventName { get; set; }
        public string EventDate { get; set; }
        public string Location { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> SessionOptions { get; set; }
    }
}
