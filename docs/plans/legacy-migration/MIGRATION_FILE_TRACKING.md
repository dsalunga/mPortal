# Legacy → Modern File Migration Tracking

**Auto-generated from:** `docs/plans/legacy-migration/inventory/legacy-source-tracking-portal.csv`  
**Total .ascx files:** 519  
**Completed (has modern equivalent):** 242  
**Not Applicable (no modern equivalent needed):** 277

## Summary by Module

| Module | Matched | Not Applicable | Total |
|---|---:|---:|---:|
| Portal/Binaries/Externals | 0 | 1 | 1 |
| Portal/WebParts/BranchLocator | 4 | 0 | 4 |
| Portal/WebParts/Integration | 104 | 23 | 127 |
| Portal/WebParts/SDKTest | 0 | 1 | 1 |
| Portal/WebParts/SystemParts | 45 | 91 | 136 |
| Portal/WebParts/SystemPartsG2 | 15 | 37 | 52 |
| Portal/WebParts/SystemPartsG3 | 2 | 8 | 10 |
| Portal/WebSystem/WebSystem-MVC | 72 | 116 | 188 |
| **TOTAL** | **242** | **277** | **519** |

## Portal/Binaries/Externals
**0 matched** | **1 not applicable** | **1 total**

### Not Applicable (No Migration Needed)

| Legacy File | Status Basis | Notes |
|---|---|---|
| Binaries/Externals/config.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |

## Portal/WebParts/BranchLocator
**4 matched** | **0 not applicable** | **4 total**

### Completed (Modern Equivalent Exists)

| Legacy File | Modern File(s) | Basis |
|---|---|---|
| WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapter.ascx | WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapter.cshtml | controller_view_match |
| WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterHome.ascx | WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterHome.cshtml | controller_view_match |
| WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterTree.ascx | WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterTree.cshtml | controller_view_match |
| WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapters.ascx | WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapters.cshtml | controller_view_match |

## Portal/WebParts/Integration
**104 matched** | **23 not applicable** | **127 total**

### Completed (Modern Equivalent Exists)

