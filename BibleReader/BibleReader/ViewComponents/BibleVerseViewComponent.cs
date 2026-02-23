using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Bible verse display/search View Component. Replaces BibleVerseView.ascx.
    /// Usage: @await Component.InvokeAsync("BibleVerse", new { versionId = 1, bookId = 1, chapter = 1 })
    /// </summary>
    public class BibleVerseViewComponent : WViewComponent
    {
        public BibleVerseViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int versionId = 0, int bookId = 0, int chapter = 0)
        {
            var model = new BibleVerseViewModel();

            // Load available versions
            model.Versions = new List<BibleVersionItem>();
            using (var reader = SqlHelper.ExecuteReader("BibleVersion_GetAll"))
            {
                while (reader.Read())
                {
                    model.Versions.Add(new BibleVersionItem
                    {
                        Id = DataHelper.GetId(reader["Id"]),
                        Name = reader["Name"]?.ToString(),
                        Abbreviation = reader["Abbreviation"]?.ToString()
                    });
                }
            }

            model.SelectedVersionId = versionId > 0 ? versionId : (model.Versions.Count > 0 ? model.Versions[0].Id : 0);
            model.SelectedBookId = bookId;
            model.SelectedChapter = chapter;

            // Load verses if a chapter is selected
            if (model.SelectedVersionId > 0 && bookId > 0 && chapter > 0)
            {
                model.Verses = new List<BibleVerseItem>();
                using (var reader = SqlHelper.ExecuteReader("BibleVerse_GetByChapter",
                    new Microsoft.Data.SqlClient.SqlParameter("@VersionId", model.SelectedVersionId),
                    new Microsoft.Data.SqlClient.SqlParameter("@BookId", bookId),
                    new Microsoft.Data.SqlClient.SqlParameter("@Chapter", chapter)))
                {
                    while (reader.Read())
                    {
                        model.Verses.Add(new BibleVerseItem
                        {
                            VerseNumber = DataHelper.GetId(reader["VerseNumber"]),
                            Text = reader["Text"]?.ToString()
                        });
                    }
                }
            }

            return View(model);
        }
    }

    public class BibleVerseViewModel
    {
        public List<BibleVersionItem> Versions { get; set; }
        public int SelectedVersionId { get; set; }
        public int SelectedBookId { get; set; }
        public int SelectedChapter { get; set; }
        public List<BibleVerseItem> Verses { get; set; }
    }

    public class BibleVersionItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }

    public class BibleVerseItem
    {
        public int VerseNumber { get; set; }
        public string Text { get; set; }
    }
}
