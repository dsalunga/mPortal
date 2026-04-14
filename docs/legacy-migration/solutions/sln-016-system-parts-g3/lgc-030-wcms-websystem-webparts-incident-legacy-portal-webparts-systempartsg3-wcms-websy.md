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

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File |
| --- | --- | --- | --- | --- | --- |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: Constants | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Constants.cs` |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentCategory | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/IncidentCategory.cs` |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/IncidentHelper.cs` |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentInstance | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/IncidentInstance.cs` |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentTicket | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/IncidentTicket.cs` |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentTicketHistory | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/IncidentTicketHistory.cs` |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentType | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/IncidentType.cs` |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Managers :: IncidentCategoryManager | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Managers/IncidentCategoryManager.cs` |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Managers :: IncidentTypeManager | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Managers/IncidentTypeManager.cs` |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IIncidentCategoryProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IIncidentCategoryProvider.cs` |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IIncidentTicketHistoryProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IIncidentTicketHistoryProvider.cs` |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IIncidentTicketProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IIncidentTicketProvider.cs` |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IIncidentTypeProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IIncidentTypeProvider.cs` |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IncidentCategoryProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IncidentCategoryProvider.cs` |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IncidentInstanceProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IncidentInstanceProvider.cs` |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IncidentTicketHistoryProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IncidentTicketHistoryProvider.cs` |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IncidentTicketProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IncidentTicketProvider.cs` |
| LGC-030 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IncidentTypeProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers/IncidentTypeProvider.cs` |
