# LGC-026 - WCMS.WebSystem.SystemPartsG2

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-026 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/WCMS.WebSystem.Apps.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Not Stated |
| Status Basis | Legacy target framework only (v4.8). |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 3 |

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
| Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/Newsletter :: NewsletterEntry | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/Newsletter/NewsletterEntry.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/Newsletter/Providers :: INewsletterProvider | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/Newsletter/Providers/INewsletterProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/Newsletter/Providers :: NewsletterSqlProvider | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/Newsletter/Providers/NewsletterSqlProvider.cs` | Library/business component; assess API compatibility and dependencies. |
