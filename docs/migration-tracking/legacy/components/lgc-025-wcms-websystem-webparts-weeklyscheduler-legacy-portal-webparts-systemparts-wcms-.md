# LGC-025 - WCMS.WebSystem.WebParts.WeeklyScheduler

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-025 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/WCMS.WebSystem.Apps.WeeklyScheduler.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Not Stated |
| Status Basis | Legacy target framework only (v4.8). |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 4 |

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
| LGC-025 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/Interfaces :: IWeeklySchedulerTaskProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/Interfaces/IWeeklySchedulerTaskProvider.cs` |
| LGC-025 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/Providers :: WeeklySchedulerSqlProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/Providers/WeeklySchedulerSqlProvider.cs` |
| LGC-025 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler :: WeeklySchedulerPresenter | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/WeeklySchedulerPresenter.cs` |
| LGC-025 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler :: WeeklySchedulerTask | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/WeeklySchedulerTask.cs` |
