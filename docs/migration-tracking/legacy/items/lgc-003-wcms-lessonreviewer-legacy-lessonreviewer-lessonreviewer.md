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
| Migration Status | Partial |
| Status Basis | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| Project References | 1 |
| Surface Artifacts | 6 |
| Component/Class Artifacts | 7 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Execution (In Progress) | Close remaining legacy runtime/UI/endpoint gaps and define cutover tests. |
| WebForms Surface Present | Yes | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | Yes | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

## Project References

| --- | --- | --- |
| ../LessonReviewer.Core/LessonReviewer.Core.csproj |

## Pages And Views

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Source File | Code-Behind / Pair | Migration Note |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-003 | Partial | Master Page | legacy/LessonReviewer/LessonReviewer :: Site.Master | `legacy/LessonReviewer/LessonReviewer/Site.Master` | `legacy/LessonReviewer/LessonReviewer/Site.Master.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| LGC-003 | Partial | Page | legacy/LessonReviewer/LessonReviewer/Admin :: Login.aspx | `legacy/LessonReviewer/LessonReviewer/Admin/Login.aspx` | `legacy/LessonReviewer/LessonReviewer/Admin/Login.aspx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| LGC-003 | Partial | Page | legacy/LessonReviewer/LessonReviewer/Admin :: Manage.aspx | `legacy/LessonReviewer/LessonReviewer/Admin/Manage.aspx` | `legacy/LessonReviewer/LessonReviewer/Admin/Manage.aspx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| LGC-003 | Partial | Page | legacy/LessonReviewer/LessonReviewer :: Default.aspx | `legacy/LessonReviewer/LessonReviewer/Default.aspx` | `legacy/LessonReviewer/LessonReviewer/Default.aspx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |

## User Controls

_No artifacts found._

## Services And Handlers

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Source File | Code-Behind / Pair | Migration Note |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-003 | Partial | Handler | legacy/LessonReviewer/LessonReviewer/Handlers :: AjaxHandler.ashx | `legacy/LessonReviewer/LessonReviewer/Handlers/AjaxHandler.ashx` | `legacy/LessonReviewer/LessonReviewer/Handlers/AjaxHandler.ashx.cs` | Legacy endpoint surface; map to ASP.NET Core APIs/services. |
| LGC-003 | Partial | Handler | legacy/LessonReviewer/LessonReviewer/Handlers :: Playback.ashx | `legacy/LessonReviewer/LessonReviewer/Handlers/Playback.ashx` | `legacy/LessonReviewer/LessonReviewer/Handlers/Playback.ashx.cs` | Legacy endpoint surface; map to ASP.NET Core APIs/services. |

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- | --- | --- |
| LGC-003 | Partial | Class Component | legacy/LessonReviewer/LessonReviewer/Admin :: Login | `legacy/LessonReviewer/LessonReviewer/Admin/Login.aspx.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-003 | Partial | Class Component | legacy/LessonReviewer/LessonReviewer/Admin :: Manage | `legacy/LessonReviewer/LessonReviewer/Admin/Manage.aspx.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-003 | Partial | Class Component | legacy/LessonReviewer/LessonReviewer :: Default | `legacy/LessonReviewer/LessonReviewer/Default.aspx.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-003 | Partial | Class Component | legacy/LessonReviewer/LessonReviewer :: Global.asax | `legacy/LessonReviewer/LessonReviewer/Global.asax.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-003 | Partial | Class Component | legacy/LessonReviewer/LessonReviewer/Handlers :: AjaxHandler | `legacy/LessonReviewer/LessonReviewer/Handlers/AjaxHandler.ashx.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-003 | Partial | Class Component | legacy/LessonReviewer/LessonReviewer/Handlers :: Playback | `legacy/LessonReviewer/LessonReviewer/Handlers/Playback.ashx.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-003 | Partial | Class Component | legacy/LessonReviewer/LessonReviewer :: Site | `legacy/LessonReviewer/LessonReviewer/Site.Master.cs` | Library/business component; assess API compatibility and dependencies. |
