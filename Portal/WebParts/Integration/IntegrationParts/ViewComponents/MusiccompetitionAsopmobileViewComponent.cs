using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from ASOPMobile.ascx (Apps/Integration/MusicCompetition).
    /// </summary>
    public class MusiccompetitionAsopmobileViewComponent : WViewComponent
    {
        public MusiccompetitionAsopmobileViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new MusiccompetitionAsopmobileViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class MusiccompetitionAsopmobileViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<MusiccompetitionAsopmobileItem> Items { get; set; } = new();
    }

    public class MusiccompetitionAsopmobileItem
    {
        public string Entry { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
