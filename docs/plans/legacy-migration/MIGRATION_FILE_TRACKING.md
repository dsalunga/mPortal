# Legacy → Modern File Migration Tracking

**Auto-generated from:** `docs/plans/legacy-migration/inventory/legacy-source-tracking-portal.csv`  
**Total .ascx files:** 519  
**Completed (has modern equivalent):** 492  
**Not Applicable (no modern equivalent needed):** 27

## Summary by Module

| Module | Matched | Not Applicable | Total |
|---|---:|---:|---:|
| Portal/Binaries/Externals | 0 | 1 | 1 |
| Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp | 4 | 0 | 4 |
| Portal/WebParts/Integration/IntegrationParts | 123 | 4 | 127 |
| Portal/WebParts/SDKTest/SDKTest | 0 | 1 | 1 |
| Portal/WebParts/SystemParts/SystemParts | 134 | 2 | 136 |
| Portal/WebParts/SystemPartsG2/SystemPartsG2 | 52 | 0 | 52 |
| Portal/WebParts/SystemPartsG3/SystemPartsG3 | 2 | 8 | 10 |
| Portal/WebSystem/WebSystem-MVC/Content | 177 | 11 | 188 |
| **TOTAL** | **492** | **27** | **519** |

## Portal/Binaries/Externals
**0 matched** | **1 not applicable** | **1 total**

### Not Applicable (No Migration Needed)

| Legacy File | Status Basis | Notes |
|---|---|---|
| Binaries/Externals/config.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |

## Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp
**4 matched** | **0 not applicable** | **4 total**

### Completed (Modern Equivalent Exists)

| Legacy File | Modern File(s) | Basis |
|---|---|---|
| WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapter.ascx | Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapter.cshtml | controller_view |
| WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterHome.ascx | Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterHome.cshtml | controller_view |
| WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterTree.ascx | Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/ChapterTree.cshtml | controller_view |
| WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapters.ascx | Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Admin/Chapters.cshtml | controller_view |

## Portal/WebParts/Integration/IntegrationParts
**123 matched** | **4 not applicable** | **127 total**

### Completed (Modern Equivalent Exists)

