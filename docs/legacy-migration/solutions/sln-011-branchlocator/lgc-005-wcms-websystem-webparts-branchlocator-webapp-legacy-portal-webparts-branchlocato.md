# LGC-005 - WCMS.WebSystem.WebParts.BranchLocator.WebApp

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-005 |
| Project Type | App |
| Project File | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/WCMS.WebSystem.Apps.BranchLocator.WebApp.csproj` |
| Modern Project File / Evidence | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/WCMS.WebSystem.Apps.BranchLocator.WebApp.csproj` |
| Project Directory | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:43, Not Applicable:5, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 1 |
| Surface Artifacts | 12 |
| Component/Class Artifacts | 5 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Completed | Migration to .NET 10 complete. All source files compile with 0 errors. |
| WebForms Surface Present | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |
| Endpoint Surface Present | No | N/A |
| Class/Component Porting | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |

## Project References

| --- | --- | --- |
| ../WCMS.WebSystem.Apps.BranchLocator/WCMS.WebSystem.Apps.BranchLocator.csproj |

## Pages And Views

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Modern File / Evidence | Code-Behind / Pair |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-005 | Completed | Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: Announcements.cshtml | Modern UI artifact; validate routing/component parity. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Announcements.cshtml` | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Announcements.cshtml` |  |
| LGC-005 | Completed | Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: SetLocation.cshtml | Modern UI artifact; validate routing/component parity. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/SetLocation.cshtml` | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/SetLocation.cshtml` |  |
| LGC-005 | Completed | Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator :: Announcements-Fullscreen.cshtml | Modern UI artifact; validate routing/component parity. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Announcements-Fullscreen.cshtml` | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Announcements-Fullscreen.cshtml` |  |
| LGC-005 | Completed | Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator :: Announcements.cshtml | Modern UI artifact; validate routing/component parity. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Announcements.cshtml` | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Announcements.cshtml` |  |
| LGC-005 | Completed | Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator ::  ChapterBrowser.cshtml | Modern UI artifact; validate routing/component parity. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/_ChapterBrowser.cshtml` | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/_ChapterBrowser.cshtml` |  |
| LGC-005 | Completed | Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator ::  ChapterDetails.cshtml | Modern UI artifact; validate routing/component parity. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/_ChapterDetails.cshtml` | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/_ChapterDetails.cshtml` |  |
| LGC-005 | Completed | Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator ::  Dashboard.cshtml | Modern UI artifact; validate routing/component parity. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/_Dashboard.cshtml` | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/_Dashboard.cshtml` |  |
| LGC-005 | Completed | Razor View | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator ::  SearchResults.cshtml | Modern UI artifact; validate routing/component parity. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/_SearchResults.cshtml` | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/_SearchResults.cshtml` |  |

## User Controls

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Modern File / Evidence | Code-Behind / Pair |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-005 | Completed | User Control | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: Chapter.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapter.ascx` | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapter.cshtml` | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapter.ascx.cs` |
| LGC-005 | Completed | User Control | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: ChapterHome.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterHome.ascx` | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterHome.cshtml` | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterHome.ascx.cs` |
| LGC-005 | Completed | User Control | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: ChapterTree.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterTree.ascx` | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterTree.cshtml` | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterTree.ascx.cs` |
| LGC-005 | Completed | User Control | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: Chapters.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapters.ascx` | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapters.cshtml` | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapters.ascx.cs` |

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Modern File / Evidence |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-005 | Completed | Class Component | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: Chapter | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapter.ascx.cs` | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapter.cshtml` |
| LGC-005 | Completed | Class Component | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: ChapterHome | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterHome.ascx.cs` | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterHome.cshtml` |
| LGC-005 | Completed | Class Component | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: ChapterTree | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterTree.ascx.cs` | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterTree.cshtml` |
| LGC-005 | Completed | Class Component | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin :: Chapters | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapters.ascx.cs` | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapters.cshtml` |
| LGC-005 | Completed | Class Component | legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Controllers :: ChapterController | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Controllers/ChapterController.cs` | `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Controllers/ChapterController.cs` |