| Legacy File | Modern File(s) | Basis |
|---|---|---|
| WebParts/Integration/IntegrationParts/Apps/Integration/Account/AccountInfo.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntAccountInfoViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Account/GetAccess.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntGetAccessViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Account/Login.ascx | WebSystem/WebSystem/ViewComponents/LoginViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Account/ProfileUpdate.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntProfileUpdateViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Account/RegisterSG.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntRegisterSGViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Account/RegisterV2.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntRegisterV2ViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/BibleReader/BibleAuth.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntBibleAuthViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/BibleReader/BibleBrowser.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntBibleBrowserViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/EventRegister/Attendee.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntAttendeeViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/EventRegister/AttendeeForm.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntAttendeeFormViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/EventRegister/Attendees.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntAttendeesViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/GlobalSwitch/GlobalSwitch.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntGlobalSwitchViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/LessonReviewer/AttendanceRequest.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntAttendanceRequestViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/LessonReviewer/AttendanceRequests.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntAttendanceRequestsViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/LessonReviewer/MemberSessions.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntMemberSessionsViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/CompositeGroupManager.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntCompositeGroupManagerViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/Controls/WebGroupTab.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntWebGroupTabViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/GroupEdit.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntGroupEditViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/GroupManager.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntGroupManagerViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/GroupOverview.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntGroupOverviewViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/GroupUserManager.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntGroupUserManagerViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/MemberAccess.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntMemberAccessViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/MemberEdit.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntMemberEditViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/MemberManager.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntMemberManagerViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/ASOPMobile.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCASOPMobileViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/ASOPWinners.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCASOPWinnersViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/AdminCompetitionEdit.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCAdminCompetitionEditViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/AdminCompetitionManager.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCAdminCompetitionManagerViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/AdminComposerEdit.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCAdminComposerEditViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/AdminComposerManager.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCAdminComposerManagerViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/AdminInterpreterScoreManager.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCAdminInterpreterScoreManagerViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/AdminSongScoreManager.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCAdminSongScoreManagerViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/CandidateEdit.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCCandidateEditViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/CandidateManager.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCCandidateManagerViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/InstagramGallery.ascx | WebParts/SystemParts/SystemParts/ViewComponents/InstagramGalleryViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCConfirmVote.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCConfirmVoteViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCConfirmVoteV2.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCConfirmVoteV2ViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCFinalists.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCFinalistsViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCFinalistsV2.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCFinalistsV2ViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCJudgeV2.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCJudgeV2ViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCJudges.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCJudgesViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCJudgesMaster.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCJudgesMasterViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCVoteResult.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCVoteResultViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCVoteResult2.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCVoteResult2ViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCVoteResultV3.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCVoteResultV3ViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCVoteResultV4.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCVoteResultV4ViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCVoteV2.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCVoteV2ViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCVoteV3.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCVoteV3ViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MessageBoard.ascx | WebSystem/WebSystem/ViewComponents/MessageBoardViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MessageBoardV2.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCMessageBoardV2ViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/VoteManager.ascx | WebParts/Integration/IntegrationParts/ViewComponents/MCVoteManagerViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/AMSAuth.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntAMSAuthViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/AMSTest.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntAMSTestViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/AddressBookBrowser.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntAddressBookBrowserViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/AddressBookMini.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntAddressBookMiniViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/ChangePassword.ascx | WebSystem/WebSystem/ViewComponents/Admin/ChangePasswordViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/ForgotPassword.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntForgotPasswordViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/LessonReviewer/ConfigAttendanceRequest.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntConfigAttendanceRequestViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/LessonReviewer/ConfigAttendanceRequests.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntConfigAttendanceRequestsViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/LessonReviewer/LogAttendance.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntLogAttendanceViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/LessonReviewer/NewsDashboard.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntNewsDashboardViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/LessonReviewer/Playback.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntPlaybackViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/LessonReviewer/RecoverSession.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntRecoverSessionViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/LessonReviewer/ServiceVideoDurationInput.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntServiceVideoDurationInputViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/LessonReviewer/SessionStart.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntSessionStartViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/MyAttendance.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntMyAttendanceViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/MyPreferences.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntMyPreferencesViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/ProfileUpdate.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntProfileUpdateViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/RequestManager.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntRequestManagerViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/RequestManagerRegister.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntRequestManagerRegisterViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/Sportsfest.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntSportsfestViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/SportsfestManager.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntSportsfestManagerViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/UpdateMyGroups.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntUpdateMyGroupsViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/UserProfileDetails.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntUserProfileDetailsViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/WebGroupUsers.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntWebGroupUsersViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/ZimbraPreauth.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntZimbraPreauthViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ActivateAccount.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntActivateAccountViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/AdminMemberLinkSync.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntAdminMemberLinkSyncViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/AdminUtilities.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntAdminUtilitiesViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ConfigExternalMember.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntConfigExternalMemberViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ConfigExternalMembers.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntConfigExternalMembersViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ConfigMemberLink.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntConfigMemberLinkViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ConfigMemberLinkEdit.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntConfigMemberLinkEditViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ConfigMemberLinks.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntConfigMemberLinksViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ConfigMembers.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntConfigMembersViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ConventionRegistration.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntConventionRegistrationViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ForgotPass.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntForgotPassViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/Login.ascx | WebSystem/WebSystem/ViewComponents/LoginViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/MemberVisitEntry.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntMemberVisitEntryViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/MemberVisitView.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntMemberVisitViewViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/RegistrationManager.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntRegistrationManagerViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/RegistrationV2.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntRegistrationV2ViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/WebGroupList.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntWebGroupListViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Reminder/MemberReminders.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntMemberRemindersViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Reminder/ReminderTemplates.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntReminderTemplatesViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/ServicePicker.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntServicePickerViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Streaming/AccessRequest.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntAccessRequestViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Apps/Integration/Streaming/StreamingConsole.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntStreamingConsoleViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Themes/area51/Parts/EventCalendar/EventCalendar/CalendarView.ascx | WebParts/SystemParts/SystemParts/ViewComponents/CalendarViewViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Themes/area51/Parts/Profile/Profile/ForgotPassword.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntForgotPasswordViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Themes/area51/Parts/Profile/Profile/MyPreferences.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntMyPreferencesViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Themes/area51/Parts/Profile/Profile/ProfileUpdate.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntProfileUpdateViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Themes/area51/Parts/Profile/Profile/UpdateMyGroups.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntUpdateMyGroupsViewComponent.cs | viewcomponent_replacement |
| WebParts/Integration/IntegrationParts/Themes/area51/Parts/Profile/Profile/UserProfileDetails.ascx | WebParts/Integration/IntegrationParts/ViewComponents/IntUserProfileDetailsViewComponent.cs | viewcomponent_replacement |

