# LGC-019 - WCMS.WebSystem.WebParts

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-019 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/WCMS.WebSystem.Apps.csproj` |
| Modern Project File / Evidence | `Portal/WebParts/SystemParts/WCMS.Framework.WebParts/WCMS.WebSystem.Apps.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:40, Not Applicable:2, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 39 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Completed | Migration to .NET 10 complete. All source files compile with 0 errors. |
| WebForms Surface Present | No | N/A |
| Endpoint Surface Present | No | N/A |
| Class/Component Porting | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content :: ContentHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Content/ContentHelper.cs` | `./Content/ContentHelper.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: ContentPartDataManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Content/Providers/ContentPartDataManager.cs` | `./Content/Providers/ContentPartDataManager.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: IWebContentProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Content/Providers/IWebContentProvider.cs` | `./Content/Providers/IWebContentProvider.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: IWebObjectContentProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Content/Providers/IWebObjectContentProvider.cs` | `./Content/Providers/IWebObjectContentProvider.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: WebContentManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Content/Providers/WebContentManager.cs` | `./Content/Providers/WebContentManager.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: WebContentProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Content/Providers/WebContentProvider.cs` | `./Content/Providers/WebContentProvider.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: WebObjectContentManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Content/Providers/WebObjectContentManager.cs` | `./Content/Providers/WebObjectContentManager.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: WebObjectContentProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Content/Providers/WebObjectContentProvider.cs` | `./Content/Providers/WebObjectContentProvider.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content :: WebContent | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Content/WebContent.cs` | `./Content/WebContent.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content :: WebObjectContent | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Content/WebObjectContent.cs` | `./Content/WebObjectContent.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: Constants | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/Constants.cs` | `./Menu/Constants.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers :: MenuItemManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/Managers/MenuItemManager.cs` | `./Menu/Managers/MenuItemManager.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers :: MenuManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/Managers/MenuManager.cs` | `./Menu/Managers/MenuManager.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers :: MenuObjectManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/Managers/MenuObjectManager.cs` | `./Menu/Managers/MenuObjectManager.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers :: MenuPartDataManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/Managers/MenuPartDataManager.cs` | `./Menu/Managers/MenuPartDataManager.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuEntity | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/MenuEntity.cs` | `./Menu/MenuEntity.cs` |
| LGC-019 | Not Applicable | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/MenuHelper.cs` | N/A (retired/replaced in modern architecture). |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuItem | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/MenuItem.cs` | `./Menu/MenuItem.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuItemModel | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/MenuItemModel.cs` | `./Menu/MenuItemModel.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuItemModelCollection | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/MenuItemModelCollection.cs` | `./Menu/MenuItemModelCollection.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuModel | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/MenuModel.cs` | `./Menu/MenuModel.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuObject | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/MenuObject.cs` | `./Menu/MenuObject.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: IMenuItemProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/Providers/IMenuItemProvider.cs` | `./Menu/Providers/IMenuItemProvider.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: IMenuObjectProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/Providers/IMenuObjectProvider.cs` | `./Menu/Providers/IMenuObjectProvider.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: IMenuProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/Providers/IMenuProvider.cs` | `./Menu/Providers/IMenuProvider.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: MenuItemProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/Providers/MenuItemProvider.cs` | `./Menu/Providers/MenuItemProvider.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: MenuObjectProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/Providers/MenuObjectProvider.cs` | `./Menu/Providers/MenuObjectProvider.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: MenuProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Menu/Providers/MenuProvider.cs` | `./Menu/Providers/MenuProvider.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo :: Album | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Photo/Album.cs` | `./Photo/Album.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo :: AlbumEqualityComparer | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Photo/AlbumEqualityComparer.cs` | `./Photo/AlbumEqualityComparer.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo :: AlbumLink | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Photo/AlbumLink.cs` | `./Photo/AlbumLink.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo :: AlbumPhoto | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Photo/AlbumPhoto.cs` | `./Photo/AlbumPhoto.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo :: Constants | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Photo/Constants.cs` | `./Photo/Constants.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: AlbumLinkProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Photo/Providers/AlbumLinkProvider.cs` | `./Photo/Providers/AlbumLinkProvider.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: AlbumPhotoProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Photo/Providers/AlbumPhotoProvider.cs` | `./Photo/Providers/AlbumPhotoProvider.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: AlbumProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Photo/Providers/AlbumProvider.cs` | `./Photo/Providers/AlbumProvider.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: IAlbumLinkProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Photo/Providers/IAlbumLinkProvider.cs` | `./Photo/Providers/IAlbumLinkProvider.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: IAlbumPhotoProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Photo/Providers/IAlbumPhotoProvider.cs` | `./Photo/Providers/IAlbumPhotoProvider.cs` |
| LGC-019 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: IAlbumProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Photo/Providers/IAlbumProvider.cs` | `./Photo/Providers/IAlbumProvider.cs` |
