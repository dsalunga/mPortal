# LGC-018 - WCMS.WebSystem.WebParts.FileManager

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-018 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.FileManager/WCMS.WebSystem.Apps.FileManager.csproj` |
| Modern Project File / Evidence | `Portal/WebParts/SystemParts/WCMS.Framework.FileManager/WCMS.WebSystem.Apps.FileManager.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.FileManager` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:13, Not Applicable:2, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 12 |

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
| LGC-018 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.FileManager :: Constants | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Constants.cs` | `./Constants.cs` |
| LGC-018 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.FileManager :: FileIdentity | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./FileIdentity.cs` | `./FileIdentity.cs` |
| LGC-018 | Not Applicable | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.FileManager :: FileManagerBase | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./FileManagerBase.cs` | N/A (retired/replaced in modern architecture). |
| LGC-018 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.FileManager :: FileManagerFile | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./FileManagerFile.cs` | `./FileManagerFile.cs` |
| LGC-018 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.FileManager :: FileManagerFolder | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./FileManagerFolder.cs` | `./FileManagerFolder.cs` |
| LGC-018 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.FileManager :: FileVersion | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./FileVersion.cs` | `./FileVersion.cs` |
| LGC-018 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.FileManager/Providers :: FileIdentityProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/FileIdentityProvider.cs` | `./Providers/FileIdentityProvider.cs` |
| LGC-018 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.FileManager/Providers :: FileManagerFileProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/FileManagerFileProvider.cs` | `./Providers/FileManagerFileProvider.cs` |
| LGC-018 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.FileManager/Providers :: FileManagerFolderProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/FileManagerFolderProvider.cs` | `./Providers/FileManagerFolderProvider.cs` |
| LGC-018 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.FileManager/Providers :: FileVersionProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/FileVersionProvider.cs` | `./Providers/FileVersionProvider.cs` |
| LGC-018 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.FileManager/Providers :: IFileIdentityProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IFileIdentityProvider.cs` | `./Providers/IFileIdentityProvider.cs` |
| LGC-018 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.FileManager/Providers :: IFileVersionProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IFileVersionProvider.cs` | `./Providers/IFileVersionProvider.cs` |