### Not Applicable (No Migration Needed)

| Legacy File | Status Basis | Notes |
|---|---|---|
| WebParts/Integration/IntegrationParts/Apps/Integration/Account/ChangePasswordV2.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/_CompositeGroupManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/_GroupUserManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Apps/Integration/Streaming/Setup.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Apps/Integration/Streaming/UserManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/ASOP/Default.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/ASOP/Inside.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/ASOP/asopv2-division-content.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/ASOP/asopv2-division-home.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/ASOP/v2/old/asopv2-content.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/ASOP/v2/old/asopv2-home.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/area51/Default.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/area51/InsideFull.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/area51/InsideWithSidebar.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/area51/Parts/EventCalendar/Event/EventViewBasic.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/area51/index.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/mcgi_website/Default.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/mcgi_website/template_home.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/mcgi_website/template_home2.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/mcgi_website/template_inside.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/mcgi_website/template_inside_full.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/mcgisg/mcgisg_home.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/mcgisg/mcgisg_inside.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |

## Portal/WebParts/SDKTest
**0 matched** | **1 not applicable** | **1 total**

### Not Applicable (No Migration Needed)

| Legacy File | Status Basis | Notes |
|---|---|---|
| WebParts/SDKTest/SDKTest/WebParts/Test/WebUserControl1.ascx | obsolete_test_sample | SDK test/sample control; not production code |

## Portal/WebParts/SystemParts
**45 matched** | **91 not applicable** | **136 total**

### Completed (Modern Equivalent Exists)

