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

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Code-Behind / Pair |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: CategoryEditView.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/CategoryEditView.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/CategoryEditView.ascx.cs` |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: CategoryManagerView.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/CategoryManagerView.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/CategoryManagerView.ascx.cs` |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: InstanceEditView.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/InstanceEditView.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/InstanceEditView.ascx.cs` |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: InstanceManagerView.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/InstanceManagerView.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/InstanceManagerView.ascx.cs` |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TicketManagerView.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TicketManagerView.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TicketManagerView.ascx.cs` |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TicketView.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TicketView.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TicketView.ascx.cs` |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TypeEditView.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TypeEditView.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TypeEditView.ascx.cs` |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TypeManagerView.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TypeManagerView.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TypeManagerView.ascx.cs` |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs :: JobSearch.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs/JobSearch.ascx` | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs/JobSearch.ascx.cs` |
| LGC-009 | Partial | User Control | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs :: JobsList.ascx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs/JobsList.ascx` |  |

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File |
| --- | --- | --- | --- | --- | --- |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: CategoryEditView | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/CategoryEditView.ascx.cs` |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: CategoryManagerView | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/CategoryManagerView.ascx.cs` |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: InstanceEditView | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/InstanceEditView.ascx.cs` |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: InstanceManagerView | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/InstanceManagerView.ascx.cs` |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TicketManagerView | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TicketManagerView.ascx.cs` |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TicketView | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TicketView.ascx.cs` |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TypeEditView | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TypeEditView.ascx.cs` |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident :: TypeManagerView | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TypeManagerView.ascx.cs` |
| LGC-009 | Partial | Class Component | legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs :: JobSearch | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs/JobSearch.ascx.cs` |
