using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from Setup.ascx (Apps/Integration/Streaming).
    /// </summary>
    public class StreamingSetupViewComponent : WViewComponent
    {
        public StreamingSetupViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new StreamingSetupViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class StreamingSetupViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectOption> CboDurationOptions { get; set; } = new();
        public List<SelectOption> CboStreamTypeOptions { get; set; } = new();
        public string SelectedCboDuration { get; set; } = string.Empty;
        public string SelectedCboStreamType { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
    }
