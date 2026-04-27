using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Resource file manager. Replaces WebResources.ascx (Central/Misc).
    /// Usage: @await Component.InvokeAsync("WebResources")
    /// </summary>
    public class WebResourcesViewComponent : WViewComponent
    {
        public WebResourcesViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(string selectedFolder = "")
        {
            var selectedFolderPath = string.IsNullOrWhiteSpace(selectedFolder)
                ? Request.Query["Folder"].ToString()
                : selectedFolder;
            var model = new WebResourcesViewModel
            {
                SelectedFolder = selectedFolderPath,
                Folders = new List<ResourceFolder>(),
                Files = new List<ResourceFileItem>()
            };

            try
            {
                var folders = WebFolder.Provider.GetList()?.ToList() ?? new List<WebFolder>();
                var folderPathMap = folders.ToDictionary(i => i.Id, i => BuildFolderPath(folders, i.Id));

                model.Folders = folders
                    .OrderBy(i => folderPathMap[i.Id])
                    .Select(folder => new ResourceFolder
                    {
                        Name = folder.Name,
                        Path = folderPathMap[folder.Id],
                        FileCount = SafeFileCount(folder.Id)
                    })
                    .ToList();

                if (string.IsNullOrWhiteSpace(model.SelectedFolder) && model.Folders.Count > 0)
                    model.SelectedFolder = model.Folders[0].Path;

                var selectedFolderEntity = folders.FirstOrDefault(i =>
                    BuildFolderPath(folders, i.Id).Equals(model.SelectedFolder, StringComparison.OrdinalIgnoreCase));

                if (selectedFolderEntity != null)
                {
                    var files = WebFile.Provider.GetList(selectedFolderEntity.Id)?.OrderBy(i => i.Name).ToList() ?? new List<WebFile>();
                    model.Files = files.Select(file => new ResourceFileItem
                        {
                            Id = file.Id,
                            Name = file.Name,
                            FilePath = CombineFolderFilePath(model.SelectedFolder, file.Name),
                            FileSize = 0,
                            FileType = ResolveFileType(file.Name),
                            ModifiedDate = null
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load resources: {ex.Message}";
            }

            return View(model);
        }

        private static int SafeFileCount(int folderId)
        {
            try
            {
                return WebFile.Provider.GetList(folderId)?.Count() ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        private static string BuildFolderPath(List<WebFolder> allFolders, int folderId)
        {
            var map = allFolders.ToDictionary(i => i.Id, i => i);
            var names = new Stack<string>();
            var currentId = folderId;
            while (currentId > 0 && map.TryGetValue(currentId, out var folder))
            {
                if (!string.IsNullOrEmpty(folder.Name))
                    names.Push(folder.Name);
                currentId = folder.ParentId;
            }

            return "/" + string.Join("/", names);
        }

        private static string CombineFolderFilePath(string folderPath, string fileName)
        {
            if (string.IsNullOrEmpty(folderPath))
                return "/" + fileName;

            return folderPath.EndsWith("/") ? folderPath + fileName : folderPath + "/" + fileName;
        }

        private static string ResolveFileType(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return "Unknown";

            var ext = System.IO.Path.GetExtension(fileName);
            return string.IsNullOrWhiteSpace(ext) ? "Unknown" : ext.TrimStart('.').ToUpperInvariant();
        }
    }

    public class WebResourcesViewModel
    {
        public string SelectedFolder { get; set; }
        public List<ResourceFolder> Folders { get; set; } = new List<ResourceFolder>();
        public List<ResourceFileItem> Files { get; set; } = new List<ResourceFileItem>();
        public string ErrorMessage { get; set; }
    }

    public class ResourceFolder
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int FileCount { get; set; }
    }

    public class ResourceFileItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string FileType { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
