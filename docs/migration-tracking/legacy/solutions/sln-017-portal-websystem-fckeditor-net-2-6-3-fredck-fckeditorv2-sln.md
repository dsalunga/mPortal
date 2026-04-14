# SLN-017 - FredCK.FCKeditorV2

## Solution Metadata

| Field | Value |
| --- | --- |
| Solution ID | SLN-017 |
| Solution Name | `FredCK.FCKeditorV2` |
| Solution File | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.sln` |
| Projects In Solution | 1 |
| Mapped Projects | 1 |
| Unmapped Projects | 0 |
| Aggregate Migration Status | Do Not Migrate As-Is |
| Status Breakdown | Do Not Migrate As-Is:1 |
| Mapped LGC Items | LGC-034 |

## Projects In Solution

| Solution Item ID | Migration Status | Project Type | Project Name | LGC ID | LGC Item | Component Card | Project File |
| --- | --- | --- | --- | --- | --- | --- | --- |
| SLN-017-P01 | Do Not Migrate As-Is | Project | `FredCK.FCKeditorV2` | LGC-034 | `FredCK.FCKeditorV2` | [Card](../components/shared/lgc-034-fredck-fckeditorv2-legacy-portal-websystem-fckeditor-net-2-6-3.md) | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.csproj` |

## Migration Actions

| Action | Priority | Status | Notes |
| --- | --- | --- | --- |
| Enforce editor replacement-only rule | High | Active | `FredCK.FCKeditorV2` is Do Not Migrate As-Is; replace all editor usage with TipTap OSS + sanitization. |
| Keep card synchronized with implementation state | Medium | Active | Preserve Do Not Migrate As-Is status and prevent lift-and-shift tasks for this solution. |
