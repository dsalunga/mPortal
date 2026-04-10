# LGC-011 - WCMS.Framework.AgentService

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-011 |
| Project Type | App |
| Project File | `legacy/Portal/WebSystem/WCMS.Framework.AgentService/WCMS.Framework.AgentService.csproj` |
| Project Directory | `legacy/Portal/WebSystem/WCMS.Framework.AgentService` |
| Output Type | WinExe |
| Target Framework | v4.8 |
| Migration Status | Not Stated |
| Status Basis | Legacy target framework only (v4.8). |
| Project References | 4 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 2 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Discovery / Planning | Assess framework/API compatibility and plan library porting. |
| WebForms Surface Present | No | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

## Project References

| --- | --- | --- |
| ../WCMS.Common/WCMS.Common.csproj |
| ../WCMS.Framework.Core.SqlProvider/WCMS.Framework.Core.SqlProvider.csproj |
| ../WCMS.Framework.Core.XmlProvider/WCMS.Framework.Core.XmlProvider.csproj |
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
| LGC-011 | Not Stated | Class Component | legacy/Portal/WebSystem/WCMS.Framework.AgentService :: FrameworkAgentService | `legacy/Portal/WebSystem/WCMS.Framework.AgentService/FrameworkAgentService.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-011 | Not Stated | Class Component | legacy/Portal/WebSystem/WCMS.Framework.AgentService :: Program | `legacy/Portal/WebSystem/WCMS.Framework.AgentService/Program.cs` | Library/business component; assess API compatibility and dependencies. |
