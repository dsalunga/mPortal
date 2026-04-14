# SLN-018 - mPortal-Web

## Solution Metadata

| Field | Value |
| --- | --- |
| Solution ID | SLN-018 |
| Solution Name | `mPortal-Web` |
| Solution File | `legacy/Portal/WebSystem/mPortal-Web.sln` |
| Projects In Solution | 3 |
| Mapped Projects | 3 |
| Unmapped Projects | 0 |
| Aggregate Migration Status | Partial |
| Status Breakdown | Partial:3 |
| Mapped LGC Items | LGC-012, LGC-041, LGC-042 |

## Projects In Solution

| Solution Item ID | Migration Status | Project Type | Project Name | LGC ID | LGC Item | Component Card | Project File |
| --- | --- | --- | --- | --- | --- | --- | --- |
| SLN-018-P01 | Partial | Project | `WCMS.WebSystem` | LGC-042 | `WCMS.WebSystem` | [Card](../components/lgc-042-wcms-websystem-legacy-portal-websystem-wcms-websystem-viewmodels.md) | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/WCMS.WebSystem.csproj` |
| SLN-018-P02 | Partial | Project | `WCMS.WebSystem.Utilities` | LGC-041 | `WCMS.WebSystem.Utilities` | [Card](../components/lgc-041-wcms-websystem-utilities-legacy-portal-websystem-wcms-websystem-utilities.md) | `legacy/Portal/WebSystem/WCMS.WebSystem.Utilities/WCMS.WebSystem.Utilities.csproj` |
| SLN-018-P03 | Partial | Project | `WCMS.WebSystem.WebApp` | LGC-012 | `WCMS.WebSystem.WebApp` | [Card](../components/lgc-012-wcms-websystem-webapp-legacy-portal-websystem-websystem-mvc.md) | `legacy/Portal/WebSystem/WebSystem-MVC/WCMS.WebSystem.WebApp.csproj` |

## Migration Actions

| Action | Priority | Status | Notes |
| --- | --- | --- | --- |
| Validate solution-level migration sequencing against component statuses | High | Not Stated | Use aggregate and row statuses as planning baseline. |
| Update per-project row statuses as migration progresses | Medium | Not Stated | Keep solution card synchronized with implementation state. |
