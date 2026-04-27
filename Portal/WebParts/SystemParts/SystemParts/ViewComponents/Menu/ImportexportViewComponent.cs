using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using WCMS.WebSystem.WebParts.Menu;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class ImportexportViewComponent : WViewComponent
    {
        private const string StatusKey = "Importexport.StatusMessage";
        private const string ErrorKey = "Importexport.ErrorMessage";

        public ImportexportViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var postController = WcmsContext.Element?.GetParameterValue("PostController");
            if (string.IsNullOrWhiteSpace(postController) ||
                postController.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                postController = "CentralPartActions";
            }

            var selectedMenuId = WcmsContext.GetId("MenuId");
            var model = new ImportexportViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                PostController = postController,
                PostAction = WcmsContext.Element?.GetParameterValue("PostAction") ?? "Importexport",
                SelectedMenuId = selectedMenuId
            };

            var menus = MenuEntity.Provider.GetList()
                .OrderBy(i => i.Name)
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Name,
                    Selected = i.Id == selectedMenuId
                })
                .ToList();
            model.Menus = menus;

            if (TempData.TryGetValue(StatusKey, out var status) && status != null)
                model.StatusMessage = status.ToString();

            if (TempData.TryGetValue(ErrorKey, out var error) && error != null)
                model.ErrorMessage = error.ToString();

            return View(model);
        }
    }

    public class ImportexportViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string PostController { get; set; } = "CentralPartActions";
        public string PostAction { get; set; } = "Importexport";
        public string StatusMessage { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public int SelectedMenuId { get; set; }
        public List<SelectListItem> Menus { get; set; } = new();

        public string cboMenus { get; set; } = string.Empty;
        public string ObjectDataSource1 { get; set; } = string.Empty;
        public string FileUpload1 { get; set; } = string.Empty;
        public string lblStatus { get; set; } = string.Empty;

    }
}