| Legacy File | Modern File(s) | Basis |
|---|---|---|
| WebParts/Integration/IntegrationParts/Apps/Integration/Account/AccountInfo.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntAccountInfoViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Account/ChangePasswordV2.ascx | Portal/WebParts/Integration/IntegrationParts/Apps/Integration/Account/ChangePasswordV2.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Apps/Integration/Account/GetAccess.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntGetAccessViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Account/Login.ascx | Portal/WebSystem/WebSystem/ViewComponents/LoginViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Account/ProfileUpdate.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntProfileUpdateViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Account/RegisterSG.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntRegisterSGViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Account/RegisterV2.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntRegisterV2ViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/BibleReader/BibleAuth.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntBibleAuthViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/BibleReader/BibleBrowser.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntBibleBrowserViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/EventRegister/Attendee.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntAttendeeViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/EventRegister/AttendeeForm.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntAttendeeFormViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/EventRegister/Attendees.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntAttendeesViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/GlobalSwitch/GlobalSwitch.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntGlobalSwitchViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/LessonReviewer/AttendanceRequest.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntAttendanceRequestViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/LessonReviewer/AttendanceRequests.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntAttendanceRequestsViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/LessonReviewer/MemberSessions.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntMemberSessionsViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/CompositeGroupManager.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntCompositeGroupManagerViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/Controls/WebGroupTab.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntWebGroupTabViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/GroupEdit.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntGroupEditViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/GroupManager.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntGroupManagerViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/GroupOverview.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntGroupOverviewViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/GroupUserManager.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntGroupUserManagerViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/MemberAccess.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntMemberAccessViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/MemberEdit.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntMemberEditViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/MemberManager.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntMemberManagerViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/_CompositeGroupManager.ascx | Portal/WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/_CompositeGroupManager.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/_GroupUserManager.ascx | Portal/WebParts/Integration/IntegrationParts/Apps/Integration/MasterList/_GroupUserManager.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/ASOPMobile.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCASOPMobileViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/ASOPWinners.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCASOPWinnersViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/AdminCompetitionEdit.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCAdminCompetitionEditViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/AdminCompetitionManager.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCAdminCompetitionManagerViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/AdminComposerEdit.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCAdminComposerEditViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/AdminComposerManager.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCAdminComposerManagerViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/AdminInterpreterScoreManager.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCAdminInterpreterScoreManagerViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/AdminSongScoreManager.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCAdminSongScoreManagerViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/CandidateEdit.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCCandidateEditViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/CandidateManager.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCCandidateManagerViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/InstagramGallery.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/InstagramGalleryViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCConfirmVote.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCConfirmVoteViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCConfirmVoteV2.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCConfirmVoteV2ViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCFinalists.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCFinalistsViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCFinalistsV2.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCFinalistsV2ViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCJudgeV2.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCJudgeV2ViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCJudges.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCJudgesViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCJudgesMaster.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCJudgesMasterViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCVoteResult.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCVoteResultViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCVoteResult2.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCVoteResult2ViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCVoteResultV3.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCVoteResultV3ViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCVoteResultV4.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCVoteResultV4ViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCVoteV2.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCVoteV2ViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MCVoteV3.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCVoteV3ViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MessageBoard.ascx | Portal/WebSystem/WebSystem/ViewComponents/MessageBoardViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/MessageBoardV2.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCMessageBoardV2ViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/MusicCompetition/VoteManager.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/MCVoteManagerViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/AMSAuth.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntAMSAuthViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/AMSTest.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntAMSTestViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/AddressBookBrowser.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntAddressBookBrowserViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/AddressBookMini.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntAddressBookMiniViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/ChangePassword.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/ChangePasswordViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/ForgotPassword.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntForgotPasswordViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/LessonReviewer/ConfigAttendanceRequest.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntConfigAttendanceRequestViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/LessonReviewer/ConfigAttendanceRequests.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntConfigAttendanceRequestsViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/LessonReviewer/LogAttendance.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntLogAttendanceViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/LessonReviewer/NewsDashboard.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntNewsDashboardViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/LessonReviewer/Playback.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntPlaybackViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/LessonReviewer/RecoverSession.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntRecoverSessionViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/LessonReviewer/ServiceVideoDurationInput.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntServiceVideoDurationInputViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/LessonReviewer/SessionStart.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntSessionStartViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/MyAttendance.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntMyAttendanceViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/MyPreferences.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntMyPreferencesViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/ProfileUpdate.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntProfileUpdateViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/RequestManager.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntRequestManagerViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/RequestManagerRegister.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntRequestManagerRegisterViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/Sportsfest.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntSportsfestViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/SportsfestManager.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntSportsfestManagerViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/UpdateMyGroups.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntUpdateMyGroupsViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/UserProfileDetails.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntUserProfileDetailsViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/WebGroupUsers.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntWebGroupUsersViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Profile/ZimbraPreauth.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntZimbraPreauthViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ActivateAccount.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntActivateAccountViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/AdminMemberLinkSync.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntAdminMemberLinkSyncViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/AdminUtilities.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntAdminUtilitiesViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ConfigExternalMember.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntConfigExternalMemberViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ConfigExternalMembers.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntConfigExternalMembersViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ConfigMemberLink.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntConfigMemberLinkViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ConfigMemberLinkEdit.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntConfigMemberLinkEditViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ConfigMemberLinks.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntConfigMemberLinksViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ConfigMembers.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntConfigMembersViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ConventionRegistration.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntConventionRegistrationViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/ForgotPass.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntForgotPassViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/Login.ascx | Portal/WebSystem/WebSystem/ViewComponents/LoginViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/MemberVisitEntry.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntMemberVisitEntryViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/MemberVisitView.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntMemberVisitViewViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/RegistrationManager.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntRegistrationManagerViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/RegistrationV2.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntRegistrationV2ViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Registration/WebGroupList.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntWebGroupListViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Reminder/MemberReminders.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntMemberRemindersViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Reminder/ReminderTemplates.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntReminderTemplatesViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/ServicePicker.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntServicePickerViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Streaming/AccessRequest.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntAccessRequestViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Streaming/Setup.ascx | Portal/WebParts/Integration/IntegrationParts/Apps/Integration/Streaming/Setup.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Apps/Integration/Streaming/StreamingConsole.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntStreamingConsoleViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Apps/Integration/Streaming/UserManager.ascx | Portal/WebParts/Integration/IntegrationParts/Apps/Integration/Streaming/UserManager.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Themes/ASOP/Inside.ascx | Portal/WebParts/Integration/IntegrationParts/Themes/ASOP/Inside.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Themes/ASOP/asopv2-division-content.ascx | Portal/WebParts/Integration/IntegrationParts/Themes/ASOP/asopv2-division-content.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Themes/ASOP/asopv2-division-home.ascx | Portal/WebParts/Integration/IntegrationParts/Themes/ASOP/asopv2-division-home.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Themes/ASOP/v2/old/asopv2-content.ascx | Portal/WebParts/Integration/IntegrationParts/Themes/ASOP/v2/old/asopv2-content.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Themes/ASOP/v2/old/asopv2-home.ascx | Portal/WebParts/Integration/IntegrationParts/Themes/ASOP/v2/old/asopv2-home.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Themes/area51/InsideFull.ascx | Portal/WebParts/Integration/IntegrationParts/Themes/area51/InsideFull.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Themes/area51/InsideWithSidebar.ascx | Portal/WebParts/Integration/IntegrationParts/Themes/area51/InsideWithSidebar.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Themes/area51/Parts/EventCalendar/Event/EventViewBasic.ascx | Portal/WebParts/Integration/IntegrationParts/Themes/area51/Parts/EventCalendar/Event/EventViewBasic.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Themes/area51/Parts/EventCalendar/EventCalendar/CalendarView.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/CalendarViewViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Themes/area51/Parts/Profile/Profile/ForgotPassword.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntForgotPasswordViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Themes/area51/Parts/Profile/Profile/MyPreferences.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntMyPreferencesViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Themes/area51/Parts/Profile/Profile/ProfileUpdate.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntProfileUpdateViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Themes/area51/Parts/Profile/Profile/UpdateMyGroups.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntUpdateMyGroupsViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Themes/area51/Parts/Profile/Profile/UserProfileDetails.ascx | Portal/WebParts/Integration/IntegrationParts/ViewComponents/IntUserProfileDetailsViewComponent.cs | name_match |
| WebParts/Integration/IntegrationParts/Themes/mcgi_website/template_home.ascx | Portal/WebParts/Integration/IntegrationParts/Themes/mcgi_website/template_home.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Themes/mcgi_website/template_home2.ascx | Portal/WebParts/Integration/IntegrationParts/Themes/mcgi_website/template_home2.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Themes/mcgi_website/template_inside.ascx | Portal/WebParts/Integration/IntegrationParts/Themes/mcgi_website/template_inside.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Themes/mcgi_website/template_inside_full.ascx | Portal/WebParts/Integration/IntegrationParts/Themes/mcgi_website/template_inside_full.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Themes/mcgisg/mcgisg_home.ascx | Portal/WebParts/Integration/IntegrationParts/Themes/mcgisg/mcgisg_home.cshtml | manual_validated_viewcomponent_alias |
| WebParts/Integration/IntegrationParts/Themes/mcgisg/mcgisg_inside.ascx | Portal/WebParts/Integration/IntegrationParts/Themes/mcgisg/mcgisg_inside.cshtml | manual_validated_viewcomponent_alias |