| Legacy File | Modern File(s) | Basis |
|---|---|---|
| WebParts/SystemParts/SystemParts/AppBundle/Article/Article.ascx | WebParts/SystemParts/SystemParts/ViewComponents/ArticleViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Article/Templates/Multiple/Basic.ascx | WebParts/SystemParts/SystemParts/ViewComponents/ArticleMultipleMultiBasicViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/ArticleMultipleMultiBasic/Default.cshtml | viewcomponent_match |
| WebParts/SystemParts/SystemParts/AppBundle/Article/Templates/Multiple/FrontNews.ascx | WebParts/SystemParts/SystemParts/ViewComponents/ArticleMultipleMultiFrontnewsViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/ArticleMultipleMultiFrontnews/Default.cshtml | viewcomponent_match |
| WebParts/SystemParts/SystemParts/AppBundle/Article/Templates/Multiple/Newsroom.ascx | WebParts/SystemParts/SystemParts/ViewComponents/ArticleMultipleMultiNewsroomViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/ArticleMultipleMultiNewsroom/Default.cshtml | viewcomponent_match |
| WebParts/SystemParts/SystemParts/AppBundle/Article/Templates/Single/Basic.ascx | WebParts/SystemParts/SystemParts/ViewComponents/ArticleSingleSingleBasicViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/ArticleSingleSingleBasic/Default.cshtml | viewcomponent_match |
| WebParts/SystemParts/SystemParts/AppBundle/Article/Templates/Single/FrontNews.ascx | WebParts/SystemParts/SystemParts/ViewComponents/ArticleSingleSingleFrontnewsViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/ArticleSingleSingleFrontnews/Default.cshtml | viewcomponent_match |
| WebParts/SystemParts/SystemParts/AppBundle/Article/Templates/Single/Newsroom.ascx | WebParts/SystemParts/SystemParts/ViewComponents/ArticleSingleSingleNewsroomViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/ArticleSingleSingleNewsroom/Default.cshtml | viewcomponent_match |
| WebParts/SystemParts/SystemParts/AppBundle/Contact/ContactUs.ascx | WebParts/SystemParts/SystemParts/ViewComponents/ContactViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/Contact/Default.cshtml | viewcomponent_match |
| WebParts/SystemParts/SystemParts/AppBundle/Contact/ContactUsV2.ascx | WebParts/SystemParts/SystemParts/ViewComponents/Contactusv2ViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/Contactusv2/Default.cshtml | viewcomponent_match |
| WebParts/SystemParts/SystemParts/AppBundle/Container/ContainerDesigner.ascx | WebParts/SystemParts/SystemParts/ViewComponents/ContainerDesignerViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Container/ThreeColumn.ascx | WebParts/SystemParts/SystemParts/ViewComponents/ThreeColumnViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Container/TwoColumn.ascx | WebParts/SystemParts/SystemParts/ViewComponents/TwoColumnViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Content/Content.ascx | WebParts/SystemParts/SystemParts/ViewComponents/ContentViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/CalendarEdit.ascx | WebParts/SystemParts/SystemParts/ViewComponents/CalendarEditViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/CalendarEventView.ascx | WebParts/SystemParts/SystemParts/ViewComponents/CalendarEventViewViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/CalendarManager.ascx | WebParts/SystemParts/SystemParts/ViewComponents/CalendarManagerViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/CalendarView.ascx | WebParts/SystemParts/SystemParts/ViewComponents/CalendarViewViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/Controls/Breadcrumb.ascx | WebSystem/WebSystem/ViewComponents/BreadcrumbViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/FileEditor.ascx | WebParts/SystemParts/SystemParts/ViewComponents/FileEditorViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/FileManager.ascx | WebParts/SystemParts/SystemParts/ViewComponents/FileManagerViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/FolderView.ascx | WebParts/SystemParts/SystemParts/ViewComponents/FolderViewViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/UploadFiles.ascx | WebParts/SystemParts/SystemParts/ViewComponents/UploadFilesViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/GenericList.ascx | WebParts/SystemParts/SystemParts/ViewComponents/GenericListViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/Survey.ascx | WebParts/SystemParts/SystemParts/ViewComponents/SurveyViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/AdminMenu.ascx | WebParts/SystemParts/SystemParts/ViewComponents/AdminMenuViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/Breadcrumb.ascx | WebSystem/WebSystem/ViewComponents/BreadcrumbViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/CMS_ListingPage.ascx | WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/ListingPageViewComponent.cs | mapped_replacement_exists |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/CascadeMenu.ascx | WebParts/SystemParts/SystemParts/ViewComponents/CascadeMenuViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/GenericMenu.ascx | WebParts/SystemParts/SystemParts/ViewComponents/GenericMenuViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/LinearMenu.ascx | WebParts/SystemParts/SystemParts/ViewComponents/LinearMenuViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Messaging/SendEmail.ascx | WebParts/SystemParts/SystemParts/ViewComponents/SendEmailViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Navigation/Breadcrumb.ascx | WebSystem/WebSystem/ViewComponents/BreadcrumbViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Office/OfficeBrowser.ascx | WebParts/SystemParts/SystemParts/ViewComponents/OfficeBrowserViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Others/DateDisplay/CMS_DateDisplay.ascx | WebParts/SystemParts/SystemParts/ViewComponents/DateDisplayViewComponent.cs | mapped_replacement_exists |
| WebParts/SystemParts/SystemParts/AppBundle/Others/DateDisplay/DateDisplay.ascx | WebParts/SystemParts/SystemParts/ViewComponents/DateDisplayViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Others/Title/SiteSection.ascx | WebParts/SystemParts/SystemParts/ViewComponents/SiteSectionViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/AdminPhotos.ascx | WebParts/SystemParts/SystemParts/ViewComponents/AdminPhotosViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/AnimatedBanner.ascx | WebParts/SystemParts/SystemParts/ViewComponents/AnimatedBannerViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/CMS_Gallery.ascx | WebParts/SystemParts/SystemParts/ViewComponents/GalleryViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/Controls/Album.ascx | WebParts/SystemParts/SystemParts/ViewComponents/PhotoalbumViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/Photoalbum/Default.cshtml | viewcomponent_match |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/InstagramGallery.ascx | WebParts/SystemParts/SystemParts/ViewComponents/InstagramGalleryViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/PictureGallery.ascx | WebParts/SystemParts/SystemParts/ViewComponents/PictureGalleryViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Search/Search.ascx | WebParts/SystemParts/SystemParts/ViewComponents/SearchViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/Twitter/TwitterHelper.ascx | WebParts/SystemParts/SystemParts/ViewComponents/TwitterHelperViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemParts/SystemParts/AppBundle/WeeklyScheduler/WeeklyScheduler.ascx | WebParts/SystemParts/SystemParts/ViewComponents/WeeklySchedulerViewComponent.cs | viewcomponent_replacement |

