# LGC-040 - WCMS.Framework.Core.XmlProvider

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-040 |
| Project Type | Library |
| Project File | `legacy/Portal/WebSystem/WCMS.Framework.Core.XmlProvider/WCMS.Framework.Core.XmlProvider.csproj` |
| Project Directory | `legacy/Portal/WebSystem/WCMS.Framework.Core.XmlProvider` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Partial |
| Status Basis | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| Project References | 2 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 1 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Execution (In Progress) | Close remaining legacy runtime/UI/endpoint gaps and define cutover tests. |
| WebForms Surface Present | No | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

## Project References

| --- | --- | --- |
| ../WCMS.Common/WCMS.Common.csproj |
| ../WCMS.Framework/WCMS.Framework.csproj |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- | --- | --- |
| LGC-040 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Framework.Core.XmlProvider :: WebObjectProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.XmlProvider/WebObjectProvider.cs` | Library/business component; assess API compatibility and dependencies. |
