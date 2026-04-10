# LGC-041 - WCMS.WebSystem.Utilities

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-041 |
| Project Type | Library |
| Project File | `legacy/Portal/WebSystem/WCMS.WebSystem.Utilities/WCMS.WebSystem.Utilities.csproj` |
| Project Directory | `legacy/Portal/WebSystem/WCMS.WebSystem.Utilities` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Partial |
| Status Basis | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| Project References | 3 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 3 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Execution (In Progress) | Close remaining legacy runtime/UI/endpoint gaps and define cutover tests. |
| WebForms Surface Present | No | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

## Project References

| Reference Include |
| --- |
| ../WCMS.Common/WCMS.Common.csproj |
| ../WCMS.Framework.Core.SqlProvider.Smo/WCMS.Framework.Core.SqlProvider.Smo.csproj |
| ../WCMS.Framework/WCMS.Framework.csproj |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- |
| Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.Utilities :: DbManager | `legacy/Portal/WebSystem/WCMS.WebSystem.Utilities/DbManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.Utilities/EmailUpdater :: EmailUpdaterTask | `legacy/Portal/WebSystem/WCMS.WebSystem.Utilities/EmailUpdater/EmailUpdaterTask.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.Utilities/Spammer :: SenderTask | `legacy/Portal/WebSystem/WCMS.WebSystem.Utilities/Spammer/SenderTask.cs` | Library/business component; assess API compatibility and dependencies. |