### Not Applicable (No Migration Needed)

| Legacy File | Status Basis | Notes |
|---|---|---|
| WebParts/SystemParts/SystemParts/AppBundle/Article/AdminPublication.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Article/AdminSubscriptionManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Article/AdminTemplateComposer.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Article/AdminTemplateManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Article/ArticleDashboard.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Article/ArticleTagView.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Article/ConfigDashboard.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Article/ConfigPublication.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Article/ConfigSendEmail.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Article/Controls/AdminTabControl.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Article/Controls/ArticleListPreview.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Contact/AdminContactDetails.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Contact/AdminContactList.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Contact/AdminInquiriesDetails.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Contact/AdminInquiriesList.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Contact/ConfigInquiriesList.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Container/ThreeRow.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Container/TwoRow.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Content/Blog.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Content/ConfigContent.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Content/ConfigOpen.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Content/FeedBack.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Content/WM_Custom.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminCalendar.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminCalendarHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminCalendarManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminCategoryEdit.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminCategoryManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminEvent.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminEventView.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminLocation.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminLocationManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminTemplateEdit.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminTemplateManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/CalendarMiniView.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/EventViewBasic.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/AdminRemoteLibraryEdit.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/AdminRemoteLibraryManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/ConfigFileManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/Controls/IndexerBreadcrumb.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/CreateFolder.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/FileView.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/IndexerDownload.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/RemoteIndexRecentUpdates.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/RemoteIndexView.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/RemoteIndexerDownload.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/RemoteLibraryView.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/RenameFile.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/TextEditor.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/ListData.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/SM_Surveys.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/SurveyFinish.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/SurveyStart.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/SurveyWelcome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_CreateQuestionItems_07.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_CreateQuestion_06.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_CreateSurveyPage_04.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_CreateSurvey_02.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_Responses_09.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_Results_08.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_SurveyPages_03.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_SurveyQuestions_05.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_Surveys_01.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/AdminMenuEdit.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/CCMS_Menu_08.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/CCMS_Menu_09.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/CMS_StdMenu_01.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigDynamicMenu01.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigDynamicMenuV2.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigLinkedMenu.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigLinkedMenuEdit.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigMenuItemEdit.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigMenuItems.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/Controls/AdminTabControl.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/DropHighlightMenu.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/ImportExport.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/StdCascadeMenu.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Office/OfficeDetails.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Others/Script/BodyEvent.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/AdminAlbum.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/AdminAlbumEdit.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/CCMS_Category.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/CCMS_Gallery_02.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/CMS_Category.ascx | obsolete_webforms | Legacy album-category admin has no deterministic 1:1 replacement in current SystemParts view components.; WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/ConfigAnimatedBanner.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/Controls/FancyBoxThumbnails.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/Controls/FullView.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/Controls/SlideShow.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/Controls/Thumbnails.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Search/SearchResults.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Search/Search_ROG.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |

## Portal/WebParts/SystemPartsG2
**15 matched** | **37 not applicable** | **52 total**

### Completed (Modern Equivalent Exists)

| Legacy File | Modern File(s) | Basis |
|---|---|---|
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/Ad.ascx | WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/AdViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/CMS_Ad.ascx | WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/AdManagerViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ajax/AjaxScriptManager.ascx | WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/AjaxScriptManagerViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/BasicList/BasicList.ascx | WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/BasicListViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/Controls/BasicList.ascx | WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/BasicListViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/Downloads.ascx | WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/DownloadsViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Media/cms_media.ascx | WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/MediaAdminViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Misc/DateDisplay/CMS_DateDisplay.ascx | WebParts/SystemParts/SystemParts/ViewComponents/DateDisplayViewComponent.cs | mapped_replacement_exists |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Misc/DateDisplay/DateDisplay.ascx | WebParts/SystemParts/SystemParts/ViewComponents/DateDisplayViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Misc/Title/SiteSection.ascx | WebParts/SystemParts/SystemParts/ViewComponents/SiteSectionViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Newsletter/Newsletter.ascx | WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/NewsletterViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/CMS_ListingPage.ascx | WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/ListingPageViewComponent.cs | mapped_replacement_exists |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/ListingPage.ascx | WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/ListingPageViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/Comments.ascx | WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/CommentsViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/Wall.ascx | WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/WallViewComponent.cs | viewcomponent_replacement |

