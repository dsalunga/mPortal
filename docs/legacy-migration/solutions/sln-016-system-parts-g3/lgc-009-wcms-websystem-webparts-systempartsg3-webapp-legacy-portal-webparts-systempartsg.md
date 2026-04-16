# LGC-009 - WCMS.WebSystem.WebParts.SystemPartsG3.WebApp

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-009 |
| Project Type | App |
| Project File | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/WCMS.WebSystem.Apps.SystemApps3.WebApp.csproj` |
| Modern Project File / Evidence | `Portal/WebParts/SystemPartsG3/SystemPartsG3/WCMS.WebSystem.Apps.SystemApps3.WebApp.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:7, Not Applicable:21, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 2 |
| Surface Artifacts | 10 |
| Component/Class Artifacts | 9 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Completed | Migration to .NET 10 complete. All source files compile with 0 errors. |
| WebForms Surface Present | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |
| Endpoint Surface Present | No | N/A |
| Class/Component Porting | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |

## Project References

| --- | --- | --- |
| ../WCMS.WebSystem.WebParts.Incident/WCMS.WebSystem.Apps.Incident.csproj |
| ../WCMS.WebSystem.WebParts.Jobs/WCMS.WebSystem.Apps.Jobs.csproj |

## Pages And Views

_No artifacts found._

## User Controls

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) | Code-Behind / Pair (relative to Project Directory) |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-009 | Not Applicable | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: CategoryEditView.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./AppBundle3/Incident/CategoryEditView.ascx` | N/A (retired/replaced in modern architecture). | `./AppBundle3/Incident/CategoryEditView.ascx.cs` |
| LGC-009 | Not Applicable | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: CategoryManagerView.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./AppBundle3/Incident/CategoryManagerView.ascx` | N/A (retired/replaced in modern architecture). | `./AppBundle3/Incident/CategoryManagerView.ascx.cs` |
| LGC-009 | Not Applicable | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: InstanceEditView.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./AppBundle3/Incident/InstanceEditView.ascx` | N/A (retired/replaced in modern architecture). | `./AppBundle3/Incident/InstanceEditView.ascx.cs` |
| LGC-009 | Not Applicable | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: InstanceManagerView.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./AppBundle3/Incident/InstanceManagerView.ascx` | N/A (retired/replaced in modern architecture). | `./AppBundle3/Incident/InstanceManagerView.ascx.cs` |
| LGC-009 | Not Applicable | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TicketManagerView.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./AppBundle3/Incident/TicketManagerView.ascx` | N/A (retired/replaced in modern architecture). | `./AppBundle3/Incident/TicketManagerView.ascx.cs` |
| LGC-009 | Not Applicable | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TicketView.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./AppBundle3/Incident/TicketView.ascx` | N/A (retired/replaced in modern architecture). | `./AppBundle3/Incident/TicketView.ascx.cs` |
| LGC-009 | Not Applicable | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TypeEditView.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./AppBundle3/Incident/TypeEditView.ascx` | N/A (retired/replaced in modern architecture). | `./AppBundle3/Incident/TypeEditView.ascx.cs` |
| LGC-009 | Not Applicable | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TypeManagerView.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./AppBundle3/Incident/TypeManagerView.ascx` | N/A (retired/replaced in modern architecture). | `./AppBundle3/Incident/TypeManagerView.ascx.cs` |
| LGC-009 | Completed | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs :: JobSearch.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./AppBundle3/Jobs/JobSearch.ascx` | `./ViewComponents/JobSearchViewComponent.cs` | `./AppBundle3/Jobs/JobSearch.ascx.cs` |
| LGC-009 | Completed | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs :: JobsList.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./AppBundle3/Jobs/JobsList.ascx` | `./ViewComponents/JobsListViewComponent.cs` |  |

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-009 | Not Applicable | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: CategoryEditView | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./AppBundle3/Incident/CategoryEditView.ascx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-009 | Not Applicable | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: CategoryManagerView | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./AppBundle3/Incident/CategoryManagerView.ascx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-009 | Not Applicable | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: InstanceEditView | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./AppBundle3/Incident/InstanceEditView.ascx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-009 | Not Applicable | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: InstanceManagerView | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./AppBundle3/Incident/InstanceManagerView.ascx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-009 | Not Applicable | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TicketManagerView | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./AppBundle3/Incident/TicketManagerView.ascx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-009 | Not Applicable | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TicketView | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./AppBundle3/Incident/TicketView.ascx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-009 | Not Applicable | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TypeEditView | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./AppBundle3/Incident/TypeEditView.ascx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-009 | Not Applicable | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TypeManagerView | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./AppBundle3/Incident/TypeManagerView.ascx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-009 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs :: JobSearch | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./AppBundle3/Jobs/JobSearch.ascx.cs` | `./ViewComponents/JobSearchViewComponent.cs` |
