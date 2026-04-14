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

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Code-Behind / Pair |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-003 | Partial | Master Page | legacy/LessonReviewer/LessonReviewer :: Site.Master | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/LessonReviewer/LessonReviewer/Site.Master` | `legacy/LessonReviewer/LessonReviewer/Site.Master.cs` |
| LGC-003 | Partial | Page | legacy/LessonReviewer/LessonReviewer/Admin :: Login.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/LessonReviewer/LessonReviewer/Admin/Login.aspx` | `legacy/LessonReviewer/LessonReviewer/Admin/Login.aspx.cs` |
| LGC-003 | Partial | Page | legacy/LessonReviewer/LessonReviewer/Admin :: Manage.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/LessonReviewer/LessonReviewer/Admin/Manage.aspx` | `legacy/LessonReviewer/LessonReviewer/Admin/Manage.aspx.cs` |
| LGC-003 | Partial | Page | legacy/LessonReviewer/LessonReviewer :: Default.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/LessonReviewer/LessonReviewer/Default.aspx` | `legacy/LessonReviewer/LessonReviewer/Default.aspx.cs` |

## User Controls

_No artifacts found._

## Services And Handlers

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Code-Behind / Pair |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-003 | Partial | Handler | legacy/LessonReviewer/LessonReviewer/Handlers :: AjaxHandler.ashx | Legacy endpoint surface; map to ASP.NET Core APIs/services. | `legacy/LessonReviewer/LessonReviewer/Handlers/AjaxHandler.ashx` | `legacy/LessonReviewer/LessonReviewer/Handlers/AjaxHandler.ashx.cs` |
| LGC-003 | Partial | Handler | legacy/LessonReviewer/LessonReviewer/Handlers :: Playback.ashx | Legacy endpoint surface; map to ASP.NET Core APIs/services. | `legacy/LessonReviewer/LessonReviewer/Handlers/Playback.ashx` | `legacy/LessonReviewer/LessonReviewer/Handlers/Playback.ashx.cs` |

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File |
| --- | --- | --- | --- | --- | --- |
| LGC-003 | Partial | Class Component | legacy/LessonReviewer/LessonReviewer/Admin :: Login | Library/business component; assess API compatibility and dependencies. | `legacy/LessonReviewer/LessonReviewer/Admin/Login.aspx.cs` |
| LGC-003 | Partial | Class Component | legacy/LessonReviewer/LessonReviewer/Admin :: Manage | Library/business component; assess API compatibility and dependencies. | `legacy/LessonReviewer/LessonReviewer/Admin/Manage.aspx.cs` |
| LGC-003 | Partial | Class Component | legacy/LessonReviewer/LessonReviewer :: Default | Library/business component; assess API compatibility and dependencies. | `legacy/LessonReviewer/LessonReviewer/Default.aspx.cs` |
| LGC-003 | Partial | Class Component | legacy/LessonReviewer/LessonReviewer :: Global.asax | Library/business component; assess API compatibility and dependencies. | `legacy/LessonReviewer/LessonReviewer/Global.asax.cs` |
| LGC-003 | Partial | Class Component | legacy/LessonReviewer/LessonReviewer/Handlers :: AjaxHandler | Library/business component; assess API compatibility and dependencies. | `legacy/LessonReviewer/LessonReviewer/Handlers/AjaxHandler.ashx.cs` |
| LGC-003 | Partial | Class Component | legacy/LessonReviewer/LessonReviewer/Handlers :: Playback | Library/business component; assess API compatibility and dependencies. | `legacy/LessonReviewer/LessonReviewer/Handlers/Playback.ashx.cs` |
| LGC-003 | Partial | Class Component | legacy/LessonReviewer/LessonReviewer :: Site | Library/business component; assess API compatibility and dependencies. | `legacy/LessonReviewer/LessonReviewer/Site.Master.cs` |
