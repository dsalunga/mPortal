using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from AdminCompetitionManager.ascx (Apps/Integration/MusicCompetition).
    /// </summary>
    public class MusiccompetitionAdmincompetitionmanagerViewComponent : WViewComponent
    {
        public MusiccompetitionAdmincompetitionmanagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new MusiccompetitionAdmincompetitionmanagerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class MusiccompetitionAdmincompetitionmanagerViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<MusiccompetitionAdmincompetitionmanagerItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class MusiccompetitionAdmincompetitionmanagerItem
    {
        public string CompetitionDate { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Judges { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ScoreLocked { get; set; } = string.Empty;
        public string VotingLocked { get; set; } = string.Empty;
    }
}
