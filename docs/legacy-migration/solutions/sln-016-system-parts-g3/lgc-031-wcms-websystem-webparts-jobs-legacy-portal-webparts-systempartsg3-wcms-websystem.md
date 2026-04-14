# LGC-031 - WCMS.WebSystem.WebParts.Jobs

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-031 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Jobs/WCMS.WebSystem.Apps.Jobs.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Jobs` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Not Stated |
| Status Basis | Legacy target framework only (v4.8). |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 4 |

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
| LGC-031 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Jobs :: Job | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Jobs/Job.cs` |
| LGC-031 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Jobs :: JobResult | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Jobs/JobResult.cs` |
| LGC-031 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Jobs/Providers :: IJobProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Jobs/Providers/IJobProvider.cs` |
| LGC-031 | Not Stated | Class Component | legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Jobs/Providers :: JobSqlProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Jobs/Providers/JobSqlProvider.cs` |
