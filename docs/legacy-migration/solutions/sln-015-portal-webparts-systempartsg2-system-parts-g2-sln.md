# SLN-015 - System-Parts-G2

## Solution Metadata

| Field | Value |
| --- | --- |
| Solution ID | SLN-015 |
| Solution Name | `System-Parts-G2` |
| Solution File | `legacy/Portal/WebParts/SystemPartsG2/System-Parts-G2.sln` |
| Modern Solution File / Evidence | `mPortal.slnx` |
| Projects In Solution | 4 |
| Mapped Projects | 4 |
| Unmapped Projects | 0 |
| Aggregate Migration Status | Completed |
| Status Breakdown | Completed:4 |
| Mapped LGC Items | LGC-008, LGC-026, LGC-027, LGC-029 |

## Projects In Solution

| Solution Item ID | Migration Status | Project Type | Project Name | LGC ID | LGC Item | Component Card | Project File | Modern Project File / Evidence |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| SLN-015-P01 | Completed | Project | `WCMS.WebSystem.Apps.SystemApps2.WebApp` | LGC-008 | `WCMS.WebSystem.WebParts.SystemPartsG2.WebApp` | [Card](./sln-015-system-parts-g2/lgc-008-wcms-websystem-webparts-systempartsg2-webapp-legacy-portal-webparts-systempartsg.md) | `legacy/Portal/WebParts/SystemPartsG2/SystemPartsG2/WCMS.WebSystem.Apps.SystemApps2.WebApp.csproj` | `Portal/WebParts/SystemPartsG2/SystemPartsG2/WCMS.WebSystem.Apps.SystemApps2.WebApp.csproj` |
| SLN-015-P02 | Completed | Project | `WCMS.WebSystem.Apps.Social.ViewModel` | LGC-029 | `WCMS.WebSystem.WebParts.Social.ViewModel` | [Card](./sln-015-system-parts-g2/lgc-029-wcms-websystem-webparts-social-viewmodel-legacy-portal-webparts-systempartsg2-wc.md) | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social.ViewModel/WCMS.WebSystem.Apps.Social.ViewModel.csproj` | `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social.ViewModel/WCMS.WebSystem.Apps.Social.ViewModel.csproj` |
| SLN-015-P03 | Completed | Project | `WCMS.WebSystem.Apps.Forum` | LGC-027 | `WCMS.WebSystem.WebParts.Forum` | [Card](./sln-015-system-parts-g2/lgc-027-wcms-websystem-webparts-forum-legacy-portal-webparts-systempartsg2-wcms-websyste.md) | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Forum/WCMS.WebSystem.Apps.Forum.csproj` | `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Forum/WCMS.WebSystem.Apps.Forum.csproj` |
| SLN-015-P04 | Completed | Project | `WCMS.WebSystem.Apps` | LGC-026 | `WCMS.WebSystem.SystemPartsG2` | [Card](./sln-015-system-parts-g2/lgc-026-wcms-websystem-systempartsg2-legacy-portal-webparts-systempartsg2-wcms-websystem.md) | `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/WCMS.WebSystem.Apps.csproj` | `Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/WCMS.WebSystem.Apps.csproj` |

## Migration Actions

| Action | Priority | Status | Notes |
| --- | --- | --- | --- |
| Validate solution-level migration sequencing against component statuses | High | Completed | Use aggregate and row statuses as planning baseline. |
| Update per-project row statuses as migration progresses | Medium | Completed | Keep solution card synchronized with implementation state. |
