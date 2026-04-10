# Legacy Migration Master Inventory

This folder tracks migration scope for all discovered legacy projects/components/utilities using repository evidence.

Companion views:

- [Canonical P-ID inventory](./master-inventory-canonical.md)
- [Canonical migration decision matrix](./canonical-cards-migration-status.md)

## Scope And Method

- Source scope: `legacy/`
- Tracked items: all discovered `*.csproj` and `*.sqlproj` files
- Evidence model: target framework metadata, project structure, and discovered runtime artifacts
- Status semantics:
  - `Completed`: modern target framework explicitly set (for example `net8.0+`)
  - `Partial`: legacy target with evidence of ongoing migration/build transition
  - `Not Stated`: legacy target and no explicit migration marker

## Inventory Summary

| Metric | Value |
| --- | --- |
| Total tracked items | 51 |
| Status - Completed | 1 |
| Status - Partial | 20 |
| Status - Not Stated | 30 |
| Type - App | 12 |
| Type - Component | 19 |
| Type - Library | 11 |
| Type - Utility | 6 |
| Type - Database | 3 |

## App Items

| ID | Item | Project File | Target Framework | Migration Status | Surface / Components | Detail | Status Basis |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-001 | BibleReader.WebApp | `legacy/BibleReader/BibleReader/BibleReader.WebApp.csproj` | v4.7 | Partial | 9 / 10 | [card](items/lgc-001-biblereader-webapp-legacy-biblereader-biblereader.md) | Legacy target (v4.7) with modern build artifacts in obj/bin. |
| LGC-002 | BibleReader.Core | `legacy/BibleReader/BibleReader.Core/BibleReader.Core.csproj` | v4.8 | Not Stated | 0 / 12 | [card](items/lgc-002-biblereader-core-legacy-biblereader-biblereader-core.md) | Legacy target framework only (v4.8). |
| LGC-003 | WCMS.LessonReviewer | `legacy/LessonReviewer/LessonReviewer/LessonReviewer.csproj` | v4.8 | Partial | 6 / 7 | [card](items/lgc-003-wcms-lessonreviewer-legacy-lessonreviewer-lessonreviewer.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| LGC-004 | WCMS.LessonReviewer.Core | `legacy/LessonReviewer/LessonReviewer.Core/LessonReviewer.Core.csproj` | v4.8 | Not Stated | 0 / 4 | [card](items/lgc-004-wcms-lessonreviewer-core-legacy-lessonreviewer-lessonreviewer-core.md) | Legacy target framework only (v4.8). |
| LGC-005 | WCMS.WebSystem.WebParts.BranchLocator.WebApp | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/WCMS.WebSystem.Apps.BranchLocator.WebApp.csproj` | v4.8 | Partial | 12 / 5 | [card](items/lgc-005-wcms-websystem-webparts-branchlocator-webapp-legacy-portal-webparts-branchlocato.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| LGC-006 | WCMS.WebSystem.Apps.Integration.WebApp | `legacy/Portal/WebParts/Integration/IntegrationParts/WCMS.WebSystem.Apps.Integration.WebApp.csproj` | v4.8 | Partial | 159 / 103 | [card](items/lgc-006-wcms-websystem-apps-integration-webapp-legacy-portal-webparts-integration-integr.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| LGC-007 | WCMS.WebSystem.WebParts.SystemParts.WebApp | `legacy/Portal/WebParts/SystemParts/SystemParts/WCMS.WebSystem.Apps.SystemApps.WebApp.csproj` | v4.8 | Partial | 145 / 124 | [card](items/lgc-007-wcms-websystem-webparts-systemparts-webapp-legacy-portal-webparts-systemparts-sy.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| LGC-008 | WCMS.WebSystem.WebParts.SystemPartsG2.WebApp | `legacy/Portal/WebParts/SystemPartsG2/SystemPartsG2/WCMS.WebSystem.Apps.SystemApps2.WebApp.csproj` | v4.8 | Partial | 59 / 47 | [card](items/lgc-008-wcms-websystem-webparts-systempartsg2-webapp-legacy-portal-webparts-systempartsg.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| LGC-009 | WCMS.WebSystem.WebParts.SystemPartsG3.WebApp | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/WCMS.WebSystem.Apps.SystemApps3.WebApp.csproj` | v4.8 | Partial | 10 / 9 | [card](items/lgc-009-wcms-websystem-webparts-systempartsg3-webapp-legacy-portal-webparts-systempartsg.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| LGC-010 | WCMS.Framework.Agent | `legacy/Portal/WebSystem/WCMS.Framewok.Agent/WCMS.Framework.Agent.csproj` | v4.8 | Not Stated | 0 / 1 | [card](items/lgc-010-wcms-framework-agent-legacy-portal-websystem-wcms-framewok-agent.md) | Legacy target framework only (v4.8). |
| LGC-011 | WCMS.Framework.AgentService | `legacy/Portal/WebSystem/WCMS.Framework.AgentService/WCMS.Framework.AgentService.csproj` | v4.8 | Not Stated | 0 / 2 | [card](items/lgc-011-wcms-framework-agentservice-legacy-portal-websystem-wcms-framework-agentservice.md) | Legacy target framework only (v4.8). |
| LGC-012 | WCMS.WebSystem.WebApp | `legacy/Portal/WebSystem/WebSystem-MVC/WCMS.WebSystem.WebApp.csproj` | v4.8 | Partial | 258 / 216 | [card](items/lgc-012-wcms-websystem-webapp-legacy-portal-websystem-websystem-mvc.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. |

## Component Items

| ID | Item | Project File | Target Framework | Migration Status | Surface / Components | Detail | Status Basis |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-013 | WCMS.WebSystem.Apps.BranchLocator | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator/WCMS.WebSystem.Apps.BranchLocator.csproj` | v4.8 | Not Stated | 0 / 7 | [card](items/lgc-013-wcms-websystem-apps-branchlocator-legacy-portal-webparts-branchlocator-wcms-webs.md) | Legacy target framework only (v4.8). |
| LGC-014 | WCMS.WebSystem.Apps.BibleReader | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/WCMS.WebSystem.Apps.BibleReader.csproj` | v4.8 | Not Stated | 0 / 11 | [card](items/lgc-014-wcms-websystem-apps-biblereader-legacy-portal-webparts-integration-wcms-websyste.md) | Legacy target framework only (v4.8). |
| LGC-015 | WCMS.WebSystem.Apps.Integration | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration/WCMS.WebSystem.Apps.Integration.csproj` | v4.8 | Not Stated | 0 / 84 | [card](items/lgc-015-wcms-websystem-apps-integration-legacy-portal-webparts-integration-wcms-websyste.md) | Legacy target framework only (v4.8). |
| LGC-016 | WCMS.WebSystem.Apps.Integration.UnitTest | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.UnitTest/WCMS.WebSystem.Apps.Integration.UnitTest.csproj` | v4.8 | Partial | 0 / 2 | [card](items/lgc-016-wcms-websystem-apps-integration-unittest-legacy-portal-webparts-integration-wcms.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| LGC-017 | SDKTest | `legacy/Portal/WebParts/SDKTest/SDKTest/SDKTest.csproj` | v4.8 | Partial | 1 / 1 | [card](items/lgc-017-sdktest-legacy-portal-webparts-sdktest-sdktest.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| LGC-018 | WCMS.WebSystem.WebParts.FileManager | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.FileManager/WCMS.WebSystem.Apps.FileManager.csproj` | v4.8 | Not Stated | 0 / 12 | [card](items/lgc-018-wcms-websystem-webparts-filemanager-legacy-portal-webparts-systemparts-wcms-fram.md) | Legacy target framework only (v4.8). |
| LGC-019 | WCMS.WebSystem.WebParts | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/WCMS.WebSystem.Apps.csproj` | v4.8 | Not Stated | 0 / 39 | [card](items/lgc-019-wcms-websystem-webparts-legacy-portal-webparts-systemparts-wcms-framework-webpar.md) | Legacy target framework only (v4.8). |
| LGC-020 | WCMS.WebSystem.WebParts.EventCalendar | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/WCMS.WebSystem.Apps.EventCalendar.csproj` | v4.8 | Not Stated | 0 / 22 | [card](items/lgc-020-wcms-websystem-webparts-eventcalendar-legacy-portal-webparts-systemparts-wcms-fr.md) | Legacy target framework only (v4.8). |
| LGC-021 | WCMS.WebSystem.WebParts.Article | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/WCMS.WebSystem.Apps.Article.csproj` | v4.8 | Not Stated | 0 / 21 | [card](items/lgc-021-wcms-websystem-webparts-article-legacy-portal-webparts-systemparts-wcms-websyste.md) | Legacy target framework only (v4.8). |
| LGC-022 | WCMS.WebSystem.WebParts.Contact | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Contact/WCMS.WebSystem.Apps.Contact.csproj` | v4.8 | Not Stated | 0 / 8 | [card](items/lgc-022-wcms-websystem-webparts-contact-legacy-portal-webparts-systemparts-wcms-websyste.md) | Legacy target framework only (v4.8). |
| LGC-023 | WCMS.WebSystem.WebParts.GenericList | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/WCMS.WebSystem.Apps.GenericList.csproj` | v4.8 | Not Stated | 0 / 11 | [card](items/lgc-023-wcms-websystem-webparts-genericlist-legacy-portal-webparts-systemparts-wcms-webs.md) | Legacy target framework only (v4.8). |
| LGC-024 | WCMS.WebSystem.WebParts.RemoteIndexer | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/WCMS.WebSystem.Apps.RemoteIndexer.csproj` | v4.8 | Not Stated | 0 / 15 | [card](items/lgc-024-wcms-websystem-webparts-remoteindexer-legacy-portal-webparts-systemparts-wcms-we.md) | Legacy target framework only (v4.8). |
| LGC-025 | WCMS.WebSystem.WebParts.WeeklyScheduler | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/WCMS.WebSystem.Apps.WeeklyScheduler.csproj` | v4.8 | Not Stated | 0 / 4 | [card](items/lgc-025-wcms-websystem-webparts-weeklyscheduler-legacy-portal-webparts-systemparts-wcms-.md) | Legacy target framework only (v4.8). |
| LGC-026 | WCMS.WebSystem.SystemPartsG2 | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/WCMS.WebSystem.Apps.csproj` | v4.8 | Not Stated | 0 / 3 | [card](items/lgc-026-wcms-websystem-systempartsg2-legacy-portal-webparts-systempartsg2-wcms-websystem.md) | Legacy target framework only (v4.8). |
| LGC-027 | WCMS.WebSystem.WebParts.Forum | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Forum/WCMS.WebSystem.Apps.Forum.csproj` | v4.8 | Not Stated | 0 / 4 | [card](items/lgc-027-wcms-websystem-webparts-forum-legacy-portal-webparts-systempartsg2-wcms-websyste.md) | Legacy target framework only (v4.8). |
| LGC-028 | WCMS.Framework.Social | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/WCMS.Framework.Social.csproj` | v4.8 | Not Stated | 0 / 13 | [card](items/lgc-028-wcms-framework-social-legacy-portal-webparts-systempartsg2-wcms-websystem-webpar.md) | Legacy target framework only (v4.8). |
| LGC-029 | WCMS.WebSystem.WebParts.Social.ViewModel | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social.ViewModel/WCMS.WebSystem.Apps.Social.ViewModel.csproj` | v4.8 | Not Stated | 0 / 2 | [card](items/lgc-029-wcms-websystem-webparts-social-viewmodel-legacy-portal-webparts-systempartsg2-wc.md) | Legacy target framework only (v4.8). |
| LGC-030 | WCMS.WebSystem.WebParts.Incident | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/WCMS.WebSystem.Apps.Incident.csproj` | v4.8 | Not Stated | 0 / 18 | [card](items/lgc-030-wcms-websystem-webparts-incident-legacy-portal-webparts-systempartsg3-wcms-websy.md) | Legacy target framework only (v4.8). |
| LGC-031 | WCMS.WebSystem.WebParts.Jobs | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Jobs/WCMS.WebSystem.Apps.Jobs.csproj` | v4.8 | Not Stated | 0 / 4 | [card](items/lgc-031-wcms-websystem-webparts-jobs-legacy-portal-webparts-systempartsg3-wcms-websystem.md) | Legacy target framework only (v4.8). |

## Library Items

| ID | Item | Project File | Target Framework | Migration Status | Surface / Components | Detail | Status Basis |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-032 | WCMS.Common | `legacy/Core/WCMS.Common/WCMS.Common.csproj` | net8.0 | Completed | 0 / 55 | [card](items/lgc-032-wcms-common-legacy-core-wcms-common.md) | Modern target framework detected (net8.0). |
| LGC-033 | Media-Player-ASP.NET-Control | `legacy/Libraries/Media-Player-ASP.NET-Control/Media-Player-ASP.NET-Control/Media-Player-ASP.NET-Control.csproj` | v4.5.2 | Partial | 0 / 1 | [card](items/lgc-033-media-player-asp-net-control-legacy-libraries-media-player-asp-net-control-media.md) | Legacy target (v4.5.2) with modern build artifacts in obj/bin. |
| LGC-034 | FredCK.FCKeditorV2 | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.csproj` | v4.8 | Partial | 8 / 18 | [card](items/lgc-034-fredck-fckeditorv2-legacy-portal-websystem-fckeditor-net-2-6-3.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| LGC-035 | FredCK.FCKeditorV2.vs2003 | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.vs2003.csproj` | Not specified | Partial | 8 / 18 | [card](items/lgc-035-fredck-fckeditorv2-vs2003-legacy-portal-websystem-fckeditor-net-2-6-3.md) | Modern build artifacts detected, but project target is not explicit. |
| LGC-036 | WCMS.Common | `legacy/Portal/WebSystem/WCMS.Common/WCMS.Common.csproj` | v4.8 | Partial | 0 / 55 | [card](items/lgc-036-wcms-common-legacy-portal-websystem-wcms-common.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| LGC-037 | WCMS.Framework | `legacy/Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj` | v4.8 | Partial | 0 / 230 | [card](items/lgc-037-wcms-framework-legacy-portal-websystem-wcms-framework.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| LGC-038 | WCMS.Framework.Core.SqlProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WCMS.Framework.Core.SqlProvider.csproj` | v4.8 | Partial | 0 / 48 | [card](items/lgc-038-wcms-framework-core-sqlprovider-legacy-portal-websystem-wcms-framework-core-sqlp.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| LGC-039 | WCMS.Framework.Core.SqlProvider.Smo | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider.Smo/WCMS.Framework.Core.SqlProvider.Smo.csproj` | v4.8 | Partial | 0 / 1 | [card](items/lgc-039-wcms-framework-core-sqlprovider-smo-legacy-portal-websystem-wcms-framework-core-.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| LGC-040 | WCMS.Framework.Core.XmlProvider | `legacy/Portal/WebSystem/WCMS.Framework.Core.XmlProvider/WCMS.Framework.Core.XmlProvider.csproj` | v4.8 | Partial | 0 / 1 | [card](items/lgc-040-wcms-framework-core-xmlprovider-legacy-portal-websystem-wcms-framework-core-xmlp.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| LGC-041 | WCMS.WebSystem.Utilities | `legacy/Portal/WebSystem/WCMS.WebSystem.Utilities/WCMS.WebSystem.Utilities.csproj` | v4.8 | Partial | 0 / 3 | [card](items/lgc-041-wcms-websystem-utilities-legacy-portal-websystem-wcms-websystem-utilities.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| LGC-042 | WCMS.WebSystem | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/WCMS.WebSystem.csproj` | v4.8 | Partial | 0 / 33 | [card](items/lgc-042-wcms-websystem-legacy-portal-websystem-wcms-websystem-viewmodels.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. |

## Utility Items

| ID | Item | Project File | Target Framework | Migration Status | Surface / Components | Detail | Status Basis |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-043 | DbManager | `legacy/Portal/Utilities/DbManager/DbManager/DbManager.csproj` | v4.7 | Not Stated | 0 / 1 | [card](items/lgc-043-dbmanager-legacy-portal-utilities-dbmanager-dbmanager.md) | Legacy target framework only (v4.7). |
| LGC-044 | DbManager | `legacy/Portal/Utilities/DbManagerWPF/DbManager/DbManager.csproj` | v4.0 | Not Stated | 2 / 2 | [card](items/lgc-044-dbmanager-legacy-portal-utilities-dbmanagerwpf-dbmanager.md) | Legacy target framework only (v4.0). |
| LGC-045 | TableEditor | `legacy/Portal/Utilities/MySQL TableEditor/TableEditor.csproj` | v4.7 | Not Stated | 0 / 1 | [card](items/lgc-045-tableeditor-legacy-portal-utilities-mysql-tableeditor.md) | Legacy target framework only (v4.7). |
| LGC-046 | PostBuildManager | `legacy/Portal/Utilities/PostBuildManager/PostBuildManager/PostBuildManager.csproj` | v4.7 | Not Stated | 0 / 1 | [card](items/lgc-046-postbuildmanager-legacy-portal-utilities-postbuildmanager-postbuildmanager.md) | Legacy target framework only (v4.7). |
| LGC-047 | WebExtractor | `legacy/Portal/Utilities/WebExtractor/WebExtractor/WebExtractor.csproj` | v4.7 | Not Stated | 0 / 1 | [card](items/lgc-047-webextractor-legacy-portal-utilities-webextractor-webextractor.md) | Legacy target framework only (v4.7). |
| LGC-048 | WebSystemDeployer | `legacy/Portal/Utilities/WebSystemDeployer/WebSystemDeployer/WebSystemDeployer.csproj` | v4.7 | Not Stated | 0 / 2 | [card](items/lgc-048-websystemdeployer-legacy-portal-utilities-websystemdeployer-websystemdeployer.md) | Legacy target framework only (v4.7). |

## Database Items

| ID | Item | Project File | Target Framework | Migration Status | Surface / Components | Detail | Status Basis |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-049 | BibleReader.Database | `legacy/Portal/WebParts/Integration/BibleReader.Database/BibleReader.Database.sqlproj` | v4.8 | Not Stated | 0 / 0 | [card](items/lgc-049-biblereader-database-legacy-portal-webparts-integration-biblereader-database.md) | Legacy target framework only (v4.8). |
| LGC-050 | WCMS.WebSystem.Apps.Integration.Database | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.Database/WCMS.WebSystem.Apps.Integration.Database.sqlproj` | v4.8 | Not Stated | 0 / 0 | [card](items/lgc-050-wcms-websystem-apps-integration-database-legacy-portal-webparts-integration-wcms.md) | Legacy target framework only (v4.8). |
| LGC-051 | WCMS.Framework.SqlDabase | `legacy/Portal/WebSystem/WCMS.Framework.SqlDabase/WCMS.Framework.SqlDabase.sqlproj` | v4.7.2 | Not Stated | 0 / 0 | [card](items/lgc-051-wcms-framework-sqldabase-legacy-portal-websystem-wcms-framework-sqldabase.md) | Legacy target framework only (v4.7.2). |
