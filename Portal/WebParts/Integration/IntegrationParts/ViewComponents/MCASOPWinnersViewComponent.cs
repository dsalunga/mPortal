using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class MCASOPWinnersViewComponent : WViewComponent
    {
        public MCASOPWinnersViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new MCASOPWinnersModel
            {
                ObjectId = WcmsContext.ObjectId
            };

            return await Task.FromResult(View(model));
        }
    }

    public class MCASOPWinnersModel
    {
        public int ObjectId { get; set; }
    }
}
