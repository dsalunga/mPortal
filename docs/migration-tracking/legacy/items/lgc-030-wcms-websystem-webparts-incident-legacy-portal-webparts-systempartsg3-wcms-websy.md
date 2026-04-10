# LGC-030 - WCMS.WebSystem.WebParts.Incident

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-030 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/WCMS.WebSystem.Apps.Incident.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Not Stated |
| Status Basis | Legacy target framework only (v4.8). |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 18 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Discovery / Planning | Assess framework/API compatibility and plan library porting. |
| WebForms Surface Present | No | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- | --- | --- |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: Constants | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Constants.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentCategory | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/IncidentCategory.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentHelper | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/IncidentHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentInstance | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/IncidentInstance.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentTicket | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/IncidentTicket.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentTicketHistory | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/IncidentTicketHistory.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentType | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/IncidentType.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Managers :: IncidentCategoryManager | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Managers/IncidentCategoryManager.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Managers :: IncidentTypeManager | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Managers/IncidentTypeManager.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IIncidentCategoryProvider | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IIncidentCategoryProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IIncidentTicketHistoryProvider | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IIncidentTicketHistoryProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IIncidentTicketProvider | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IIncidentTicketProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IIncidentTypeProvider | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IIncidentTypeProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IncidentCategoryProvider | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IncidentCategoryProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IncidentInstanceProvider | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IncidentInstanceProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IncidentTicketHistoryProvider | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IncidentTicketHistoryProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IncidentTicketProvider | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IncidentTicketProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IncidentTypeProvider | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IncidentTypeProvider.cs` | Library/business component; assess API compatibility and dependencies. |
