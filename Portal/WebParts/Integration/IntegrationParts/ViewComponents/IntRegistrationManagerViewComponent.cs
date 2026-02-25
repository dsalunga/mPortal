using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class IntRegistrationManagerViewComponent : WViewComponent
    {
        public IntRegistrationManagerViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IntRegistrationManagerModel
            {
                ObjectId = WcmsContext.ObjectId
            };

            return await Task.FromResult(View(model));
        }
    }

    public class IntRegistrationManagerModel
    {
        public int ObjectId { get; set; }
        public List<RegistrationItem> Registrations { get; set; }
        public string SearchTerm { get; set; }
        public int TotalCount { get; set; }
        public string FilterStatus { get; set; }
    }

    public class RegistrationItem
    {
        public string RegistrationId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string RegisteredDate { get; set; }
    }
}