### Not Applicable (No Migration Needed)

| Legacy File | Status Basis | Notes |
|---|---|---|
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_ad_02.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_adcategories_01.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_adcategory_02.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_aditem_04.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_aditems_03.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_adwriter_01.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/BasicList/CMS_01_BasicList.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/BasicList/ItemTemplates/Single.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/BasicList/ItemTemplates/TwoColumns.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/BasicList/ItemTemplates/TwoRows.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/Controls/BasicList_A3.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/Controls/DetailedList.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/Controls/GroupByYear.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/PM_Downloads_Edit_02.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/PM_Downloads_Master_01.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/SM_Downloads_03.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/FlashBanner/Renderer.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Media/content_media_01.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Media/content_media_02.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Media/fullcontent.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Media/mediahome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Media/medialist.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Misc/Script/BodyEvent.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Newsletter/AdminNewsletter.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Newsletter/CCMS_MailSender.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Newsletter/CCMS_eNewsletter.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Newsletter/CMS_Enewsletter.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Newsletter/eNewsLetter.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/listsimplehoriz1.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/listsimplevert1.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/splashnavigator.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/subsiteslist.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/subsiteslist_nb.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/ConfigWall.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/MobileWall.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/Plugins/GenericWallPost.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/Plugins/ProfileUpdateWall.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |

## Portal/WebParts/SystemPartsG3
**2 matched** | **8 not applicable** | **10 total**

### Completed (Modern Equivalent Exists)

| Legacy File | Modern File(s) | Basis |
|---|---|---|
| WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs/JobSearch.ascx | WebParts/SystemPartsG3/SystemPartsG3/ViewComponents/JobSearchViewComponent.cs | viewcomponent_replacement |
| WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs/JobsList.ascx | WebParts/SystemPartsG3/SystemPartsG3/ViewComponents/JobsListViewComponent.cs | viewcomponent_replacement |

### Not Applicable (No Migration Needed)

| Legacy File | Status Basis | Notes |
|---|---|---|
| WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/CategoryEditView.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/CategoryManagerView.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/InstanceEditView.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/InstanceManagerView.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TicketManagerView.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TicketView.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TypeEditView.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Incident/TypeManagerView.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |

## Portal/WebSystem/WebSystem-MVC
**72 matched** | **116 not applicable** | **188 total**

### Completed (Modern Equivalent Exists)

