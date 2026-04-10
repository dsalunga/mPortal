# LGC-023 - WCMS.WebSystem.WebParts.GenericList

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-023 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/WCMS.WebSystem.Apps.GenericList.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Not Stated |
| Status Basis | Legacy target framework only (v4.8). |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 11 |

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
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericList | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericList.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListColumn | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListColumn.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListColumnOption | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListColumnOption.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListColumnOptionType | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListColumnOptionType.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListField | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListField.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListPartition | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListPartition.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList :: GenericListRow | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/GenericListRow.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Interfaces :: IGenericListFieldProvider | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Interfaces/IGenericListFieldProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Interfaces :: IGenericListRowProvider | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Interfaces/IGenericListRowProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Providers :: GenericListFieldProvider | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Providers/GenericListFieldProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Providers :: GenericListRowProvider | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Providers/GenericListRowProvider.cs` | Library/business component; assess API compatibility and dependencies. |
