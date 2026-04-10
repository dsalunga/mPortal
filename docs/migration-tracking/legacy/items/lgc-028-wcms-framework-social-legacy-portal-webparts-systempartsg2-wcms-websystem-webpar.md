# LGC-028 - WCMS.Framework.Social

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-028 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/WCMS.Framework.Social.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Not Stated |
| Status Basis | Legacy target framework only (v4.8). |
| Project References | 2 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 13 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Discovery / Planning | Assess framework/API compatibility and plan library porting. |
| WebForms Surface Present | No | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

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

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- | --- | --- |
| LGC-028 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social :: Constant | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Constant.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-028 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social :: GenericWallEvent | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/GenericWallEvent.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-028 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social :: IWallUpdateEvent | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/IWallUpdateEvent.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-028 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Managers :: WallPluginManager | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Managers/WallPluginManager.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-028 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Managers :: WallUpdateManager | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Managers/WallUpdateManager.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-028 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social :: ProfileUpdateEvent | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/ProfileUpdateEvent.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-028 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Providers :: IWallPluginProvider | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Providers/IWallPluginProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-028 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Providers :: IWallUpdateProvider | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Providers/IWallUpdateProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-028 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Providers :: WallPluginSqlProvider | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Providers/WallPluginSqlProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-028 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Providers :: WallUpdateSqlProvider | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/Providers/WallUpdateSqlProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-028 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social :: WallPlugin | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/WallPlugin.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-028 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social :: WallUpdate | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/WallUpdate.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-028 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social :: WallUpdateEventBase | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/WallUpdateEventBase.cs` | Library/business component; assess API compatibility and dependencies. |
