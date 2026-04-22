# Legacy Migration Solution Inventory

This document tracks migration status per Visual Studio solution (`*.sln`) discovered under `legacy/`.
Aggregate solution status is derived from mapped `LGC-###` project statuses in `master-inventory-projects.md` (refreshed April 15, 2026).

> **Program Status (2026-04-17): Completed (current migration scope closed).**  
> Solution-level mapping remains: 16 `Completed`, 1 `Mixed`, 1 `Do Not Migrate As-Is` (FCKeditor replacement-only).  
> Tracker totals are now 5,306 files: 3,878 `completed`, 1,428 `not_applicable`, 0 `incomplete`, 0 `not_started`.  
> See [EXECUTION_BOARD.md](../legacy-migration-v1/EXECUTION_BOARD.md) for closure history and future execution batches when new scope is added.

Companion views:

- [LGC project/component inventory](./master-inventory-projects.md)
- [Canonical P-ID inventory](./master-inventory-summary.md)
- [Per-solution cards](./solutions/README.md)

## Inventory Summary

| Metric | Value |
| --- | --- |
| Total solutions | 18 |
| Total projects referenced by solutions | 52 |
| Total mapped projects | 52 |
| Total unmapped projects | 0 |
| Aggregate Status - Completed | 16 |
| Aggregate Status - Mixed | 1 |
| Aggregate Status - Not Stated | 0 |
| Aggregate Status - Partial | 0 |
| Aggregate Status - Do Not Migrate As-Is | 1 |

## Solution Master Table

| Solution ID | Solution Name | Aggregate Migration Status | Projects | Mapped | Unmapped | Mapped LGC IDs | Detail | Solution File | Modern Solution File / Evidence |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| SLN-001 | `BibleReader` | Completed | 2 | 2 | 0 | LGC-001, LGC-002 | [card](solutions/sln-001-biblereader-biblereader-sln.md) | `legacy/BibleReader/BibleReader.sln` | `mPortal.slnx` |
| SLN-002 | `WCMS.Common` | Completed | 1 | 1 | 0 | LGC-032 | [card](solutions/sln-002-core-wcms-common-sln.md) | `legacy/Core/WCMS.Common.sln` | `mPortal.slnx` |
| SLN-003 | `LessonReviewer` | Completed | 2 | 2 | 0 | LGC-003, LGC-004 | [card](solutions/sln-003-lessonreviewer-lessonreviewer-sln.md) | `legacy/LessonReviewer/LessonReviewer.sln` | `mPortal.slnx` |
| SLN-004 | `Media-Player-ASP.NET-Control` | Completed | 1 | 1 | 0 | LGC-033 | [card](solutions/sln-004-libraries-media-player-asp-net-control-media-player-asp-net-control-sln.md) | `legacy/Libraries/Media-Player-ASP.NET-Control/Media-Player-ASP.NET-Control.sln` | `mPortal.slnx` |
| SLN-005 | `DbManager` | Completed | 1 | 1 | 0 | LGC-043 | [card](solutions/sln-005-portal-utilities-dbmanager-dbmanager-sln.md) | `legacy/Portal/Utilities/DbManager/DbManager.sln` | `mPortal.slnx` |
| SLN-006 | `DbManager` | Completed | 1 | 1 | 0 | LGC-044 | [card](solutions/sln-006-portal-utilities-dbmanagerwpf-dbmanager-sln.md) | `legacy/Portal/Utilities/DbManagerWPF/DbManager.sln` | `mPortal.slnx` |
| SLN-007 | `TableEditor` | Completed | 1 | 1 | 0 | LGC-045 | [card](solutions/sln-007-portal-utilities-mysql-tableeditor-tableeditor-sln.md) | `legacy/Portal/Utilities/MySQL TableEditor/TableEditor.sln` | `mPortal.slnx` |
| SLN-008 | `PostBuildManager` | Completed | 1 | 1 | 0 | LGC-046 | [card](solutions/sln-008-portal-utilities-postbuildmanager-postbuildmanager-sln.md) | `legacy/Portal/Utilities/PostBuildManager/PostBuildManager.sln` | `mPortal.slnx` |
| SLN-009 | `WebExtractor` | Completed | 1 | 1 | 0 | LGC-047 | [card](solutions/sln-009-portal-utilities-webextractor-webextractor-sln.md) | `legacy/Portal/Utilities/WebExtractor/WebExtractor.sln` | `mPortal.slnx` |
| SLN-010 | `WebSystemDeployer` | Completed | 1 | 1 | 0 | LGC-048 | [card](solutions/sln-010-portal-utilities-websystemdeployer-websystemdeployer-sln.md) | `legacy/Portal/Utilities/WebSystemDeployer/WebSystemDeployer.sln` | `mPortal.slnx` |
| SLN-011 | `BranchLocator` | Completed | 2 | 2 | 0 | LGC-005, LGC-013 | [card](solutions/sln-011-portal-webparts-branchlocator-branchlocator-sln.md) | `legacy/Portal/WebParts/BranchLocator/BranchLocator.sln` | `mPortal.slnx` |
| SLN-012 | `Integration` | Completed | 7 | 7 | 0 | LGC-002, LGC-006, LGC-014, LGC-015, LGC-016, LGC-049, LGC-050 | [card](solutions/sln-012-portal-webparts-integration-integration-sln.md) | `legacy/Portal/WebParts/Integration/Integration.sln` | `mPortal.slnx` |
| SLN-013 | `SDKTest` | Completed | 1 | 1 | 0 | LGC-017 | [card](solutions/sln-013-portal-webparts-sdktest-sdktest-sln.md) | `legacy/Portal/WebParts/SDKTest/SDKTest.sln` | `mPortal.slnx` |
| SLN-014 | `System-Parts` | Completed | 9 | 9 | 0 | LGC-007, LGC-018, LGC-019, LGC-020, LGC-021, LGC-022, LGC-023, LGC-024, LGC-025 | [card](solutions/sln-014-portal-webparts-systemparts-system-parts-sln.md) | `legacy/Portal/WebParts/SystemParts/System-Parts.sln` | `mPortal.slnx` |
| SLN-015 | `System-Parts-G2` | Completed | 4 | 4 | 0 | LGC-008, LGC-026, LGC-027, LGC-029 | [card](solutions/sln-015-portal-webparts-systempartsg2-system-parts-g2-sln.md) | `legacy/Portal/WebParts/SystemPartsG2/System-Parts-G2.sln` | `mPortal.slnx` |
| SLN-016 | `System-Parts-G3` | Completed | 3 | 3 | 0 | LGC-009, LGC-030, LGC-031 | [card](solutions/sln-016-portal-webparts-systempartsg3-system-parts-g3-sln.md) | `legacy/Portal/WebParts/SystemPartsG3/System-Parts-G3.sln` | `mPortal.slnx` |
| SLN-017 | `FredCK.FCKeditorV2` | Do Not Migrate As-Is | 1 | 1 | 0 | LGC-034 | [card](solutions/sln-017-portal-websystem-fckeditor-net-2-6-3-fredck-fckeditorv2-sln.md) | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.sln` | `mPortal.slnx` |
| SLN-019 | `mPortal` | Mixed (Completed, Do Not Migrate As-Is) | 13 | 13 | 0 | LGC-010, LGC-011, LGC-012, LGC-028, LGC-034, LGC-036, LGC-037, LGC-038, LGC-039, LGC-040, LGC-041, LGC-042, LGC-051 | [card](solutions/sln-019-portal-websystem-mportal-sln.md) | `legacy/Portal/WebSystem/mPortal.sln` | `mPortal.slnx` |
