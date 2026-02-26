# Top 30 P0/P1 Migration Backlog

Date: 2026-02-26

Source: `legacy-to-modern-mapping.csv`

## Selection rules

- Include only `P0` and `P1` rows not marked `Retired-By-Design`.
- Score by priority, unresolved status, module risk concentration, and critical endpoint keywords.
- Use sequence buckets to front-load contract and CMS-core blockers.

## Backlog mix

- Total items: **30**
- Priority split: **P0=15**, **P1=15**

Sequence buckets:
- `S1-Contracts`: **15**
- `S2-CMS-Core`: **15**

Suggested owners:
- `WebSystem CMS Team`: **19**
- `SystemParts G2 Team`: **4**
- `Integration Team`: **4**
- `SystemParts Team`: **1**
- `LessonReviewer Team`: **1**
- `BibleReader Team`: **1**

## Execution tracking

- [x] Rank 1 (`MAP-131`) - `Portal/WebSystem/WebSystem-MVC/Content/Admin/Handlers/Handler.ashx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyAdminHandlerController.cs` (legacy route parity + fallback image support).
- [x] Rank 2 (`MAP-098`) - `Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/Handler.ashx`  
  Implemented by `Portal/WebParts/SystemPartsG2/SystemPartsG2/Api/LegacyDownloadHandlerController.cs` (filename DB lookup + force download parity).
- [x] Rank 3 (`MAP-102`) - `Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/FlashBanner/FlashService.asmx`  
  Implemented by `Portal/WebParts/SystemPartsG2/SystemPartsG2/Api/LegacyFlashServiceController.cs` (legacy XML payload + route aliases).
- [x] Rank 4 (`MAP-103`) - `Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/FlashBanner/Handler.ashx`  
  Implemented by `Portal/WebParts/SystemPartsG2/SystemPartsG2/Api/LegacyFlashBannerHandlerController.cs` (legacy XML payload + route aliases).
- [x] Rank 5 (`MAP-122`) - `Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/WebService.asmx`  
  Implemented by `Portal/WebParts/SystemPartsG2/SystemPartsG2/Api/LegacySocialWebServiceController.cs` (ASMX-style `{ d: ... }` responses + route aliases).
- [x] Rank 6 (`MAP-017`) - `Portal/WebParts/SystemParts/SystemParts/AppBundle/Article/EmailPreview.ashx`  
  Implemented by `Portal/WebParts/SystemParts/SystemParts/Api/LegacyArticleEmailPreviewController.cs` (legacy email template preview contract + route aliases).

## Top 30 (ranked)

| Rank | Pri | Seq | Owner | Legacy | Proposed Replacement |
|---:|---|---|---|---|---|
| 1 | P0 | S1-Contracts | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Admin/Handlers/Handler.ashx` | `(none)` |
| 2 | P0 | S1-Contracts | SystemParts G2 Team | `Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/Handler.ashx` | `(none)` |
| 3 | P0 | S1-Contracts | SystemParts G2 Team | `Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/FlashBanner/FlashService.asmx` | `(none)` |
| 4 | P0 | S1-Contracts | SystemParts G2 Team | `Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/FlashBanner/Handler.ashx` | `(none)` |
| 5 | P0 | S1-Contracts | SystemParts G2 Team | `Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/WebService.asmx` | `(none)` |
| 6 | P0 | S1-Contracts | SystemParts Team | `Portal/WebParts/SystemParts/SystemParts/AppBundle/Article/EmailPreview.ashx` | `(none)` |
| 7 | P0 | S1-Contracts | LessonReviewer Team | `LessonReviewer/LessonReviewer/Handlers/AjaxHandler.ashx` | `(none)` |
| 8 | P0 | S1-Contracts | Integration Team | `Portal/WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/ASOP-WS.asmx` | `(none)` |
| 9 | P0 | S1-Contracts | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Handlers/AjaxHandler.ashx` | `Portal/WebSystem/WebSystem-MVC/Api/FrameworkApiController.cs` |
| 10 | P0 | S1-Contracts | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/AccountService.asmx` | `Portal/WebSystem/WebSystem-MVC/Api/AccountApiController.cs` |
| 11 | P0 | S1-Contracts | Integration Team | `Portal/WebParts/Integration/IntegrationParts/Apps/Integration/Streaming/VerifySession.ashx` | `Portal/WebParts/Integration/IntegrationParts/Api/MemberApiController.cs` |
| 12 | P0 | S1-Contracts | Integration Team | `Portal/WebParts/Integration/IntegrationParts/Apps/Integration/Profile/MemberService.asmx` | `Portal/WebParts/Integration/IntegrationParts/Api/MemberApiController.cs` |
| 13 | P0 | S1-Contracts | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Parts/Common/FxService.asmx` | `Portal/WebSystem/WebSystem-MVC/Api/FrameworkApiController.cs` |
| 14 | P0 | S1-Contracts | Integration Team | `Portal/WebParts/Integration/IntegrationParts/Apps/Integration/BibleReader/BibleService.asmx` | `BibleReader/BibleReader/Api/BibleApiController.cs` |
| 15 | P0 | S1-Contracts | BibleReader Team | `BibleReader/BibleReader/BibleService.asmx` | `BibleReader/BibleReader/Api/BibleApiController.cs` |
| 16 | P1 | S2-CMS-Core | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Security/UserActivities.ascx` | `(none)` |
| 17 | P1 | S2-CMS-Core | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Admin/WebBinding.aspx` | `(none)` |
| 18 | P1 | S2-CMS-Core | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Admin/WebBindings.aspx` | `(none)` |
| 19 | P1 | S2-CMS-Core | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Admin/WebEvents.aspx` | `(none)` |
| 20 | P1 | S2-CMS-Core | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Admin/WebLogs.aspx` | `(none)` |
| 21 | P1 | S2-CMS-Core | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Admin/WebOpen.aspx` | `(none)` |
| 22 | P1 | S2-CMS-Core | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/UserProfileForm.ascx` | `(none)` |
| 23 | P1 | S2-CMS-Core | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/UserRolesForm.ascx` | `(none)` |
| 24 | P1 | S2-CMS-Core | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebResourceManager.ascx` | `(none)` |
| 25 | P1 | S2-CMS-Core | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/ImportExportHome.ascx` | `(none)` |
| 26 | P1 | S2-CMS-Core | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/ImportExportPage.ascx` | `(none)` |
| 27 | P1 | S2-CMS-Core | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/ImportExportParameterSets.ascx` | `(none)` |
| 28 | P1 | S2-CMS-Core | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/MessageQueueManager.ascx` | `(none)` |
| 29 | P1 | S2-CMS-Core | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/SmtpAnalyzer.ascx` | `(none)` |
| 30 | P1 | S2-CMS-Core | WebSystem CMS Team | `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/WebOpen.ascx` | `(none)` |

## Execution guidance

1. Complete all `S1-Contracts` items first and add integration tests per endpoint contract.
2. Complete `S2-CMS-Core` items next to unblock `/Central` and admin workflows.
3. Process `S3-SystemParts` and `S4-SystemPartsG2` in parallel streams once contract/core blockers are closed.
4. Keep this file and `top30-p0-p1-prioritized.csv` updated with status columns in each implementation PR.
