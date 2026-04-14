# Legacy Migration Solution Inventory

This document tracks migration status per Visual Studio solution (`*.sln`) discovered under `legacy/`.

Companion views:

- [LGC project/component inventory](./master-inventory-components.md)
- [Canonical P-ID inventory](./master-inventory-canonical.md)
- [Per-solution cards](./solutions/README.md)

## Inventory Summary

| Metric | Value |
| --- | --- |
| Total solutions | 19 |
| Total projects referenced by solutions | 55 |
| Total mapped projects | 55 |
| Total unmapped projects | 0 |
| Aggregate Status - Completed | 1 |
| Aggregate Status - Mixed (Partial, Not Stated) | 8 |
| Aggregate Status - Not Stated | 6 |
| Aggregate Status - Partial | 4 |

## Solution Master Table

| Solution ID | Solution Name | Aggregate Migration Status | Projects | Mapped | Unmapped | Mapped LGC IDs | Detail | Solution File |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| SLN-001 | `BibleReader` | Mixed (Partial, Not Stated) | 2 | 2 | 0 | LGC-001, LGC-002 | [card](solutions/sln-001-biblereader-biblereader-sln.md) | `legacy/BibleReader/BibleReader.sln` |
| SLN-002 | `WCMS.Common` | Completed | 1 | 1 | 0 | LGC-032 | [card](solutions/sln-002-core-wcms-common-sln.md) | `legacy/Core/WCMS.Common.sln` |
| SLN-003 | `LessonReviewer` | Mixed (Partial, Not Stated) | 2 | 2 | 0 | LGC-003, LGC-004 | [card](solutions/sln-003-lessonreviewer-lessonreviewer-sln.md) | `legacy/LessonReviewer/LessonReviewer.sln` |
| SLN-004 | `Media-Player-ASP.NET-Control` | Partial | 1 | 1 | 0 | LGC-033 | [card](solutions/sln-004-libraries-media-player-asp-net-control-media-player-asp-net-control-sln.md) | `legacy/Libraries/Media-Player-ASP.NET-Control/Media-Player-ASP.NET-Control.sln` |
| SLN-005 | `DbManager` | Not Stated | 1 | 1 | 0 | LGC-043 | [card](solutions/sln-005-portal-utilities-dbmanager-dbmanager-sln.md) | `legacy/Portal/Utilities/DbManager/DbManager.sln` |
| SLN-006 | `DbManager` | Not Stated | 1 | 1 | 0 | LGC-044 | [card](solutions/sln-006-portal-utilities-dbmanagerwpf-dbmanager-sln.md) | `legacy/Portal/Utilities/DbManagerWPF/DbManager.sln` |
| SLN-007 | `TableEditor` | Not Stated | 1 | 1 | 0 | LGC-045 | [card](solutions/sln-007-portal-utilities-mysql-tableeditor-tableeditor-sln.md) | `legacy/Portal/Utilities/MySQL TableEditor/TableEditor.sln` |
| SLN-008 | `PostBuildManager` | Not Stated | 1 | 1 | 0 | LGC-046 | [card](solutions/sln-008-portal-utilities-postbuildmanager-postbuildmanager-sln.md) | `legacy/Portal/Utilities/PostBuildManager/PostBuildManager.sln` |
| SLN-009 | `WebExtractor` | Not Stated | 1 | 1 | 0 | LGC-047 | [card](solutions/sln-009-portal-utilities-webextractor-webextractor-sln.md) | `legacy/Portal/Utilities/WebExtractor/WebExtractor.sln` |
| SLN-010 | `WebSystemDeployer` | Not Stated | 1 | 1 | 0 | LGC-048 | [card](solutions/sln-010-portal-utilities-websystemdeployer-websystemdeployer-sln.md) | `legacy/Portal/Utilities/WebSystemDeployer/WebSystemDeployer.sln` |
| SLN-011 | `BranchLocator` | Mixed (Partial, Not Stated) | 2 | 2 | 0 | LGC-005, LGC-013 | [card](solutions/sln-011-portal-webparts-branchlocator-branchlocator-sln.md) | `legacy/Portal/WebParts/BranchLocator/BranchLocator.sln` |
| SLN-012 | `Integration` | Mixed (Partial, Not Stated) | 7 | 7 | 0 | LGC-002, LGC-006, LGC-014, LGC-015, LGC-016, LGC-049, LGC-050 | [card](solutions/sln-012-portal-webparts-integration-integration-sln.md) | `legacy/Portal/WebParts/Integration/Integration.sln` |
| SLN-013 | `SDKTest` | Partial | 1 | 1 | 0 | LGC-017 | [card](solutions/sln-013-portal-webparts-sdktest-sdktest-sln.md) | `legacy/Portal/WebParts/SDKTest/SDKTest.sln` |
| SLN-014 | `System-Parts` | Mixed (Partial, Not Stated) | 9 | 9 | 0 | LGC-007, LGC-018, LGC-019, LGC-020, LGC-021, LGC-022, LGC-023, LGC-024, LGC-025 | [card](solutions/sln-014-portal-webparts-systemparts-system-parts-sln.md) | `legacy/Portal/WebParts/SystemParts/System-Parts.sln` |
| SLN-015 | `System-Parts-G2` | Mixed (Partial, Not Stated) | 4 | 4 | 0 | LGC-008, LGC-026, LGC-027, LGC-029 | [card](solutions/sln-015-portal-webparts-systempartsg2-system-parts-g2-sln.md) | `legacy/Portal/WebParts/SystemPartsG2/System-Parts-G2.sln` |
| SLN-016 | `System-Parts-G3` | Mixed (Partial, Not Stated) | 3 | 3 | 0 | LGC-009, LGC-030, LGC-031 | [card](solutions/sln-016-portal-webparts-systempartsg3-system-parts-g3-sln.md) | `legacy/Portal/WebParts/SystemPartsG3/System-Parts-G3.sln` |
| SLN-017 | `FredCK.FCKeditorV2` | Partial | 1 | 1 | 0 | LGC-034 | [card](solutions/sln-017-portal-websystem-fckeditor-net-2-6-3-fredck-fckeditorv2-sln.md) | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.sln` |
| SLN-018 | `mPortal-Web` | Partial | 3 | 3 | 0 | LGC-012, LGC-041, LGC-042 | [card](solutions/sln-018-portal-websystem-mportal-web-sln.md) | `legacy/Portal/WebSystem/mPortal-Web.sln` |
| SLN-019 | `mPortal` | Mixed (Partial, Not Stated) | 13 | 13 | 0 | LGC-010, LGC-011, LGC-012, LGC-028, LGC-034, LGC-036, LGC-037, LGC-038, LGC-039, LGC-040, LGC-041, LGC-042, LGC-051 | [card](solutions/sln-019-portal-websystem-mportal-sln.md) | `legacy/Portal/WebSystem/mPortal.sln` |
