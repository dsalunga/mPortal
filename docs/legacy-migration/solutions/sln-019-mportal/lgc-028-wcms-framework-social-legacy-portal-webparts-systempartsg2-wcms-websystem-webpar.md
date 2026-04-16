# LGC-028 - WCMS.Framework.Social

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-028 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/WCMS.Framework.Social.csproj` |
| Modern Project File / Evidence | `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/WCMS.Framework.Social.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:15, Not Applicable:0, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 2 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 13 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Completed | Migration to .NET 10 complete. All source files compile with 0 errors. |
| WebForms Surface Present | No | N/A |
| Endpoint Surface Present | No | N/A |
| Class/Component Porting | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |

## Project References

| --- | --- | --- |
| ../../../WebSystem/WCMS.Common/WCMS.Common.csproj |
| ../../../WebSystem/WCMS.Framework/WCMS.Framework.csproj |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-028 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social :: Constant | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Constant.cs` | `./Constant.cs` |
| LGC-028 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social :: GenericWallEvent | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./GenericWallEvent.cs` | `./GenericWallEvent.cs` |
| LGC-028 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social :: IWallUpdateEvent | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./IWallUpdateEvent.cs` | `./IWallUpdateEvent.cs` |
| LGC-028 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Managers :: WallPluginManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Managers/WallPluginManager.cs` | `./Managers/WallPluginManager.cs` |
| LGC-028 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Managers :: WallUpdateManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Managers/WallUpdateManager.cs` | `./Managers/WallUpdateManager.cs` |
| LGC-028 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social :: ProfileUpdateEvent | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./ProfileUpdateEvent.cs` | `./ProfileUpdateEvent.cs` |
| LGC-028 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Providers :: IWallPluginProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IWallPluginProvider.cs` | `./Providers/IWallPluginProvider.cs` |
| LGC-028 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Providers :: IWallUpdateProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IWallUpdateProvider.cs` | `./Providers/IWallUpdateProvider.cs` |
| LGC-028 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Providers :: WallPluginSqlProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/WallPluginSqlProvider.cs` | `./Providers/WallPluginSqlProvider.cs` |
| LGC-028 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Providers :: WallUpdateSqlProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/WallUpdateSqlProvider.cs` | `./Providers/WallUpdateSqlProvider.cs` |
| LGC-028 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social :: WallPlugin | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WallPlugin.cs` | `./WallPlugin.cs` |
| LGC-028 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social :: WallUpdate | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WallUpdate.cs` | `./WallUpdate.cs` |
| LGC-028 | Completed | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social :: WallUpdateEventBase | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./WallUpdateEventBase.cs` | `./WallUpdateEventBase.cs` |
