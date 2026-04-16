using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Text file editor. Replaces FileEditor.ascx (SystemParts/FileManager).
    /// Usage: @await Component.InvokeAsync("FileEditor", new { objectId, recordId })
    /// </summary>
    public class FileEditorViewComponent : WViewComponent
    {
        public FileEditorViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new FileEditorViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                IsLoggedIn = WcmsSession.IsLoggedIn,
                Content = string.Empty
            };

            return View("~/Views/Shared/Components/FileManager/FileEditor/Default.cshtml", model);
        }
    }

    public class FileEditorViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
