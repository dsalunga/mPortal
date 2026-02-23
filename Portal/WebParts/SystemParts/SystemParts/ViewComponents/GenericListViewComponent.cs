using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Configurable data list. Replaces GenericList.ascx (SystemParts/GenericList).
    /// Usage: @await Component.InvokeAsync("GenericList", new { objectId, recordId })
    /// </summary>
    public class GenericListViewComponent : WViewComponent
    {
        public GenericListViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new GenericListViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                Columns = new List<GenericListColumn>(),
                Rows = new List<Dictionary<string, string>>(),
                BasePath = WcmsContext.BasePath
            };

            return View(model);
        }
    }

    public class GenericListViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Title { get; set; }
        public string BasePath { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalRecords { get; set; }
        public List<GenericListColumn> Columns { get; set; }
        public List<Dictionary<string, string>> Rows { get; set; }
    }

    public class GenericListColumn
    {
        public string Name { get; set; }
        public string Header { get; set; }
        public string DataType { get; set; }
        public bool Sortable { get; set; }
        public bool Visible { get; set; }
    }
}
