# SLN-019 - mPortal

## Solution Metadata

| Field | Value |
| --- | --- |
| Solution ID | SLN-019 |
| Solution Name | `mPortal` |
| Solution File | `legacy/Portal/WebSystem/mPortal.sln` |
| Projects In Solution | 13 |
| Mapped Projects | 13 |
| Unmapped Projects | 0 |
| Aggregate Migration Status | Mixed (Do Not Migrate As-Is, Partial, Not Stated) |
| Status Breakdown | Do Not Migrate As-Is:1, Not Stated:4, Partial:8 |
| Mapped LGC Items | LGC-010, LGC-011, LGC-012, LGC-028, LGC-034, LGC-036, LGC-037, LGC-038, LGC-039, LGC-040, LGC-041, LGC-042, LGC-051 |

## Projects In Solution

| Solution Item ID | Migration Status | Project Type | Project Name | LGC ID | LGC Item | Component Card | Project File |
| --- | --- | --- | --- | --- | --- | --- | --- |
| SLN-019-P01 | Do Not Migrate As-Is | Project | `FredCK.FCKeditorV2` | LGC-034 | `FredCK.FCKeditorV2` | [Card](./shared/lgc-034-fredck-fckeditorv2-legacy-portal-websystem-fckeditor-net-2-6-3.md) | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.csproj` |
| SLN-019-P02 | Partial | Project | `WCMS.Common` | LGC-036 | `WCMS.Common` | [Card](./sln-019-mportal/lgc-036-wcms-common-legacy-portal-websystem-wcms-common.md) | `legacy/Portal/WebSystem/WCMS.Common/WCMS.Common.csproj` |
| SLN-019-P03 | Partial | Project | `WCMS.Framework` | LGC-037 | `WCMS.Framework` | [Card](./sln-019-mportal/lgc-037-wcms-framework-legacy-portal-websystem-wcms-framework.md) | `legacy/Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj` |
| SLN-019-P04 | Not Stated | Project | `WCMS.Framework.Agent` | LGC-010 | `WCMS.Framework.Agent` | [Card](./sln-019-mportal/lgc-010-wcms-framework-agent-legacy-portal-websystem-wcms-framewok-agent.md) | `legacy/Portal/WebSystem/WCMS.Framewok.Agent/WCMS.Framework.Agent.csproj` |
| SLN-019-P05 | Partial | Project | `WCMS.Framework.Core.SqlProvider` | LGC-038 | `WCMS.Framework.Core.SqlProvider` | [Card](./sln-019-mportal/lgc-038-wcms-framework-core-sqlprovider-legacy-portal-websystem-wcms-framework-core-sqlp.md) | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WCMS.Framework.Core.SqlProvider.csproj` |
| SLN-019-P06 | Partial | Project | `WCMS.Framework.Core.XmlProvider` | LGC-040 | `WCMS.Framework.Core.XmlProvider` | [Card](./sln-019-mportal/lgc-040-wcms-framework-core-xmlprovider-legacy-portal-websystem-wcms-framework-core-xmlp.md) | `legacy/Portal/WebSystem/WCMS.Framework.Core.XmlProvider/WCMS.Framework.Core.XmlProvider.csproj` |
| SLN-019-P07 | Partial | Project | `WCMS.Framework.Core.SqlProvider.Smo` | LGC-039 | `WCMS.Framework.Core.SqlProvider.Smo` | [Card](./sln-019-mportal/lgc-039-wcms-framework-core-sqlprovider-smo-legacy-portal-websystem-wcms-framework-core-.md) | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider.Smo/WCMS.Framework.Core.SqlProvider.Smo.csproj` |
| SLN-019-P08 | Partial | Project | `WCMS.WebSystem` | LGC-042 | `WCMS.WebSystem` | [Card](./sln-019-mportal/lgc-042-wcms-websystem-legacy-portal-websystem-wcms-websystem-viewmodels.md) | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/WCMS.WebSystem.csproj` |
| SLN-019-P09 | Partial | Project | `WCMS.WebSystem.Utilities` | LGC-041 | `WCMS.WebSystem.Utilities` | [Card](./sln-019-mportal/lgc-041-wcms-websystem-utilities-legacy-portal-websystem-wcms-websystem-utilities.md) | `legacy/Portal/WebSystem/WCMS.WebSystem.Utilities/WCMS.WebSystem.Utilities.csproj` |
| SLN-019-P10 | Partial | Project | `WCMS.WebSystem.WebApp` | LGC-012 | `WCMS.WebSystem.WebApp` | [Card](./sln-019-mportal/lgc-012-wcms-websystem-webapp-legacy-portal-websystem-websystem-mvc.md) | `legacy/Portal/WebSystem/WebSystem-MVC/WCMS.WebSystem.WebApp.csproj` |
| SLN-019-P11 | Not Stated | Project | `WCMS.Framework.AgentService` | LGC-011 | `WCMS.Framework.AgentService` | [Card](./sln-019-mportal/lgc-011-wcms-framework-agentservice-legacy-portal-websystem-wcms-framework-agentservice.md) | `legacy/Portal/WebSystem/WCMS.Framework.AgentService/WCMS.Framework.AgentService.csproj` |
| SLN-019-P12 | Not Stated | Project | `WCMS.Framework.Social` | LGC-028 | `WCMS.Framework.Social` | [Card](./sln-019-mportal/lgc-028-wcms-framework-social-legacy-portal-webparts-systempartsg2-wcms-websystem-webpar.md) | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/WCMS.Framework.Social.csproj` |
| SLN-019-P13 | Not Stated | Database Project | `WCMS.Framework.SqlDabase` | LGC-051 | `WCMS.Framework.SqlDabase` | [Card](./sln-019-mportal/lgc-051-wcms-framework-sqldabase-legacy-portal-websystem-wcms-framework-sqldabase.md) | `legacy/Portal/WebSystem/WCMS.Framework.SqlDabase/WCMS.Framework.SqlDabase.sqlproj` |

## Migration Actions

| Action | Priority | Status | Notes |
| --- | --- | --- | --- |
| Validate solution-level migration sequencing against component statuses | High | Not Stated | Use aggregate and row statuses as planning baseline. |
| Update per-project row statuses as migration progresses | Medium | Not Stated | Keep solution card synchronized with implementation state. |