### Not Applicable (No Migration Needed)

| Legacy File | Status Basis | Notes |
|---|---|---|
| WebParts/Integration/IntegrationParts/Themes/ASOP/Default.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/area51/Default.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/area51/index.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/Integration/IntegrationParts/Themes/mcgi_website/Default.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |

## Portal/WebParts/SDKTest/SDKTest
**0 matched** | **1 not applicable** | **1 total**

### Not Applicable (No Migration Needed)

| Legacy File | Status Basis | Notes |
|---|---|---|
| WebParts/SDKTest/SDKTest/WebParts/Test/WebUserControl1.ascx | obsolete_test_sample | SDK test/sample control; not production code |

## Portal/WebParts/SystemParts/SystemParts
**134 matched** | **2 not applicable** | **136 total**

### Completed (Modern Equivalent Exists)

| Legacy File | Modern File(s) | Basis |
|---|---|---|
| WebParts/SystemParts/SystemParts/AppBundle/Article/AdminPublication.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Article/AdminPublication.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Article/AdminSubscriptionManager.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Article/AdminSubscriptionManager.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Article/AdminTemplateComposer.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Article/AdminTemplateComposer.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Article/AdminTemplateManager.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Article/AdminTemplateManager.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Article/Article.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/ArticleViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Article/ArticleDashboard.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Article/ArticleDashboard.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Article/ArticleTagView.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Article/ArticleTagView.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Article/ConfigDashboard.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Article/ConfigDashboard.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Article/ConfigPublication.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Article/ConfigPublication.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Article/ConfigSendEmail.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Article/ConfigSendEmail.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Article/Controls/ArticleListPreview.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Article/Controls/ArticleListPreview.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Article/Templates/Multiple/Basic.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/ArticleMultipleMultiBasicViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/ArticleMultipleMultiBasic/Default.cshtml | manual_match |
| WebParts/SystemParts/SystemParts/AppBundle/Article/Templates/Multiple/FrontNews.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/ArticleMultipleMultiFrontnewsViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/ArticleMultipleMultiFrontnews/Default.cshtml | manual_match |
| WebParts/SystemParts/SystemParts/AppBundle/Article/Templates/Multiple/Newsroom.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/ArticleMultipleMultiNewsroomViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/ArticleMultipleMultiNewsroom/Default.cshtml | manual_match |
| WebParts/SystemParts/SystemParts/AppBundle/Article/Templates/Single/Basic.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/ArticleSingleSingleBasicViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/ArticleSingleSingleBasic/Default.cshtml | manual_match |
| WebParts/SystemParts/SystemParts/AppBundle/Article/Templates/Single/FrontNews.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/ArticleSingleSingleFrontnewsViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/ArticleSingleSingleFrontnews/Default.cshtml | manual_match |
| WebParts/SystemParts/SystemParts/AppBundle/Article/Templates/Single/Newsroom.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/ArticleSingleSingleNewsroomViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/ArticleSingleSingleNewsroom/Default.cshtml | manual_match |
| WebParts/SystemParts/SystemParts/AppBundle/Contact/AdminContactDetails.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Contact/AdminContactDetails.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Contact/AdminContactList.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Contact/AdminContactList.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Contact/AdminInquiriesDetails.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Contact/AdminInquiriesDetails.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Contact/AdminInquiriesList.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Contact/AdminInquiriesList.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Contact/ConfigInquiriesList.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Contact/ConfigInquiriesList.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Contact/ContactUs.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/ContactViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/Contact/Default.cshtml | manual_match |
| WebParts/SystemParts/SystemParts/AppBundle/Contact/ContactUsV2.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/Contactusv2ViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/Contactusv2/Default.cshtml | manual_match |
| WebParts/SystemParts/SystemParts/AppBundle/Container/ContainerDesigner.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/ContainerDesignerViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Container/ThreeColumn.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/ThreeColumnViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Container/ThreeRow.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Container/ThreeRow.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Container/TwoColumn.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/TwoColumnViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Container/TwoRow.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Container/TwoRow.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Content/Blog.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Content/Blog.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Content/ConfigContent.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Content/ConfigContent.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Content/ConfigOpen.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Content/ConfigOpen.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Content/Content.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/ContentViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Content/FeedBack.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Content/FeedBack.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Content/WM_Custom.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Content/WM_Custom.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminCalendar.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminCalendar.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminCalendarHome.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminCalendarHome.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminCalendarManager.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminCalendarManager.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminCategoryEdit.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminCategoryEdit.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminCategoryManager.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminCategoryManager.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminEvent.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminEvent.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminEventView.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminEventView.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminLocation.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminLocation.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminLocationManager.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminLocationManager.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminTemplateEdit.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminTemplateEdit.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminTemplateManager.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/AdminTemplateManager.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/CalendarEdit.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/CalendarEditViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/CalendarEventView.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/CalendarEventViewViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/CalendarManager.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/CalendarManagerViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/CalendarMiniView.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/CalendarMiniView.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/CalendarView.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/CalendarViewViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/EventViewBasic.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/EventCalendar/EventViewBasic.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/AdminRemoteLibraryEdit.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/FileManager/AdminRemoteLibraryEdit.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/AdminRemoteLibraryManager.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/FileManager/AdminRemoteLibraryManager.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/ConfigFileManager.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/FileManager/ConfigFileManager.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/Controls/Breadcrumb.ascx | Portal/WebSystem/WebSystem/ViewComponents/BreadcrumbViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/Controls/IndexerBreadcrumb.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/FileManager/Controls/IndexerBreadcrumb.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/CreateFolder.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/FileManager/CreateFolder.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/FileEditor.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/FileEditorViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/FileManager.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/FileManagerViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/FileView.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/FileManager/FileView.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/FolderView.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/FolderViewViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/IndexerDownload.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/FileManager/IndexerDownload.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/RemoteIndexRecentUpdates.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/FileManager/RemoteIndexRecentUpdates.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/RemoteIndexView.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/FileManager/RemoteIndexView.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/RemoteIndexerDownload.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/FileManager/RemoteIndexerDownload.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/RemoteLibraryView.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/FileManager/RemoteLibraryView.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/RenameFile.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/FileManager/RenameFile.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/TextEditor.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/FileManager/TextEditor.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/FileManager/UploadFiles.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/UploadFilesViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/GenericList.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/GenericListViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/ListData.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/GenericList/ListData.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/SM_Surveys.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/GenericList/SM_Surveys.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/Survey.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/SurveyViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/SurveyFinish.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/GenericList/SurveyFinish.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/SurveyStart.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/GenericList/SurveyStart.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/SurveyWelcome.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/GenericList/SurveyWelcome.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_CreateQuestionItems_07.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_CreateQuestionItems_07.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_CreateQuestion_06.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_CreateQuestion_06.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_CreateSurveyPage_04.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_CreateSurveyPage_04.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_CreateSurvey_02.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_CreateSurvey_02.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_Responses_09.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_Responses_09.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_Results_08.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_Results_08.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_SurveyPages_03.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_SurveyPages_03.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_SurveyQuestions_05.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_SurveyQuestions_05.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_Surveys_01.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/GenericList/WM_Surveys_01.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/AdminMenu.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/AdminMenuViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/AdminMenuEdit.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Menu/AdminMenuEdit.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/Breadcrumb.ascx | Portal/WebSystem/WebSystem/ViewComponents/BreadcrumbViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/CCMS_Menu_08.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Menu/CCMS_Menu_08.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/CCMS_Menu_09.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Menu/CCMS_Menu_09.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/CMS_ListingPage.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/ListingPageViewComponent.cs | mapped_replacement_exists |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/CMS_StdMenu_01.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Menu/CMS_StdMenu_01.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/CascadeMenu.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/CascadeMenuViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigDynamicMenu01.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigDynamicMenu01.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigDynamicMenuV2.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigDynamicMenuV2.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigLinkedMenu.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigLinkedMenu.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigLinkedMenuEdit.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigLinkedMenuEdit.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigMenuItemEdit.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigMenuItemEdit.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigMenuItems.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Menu/ConfigMenuItems.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/DropHighlightMenu.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Menu/DropHighlightMenu.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/GenericMenu.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/GenericMenuViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/ImportExport.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Menu/ImportExport.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/LinearMenu.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/LinearMenuViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/StdCascadeMenu.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Menu/StdCascadeMenu.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Messaging/SendEmail.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/SendEmailViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Navigation/Breadcrumb.ascx | Portal/WebSystem/WebSystem/ViewComponents/BreadcrumbViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Office/OfficeBrowser.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/OfficeBrowserViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Office/OfficeDetails.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Office/OfficeDetails.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Others/DateDisplay/CMS_DateDisplay.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/DateDisplayViewComponent.cs | mapped_replacement_exists |
| WebParts/SystemParts/SystemParts/AppBundle/Others/DateDisplay/DateDisplay.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/DateDisplayViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Others/Script/BodyEvent.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Others/Script/BodyEvent.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Others/Title/SiteSection.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/SiteSectionViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/AdminAlbum.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Photo/AdminAlbum.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/AdminAlbumEdit.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Photo/AdminAlbumEdit.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/AdminPhotos.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/AdminPhotosViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/AnimatedBanner.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/AnimatedBannerViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/CCMS_Category.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Photo/CCMS_Category.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/CCMS_Gallery_02.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Photo/CCMS_Gallery_02.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/CMS_Category.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Photo/CMS_Category.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/CMS_Gallery.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/GalleryViewComponent.cs | manual_validated_viewcomponent_map |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/ConfigAnimatedBanner.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Photo/ConfigAnimatedBanner.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/Controls/Album.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/PhotoalbumViewComponent.cs + Portal/WebParts/SystemParts/SystemParts/Views/Shared/Components/Photoalbum/Default.cshtml | manual_match |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/Controls/FancyBoxThumbnails.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Photo/Controls/FancyBoxThumbnails.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/Controls/FullView.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Photo/Controls/FullView.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/Controls/SlideShow.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Photo/Controls/SlideShow.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/Controls/Thumbnails.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Photo/Controls/Thumbnails.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/InstagramGallery.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/InstagramGalleryViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Photo/PictureGallery.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/PictureGalleryViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Search/Search.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/SearchViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/Search/SearchResults.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Search/SearchResults.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Search/Search_ROG.ascx | Portal/WebParts/SystemParts/SystemParts/AppBundle/Search/Search_ROG.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemParts/SystemParts/AppBundle/Twitter/TwitterHelper.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/TwitterHelperViewComponent.cs | name_match |
| WebParts/SystemParts/SystemParts/AppBundle/WeeklyScheduler/WeeklyScheduler.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/WeeklySchedulerViewComponent.cs | name_match |

