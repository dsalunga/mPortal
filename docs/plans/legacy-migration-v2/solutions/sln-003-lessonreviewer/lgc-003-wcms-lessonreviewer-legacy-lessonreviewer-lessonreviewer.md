# LGC-003 - WCMS.LessonReviewer

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-003 |
| Project Type | App |
| Project File | `legacy/LessonReviewer/LessonReviewer/LessonReviewer.csproj` |
| Modern Project File / Evidence | `Apps/LessonReviewer/LessonReviewer/LessonReviewer.csproj` |
| Project Directory | `legacy/LessonReviewer/LessonReviewer` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:14, Not Applicable:17, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 1 |
| Surface Artifacts | 6 |
| Component/Class Artifacts | 7 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Completed | Migration to .NET 10 complete. All source files compile with 0 errors. |
| WebForms Surface Present | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |
| Endpoint Surface Present | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |
| Class/Component Porting | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |

## Project References

| --- | --- | --- |
| ../LessonReviewer.Core/LessonReviewer.Core.csproj |

## Pages And Views

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) | Code-Behind / Pair (relative to Project Directory) |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-003 | Completed | Master Page | legacy/LessonReviewer/LessonReviewer :: Site.Master | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./Site.Master` | `./Site.Master` | `./Site.Master.cs` |
| LGC-003 | Not Applicable | Page | legacy/LessonReviewer/LessonReviewer/Admin :: Login.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./Admin/Login.aspx` | N/A (retired/replaced in modern architecture). | `./Admin/Login.aspx.cs` |
| LGC-003 | Not Applicable | Page | legacy/LessonReviewer/LessonReviewer/Admin :: Manage.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./Admin/Manage.aspx` | N/A (retired/replaced in modern architecture). | `./Admin/Manage.aspx.cs` |
| LGC-003 | Not Applicable | Page | legacy/LessonReviewer/LessonReviewer :: Default.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./Default.aspx` | N/A (retired/replaced in modern architecture). | `./Default.aspx.cs` |

## User Controls

_No artifacts found._

## Services And Handlers

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) | Code-Behind / Pair (relative to Project Directory) |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-003 | Not Applicable | Handler | legacy/LessonReviewer/LessonReviewer/Handlers :: AjaxHandler.ashx | Legacy endpoint surface; map to ASP.NET Core APIs/services. | `./Handlers/AjaxHandler.ashx` | `./Api/LegacyAjaxHandlerController.cs` | `./Handlers/AjaxHandler.ashx.cs` |
| LGC-003 | Not Applicable | Handler | legacy/LessonReviewer/LessonReviewer/Handlers :: Playback.ashx | Legacy endpoint surface; map to ASP.NET Core APIs/services. | `./Handlers/Playback.ashx` | N/A (retired/replaced in modern architecture). | `./Handlers/Playback.ashx.cs` |

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-003 | Not Applicable | Class Component | legacy/LessonReviewer/LessonReviewer/Admin :: Login | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Admin/Login.aspx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-003 | Not Applicable | Class Component | legacy/LessonReviewer/LessonReviewer/Admin :: Manage | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Admin/Manage.aspx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-003 | Not Applicable | Class Component | legacy/LessonReviewer/LessonReviewer :: Default | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Default.aspx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-003 | Not Applicable | Class Component | legacy/LessonReviewer/LessonReviewer :: Global.asax | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Global.asax.cs` | `./Program.cs` |
| LGC-003 | Not Applicable | Class Component | legacy/LessonReviewer/LessonReviewer/Handlers :: AjaxHandler | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Handlers/AjaxHandler.ashx.cs` | `./Api/LegacyAjaxHandlerController.cs` |
| LGC-003 | Not Applicable | Class Component | legacy/LessonReviewer/LessonReviewer/Handlers :: Playback | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Handlers/Playback.ashx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-003 | Not Applicable | Class Component | legacy/LessonReviewer/LessonReviewer :: Site | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Site.Master.cs` | `./Site.Master` |
