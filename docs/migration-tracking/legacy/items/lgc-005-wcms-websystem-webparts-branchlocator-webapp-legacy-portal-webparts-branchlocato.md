# LGC-005 - WCMS.WebSystem.WebParts.BranchLocator.WebApp

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-005 |
| Project Type | App |
| Project File | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/WCMS.WebSystem.Apps.BranchLocator.WebApp.csproj` |
| Project Directory | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Partial |
| Status Basis | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| Project References | 1 |
| Surface Artifacts | 12 |
| Component/Class Artifacts | 5 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Execution (In Progress) | Close remaining legacy runtime/UI/endpoint gaps and define cutover tests. |
| WebForms Surface Present | Yes | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

## Project References

| Reference Include |
| --- |
| ../WCMS.WebSystem.Apps.BranchLocator/WCMS.WebSystem.Apps.BranchLocator.csproj |

## Pages And Views

| Artifact Type | Feature / Functionality (Inferred) | Source File | Code-Behind / Pair | Migration Note |
| --- | --- | --- | --- | --- |
| Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: Announcements.cshtml | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Announcements.cshtml` |  | Modern UI artifact; validate routing/component parity. |
| Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: SetLocation.cshtml | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/SetLocation.cshtml` |  | Modern UI artifact; validate routing/component parity. |
| Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator :: Announcements-Fullscreen.cshtml | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Announcements-Fullscreen.cshtml` |  | Modern UI artifact; validate routing/component parity. |
| Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator :: Announcements.cshtml | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Announcements.cshtml` |  | Modern UI artifact; validate routing/component parity. |
| Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator ::  ChapterBrowser.cshtml | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/_ChapterBrowser.cshtml` |  | Modern UI artifact; validate routing/component parity. |
| Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator ::  ChapterDetails.cshtml | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/_ChapterDetails.cshtml` |  | Modern UI artifact; validate routing/component parity. |
| Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator ::  Dashboard.cshtml | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/_Dashboard.cshtml` |  | Modern UI artifact; validate routing/component parity. |
| Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator ::  SearchResults.cshtml | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/_SearchResults.cshtml` |  | Modern UI artifact; validate routing/component parity. |

## User Controls

| Artifact Type | Feature / Functionality (Inferred) | Source File | Code-Behind / Pair | Migration Note |
| --- | --- | --- | --- | --- |
| User Control | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: Chapter.ascx | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapter.ascx` | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapter.ascx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| User Control | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: ChapterHome.ascx | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterHome.ascx` | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterHome.ascx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| User Control | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: ChapterTree.ascx | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterTree.ascx` | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterTree.ascx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| User Control | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: Chapters.ascx | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapters.ascx` | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapters.ascx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |

## Services And Handlers

_No artifacts found._

## Components And Classes

| Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- |
| Class Component | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: Chapter | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapter.ascx.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: ChapterHome | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterHome.ascx.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: ChapterTree | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterTree.ascx.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: Chapters | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapters.ascx.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Controllers :: ChapterController | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Controllers/ChapterController.cs` | Library/business component; assess API compatibility and dependencies. |
