# LGC-030 - WCMS.WebSystem.WebParts.Incident

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-030 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/WCMS.WebSystem.Apps.Incident.csproj` |
| Modern Project File / Evidence | `Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/WCMS.WebSystem.Apps.Incident.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:20, Not Applicable:0, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 18 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Completed | Migration to .NET 10 complete. All source files compile with 0 errors. |
| WebForms Surface Present | No | N/A |
| Endpoint Surface Present | No | N/A |
| Class/Component Porting | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: Constants | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Constants.cs` | `./Constants.cs` |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentCategory | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./IncidentCategory.cs` | `./IncidentCategory.cs` |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./IncidentHelper.cs` | `./IncidentHelper.cs` |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentInstance | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./IncidentInstance.cs` | `./IncidentInstance.cs` |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentTicket | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./IncidentTicket.cs` | `./IncidentTicket.cs` |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentTicketHistory | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./IncidentTicketHistory.cs` | `./IncidentTicketHistory.cs` |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident :: IncidentType | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./IncidentType.cs` | `./IncidentType.cs` |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Managers :: IncidentCategoryManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Managers/IncidentCategoryManager.cs` | `./Managers/IncidentCategoryManager.cs` |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Managers :: IncidentTypeManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Managers/IncidentTypeManager.cs` | `./Managers/IncidentTypeManager.cs` |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IIncidentCategoryProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IIncidentCategoryProvider.cs` | `./Providers/IIncidentCategoryProvider.cs` |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IIncidentTicketHistoryProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IIncidentTicketHistoryProvider.cs` | `./Providers/IIncidentTicketHistoryProvider.cs` |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IIncidentTicketProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IIncidentTicketProvider.cs` | `./Providers/IIncidentTicketProvider.cs` |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IIncidentTypeProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IIncidentTypeProvider.cs` | `./Providers/IIncidentTypeProvider.cs` |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IncidentCategoryProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IncidentCategoryProvider.cs` | `./Providers/IncidentCategoryProvider.cs` |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IncidentInstanceProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IncidentInstanceProvider.cs` | `./Providers/IncidentInstanceProvider.cs` |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IncidentTicketHistoryProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IncidentTicketHistoryProvider.cs` | `./Providers/IncidentTicketHistoryProvider.cs` |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IncidentTicketProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IncidentTicketProvider.cs` | `./Providers/IncidentTicketProvider.cs` |
| LGC-030 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/Providers :: IncidentTypeProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IncidentTypeProvider.cs` | `./Providers/IncidentTypeProvider.cs` |
