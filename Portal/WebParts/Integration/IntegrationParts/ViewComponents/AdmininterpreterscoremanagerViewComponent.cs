using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from AdminInterpreterScoreManager.ascx (Apps/Integration/MusicCompetition).
    /// </summary>
    public class AdmininterpreterscoremanagerViewComponent : WViewComponent
    {
        public AdmininterpreterscoremanagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdmininterpreterscoremanagerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class AdmininterpreterscoremanagerViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<AdmininterpreterscoremanagerItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class AdmininterpreterscoremanagerItem
    {
        public int Id { get; set; }
        public string Interpretation { get; set; } = string.Empty;
        public string Interpreter { get; set; } = string.Empty;
        public string Judge { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string OverallImpact { get; set; } = string.Empty;
        public string StagePresence { get; set; } = string.Empty;
        public string Total { get; set; } = string.Empty;
        public string VoiceQuality { get; set; } = string.Empty;
    }
}