| Legacy File | Modern File(s) | Basis |
|---|---|---|
| WebSystem/WebSystem-MVC/Content/Controls/Breadcrumb.ascx | WebSystem/WebSystem/ViewComponents/BreadcrumbViewComponent.cs | viewcomponent_replacement |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Agent/Dashboard.ascx | WebSystem/WebSystem/ViewComponents/Admin/AgentDashboardViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/AgentDashboard/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Agent/TaskEditor.ascx | WebSystem/WebSystem/ViewComponents/Admin/TaskEditorViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/TaskEditor/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Breadcrumb.ascx | WebSystem/WebSystem/ViewComponents/BreadcrumbViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/Breadcrumb/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/ChangePasswordForm.ascx | WebSystem/WebSystem/ViewComponents/Admin/ChangePasswordViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ChangePassword/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/DesignMode.ascx | WebSystem/WebSystem/ViewComponents/Admin/DesignModeViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/DesignMode/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/ElementDesigner.ascx | WebSystem/WebSystem/ViewComponents/Admin/ElementDesignerViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ElementDesigner/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/PanelDesigner.ascx | WebSystem/WebSystem/ViewComponents/Admin/PanelDesignerViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/PanelDesigner/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebPagesControl.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebPagesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPages/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebSitesControl.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebSitesControlViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebSitesControl/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Dashboard.ascx | WebSystem/WebSystem/ViewComponents/Admin/AdminDashboardViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/AdminDashboard/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/HeaderNavbar.ascx | WebSystem/WebSystem/ViewComponents/Admin/HeaderNavbarViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/HeaderNavbar/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/HeaderPanel.ascx | WebSystem/WebSystem/ViewComponents/Admin/HeaderPanelViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/HeaderPanel/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/SubscriptionManager.ascx | WebSystem/WebSystem/ViewComponents/Admin/SubscriptionManagerViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/SubscriptionManager/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebAddresses.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebAddressesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebAddresses/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebOffices.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebOfficesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebOffices/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebParameters.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebParametersViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebParameters/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebResources.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebResourcesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebResources/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/ChangePassword.ascx | WebSystem/WebSystem/ViewComponents/Admin/ChangePasswordViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ChangePassword/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/CreateUser.ascx | WebSystem/WebSystem/ViewComponents/Admin/CreateUserViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/CreateUser/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/UserProfile.ascx | WebSystem/WebSystem/ViewComponents/Admin/UserProfileViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/UserProfile/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebGroups.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebGroupsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebGroups/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebPermissions.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebPermissionsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPermissions/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebRoles.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebRolesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebRoles/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebSecurity.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebSecurityTreeViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebSecurityTree/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebSecurityTree.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebSecurityTreeViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebSecurityTree/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebUsers.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebUsersViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebUsers/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/SideBar.ascx | WebSystem/WebSystem/ViewComponents/SideBarViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/SideBar/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/SiteMap.ascx | WebSystem/WebSystem/ViewComponents/Admin/SiteMapViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/SiteMap/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebSkins.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebSkinsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebSkins/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebTemplateEditor.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebTemplateEditorViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebTemplateEditor/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebTemplatePanels.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebTemplatePanelsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebTemplatePanels/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebTemplates.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebTemplatesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebTemplates/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebThemes.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebThemesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebThemes/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/DataSyncDashboard.ascx | WebSystem/WebSystem/ViewComponents/Admin/DataSyncDashboardViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/DataSyncDashboard/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/EventLogManager.ascx | WebSystem/WebSystem/ViewComponents/Admin/EventLogManagerViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/EventLogManager/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/PerformanceLogManager.ascx | WebSystem/WebSystem/ViewComponents/Admin/PerformanceLogManagerViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/PerformanceLogManager/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/QueryAnalyzer.ascx | WebSystem/WebSystem/ViewComponents/Admin/QueryAnalyzerViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/QueryAnalyzer/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/ShortUrlManager.ascx | WebSystem/WebSystem/ViewComponents/Admin/ShortUrlManagerViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ShortUrlManager/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebRegistry.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebRegistryViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebRegistry/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartConfig.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebPartConfigViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPartConfig/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartControlTemplates.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebPartControlTemplatesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPartControlTemplates/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartControls.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebPartControlsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPartControls/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartTree.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebPartTreeViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPartTree/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebParts.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebPartsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebParts/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPartPreview.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebPartPreviewViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPartPreview/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebChildPages.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebPagesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPages/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebChildSites.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebChildSitesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebChildSites/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebIdentities.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebIdentitiesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebIdentities/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebMasterPages.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebMasterPagesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebMasterPages/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPage.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebPageViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPage/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPageElements.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebPageElementsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPageElements/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPagePanels.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebPagePanelsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPagePanels/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPages.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebPagesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPages/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebSite.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebSiteViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebSite/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebSites.ascx | WebSystem/WebSystem/ViewComponents/Admin/WebSitesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebSites/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Common/Comments.ascx | WebSystem/WebSystem/ViewComponents/CommentsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/Comments/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Common/Login.ascx | WebSystem/WebSystem/ViewComponents/LoginViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/Login/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Common/MessageBoard.ascx | WebSystem/WebSystem/ViewComponents/MessageBoardViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/MessageBoard/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Common/TriggerTask.ascx | WebSystem/WebSystem/ViewComponents/TriggerTaskViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/TriggerTask/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Parts/Common/UserPhotoUpload.ascx | WebSystem/WebSystem/ViewComponents/UserPhotoUploadViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/UserPhotoUpload/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Themes/Basic/Basic.ascx | WebSystem/WebSystem/ViewComponents/ThemeBasicViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeBasic/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Themes/Central/Basic.ascx | WebSystem/WebSystem/ViewComponents/ThemeCentralBasicViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeCentralBasic/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Themes/Central/Responsive.ascx | WebSystem/WebSystem/ViewComponents/ThemeCentralResponsiveViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeCentralResponsive/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Themes/Central/ResponsiveWithSidebar.ascx | WebSystem/WebSystem/ViewComponents/ThemeCentralResponsiveWithSidebarViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeCentralResponsiveWithSidebar/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Themes/Default/Default.ascx | WebSystem/WebSystem/ViewComponents/ThemeDefaultViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeDefault/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Themes/Default/ForAjaxControlToolkit.ascx | WebSystem/WebSystem/ViewComponents/ThemeDefaultForAjaxControlToolkitViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeDefaultForAjaxControlToolkit/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Themes/bootstrap3/navbar-fixed-top.ascx | WebSystem/WebSystem/ViewComponents/ThemeBootstrap3NavbarViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeBootstrap3Navbar/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Themes/test/Parts/Article/Default/Article.ascx | WebSystem/WebSystem/ViewComponents/ThemeTestArticleViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeTestArticle/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Themes/test/Parts/Contact/Contact/ContactUs.ascx | WebSystem/WebSystem/ViewComponents/ThemeTestContactViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeTestContact/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Themes/test/StandAloneTemplate.ascx | WebSystem/WebSystem/ViewComponents/ThemeTestStandaloneViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeTestStandalone/Default.cshtml | viewcomponent_match |
| WebSystem/WebSystem-MVC/Content/Themes/test/test.ascx | WebSystem/WebSystem/ViewComponents/ThemeTestViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeTest/Default.cshtml | viewcomponent_match |

