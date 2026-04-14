# LGC-024 - WCMS.WebSystem.WebParts.RemoteIndexer

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-024 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/WCMS.WebSystem.Apps.RemoteIndexer.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Not Stated |
| Status Basis | Legacy target framework only (v4.8). |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 15 |

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
| LGC-024 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common :: FtpIndexer | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common/FtpIndexer.cs` |
| LGC-024 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common :: IRemoteIndexer | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common/IRemoteIndexer.cs` |
| LGC-024 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common :: WindowsFileSystem | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common/WindowsFileSystem.cs` |
| LGC-024 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: Constants | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Constants.cs` |
| LGC-024 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: Program | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Program.cs` |
| LGC-024 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers :: IRemoteItemProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers/IRemoteItemProvider.cs` |
| LGC-024 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers :: IRemoteLibraryProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers/IRemoteLibraryProvider.cs` |
| LGC-024 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers :: RemoteItemSqlProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers/RemoteItemSqlProvider.cs` |
| LGC-024 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers :: RemoteLibrarySqlProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers/RemoteLibrarySqlProvider.cs` |
| LGC-024 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteIndexerTask | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteIndexerTask.cs` |
| LGC-024 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteIndexerViewBase | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteIndexerViewBase.cs` |
| LGC-024 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteItem | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteItem.cs` |
| LGC-024 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteLibrary | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteLibrary.cs` |
| LGC-024 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteLibraryHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteLibraryHelper.cs` |
| LGC-024 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteLibraryIndexer | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteLibraryIndexer.cs` |
