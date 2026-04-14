# LGC-019 - WCMS.WebSystem.WebParts

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-019 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/WCMS.WebSystem.Apps.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Not Stated |
| Status Basis | Legacy target framework only (v4.8). |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 39 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Discovery / Planning | Assess framework/API compatibility and plan library porting. |
| WebForms Surface Present | No | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File |
| --- | --- | --- | --- | --- | --- |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content :: ContentHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/ContentHelper.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: ContentPartDataManager | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers/ContentPartDataManager.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: IWebContentProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers/IWebContentProvider.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: IWebObjectContentProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers/IWebObjectContentProvider.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: WebContentManager | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers/WebContentManager.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: WebContentProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers/WebContentProvider.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: WebObjectContentManager | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers/WebObjectContentManager.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: WebObjectContentProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers/WebObjectContentProvider.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content :: WebContent | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/WebContent.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content :: WebObjectContent | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/WebObjectContent.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: Constants | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Constants.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers :: MenuItemManager | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers/MenuItemManager.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers :: MenuManager | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers/MenuManager.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers :: MenuObjectManager | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers/MenuObjectManager.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers :: MenuPartDataManager | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers/MenuPartDataManager.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuEntity | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/MenuEntity.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/MenuHelper.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuItem | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/MenuItem.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuItemModel | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/MenuItemModel.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuItemModelCollection | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/MenuItemModelCollection.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuModel | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/MenuModel.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuObject | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/MenuObject.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: IMenuItemProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers/IMenuItemProvider.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: IMenuObjectProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers/IMenuObjectProvider.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: IMenuProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers/IMenuProvider.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: MenuItemProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers/MenuItemProvider.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: MenuObjectProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers/MenuObjectProvider.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: MenuProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers/MenuProvider.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo :: Album | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Album.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo :: AlbumEqualityComparer | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/AlbumEqualityComparer.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo :: AlbumLink | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/AlbumLink.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo :: AlbumPhoto | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/AlbumPhoto.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo :: Constants | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Constants.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: AlbumLinkProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers/AlbumLinkProvider.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: AlbumPhotoProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers/AlbumPhotoProvider.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: AlbumProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers/AlbumProvider.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: IAlbumLinkProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers/IAlbumLinkProvider.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: IAlbumPhotoProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers/IAlbumPhotoProvider.cs` |
| LGC-019 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: IAlbumProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers/IAlbumProvider.cs` |