### Not Applicable (No Migration Needed)

| Legacy File | Status Basis | Notes |
|---|---|---|
| WebSystem/WebSystem-MVC/Content/Controls/CKEditor.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Controls/ComboDatePicker.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Controls/ContextActionBar.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Controls/FullNamePicker.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Controls/MonthPicker.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Controls/PhoneNumber.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Controls/TabControl.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Controls/TabControlV1.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Controls/TextEditor.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Controls/WMPControl.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Agent/TaskManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Agent/TaskView.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/ManagementSecurityOption.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/ParameterSetSelector.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/SaveInFolder.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/TreeControls.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/UserProfileForm.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/UserRolesForm.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebGenericTab.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebGroupTab.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebMasterPageTab.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebPagePanelTab.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebPageTab.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebPartControlTab.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebPartControlTemplateTab.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebPartTab.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebRoleTab.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebSiteElementSelector.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebSiteTab.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebTemplateHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebThemeHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebUserTab.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Manager/Home.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebAddress.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebOffice.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebOfficeHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebOfficeTree.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebParameter.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebParameterSet.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebParameterSetHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebParameterSets.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebParametersXml.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebResource.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebResourceManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/UserActivities.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebGlobalPolicy.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebGlobalPolicyHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebGroup.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebGroupHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebGroupTree.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebGroupUsers.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebObjectPermissions.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebRoleHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebUserGroups.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebUserHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebUserPermissions.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebUserRoles.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebSkin.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebSkinHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebTemplate.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebTemplateHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebTemplatePanel.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebTemplateVersions.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebTheme.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebThemeHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/DataStoreManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/DataSyncManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/ImportExportHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/ImportExportPage.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/ImportExportParameterSets.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/MessageQueueManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/ShortUrlEdit.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/SmtpAnalyzer.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/UserSessionManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebDataExplorer.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebDataRows.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebFolder.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebFolderTree.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebObjectEdit.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebRegistryEntry.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebRegistryTree.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebToolsTree.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/TreePanel.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebOpen.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPart.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartAdmin.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartAdminEntry.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartAdminHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartAdminMgmt.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartConfigEntry.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartControl.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartControlHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartControlTemplate.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartControlTemplateHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartTemplatePanel.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartTemplatePanels.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebIdentity.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebLinkedParts.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebMasterPage.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebMasterPageHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPageElement.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPageElementHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPageHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPagePanel.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPagePanelHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebSiteHome.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebSiteTree.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Common/AdminCommentManager.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Common/LoginV2.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Test/CategoryPart.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Test/DetailsPart.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Test/ListPart.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Test/TemplateFormEditor.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebSystem/WebSystem-MVC/Content/Parts/Test/WebUserControl1.ascx | obsolete_pattern | Default VS template placeholder control |
| WebSystem/WebSystem-MVC/Content/Plugins/fckeditor/editor/filemanager/connectors/aspx/config.ascx | not_applicable_vendor_path |  |
