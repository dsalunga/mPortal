# LGC-009 - WCMS.WebSystem.WebParts.SystemPartsG3.WebApp

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-009 |
| Project Type | App |
| Project File | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/WCMS.WebSystem.Apps.SystemApps3.WebApp.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Partial |
| Status Basis | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| Project References | 2 |
| Surface Artifacts | 10 |
| Component/Class Artifacts | 9 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Execution (In Progress) | Close remaining legacy runtime/UI/endpoint gaps and define cutover tests. |
| WebForms Surface Present | Yes | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

## Project References

| --- | --- | --- |
| ../WCMS.WebSystem.WebParts.Incident/WCMS.WebSystem.Apps.Incident.csproj |
| ../WCMS.WebSystem.WebParts.Jobs/WCMS.WebSystem.Apps.Jobs.csproj |

## Pages And Views

_No artifacts found._

## User Controls

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Source File | Code-Behind / Pair | Migration Note |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: CategoryEditView.ascx | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/CategoryEditView.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/CategoryEditView.ascx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: CategoryManagerView.ascx | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/CategoryManagerView.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/CategoryManagerView.ascx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: InstanceEditView.ascx | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/InstanceEditView.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/InstanceEditView.ascx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: InstanceManagerView.ascx | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/InstanceManagerView.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/InstanceManagerView.ascx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TicketManagerView.ascx | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TicketManagerView.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TicketManagerView.ascx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TicketView.ascx | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TicketView.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TicketView.ascx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TypeEditView.ascx | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TypeEditView.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TypeEditView.ascx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TypeManagerView.ascx | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TypeManagerView.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TypeManagerView.ascx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs :: JobSearch.ascx | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs/JobSearch.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs/JobSearch.ascx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs :: JobsList.ascx | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs/JobsList.ascx` |  | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- | --- | --- |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: CategoryEditView | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/CategoryEditView.ascx.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: CategoryManagerView | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/CategoryManagerView.ascx.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: InstanceEditView | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/InstanceEditView.ascx.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: InstanceManagerView | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/InstanceManagerView.ascx.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TicketManagerView | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TicketManagerView.ascx.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TicketView | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TicketView.ascx.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TypeEditView | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TypeEditView.ascx.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TypeManagerView | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TypeManagerView.ascx.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs :: JobSearch | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs/JobSearch.ascx.cs` | Library/business component; assess API compatibility and dependencies. |
