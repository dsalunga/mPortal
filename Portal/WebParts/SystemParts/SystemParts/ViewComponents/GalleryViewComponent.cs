using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Diagnostics;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Image/media gallery View Component. Replaces Gallery.ascx (SystemParts/Gallery).
    /// Usage: @await Component.InvokeAsync("Gallery", new { objectId, recordId })
    /// </summary>
    public class GalleryViewComponent : WViewComponent
    {
        public GalleryViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            var sw = PerformanceLog.StartLog();

            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new GalleryViewModel { Items = new List<GalleryItem>() };

            using (var reader = SqlHelper.ExecuteReader("Gallery_Get",
                new Microsoft.Data.SqlClient.SqlParameter("@ObjectId", WcmsContext.ObjectId),
                new Microsoft.Data.SqlClient.SqlParameter("@RecordId", WcmsContext.RecordId)))
            {
                while (reader.Read())
                {
                    model.Items.Add(new GalleryItem
                    {
                        Id = DataHelper.GetId(reader["Id"]),
                        Title = reader["Title"]?.ToString(),
                        ImageUrl = reader["ImageUrl"]?.ToString(),
                        ThumbnailUrl = reader["ThumbnailUrl"]?.ToString(),
                        Description = reader["Description"]?.ToString()
                    });
                }
            }

            PerformanceLog.EndLog(
                string.Format("Gallery: {0}/{1}", WcmsContext.ObjectId, WcmsContext.RecordId),
                sw, WcmsContext.PageId);

            return View(model);
        }
    }

    public class GalleryViewModel
    {
        public List<GalleryItem> Items { get; set; }
    }

    public class GalleryItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
    }
}
