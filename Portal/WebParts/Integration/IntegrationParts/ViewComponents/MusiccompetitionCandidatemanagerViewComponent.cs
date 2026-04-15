using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from CandidateManager.ascx (Apps/Integration/MusicCompetition).
    /// </summary>
    public class MusiccompetitionCandidatemanagerViewComponent : WViewComponent
    {
        public MusiccompetitionCandidatemanagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new MusiccompetitionCandidatemanagerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class MusiccompetitionCandidatemanagerViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<MusiccompetitionCandidatemanagerItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class MusiccompetitionCandidatemanagerItem
    {
        public string Competition { get; set; } = string.Empty;
        public string ComposerLyricist { get; set; } = string.Empty;
        public string Entry { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Interpreter { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PhotoFile { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;
        public string SourceUrl { get; set; } = string.Empty;
    }
}
