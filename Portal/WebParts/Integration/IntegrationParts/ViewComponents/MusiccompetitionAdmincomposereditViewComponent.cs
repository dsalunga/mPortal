using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from AdminComposerEdit.ascx (Apps/Integration/MusicCompetition).
    /// </summary>
    public class MusiccompetitionAdmincomposereditViewComponent : WViewComponent
    {
        public MusiccompetitionAdmincomposereditViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new MusiccompetitionAdmincomposereditViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class MusiccompetitionAdmincomposereditViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool Active { get; set; } = false;
        public List<SelectOption> CboCompetitionOptions { get; set; } = new();
        public string Entry { get; set; } = string.Empty;
        public string Locale { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string NickName { get; set; } = string.Empty;
        public string PhotoFile { get; set; } = string.Empty;
        public string SelectedCboCompetition { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
        public string Work { get; set; } = string.Empty;
    }
    }