### Not Applicable (No Migration Needed)

| Legacy File | Status Basis | Notes |
|---|---|---|
| WebParts/SystemParts/SystemParts/AppBundle/Article/Controls/AdminTabControl.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |
| WebParts/SystemParts/SystemParts/AppBundle/Menu/Controls/AdminTabControl.ascx | obsolete_webforms | WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed) |

## Portal/WebParts/SystemPartsG2/SystemPartsG2
**52 matched** | **0 not applicable** | **52 total**

### Completed (Modern Equivalent Exists)

| Legacy File | Modern File(s) | Basis |
|---|---|---|
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/Ad.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/AdViewComponent.cs | name_match |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/CMS_Ad.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/AdManagerViewComponent.cs | manual_validated_viewcomponent_map |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_ad_02.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_ad_02.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_adcategories_01.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_adcategories_01.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_adcategory_02.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_adcategory_02.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_aditem_04.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_aditem_04.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_aditems_03.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_aditems_03.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_adwriter_01.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ads/content_adwriter_01.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Ajax/AjaxScriptManager.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/AjaxScriptManagerViewComponent.cs | name_match |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/BasicList/BasicList.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/BasicListViewComponent.cs | name_match |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/BasicList/CMS_01_BasicList.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/BasicList/CMS_01_BasicList.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/BasicList/ItemTemplates/Single.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/BasicList/ItemTemplates/Single.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/BasicList/ItemTemplates/TwoColumns.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/BasicList/ItemTemplates/TwoColumns.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/BasicList/ItemTemplates/TwoRows.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/BasicList/ItemTemplates/TwoRows.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/Controls/BasicList.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/BasicListViewComponent.cs | name_match |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/Controls/BasicList_A3.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/Controls/BasicList_A3.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/Controls/DetailedList.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/Controls/DetailedList.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/Controls/GroupByYear.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/Controls/GroupByYear.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/Downloads.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/DownloadsViewComponent.cs | name_match |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/PM_Downloads_Edit_02.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/PM_Downloads_Edit_02.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/PM_Downloads_Master_01.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/PM_Downloads_Master_01.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/SM_Downloads_03.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Download/SM_Downloads_03.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/FlashBanner/Renderer.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/FlashBanner/Renderer.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Media/cms_media.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/MediaAdminViewComponent.cs | manual_validated_viewcomponent_map |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Media/content_media_01.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Media/content_media_01.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Media/content_media_02.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Media/content_media_02.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Media/fullcontent.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Media/fullcontent.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Media/mediahome.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Media/mediahome.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Media/medialist.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Media/medialist.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Misc/DateDisplay/CMS_DateDisplay.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/DateDisplayViewComponent.cs | mapped_replacement_exists |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Misc/DateDisplay/DateDisplay.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/DateDisplayViewComponent.cs | name_match |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Misc/Script/BodyEvent.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Misc/Script/BodyEvent.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Misc/Title/SiteSection.ascx | Portal/WebParts/SystemParts/SystemParts/ViewComponents/SiteSectionViewComponent.cs | name_match |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Newsletter/AdminNewsletter.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Newsletter/AdminNewsletter.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Newsletter/CCMS_MailSender.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Newsletter/CCMS_MailSender.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Newsletter/CCMS_eNewsletter.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Newsletter/CCMS_eNewsletter.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Newsletter/CMS_Enewsletter.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Newsletter/CMS_Enewsletter.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Newsletter/Newsletter.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/NewsletterViewComponent.cs | name_match |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Newsletter/eNewsLetter.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Newsletter/eNewsLetter.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/CMS_ListingPage.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/ListingPageViewComponent.cs | mapped_replacement_exists |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/ListingPage.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/ListingPageViewComponent.cs | name_match |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/listsimplehoriz1.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/listsimplehoriz1.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/listsimplevert1.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/listsimplevert1.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/splashnavigator.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/splashnavigator.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/subsiteslist.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/subsiteslist.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/subsiteslist_nb.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/SiteList/subsiteslist_nb.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/Comments.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/CommentsViewComponent.cs | name_match |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/ConfigWall.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/ConfigWall.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/MobileWall.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/MobileWall.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/Plugins/GenericWallPost.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/Plugins/GenericWallPost.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/Plugins/ProfileUpdateWall.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/Plugins/ProfileUpdateWall.cshtml | manual_validated_viewcomponent_alias |
| WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/Social/Wall.ascx | Portal/WebParts/SystemPartsG2/SystemPartsG2/ViewComponents/WallViewComponent.cs | name_match |

