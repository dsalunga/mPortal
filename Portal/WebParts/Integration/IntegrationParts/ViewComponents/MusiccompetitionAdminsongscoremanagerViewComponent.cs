using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from AdminSongScoreManager.ascx (Apps/Integration/MusicCompetition).
    /// </summary>
    public class MusiccompetitionAdminsongscoremanagerViewComponent : WViewComponent
    {
        public MusiccompetitionAdminsongscoremanagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new MusiccompetitionAdminsongscoremanagerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class MusiccompetitionAdminsongscoremanagerViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<MusiccompetitionAdminsongscoremanagerItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class MusiccompetitionAdminsongscoremanagerItem
    {
        public string Entry { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Judge { get; set; } = string.Empty;
        public string LyricsMessage { get; set; } = string.Empty;
        public string Musicality { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string OverallImpact { get; set; } = string.Empty;
        public string Total { get; set; } = string.Empty;
    }
}
