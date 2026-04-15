using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from AdminCompetitionEdit.ascx (Apps/Integration/MusicCompetition).
    /// </summary>
    public class AdmincompetitioneditViewComponent : WViewComponent
    {
        public AdmincompetitioneditViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdmincompetitioneditViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class AdmincompetitioneditViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectOption> CboInterpretersOptions { get; set; } = new();
        public List<SelectOption> CboPeoplesChoiceOptions { get; set; } = new();
        public string Date { get; set; } = string.Empty;
        public string Judges { get; set; } = string.Empty;
        public bool Locked { get; set; } = false;
        public string Name { get; set; } = string.Empty;
        public string SelectedCboInterpreters { get; set; } = string.Empty;
        public string SelectedCboPeoplesChoice { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
        public bool VotingLocked { get; set; } = false;
    }
    }
