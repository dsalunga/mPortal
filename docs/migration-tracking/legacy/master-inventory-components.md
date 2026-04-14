# Legacy Migration Master Inventory

This folder tracks migration scope for all discovered legacy projects/components/utilities using repository evidence.

Companion views:

- [Canonical P-ID inventory](./master-inventory-canonical.md)
- [Canonical migration decision matrix](./master-inventory-canonical.md#canonical-migration-decision-matrix)

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
| Total tracked items | 50 |
| Status - Completed | 1 |
| Status - Partial | 19 |
| Status - Not Stated | 30 |
| Type - App | 12 |
| Type - Component | 19 |
| Type - Library | 10 |
| Type - Utility | 6 |
| Type - Database | 3 |

## App Items

| ID | Item | Target Framework | Migration Status | Surface / Components | Detail | Status Basis | Project File |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-001 | BibleReader.WebApp | v4.7 | Partial | 9 / 10 | [card](components/solutions/sln-001-biblereader/lgc-001-biblereader-webapp-legacy-biblereader-biblereader.md) | Legacy target (v4.7) with modern build artifacts in obj/bin. | `legacy/BibleReader/BibleReader/BibleReader.WebApp.csproj` |
| LGC-002 | BibleReader.Core | v4.8 | Not Stated | 0 / 12 | [card](components/shared/lgc-002-biblereader-core-legacy-biblereader-biblereader-core.md) | Legacy target framework only (v4.8). | `legacy/BibleReader/BibleReader.Core/BibleReader.Core.csproj` |
| LGC-003 | WCMS.LessonReviewer | v4.8 | Partial | 6 / 7 | [card](components/solutions/sln-003-lessonreviewer/lgc-003-wcms-lessonreviewer-legacy-lessonreviewer-lessonreviewer.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. | `legacy/LessonReviewer/LessonReviewer/LessonReviewer.csproj` |
| LGC-004 | WCMS.LessonReviewer.Core | v4.8 | Not Stated | 0 / 4 | [card](components/solutions/sln-003-lessonreviewer/lgc-004-wcms-lessonreviewer-core-legacy-lessonreviewer-lessonreviewer-core.md) | Legacy target framework only (v4.8). | `legacy/LessonReviewer/LessonReviewer.Core/LessonReviewer.Core.csproj` |
| LGC-005 | WCMS.WebSystem.WebParts.BranchLocator.WebApp | v4.8 | Partial | 12 / 5 | [card](components/solutions/sln-011-branchlocator/lgc-005-wcms-websystem-webparts-branchlocator-webapp-legacy-portal-webparts-branchlocato.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/WCMS.WebSystem.Apps.BranchLocator.WebApp.csproj` |
| LGC-006 | WCMS.WebSystem.Apps.Integration.WebApp | v4.8 | Partial | 159 / 103 | [card](components/solutions/sln-012-integration/lgc-006-wcms-websystem-apps-integration-webapp-legacy-portal-webparts-integration-integr.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. | `legacy/Portal/WebParts/Integration/IntegrationParts/WCMS.WebSystem.Apps.Integration.WebApp.csproj` |
| LGC-007 | WCMS.WebSystem.WebParts.SystemParts.WebApp | v4.8 | Partial | 145 / 124 | [card](components/solutions/sln-014-system-parts/lgc-007-wcms-websystem-webparts-systemparts-webapp-legacy-portal-webparts-systemparts-sy.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. | `legacy/Portal/WebParts/SystemParts/SystemParts/WCMS.WebSystem.Apps.SystemApps.WebApp.csproj` |
| LGC-008 | WCMS.WebSystem.WebParts.SystemPartsG2.WebApp | v4.8 | Partial | 59 / 47 | [card](components/solutions/sln-015-system-parts-g2/lgc-008-wcms-websystem-webparts-systempartsg2-webapp-legacy-portal-webparts-systempartsg.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. | `legacy/Portal/WebParts/SystemPartsG2/SystemPartsG2/WCMS.WebSystem.Apps.SystemApps2.WebApp.csproj` |
| LGC-009 | WCMS.WebSystem.WebParts.SystemPartsG3.WebApp | v4.8 | Partial | 10 / 9 | [card](components/solutions/sln-016-system-parts-g3/lgc-009-wcms-websystem-webparts-systempartsg3-webapp-legacy-portal-webparts-systempartsg.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. | `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/WCMS.WebSystem.Apps.SystemApps3.WebApp.csproj` |
| LGC-010 | WCMS.Framework.Agent | v4.8 | Not Stated | 0 / 1 | [card](components/solutions/sln-019-mportal/lgc-010-wcms-framework-agent-legacy-portal-websystem-wcms-framewok-agent.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebSystem/WCMS.Framewok.Agent/WCMS.Framework.Agent.csproj` |
| LGC-011 | WCMS.Framework.AgentService | v4.8 | Not Stated | 0 / 2 | [card](components/solutions/sln-019-mportal/lgc-011-wcms-framework-agentservice-legacy-portal-websystem-wcms-framework-agentservice.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebSystem/WCMS.Framework.AgentService/WCMS.Framework.AgentService.csproj` |
| LGC-012 | WCMS.WebSystem.WebApp | v4.8 | Partial | 258 / 216 | [card](components/solutions/sln-019-mportal/lgc-012-wcms-websystem-webapp-legacy-portal-websystem-websystem-mvc.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. | `legacy/Portal/WebSystem/WebSystem-MVC/WCMS.WebSystem.WebApp.csproj` |

## Component Items

| ID | Item | Target Framework | Migration Status | Surface / Components | Detail | Status Basis | Project File |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-013 | WCMS.WebSystem.Apps.BranchLocator | v4.8 | Not Stated | 0 / 7 | [card](components/solutions/sln-011-branchlocator/lgc-013-wcms-websystem-apps-branchlocator-legacy-portal-webparts-branchlocator-wcms-webs.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator/WCMS.WebSystem.Apps.BranchLocator.csproj` |
| LGC-014 | WCMS.WebSystem.Apps.BibleReader | v4.8 | Not Stated | 0 / 11 | [card](components/solutions/sln-012-integration/lgc-014-wcms-websystem-apps-biblereader-legacy-portal-webparts-integration-wcms-websyste.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/WCMS.WebSystem.Apps.BibleReader.csproj` |
| LGC-015 | WCMS.WebSystem.Apps.Integration | v4.8 | Not Stated | 0 / 84 | [card](components/solutions/sln-012-integration/lgc-015-wcms-websystem-apps-integration-legacy-portal-webparts-integration-wcms-websyste.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration/WCMS.WebSystem.Apps.Integration.csproj` |
| LGC-016 | WCMS.WebSystem.Apps.Integration.UnitTest | v4.8 | Partial | 0 / 2 | [card](components/solutions/sln-012-integration/lgc-016-wcms-websystem-apps-integration-unittest-legacy-portal-webparts-integration-wcms.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.UnitTest/WCMS.WebSystem.Apps.Integration.UnitTest.csproj` |
| LGC-017 | SDKTest | v4.8 | Partial | 1 / 1 | [card](components/solutions/sln-013-sdktest/lgc-017-sdktest-legacy-portal-webparts-sdktest-sdktest.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. | `legacy/Portal/WebParts/SDKTest/SDKTest/SDKTest.csproj` |
| LGC-018 | WCMS.WebSystem.WebParts.FileManager | v4.8 | Not Stated | 0 / 12 | [card](components/solutions/sln-014-system-parts/lgc-018-wcms-websystem-webparts-filemanager-legacy-portal-webparts-systemparts-wcms-fram.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.FileManager/WCMS.WebSystem.Apps.FileManager.csproj` |
| LGC-019 | WCMS.WebSystem.WebParts | v4.8 | Not Stated | 0 / 39 | [card](components/solutions/sln-014-system-parts/lgc-019-wcms-websystem-webparts-legacy-portal-webparts-systemparts-wcms-framework-webpar.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts/WCMS.WebSystem.Apps.csproj` |
| LGC-020 | WCMS.WebSystem.WebParts.EventCalendar | v4.8 | Not Stated | 0 / 22 | [card](components/solutions/sln-014-system-parts/lgc-020-wcms-websystem-webparts-eventcalendar-legacy-portal-webparts-systemparts-wcms-fr.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/WCMS.WebSystem.Apps.EventCalendar.csproj` |
| LGC-021 | WCMS.WebSystem.WebParts.Article | v4.8 | Not Stated | 0 / 21 | [card](components/solutions/sln-014-system-parts/lgc-021-wcms-websystem-webparts-article-legacy-portal-webparts-systemparts-wcms-websyste.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/WCMS.WebSystem.Apps.Article.csproj` |
| LGC-022 | WCMS.WebSystem.WebParts.Contact | v4.8 | Not Stated | 0 / 8 | [card](components/solutions/sln-014-system-parts/lgc-022-wcms-websystem-webparts-contact-legacy-portal-webparts-systemparts-wcms-websyste.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Contact/WCMS.WebSystem.Apps.Contact.csproj` |
| LGC-023 | WCMS.WebSystem.WebParts.GenericList | v4.8 | Not Stated | 0 / 11 | [card](components/solutions/sln-014-system-parts/lgc-023-wcms-websystem-webparts-genericlist-legacy-portal-webparts-systemparts-wcms-webs.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/WCMS.WebSystem.Apps.GenericList.csproj` |
| LGC-024 | WCMS.WebSystem.WebParts.RemoteIndexer | v4.8 | Not Stated | 0 / 15 | [card](components/solutions/sln-014-system-parts/lgc-024-wcms-websystem-webparts-remoteindexer-legacy-portal-webparts-systemparts-wcms-we.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/WCMS.WebSystem.Apps.RemoteIndexer.csproj` |
| LGC-025 | WCMS.WebSystem.WebParts.WeeklyScheduler | v4.8 | Not Stated | 0 / 4 | [card](components/solutions/sln-014-system-parts/lgc-025-wcms-websystem-webparts-weeklyscheduler-legacy-portal-webparts-systemparts-wcms-.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/WCMS.WebSystem.Apps.WeeklyScheduler.csproj` |
| LGC-026 | WCMS.WebSystem.SystemPartsG2 | v4.8 | Not Stated | 0 / 3 | [card](components/solutions/sln-015-system-parts-g2/lgc-026-wcms-websystem-systempartsg2-legacy-portal-webparts-systempartsg2-wcms-websystem.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/WCMS.WebSystem.Apps.csproj` |
| LGC-027 | WCMS.WebSystem.WebParts.Forum | v4.8 | Not Stated | 0 / 4 | [card](components/solutions/sln-015-system-parts-g2/lgc-027-wcms-websystem-webparts-forum-legacy-portal-webparts-systempartsg2-wcms-websyste.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Forum/WCMS.WebSystem.Apps.Forum.csproj` |
| LGC-028 | WCMS.Framework.Social | v4.8 | Not Stated | 0 / 13 | [card](components/solutions/sln-019-mportal/lgc-028-wcms-framework-social-legacy-portal-webparts-systempartsg2-wcms-websystem-webpar.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/WCMS.Framework.Social.csproj` |
| LGC-029 | WCMS.WebSystem.WebParts.Social.ViewModel | v4.8 | Not Stated | 0 / 2 | [card](components/solutions/sln-015-system-parts-g2/lgc-029-wcms-websystem-webparts-social-viewmodel-legacy-portal-webparts-systempartsg2-wc.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social.ViewModel/WCMS.WebSystem.Apps.Social.ViewModel.csproj` |
| LGC-030 | WCMS.WebSystem.WebParts.Incident | v4.8 | Not Stated | 0 / 18 | [card](components/solutions/sln-016-system-parts-g3/lgc-030-wcms-websystem-webparts-incident-legacy-portal-webparts-systempartsg3-wcms-websy.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/WCMS.WebSystem.Apps.Incident.csproj` |
| LGC-031 | WCMS.WebSystem.WebParts.Jobs | v4.8 | Not Stated | 0 / 4 | [card](components/solutions/sln-016-system-parts-g3/lgc-031-wcms-websystem-webparts-jobs-legacy-portal-webparts-systempartsg3-wcms-websystem.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Jobs/WCMS.WebSystem.Apps.Jobs.csproj` |

## Library Items

| ID | Item | Target Framework | Migration Status | Surface / Components | Detail | Status Basis | Project File |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-032 | WCMS.Common | net8.0 | Completed | 0 / 55 | [card](components/solutions/sln-002-wcms-common/lgc-032-wcms-common-legacy-core-wcms-common.md) | Modern target framework detected (net8.0). | `legacy/Core/WCMS.Common/WCMS.Common.csproj` |
| LGC-033 | Media-Player-ASP.NET-Control | v4.5.2 | Partial | 0 / 1 | [card](components/solutions/sln-004-media-player-asp-net-control/lgc-033-media-player-asp-net-control-legacy-libraries-media-player-asp-net-control-media.md) | Legacy target (v4.5.2) with modern build artifacts in obj/bin. | `legacy/Libraries/Media-Player-ASP.NET-Control/Media-Player-ASP.NET-Control/Media-Player-ASP.NET-Control.csproj` |
| LGC-034 | FredCK.FCKeditorV2 | v4.8 | Partial | 8 / 18 | [card](components/shared/lgc-034-fredck-fckeditorv2-legacy-portal-websystem-fckeditor-net-2-6-3.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.csproj` |
| LGC-036 | WCMS.Common | v4.8 | Partial | 0 / 55 | [card](components/solutions/sln-019-mportal/lgc-036-wcms-common-legacy-portal-websystem-wcms-common.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. | `legacy/Portal/WebSystem/WCMS.Common/WCMS.Common.csproj` |
| LGC-037 | WCMS.Framework | v4.8 | Partial | 0 / 230 | [card](components/solutions/sln-019-mportal/lgc-037-wcms-framework-legacy-portal-websystem-wcms-framework.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. | `legacy/Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj` |
| LGC-038 | WCMS.Framework.Core.SqlProvider | v4.8 | Partial | 0 / 48 | [card](components/solutions/sln-019-mportal/lgc-038-wcms-framework-core-sqlprovider-legacy-portal-websystem-wcms-framework-core-sqlp.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WCMS.Framework.Core.SqlProvider.csproj` |
| LGC-039 | WCMS.Framework.Core.SqlProvider.Smo | v4.8 | Partial | 0 / 1 | [card](components/solutions/sln-019-mportal/lgc-039-wcms-framework-core-sqlprovider-smo-legacy-portal-websystem-wcms-framework-core-.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. | `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider.Smo/WCMS.Framework.Core.SqlProvider.Smo.csproj` |
| LGC-040 | WCMS.Framework.Core.XmlProvider | v4.8 | Partial | 0 / 1 | [card](components/solutions/sln-019-mportal/lgc-040-wcms-framework-core-xmlprovider-legacy-portal-websystem-wcms-framework-core-xmlp.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. | `legacy/Portal/WebSystem/WCMS.Framework.Core.XmlProvider/WCMS.Framework.Core.XmlProvider.csproj` |
| LGC-041 | WCMS.WebSystem.Utilities | v4.8 | Partial | 0 / 3 | [card](components/solutions/sln-019-mportal/lgc-041-wcms-websystem-utilities-legacy-portal-websystem-wcms-websystem-utilities.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. | `legacy/Portal/WebSystem/WCMS.WebSystem.Utilities/WCMS.WebSystem.Utilities.csproj` |
| LGC-042 | WCMS.WebSystem | v4.8 | Partial | 0 / 33 | [card](components/solutions/sln-019-mportal/lgc-042-wcms-websystem-legacy-portal-websystem-wcms-websystem-viewmodels.md) | Legacy target (v4.8) with modern build artifacts in obj/bin. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/WCMS.WebSystem.csproj` |

## Utility Items

| ID | Item | Target Framework | Migration Status | Surface / Components | Detail | Status Basis | Project File |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-043 | DbManager | v4.7 | Not Stated | 0 / 1 | [card](components/solutions/sln-005-dbmanager/lgc-043-dbmanager-legacy-portal-utilities-dbmanager-dbmanager.md) | Legacy target framework only (v4.7). | `legacy/Portal/Utilities/DbManager/DbManager/DbManager.csproj` |
| LGC-044 | DbManager | v4.0 | Not Stated | 2 / 2 | [card](components/solutions/sln-006-dbmanager/lgc-044-dbmanager-legacy-portal-utilities-dbmanagerwpf-dbmanager.md) | Legacy target framework only (v4.0). | `legacy/Portal/Utilities/DbManagerWPF/DbManager/DbManager.csproj` |
| LGC-045 | TableEditor | v4.7 | Not Stated | 0 / 1 | [card](components/solutions/sln-007-tableeditor/lgc-045-tableeditor-legacy-portal-utilities-mysql-tableeditor.md) | Legacy target framework only (v4.7). | `legacy/Portal/Utilities/MySQL TableEditor/TableEditor.csproj` |
| LGC-046 | PostBuildManager | v4.7 | Not Stated | 0 / 1 | [card](components/solutions/sln-008-postbuildmanager/lgc-046-postbuildmanager-legacy-portal-utilities-postbuildmanager-postbuildmanager.md) | Legacy target framework only (v4.7). | `legacy/Portal/Utilities/PostBuildManager/PostBuildManager/PostBuildManager.csproj` |
| LGC-047 | WebExtractor | v4.7 | Not Stated | 0 / 1 | [card](components/solutions/sln-009-webextractor/lgc-047-webextractor-legacy-portal-utilities-webextractor-webextractor.md) | Legacy target framework only (v4.7). | `legacy/Portal/Utilities/WebExtractor/WebExtractor/WebExtractor.csproj` |
| LGC-048 | WebSystemDeployer | v4.7 | Not Stated | 0 / 2 | [card](components/solutions/sln-010-websystemdeployer/lgc-048-websystemdeployer-legacy-portal-utilities-websystemdeployer-websystemdeployer.md) | Legacy target framework only (v4.7). | `legacy/Portal/Utilities/WebSystemDeployer/WebSystemDeployer/WebSystemDeployer.csproj` |

## Database Items

| ID | Item | Target Framework | Migration Status | Surface / Components | Detail | Status Basis | Project File |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-049 | BibleReader.Database | v4.8 | Not Stated | 0 / 0 | [card](components/solutions/sln-012-integration/lgc-049-biblereader-database-legacy-portal-webparts-integration-biblereader-database.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/Integration/BibleReader.Database/BibleReader.Database.sqlproj` |
| LGC-050 | WCMS.WebSystem.Apps.Integration.Database | v4.8 | Not Stated | 0 / 0 | [card](components/solutions/sln-012-integration/lgc-050-wcms-websystem-apps-integration-database-legacy-portal-webparts-integration-wcms.md) | Legacy target framework only (v4.8). | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.Database/WCMS.WebSystem.Apps.Integration.Database.sqlproj` |
| LGC-051 | WCMS.Framework.SqlDabase | v4.7.2 | Not Stated | 0 / 0 | [card](components/solutions/sln-019-mportal/lgc-051-wcms-framework-sqldabase-legacy-portal-websystem-wcms-framework-sqldabase.md) | Legacy target framework only (v4.7.2). | `legacy/Portal/WebSystem/WCMS.Framework.SqlDabase/WCMS.Framework.SqlDabase.sqlproj` |
