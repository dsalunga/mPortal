using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Short URL management. Replaces ShortUrlManager.ascx (Central/Tools).
    /// Usage: @await Component.InvokeAsync("ShortUrlManager")
    /// </summary>
    public class ShortUrlManagerViewComponent : WViewComponent
    {
        public ShortUrlManagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(string search = null)
        {
            var model = new ShortUrlManagerViewModel
            {
                SearchTerm = search,
                Urls = new List<ShortUrlItem>(),
                CurrentPage = 1,
                PageSize = 25,
                TotalCount = 0
            };

            return View(model);
        }
    }

    public class ShortUrlManagerViewModel
    {
        public string SearchTerm { get; set; }
        public List<ShortUrlItem> Urls { get; set; } = new List<ShortUrlItem>();
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
        public string ErrorMessage { get; set; }
    }

    public class ShortUrlItem
    {
        public int Id { get; set; }
        public string ShortCode { get; set; }
        public string TargetUrl { get; set; }
        public int HitCount { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
