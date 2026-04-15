using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from BibleBrowser.ascx (Apps/Integration/BibleReader).
    /// </summary>
    public class BiblereaderBiblebrowserViewComponent : WViewComponent
    {
        public BiblereaderBiblebrowserViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new BiblereaderBiblebrowserViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class BiblereaderBiblebrowserViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectOption> CboBooksOptions { get; set; } = new();
        public List<SelectOption> CboChaptersOptions { get; set; } = new();
        public List<SelectOption> CboLanguagesOptions { get; set; } = new();
        public List<SelectOption> CboVersionsOptions { get; set; } = new();
        public string Search { get; set; } = string.Empty;
        public string SelectedCboBooks { get; set; } = string.Empty;
        public string SelectedCboChapters { get; set; } = string.Empty;
        public string SelectedCboLanguages { get; set; } = string.Empty;
        public string SelectedCboVersions { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
    }
