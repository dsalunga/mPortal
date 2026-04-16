# LGC-024 - WCMS.WebSystem.WebParts.RemoteIndexer

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-024 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/WCMS.WebSystem.Apps.RemoteIndexer.csproj` |
| Modern Project File / Evidence | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/WCMS.WebSystem.Apps.RemoteIndexer.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:16, Not Applicable:2, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 15 |

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
| LGC-024 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common :: FtpIndexer | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common/FtpIndexer.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common/FtpIndexer.cs` |
| LGC-024 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common :: IRemoteIndexer | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common/IRemoteIndexer.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common/IRemoteIndexer.cs` |
| LGC-024 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common :: WindowsFileSystem | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common/WindowsFileSystem.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common/WindowsFileSystem.cs` |
| LGC-024 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: Constants | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Constants.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Constants.cs` |
| LGC-024 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: Program | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Program.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Program.cs` |
| LGC-024 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers :: IRemoteItemProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers/IRemoteItemProvider.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers/IRemoteItemProvider.cs` |
| LGC-024 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers :: IRemoteLibraryProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers/IRemoteLibraryProvider.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers/IRemoteLibraryProvider.cs` |
| LGC-024 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers :: RemoteItemSqlProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers/RemoteItemSqlProvider.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers/RemoteItemSqlProvider.cs` |
| LGC-024 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers :: RemoteLibrarySqlProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers/RemoteLibrarySqlProvider.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers/RemoteLibrarySqlProvider.cs` |
| LGC-024 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteIndexerTask | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteIndexerTask.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteIndexerTask.cs` |
| LGC-024 | Not Applicable | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteIndexerViewBase | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteIndexerViewBase.cs` | N/A (retired/replaced in modern architecture). |
| LGC-024 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteItem | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteItem.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteItem.cs` |
| LGC-024 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteLibrary | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteLibrary.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteLibrary.cs` |
| LGC-024 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteLibraryHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteLibraryHelper.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteLibraryHelper.cs` |
| LGC-024 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteLibraryIndexer | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteLibraryIndexer.cs` | `Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteLibraryIndexer.cs` |
