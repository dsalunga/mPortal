# SLN-012 - Integration

## Solution Metadata

| Field | Value |
| --- | --- |
| Solution ID | SLN-012 |
| Solution Name | `Integration` |
| Solution File | `legacy/Portal/WebParts/Integration/Integration.sln` |
| Projects In Solution | 7 |
| Mapped Projects | 7 |
| Unmapped Projects | 0 |
| Aggregate Migration Status | Mixed (Partial, Not Stated) |
| Status Breakdown | Not Stated:5, Partial:2 |
| Mapped LGC Items | LGC-002, LGC-006, LGC-014, LGC-015, LGC-016, LGC-049, LGC-050 |

## Projects In Solution

| Solution Item ID | Migration Status | Project Type | Project Name | LGC ID | LGC Item | Component Card | Project File |
| --- | --- | --- | --- | --- | --- | --- | --- |
| SLN-012-P01 | Partial | Project | `WCMS.WebSystem.Apps.Integration.WebApp` | LGC-006 | `WCMS.WebSystem.Apps.Integration.WebApp` | [Card](../components/lgc-006-wcms-websystem-apps-integration-webapp-legacy-portal-webparts-integration-integr.md) | `legacy/Portal/WebParts/Integration/IntegrationParts/WCMS.WebSystem.Apps.Integration.WebApp.csproj` |
| SLN-012-P02 | Not Stated | Project | `WCMS.WebSystem.Apps.Integration` | LGC-015 | `WCMS.WebSystem.Apps.Integration` | [Card](../components/lgc-015-wcms-websystem-apps-integration-legacy-portal-webparts-integration-wcms-websyste.md) | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration/WCMS.WebSystem.Apps.Integration.csproj` |
| SLN-012-P03 | Not Stated | Project | `WCMS.WebSystem.Apps.BibleReader` | LGC-014 | `WCMS.WebSystem.Apps.BibleReader` | [Card](../components/lgc-014-wcms-websystem-apps-biblereader-legacy-portal-webparts-integration-wcms-websyste.md) | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.BibleReader/WCMS.WebSystem.Apps.BibleReader.csproj` |
| SLN-012-P04 | Not Stated | Project | `BibleReader.Core` | LGC-002 | `BibleReader.Core` | [Card](../components/lgc-002-biblereader-core-legacy-biblereader-biblereader-core.md) | `legacy/BibleReader/BibleReader.Core/BibleReader.Core.csproj` |
| SLN-012-P05 | Not Stated | Database Project | `BibleReader.Database` | LGC-049 | `BibleReader.Database` | [Card](../components/lgc-049-biblereader-database-legacy-portal-webparts-integration-biblereader-database.md) | `legacy/Portal/WebParts/Integration/BibleReader.Database/BibleReader.Database.sqlproj` |
| SLN-012-P06 | Partial | Project | `WCMS.WebSystem.Apps.Integration.UnitTest` | LGC-016 | `WCMS.WebSystem.Apps.Integration.UnitTest` | [Card](../components/lgc-016-wcms-websystem-apps-integration-unittest-legacy-portal-webparts-integration-wcms.md) | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.UnitTest/WCMS.WebSystem.Apps.Integration.UnitTest.csproj` |
| SLN-012-P07 | Not Stated | Database Project | `WCMS.WebSystem.Apps.Integration.Database` | LGC-050 | `WCMS.WebSystem.Apps.Integration.Database` | [Card](../components/lgc-050-wcms-websystem-apps-integration-database-legacy-portal-webparts-integration-wcms.md) | `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.Database/WCMS.WebSystem.Apps.Integration.Database.sqlproj` |

## Migration Actions

| Action | Priority | Status | Notes |
| --- | --- | --- | --- |
| Validate solution-level migration sequencing against component statuses | High | Not Stated | Use aggregate and row statuses as planning baseline. |
| Update per-project row statuses as migration progresses | Medium | Not Stated | Keep solution card synchronized with implementation state. |
