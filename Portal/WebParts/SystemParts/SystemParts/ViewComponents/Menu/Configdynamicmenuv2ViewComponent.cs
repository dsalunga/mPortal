using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.ViewComponents;
using WCMS.WebSystem.WebParts.Menu;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class Configdynamicmenuv2ViewComponent : WViewComponent
    {
        private const string StatusKey = "Configdynamicmenuv2.StatusMessage";
        private const string ErrorKey = "Configdynamicmenuv2.ErrorMessage";

        public Configdynamicmenuv2ViewComponent(IWContext context) : base(context) { }

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

            var model = new Configdynamicmenuv2ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                PostController = postController,
                PostAction = WcmsContext.Element?.GetParameterValue("PostAction") ?? "Configdynamicmenuv2"
            };

            var query = WcmsContext.Query ?? new WQuery(WSession.Context);
            var manageMenuQuery = query.Clone();
            manageMenuQuery.SetCmd("AdminMenu.ascx");
            model.ManageMenuUrl = manageMenuQuery.BuildQuery();

            var element = WcmsContext.Element as ParameterizedWebObject;
            var selectedMenuId = element != null
                ? DataUtil.GetId(element.GetParameterValue(ParameterKeys.MenuId))
                : -1;
            model.SelectedMenuId = selectedMenuId;

            var currentParameterSetName = element?.GetParameterValue(WConstants.ParameterSetKey) ?? string.Empty;
            var parameterSets = WebParameterSet.Provider.GetList().OrderBy(i => i.Name).ToList();
            model.ParameterSets = parameterSets.Select(set => new SelectListItem
            {
                Value = set.Name,
                Text = set.Name,
                Selected = set.Name.Equals(currentParameterSetName, StringComparison.OrdinalIgnoreCase)
            }).ToList();

            model.Menus = MenuEntity.Provider.GetList()
                .OrderBy(i => i.Name)
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Name,
                    Selected = i.Id == selectedMenuId
                })
                .ToList();

            model.Menus.Insert(0, new SelectListItem
            {
                Value = "-1",
                Text = "-- None, use site structure --",
                Selected = selectedMenuId < 1
            });

            if (TempData.TryGetValue(StatusKey, out var status) && status != null)
                model.StatusMessage = status.ToString();

            if (TempData.TryGetValue(ErrorKey, out var error) && error != null)
                model.ErrorMessage = error.ToString();

            return View(model);
        }
    }

    public class Configdynamicmenuv2ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string PostController { get; set; } = "CentralPartActions";
        public string PostAction { get; set; } = "Configdynamicmenuv2";
        public string StatusMessage { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public int SelectedMenuId { get; set; } = -1;
        public string ManageMenuUrl { get; set; } = "#";
        public List<SelectListItem> Menus { get; set; } = new();
        public List<SelectListItem> ParameterSets { get; set; } = new();

        public string cboMenu { get; set; } = string.Empty;
        public string lblStatus { get; set; } = string.Empty;

    }
}
