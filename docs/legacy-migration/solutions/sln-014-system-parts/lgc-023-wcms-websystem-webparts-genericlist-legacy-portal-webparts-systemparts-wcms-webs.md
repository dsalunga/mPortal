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

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericList | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./GenericList.cs` | `./GenericList.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListColumn | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./GenericListColumn.cs` | `./GenericListColumn.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListColumnOption | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./GenericListColumnOption.cs` | `./GenericListColumnOption.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListColumnOptionType | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./GenericListColumnOptionType.cs` | `./GenericListColumnOptionType.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListField | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./GenericListField.cs` | `./GenericListField.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListPartition | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./GenericListPartition.cs` | `./GenericListPartition.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListRow | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./GenericListRow.cs` | `./GenericListRow.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Interfaces :: IGenericListFieldProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Interfaces/IGenericListFieldProvider.cs` | `./Interfaces/IGenericListFieldProvider.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Interfaces :: IGenericListRowProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Interfaces/IGenericListRowProvider.cs` | `./Interfaces/IGenericListRowProvider.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Providers :: GenericListFieldProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/GenericListFieldProvider.cs` | `./Providers/GenericListFieldProvider.cs` |
| LGC-023 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Providers :: GenericListRowProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/GenericListRowProvider.cs` | `./Providers/GenericListRowProvider.cs` |
