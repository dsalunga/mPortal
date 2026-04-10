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

| Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content :: ContentHelper | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/ContentHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: ContentPartDataManager | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers/ContentPartDataManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: IWebContentProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers/IWebContentProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: IWebObjectContentProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers/IWebObjectContentProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: WebContentManager | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers/WebContentManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: WebContentProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers/WebContentProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: WebObjectContentManager | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers/WebObjectContentManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers :: WebObjectContentProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/Providers/WebObjectContentProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content :: WebContent | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/WebContent.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content :: WebObjectContent | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Content/WebObjectContent.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: Constants | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Constants.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers :: MenuItemManager | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers/MenuItemManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers :: MenuManager | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers/MenuManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers :: MenuObjectManager | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers/MenuObjectManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers :: MenuPartDataManager | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Managers/MenuPartDataManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuEntity | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/MenuEntity.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuHelper | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/MenuHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuItem | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/MenuItem.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuItemModel | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/MenuItemModel.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuItemModelCollection | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/MenuItemModelCollection.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuModel | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/MenuModel.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu :: MenuObject | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/MenuObject.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: IMenuItemProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers/IMenuItemProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: IMenuObjectProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers/IMenuObjectProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: IMenuProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers/IMenuProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: MenuItemProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers/MenuItemProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: MenuObjectProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers/MenuObjectProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers :: MenuProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Menu/Providers/MenuProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo :: Album | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Album.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo :: AlbumEqualityComparer | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/AlbumEqualityComparer.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo :: AlbumLink | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/AlbumLink.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo :: AlbumPhoto | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/AlbumPhoto.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo :: Constants | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Constants.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: AlbumLinkProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers/AlbumLinkProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: AlbumPhotoProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers/AlbumPhotoProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: AlbumProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers/AlbumProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: IAlbumLinkProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers/IAlbumLinkProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: IAlbumPhotoProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers/IAlbumPhotoProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers :: IAlbumProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Photo/Providers/IAlbumProvider.cs` | Library/business component; assess API compatibility and dependencies. |
