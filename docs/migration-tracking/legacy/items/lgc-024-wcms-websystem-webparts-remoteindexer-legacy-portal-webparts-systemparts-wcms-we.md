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

| Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common :: FtpIndexer | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common/FtpIndexer.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common :: IRemoteIndexer | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common/IRemoteIndexer.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common :: WindowsFileSystem | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Common/WindowsFileSystem.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: Constants | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Constants.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: Program | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Program.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers :: IRemoteItemProvider | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers/IRemoteItemProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers :: IRemoteLibraryProvider | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers/IRemoteLibraryProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers :: RemoteItemSqlProvider | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers/RemoteItemSqlProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers :: RemoteLibrarySqlProvider | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Providers/RemoteLibrarySqlProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteIndexerTask | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteIndexerTask.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteIndexerViewBase | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteIndexerViewBase.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteItem | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteItem.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteLibrary | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteLibrary.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteLibraryHelper | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteLibraryHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer :: RemoteLibraryIndexer | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/RemoteLibraryIndexer.cs` | Library/business component; assess API compatibility and dependencies. |
