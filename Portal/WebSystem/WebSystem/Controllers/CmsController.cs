using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CmsController : Controller
    {
        [HttpGet]
        public IActionResult Render()
        {
            var page = HttpContext.Items["ResolvedPage"] as WPage;
            if (page == null)
            {
                var pageId = DataHelper.GetId(Request, WebColumns.PageIdInternal);
                if (pageId > 0)
                {
                    page = WPage.Get(pageId);
                }
            }

            if (page == null)
            {
                if (ShouldRedirectToSetup())
                {
                    return Redirect("/Central/Setup");
                }

                return NotFound();
            }

            var query = new WQuery(Request);
            if (!WLoaderPageBase.CheckAccess(page, query, WSession.Current))
            {
                return new EmptyResult();
            }

            EnsureContextQuery(page);
            HttpContext.Items[WContext.CurrentPageItemKey] = page;

            var resources = WHelper.LoadResources(page)?.ToString() ?? string.Empty;

            var model = BuildModel(page, resources);

            if (WSession.Current.IsLoggedIn)
            {
                WSession.UserSessions.Update(WSession.Current.UserId, page.Id);
            }

            return View("~/Views/Cms/Render.cshtml", model);
        }

        private static CmsPageRenderModel BuildModel(WPage page, string resources)
        {
            var model = new CmsPageRenderModel
            {
                Page = page,
                ResourcesHtml = resources
            };

            if (page.GetEvalTypeId() == PageTypes.Static)
            {
                model.IsStaticPage = true;
                model.TemplatePath = WHelper.NormalizeLegacyViewPath(page.PartControlTemplate.Path);
                return model;
            }

            model.MasterPage = page.MasterPage;
            model.Template = model.MasterPage?.Template;
            model.TemplatePath = model.Template != null
                ? WHelper.NormalizeLegacyViewPath($"~/Content/Themes/{model.Template.Identity}/{model.Template.FileName}")
                : string.Empty;

            model.Panels = model.Template?.Panels?.OrderBy(p => p.Rank).ThenBy(p => p.Id).ToList()
                           ?? new List<WebTemplatePanel>();

            var pageElements = page.Elements?.Where(e => e.IsActive).Cast<IPageElement>() ?? Enumerable.Empty<IPageElement>();
            var masterElements = model.MasterPage?.Elements?.Where(e => e.IsActive).Cast<IPageElement>() ?? Enumerable.Empty<IPageElement>();
            model.Elements = pageElements.Concat(masterElements).ToList();

            return model;
        }

        private void EnsureContextQuery(WPage page)
        {
            var query = QueryHelpers.ParseQuery(Request.QueryString.HasValue ? Request.QueryString.Value : string.Empty);
            var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var pair in query)
            {
                values[pair.Key] = pair.Value.FirstOrDefault();
            }

            var pageId = page.Id.ToString(CultureInfo.InvariantCulture);

            if (!values.ContainsKey(WebColumns.PageIdInternal))
            {
                values[WebColumns.PageIdInternal] = pageId;
            }

            if (!values.ContainsKey(WebColumns.PageId))
            {
                values[WebColumns.PageId] = pageId;
            }

            var filtered = values
                .Where(v => !string.IsNullOrWhiteSpace(v.Value))
                .Select(v => new KeyValuePair<string, string>(v.Key, v.Value))
                .ToList();

            Request.QueryString = QueryString.Create(filtered);
        }

        private bool ShouldRedirectToSetup()
        {
            if (!IsSetupCandidatePath(Request.Path))
                return false;

            if (!WebObject.IsInitialized)
                return true;

            try
            {
                using var reader = DbHelper.ExecuteReader(CommandType.Text, "SELECT 1");
                return false;
            }
            catch
            {
                return true;
            }
        }

        private static bool IsSetupCandidatePath(PathString path)
        {
            if (!path.HasValue)
                return true;

            var value = path.Value ?? string.Empty;
            if (string.Equals(value, "/", StringComparison.OrdinalIgnoreCase))
                return true;

            if (string.Equals(value, "/default", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(value, "/default.aspx", StringComparison.OrdinalIgnoreCase))
                return true;

            if (value.StartsWith("/central", StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
    }

    public class CmsPageRenderModel
    {
        public WPage Page { get; set; }
        public WebMasterPage MasterPage { get; set; }
        public WebTemplate Template { get; set; }
        public List<WebTemplatePanel> Panels { get; set; } = new List<WebTemplatePanel>();
        public List<IPageElement> Elements { get; set; } = new List<IPageElement>();
        public string TemplatePath { get; set; } = string.Empty;
        public string ResourcesHtml { get; set; } = string.Empty;
        public bool IsStaticPage { get; set; }
    }
}
