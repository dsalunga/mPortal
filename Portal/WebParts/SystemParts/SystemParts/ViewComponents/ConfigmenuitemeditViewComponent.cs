using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from ConfigMenuItemEdit.ascx (SystemParts/Menu).
    /// </summary>
    public class ConfigmenuitemeditViewComponent : WViewComponent
    {
        public ConfigmenuitemeditViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ConfigmenuitemeditViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class ConfigmenuitemeditViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Caption { get; set; } = string.Empty;
        public bool CheckPermission { get; set; } = false;
        public bool IsActive { get; set; }
        public string NavigateURL { get; set; } = string.Empty;
        public List<SelectOption> ObjectDataSource1Options { get; set; } = new();
        public string Rank { get; set; } = string.Empty;
        public string SelectedObjectDataSource1 { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
    }
