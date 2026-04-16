# LGC-025 - WCMS.WebSystem.WebParts.WeeklyScheduler

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-025 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/WCMS.WebSystem.Apps.WeeklyScheduler.csproj` |
| Modern Project File / Evidence | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/WCMS.WebSystem.Apps.WeeklyScheduler.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:6, Not Applicable:3, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 4 |

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

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Modern File / Evidence |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-025 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/Interfaces :: IWeeklySchedulerTaskProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/Interfaces/IWeeklySchedulerTaskProvider.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/Interfaces/IWeeklySchedulerTaskProvider.cs` |
| LGC-025 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/Providers :: WeeklySchedulerSqlProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/Providers/WeeklySchedulerSqlProvider.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/Providers/WeeklySchedulerSqlProvider.cs` |
| LGC-025 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler :: WeeklySchedulerPresenter | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/WeeklySchedulerPresenter.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/WeeklySchedulerPresenter.cs` |
| LGC-025 | Not Applicable | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler :: WeeklySchedulerTask | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/WeeklySchedulerTask.cs` | N/A (retired/replaced in modern architecture). |
