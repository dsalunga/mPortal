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

| --- | --- | --- |
| ../WCMS.WebSystem.Apps.BranchLocator/WCMS.WebSystem.Apps.BranchLocator.csproj |

## Pages And Views

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Code-Behind / Pair |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-005 | Partial | Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: Announcements.cshtml | Modern UI artifact; validate routing/component parity. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Announcements.cshtml` |  |
| LGC-005 | Partial | Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: SetLocation.cshtml | Modern UI artifact; validate routing/component parity. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/SetLocation.cshtml` |  |
| LGC-005 | Partial | Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator :: Announcements-Fullscreen.cshtml | Modern UI artifact; validate routing/component parity. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Announcements-Fullscreen.cshtml` |  |
| LGC-005 | Partial | Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator :: Announcements.cshtml | Modern UI artifact; validate routing/component parity. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Announcements.cshtml` |  |
| LGC-005 | Partial | Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator ::  ChapterBrowser.cshtml | Modern UI artifact; validate routing/component parity. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/_ChapterBrowser.cshtml` |  |
| LGC-005 | Partial | Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator ::  ChapterDetails.cshtml | Modern UI artifact; validate routing/component parity. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/_ChapterDetails.cshtml` |  |
| LGC-005 | Partial | Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator ::  Dashboard.cshtml | Modern UI artifact; validate routing/component parity. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/_Dashboard.cshtml` |  |
| LGC-005 | Partial | Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator ::  SearchResults.cshtml | Modern UI artifact; validate routing/component parity. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/_SearchResults.cshtml` |  |

## User Controls

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Code-Behind / Pair |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-005 | Partial | User Control | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: Chapter.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapter.ascx` | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapter.ascx.cs` |
| LGC-005 | Partial | User Control | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: ChapterHome.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterHome.ascx` | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterHome.ascx.cs` |
| LGC-005 | Partial | User Control | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: ChapterTree.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterTree.ascx` | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterTree.ascx.cs` |
| LGC-005 | Partial | User Control | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: Chapters.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapters.ascx` | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapters.ascx.cs` |

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File |
| --- | --- | --- | --- | --- | --- |
| LGC-005 | Partial | Class Component | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: Chapter | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapter.ascx.cs` |
| LGC-005 | Partial | Class Component | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: ChapterHome | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterHome.ascx.cs` |
| LGC-005 | Partial | Class Component | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: ChapterTree | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterTree.ascx.cs` |
| LGC-005 | Partial | Class Component | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: Chapters | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapters.ascx.cs` |
| LGC-005 | Partial | Class Component | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Controllers :: ChapterController | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Controllers/ChapterController.cs` |
