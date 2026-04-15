# LGC-003 - WCMS.LessonReviewer

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-003 |
| Project Type | App |
| Project File | `legacy/LessonReviewer/LessonReviewer/LessonReviewer.csproj` |
| Project Directory | `legacy/LessonReviewer/LessonReviewer` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Modern counterpart on .NET 10 verified; compiles with 0 errors. |
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

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Code-Behind / Pair |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-003 | Completed | Master Page | legacy/LessonReviewer/LessonReviewer :: Site.Master | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/LessonReviewer/LessonReviewer/Site.Master` | `legacy/LessonReviewer/LessonReviewer/Site.Master.cs` |
| LGC-003 | Completed | Page | legacy/LessonReviewer/LessonReviewer/Admin :: Login.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/LessonReviewer/LessonReviewer/Admin/Login.aspx` | `legacy/LessonReviewer/LessonReviewer/Admin/Login.aspx.cs` |
| LGC-003 | Completed | Page | legacy/LessonReviewer/LessonReviewer/Admin :: Manage.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/LessonReviewer/LessonReviewer/Admin/Manage.aspx` | `legacy/LessonReviewer/LessonReviewer/Admin/Manage.aspx.cs` |
| LGC-003 | Completed | Page | legacy/LessonReviewer/LessonReviewer :: Default.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/LessonReviewer/LessonReviewer/Default.aspx` | `legacy/LessonReviewer/LessonReviewer/Default.aspx.cs` |

## User Controls

_No artifacts found._

## Services And Handlers

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Code-Behind / Pair |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-003 | Completed | Handler | legacy/LessonReviewer/LessonReviewer/Handlers :: AjaxHandler.ashx | Legacy endpoint surface; map to ASP.NET Core APIs/services. | `legacy/LessonReviewer/LessonReviewer/Handlers/AjaxHandler.ashx` | `legacy/LessonReviewer/LessonReviewer/Handlers/AjaxHandler.ashx.cs` |
| LGC-003 | Completed | Handler | legacy/LessonReviewer/LessonReviewer/Handlers :: Playback.ashx | Legacy endpoint surface; map to ASP.NET Core APIs/services. | `legacy/LessonReviewer/LessonReviewer/Handlers/Playback.ashx` | `legacy/LessonReviewer/LessonReviewer/Handlers/Playback.ashx.cs` |

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File |
| --- | --- | --- | --- | --- | --- |
| LGC-003 | Completed | Class Component | legacy/LessonReviewer/LessonReviewer/Admin :: Login | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/LessonReviewer/LessonReviewer/Admin/Login.aspx.cs` |
| LGC-003 | Completed | Class Component | legacy/LessonReviewer/LessonReviewer/Admin :: Manage | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/LessonReviewer/LessonReviewer/Admin/Manage.aspx.cs` |
| LGC-003 | Completed | Class Component | legacy/LessonReviewer/LessonReviewer :: Default | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/LessonReviewer/LessonReviewer/Default.aspx.cs` |
| LGC-003 | Completed | Class Component | legacy/LessonReviewer/LessonReviewer :: Global.asax | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/LessonReviewer/LessonReviewer/Global.asax.cs` |
| LGC-003 | Completed | Class Component | legacy/LessonReviewer/LessonReviewer/Handlers :: AjaxHandler | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/LessonReviewer/LessonReviewer/Handlers/AjaxHandler.ashx.cs` |
| LGC-003 | Completed | Class Component | legacy/LessonReviewer/LessonReviewer/Handlers :: Playback | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/LessonReviewer/LessonReviewer/Handlers/Playback.ashx.cs` |
| LGC-003 | Completed | Class Component | legacy/LessonReviewer/LessonReviewer :: Site | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/LessonReviewer/LessonReviewer/Site.Master.cs` |
