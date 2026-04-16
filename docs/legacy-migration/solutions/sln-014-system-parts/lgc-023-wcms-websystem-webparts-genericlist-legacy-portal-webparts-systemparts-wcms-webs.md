# LGC-023 - WCMS.WebSystem.WebParts.GenericList

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-023 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/WCMS.WebSystem.Apps.GenericList.csproj` |
| Modern Project File / Evidence | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/WCMS.WebSystem.Apps.GenericList.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:13, Not Applicable:1, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 11 |

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
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericList | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericList.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericList.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListColumn | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListColumn.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListColumn.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListColumnOption | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListColumnOption.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListColumnOption.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListColumnOptionType | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListColumnOptionType.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListColumnOptionType.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListField | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListField.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListField.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListPartition | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListPartition.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListPartition.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListRow | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListRow.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListRow.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Interfaces :: IGenericListFieldProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Interfaces/IGenericListFieldProvider.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Interfaces/IGenericListFieldProvider.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Interfaces :: IGenericListRowProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Interfaces/IGenericListRowProvider.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Interfaces/IGenericListRowProvider.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Providers :: GenericListFieldProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Providers/GenericListFieldProvider.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Providers/GenericListFieldProvider.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Providers :: GenericListRowProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Providers/GenericListRowProvider.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Providers/GenericListRowProvider.cs` |
