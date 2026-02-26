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
- [x] Rank 7 (`MAP-004`) - `LessonReviewer/LessonReviewer/Handlers/AjaxHandler.ashx`  
  Implemented by `LessonReviewer/LessonReviewer/Api/LegacyAjaxHandlerController.cs` (Status + KeepAlive contract parity).
- [x] Rank 8 (`MAP-009`) - `Portal/WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/ASOP-WS.asmx`  
  Implemented by `Portal/WebParts/Integration/IntegrationParts/Api/LegacyAsopWebServiceController.cs` (ASMX-style `{ d: ... }` methods for HelloWorld, GetCandidate, IsEmailNotTaken, Vote).
- [x] Rank 9 (`MAP-149`) - `Portal/WebSystem/WebSystem-MVC/Content/Handlers/AjaxHandler.ashx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyWebSystemAjaxHandlerController.cs` (SetDesignPanel, GetText, Status/KeepAlive, SessionValid contract parity).
- [x] Rank 10 (`MAP-150`) - `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/AccountService.asmx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyAccountServiceController.cs` (legacy `GetUserSession`/`GetUserSessionByAuthKey` routes with `{ d: ... }` response shape).
- [x] Rank 11 (`MAP-011`) - `Portal/WebParts/Integration/IntegrationParts/Apps/Integration/Streaming/VerifySession.ashx`  
  Implemented by `Portal/WebParts/Integration/IntegrationParts/Api/LegacyStreamingVerifySessionController.cs` (legacy stream/session validation contract with `OK`/`FAIL` response).
- [x] Rank 12 (`MAP-010`) - `Portal/WebParts/Integration/IntegrationParts/Apps/Integration/Profile/MemberService.asmx`  
  Implemented by `Portal/WebParts/Integration/IntegrationParts/Api/LegacyMemberServiceController.cs` (legacy MemberService method routes and `{ d: ... }` payload parity).
- [x] Rank 13 (`MAP-174`) - `Portal/WebSystem/WebSystem-MVC/Content/Parts/Common/FxService.asmx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyFxServiceController.cs` (legacy FxService method routes + rendered comment HTML contract + `{ d: ... }` payloads).
- [x] Rank 14 (`MAP-008`) - `Portal/WebParts/Integration/IntegrationParts/Apps/Integration/BibleReader/BibleService.asmx`  
  Implemented by `Portal/WebParts/Integration/IntegrationParts/Api/LegacyIntegrationBibleServiceController.cs` (legacy BibleReader app-access/version-access method routes and payload contracts).
- [x] Rank 15 (`MAP-003`) - `BibleReader/BibleReader/BibleService.asmx`  
  Implemented by `BibleReader/BibleReader/Api/LegacyBibleServiceController.cs` (legacy HelloWorld/GetVerseContent/GetVerse routes with `{ d: ... }` payload parity).
- [x] Rank 16 (`MAP-159`) - `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Security/UserActivities.ascx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyCentralControlRoutesController.cs` (legacy control path redirected to `/Central/Security/WebUsers/` with query-string preservation).
- [x] Rank 17 (`MAP-136`) - `Portal/WebSystem/WebSystem-MVC/Content/Admin/WebBinding.aspx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyAdminPagesController.cs` (legacy entry point redirected to `/Central/Site/WebSiteHeaders/` with query-string preservation).
- [x] Rank 18 (`MAP-137`) - `Portal/WebSystem/WebSystem-MVC/Content/Admin/WebBindings.aspx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyAdminPagesController.cs` (legacy entry point redirected to `/Central/Site/WebSiteHeaders/` with query-string preservation).
- [x] Rank 19 (`MAP-138`) - `Portal/WebSystem/WebSystem-MVC/Content/Admin/WebEvents.aspx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyAdminPagesController.cs` (legacy entry point redirected to `/Central/Dashboard/` with query-string preservation).
- [x] Rank 20 (`MAP-139`) - `Portal/WebSystem/WebSystem-MVC/Content/Admin/WebLogs.aspx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyAdminPagesController.cs` (legacy entry point redirected to `/Central/Tools/SessionDiagnostics/` with query-string preservation).
- [x] Rank 21 (`MAP-140`) - `Portal/WebSystem/WebSystem-MVC/Content/Admin/WebOpen.aspx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyAdminPagesController.cs` (legacy entry point redirected to `/Central/WebOpen/` with query-string preservation).
- [x] Rank 22 (`MAP-155`) - `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/UserProfileForm.ascx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyCentralControlRoutesController.cs` (legacy control path redirected to `/Central/Security/UserProfile/` with query-string preservation).
- [x] Rank 23 (`MAP-156`) - `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/UserRolesForm.ascx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyCentralControlRoutesController.cs` (legacy control path redirected to `/Central/Security/WebUserRoles/` with query-string preservation).
- [x] Rank 24 (`MAP-158`) - `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebResourceManager.ascx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyCentralControlRoutesController.cs` (legacy control path redirected to `/Central/Tools/WebResourceManager/` with query-string preservation).
- [x] Rank 25 (`MAP-160`) - `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/ImportExportHome.ascx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyCentralControlRoutesController.cs` (legacy control path redirected to `/Central/Tools/WebDataStoreManager/` with query-string preservation).
- [x] Rank 26 (`MAP-161`) - `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/ImportExportPage.ascx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyCentralControlRoutesController.cs` (legacy control path redirected to `/Central/Tools/WebDataStoreManager/` with query-string preservation).
- [x] Rank 27 (`MAP-162`) - `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/ImportExportParameterSets.ascx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyCentralControlRoutesController.cs` (legacy control path redirected to `/Central/Misc/WebParameterSets/` with query-string preservation).
- [x] Rank 28 (`MAP-163`) - `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/MessageQueueManager.ascx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyCentralControlRoutesController.cs` (legacy control path redirected to `/Central/Tools/MessageQueueManager/` with query-string preservation).
- [x] Rank 29 (`MAP-165`) - `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/SmtpAnalyzer.ascx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyCentralControlRoutesController.cs` (legacy control path redirected to `/Central/Tools/SmtpAnalyzer/` with query-string preservation).
- [x] Rank 30 (`MAP-170`) - `Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/WebOpen.ascx`  
  Implemented by `Portal/WebSystem/WebSystem-MVC/Api/LegacyCentralControlRoutesController.cs` (legacy control path redirected to `/Central/WebOpen/` with query-string preservation).

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