## Portal/WebParts/SystemPartsG3/SystemPartsG3
**2 matched** | **8 not applicable** | **10 total**

### Completed (Modern Equivalent Exists)

| Legacy File | Modern File(s) | Basis |
|---|---|---|
| WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs/JobSearch.ascx | Portal/WebParts/SystemPartsG3/SystemPartsG3/ViewComponents/JobSearchViewComponent.cs | name_match |
| WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/Jobs/JobsList.ascx | Portal/WebParts/SystemPartsG3/SystemPartsG3/ViewComponents/JobsListViewComponent.cs | name_match |

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

## Portal/WebSystem/WebSystem-MVC/Content
**177 matched** | **11 not applicable** | **188 total**

### Completed (Modern Equivalent Exists)

| Legacy File | Modern File(s) | Basis |
|---|---|---|
| WebSystem/WebSystem-MVC/Content/Controls/Breadcrumb.ascx | Portal/WebSystem/WebSystem/ViewComponents/BreadcrumbViewComponent.cs | name_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Agent/Dashboard.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/AgentDashboardViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/AgentDashboard/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Agent/TaskEditor.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/TaskEditorViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/TaskEditor/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Agent/TaskManager.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Agent/TaskManager.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Agent/TaskView.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Agent/TaskView.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Breadcrumb.ascx | Portal/WebSystem/WebSystem/ViewComponents/BreadcrumbViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/Breadcrumb/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/ChangePasswordForm.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/ChangePasswordViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ChangePassword/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/DesignMode.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/DesignModeViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/DesignMode/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/ElementDesigner.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/ElementDesignerViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ElementDesigner/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/ManagementSecurityOption.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/ManagementSecurityOption.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/PanelDesigner.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/PanelDesignerViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/PanelDesigner/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/ParameterSetSelector.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/ParameterSetSelector.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/SaveInFolder.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/SaveInFolder.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/TreeControls.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/TreeControls.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/UserProfileForm.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/UserProfileForm.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/UserRolesForm.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/UserRolesForm.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebGenericTab.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/WebGenericTab.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebGroupTab.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/WebGroupTab.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebMasterPageTab.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/WebMasterPageTab.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebPagePanelTab.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/WebPagePanelTab.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebPageTab.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/WebPageTab.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebPagesControl.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebPagesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPages/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebPartControlTab.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/WebPartControlTab.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebPartControlTemplateTab.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/WebPartControlTemplateTab.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebPartTab.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/WebPartTab.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebRoleTab.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/WebRoleTab.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebSiteElementSelector.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/WebSiteElementSelector.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebSiteTab.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/WebSiteTab.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebSitesControl.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebSitesControlViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebSitesControl/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebTemplateHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/WebTemplateHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebThemeHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/WebThemeHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/WebUserTab.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Controls/WebUserTab.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Dashboard.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/AdminDashboardViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/AdminDashboard/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/HeaderNavbar.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/HeaderNavbarViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/HeaderNavbar/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/HeaderPanel.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/HeaderPanelViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/HeaderPanel/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Manager/Home.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Manager/Home.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/SubscriptionManager.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/SubscriptionManagerViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/SubscriptionManager/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebAddress.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Misc/WebAddress.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebAddresses.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebAddressesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebAddresses/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebOffice.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Misc/WebOffice.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebOfficeHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Misc/WebOfficeHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebOfficeTree.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Misc/WebOfficeTree.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebOffices.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebOfficesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebOffices/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebParameter.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Misc/WebParameter.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebParameterSet.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Misc/WebParameterSet.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebParameterSetHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Misc/WebParameterSetHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebParameterSets.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Misc/WebParameterSets.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebParameters.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebParametersViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebParameters/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebParametersXml.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Misc/WebParametersXml.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebResource.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Misc/WebResource.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebResourceManager.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Misc/WebResourceManager.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Misc/WebResources.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebResourcesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebResources/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/ChangePassword.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/ChangePasswordViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ChangePassword/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/CreateUser.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/CreateUserViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/CreateUser/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/UserActivities.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Security/UserActivities.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/UserProfile.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/UserProfileViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/UserProfile/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebGlobalPolicy.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Security/WebGlobalPolicy.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebGlobalPolicyHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Security/WebGlobalPolicyHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebGroup.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Security/WebGroup.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebGroupHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Security/WebGroupHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebGroupTree.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Security/WebGroupTree.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebGroupUsers.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Security/WebGroupUsers.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebGroups.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebGroupsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebGroups/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebObjectPermissions.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Security/WebObjectPermissions.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebPermissions.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebPermissionsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPermissions/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebRoleHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Security/WebRoleHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebRoles.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebRolesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebRoles/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebSecurity.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebSecurityTreeViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebSecurityTree/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebSecurityTree.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebSecurityTreeViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebSecurityTree/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebUserGroups.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Security/WebUserGroups.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebUserHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Security/WebUserHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebUserPermissions.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Security/WebUserPermissions.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebUserRoles.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Security/WebUserRoles.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Security/WebUsers.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebUsersViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebUsers/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/SideBar.ascx | Portal/WebSystem/WebSystem/ViewComponents/SideBarViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/SideBar/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/SiteMap.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/SiteMapViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/SiteMap/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebSkin.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Template/WebSkin.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebSkinHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Template/WebSkinHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebSkins.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebSkinsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebSkins/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebTemplate.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Template/WebTemplate.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebTemplateEditor.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebTemplateEditorViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebTemplateEditor/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebTemplateHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Template/WebTemplateHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebTemplatePanel.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Template/WebTemplatePanel.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebTemplatePanels.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebTemplatePanelsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebTemplatePanels/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebTemplateVersions.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Template/WebTemplateVersions.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebTemplates.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebTemplatesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebTemplates/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebTheme.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Template/WebTheme.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebThemeHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Template/WebThemeHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Template/WebThemes.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebThemesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebThemes/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/DataStoreManager.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Tools/DataStoreManager.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/DataSyncDashboard.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/DataSyncDashboardViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/DataSyncDashboard/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/DataSyncManager.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Tools/DataSyncManager.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/EventLogManager.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/EventLogManagerViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/EventLogManager/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/ImportExportHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Tools/ImportExportHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/ImportExportPage.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Tools/ImportExportPage.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/ImportExportParameterSets.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Tools/ImportExportParameterSets.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/MessageQueueManager.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Tools/MessageQueueManager.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/PerformanceLogManager.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/PerformanceLogManagerViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/PerformanceLogManager/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/QueryAnalyzer.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/QueryAnalyzerViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/QueryAnalyzer/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/ShortUrlEdit.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Tools/ShortUrlEdit.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/ShortUrlManager.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/ShortUrlManagerViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ShortUrlManager/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/SmtpAnalyzer.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Tools/SmtpAnalyzer.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/UserSessionManager.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Tools/UserSessionManager.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebDataExplorer.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Tools/WebDataExplorer.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebDataRows.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Tools/WebDataRows.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebFolder.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Tools/WebFolder.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebFolderTree.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Tools/WebFolderTree.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebObjectEdit.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Tools/WebObjectEdit.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebRegistry.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebRegistryViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebRegistry/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebRegistryEntry.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Tools/WebRegistryEntry.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebRegistryTree.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Tools/WebRegistryTree.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/Tools/WebToolsTree.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/Tools/WebToolsTree.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/TreePanel.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/TreePanel.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebOpen.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebOpen.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPart.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebPart/WebPart.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartAdmin.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebPart/WebPartAdmin.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartAdminEntry.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebPart/WebPartAdminEntry.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartAdminHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebPart/WebPartAdminHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartAdminMgmt.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebPart/WebPartAdminMgmt.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartConfig.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebPartConfigViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPartConfig/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartConfigEntry.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebPart/WebPartConfigEntry.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartControl.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebPart/WebPartControl.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartControlHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebPart/WebPartControlHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartControlTemplate.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebPart/WebPartControlTemplate.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartControlTemplateHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebPart/WebPartControlTemplateHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartControlTemplates.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebPartControlTemplatesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPartControlTemplates/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartControls.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebPartControlsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPartControls/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebPart/WebPartHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartTemplatePanel.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebPart/WebPartTemplatePanel.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartTemplatePanels.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebPart/WebPartTemplatePanels.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebPartTree.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebPartTreeViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPartTree/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPart/WebParts.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebPartsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebParts/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebPartPreview.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebPartPreviewViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPartPreview/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebChildPages.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebPagesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPages/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebChildSites.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebChildSitesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebChildSites/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebIdentities.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebIdentitiesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebIdentities/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebIdentity.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebSites/WebIdentity.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebLinkedParts.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebSites/WebLinkedParts.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebMasterPage.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebSites/WebMasterPage.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebMasterPageHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebSites/WebMasterPageHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebMasterPages.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebMasterPagesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebMasterPages/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPage.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebPageViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPage/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPageElement.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebSites/WebPageElement.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPageElementHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebSites/WebPageElementHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPageElements.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebPageElementsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPageElements/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPageHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebSites/WebPageHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPagePanel.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebSites/WebPagePanel.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPagePanelHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebSites/WebPagePanelHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPagePanels.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebPagePanelsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPagePanels/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebPages.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebPagesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebPages/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebSite.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebSiteViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebSite/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebSiteHome.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebSites/WebSiteHome.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebSiteTree.ascx | Portal/WebSystem/WebSystem/Content/Parts/Central/WebSites/WebSiteTree.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Central/WebSites/WebSites.ascx | Portal/WebSystem/WebSystem/ViewComponents/Admin/WebSitesViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/WebSites/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Common/AdminCommentManager.ascx | Portal/WebSystem/WebSystem/Content/Parts/Common/AdminCommentManager.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Common/Comments.ascx | Portal/WebSystem/WebSystem/ViewComponents/CommentsViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/Comments/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Common/Login.ascx | Portal/WebSystem/WebSystem/ViewComponents/LoginViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/Login/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Common/LoginV2.ascx | Portal/WebSystem/WebSystem/Content/Parts/Common/LoginV2.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Common/MessageBoard.ascx | Portal/WebSystem/WebSystem/ViewComponents/MessageBoardViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/MessageBoard/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Common/TriggerTask.ascx | Portal/WebSystem/WebSystem/ViewComponents/TriggerTaskViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/TriggerTask/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Common/UserPhotoUpload.ascx | Portal/WebSystem/WebSystem/ViewComponents/UserPhotoUploadViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/UserPhotoUpload/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Parts/Test/CategoryPart.ascx | Portal/WebSystem/WebSystem/Content/Parts/Test/CategoryPart.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Test/DetailsPart.ascx | Portal/WebSystem/WebSystem/Content/Parts/Test/DetailsPart.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Test/ListPart.ascx | Portal/WebSystem/WebSystem/Content/Parts/Test/ListPart.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Test/TemplateFormEditor.ascx | Portal/WebSystem/WebSystem/Content/Parts/Test/TemplateFormEditor.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Parts/Test/WebUserControl1.ascx | Portal/WebSystem/WebSystem/Content/Parts/Test/WebUserControl1.cshtml | manual_validated_path_equivalent |
| WebSystem/WebSystem-MVC/Content/Themes/Basic/Basic.ascx | Portal/WebSystem/WebSystem/ViewComponents/ThemeBasicViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeBasic/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Themes/Central/Basic.ascx | Portal/WebSystem/WebSystem/ViewComponents/ThemeCentralBasicViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeCentralBasic/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Themes/Central/Responsive.ascx | Portal/WebSystem/WebSystem/ViewComponents/ThemeCentralResponsiveViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeCentralResponsive/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Themes/Central/ResponsiveWithSidebar.ascx | Portal/WebSystem/WebSystem/ViewComponents/ThemeCentralResponsiveWithSidebarViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeCentralResponsiveWithSidebar/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Themes/Default/Default.ascx | Portal/WebSystem/WebSystem/ViewComponents/ThemeDefaultViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeDefault/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Themes/Default/ForAjaxControlToolkit.ascx | Portal/WebSystem/WebSystem/ViewComponents/ThemeDefaultForAjaxControlToolkitViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeDefaultForAjaxControlToolkit/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Themes/bootstrap3/navbar-fixed-top.ascx | Portal/WebSystem/WebSystem/ViewComponents/ThemeBootstrap3NavbarViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeBootstrap3Navbar/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Themes/test/Parts/Article/Default/Article.ascx | Portal/WebSystem/WebSystem/ViewComponents/ThemeTestArticleViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeTestArticle/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Themes/test/Parts/Contact/Contact/ContactUs.ascx | Portal/WebSystem/WebSystem/ViewComponents/ThemeTestContactViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeTestContact/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Themes/test/StandAloneTemplate.ascx | Portal/WebSystem/WebSystem/ViewComponents/ThemeTestStandaloneViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeTestStandalone/Default.cshtml | manual_match |
| WebSystem/WebSystem-MVC/Content/Themes/test/test.ascx | Portal/WebSystem/WebSystem/ViewComponents/ThemeTestViewComponent.cs + Portal/WebSystem/WebSystem/Views/Shared/Components/ThemeTest/Default.cshtml | manual_match |

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
| WebSystem/WebSystem-MVC/Content/Plugins/fckeditor/editor/filemanager/connectors/aspx/config.ascx | not_applicable_vendor_path |  |
